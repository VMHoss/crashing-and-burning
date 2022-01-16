using System;
using UnityEngine;

// Token: 0x02000042 RID: 66
[AddComponentMenu("NGUI/Interaction/Scroll Bar")]
[ExecuteInEditMode]
public class UIScrollBar : MonoBehaviour
{
	// Token: 0x1700002B RID: 43
	// (get) Token: 0x06000213 RID: 531 RVA: 0x0000E9B8 File Offset: 0x0000CBB8
	public Transform cachedTransform
	{
		get
		{
			if (this.mTrans == null)
			{
				this.mTrans = base.transform;
			}
			return this.mTrans;
		}
	}

	// Token: 0x1700002C RID: 44
	// (get) Token: 0x06000214 RID: 532 RVA: 0x0000E9E0 File Offset: 0x0000CBE0
	public Camera cachedCamera
	{
		get
		{
			if (this.mCam == null)
			{
				this.mCam = NGUITools.FindCameraForLayer(base.gameObject.layer);
			}
			return this.mCam;
		}
	}

	// Token: 0x1700002D RID: 45
	// (get) Token: 0x06000215 RID: 533 RVA: 0x0000EA10 File Offset: 0x0000CC10
	// (set) Token: 0x06000216 RID: 534 RVA: 0x0000EA18 File Offset: 0x0000CC18
	public UISprite background
	{
		get
		{
			return this.mBG;
		}
		set
		{
			if (this.mBG != value)
			{
				this.mBG = value;
				this.mIsDirty = true;
			}
		}
	}

	// Token: 0x1700002E RID: 46
	// (get) Token: 0x06000217 RID: 535 RVA: 0x0000EA3C File Offset: 0x0000CC3C
	// (set) Token: 0x06000218 RID: 536 RVA: 0x0000EA44 File Offset: 0x0000CC44
	public UISprite foreground
	{
		get
		{
			return this.mFG;
		}
		set
		{
			if (this.mFG != value)
			{
				this.mFG = value;
				this.mIsDirty = true;
			}
		}
	}

	// Token: 0x1700002F RID: 47
	// (get) Token: 0x06000219 RID: 537 RVA: 0x0000EA68 File Offset: 0x0000CC68
	// (set) Token: 0x0600021A RID: 538 RVA: 0x0000EA70 File Offset: 0x0000CC70
	public UIScrollBar.Direction direction
	{
		get
		{
			return this.mDir;
		}
		set
		{
			if (this.mDir != value)
			{
				this.mDir = value;
				this.mIsDirty = true;
				if (this.mBG != null)
				{
					Transform cachedTransform = this.mBG.cachedTransform;
					Vector3 localScale = cachedTransform.localScale;
					if ((this.mDir == UIScrollBar.Direction.Vertical && localScale.x > localScale.y) || (this.mDir == UIScrollBar.Direction.Horizontal && localScale.x < localScale.y))
					{
						float x = localScale.x;
						localScale.x = localScale.y;
						localScale.y = x;
						cachedTransform.localScale = localScale;
						this.ForceUpdate();
						if (this.mBG.collider != null)
						{
							NGUITools.AddWidgetCollider(this.mBG.gameObject);
						}
						if (this.mFG.collider != null)
						{
							NGUITools.AddWidgetCollider(this.mFG.gameObject);
						}
					}
				}
			}
		}
	}

	// Token: 0x17000030 RID: 48
	// (get) Token: 0x0600021B RID: 539 RVA: 0x0000EB74 File Offset: 0x0000CD74
	// (set) Token: 0x0600021C RID: 540 RVA: 0x0000EB7C File Offset: 0x0000CD7C
	public bool inverted
	{
		get
		{
			return this.mInverted;
		}
		set
		{
			if (this.mInverted != value)
			{
				this.mInverted = value;
				this.mIsDirty = true;
			}
		}
	}

	// Token: 0x17000031 RID: 49
	// (get) Token: 0x0600021D RID: 541 RVA: 0x0000EB98 File Offset: 0x0000CD98
	// (set) Token: 0x0600021E RID: 542 RVA: 0x0000EBA0 File Offset: 0x0000CDA0
	public float scrollValue
	{
		get
		{
			return this.mScroll;
		}
		set
		{
			float num = Mathf.Clamp01(value);
			if (this.mScroll != num)
			{
				this.mScroll = num;
				this.mIsDirty = true;
				if (this.onChange != null)
				{
					this.onChange(this);
				}
			}
		}
	}

	// Token: 0x17000032 RID: 50
	// (get) Token: 0x0600021F RID: 543 RVA: 0x0000EBE8 File Offset: 0x0000CDE8
	// (set) Token: 0x06000220 RID: 544 RVA: 0x0000EBF0 File Offset: 0x0000CDF0
	public float barSize
	{
		get
		{
			return this.mSize;
		}
		set
		{
			float num = Mathf.Clamp01(value);
			if (this.mSize != num)
			{
				this.mSize = num;
				this.mIsDirty = true;
				if (this.onChange != null)
				{
					this.onChange(this);
				}
			}
		}
	}

