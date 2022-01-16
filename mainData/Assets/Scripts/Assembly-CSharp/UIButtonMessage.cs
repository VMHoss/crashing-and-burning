using System;
using UnityEngine;

// Token: 0x02000024 RID: 36
[AddComponentMenu("NGUI/Interaction/Button Message")]
public class UIButtonMessage : MonoBehaviour
{
	// Token: 0x06000164 RID: 356 RVA: 0x000098AC File Offset: 0x00007AAC
	private void Start()
	{
		this.mStarted = true;
	}

	// Token: 0x06000165 RID: 357 RVA: 0x000098B8 File Offset: 0x00007AB8
	private void OnEnable()
	{
		if (this.mStarted && this.mHighlighted)
		{
			this.OnHover(UICamera.IsHighlighted(base.gameObject));
		}
	}

	// Token: 0x06000166 RID: 358 RVA: 0x000098E4 File Offset: 0x00007AE4
	private void OnHover(bool isOver)
	{
		if (base.enabled)
		{
			if ((isOver && this.trigger == UIButtonMessage.Trigger.OnMouseOver) || (!isOver && this.trigger == UIButtonMessage.Trigger.OnMouseOut))
			{
				this.Send();
			}
			this.mHighlighted = isOver;
		}
	}

	// Token: 0x06000167 RID: 359 RVA: 0x00009930 File Offset: 0x00007B30
	private void OnPress(bool isPressed)
	{
		if (base.enabled && ((isPressed && this.trigger == UIButtonMessage.Trigger.OnPress) || (!isPressed && this.trigger == UIButtonMessage.Trigger.OnRelease)))
		{
			this.Send();
		}
	}

	// Token: 0x06000168 RID: 360 RVA: 0x00009968 File Offset: 0x00007B68
	private void OnClick()
	{
		if (base.enabled && this.trigger == UIButtonMessage.Trigger.OnClick)
		{
			this.Send();
		}
	}

	// Token: 0x06000169 RID: 361 RVA: 0x00009988 File Offset: 0x00007B88
	private void OnDoubleClick()
	{
		if (base.enabled && this.trigger == UIButtonMessage.Trigger.OnDoubleClick)
		{
			this.Send();
		}
	}

	// Token: 0x0600016A RID: 362 RVA: 0x000099A8 File Offset: 0x00007BA8
	private void Send()
	{
		if (string.IsNullOrEmpty(this.functionName))
		{
			return;
		}
		if (this.target == null)
		{
			this.target = base.gameObject;
		}
		if (this.includeChildren)
		{
			Transform[] componentsInChildren = this.target.GetComponentsInChildren<Transform>();
			int i = 0;
			int num = componentsInChildren.Length;
			while (i < num)
			{
				Transform transform = componentsInChildren[i];
				transform.gameObject.SendMessage(this.functionName, base.gameObject, SendMessageOptions.DontRequireReceiver);
				i++;
			}
		}
		else
		{
			this.target.SendMessage(this.functionName, base.gameObject, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x04000196 RID: 406
	public GameObject target;

	// Token: 0x04000197 RID: 407
	public string functionName;

	// Token: 0x04000198 RID: 408
	public UIButtonMessage.Trigger trigger;

	// Token: 0x04000199 RID: 409
	public bool includeChildren;

	// Token: 0x0400019A RID: 410
	private bool mStarted;

	// Token: 0x0400019B RID: 411
	private bool mHighlighted;

	// Token: 0x02000025 RID: 37
	public enum Trigger
	{
		// Token: 0x0400019D RID: 413
		OnClick,
		// Token: 0x0400019E RID: 414
		OnMouseOver,
		// Token: 0x0400019F RID: 415
		OnMouseOut,
		// Token: 0x040001A0 RID: 416
		OnPress,
		// Token: 0x040001A1 RID: 417
		OnRelease,
		// Token: 0x040001A2 RID: 418
		OnDoubleClick
	}
}
