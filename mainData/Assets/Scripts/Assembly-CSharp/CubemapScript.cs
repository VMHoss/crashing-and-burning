using System;
using UnityEngine;

// Token: 0x020000C0 RID: 192
[ExecuteInEditMode]
public class CubemapScript : MonoBehaviour
{
	// Token: 0x060005DF RID: 1503 RVA: 0x0002A4E0 File Offset: 0x000286E0
	public void Initialize(int aCullingMask, bool anOneFacePerFrame)
	{
		this.pTransform = base.transform;
		this.pCullingMask = aCullingMask;
		this.oneFacePerFrame = anOneFacePerFrame;
		this.UpdateCubemap(63);
	}

	// Token: 0x060005E0 RID: 1504 RVA: 0x0002A510 File Offset: 0x00028710
	private void LateUpdate()
	{
		if (this.oneFacePerFrame)
		{
			int num = Time.frameCount % 6;
			int aFaceMask = 1 << num;
			this.UpdateCubemap(aFaceMask);
		}
		else
		{
			this.UpdateCubemap(63);
		}
	}

	// Token: 0x060005E1 RID: 1505 RVA: 0x0002A54C File Offset: 0x0002874C
	private void UpdateCubemap(int aFaceMask)
	{
		if (!this.pCam)
		{
			this.pCam = new GameObject("CubemapCamera", new Type[]
			{
				typeof(Camera)
			})
			{
				hideFlags = HideFlags.HideAndDontSave,
				transform = 
				{
					position = base.transform.position,
					rotation = Quaternion.identity
				}
			}.camera;
			this.pCam.farClipPlane = 200f;
			this.pCam.enabled = false;
			this.pCam.cullingMask = this.pCullingMask;
		}
		if (!this.rtex)
		{
			this.rtex = new RenderTexture(this.cubemapSize, this.cubemapSize, 16);
			this.rtex.isCubemap = true;
			this.rtex.hideFlags = HideFlags.HideAndDontSave;
			base.renderer.sharedMaterial.SetTexture("_Cube", this.rtex);
		}
		this.pCam.transform.position = this.pTransform.position;
		this.pCam.RenderToCubemap(this.rtex, aFaceMask);
	}

	// Token: 0x060005E2 RID: 1506 RVA: 0x0002A67C File Offset: 0x0002887C
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(this.pCam);
		UnityEngine.Object.Destroy(this.rtex);
	}

	// Token: 0x0400066D RID: 1645
	public int cubemapSize = 256;

	// Token: 0x0400066E RID: 1646
	public bool oneFacePerFrame;

	// Token: 0x0400066F RID: 1647
	private Camera pCam;

	// Token: 0x04000670 RID: 1648
	public RenderTexture rtex;

	// Token: 0x04000671 RID: 1649
	private int pCullingMask;

	// Token: 0x04000672 RID: 1650
	private Transform pTransform;
}
