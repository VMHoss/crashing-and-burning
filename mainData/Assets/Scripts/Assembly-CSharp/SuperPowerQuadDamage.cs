using System;
using UnityEngine;

// Token: 0x0200012A RID: 298
public class SuperPowerQuadDamage : SuperPowerBase
{
	// Token: 0x06000890 RID: 2192 RVA: 0x0003F2C4 File Offset: 0x0003D4C4
	public SuperPowerQuadDamage(CarScript aCarScript) : base(aCarScript, "QuadDamage")
	{
		this.pCarRimColor = GenericFunctionsScript.ColorFromList(this.pSuperPowerProps["CarRimColor"].l);
	}

	// Token: 0x06000891 RID: 2193 RVA: 0x0003F300 File Offset: 0x0003D500
	protected override void ActivateSpecific()
	{
		this.pCarScript.SetRimColor(this.pCarRimColor);
	}

	// Token: 0x06000892 RID: 2194 RVA: 0x0003F314 File Offset: 0x0003D514
	protected override void DeActivateSpecific(bool aDestroy)
	{
		this.pCarScript.ResetRimColor();
	}

	// Token: 0x040008C8 RID: 2248
	private Color pCarRimColor;
}
