using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200004F RID: 79
[Serializable]
public class BMFont
{
	// Token: 0x17000038 RID: 56
	// (get) Token: 0x06000252 RID: 594 RVA: 0x00010924 File Offset: 0x0000EB24
	public bool isValid
	{
		get
		{
			return this.mSaved.Count > 0;
		}
	}

	// Token: 0x17000039 RID: 57
	// (get) Token: 0x06000253 RID: 595 RVA: 0x00010934 File Offset: 0x0000EB34
	// (set) Token: 0x06000254 RID: 596 RVA: 0x0001093C File Offset: 0x0000EB3C
	public int charSize
	{
		get
		{
			return this.mSize;
		}
		set
		{
			this.mSize = value;
		}
	}

	// Token: 0x1700003A RID: 58
	// (get) Token: 0x06000255 RID: 597 RVA: 0x00010948 File Offset: 0x0000EB48
	// (set) Token: 0x06000256 RID: 598 RVA: 0x00010950 File Offset: 0x0000EB50
	public int baseOffset
	{
		get
		{
			return this.mBase;
		}
		set
		{
			this.mBase = value;
		}
	}

	// Token: 0x1700003B RID: 59
	// (get) Token: 0x06000257 RID: 599 RVA: 0x0001095C File Offset: 0x0000EB5C
	// (set) Token: 0x06000258 RID: 600 RVA: 0x00010964 File Offset: 0x0000EB64
	public int texWidth
	{
		get
		{
			return this.mWidth;
		}
		set
		{
			this.mWidth = value;
		}
	}

	// Token: 0x1700003C RID: 60
	// (get) Token: 0x06000259 RID: 601 RVA: 0x00010970 File Offset: 0x0000EB70
	// (set) Token: 0x0600025A RID: 602 RVA: 0x00010978 File Offset: 0x0000EB78
	public int texHeight
	{
		get
		{
			return this.mHeight;
		}
		set
		{
			this.mHeight = value;
		}
	}

	// Token: 0x1700003D RID: 61
	// (get) Token: 0x0600025B RID: 603 RVA: 0x00010984 File Offset: 0x0000EB84
	public int glyphCount
	{
		get
		{
			return (!this.isValid) ? 0 : this.mSaved.Count;
		}
	}

	// Token: 0x1700003E RID: 62
	// (get) Token: 0x0600025C RID: 604 RVA: 0x000109A4 File Offset: 0x0000EBA4
	// (set) Token: 0x0600025D RID: 605 RVA: 0x000109AC File Offset: 0x0000EBAC
	public string spriteName
	{
		get
		{
			return this.mSpriteName;
		}
		set
		{
			this.mSpriteName = value;
		}
	}

	// Token: 0x0600025E RID: 606 RVA: 0x000109B8 File Offset: 0x0000EBB8
	public BMGlyph GetGlyph(int index, bool createIfMissing)
	{
		BMGlyph bmglyph = null;
		if (this.mDict.Count == 0)
		{
			int i = 0;
			int count = this.mSaved.Count;
			while (i < count)
			{
				BMGlyph bmglyph2 = this.mSaved[i];
				this.mDict.Add(bmglyph2.index, bmglyph2);
				i++;
			}
		}
		if (!this.mDict.TryGetValue(index, out bmglyph) && createIfMissing)
		{
			bmglyph = new BMGlyph();
			bmglyph.index = index;
			this.mSaved.Add(bmglyph);
			this.mDict.Add(index, bmglyph);
		}
		return bmglyph;
	}

	// Token: 0x0600025F RID: 607 RVA: 0x00010A54 File Offset: 0x0000EC54
	public BMGlyph GetGlyph(int index)
	{
		return this.GetGlyph(index, false);
	}

	// Token: 0x06000260 RID: 608 RVA: 0x00010A60 File Offset: 0x0000EC60
	public void Clear()
	{
		this.mDict.Clear();
		this.mSaved.Clear();
	}

	// Token: 0x06000261 RID: 609 RVA: 0x00010A78 File Offset: 0x0000EC78
	public void Trim(int xMin, int yMin, int xMax, int yMax)
	{
		if (this.isValid)
		{
			int i = 0;
			int count = this.mSaved.Count;
			while (i < count)
			{
				BMGlyph bmglyph = this.mSaved[i];
				if (bmglyph != null)
				{
					bmglyph.Trim(xMin, yMin, xMax, yMax);
				}
				i++;
			}
		}
	}

	// Token: 0x040002D7 RID: 727
	[SerializeField]
	[HideInInspector]
	private int mSize;

	// Token: 0x040002D8 RID: 728
	[SerializeField]
	[HideInInspector]
	private int mBase;

	// Token: 0x040002D9 RID: 729
	[HideInInspector]
	[SerializeField]
	private int mWidth;

	// Token: 0x040002DA RID: 730
	[SerializeField]
	[HideInInspector]
	private int mHeight;

	// Token: 0x040002DB RID: 731
	[SerializeField]
	[HideInInspector]
	private string mSpriteName;

	// Token: 0x040002DC RID: 732
	[SerializeField]
	[HideInInspector]
	private List<BMGlyph> mSaved = new List<BMGlyph>();

	// Token: 0x040002DD RID: 733
	private Dictionary<int, BMGlyph> mDict = new Dictionary<int, BMGlyph>();
}
