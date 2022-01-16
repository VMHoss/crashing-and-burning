using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000146 RID: 326
public class RailGunScript : WeaponScript
{
	// Token: 0x0600097B RID: 2427 RVA: 0x0004A684 File Offset: 0x00048884
	public RailGunScript(Transform aWeaponSlot, string aWeapon) : base(aWeaponSlot, aWeapon)
	{
		this.pRailGunProjectileProps = Data.Shared["Projectiles"].d[this.pProjectile].d;
		this.pRailGunCharge = Loader.LoadGameObject("Shared", "Projectiles/RailGunCharge");
		this.pRailGunCharge.transform.parent = this.pFireDummy;
		this.pRailGunCharge.transform.localPosition = Vector3.zero;
		this.pRailGunCharge.transform.localRotation = Quaternion.identity;
		this.pRailGunCharge.SetActive(false);
		this.pRailGunBeam = Loader.LoadGameObject("Shared", "Projectiles/" + this.pRailGunProjectileProps["Prefab"].s);
		this.pRailGunBeam.transform.parent = this.pFireDummy;
		this.pRailGunBeam.transform.localPosition = Vector3.zero;
		this.pRailGunBeam.transform.localRotation = Quaternion.identity;
		this.pRailGunBeam.SetActive(false);
		this.pRailGunImpact = Loader.LoadGameObject("Shared", "Projectiles/RailGunImpact");
		this.pRailGunImpact.transform.parent = this.pRailGunBeam.transform;
		this.pRailGunImpact.transform.localPosition = new Vector3(0f, 0.99f, 0f);
		this.pRailGunImpact.transform.localRotation = Quaternion.identity;
		this.pRailGunImpact.SetActive(false);
		this.pRailGunMask = (1 << GameData.defaultLayer | 1 << GameData.destructibleLayer | 1 << GameData.trafficLayer | 1 << GameData.damagedTrafficLayer);
		this.pRailGunRange = this.pRailGunProjectileProps["Range"].f;
		this.pRailGunRangeInv = 1f / this.pRailGunRange;
		this.pRailGunDamage = this.pRailGunProjectileProps["Damage"].f;
		this.pMaxCharge = this.pWeaponProps["MaxCharge"].f;
		GameObject gameObject = Loader.LoadGameObject("Shared", "Projectiles/RailGunRings_PS");
		this.pRailGunRings = gameObject.particleSystem;
		this.pRailGunRings.enableEmission = false;
		this.pRailGunChargeAudio = Scripts.audioManager.PlaySFX("Weapons/RailGun", 1f, -1);
		this.pRailGunChargeAudio.enabled = false;
	}

	// Token: 0x0600097C RID: 2428 RVA: 0x0004A944 File Offset: 0x00048B44
	public override void FireDown(float anInheritedSpeed)
	{
		if (!this.pFiringRailGun)
		{
			this.pChargingRailGun = true;
			this.pRailGunCharge.SetActive(true);
			this.pChargeTime = 0.05f;
			this.pRailGunChargeAudio.enabled = true;
		}
	}

	// Token: 0x0600097D RID: 2429 RVA: 0x0004A97C File Offset: 0x00048B7C
	public override void FireHold(float anInheritedSpeed)
	{
		if (!this.pFiringRailGun && this.pChargingRailGun)
		{
			this.pChargeTime += Time.deltaTime;
			if (this.pChargeTime > this.pMaxCharge)
			{
				this.pChargeTime = this.pMaxCharge;
			}
		}
	}

	// Token: 0x0600097E RID: 2430 RVA: 0x0004A9D0 File Offset: 0x00048BD0
	public override void FireUp(float anInheritedSpeed)
	{
		if (!this.pFiringRailGun && this.pChargingRailGun)
		{
			if (this.pChargeTime >= this.pMaxCharge - 0.01f)
			{
				this.ActivateRailGun();
			}
			else
			{
				this.DeActivateRailGun();
			}
		}
	}

	// Token: 0x0600097F RID: 2431 RVA: 0x0004AA1C File Offset: 0x00048C1C
	public override void UpdateSpecific()
	{
		this.UpdateRailGunCharge();
		if (this.pFiringRailGun)
		{
			this.UpdateRailGun();
			if (this.pChargeTime > 0f)
			{
				this.pChargeTime -= Time.deltaTime;
				if (this.pChargeTime <= 0f)
				{
					this.DeActivateRailGun();
				}
			}
		}
	}

	// Token: 0x06000980 RID: 2432 RVA: 0x0004AA78 File Offset: 0x00048C78
	public override void DetachSpecific()
	{
		this.pChargeTime = -1f;
		this.DeActivateRailGun();
	}

