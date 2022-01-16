using System;
using UnityEngine;

// Token: 0x0200013E RID: 318
public class DrillScript : CarScript
{
	// Token: 0x06000957 RID: 2391 RVA: 0x00049684 File Offset: 0x00047884
	protected override void StartSpecific()
	{
		this.pTrackMaterial = (UnityEngine.Object.Instantiate(Loader.LoadMaterial("BossCarPack", "Drill/DrillTracks_Material")) as Material);
		base.transform.Find("DrillTracks").renderer.sharedMaterial = this.pTrackMaterial;
		this.pDrillHeadPivotFix = base.transform.Find("DrillHeadPivotFix");
		this.pDrillHeadPivotFix.gameObject.SetLayerRecursively(GameData.carLayer);
		this.pDrillHeadRotation = this.pDrillHeadPivotFix.localRotation;
	}

	// Token: 0x06000958 RID: 2392 RVA: 0x0004970C File Offset: 0x0004790C
	protected override void UpdateSpecific()
	{
		if (!this.pDestroyed)
		{
			this.pTrackMatOffset.x = this.pTrackMatOffset.x - this.carData.currentSpeed * 0.15f * Time.deltaTime;
			if (this.pTrackMatOffset.x < 0f)
			{
				this.pTrackMatOffset.x = this.pTrackMatOffset.x + 1f;
			}
			if (this.pTrackMatOffset.x > 1f)
			{
				this.pTrackMatOffset.x = this.pTrackMatOffset.x - 1f;
			}
			this.pTrackMaterial.SetTextureOffset("_MainTex", this.pTrackMatOffset);
			this.pDrillRotationAngle += Time.deltaTime * 720f;
			this.pDrillHeadPivotFix.localRotation = this.pDrillHeadRotation * Quaternion.Euler(0f, this.pDrillRotationAngle, 0f);
		}
	}

	// Token: 0x040009D2 RID: 2514
	private Vector2 pTrackMatOffset = Vector2.zero;

	// Token: 0x040009D3 RID: 2515
	private Material pTrackMaterial;

	// Token: 0x040009D4 RID: 2516
	private Transform pDrillHeadPivotFix;

	// Token: 0x040009D5 RID: 2517
	private Quaternion pDrillHeadRotation;

	// Token: 0x040009D6 RID: 2518
	private float pDrillRotationAngle;
}
