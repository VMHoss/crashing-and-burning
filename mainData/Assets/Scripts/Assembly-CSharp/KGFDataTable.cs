using System;
using System.Collections.Generic;

// Token: 0x0200017F RID: 383
public class KGFDataTable
{
	// Token: 0x170000F7 RID: 247
	// (get) Token: 0x06000B94 RID: 2964 RVA: 0x00055EF8 File Offset: 0x000540F8
	public List<KGFDataColumn> Columns
	{
		get
		{
			return this.itsColumns;
		}
	}

	// Token: 0x170000F8 RID: 248
	// (get) Token: 0x06000B95 RID: 2965 RVA: 0x00055F00 File Offset: 0x00054100
	public List<KGFDataRow> Rows
	{
		get
		{
			return this.itsRows;
		}
	}

	// Token: 0x06000B96 RID: 2966 RVA: 0x00055F08 File Offset: 0x00054108
	public KGFDataRow NewRow()
	{
		return new KGFDataRow(this);
	}

	// Token: 0x04000B89 RID: 2953
	private List<KGFDataColumn> itsColumns = new List<KGFDataColumn>();

	// Token: 0x04000B8A RID: 2954
	private List<KGFDataRow> itsRows = new List<KGFDataRow>();
}
