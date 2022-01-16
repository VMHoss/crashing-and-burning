using System;
using UnityEngine;

// Token: 0x02000034 RID: 52
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/Drag Panel Contents")]
public class UIDragPanelContents : MonoBehaviour
{
	// Token: 0x060001B5 RID: 437 RVA: 0x0000B798 File Offset: 0x00009998
	private void Awake()
	{
		if (this.panel != null)
		{
			if (this.draggablePanel == null)
			{
				this.draggablePanel = this.panel.GetComponent<UIDraggablePanel>();
				if (this.draggablePanel == null)
				{
					this.draggablePanel = this.panel.gameObject.AddComponent<UIDraggablePanel>();
				}
			}
			this.panel = null;
		}
	}

	// Token: 0x060001B6 RID: 438 RVA: 0x0000B808 File Offset: 0x00009A08
	private void Start()
	{
		if (this.draggablePanel == null)
		{
			this.draggablePanel = NGUITools.FindInParents<UIDraggablePanel>(base.gameObject);
		}
	}

	// Token: 0x060001B7 RID: 439 RVA: 0x0000B838 File Offset: 0x00009A38
	private void OnPress(bool pressed)
	{
		if (base.enabled && NGUITools.GetActive(base.gameObject) && this.draggablePanel != null)
		{
			this.draggablePanel.Press(pressed);
		}
	}

	// Token: 0x060001B8 RID: 440 RVA: 0x0000B880 File Offset: 0x00009A80
	private void OnDrag(Vector2 delta)
	{
		if (base.enabled && NGUITools.GetActive(base.gameObject) && this.draggablePanel != null)
		{
			this.draggablePanel.Drag();
		}
	}

	// Token: 0x060001B9 RID: 441 RVA: 0x0000B8BC File Offset: 0x00009ABC
	private void OnScroll(float delta)
	{
		if (base.enabled && NGUITools.GetActive(base.gameObject) && this.draggablePanel != null)
		{
			this.draggablePanel.Scroll(delta);
		}
	}

	// Token: 0x04000207 RID: 519
	public UIDraggablePanel draggablePanel;

	// Token: 0x04000208 RID: 520
	[SerializeField]
	[HideInInspector]
	private UIPanel panel;
}
