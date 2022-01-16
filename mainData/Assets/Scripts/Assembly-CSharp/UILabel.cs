using System;
using UnityEngine;

// Token: 0x02000083 RID: 131
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/Label")]
public class UILabel : UIWidget
{
	// Token: 0x170000A6 RID: 166
	// (get) Token: 0x0600042E RID: 1070 RVA: 0x0001CE00 File Offset: 0x0001B000
	// (set) Token: 0x0600042F RID: 1071 RVA: 0x0001CE98 File Offset: 0x0001B098
	private bool hasChanged
	{
		get
		{
			return this.mShouldBeProcessed || this.mLastText != this.text || this.mLastWidth != this.mMaxLineWidth || this.mLastEncoding != this.mEncoding || this.mLastCount != this.mMaxLineCount || this.mLastPass != this.mPassword || this.mLastShow != this.mShowLastChar || this.mLastEffect != this.mEffectStyle;
		}
		set
		{
			if (value)
			{
				this.mChanged = true;
				this.mShouldBeProcessed = true;
			}
			else
			{
				this.mShouldBeProcessed = false;
				this.mLastText = this.text;
				this.mLastWidth = this.mMaxLineWidth;
				this.mLastEncoding = this.mEncoding;
				this.mLastCount = this.mMaxLineCount;
				this.mLastPass = this.mPassword;
				this.mLastShow = this.mShowLastChar;
				this.mLastEffect = this.mEffectStyle;
			}
		}
	}

