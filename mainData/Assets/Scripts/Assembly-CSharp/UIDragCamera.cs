using System;
using UnityEngine;

// Token: 0x02000031 RID: 49
[AddComponentMenu("NGUI/Interaction/Drag Camera")]
[ExecuteInEditMode]
public class UIDragCamera : IgnoreTimeScale
{
	// Token: 0x060001AA RID: 426 RVA: 0x0000B038 File Offset: 0x00009238
	private void Awake()
	{
		if (this.target != null)
		{
			if (this.draggableCamera == null)
			{
				this.draggableCamera = this.target.GetComponent<UIDraggableCamera>();
				if (this.draggableCamera == null)
				{
					this.draggableCamera = this.target.gameObject.AddComponent<UIDraggableCamera>();
				}
			}
			this.target = null;
		}
		else if (this.draggableCamera == null)
		{
			this.draggableCamera = NGUITools.FindInParents<UIDraggableCamera>(base.gameObject);
		}
	}

	// Token: 0x060001AB RID: 427 RVA: 0x0000B0D0 File Offset: 0x000092D0
	private void OnPress(bool isPressed)
	{
		if (base.enabled && NGUITools.GetActive(base.gameObject) && this.draggableCamera != null)
		{
			this.draggableCamera.Press(isPressed);
		}
	}

	// Token: 0x060001AC RID: 428 RVA: 0x0000B118 File Offset: 0x00009318
	private void OnDrag(Vector2 delta)
	{
		if (base.enabled && NGUITools.GetActive(base.gameObject) && this.draggableCamera != null)
		{
			this.draggableCamera.Drag(delta);
		}
	}

	// Token: 0x060001AD RID: 429 RVA: 0x0000B160 File Offset: 0x00009360
	private void OnScroll(float delta)
	{
		if (base.enabled && NGUITools.GetActive(base.gameObject) && this.draggableCamera != null)
		{
			this.draggableCamera.Scroll(delta);
		}
	}

	// Token: 0x040001F4 RID: 500
	public UIDraggableCamera draggableCamera;

	// Token: 0x040001F5 RID: 501
	[SerializeField]
	[HideInInspector]
	private Component target;
}
