using System;
using AnimationOrTween;
using UnityEngine;

// Token: 0x0200002C RID: 44
[AddComponentMenu("NGUI/Interaction/Button Tween")]
public class UIButtonTween : MonoBehaviour
{
	// Token: 0x0600018C RID: 396 RVA: 0x0000A474 File Offset: 0x00008674
	private void Start()
	{
		this.mStarted = true;
		if (this.tweenTarget == null)
		{
			this.tweenTarget = base.gameObject;
		}
	}

	// Token: 0x0600018D RID: 397 RVA: 0x0000A4A8 File Offset: 0x000086A8
	private void OnEnable()
	{
		if (this.mStarted && this.mHighlighted)
		{
			this.OnHover(UICamera.IsHighlighted(base.gameObject));
		}
	}

	// Token: 0x0600018E RID: 398 RVA: 0x0000A4D4 File Offset: 0x000086D4
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

	// Token: 0x0600018F RID: 399 RVA: 0x0000A52C File Offset: 0x0000872C
	private void OnPress(bool isPressed)
	{
		if (base.enabled && (this.trigger == Trigger.OnPress || (this.trigger == Trigger.OnPressTrue && isPressed) || (this.trigger == Trigger.OnPressFalse && !isPressed)))
		{
			this.Play(isPressed);
		}
	}

	// Token: 0x06000190 RID: 400 RVA: 0x0000A57C File Offset: 0x0000877C
	private void OnClick()
	{
		if (base.enabled && this.trigger == Trigger.OnClick)
		{
			this.Play(true);
		}
	}

	// Token: 0x06000191 RID: 401 RVA: 0x0000A59C File Offset: 0x0000879C
	private void OnDoubleClick()
	{
		if (base.enabled && this.trigger == Trigger.OnDoubleClick)
		{
			this.Play(true);
		}
	}

	// Token: 0x06000192 RID: 402 RVA: 0x0000A5C0 File Offset: 0x000087C0
	private void OnSelect(bool isSelected)
	{
		if (base.enabled && (this.trigger == Trigger.OnSelect || (this.trigger == Trigger.OnSelectTrue && isSelected) || (this.trigger == Trigger.OnSelectFalse && !isSelected)))
		{
			this.Play(true);
		}
	}

	// Token: 0x06000193 RID: 403 RVA: 0x0000A614 File Offset: 0x00008814
	private void OnActivate(bool isActive)
	{
		if (base.enabled && (this.trigger == Trigger.OnActivate || (this.trigger == Trigger.OnActivateTrue && isActive) || (this.trigger == Trigger.OnActivateFalse && !isActive)))
		{
			this.Play(isActive);
		}
	}

	// Token: 0x06000194 RID: 404 RVA: 0x0000A664 File Offset: 0x00008864
	private void Update()
	{
		if (this.disableWhenFinished != DisableCondition.DoNotDisable && this.mTweens != null)
		{
			bool flag = true;
			bool flag2 = true;
			int i = 0;
			int num = this.mTweens.Length;
			while (i < num)
			{
				UITweener uitweener = this.mTweens[i];
				if (uitweener.tweenGroup == this.tweenGroup)
				{
					if (uitweener.enabled)
					{
						flag = false;
						break;
					}
					if (uitweener.direction != (Direction)this.disableWhenFinished)
					{
						flag2 = false;
					}
				}
				i++;
			}
			if (flag)
			{
				if (flag2)
				{
					NGUITools.SetActive(this.tweenTarget, false);
				}
				this.mTweens = null;
			}
		}
	}

	// Token: 0x06000195 RID: 405 RVA: 0x0000A710 File Offset: 0x00008910
	public void Play(bool forward)
	{
		GameObject gameObject = (!(this.tweenTarget == null)) ? this.tweenTarget : base.gameObject;
		if (!NGUITools.GetActive(gameObject))
		{
			if (this.ifDisabledOnPlay != EnableCondition.EnableThenPlay)
			{
				return;
			}
			NGUITools.SetActive(gameObject, true);
		}
		this.mTweens = ((!this.includeChildren) ? gameObject.GetComponents<UITweener>() : gameObject.GetComponentsInChildren<UITweener>());
		if (this.mTweens.Length == 0)
		{
			if (this.disableWhenFinished != DisableCondition.DoNotDisable)
			{
				NGUITools.SetActive(this.tweenTarget, false);
			}
		}
		else
		{
			bool flag = false;
			if (this.playDirection == Direction.Reverse)
			{
				forward = !forward;
			}
			int i = 0;
			int num = this.mTweens.Length;
			while (i < num)
			{
				UITweener uitweener = this.mTweens[i];
				if (uitweener.tweenGroup == this.tweenGroup)
				{
					if (!flag && !NGUITools.GetActive(gameObject))
					{
						flag = true;
						NGUITools.SetActive(gameObject, true);
					}
					if (this.playDirection == Direction.Toggle)
					{
						uitweener.Toggle();
					}
					else
					{
						uitweener.Play(forward);
					}
					if (this.resetOnPlay)
					{
						uitweener.Reset();
					}
					uitweener.onFinished = this.onFinished;
					if (this.eventReceiver != null && !string.IsNullOrEmpty(this.callWhenFinished))
					{
						uitweener.eventReceiver = this.eventReceiver;
						uitweener.callWhenFinished = this.callWhenFinished;
					}
				}
				i++;
			}
		}
	}

	// Token: 0x040001CF RID: 463
	public GameObject tweenTarget;

	// Token: 0x040001D0 RID: 464
	public int tweenGroup;

	// Token: 0x040001D1 RID: 465
	public Trigger trigger;

	// Token: 0x040001D2 RID: 466
	public Direction playDirection = Direction.Forward;

	// Token: 0x040001D3 RID: 467
	public bool resetOnPlay;

	// Token: 0x040001D4 RID: 468
	public EnableCondition ifDisabledOnPlay;

	// Token: 0x040001D5 RID: 469
	public DisableCondition disableWhenFinished;

	// Token: 0x040001D6 RID: 470
	public bool includeChildren;

	// Token: 0x040001D7 RID: 471
	public GameObject eventReceiver;

	// Token: 0x040001D8 RID: 472
	public string callWhenFinished;

	// Token: 0x040001D9 RID: 473
	public UITweener.OnFinished onFinished;

	// Token: 0x040001DA RID: 474
	private UITweener[] mTweens;

	// Token: 0x040001DB RID: 475
	private bool mStarted;

	// Token: 0x040001DC RID: 476
	private bool mHighlighted;
}
