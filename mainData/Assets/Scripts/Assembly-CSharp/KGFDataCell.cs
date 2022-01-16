using System;

// Token: 0x02000182 RID: 386
public class KGFDataCell
{
	// Token: 0x06000BA7 RID: 2983 RVA: 0x000562B8 File Offset: 0x000544B8
	public KGFDataCell(KGFDataColumn theColumn, KGFDataRow theRow)
	{
		this.itsColumn = theColumn;
		this.itsRow = theRow;
		this.itsValue = null;
	}

	// Token: 0x170000FE RID: 254
	// (get) Token: 0x06000BA8 RID: 2984 RVA: 0x000562D8 File Offset: 0x000544D8
	public KGFDataColumn Column
	{
		get
		{
			return this.itsColumn;
		}
	}

	// Token: 0x170000FF RID: 255
	// (get) Token: 0x06000BA9 RID: 2985 RVA: 0x000562E0 File Offset: 0x000544E0
	public KGFDataRow Row
	{
		get
		{
			return this.itsRow;
		}
	}

	// Token: 0x17000100 RID: 256
	// (get) Token: 0x06000BAA RID: 2986 RVA: 0x000562E8 File Offset: 0x000544E8
	// (set) Token: 0x06000BAB RID: 2987 RVA: 0x000562F0 File Offset: 0x000544F0
	public object Value
	{
		get
		{
			return this.itsValue;
		}
		set
		{
			this.itsValue = value;
		}
	}

	// Token: 0x06000BAC RID: 2988 RVA: 0x000562FC File Offset: 0x000544FC
	public override string ToString()
	{
		return this.itsValue.ToString();
	}

	// Token: 0x04000B8F RID: 2959
	private KGFDataColumn itsColumn;

	// Token: 0x04000B90 RID: 2960
	private KGFDataRow itsRow;

	// Token: 0x04000B91 RID: 2961
	private object itsValue;
}
