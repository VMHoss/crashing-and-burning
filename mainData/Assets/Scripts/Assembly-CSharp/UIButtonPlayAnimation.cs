using System;
using AnimationOrTween;
using UnityEngine;

// Token: 0x02000027 RID: 39
[AddComponentMenu("NGUI/Interaction/Button Play Animation")]
public class UIButtonPlayAnimation : MonoBehaviour
{
	// Token: 0x06000172 RID: 370 RVA: 0x00009C60 File Offset: 0x00007E60
	private void Start()
	{
		this.mStarted = true;
	}

	// Token: 0x06000173 RID: 371 RVA: 0x00009C6C File Offset: 0x00007E6C
	private void OnEnable()
	{
		if (this.mStarted && this.mHighlighted)
		{
			this.OnHover(UICamera.IsHighlighted(base.gameObject));
		}
	}

	// Token: 0x06000174 RID: 372 RVA: 0x00009C98 File Offset: 0x00007E98
	private void OnHover(bool isOver)
	{
		if (base.enabled)
		{
			if (this.trigger == Trigger.OnHover || (this.trigger == Trigger.OnHoverTrue && isOver) || (this.trigger == Trigger.OnHoverFalse && !isOver))
			{
				this.Play(isOver);
			}
			this.mHighlighted = isOver;
		}
	}

	// Token: 0x06000175 RID: 373 RVA: 0x00009CF0 File Offset: 0x00007EF0
	private void OnPress(bool isPressed)
	{
		if (base.enabled && (this.trigger == Trigger.OnPress || (this.trigger == Trigger.OnPressTrue && isPressed) || (this.trigger == Trigger.OnPressFalse && !isPressed)))
		{
			this.Play(isPressed);
		}
	}

	// Token: 0x06000176 RID: 374 RVA: 0x00009D40 File Offset: 0x00007F40
	private void OnClick()
	{
		if (base.enabled && this.trigger == Trigger.OnClick)
		{
			this.Play(true);
		}
	}

	// Token: 0x06000177 RID: 375 RVA: 0x00009D60 File Offset: 0x00007F60
	private void OnDoubleClick()
	{
		if (base.enabled && this.trigger == Trigger.OnDoubleClick)
		{
			this.Play(true);
		}
	}

	// Token: 0x06000178 RID: 376 RVA: 0x00009D84 File Offset: 0x00007F84
	private void OnSelect(bool isSelected)
	{
		if (base.enabled && (this.trigger == Trigger.OnSelect || (this.trigger == Trigger.OnSelectTrue && isSelected) || (this.trigger == Trigger.OnSelectFalse && !isSelected)))
		{
			this.Play(true);
		}
	}

	// Token: 0x06000179 RID: 377 RVA: 0x00009DD8 File Offset: 0x00007FD8
	private void OnActivate(bool isActive)
	{
		if (base.enabled && (this.trigger == Trigger.OnActivate || (this.trigger == Trigger.OnActivateTrue && isActive) || (this.trigger == Trigger.OnActivateFalse && !isActive)))
		{
			this.Play(isActive);
		}
	}

	// Token: 0x0600017A RID: 378 RVA: 0x00009E28 File Offset: 0x00008028
	private void Play(bool forward)
	{
		if (this.target == null)
		{
			this.target = base.GetComponentInChildren<Animation>();
		}
		if (this.target != null)
		{
			if (this.clearSelection && UICamera.selectedObject == base.gameObject)
			{
				UICamera.selectedObject = null;
			}
			int num = (int)(-(int)this.playDirection);
			Direction direction = (Direction)((!forward) ? num : ((int)this.playDirection));
			ActiveAnimation activeAnimation = ActiveAnimation.Play(this.target, this.clipName, direction, this.ifDisabledOnPlay, this.disableWhenFinished);
			if (activeAnimation == null)
			{
				return;
			}
			if (this.resetOnPlay)
			{
				activeAnimation.Reset();
			}
			activeAnimation.onFinished = this.onFinished;
			if (this.eventReceiver != null && !string.IsNullOrEmpty(this.callWhenFinished))
			{
				activeAnimation.eventReceiver = this.eventReceiver;
				activeAnimation.callWhenFinished = this.callWhenFinished;
			}
			else
			{
				activeAnimation.eventReceiver = null;
			}
		}
	}

	// Token: 0x040001AA RID: 426
	public Animation target;

	// Token: 0x040001AB RID: 427
	public string clipName;

	// Token: 0x040001AC RID: 428
	public Trigger trigger;

	// Token: 0x040001AD RID: 429
	public Direction playDirection = Direction.Forward;

	// Token: 0x040001AE RID: 430
	public bool resetOnPlay;

	// Token: 0x040001AF RID: 431
	public bool clearSelection;

	// Token: 0x040001B0 RID: 432
	public EnableCondition ifDisabledOnPlay;

	// Token: 0x040001B1 RID: 433
	public DisableCondition disableWhenFinished;

	// Token: 0x040001B2 RID: 434
	public GameObject eventReceiver;

	// Token: 0x040001B3 RID: 435
	public string callWhenFinished;

	// Token: 0x040001B4 RID: 436
	public ActiveAnimation.OnFinished onFinished;

	// Token: 0x040001B5 RID: 437
	private bool mStarted;

	// Token: 0x040001B6 RID: 438
	private bool mHighlighted;
}
