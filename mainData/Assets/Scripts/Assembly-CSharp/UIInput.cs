using System;
using UnityEngine;

// Token: 0x02000080 RID: 128
[AddComponentMenu("NGUI/UI/Input (Basic)")]
public class UIInput : MonoBehaviour
{
	// Token: 0x170000A2 RID: 162
	// (get) Token: 0x06000418 RID: 1048 RVA: 0x0001C174 File Offset: 0x0001A374
	// (set) Token: 0x06000419 RID: 1049 RVA: 0x0001C190 File Offset: 0x0001A390
	public virtual string text
	{
		get
		{
			if (this.mDoInit)
			{
				this.Init();
			}
			return this.mText;
		}
		set
		{
			if (this.mDoInit)
			{
				this.Init();
			}
			this.mText = value;
			if (this.label != null)
			{
				if (string.IsNullOrEmpty(value))
				{
					value = this.mDefaultText;
				}
				this.label.supportEncoding = false;
				this.label.text = ((!this.selected) ? value : (value + this.caratChar));
				this.label.showLastPasswordChar = this.selected;
				this.label.color = ((!this.selected && !(value != this.mDefaultText)) ? this.mDefaultColor : this.activeColor);
			}
		}
	}

	// Token: 0x170000A3 RID: 163
	// (get) Token: 0x0600041A RID: 1050 RVA: 0x0001C258 File Offset: 0x0001A458
	// (set) Token: 0x0600041B RID: 1051 RVA: 0x0001C26C File Offset: 0x0001A46C
	public bool selected
	{
		get
		{
			return UICamera.selectedObject == base.gameObject;
		}
		set
		{
			if (!value && UICamera.selectedObject == base.gameObject)
			{
				UICamera.selectedObject = null;
			}
			else if (value)
			{
				UICamera.selectedObject = base.gameObject;
			}
		}
	}

	// Token: 0x170000A4 RID: 164
	// (get) Token: 0x0600041C RID: 1052 RVA: 0x0001C2B0 File Offset: 0x0001A4B0
	// (set) Token: 0x0600041D RID: 1053 RVA: 0x0001C2B8 File Offset: 0x0001A4B8
	public string defaultText
	{
		get
		{
			return this.mDefaultText;
		}
		set
		{
			if (this.label.text == this.mDefaultText)
			{
				this.label.text = value;
			}
			this.mDefaultText = value;
		}
	}

	// Token: 0x0600041E RID: 1054 RVA: 0x0001C2F4 File Offset: 0x0001A4F4
	protected void Init()
	{
		if (this.mDoInit)
		{
			this.mDoInit = false;
			if (this.label == null)
			{
				this.label = base.GetComponentInChildren<UILabel>();
			}
			if (this.label != null)
			{
				if (this.useLabelTextAtStart)
				{
					this.mText = this.label.text;
				}
				this.mDefaultText = this.label.text;
				this.mDefaultColor = this.label.color;
				this.label.supportEncoding = false;
				this.label.password = this.isPassword;
				this.mPivot = this.label.pivot;
				this.mPosition = this.label.cachedTransform.localPosition.x;
			}
			else
			{
				base.enabled = false;
			}
		}
	}

	// Token: 0x0600041F RID: 1055 RVA: 0x0001C3D8 File Offset: 0x0001A5D8
	private void OnEnable()
	{
		if (UICamera.IsHighlighted(base.gameObject))
		{
			this.OnSelect(true);
		}
	}

	// Token: 0x06000420 RID: 1056 RVA: 0x0001C3F4 File Offset: 0x0001A5F4
	private void OnDisable()
	{
		if (UICamera.IsHighlighted(base.gameObject))
		{
			this.OnSelect(false);
		}
	}