	// Token: 0x06000981 RID: 2433 RVA: 0x0004AA8C File Offset: 0x00048C8C
	private void UpdateRailGunCharge()
	{
		if (this.pChargingRailGun)
		{
			float num = this.pChargeTime * 0.5f + UnityEngine.Random.Range(0.25f, 0.5f);
			this.pRailGunCharge.transform.localScale = new Vector3(num, num, num);
			this.pRailGunCharge.transform.localRotation = Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f);
		}
	}

	// Token: 0x06000982 RID: 2434 RVA: 0x0004AB08 File Offset: 0x00048D08
	private void ActivateRailGun()
	{
		this.pRailGunBeam.transform.parent = null;
		this.pFiringRailGun = true;
		this.pChargingRailGun = false;
		this.pRailGunBeam.SetActive(true);
		base.PlayFireSound();
		this.pRailGunCharge.SetActive(false);
		this.pRailGunRings.enableEmission = false;
		this.pRailGunRingOrigPos = this.pRailGunBeam.transform.position;
		this.pRailGunRings.transform.position = this.pRailGunRingOrigPos;
		this.pRingProgress = 0f;
		float num = 0.1f + this.pChargeTime * 0.5f;
		RaycastHit[] array = Physics.RaycastAll(this.pRailGunBeam.transform.position, this.pRailGunBeam.transform.up, this.pRailGunRange, this.pRailGunMask);
		foreach (RaycastHit raycastHit in array)
		{
			raycastHit.collider.SendMessage("Damage", new DamageInfo(this.pRailGunDamage), SendMessageOptions.DontRequireReceiver);
		}
		this.pRailGunBeam.transform.localScale = new Vector3(num, this.pRailGunRange, num);
		this.pRailGunImpact.SetActive(true);
		this.pMaxRingProgress = 1f;
		this.pMaxHitPos = this.pRailGunBeam.transform.position + this.pRailGunBeam.transform.up * this.pRailGunRange;
		this.pRailGunChargeAudio.enabled = false;
	}

	// Token: 0x06000983 RID: 2435 RVA: 0x0004AC98 File Offset: 0x00048E98
	private void UpdateRailGun()
	{
		float num = 0.1f + this.pChargeTime * 0.5f;
		if (this.pRingProgress <= this.pMaxRingProgress)
		{
			this.pRailGunRings.transform.position = Vector3.Lerp(this.pRailGunRingOrigPos, this.pMaxHitPos, this.pRingProgress);
			this.pRailGunRings.enableEmission = true;
		}
		else
		{
			this.pRailGunRings.enableEmission = false;
		}
		this.pRailGunBeam.transform.localScale = new Vector3(num, this.pRailGunRange, num);
		this.pRingProgress += Time.deltaTime * 5f;
	}

	// Token: 0x06000984 RID: 2436 RVA: 0x0004AD44 File Offset: 0x00048F44
	private void DeActivateRailGun()
	{
		this.pRailGunBeam.transform.parent = this.pFireDummy;
		this.pRailGunBeam.transform.localPosition = Vector3.zero;
		this.pRailGunBeam.transform.localRotation = Quaternion.identity;
		this.pRailGunBeam.SetActive(false);
		this.pFiringRailGun = false;
		this.pFireTimer = this.pFireCoolDown;
		this.pRailGunCharge.SetActive(false);
		this.pChargingRailGun = false;
		this.pRailGunChargeAudio.enabled = false;
		this.pRailGunRings.enableEmission = false;
	}

	// Token: 0x040009F8 RID: 2552
	private Dictionary<string, DicEntry> pRailGunProjectileProps;

	// Token: 0x040009F9 RID: 2553
	private GameObject pRailGunCharge;

	// Token: 0x040009FA RID: 2554
	private GameObject pRailGunBeam;

	// Token: 0x040009FB RID: 2555
	private GameObject pRailGunImpact;

	// Token: 0x040009FC RID: 2556
	private float pRailGunRange = 100f;

	// Token: 0x040009FD RID: 2557
	private float pRailGunRangeInv = 0.01f;

	// Token: 0x040009FE RID: 2558
	private float pRailGunDamage = 1f;

	// Token: 0x040009FF RID: 2559
	private float pMaxCharge = 0.5f;

	// Token: 0x04000A00 RID: 2560
	private int pRailGunMask;

	// Token: 0x04000A01 RID: 2561
	private float pChargeTime;

	// Token: 0x04000A02 RID: 2562
	private bool pFiringRailGun;

	// Token: 0x04000A03 RID: 2563
	private bool pChargingRailGun;

	// Token: 0x04000A04 RID: 2564
	private ParticleSystem pRailGunRings;

	// Token: 0x04000A05 RID: 2565
	private Vector3 pRailGunRingOrigPos = Vector3.zero;

	// Token: 0x04000A06 RID: 2566
	private Vector3 pMaxHitPos = Vector3.zero;

	// Token: 0x04000A07 RID: 2567
	private float pRingProgress;

	// Token: 0x04000A08 RID: 2568
	private float pMaxRingProgress = 1f;

	// Token: 0x04000A09 RID: 2569
	private AudioSource pRailGunChargeAudio;
}
