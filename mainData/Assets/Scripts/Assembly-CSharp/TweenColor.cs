using System;
using UnityEngine;

// Token: 0x02000068 RID: 104
[AddComponentMenu("NGUI/Tween/Color")]
public class TweenColor : UITweener
{
	// Token: 0x1700006C RID: 108
	// (get) Token: 0x06000367 RID: 871 RVA: 0x00016138 File Offset: 0x00014338
	// (set) Token: 0x06000368 RID: 872 RVA: 0x000161A4 File Offset: 0x000143A4
	public Color color
	{
		get
		{
			if (this.mWidget != null)
			{
				return this.mWidget.color;
			}
			if (this.mLight != null)
			{
				return this.mLight.color;
			}
			if (this.mMat != null)
			{
				return this.mMat.color;
			}
			return Color.black;
		}
		set
		{
			if (this.mWidget != null)
			{
				this.mWidget.color = value;
			}
			if (this.mMat != null)
			{
				this.mMat.color = value;
			}
			if (this.mLight != null)
			{
				this.mLight.color = value;
				this.mLight.enabled = (value.r + value.g + value.b > 0.01f);
			}
		}
	}

	// Token: 0x06000369 RID: 873 RVA: 0x00016234 File Offset: 0x00014434
	private void Awake()
	{
		this.mWidget = base.GetComponentInChildren<UIWidget>();
		Renderer renderer = base.renderer;
		if (renderer != null)
		{
			this.mMat = renderer.material;
		}
		this.mLight = base.light;
	}

	// Token: 0x0600036A RID: 874 RVA: 0x00016278 File Offset: 0x00014478
	protected override void OnUpdate(float factor, bool isFinished)
	{
		this.color = Color.Lerp(this.from, this.to, factor);
	}

	// Token: 0x0600036B RID: 875 RVA: 0x00016294 File Offset: 0x00014494
	public static TweenColor Begin(GameObject go, float duration, Color color)
	{
		TweenColor tweenColor = UITweener.Begin<TweenColor>(go, duration);
		tweenColor.from = tweenColor.color;
		tweenColor.to = color;
		if (duration <= 0f)
		{
			tweenColor.Sample(1f, true);
			tweenColor.enabled = false;
		}
		return tweenColor;
	}

	// Token: 0x04000377 RID: 887
	public Color from = Color.white;

	// Token: 0x04000378 RID: 888
	public Color to = Color.white;

	// Token: 0x04000379 RID: 889
	private Transform mTrans;

	// Token: 0x0400037A RID: 890
	private UIWidget mWidget;

	// Token: 0x0400037B RID: 891
	private Material mMat;

	// Token: 0x0400037C RID: 892
	private Light mLight;
}
