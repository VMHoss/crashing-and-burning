using System;
using UnityEngine;

// Token: 0x02000021 RID: 33
[AddComponentMenu("NGUI/Interaction/Button Color")]
public class UIButtonColor : MonoBehaviour
{
	// Token: 0x1700001C RID: 28
	// (get) Token: 0x06000155 RID: 341 RVA: 0x0000930C File Offset: 0x0000750C
	// (set) Token: 0x06000156 RID: 342 RVA: 0x00009328 File Offset: 0x00007528
	public Color defaultColor
	{
		get
		{
			if (!this.mStarted)
			{
				this.Init();
			}
			return this.mColor;
		}
		set
		{
			this.mColor = value;
		}
	}

	// Token: 0x06000157 RID: 343 RVA: 0x00009334 File Offset: 0x00007534
	private void Start()
	{
		if (!this.mStarted)
		{
			this.Init();
			this.mStarted = true;
		}
	}

	// Token: 0x06000158 RID: 344 RVA: 0x00009350 File Offset: 0x00007550
	protected virtual void OnEnable()
	{
		if (this.mStarted && this.mHighlighted)
		{
			this.OnHover(UICamera.IsHighlighted(base.gameObject));
		}
	}

	// Token: 0x06000159 RID: 345 RVA: 0x0000937C File Offset: 0x0000757C
	private void OnDisable()
	{
		if (this.mStarted && this.tweenTarget != null)
		{
			TweenColor component = this.tweenTarget.GetComponent<TweenColor>();
			if (component != null)
			{
				component.color = this.mColor;
				component.enabled = false;
			}
		}
	}

	// Token: 0x0600015A RID: 346 RVA: 0x000093D0 File Offset: 0x000075D0
	protected void Init()
	{
		if (this.tweenTarget == null)
		{
			this.tweenTarget = base.gameObject;
		}
		UIWidget component = this.tweenTarget.GetComponent<UIWidget>();
		if (component != null)
		{
			this.mColor = component.color;
		}
		else
		{
			Renderer renderer = this.tweenTarget.renderer;
			if (renderer != null)
			{
				this.mColor = renderer.material.color;
			}
			else
			{
				Light light = this.tweenTarget.light;
				if (light != null)
				{
					this.mColor = light.color;
				}
				else
				{
					Debug.LogWarning(NGUITools.GetHierarchy(base.gameObject) + " has nothing for UIButtonColor to color", this);
					base.enabled = false;
				}
			}
		}
		this.OnEnable();
	}

	// Token: 0x0600015B RID: 347 RVA: 0x000094A4 File Offset: 0x000076A4
	public virtual void OnPress(bool isPressed)
	{
		if (base.enabled)
		{
			if (!this.mStarted)
			{
				this.Start();
			}
			TweenColor.Begin(this.tweenTarget, this.duration, (!isPressed) ? ((!UICamera.IsHighlighted(base.gameObject)) ? this.mColor : this.hover) : this.pressed);
		}
	}

	// Token: 0x0600015C RID: 348 RVA: 0x00009514 File Offset: 0x00007714
	public virtual void OnHover(bool isOver)
	{
		if (base.enabled)
		{
			if (!this.mStarted)
			{
				this.Start();
			}
			TweenColor.Begin(this.tweenTarget, this.duration, (!isOver) ? this.mColor : this.hover);
			this.mHighlighted = isOver;
		}
	}

	// Token: 0x04000188 RID: 392
	public GameObject tweenTarget;

	// Token: 0x04000189 RID: 393
	public Color hover = new Color(0.6f, 1f, 0.2f, 1f);

	// Token: 0x0400018A RID: 394
	public Color pressed = Color.grey;

	// Token: 0x0400018B RID: 395
	public float duration = 0.2f;

	// Token: 0x0400018C RID: 396
	protected Color mColor;

	// Token: 0x0400018D RID: 397
	protected bool mStarted;

	// Token: 0x0400018E RID: 398
	protected bool mHighlighted;
}
