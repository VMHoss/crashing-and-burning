using System;
using UnityEngine;

// Token: 0x02000069 RID: 105
[AddComponentMenu("NGUI/Tween/Field of View")]
[RequireComponent(typeof(Camera))]
public class TweenFOV : UITweener
{
	// Token: 0x1700006D RID: 109
	// (get) Token: 0x0600036D RID: 877 RVA: 0x000162E4 File Offset: 0x000144E4
	public Camera cachedCamera
	{
		get
		{
			if (this.mCam == null)
			{
				this.mCam = base.camera;
			}
			return this.mCam;
		}
	}

	// Token: 0x1700006E RID: 110
	// (get) Token: 0x0600036E RID: 878 RVA: 0x0001630C File Offset: 0x0001450C
	// (set) Token: 0x0600036F RID: 879 RVA: 0x0001631C File Offset: 0x0001451C
	public float fov
	{
		get
		{
			return this.cachedCamera.fieldOfView;
		}
		set
		{
			this.cachedCamera.fieldOfView = value;
		}
	}

	// Token: 0x06000370 RID: 880 RVA: 0x0001632C File Offset: 0x0001452C
	protected override void OnUpdate(float factor, bool isFinished)
	{
		this.cachedCamera.fieldOfView = this.from * (1f - factor) + this.to * factor;
	}

	// Token: 0x06000371 RID: 881 RVA: 0x0001635C File Offset: 0x0001455C
	public static TweenFOV Begin(GameObject go, float duration, float to)
	{
		TweenFOV tweenFOV = UITweener.Begin<TweenFOV>(go, duration);
		tweenFOV.from = tweenFOV.fov;
		tweenFOV.to = to;
		if (duration <= 0f)
		{
			tweenFOV.Sample(1f, true);
			tweenFOV.enabled = false;
		}
		return tweenFOV;
	}

	// Token: 0x0400037D RID: 893
	public float from;

	// Token: 0x0400037E RID: 894
	public float to;

	// Token: 0x0400037F RID: 895
	private Camera mCam;
}
