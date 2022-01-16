using System;
using UnityEngine;

// Token: 0x02000039 RID: 57
[AddComponentMenu("NGUI/Interaction/Forward Events")]
public class UIForwardEvents : MonoBehaviour
{
	// Token: 0x060001DD RID: 477 RVA: 0x0000D04C File Offset: 0x0000B24C
	private void OnHover(bool isOver)
	{
		if (this.onHover && this.target != null)
		{
			this.target.SendMessage("OnHover", isOver, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x060001DE RID: 478 RVA: 0x0000D084 File Offset: 0x0000B284
	private void OnPress(bool pressed)
	{
		if (this.onPress && this.target != null)
		{
			this.target.SendMessage("OnPress", pressed, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x060001DF RID: 479 RVA: 0x0000D0BC File Offset: 0x0000B2BC
	private void OnClick()
	{
		if (this.onClick && this.target != null)
		{
			this.target.SendMessage("OnClick", SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x060001E0 RID: 480 RVA: 0x0000D0EC File Offset: 0x0000B2EC
	private void OnDoubleClick()
	{
		if (this.onDoubleClick && this.target != null)
		{
			this.target.SendMessage("OnDoubleClick", SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x060001E1 RID: 481 RVA: 0x0000D11C File Offset: 0x0000B31C
	private void OnSelect(bool selected)
	{
		if (this.onSelect && this.target != null)
		{
			this.target.SendMessage("OnSelect", selected, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x060001E2 RID: 482 RVA: 0x0000D154 File Offset: 0x0000B354
	private void OnDrag(Vector2 delta)
	{
		if (this.onDrag && this.target != null)
		{
			this.target.SendMessage("OnDrag", delta, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x060001E3 RID: 483 RVA: 0x0000D18C File Offset: 0x0000B38C
	private void OnDrop(GameObject go)
	{
		if (this.onDrop && this.target != null)
		{
			this.target.SendMessage("OnDrop", go, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x060001E4 RID: 484 RVA: 0x0000D1C8 File Offset: 0x0000B3C8
	private void OnInput(string text)
	{
		if (this.onInput && this.target != null)
		{
			this.target.SendMessage("OnInput", text, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x060001E5 RID: 485 RVA: 0x0000D204 File Offset: 0x0000B404
	private void OnSubmit()
	{
		if (this.onSubmit && this.target != null)
		{
			this.target.SendMessage("OnSubmit", SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x060001E6 RID: 486 RVA: 0x0000D234 File Offset: 0x0000B434
	private void OnScroll(float delta)
	{
		if (this.onScroll && this.target != null)
		{
			this.target.SendMessage("OnScroll", delta, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x0400023B RID: 571
	public GameObject target;

	// Token: 0x0400023C RID: 572
	public bool onHover;

	// Token: 0x0400023D RID: 573
	public bool onPress;

	// Token: 0x0400023E RID: 574
	public bool onClick;

	// Token: 0x0400023F RID: 575
	public bool onDoubleClick;

	// Token: 0x04000240 RID: 576
	public bool onSelect;

	// Token: 0x04000241 RID: 577
	public bool onDrag;

	// Token: 0x04000242 RID: 578
	public bool onDrop;

	// Token: 0x04000243 RID: 579
	public bool onInput;

	// Token: 0x04000244 RID: 580
	public bool onSubmit;

	// Token: 0x04000245 RID: 581
	public bool onScroll;
}
