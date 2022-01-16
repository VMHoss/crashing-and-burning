using System;
using UnityEngine;

// Token: 0x02000051 RID: 81
[Serializable]
public class BMSymbol
{
	// Token: 0x1700003F RID: 63
	// (get) Token: 0x06000267 RID: 615 RVA: 0x00010C88 File Offset: 0x0000EE88
	public int length
	{
		get
		{
			if (this.mLength == 0)
			{
				this.mLength = this.sequence.Length;
			}
			return this.mLength;
		}
	}

	// Token: 0x17000040 RID: 64
	// (get) Token: 0x06000268 RID: 616 RVA: 0x00010CB8 File Offset: 0x0000EEB8
	public int offsetX
	{
		get
		{
			return this.mOffsetX;
		}
	}

	// Token: 0x17000041 RID: 65
	// (get) Token: 0x06000269 RID: 617 RVA: 0x00010CC0 File Offset: 0x0000EEC0
	public int offsetY
	{
		get
		{
			return this.mOffsetY;
		}
	}

	// Token: 0x17000042 RID: 66
	// (get) Token: 0x0600026A RID: 618 RVA: 0x00010CC8 File Offset: 0x0000EEC8
	public int width
	{
		get
		{
			return this.mWidth;
		}
	}

	// Token: 0x17000043 RID: 67
	// (get) Token: 0x0600026B RID: 619 RVA: 0x00010CD0 File Offset: 0x0000EED0
	public int height
	{
		get
		{
			return this.mHeight;
		}
	}

	// Token: 0x17000044 RID: 68
	// (get) Token: 0x0600026C RID: 620 RVA: 0x00010CD8 File Offset: 0x0000EED8
	public int advance
	{
		get
		{
			return this.mAdvance;
		}
	}

	// Token: 0x17000045 RID: 69
	// (get) Token: 0x0600026D RID: 621 RVA: 0x00010CE0 File Offset: 0x0000EEE0
	public Rect uvRect
	{
		get
		{
			return this.mUV;
		}
	}

	// Token: 0x0600026E RID: 622 RVA: 0x00010CE8 File Offset: 0x0000EEE8
	public void MarkAsDirty()
	{
		this.mIsValid = false;
	}

	// Token: 0x0600026F RID: 623 RVA: 0x00010CF4 File Offset: 0x0000EEF4
	public bool Validate(UIAtlas atlas)
	{
		if (atlas == null)
		{
			return false;
		}
		if (!this.mIsValid)
		{
			if (string.IsNullOrEmpty(this.spriteName))
			{
				return false;
			}
			this.mSprite = ((!(atlas != null)) ? null : atlas.GetSprite(this.spriteName));
			if (this.mSprite != null)
			{
				Texture texture = atlas.texture;
				if (texture == null)
				{
					this.mSprite = null;
				}
				else
				{
					Rect rect = this.mSprite.outer;
					this.mUV = rect;
					if (atlas.coordinates == UIAtlas.Coordinates.Pixels)
					{
						this.mUV = NGUIMath.ConvertToTexCoords(this.mUV, texture.width, texture.height);
					}
					else
					{
						rect = NGUIMath.ConvertToPixels(rect, texture.width, texture.height, true);
					}
					this.mOffsetX = Mathf.RoundToInt(this.mSprite.paddingLeft * rect.width);
					this.mOffsetY = Mathf.RoundToInt(this.mSprite.paddingTop * rect.width);
					this.mWidth = Mathf.RoundToInt(rect.width);
					this.mHeight = Mathf.RoundToInt(rect.height);
					this.mAdvance = Mathf.RoundToInt(rect.width + (this.mSprite.paddingRight + this.mSprite.paddingLeft) * rect.width);
					this.mIsValid = true;
				}
			}
		}
		return this.mSprite != null;
	}

	// Token: 0x040002E8 RID: 744
	public string sequence;

	// Token: 0x040002E9 RID: 745
	public string spriteName;

	// Token: 0x040002EA RID: 746
	private UIAtlas.Sprite mSprite;

	// Token: 0x040002EB RID: 747
	private bool mIsValid;

	// Token: 0x040002EC RID: 748
	private int mLength;

	// Token: 0x040002ED RID: 749
	private int mOffsetX;

	// Token: 0x040002EE RID: 750
	private int mOffsetY;

	// Token: 0x040002EF RID: 751
	private int mWidth;

	// Token: 0x040002F0 RID: 752
	private int mHeight;

	// Token: 0x040002F1 RID: 753
	private int mAdvance;

	// Token: 0x040002F2 RID: 754
	private Rect mUV;
}
