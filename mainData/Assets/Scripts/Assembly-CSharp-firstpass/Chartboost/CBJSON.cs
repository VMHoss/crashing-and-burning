using System;
using System.Collections;
using System.IO;
using System.Text;

namespace Chartboost
{
	// Token: 0x0200000C RID: 12
	public static class CBJSON
	{
		// Token: 0x0600005C RID: 92 RVA: 0x000037AC File Offset: 0x000019AC
		public static object Deserialize(string json)
		{
			if (json == null)
			{
				return null;
			}
			return CBJSON.Parser.Parse(json);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000037BC File Offset: 0x000019BC
		public static string Serialize(object obj)
		{
			return CBJSON.Serializer.Serialize(obj);
		}

		// Token: 0x0200000D RID: 13
		private sealed class Parser : IDisposable
		{
			// Token: 0x0600005E RID: 94 RVA: 0x000037C4 File Offset: 0x000019C4
			private Parser(string jsonString)
			{
				this.json = new StringReader(jsonString);
			}

			// Token: 0x0600005F RID: 95 RVA: 0x000037D8 File Offset: 0x000019D8
			public static bool IsWordBreak(char c)
			{
				return char.IsWhiteSpace(c) || "{}[],:\"".IndexOf(c) != -1;
			}

			// Token: 0x06000060 RID: 96 RVA: 0x000037FC File Offset: 0x000019FC
			public static object Parse(string jsonString)
			{
				object result;
				using (CBJSON.Parser parser = new CBJSON.Parser(jsonString))
				{
					result = parser.ParseValue();
				}
				return result;
			}

			// Token: 0x06000061 RID: 97 RVA: 0x0000384C File Offset: 0x00001A4C
			public void Dispose()
			{
				this.json.Dispose();
				this.json = null;
			}

			// Token: 0x06000062 RID: 98 RVA: 0x00003860 File Offset: 0x00001A60
			private Hashtable ParseObject()
			{
				Hashtable hashtable = new Hashtable();
				this.json.Read();
				for (;;)
				{
					CBJSON.Parser.TOKEN nextToken = this.NextToken;
					switch (nextToken)
					{
					case CBJSON.Parser.TOKEN.NONE:
						goto IL_37;
					default:
						if (nextToken != CBJSON.Parser.TOKEN.COMMA)
						{
							string text = this.ParseString();
							if (text == null)
							{
								goto Block_2;
							}
							if (this.NextToken != CBJSON.Parser.TOKEN.COLON)
							{
								goto Block_3;
							}
							this.json.Read();
							hashtable[text] = this.ParseValue();
						}
						break;
					case CBJSON.Parser.TOKEN.CURLY_CLOSE:
						return hashtable;
					}
				}
				IL_37:
				return null;
				Block_2:
				return null;
				Block_3:
				return null;
			}

			// Token: 0x06000063 RID: 99 RVA: 0x000038EC File Offset: 0x00001AEC
			private ArrayList ParseArray()
			{
				ArrayList arrayList = new ArrayList();
				this.json.Read();
				bool flag = true;
				while (flag)
				{
					CBJSON.Parser.TOKEN nextToken = this.NextToken;
					CBJSON.Parser.TOKEN token = nextToken;
					switch (token)
					{
					case CBJSON.Parser.TOKEN.SQUARED_CLOSE:
						flag = false;
						break;
					default:
					{
						if (token == CBJSON.Parser.TOKEN.NONE)
						{
							return null;
						}
						object value = this.ParseByToken(nextToken);
						arrayList.Add(value);
						break;
					}
					case CBJSON.Parser.TOKEN.COMMA:
						break;
					}
				}
				return arrayList;
			}

			// Token: 0x06000064 RID: 100 RVA: 0x00003968 File Offset: 0x00001B68
			private object ParseValue()
			{
				CBJSON.Parser.TOKEN nextToken = this.NextToken;
				return this.ParseByToken(nextToken);
			}

			// Token: 0x06000065 RID: 101 RVA: 0x00003984 File Offset: 0x00001B84
			private object ParseByToken(CBJSON.Parser.TOKEN token)
			{
				switch (token)
				{
				case CBJSON.Parser.TOKEN.CURLY_OPEN:
					return this.ParseObject();
				case CBJSON.Parser.TOKEN.SQUARED_OPEN:
					return this.ParseArray();
				case CBJSON.Parser.TOKEN.STRING:
					return this.ParseString();
				case CBJSON.Parser.TOKEN.NUMBER:
					return this.ParseNumber();
				case CBJSON.Parser.TOKEN.TRUE:
					return true;
				case CBJSON.Parser.TOKEN.FALSE:
					return false;
				case CBJSON.Parser.TOKEN.NULL:
					return null;
				}
				return null;
			}

