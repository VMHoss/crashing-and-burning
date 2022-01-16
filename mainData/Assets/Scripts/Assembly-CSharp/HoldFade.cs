using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000A4 RID: 164
public class HoldFade : MonoBehaviour
{
	// Token: 0x06000526 RID: 1318 RVA: 0x00024728 File Offset: 0x00022928
	private void Start()
	{
		this.fadeTween = base.gameObject.GetComponent<TweenColor>();
		this.fadeTweenFromColor = this.fadeTween.from;
		this.fadeTweenToColor = this.fadeTween.to;
		this.fadeTweenTarget = this.fadeTween.eventReceiver;
		this.fadeTweenCall = this.fadeTween.callWhenFinished;
		this.holdTween = base.gameObject.AddComponent<TweenColor>();
		this.holdTween.duration = this.time;
		string text = this.type;
		if (text != null)
		{
			if (HoldFade.<>f__switch$map2 == null)
			{
				HoldFade.<>f__switch$map2 = new Dictionary<string, int>(2)
				{
					{
						"In",
						0
					},
					{
						"Out",
						1
					}
				};
			}
			int num;
			if (HoldFade.<>f__switch$map2.TryGetValue(text, out num))
			{
				if (num != 0)
				{
					if (num == 1)
					{
						this.holdTween.from = this.fadeTweenToColor;
						this.holdTween.to = this.fadeTweenToColor;
						this.holdTween.eventReceiver = this.fadeTweenTarget;
						this.holdTween.callWhenFinished = this.fadeTweenCall;
						this.holdTween.enabled = false;
						this.fadeTween.eventReceiver = base.gameObject;
						this.fadeTween.callWhenFinished = "FadeDone";
					}
				}
				else
				{
					this.holdTween.from = this.fadeTweenFromColor;
					this.holdTween.to = this.fadeTweenFromColor;
					this.holdTween.eventReceiver = base.gameObject;
					this.holdTween.callWhenFinished = "HoldDone";
					this.fadeTween.enabled = false;
				}
			}
		}
	}

	// Token: 0x06000527 RID: 1319 RVA: 0x000248D8 File Offset: 0x00022AD8
	public void HoldDone()
	{
		this.fadeTween.enabled = true;
	}

	// Token: 0x06000528 RID: 1320 RVA: 0x000248E8 File Offset: 0x00022AE8
	public void FadeDone()
	{
		this.holdTween.enabled = true;
		UnityEngine.Object.Destroy(base.gameObject, this.time + 0.1f);
	}

	// Token: 0x04000533 RID: 1331
	public string type = "In";

	// Token: 0x04000534 RID: 1332
	public float time = 1f;

	// Token: 0x04000535 RID: 1333
	public TweenColor fadeTween;

	// Token: 0x04000536 RID: 1334
	public Color fadeTweenFromColor;

	// Token: 0x04000537 RID: 1335
	public Color fadeTweenToColor;

	// Token: 0x04000538 RID: 1336
	public string fadeTweenCall;

	// Token: 0x04000539 RID: 1337
	public GameObject fadeTweenTarget;

	// Token: 0x0400053A RID: 1338
	public TweenColor holdTween;
}
