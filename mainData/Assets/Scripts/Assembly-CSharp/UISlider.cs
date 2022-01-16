using System;
using UnityEngine;

// Token: 0x02000044 RID: 68
[AddComponentMenu("NGUI/Interaction/Slider")]
public class UISlider : IgnoreTimeScale
{
	// Token: 0x17000034 RID: 52
	// (get) Token: 0x0600022D RID: 557 RVA: 0x0000F438 File Offset: 0x0000D638
	// (set) Token: 0x0600022E RID: 558 RVA: 0x0000F474 File Offset: 0x0000D674
	public float sliderValue
	{
		get
		{
			float num = this.rawValue;
			if (this.numberOfSteps > 1)
			{
				num = Mathf.Round(num * (float)(this.numberOfSteps - 1)) / (float)(this.numberOfSteps - 1);
			}
			return num;
		}
		set
		{
			this.Set(value, false);
		}
	}

	// Token: 0x17000035 RID: 53
	// (get) Token: 0x0600022F RID: 559 RVA: 0x0000F480 File Offset: 0x0000D680
	// (set) Token: 0x06000230 RID: 560 RVA: 0x0000F488 File Offset: 0x0000D688
	public Vector2 fullSize
	{
		get
		{
			return this.mSize;
		}
		set
		{
			if (this.mSize != value)
			{
				this.mSize = value;
				this.ForceUpdate();
			}
		}
	}

	// Token: 0x06000231 RID: 561 RVA: 0x0000F4A8 File Offset: 0x0000D6A8
	private void Init()
	{
		this.mInitDone = true;
		if (this.foreground != null)
		{
			this.mFGWidget = this.foreground.GetComponent<UIWidget>();
			this.mFGFilled = ((!(this.mFGWidget != null)) ? null : (this.mFGWidget as UISprite));
			this.mFGTrans = this.foreground.transform;
			if (this.mSize == Vector2.zero)
			{
				this.mSize = this.foreground.localScale;
			}
			if (this.mCenter == Vector2.zero)
			{
				this.mCenter = this.foreground.localPosition + this.foreground.localScale * 0.5f;
			}
		}
		else if (this.mCol != null)
		{
			if (this.mSize == Vector2.zero)
			{
				this.mSize = this.mCol.size;
			}
			if (this.mCenter == Vector2.zero)
			{
				this.mCenter = this.mCol.center;
			}
		}
		else
		{
			Debug.LogWarning("UISlider expected to find a foreground object or a box collider to work with", this);
		}
	}

	// Token: 0x06000232 RID: 562 RVA: 0x0000F604 File Offset: 0x0000D804
	private void Awake()
	{
		this.mTrans = base.transform;
		this.mCol = (base.collider as BoxCollider);
	}

	// Token: 0x06000233 RID: 563 RVA: 0x0000F624 File Offset: 0x0000D824
	private void Start()
	{
		this.Init();
		if (Application.isPlaying && this.thumb != null && this.thumb.collider != null)
		{
			UIEventListener uieventListener = UIEventListener.Get(this.thumb.gameObject);
			UIEventListener uieventListener2 = uieventListener;
			uieventListener2.onPress = (UIEventListener.BoolDelegate)Delegate.Combine(uieventListener2.onPress, new UIEventListener.BoolDelegate(this.OnPressThumb));
			UIEventListener uieventListener3 = uieventListener;
			uieventListener3.onDrag = (UIEventListener.VectorDelegate)Delegate.Combine(uieventListener3.onDrag, new UIEventListener.VectorDelegate(this.OnDragThumb));
		}
		this.Set(this.rawValue, true);
	}

	// Token: 0x06000234 RID: 564 RVA: 0x0000F6CC File Offset: 0x0000D8CC
	private void OnPress(bool pressed)
	{
		if (pressed && UICamera.currentTouchID != -100)
		{
			this.UpdateDrag();
		}
	}

	// Token: 0x06000235 RID: 565 RVA: 0x0000F6E8 File Offset: 0x0000D8E8
	private void OnDrag(Vector2 delta)
	{
		this.UpdateDrag();
	}

	// Token: 0x06000236 RID: 566 RVA: 0x0000F6F0 File Offset: 0x0000D8F0
	private void OnPressThumb(GameObject go, bool pressed)
	{
		if (pressed)
		{
			this.UpdateDrag();
		}
	}

	// Token: 0x06000237 RID: 567 RVA: 0x0000F700 File Offset: 0x0000D900
	private void OnDragThumb(GameObject go, Vector2 delta)
	{
		this.UpdateDrag();
	}

	// Token: 0x06000238 RID: 568 RVA: 0x0000F708 File Offset: 0x0000D908
	private void OnKey(KeyCode key)
	{
		float num = ((float)this.numberOfSteps <= 1f) ? 0.125f : (1f / (float)(this.numberOfSteps - 1));
		if (this.direction == UISlider.Direction.Horizontal)
		{
			if (key == KeyCode.LeftArrow)
			{
				this.Set(this.rawValue - num, false);
			}
			else if (key == KeyCode.RightArrow)
			{
				this.Set(this.rawValue + num, false);
			}
		}
		else if (key == KeyCode.DownArrow)
		{
			this.Set(this.rawValue - num, false);
		}
		else if (key == KeyCode.UpArrow)
		{
			this.Set(this.rawValue + num, false);
		}
	}

