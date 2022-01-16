using System;
using UnityEngine;

// Token: 0x02000036 RID: 54
[AddComponentMenu("NGUI/Interaction/Draggable Panel")]
[RequireComponent(typeof(UIPanel))]
[ExecuteInEditMode]
public class UIDraggablePanel : IgnoreTimeScale
{
	// Token: 0x17000020 RID: 32
	// (get) Token: 0x060001C6 RID: 454 RVA: 0x0000BF24 File Offset: 0x0000A124
	public UIPanel panel
	{
		get
		{
			return this.mPanel;
		}
	}

	// Token: 0x17000021 RID: 33
	// (get) Token: 0x060001C7 RID: 455 RVA: 0x0000BF2C File Offset: 0x0000A12C
	public Bounds bounds
	{
		get
		{
			if (!this.mCalculatedBounds)
			{
				this.mCalculatedBounds = true;
				this.mBounds = NGUIMath.CalculateRelativeWidgetBounds(this.mTrans, this.mTrans);
			}
			return this.mBounds;
		}
	}

	// Token: 0x17000022 RID: 34
	// (get) Token: 0x060001C8 RID: 456 RVA: 0x0000BF60 File Offset: 0x0000A160
	public bool shouldMoveHorizontally
	{
		get
		{
			float num = this.bounds.size.x;
			if (this.mPanel.clipping == UIDrawCall.Clipping.SoftClip)
			{
				num += this.mPanel.clipSoftness.x * 2f;
			}
			return num > this.mPanel.clipRange.z;
		}
	}

	// Token: 0x17000023 RID: 35
	// (get) Token: 0x060001C9 RID: 457 RVA: 0x0000BFC8 File Offset: 0x0000A1C8
	public bool shouldMoveVertically
	{
		get
		{
			float num = this.bounds.size.y;
			if (this.mPanel.clipping == UIDrawCall.Clipping.SoftClip)
			{
				num += this.mPanel.clipSoftness.y * 2f;
			}
			return num > this.mPanel.clipRange.w;
		}
	}

	// Token: 0x17000024 RID: 36
	// (get) Token: 0x060001CA RID: 458 RVA: 0x0000C030 File Offset: 0x0000A230
	private bool shouldMove
	{
		get
		{
			if (!this.disableDragIfFits)
			{
				return true;
			}
			if (this.mPanel == null)
			{
				this.mPanel = base.GetComponent<UIPanel>();
			}
			Vector4 clipRange = this.mPanel.clipRange;
			Bounds bounds = this.bounds;
			float num = (clipRange.z != 0f) ? (clipRange.z * 0.5f) : ((float)Screen.width);
			float num2 = (clipRange.w != 0f) ? (clipRange.w * 0.5f) : ((float)Screen.height);
			if (!Mathf.Approximately(this.scale.x, 0f))
			{
				if (bounds.min.x < clipRange.x - num)
				{
					return true;
				}
				if (bounds.max.x > clipRange.x + num)
				{
					return true;
				}
			}
			if (!Mathf.Approximately(this.scale.y, 0f))
			{
				if (bounds.min.y < clipRange.y - num2)
				{
					return true;
				}
				if (bounds.max.y > clipRange.y + num2)
				{
					return true;
				}
			}
			return false;
		}
	}

	// Token: 0x17000025 RID: 37
	// (get) Token: 0x060001CB RID: 459 RVA: 0x0000C184 File Offset: 0x0000A384
	// (set) Token: 0x060001CC RID: 460 RVA: 0x0000C18C File Offset: 0x0000A38C
	public Vector3 currentMomentum
	{
		get
		{
			return this.mMomentum;
		}
		set
		{
			this.mMomentum = value;
			this.mShouldMove = true;
		}
	}

	// Token: 0x060001CD RID: 461 RVA: 0x0000C19C File Offset: 0x0000A39C
	private void Awake()
	{
		this.mTrans = base.transform;
		this.mPanel = base.GetComponent<UIPanel>();
	}

