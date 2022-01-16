using System;
using UnityEngine;

// Token: 0x02000112 RID: 274
public class FuelAirRPGRocketScript : LineProjectileScript
{
	// Token: 0x060007EB RID: 2027 RVA: 0x0003BEE0 File Offset: 0x0003A0E0
	protected override void StartSpecific()
	{
		this.pTrailScript = TrailScript.AddTrail(base.gameObject, TrailScript.Normal.X);
		if (Data.platform != "PC")
		{
			UnityEngine.Object.Destroy(base.transform.GetChild(0).gameObject);
		}
		this.pDelayedExplosion = this.pProps["DelayedExplosion"].s;
		this.pDelayTime = this.pProps["DelayTime"].f;
	}

	// Token: 0x060007EC RID: 2028 RVA: 0x0003BF60 File Offset: 0x0003A160
	protected override void HitSpecific(RaycastHit aRaycastHit)
	{
		aRaycastHit.collider.SendMessage("Damage", new DamageInfo(this.pProps["Damage"].f, aRaycastHit.point, base.transform.up), SendMessageOptions.DontRequireReceiver);
		new GameObject("DelayedExplosion")
		{
			transform = 
			{
				position = aRaycastHit.point
			}
		}.AddComponent<DelayedExplosionScript>().Initialize(this.pDelayedExplosion, this.pDelayTime);
	}

	// Token: 0x060007ED RID: 2029 RVA: 0x0003BFE0 File Offset: 0x0003A1E0
	protected override void DestroySpecific()
	{
		UnityEngine.Object.Destroy(this.pTrailScript.gameObject, 1f);
	}

	// Token: 0x04000875 RID: 2165
	private TrailScript pTrailScript;

	// Token: 0x04000876 RID: 2166
	private string pDelayedExplosion = string.Empty;

	// Token: 0x04000877 RID: 2167
	private float pDelayTime = 1f;
}
