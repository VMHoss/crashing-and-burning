using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000D6 RID: 214
public class TextLoader
{
	// Token: 0x06000679 RID: 1657 RVA: 0x0002E258 File Offset: 0x0002C458
	private TextLoader()
	{
		throw new UnityException("There's no need to create an instance of this class. Use TextLoader.loadText instead.");
	}

	// Token: 0x0600067B RID: 1659 RVA: 0x0002E28C File Offset: 0x0002C48C
	public static void LoadText(string aTextFileData, Dictionary<string, DicEntry> aVarDic)
	{
		TextLoader.pDictionaryVar = aVarDic;
		TextLoader.pCurrentParseSetName = string.Empty;
		TextLoader.pText = aTextFileData;
		TextLoader.pProgress = 0;
		TextLoader.pLine = 0;
		TextLoader.pTextLength = TextLoader.pText.Length;
		while (TextLoader.pProgress < TextLoader.pTextLength)
		{
			TextLoader.pLine++;
			TextLoader.pParseLine();
		}
	}

	// Token: 0x0600067C RID: 1660 RVA: 0x0002E2F0 File Offset: 0x0002C4F0
	public static string SaveText(Dictionary<string, DicEntry> aVarDic)
	{
		return TextLoader.SaveText(aVarDic, null);
	}

	// Token: 0x0600067D RID: 1661 RVA: 0x0002E2FC File Offset: 0x0002C4FC
	public static string SaveText(Dictionary<string, DicEntry> aVarDic, List<string> aSaveList)
	{
		if (aSaveList == null)
		{
			if (!aVarDic.ContainsKey("SaveList"))
			{
				throw new UnityException("TextLoader::saveText, no save list was given or found");
			}
			aSaveList = new List<string>();
			foreach (DicEntry dicEntry in aVarDic["SaveList"].l)
			{
				aSaveList.Add(dicEntry.s);
			}
		}
		TextLoader.pSaveString = string.Empty;
		foreach (string text in aSaveList)
		{
			if (!aVarDic.ContainsKey(text))
			{
				Debug.LogError("TextLoader::saveText, property " + text + " was not found in given dictionary! Saving of this property is skipped.");
			}
			else
			{
				TextLoader.pSaveMainProp(aVarDic, text);
				TextLoader.pSaveString += "\r\n";
			}
		}
		return TextLoader.pSaveString;
	}

	// Token: 0x0600067E RID: 1662 RVA: 0x0002E434 File Offset: 0x0002C634
	private static void pSkipWhiteSpace()
	{
		string a = string.Empty;
		do
		{
			a = TextLoader.pText[TextLoader.pProgress].ToString();
			TextLoader.pProgress++;
		}
		while ((TextLoader.pProgress < TextLoader.pTextLength && a == " ") || a == "\t");
		TextLoader.pProgress--;
	}

	// Token: 0x0600067F RID: 1663 RVA: 0x0002E4A8 File Offset: 0x0002C6A8
	private static string pRemoveTrailingWhiteSpace(string aString)
	{
		int num = aString.Length - 1;
		string a;
		do
		{
			a = aString[num].ToString();
		}
		while (num-- >= 1 && (a == " " || a == "\t"));
		return aString.Substring(0, num + 2);
	}

	// Token: 0x06000680 RID: 1664 RVA: 0x0002E504 File Offset: 0x0002C704
	private static bool pTestForNewLine()
	{
		char c = TextLoader.pText[TextLoader.pProgress];
		if (c != '\r' && c != '\n')
		{
			return false;
		}
		if (c == '\r' && TextLoader.pText[TextLoader.pProgress + 1].ToString() != "\n")
		{
			throw new UnityException("No Line Feed (/n) after a Carriage Return (/r)");
		}
		return true;
	}

	// Token: 0x06000681 RID: 1665 RVA: 0x0002E570 File Offset: 0x0002C770
	private static bool pTestForString(string aString)
	{
		int num = 0;
		int length = aString.Length;
		while (num != length && TextLoader.pText[TextLoader.pProgress + num] == aString[num])
		{
			num++;
		}
		return num == length;
	}

