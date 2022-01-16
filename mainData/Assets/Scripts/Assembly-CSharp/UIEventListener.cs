using System;
using UnityEngine;

// Token: 0x0200005C RID: 92
[AddComponentMenu("NGUI/Internal/Event Listener")]
public class UIEventListener : MonoBehaviour
{
	// Token: 0x06000301 RID: 769 RVA: 0x00014524 File Offset: 0x00012724
	private void OnSubmit()
	{
		if (this.onSubmit != null)
		{
			this.onSubmit(base.gameObject);
		}
	}

	// Token: 0x06000302 RID: 770 RVA: 0x00014544 File Offset: 0x00012744
	private void OnClick()
	{
		if (this.onClick != null)
		{
			this.onClick(base.gameObject);
		}
	}

	// Token: 0x06000303 RID: 771 RVA: 0x00014564 File Offset: 0x00012764
	private void OnDoubleClick()
	{
		if (this.onDoubleClick != null)
		{
			this.onDoubleClick(base.gameObject);
		}
	}

	// Token: 0x06000304 RID: 772 RVA: 0x00014584 File Offset: 0x00012784
	private void OnHover(bool isOver)
	{
		if (this.onHover != null)
		{
			this.onHover(base.gameObject, isOver);
		}
	}

	// Token: 0x06000305 RID: 773 RVA: 0x000145A4 File Offset: 0x000127A4
	private void OnPress(bool isPressed)
	{
		if (this.onPress != null)
		{
			this.onPress(base.gameObject, isPressed);
		}
	}

	// Token: 0x06000306 RID: 774 RVA: 0x000145C4 File Offset: 0x000127C4
	private void OnSelect(bool selected)
	{
		if (this.onSelect != null)
		{
			this.onSelect(base.gameObject, selected);
		}
	}

	// Token: 0x06000307 RID: 775 RVA: 0x000145E4 File Offset: 0x000127E4
	private void OnScroll(float delta)
	{
		if (this.onScroll != null)
		{
			this.onScroll(base.gameObject, delta);
		}
	}

	// Token: 0x06000308 RID: 776 RVA: 0x00014604 File Offset: 0x00012804
	private void OnDrag(Vector2 delta)
	{
		if (this.onDrag != null)
		{
			this.onDrag(base.gameObject, delta);
		}
	}

	// Token: 0x06000309 RID: 777 RVA: 0x00014624 File Offset: 0x00012824
	private void OnDrop(GameObject go)
	{
		if (this.onDrop != null)
		{
			this.onDrop(base.gameObject, go);
		}
	}

	// Token: 0x0600030A RID: 778 RVA: 0x00014644 File Offset: 0x00012844
	private void OnInput(string text)
	{
		if (this.onInput != null)
		{
			this.onInput(base.gameObject, text);
		}
	}

	// Token: 0x0600030B RID: 779 RVA: 0x00014664 File Offset: 0x00012864
	private void OnKey(KeyCode key)
	{
		if (this.onKey != null)
		{
			this.onKey(base.gameObject, key);
		}
	}

	// Token: 0x0600030C RID: 780 RVA: 0x00014684 File Offset: 0x00012884
	public static UIEventListener Get(GameObject go)
	{
		UIEventListener uieventListener = go.GetComponent<UIEventListener>();
		if (uieventListener == null)
		{
			uieventListener = go.AddComponent<UIEventListener>();
		}
		return uieventListener;
	}

	// Token: 0x04000323 RID: 803
	public object parameter;

	// Token: 0x04000324 RID: 804
	public UIEventListener.VoidDelegate onSubmit;

	// Token: 0x04000325 RID: 805
	public UIEventListener.VoidDelegate onClick;

	// Token: 0x04000326 RID: 806
	public UIEventListener.VoidDelegate onDoubleClick;

	// Token: 0x04000327 RID: 807
	public UIEventListener.BoolDelegate onHover;

	// Token: 0x04000328 RID: 808
	public UIEventListener.BoolDelegate onPress;

	// Token: 0x04000329 RID: 809
	public UIEventListener.BoolDelegate onSelect;

	// Token: 0x0400032A RID: 810
	public UIEventListener.FloatDelegate onScroll;

	// Token: 0x0400032B RID: 811
	public UIEventListener.VectorDelegate onDrag;

	// Token: 0x0400032C RID: 812
	public UIEventListener.ObjectDelegate onDrop;

	// Token: 0x0400032D RID: 813
	public UIEventListener.StringDelegate onInput;

	// Token: 0x0400032E RID: 814
	public UIEventListener.KeyCodeDelegate onKey;

	// Token: 0x020001E8 RID: 488
	// (Invoke) Token: 0x06000DBD RID: 3517
	public delegate void VoidDelegate(GameObject go);

	// Token: 0x020001E9 RID: 489
	// (Invoke) Token: 0x06000DC1 RID: 3521
	public delegate void BoolDelegate(GameObject go, bool state);

	// Token: 0x020001EA RID: 490
	// (Invoke) Token: 0x06000DC5 RID: 3525
	public delegate void FloatDelegate(GameObject go, float delta);

	// Token: 0x020001EB RID: 491
	// (Invoke) Token: 0x06000DC9 RID: 3529
	public delegate void VectorDelegate(GameObject go, Vector2 delta);

	// Token: 0x020001EC RID: 492
	// (Invoke) Token: 0x06000DCD RID: 3533
	public delegate void StringDelegate(GameObject go, string text);

	// Token: 0x020001ED RID: 493
	// (Invoke) Token: 0x06000DD1 RID: 3537
	public delegate void ObjectDelegate(GameObject go, GameObject draggedObject);

	// Token: 0x020001EE RID: 494
	// (Invoke) Token: 0x06000DD5 RID: 3541
	public delegate void KeyCodeDelegate(GameObject go, KeyCode key);
}
