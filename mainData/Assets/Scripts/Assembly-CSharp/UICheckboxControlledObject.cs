using System;
using UnityEngine;

// Token: 0x02000030 RID: 48
[AddComponentMenu("NGUI/Interaction/Checkbox Controlled Object")]
public class UICheckboxControlledObject : MonoBehaviour
{
	// Token: 0x060001A7 RID: 423 RVA: 0x0000AFA8 File Offset: 0x000091A8
	private void OnEnable()
	{
		UICheckbox component = base.GetComponent<UICheckbox>();
		if (component != null)
		{
			this.OnActivate(component.isChecked);
		}
	}

	// Token: 0x060001A8 RID: 424 RVA: 0x0000AFD4 File Offset: 0x000091D4
	private void OnActivate(bool isActive)
	{
		if (this.target != null)
		{
			NGUITools.SetActive(this.target, (!this.inverse) ? isActive : (!isActive));
			UIPanel uipanel = NGUITools.FindInParents<UIPanel>(this.target);
			if (uipanel != null)
			{
				uipanel.Refresh();
			}
		}
	}

	// Token: 0x040001F2 RID: 498
	public GameObject target;

	// Token: 0x040001F3 RID: 499
	public bool inverse;
}
