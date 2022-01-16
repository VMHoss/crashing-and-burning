using System;
using UnityEngine;

// Token: 0x02000085 RID: 133
[AddComponentMenu("NGUI/UI/Localize")]
[RequireComponent(typeof(UIWidget))]
public class UILocalize : MonoBehaviour
{
	// Token: 0x06000455 RID: 1109 RVA: 0x0001DE40 File Offset: 0x0001C040
	private void OnLocalize(Localization loc)
	{
		if (this.mLanguage != loc.currentLanguage)
		{
			this.Localize();
		}
	}

	// Token: 0x06000456 RID: 1110 RVA: 0x0001DE60 File Offset: 0x0001C060
	private void OnEnable()
	{
		if (this.mStarted && Localization.instance != null)
		{
			this.Localize();
		}
	}

	// Token: 0x06000457 RID: 1111 RVA: 0x0001DE84 File Offset: 0x0001C084
	private void Start()
	{
		this.mStarted = true;
		if (Localization.instance != null)
		{
			this.Localize();
		}
	}

	// Token: 0x06000458 RID: 1112 RVA: 0x0001DEA4 File Offset: 0x0001C0A4
	public void Localize()
	{
		Localization instance = Localization.instance;
		UIWidget component = base.GetComponent<UIWidget>();
		UILabel uilabel = component as UILabel;
		UISprite uisprite = component as UISprite;
		if (string.IsNullOrEmpty(this.mLanguage) && string.IsNullOrEmpty(this.key) && uilabel != null)
		{
			this.key = uilabel.text;
		}
		string text = (!string.IsNullOrEmpty(this.key)) ? instance.Get(this.key) : string.Empty;
		if (uilabel != null)
		{
			UIInput uiinput = NGUITools.FindInParents<UIInput>(uilabel.gameObject);
			if (uiinput != null && uiinput.label == uilabel)
			{
				uiinput.defaultText = text;
			}
			else
			{
				uilabel.text = text;
			}
		}
		else if (uisprite != null)
		{
			uisprite.spriteName = text;
			uisprite.MakePixelPerfect();
		}
		this.mLanguage = instance.currentLanguage;
	}

	// Token: 0x04000474 RID: 1140
	public string key;

	// Token: 0x04000475 RID: 1141
	private string mLanguage;

	// Token: 0x04000476 RID: 1142
	private bool mStarted;
}
