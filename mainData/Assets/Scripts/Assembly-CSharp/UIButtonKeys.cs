using System;
using UnityEngine;

// Token: 0x02000023 RID: 35
[AddComponentMenu("NGUI/Interaction/Button Keys")]
[RequireComponent(typeof(Collider))]
public class UIButtonKeys : MonoBehaviour
{
	// Token: 0x06000160 RID: 352 RVA: 0x000095F4 File Offset: 0x000077F4
	private void Start()
	{
		if (this.startsSelected && (UICamera.selectedObject == null || !NGUITools.GetActive(UICamera.selectedObject)))
		{
			UICamera.selectedObject = base.gameObject;
		}
	}

	// Token: 0x06000161 RID: 353 RVA: 0x0000962C File Offset: 0x0000782C
	private void OnKey(KeyCode key)
	{
		if (base.enabled && NGUITools.GetActive(base.gameObject))
		{
			switch (key)
			{
			case KeyCode.UpArrow:
				if (this.selectOnUp != null)
				{
					UICamera.selectedObject = this.selectOnUp.gameObject;
				}
				break;
			case KeyCode.DownArrow:
				if (this.selectOnDown != null)
				{
					UICamera.selectedObject = this.selectOnDown.gameObject;
				}
				break;
			case KeyCode.RightArrow:
				if (this.selectOnRight != null)
				{
					UICamera.selectedObject = this.selectOnRight.gameObject;
				}
				break;
			case KeyCode.LeftArrow:
				if (this.selectOnLeft != null)
				{
					UICamera.selectedObject = this.selectOnLeft.gameObject;
				}
				break;
			default:
				if (key == KeyCode.Tab)
				{
					if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
					{
						if (this.selectOnLeft != null)
						{
							UICamera.selectedObject = this.selectOnLeft.gameObject;
						}
						else if (this.selectOnUp != null)
						{
							UICamera.selectedObject = this.selectOnUp.gameObject;
						}
						else if (this.selectOnDown != null)
						{
							UICamera.selectedObject = this.selectOnDown.gameObject;
						}
						else if (this.selectOnRight != null)
						{
							UICamera.selectedObject = this.selectOnRight.gameObject;
						}
					}
					else if (this.selectOnRight != null)
					{
						UICamera.selectedObject = this.selectOnRight.gameObject;
					}
					else if (this.selectOnDown != null)
					{
						UICamera.selectedObject = this.selectOnDown.gameObject;
					}
					else if (this.selectOnUp != null)
					{
						UICamera.selectedObject = this.selectOnUp.gameObject;
					}
					else if (this.selectOnLeft != null)
					{
						UICamera.selectedObject = this.selectOnLeft.gameObject;
					}
				}
				break;
			}
		}
	}

	// Token: 0x06000162 RID: 354 RVA: 0x00009868 File Offset: 0x00007A68
	private void OnClick()
	{
		if (base.enabled && this.selectOnClick != null)
		{
			UICamera.selectedObject = this.selectOnClick.gameObject;
		}
	}

	// Token: 0x04000190 RID: 400
	public bool startsSelected;

	// Token: 0x04000191 RID: 401
	public UIButtonKeys selectOnClick;

	// Token: 0x04000192 RID: 402
	public UIButtonKeys selectOnUp;

	// Token: 0x04000193 RID: 403
	public UIButtonKeys selectOnDown;

	// Token: 0x04000194 RID: 404
	public UIButtonKeys selectOnLeft;

	// Token: 0x04000195 RID: 405
	public UIButtonKeys selectOnRight;
}
