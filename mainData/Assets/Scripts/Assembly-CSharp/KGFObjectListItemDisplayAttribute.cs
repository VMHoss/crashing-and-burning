using System;

// Token: 0x0200016E RID: 366
public class KGFObjectListItemDisplayAttribute : Attribute
{
	// Token: 0x06000AC3 RID: 2755 RVA: 0x000517F0 File Offset: 0x0004F9F0
	public KGFObjectListItemDisplayAttribute(string theHeader)
	{
		this.itsHeader = theHeader;
		this.itsSearchable = false;
		this.itsDisplay = true;
	}

	// Token: 0x06000AC4 RID: 2756 RVA: 0x00051810 File Offset: 0x0004FA10
	public KGFObjectListItemDisplayAttribute(string theHeader, bool theSearchable)
	{
		this.itsHeader = theHeader;
		this.itsSearchable = theSearchable;
		this.itsDisplay = true;
	}

	// Token: 0x06000AC5 RID: 2757 RVA: 0x00051830 File Offset: 0x0004FA30
	public KGFObjectListItemDisplayAttribute(string theHeader, bool theSearchable, bool theDisplay)
	{
		this.itsHeader = theHeader;
		this.itsSearchable = theSearchable;
		this.itsDisplay = theDisplay;
	}

	// Token: 0x170000F4 RID: 244
	// (get) Token: 0x06000AC6 RID: 2758 RVA: 0x00051850 File Offset: 0x0004FA50
	public string Header
	{
		get
		{
			return this.itsHeader;
		}
	}

	// Token: 0x170000F5 RID: 245
	// (get) Token: 0x06000AC7 RID: 2759 RVA: 0x00051858 File Offset: 0x0004FA58
	public bool Searchable
	{
		get
		{
			return this.itsSearchable;
		}
	}

	// Token: 0x170000F6 RID: 246
	// (get) Token: 0x06000AC8 RID: 2760 RVA: 0x00051860 File Offset: 0x0004FA60
	public bool Display
	{
		get
		{
			return this.itsDisplay;
		}
	}

	// Token: 0x04000B27 RID: 2855
	private string itsHeader;

	// Token: 0x04000B28 RID: 2856
	private bool itsSearchable;

	// Token: 0x04000B29 RID: 2857
	private bool itsDisplay;
}