	// Token: 0x17000033 RID: 51
	// (get) Token: 0x06000221 RID: 545 RVA: 0x0000EC38 File Offset: 0x0000CE38
	// (set) Token: 0x06000222 RID: 546 RVA: 0x0000EC84 File Offset: 0x0000CE84
	public float alpha
	{
		get
		{
			if (this.mFG != null)
			{
				return this.mFG.alpha;
			}
			if (this.mBG != null)
			{
				return this.mBG.alpha;
			}
			return 0f;
		}
		set
		{
			if (this.mFG != null)
			{
				this.mFG.alpha = value;
				NGUITools.SetActiveSelf(this.mFG.gameObject, this.mFG.alpha > 0.001f);
			}
			if (this.mBG != null)
			{
				this.mBG.alpha = value;
				NGUITools.SetActiveSelf(this.mBG.gameObject, this.mBG.alpha > 0.001f);
			}
		}
	}

	// Token: 0x06000223 RID: 547 RVA: 0x0000ED10 File Offset: 0x0000CF10
	private void CenterOnPos(Vector2 localPos)
	{
		if (this.mBG == null || this.mFG == null)
		{
			return;
		}
		Bounds bounds = NGUIMath.CalculateRelativeInnerBounds(this.cachedTransform, this.mBG);
		Bounds bounds2 = NGUIMath.CalculateRelativeInnerBounds(this.cachedTransform, this.mFG);
		if (this.mDir == UIScrollBar.Direction.Horizontal)
		{
			float num = bounds.size.x - bounds2.size.x;
			float num2 = num * 0.5f;
			float num3 = bounds.center.x - num2;
			float num4 = (num <= 0f) ? 0f : ((localPos.x - num3) / num);
			this.scrollValue = ((!this.mInverted) ? num4 : (1f - num4));
		}
		else
		{
			float num5 = bounds.size.y - bounds2.size.y;
			float num6 = num5 * 0.5f;
			float num7 = bounds.center.y - num6;
			float num8 = (num5 <= 0f) ? 0f : (1f - (localPos.y - num7) / num5);
			this.scrollValue = ((!this.mInverted) ? num8 : (1f - num8));
		}
	}

	// Token: 0x06000224 RID: 548 RVA: 0x0000EE84 File Offset: 0x0000D084
	private void Reposition(Vector2 screenPos)
	{
		Transform cachedTransform = this.cachedTransform;
		Plane plane = new Plane(cachedTransform.rotation * Vector3.back, cachedTransform.position);
		Ray ray = this.cachedCamera.ScreenPointToRay(screenPos);
		float distance;
		if (!plane.Raycast(ray, out distance))
		{
			return;
		}
		this.CenterOnPos(cachedTransform.InverseTransformPoint(ray.GetPoint(distance)));
	}

	// Token: 0x06000225 RID: 549 RVA: 0x0000EEF0 File Offset: 0x0000D0F0
	private void OnPressBackground(GameObject go, bool isPressed)
	{
		this.mCam = UICamera.currentCamera;
		this.Reposition(UICamera.lastTouchPosition);
		if (!isPressed && this.onDragFinished != null)
		{
			this.onDragFinished();
		}
	}

	// Token: 0x06000226 RID: 550 RVA: 0x0000EF30 File Offset: 0x0000D130
	private void OnDragBackground(GameObject go, Vector2 delta)
	{
		this.mCam = UICamera.currentCamera;
		this.Reposition(UICamera.lastTouchPosition);
	}

	// Token: 0x06000227 RID: 551 RVA: 0x0000EF48 File Offset: 0x0000D148
	private void OnPressForeground(GameObject go, bool isPressed)
	{
		if (isPressed)
		{
			this.mCam = UICamera.currentCamera;
			Bounds bounds = NGUIMath.CalculateAbsoluteWidgetBounds(this.mFG.cachedTransform);
			this.mScreenPos = this.mCam.WorldToScreenPoint(bounds.center);
		}
		else if (this.onDragFinished != null)
		{
			this.onDragFinished();
		}
	}

	// Token: 0x06000228 RID: 552 RVA: 0x0000EFB0 File Offset: 0x0000D1B0
	private void OnDragForeground(GameObject go, Vector2 delta)
	{
		this.mCam = UICamera.currentCamera;
		this.Reposition(this.mScreenPos + UICamera.currentTouch.totalDelta);
	}

