using System;
using UnityEngine;

// Token: 0x02000028 RID: 40
[AddComponentMenu("NGUI/Interaction/Button Rotation")]
public class UIButtonRotation : MonoBehaviour
{
	// Token: 0x0600017C RID: 380 RVA: 0x00009F60 File Offset: 0x00008160
	private void Start()
	{
		if (!this.mStarted)
		{
			this.mStarted = true;
			if (this.tweenTarget == null)
			{
				this.tweenTarget = base.transform;
			}
			this.mRot = this.tweenTarget.localRotation;
		}
	}

	// Token: 0x0600017D RID: 381 RVA: 0x00009FB0 File Offset: 0x000081B0
	private void OnEnable()
	{
		if (this.mStarted && this.mHighlighted)
		{
			this.OnHover(UICamera.IsHighlighted(base.gameObject));
		}
	}

	// Token: 0x0600017E RID: 382 RVA: 0x00009FDC File Offset: 0x000081DC
	private void OnDisable()
	{
		if (this.mStarted && this.tweenTarget != null)
		{
			TweenRotation component = this.tweenTarget.GetComponent<TweenRotation>();
			if (component != null)
			{
				component.rotation = this.mRot;
				component.enabled = false;
			}
		}
	}

	// Token: 0x0600017F RID: 383 RVA: 0x0000A030 File Offset: 0x00008230
	private void OnPress(bool isPressed)
	{
		if (base.enabled)
		{
			if (!this.mStarted)
			{
				this.Start();
			}
			TweenRotation.Begin(this.tweenTarget.gameObject, this.duration, (!isPressed) ? ((!UICamera.IsHighlighted(base.gameObject)) ? this.mRot : (this.mRot * Quaternion.Euler(this.hover))) : (this.mRot * Quaternion.Euler(this.pressed))).method = UITweener.Method.EaseInOut;
		}
	}

	// Token: 0x06000180 RID: 384 RVA: 0x0000A0C8 File Offset: 0x000082C8
	private void OnHover(bool isOver)
	{
		if (base.enabled)
		{
			if (!this.mStarted)
			{
				this.Start();
			}
			TweenRotation.Begin(this.tweenTarget.gameObject, this.duration, (!isOver) ? this.mRot : (this.mRot * Quaternion.Euler(this.hover))).method = UITweener.Method.EaseInOut;
			this.mHighlighted = isOver;
		}
	}

	// Token: 0x040001B7 RID: 439
	public Transform tweenTarget;

	// Token: 0x040001B8 RID: 440
	public Vector3 hover = Vector3.zero;

	// Token: 0x040001B9 RID: 441
	public Vector3 pressed = Vector3.zero;

	// Token: 0x040001BA RID: 442
	public float duration = 0.2f;

	// Token: 0x040001BB RID: 443
	private Quaternion mRot;

	// Token: 0x040001BC RID: 444
	private bool mStarted;

	// Token: 0x040001BD RID: 445
	private bool mHighlighted;
}
