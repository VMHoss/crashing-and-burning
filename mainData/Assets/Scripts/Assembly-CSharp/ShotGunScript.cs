using System;
using UnityEngine;

// Token: 0x02000147 RID: 327
public class ShotGunScript : WeaponScript
{
	// Token: 0x06000985 RID: 2437 RVA: 0x0004ADDC File Offset: 0x00048FDC
	public ShotGunScript(Transform aWeaponSlot, string aWeapon) : base(aWeaponSlot, aWeapon)
	{
		this.pBulletsPerShot = this.pWeaponProps["BulletsPerShot"].i;
		this.pMountedLeft = ((this.pWeaponGO.transform.parent.localPosition.x >= 0f) ? -1 : 1);
	}

	// Token: 0x06000986 RID: 2438 RVA: 0x0004AE50 File Offset: 0x00049050
	public override void FireDown(float anInheritedSpeed)
	{
		this.FireProjectile(anInheritedSpeed);
	}

	// Token: 0x06000987 RID: 2439 RVA: 0x0004AE5C File Offset: 0x0004905C
	public override void FireHold(float anInheritedSpeed)
	{
		if ((double)this.pFireTimer <= 0.0)
		{
			this.FireProjectile(anInheritedSpeed);
		}
	}

	// Token: 0x06000988 RID: 2440 RVA: 0x0004AE7C File Offset: 0x0004907C
	public override void FireUp(float anInheritedSpeed)
	{
	}

	// Token: 0x06000989 RID: 2441 RVA: 0x0004AE80 File Offset: 0x00049080
	public override void DetachSpecific()
	{
	}

	// Token: 0x0600098A RID: 2442 RVA: 0x0004AE84 File Offset: 0x00049084
	private void FireProjectile(float anInheritedSpeed)
	{
		this.pFireTimer = this.pFireCoolDown;
		base.PlayFireSound();
		GameObject gameObject = Loader.LoadGameObject("Shared", "Effects/" + this.pMuzzleFlash);
		gameObject.transform.parent = this.pFireDummy;
		gameObject.transform.localPosition = Vector3.zero;
		gameObject.transform.localRotation = Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 90f);
		UnityEngine.Object.Destroy(gameObject, gameObject.particleSystem.duration);
		for (int i = 0; i < this.pBulletsPerShot; i++)
		{
			Projectiles.CreateProjectile(this.pProjectile, this.pFireDummy.position, this.pFireDummy.rotation * Quaternion.Euler(UnityEngine.Random.Range(-3f, 3f), 0f, UnityEngine.Random.Range(0f, 2f) * (float)this.pMountedLeft), anInheritedSpeed);
		}
	}

	// Token: 0x04000A0A RID: 2570
	private int pBulletsPerShot = 1;

	// Token: 0x04000A0B RID: 2571
	private int pMountedLeft = 1;
}
