using System;
using UnityEngine;

// Token: 0x0200003D RID: 61
[RequireComponent(typeof(UIInput))]
[AddComponentMenu("NGUI/Interaction/Input Validator")]
public class UIInputValidator : MonoBehaviour
{
	// Token: 0x060001F5 RID: 501 RVA: 0x0000D6C8 File Offset: 0x0000B8C8
	private void Start()
	{
		base.GetComponent<UIInput>().validator = new UIInput.Validator(this.Validate);
	}

	// Token: 0x060001F6 RID: 502 RVA: 0x0000D6E4 File Offset: 0x0000B8E4
	private char Validate(string text, char ch)
	{
		if (this.logic == UIInputValidator.Validation.None || !base.enabled)
		{
			return ch;
		}
		if (this.logic == UIInputValidator.Validation.Integer)
		{
			if (ch >= '0' && ch <= '9')
			{
				return ch;
			}
			if (ch == '-' && text.Length == 0)
			{
				return ch;
			}
		}
		else if (this.logic == UIInputValidator.Validation.Float)
		{
			if (ch >= '0' && ch <= '9')
			{
				return ch;
			}
			if (ch == '-' && text.Length == 0)
			{
				return ch;
			}
			if (ch == '.' && !text.Contains("."))
			{
				return ch;
			}
		}
		else if (this.logic == UIInputValidator.Validation.Alphanumeric)
		{
			if (ch >= 'A' && ch <= 'Z')
			{
				return ch;
			}
			if (ch >= 'a' && ch <= 'z')
			{
				return ch;
			}
			if (ch >= '0' && ch <= '9')
			{
				return ch;
			}
		}
		else if (this.logic == UIInputValidator.Validation.Username)
		{
			if (ch >= 'A' && ch <= 'Z')
			{
				return ch - 'A' + 'a';
			}
			if (ch >= 'a' && ch <= 'z')
			{
				return ch;
			}
			if (ch >= '0' && ch <= '9')
			{
				return ch;
			}
		}
		else if (this.logic == UIInputValidator.Validation.Name)
		{
			char c = (text.Length <= 0) ? ' ' : text[text.Length - 1];
			if (ch >= 'a' && ch <= 'z')
			{
				if (c == ' ')
				{
					return ch - 'a' + 'A';
				}
				return ch;
			}
			else if (ch >= 'A' && ch <= 'Z')
			{
				if (c != ' ' && c != '\'')
				{
					return ch - 'A' + 'a';
				}
				return ch;
			}
			else if (ch == '\'')
			{
				if (c != ' ' && c != '\'' && !text.Contains("'"))
				{
					return ch;
				}
			}
			else if (ch == ' ' && c != ' ' && c != '\'')
			{
				return ch;
			}
		}
		return '\0';
	}

	// Token: 0x04000256 RID: 598
	public UIInputValidator.Validation logic;

	// Token: 0x0200003E RID: 62
	public enum Validation
	{
		// Token: 0x04000258 RID: 600
		None,
		// Token: 0x04000259 RID: 601
		Integer,
		// Token: 0x0400025A RID: 602
		Float,
		// Token: 0x0400025B RID: 603
		Alphanumeric,
		// Token: 0x0400025C RID: 604
		Username,
		// Token: 0x0400025D RID: 605
		Name
	}
}
