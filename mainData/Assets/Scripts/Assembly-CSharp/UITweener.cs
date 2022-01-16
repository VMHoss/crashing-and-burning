using System;
using AnimationOrTween;
using UnityEngine;

// Token: 0x02000070 RID: 112
public abstract class UITweener : IgnoreTimeScale
{
	// Token: 0x17000079 RID: 121
	// (get) Token: 0x06000395 RID: 917 RVA: 0x00016B3C File Offset: 0x00014D3C
	public float amountPerDelta
	{
		get
		{
			if (this.mDuration != this.duration)
			{
				this.mDuration = this.duration;
				this.mAmountPerDelta = Mathf.Abs((this.duration <= 0f) ? 1000f : (1f / this.duration));
			}
			return this.mAmountPerDelta;
		}
	}

	// Token: 0x1700007A RID: 122
	// (get) Token: 0x06000396 RID: 918 RVA: 0x00016BA0 File Offset: 0x00014DA0
	public float tweenFactor
	{
		get
		{
			return this.mFactor;
		}
	}

	// Token: 0x1700007B RID: 123
	// (get) Token: 0x06000397 RID: 919 RVA: 0x00016BA8 File Offset: 0x00014DA8
	public Direction direction
	{
		get
		{
			return (this.mAmountPerDelta >= 0f) ? Direction.Forward : Direction.Reverse;
		}
	}

	// Token: 0x06000398 RID: 920 RVA: 0x00016BC4 File Offset: 0x00014DC4
	private void Start()
	{
		this.Update();
	}

	// Token: 0x06000399 RID: 921 RVA: 0x00016BCC File Offset: 0x00014DCC
	private void Update()
	{
		float num = (!this.ignoreTimeScale) ? Time.deltaTime : base.UpdateRealTimeDelta();
		float num2 = (!this.ignoreTimeScale) ? Time.time : base.realTime;
		if (!this.mStarted)
		{
			this.mStarted = true;
			this.mStartTime = num2 + this.delay;
		}
		if (num2 < this.mStartTime)
		{
			return;
		}
		this.mFactor += this.amountPerDelta * num;
		if (this.style == UITweener.Style.Loop)
		{
			if (this.mFactor > 1f)
			{
				this.mFactor -= Mathf.Floor(this.mFactor);
			}
		}
		else if (this.style == UITweener.Style.PingPong)
		{
			if (this.mFactor > 1f)
			{
				this.mFactor = 1f - (this.mFactor - Mathf.Floor(this.mFactor));
				this.mAmountPerDelta = -this.mAmountPerDelta;
			}
			else if (this.mFactor < 0f)
			{
				this.mFactor = -this.mFactor;
				this.mFactor -= Mathf.Floor(this.mFactor);
				this.mAmountPerDelta = -this.mAmountPerDelta;
			}
		}
		if (this.style == UITweener.Style.Once && (this.mFactor > 1f || this.mFactor < 0f))
		{
			this.mFactor = Mathf.Clamp01(this.mFactor);
			this.Sample(this.mFactor, true);
			if (this.onFinished != null)
			{
				this.onFinished(this);
			}
			if (this.eventReceiver != null && !string.IsNullOrEmpty(this.callWhenFinished))
			{
				this.eventReceiver.SendMessage(this.callWhenFinished, this, SendMessageOptions.DontRequireReceiver);
			}
			if ((this.mFactor == 1f && this.mAmountPerDelta > 0f) || (this.mFactor == 0f && this.mAmountPerDelta < 0f))
			{
				base.enabled = false;
			}
		}
		else
		{
			this.Sample(this.mFactor, false);
		}
	}

	// Token: 0x0600039A RID: 922 RVA: 0x00016E08 File Offset: 0x00015008
	private void OnDisable()
	{
		this.mStarted = false;
	}

	// Token: 0x0600039B RID: 923 RVA: 0x00016E14 File Offset: 0x00015014
	public void Sample(float factor, bool isFinished)
	{
		float num = Mathf.Clamp01(factor);
		if (this.method == UITweener.Method.EaseIn)
		{
			num = 1f - Mathf.Sin(1.5707964f * (1f - num));
			if (this.steeperCurves)
			{
				num *= num;
			}
		}
		else if (this.method == UITweener.Method.EaseOut)
		{
			num = Mathf.Sin(1.5707964f * num);
			if (this.steeperCurves)
			{
				num = 1f - num;
				num = 1f - num * num;
			}
		}
		else if (this.method == UITweener.Method.EaseInOut)
		{
			num -= Mathf.Sin(num * 6.2831855f) / 6.2831855f;
			if (this.steeperCurves)
			{
				num = num * 2f - 1f;
				float num2 = Mathf.Sign(num);
				num = 1f - Mathf.Abs(num);
				num = 1f - num * num;
				num = num2 * num * 0.5f + 0.5f;
			}
		}
		else if (this.method == UITweener.Method.BounceIn)
		{
			num = this.BounceLogic(num);
		}
		else if (this.method == UITweener.Method.BounceOut)
		{
			num = 1f - this.BounceLogic(1f - num);
		}
		this.OnUpdate((this.animationCurve == null) ? num : this.animationCurve.Evaluate(num), isFinished);
	}

