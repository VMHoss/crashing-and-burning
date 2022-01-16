using System;
using UnityEngine;

// Token: 0x0200006A RID: 106
[AddComponentMenu("NGUI/Tween/Orthographic Size")]
[RequireComponent(typeof(Camera))]
public class TweenOrthoSize : UITweener
{
	// Token: 0x1700006F RID: 111
	// (get) Token: 0x06000373 RID: 883 RVA: 0x000163AC File Offset: 0x000145AC
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

	// Token: 0x17000070 RID: 112
	// (get) Token: 0x06000374 RID: 884 RVA: 0x000163D4 File Offset: 0x000145D4
	// (set) Token: 0x06000375 RID: 885 RVA: 0x000163E4 File Offset: 0x000145E4
	public float orthoSize
	{
		get
		{
			return this.cachedCamera.orthographicSize;
		}
		set
		{
			this.cachedCamera.orthographicSize = value;
		}
	}

	// Token: 0x06000376 RID: 886 RVA: 0x000163F4 File Offset: 0x000145F4
	protected override void OnUpdate(float factor, bool isFinished)
	{
		this.cachedCamera.orthographicSize = this.from * (1f - factor) + this.to * factor;
	}

	// Token: 0x06000377 RID: 887 RVA: 0x00016424 File Offset: 0x00014624
	public static TweenOrthoSize Begin(GameObject go, float duration, float to)
	{
		TweenOrthoSize tweenOrthoSize = UITweener.Begin<TweenOrthoSize>(go, duration);
		tweenOrthoSize.from = tweenOrthoSize.orthoSize;
		tweenOrthoSize.to = to;
		if (duration <= 0f)
		{
			tweenOrthoSize.Sample(1f, true);
			tweenOrthoSize.enabled = false;
		}
		return tweenOrthoSize;
	}

	// Token: 0x04000380 RID: 896
	public float from;

	// Token: 0x04000381 RID: 897
	public float to;

	// Token: 0x04000382 RID: 898
	private Camera mCam;
}
