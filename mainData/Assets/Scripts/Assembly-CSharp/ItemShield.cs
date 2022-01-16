using System;
using UnityEngine;

// Token: 0x020000F8 RID: 248
public class ItemShield : ItemBase
{
	// Token: 0x0600077A RID: 1914 RVA: 0x00038F90 File Offset: 0x00037190
	public ItemShield(CarScript aCarScript) : base(aCarScript, "Shield")
	{
		this.pShieldTimeConst = this.pItemProps["ShieldTime"].f;
		this.pForceField = Loader.LoadGameObject("Shared", "Effects/ForceField_PS");
		this.pForceField.transform.parent = this.pCarScript.transform;
		this.pForceField.transform.localPosition = new Vector3(0f, 0f, 0f);
		this.pForceField.transform.localRotation = Quaternion.identity;
		this.pForceField.transform.localScale = Vector3.zero;
		this.pShieldRadius = this.pCarScript.renderer.bounds.size.magnitude * 0.6f;
	}

	// Token: 0x0600077B RID: 1915 RVA: 0x00039090 File Offset: 0x00037290
	public override bool ActionSpecific()
	{
		this.pForceState = ItemShield.ForceState.IN;
		this.pForceInOutTimer = 0f;
		this.pCarScript.GrantImmunity(this.pShieldTimeConst);
		this.pShieldTimer = this.pShieldTimeConst;
		return true;
	}

	// Token: 0x0600077C RID: 1916 RVA: 0x000390D0 File Offset: 0x000372D0
	public override void UpdateSpecific()
	{
		if (this.pShieldTimer > 0f)
		{
			this.pShieldTimer -= Time.deltaTime;
		}
		switch (this.pForceState)
		{
		case ItemShield.ForceState.IN:
			this.pForceInOutTimer += Time.deltaTime;
			if (this.pForceInOutTimer > 0.5f)
			{
				this.pForceInOutTimer = 0.5f;
				this.pForceState = ItemShield.ForceState.ON;
			}
			this.pForceField.transform.localScale = Vector3.one * this.pForceInOutTimer * this.pShieldRadius * 2f;
			break;
		case ItemShield.ForceState.ON:
			if (this.pShieldTimer < 0.5f)
			{
				this.pForceState = ItemShield.ForceState.OUT;
			}
			break;
		case ItemShield.ForceState.OUT:
			this.pForceInOutTimer -= Time.deltaTime;
			if (this.pForceInOutTimer < 0f)
			{
				this.pForceInOutTimer = 0f;
				this.pForceState = ItemShield.ForceState.OFF;
			}
			this.pForceField.transform.localScale = Vector3.one * this.pForceInOutTimer * this.pShieldRadius * 2f;
			break;
		}
		if (this.pForceState != ItemShield.ForceState.OFF)
		{
			this.pForceField.transform.localRotation *= Quaternion.Euler(Vector3.one * 30f * Time.deltaTime);
		}
	}

	// Token: 0x04000800 RID: 2048
	private float pShieldTimeConst = 5f;

	// Token: 0x04000801 RID: 2049
	private ItemShield.ForceState pForceState;

	// Token: 0x04000802 RID: 2050
	private float pForceInOutTimer;

	// Token: 0x04000803 RID: 2051
	private GameObject pForceField;

	// Token: 0x04000804 RID: 2052
	private float pShieldTimer = 5f;

	// Token: 0x04000805 RID: 2053
	private float pShieldRadius = 10f;

	// Token: 0x020000F9 RID: 249
	private enum ForceState
	{
		// Token: 0x04000807 RID: 2055
		OFF,
		// Token: 0x04000808 RID: 2056
		IN,
		// Token: 0x04000809 RID: 2057
		ON,
		// Token: 0x0400080A RID: 2058
		OUT
	}
}
