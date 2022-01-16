using System;
using System.Collections.Generic;

// Token: 0x02000050 RID: 80
[Serializable]
public class BMGlyph
{
	// Token: 0x06000263 RID: 611 RVA: 0x00010AD4 File Offset: 0x0000ECD4
	public int GetKerning(int previousChar)
	{
		if (this.kerning != null)
		{
			int i = 0;
			int count = this.kerning.Count;
			while (i < count)
			{
				if (this.kerning[i] == previousChar)
				{
					return this.kerning[i + 1];
				}
				i += 2;
			}
		}
		return 0;
	}

	// Token: 0x06000264 RID: 612 RVA: 0x00010B2C File Offset: 0x0000ED2C
	public void SetKerning(int previousChar, int amount)
	{
		if (this.kerning == null)
		{
			this.kerning = new List<int>();
		}
		for (int i = 0; i < this.kerning.Count; i += 2)
		{
			if (this.kerning[i] == previousChar)
			{
				this.kerning[i + 1] = amount;
				return;
			}
		}
		this.kerning.Add(previousChar);
		this.kerning.Add(amount);
	}

	// Token: 0x06000265 RID: 613 RVA: 0x00010BA8 File Offset: 0x0000EDA8
	public void Trim(int xMin, int yMin, int xMax, int yMax)
	{
		int num = this.x + this.width;
		int num2 = this.y + this.height;
		if (this.x < xMin)
		{
			int num3 = xMin - this.x;
			this.x += num3;
			this.width -= num3;
			this.offsetX += num3;
		}
		if (this.y < yMin)
		{
			int num4 = yMin - this.y;
			this.y += num4;
			this.height -= num4;
			this.offsetY += num4;
		}
		if (num > xMax)
		{
			this.width -= num - xMax;
		}
		if (num2 > yMax)
		{
			this.height -= num2 - yMax;
		}
	}

	// Token: 0x040002DE RID: 734
	public int index;

	// Token: 0x040002DF RID: 735
	public int x;

	// Token: 0x040002E0 RID: 736
	public int y;

	// Token: 0x040002E1 RID: 737
	public int width;

	// Token: 0x040002E2 RID: 738
	public int height;

	// Token: 0x040002E3 RID: 739
	public int offsetX;

	// Token: 0x040002E4 RID: 740
	public int offsetY;

	// Token: 0x040002E5 RID: 741
	public int advance;

	// Token: 0x040002E6 RID: 742
	public int channel;

	// Token: 0x040002E7 RID: 743
	public List<int> kerning;
}
