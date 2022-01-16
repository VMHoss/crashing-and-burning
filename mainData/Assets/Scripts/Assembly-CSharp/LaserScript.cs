using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000143 RID: 323
public class LaserScript : WeaponScript
{
	// Token: 0x06000967 RID: 2407 RVA: 0x00049EEC File Offset: 0x000480EC
	public LaserScript(Transform aWeaponSlot, string aWeapon) : base(aWeaponSlot, aWeapon)
	{
		this.pLaserProjectileProps = Data.Shared["Projectiles"].d[this.pProjectile].d;
		this.pLaserCharge = Loader.LoadGameObject("Shared", "Projectiles/LaserCharge");
		this.pLaserCharge.transform.parent = this.pFireDummy;
		this.pLaserCharge.transform.localPosition = Vector3.zero;
		this.pLaserCharge.transform.localRotation = Quaternion.identity;
		this.pLaserCharge.SetActive(false);
		this.pLaserBeam = Loader.LoadGameObject("Shared", "Projectiles/" + this.pLaserProjectileProps["Prefab"].s);
		this.pLaserBeam.transform.parent = this.pFireDummy;
		this.pLaserBeam.transform.localPosition = Vector3.zero;
		this.pLaserBeam.transform.localRotation = Quaternion.identity;
		this.pLaserBeam.SetActive(false);
		this.pLaserImpact = Loader.LoadGameObject("Shared", "Projectiles/LaserImpact");
		this.pLaserImpact.transform.parent = this.pLaserBeam.transform;
		this.pLaserImpact.transform.localPosition = new Vector3(0f, 0.99f, 0f);
		this.pLaserImpact.transform.localRotation = Quaternion.identity;
		this.pLaserImpact.SetActive(false);
		this.pLaserMask = (1 << GameData.defaultLayer | 1 << GameData.destructibleLayer | 1 << GameData.trafficLayer | 1 << GameData.damagedTrafficLayer);
		this.pLaserRange = this.pLaserProjectileProps["Range"].f;
		this.pLaserDamage = this.pLaserProjectileProps["Damage"].f;
		this.pMaxCharge = this.pWeaponProps["MaxCharge"].f;
		this.pLaserChargeAudio = Scripts.audioManager.PlaySFX("Weapons/Laser", 1f, -1);
		this.pLaserChargeAudio.enabled = false;
	}

	// Token: 0x06000968 RID: 2408 RVA: 0x0004A148 File Offset: 0x00048348
	public override void FireDown(float anInheritedSpeed)
	{
		if (!this.pFiringLaser)
		{
			this.pChargingLaser = true;
			this.pLaserCharge.SetActive(true);
			this.pChargeTime = 0.05f;
			this.pLaserChargeAudio.enabled = true;
		}
	}

	// Token: 0x06000969 RID: 2409 RVA: 0x0004A180 File Offset: 0x00048380
	public override void FireHold(float anInheritedSpeed)
	{
		if (!this.pFiringLaser && this.pChargingLaser)
		{
			this.pChargeTime += Time.deltaTime;
			if (this.pChargeTime > this.pMaxCharge)
			{
				this.pChargeTime = this.pMaxCharge;
			}
		}
	}

	// Token: 0x0600096A RID: 2410 RVA: 0x0004A1D4 File Offset: 0x000483D4
	public override void FireUp(float anInheritedSpeed)
	{
		if (!this.pFiringLaser && this.pChargingLaser)
		{
			this.ActivateLaser();
		}
	}

	// Token: 0x0600096B RID: 2411 RVA: 0x0004A1F4 File Offset: 0x000483F4
	public override void UpdateSpecific()
	{
		this.UpdateLaserCharge();
		if (this.pFiringLaser)
		{
			this.UpdateLaser();
			if (this.pChargeTime > 0f)
			{
				this.pChargeTime -= Time.deltaTime;
				if (this.pChargeTime <= 0f)
				{
					this.DeActivateLaser();
				}
			}
		}
	}

	// Token: 0x0600096C RID: 2412 RVA: 0x0004A250 File Offset: 0x00048450
	public override void DetachSpecific()
	{
		this.pChargeTime = -1f;
		this.DeActivateLaser();
	}

	// Token: 0x0600096D RID: 2413 RVA: 0x0004A264 File Offset: 0x00048464
	private void UpdateLaserCharge()
	{
		if (this.pChargingLaser || this.pFiringLaser)
		{
			float num = this.pChargeTime * 0.5f + UnityEngine.Random.Range(0.25f, 0.5f);
			this.pLaserCharge.transform.localScale = new Vector3(num, num, num);
			this.pLaserCharge.transform.localRotation = Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f);
		}
	}

	// Token: 0x0600096E RID: 2414 RVA: 0x0004A2EC File Offset: 0x000484EC
	private void ActivateLaser()
	{
		this.pFiringLaser = true;
		this.pChargingLaser = false;
		this.pLaserBeam.SetActive(true);
		base.PlayFireSound();
		this.UpdateLaser();
	}

	// Token: 0x0600096F RID: 2415 RVA: 0x0004A320 File Offset: 0x00048520
	private void UpdateLaser()
	{
		float num = this.pChargeTime * 0.4f + UnityEngine.Random.Range(0.15f, 0.3f);
		RaycastHit raycastHit;
		if (Physics.Raycast(this.pLaserBeam.transform.position, this.pLaserBeam.transform.up, out raycastHit, this.pLaserRange, this.pLaserMask))
		{
			raycastHit.collider.SendMessage("Damage", new DamageInfo(this.pLaserDamage), SendMessageOptions.DontRequireReceiver);
			this.pLaserBeam.transform.localScale = new Vector3(num, raycastHit.distance, num);
			this.pLaserImpact.SetActive(true);
		}
		else
		{
			this.pLaserBeam.transform.localScale = new Vector3(num, this.pLaserRange, num);
			this.pLaserImpact.SetActive(false);
		}
		this.pLaserCharge.transform.localScale = new Vector3(num, num, num);
	}

	// Token: 0x06000970 RID: 2416 RVA: 0x0004A410 File Offset: 0x00048610
	private void DeActivateLaser()
	{
		this.pLaserBeam.SetActive(false);
		this.pLaserCharge.SetActive(false);
		this.pFiringLaser = false;
		this.pFireTimer = this.pFireCoolDown;
		this.pLaserChargeAudio.enabled = false;
	}

	// Token: 0x040009E9 RID: 2537
	private Dictionary<string, DicEntry> pLaserProjectileProps;

	// Token: 0x040009EA RID: 2538
	private GameObject pLaserCharge;

	// Token: 0x040009EB RID: 2539
	private GameObject pLaserBeam;

	// Token: 0x040009EC RID: 2540
	private GameObject pLaserImpact;

	// Token: 0x040009ED RID: 2541
	private float pLaserRange = 100f;

	// Token: 0x040009EE RID: 2542
	private float pLaserDamage = 1f;

	// Token: 0x040009EF RID: 2543
	private float pMaxCharge = 0.5f;

	// Token: 0x040009F0 RID: 2544
	private int pLaserMask;

	// Token: 0x040009F1 RID: 2545
	private float pChargeTime;

	// Token: 0x040009F2 RID: 2546
	private bool pFiringLaser;

	// Token: 0x040009F3 RID: 2547
	private bool pChargingLaser;

	// Token: 0x040009F4 RID: 2548
	private AudioSource pLaserChargeAudio;
}
