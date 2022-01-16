using System;
using System.Collections;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

// Token: 0x02000003 RID: 3
public class CJComm : CJDefinitions
{
	// Token: 0x06000011 RID: 17 RVA: 0x00002594 File Offset: 0x00000794
	private void Start()
	{
		if (Debug.isDebugBuild)
		{
			this.developerBuild = true;
		}
		if (this.developerKey == null && this.developerBuild)
		{
			Debug.LogError("Developer Key isn't set");
			this.Call("console.error('### ERROR: Developer Key is not set');");
		}
		if (this.gameKey == null && this.developerBuild)
		{
			Debug.LogError("Game Key isn't set");
			this.Call("console.error('### ERROR: Game Key is not set');");
		}
	}

	// Token: 0x06000012 RID: 18 RVA: 0x00002608 File Offset: 0x00000808
	public void showError(string param)
	{
		this.Call("console.error('### ERROR: " + param + "');");
	}

	// Token: 0x06000013 RID: 19 RVA: 0x00002620 File Offset: 0x00000820
	public Hashtable Read(string valor)
	{
		valor = valor.Replace("\"", string.Empty);
		valor = valor.Replace("}", string.Empty);
		valor = valor.Replace("{", string.Empty);
		string[] array = valor.Split(new char[]
		{
			':',
			','
		});
		Hashtable hashtable = new Hashtable();
		string key = null;
		for (int i = 0; i < array.Length; i++)
		{
			if (i % 2 == 0)
			{
				key = array[i];
			}
			else
			{
				hashtable.Add(key, array[i]);
			}
		}
		return hashtable;
	}

	// Token: 0x06000014 RID: 20 RVA: 0x000026B4 File Offset: 0x000008B4
	public void Call(string valor)
	{
		Application.ExternalEval(valor);
		if (this.developerBuild)
		{
			Debug.Log(valor);
		}
	}

	// Token: 0x06000015 RID: 21 RVA: 0x000026D0 File Offset: 0x000008D0
	public static string Encode64(string toEncode)
	{
		byte[] bytes = Encoding.UTF8.GetBytes(toEncode);
		return Convert.ToBase64String(bytes);
	}

	// Token: 0x06000016 RID: 22 RVA: 0x000026F4 File Offset: 0x000008F4
	private string EncodeMd5(string valor)
	{
		UTF8Encoding utf8Encoding = new UTF8Encoding();
		byte[] bytes = utf8Encoding.GetBytes(valor);
		MD5CryptoServiceProvider md5CryptoServiceProvider = new MD5CryptoServiceProvider();
		byte[] array = md5CryptoServiceProvider.ComputeHash(bytes);
		string text = string.Empty;
		for (int i = 0; i < array.Length; i++)
		{
			text += Convert.ToString(array[i], 16).PadLeft(2, '0');
		}
		return text.PadLeft(32, '0');
	}

	// Token: 0x06000017 RID: 23 RVA: 0x00002764 File Offset: 0x00000964
	public string salt()
	{
		if (this.developerKey == null)
		{
			if (this.developerBuild)
			{
				Debug.LogError("Developer Key isn't set, salt generation failed");
				this.Call("console.error('### ERROR: Developer Key is not set, salt generation failed')");
			}
			return null;
		}
		if (this.gameKey == null)
		{
			if (this.developerBuild)
			{
				Debug.LogError("Game Key isn't set, salt generation failed");
				this.Call("console.error('### ERROR: Game Key is not set, salt generation failed')");
			}
			return null;
		}
		string text = DateTime.Now.ToString("HH:mm:ss tt");
		return CJComm.Encode64(string.Concat(new string[]
		{
			this.developerKey,
			"/",
			this.gameKey,
			"/",
			text,
			"/",
			this.EncodeMd5(text + "/" + this.developerKey)
		}));
	}

	// Token: 0x0400000F RID: 15
	public string developerKey;

	// Token: 0x04000010 RID: 16
	public string gameKey;

	// Token: 0x04000011 RID: 17
	public string username = "undefined";

	// Token: 0x04000012 RID: 18
	public bool isSignedIn;
}
