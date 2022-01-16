using System;
using UnityEngine;

// Token: 0x0200003C RID: 60
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/Image Button")]
public class UIImageButton : MonoBehaviour
{
	// Token: 0x17000026 RID: 38
	// (get) Token: 0x060001ED RID: 493 RVA: 0x0000D534 File Offset: 0x0000B734
	// (set) Token: 0x060001EE RID: 494 RVA: 0x0000D55C File Offset: 0x0000B75C
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
				this.UpdateImage();
			}
		}
	}

	// Token: 0x060001EF RID: 495 RVA: 0x0000D598 File Offset: 0x0000B798
	private void Awake()
	{
		if (this.target == null)
		{
			this.target = base.GetComponentInChildren<UISprite>();
		}
	}

	// Token: 0x060001F0 RID: 496 RVA: 0x0000D5B8 File Offset: 0x0000B7B8
	private void OnEnable()
	{
		this.UpdateImage();
	}

	// Token: 0x060001F1 RID: 497 RVA: 0x0000D5C0 File Offset: 0x0000B7C0
	private void UpdateImage()
	{
		if (this.target != null)
		{
			if (this.isEnabled)
			{
				this.target.spriteName = ((!UICamera.IsHighlighted(base.gameObject)) ? this.normalSprite : this.hoverSprite);
			}
			else
			{
				this.target.spriteName = this.disabledSprite;
			}
			this.target.MakePixelPerfect();
		}
	}

	// Token: 0x060001F2 RID: 498 RVA: 0x0000D638 File Offset: 0x0000B838
	private void OnHover(bool isOver)
	{
		if (base.enabled && this.target != null)
		{
			this.target.spriteName = ((!isOver) ? this.normalSprite : this.hoverSprite);
			this.target.MakePixelPerfect();
		}
	}

	// Token: 0x060001F3 RID: 499 RVA: 0x0000D690 File Offset: 0x0000B890
	private void OnPress(bool pressed)
	{
		if (pressed)
		{
			this.target.spriteName = this.pressedSprite;
			this.target.MakePixelPerfect();
		}
		else
		{
			this.UpdateImage();
		}
	}

	// Token: 0x04000251 RID: 593
	public UISprite target;

	// Token: 0x04000252 RID: 594
	public string normalSprite;

	// Token: 0x04000253 RID: 595
	public string hoverSprite;

	// Token: 0x04000254 RID: 596
	public string pressedSprite;

	// Token: 0x04000255 RID: 597
	public string disabledSprite;
}
