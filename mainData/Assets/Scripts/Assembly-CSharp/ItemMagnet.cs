using System;
using UnityEngine;

// Token: 0x020000F6 RID: 246
public class ItemMagnet : ItemBase
{
	// Token: 0x06000774 RID: 1908 RVA: 0x00038D68 File Offset: 0x00036F68
	public ItemMagnet(CarScript aCarScript) : base(aCarScript, "Magnet")
	{
		this.pAttractTimeConst = this.pItemProps["AttractTime"].f;
	}

	// Token: 0x06000775 RID: 1909 RVA: 0x00038DA8 File Offset: 0x00036FA8
	public override void StartSpecific()
	{
		this.pMagnetEffect = Loader.LoadGameObject("Shared", "Effects/Magnet_PS");
		this.pMagnetEffect.transform.parent = this.pCarScript.transform;
		this.pMagnetEffect.transform.localPosition = Vector3.zero;
		this.pMagnetEffect.transform.localRotation = Quaternion.identity;
		this.pMagnetEffect.SetActive(false);
	}

	// Token: 0x06000776 RID: 1910 RVA: 0x00038E1C File Offset: 0x0003701C
	public override void UpdateSpecific()
	{
		if (this.pAttractTimer > 0f)
		{
			this.pAttractTimer -= Time.deltaTime;
			if (this.pAttractTimer <= 0f)
			{
				this.pCarScript.cashMagnet = false;
				this.pMagnetEffect.SetActive(false);
			}
		}
	}

	// Token: 0x06000777 RID: 1911 RVA: 0x00038E74 File Offset: 0x00037074
	public override bool ActionSpecific()
	{
		this.pAttractTimer = this.pAttractTimeConst;
		this.pCarScript.cashMagnet = true;
		this.pMagnetEffect.SetActive(true);
		return true;
	}

	// Token: 0x040007FC RID: 2044
	private float pAttractTimeConst = 2f;

	// Token: 0x040007FD RID: 2045
	private float pAttractTimer = -1f;

	// Token: 0x040007FE RID: 2046
	private GameObject pMagnetEffect;
}
