using System;

// Token: 0x020000F4 RID: 244
public class ItemCarBomb : ItemBase
{
	// Token: 0x0600076F RID: 1903 RVA: 0x00038BA8 File Offset: 0x00036DA8
	public ItemCarBomb(CarScript aCarScript) : base(aCarScript, "CarBomb")
	{
	}

	// Token: 0x06000770 RID: 1904 RVA: 0x00038BB8 File Offset: 0x00036DB8
	public override bool ActionSpecific()
	{
		this.pCarScript.ExplodeCar(true);
		int upgradeLevel = Shop.GetUpgradeLevel("CarBombStrength");
		string anExplosionName;
		if (upgradeLevel == 0)
		{
			anExplosionName = this.pItemProps["Explosion"].s;
		}
		else
		{
			anExplosionName = "CarBomb" + Data.Shared["Upgrades"].d["CarBombStrength"].d["Upgrade" + upgradeLevel].i;
		}
		ExplosionScript.AddExplosion(anExplosionName, this.pCarScript.transform.position);
		return true;
	}
}
