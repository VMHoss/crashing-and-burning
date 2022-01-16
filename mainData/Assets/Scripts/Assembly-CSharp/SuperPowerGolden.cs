using System;
using UnityEngine;

// Token: 0x02000129 RID: 297
public class SuperPowerGolden : SuperPowerBase
{
	// Token: 0x0600088D RID: 2189 RVA: 0x0003F264 File Offset: 0x0003D464
	public SuperPowerGolden(CarScript aCarScript) : base(aCarScript, "Golden")
	{
		this.pCarRimColor = GenericFunctionsScript.ColorFromList(this.pSuperPowerProps["CarRimColor"].l);
	}

	// Token: 0x0600088E RID: 2190 RVA: 0x0003F2A0 File Offset: 0x0003D4A0
	protected override void ActivateSpecific()
	{
		this.pCarScript.SetRimColor(this.pCarRimColor);
	}

	// Token: 0x0600088F RID: 2191 RVA: 0x0003F2B4 File Offset: 0x0003D4B4
	protected override void DeActivateSpecific(bool aDestroy)
	{
		this.pCarScript.ResetRimColor();
	}

	// Token: 0x040008C7 RID: 2247
	private Color pCarRimColor;
}
