using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000055 RID: 85
[AddComponentMenu("NGUI/Internal/Localization")]
public class Localization : MonoBehaviour
{
	// Token: 0x1700004A RID: 74
	// (get) Token: 0x0600028B RID: 651 RVA: 0x00011584 File Offset: 0x0000F784
	public static Localization instance
	{
		get
		{
			if (Localization.mInstance == null)
			{
				Localization.mInstance = (UnityEngine.Object.FindObjectOfType(typeof(Localization)) as Localization);
				if (Localization.mInstance == null)
				{
					GameObject gameObject = new GameObject("_Localization");
					UnityEngine.Object.DontDestroyOnLoad(gameObject);
					Localization.mInstance = gameObject.AddComponent<Localization>();
				}
			}
			return Localization.mInstance;
		}
	}

	// Token: 0x1700004B RID: 75
	// (get) Token: 0x0600028C RID: 652 RVA: 0x000115EC File Offset: 0x0000F7EC
	// (set) Token: 0x0600028D RID: 653 RVA: 0x000115F4 File Offset: 0x0000F7F4
	public string currentLanguage
	{
		get
		{
			return this.mLanguage;
		}
		set
		{
			if (this.mLanguage != value)
			{
				this.startingLanguage = value;
				if (!string.IsNullOrEmpty(value))
				{
					if (this.languages != null)
					{
						int i = 0;
						int num = this.languages.Length;
						while (i < num)
						{
							TextAsset textAsset = this.languages[i];
							if (textAsset != null && textAsset.name == value)
							{
								this.Load(textAsset);
								return;
							}
							i++;
						}
					}
					TextAsset textAsset2 = Resources.Load(value, typeof(TextAsset)) as TextAsset;
					if (textAsset2 != null)
					{
						this.Load(textAsset2);
						return;
					}
				}
				this.mDictionary.Clear();
				PlayerPrefs.DeleteKey("Language");
			}
		}
	}

	// Token: 0x0600028E RID: 654 RVA: 0x000116B8 File Offset: 0x0000F8B8
	private void Awake()
	{
		if (Localization.mInstance == null)
		{
			Localization.mInstance = this;
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			this.currentLanguage = PlayerPrefs.GetString("Language", this.startingLanguage);
			if (string.IsNullOrEmpty(this.mLanguage) && this.languages != null && this.languages.Length > 0)
			{
				this.currentLanguage = this.languages[0].name;
			}
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x0600028F RID: 655 RVA: 0x00011748 File Offset: 0x0000F948
	private void OnEnable()
	{
		if (Localization.mInstance == null)
		{
			Localization.mInstance = this;
		}
	}

	// Token: 0x06000290 RID: 656 RVA: 0x00011760 File Offset: 0x0000F960
	private void OnDestroy()
	{
		if (Localization.mInstance == this)
		{
			Localization.mInstance = null;
		}
	}

	// Token: 0x06000291 RID: 657 RVA: 0x00011778 File Offset: 0x0000F978
	private void Load(TextAsset asset)
	{
		this.mLanguage = asset.name;
		PlayerPrefs.SetString("Language", this.mLanguage);
		ByteReader byteReader = new ByteReader(asset);
		this.mDictionary = byteReader.ReadDictionary();
		UIRoot.Broadcast("OnLocalize", this);
	}

	// Token: 0x06000292 RID: 658 RVA: 0x000117C0 File Offset: 0x0000F9C0
	public string Get(string key)
	{
		string text;
		if (this.mDictionary.TryGetValue(key + " Mobile", out text))
		{
			return text;
		}
		return (!this.mDictionary.TryGetValue(key, out text)) ? key : text;
	}

	// Token: 0x06000293 RID: 659 RVA: 0x00011808 File Offset: 0x0000FA08
	public static string Localize(string key)
	{
		return (!(Localization.instance != null)) ? key : Localization.instance.Get(key);
	}

	// Token: 0x040002FC RID: 764
	private static Localization mInstance;

	// Token: 0x040002FD RID: 765
	public string startingLanguage = "English";

	// Token: 0x040002FE RID: 766
	public TextAsset[] languages;

	// Token: 0x040002FF RID: 767
	private Dictionary<string, string> mDictionary = new Dictionary<string, string>();

	// Token: 0x04000300 RID: 768
	private string mLanguage;
}
