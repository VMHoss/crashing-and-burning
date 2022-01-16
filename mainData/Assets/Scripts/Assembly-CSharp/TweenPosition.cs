using System;
using UnityEngine;

// Token: 0x0200006B RID: 107
[AddComponentMenu("NGUI/Tween/Position")]
public class TweenPosition : UITweener
{
	// Token: 0x17000071 RID: 113
	// (get) Token: 0x06000379 RID: 889 RVA: 0x00016474 File Offset: 0x00014674
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

	// Token: 0x17000072 RID: 114
	// (get) Token: 0x0600037A RID: 890 RVA: 0x0001649C File Offset: 0x0001469C
	// (set) Token: 0x0600037B RID: 891 RVA: 0x000164AC File Offset: 0x000146AC
	public Vector3 position
	{
		get
		{
			return this.cachedTransform.localPosition;
		}
		set
		{
			this.cachedTransform.localPosition = value;
		}
	}

	// Token: 0x0600037C RID: 892 RVA: 0x000164BC File Offset: 0x000146BC
	protected override void OnUpdate(float factor, bool isFinished)
	{
		this.cachedTransform.localPosition = this.from * (1f - factor) + this.to * factor;
	}

	// Token: 0x0600037D RID: 893 RVA: 0x000164F8 File Offset: 0x000146F8
	public static TweenPosition Begin(GameObject go, float duration, Vector3 pos)
	{
		TweenPosition tweenPosition = UITweener.Begin<TweenPosition>(go, duration);
		tweenPosition.from = tweenPosition.position;
		tweenPosition.to = pos;
		if (duration <= 0f)
		{
			tweenPosition.Sample(1f, true);
			tweenPosition.enabled = false;
		}
		return tweenPosition;
	}

	// Token: 0x04000383 RID: 899
	public Vector3 from;

	// Token: 0x04000384 RID: 900
	public Vector3 to;

	// Token: 0x04000385 RID: 901
	private Transform mTrans;
}
