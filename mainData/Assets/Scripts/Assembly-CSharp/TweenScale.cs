using System;
using UnityEngine;

// Token: 0x0200006D RID: 109
[AddComponentMenu("NGUI/Tween/Scale")]
public class TweenScale : UITweener
{
	// Token: 0x17000075 RID: 117
	// (get) Token: 0x06000385 RID: 901 RVA: 0x0001663C File Offset: 0x0001483C
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

	// Token: 0x17000076 RID: 118
	// (get) Token: 0x06000386 RID: 902 RVA: 0x00016664 File Offset: 0x00014864
	// (set) Token: 0x06000387 RID: 903 RVA: 0x00016674 File Offset: 0x00014874
	public Vector3 scale
	{
		get
		{
			return this.cachedTransform.localScale;
		}
		set
		{
			this.cachedTransform.localScale = value;
		}
	}

	// Token: 0x06000388 RID: 904 RVA: 0x00016684 File Offset: 0x00014884
	protected override void OnUpdate(float factor, bool isFinished)
	{
		this.cachedTransform.localScale = this.from * (1f - factor) + this.to * factor;
		if (this.updateTable)
		{
			if (this.mTable == null)
			{
				this.mTable = NGUITools.FindInParents<UITable>(base.gameObject);
				if (this.mTable == null)
				{
					this.updateTable = false;
					return;
				}
			}
			this.mTable.repositionNow = true;
		}
	}

	// Token: 0x06000389 RID: 905 RVA: 0x00016714 File Offset: 0x00014914
	public static TweenScale Begin(GameObject go, float duration, Vector3 scale)
	{
		TweenScale tweenScale = UITweener.Begin<TweenScale>(go, duration);
		tweenScale.from = tweenScale.scale;
		tweenScale.to = scale;
		if (duration <= 0f)
		{
			tweenScale.Sample(1f, true);
			tweenScale.enabled = false;
		}
		return tweenScale;
	}

	// Token: 0x04000389 RID: 905
	public Vector3 from = Vector3.one;

	// Token: 0x0400038A RID: 906
	public Vector3 to = Vector3.one;

	// Token: 0x0400038B RID: 907
	public bool updateTable;

	// Token: 0x0400038C RID: 908
	private Transform mTrans;

	// Token: 0x0400038D RID: 909
	private UITable mTable;
}