	// Token: 0x06000229 RID: 553 RVA: 0x0000EFE4 File Offset: 0x0000D1E4
	private void Start()
	{
		if (this.background != null && this.background.collider != null)
		{
			UIEventListener uieventListener = UIEventListener.Get(this.background.gameObject);
			UIEventListener uieventListener2 = uieventListener;
			uieventListener2.onPress = (UIEventListener.BoolDelegate)Delegate.Combine(uieventListener2.onPress, new UIEventListener.BoolDelegate(this.OnPressBackground));
			UIEventListener uieventListener3 = uieventListener;
			uieventListener3.onDrag = (UIEventListener.VectorDelegate)Delegate.Combine(uieventListener3.onDrag, new UIEventListener.VectorDelegate(this.OnDragBackground));
		}
		if (this.foreground != null && this.foreground.collider != null)
		{
			UIEventListener uieventListener4 = UIEventListener.Get(this.foreground.gameObject);
			UIEventListener uieventListener5 = uieventListener4;
			uieventListener5.onPress = (UIEventListener.BoolDelegate)Delegate.Combine(uieventListener5.onPress, new UIEventListener.BoolDelegate(this.OnPressForeground));
			UIEventListener uieventListener6 = uieventListener4;
			uieventListener6.onDrag = (UIEventListener.VectorDelegate)Delegate.Combine(uieventListener6.onDrag, new UIEventListener.VectorDelegate(this.OnDragForeground));
		}
		this.ForceUpdate();
	}

	// Token: 0x0600022A RID: 554 RVA: 0x0000F0F0 File Offset: 0x0000D2F0
	private void Update()
	{
		if (this.mIsDirty)
		{
			this.ForceUpdate();
		}
	}

	// Token: 0x0600022B RID: 555 RVA: 0x0000F104 File Offset: 0x0000D304
	public void ForceUpdate()
	{
		this.mIsDirty = false;
		if (this.mBG != null && this.mFG != null)
		{
			this.mSize = Mathf.Clamp01(this.mSize);
			this.mScroll = Mathf.Clamp01(this.mScroll);
			Vector4 border = this.mBG.border;
			Vector4 border2 = this.mFG.border;
			Vector2 vector = new Vector2(Mathf.Max(0f, this.mBG.cachedTransform.localScale.x - border.x - border.z), Mathf.Max(0f, this.mBG.cachedTransform.localScale.y - border.y - border.w));
			float num = (!this.mInverted) ? this.mScroll : (1f - this.mScroll);
			if (this.mDir == UIScrollBar.Direction.Horizontal)
			{
				Vector2 vector2 = new Vector2(vector.x * this.mSize, vector.y);
				this.mFG.pivot = UIWidget.Pivot.Left;
				this.mBG.pivot = UIWidget.Pivot.Left;
				this.mBG.cachedTransform.localPosition = Vector3.zero;
				this.mFG.cachedTransform.localPosition = new Vector3(border.x - border2.x + (vector.x - vector2.x) * num, 0f, 0f);
				this.mFG.cachedTransform.localScale = new Vector3(vector2.x + border2.x + border2.z, vector2.y + border2.y + border2.w, 1f);
				if (num < 0.999f && num > 0.001f)
				{
					this.mFG.MakePixelPerfect();
				}
			}
			else
			{
				Vector2 vector3 = new Vector2(vector.x, vector.y * this.mSize);
				this.mFG.pivot = UIWidget.Pivot.Top;
				this.mBG.pivot = UIWidget.Pivot.Top;
				this.mBG.cachedTransform.localPosition = Vector3.zero;
				this.mFG.cachedTransform.localPosition = new Vector3(0f, -border.y + border2.y - (vector.y - vector3.y) * num, 0f);
				this.mFG.cachedTransform.localScale = new Vector3(vector3.x + border2.x + border2.z, vector3.y + border2.y + border2.w, 1f);
				if (num < 0.999f && num > 0.001f)
				{
					this.mFG.MakePixelPerfect();
				}
			}
		}
	}

	// Token: 0x04000280 RID: 640
	[HideInInspector]
	[SerializeField]
	private UISprite mBG;

	// Token: 0x04000281 RID: 641
	[HideInInspector]
	[SerializeField]
	private UISprite mFG;

	// Token: 0x04000282 RID: 642
	[SerializeField]
	[HideInInspector]
	private UIScrollBar.Direction mDir;

	// Token: 0x04000283 RID: 643
	[SerializeField]
	[HideInInspector]
	private bool mInverted;

	// Token: 0x04000284 RID: 644
	[HideInInspector]
	[SerializeField]
	private float mScroll;

	// Token: 0x04000285 RID: 645
	[HideInInspector]
	[SerializeField]
	private float mSize = 1f;

	// Token: 0x04000286 RID: 646
	private Transform mTrans;

	// Token: 0x04000287 RID: 647
	private bool mIsDirty;

	// Token: 0x04000288 RID: 648
	private Camera mCam;

	// Token: 0x04000289 RID: 649
	private Vector2 mScreenPos = Vector2.zero;

	// Token: 0x0400028A RID: 650
	public UIScrollBar.OnScrollBarChange onChange;

	// Token: 0x0400028B RID: 651
	public UIScrollBar.OnDragFinished onDragFinished;

	// Token: 0x02000043 RID: 67
	public enum Direction
	{
		// Token: 0x0400028D RID: 653
		Horizontal,
		// Token: 0x0400028E RID: 654
		Vertical
	}

	// Token: 0x020001E2 RID: 482
	// (Invoke) Token: 0x06000DA5 RID: 3493
	public delegate void OnScrollBarChange(UIScrollBar sb);

	// Token: 0x020001E3 RID: 483
	// (Invoke) Token: 0x06000DA9 RID: 3497
	public delegate void OnDragFinished();
}