	// Token: 0x0600039C RID: 924 RVA: 0x00016F68 File Offset: 0x00015168
	private float BounceLogic(float val)
	{
		if (val < 0.363636f)
		{
			val = 7.5685f * val * val;
		}
		else if (val < 0.727272f)
		{
			val = 7.5625f * (val -= 0.545454f) * val + 0.75f;
		}
		else if (val < 0.90909f)
		{
			val = 7.5625f * (val -= 0.818181f) * val + 0.9375f;
		}
		else
		{
			val = 7.5625f * (val -= 0.9545454f) * val + 0.984375f;
		}
		return val;
	}

	// Token: 0x0600039D RID: 925 RVA: 0x00017000 File Offset: 0x00015200
	public void Play(bool forward)
	{
		this.mAmountPerDelta = Mathf.Abs(this.amountPerDelta);
		if (!forward)
		{
			this.mAmountPerDelta = -this.mAmountPerDelta;
		}
		base.enabled = true;
	}

	// Token: 0x0600039E RID: 926 RVA: 0x00017030 File Offset: 0x00015230
	public void Reset()
	{
		this.mStarted = false;
		this.mFactor = ((this.mAmountPerDelta >= 0f) ? 0f : 1f);
		this.Sample(this.mFactor, false);
	}

	// Token: 0x0600039F RID: 927 RVA: 0x0001706C File Offset: 0x0001526C
	public void Toggle()
	{
		if (this.mFactor > 0f)
		{
			this.mAmountPerDelta = -this.amountPerDelta;
		}
		else
		{
			this.mAmountPerDelta = Mathf.Abs(this.amountPerDelta);
		}
		base.enabled = true;
	}

	// Token: 0x060003A0 RID: 928
	protected abstract void OnUpdate(float factor, bool isFinished);

	// Token: 0x060003A1 RID: 929 RVA: 0x000170B4 File Offset: 0x000152B4
	public static T Begin<T>(GameObject go, float duration) where T : UITweener
	{
		T t = go.GetComponent<T>();
		if (t == null)
		{
			t = go.AddComponent<T>();
		}
		t.mStarted = false;
		t.duration = duration;
		t.mFactor = 0f;
		t.mAmountPerDelta = Mathf.Abs(t.mAmountPerDelta);
		t.style = UITweener.Style.Once;
		t.animationCurve = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 0f, 0f, 1f),
			new Keyframe(1f, 1f, 1f, 0f)
		});
		t.eventReceiver = null;
		t.callWhenFinished = null;
		t.onFinished = null;
		t.enabled = true;
		return t;
	}

	// Token: 0x04000398 RID: 920
	public UITweener.OnFinished onFinished;

	// Token: 0x04000399 RID: 921
	public UITweener.Method method;

	// Token: 0x0400039A RID: 922
	public UITweener.Style style;

	// Token: 0x0400039B RID: 923
	public AnimationCurve animationCurve = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(0f, 0f, 0f, 1f),
		new Keyframe(1f, 1f, 1f, 0f)
	});

	// Token: 0x0400039C RID: 924
	public bool ignoreTimeScale = true;

	// Token: 0x0400039D RID: 925
	public float delay;

	// Token: 0x0400039E RID: 926
	public float duration = 1f;

	// Token: 0x0400039F RID: 927
	public bool steeperCurves;

	// Token: 0x040003A0 RID: 928
	public int tweenGroup;

	// Token: 0x040003A1 RID: 929
	public GameObject eventReceiver;

	// Token: 0x040003A2 RID: 930
	public string callWhenFinished;

	// Token: 0x040003A3 RID: 931
	private bool mStarted;

	// Token: 0x040003A4 RID: 932
	private float mStartTime;

	// Token: 0x040003A5 RID: 933
	private float mDuration;

	// Token: 0x040003A6 RID: 934
	private float mAmountPerDelta = 1f;

	// Token: 0x040003A7 RID: 935
	private float mFactor;

	// Token: 0x02000071 RID: 113
	public enum Method
	{
		// Token: 0x040003A9 RID: 937
		Linear,
		// Token: 0x040003AA RID: 938
		EaseIn,
		// Token: 0x040003AB RID: 939
		EaseOut,
		// Token: 0x040003AC RID: 940
		EaseInOut,
		// Token: 0x040003AD RID: 941
		BounceIn,
		// Token: 0x040003AE RID: 942
		BounceOut
	}

	// Token: 0x02000072 RID: 114
	public enum Style
	{
		// Token: 0x040003B0 RID: 944
		Once,
		// Token: 0x040003B1 RID: 945
		Loop,
		// Token: 0x040003B2 RID: 946
		PingPong
	}

	// Token: 0x020001F1 RID: 497
	// (Invoke) Token: 0x06000DE1 RID: 3553
	public delegate void OnFinished(UITweener tween);
}
