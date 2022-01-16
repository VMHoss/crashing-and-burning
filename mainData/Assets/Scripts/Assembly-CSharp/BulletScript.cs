using System;
using UnityEngine;

// Token: 0x02000111 RID: 273
public class BulletScript : LineProjectileScript
{
	// Token: 0x060007E7 RID: 2023 RVA: 0x0003BE3C File Offset: 0x0003A03C
	protected override void StartSpecific()
	{
		base.Invoke("ShowStreak", 0.02f);
	}

	// Token: 0x060007E8 RID: 2024 RVA: 0x0003BE50 File Offset: 0x0003A050
	protected override void HitSpecific(RaycastHit aRaycastHit)
	{
		aRaycastHit.collider.SendMessage("Damage", new DamageInfo(this.pProps["Damage"].f, aRaycastHit.point, base.transform.up), SendMessageOptions.DontRequireReceiver);
	}

	// Token: 0x060007E9 RID: 2025 RVA: 0x0003BE9C File Offset: 0x0003A09C
	private void ShowStreak()
	{
		base.transform.GetChild(0).renderer.enabled = true;
	}
}
