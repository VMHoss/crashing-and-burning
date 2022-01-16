using System;
using UnityEngine;

// Token: 0x02000029 RID: 41
[AddComponentMenu("NGUI/Interaction/Button Scale")]
public class UIButtonScale : MonoBehaviour
{
	// Token: 0x06000182 RID: 386 RVA: 0x0000A190 File Offset: 0x00008390
	private void Start()
	{
		if (!this.mStarted)
		{
			this.mStarted = true;
			if (this.tweenTarget == null)
			{
				this.tweenTarget = base.transform;
			}
			this.mScale = this.tweenTarget.localScale;
		}
	}

	// Token: 0x06000183 RID: 387 RVA: 0x0000A1E0 File Offset: 0x000083E0
	private void OnEnable()
	{
		if (this.mStarted && this.mHighlighted)
		{
			this.OnHover(UICamera.IsHighlighted(base.gameObject));
		}
	}

	// Token: 0x06000184 RID: 388 RVA: 0x0000A20C File Offset: 0x0000840C
	private void OnDisable()
	{
		if (this.mStarted && this.tweenTarget != null)
		{
			TweenScale component = this.tweenTarget.GetComponent<TweenScale>();
			if (component != null)
			{
				component.scale = this.mScale;
				component.enabled = false;
			}
		}
	}

	// Token: 0x06000185 RID: 389 RVA: 0x0000A260 File Offset: 0x00008460
	private void OnPress(bool isPressed)
	{
		if (base.enabled)
		{
			if (!this.mStarted)
			{
				this.Start();
			}
			TweenScale.Begin(this.tweenTarget.gameObject, this.duration, (!isPressed) ? ((!UICamera.IsHighlighted(base.gameObject)) ? this.mScale : Vector3.Scale(this.mScale, this.hover)) : Vector3.Scale(this.mScale, this.pressed)).method = UITweener.Method.EaseInOut;
		}
	}

	// Token: 0x06000186 RID: 390 RVA: 0x0000A2F0 File Offset: 0x000084F0
	private void OnHover(bool isOver)
	{
		if (base.enabled)
		{
			if (!this.mStarted)
			{
				this.Start();
			}
			TweenScale.Begin(this.tweenTarget.gameObject, this.duration, (!isOver) ? this.mScale : Vector3.Scale(this.mScale, this.hover)).method = UITweener.Method.EaseInOut;
			this.mHighlighted = isOver;
		}
	}

	// Token: 0x040001BE RID: 446
	public Transform tweenTarget;

	// Token: 0x040001BF RID: 447
	public Vector3 hover = new Vector3(1.1f, 1.1f, 1.1f);

	// Token: 0x040001C0 RID: 448
	public Vector3 pressed = new Vector3(1.05f, 1.05f, 1.05f);

	// Token: 0x040001C1 RID: 449
	public float duration = 0.2f;

	// Token: 0x040001C2 RID: 450
	private Vector3 mScale;

	// Token: 0x040001C3 RID: 451
	private bool mStarted;

	// Token: 0x040001C4 RID: 452
	private bool mHighlighted;
}
