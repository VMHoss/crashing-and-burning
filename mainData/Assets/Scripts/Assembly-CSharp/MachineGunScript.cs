using System;
using UnityEngine;

// Token: 0x02000144 RID: 324
public class MachineGunScript : WeaponScript
{
	// Token: 0x06000971 RID: 2417 RVA: 0x0004A44C File Offset: 0x0004864C
	public MachineGunScript(Transform aWeaponSlot, string aWeapon) : base(aWeaponSlot, aWeapon)
	{
		this.pMinigunAudio = Scripts.audioManager.PlaySFX(this.pFireSound, 1f, -1);
		this.pMinigunAudio.enabled = false;
	}

	// Token: 0x06000972 RID: 2418 RVA: 0x0004A48C File Offset: 0x0004868C
	public override void FireDown(float anInheritedSpeed)
	{
		this.pMinigunAudio.enabled = true;
	}

	// Token: 0x06000973 RID: 2419 RVA: 0x0004A49C File Offset: 0x0004869C
	public override void FireHold(float anInheritedSpeed)
	{
		if ((double)this.pFireTimer <= 0.0)
		{
			this.FireProjectile(anInheritedSpeed);
		}
	}

	// Token: 0x06000974 RID: 2420 RVA: 0x0004A4BC File Offset: 0x000486BC
	public override void FireUp(float anInheritedSpeed)
	{
		this.pMinigunAudio.enabled = false;
	}

	// Token: 0x06000975 RID: 2421 RVA: 0x0004A4CC File Offset: 0x000486CC
	public override void DetachSpecific()
	{
		this.pMinigunAudio.enabled = false;
	}

	// Token: 0x06000976 RID: 2422 RVA: 0x0004A4DC File Offset: 0x000486DC
	private void FireProjectile(float anInheritedSpeed)
	{
		this.pFireTimer = this.pFireCoolDown;
		GameObject gameObject = Loader.LoadGameObject("Shared", "Effects/" + this.pMuzzleFlash);
		gameObject.transform.parent = this.pFireDummy;
		gameObject.transform.localPosition = Vector3.zero;
		gameObject.transform.localRotation = Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 90f);
		UnityEngine.Object.Destroy(gameObject, gameObject.particleSystem.duration);
		Projectiles.CreateProjectile(this.pProjectile, this.pFireDummy, anInheritedSpeed);
	}

	// Token: 0x040009F5 RID: 2549
	private AudioSource pMinigunAudio;
}