	// Token: 0x06000421 RID: 1057 RVA: 0x0001C410 File Offset: 0x0001A610
	private void OnSelect(bool isSelected)
	{
		if (this.mDoInit)
		{
			this.Init();
		}
		if (this.label != null && base.enabled && NGUITools.GetActive(base.gameObject))
		{
			if (isSelected)
			{
				this.mText = ((this.useLabelTextAtStart || !(this.label.text == this.mDefaultText)) ? this.label.text : string.Empty);
				this.label.color = this.activeColor;
				if (this.isPassword)
				{
					this.label.password = true;
				}
				if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android)
				{
					if (this.isPassword)
					{
						this.mKeyboard = TouchScreenKeyboard.Open(this.mText, TouchScreenKeyboardType.Default, false, false, true);
					}
					else
					{
						this.mKeyboard = TouchScreenKeyboard.Open(this.mText, (TouchScreenKeyboardType)this.type, this.autoCorrect);
					}
				}
				else
				{
					Input.imeCompositionMode = IMECompositionMode.On;
					Transform cachedTransform = this.label.cachedTransform;
					Vector3 position = this.label.pivotOffset;
					position.y += this.label.relativeSize.y;
					position = cachedTransform.TransformPoint(position);
					Input.compositionCursorPos = UICamera.currentCamera.WorldToScreenPoint(position);
				}
				this.UpdateLabel();
			}
			else
			{
				if (this.mKeyboard != null)
				{
					this.mKeyboard.active = false;
				}
				if (string.IsNullOrEmpty(this.mText))
				{
					this.label.text = this.mDefaultText;
					this.label.color = this.mDefaultColor;
					if (this.isPassword)
					{
						this.label.password = false;
					}
				}
				else
				{
					this.label.text = this.mText;
				}
				this.label.showLastPasswordChar = false;
				Input.imeCompositionMode = IMECompositionMode.Off;
				this.RestoreLabel();
			}
		}
	}

	// Token: 0x06000422 RID: 1058 RVA: 0x0001C624 File Offset: 0x0001A824
	private void Update()
	{
		if (this.mKeyboard != null)
		{
			string text = this.mKeyboard.text;
			if (this.mText != text)
			{
				this.mText = string.Empty;
				foreach (char c in text)
				{
					if (this.validator != null)
					{
						c = this.validator(this.mText, c);
					}
					if (c != '\0')
					{
						this.mText += c;
					}
				}
				if (this.maxChars > 0 && this.mText.Length > this.maxChars)
				{
					this.mText = this.mText.Substring(0, this.maxChars);
				}
				if (this.mText != text)
				{
					this.mKeyboard.text = this.mText;
				}
				this.UpdateLabel();
			}
			if (this.mKeyboard.done)
			{
				this.mKeyboard = null;
				UIInput.current = this;
				if (this.onSubmit != null)
				{
					this.onSubmit(this.mText);
				}
				if (this.eventReceiver == null)
				{
					this.eventReceiver = base.gameObject;
				}
				this.eventReceiver.SendMessage(this.functionName, this.mText, SendMessageOptions.DontRequireReceiver);
				UIInput.current = null;
				this.selected = false;
			}
		}
	}

	// Token: 0x06000423 RID: 1059 RVA: 0x0001C79C File Offset: 0x0001A99C
	private void OnInput(string input)
	{
		if (this.mDoInit)
		{
			this.Init();
		}
		if (this.selected && base.enabled && NGUITools.GetActive(base.gameObject))
		{
			if (Application.platform == RuntimePlatform.Android)
			{
				return;
			}
			if (Application.platform == RuntimePlatform.IPhonePlayer)
			{
				return;
			}
			this.Append(input);
		}
	}

	// Token: 0x06000424 RID: 1060 RVA: 0x0001C800 File Offset: 0x0001AA00
	private void Append(string input)
	{
		int i = 0;
		int length = input.Length;
		while (i < length)
		{
			char c = input[i];
			if (c == '\b')
			{
				if (this.mText.Length > 0)
				{
					this.mText = this.mText.Substring(0, this.mText.Length - 1);
					base.SendMessage("OnInputChanged", this, SendMessageOptions.DontRequireReceiver);
				}
			}
			else if (c == '\r' || c == '\n')
			{
				if ((UICamera.current.submitKey0 == KeyCode.Return || UICamera.current.submitKey1 == KeyCode.Return) && (!this.label.multiLine || (!Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.RightControl))))
				{
					UIInput.current = this;
					if (this.onSubmit != null)
					{
						this.onSubmit(this.mText);
					}
					if (this.eventReceiver == null)
					{
						this.eventReceiver = base.gameObject;
					}
					this.eventReceiver.SendMessage(this.functionName, this.mText, SendMessageOptions.DontRequireReceiver);
					UIInput.current = null;
					this.selected = false;
					return;
				}
				if (this.validator != null)
				{
					c = this.validator(this.mText, c);
				}
				if (c != '\0')
				{
					if (c == '\n' || c == '\r')
					{
						if (this.label.multiLine)
						{
							this.mText += "\n";
						}
					}
					else
					{
						this.mText += c;
					}
					base.SendMessage("OnInputChanged", this, SendMessageOptions.DontRequireReceiver);
				}
			}
			else if (c >= ' ')
			{
				if (this.validator != null)
				{
					c = this.validator(this.mText, c);
				}
				if (c != '\0')
				{
					this.mText += c;
					base.SendMessage("OnInputChanged", this, SendMessageOptions.DontRequireReceiver);
				}
			}
			i++;
		}
		this.UpdateLabel();
	}

	// Token: 0x06000425 RID: 1061 RVA: 0x0001CA24 File Offset: 0x0001AC24
	private void UpdateLabel()
	{
		if (this.mDoInit)
		{
			this.Init();
		}
		if (this.maxChars > 0 && this.mText.Length > this.maxChars)
		{
			this.mText = this.mText.Substring(0, this.maxChars);
		}
		if (this.label.font != null)
		{
			string text;
			if (this.isPassword && this.selected)
			{
				text = string.Empty;
				int i = 0;
				int length = this.mText.Length;
				while (i < length)
				{
					text += "*";
					i++;
				}
				text = text + Input.compositionString + this.caratChar;
			}
			else
			{
				text = ((!this.selected) ? this.mText : (this.mText + Input.compositionString + this.caratChar));
			}
			this.label.supportEncoding = false;
			if (!this.label.shrinkToFit)
			{
				if (this.label.multiLine)
				{
					text = this.label.font.WrapText(text, (float)this.label.lineWidth / this.label.cachedTransform.localScale.x, 0, false, UIFont.SymbolStyle.None);
				}
				else
				{
					string endOfLineThatFits = this.label.font.GetEndOfLineThatFits(text, (float)this.label.lineWidth / this.label.cachedTransform.localScale.x, false, UIFont.SymbolStyle.None);
					if (endOfLineThatFits != text)
					{
						text = endOfLineThatFits;
						Vector3 localPosition = this.label.cachedTransform.localPosition;
						localPosition.x = this.mPosition + (float)this.label.lineWidth;
						if (this.mPivot == UIWidget.Pivot.Left)
						{
							this.label.pivot = UIWidget.Pivot.Right;
						}
						else if (this.mPivot == UIWidget.Pivot.TopLeft)
						{
							this.label.pivot = UIWidget.Pivot.TopRight;
						}
						else if (this.mPivot == UIWidget.Pivot.BottomLeft)
						{
							this.label.pivot = UIWidget.Pivot.BottomRight;
						}
						this.label.cachedTransform.localPosition = localPosition;
					}
					else
					{
						this.RestoreLabel();
					}
				}
			}
			this.label.text = text;
			this.label.showLastPasswordChar = this.selected;
		}
	}

	// Token: 0x06000426 RID: 1062 RVA: 0x0001CC88 File Offset: 0x0001AE88
	private void RestoreLabel()
	{
		if (this.label != null)
		{
			this.label.pivot = this.mPivot;
			Vector3 localPosition = this.label.cachedTransform.localPosition;
			localPosition.x = this.mPosition;
			this.label.cachedTransform.localPosition = localPosition;
		}
	}

	// Token: 0x04000437 RID: 1079
	public static UIInput current;

	// Token: 0x04000438 RID: 1080
	public UILabel label;

	// Token: 0x04000439 RID: 1081
	public int maxChars;

	// Token: 0x0400043A RID: 1082
	public string caratChar = "|";

	// Token: 0x0400043B RID: 1083
	public UIInput.Validator validator;

	// Token: 0x0400043C RID: 1084
	public UIInput.KeyboardType type;

	// Token: 0x0400043D RID: 1085
	public bool isPassword;

	// Token: 0x0400043E RID: 1086
	public bool autoCorrect;

	// Token: 0x0400043F RID: 1087
	public bool useLabelTextAtStart;

	// Token: 0x04000440 RID: 1088
	public Color activeColor = Color.white;

	// Token: 0x04000441 RID: 1089
	public GameObject selectOnTab;

	// Token: 0x04000442 RID: 1090
	public GameObject eventReceiver;

	// Token: 0x04000443 RID: 1091
	public string functionName = "OnSubmit";

	// Token: 0x04000444 RID: 1092
	public UIInput.OnSubmit onSubmit;

	// Token: 0x04000445 RID: 1093
	private string mText = string.Empty;

	// Token: 0x04000446 RID: 1094
	private string mDefaultText = string.Empty;

	// Token: 0x04000447 RID: 1095
	private Color mDefaultColor = Color.white;

	// Token: 0x04000448 RID: 1096
	private UIWidget.Pivot mPivot = UIWidget.Pivot.Left;

	// Token: 0x04000449 RID: 1097
	private float mPosition;

	// Token: 0x0400044A RID: 1098
	private TouchScreenKeyboard mKeyboard;

	// Token: 0x0400044B RID: 1099
	private bool mDoInit = true;

	// Token: 0x02000081 RID: 129
	public enum KeyboardType
	{
		// Token: 0x0400044D RID: 1101
		Default,
		// Token: 0x0400044E RID: 1102
		ASCIICapable,
		// Token: 0x0400044F RID: 1103
		NumbersAndPunctuation,
		// Token: 0x04000450 RID: 1104
		URL,
		// Token: 0x04000451 RID: 1105
		NumberPad,
		// Token: 0x04000452 RID: 1106
		PhonePad,
		// Token: 0x04000453 RID: 1107
		NamePhonePad,
		// Token: 0x04000454 RID: 1108
		EmailAddress
	}

	// Token: 0x020001F3 RID: 499
	// (Invoke) Token: 0x06000DE9 RID: 3561
	public delegate char Validator(string currentText, char nextChar);

	// Token: 0x020001F4 RID: 500
	// (Invoke) Token: 0x06000DED RID: 3565
	public delegate void OnSubmit(string inputString);
}
