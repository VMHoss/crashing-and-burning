using System;
using AnimationOrTween;
using UnityEngine;

// Token: 0x0200002E RID: 46
[AddComponentMenu("NGUI/Interaction/Checkbox")]
public class UICheckbox : MonoBehaviour
{
	// Token: 0x1700001E RID: 30
	// (get) Token: 0x0600019C RID: 412 RVA: 0x0000ABD8 File Offset: 0x00008DD8
	// (set) Token: 0x0600019D RID: 413 RVA: 0x0000ABE0 File Offset: 0x00008DE0
	public bool isChecked
	{
		get
		{
			return this.mChecked;
		}
		set
		{
			if (this.radioButtonRoot == null || value || this.optionCanBeNone || !this.mStarted)
			{
				this.Set(value);
			}
		}
	}

	// Token: 0x0600019E RID: 414 RVA: 0x0000AC24 File Offset: 0x00008E24
	private void Awake()
	{
		this.mTrans = base.transform;
		if (this.checkSprite != null)
		{
			this.checkSprite.alpha = ((!this.startsChecked) ? 0f : 1f);
		}
		if (this.option)
		{
			this.option = false;
			if (this.radioButtonRoot == null)
			{
				this.radioButtonRoot = this.mTrans.parent;
			}
		}
	}

	// Token: 0x0600019F RID: 415 RVA: 0x0000ACA8 File Offset: 0x00008EA8
	private void Start()
	{
		if (this.eventReceiver == null)
		{
			this.eventReceiver = base.gameObject;
		}
		this.mChecked = !this.startsChecked;
		this.mStarted = true;
		this.Set(this.startsChecked);
	}

	// Token: 0x060001A0 RID: 416 RVA: 0x0000ACF4 File Offset: 0x00008EF4
	private void OnClick()
	{
		if (base.enabled)
		{
			this.isChecked = !this.isChecked;
		}
	}

	// Token: 0x060001A1 RID: 417 RVA: 0x0000AD10 File Offset: 0x00008F10
	private void Set(bool state)
	{
		if (!this.mStarted)
		{
			this.mChecked = state;
			this.startsChecked = state;
			if (this.checkSprite != null)
			{
				this.checkSprite.alpha = ((!state) ? 0f : 1f);
			}
		}
		else if (this.mChecked != state)
		{
			if (this.radioButtonRoot != null && state)
			{
				UICheckbox[] componentsInChildren = this.radioButtonRoot.GetComponentsInChildren<UICheckbox>(true);
				int i = 0;
				int num = componentsInChildren.Length;
				while (i < num)
				{
					UICheckbox uicheckbox = componentsInChildren[i];
					if (uicheckbox != this && uicheckbox.radioButtonRoot == this.radioButtonRoot)
					{
						uicheckbox.Set(false);
					}
					i++;
				}
			}
			this.mChecked = state;
			if (this.checkSprite != null)
			{
				if (this.instantTween)
				{
					this.checkSprite.alpha = ((!this.mChecked) ? 0f : 1f);
				}
				else
				{
					TweenAlpha.Begin(this.checkSprite.gameObject, 0.15f, (!this.mChecked) ? 0f : 1f);
				}
			}
			UICheckbox.current = this;
			if (this.onStateChange != null)
			{
				this.onStateChange(this.mChecked);
			}
			if (this.eventReceiver != null && !string.IsNullOrEmpty(this.functionName))
			{
				this.eventReceiver.SendMessage(this.functionName, this.mChecked, SendMessageOptions.DontRequireReceiver);
			}
			UICheckbox.current = null;
			if (this.checkAnimation != null)
			{
				ActiveAnimation.Play(this.checkAnimation, (!state) ? Direction.Reverse : Direction.Forward);
			}
		}
	}

	// Token: 0x040001E1 RID: 481
	public static UICheckbox current;

	// Token: 0x040001E2 RID: 482
	public UISprite checkSprite;

	// Token: 0x040001E3 RID: 483
	public Animation checkAnimation;

	// Token: 0x040001E4 RID: 484
	public bool instantTween;

	// Token: 0x040001E5 RID: 485
	public bool startsChecked = true;

	// Token: 0x040001E6 RID: 486
	public Transform radioButtonRoot;

	// Token: 0x040001E7 RID: 487
	public bool optionCanBeNone;

	// Token: 0x040001E8 RID: 488
	public GameObject eventReceiver;

	// Token: 0x040001E9 RID: 489
	public string functionName = "OnActivate";

	// Token: 0x040001EA RID: 490
	public UICheckbox.OnStateChange onStateChange;

	// Token: 0x040001EB RID: 491
	[HideInInspector]
	[SerializeField]
	private bool option;

	// Token: 0x040001EC RID: 492
	private bool mChecked = true;

	// Token: 0x040001ED RID: 493
	private bool mStarted;

	// Token: 0x040001EE RID: 494
	private Transform mTrans;

	// Token: 0x020001DF RID: 479
	// (Invoke) Token: 0x06000D99 RID: 3481
	public delegate void OnStateChange(bool state);
}
