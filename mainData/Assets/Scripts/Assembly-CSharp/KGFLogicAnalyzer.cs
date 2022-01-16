using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000176 RID: 374
public class KGFLogicAnalyzer
{
	// Token: 0x06000B1D RID: 2845 RVA: 0x00053B98 File Offset: 0x00051D98
	public static bool? Analyze(string theLogicString)
	{
		string empty = string.Empty;
		if (!KGFLogicAnalyzer.CheckSyntax(theLogicString, out empty))
		{
			Debug.LogError("KGFLogicAnalyzer: syntax error: " + empty);
			return null;
		}
		if (!KGFLogicAnalyzer.CheckOperands(theLogicString, out empty))
		{
			Debug.LogError("KGFLogicAnalyzer: syntax error: " + empty);
			return null;
		}
		int num = 0;
		if (!theLogicString.Contains(")"))
		{
			theLogicString = "(" + theLogicString + ")";
		}
		while (theLogicString.Contains(")"))
		{
			KGFLogicAnalyzer.EvaluateBraces(ref theLogicString);
			num++;
			if (num == 30)
			{
				break;
			}
		}
		if (theLogicString.ToLower() == "true")
		{
			return new bool?(true);
		}
		if (theLogicString.ToLower() == "false")
		{
			return new bool?(false);
		}
		Debug.LogError("KGFLogicAnalyzer: unexpected result: " + theLogicString);
		return null;
	}

	// Token: 0x06000B1E RID: 2846 RVA: 0x00053C9C File Offset: 0x00051E9C
	private static void EvaluateBraces(ref string theLogicString)
	{
		string text = theLogicString.Replace(" ", string.Empty);
		int num = text.IndexOf(')');
		string text2 = text.Substring(0, num + 1);
		int num2 = text2.LastIndexOf('(');
		int length = num - num2 - 1;
		string theLogicString2 = text.Substring(num2 + 1, length);
		bool? flag = KGFLogicAnalyzer.AnalyseLogicBlock(theLogicString2);
		if (flag == null)
		{
			Debug.LogError("Logic block result is null. Something went wrong!");
			return;
		}
		string str = theLogicString.Substring(0, num2);
		string str2 = theLogicString.Substring(num + 1);
		theLogicString = str + flag.Value.ToString() + str2;
	}

	// Token: 0x06000B1F RID: 2847 RVA: 0x00053D44 File Offset: 0x00051F44
	public static void ClearOperandValues()
	{
		KGFLogicAnalyzer.itsOperandValues.Clear();
	}

	// Token: 0x06000B20 RID: 2848 RVA: 0x00053D50 File Offset: 0x00051F50
	public static void SetOperandValue(string theOperandName, bool theValue)
	{
		if (KGFLogicAnalyzer.itsOperandValues.ContainsKey(theOperandName))
		{
			KGFLogicAnalyzer.itsOperandValues[theOperandName] = theValue;
		}
		else
		{
			KGFLogicAnalyzer.itsOperandValues.Add(theOperandName, theValue);
		}
	}

	// Token: 0x06000B21 RID: 2849 RVA: 0x00053D80 File Offset: 0x00051F80
	public static bool? GetOperandValue(string theOperandName)
	{
		if (KGFLogicAnalyzer.itsOperandValues.ContainsKey(theOperandName))
		{
			return new bool?(KGFLogicAnalyzer.itsOperandValues[theOperandName]);
		}
		Debug.LogError("KGFLogicAnalyzer: no operand value for operand: " + theOperandName);
		return null;
	}

