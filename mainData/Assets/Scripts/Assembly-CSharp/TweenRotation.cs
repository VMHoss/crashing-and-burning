using System;
using UnityEngine;

// Token: 0x0200006C RID: 108
[AddComponentMenu("NGUI/Tween/Rotation")]
public class TweenRotation : UITweener
{
	// Token: 0x17000073 RID: 115
	// (get) Token: 0x0600037F RID: 895 RVA: 0x00016548 File Offset: 0x00014748
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

	// Token: 0x17000074 RID: 116
	// (get) Token: 0x06000380 RID: 896 RVA: 0x00016570 File Offset: 0x00014770
	// (set) Token: 0x06000381 RID: 897 RVA: 0x00016580 File Offset: 0x00014780
	public Quaternion rotation
	{
		get
		{
			return this.cachedTransform.localRotation;
		}
		set
		{
			this.cachedTransform.localRotation = value;
		}
	}

	// Token: 0x06000382 RID: 898 RVA: 0x00016590 File Offset: 0x00014790
	protected override void OnUpdate(float factor, bool isFinished)
	{
		this.cachedTransform.localRotation = Quaternion.Slerp(Quaternion.Euler(this.from), Quaternion.Euler(this.to), factor);
	}

	// Token: 0x06000383 RID: 899 RVA: 0x000165C4 File Offset: 0x000147C4
	public static TweenRotation Begin(GameObject go, float duration, Quaternion rot)
	{
		TweenRotation tweenRotation = UITweener.Begin<TweenRotation>(go, duration);
		tweenRotation.from = tweenRotation.rotation.eulerAngles;
		tweenRotation.to = rot.eulerAngles;
		if (duration <= 0f)
		{
			tweenRotation.Sample(1f, true);
			tweenRotation.enabled = false;
		}
		return tweenRotation;
	}

	// Token: 0x04000386 RID: 902
	public Vector3 from;

	// Token: 0x04000387 RID: 903
	public Vector3 to;

	// Token: 0x04000388 RID: 904
	private Transform mTrans;
}
