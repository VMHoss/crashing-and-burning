using System;
using UnityEngine;

// Token: 0x02000054 RID: 84
[AddComponentMenu("NGUI/Internal/Ignore TimeScale Behaviour")]
public class IgnoreTimeScale : MonoBehaviour
{
	// Token: 0x17000048 RID: 72
	// (get) Token: 0x06000286 RID: 646 RVA: 0x00011468 File Offset: 0x0000F668
	public float realTime
	{
		get
		{
			return this.mRt;
		}
	}

	// Token: 0x17000049 RID: 73
	// (get) Token: 0x06000287 RID: 647 RVA: 0x00011470 File Offset: 0x0000F670
	public float realTimeDelta
	{
		get
		{
			return this.mTimeDelta;
		}
	}

	// Token: 0x06000288 RID: 648 RVA: 0x00011478 File Offset: 0x0000F678
	protected virtual void OnEnable()
	{
		this.mTimeStarted = true;
		this.mTimeDelta = 0f;
		this.mTimeStart = Time.realtimeSinceStartup;
	}

	// Token: 0x06000289 RID: 649 RVA: 0x00011498 File Offset: 0x0000F698
	protected float UpdateRealTimeDelta()
	{
		this.mRt = Time.realtimeSinceStartup;
		if (this.mTimeStarted)
		{
			float b = this.mRt - this.mTimeStart;
			this.mActual += Mathf.Max(0f, b);
			this.mTimeDelta = 0.001f * Mathf.Round(this.mActual * 1000f);
			this.mActual -= this.mTimeDelta;
			if (this.mTimeDelta > 1f)
			{
				this.mTimeDelta = 1f;
			}
			this.mTimeStart = this.mRt;
		}
		else
		{
			this.mTimeStarted = true;
			this.mTimeStart = this.mRt;
			this.mTimeDelta = 0f;
		}
		return this.mTimeDelta;
	}

	// Token: 0x040002F7 RID: 759
	private float mRt;

	// Token: 0x040002F8 RID: 760
	private float mTimeStart;

	// Token: 0x040002F9 RID: 761
	private float mTimeDelta;

	// Token: 0x040002FA RID: 762
	private float mActual;

	// Token: 0x040002FB RID: 763
	private bool mTimeStarted;
}
