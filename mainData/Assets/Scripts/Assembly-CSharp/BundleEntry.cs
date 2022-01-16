using System;

// Token: 0x020000D1 RID: 209
public class BundleEntry
{
	// Token: 0x06000654 RID: 1620 RVA: 0x0002D8B4 File Offset: 0x0002BAB4
	public BundleEntry(string aMapName, string aBundleName)
	{
		this.mapName = aMapName;
		this.bundleName = aBundleName;
	}

	// Token: 0x040006B4 RID: 1716
	public string mapName;

	// Token: 0x040006B5 RID: 1717
	public string bundleName;
}
