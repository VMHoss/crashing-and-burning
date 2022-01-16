using System;
using UnityEngine;

// Token: 0x0200001F RID: 31
[AddComponentMenu("NGUI/Interaction/Button")]
public class UIButton : UIButtonColor
{
	// Token: 0x0600014C RID: 332 RVA: 0x0000916C File Offset: 0x0000736C
	protected override void OnEnable()
	{
		if (this.isEnabled)
		{
			base.OnEnable();
		}
		else
		{
			this.UpdateColor(false, true);
		}
	}

	// Token: 0x0600014D RID: 333 RVA: 0x0000918C File Offset: 0x0000738C
	public override void OnHover(bool isOver)
	{
		if (this.isEnabled)
		{
			base.OnHover(isOver);
		}
	}

	// Token: 0x0600014E RID: 334 RVA: 0x000091A0 File Offset: 0x000073A0
	public override void OnPress(bool isPressed)
	{
		if (this.isEnabled)
		{
			base.OnPress(isPressed);
		}
	}

	// Token: 0x1700001B RID: 27
	// (get) Token: 0x0600014F RID: 335 RVA: 0x000091B4 File Offset: 0x000073B4
	// (set) Token: 0x06000150 RID: 336 RVA: 0x000091DC File Offset: 0x000073DC
	public bool isEnabled
	{
		get
		{
			Collider collider = base.collider;
			return collider && collider.enabled;
		}
		set
		{
			Collider collider = base.collider;
			if (!collider)
			{
				return;
			}
			if (collider.enabled != value)
			{
				collider.enabled = value;
				this.UpdateColor(value, false);
			}
		}
	}

	// Token: 0x06000151 RID: 337 RVA: 0x00009218 File Offset: 0x00007418
	public void UpdateColor(bool shouldBeEnabled, bool immediate)
	{
		if (this.tweenTarget)
		{
			if (!this.mStarted)
			{
				this.mStarted = true;
				base.Init();
			}
			Color color = (!shouldBeEnabled) ? this.disabledColor : base.defaultColor;
			TweenColor tweenColor = TweenColor.Begin(this.tweenTarget, 0.15f, color);
			if (immediate)
			{
				tweenColor.color = color;
				tweenColor.enabled = false;
			}
		}
	}

	// Token: 0x04000185 RID: 389
	public Color disabledColor = Color.grey;
}
