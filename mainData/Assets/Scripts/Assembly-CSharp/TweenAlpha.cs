using System;
using UnityEngine;

// Token: 0x02000067 RID: 103
[AddComponentMenu("NGUI/Tween/Alpha")]
public class TweenAlpha : UITweener
{
	// Token: 0x1700006B RID: 107
	// (get) Token: 0x06000361 RID: 865 RVA: 0x00015FF0 File Offset: 0x000141F0
	// (set) Token: 0x06000362 RID: 866 RVA: 0x0001603C File Offset: 0x0001423C
	public float alpha
	{
		get
		{
			if (this.mWidget != null)
			{
				return this.mWidget.alpha;
			}
			if (this.mPanel != null)
			{
				return this.mPanel.alpha;
			}
			return 0f;
		}
		set
		{
			if (this.mWidget != null)
			{
				this.mWidget.alpha = value;
			}
			else if (this.mPanel != null)
			{
				this.mPanel.alpha = value;
			}
		}
	}

	// Token: 0x06000363 RID: 867 RVA: 0x00016088 File Offset: 0x00014288
	private void Awake()
	{
		this.mPanel = base.GetComponent<UIPanel>();
		if (this.mPanel == null)
		{
			this.mWidget = base.GetComponentInChildren<UIWidget>();
		}
	}

	// Token: 0x06000364 RID: 868 RVA: 0x000160B4 File Offset: 0x000142B4
	protected override void OnUpdate(float factor, bool isFinished)
	{
		this.alpha = Mathf.Lerp(this.from, this.to, factor);
	}

	// Token: 0x06000365 RID: 869 RVA: 0x000160D0 File Offset: 0x000142D0
	public static TweenAlpha Begin(GameObject go, float duration, float alpha)
	{
		TweenAlpha tweenAlpha = UITweener.Begin<TweenAlpha>(go, duration);
		tweenAlpha.from = tweenAlpha.alpha;
		tweenAlpha.to = alpha;
		if (duration <= 0f)
		{
			tweenAlpha.Sample(1f, true);
			tweenAlpha.enabled = false;
		}
		return tweenAlpha;
	}

	// Token: 0x04000372 RID: 882
	public float from = 1f;

	// Token: 0x04000373 RID: 883
	public float to = 1f;

	// Token: 0x04000374 RID: 884
	private Transform mTrans;

	// Token: 0x04000375 RID: 885
	private UIWidget mWidget;

	// Token: 0x04000376 RID: 886
	private UIPanel mPanel;
}
