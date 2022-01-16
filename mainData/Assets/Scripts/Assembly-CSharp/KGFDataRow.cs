using System;
using System.Collections.Generic;

// Token: 0x02000181 RID: 385
public class KGFDataRow
{
	// Token: 0x06000B9D RID: 2973 RVA: 0x00055F60 File Offset: 0x00054160
	public KGFDataRow()
	{
		this.itsTable = null;
	}

	// Token: 0x06000B9E RID: 2974 RVA: 0x00055F7C File Offset: 0x0005417C
	public KGFDataRow(KGFDataTable theTable)
	{
		this.itsTable = theTable;
		foreach (KGFDataColumn theColumn in theTable.Columns)
		{
			this.itsCells.Add(new KGFDataCell(theColumn, this));
		}
	}

	// Token: 0x170000FB RID: 251
	public KGFDataCell this[int theIndex]
	{
		get
		{
			if (theIndex >= 0 && theIndex < this.itsTable.Columns.Count)
			{
				return this.itsCells[theIndex];
			}
			throw new ArgumentOutOfRangeException();
		}
		set
		{
			if (theIndex >= 0 && theIndex < this.itsTable.Columns.Count)
			{
				this.itsCells[theIndex] = value;
				return;
			}
			throw new ArgumentOutOfRangeException();
		}
	}

	// Token: 0x170000FC RID: 252
	public KGFDataCell this[string theName]
	{
		get
		{
			foreach (KGFDataCell kgfdataCell in this.itsCells)
			{
				if (kgfdataCell.Column.ColumnName.Equals(theName))
				{
					return kgfdataCell;
				}
			}
			throw new ArgumentOutOfRangeException();
		}
		set
		{
			bool flag = false;
			for (int i = 0; i < this.itsCells.Count; i++)
			{
				if (this.itsCells[i].Column.ColumnName.Equals(theName))
				{
					this.itsCells[i] = value;
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				throw new ArgumentOutOfRangeException();
			}
		}
	}

	// Token: 0x170000FD RID: 253
	public KGFDataCell this[KGFDataColumn theColumn]
	{
		get
		{
			for (int i = 0; i < this.itsTable.Columns.Count; i++)
			{
				if (this.itsCells[i].Column.Equals(theColumn))
				{
					return this.itsCells[i];
				}
			}
			throw new ArgumentOutOfRangeException();
		}
		set
		{
			for (int i = 0; i < this.itsTable.Columns.Count; i++)
			{
				if (this.itsCells[i].Column.Equals(theColumn))
				{
					this.itsCells[i] = value;
				}
			}
			throw new ArgumentOutOfRangeException();
		}
	}

	// Token: 0x06000BA5 RID: 2981 RVA: 0x0005621C File Offset: 0x0005441C
	public bool IsNull(KGFDataColumn theColumn)
	{
		return this.IsNull(theColumn.ColumnName);
	}

	// Token: 0x06000BA6 RID: 2982 RVA: 0x0005622C File Offset: 0x0005442C
	public bool IsNull(string theColumn)
	{
		foreach (KGFDataCell kgfdataCell in this.itsCells)
		{
			if (kgfdataCell.Column.ColumnName.Equals(theColumn) && kgfdataCell.Value != null)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x04000B8D RID: 2957
	private KGFDataTable itsTable;

	// Token: 0x04000B8E RID: 2958
	private List<KGFDataCell> itsCells = new List<KGFDataCell>();
}
