using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000D2 RID: 210
public class BundleLoader
{
	// Token: 0x06000655 RID: 1621 RVA: 0x0002D8CC File Offset: 0x0002BACC
	public BundleLoader()
	{
		this.pBundleDownloads = new List<BundleEntry>();
		this.pDownloadIndex = 0;
		this.pStartedDownloading = false;
	}

	// Token: 0x06000656 RID: 1622 RVA: 0x0002D8F0 File Offset: 0x0002BAF0
	public void AddDownload(BundleEntry aBundleEntry)
	{
		this.pBundleDownloads.Add(aBundleEntry);
	}

	// Token: 0x06000657 RID: 1623 RVA: 0x0002D900 File Offset: 0x0002BB00
	public float GetProgress()
	{
		float num = (float)this.pDownloadIndex / (float)this.pBundleDownloads.Count;
		if (this.pCurrentDownload != null)
		{
			num += this.pCurrentDownload.progress * (1f / (float)this.pBundleDownloads.Count);
		}
		return num;
	}

	// Token: 0x06000658 RID: 1624 RVA: 0x0002D950 File Offset: 0x0002BB50
	public void StartDownloads()
	{
		Debug.Log("Following bundles are required and may be loaded if not loaded already:");
		foreach (BundleEntry bundleEntry in this.pBundleDownloads)
		{
			Debug.Log("Bundle: " + bundleEntry.mapName + "\\" + bundleEntry.bundleName);
		}
		this.pDownloadIndex = 0;
		this.pCurrentDownload = null;
	}

	// Token: 0x06000659 RID: 1625 RVA: 0x0002D9E8 File Offset: 0x0002BBE8
	public bool Update()
	{
		if (this.pStartedDownloading)
		{
			if (this.pCurrentDownload.isDone)
			{
				this.pDownloadIndex++;
				return !this.StartNextDownload();
			}
		}
		else
		{
			this.StartNextDownload();
			if (this.pCurrentDownload == null)
			{
				return true;
			}
			this.pStartedDownloading = true;
		}
		return false;
	}

	// Token: 0x0600065A RID: 1626 RVA: 0x0002DA50 File Offset: 0x0002BC50
	private bool StartNextDownload()
	{
		while (this.pDownloadIndex < this.pBundleDownloads.Count)
		{
			if (!Data.stringToBundle.ContainsKey(this.pBundleDownloads[this.pDownloadIndex].bundleName))
			{
				this.LoadNextAssetBundle(this.pBundleDownloads[this.pDownloadIndex].mapName + "/" + this.pBundleDownloads[this.pDownloadIndex].bundleName);
				return true;
			}
			this.pDownloadIndex++;
		}
		this.pDownloadIndex = this.pBundleDownloads.Count;
		return false;
	}

	// Token: 0x0600065B RID: 1627 RVA: 0x0002DB08 File Offset: 0x0002BD08
	private void LoadNextAssetBundle(string anAssetBundleName)
	{
		if (Application.platform == RuntimePlatform.OSXWebPlayer || Application.platform == RuntimePlatform.WindowsWebPlayer)
		{
			this.pCurrentDownload = new WWW(Data.basePath + anAssetBundleName + ".unity3d");
		}
		else
		{
			this.pCurrentDownload = new WWW(string.Concat(new string[]
			{
				"file://",
				Application.dataPath,
				"/Project Assets/Bundles/",
				anAssetBundleName,
				".unity3d"
			}));
		}
		Data.stringToBundle[this.pBundleDownloads[this.pDownloadIndex].bundleName] = this.pCurrentDownload;
	}

	// Token: 0x040006B6 RID: 1718
	private List<BundleEntry> pBundleDownloads;

	// Token: 0x040006B7 RID: 1719
	private int pDownloadIndex;

	// Token: 0x040006B8 RID: 1720
	private WWW pCurrentDownload;

	// Token: 0x040006B9 RID: 1721
	private bool pStartedDownloading;
}