	// Token: 0x060001CE RID: 462 RVA: 0x0000C1B8 File Offset: 0x0000A3B8
	private void Start()
	{
		this.UpdateScrollbars(true);
		if (this.horizontalScrollBar != null)
		{
			UIScrollBar uiscrollBar = this.horizontalScrollBar;
			uiscrollBar.onChange = (UIScrollBar.OnScrollBarChange)Delegate.Combine(uiscrollBar.onChange, new UIScrollBar.OnScrollBarChange(this.OnHorizontalBar));
			this.horizontalScrollBar.alpha = ((this.showScrollBars != UIDraggablePanel.ShowCondition.Always && !this.shouldMoveHorizontally) ? 0f : 1f);
		}
		if (this.verticalScrollBar != null)
		{
			UIScrollBar uiscrollBar2 = this.verticalScrollBar;
			uiscrollBar2.onChange = (UIScrollBar.OnScrollBarChange)Delegate.Combine(uiscrollBar2.onChange, new UIScrollBar.OnScrollBarChange(this.OnVerticalBar));
			this.verticalScrollBar.alpha = ((this.showScrollBars != UIDraggablePanel.ShowCondition.Always && !this.shouldMoveVertically) ? 0f : 1f);
		}
	}

	// Token: 0x060001CF RID: 463 RVA: 0x0000C29C File Offset: 0x0000A49C
	public bool RestrictWithinBounds(bool instant)
	{
		Vector3 vector = this.mPanel.CalculateConstrainOffset(this.bounds.min, this.bounds.max);
		if (vector.magnitude > 0.001f)
		{
			if (!instant && this.dragEffect == UIDraggablePanel.DragEffect.MomentumAndSpring)
			{
				SpringPanel.Begin(this.mPanel.gameObject, this.mTrans.localPosition + vector, 13f);
			}
			else
			{
				this.MoveRelative(vector);
				this.mMomentum = Vector3.zero;
				this.mScroll = 0f;
			}
			return true;
		}
		return false;
	}

	// Token: 0x060001D0 RID: 464 RVA: 0x0000C34C File Offset: 0x0000A54C
	public void DisableSpring()
	{
		SpringPanel component = base.GetComponent<SpringPanel>();
		if (component != null)
		{
			component.enabled = false;
		}
	}

	// Token: 0x060001D1 RID: 465 RVA: 0x0000C374 File Offset: 0x0000A574
	public void UpdateScrollbars(bool recalculateBounds)
	{
		if (this.mPanel == null)
		{
			return;
		}
		if (this.horizontalScrollBar != null || this.verticalScrollBar != null)
		{
			if (recalculateBounds)
			{
				this.mCalculatedBounds = false;
				this.mShouldMove = this.shouldMove;
			}
			Bounds bounds = this.bounds;
			Vector2 a = bounds.min;
			Vector2 a2 = bounds.max;
			if (this.mPanel.clipping == UIDrawCall.Clipping.SoftClip)
			{
				Vector2 clipSoftness = this.mPanel.clipSoftness;
				a -= clipSoftness;
				a2 += clipSoftness;
			}
			if (this.horizontalScrollBar != null && a2.x > a.x)
			{
				Vector4 clipRange = this.mPanel.clipRange;
				float num = clipRange.z * 0.5f;
				float num2 = clipRange.x - num - bounds.min.x;
				float num3 = bounds.max.x - num - clipRange.x;
				float num4 = a2.x - a.x;
				num2 = Mathf.Clamp01(num2 / num4);
				num3 = Mathf.Clamp01(num3 / num4);
				float num5 = num2 + num3;
				this.mIgnoreCallbacks = true;
				this.horizontalScrollBar.barSize = 1f - num5;
				this.horizontalScrollBar.scrollValue = ((num5 <= 0.001f) ? 0f : (num2 / num5));
				this.mIgnoreCallbacks = false;
			}
			if (this.verticalScrollBar != null && a2.y > a.y)
			{
				Vector4 clipRange2 = this.mPanel.clipRange;
				float num6 = clipRange2.w * 0.5f;
				float num7 = clipRange2.y - num6 - a.y;
				float num8 = a2.y - num6 - clipRange2.y;
				float num9 = a2.y - a.y;
				num7 = Mathf.Clamp01(num7 / num9);
				num8 = Mathf.Clamp01(num8 / num9);
				float num10 = num7 + num8;
				this.mIgnoreCallbacks = true;
				this.verticalScrollBar.barSize = 1f - num10;
				this.verticalScrollBar.scrollValue = ((num10 <= 0.001f) ? 0f : (1f - num7 / num10));
				this.mIgnoreCallbacks = false;
			}
		}
		else if (recalculateBounds)
		{
			this.mCalculatedBounds = false;
		}
	}

