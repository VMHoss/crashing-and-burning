using System;
using UnityEngine;

// Token: 0x0200002A RID: 42
[AddComponentMenu("NGUI/Interaction/Button Sound")]
public class UIButtonSound : MonoBehaviour
{
	// Token: 0x06000188 RID: 392 RVA: 0x0000A380 File Offset: 0x00008580
	private void OnHover(bool isOver)
	{
		if (base.enabled && ((isOver && this.trigger == UIButtonSound.Trigger.OnMouseOver) || (!isOver && this.trigger == UIButtonSound.Trigger.OnMouseOut)))
		{
			NGUITools.PlaySound(this.audioClip, this.volume, this.pitch);
		}
	}

	// Token: 0x06000189 RID: 393 RVA: 0x0000A3D4 File Offset: 0x000085D4
	private void OnPress(bool isPressed)
	{
		if (base.enabled && ((isPressed && this.trigger == UIButtonSound.Trigger.OnPress) || (!isPressed && this.trigger == UIButtonSound.Trigger.OnRelease)))
		{
			NGUITools.PlaySound(this.audioClip, this.volume, this.pitch);
		}
	}

	// Token: 0x0600018A RID: 394 RVA: 0x0000A428 File Offset: 0x00008628
	private void OnClick()
	{
		if (base.enabled && this.trigger == UIButtonSound.Trigger.OnClick)
		{
			NGUITools.PlaySound(this.audioClip, this.volume, this.pitch);
		}
	}

	// Token: 0x040001C5 RID: 453
	public AudioClip audioClip;

	// Token: 0x040001C6 RID: 454
	public UIButtonSound.Trigger trigger;

	// Token: 0x040001C7 RID: 455
	public float volume = 1f;

	// Token: 0x040001C8 RID: 456
	public float pitch = 1f;

	// Token: 0x0200002B RID: 43
	public enum Trigger
	{
		// Token: 0x040001CA RID: 458
		OnClick,
		// Token: 0x040001CB RID: 459
		OnMouseOver,
		// Token: 0x040001CC RID: 460
		OnMouseOut,
		// Token: 0x040001CD RID: 461
		OnPress,
		// Token: 0x040001CE RID: 462
		OnRelease
	}
}
