using System;
using AnimationOrTween;
using UnityEngine;

// Token: 0x0200004A RID: 74
[AddComponentMenu("NGUI/Internal/Active Animation")]
[RequireComponent(typeof(Animation))]
public class ActiveAnimation : IgnoreTimeScale
{
	// Token: 0x17000037 RID: 55
	// (get) Token: 0x0600024A RID: 586 RVA: 0x0001034C File Offset: 0x0000E54C
	public bool isPlaying
	{
		get
		{
			if (this.mAnim == null)
			{
				return false;
			}
			foreach (object obj in this.mAnim)
			{
				AnimationState animationState = (AnimationState)obj;
				if (this.mAnim.IsPlaying(animationState.name))
				{
					if (this.mLastDirection == Direction.Forward)
					{
						if (animationState.time < animationState.length)
						{
							return true;
						}
					}
					else
					{
						if (this.mLastDirection != Direction.Reverse)
						{
							return true;
						}
						if (animationState.time > 0f)
						{
							return true;
						}
					}
				}
			}
			return false;
		}
	}

	// Token: 0x0600024B RID: 587 RVA: 0x00010440 File Offset: 0x0000E640
	public void Reset()
	{
		if (this.mAnim != null)
		{
			foreach (object obj in this.mAnim)
			{
				AnimationState animationState = (AnimationState)obj;
				if (this.mLastDirection == Direction.Reverse)
				{
					animationState.time = animationState.length;
				}
				else if (this.mLastDirection == Direction.Forward)
				{
					animationState.time = 0f;
				}
			}
		}
	}

	// Token: 0x0600024C RID: 588 RVA: 0x000104F0 File Offset: 0x0000E6F0
	private void Update()
	{
		float num = base.UpdateRealTimeDelta();
		if (num == 0f)
		{
			return;
		}
		if (this.mAnim != null)
		{
			bool flag = false;
			foreach (object obj in this.mAnim)
			{
				AnimationState animationState = (AnimationState)obj;
				if (this.mAnim.IsPlaying(animationState.name))
				{
					float num2 = animationState.speed * num;
					animationState.time += num2;
					if (num2 < 0f)
					{
						if (animationState.time > 0f)
						{
							flag = true;
						}
						else
						{
							animationState.time = 0f;
						}
					}
					else if (animationState.time < animationState.length)
					{
						flag = true;
					}
					else
					{
						animationState.time = animationState.length;
					}
				}
			}
			this.mAnim.Sample();
			if (flag)
			{
				return;
			}
			base.enabled = false;
			if (this.mNotify)
			{
				this.mNotify = false;
				if (this.onFinished != null)
				{
					this.onFinished(this);
				}
				if (this.eventReceiver != null && !string.IsNullOrEmpty(this.callWhenFinished))
				{
					this.eventReceiver.SendMessage(this.callWhenFinished, this, SendMessageOptions.DontRequireReceiver);
				}
				if (this.mDisableDirection != Direction.Toggle && this.mLastDirection == this.mDisableDirection)
				{
					NGUITools.SetActive(base.gameObject, false);
				}
			}
		}
		else
		{
			base.enabled = false;
		}
	}

	// Token: 0x0600024D RID: 589 RVA: 0x000106B8 File Offset: 0x0000E8B8
	private void Play(string clipName, Direction playDirection)
	{
		if (this.mAnim != null)
		{
			base.enabled = true;
			this.mAnim.enabled = false;
			if (playDirection == Direction.Toggle)
			{
				playDirection = ((this.mLastDirection == Direction.Forward) ? Direction.Reverse : Direction.Forward);
			}
			bool flag = string.IsNullOrEmpty(clipName);
			if (flag)
			{
				if (!this.mAnim.isPlaying)
				{
					this.mAnim.Play();
				}
			}
			else if (!this.mAnim.IsPlaying(clipName))
			{
				this.mAnim.Play(clipName);
			}
			foreach (object obj in this.mAnim)
			{
				AnimationState animationState = (AnimationState)obj;
				if (string.IsNullOrEmpty(clipName) || animationState.name == clipName)
				{
					float num = Mathf.Abs(animationState.speed);
					animationState.speed = num * (float)playDirection;
					if (playDirection == Direction.Reverse && animationState.time == 0f)
					{
						animationState.time = animationState.length;
					}
					else if (playDirection == Direction.Forward && animationState.time == animationState.length)
					{
						animationState.time = 0f;
					}
				}
			}
			this.mLastDirection = playDirection;
			this.mNotify = true;
			this.mAnim.Sample();
		}
	}

	// Token: 0x0600024E RID: 590 RVA: 0x00010844 File Offset: 0x0000EA44
	public static ActiveAnimation Play(Animation anim, string clipName, Direction playDirection, EnableCondition enableBeforePlay, DisableCondition disableCondition)
	{
		if (!NGUITools.GetActive(anim.gameObject))
		{
			if (enableBeforePlay != EnableCondition.EnableThenPlay)
			{
				return null;
			}
			NGUITools.SetActive(anim.gameObject, true);
			UIPanel[] componentsInChildren = anim.gameObject.GetComponentsInChildren<UIPanel>();
			int i = 0;
			int num = componentsInChildren.Length;
			while (i < num)
			{
				componentsInChildren[i].Refresh();
				i++;
			}
		}
		ActiveAnimation activeAnimation = anim.GetComponent<ActiveAnimation>();
		if (activeAnimation == null)
		{
			activeAnimation = anim.gameObject.AddComponent<ActiveAnimation>();
		}
		activeAnimation.mAnim = anim;
		activeAnimation.mDisableDirection = (Direction)disableCondition;
		activeAnimation.eventReceiver = null;
		activeAnimation.callWhenFinished = null;
		activeAnimation.onFinished = null;
		activeAnimation.Play(clipName, playDirection);
		return activeAnimation;
	}

	// Token: 0x0600024F RID: 591 RVA: 0x000108EC File Offset: 0x0000EAEC
	public static ActiveAnimation Play(Animation anim, string clipName, Direction playDirection)
	{
		return ActiveAnimation.Play(anim, clipName, playDirection, EnableCondition.DoNothing, DisableCondition.DoNotDisable);
	}

	// Token: 0x06000250 RID: 592 RVA: 0x000108F8 File Offset: 0x0000EAF8
	public static ActiveAnimation Play(Animation anim, Direction playDirection)
	{
		return ActiveAnimation.Play(anim, null, playDirection, EnableCondition.DoNothing, DisableCondition.DoNotDisable);
	}

	// Token: 0x040002B6 RID: 694
	public ActiveAnimation.OnFinished onFinished;

	// Token: 0x040002B7 RID: 695
	public GameObject eventReceiver;

	// Token: 0x040002B8 RID: 696
	public string callWhenFinished;

	// Token: 0x040002B9 RID: 697
	private Animation mAnim;

	// Token: 0x040002BA RID: 698
	private Direction mLastDirection;

	// Token: 0x040002BB RID: 699
	private Direction mDisableDirection;

	// Token: 0x040002BC RID: 700
	private bool mNotify;

	// Token: 0x020001E6 RID: 486
	// (Invoke) Token: 0x06000DB5 RID: 3509
	public delegate void OnFinished(ActiveAnimation anim);
}
