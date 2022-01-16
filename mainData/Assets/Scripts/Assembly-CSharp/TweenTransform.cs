using System;
using UnityEngine;

// Token: 0x0200006E RID: 110
[AddComponentMenu("NGUI/Tween/Transform")]
public class TweenTransform : UITweener
{
	// Token: 0x0600038B RID: 907 RVA: 0x00016764 File Offset: 0x00014964
	protected override void OnUpdate(float factor, bool isFinished)
	{
		if (this.to != null)
		{
			if (this.mTrans == null)
			{
				this.mTrans = base.transform;
				this.mPos = this.mTrans.position;
				this.mRot = this.mTrans.rotation;
				this.mScale = this.mTrans.localScale;
			}
			if (this.from != null)
			{
				this.mTrans.position = this.from.position * (1f - factor) + this.to.position * factor;
				this.mTrans.localScale = this.from.localScale * (1f - factor) + this.to.localScale * factor;
				this.mTrans.rotation = Quaternion.Slerp(this.from.rotation, this.to.rotation, factor);
			}
			else
			{
				this.mTrans.position = this.mPos * (1f - factor) + this.to.position * factor;
				this.mTrans.localScale = this.mScale * (1f - factor) + this.to.localScale * factor;
				this.mTrans.rotation = Quaternion.Slerp(this.mRot, this.to.rotation, factor);
			}
			if (this.parentWhenFinished && isFinished)
			{
				this.mTrans.parent = this.to;
			}
		}
	}

	// Token: 0x0600038C RID: 908 RVA: 0x0001692C File Offset: 0x00014B2C
	public static TweenTransform Begin(GameObject go, float duration, Transform to)
	{
		return TweenTransform.Begin(go, duration, null, to);
	}

	// Token: 0x0600038D RID: 909 RVA: 0x00016938 File Offset: 0x00014B38
	public static TweenTransform Begin(GameObject go, float duration, Transform from, Transform to)
	{
		TweenTransform tweenTransform = UITweener.Begin<TweenTransform>(go, duration);
		tweenTransform.from = from;
		tweenTransform.to = to;
		if (duration <= 0f)
		{
			tweenTransform.Sample(1f, true);
			tweenTransform.enabled = false;
		}
		return tweenTransform;
	}

	// Token: 0x0400038E RID: 910
	public Transform from;

	// Token: 0x0400038F RID: 911
	public Transform to;

	// Token: 0x04000390 RID: 912
	public bool parentWhenFinished;

	// Token: 0x04000391 RID: 913
	private Transform mTrans;

	// Token: 0x04000392 RID: 914
	private Vector3 mPos;

	// Token: 0x04000393 RID: 915
	private Quaternion mRot;

	// Token: 0x04000394 RID: 916
	private Vector3 mScale;
}