	// Token: 0x060001D2 RID: 466 RVA: 0x0000C604 File Offset: 0x0000A804
	public void SetDragAmount(float x, float y, bool updateScrollbars)
	{
		this.DisableSpring();
		Bounds bounds = this.bounds;
		if (bounds.min.x == bounds.max.x || bounds.min.y == bounds.max.x)
		{
			return;
		}
		Vector4 clipRange = this.mPanel.clipRange;
		float num = clipRange.z * 0.5f;
		float num2 = clipRange.w * 0.5f;
		float num3 = bounds.min.x + num;
		float num4 = bounds.max.x - num;
		float num5 = bounds.min.y + num2;
		float num6 = bounds.max.y - num2;
		if (this.mPanel.clipping == UIDrawCall.Clipping.SoftClip)
		{
			num3 -= this.mPanel.clipSoftness.x;
			num4 += this.mPanel.clipSoftness.x;
			num5 -= this.mPanel.clipSoftness.y;
			num6 += this.mPanel.clipSoftness.y;
		}
		float num7 = Mathf.Lerp(num3, num4, x);
		float num8 = Mathf.Lerp(num6, num5, y);
		if (!updateScrollbars)
		{
			Vector3 localPosition = this.mTrans.localPosition;
			if (this.scale.x != 0f)
			{
				localPosition.x += clipRange.x - num7;
			}
			if (this.scale.y != 0f)
			{
				localPosition.y += clipRange.y - num8;
			}
			this.mTrans.localPosition = localPosition;
		}
		clipRange.x = num7;
		clipRange.y = num8;
		this.mPanel.clipRange = clipRange;
		if (updateScrollbars)
		{
			this.UpdateScrollbars(false);
		}
	}

	// Token: 0x060001D3 RID: 467 RVA: 0x0000C814 File Offset: 0x0000AA14
	public void ResetPosition()
	{
		this.mCalculatedBounds = false;
		this.SetDragAmount(this.relativePositionOnReset.x, this.relativePositionOnReset.y, false);
		this.SetDragAmount(this.relativePositionOnReset.x, this.relativePositionOnReset.y, true);
	}

	// Token: 0x060001D4 RID: 468 RVA: 0x0000C864 File Offset: 0x0000AA64
	private void OnHorizontalBar(UIScrollBar sb)
	{
		if (!this.mIgnoreCallbacks)
		{
			float x = (!(this.horizontalScrollBar != null)) ? 0f : this.horizontalScrollBar.scrollValue;
			float y = (!(this.verticalScrollBar != null)) ? 0f : this.verticalScrollBar.scrollValue;
			this.SetDragAmount(x, y, false);
		}
	}

	// Token: 0x060001D5 RID: 469 RVA: 0x0000C8D4 File Offset: 0x0000AAD4
	private void OnVerticalBar(UIScrollBar sb)
	{
		if (!this.mIgnoreCallbacks)
		{
			float x = (!(this.horizontalScrollBar != null)) ? 0f : this.horizontalScrollBar.scrollValue;
			float y = (!(this.verticalScrollBar != null)) ? 0f : this.verticalScrollBar.scrollValue;
			this.SetDragAmount(x, y, false);
		}
	}

	// Token: 0x060001D6 RID: 470 RVA: 0x0000C944 File Offset: 0x0000AB44
	public void MoveRelative(Vector3 relative)
	{
		this.mTrans.localPosition += relative;
		Vector4 clipRange = this.mPanel.clipRange;
		clipRange.x -= relative.x;
		clipRange.y -= relative.y;
		this.mPanel.clipRange = clipRange;
		this.UpdateScrollbars(false);
	}

	// Token: 0x060001D7 RID: 471 RVA: 0x0000C9B4 File Offset: 0x0000ABB4
	public void MoveAbsolute(Vector3 absolute)
	{
		Vector3 a = this.mTrans.InverseTransformPoint(absolute);
		Vector3 b = this.mTrans.InverseTransformPoint(Vector3.zero);
		this.MoveRelative(a - b);
	}

	// Token: 0x060001D8 RID: 472 RVA: 0x0000C9EC File Offset: 0x0000ABEC
	public void Press(bool pressed)
	{
		if (this.smoothDragStart && pressed)
		{
			this.mDragStarted = false;
			this.mDragStartOffset = Vector2.zero;
		}
		if (base.enabled && NGUITools.GetActive(base.gameObject))
		{
			if (!pressed && this.mDragID == UICamera.currentTouchID)
			{
				this.mDragID = -10;
			}
			this.mCalculatedBounds = false;
			this.mShouldMove = this.shouldMove;
			if (!this.mShouldMove)
			{
				return;
			}
			this.mPressed = pressed;
			if (pressed)
			{
				this.mMomentum = Vector3.zero;
				this.mScroll = 0f;
				this.DisableSpring();
				this.mLastPos = UICamera.lastHit.point;
				this.mPlane = new Plane(this.mTrans.rotation * Vector3.back, this.mLastPos);
			}
			else
			{
				if (this.restrictWithinPanel && this.mPanel.clipping != UIDrawCall.Clipping.None && this.dragEffect == UIDraggablePanel.DragEffect.MomentumAndSpring)
				{
					this.RestrictWithinBounds(false);
				}
				if (this.onDragFinished != null)
				{
					this.onDragFinished();
				}
			}
		}
	}

