using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000D4 RID: 212
public class DicEntry
{
	// Token: 0x06000665 RID: 1637 RVA: 0x0002DFE8 File Offset: 0x0002C1E8
	public DicEntry(bool aB)
	{
		this.type = DicEntry.EntryType.BOOL;
		this.b = aB;
	}

	// Token: 0x06000666 RID: 1638 RVA: 0x0002E000 File Offset: 0x0002C200
	public DicEntry(int aI)
	{
		this.type = DicEntry.EntryType.INT;
		this.i = aI;
	}

	// Token: 0x06000667 RID: 1639 RVA: 0x0002E018 File Offset: 0x0002C218
	public DicEntry(float aF)
	{
		this.type = DicEntry.EntryType.FLOAT;
		this.f = aF;
	}

	// Token: 0x06000668 RID: 1640 RVA: 0x0002E030 File Offset: 0x0002C230
	public DicEntry(string aS)
	{
		this.type = DicEntry.EntryType.STRING;
		this.s = aS;
	}

	// Token: 0x06000669 RID: 1641 RVA: 0x0002E048 File Offset: 0x0002C248
	public DicEntry(Dictionary<string, DicEntry> aD)
	{
		this.type = DicEntry.EntryType.DICT;
		this.d = aD;
	}

	// Token: 0x0600066A RID: 1642 RVA: 0x0002E060 File Offset: 0x0002C260
	public DicEntry(List<DicEntry> aL)
	{
		this.type = DicEntry.EntryType.LIST;
		this.l = aL;
	}

	// Token: 0x0600066B RID: 1643 RVA: 0x0002E078 File Offset: 0x0002C278
	public DicEntry(DicEntry aDicEntry)
	{
		this.type = aDicEntry.type;
		switch (this.type)
		{
		case DicEntry.EntryType.BOOL:
			this._b = aDicEntry.b;
			break;
		case DicEntry.EntryType.INT:
			this._i = aDicEntry.i;
			break;
		case DicEntry.EntryType.FLOAT:
			this._f = aDicEntry.f;
			break;
		case DicEntry.EntryType.STRING:
			this._s = aDicEntry.s;
			break;
		case DicEntry.EntryType.DICT:
			this._d = aDicEntry.d;
			break;
		case DicEntry.EntryType.LIST:
			this._l = aDicEntry.l;
			break;
		default:
			throw new UnityException("Copy constructor: unknown type");
		}
	}

	// Token: 0x0600066C RID: 1644 RVA: 0x0002E134 File Offset: 0x0002C334
	public bool IsComplexProp()
	{
		return this.type == DicEntry.EntryType.LIST || this.type == DicEntry.EntryType.DICT;
	}

	// Token: 0x170000E6 RID: 230
	// (get) Token: 0x0600066D RID: 1645 RVA: 0x0002E150 File Offset: 0x0002C350
	// (set) Token: 0x0600066E RID: 1646 RVA: 0x0002E170 File Offset: 0x0002C370
	public bool b
	{
		get
		{
			if (this.type != DicEntry.EntryType.BOOL)
			{
				throw new UnityException("Got uninitialized boolean");
			}
			return this._b;
		}
		set
		{
			this._b = value;
		}
	}

	// Token: 0x170000E7 RID: 231
	// (get) Token: 0x0600066F RID: 1647 RVA: 0x0002E17C File Offset: 0x0002C37C
	// (set) Token: 0x06000670 RID: 1648 RVA: 0x0002E19C File Offset: 0x0002C39C
	public int i
	{
		get
		{
			if (this.type != DicEntry.EntryType.INT)
			{
				throw new UnityException("Got uninitialized int");
			}
			return this._i;
		}
		set
		{
			this._i = value;
		}
	}

	// Token: 0x170000E8 RID: 232
	// (get) Token: 0x06000671 RID: 1649 RVA: 0x0002E1A8 File Offset: 0x0002C3A8
	// (set) Token: 0x06000672 RID: 1650 RVA: 0x0002E1C8 File Offset: 0x0002C3C8
	public float f
	{
		get
		{
			if (this.type != DicEntry.EntryType.FLOAT)
			{
				throw new UnityException("Got uninitialized float");
			}
			return this._f;
		}
		set
		{
			this._f = value;
		}
	}

	// Token: 0x170000E9 RID: 233
	// (get) Token: 0x06000673 RID: 1651 RVA: 0x0002E1D4 File Offset: 0x0002C3D4
	// (set) Token: 0x06000674 RID: 1652 RVA: 0x0002E1F4 File Offset: 0x0002C3F4
	public string s
	{
		get
		{
			if (this.type != DicEntry.EntryType.STRING)
			{
				throw new UnityException("Got uninitialized string");
			}
			return this._s;
		}
		set
		{
			this._s = value;
		}
	}

	// Token: 0x170000EA RID: 234
	// (get) Token: 0x06000675 RID: 1653 RVA: 0x0002E200 File Offset: 0x0002C400
	// (set) Token: 0x06000676 RID: 1654 RVA: 0x0002E220 File Offset: 0x0002C420
	public Dictionary<string, DicEntry> d
	{
		get
		{
			if (this.type != DicEntry.EntryType.DICT)
			{
				throw new UnityException("Got uninitialized dictionary");
			}
			return this._d;
		}
		set
		{
			this._d = value;
		}
	}

	// Token: 0x170000EB RID: 235
	// (get) Token: 0x06000677 RID: 1655 RVA: 0x0002E22C File Offset: 0x0002C42C
	// (set) Token: 0x06000678 RID: 1656 RVA: 0x0002E24C File Offset: 0x0002C44C
	public List<DicEntry> l
	{
		get
		{
			if (this.type != DicEntry.EntryType.LIST)
			{
				throw new UnityException("Got uninitialized list");
			}
			return this._l;
		}
		set
		{
			this._l = value;
		}
	}

	// Token: 0x040006BA RID: 1722
	public DicEntry.EntryType type;

	// Token: 0x040006BB RID: 1723
	private bool _b;

	// Token: 0x040006BC RID: 1724
	private int _i;

	// Token: 0x040006BD RID: 1725
	private float _f;

	// Token: 0x040006BE RID: 1726
	private string _s;

	// Token: 0x040006BF RID: 1727
	private Dictionary<string, DicEntry> _d;

	// Token: 0x040006C0 RID: 1728
	private List<DicEntry> _l;

	// Token: 0x020000D5 RID: 213
	public enum EntryType
	{
		// Token: 0x040006C2 RID: 1730
		BOOL = 1,
		// Token: 0x040006C3 RID: 1731
		INT,
		// Token: 0x040006C4 RID: 1732
		FLOAT,
		// Token: 0x040006C5 RID: 1733
		STRING,
		// Token: 0x040006C6 RID: 1734
		DICT,
		// Token: 0x040006C7 RID: 1735
		LIST
	}
}