	// Token: 0x06000682 RID: 1666 RVA: 0x0002E5B8 File Offset: 0x0002C7B8
	private static bool pTestForStringWithinLine(string aString)
	{
		string text = string.Empty;
		char c = '0';
		int num = 0;
		while (TextLoader.pProgress + num != TextLoader.pText.Length)
		{
			char c2 = c;
			c = TextLoader.pText[TextLoader.pProgress + num];
			if (c == '\r' || c == '\n' || (c == '/' && c2 == '/'))
			{
				IL_78:
				return text.IndexOf(aString) != -1;
			}
			text += c;
			num++;
		}
		goto IL_78;
	}

	// Token: 0x06000683 RID: 1667 RVA: 0x0002E64C File Offset: 0x0002C84C
	private static void pReadString(string aString)
	{
		int num = 0;
		int length = aString.Length;
		while (num != length && TextLoader.pText[TextLoader.pProgress] == aString[num])
		{
			num++;
			TextLoader.pProgress++;
		}
		if (num != length)
		{
			throw new UnityException("Did not read " + aString + " in the following " + TextLoader.pText.Substring(TextLoader.pProgress - 10, TextLoader.pProgress + 10));
		}
	}

	// Token: 0x06000684 RID: 1668 RVA: 0x0002E6D0 File Offset: 0x0002C8D0
	private static void pSkipUntilNewLine()
	{
		while (TextLoader.pProgress < TextLoader.pTextLength && TextLoader.pText[TextLoader.pProgress] != '\r' && TextLoader.pText[TextLoader.pProgress] != '\n')
		{
			TextLoader.pProgress++;
		}
		if (TextLoader.pText[TextLoader.pProgress] == '\r')
		{
			TextLoader.pProgress++;
		}
		TextLoader.pProgress++;
	}

	// Token: 0x06000685 RID: 1669 RVA: 0x0002E758 File Offset: 0x0002C958
	private static string pGetIdentifier()
	{
		return TextLoader.pGetIdentifier(string.Empty);
	}

	// Token: 0x06000686 RID: 1670 RVA: 0x0002E764 File Offset: 0x0002C964
	private static string pGetIdentifier(string aStopChar)
	{
		string text = string.Empty;
		string text2 = TextLoader.pText[TextLoader.pProgress].ToString();
		while (text2 != " " && text2 != "\t" && text2 != "," && text2 != aStopChar && TextLoader.pProgress < TextLoader.pTextLength)
		{
			text += text2;
			TextLoader.pProgress++;
			text2 = TextLoader.pText[TextLoader.pProgress].ToString();
		}
		if (text.Length == 0)
		{
			throw new UnityException("Failed to read identifier at character " + text);
		}
		return text;
	}

	// Token: 0x06000687 RID: 1671 RVA: 0x0002E828 File Offset: 0x0002CA28
	private static string pGetBaseValue()
	{
		string text = string.Empty;
		char c = TextLoader.pText[TextLoader.pProgress];
		char c2 = '0';
		while (c != '\r' && c != '\n' && c != ',' && c != ']' && (c2 != '/' || c != '/') && TextLoader.pProgress < TextLoader.pTextLength)
		{
			text += c;
			TextLoader.pProgress++;
			c2 = c;
			c = TextLoader.pText[TextLoader.pProgress];
		}
		if (c2 == '/' && c == '/')
		{
			text = text.Substring(0, text.Length - 1);
		}
		return TextLoader.pRemoveTrailingWhiteSpace(text);
	}

	// Token: 0x06000688 RID: 1672 RVA: 0x0002E8E4 File Offset: 0x0002CAE4
	private static void pParseValue(Dictionary<string, DicEntry> aDictionary)
	{
		TextLoader.pParseValue(aDictionary, string.Empty, null);
	}

	// Token: 0x06000689 RID: 1673 RVA: 0x0002E8F4 File Offset: 0x0002CAF4
	private static void pParseValue(Dictionary<string, DicEntry> aDictionary, string aProperty)
	{
		TextLoader.pParseValue(aDictionary, aProperty, null);
	}

