using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200012C RID: 300
public class SuperPowerToxic : SuperPowerBase
{
	// Token: 0x06000897 RID: 2199 RVA: 0x0003F65C File Offset: 0x0003D85C
	public SuperPowerToxic(CarScript aCarScript) : base(aCarScript, "Toxic")
	{
		this.pAreaColor = GenericFunctionsScript.ColorFromList(this.pSuperPowerProps["AreaColor"].l);
		this.pToxicGO = Loader.LoadGameObject("Shared", "Effects/SuperPowers/ToxicSmoke_PS");
		this.pToxicGO.transform.parent = aCarScript.transform;
		this.pToxicGO.transform.localPosition = Vector3.up;
		this.pToxicGO.transform.localRotation = Quaternion.Euler(-90f, 0f, 0f);
		this.pToxicsPS = new List<ParticleSystem>(this.pToxicGO.GetComponentsInChildren<ParticleSystem>());
		this.pToxicsPS.Add(this.pToxicGO.particleSystem);
		foreach (ParticleSystem particleSystem in this.pToxicsPS)
		{
			particleSystem.enableEmission = false;
		}
	}

	// Token: 0x06000898 RID: 2200 RVA: 0x0003F780 File Offset: 0x0003D980
	protected override void ActivateSpecific()
	{
		Scripts.trackScript.trackManager.SetActiveJoepsMaterial(true);
		Scripts.trackScript.trackManager.UpdateJoepsMaterialColor(this.pAreaColor);
		foreach (ParticleSystem particleSystem in this.pToxicsPS)
		{
			particleSystem.enableEmission = true;
		}
	}

	// Token: 0x06000899 RID: 2201 RVA: 0x0003F80C File Offset: 0x0003DA0C
	protected override void DeActivateSpecific(bool aDestroy)
	{
		Scripts.trackScript.trackManager.SetActiveJoepsMaterial(false);
		foreach (ParticleSystem particleSystem in this.pToxicsPS)
		{
			particleSystem.enableEmission = false;
		}
		if (aDestroy)
		{
			UnityEngine.Object.Destroy(this.pToxicGO, 2f);
		}
	}

	// Token: 0x0600089A RID: 2202 RVA: 0x0003F898 File Offset: 0x0003DA98
	protected override void UpdateSpecific()
	{
		this.pIntervalCheckTimer -= Time.deltaTime;
		if (this.pIntervalCheckTimer < 0f)
		{
			this.pIntervalCheckTimer = 0.5f;
			Vector3 position = this.pCarScript.transform.position;
			this.pObjectsToCheck = Scripts.trafficManager.GetTrafficInRange(position, 20f).ConvertAll<Transform>((TrafficScript x) => x.transform);
			List<DestructibleScript> list = new List<DestructibleScript>();
			Vector3 aStartRectPos = position - new Vector3(20f, 20f);
			Vector3 anEndRectPos = position + new Vector3(20f, 20f);
			List<GridEntry> gridEntriesFromRectangle = Scripts.gridManager.GetGridEntriesFromRectangle(aStartRectPos, anEndRectPos);
			foreach (GridEntry gridEntry in gridEntriesFromRectangle)
			{
				(gridEntry as BlockData).GetDestructiblesInRange(position, 400f, list);
			}
			this.pObjectsToCheck.AddRange(list.ConvertAll<Transform>((DestructibleScript x) => x.transform));
			foreach (Transform transform in this.pObjectsToCheck)
			{
				transform.SendMessage("Damage", new DamageInfo(50f), SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	// Token: 0x040008CD RID: 2253
	private const float TOXICRANGE = 20f;

	// Token: 0x040008CE RID: 2254
	private const float TOXICRANGE_SQR = 400f;

	// Token: 0x040008CF RID: 2255
	private const float INTERVALCHECKTIME = 0.5f;

	// Token: 0x040008D0 RID: 2256
	private Color pAreaColor;

	// Token: 0x040008D1 RID: 2257
	private GameObject pToxicGO;

	// Token: 0x040008D2 RID: 2258
	private List<ParticleSystem> pToxicsPS;

	// Token: 0x040008D3 RID: 2259
	private float pIntervalCheckTimer;

	// Token: 0x040008D4 RID: 2260
	private List<Transform> pObjectsToCheck;
}
