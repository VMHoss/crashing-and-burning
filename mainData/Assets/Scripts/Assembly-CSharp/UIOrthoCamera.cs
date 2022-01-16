using System;
using UnityEngine;

// Token: 0x02000086 RID: 134
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/Orthographic Camera")]
[RequireComponent(typeof(Camera))]
public class UIOrthoCamera : MonoBehaviour
{
	// Token: 0x0600045A RID: 1114 RVA: 0x0001DFB0 File Offset: 0x0001C1B0
	private void Start()
	{
		this.mCam = base.camera;
		this.mTrans = base.transform;
		this.mCam.orthographic = true;
	}

	// Token: 0x0600045B RID: 1115 RVA: 0x0001DFE4 File Offset: 0x0001C1E4
	private void Update()
	{
		float num = this.mCam.rect.yMin * (float)Screen.height;
		float num2 = this.mCam.rect.yMax * (float)Screen.height;
		float num3 = (num2 - num) * 0.5f * this.mTrans.lossyScale.y;
		if (!Mathf.Approximately(this.mCam.orthographicSize, num3))
		{
			this.mCam.orthographicSize = num3;
		}
	}

	// Token: 0x04000477 RID: 1143
	private Camera mCam;

	// Token: 0x04000478 RID: 1144
	private Transform mTrans;
}