	// Token: 0x0600068A RID: 1674 RVA: 0x0002E900 File Offset: 0x0002CB00
	private static void pParseValue(Dictionary<string, DicEntry> aDictionary, string aProperty, List<DicEntry> aList)
	{
		if (TextLoader.pText[TextLoader.pProgress].ToString() == "[")
		{
			if (TextLoader.pText[TextLoader.pProgress + 1].ToString() == "#")
			{
				TextLoader.pProgress++;
				aDictionary[aProperty] = new DicEntry(new Dictionary<string, DicEntry>());
				do
				{
					TextLoader.pSkipWhiteSpace();
					TextLoader.pReadString("#");
					string text = TextLoader.pGetIdentifier(":");
					TextLoader.pSkipWhiteSpace();
					TextLoader.pReadString(":");
					TextLoader.pSkipWhiteSpace();
					TextLoader.pParseValue(aDictionary[aProperty].d, text);
					TextLoader.pSkipWhiteSpace();
				}
				while (TextLoader.pText[TextLoader.pProgress++].ToString() == ",");
				TextLoader.pProgress--;
			}
			else
			{
				TextLoader.pProgress++;
				aDictionary[aProperty] = new DicEntry(new List<DicEntry>());
				TextLoader.pSkipWhiteSpace();
				if (TextLoader.pText[TextLoader.pProgress].ToString() != "]")
				{
					do
					{
						TextLoader.pSkipWhiteSpace();
						TextLoader.pParseValue(null, string.Empty, aDictionary[aProperty].l);
						TextLoader.pSkipWhiteSpace();
					}
					while (TextLoader.pText[TextLoader.pProgress++].ToString() == ",");
					TextLoader.pProgress--;
				}
			}
			TextLoader.pSkipWhiteSpace();
			TextLoader.pReadString("]");
		}
		else
		{
			string text = TextLoader.pGetBaseValue();
			int aI;
			float aF;
			if (int.TryParse(text, out aI))
			{
				if (aList == null)
				{
					aDictionary[aProperty] = new DicEntry(aI);
				}
				else
				{
					aList.Add(new DicEntry(aI));
				}
			}
			else if (float.TryParse(text, out aF))
			{
				if (aList == null)
				{
					aDictionary[aProperty] = new DicEntry(aF);
				}
				else
				{
					aList.Add(new DicEntry(aF));
				}
			}
			else if (text == "true" || text == "True" || text == "false" || text == "False")
			{
				if (text == "true" || text == "True")
				{
					if (aList == null)
					{
						aDictionary[aProperty] = new DicEntry(true);
					}
					else
					{
						aList.Add(new DicEntry(true));
					}
				}
				else if (aList == null)
				{
					aDictionary[aProperty] = new DicEntry(false);
				}
				else
				{
					aList.Add(new DicEntry(false));
				}
			}
			else if (aList == null)
			{
				aDictionary[aProperty] = new DicEntry(text);
			}
			else
			{
				aList.Add(new DicEntry(text));
			}
		}
	}

	// Token: 0x0600068B RID: 1675 RVA: 0x0002EC00 File Offset: 0x0002CE00
	private static void pParseLine()
	{
		TextLoader.pSkipWhiteSpace();
		if (TextLoader.pTestForNewLine())
		{
			TextLoader.pSkipUntilNewLine();
			return;
		}
		if (TextLoader.pTestForString("//"))
		{
			TextLoader.pSkipUntilNewLine();
			return;
		}
		if (TextLoader.pCurrentParseSetName == string.Empty)
		{
			if (TextLoader.pTestForStringWithinLine("_Begin"))
			{
				string aProperty = TextLoader.pGetIdentifier("_");
				TextLoader.pReadString("_Begin");
				TextLoader.pCurrentParseSetName = aProperty;
				TextLoader.pDictionaryVar[TextLoader.pCurrentParseSetName] = new DicEntry(new Dictionary<string, DicEntry>());
			}
			else
			{
				TextLoader.pSkipWhiteSpace();
				if (TextLoader.pProgress == TextLoader.pText.Length - 1)
				{
					TextLoader.pProgress = TextLoader.pText.Length;
					return;
				}
				string aProperty = TextLoader.pGetIdentifier();
				TextLoader.pSkipWhiteSpace();
				TextLoader.pParseValue(TextLoader.pDictionaryVar, aProperty);
			}
		}
		else if (TextLoader.pTestForString(TextLoader.pCurrentParseSetName + "_End"))
		{
			TextLoader.pReadString(TextLoader.pCurrentParseSetName + "_End");
			TextLoader.pCurrentParseSetName = string.Empty;
		}
		else
		{
			string aProperty = TextLoader.pGetIdentifier();
			TextLoader.pSkipWhiteSpace();
			TextLoader.pParseValue(TextLoader.pDictionaryVar[TextLoader.pCurrentParseSetName].d, aProperty);
		}
		TextLoader.pSkipUntilNewLine();
	}

