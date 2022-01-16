using System;
using UnityEngine;

// Token: 0x02000141 RID: 321
public class TankScript : CarScript
{
	// Token: 0x0600095E RID: 2398 RVA: 0x00049940 File Offset: 0x00047B40
	protected override void StartSpecific()
	{
		this.pTrackMaterial = (UnityEngine.Object.Instantiate(Loader.LoadMaterial("MilitaryCarPack", "Tank/TankTracks_Material")) as Material);
		base.transform.Find("TankTrackLeft").renderer.sharedMaterial = this.pTrackMaterial;
		base.transform.Find("TankTrackRight").renderer.sharedMaterial = this.pTrackMaterial;
		base.transform.Find("TankTurret").gameObject.SetLayerRecursively(GameData.carLayer);
	}

	// Token: 0x0600095F RID: 2399 RVA: 0x000499CC File Offset: 0x00047BCC
	protected override void UpdateSpecific()
	{
		if (!this.pDestroyed)
		{
			this.pTrackMatOffset.y = this.pTrackMatOffset.y + this.carData.currentSpeed * 0.36f * Time.deltaTime;
			if (this.pTrackMatOffset.y < 0f)
			{
				this.pTrackMatOffset.y = this.pTrackMatOffset.y + 1f;
			}
			if (this.pTrackMatOffset.y > 1f)
			{
				this.pTrackMatOffset.y = this.pTrackMatOffset.y - 1f;
			}
			this.pTrackMaterial.SetTextureOffset("_MainTex", this.pTrackMatOffset);
		}
	}

	// Token: 0x040009DA RID: 2522
	private Vector2 pTrackMatOffset = Vector2.zero;

	// Token: 0x040009DB RID: 2523
	private Material pTrackMaterial;
}
