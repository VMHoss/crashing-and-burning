using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000127 RID: 295
public class SuperPowerBase
{
	// Token: 0x0600087D RID: 2173 RVA: 0x0003EDA8 File Offset: 0x0003CFA8
	protected SuperPowerBase(CarScript aCarScript, string aSuperPowerName)
	{
		this.pCarScript = aCarScript;
		this.name = aSuperPowerName;
		this.pSuperPowerProps = Data.Shared["SuperPowers"].d[aSuperPowerName].d;
		if (this.pSuperPowerProps["Sound"].s != "None")
		{
			this.pSound = Scripts.audioManager.PlaySFX(this.pSuperPowerProps["Sound"].s, 1f, -1);
			this.pSound.Stop();
			this.pSound.enabled = false;
		}
	}

	// Token: 0x0600087E RID: 2174 RVA: 0x0003EE74 File Offset: 0x0003D074
	public void Activate()
	{
		this.Activate(-1f);
	}

	// Token: 0x0600087F RID: 2175 RVA: 0x0003EE84 File Offset: 0x0003D084
	public void Activate(float aDuration)
	{
		this.pActive = true;
		this.pDuration = aDuration;
		this.pSound.enabled = true;
		this.pSound.Play();
		this.ActivateSpecific();
	}

	// Token: 0x06000880 RID: 2176 RVA: 0x0003EEB4 File Offset: 0x0003D0B4
	public void DeActivate()
	{
		this.DeActivate(false);
	}

	// Token: 0x06000881 RID: 2177 RVA: 0x0003EEC0 File Offset: 0x0003D0C0
	public void DeActivate(bool aDestroy)
	{
		this.pActive = false;
		if (aDestroy)
		{
			UnityEngine.Object.Destroy(this.pSound);
		}
		else if (this.pSound.enabled)
		{
			this.pSound.Stop();
			this.pSound.enabled = false;
		}
		this.DeActivateSpecific(aDestroy);
	}

	// Token: 0x06000882 RID: 2178 RVA: 0x0003EF18 File Offset: 0x0003D118
	public bool IsActive()
	{
		return this.pActive;
	}

	// Token: 0x06000883 RID: 2179 RVA: 0x0003EF20 File Offset: 0x0003D120
	public void SetSound(bool anEnabled)
	{
		this.pSound.enabled = anEnabled;
		if (anEnabled)
		{
			this.pSound.Play();
		}
		else
		{
			this.pSound.Stop();
		}
	}

	// Token: 0x06000884 RID: 2180 RVA: 0x0003EF50 File Offset: 0x0003D150
	public bool Update()
	{
		if (this.pDuration >= 0f)
		{
			this.pDuration -= Time.deltaTime;
			if (this.pDuration < 0f)
			{
				return true;
			}
		}
		this.UpdateSpecific();
		return false;
	}

	// Token: 0x06000885 RID: 2181 RVA: 0x0003EF90 File Offset: 0x0003D190
	protected virtual void ActivateSpecific()
	{
	}

	// Token: 0x06000886 RID: 2182 RVA: 0x0003EF94 File Offset: 0x0003D194
	protected virtual void DeActivateSpecific(bool aDestroy)
	{
	}

	// Token: 0x06000887 RID: 2183 RVA: 0x0003EF98 File Offset: 0x0003D198
	protected virtual void UpdateSpecific()
	{
	}

	// Token: 0x06000888 RID: 2184 RVA: 0x0003EF9C File Offset: 0x0003D19C
	public virtual void FixedCarUpdate()
	{
	}

	// Token: 0x040008BD RID: 2237
	public readonly string name = string.Empty;

	// Token: 0x040008BE RID: 2238
	private float pDuration = -1f;

	// Token: 0x040008BF RID: 2239
	protected Dictionary<string, DicEntry> pSuperPowerProps;

	// Token: 0x040008C0 RID: 2240
	protected bool pActive = true;

	// Token: 0x040008C1 RID: 2241
	protected CarScript pCarScript;

	// Token: 0x040008C2 RID: 2242
	protected AudioSource pSound;
}
