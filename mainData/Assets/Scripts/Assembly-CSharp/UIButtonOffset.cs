using System;
using UnityEngine;

// Token: 0x02000026 RID: 38
[AddComponentMenu("NGUI/Interaction/Button Offset")]
public class UIButtonOffset : MonoBehaviour
{
	// Token: 0x0600016C RID: 364 RVA: 0x00009A80 File Offset: 0x00007C80
	private void Start()
	{
		if (!this.mStarted)
		{
			this.mStarted = true;
			if (this.tweenTarget == null)
			{
				this.tweenTarget = base.transform;
			}
			this.mPos = this.tweenTarget.localPosition;
		}
	}

	// Token: 0x0600016D RID: 365 RVA: 0x00009AD0 File Offset: 0x00007CD0
	private void OnEnable()
	{
		if (this.mStarted && this.mHighlighted)
		{
			this.OnHover(UICamera.IsHighlighted(base.gameObject));
		}
	}

	// Token: 0x0600016E RID: 366 RVA: 0x00009AFC File Offset: 0x00007CFC
	private void OnDisable()
	{
		if (this.mStarted && this.tweenTarget != null)
		{
			TweenPosition component = this.tweenTarget.GetComponent<TweenPosition>();
			if (component != null)
			{
				component.position = this.mPos;
				component.enabled = false;
			}
		}
	}

	// Token: 0x0600016F RID: 367 RVA: 0x00009B50 File Offset: 0x00007D50
	private void OnPress(bool isPressed)
	{
		if (base.enabled)
		{
			if (!this.mStarted)
			{
				this.Start();
			}
			TweenPosition.Begin(this.tweenTarget.gameObject, this.duration, (!isPressed) ? ((!UICamera.IsHighlighted(base.gameObject)) ? this.mPos : (this.mPos + this.hover)) : (this.mPos + this.pressed)).method = UITweener.Method.EaseInOut;
		}
	}

	// Token: 0x06000170 RID: 368 RVA: 0x00009BE0 File Offset: 0x00007DE0
	private void OnHover(bool isOver)
	{
		if (base.enabled)
		{
			if (!this.mStarted)
			{
				this.Start();
			}
			TweenPosition.Begin(this.tweenTarget.gameObject, this.duration, (!isOver) ? this.mPos : (this.mPos + this.hover)).method = UITweener.Method.EaseInOut;
			this.mHighlighted = isOver;
		}
	}

	// Token: 0x040001A3 RID: 419
	public Transform tweenTarget;

	// Token: 0x040001A4 RID: 420
	public Vector3 hover = Vector3.zero;

	// Token: 0x040001A5 RID: 421
	public Vector3 pressed = new Vector3(2f, -2f);

	// Token: 0x040001A6 RID: 422
	public float duration = 0.2f;

	// Token: 0x040001A7 RID: 423
	private Vector3 mPos;

	// Token: 0x040001A8 RID: 424
	private bool mStarted;

	// Token: 0x040001A9 RID: 425
	private bool mHighlighted;
}
