using System;

// Token: 0x02000180 RID: 384
public class KGFDataColumn
{
	// Token: 0x06000B97 RID: 2967 RVA: 0x00055F10 File Offset: 0x00054110
	public KGFDataColumn(string theName, Type theType)
	{
		this.itsName = theName;
		this.itsType = theType;
	}

	// Token: 0x06000B98 RID: 2968 RVA: 0x00055F28 File Offset: 0x00054128
	public void Add(string theName, Type theType)
	{
		this.itsName = theName;
		this.itsType = theType;
	}

	// Token: 0x170000F9 RID: 249
	// (get) Token: 0x06000B99 RID: 2969 RVA: 0x00055F38 File Offset: 0x00054138
	// (set) Token: 0x06000B9A RID: 2970 RVA: 0x00055F40 File Offset: 0x00054140
	public string ColumnName
	{
		get
		{
			return this.itsName;
		}
		set
		{
			this.itsName = value;
		}
	}

	// Token: 0x170000FA RID: 250
	// (get) Token: 0x06000B9B RID: 2971 RVA: 0x00055F4C File Offset: 0x0005414C
	// (set) Token: 0x06000B9C RID: 2972 RVA: 0x00055F54 File Offset: 0x00054154
	public Type ColumnType
	{
		get
		{
			return this.itsType;
		}
		set
		{
			this.itsType = value;
		}
	}

	// Token: 0x04000B8B RID: 2955
	private string itsName;

	// Token: 0x04000B8C RID: 2956
	private Type itsType;
}