	// Token: 0x060001D9 RID: 473 RVA: 0x0000CB20 File Offset: 0x0000AD20
	public void Drag()
	{
		if (base.enabled && NGUITools.GetActive(base.gameObject) && this.mShouldMove)
		{
			if (this.mDragID == -10)
			{
				this.mDragID = UICamera.currentTouchID;
			}
			UICamera.currentTouch.clickNotification = UICamera.ClickNotification.BasedOnDelta;
			if (this.smoothDragStart && !this.mDragStarted)
			{
				this.mDragStarted = true;
				this.mDragStartOffset = UICamera.currentTouch.totalDelta;
			}
			Ray ray = (!this.smoothDragStart) ? UICamera.currentCamera.ScreenPointToRay(UICamera.currentTouch.pos) : UICamera.currentCamera.ScreenPointToRay(UICamera.currentTouch.pos - this.mDragStartOffset);
			float distance = 0f;
			if (this.mPlane.Raycast(ray, out distance))
			{
				Vector3 point = ray.GetPoint(distance);
				Vector3 vector = point - this.mLastPos;
				this.mLastPos = point;
				if (vector.x != 0f || vector.y != 0f)
				{
					vector = this.mTrans.InverseTransformDirection(vector);
					vector.Scale(this.scale);
					vector = this.mTrans.TransformDirection(vector);
				}
				this.mMomentum = Vector3.Lerp(this.mMomentum, this.mMomentum + vector * (0.01f * this.momentumAmount), 0.67f);
				if (!this.iOSDragEmulation)
				{
					this.MoveAbsolute(vector);
				}
				else if (this.mPanel.CalculateConstrainOffset(this.bounds.min, this.bounds.max).magnitude > 0.001f)
				{
					this.MoveAbsolute(vector * 0.5f);
					this.mMomentum *= 0.5f;
				}
				else
				{
					this.MoveAbsolute(vector);
				}
				if (this.restrictWithinPanel && this.mPanel.clipping != UIDrawCall.Clipping.None && this.dragEffect != UIDraggablePanel.DragEffect.MomentumAndSpring)
				{
					this.RestrictWithinBounds(true);
				}
			}
		}
	}

	// Token: 0x060001DA RID: 474 RVA: 0x0000CD64 File Offset: 0x0000AF64
	public void Scroll(float delta)
	{
		if (base.enabled && NGUITools.GetActive(base.gameObject) && this.scrollWheelFactor != 0f)
		{
			this.DisableSpring();
			this.mShouldMove = this.shouldMove;
			if (Mathf.Sign(this.mScroll) != Mathf.Sign(delta))
			{
				this.mScroll = 0f;
			}
			this.mScroll += delta * this.scrollWheelFactor;
		}
	}

	// Token: 0x060001DB RID: 475 RVA: 0x0000CDE4 File Offset: 0x0000AFE4
	private void LateUpdate()
	{
		if (this.mPanel.changedLastFrame)
		{
			this.UpdateScrollbars(true);
		}
		if (this.repositionClipping)
		{
			this.repositionClipping = false;
			this.mCalculatedBounds = false;
			this.SetDragAmount(this.relativePositionOnReset.x, this.relativePositionOnReset.y, true);
		}
		if (!Application.isPlaying)
		{
			return;
		}
		float num = base.UpdateRealTimeDelta();
		if (this.showScrollBars != UIDraggablePanel.ShowCondition.Always)
		{
			bool flag = false;
			bool flag2 = false;
			if (this.showScrollBars != UIDraggablePanel.ShowCondition.WhenDragging || this.mDragID != -10 || this.mMomentum.magnitude > 0.01f)
			{
				flag = this.shouldMoveVertically;
				flag2 = this.shouldMoveHorizontally;
			}
			if (this.verticalScrollBar)
			{
				float num2 = this.verticalScrollBar.alpha;
				num2 += ((!flag) ? (-num * 3f) : (num * 6f));
				num2 = Mathf.Clamp01(num2);
				if (this.verticalScrollBar.alpha != num2)
				{
					this.verticalScrollBar.alpha = num2;
				}
			}
			if (this.horizontalScrollBar)
			{
				float num3 = this.horizontalScrollBar.alpha;
				num3 += ((!flag2) ? (-num * 3f) : (num * 6f));
				num3 = Mathf.Clamp01(num3);
				if (this.horizontalScrollBar.alpha != num3)
				{
					this.horizontalScrollBar.alpha = num3;
				}
			}
		}
		if (this.mShouldMove && !this.mPressed)
		{
			this.mMomentum -= this.scale * (this.mScroll * 0.05f);
			if (this.mMomentum.magnitude > 0.0001f)
			{
				this.mScroll = NGUIMath.SpringLerp(this.mScroll, 0f, 20f, num);
				Vector3 absolute = NGUIMath.SpringDampen(ref this.mMomentum, 9f, num);
				this.MoveAbsolute(absolute);
				if (this.restrictWithinPanel && this.mPanel.clipping != UIDrawCall.Clipping.None)
				{
					this.RestrictWithinBounds(false);
				}
				return;
			}
			this.mScroll = 0f;
			this.mMomentum = Vector3.zero;
		}
		else
		{
			this.mScroll = 0f;
		}
		NGUIMath.SpringDampen(ref this.mMomentum, 9f, num);
	}