			// Token: 0x06000066 RID: 102 RVA: 0x000039FC File Offset: 0x00001BFC
			private string ParseString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				this.json.Read();
				bool flag = true;
				while (flag)
				{
					if (this.json.Peek() == -1)
					{
						break;
					}
					char nextChar = this.NextChar;
					char c = nextChar;
					if (c != '"')
					{
						if (c != '\\')
						{
							stringBuilder.Append(nextChar);
						}
						else if (this.json.Peek() == -1)
						{
							flag = false;
						}
						else
						{
							nextChar = this.NextChar;
							char c2 = nextChar;
							switch (c2)
							{
							case 'n':
								stringBuilder.Append('\n');
								break;
							default:
								if (c2 != '"' && c2 != '/' && c2 != '\\')
								{
									if (c2 != 'b')
									{
										if (c2 == 'f')
										{
											stringBuilder.Append('\f');
										}
									}
									else
									{
										stringBuilder.Append('\b');
									}
								}
								else
								{
									stringBuilder.Append(nextChar);
								}
								break;
							case 'r':
								stringBuilder.Append('\r');
								break;
							case 't':
								stringBuilder.Append('\t');
								break;
							case 'u':
							{
								char[] array = new char[4];
								for (int i = 0; i < 4; i++)
								{
									array[i] = this.NextChar;
								}
								stringBuilder.Append((char)Convert.ToInt32(new string(array), 16));
								break;
							}
							}
						}
					}
					else
					{
						flag = false;
					}
				}
				return stringBuilder.ToString();
			}

			// Token: 0x06000067 RID: 103 RVA: 0x00003B94 File Offset: 0x00001D94
			private object ParseNumber()
			{
				string nextWord = this.NextWord;
				if (nextWord.IndexOf('.') == -1)
				{
					long num;
					long.TryParse(nextWord, out num);
					return num;
				}
				double num2;
				double.TryParse(nextWord, out num2);
				return num2;
			}

			// Token: 0x06000068 RID: 104 RVA: 0x00003BD8 File Offset: 0x00001DD8
			private void EatWhitespace()
			{
				while (char.IsWhiteSpace(this.PeekChar))
				{
					this.json.Read();
					if (this.json.Peek() == -1)
					{
						break;
					}
				}
			}

			// Token: 0x17000001 RID: 1
			// (get) Token: 0x06000069 RID: 105 RVA: 0x00003C14 File Offset: 0x00001E14
			private char PeekChar
			{
				get
				{
					return Convert.ToChar(this.json.Peek());
				}
			}

			// Token: 0x17000002 RID: 2
			// (get) Token: 0x0600006A RID: 106 RVA: 0x00003C28 File Offset: 0x00001E28
			private char NextChar
			{
				get
				{
					return Convert.ToChar(this.json.Read());
				}
			}

			// Token: 0x17000003 RID: 3
			// (get) Token: 0x0600006B RID: 107 RVA: 0x00003C3C File Offset: 0x00001E3C
			private string NextWord
			{
				get
				{
					StringBuilder stringBuilder = new StringBuilder();
					while (!CBJSON.Parser.IsWordBreak(this.PeekChar))
					{
						stringBuilder.Append(this.NextChar);
						if (this.json.Peek() == -1)
						{
							break;
						}
					}
					return stringBuilder.ToString();
				}
			}

			// Token: 0x17000004 RID: 4
			// (get) Token: 0x0600006C RID: 108 RVA: 0x00003C90 File Offset: 0x00001E90
			private CBJSON.Parser.TOKEN NextToken
			{
				get
				{
					this.EatWhitespace();
					if (this.json.Peek() == -1)
					{
						return CBJSON.Parser.TOKEN.NONE;
					}
					char peekChar = this.PeekChar;
					switch (peekChar)
					{
					case '"':
						return CBJSON.Parser.TOKEN.STRING;
					default:
						switch (peekChar)
						{
						case '[':
							return CBJSON.Parser.TOKEN.SQUARED_OPEN;
						default:
						{
							switch (peekChar)
							{
							case '{':
								return CBJSON.Parser.TOKEN.CURLY_OPEN;
							case '}':
								this.json.Read();
								return CBJSON.Parser.TOKEN.CURLY_CLOSE;
							}
							string nextWord = this.NextWord;
							switch (nextWord)
							{
							case "false":
								return CBJSON.Parser.TOKEN.FALSE;
							case "true":
								return CBJSON.Parser.TOKEN.TRUE;
							case "null":
								return CBJSON.Parser.TOKEN.NULL;
							}
							return CBJSON.Parser.TOKEN.NONE;
						}
						case ']':
							this.json.Read();
							return CBJSON.Parser.TOKEN.SQUARED_CLOSE;
						}
						break;
					case ',':
						this.json.Read();
						return CBJSON.Parser.TOKEN.COMMA;
					case '-':
					case '0':
					case '1':
					case '2':
					case '3':
					case '4':
					case '5':
					case '6':
					case '7':
					case '8':
					case '9':
						return CBJSON.Parser.TOKEN.NUMBER;
					case ':':
						return CBJSON.Parser.TOKEN.COLON;
					}
				}
			}

			// Token: 0x04000028 RID: 40
			private const string WORD_BREAK = "{}[],:\"";

			// Token: 0x04000029 RID: 41
			private StringReader json;

