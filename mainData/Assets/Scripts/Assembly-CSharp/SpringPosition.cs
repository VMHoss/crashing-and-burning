using System;
using UnityEngine;

// Token: 0x02000066 RID: 102
[AddComponentMenu("NGUI/Tween/Spring Position")]
public class SpringPosition : IgnoreTimeScale
{
	// Token: 0x0600035D RID: 861 RVA: 0x00015D4C File Offset: 0x00013F4C
	private void Start()
	{
		this.mTrans = base.transform;
	}

	// Token: 0x0600035E RID: 862 RVA: 0x00015D5C File Offset: 0x00013F5C
	private void Update()
	{
		float deltaTime = (!this.ignoreTimeScale) ? Time.deltaTime : base.UpdateRealTimeDelta();
		if (this.worldSpace)
		{
			if (this.mThreshold == 0f)
			{
				this.mThreshold = (this.target - this.mTrans.position).magnitude * 0.001f;
			}
			this.mTrans.position = NGUIMath.SpringLerp(this.mTrans.position, this.target, this.strength, deltaTime);
			if (this.mThreshold >= (this.target - this.mTrans.position).magnitude)
			{
				this.mTrans.position = this.target;
				if (this.onFinished != null)
				{
					this.onFinished(this);
				}
				if (this.eventReceiver != null && !string.IsNullOrEmpty(this.callWhenFinished))
				{
					this.eventReceiver.SendMessage(this.callWhenFinished, this, SendMessageOptions.DontRequireReceiver);
				}
				base.enabled = false;
			}
		}
		else
		{
			if (this.mThreshold == 0f)
			{
				this.mThreshold = (this.target - this.mTrans.localPosition).magnitude * 0.001f;
			}
			this.mTrans.localPosition = NGUIMath.SpringLerp(this.mTrans.localPosition, this.target, this.strength, deltaTime);
			if (this.mThreshold >= (this.target - this.mTrans.localPosition).magnitude)
			{
				this.mTrans.localPosition = this.target;
				if (this.onFinished != null)
				{
					this.onFinished(this);
				}
				if (this.eventReceiver != null && !string.IsNullOrEmpty(this.callWhenFinished))
				{
					this.eventReceiver.SendMessage(this.callWhenFinished, this, SendMessageOptions.DontRequireReceiver);
				}
				base.enabled = false;
			}
		}
	}

	// Token: 0x0600035F RID: 863 RVA: 0x00015F74 File Offset: 0x00014174
	public static SpringPosition Begin(GameObject go, Vector3 pos, float strength)
	{
		SpringPosition springPosition = go.GetComponent<SpringPosition>();
		if (springPosition == null)
		{
			springPosition = go.AddComponent<SpringPosition>();
		}
		springPosition.target = pos;
		springPosition.strength = strength;
		springPosition.onFinished = null;
		if (!springPosition.enabled)
		{
			springPosition.mThreshold = 0f;
			springPosition.enabled = true;
		}
		return springPosition;
	}

	// Token: 0x04000369 RID: 873
	public Vector3 target = Vector3.zero;

	// Token: 0x0400036A RID: 874
	public float strength = 10f;

	// Token: 0x0400036B RID: 875
	public bool worldSpace;

	// Token: 0x0400036C RID: 876
	public bool ignoreTimeScale;

	// Token: 0x0400036D RID: 877
	public GameObject eventReceiver;

	// Token: 0x0400036E RID: 878
	public string callWhenFinished;

	// Token: 0x0400036F RID: 879
	public SpringPosition.OnFinished onFinished;

	// Token: 0x04000370 RID: 880
	private Transform mTrans;

	// Token: 0x04000371 RID: 881
	private float mThreshold;

	// Token: 0x020001F0 RID: 496
	// (Invoke) Token: 0x06000DDD RID: 3549
	public delegate void OnFinished(SpringPosition spring);
}
