using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// Token: 0x02000092 RID: 146
[AddComponentMenu("NGUI/UI/Text List")]
public class UITextList : MonoBehaviour
{
	// Token: 0x060004CD RID: 1229 RVA: 0x00022B94 File Offset: 0x00020D94
	public void Clear()
	{
		this.mParagraphs.Clear();
		this.UpdateVisibleText();
	}

	// Token: 0x060004CE RID: 1230 RVA: 0x00022BA8 File Offset: 0x00020DA8
	public void Add(string text)
	{
		this.Add(text, true);
	}

	// Token: 0x060004CF RID: 1231 RVA: 0x00022BB4 File Offset: 0x00020DB4
	protected void Add(string text, bool updateVisible)
	{
		UITextList.Paragraph paragraph;
		if (this.mParagraphs.Count < this.maxEntries)
		{
			paragraph = new UITextList.Paragraph();
		}
		else
		{
			paragraph = this.mParagraphs[0];
			this.mParagraphs.RemoveAt(0);
		}
		paragraph.text = text;
		this.mParagraphs.Add(paragraph);
		if (this.textLabel != null && this.textLabel.font != null)
		{
			paragraph.lines = this.textLabel.font.WrapText(paragraph.text, this.maxWidth / this.textLabel.transform.localScale.y, this.textLabel.maxLineCount, this.textLabel.supportEncoding, this.textLabel.symbolStyle).Split(this.mSeparator);
			this.mTotalLines = 0;
			int i = 0;
			int count = this.mParagraphs.Count;
			while (i < count)
			{
				this.mTotalLines += this.mParagraphs[i].lines.Length;
				i++;
			}
		}
		if (updateVisible)
		{
			this.UpdateVisibleText();
		}
	}

	// Token: 0x060004D0 RID: 1232 RVA: 0x00022CF0 File Offset: 0x00020EF0
	private void Awake()
	{
		if (this.textLabel == null)
		{
			this.textLabel = base.GetComponentInChildren<UILabel>();
		}
		if (this.textLabel != null)
		{
			this.textLabel.lineWidth = 0;
		}
		Collider collider = base.collider;
		if (collider != null)
		{
			if (this.maxHeight <= 0f)
			{
				this.maxHeight = collider.bounds.size.y / base.transform.lossyScale.y;
			}
			if (this.maxWidth <= 0f)
			{
				this.maxWidth = collider.bounds.size.x / base.transform.lossyScale.x;
			}
		}
	}

	// Token: 0x060004D1 RID: 1233 RVA: 0x00022DD0 File Offset: 0x00020FD0
	private void OnSelect(bool selected)
	{
		this.mSelected = selected;
	}

	// Token: 0x060004D2 RID: 1234 RVA: 0x00022DDC File Offset: 0x00020FDC
	protected void UpdateVisibleText()
	{
		if (this.textLabel != null)
		{
			UIFont font = this.textLabel.font;
			if (font != null)
			{
				int num = 0;
				int num2 = (this.maxHeight <= 0f) ? 100000 : Mathf.FloorToInt(this.maxHeight / this.textLabel.cachedTransform.localScale.y);
				int num3 = Mathf.RoundToInt(this.mScroll);
				if (num2 + num3 > this.mTotalLines)
				{
					num3 = Mathf.Max(0, this.mTotalLines - num2);
					this.mScroll = (float)num3;
				}
				if (this.style == UITextList.Style.Chat)
				{
					num3 = Mathf.Max(0, this.mTotalLines - num2 - num3);
				}
				StringBuilder stringBuilder = new StringBuilder();
				int i = 0;
				int count = this.mParagraphs.Count;
				while (i < count)
				{
					UITextList.Paragraph paragraph = this.mParagraphs[i];
					int j = 0;
					int num4 = paragraph.lines.Length;
					while (j < num4)
					{
						string value = paragraph.lines[j];
						if (num3 > 0)
						{
							num3--;
						}
						else
						{
							if (stringBuilder.Length > 0)
							{
								stringBuilder.Append("\n");
							}
							stringBuilder.Append(value);
							num++;
							if (num >= num2)
							{
								break;
							}
						}
						j++;
					}
					if (num >= num2)
					{
						break;
					}
					i++;
				}
				this.textLabel.text = stringBuilder.ToString();
			}
		}
	}

	// Token: 0x060004D3 RID: 1235 RVA: 0x00022F6C File Offset: 0x0002116C
	private void OnScroll(float val)
	{
		if (this.mSelected && this.supportScrollWheel)
		{
			val *= ((this.style != UITextList.Style.Chat) ? -10f : 10f);
			this.mScroll = Mathf.Max(0f, this.mScroll + val);
			this.UpdateVisibleText();
		}
	}

	// Token: 0x040004E0 RID: 1248
	public UITextList.Style style;

	// Token: 0x040004E1 RID: 1249
	public UILabel textLabel;

	// Token: 0x040004E2 RID: 1250
	public float maxWidth;

	// Token: 0x040004E3 RID: 1251
	public float maxHeight;

	// Token: 0x040004E4 RID: 1252
	public int maxEntries = 50;

	// Token: 0x040004E5 RID: 1253
	public bool supportScrollWheel = true;

	// Token: 0x040004E6 RID: 1254
	protected char[] mSeparator = new char[]
	{
		'\n'
	};

	// Token: 0x040004E7 RID: 1255
	protected List<UITextList.Paragraph> mParagraphs = new List<UITextList.Paragraph>();

	// Token: 0x040004E8 RID: 1256
	protected float mScroll;

	// Token: 0x040004E9 RID: 1257
	protected bool mSelected;

	// Token: 0x040004EA RID: 1258
	protected int mTotalLines;

	// Token: 0x02000093 RID: 147
	public enum Style
	{
		// Token: 0x040004EC RID: 1260
		Text,
		// Token: 0x040004ED RID: 1261
		Chat
	}

	// Token: 0x02000094 RID: 148
	protected class Paragraph
	{
		// Token: 0x040004EE RID: 1262
		public string text;

		// Token: 0x040004EF RID: 1263
		public string[] lines;
	}
}
