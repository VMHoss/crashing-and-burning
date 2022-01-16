using System;
using UnityEngine;

// Token: 0x020000F5 RID: 245
public class ItemJump : ItemBase
{
	// Token: 0x06000771 RID: 1905 RVA: 0x00038C64 File Offset: 0x00036E64
	public ItemJump(CarScript aCarScript) : base(aCarScript, "Jump")
	{
		this.pJumpVelocity = this.pItemProps["JumpVelocity"].f;
	}

	// Token: 0x06000772 RID: 1906 RVA: 0x00038CA4 File Offset: 0x00036EA4
	public override void UpdateSpecific()
	{
		if (this.pPound)
		{
			if (this.pCarScript.hovering && this.pAirTimer > 0.1f)
			{
				ExplosionScript.AddExplosion("JumpPound", this.pCarScript.transform.position);
				this.pPound = false;
			}
			else
			{
				this.pAirTimer += Time.deltaTime;
			}
		}
	}

	// Token: 0x06000773 RID: 1907 RVA: 0x00038D14 File Offset: 0x00036F14
	public override bool ActionSpecific()
	{
		if (this.pCarScript.hovering)
		{
			this.pRigidBody.AddForce(new Vector3(0f, this.pJumpVelocity, 0f), ForceMode.VelocityChange);
			this.pPound = true;
			this.pAirTimer = 0f;
			return true;
		}
		return false;
	}

	// Token: 0x040007F9 RID: 2041
	private float pJumpVelocity = 5f;

	// Token: 0x040007FA RID: 2042
	private bool pPound;

	// Token: 0x040007FB RID: 2043
	private float pAirTimer;
}
