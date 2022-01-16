using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000F2 RID: 242
public class ItemBase
{
	// Token: 0x06000760 RID: 1888 RVA: 0x000386FC File Offset: 0x000368FC
	protected ItemBase(CarScript aCarScript, string anItemName)
	{
		this.name = anItemName;
		this.pItemProps = Data.Shared["Gadgets"].d[anItemName].d;
		this.pCarScript = aCarScript;
		this.pRigidBody = this.pCarScript.rigidbody;
		this.pCoolDown = this.pItemProps["CoolDown"].f;
		this.pActionSoundName = this.pItemProps["ActionSound"].s;
	}

	// Token: 0x06000761 RID: 1889 RVA: 0x000387AC File Offset: 0x000369AC
	public void UpdateGeneric()
	{
		if (this.pCoolDownTimer > 0f)
		{
			this.pCoolDownTimer -= Time.deltaTime;
		}
		this.UpdateSpecific();
	}

	// Token: 0x06000762 RID: 1890 RVA: 0x000387E4 File Offset: 0x000369E4
	public void ActionGeneric()
	{
		if ((double)this.pCoolDownTimer > 0.0)
		{
			return;
		}
		if (this.ActionSpecific())
		{
			this.pCoolDownTimer = this.pCoolDown;
			if (this.pActionSoundName != "None")
			{
				Scripts.audioManager.PlaySFX(this.pActionSoundName);
			}
		}
	}

	// Token: 0x06000763 RID: 1891 RVA: 0x00038844 File Offset: 0x00036A44
	public virtual void StartSpecific()
	{
	}

	// Token: 0x06000764 RID: 1892 RVA: 0x00038848 File Offset: 0x00036A48
	public virtual void UpdateSpecific()
	{
	}

	// Token: 0x06000765 RID: 1893 RVA: 0x0003884C File Offset: 0x00036A4C
	public virtual bool ActionSpecific()
	{
		return false;
	}

	// Token: 0x06000766 RID: 1894 RVA: 0x00038850 File Offset: 0x00036A50
	public virtual void ExplodeCarSpecific()
	{
	}

	// Token: 0x06000767 RID: 1895 RVA: 0x00038854 File Offset: 0x00036A54
	public virtual void FixedUpdateSpecific()
	{
	}

	// Token: 0x040007EA RID: 2026
	public string name;

	// Token: 0x040007EB RID: 2027
	protected Dictionary<string, DicEntry> pItemProps;

	// Token: 0x040007EC RID: 2028
	protected CarScript pCarScript;

	// Token: 0x040007ED RID: 2029
	protected Rigidbody pRigidBody;

	// Token: 0x040007EE RID: 2030
	protected float pCoolDownTimer = -1f;

	// Token: 0x040007EF RID: 2031
	private readonly float pCoolDown = 1f;

	// Token: 0x040007F0 RID: 2032
	private readonly string pActionSoundName = "None";
}
