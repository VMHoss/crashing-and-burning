using System;
using UnityEngine;

// Token: 0x02000082 RID: 130
[AddComponentMenu("NGUI/UI/Input (Saved)")]
public class UIInputSaved : UIInput
{
	// Token: 0x170000A5 RID: 165
	// (get) Token: 0x06000428 RID: 1064 RVA: 0x0001CCF0 File Offset: 0x0001AEF0
	// (set) Token: 0x06000429 RID: 1065 RVA: 0x0001CCF8 File Offset: 0x0001AEF8
	public override string text
	{
		get
		{
			return base.text;
		}
		set
		{
			base.text = value;
			this.SaveToPlayerPrefs(value);
		}
	}

	// Token: 0x0600042A RID: 1066 RVA: 0x0001CD08 File Offset: 0x0001AF08
	private void Awake()
	{
		this.onSubmit = new UIInput.OnSubmit(this.SaveToPlayerPrefs);
		if (!string.IsNullOrEmpty(this.playerPrefsField) && PlayerPrefs.HasKey(this.playerPrefsField))
		{
			this.text = PlayerPrefs.GetString(this.playerPrefsField);
		}
	}

	// Token: 0x0600042B RID: 1067 RVA: 0x0001CD58 File Offset: 0x0001AF58
	private void SaveToPlayerPrefs(string val)
	{
		if (!string.IsNullOrEmpty(this.playerPrefsField))
		{
			PlayerPrefs.SetString(this.playerPrefsField, val);
		}
	}

	// Token: 0x0600042C RID: 1068 RVA: 0x0001CD78 File Offset: 0x0001AF78
	private void OnApplicationQuit()
	{
		this.SaveToPlayerPrefs(this.text);
	}

	// Token: 0x04000455 RID: 1109
	public string playerPrefsField;
}
