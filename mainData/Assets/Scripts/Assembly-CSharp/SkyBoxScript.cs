using System;
using UnityEngine;

// Token: 0x020000DF RID: 223
[RequireComponent(typeof(Camera))]
public class SkyBoxScript : MonoBehaviour
{
	// Token: 0x060006C1 RID: 1729 RVA: 0x000307EC File Offset: 0x0002E9EC
	private void Awake()
	{
		base.gameObject.layer = LayerMask.NameToLayer("Skybox");
		base.camera.cullingMask = 1 << base.gameObject.layer;
		base.camera.depth = -2f;
		base.camera.nearClipPlane = 1f;
		base.camera.farClipPlane = 100f;
	}

	// Token: 0x060006C2 RID: 1730 RVA: 0x0003085C File Offset: 0x0002EA5C
	private void LateUpdate()
	{
		base.transform.rotation = this.pLinkedToCamera.transform.rotation;
		if (!Mathf.Approximately(base.camera.fieldOfView, this.pLinkedToCamera.fieldOfView))
		{
			base.camera.fieldOfView = this.pLinkedToCamera.fieldOfView;
		}
	}

	// Token: 0x060006C3 RID: 1731 RVA: 0x000308BC File Offset: 0x0002EABC
	public void LinkToCamera(Camera aCamera)
	{
		this.pLinkedToCamera = aCamera;
		base.camera.fieldOfView = aCamera.fieldOfView;
	}

	// Token: 0x040006EC RID: 1772
	private Camera pLinkedToCamera;
}