	// Token: 0x06000B22 RID: 2850 RVA: 0x00053DC8 File Offset: 0x00051FC8
	private static bool? AnalyseLogicBlock(string theLogicString)
	{
		KGFLogicAnalyzer.KGFLogicOperand kgflogicOperand = new KGFLogicAnalyzer.KGFLogicOperand();
		string text = theLogicString.Replace(" ", string.Empty);
		string[] separator = new string[]
		{
			KGFLogicAnalyzer.itsStringAnd,
			KGFLogicAnalyzer.itsStringOr
		};
		string[] array = text.Split(separator, StringSplitOptions.None);
		foreach (string name in array)
		{
			KGFLogicAnalyzer.KGFLogicOperand kgflogicOperand2 = new KGFLogicAnalyzer.KGFLogicOperand();
			kgflogicOperand2.SetName(name);
			kgflogicOperand.AddOperand(kgflogicOperand2);
		}
		for (int j = 0; j < array.Length - 1; j++)
		{
			text = text.Remove(0, array[j].Length);
			string theOperator = text.Substring(0, 2);
			kgflogicOperand.AddOperator(theOperator);
			text = text.Remove(0, 2);
		}
		return kgflogicOperand.Evaluate();
	}

	// Token: 0x06000B23 RID: 2851 RVA: 0x00053E94 File Offset: 0x00052094
	public static bool CheckSyntax(string theLogicString, out string theErrorString)
	{
		theErrorString = string.Empty;
		if (theLogicString.IndexOf(KGFLogicAnalyzer.itsStringAnd) == 0)
		{
			theErrorString = "condition cannot start with &&";
			return false;
		}
		if (theLogicString.IndexOf(KGFLogicAnalyzer.itsStringOr) == 0)
		{
			theErrorString = "condition cannot start with ||";
			return false;
		}
		if (theLogicString.LastIndexOf(KGFLogicAnalyzer.itsStringAnd) == theLogicString.Length - 2 && theLogicString.Length != 1)
		{
			theErrorString = "condition cannot end with &&";
			return false;
		}
		if (theLogicString.LastIndexOf(KGFLogicAnalyzer.itsStringOr) == theLogicString.Length - 2 && theLogicString.Length != 1)
		{
			theErrorString = "condition cannot end with ||";
			return false;
		}
		string text = theLogicString.Replace(" ", string.Empty);
		int num = text.Split(new char[]
		{
			'('
		}).Length - 1;
		int num2 = text.Split(new char[]
		{
			')'
		}).Length - 1;
		if (num > num2)
		{
			theErrorString = "missing closing brace";
			return false;
		}
		if (num2 > num)
		{
			theErrorString = "missing opening brace";
			return false;
		}
		string[] separator = new string[]
		{
			KGFLogicAnalyzer.itsStringAnd,
			KGFLogicAnalyzer.itsStringOr
		};
		string text2 = text.Replace("(", string.Empty);
		text2 = text2.Replace(")", string.Empty);
		string[] array = text2.Split(separator, StringSplitOptions.None);
		foreach (string text3 in array)
		{
			if (text3.Contains("&"))
			{
				theErrorString = "condition cannot contain the character &. Use && for logical and.";
				return false;
			}
			if (text3.Contains("|"))
			{
				theErrorString = "condition cannot contain the character |. Use || for logical or.";
				return false;
			}
		}
		return true;
	}

	// Token: 0x06000B24 RID: 2852 RVA: 0x00054038 File Offset: 0x00052238
	public static bool CheckOperands(string theLogicString, out string theErrorString)
	{
		theErrorString = string.Empty;
		string[] separator = new string[]
		{
			KGFLogicAnalyzer.itsStringAnd,
			KGFLogicAnalyzer.itsStringOr
		};
		string text = theLogicString.Replace(" ", string.Empty);
		string text2 = text.Replace("(", string.Empty);
		text2 = text2.Replace(")", string.Empty);
		string[] array = text2.Split(separator, StringSplitOptions.None);
		foreach (string text3 in array)
		{
			if (KGFLogicAnalyzer.GetOperandValue(text3) == null)
			{
				theErrorString = "no operand value for operand: " + text3;
				return false;
			}
		}
		return true;
	}

	// Token: 0x04000B5E RID: 2910
	private static string itsStringAnd = "&&";

