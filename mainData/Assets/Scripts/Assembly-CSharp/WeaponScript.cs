using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000148 RID: 328
public class WeaponScript
{
	// Token: 0x0600098B RID: 2443 RVA: 0x0004AF88 File Offset: 0x00049188
	protected WeaponScript(Transform aWeaponSlot, string aWeapon)
	{
		this.pWeaponProps = Data.Shared["Weapons"].d[aWeapon].d;
		this.pFireCoolDown = this.pWeaponProps["CoolDownTime"].f;
		this.pProjectile = this.pWeaponProps["Projectile"].s;
		this.pFireSound = this.pWeaponProps["FireSound"].s;
		this.pOverrideFireSound = this.pWeaponProps["OverrideFireSound"].b;
		this.pMuzzleFlash = this.pWeaponProps["MuzzleFlash"].s;
		this.pWeaponGO = Loader.LoadGameObject("Shared", "Weapons/" + this.pWeaponProps["Prefab"].s);
		this.pWeaponGO.transform.parent = aWeaponSlot;
		this.pWeaponGO.transform.localPosition = Vector3.zero;
		this.pWeaponGO.transform.localRotation = Quaternion.Euler(90f, 270f, 0f);
		this.pWeaponGO.layer = GameData.carPartLayer;
		this.pFireDummy = this.pWeaponGO.transform.GetChild(0);
	}

	// Token: 0x0600098C RID: 2444 RVA: 0x0004B108 File Offset: 0x00049308
	public static WeaponScript CreateWeapon(Transform aWeaponSlot, string aWeapon)
	{
		WeaponScript result = null;
		switch (aWeapon)
		{
		case "GrenadeLauncher":
		case "RocketLauncher":
		case "StrikerLauncher":
		case "MissileLauncher":
		case "HeavyMachineGun":
		case "FuelAirRPGs":
		case "TankMainGun":
			return new ProjectileWeaponScript(aWeaponSlot, aWeapon);
		case "FlameThrower":
			return new FlameThrowerScript(aWeaponSlot, aWeapon);
		case "Laser":
			return new LaserScript(aWeaponSlot, aWeapon);
		case "MachineGun":
			return new MachineGunScript(aWeaponSlot, aWeapon);
		case "ShotGun":
			return new ShotGunScript(aWeaponSlot, aWeapon);
		case "RailGun":
			return new RailGunScript(aWeaponSlot, aWeapon);
		}
		Debug.LogError("Weapon " + aWeapon + " not implemented.");
		return result;
	}

	// Token: 0x0600098D RID: 2445 RVA: 0x0004B268 File Offset: 0x00049468
	public virtual void FireDown(float anInheritedSpeed)
	{
	}

	// Token: 0x0600098E RID: 2446 RVA: 0x0004B26C File Offset: 0x0004946C
	public virtual void FireHold(float anInheritedSpeed)
	{
	}

	// Token: 0x0600098F RID: 2447 RVA: 0x0004B270 File Offset: 0x00049470
	public virtual void FireUp(float anInheritedSpeed)
	{
	}

	// Token: 0x06000990 RID: 2448 RVA: 0x0004B274 File Offset: 0x00049474
	public virtual void UpdateSpecific()
	{
	}

	// Token: 0x06000991 RID: 2449 RVA: 0x0004B278 File Offset: 0x00049478
	public virtual void DetachSpecific()
	{
	}

	// Token: 0x06000992 RID: 2450 RVA: 0x0004B27C File Offset: 0x0004947C
	protected void PlayFireSound()
	{
		if (this.pFireSound != "None")
		{
			if (this.pOverrideFireSound && this.pFireSoundAudio != null)
			{
				UnityEngine.Object.DestroyImmediate(this.pFireSoundAudio);
			}
			this.pFireSoundAudio = Scripts.audioManager.PlaySFX(this.pFireSound);
		}
	}

	// Token: 0x06000993 RID: 2451 RVA: 0x0004B2DC File Offset: 0x000494DC
	public void Detach()
	{
		this.pWeaponGO.transform.parent = null;
		this.pWeaponGO.layer = GameData.carPartLayer;
		this.pWeaponGO.AddComponent<BoxCollider>();
		this.pWeaponGO.AddComponent<Rigidbody>();
		this.pWeaponGO.rigidbody.velocity = (this.pWeaponGO.transform.position - GameData.playerCarScript.transform.position) * 10f;
		this.pWeaponGO.rigidbody.angularVelocity = UnityEngine.Random.onUnitSphere * 4f;
		this.pWeaponGO.rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
		this.DetachSpecific();
		UnityEngine.Object.Destroy(this.pWeaponGO, 3f);
	}

	// Token: 0x06000994 RID: 2452 RVA: 0x0004B3A8 File Offset: 0x000495A8
	public void Update()
	{
		if ((double)this.pFireTimer > 0.0)
		{
			this.pFireTimer -= Time.deltaTime;
		}
		this.UpdateSpecific();
	}

	// Token: 0x04000A0C RID: 2572
	protected Dictionary<string, DicEntry> pWeaponProps;

	// Token: 0x04000A0D RID: 2573
	protected readonly float pFireCoolDown;

	// Token: 0x04000A0E RID: 2574
	protected readonly string pProjectile = "None";

	// Token: 0x04000A0F RID: 2575
	protected readonly string pFireSound = "None";

	// Token: 0x04000A10 RID: 2576
	protected readonly bool pOverrideFireSound;

	// Token: 0x04000A11 RID: 2577
	protected readonly string pMuzzleFlash = "None";

	// Token: 0x04000A12 RID: 2578
	protected float pFireTimer;

	// Token: 0x04000A13 RID: 2579
	protected Transform pFireDummy;

	// Token: 0x04000A14 RID: 2580
	protected GameObject pWeaponGO;

	// Token: 0x04000A15 RID: 2581
	protected AudioSource pFireSoundAudio;
}
