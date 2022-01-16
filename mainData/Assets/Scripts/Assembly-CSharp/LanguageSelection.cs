using System;
using UnityEngine;

// Token: 0x0200001E RID: 30
[AddComponentMenu("NGUI/Interaction/Language Selection")]
[RequireComponent(typeof(UIPopupList))]
public class LanguageSelection : MonoBehaviour
{
	// Token: 0x06000148 RID: 328 RVA: 0x00009060 File Offset: 0x00007260
	private void Start()
	{
		this.mList = base.GetComponent<UIPopupList>();
		this.UpdateList();
		this.mList.eventReceiver = base.gameObject;
		this.mList.functionName = "OnLanguageSelection";
	}

	// Token: 0x06000149 RID: 329 RVA: 0x00009098 File Offset: 0x00007298
	private void UpdateList()
	{
		if (Localization.instance != null && Localization.instance.languages != null)
		{
			this.mList.items.Clear();
			int i = 0;
			int num = Localization.instance.languages.Length;
			while (i < num)
			{
				TextAsset textAsset = Localization.instance.languages[i];
				if (textAsset != null)
				{
					this.mList.items.Add(textAsset.name);
				}
				i++;
			}
			this.mList.selection = Localization.instance.currentLanguage;
		}
	}

	// Token: 0x0600014A RID: 330 RVA: 0x00009138 File Offset: 0x00007338
	private void OnLanguageSelection(string language)
	{
		if (Localization.instance != null)
		{
			Localization.instance.currentLanguage = language;
		}
	}

	// Token: 0x04000184 RID: 388
	private UIPopupList mList;
}