	// Token: 0x0600068C RID: 1676 RVA: 0x0002ED40 File Offset: 0x0002CF40
	private static void pSaveMainProp(Dictionary<string, DicEntry> aVarDic, string aProp)
	{
		TextLoader.pSaveString += aProp;
		TextLoader.pSaveString += " ";
		TextLoader.pSaveProp(aVarDic[aProp]);
	}

	// Token: 0x0600068D RID: 1677 RVA: 0x0002ED80 File Offset: 0x0002CF80
	private static void pSaveProp(DicEntry aDicEntry)
	{
		if (aDicEntry.IsComplexProp())
		{
			if (aDicEntry.type == DicEntry.EntryType.DICT)
			{
				TextLoader.pSaveDict(aDicEntry);
			}
			else
			{
				TextLoader.pSaveList(aDicEntry);
			}
		}
		else
		{
			TextLoader.pSaveBaseProp(aDicEntry);
		}
	}

	// Token: 0x0600068E RID: 1678 RVA: 0x0002EDB8 File Offset: 0x0002CFB8
	private static void pSaveBaseProp(DicEntry aDicEntry)
	{
		switch (aDicEntry.type)
		{
		case DicEntry.EntryType.BOOL:
			if (aDicEntry.b)
			{
				TextLoader.pSaveString += "true";
			}
			else
			{
				TextLoader.pSaveString += "false";
			}
			break;
		case DicEntry.EntryType.INT:
			TextLoader.pSaveString += aDicEntry.i.ToString();
			break;
		case DicEntry.EntryType.FLOAT:
			TextLoader.pSaveString += aDicEntry.f.ToString();
			break;
		case DicEntry.EntryType.STRING:
			TextLoader.pSaveString += aDicEntry.s;
			break;
		}
	}

	// Token: 0x0600068F RID: 1679 RVA: 0x0002EE84 File Offset: 0x0002D084
	private static void pSaveDict(DicEntry aDicEntry)
	{
		TextLoader.pSaveString += "[";
		int num = aDicEntry.d.Count;
		if (num == 0)
		{
			throw new UnityException("Attempted to write an empty dictionary, this is currently not possible.");
		}
		foreach (KeyValuePair<string, DicEntry> keyValuePair in aDicEntry.d)
		{
			TextLoader.pSaveString += "#";
			TextLoader.pSaveString += keyValuePair.Key;
			TextLoader.pSaveString += ":";
			TextLoader.pSaveProp(keyValuePair.Value);
			num--;
			if (num > 0)
			{
				TextLoader.pSaveString += ",";
			}
		}
		TextLoader.pSaveString += "]";
	}

	// Token: 0x06000690 RID: 1680 RVA: 0x0002EF94 File Offset: 0x0002D194
	private static void pSaveList(DicEntry aDicEntry)
	{
		TextLoader.pSaveString += "[";
		int num = aDicEntry.l.Count;
		foreach (DicEntry aDicEntry2 in aDicEntry.l)
		{
			TextLoader.pSaveProp(aDicEntry2);
			num--;
			if (num > 0)
			{
				TextLoader.pSaveString += ",";
			}
		}
		TextLoader.pSaveString += "]";
	}

	// Token: 0x040006C8 RID: 1736
	private static Dictionary<string, DicEntry> pDictionaryVar;

	// Token: 0x040006C9 RID: 1737
	private static string pCurrentParseSetName = string.Empty;

	// Token: 0x040006CA RID: 1738
	private static string pText = string.Empty;

	// Token: 0x040006CB RID: 1739
	private static int pProgress;

	// Token: 0x040006CC RID: 1740
	private static int pLine;

	// Token: 0x040006CD RID: 1741
	private static int pTextLength;

	// Token: 0x040006CE RID: 1742
	private static string pSaveString = string.Empty;
}
