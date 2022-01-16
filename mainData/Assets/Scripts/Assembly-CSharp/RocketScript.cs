using System;
using UnityEngine;

// Token: 0x02000117 RID: 279
public class RocketScript : LineProjectileScript
{
	// Token: 0x06000801 RID: 2049 RVA: 0x0003C9FC File Offset: 0x0003ABFC
	protected override void StartSpecific()
	{
		this.pTrailScript = TrailScript.AddTrail(base.gameObject, TrailScript.Normal.X);
		if (Data.platform != "PC")
		{
			UnityEngine.Object.Destroy(base.transform.GetChild(0).gameObject);
		}
	}

	// Token: 0x06000802 RID: 2050 RVA: 0x0003CA48 File Offset: 0x0003AC48
	protected override void HitSpecific(RaycastHit aRaycastHit)
	{
		aRaycastHit.collider.SendMessage("Damage", new DamageInfo(this.pProps["Damage"].f, aRaycastHit.point, base.transform.up), SendMessageOptions.DontRequireReceiver);
	}

	// Token: 0x06000803 RID: 2051 RVA: 0x0003CA94 File Offset: 0x0003AC94
	protected override void DestroySpecific()
	{
		UnityEngine.Object.Destroy(this.pTrailScript.gameObject, 1f);
	}

	// Token: 0x04000884 RID: 2180
	private TrailScript pTrailScript;
}