	// Token: 0x04000B5F RID: 2911
	private static string itsStringOr = "||";

	// Token: 0x04000B60 RID: 2912
	private static Dictionary<string, bool> itsOperandValues = new Dictionary<string, bool>();

	// Token: 0x02000177 RID: 375
	public class KGFLogicOperand
	{
		// Token: 0x06000B26 RID: 2854 RVA: 0x0005411C File Offset: 0x0005231C
		public void AddOperand(KGFLogicAnalyzer.KGFLogicOperand theOperand)
		{
			this.itsListOfOperands.Add(theOperand);
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x0005412C File Offset: 0x0005232C
		public void AddOperator(string theOperator)
		{
			this.itsListOfOperators.Add(theOperator);
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x0005413C File Offset: 0x0005233C
		public void SetName(string theName)
		{
			this.itsOperandName = theName;
			if (theName.ToLower() == "true")
			{
				this.itsValue = new bool?(true);
			}
			else if (theName.ToLower() == "false")
			{
				this.itsValue = new bool?(false);
			}
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x00054198 File Offset: 0x00052398
		public string GetName()
		{
			return this.itsOperandName;
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x000541A0 File Offset: 0x000523A0
		public void SetValue(bool theValue)
		{
			this.itsValue = new bool?(theValue);
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x000541B0 File Offset: 0x000523B0
		public bool? GetValue()
		{
			bool? flag = this.itsValue;
			if (flag != null)
			{
				return new bool?(this.itsValue.Value);
			}
			if (!(this.itsOperandName != string.Empty))
			{
				return this.Evaluate();
			}
			this.itsValue = KGFLogicAnalyzer.GetOperandValue(this.itsOperandName);
			bool? flag2 = this.itsValue;
			if (flag2 == null)
			{
				return null;
			}
			return this.itsValue;
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x00054238 File Offset: 0x00052438
		public bool? Evaluate()
		{
			if (this.itsListOfOperands.Count == 1)
			{
				return this.itsListOfOperands[0].GetValue();
			}
			bool? flag = new bool?(false);
			for (int i = 0; i < this.itsListOfOperands.Count - 1; i++)
			{
				if (i == 0)
				{
					flag = this.EveluateTwoOperands(this.itsListOfOperands[i].GetValue(), this.itsListOfOperands[i + 1].GetValue(), this.itsListOfOperators[i]);
				}
				else
				{
					flag = this.EveluateTwoOperands(flag, this.itsListOfOperands[i + 1].GetValue(), this.itsListOfOperators[i]);
				}
			}
			return flag;
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x000542F8 File Offset: 0x000524F8
		private bool? EveluateTwoOperands(bool? theValue1, bool? theValue2, string theOperator)
		{
			if (theValue1 == null)
			{
				Debug.LogError("KGFLogicAnalyzer: cannot evaluate because theValue1 is null");
				return null;
			}
			if (theValue2 == null)
			{
				Debug.LogError("KGFLogicAnalyzer: cannot evaluate because theValue2 is null");
				return null;
			}
			if (theOperator == "&&")
			{
				return new bool?(theValue1.Value && theValue2.Value);
			}
			if (theOperator == "||")
			{
				return new bool?(theValue1.Value || theValue2.Value);
			}
			Debug.LogError("KGFLogicAnalyzer: wrong operator: " + theOperator);
			return null;
		}

		// Token: 0x04000B61 RID: 2913
		public string itsOperandName = string.Empty;

		// Token: 0x04000B62 RID: 2914
		private bool? itsValue;

		// Token: 0x04000B63 RID: 2915
		public List<KGFLogicAnalyzer.KGFLogicOperand> itsListOfOperands = new List<KGFLogicAnalyzer.KGFLogicOperand>();

		// Token: 0x04000B64 RID: 2916
		public List<string> itsListOfOperators = new List<string>();
	}
}