	// Token: 0x06000239 RID: 569 RVA: 0x0000F7C4 File Offset: 0x0000D9C4
	private void UpdateDrag()
	{
		if (this.mCol == null || UICamera.currentCamera == null || UICamera.currentTouch == null)
		{
			return;
		}
		UICamera.currentTouch.clickNotification = UICamera.ClickNotification.None;
		Ray ray = UICamera.currentCamera.ScreenPointToRay(UICamera.currentTouch.pos);
		Plane plane = new Plane(this.mTrans.rotation * Vector3.back, this.mTrans.position);
		float distance;
		if (!plane.Raycast(ray, out distance))
		{
			return;
		}
		Vector3 b = this.mTrans.localPosition + (this.mCenter - this.mSize * 0.5f);
		Vector3 b2 = this.mTrans.localPosition - b;
		Vector3 a = this.mTrans.InverseTransformPoint(ray.GetPoint(distance));
		Vector3 vector = a + b2;
		this.Set((this.direction != UISlider.Direction.Horizontal) ? (vector.y / this.mSize.y) : (vector.x / this.mSize.x), false);
	}

	// Token: 0x0600023A RID: 570 RVA: 0x0000F8FC File Offset: 0x0000DAFC
	private void Set(float input, bool force)
	{
		if (!this.mInitDone)
		{
			this.Init();
		}
		float num = Mathf.Clamp01(input);
		if (num < 0.001f)
		{
			num = 0f;
		}
		float sliderValue = this.sliderValue;
		this.rawValue = num;
		float sliderValue2 = this.sliderValue;
		if (force || sliderValue != sliderValue2)
		{
			Vector3 localScale = this.mSize;
			if (this.direction == UISlider.Direction.Horizontal)
			{
				localScale.x *= sliderValue2;
			}
			else
			{
				localScale.y *= sliderValue2;
			}
			if (this.mFGFilled != null && this.mFGFilled.type == UISprite.Type.Filled)
			{
				this.mFGFilled.fillAmount = sliderValue2;
			}
			else if (this.foreground != null)
			{
				this.mFGTrans.localScale = localScale;
				if (this.mFGWidget != null)
				{
					if (sliderValue2 > 0.001f)
					{
						this.mFGWidget.enabled = true;
						this.mFGWidget.MarkAsChanged();
					}
					else
					{
						this.mFGWidget.enabled = false;
					}
				}
			}
			if (this.thumb != null)
			{
				Vector3 localPosition = this.thumb.localPosition;
				if (this.mFGFilled != null && this.mFGFilled.type == UISprite.Type.Filled)
				{
					if (this.mFGFilled.fillDirection == UISprite.FillDirection.Horizontal)
					{
						localPosition.x = ((!this.mFGFilled.invert) ? localScale.x : (this.mSize.x - localScale.x));
					}
					else if (this.mFGFilled.fillDirection == UISprite.FillDirection.Vertical)
					{
						localPosition.y = ((!this.mFGFilled.invert) ? localScale.y : (this.mSize.y - localScale.y));
					}
					else
					{
						Debug.LogWarning("Slider thumb is only supported with Horizontal or Vertical fill direction", this);
					}
				}
				else if (this.direction == UISlider.Direction.Horizontal)
				{
					localPosition.x = localScale.x;
				}
				else
				{
					localPosition.y = localScale.y;
				}
				this.thumb.localPosition = localPosition;
			}
			UISlider.current = this;
			if (this.eventReceiver != null && !string.IsNullOrEmpty(this.functionName) && Application.isPlaying)
			{
				this.eventReceiver.SendMessage(this.functionName, sliderValue2, SendMessageOptions.DontRequireReceiver);
			}
			if (this.onValueChange != null)
			{
				this.onValueChange(sliderValue2);
			}
			UISlider.current = null;
		}
	}

	// Token: 0x0600023B RID: 571 RVA: 0x0000FBA8 File Offset: 0x0000DDA8
	public void ForceUpdate()
	{
		this.Set(this.rawValue, true);
	}

	// Token: 0x0400028F RID: 655
	public static UISlider current;

	// Token: 0x04000290 RID: 656
	public Transform foreground;

	// Token: 0x04000291 RID: 657
	public Transform thumb;

	// Token: 0x04000292 RID: 658
	public UISlider.Direction direction;

	// Token: 0x04000293 RID: 659
	public GameObject eventReceiver;

	// Token: 0x04000294 RID: 660
	public string functionName = "OnSliderChange";

	// Token: 0x04000295 RID: 661
	public UISlider.OnValueChange onValueChange;

	// Token: 0x04000296 RID: 662
	public int numberOfSteps;

	// Token: 0x04000297 RID: 663
	[SerializeField]
	[HideInInspector]
	private float rawValue = 1f;

	// Token: 0x04000298 RID: 664
	private BoxCollider mCol;

	// Token: 0x04000299 RID: 665
	private Transform mTrans;

	// Token: 0x0400029A RID: 666
	private Transform mFGTrans;

	// Token: 0x0400029B RID: 667
	private UIWidget mFGWidget;

	// Token: 0x0400029C RID: 668
	private UISprite mFGFilled;

	// Token: 0x0400029D RID: 669
	private bool mInitDone;

	// Token: 0x0400029E RID: 670
	private Vector2 mSize = Vector2.zero;

	// Token: 0x0400029F RID: 671
	private Vector2 mCenter = Vector3.zero;

	// Token: 0x02000045 RID: 69
	public enum Direction
	{
		// Token: 0x040002A1 RID: 673
		Horizontal,
		// Token: 0x040002A2 RID: 674
		Vertical
	}

	// Token: 0x020001E4 RID: 484
	// (Invoke) Token: 0x06000DAD RID: 3501
	public delegate void OnValueChange(float val);
}
