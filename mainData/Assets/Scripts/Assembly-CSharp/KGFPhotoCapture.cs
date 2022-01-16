using System;
using System.Collections;
using UnityEngine;

// Token: 0x020001AE RID: 430
public class KGFPhotoCapture : MonoBehaviour
{
	// Token: 0x06000CCB RID: 3275 RVA: 0x0005F028 File Offset: 0x0005D228
	private void Start()
	{
		this.itsPhotoData = this.itsMapSystem.GetNextPhotoData();
		if (this.itsPhotoData != null)
		{
			base.transform.position = this.CalculateCameraPosition(this.itsPhotoData);
		}
	}

	// Token: 0x06000CCC RID: 3276 RVA: 0x0005F068 File Offset: 0x0005D268
	private Vector3 CalculateCameraPosition(KGFMapSystem.KGFPhotoData thePhotoData)
	{
		Vector3 itsPosition = thePhotoData.itsPosition;
		float num = thePhotoData.itsMeters / 2f;
		itsPosition.x += num;
		if (this.itsMapSystem.GetOrientation() == KGFMapSystem.KGFMapSystemOrientation.XZDefault)
		{
			itsPosition.z += num;
			itsPosition.y += base.camera.farClipPlane;
		}
		else
		{
			itsPosition.y += num;
			itsPosition.z -= base.camera.farClipPlane;
		}
		return itsPosition;
	}

	// Token: 0x06000CCD RID: 3277 RVA: 0x0005F100 File Offset: 0x0005D300
	private IEnumerator OnPostRender()
	{
		if (this.itsPhotoData != null)
		{
			this.itsPhotoData.itsTexture.ReadPixels(new Rect(0f, 0f, this.itsPhotoData.itsTextureSize, this.itsPhotoData.itsTextureSize), 0, 0);
			this.itsPhotoData.itsTexture.wrapMode = TextureWrapMode.Clamp;
			this.itsPhotoData.itsTexture.Apply();
			this.itsOldPhotoData = this.itsPhotoData;
			this.itsPhotoData = this.itsMapSystem.GetNextPhotoData();
			if (this.itsPhotoData != null)
			{
				base.transform.position = this.CalculateCameraPosition(this.itsPhotoData);
			}
			yield return new WaitForEndOfFrame();
			KGFMapSystem.KGFSetChildrenActiveRecursively(this.itsOldPhotoData.itsPhotoPlane, true);
		}
		if (this.itsPhotoData == null)
		{
			yield return new WaitForEndOfFrame();
			this.itsDestroy = true;
		}
		yield break;
	}

	// Token: 0x06000CCE RID: 3278 RVA: 0x0005F11C File Offset: 0x0005D31C
	private void Update()
	{
		if (this.itsDestroy)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04000C8D RID: 3213
	private KGFMapSystem.KGFPhotoData itsOldPhotoData;

	// Token: 0x04000C8E RID: 3214
	public KGFMapSystem itsMapSystem;

	// Token: 0x04000C8F RID: 3215
	private KGFMapSystem.KGFPhotoData itsPhotoData;

	// Token: 0x04000C90 RID: 3216
	private bool itsDestroy;
}
