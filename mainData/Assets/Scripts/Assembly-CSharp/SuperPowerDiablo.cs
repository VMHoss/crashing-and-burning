using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000128 RID: 296
public class SuperPowerDiablo : SuperPowerBase
{
	// Token: 0x06000889 RID: 2185 RVA: 0x0003EFA0 File Offset: 0x0003D1A0
	public SuperPowerDiablo(CarScript aCarScript) : base(aCarScript, "Diablo")
	{
		this.pAreaColor = GenericFunctionsScript.ColorFromList(this.pSuperPowerProps["AreaColor"].l);
		this.pDiabloFiresPS = new List<ParticleSystem>();
		List<GameObject> allWheels = this.pCarScript.GetAllWheels();
		foreach (GameObject gameObject in allWheels)
		{
			GameObject gameObject2 = Loader.LoadGameObject("Shared", "Effects/SuperPowers/DiabloFire_PS");
			gameObject2.transform.parent = gameObject.transform;
			gameObject2.transform.localPosition = Vector3.zero;
			gameObject2.transform.localRotation = Quaternion.identity;
			this.pDiabloFiresPS.Add(gameObject2.particleSystem);
			gameObject2.particleSystem.enableEmission = false;
		}
	}

	// Token: 0x0600088A RID: 2186 RVA: 0x0003F0A8 File Offset: 0x0003D2A8
	protected override void ActivateSpecific()
	{
		Scripts.trackScript.trackManager.SetActiveJoepsMaterial(true);
		Scripts.trackScript.trackManager.UpdateJoepsMaterialColor(this.pAreaColor);
		foreach (ParticleSystem particleSystem in this.pDiabloFiresPS)
		{
			particleSystem.enableEmission = true;
		}
	}

	// Token: 0x0600088B RID: 2187 RVA: 0x0003F134 File Offset: 0x0003D334
	protected override void DeActivateSpecific(bool aDestroy)
	{
		Scripts.trackScript.trackManager.SetActiveJoepsMaterial(false);
		foreach (ParticleSystem particleSystem in this.pDiabloFiresPS)
		{
			particleSystem.enableEmission = false;
			if (aDestroy)
			{
				UnityEngine.Object.Destroy(particleSystem.gameObject, 1f);
			}
		}
	}

	// Token: 0x0600088C RID: 2188 RVA: 0x0003F1C0 File Offset: 0x0003D3C0
	protected override void UpdateSpecific()
	{
		this.pCometTimer -= Time.deltaTime;
		if (this.pCometTimer <= 0f)
		{
			this.pCometTimer = 0.2f;
			float y = (float)UnityEngine.Random.Range(-50, 50);
			Quaternion rotation = Quaternion.Euler(0f, y, 0f);
			ExplosionScript.AddExplosion("DiabloExplosion", this.pCarScript.transform.position + Vector3.up + rotation * this.pCarScript.transform.forward * (float)UnityEngine.Random.Range(2, 25));
		}
	}

	// Token: 0x040008C3 RID: 2243
	private const float COMETPERIOD = 0.2f;

	// Token: 0x040008C4 RID: 2244
	private Color pAreaColor;

	// Token: 0x040008C5 RID: 2245
	private List<ParticleSystem> pDiabloFiresPS;

	// Token: 0x040008C6 RID: 2246
	private float pCometTimer = 0.2f;
}
