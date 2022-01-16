using System;
using UnityEngine;

// Token: 0x0200002F RID: 47
[AddComponentMenu("NGUI/Interaction/Checkbox Controlled Component")]
public class UICheckboxControlledComponent : MonoBehaviour
{
	// Token: 0x060001A3 RID: 419 RVA: 0x0000AEF4 File Offset: 0x000090F4
	private void Start()
	{
		UICheckbox component = base.GetComponent<UICheckbox>();
		if (component != null)
		{
			this.mUsingDelegates = true;
			UICheckbox uicheckbox = component;
			uicheckbox.onStateChange = (UICheckbox.OnStateChange)Delegate.Combine(uicheckbox.onStateChange, new UICheckbox.OnStateChange(this.OnActivateDelegate));
		}
	}

	// Token: 0x060001A4 RID: 420 RVA: 0x0000AF40 File Offset: 0x00009140
	private void OnActivateDelegate(bool isActive)
	{
		if (base.enabled && this.target != null)
		{
			this.target.enabled = ((!this.inverse) ? isActive : (!isActive));
		}
	}

	// Token: 0x060001A5 RID: 421 RVA: 0x0000AF8C File Offset: 0x0000918C
	private void OnActivate(bool isActive)
	{
		if (!this.mUsingDelegates)
		{
			this.OnActivateDelegate(isActive);
		}
	}

	// Token: 0x040001EF RID: 495
	public MonoBehaviour target;

	// Token: 0x040001F0 RID: 496
	public bool inverse;

	// Token: 0x040001F1 RID: 497
	private bool mUsingDelegates;
}