	// Token: 0x170000A7 RID: 167
	// (get) Token: 0x06000430 RID: 1072 RVA: 0x0001CF1C File Offset: 0x0001B11C
	// (set) Token: 0x06000431 RID: 1073 RVA: 0x0001CF24 File Offset: 0x0001B124
	public UIFont font
	{
		get
		{
			return this.mFont;
		}
		set
		{
			if (this.mFont != value)
			{
				this.mFont = value;
				this.material = ((!(this.mFont != null)) ? null : this.mFont.material);
				this.mChanged = true;
				this.hasChanged = true;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x170000A8 RID: 168
	// (get) Token: 0x06000432 RID: 1074 RVA: 0x0001CF88 File Offset: 0x0001B188
	// (set) Token: 0x06000433 RID: 1075 RVA: 0x0001CF90 File Offset: 0x0001B190
	public string text
	{
		get
		{
			return this.mText;
		}
		set
		{
			if (string.IsNullOrEmpty(value))
			{
				if (!string.IsNullOrEmpty(this.mText))
				{
					this.mText = string.Empty;
				}
				this.hasChanged = true;
			}
			else if (this.mText != value)
			{
				this.mText = value;
				this.hasChanged = true;
			}
		}
	}

	// Token: 0x170000A9 RID: 169
	// (get) Token: 0x06000434 RID: 1076 RVA: 0x0001CFF0 File Offset: 0x0001B1F0
	// (set) Token: 0x06000435 RID: 1077 RVA: 0x0001CFF8 File Offset: 0x0001B1F8
	public bool supportEncoding
	{
		get
		{
			return this.mEncoding;
		}
		set
		{
			if (this.mEncoding != value)
			{
				this.mEncoding = value;
				this.hasChanged = true;
				if (value)
				{
					this.mPassword = false;
				}
			}
		}
	}

	// Token: 0x170000AA RID: 170
	// (get) Token: 0x06000436 RID: 1078 RVA: 0x0001D024 File Offset: 0x0001B224
	// (set) Token: 0x06000437 RID: 1079 RVA: 0x0001D02C File Offset: 0x0001B22C
	public UIFont.SymbolStyle symbolStyle
	{
		get
		{
			return this.mSymbols;
		}
		set
		{
			if (this.mSymbols != value)
			{
				this.mSymbols = value;
				this.hasChanged = true;
			}
		}
	}

	// Token: 0x170000AB RID: 171
	// (get) Token: 0x06000438 RID: 1080 RVA: 0x0001D048 File Offset: 0x0001B248
	// (set) Token: 0x06000439 RID: 1081 RVA: 0x0001D050 File Offset: 0x0001B250
	public int lineWidth
	{
		get
		{
			return this.mMaxLineWidth;
		}
		set
		{
			if (this.mMaxLineWidth != value)
			{
				this.mMaxLineWidth = value;
				this.hasChanged = true;
			}
		}
	}

	// Token: 0x170000AC RID: 172
	// (get) Token: 0x0600043A RID: 1082 RVA: 0x0001D06C File Offset: 0x0001B26C
	// (set) Token: 0x0600043B RID: 1083 RVA: 0x0001D07C File Offset: 0x0001B27C
	public bool multiLine
	{
		get
		{
			return this.mMaxLineCount != 1;
		}
		set
		{
			if (this.mMaxLineCount != 1 != value)
			{
				this.mMaxLineCount = ((!value) ? 1 : 0);
				this.hasChanged = true;
				if (value)
				{
					this.mPassword = false;
				}
			}
		}
	}

	// Token: 0x170000AD RID: 173
	// (get) Token: 0x0600043C RID: 1084 RVA: 0x0001D0B8 File Offset: 0x0001B2B8
	// (set) Token: 0x0600043D RID: 1085 RVA: 0x0001D0C0 File Offset: 0x0001B2C0
	public int maxLineCount
	{
		get
		{
			return this.mMaxLineCount;
		}
		set
		{
			if (this.mMaxLineCount != value)
			{
				this.mMaxLineCount = Mathf.Max(value, 0);
				this.hasChanged = true;
				if (value == 1)
				{
					this.mPassword = false;
				}
			}
		}
	}

	// Token: 0x170000AE RID: 174
	// (get) Token: 0x0600043E RID: 1086 RVA: 0x0001D0FC File Offset: 0x0001B2FC
	// (set) Token: 0x0600043F RID: 1087 RVA: 0x0001D104 File Offset: 0x0001B304
	public bool password
	{
		get
		{
			return this.mPassword;
		}
		set
		{
			if (this.mPassword != value)
			{
				if (value)
				{
					this.mMaxLineCount = 1;
					this.mEncoding = false;
				}
				this.mPassword = value;
				this.hasChanged = true;
			}
		}
	}

	// Token: 0x170000AF RID: 175
	// (get) Token: 0x06000440 RID: 1088 RVA: 0x0001D140 File Offset: 0x0001B340
	// (set) Token: 0x06000441 RID: 1089 RVA: 0x0001D148 File Offset: 0x0001B348
	public bool showLastPasswordChar
	{
		get
		{
			return this.mShowLastChar;
		}
		set
		{
			if (this.mShowLastChar != value)
			{
				this.mShowLastChar = value;
				this.hasChanged = true;
			}
		}
	}

	// Token: 0x170000B0 RID: 176
	// (get) Token: 0x06000442 RID: 1090 RVA: 0x0001D164 File Offset: 0x0001B364
	// (set) Token: 0x06000443 RID: 1091 RVA: 0x0001D16C File Offset: 0x0001B36C
	public UILabel.Effect effectStyle
	{
		get
		{
			return this.mEffectStyle;
		}
		set
		{
			if (this.mEffectStyle != value)
			{
				this.mEffectStyle = value;
				this.hasChanged = true;
			}
		}
	}

	// Token: 0x170000B1 RID: 177
	// (get) Token: 0x06000444 RID: 1092 RVA: 0x0001D188 File Offset: 0x0001B388
	// (set) Token: 0x06000445 RID: 1093 RVA: 0x0001D190 File Offset: 0x0001B390
	public Color effectColor
	{
		get
		{
			return this.mEffectColor;
		}
		set
		{
			if (!this.mEffectColor.Equals(value))
			{
				this.mEffectColor = value;
				if (this.mEffectStyle != UILabel.Effect.None)
				{
					this.hasChanged = true;
				}
			}
		}
	}

	// Token: 0x170000B2 RID: 178
	// (get) Token: 0x06000446 RID: 1094 RVA: 0x0001D1C4 File Offset: 0x0001B3C4
	// (set) Token: 0x06000447 RID: 1095 RVA: 0x0001D1CC File Offset: 0x0001B3CC
	public Vector2 effectDistance
	{
		get
		{
			return this.mEffectDistance;
		}
		set
		{
			if (this.mEffectDistance != value)
			{
				this.mEffectDistance = value;
				this.hasChanged = true;
			}
		}
	}

	// Token: 0x170000B3 RID: 179
	// (get) Token: 0x06000448 RID: 1096 RVA: 0x0001D1F0 File Offset: 0x0001B3F0
	// (set) Token: 0x06000449 RID: 1097 RVA: 0x0001D1F8 File Offset: 0x0001B3F8
	public bool shrinkToFit
	{
		get
		{
			return this.mShrinkToFit;
		}
		set
		{
			if (this.mShrinkToFit != value)
			{
				this.mShrinkToFit = value;
				if ((float)this.mMaxLineWidth > 0f)
				{
					this.hasChanged = true;
				}
			}
		}
	}

	// Token: 0x170000B4 RID: 180
	// (get) Token: 0x0600044A RID: 1098 RVA: 0x0001D228 File Offset: 0x0001B428
	public string processedText
	{
		get
		{
			if (this.mLastScale != base.cachedTransform.localScale)
			{
				this.mLastScale = base.cachedTransform.localScale;
				this.mShouldBeProcessed = true;
			}
			if (this.hasChanged)
			{
				this.ProcessText();
			}
			return this.mProcessedText;
		}
	}

	// Token: 0x170000B5 RID: 181
	// (get) Token: 0x0600044B RID: 1099 RVA: 0x0001D280 File Offset: 0x0001B480
	public override Material material
	{
		get
		{
			Material material = base.material;
			if (material == null)
			{
				material = ((!(this.mFont != null)) ? null : this.mFont.material);
				this.material = material;
			}
			return material;
		}
	}

	// Token: 0x170000B6 RID: 182
	// (get) Token: 0x0600044C RID: 1100 RVA: 0x0001D2CC File Offset: 0x0001B4CC
	public override Vector2 relativeSize
	{
		get
		{
			if (this.mFont == null)
			{
				return Vector3.one;
			}
			if (this.hasChanged)
			{
				this.ProcessText();
			}
			return this.mSize;
		}
	}

	// Token: 0x0600044D RID: 1101 RVA: 0x0001D314 File Offset: 0x0001B514
	protected override void OnStart()
	{
		if (this.mLineWidth > 0f)
		{
			this.mMaxLineWidth = Mathf.RoundToInt(this.mLineWidth);
			this.mLineWidth = 0f;
		}
		if (!this.mMultiline)
		{
			this.mMaxLineCount = 1;
			this.mMultiline = true;
		}
		if (Languages.replaceFontMapping != null && Languages.replaceFontMapping.ContainsKey(this.font.name))
		{
			this.font = Languages.replaceFontMapping[this.font.name];
		}
		this.mPremultiply = (this.font != null && this.font.material != null && this.font.material.shader.name.Contains("Premultiplied"));
	}

	// Token: 0x0600044E RID: 1102 RVA: 0x0001D3F4 File Offset: 0x0001B5F4
	public override void MarkAsChanged()
	{
		this.hasChanged = true;
		base.MarkAsChanged();
	}

	// Token: 0x0600044F RID: 1103 RVA: 0x0001D404 File Offset: 0x0001B604
	private void ProcessText()
	{
		this.mChanged = true;
		this.hasChanged = false;
		this.mLastText = this.mText;
		this.mProcessedText = this.mText;
		if (this.mPassword)
		{
			string text = string.Empty;
			if (this.mShowLastChar)
			{
				int i = 0;
				int num = this.mProcessedText.Length - 1;
				while (i < num)
				{
					text += "*";
					i++;
				}
				if (this.mProcessedText.Length > 0)
				{
					text += this.mProcessedText[this.mProcessedText.Length - 1];
				}
			}
			else
			{
				int j = 0;
				int length = this.mProcessedText.Length;
				while (j < length)
				{
					text += "*";
					j++;
				}
			}
			this.mProcessedText = this.mFont.WrapText(text, (float)this.mMaxLineWidth / base.cachedTransform.localScale.x, this.mMaxLineCount, false, UIFont.SymbolStyle.None);
		}
		else if (!this.mShrinkToFit)
		{
			if (this.mMaxLineWidth > 0)
			{
				this.mProcessedText = this.mFont.WrapText(this.mProcessedText, (float)this.mMaxLineWidth / base.cachedTransform.localScale.x, this.mMaxLineCount, this.mEncoding, this.mSymbols);
			}
			else if (this.mMaxLineCount > 0)
			{
				this.mProcessedText = this.mFont.WrapText(this.mProcessedText, 100000f, this.mMaxLineCount, this.mEncoding, this.mSymbols);
			}
		}
		float num2 = Mathf.Abs(base.cachedTransform.localScale.x);
		this.mSize = (string.IsNullOrEmpty(this.mProcessedText) ? Vector2.one : this.mFont.CalculatePrintedSize(this.mProcessedText, this.mEncoding, this.mSymbols));
		if (this.mShrinkToFit && this.mMaxLineWidth > 0)
		{
			if (num2 > 0f)
			{
				float num3 = (float)this.mMaxLineWidth / (float)this.mFont.size;
				num2 = ((this.mSize.x <= num3) ? ((float)this.mFont.size) : (num3 / this.mSize.x * (float)this.mFont.size));
				base.cachedTransform.localScale = new Vector3(num2, num2, 1f);
			}
			else
			{
				this.mSize.x = 1f;
				base.cachedTransform.localScale = new Vector3((float)this.mFont.size, (float)this.mFont.size, 1f);
			}
		}
		else
		{
			this.mSize.x = Mathf.Max(this.mSize.x, (num2 <= 0f) ? 1f : ((float)this.lineWidth / num2));
		}
		this.mSize.y = Mathf.Max(this.mSize.y, 1f);
	}

	// Token: 0x06000450 RID: 1104 RVA: 0x0001D74C File Offset: 0x0001B94C
	public void MakePositionPerfect()
	{
		float pixelSize = this.font.pixelSize;
		Vector3 localScale = base.cachedTransform.localScale;
		if (this.mFont.size == Mathf.RoundToInt(localScale.x / pixelSize) && this.mFont.size == Mathf.RoundToInt(localScale.y / pixelSize) && base.cachedTransform.localRotation == Quaternion.identity)
		{
			Vector2 vector = this.relativeSize * localScale.x;
			int num = Mathf.RoundToInt(vector.x / pixelSize);
			int num2 = Mathf.RoundToInt(vector.y / pixelSize);
			Vector3 localPosition = base.cachedTransform.localPosition;
			localPosition.x = (float)Mathf.FloorToInt(localPosition.x / pixelSize);
			localPosition.y = (float)Mathf.CeilToInt(localPosition.y / pixelSize);
			localPosition.z = (float)Mathf.RoundToInt(localPosition.z);
			if (num % 2 == 1 && (base.pivot == UIWidget.Pivot.Top || base.pivot == UIWidget.Pivot.Center || base.pivot == UIWidget.Pivot.Bottom))
			{
				localPosition.x += 0.5f;
			}
			if (num2 % 2 == 1 && (base.pivot == UIWidget.Pivot.Left || base.pivot == UIWidget.Pivot.Center || base.pivot == UIWidget.Pivot.Right))
			{
				localPosition.y -= 0.5f;
			}
			localPosition.x *= pixelSize;
			localPosition.y *= pixelSize;
			if (base.cachedTransform.localPosition != localPosition)
			{
				base.cachedTransform.localPosition = localPosition;
			}
		}
	}

	// Token: 0x06000451 RID: 1105 RVA: 0x0001D90C File Offset: 0x0001BB0C
	public override void MakePixelPerfect()
	{
		if (this.mFont != null)
		{
			float pixelSize = this.font.pixelSize;
			Vector3 localScale = base.cachedTransform.localScale;
			localScale.x = (float)this.mFont.size * pixelSize;
			localScale.y = localScale.x;
			localScale.z = 1f;
			Vector2 vector = this.relativeSize * localScale.x;
			int num = Mathf.RoundToInt(vector.x / pixelSize);
			int num2 = Mathf.RoundToInt(vector.y / pixelSize);
			Vector3 localPosition = base.cachedTransform.localPosition;
			localPosition.x = (float)(Mathf.CeilToInt(localPosition.x / pixelSize * 4f) >> 2);
			localPosition.y = (float)(Mathf.CeilToInt(localPosition.y / pixelSize * 4f) >> 2);
			localPosition.z = (float)Mathf.RoundToInt(localPosition.z);
			if (base.cachedTransform.localRotation == Quaternion.identity)
			{
				if (num % 2 == 1 && (base.pivot == UIWidget.Pivot.Top || base.pivot == UIWidget.Pivot.Center || base.pivot == UIWidget.Pivot.Bottom))
				{
					localPosition.x += 0.5f;
				}
				if (num2 % 2 == 1 && (base.pivot == UIWidget.Pivot.Left || base.pivot == UIWidget.Pivot.Center || base.pivot == UIWidget.Pivot.Right))
				{
					localPosition.y += 0.5f;
				}
			}
			localPosition.x *= pixelSize;
			localPosition.y *= pixelSize;
			base.cachedTransform.localPosition = localPosition;
			base.cachedTransform.localScale = localScale;
		}
		else
		{
			base.MakePixelPerfect();
		}
	}

	// Token: 0x06000452 RID: 1106 RVA: 0x0001DAE0 File Offset: 0x0001BCE0
	private void ApplyShadow(BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols, int start, int end, float x, float y)
	{
		Color color = this.mEffectColor;
		color.a *= base.alpha * this.mPanel.alpha;
		Color32 color2 = (!this.font.premultipliedAlpha) ? color : NGUITools.ApplyPMA(color);
		for (int i = start; i < end; i++)
		{
			verts.Add(verts.buffer[i]);
			uvs.Add(uvs.buffer[i]);
			cols.Add(cols.buffer[i]);
			Vector3 vector = verts.buffer[i];
			vector.x += x;
			vector.y += y;
			verts.buffer[i] = vector;
			cols.buffer[i] = color2;
		}
	}

	// Token: 0x06000453 RID: 1107 RVA: 0x0001DBE8 File Offset: 0x0001BDE8
	public override void OnFill(BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols)
	{
		if (this.mFont == null)
		{
			return;
		}
		this.MakePositionPerfect();
		UIWidget.Pivot pivot = base.pivot;
		int start = verts.size;
		Color c = base.color;
		c.a *= this.mPanel.alpha;
		if (this.font.premultipliedAlpha)
		{
			c = NGUITools.ApplyPMA(c);
		}
		if (pivot == UIWidget.Pivot.Left || pivot == UIWidget.Pivot.TopLeft || pivot == UIWidget.Pivot.BottomLeft)
		{
			this.mFont.Print(this.processedText, c, verts, uvs, cols, this.mEncoding, this.mSymbols, UIFont.Alignment.Left, 0, this.mPremultiply);
		}
		else if (pivot == UIWidget.Pivot.Right || pivot == UIWidget.Pivot.TopRight || pivot == UIWidget.Pivot.BottomRight)
		{
			this.mFont.Print(this.processedText, c, verts, uvs, cols, this.mEncoding, this.mSymbols, UIFont.Alignment.Right, Mathf.RoundToInt(this.relativeSize.x * (float)this.mFont.size), this.mPremultiply);
		}
		else
		{
			this.mFont.Print(this.processedText, c, verts, uvs, cols, this.mEncoding, this.mSymbols, UIFont.Alignment.Center, Mathf.RoundToInt(this.relativeSize.x * (float)this.mFont.size), this.mPremultiply);
		}
		if (this.effectStyle != UILabel.Effect.None)
		{
			Vector3 localScale = base.cachedTransform.localScale;
			if (localScale.x == 0f || localScale.y == 0f)
			{
				return;
			}
			int size = verts.size;
			float num = 1f / (float)this.mFont.size;
			float num2 = num * this.mEffectDistance.x;
			float num3 = num * this.mEffectDistance.y;
			this.ApplyShadow(verts, uvs, cols, start, size, num2, -num3);
			if (this.effectStyle == UILabel.Effect.Outline)
			{
				start = size;
				size = verts.size;
				this.ApplyShadow(verts, uvs, cols, start, size, -num2, num3);
				start = size;
				size = verts.size;
				this.ApplyShadow(verts, uvs, cols, start, size, num2, num3);
				start = size;
				size = verts.size;
				this.ApplyShadow(verts, uvs, cols, start, size, -num2, -num3);
			}
		}
	}

	// Token: 0x04000456 RID: 1110
	[HideInInspector]
	[SerializeField]
	private UIFont mFont;

	// Token: 0x04000457 RID: 1111
	[HideInInspector]
	[SerializeField]
	private string mText = string.Empty;

	// Token: 0x04000458 RID: 1112
	[HideInInspector]
	[SerializeField]
	private int mMaxLineWidth;

	// Token: 0x04000459 RID: 1113
	[SerializeField]
	[HideInInspector]
	private bool mEncoding = true;

	// Token: 0x0400045A RID: 1114
	[SerializeField]
	[HideInInspector]
	private int mMaxLineCount;

	// Token: 0x0400045B RID: 1115
	[SerializeField]
	[HideInInspector]
	private bool mPassword;

	// Token: 0x0400045C RID: 1116
	[SerializeField]
	[HideInInspector]
	private bool mShowLastChar;

	// Token: 0x0400045D RID: 1117
	[HideInInspector]
	[SerializeField]
	private UILabel.Effect mEffectStyle;

	// Token: 0x0400045E RID: 1118
	[HideInInspector]
	[SerializeField]
	private Color mEffectColor = Color.black;

	// Token: 0x0400045F RID: 1119
	[HideInInspector]
	[SerializeField]
	private UIFont.SymbolStyle mSymbols = UIFont.SymbolStyle.Uncolored;

	// Token: 0x04000460 RID: 1120
	[SerializeField]
	[HideInInspector]
	private Vector2 mEffectDistance = Vector2.one;

	// Token: 0x04000461 RID: 1121
	[SerializeField]
	[HideInInspector]
	private bool mShrinkToFit;

	// Token: 0x04000462 RID: 1122
	[HideInInspector]
	[SerializeField]
	private float mLineWidth;

	// Token: 0x04000463 RID: 1123
	[HideInInspector]
	[SerializeField]
	private bool mMultiline = true;

	// Token: 0x04000464 RID: 1124
	private bool mShouldBeProcessed = true;

	// Token: 0x04000465 RID: 1125
	private string mProcessedText;

	// Token: 0x04000466 RID: 1126
	private Vector3 mLastScale = Vector3.one;

	// Token: 0x04000467 RID: 1127
	private string mLastText = string.Empty;

	// Token: 0x04000468 RID: 1128
	private int mLastWidth;

	// Token: 0x04000469 RID: 1129
	private bool mLastEncoding = true;

	// Token: 0x0400046A RID: 1130
	private int mLastCount;

	// Token: 0x0400046B RID: 1131
	private bool mLastPass;

	// Token: 0x0400046C RID: 1132
	private bool mLastShow;

	// Token: 0x0400046D RID: 1133
	private UILabel.Effect mLastEffect;

	// Token: 0x0400046E RID: 1134
	private Vector3 mSize = Vector3.zero;

	// Token: 0x0400046F RID: 1135
	private bool mPremultiply;

	// Token: 0x02000084 RID: 132
	public enum Effect
	{
		// Token: 0x04000471 RID: 1137
		None,
		// Token: 0x04000472 RID: 1138
		Shadow,
		// Token: 0x04000473 RID: 1139
		Outline
	}
}
