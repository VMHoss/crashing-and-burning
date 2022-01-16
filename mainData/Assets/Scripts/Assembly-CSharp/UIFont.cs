using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// Token: 0x0200007D RID: 125
[AddComponentMenu("NGUI/UI/Font")]
[ExecuteInEditMode]
public class UIFont : MonoBehaviour
{
	// Token: 0x1700008B RID: 139
	// (get) Token: 0x060003E1 RID: 993 RVA: 0x00019D08 File Offset: 0x00017F08
	public BMFont bmFont
	{
		get
		{
			return (!(this.mReplacement != null)) ? this.mFont : this.mReplacement.bmFont;
		}
	}

	// Token: 0x1700008C RID: 140
	// (get) Token: 0x060003E2 RID: 994 RVA: 0x00019D34 File Offset: 0x00017F34
	public int texWidth
	{
		get
		{
			return (!(this.mReplacement != null)) ? ((this.mFont == null) ? 1 : this.mFont.texWidth) : this.mReplacement.texWidth;
		}
	}

	// Token: 0x1700008D RID: 141
	// (get) Token: 0x060003E3 RID: 995 RVA: 0x00019D74 File Offset: 0x00017F74
	public int texHeight
	{
		get
		{
			return (!(this.mReplacement != null)) ? ((this.mFont == null) ? 1 : this.mFont.texHeight) : this.mReplacement.texHeight;
		}
	}

	// Token: 0x1700008E RID: 142
	// (get) Token: 0x060003E4 RID: 996 RVA: 0x00019DB4 File Offset: 0x00017FB4
	public bool hasSymbols
	{
		get
		{
			return (!(this.mReplacement != null)) ? (this.mSymbols.Count != 0) : this.mReplacement.hasSymbols;
		}
	}

	// Token: 0x1700008F RID: 143
	// (get) Token: 0x060003E5 RID: 997 RVA: 0x00019DF4 File Offset: 0x00017FF4
	public List<BMSymbol> symbols
	{
		get
		{
			return (!(this.mReplacement != null)) ? this.mSymbols : this.mReplacement.symbols;
		}
	}

	// Token: 0x17000090 RID: 144
	// (get) Token: 0x060003E6 RID: 998 RVA: 0x00019E20 File Offset: 0x00018020
	// (set) Token: 0x060003E7 RID: 999 RVA: 0x00019E4C File Offset: 0x0001804C
	public UIAtlas atlas
	{
		get
		{
			return (!(this.mReplacement != null)) ? this.mAtlas : this.mReplacement.atlas;
		}
		set
		{
			if (this.mReplacement != null)
			{
				this.mReplacement.atlas = value;
			}
			else if (this.mAtlas != value)
			{
				if (value == null)
				{
					if (this.mAtlas != null)
					{
						this.mMat = this.mAtlas.spriteMaterial;
					}
					if (this.sprite != null)
					{
						this.mUVRect = this.uvRect;
					}
				}
				this.mPMA = -1;
				this.mAtlas = value;
				this.MarkAsDirty();
			}
		}
	}

	// Token: 0x17000091 RID: 145
	// (get) Token: 0x060003E8 RID: 1000 RVA: 0x00019EE8 File Offset: 0x000180E8
	// (set) Token: 0x060003E9 RID: 1001 RVA: 0x00019FAC File Offset: 0x000181AC
	public Material material
	{
		get
		{
			if (this.mReplacement != null)
			{
				return this.mReplacement.material;
			}
			if (this.mAtlas != null)
			{
				return this.mAtlas.spriteMaterial;
			}
			if (this.mMat != null)
			{
				if (this.mDynamicFont != null && this.mMat != this.mDynamicFont.material)
				{
					this.mMat.mainTexture = this.mDynamicFont.material.mainTexture;
				}
				return this.mMat;
			}
			if (this.mDynamicFont != null)
			{
				return this.mDynamicFont.material;
			}
			return null;
		}
		set
		{
			if (this.mReplacement != null)
			{
				this.mReplacement.material = value;
			}
			else if (this.mMat != value)
			{
				this.mPMA = -1;
				this.mMat = value;
				this.MarkAsDirty();
			}
		}
	}

	// Token: 0x17000092 RID: 146
	// (get) Token: 0x060003EA RID: 1002 RVA: 0x0001A000 File Offset: 0x00018200
	// (set) Token: 0x060003EB RID: 1003 RVA: 0x0001A050 File Offset: 0x00018250
	public float pixelSize
	{
		get
		{
			if (this.mReplacement != null)
			{
				return this.mReplacement.pixelSize;
			}
			if (this.mAtlas != null)
			{
				return this.mAtlas.pixelSize;
			}
			return this.mPixelSize;
		}
		set
		{
			if (this.mReplacement != null)
			{
				this.mReplacement.pixelSize = value;
			}
			else if (this.mAtlas != null)
			{
				this.mAtlas.pixelSize = value;
			}
			else
			{
				float num = Mathf.Clamp(value, 0.25f, 4f);
				if (this.mPixelSize != num)
				{
					this.mPixelSize = num;
					this.MarkAsDirty();
				}
			}
		}
	}

	// Token: 0x17000093 RID: 147
	// (get) Token: 0x060003EC RID: 1004 RVA: 0x0001A0CC File Offset: 0x000182CC
	public bool premultipliedAlpha
	{
		get
		{
			if (this.mReplacement != null)
			{
				return this.mReplacement.premultipliedAlpha;
			}
			if (this.mAtlas != null)
			{
				return this.mAtlas.premultipliedAlpha;
			}
			if (this.mPMA == -1)
			{
				Material material = this.material;
				this.mPMA = ((!(material != null) || !(material.shader != null) || !material.shader.name.Contains("Premultiplied")) ? 0 : 1);
			}
			return this.mPMA == 1;
		}
	}

