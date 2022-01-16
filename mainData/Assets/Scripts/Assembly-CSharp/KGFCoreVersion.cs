using System;

// Token: 0x0200017E RID: 382
public static class KGFCoreVersion
{
	// Token: 0x06000B92 RID: 2962 RVA: 0x00055EC4 File Offset: 0x000540C4
	public static Version GetCurrentVersion()
	{
		return KGFCoreVersion.itsVersion.Clone() as Version;
	}

	// Token: 0x04000B88 RID: 2952
	private static Version itsVersion = new Version(1, 2, 0, 0);
}