	// Token: 0x04000217 RID: 535
	public bool restrictWithinPanel = true;

	// Token: 0x04000218 RID: 536
	public bool disableDragIfFits;

	// Token: 0x04000219 RID: 537
	public UIDraggablePanel.DragEffect dragEffect = UIDraggablePanel.DragEffect.MomentumAndSpring;

	// Token: 0x0400021A RID: 538
	public bool smoothDragStart = true;

	// Token: 0x0400021B RID: 539
	public Vector3 scale = Vector3.one;

	// Token: 0x0400021C RID: 540
	public float scrollWheelFactor;

	// Token: 0x0400021D RID: 541
	public float momentumAmount = 35f;

	// Token: 0x0400021E RID: 542
	public Vector2 relativePositionOnReset = Vector2.zero;

	// Token: 0x0400021F RID: 543
	public bool repositionClipping;

	// Token: 0x04000220 RID: 544
	public bool iOSDragEmulation = true;

	// Token: 0x04000221 RID: 545
	public UIScrollBar horizontalScrollBar;

	// Token: 0x04000222 RID: 546
	public UIScrollBar verticalScrollBar;

	// Token: 0x04000223 RID: 547
	public UIDraggablePanel.ShowCondition showScrollBars = UIDraggablePanel.ShowCondition.OnlyIfNeeded;

	// Token: 0x04000224 RID: 548
	public UIDraggablePanel.OnDragFinished onDragFinished;

	// Token: 0x04000225 RID: 549
	private Transform mTrans;

	// Token: 0x04000226 RID: 550
	private UIPanel mPanel;

	// Token: 0x04000227 RID: 551
	private Plane mPlane;

	// Token: 0x04000228 RID: 552
	private Vector3 mLastPos;

	// Token: 0x04000229 RID: 553
	private bool mPressed;

	// Token: 0x0400022A RID: 554
	private Vector3 mMomentum = Vector3.zero;

	// Token: 0x0400022B RID: 555
	private float mScroll;

	// Token: 0x0400022C RID: 556
	private Bounds mBounds;

	// Token: 0x0400022D RID: 557
	private bool mCalculatedBounds;

	// Token: 0x0400022E RID: 558
	private bool mShouldMove;

	// Token: 0x0400022F RID: 559
	private bool mIgnoreCallbacks;

	// Token: 0x04000230 RID: 560
	private int mDragID = -10;

	// Token: 0x04000231 RID: 561
	private Vector2 mDragStartOffset = Vector2.zero;

	// Token: 0x04000232 RID: 562
	private bool mDragStarted;

	// Token: 0x02000037 RID: 55
	public enum DragEffect
	{
		// Token: 0x04000234 RID: 564
		None,
		// Token: 0x04000235 RID: 565
		Momentum,
		// Token: 0x04000236 RID: 566
		MomentumAndSpring
	}

	// Token: 0x02000038 RID: 56
	public enum ShowCondition
	{
		// Token: 0x04000238 RID: 568
		Always,
		// Token: 0x04000239 RID: 569
		OnlyIfNeeded,
		// Token: 0x0400023A RID: 570
		WhenDragging
	}

	// Token: 0x020001E0 RID: 480
	// (Invoke) Token: 0x06000D9D RID: 3485
	public delegate void OnDragFinished();
}
