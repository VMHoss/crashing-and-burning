using System;

// Token: 0x0200017D RID: 381
[Serializable]
public class IDReference
{
	// Token: 0x06000B8A RID: 2954 RVA: 0x00055E68 File Offset: 0x00054068
	public string GetID()
	{
		return this.itsID;
	}

	// Token: 0x06000B8B RID: 2955 RVA: 0x00055E70 File Offset: 0x00054070
	public void SetID(string theID)
	{
		this.itsID = theID;
		this.itsEmpty = false;
	}

	// Token: 0x06000B8C RID: 2956 RVA: 0x00055E80 File Offset: 0x00054080
	public bool GetHasValue()
	{
		return !this.itsEmpty;
	}

	// Token: 0x06000B8D RID: 2957 RVA: 0x00055E8C File Offset: 0x0005408C
	public void SetEmpty()
	{
		this.itsEmpty = true;
	}

	// Token: 0x06000B8E RID: 2958 RVA: 0x00055E98 File Offset: 0x00054098
	public override string ToString()
	{
		return this.GetID();
	}

	// Token: 0x06000B8F RID: 2959 RVA: 0x00055EA0 File Offset: 0x000540A0
	public bool GetCanBeDeleted()
	{
		return this.itsCanBeDeleted;
	}

	// Token: 0x06000B90 RID: 2960 RVA: 0x00055EA8 File Offset: 0x000540A8
	public void SetCanBeDeleted(bool theCanBeDeleted)
	{
		this.itsCanBeDeleted = theCanBeDeleted;
	}

	// Token: 0x04000B85 RID: 2949
	public string itsID = string.Empty;

	// Token: 0x04000B86 RID: 2950
	public bool itsEmpty = true;

	// Token: 0x04000B87 RID: 2951
	public bool itsCanBeDeleted;
}
