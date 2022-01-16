using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200012B RID: 299
public class SuperPowerStuntMan : SuperPowerBase
{
	// Token: 0x06000893 RID: 2195 RVA: 0x0003F324 File Offset: 0x0003D524
	public SuperPowerStuntMan(CarScript aCarScript) : base(aCarScript, "StuntMan")
	{
		this.pLeftTrail = TrailScript.AddTrail(aCarScript.GetLeftRearWheel());
		this.pRightTrail = TrailScript.AddTrail(aCarScript.GetRightRearWheel());
		TrailScript trailScript = this.pLeftTrail;
		bool generatingTrail = false;
		this.pRightTrail.generatingTrail = generatingTrail;
		trailScript.generatingTrail = generatingTrail;
		this.pStuntGO = Loader.LoadGameObject("Shared", "Effects/SuperPowers/StuntmanParticle_PS");
		this.pStuntGO.transform.parent = aCarScript.transform;
		this.pStuntGO.transform.localPosition = Vector3.zero;
		this.pStuntGO.transform.localRotation = Quaternion.Euler(0f, -180f, 0f);
		this.pStuntsPS = new List<ParticleSystem>(this.pStuntGO.GetComponentsInChildren<ParticleSystem>());
		this.pStuntsPS.Add(this.pStuntGO.particleSystem);
		foreach (ParticleSystem particleSystem in this.pStuntsPS)
		{
			particleSystem.enableEmission = false;
		}
	}

	// Token: 0x06000894 RID: 2196 RVA: 0x0003F464 File Offset: 0x0003D664
	protected override void ActivateSpecific()
	{
		TrailScript trailScript = this.pLeftTrail;
		bool generatingTrail = true;
		this.pRightTrail.generatingTrail = generatingTrail;
		trailScript.generatingTrail = generatingTrail;
		foreach (ParticleSystem particleSystem in this.pStuntsPS)
		{
			particleSystem.enableEmission = true;
		}
	}

	// Token: 0x06000895 RID: 2197 RVA: 0x0003F4E4 File Offset: 0x0003D6E4
	protected override void DeActivateSpecific(bool aDestroy)
	{
		TrailScript trailScript = this.pLeftTrail;
		bool generatingTrail = false;
		this.pRightTrail.generatingTrail = generatingTrail;
		trailScript.generatingTrail = generatingTrail;
		foreach (ParticleSystem particleSystem in this.pStuntsPS)
		{
			particleSystem.enableEmission = false;
		}
		if (aDestroy)
		{
			UnityEngine.Object.Destroy(this.pLeftTrail, 2f);
			UnityEngine.Object.Destroy(this.pRightTrail, 2f);
			UnityEngine.Object.Destroy(this.pStuntGO, 2f);
		}
	}

	// Token: 0x06000896 RID: 2198 RVA: 0x0003F59C File Offset: 0x0003D79C
	public override void FixedCarUpdate()
	{
		if (!this.pCarScript.hovering)
		{
			if (this.pCarScript.carData.leftKey > 0f)
			{
				this.pCarScript.rigidbody.AddRelativeTorque(Vector3.forward * 10f * this.pCarScript.rigidbody.mass);
			}
			else if (this.pCarScript.carData.rightKey > 0f)
			{
				this.pCarScript.rigidbody.AddRelativeTorque(Vector3.forward * 10f * -this.pCarScript.rigidbody.mass);
			}
		}
	}

	// Token: 0x040008C9 RID: 2249
	private TrailScript pLeftTrail;

	// Token: 0x040008CA RID: 2250
	private TrailScript pRightTrail;

	// Token: 0x040008CB RID: 2251
	private GameObject pStuntGO;

	// Token: 0x040008CC RID: 2252
	private List<ParticleSystem> pStuntsPS;
}
