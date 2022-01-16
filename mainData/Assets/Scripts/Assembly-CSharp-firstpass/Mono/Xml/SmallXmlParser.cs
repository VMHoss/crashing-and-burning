using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Text;

namespace Mono.Xml
{
	// Token: 0x0200003D RID: 61
	public class SmallXmlParser
	{
		// Token: 0x06000227 RID: 551 RVA: 0x000089F8 File Offset: 0x00006BF8
		private Exception Error(string msg)
		{
			return new SmallXmlParserException(msg, this.line, this.column);
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00008A0C File Offset: 0x00006C0C
		private Exception UnexpectedEndError()
		{
			string[] array = new string[this.elementNames.Count];
			((ICollection)this.elementNames).CopyTo(array, 0);
			return this.Error(string.Format("Unexpected end of stream. Element stack content is {0}", string.Join(",", array)));
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00008A54 File Offset: 0x00006C54
		private bool IsNameChar(char c, bool start)
		{
			if (c == '-' || c == '.')
			{
				return !start;
			}
			if (c == ':' || c == '_')
			{
				return true;
			}
			if (c > 'Ā')
			{
				if (c == 'ۥ' || c == 'ۦ' || c == 'ՙ')
				{
					return true;
				}
				if ('ʻ' <= c && c <= 'ˁ')
				{
					return true;
				}
			}
			switch (char.GetUnicodeCategory(c))
			{
			case UnicodeCategory.UppercaseLetter:
			case UnicodeCategory.LowercaseLetter:
			case UnicodeCategory.TitlecaseLetter:
			case UnicodeCategory.OtherLetter:
			case UnicodeCategory.LetterNumber:
				return true;
			case UnicodeCategory.ModifierLetter:
			case UnicodeCategory.NonSpacingMark:
			case UnicodeCategory.SpacingCombiningMark:
			case UnicodeCategory.EnclosingMark:
			case UnicodeCategory.DecimalDigitNumber:
				return !start;
			default:
				return false;
			}
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00008B20 File Offset: 0x00006D20
		private bool IsWhitespace(int c)
		{
			switch (c)
			{
			case 9:
			case 10:
			case 13:
				break;
			default:
				if (c != 32)
				{
					return false;
				}
				break;
			}
			return true;
		}

		// Token: 0x0600022B RID: 555 RVA: 0x00008B5C File Offset: 0x00006D5C
		public void SkipWhitespaces()
		{
			this.SkipWhitespaces(false);
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00008B68 File Offset: 0x00006D68
		private void HandleWhitespaces()
		{
			while (this.IsWhitespace(this.Peek()))
			{
				this.buffer.Append((char)this.Read());
			}
			if (this.Peek() != 60 && this.Peek() >= 0)
			{
				this.isWhitespace = false;
			}
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00008BC0 File Offset: 0x00006DC0
		public void SkipWhitespaces(bool expected)
		{
			for (;;)
			{
				int num = this.Peek();
				switch (num)
				{
				case 9:
				case 10:
				case 13:
					break;
				default:
					if (num != 32)
					{
						goto Block_0;
					}
					break;
				}
				this.Read();
				if (expected)
				{
					expected = false;
				}
			}
			Block_0:
			if (expected)
			{
				throw this.Error("Whitespace is expected.");
			}
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00008C2C File Offset: 0x00006E2C
		private int Peek()
		{
			return this.reader.Peek();
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00008C3C File Offset: 0x00006E3C
		private int Read()
		{
			int num = this.reader.Read();
			if (num == 10)
			{
				this.resetColumn = true;
			}
			if (this.resetColumn)
			{
				this.line++;
				this.resetColumn = false;
				this.column = 1;
			}
			else
			{
				this.column++;
			}
			return num;
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00008CA0 File Offset: 0x00006EA0
		public void Expect(int c)
		{
			int num = this.Read();
			if (num < 0)
			{
				throw this.UnexpectedEndError();
			}
			if (num != c)
			{
				throw this.Error(string.Format("Expected '{0}' but got {1}", (char)c, (char)num));
			}
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00008CE8 File Offset: 0x00006EE8
		private string ReadUntil(char until, bool handleReferences)
		{
			while (this.Peek() >= 0)
			{
				char c = (char)this.Read();
				if (c == until)
				{
					string result = this.buffer.ToString();
					this.buffer.Length = 0;
					return result;
				}
				if (handleReferences && c == '&')
				{
					this.ReadReference();
				}
				else
				{
					this.buffer.Append(c);
				}
			}
			throw this.UnexpectedEndError();
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00008D60 File Offset: 0x00006F60
		public string ReadName()
		{
			int num = 0;
			if (this.Peek() < 0 || !this.IsNameChar((char)this.Peek(), true))
			{
				throw this.Error("XML name start character is expected.");
			}
			for (int i = this.Peek(); i >= 0; i = this.Peek())
			{
				char c = (char)i;
				if (!this.IsNameChar(c, false))
				{
					break;
				}
				if (num == this.nameBuffer.Length)
				{
					char[] destinationArray = new char[num * 2];
					Array.Copy(this.nameBuffer, 0, destinationArray, 0, num);
					this.nameBuffer = destinationArray;
				}
				this.nameBuffer[num++] = c;
				this.Read();
			}
			if (num == 0)
			{
				throw this.Error("Valid XML name is expected.");
			}
			return new string(this.nameBuffer, 0, num);
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00008E2C File Offset: 0x0000702C
		public void Parse(TextReader input, SmallXmlParser.IContentHandler handler)
		{
			this.reader = input;
			this.handler = handler;
			handler.OnStartParsing(this);
			while (this.Peek() >= 0)
			{
				this.ReadContent();
			}
			this.HandleBufferedContent();
			if (this.elementNames.Count > 0)
			{
				throw this.Error(string.Format("Insufficient close tag: {0}", this.elementNames.Peek()));
			}
			handler.OnEndParsing(this);
			this.Cleanup();
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00008EA8 File Offset: 0x000070A8
		private void Cleanup()
		{
			this.line = 1;
			this.column = 0;
			this.handler = null;
			this.reader = null;
			this.elementNames.Clear();
			this.xmlSpaces.Clear();
			this.attributes.Clear();
			this.buffer.Length = 0;
			this.xmlSpace = null;
			this.isWhitespace = false;
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00008F0C File Offset: 0x0000710C
		public void ReadContent()
		{
			if (this.IsWhitespace(this.Peek()))
			{
				if (this.buffer.Length == 0)
				{
					this.isWhitespace = true;
				}
				this.HandleWhitespaces();
			}
			if (this.Peek() != 60)
			{
				this.ReadCharacters();
				return;
			}
			this.Read();
			int num = this.Peek();
			if (num != 33)
			{
				if (num != 47)
				{
					string text;
					if (num != 63)
					{
						this.HandleBufferedContent();
						text = this.ReadName();
						while (this.Peek() != 62 && this.Peek() != 47)
						{
							this.ReadAttribute(this.attributes);
						}
						this.handler.OnStartElement(text, this.attributes);
						this.attributes.Clear();
						this.SkipWhitespaces();
						if (this.Peek() == 47)
						{
							this.Read();
							this.handler.OnEndElement(text);
						}
						else
						{
							this.elementNames.Push(text);
							this.xmlSpaces.Push(this.xmlSpace);
						}
						this.Expect(62);
						return;
					}
					this.HandleBufferedContent();
					this.Read();
					text = this.ReadName();
					this.SkipWhitespaces();
					string text2 = string.Empty;
					if (this.Peek() != 63)
					{
						for (;;)
						{
							text2 += this.ReadUntil('?', false);
							if (this.Peek() == 62)
							{
								break;
							}
							text2 += "?";
						}
					}
					this.handler.OnProcessingInstruction(text, text2);
					this.Expect(62);
					return;
				}
				else
				{
					this.HandleBufferedContent();
					if (this.elementNames.Count == 0)
					{
						throw this.UnexpectedEndError();
					}
					this.Read();
					string text = this.ReadName();
					this.SkipWhitespaces();
					string text3 = (string)this.elementNames.Pop();
					this.xmlSpaces.Pop();
					if (this.xmlSpaces.Count > 0)
					{
						this.xmlSpace = (string)this.xmlSpaces.Peek();
					}
					else
					{
						this.xmlSpace = null;
					}
					if (text != text3)
					{
						throw this.Error(string.Format("End tag mismatch: expected {0} but found {1}", text3, text));
					}
					this.handler.OnEndElement(text);
					this.Expect(62);
					return;
				}
			}
			else
			{
				this.Read();
				if (this.Peek() == 91)
				{
					this.Read();
					if (this.ReadName() != "CDATA")
					{
						throw this.Error("Invalid declaration markup");
					}
					this.Expect(91);
					this.ReadCDATASection();
					return;
				}
				else
				{
					if (this.Peek() == 45)
					{
						this.ReadComment();
						return;
					}
					if (this.ReadName() != "DOCTYPE")
					{
						throw this.Error("Invalid declaration markup.");
					}
					throw this.Error("This parser does not support document type.");
				}
			}
		}

		// Token: 0x06000236 RID: 566 RVA: 0x000091E4 File Offset: 0x000073E4
		private void HandleBufferedContent()
		{
			if (this.buffer.Length == 0)
			{
				return;
			}
			if (this.isWhitespace)
			{
				this.handler.OnIgnorableWhitespace(this.buffer.ToString());
			}
			else
			{
				this.handler.OnChars(this.buffer.ToString());
			}
			this.buffer.Length = 0;
			this.isWhitespace = false;
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00009254 File Offset: 0x00007454
		private void ReadCharacters()
		{
			this.isWhitespace = false;
			for (;;)
			{
				int num = this.Peek();
				int num2 = num;
				if (num2 == -1)
				{
					break;
				}
				if (num2 != 38)
				{
					if (num2 == 60)
					{
						return;
					}
					this.buffer.Append((char)this.Read());
				}
				else
				{
					this.Read();
					this.ReadReference();
				}
			}
		}

		// Token: 0x06000238 RID: 568 RVA: 0x000092C0 File Offset: 0x000074C0
		private void ReadReference()
		{
			if (this.Peek() != 35)
			{
				string text = this.ReadName();
				this.Expect(59);
				string text2 = text;
				switch (text2)
				{
				case "amp":
					this.buffer.Append('&');
					return;
				case "quot":
					this.buffer.Append('"');
					return;
				case "apos":
					this.buffer.Append('\'');
					return;
				case "lt":
					this.buffer.Append('<');
					return;
				case "gt":
					this.buffer.Append('>');
					return;
				}
				throw this.Error("General non-predefined entity reference is not supported in this parser.");
			}
			this.Read();
			this.ReadCharacterReference();
		}

		// Token: 0x06000239 RID: 569 RVA: 0x000093F4 File Offset: 0x000075F4
		private int ReadCharacterReference()
		{
			int num = 0;
			if (this.Peek() == 120)
			{
				this.Read();
				for (int i = this.Peek(); i >= 0; i = this.Peek())
				{
					if (48 <= i && i <= 57)
					{
						num <<= 4 + i - 48;
					}
					else if (65 <= i && i <= 70)
					{
						num <<= 4 + i - 65 + 10;
					}
					else
					{
						if (97 > i || i > 102)
						{
							break;
						}
						num <<= 4 + i - 97 + 10;
					}
					this.Read();
				}
			}
			else
			{
				for (int j = this.Peek(); j >= 0; j = this.Peek())
				{
					if (48 > j || j > 57)
					{
						break;
					}
					num <<= 4 + j - 48;
					this.Read();
				}
			}
			return num;
		}

		// Token: 0x0600023A RID: 570 RVA: 0x000094F4 File Offset: 0x000076F4
		private void ReadAttribute(SmallXmlParser.AttrListImpl a)
		{
			this.SkipWhitespaces(true);
			if (this.Peek() == 47 || this.Peek() == 62)
			{
				return;
			}
			string text = this.ReadName();
			this.SkipWhitespaces();
			this.Expect(61);
			this.SkipWhitespaces();
			int num = this.Read();
			string value;
			if (num != 34)
			{
				if (num != 39)
				{
					throw this.Error("Invalid attribute value markup.");
				}
				value = this.ReadUntil('\'', true);
			}
			else
			{
				value = this.ReadUntil('"', true);
			}
			if (text == "xml:space")
			{
				this.xmlSpace = value;
			}
			a.Add(text, value);
		}

		// Token: 0x0600023B RID: 571 RVA: 0x000095A4 File Offset: 0x000077A4
		private void ReadCDATASection()
		{
			int num = 0;
			while (this.Peek() >= 0)
			{
				char c = (char)this.Read();
				if (c == ']')
				{
					num++;
				}
				else
				{
					if (c == '>' && num > 1)
					{
						for (int i = num; i > 2; i--)
						{
							this.buffer.Append(']');
						}
						return;
					}
					for (int j = 0; j < num; j++)
					{
						this.buffer.Append(']');
					}
					num = 0;
					this.buffer.Append(c);
				}
			}
			throw this.UnexpectedEndError();
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00009648 File Offset: 0x00007848
		private void ReadComment()
		{
			this.Expect(45);
			this.Expect(45);
			for (;;)
			{
				if (this.Read() == 45)
				{
					if (this.Read() == 45)
					{
						break;
					}
				}
			}
			if (this.Read() != 62)
			{
				throw this.Error("'--' is not allowed inside comment markup.");
			}
		}

		// Token: 0x040000C3 RID: 195
		private SmallXmlParser.IContentHandler handler;

		// Token: 0x040000C4 RID: 196
		private TextReader reader;

		// Token: 0x040000C5 RID: 197
		private Stack elementNames = new Stack();

		// Token: 0x040000C6 RID: 198
		private Stack xmlSpaces = new Stack();

		// Token: 0x040000C7 RID: 199
		private string xmlSpace;

		// Token: 0x040000C8 RID: 200
		private StringBuilder buffer = new StringBuilder(200);

		// Token: 0x040000C9 RID: 201
		private char[] nameBuffer = new char[30];

		// Token: 0x040000CA RID: 202
		private bool isWhitespace;

		// Token: 0x040000CB RID: 203
		private SmallXmlParser.AttrListImpl attributes = new SmallXmlParser.AttrListImpl();

		// Token: 0x040000CC RID: 204
		private int line = 1;

		// Token: 0x040000CD RID: 205
		private int column;

		// Token: 0x040000CE RID: 206
		private bool resetColumn;

		// Token: 0x0200003E RID: 62
		public interface IContentHandler
		{
			// Token: 0x0600023D RID: 573
			void OnStartParsing(SmallXmlParser parser);

			// Token: 0x0600023E RID: 574
			void OnEndParsing(SmallXmlParser parser);

			// Token: 0x0600023F RID: 575
			void OnStartElement(string name, SmallXmlParser.IAttrList attrs);

			// Token: 0x06000240 RID: 576
			void OnEndElement(string name);

			// Token: 0x06000241 RID: 577
			void OnProcessingInstruction(string name, string text);

			// Token: 0x06000242 RID: 578
			void OnChars(string text);

			// Token: 0x06000243 RID: 579
			void OnIgnorableWhitespace(string text);
		}

		// Token: 0x0200003F RID: 63
		public interface IAttrList
		{
			// Token: 0x17000021 RID: 33
			// (get) Token: 0x06000244 RID: 580
			int Length { get; }

			// Token: 0x17000022 RID: 34
			// (get) Token: 0x06000245 RID: 581
			bool IsEmpty { get; }

			// Token: 0x06000246 RID: 582
			string GetName(int i);

			// Token: 0x06000247 RID: 583
			string GetValue(int i);

			// Token: 0x06000248 RID: 584
			string GetValue(string name);

			// Token: 0x17000023 RID: 35
			// (get) Token: 0x06000249 RID: 585
			string[] Names { get; }

			// Token: 0x17000024 RID: 36
			// (get) Token: 0x0600024A RID: 586
			string[] Values { get; }
		}

		// Token: 0x02000040 RID: 64
		private class AttrListImpl : SmallXmlParser.IAttrList
		{
			// Token: 0x17000025 RID: 37
			// (get) Token: 0x0600024C RID: 588 RVA: 0x000096CC File Offset: 0x000078CC
			public int Length
			{
				get
				{
					return this.attrNames.Count;
				}
			}

			// Token: 0x17000026 RID: 38
			// (get) Token: 0x0600024D RID: 589 RVA: 0x000096DC File Offset: 0x000078DC
			public bool IsEmpty
			{
				get
				{
					return this.attrNames.Count == 0;
				}
			}

			// Token: 0x0600024E RID: 590 RVA: 0x000096EC File Offset: 0x000078EC
			public string GetName(int i)
			{
				return (string)this.attrNames[i];
			}

			// Token: 0x0600024F RID: 591 RVA: 0x00009700 File Offset: 0x00007900
			public string GetValue(int i)
			{
				return (string)this.attrValues[i];
			}

			// Token: 0x06000250 RID: 592 RVA: 0x00009714 File Offset: 0x00007914
			public string GetValue(string name)
			{
				for (int i = 0; i < this.attrNames.Count; i++)
				{
					if ((string)this.attrNames[i] == name)
					{
						return (string)this.attrValues[i];
					}
				}
				return null;
			}

			// Token: 0x17000027 RID: 39
			// (get) Token: 0x06000251 RID: 593 RVA: 0x0000976C File Offset: 0x0000796C
			public string[] Names
			{
				get
				{
					return (string[])this.attrNames.ToArray(typeof(string));
				}
			}

			// Token: 0x17000028 RID: 40
			// (get) Token: 0x06000252 RID: 594 RVA: 0x00009788 File Offset: 0x00007988
			public string[] Values
			{
				get
				{
					return (string[])this.attrValues.ToArray(typeof(string));
				}
			}

			// Token: 0x06000253 RID: 595 RVA: 0x000097A4 File Offset: 0x000079A4
			internal void Clear()
			{
				this.attrNames.Clear();
				this.attrValues.Clear();
			}

			// Token: 0x06000254 RID: 596 RVA: 0x000097BC File Offset: 0x000079BC
			internal void Add(string name, string value)
			{
				this.attrNames.Add(name);
				this.attrValues.Add(value);
			}

			// Token: 0x040000D0 RID: 208
			private ArrayList attrNames = new ArrayList();

			// Token: 0x040000D1 RID: 209
			private ArrayList attrValues = new ArrayList();
		}
	}
}
