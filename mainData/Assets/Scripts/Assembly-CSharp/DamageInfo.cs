using System;
using UnityEngine;

// Token: 0x020000E6 RID: 230
public class DamageInfo
{
	// Token: 0x060006FF RID: 1791 RVA: 0x00033344 File Offset: 0x00031544
	public DamageInfo(float aDamage)
	{
		this.damage = aDamage;
	}

	// Token: 0x06000700 RID: 1792 RVA: 0x00033354 File Offset: 0x00031554
	public DamageInfo(float aDamage, Vector3 aHitPos, Vector3 aHitDir)
	{
		this.damage = aDamage;
		this.hitPos = aHitPos;
		this.hitDir = aHitDir;
	}

	// Token: 0x04000728 RID: 1832
	public float damage;

	// Token: 0x04000729 RID: 1833
	public Vector3 hitPos;

	// Token: 0x0400072A RID: 1834
	public Vector3 hitDir;
}
