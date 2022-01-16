using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000142 RID: 322
public class FlameThrowerScript : WeaponScript
{
	// Token: 0x06000960 RID: 2400 RVA: 0x00049A7C File Offset: 0x00047C7C
	public FlameThrowerScript(Transform aWeaponSlot, string aWeapon) : base(aWeaponSlot, aWeapon)
	{
		this.pFlameThrowerProjectileProps = Data.Shared["Projectiles"].d[this.pProjectile].d;
		this.pFlameDamage = this.pFlameThrowerProjectileProps["Damage"].f;
		GameObject gameObject = Loader.LoadGameObject("Shared", "Projectiles/" + this.pFlameThrowerProjectileProps["Prefab"].s);
		gameObject.transform.parent = this.pFireDummy;
		gameObject.transform.localPosition = Vector3.zero;
		gameObject.transform.localRotation = Quaternion.Euler(-90f, 0f, 0f);
		this.pFlameThrowerFire = gameObject.particleSystem;
		this.pFlameThrowerFire.enableEmission = false;
		this.pFlameChild1 = gameObject.transform.GetChild(0).particleSystem;
		this.pFlameChild1.enableEmission = false;
		this.pFlameChild2 = gameObject.transform.GetChild(1).particleSystem;
		this.pFlameChild2.enableEmission = false;
		this.pFlameLoopAudio = Scripts.audioManager.PlaySFX("Weapons/FlameThrower", 1f, -1);
		this.pFlameLoopAudio.enabled = false;
	}

	// Token: 0x06000961 RID: 2401 RVA: 0x00049BDC File Offset: 0x00047DDC
	public override void FireDown(float anInheritedSpeed)
	{
		this.pFlameThrowerFire.enableEmission = true;
		this.pFlameChild1.enableEmission = true;
		this.pFlameChild2.enableEmission = true;
		base.PlayFireSound();
		this.pFlameLoopAudio.enabled = true;
	}

	// Token: 0x06000962 RID: 2402 RVA: 0x00049C20 File Offset: 0x00047E20
	public override void FireUp(float anInheritedSpeed)
	{
		this.pFlameThrowerFire.enableEmission = false;
		this.pFlameChild1.enableEmission = false;
		this.pFlameChild2.enableEmission = false;
		this.pFlameLoopAudio.enabled = false;
	}

	// Token: 0x06000963 RID: 2403 RVA: 0x00049C60 File Offset: 0x00047E60
	public override void UpdateSpecific()
	{
		if (this.pUpdateCheckObjectsTimer > 0f)
		{
			this.pUpdateCheckObjectsTimer -= Time.deltaTime;
		}
		int particleCount = this.pFlameThrowerFire.particleCount;
		if (particleCount > 0)
		{
			if (this.pUpdateCheckObjectsTimer <= 0f)
			{
				this.pUpdateCheckObjectsTimer = 0.5f;
				Vector3 position = this.pFireDummy.position;
				this.pObjectsToCheck = Scripts.trafficManager.GetTrafficInRange(position, 30f).ConvertAll<Transform>((TrafficScript x) => x.transform);
				List<DestructibleScript> list = new List<DestructibleScript>();
				Vector3 aStartRectPos = position - new Vector3(30f, 30f);
				Vector3 anEndRectPos = position + new Vector3(30f, 30f);
				List<GridEntry> gridEntriesFromRectangle = Scripts.gridManager.GetGridEntriesFromRectangle(aStartRectPos, anEndRectPos);
				foreach (GridEntry gridEntry in gridEntriesFromRectangle)
				{
					(gridEntry as BlockData).GetDestructiblesInRange(position, 900f, list);
				}
				this.pObjectsToCheck.AddRange(list.ConvertAll<Transform>((DestructibleScript x) => x.transform));
			}
			ParticleSystem.Particle[] array = new ParticleSystem.Particle[particleCount];
			this.pFlameThrowerFire.GetParticles(array);
			foreach (Transform transform in this.pObjectsToCheck)
			{
				for (int i = 0; i < particleCount; i++)
				{
					if ((array[i].position - transform.transform.position).sqrMagnitude < 9f)
					{
						transform.SendMessage("Damage", new DamageInfo(this.pFlameDamage), SendMessageOptions.DontRequireReceiver);
					}
				}
			}
		}
	}

	// Token: 0x06000964 RID: 2404 RVA: 0x00049E9C File Offset: 0x0004809C
	public override void DetachSpecific()
	{
		this.pFlameThrowerFire.enableEmission = false;
		this.pFlameChild1.enableEmission = false;
		this.pFlameChild2.enableEmission = false;
		this.pFlameLoopAudio.enabled = false;
	}

	// Token: 0x040009DC RID: 2524
	private const float UPDATE_OBJECTS_TO_CHECK = 0.5f;

	// Token: 0x040009DD RID: 2525
	private const float PARTICLE_RANGE = 30f;

	// Token: 0x040009DE RID: 2526
	private const float PARTICLE_RANGE_SQR = 900f;

	// Token: 0x040009DF RID: 2527
	private Dictionary<string, DicEntry> pFlameThrowerProjectileProps;

	// Token: 0x040009E0 RID: 2528
	private float pFlameDamage = 1f;

	// Token: 0x040009E1 RID: 2529
	private ParticleSystem pFlameThrowerFire;

	// Token: 0x040009E2 RID: 2530
	private ParticleSystem pFlameChild1;

	// Token: 0x040009E3 RID: 2531
	private ParticleSystem pFlameChild2;

	// Token: 0x040009E4 RID: 2532
	private float pUpdateCheckObjectsTimer = -0.1f;

	// Token: 0x040009E5 RID: 2533
	private List<Transform> pObjectsToCheck;

	// Token: 0x040009E6 RID: 2534
	private AudioSource pFlameLoopAudio;
}