	// Token: 0x17000094 RID: 148
	// (get) Token: 0x060003ED RID: 1005 RVA: 0x0001A174 File Offset: 0x00018374
	public Texture2D texture
	{
		get
		{
			if (this.mReplacement != null)
			{
				return this.mReplacement.texture;
			}
			Material material = this.material;
			return (!(material != null)) ? null : (material.mainTexture as Texture2D);
		}
	}

	// Token: 0x17000095 RID: 149
	// (get) Token: 0x060003EE RID: 1006 RVA: 0x0001A1C4 File Offset: 0x000183C4
	// (set) Token: 0x060003EF RID: 1007 RVA: 0x0001A338 File Offset: 0x00018538
	public Rect uvRect
	{
		get
		{
			if (this.mReplacement != null)
			{
				return this.mReplacement.uvRect;
			}
			if (this.mAtlas != null && this.mSprite == null && this.sprite != null)
			{
				Texture texture = this.mAtlas.texture;
				if (texture != null)
				{
					this.mUVRect = this.mSprite.outer;
					if (this.mAtlas.coordinates == UIAtlas.Coordinates.Pixels)
					{
						this.mUVRect = NGUIMath.ConvertToTexCoords(this.mUVRect, texture.width, texture.height);
					}
					if (this.mSprite.hasPadding)
					{
						Rect rect = this.mUVRect;
						this.mUVRect.xMin = rect.xMin - this.mSprite.paddingLeft * rect.width;
						this.mUVRect.yMin = rect.yMin - this.mSprite.paddingBottom * rect.height;
						this.mUVRect.xMax = rect.xMax + this.mSprite.paddingRight * rect.width;
						this.mUVRect.yMax = rect.yMax + this.mSprite.paddingTop * rect.height;
					}
					if (this.mSprite.hasPadding)
					{
						this.Trim();
					}
				}
			}
			return this.mUVRect;
		}
		set
		{
			if (this.mReplacement != null)
			{
				this.mReplacement.uvRect = value;
			}
			else if (this.sprite == null && this.mUVRect != value)
			{
				this.mUVRect = value;
				this.MarkAsDirty();
			}
		}
	}

	// Token: 0x17000096 RID: 150
	// (get) Token: 0x060003F0 RID: 1008 RVA: 0x0001A390 File Offset: 0x00018590
	// (set) Token: 0x060003F1 RID: 1009 RVA: 0x0001A3CC File Offset: 0x000185CC
	public string spriteName
	{
		get
		{
			return (!(this.mReplacement != null)) ? this.mFont.spriteName : this.mReplacement.spriteName;
		}
		set
		{
			if (this.mReplacement != null)
			{
				this.mReplacement.spriteName = value;
			}
			else if (this.mFont.spriteName != value)
			{
				this.mFont.spriteName = value;
				this.MarkAsDirty();
			}
		}
	}

	// Token: 0x17000097 RID: 151
	// (get) Token: 0x060003F2 RID: 1010 RVA: 0x0001A424 File Offset: 0x00018624
	// (set) Token: 0x060003F3 RID: 1011 RVA: 0x0001A450 File Offset: 0x00018650
	public int horizontalSpacing
	{
		get
		{
			return (!(this.mReplacement != null)) ? this.mSpacingX : this.mReplacement.horizontalSpacing;
		}
		set
		{
			if (this.mReplacement != null)
			{
				this.mReplacement.horizontalSpacing = value;
			}
			else if (this.mSpacingX != value)
			{
				this.mSpacingX = value;
				this.MarkAsDirty();
			}
		}
	}

	// Token: 0x17000098 RID: 152
	// (get) Token: 0x060003F4 RID: 1012 RVA: 0x0001A490 File Offset: 0x00018690
	// (set) Token: 0x060003F5 RID: 1013 RVA: 0x0001A4BC File Offset: 0x000186BC
	public int verticalSpacing
	{
		get
		{
			return (!(this.mReplacement != null)) ? this.mSpacingY : this.mReplacement.verticalSpacing;
		}
		set
		{
			if (this.mReplacement != null)
			{
				this.mReplacement.verticalSpacing = value;
			}
			else if (this.mSpacingY != value)
			{
				this.mSpacingY = value;
				this.MarkAsDirty();
			}
		}
	}

	// Token: 0x17000099 RID: 153
	// (get) Token: 0x060003F6 RID: 1014 RVA: 0x0001A4FC File Offset: 0x000186FC
	public bool isValid
	{
		get
		{
			return this.mDynamicFont != null || this.mFont.isValid;
		}
	}

	// Token: 0x1700009A RID: 154
	// (get) Token: 0x060003F7 RID: 1015 RVA: 0x0001A520 File Offset: 0x00018720
	public int size
	{
		get
		{
			return (!(this.mReplacement != null)) ? ((!this.isDynamic) ? this.mFont.charSize : this.mDynamicFontSize) : this.mReplacement.size;
		}
	}