			// Token: 0x0200000E RID: 14
			private enum TOKEN
			{
				// Token: 0x0400002C RID: 44
				NONE,
				// Token: 0x0400002D RID: 45
				CURLY_OPEN,
				// Token: 0x0400002E RID: 46
				CURLY_CLOSE,
				// Token: 0x0400002F RID: 47
				SQUARED_OPEN,
				// Token: 0x04000030 RID: 48
				SQUARED_CLOSE,
				// Token: 0x04000031 RID: 49
				COLON,
				// Token: 0x04000032 RID: 50
				COMMA,
				// Token: 0x04000033 RID: 51
				STRING,
				// Token: 0x04000034 RID: 52
				NUMBER,
				// Token: 0x04000035 RID: 53
				TRUE,
				// Token: 0x04000036 RID: 54
				FALSE,
				// Token: 0x04000037 RID: 55
				NULL
			}
		}

		// Token: 0x0200000F RID: 15
		private sealed class Serializer
		{
			// Token: 0x0600006D RID: 109 RVA: 0x00003E08 File Offset: 0x00002008
			private Serializer()
			{
				this.builder = new StringBuilder();
			}

			// Token: 0x0600006E RID: 110 RVA: 0x00003E1C File Offset: 0x0000201C
			public static string Serialize(object obj)
			{
				CBJSON.Serializer serializer = new CBJSON.Serializer();
				serializer.SerializeValue(obj);
				return serializer.builder.ToString();
			}

			// Token: 0x0600006F RID: 111 RVA: 0x00003E44 File Offset: 0x00002044
			private void SerializeValue(object val)
			{
				string str;
				ArrayList anArray;
				Hashtable obj;
				if (val == null)
				{
					this.builder.Append("null");
				}
				else if ((str = (val as string)) != null)
				{
					this.SerializeString(str);
				}
				else if (val is bool)
				{
					this.builder.Append((!(bool)val) ? "false" : "true");
				}
				else if ((anArray = (val as ArrayList)) != null)
				{
					this.SerializeArray(anArray);
				}
				else if ((obj = (val as Hashtable)) != null)
				{
					this.SerializeObject(obj);
				}
				else if (val is char)
				{
					this.SerializeString(new string((char)val, 1));
				}
				else
				{
					this.SerializeOther(val);
				}
			}

			// Token: 0x06000070 RID: 112 RVA: 0x00003F18 File Offset: 0x00002118
			private void SerializeObject(Hashtable obj)
			{
				bool flag = true;
				this.builder.Append('{');
				foreach (object obj2 in obj)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj2;
					if (!flag)
					{
						this.builder.Append(',');
					}
					this.SerializeString(dictionaryEntry.Key.ToString());
					this.builder.Append(':');
					this.SerializeValue(dictionaryEntry.Value);
					flag = false;
				}
				this.builder.Append('}');
			}

			// Token: 0x06000071 RID: 113 RVA: 0x00003FDC File Offset: 0x000021DC
			private void SerializeArray(ArrayList anArray)
			{
				this.builder.Append('[');
				bool flag = true;
				for (int i = 0; i < anArray.Count; i++)
				{
					object val = anArray[i];
					if (!flag)
					{
						this.builder.Append(',');
					}
					this.SerializeValue(val);
					flag = false;
				}
				this.builder.Append(']');
			}

			// Token: 0x06000072 RID: 114 RVA: 0x00004044 File Offset: 0x00002244
			private void SerializeString(string str)
			{
				this.builder.Append('"');
				foreach (char c in str.ToCharArray())
				{
					char c2 = c;
					switch (c2)
					{
					case '\b':
						this.builder.Append("\\b");
						break;
					case '\t':
						this.builder.Append("\\t");
						break;
					case '\n':
						this.builder.Append("\\n");
						break;
					default:
						if (c2 != '"')
						{
							if (c2 != '\\')
							{
								int num = Convert.ToInt32(c);
								if (num >= 32 && num <= 126)
								{
									this.builder.Append(c);
								}
								else
								{
									this.builder.Append("\\u");
									this.builder.Append(num.ToString("x4"));
								}
							}
							else
							{
								this.builder.Append("\\\\");
							}
						}
						else
						{
							this.builder.Append("\\\"");
						}
						break;
					case '\f':
						this.builder.Append("\\f");
						break;
					case '\r':
						this.builder.Append("\\r");
						break;
					}
				}
				this.builder.Append('"');
			}

			// Token: 0x06000073 RID: 115 RVA: 0x000041B8 File Offset: 0x000023B8
			private void SerializeOther(object val)
			{
				if (val is float)
				{
					this.builder.Append(((float)val).ToString("R"));
				}
				else if (val is int || val is uint || val is long || val is sbyte || val is byte || val is short || val is ushort || val is ulong)
				{
					this.builder.Append(val);
				}
				else if (val is double || val is decimal)
				{
					this.builder.Append(Convert.ToDouble(val).ToString("R"));
				}
				else
				{
					this.SerializeString(val.ToString());
				}
			}

			// Token: 0x04000038 RID: 56
			private StringBuilder builder;
		}
	}
}
