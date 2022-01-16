using System;
using UnityEngine;

// Token: 0x02000098 RID: 152
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
[AddComponentMenu("NGUI/UI/Viewport Camera")]
public class UIViewport : MonoBehaviour
{
	// Token: 0x060004EF RID: 1263 RVA: 0x00023B4C File Offset: 0x00021D4C
	private void Start()
	{
		this.mCam = base.camera;
		if (this.sourceCamera == null)
		{
			this.sourceCamera = Camera.main;
		}
	}

	// Token: 0x060004F0 RID: 1264 RVA: 0x00023B84 File Offset: 0x00021D84
	private void LateUpdate()
	{
		if (this.topLeft != null && this.bottomRight != null)
		{
			Vector3 vector = this.sourceCamera.WorldToScreenPoint(this.topLeft.position);
			Vector3 vector2 = this.sourceCamera.WorldToScreenPoint(this.bottomRight.position);
			Rect rect = new Rect(vector.x / (float)Screen.width, vector2.y / (float)Screen.height, (vector2.x - vector.x) / (float)Screen.width, (vector.y - vector2.y) / (float)Screen.height);
			float num = this.fullSize * rect.height;
			if (rect != this.mCam.rect)
			{
				this.mCam.rect = rect;
			}
			if (this.mCam.orthographicSize != num)
			{
				this.mCam.orthographicSize = num;
			}
		}
	}

	// Token: 0x04000502 RID: 1282
	public Camera sourceCamera;

	// Token: 0x04000503 RID: 1283
	public Transform topLeft;

	// Token: 0x04000504 RID: 1284
	public Transform bottomRight;

	// Token: 0x04000505 RID: 1285
	public float fullSize = 1f;

	// Token: 0x04000506 RID: 1286
	private Camera mCam;
}