	// Token: 0x1700009B RID: 155
	// (get) Token: 0x060003F8 RID: 1016 RVA: 0x0001A570 File Offset: 0x00018770
	public UIAtlas.Sprite sprite
	{
		get
		{
			if (this.mReplacement != null)
			{
				return this.mReplacement.sprite;
			}
			if (!this.mSpriteSet)
			{
				this.mSprite = null;
			}
			if (this.mSprite == null)
			{
				if (this.mAtlas != null && !string.IsNullOrEmpty(this.mFont.spriteName))
				{
					this.mSprite = this.mAtlas.GetSprite(this.mFont.spriteName);
					if (this.mSprite == null)
					{
						this.mSprite = this.mAtlas.GetSprite(base.name);
					}
					this.mSpriteSet = true;
					if (this.mSprite == null)
					{
						this.mFont.spriteName = null;
					}
				}
				int i = 0;
				int count = this.mSymbols.Count;
				while (i < count)
				{
					this.symbols[i].MarkAsDirty();
					i++;
				}
			}
			return this.mSprite;
		}
	}

	// Token: 0x1700009C RID: 156
	// (get) Token: 0x060003F9 RID: 1017 RVA: 0x0001A670 File Offset: 0x00018870
	// (set) Token: 0x060003FA RID: 1018 RVA: 0x0001A678 File Offset: 0x00018878
	public UIFont replacement
	{
		get
		{
			return this.mReplacement;
		}
		set
		{
			UIFont uifont = value;
			if (uifont == this)
			{
				uifont = null;
			}
			if (this.mReplacement != uifont)
			{
				if (uifont != null && uifont.replacement == this)
				{
					uifont.replacement = null;
				}
				if (this.mReplacement != null)
				{
					this.MarkAsDirty();
				}
				this.mReplacement = uifont;
				this.MarkAsDirty();
			}
		}
	}

	// Token: 0x1700009D RID: 157
	// (get) Token: 0x060003FB RID: 1019 RVA: 0x0001A6F0 File Offset: 0x000188F0
	public bool isDynamic
	{
		get
		{
			return this.mDynamicFont != null;
		}
	}

	// Token: 0x1700009E RID: 158
	// (get) Token: 0x060003FC RID: 1020 RVA: 0x0001A700 File Offset: 0x00018900
	// (set) Token: 0x060003FD RID: 1021 RVA: 0x0001A72C File Offset: 0x0001892C
	public Font dynamicFont
	{
		get
		{
			return (!(this.mReplacement != null)) ? this.mDynamicFont : this.mReplacement.dynamicFont;
		}
		set
		{
			if (this.mReplacement != null)
			{
				this.mReplacement.dynamicFont = value;
			}
			else if (this.mDynamicFont != value)
			{
				if (this.mDynamicFont != null)
				{
					this.material = null;
				}
				this.mDynamicFont = value;
				this.MarkAsDirty();
			}
		}
	}

	// Token: 0x1700009F RID: 159
	// (get) Token: 0x060003FE RID: 1022 RVA: 0x0001A794 File Offset: 0x00018994
	// (set) Token: 0x060003FF RID: 1023 RVA: 0x0001A7C0 File Offset: 0x000189C0
	public int dynamicFontSize
	{
		get
		{
			return (!(this.mReplacement != null)) ? this.mDynamicFontSize : this.mReplacement.dynamicFontSize;
		}
		set
		{
			if (this.mReplacement != null)
			{
				this.mReplacement.dynamicFontSize = value;
			}
			else
			{
				value = Mathf.Clamp(value, 4, 128);
				if (this.mDynamicFontSize != value)
				{
					this.mDynamicFontSize = value;
					this.MarkAsDirty();
				}
			}
		}
	}

	// Token: 0x170000A0 RID: 160
	// (get) Token: 0x06000400 RID: 1024 RVA: 0x0001A818 File Offset: 0x00018A18
	// (set) Token: 0x06000401 RID: 1025 RVA: 0x0001A844 File Offset: 0x00018A44
	public FontStyle dynamicFontStyle
	{
		get
		{
			return (!(this.mReplacement != null)) ? this.mDynamicFontStyle : this.mReplacement.dynamicFontStyle;
		}
		set
		{
			if (this.mReplacement != null)
			{
				this.mReplacement.dynamicFontStyle = value;
			}
			else if (this.mDynamicFontStyle != value)
			{
				this.mDynamicFontStyle = value;
				this.MarkAsDirty();
			}
		}
	}

	// Token: 0x06000402 RID: 1026 RVA: 0x0001A884 File Offset: 0x00018A84
	private void Trim()
	{
		Texture texture = this.mAtlas.texture;
		if (texture != null && this.mSprite != null)
		{
			Rect rect = NGUIMath.ConvertToPixels(this.mUVRect, this.texture.width, this.texture.height, true);
			Rect rect2 = (this.mAtlas.coordinates != UIAtlas.Coordinates.TexCoords) ? this.mSprite.outer : NGUIMath.ConvertToPixels(this.mSprite.outer, texture.width, texture.height, true);
			int xMin = Mathf.RoundToInt(rect2.xMin - rect.xMin);
			int yMin = Mathf.RoundToInt(rect2.yMin - rect.yMin);
			int xMax = Mathf.RoundToInt(rect2.xMax - rect.xMin);
			int yMax = Mathf.RoundToInt(rect2.yMax - rect.yMin);
			this.mFont.Trim(xMin, yMin, xMax, yMax);
		}
	}

	// Token: 0x06000403 RID: 1027 RVA: 0x0001A980 File Offset: 0x00018B80
	private bool References(UIFont font)
	{
		return !(font == null) && (font == this || (this.mReplacement != null && this.mReplacement.References(font)));
	}

