using System;
using UnityEngine;

// Token: 0x02000019 RID: 25
public class VirtualScreen : MonoSingleton<VirtualScreen>
{
	// Token: 0x1400002E RID: 46
	// (add) Token: 0x06000107 RID: 263 RVA: 0x0000841C File Offset: 0x0000661C
	// (remove) Token: 0x06000108 RID: 264 RVA: 0x00008434 File Offset: 0x00006634
	public static event VirtualScreen.On_ScreenResizeHandler On_ScreenResize;

	// Token: 0x06000109 RID: 265 RVA: 0x0000844C File Offset: 0x0000664C
	private void Awake()
	{
		this.realWidth = (this.oldRealWidth = (float)Screen.width);
		this.realHeight = (this.oldRealHeight = (float)Screen.height);
		this.ComputeScreen();
	}

	// Token: 0x0600010A RID: 266 RVA: 0x0000848C File Offset: 0x0000668C
	private void Update()
	{
		this.realWidth = (float)Screen.width;
		this.realHeight = (float)Screen.height;
		if (this.realWidth != this.oldRealWidth || this.realHeight != this.oldRealHeight)
		{
			this.ComputeScreen();
			if (VirtualScreen.On_ScreenResize != null)
			{
				VirtualScreen.On_ScreenResize();
			}
		}
		this.oldRealWidth = this.realWidth;
		this.oldRealHeight = this.realHeight;
	}

	// Token: 0x0600010B RID: 267 RVA: 0x00008508 File Offset: 0x00006708
	public void ComputeScreen()
	{
		VirtualScreen.width = this.virtualWidth;
		VirtualScreen.height = this.virtualHeight;
		VirtualScreen.xRatio = 1f;
		VirtualScreen.yRatio = 1f;
		float num;
		float num2;
		if (Screen.width > Screen.height)
		{
			num = (float)Screen.width / (float)Screen.height;
			num2 = VirtualScreen.width;
		}
		else
		{
			num = (float)Screen.height / (float)Screen.width;
			num2 = VirtualScreen.height;
		}
		float num3 = num2 / num;
		if (Screen.width > Screen.height)
		{
			VirtualScreen.height = num3;
			VirtualScreen.xRatio = (float)Screen.width / VirtualScreen.width;
			VirtualScreen.yRatio = (float)Screen.height / VirtualScreen.height;
		}
		else
		{
			VirtualScreen.width = num3;
			VirtualScreen.xRatio = (float)Screen.width / VirtualScreen.width;
			VirtualScreen.yRatio = (float)Screen.height / VirtualScreen.height;
		}
	}

	// Token: 0x0600010C RID: 268 RVA: 0x000085F8 File Offset: 0x000067F8
	public static void ComputeVirtualScreen()
	{
		MonoSingleton<VirtualScreen>.instance.ComputeScreen();
	}

	// Token: 0x0600010D RID: 269 RVA: 0x00008604 File Offset: 0x00006804
	public static void SetGuiScaleMatrix()
	{
		GUI.matrix = Matrix4x4.Scale(new Vector3(VirtualScreen.xRatio, VirtualScreen.yRatio, 1f));
	}

	// Token: 0x0600010E RID: 270 RVA: 0x00008624 File Offset: 0x00006824
	public static Rect GetRealRect(Rect rect)
	{
		return new Rect(rect.x * VirtualScreen.xRatio, rect.y * VirtualScreen.yRatio, rect.width * VirtualScreen.xRatio, rect.height * VirtualScreen.yRatio);
	}

	// Token: 0x04000172 RID: 370
	public float virtualWidth = 1024f;

	// Token: 0x04000173 RID: 371
	public float virtualHeight = 768f;

	// Token: 0x04000174 RID: 372
	public static float width = 1024f;

	// Token: 0x04000175 RID: 373
	public static float height = 768f;

	// Token: 0x04000176 RID: 374
	public static float xRatio = 1f;

	// Token: 0x04000177 RID: 375
	public static float yRatio = 1f;

	// Token: 0x04000178 RID: 376
	private float realWidth;

	// Token: 0x04000179 RID: 377
	private float realHeight;

	// Token: 0x0400017A RID: 378
	private float oldRealWidth;

	// Token: 0x0400017B RID: 379
	private float oldRealHeight;

	// Token: 0x0200001A RID: 26
	public enum ScreenResolution
	{
		// Token: 0x0400017E RID: 382
		IPhoneTall,
		// Token: 0x0400017F RID: 383
		IPhoneWide,
		// Token: 0x04000180 RID: 384
		IPhone4GTall,
		// Token: 0x04000181 RID: 385
		IPhone4GWide,
		// Token: 0x04000182 RID: 386
		IPadTall,
		// Token: 0x04000183 RID: 387
		IPadWide
	}

	// Token: 0x020001DE RID: 478
	// (Invoke) Token: 0x06000D95 RID: 3477
	public delegate void On_ScreenResizeHandler();
}
