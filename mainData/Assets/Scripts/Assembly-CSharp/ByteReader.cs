using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// Token: 0x02000053 RID: 83
public class ByteReader
{
	// Token: 0x0600027F RID: 639 RVA: 0x000112B4 File Offset: 0x0000F4B4
	public ByteReader(byte[] bytes)
	{
		this.mBuffer = bytes;
	}

	// Token: 0x06000280 RID: 640 RVA: 0x000112C4 File Offset: 0x0000F4C4
	public ByteReader(TextAsset asset)
	{
		this.mBuffer = asset.bytes;
	}

	// Token: 0x17000047 RID: 71
	// (get) Token: 0x06000281 RID: 641 RVA: 0x000112D8 File Offset: 0x0000F4D8
	public bool canRead
	{
		get
		{
			return this.mBuffer != null && this.mOffset < this.mBuffer.Length;
		}
	}

	// Token: 0x06000282 RID: 642 RVA: 0x000112F8 File Offset: 0x0000F4F8
	private static string ReadLine(byte[] buffer, int start, int count)
	{
		return Encoding.UTF8.GetString(buffer, start, count);
	}

	// Token: 0x06000283 RID: 643 RVA: 0x00011308 File Offset: 0x0000F508
	public string ReadLine()
	{
		int num = this.mBuffer.Length;
		while (this.mOffset < num && this.mBuffer[this.mOffset] < 32)
		{
			this.mOffset++;
		}
		int i = this.mOffset;
		if (i < num)
		{
			while (i < num)
			{
				int num2 = (int)this.mBuffer[i++];
				if (num2 == 10 || num2 == 13)
				{
					IL_81:
					string result = ByteReader.ReadLine(this.mBuffer, this.mOffset, i - this.mOffset - 1);
					this.mOffset = i;
					return result;
				}
			}
			i++;
			goto IL_81;
		}
		this.mOffset = num;
		return null;
	}

	// Token: 0x06000284 RID: 644 RVA: 0x000113C8 File Offset: 0x0000F5C8
	public Dictionary<string, string> ReadDictionary()
	{
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		char[] separator = new char[]
		{
			'='
		};
		while (this.canRead)
		{
			string text = this.ReadLine();
			if (text == null)
			{
				break;
			}
			if (!text.StartsWith("//"))
			{
				string[] array = text.Split(separator, 2, StringSplitOptions.RemoveEmptyEntries);
				if (array.Length == 2)
				{
					string key = array[0].Trim();
					string value = array[1].Trim().Replace("\\n", "\n");
					dictionary[key] = value;
				}
			}
		}
		return dictionary;
	}

	// Token: 0x040002F5 RID: 757
	private byte[] mBuffer;

	// Token: 0x040002F6 RID: 758
	private int mOffset;
}
