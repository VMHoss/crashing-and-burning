using System;
using UnityEngine;

// Token: 0x02000140 RID: 320
public class PanzerTruckScript : CarScript
{
	// Token: 0x0600095B RID: 2395 RVA: 0x0004981C File Offset: 0x00047A1C
	protected override void StartSpecific()
	{
		this.pTrackMaterial = (UnityEngine.Object.Instantiate(Loader.LoadMaterial("Vehicles", "PanzerTruck/PanzerTruckTracks_Material")) as Material);
		Transform transform = base.transform.Find("PanzerTruckTracks");
		transform.gameObject.layer = GameData.carLayer;
		transform.renderer.sharedMaterial = this.pTrackMaterial;
	}

	// Token: 0x0600095C RID: 2396 RVA: 0x0004987C File Offset: 0x00047A7C
	protected override void UpdateSpecific()
	{
		if (!this.pDestroyed)
		{
			this.pTrackMatOffset.x = this.pTrackMatOffset.x - this.carData.currentSpeed * 0.12f * Time.deltaTime;
			if (this.pTrackMatOffset.x < 0f)
			{
				this.pTrackMatOffset.x = this.pTrackMatOffset.x + 1f;
			}
			if (this.pTrackMatOffset.x > 1f)
			{
				this.pTrackMatOffset.x = this.pTrackMatOffset.x - 1f;
			}
			this.pTrackMaterial.SetTextureOffset("_MainTex", this.pTrackMatOffset);
		}
	}

	// Token: 0x040009D7 RID: 2519
	private Vector2 pTrackMatOffset = Vector2.zero;

	// Token: 0x040009D8 RID: 2520
	private Material pTrackMaterial;

	// Token: 0x040009D9 RID: 2521
	private Transform pDrillHead;
}
