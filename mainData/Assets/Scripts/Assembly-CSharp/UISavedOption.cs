using System;
using UnityEngine;

// Token: 0x02000041 RID: 65
[AddComponentMenu("NGUI/Interaction/Saved Option")]
public class UISavedOption : MonoBehaviour
{
	// Token: 0x1700002A RID: 42
	// (get) Token: 0x0600020B RID: 523 RVA: 0x0000E6F8 File Offset: 0x0000C8F8
	private string key
	{
		get
		{
			return (!string.IsNullOrEmpty(this.keyName)) ? this.keyName : ("NGUI State: " + base.name);
		}
	}

	// Token: 0x0600020C RID: 524 RVA: 0x0000E728 File Offset: 0x0000C928
	private void Awake()
	{
		this.mList = base.GetComponent<UIPopupList>();
		this.mCheck = base.GetComponent<UICheckbox>();
		if (this.mList != null)
		{
			UIPopupList uipopupList = this.mList;
			uipopupList.onSelectionChange = (UIPopupList.OnSelectionChange)Delegate.Combine(uipopupList.onSelectionChange, new UIPopupList.OnSelectionChange(this.SaveSelection));
		}
		if (this.mCheck != null)
		{
			UICheckbox uicheckbox = this.mCheck;
			uicheckbox.onStateChange = (UICheckbox.OnStateChange)Delegate.Combine(uicheckbox.onStateChange, new UICheckbox.OnStateChange(this.SaveState));
		}
	}

	// Token: 0x0600020D RID: 525 RVA: 0x0000E7C0 File Offset: 0x0000C9C0
	private void OnDestroy()
	{
		if (this.mCheck != null)
		{
			UICheckbox uicheckbox = this.mCheck;
			uicheckbox.onStateChange = (UICheckbox.OnStateChange)Delegate.Remove(uicheckbox.onStateChange, new UICheckbox.OnStateChange(this.SaveState));
		}
		if (this.mList != null)
		{
			UIPopupList uipopupList = this.mList;
			uipopupList.onSelectionChange = (UIPopupList.OnSelectionChange)Delegate.Remove(uipopupList.onSelectionChange, new UIPopupList.OnSelectionChange(this.SaveSelection));
		}
	}

	// Token: 0x0600020E RID: 526 RVA: 0x0000E840 File Offset: 0x0000CA40
	private void OnEnable()
	{
		if (this.mList != null)
		{
			string @string = PlayerPrefs.GetString(this.key);
			if (!string.IsNullOrEmpty(@string))
			{
				this.mList.selection = @string;
			}
			return;
		}
		if (this.mCheck != null)
		{
			this.mCheck.isChecked = (PlayerPrefs.GetInt(this.key, 1) != 0);
		}
		else
		{
			string string2 = PlayerPrefs.GetString(this.key);
			UICheckbox[] componentsInChildren = base.GetComponentsInChildren<UICheckbox>(true);
			int i = 0;
			int num = componentsInChildren.Length;
			while (i < num)
			{
				UICheckbox uicheckbox = componentsInChildren[i];
				uicheckbox.isChecked = (uicheckbox.name == string2);
				i++;
			}
		}
	}

	// Token: 0x0600020F RID: 527 RVA: 0x0000E8FC File Offset: 0x0000CAFC
	private void OnDisable()
	{
		if (this.mCheck == null && this.mList == null)
		{
			UICheckbox[] componentsInChildren = base.GetComponentsInChildren<UICheckbox>(true);
			int i = 0;
			int num = componentsInChildren.Length;
			while (i < num)
			{
				UICheckbox uicheckbox = componentsInChildren[i];
				if (uicheckbox.isChecked)
				{
					this.SaveSelection(uicheckbox.name);
					break;
				}
				i++;
			}
		}
	}

	// Token: 0x06000210 RID: 528 RVA: 0x0000E96C File Offset: 0x0000CB6C
	private void SaveSelection(string selection)
	{
		PlayerPrefs.SetString(this.key, selection);
	}

	// Token: 0x06000211 RID: 529 RVA: 0x0000E97C File Offset: 0x0000CB7C
	private void SaveState(bool state)
	{
		PlayerPrefs.SetInt(this.key, (!state) ? 0 : 1);
	}

	// Token: 0x0400027D RID: 637
	public string keyName;

	// Token: 0x0400027E RID: 638
	private UIPopupList mList;

	// Token: 0x0400027F RID: 639
	private UICheckbox mCheck;
}
