using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000115 RID: 277
public class ProjectileScript : MonoBehaviour
{
	// Token: 0x060007F7 RID: 2039 RVA: 0x0003C51C File Offset: 0x0003A71C
	protected virtual void StartSpecific()
	{
	}

	// Token: 0x060007F8 RID: 2040 RVA: 0x0003C520 File Offset: 0x0003A720
	protected virtual void UpdateSpecific()
	{
	}

	// Token: 0x060007F9 RID: 2041 RVA: 0x0003C524 File Offset: 0x0003A724
	protected virtual void DestroySpecific()
	{
	}

	// Token: 0x060007FA RID: 2042 RVA: 0x0003C528 File Offset: 0x0003A728
	public void Initialize(string aProjectileName, Dictionary<string, DicEntry> aProps, float anInheritedSpeed)
	{
		this.pName = aProjectileName;
		this.pProps = aProps;
		this.pSpeed = this.pProps["Speed"].f + anInheritedSpeed;
		this.pRange = this.pProps["Range"].f;
		this.pDamage = this.pProps["Damage"].f;
		this.pImpactEffect = this.pProps["ImpactEffect"].s;
		this.pImpactSound = this.pProps["ImpactSound"].s;
		if (this.pName == "MountedGunBullet" || this.pName == "SamSiteRocket")
		{
			this.pCastMask = (1 << GameData.defaultLayer | 1 << GameData.carLayer);
		}
		else
		{
			this.pCastMask = (1 << GameData.defaultLayer | 1 << GameData.trafficLayer | 1 << GameData.damagedTrafficLayer | 1 << GameData.destructibleLayer);
		}
		this.StartSpecific();
	}

	// Token: 0x060007FB RID: 2043 RVA: 0x0003C650 File Offset: 0x0003A850
	private void Update()
	{
		if (Data.pause)
		{
			return;
		}
		this.UpdateSpecific();
	}

	// Token: 0x060007FC RID: 2044 RVA: 0x0003C664 File Offset: 0x0003A864
	protected void Explode()
	{
		if (this.pProps["ImpactExplosion"].s != "None")
		{
			ExplosionScript.AddExplosion(this.pProps["ImpactExplosion"].s, base.transform.position);
		}
		this.DestroySpecific();
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x04000879 RID: 2169
	private string pName;

	// Token: 0x0400087A RID: 2170
	protected Dictionary<string, DicEntry> pProps;

	// Token: 0x0400087B RID: 2171
	protected float pSpeed = 1f;

	// Token: 0x0400087C RID: 2172
	protected float pRange = 1f;

	// Token: 0x0400087D RID: 2173
	protected float pDamage = 1f;

	// Token: 0x0400087E RID: 2174
	protected string pImpactEffect = "None";

	// Token: 0x0400087F RID: 2175
	protected string pImpactSound = "None";

	// Token: 0x04000880 RID: 2176
	protected int pCastMask;

	// Token: 0x04000881 RID: 2177
	protected float pDistTravelled;
}
