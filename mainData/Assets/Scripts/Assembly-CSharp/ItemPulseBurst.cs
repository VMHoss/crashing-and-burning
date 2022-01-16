using System;
using UnityEngine;

// Token: 0x020000F7 RID: 247
public class ItemPulseBurst : ItemBase
{
	// Token: 0x06000778 RID: 1912 RVA: 0x00038E9C File Offset: 0x0003709C
	public ItemPulseBurst(CarScript aCarScript) : base(aCarScript, "PulseBurst")
	{
		if (Shop.GetUpgradeLevel("PulseBurstStrength") == 0)
		{
			this.pExplosionName = this.pItemProps["Explosion"].s;
		}
		else
		{
			this.pExplosionName = "PulseBurst" + Data.Shared["Upgrades"].d["PulseBurstStrength"].d["Upgrade" + Shop.GetUpgradeLevel("PulseBurstStrength")].i;
		}
	}

	// Token: 0x06000779 RID: 1913 RVA: 0x00038F4C File Offset: 0x0003714C
	public override bool ActionSpecific()
	{
		ExplosionScript.AddExplosion(this.pExplosionName, this.pCarScript.transform.position + new Vector3(0f, 0.2f, 0f));
		return true;
	}

	// Token: 0x040007FF RID: 2047
	private string pExplosionName = "FlameBurst";
}