	// Token: 0x06000404 RID: 1028 RVA: 0x0001A9CC File Offset: 0x00018BCC
	public static bool CheckIfRelated(UIFont a, UIFont b)
	{
		return !(a == null) && !(b == null) && ((a.isDynamic && a.dynamicTexture == b.dynamicTexture) || a == b || a.References(b) || b.References(a));
	}

	// Token: 0x170000A1 RID: 161
	// (get) Token: 0x06000405 RID: 1029 RVA: 0x0001AA38 File Offset: 0x00018C38
	private Texture dynamicTexture
	{
		get
		{
			if (this.mReplacement)
			{
				return this.mReplacement.dynamicTexture;
			}
			if (this.isDynamic)
			{
				return this.mDynamicFont.material.mainTexture;
			}
			return null;
		}
	}

	// Token: 0x06000406 RID: 1030 RVA: 0x0001AA80 File Offset: 0x00018C80
	public void MarkAsDirty()
	{
		if (this.mReplacement != null)
		{
			this.mReplacement.MarkAsDirty();
		}
		this.RecalculateDynamicOffset();
		this.mSprite = null;
		UILabel[] array = NGUITools.FindActive<UILabel>();
		int i = 0;
		int num = array.Length;
		while (i < num)
		{
			UILabel uilabel = array[i];
			if (uilabel.enabled && NGUITools.GetActive(uilabel.gameObject) && UIFont.CheckIfRelated(this, uilabel.font))
			{
				UIFont font = uilabel.font;
				uilabel.font = null;
				uilabel.font = font;
			}
			i++;
		}
		int j = 0;
		int count = this.mSymbols.Count;
		while (j < count)
		{
			this.symbols[j].MarkAsDirty();
			j++;
		}
	}

