using System;
using UnityEngine;

// Token: 0x02000145 RID: 325
public class ProjectileWeaponScript : WeaponScript
{
	// Token: 0x06000977 RID: 2423 RVA: 0x0004A580 File Offset: 0x00048780
	public ProjectileWeaponScript(Transform aWeaponSlot, string aWeapon) : base(aWeaponSlot, aWeapon)
	{
	}

	// Token: 0x06000978 RID: 2424 RVA: 0x0004A58C File Offset: 0x0004878C
	public override void FireDown(float anInheritedSpeed)
	{
		this.FireProjectile(anInheritedSpeed);
	}

	// Token: 0x06000979 RID: 2425 RVA: 0x0004A598 File Offset: 0x00048798
	public override void FireHold(float anInheritedSpeed)
	{
		if ((double)this.pFireTimer <= 0.0)
		{
			this.FireProjectile(anInheritedSpeed);
		}
	}

	// Token: 0x0600097A RID: 2426 RVA: 0x0004A5B8 File Offset: 0x000487B8
	private void FireProjectile(float anInheritedSpeed)
	{
		this.pFireTimer = this.pFireCoolDown;
		base.PlayFireSound();
		if (this.pMuzzleFlashPS == null)
		{
			GameObject gameObject = Loader.LoadGameObject("Shared", "Effects/" + this.pMuzzleFlash);
			this.pMuzzleFlashPSXform = gameObject.transform;
			this.pMuzzleFlashPSXform.parent = this.pFireDummy;
			this.pMuzzleFlashPSXform.localPosition = Vector3.zero;
			this.pMuzzleFlashPS = gameObject.particleSystem;
		}
		this.pMuzzleFlashPSXform.localRotation = Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 90f);
		this.pMuzzleFlashPS.Emit(1);
		Projectiles.CreateProjectile(this.pProjectile, this.pFireDummy, anInheritedSpeed);
	}

	// Token: 0x040009F6 RID: 2550
	private ParticleSystem pMuzzleFlashPS;

	// Token: 0x040009F7 RID: 2551
	private Transform pMuzzleFlashPSXform;
}
