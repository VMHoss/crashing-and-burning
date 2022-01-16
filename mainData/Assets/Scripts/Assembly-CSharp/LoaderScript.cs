using System;
using UnityEngine;

// Token: 0x020000FC RID: 252
public class LoaderScript : MonoBehaviour
{
	// Token: 0x0600078C RID: 1932 RVA: 0x000399A8 File Offset: 0x00037BA8
	private void Awake()
	{
		if (GameObject.Find("StartUp") == null)
		{
			Debug.LogWarning("Started in Loading scene... Going to StartUp scene");
			Application.LoadLevel("StartUp");
			return;
		}
		Languages.Init();
	}

	// Token: 0x0600078D RID: 1933 RVA: 0x000399DC File Offset: 0x00037BDC
	private void Start()
	{
		Debug.Log("Loader Start");
		this.loaderPanel.SetActive(true);
		this.localization = GameObject.Find("Localization").GetComponent<Localization>();
		GenericFunctionsScript.Fade("FromBlackToZero");
		if (Data.requiredBundles.Count > 0)
		{
			this.pBundleLoader = new BundleLoader();
			this.SetDownloads();
			this.scriptStartedTime = Time.realtimeSinceStartup;
		}
		else
		{
			this.scriptStartedTime = Time.realtimeSinceStartup;
		}
	}

	// Token: 0x0600078E RID: 1934 RVA: 0x00039A5C File Offset: 0x00037C5C
	private void Update()
	{
		if (!Data.useAssetBundles || this.pBundleLoader == null || this.pBundleLoader.Update())
		{
			if (Time.realtimeSinceStartup - this.scriptStartedTime > 1.5f)
			{
				Data.requiredBundles.Clear();
				Application.LoadLevel("Game");
			}
		}
		else
		{
			float progress = this.pBundleLoader.GetProgress();
			this.loadingBarFill.fillAmount = progress;
		}
	}

	// Token: 0x0600078F RID: 1935 RVA: 0x00039AD8 File Offset: 0x00037CD8
	private void SetDownloads()
	{
		Data.requiredBundles.Add(new BundleEntry("Shared", "Shared"));
		Data.requiredBundles.Add(new BundleEntry("Levels", "Level" + Data.level));
		Data.requiredBundles.Add(new BundleEntry("Vehicles", "Vehicles"));
		this.pBundleLoader.StartDownloads();
	}

	// Token: 0x0400080F RID: 2063
	public GameObject loaderCamera;

	// Token: 0x04000810 RID: 2064
	public GameObject loaderPanel;

	// Token: 0x04000811 RID: 2065
	public Localization localization;

	// Token: 0x04000812 RID: 2066
	public UIFilledSprite loadingBarFill;

	// Token: 0x04000813 RID: 2067
	public UISprite loadingBackground;

	// Token: 0x04000814 RID: 2068
	public float scriptStartedTime;

	// Token: 0x04000815 RID: 2069
	private BundleLoader pBundleLoader;
}