	// Token: 0x06000407 RID: 1031 RVA: 0x0001AB50 File Offset: 0x00018D50
	public bool RecalculateDynamicOffset()
	{
		if (this.mDynamicFont != null)
		{
			this.mDynamicFont.RequestCharactersInTexture("j", this.mDynamicFontSize, this.mDynamicFontStyle);
			CharacterInfo characterInfo;
			this.mDynamicFont.GetCharacterInfo('j', out characterInfo, this.mDynamicFontSize, this.mDynamicFontStyle);
			float num = (float)this.mDynamicFontSize + characterInfo.vert.yMax;
			if (!object.Equals(this.mDynamicFontOffset, num))
			{
				this.mDynamicFontOffset = num;
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000408 RID: 1032 RVA: 0x0001ABE0 File Offset: 0x00018DE0
	public Vector2 CalculatePrintedSize(string text, bool encoding, UIFont.SymbolStyle symbolStyle)
	{
		if (this.mReplacement != null)
		{
			return this.mReplacement.CalculatePrintedSize(text, encoding, symbolStyle);
		}
		Vector2 zero = Vector2.zero;
		bool isDynamic = this.isDynamic;
		if (isDynamic || (this.mFont != null && this.mFont.isValid && !string.IsNullOrEmpty(text)))
		{
			if (encoding)
			{
				text = NGUITools.StripSymbols(text);
			}
			if (isDynamic)
			{
				this.mDynamicFont.textureRebuildCallback = new Font.FontTextureRebuildCallback(this.OnFontChanged);
				this.mDynamicFont.RequestCharactersInTexture(text, this.mDynamicFontSize, this.mDynamicFontStyle);
				this.mDynamicFont.textureRebuildCallback = null;
			}
			int length = text.Length;
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			int size = this.size;
			int num5 = size + this.mSpacingY;
			bool flag = encoding && symbolStyle != UIFont.SymbolStyle.None && this.hasSymbols;
			for (int i = 0; i < length; i++)
			{
				char c = text[i];
				if (c == '\n')
				{
					if (num2 > num)
					{
						num = num2;
					}
					num2 = 0;
					num3 += num5;
					num4 = 0;
				}
				else if (c < ' ')
				{
					num4 = 0;
				}
				else if (!isDynamic)
				{
					BMSymbol bmsymbol = (!flag) ? null : this.MatchSymbol(text, i, length);
					if (bmsymbol == null)
					{
						BMGlyph glyph = this.mFont.GetGlyph((int)c);
						if (glyph != null)
						{
							num2 += this.mSpacingX + ((num4 == 0) ? glyph.advance : (glyph.advance + glyph.GetKerning(num4)));
							num4 = (int)c;
						}
					}
					else
					{
						num2 += this.mSpacingX + bmsymbol.width;
						i += bmsymbol.length - 1;
						num4 = 0;
					}
				}
				else if (this.mDynamicFont.GetCharacterInfo(c, out UIFont.mChar, this.mDynamicFontSize, this.mDynamicFontStyle))
				{
					num2 += (int)((float)this.mSpacingX + UIFont.mChar.width);
				}
			}
			float num6 = (size <= 0) ? 1f : (1f / (float)size);
			zero.x = num6 * (float)((num2 <= num) ? num : num2);
			zero.y = num6 * (float)(num3 + num5);
		}
		return zero;
	}

	// Token: 0x06000409 RID: 1033 RVA: 0x0001AE4C File Offset: 0x0001904C
	private static void EndLine(ref StringBuilder s)
	{
		int num = s.Length - 1;
		if (num > 0 && s[num] == ' ')
		{
			s[num] = '\n';
		}
		else
		{
			s.Append('\n');
		}
	}

	// Token: 0x0600040A RID: 1034 RVA: 0x0001AE94 File Offset: 0x00019094
	public string GetEndOfLineThatFits(string text, float maxWidth, bool encoding, UIFont.SymbolStyle symbolStyle)
	{
		if (this.mReplacement != null)
		{
			return this.mReplacement.GetEndOfLineThatFits(text, maxWidth, encoding, symbolStyle);
		}
		int num = Mathf.RoundToInt(maxWidth * (float)this.size);
		if (num < 1)
		{
			return text;
		}
		int length = text.Length;
		int num2 = num;
		BMGlyph bmglyph = null;
		int num3 = length;
		bool flag = encoding && symbolStyle != UIFont.SymbolStyle.None && this.hasSymbols;
		bool isDynamic = this.isDynamic;
		if (isDynamic)
		{
			this.mDynamicFont.textureRebuildCallback = new Font.FontTextureRebuildCallback(this.OnFontChanged);
			this.mDynamicFont.RequestCharactersInTexture(text, this.mDynamicFontSize, this.mDynamicFontStyle);
			this.mDynamicFont.textureRebuildCallback = null;
		}
		while (num3 > 0 && num2 > 0)
		{
			char c = text[--num3];
			BMSymbol bmsymbol = (!flag) ? null : this.MatchSymbol(text, num3, length);
			int num4 = this.mSpacingX;
			if (!isDynamic)
			{
				if (bmsymbol != null)
				{
					num4 += bmsymbol.advance;
				}
				else
				{
					BMGlyph glyph = this.mFont.GetGlyph((int)c);
					if (glyph == null)
					{
						bmglyph = null;
						continue;
					}
					num4 += glyph.advance + ((bmglyph != null) ? bmglyph.GetKerning((int)c) : 0);
					bmglyph = glyph;
				}
			}
			else if (this.mDynamicFont.GetCharacterInfo(c, out UIFont.mChar, this.mDynamicFontSize, this.mDynamicFontStyle))
			{
				num4 += (int)UIFont.mChar.width;
			}
			num2 -= num4;
		}
		if (num2 < 0)
		{
			num3++;
		}
		return text.Substring(num3, length - num3);
	}

	// Token: 0x0600040B RID: 1035 RVA: 0x0001B04C File Offset: 0x0001924C
	public string WrapText(string text, float maxWidth, int maxLineCount, bool encoding, UIFont.SymbolStyle symbolStyle)
	{
		if (this.mReplacement != null)
		{
			return this.mReplacement.WrapText(text, maxWidth, maxLineCount, encoding, symbolStyle);
		}
		int num = Mathf.RoundToInt(maxWidth * (float)this.size);
		if (num < 1)
		{
			return text;
		}
		StringBuilder stringBuilder = new StringBuilder();
		int length = text.Length;
		int num2 = num;
		int num3 = 0;
		int num4 = 0;
		int i = 0;
		bool flag = true;
		bool flag2 = maxLineCount != 1;
		int num5 = 1;
		bool flag3 = encoding && symbolStyle != UIFont.SymbolStyle.None && this.hasSymbols;
		bool isDynamic = this.isDynamic;
		if (isDynamic)
		{
			this.mDynamicFont.textureRebuildCallback = new Font.FontTextureRebuildCallback(this.OnFontChanged);
			this.mDynamicFont.RequestCharactersInTexture(text, this.mDynamicFontSize, this.mDynamicFontStyle);
			this.mDynamicFont.textureRebuildCallback = null;
		}
		while (i < length)
		{
			char c = text[i];
			if (c == '\n')
			{
				if (!flag2 || num5 == maxLineCount)
				{
					break;
				}
				num2 = num;
				if (num4 < i)
				{
					stringBuilder.Append(text.Substring(num4, i - num4 + 1));
				}
				else
				{
					stringBuilder.Append(c);
				}
				flag = true;
				num5++;
				num4 = i + 1;
				num3 = 0;
			}
			else
			{
				if (c == ' ' && num3 != 32 && num4 < i)
				{
					stringBuilder.Append(text.Substring(num4, i - num4 + 1));
					flag = false;
					num4 = i + 1;
					num3 = (int)c;
				}
				if (encoding && c == '[' && i + 2 < length)
				{
					if (text[i + 1] == '-' && text[i + 2] == ']')
					{
						i += 2;
						goto IL_3E7;
					}
					if (i + 7 < length && text[i + 7] == ']' && NGUITools.EncodeColor(NGUITools.ParseColor(text, i + 1)) == text.Substring(i + 1, 6).ToUpper())
					{
						i += 7;
						goto IL_3E7;
					}
				}
				BMSymbol bmsymbol = (!flag3) ? null : this.MatchSymbol(text, i, length);
				int num6 = this.mSpacingX;
				if (!isDynamic)
				{
					if (bmsymbol != null)
					{
						num6 += bmsymbol.advance;
					}
					else
					{
						BMGlyph bmglyph = (bmsymbol != null) ? null : this.mFont.GetGlyph((int)c);
						if (bmglyph == null)
						{
							goto IL_3E7;
						}
						num6 += ((num3 == 0) ? bmglyph.advance : (bmglyph.advance + bmglyph.GetKerning(num3)));
					}
				}
				else if (this.mDynamicFont.GetCharacterInfo(c, out UIFont.mChar, this.mDynamicFontSize, this.mDynamicFontStyle))
				{
					num6 += Mathf.RoundToInt(UIFont.mChar.width);
				}
				num2 -= num6;
				if (num2 < 0)
				{
					if (flag || !flag2 || num5 == maxLineCount)
					{
						stringBuilder.Append(text.Substring(num4, Mathf.Max(0, i - num4)));
						if (!flag2 || num5 == maxLineCount)
						{
							num4 = i;
							break;
						}
						UIFont.EndLine(ref stringBuilder);
						flag = true;
						num5++;
						if (c == ' ')
						{
							num4 = i + 1;
							num2 = num;
						}
						else
						{
							num4 = i;
							num2 = num - num6;
						}
						num3 = 0;
					}
					else
					{
						while (num4 < length && text[num4] == ' ')
						{
							num4++;
						}
						flag = true;
						num2 = num;
						i = num4 - 1;
						num3 = 0;
						if (!flag2 || num5 == maxLineCount)
						{
							break;
						}
						num5++;
						UIFont.EndLine(ref stringBuilder);
						goto IL_3E7;
					}
				}
				else
				{
					num3 = (int)c;
				}
				if (!isDynamic && bmsymbol != null)
				{
					i += bmsymbol.length - 1;
					num3 = 0;
				}
			}
			IL_3E7:
			i++;
		}
		if (num4 < i)
		{
			stringBuilder.Append(text.Substring(num4, i - num4));
		}
		return stringBuilder.ToString();
	}

	// Token: 0x0600040C RID: 1036 RVA: 0x0001B474 File Offset: 0x00019674
	public string WrapText(string text, float maxWidth, int maxLineCount, bool encoding)
	{
		return this.WrapText(text, maxWidth, maxLineCount, encoding, UIFont.SymbolStyle.None);
	}

	// Token: 0x0600040D RID: 1037 RVA: 0x0001B484 File Offset: 0x00019684
	public string WrapText(string text, float maxWidth, int maxLineCount)
	{
		return this.WrapText(text, maxWidth, maxLineCount, false, UIFont.SymbolStyle.None);
	}

	// Token: 0x0600040E RID: 1038 RVA: 0x0001B494 File Offset: 0x00019694
	private void Align(BetterList<Vector3> verts, int indexOffset, UIFont.Alignment alignment, int x, int lineWidth)
	{
		if (alignment != UIFont.Alignment.Left)
		{
			int size = this.size;
			if (size > 0)
			{
				float num = (alignment != UIFont.Alignment.Right) ? ((float)(lineWidth - x) * 0.5f) : ((float)(lineWidth - x));
				num = (float)Mathf.RoundToInt(num);
				if (num < 0f)
				{
					num = 0f;
				}
				num /= (float)this.size;
				for (int i = indexOffset; i < verts.size; i++)
				{
					Vector3 vector = verts.buffer[i];
					vector.x += num;
					verts.buffer[i] = vector;
				}
			}
		}
	}

	// Token: 0x0600040F RID: 1039 RVA: 0x0001B544 File Offset: 0x00019744
	private void OnFontChanged()
	{
		this.MarkAsDirty();
	}

	// Token: 0x06000410 RID: 1040 RVA: 0x0001B54C File Offset: 0x0001974C
	public void Print(string text, Color32 color, BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols, bool encoding, UIFont.SymbolStyle symbolStyle, UIFont.Alignment alignment, int lineWidth, bool premultiply)
	{
		if (this.mReplacement != null)
		{
			this.mReplacement.Print(text, color, verts, uvs, cols, encoding, symbolStyle, alignment, lineWidth, premultiply);
		}
		else if (text != null)
		{
			if (!this.isValid)
			{
				Debug.LogError("Attempting to print using an invalid font!");
				return;
			}
			bool isDynamic = this.isDynamic;
			if (isDynamic)
			{
				this.mDynamicFont.textureRebuildCallback = new Font.FontTextureRebuildCallback(this.OnFontChanged);
				this.mDynamicFont.RequestCharactersInTexture(text, this.mDynamicFontSize, this.mDynamicFontStyle);
				this.mDynamicFont.textureRebuildCallback = null;
			}
			this.mColors.Clear();
			this.mColors.Add(color);
			int size = this.size;
			Vector2 vector = (size <= 0) ? Vector2.one : new Vector2(1f / (float)size, 1f / (float)size);
			int size2 = verts.size;
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			int num5 = size + this.mSpacingY;
			Vector3 zero = Vector3.zero;
			Vector3 zero2 = Vector3.zero;
			Vector2 zero3 = Vector2.zero;
			Vector2 zero4 = Vector2.zero;
			float num6 = this.uvRect.width / (float)this.mFont.texWidth;
			float num7 = this.mUVRect.height / (float)this.mFont.texHeight;
			int length = text.Length;
			bool flag = encoding && symbolStyle != UIFont.SymbolStyle.None && this.hasSymbols && this.sprite != null;
			for (int i = 0; i < length; i++)
			{
				char c = text[i];
				if (c == '\n')
				{
					if (num2 > num)
					{
						num = num2;
					}
					if (alignment != UIFont.Alignment.Left)
					{
						this.Align(verts, size2, alignment, num2, lineWidth);
						size2 = verts.size;
					}
					num2 = 0;
					num3 += num5;
					num4 = 0;
				}
				else if (c < ' ')
				{
					num4 = 0;
				}
				else
				{
					if (encoding && c == '[')
					{
						int num8 = NGUITools.ParseSymbol(text, i, this.mColors, premultiply);
						if (num8 > 0)
						{
							color = this.mColors[this.mColors.Count - 1];
							i += num8 - 1;
							goto IL_96C;
						}
					}
					if (!isDynamic)
					{
						BMSymbol bmsymbol = (!flag) ? null : this.MatchSymbol(text, i, length);
						if (bmsymbol == null)
						{
							BMGlyph glyph = this.mFont.GetGlyph((int)c);
							if (glyph == null)
							{
								goto IL_96C;
							}
							if (num4 != 0)
							{
								num2 += glyph.GetKerning(num4);
							}
							if (c == ' ')
							{
								num2 += this.mSpacingX + glyph.advance;
								num4 = (int)c;
								goto IL_96C;
							}
							zero.x = vector.x * (float)(num2 + glyph.offsetX);
							zero.y = -vector.y * (float)(num3 + glyph.offsetY);
							zero2.x = zero.x + vector.x * (float)glyph.width;
							zero2.y = zero.y - vector.y * (float)glyph.height;
							zero3.x = this.mUVRect.xMin + num6 * (float)glyph.x;
							zero3.y = this.mUVRect.yMax - num7 * (float)glyph.y;
							zero4.x = zero3.x + num6 * (float)glyph.width;
							zero4.y = zero3.y - num7 * (float)glyph.height;
							num2 += this.mSpacingX + glyph.advance;
							num4 = (int)c;
							if (glyph.channel == 0 || glyph.channel == 15)
							{
								for (int j = 0; j < 4; j++)
								{
									cols.Add(color);
								}
							}
							else
							{
								Color color2 = color;
								color2 *= 0.49f;
								switch (glyph.channel)
								{
								case 1:
									color2.b += 0.51f;
									break;
								case 2:
									color2.g += 0.51f;
									break;
								case 4:
									color2.r += 0.51f;
									break;
								case 8:
									color2.a += 0.51f;
									break;
								}
								for (int k = 0; k < 4; k++)
								{
									cols.Add(color2);
								}
							}
						}
						else
						{
							zero.x = vector.x * (float)(num2 + bmsymbol.offsetX);
							zero.y = -vector.y * (float)(num3 + bmsymbol.offsetY);
							zero2.x = zero.x + vector.x * (float)bmsymbol.width;
							zero2.y = zero.y - vector.y * (float)bmsymbol.height;
							Rect uvRect = bmsymbol.uvRect;
							zero3.x = uvRect.xMin;
							zero3.y = uvRect.yMax;
							zero4.x = uvRect.xMax;
							zero4.y = uvRect.yMin;
							num2 += this.mSpacingX + bmsymbol.advance;
							i += bmsymbol.length - 1;
							num4 = 0;
							if (symbolStyle == UIFont.SymbolStyle.Colored)
							{
								for (int l = 0; l < 4; l++)
								{
									cols.Add(color);
								}
							}
							else
							{
								Color32 item = Color.white;
								item.a = color.a;
								for (int m = 0; m < 4; m++)
								{
									cols.Add(item);
								}
							}
						}
						verts.Add(new Vector3(zero2.x, zero.y));
						verts.Add(new Vector3(zero2.x, zero2.y));
						verts.Add(new Vector3(zero.x, zero2.y));
						verts.Add(new Vector3(zero.x, zero.y));
						uvs.Add(new Vector2(zero4.x, zero3.y));
						uvs.Add(new Vector2(zero4.x, zero4.y));
						uvs.Add(new Vector2(zero3.x, zero4.y));
						uvs.Add(new Vector2(zero3.x, zero3.y));
					}
					else if (this.mDynamicFont.GetCharacterInfo(c, out UIFont.mChar, this.mDynamicFontSize, this.mDynamicFontStyle))
					{
						zero.x = vector.x * ((float)num2 + UIFont.mChar.vert.xMin);
						zero.y = -vector.y * ((float)num3 - UIFont.mChar.vert.yMax + this.mDynamicFontOffset);
						zero2.x = zero.x + vector.x * UIFont.mChar.vert.width;
						zero2.y = zero.y - vector.y * UIFont.mChar.vert.height;
						zero3.x = UIFont.mChar.uv.xMin;
						zero3.y = UIFont.mChar.uv.yMin;
						zero4.x = UIFont.mChar.uv.xMax;
						zero4.y = UIFont.mChar.uv.yMax;
						num2 += this.mSpacingX + (int)UIFont.mChar.width;
						for (int n = 0; n < 4; n++)
						{
							cols.Add(color);
						}
						if (UIFont.mChar.flipped)
						{
							uvs.Add(new Vector2(zero3.x, zero4.y));
							uvs.Add(new Vector2(zero3.x, zero3.y));
							uvs.Add(new Vector2(zero4.x, zero3.y));
							uvs.Add(new Vector2(zero4.x, zero4.y));
						}
						else
						{
							uvs.Add(new Vector2(zero4.x, zero3.y));
							uvs.Add(new Vector2(zero3.x, zero3.y));
							uvs.Add(new Vector2(zero3.x, zero4.y));
							uvs.Add(new Vector2(zero4.x, zero4.y));
						}
						verts.Add(new Vector3(zero2.x, zero.y));
						verts.Add(new Vector3(zero.x, zero.y));
						verts.Add(new Vector3(zero.x, zero2.y));
						verts.Add(new Vector3(zero2.x, zero2.y));
					}
				}
				IL_96C:;
			}
			if (alignment != UIFont.Alignment.Left && size2 < verts.size)
			{
				this.Align(verts, size2, alignment, num2, lineWidth);
				size2 = verts.size;
			}
		}
	}

	// Token: 0x06000411 RID: 1041 RVA: 0x0001BEFC File Offset: 0x0001A0FC
	private BMSymbol GetSymbol(string sequence, bool createIfMissing)
	{
		int i = 0;
		int count = this.mSymbols.Count;
		while (i < count)
		{
			BMSymbol bmsymbol = this.mSymbols[i];
			if (bmsymbol.sequence == sequence)
			{
				return bmsymbol;
			}
			i++;
		}
		if (createIfMissing)
		{
			BMSymbol bmsymbol2 = new BMSymbol();
			bmsymbol2.sequence = sequence;
			this.mSymbols.Add(bmsymbol2);
			return bmsymbol2;
		}
		return null;
	}

	// Token: 0x06000412 RID: 1042 RVA: 0x0001BF6C File Offset: 0x0001A16C
	private BMSymbol MatchSymbol(string text, int offset, int textLength)
	{
		int count = this.mSymbols.Count;
		if (count == 0)
		{
			return null;
		}
		textLength -= offset;
		for (int i = 0; i < count; i++)
		{
			BMSymbol bmsymbol = this.mSymbols[i];
			int length = bmsymbol.length;
			if (length != 0 && textLength >= length)
			{
				bool flag = true;
				for (int j = 0; j < length; j++)
				{
					if (text[offset + j] != bmsymbol.sequence[j])
					{
						flag = false;
						break;
					}
				}
				if (flag && bmsymbol.Validate(this.atlas))
				{
					return bmsymbol;
				}
			}
		}
		return null;
	}

	// Token: 0x06000413 RID: 1043 RVA: 0x0001C024 File Offset: 0x0001A224
	public void AddSymbol(string sequence, string spriteName)
	{
		BMSymbol symbol = this.GetSymbol(sequence, true);
		symbol.spriteName = spriteName;
		this.MarkAsDirty();
	}

	// Token: 0x06000414 RID: 1044 RVA: 0x0001C048 File Offset: 0x0001A248
	public void RemoveSymbol(string sequence)
	{
		BMSymbol symbol = this.GetSymbol(sequence, false);
		if (symbol != null)
		{
			this.symbols.Remove(symbol);
		}
		this.MarkAsDirty();
	}

	// Token: 0x06000415 RID: 1045 RVA: 0x0001C078 File Offset: 0x0001A278
	public void RenameSymbol(string before, string after)
	{
		BMSymbol symbol = this.GetSymbol(before, false);
		if (symbol != null)
		{
			symbol.sequence = after;
		}
		this.MarkAsDirty();
	}

	// Token: 0x06000416 RID: 1046 RVA: 0x0001C0A4 File Offset: 0x0001A2A4
	public bool UsesSprite(string s)
	{
		if (!string.IsNullOrEmpty(s))
		{
			if (s.Equals(this.spriteName))
			{
				return true;
			}
			int i = 0;
			int count = this.symbols.Count;
			while (i < count)
			{
				BMSymbol bmsymbol = this.symbols[i];
				if (s.Equals(bmsymbol.spriteName))
				{
					return true;
				}
				i++;
			}
		}
		return false;
	}

	// Token: 0x0400041D RID: 1053
	[HideInInspector]
	[SerializeField]
	private Material mMat;

	// Token: 0x0400041E RID: 1054
	[SerializeField]
	[HideInInspector]
	private Rect mUVRect = new Rect(0f, 0f, 1f, 1f);

	// Token: 0x0400041F RID: 1055
	[HideInInspector]
	[SerializeField]
	private BMFont mFont = new BMFont();

	// Token: 0x04000420 RID: 1056
	[HideInInspector]
	[SerializeField]
	private int mSpacingX;

	// Token: 0x04000421 RID: 1057
	[HideInInspector]
	[SerializeField]
	private int mSpacingY;

	// Token: 0x04000422 RID: 1058
	[HideInInspector]
	[SerializeField]
	private UIAtlas mAtlas;

	// Token: 0x04000423 RID: 1059
	[HideInInspector]
	[SerializeField]
	private UIFont mReplacement;

	// Token: 0x04000424 RID: 1060
	[SerializeField]
	[HideInInspector]
	private float mPixelSize = 1f;

	// Token: 0x04000425 RID: 1061
	[HideInInspector]
	[SerializeField]
	private List<BMSymbol> mSymbols = new List<BMSymbol>();

	// Token: 0x04000426 RID: 1062
	[HideInInspector]
	[SerializeField]
	private Font mDynamicFont;

	// Token: 0x04000427 RID: 1063
	[SerializeField]
	[HideInInspector]
	private int mDynamicFontSize = 16;

	// Token: 0x04000428 RID: 1064
	[HideInInspector]
	[SerializeField]
	private FontStyle mDynamicFontStyle;

	// Token: 0x04000429 RID: 1065
	[HideInInspector]
	[SerializeField]
	private float mDynamicFontOffset;

	// Token: 0x0400042A RID: 1066
	private UIAtlas.Sprite mSprite;

	// Token: 0x0400042B RID: 1067
	private int mPMA = -1;

	// Token: 0x0400042C RID: 1068
	private bool mSpriteSet;

	// Token: 0x0400042D RID: 1069
	private List<Color> mColors = new List<Color>();

	// Token: 0x0400042E RID: 1070
	private static CharacterInfo mChar;

	// Token: 0x0200007E RID: 126
	public enum Alignment
	{
		// Token: 0x04000430 RID: 1072
		Left,
		// Token: 0x04000431 RID: 1073
		Center,
		// Token: 0x04000432 RID: 1074
		Right
	}

	// Token: 0x0200007F RID: 127
	public enum SymbolStyle
	{
		// Token: 0x04000434 RID: 1076
		None,
		// Token: 0x04000435 RID: 1077
		Uncolored,
		// Token: 0x04000436 RID: 1078
		Colored
	}
}
