using System;
using UnityEngine;

// Token: 0x02000032 RID: 50
[AddComponentMenu("NGUI/Interaction/Drag Object")]
public class UIDragObject : IgnoreTimeScale
{
	// Token: 0x060001AF RID: 431 RVA: 0x0000B1E4 File Offset: 0x000093E4
	private void FindPanel()
	{
		this.mPanel = ((!(this.target != null)) ? null : UIPanel.Find(this.target.transform, false));
		if (this.mPanel == null)
		{
			this.restrictWithinPanel = false;
		}
	}

	// Token: 0x060001B0 RID: 432 RVA: 0x0000B238 File Offset: 0x00009438
	private void OnPress(bool pressed)
	{
		if (base.enabled && NGUITools.GetActive(base.gameObject) && this.target != null)
		{
			this.mPressed = pressed;
			if (pressed)
			{
				if (this.restrictWithinPanel && this.mPanel == null)
				{
					this.FindPanel();
				}
				if (this.restrictWithinPanel)
				{
					this.mBounds = NGUIMath.CalculateRelativeWidgetBounds(this.mPanel.cachedTransform, this.target);
				}
				this.mMomentum = Vector3.zero;
				this.mScroll = 0f;
				SpringPosition component = this.target.GetComponent<SpringPosition>();
				if (component != null)
				{
					component.enabled = false;
				}
				this.mLastPos = UICamera.lastHit.point;
				Transform transform = UICamera.currentCamera.transform;
				this.mPlane = new Plane(((!(this.mPanel != null)) ? transform.rotation : this.mPanel.cachedTransform.rotation) * Vector3.back, this.mLastPos);
			}
			else if (this.restrictWithinPanel && this.mPanel.clipping != UIDrawCall.Clipping.None && this.dragEffect == UIDragObject.DragEffect.MomentumAndSpring)
			{
				this.mPanel.ConstrainTargetToBounds(this.target, ref this.mBounds, false);
			}
		}
	}

	// Token: 0x060001B1 RID: 433 RVA: 0x0000B3A4 File Offset: 0x000095A4
	private void OnDrag(Vector2 delta)
	{
		if (base.enabled && NGUITools.GetActive(base.gameObject) && this.target != null)
		{
			UICamera.currentTouch.clickNotification = UICamera.ClickNotification.BasedOnDelta;
			Ray ray = UICamera.currentCamera.ScreenPointToRay(UICamera.currentTouch.pos);
			float distance = 0f;
			if (this.mPlane.Raycast(ray, out distance))
			{
				Vector3 point = ray.GetPoint(distance);
				Vector3 vector = point - this.mLastPos;
				this.mLastPos = point;
				if (vector.x != 0f || vector.y != 0f)
				{
					vector = this.target.InverseTransformDirection(vector);
					vector.Scale(this.scale);
					vector = this.target.TransformDirection(vector);
				}
				if (this.dragEffect != UIDragObject.DragEffect.None)
				{
					this.mMomentum = Vector3.Lerp(this.mMomentum, this.mMomentum + vector * (0.01f * this.momentumAmount), 0.67f);
				}
				if (this.restrictWithinPanel)
				{
					Vector3 localPosition = this.target.localPosition;
					this.target.position += vector;
					this.mBounds.center = this.mBounds.center + (this.target.localPosition - localPosition);
					if (this.dragEffect != UIDragObject.DragEffect.MomentumAndSpring && this.mPanel.clipping != UIDrawCall.Clipping.None && this.mPanel.ConstrainTargetToBounds(this.target, ref this.mBounds, true))
					{
						this.mMomentum = Vector3.zero;
						this.mScroll = 0f;
					}
				}
				else
				{
					this.target.position += vector;
				}
			}
		}
	}

	// Token: 0x060001B2 RID: 434 RVA: 0x0000B588 File Offset: 0x00009788
	private void LateUpdate()
	{
		float deltaTime = base.UpdateRealTimeDelta();
		if (this.target == null)
		{
			return;
		}
		if (this.mPressed)
		{
			SpringPosition component = this.target.GetComponent<SpringPosition>();
			if (component != null)
			{
				component.enabled = false;
			}
			this.mScroll = 0f;
		}
		else
		{
			this.mMomentum += this.scale * (-this.mScroll * 0.05f);
			this.mScroll = NGUIMath.SpringLerp(this.mScroll, 0f, 20f, deltaTime);
			if (this.mMomentum.magnitude > 0.0001f)
			{
				if (this.mPanel == null)
				{
					this.FindPanel();
				}
				if (this.mPanel != null)
				{
					this.target.position += NGUIMath.SpringDampen(ref this.mMomentum, 9f, deltaTime);
					if (this.restrictWithinPanel && this.mPanel.clipping != UIDrawCall.Clipping.None)
					{
						this.mBounds = NGUIMath.CalculateRelativeWidgetBounds(this.mPanel.cachedTransform, this.target);
						if (!this.mPanel.ConstrainTargetToBounds(this.target, ref this.mBounds, this.dragEffect == UIDragObject.DragEffect.None))
						{
							SpringPosition component2 = this.target.GetComponent<SpringPosition>();
							if (component2 != null)
							{
								component2.enabled = false;
							}
						}
					}
					return;
				}
			}
			else
			{
				this.mScroll = 0f;
			}
		}
		NGUIMath.SpringDampen(ref this.mMomentum, 9f, deltaTime);
	}

	// Token: 0x060001B3 RID: 435 RVA: 0x0000B730 File Offset: 0x00009930
	private void OnScroll(float delta)
	{
		if (base.enabled && NGUITools.GetActive(base.gameObject))
		{
			if (Mathf.Sign(this.mScroll) != Mathf.Sign(delta))
			{
				this.mScroll = 0f;
			}
			this.mScroll += delta * this.scrollWheelFactor;
		}
	}

	// Token: 0x040001F6 RID: 502
	public Transform target;

	// Token: 0x040001F7 RID: 503
	public Vector3 scale = Vector3.one;

	// Token: 0x040001F8 RID: 504
	public float scrollWheelFactor;

	// Token: 0x040001F9 RID: 505
	public bool restrictWithinPanel;

	// Token: 0x040001FA RID: 506
	public UIDragObject.DragEffect dragEffect = UIDragObject.DragEffect.MomentumAndSpring;

	// Token: 0x040001FB RID: 507
	public float momentumAmount = 35f;

	// Token: 0x040001FC RID: 508
	private Plane mPlane;

	// Token: 0x040001FD RID: 509
	private Vector3 mLastPos;

	// Token: 0x040001FE RID: 510
	private UIPanel mPanel;

	// Token: 0x040001FF RID: 511
	private bool mPressed;

	// Token: 0x04000200 RID: 512
	private Vector3 mMomentum = Vector3.zero;

	// Token: 0x04000201 RID: 513
	private float mScroll;

	// Token: 0x04000202 RID: 514
	private Bounds mBounds;

	// Token: 0x02000033 RID: 51
	public enum DragEffect
	{
		// Token: 0x04000204 RID: 516
		None,
		// Token: 0x04000205 RID: 517
		Momentum,
		// Token: 0x04000206 RID: 518
		MomentumAndSpring
	}
}
