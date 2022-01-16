using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000E0 RID: 224
public class StartUpCode : MonoBehaviour
{
	// Token: 0x060006C6 RID: 1734 RVA: 0x000308E4 File Offset: 0x0002EAE4
	private void Awake()
	{
		if (StartUpCode.pRanOnce)
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		StartUpCode.pRanOnce = true;
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		Data.Init(this.globalsText.text, this.sharedText.text);
		if (SiteLock.IsSiteLocked())
		{
			Debug.Log("Site lock: " + Application.absoluteURL);
			Application.LoadLevel("SiteLock");
			return;
		}
		if (Data.loadUserData)
		{
			UserData.Load();
		}
		GameData.Start();
		Scripts.Init();
		Application.targetFrameRate = Data.targetFrameRate;
		GameObject gameObject = new GameObject("GameAudio");
		gameObject.AddComponent<AudioListener>();
		Scripts.audioManager = gameObject.AddComponent<GlobalAudio>();
		UnityEngine.Object.DontDestroyOnLoad(gameObject);
	}

	// Token: 0x060006C7 RID: 1735 RVA: 0x000309A4 File Offset: 0x0002EBA4
	private void OnApplicationPause(bool aPaused)
	{
		if (!SiteLock.IsSiteLocked())
		{
			GameData.OnApplicationPause(aPaused);
		}
	}

	// Token: 0x060006C8 RID: 1736 RVA: 0x000309B8 File Offset: 0x0002EBB8
	private void OnApplicationQuit()
	{
		foreach (KeyValuePair<string, WWW> keyValuePair in Data.stringToBundle)
		{
			if (keyValuePair.Value != null && keyValuePair.Value.isDone)
			{
				keyValuePair.Value.assetBundle.Unload(false);
			}
		}
	}

	// Token: 0x040006ED RID: 1773
	private static bool pRanOnce;

	// Token: 0x040006EE RID: 1774
	public TextAsset globalsText;

	// Token: 0x040006EF RID: 1775
	public TextAsset sharedText;
}
