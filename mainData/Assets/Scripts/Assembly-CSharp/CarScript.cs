using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200013C RID: 316
public class CarScript : MonoBehaviour
{
	// Token: 0x06000910 RID: 2320 RVA: 0x000445AC File Offset: 0x000427AC
	protected virtual void StartSpecific()
	{
	}

	// Token: 0x06000911 RID: 2321 RVA: 0x000445B0 File Offset: 0x000427B0
	protected virtual void UpdateSpecific()
	{
	}

	// Token: 0x06000912 RID: 2322 RVA: 0x000445B4 File Offset: 0x000427B4
	protected virtual void ExplodeCarSpecific()
	{
	}

	// Token: 0x06000913 RID: 2323 RVA: 0x000445B8 File Offset: 0x000427B8
	protected virtual void ActionSpecific()
	{
	}

	// Token: 0x06000914 RID: 2324 RVA: 0x000445BC File Offset: 0x000427BC
	protected virtual void FixedUpdateSpecific()
	{
	}

	// Token: 0x06000915 RID: 2325 RVA: 0x000445C0 File Offset: 0x000427C0
	private void Awake()
	{
		this.pCarBody = base.gameObject;
		this.pCarBody.AddComponent<Rigidbody>();
		this.carData = GameData.vehicles[this.pCarBody.name];
		this.pCarBody.rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
		this.pCarBody.rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
		this.pCarBody.collider.material = GameData.trackPhysicMaterial;
		this.pCarBody.layer = GameData.carLayer;
		this.LockCarAtStart(true);
		if (this.pCarBody.collider == null)
		{
			this.pCarBody.AddComponent<MeshCollider>().convex = true;
		}
		this.pWheels = new List<GameObject>();
		this.pWheelRayPositions = new List<Vector3>();
		this.pWheelLocalPositions = new List<Vector3>();
		this.pWheelOffset = new List<float>();
		this.pVehicleAdjGrip = this.carData.vehicleGrip;
		this.pickUpRangeSqr = base.renderer.bounds.extents.x * base.renderer.bounds.extents.x * 12f;
		this.pWheels.Add(this.pCarBody.transform.Find(this.carData.modelName + "Wheel_LF").gameObject);
		this.pWheels.Add(this.pCarBody.transform.Find(this.carData.modelName + "Wheel_RF").gameObject);
		this.pWheels.Add(this.pCarBody.transform.Find(this.carData.modelName + "Wheel_LB").gameObject);
		this.pWheels.Add(this.pCarBody.transform.Find(this.carData.modelName + "Wheel_RB").gameObject);
		foreach (GameObject gameObject in this.pWheels)
		{
			gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
		}
		this.pWheelLocalPositions.Add(this.pWheels[0].transform.localPosition);
		this.pWheelLocalPositions.Add(this.pWheels[1].transform.localPosition);
		this.pWheelLocalPositions.Add(this.pWheels[2].transform.localPosition);
		this.pWheelLocalPositions.Add(this.pWheels[3].transform.localPosition);
		this.carData.wheelRadius = this.pWheels[0].transform.localPosition.y;
		base.renderer.sharedMaterial = (UnityEngine.Object.Instantiate(Loader.LoadMaterial(this.carData.assetBundle, this.carData.modelName + "/" + this.carData.modelName + "_Material")) as Material);
		Material sharedMaterial = Loader.LoadMaterial(this.carData.assetBundle, this.carData.modelName + "/" + this.carData.modelName + "Wheels_Material");
		foreach (GameObject gameObject2 in this.pWheels)
		{
			gameObject2.renderer.sharedMaterial = sharedMaterial;
		}
		Transform transform = this.pCarBody.transform.Find(this.carData.modelName + "Glass");
		if (transform != null)
		{
			this.pCarGlass = transform.gameObject;
			this.pCarGlass.renderer.sharedMaterial = base.renderer.sharedMaterial;
		}
		else
		{
			this.pCarGlass = null;
		}
		this.pSmokeDummy = this.pCarBody.transform.Find(this.carData.modelName + "Smoke_Dummy").gameObject;
		if (this.pSmokeDummy == null)
		{
			Debug.LogWarning("No smoke dummy found with name: " + this.carData.modelName + "Smoke_Dummy");
		}
		this.pExhaustPSList = new List<ParticleSystem>();
		this.pExhaustNitroPSList = new List<ParticleSystem>();
		for (int i = 1; i <= 5; i++)
		{
			Transform transform2 = this.pCarBody.transform.Find(string.Concat(new object[]
			{
				this.carData.modelName,
				"Exhaust",
				i,
				"_Dummy"
			}));
			if (transform2 == null)
			{
				break;
			}
			GameObject gameObject3 = Loader.LoadGameObject("Shared", "Effects/ExhaustSmoke_PS");
			gameObject3.name = this.carData.carName + "ExhaustSmoke_PS" + i;
			gameObject3.transform.parent = transform2;
			gameObject3.transform.localPosition = Vector3.zero;
			gameObject3.transform.localRotation = Quaternion.Euler(-90f, 0f, 0f);
			gameObject3.particleSystem.Stop();
			this.pExhaustPSList.Add(gameObject3.particleSystem);
			GameObject gameObject4 = Loader.LoadGameObject("Shared", "Effects/CarParticles/Nitro_PS");
			gameObject4.name = this.carData.carName + "Nitro_PS" + i;
			gameObject4.transform.parent = transform2;
			gameObject4.transform.localPosition = Vector3.zero;
			gameObject4.transform.localRotation = Quaternion.Euler(-90f, 0f, 0f);
			gameObject4.particleSystem.Stop();
			this.pExhaustNitroPSList.Add(gameObject4.particleSystem);
			if (this.pNitroStartSize < -0.5f)
			{
				this.pNitroStartSize = this.pExhaustNitroPSList[0].startSize;
				this.pNitroStartSpeed = this.pExhaustNitroPSList[0].startSpeed;
			}
		}
		this.pWheelLastSkidMark = new int[4];
		for (int j = 0; j < 4; j++)
		{
			this.pWheelLastSkidMark[j] = -1;
		}
		this.pWheelOnGround = new bool[4];
		for (int k = 0; k < 4; k++)
		{
			this.pWheelOnGround[k] = false;
		}
		this.pWheelNormals = new Vector3[4];
		for (int l = 0; l < 4; l++)
		{
			this.pWheelNormals[l] = new Vector3(0f, 1f, 0f);
		}
		this.pCOM = this.pCarBody.rigidbody.centerOfMass;
		this.pCarBody.rigidbody.mass = this.carData.vehicleMass;
		this.pCarBody.rigidbody.centerOfMass = new Vector3(this.pCOM.x, 0.1f, this.pCOM.z);
		this.pCarBody.rigidbody.angularDrag = this.carData.vehicleAngularDrag;
		this.pCarBody.rigidbody.drag = 0f;
		this.pCarBody.collider.material.dynamicFriction = 0.5f;
		this.pCarBody.collider.material.staticFriction = 0.1f;
		this.pCarBody.collider.material.bounciness = 0.05f;
		float d = Mathf.Abs(this.pWheels[0].transform.localPosition.x - this.pWheels[1].transform.localPosition.x) * 0.5f;
		float d2 = Mathf.Abs(this.pWheels[1].transform.localPosition.z - this.pWheels[3].transform.localPosition.z) * 0.5f;
		Vector3 a = this.pCOM;
		a.y = this.carData.wheelRadius;
		this.pWheelRayPositions.Add(a + new Vector3(1f, 0f, 0f) * d + new Vector3(0f, 0f, -1f) * d2);
		this.pWheelRayPositions.Add(a + new Vector3(-1f, 0f, 0f) * d + new Vector3(0f, 0f, -1f) * d2);
		this.pWheelRayPositions.Add(a + new Vector3(1f, 0f, 0f) * d + new Vector3(0f, 0f, 1f) * d2);
		this.pWheelRayPositions.Add(a + new Vector3(-1f, 0f, 0f) * d + new Vector3(0f, 0f, 1f) * d2);
		this.pWheelOffset.Add(0f);
		this.pWheelOffset.Add(0f);
		this.pWheelOffset.Add(0f);
		this.pWheelOffset.Add(0f);
		this.pEngineStationary = Scripts.audioManager.PlaySFX("Vehicles/" + this.carData.engineAudio + "Stationair", 0.75f, -1, base.gameObject);
		this.pEngineStationary.priority = 3;
		this.pEngineStationary.minDistance = 20f;
		this.pEngineStationary.volume = 0f;
		this.pEngineStationary.dopplerLevel = 0f;
		this.pEngineTopSpeed = Scripts.audioManager.PlaySFX("Vehicles/" + this.carData.engineAudio + "Driving", 0f, -1, base.gameObject);
		this.pEngineTopSpeed.priority = 3;
		this.pEngineTopSpeed.pitch = 0.5f;
		this.pEngineTopSpeed.dopplerLevel = 0f;
		this.pEngineTopSpeed.minDistance = 20f;
		this.pEngineTopSpeed.volume = 0f;
		this.pSkidSound = Scripts.audioManager.PlaySFX("Skid", 0.75f, -1, base.gameObject);
		if (this.pSkidSound != null)
		{
			this.pSkidSound.priority = 3;
			this.pSkidSound.Stop();
			this.pSkidSound.enabled = false;
			this.pSkidSound.minDistance = 20f;
		}
		this.pCarAsphalt = Scripts.audioManager.PlaySFX("CarAsphalt", 0f, -1, base.gameObject);
		if (this.pCarAsphalt != null)
		{
			this.pCarAsphalt.priority = 3;
			this.pCarAsphalt.Stop();
			this.pCarAsphalt.enabled = false;
			this.pCarAsphalt.minDistance = 20f;
		}
		this.pCarSlide = Scripts.audioManager.PlaySFX("Slide", 0f, -1, base.gameObject);
		if (this.pCarSlide != null)
		{
			this.pCarSlide.priority = 3;
			this.pCarSlide.Stop();
			this.pCarSlide.enabled = false;
			this.pCarSlide.minDistance = 20f;
		}
		this.pNitroSound = Scripts.audioManager.PlaySFX("Effects/NitroBoostLoop", 1f, -1, base.gameObject);
		this.pNitroSound.Stop();
		this.pNitroSound.enabled = false;
		this.pWheelSmoke_PS = new ParticleSystem[4];
		for (int m = 2; m < 4; m++)
		{
			this.pWheelSmoke_PS[m] = Loader.LoadGameObject("Shared", "Effects/CarParticles/WheelSmoke_PS").particleSystem;
			this.pWheelSmoke_PS[m].transform.parent = base.transform;
			this.pWheelSmoke_PS[m].transform.localPosition = this.pWheels[m].transform.localPosition;
			this.pWheelSmoke_PS[m].enableEmission = false;
		}
		Transform transform3 = base.gameObject.transform.Find(this.carData.modelName + "Glass");
		if (transform3 != null)
		{
			transform3.gameObject.layer = LayerMask.NameToLayer("CarPart");
		}
		foreach (GameObject gameObject5 in this.pWheels)
		{
			gameObject5.layer = LayerMask.NameToLayer("CarPart");
		}
		this.pDestrExplosionPSList = new List<GameObject>();
		this.pDestrExplosionPSPosList = new List<Vector3>();
		this.pDestrFirePSList = new List<GameObject>();
		this.pDestrFirePSPosList = new List<Vector3>();
		this.pHoodSmoke = Loader.LoadGameObject("Shared", "Effects/CarParticles/DamagedSmoke_PS").particleSystem;
		this.pHoodSmoke.transform.parent = base.transform;
		this.pHoodSmoke.transform.localPosition = this.pSmokeDummy.transform.localPosition;
		this.pHoodSmoke.Stop();
	}

	// Token: 0x06000916 RID: 2326 RVA: 0x00045448 File Offset: 0x00043648
	private void Start()
	{
		this.pTrackManager = Scripts.trackScript.trackManager;
		this.RestoreCar(false);
		Transform[] componentsInChildren = base.GetComponentsInChildren<Transform>();
		foreach (DicEntry dicEntry in this.carData.debrisList)
		{
			foreach (Transform transform in componentsInChildren)
			{
				if (transform.name == dicEntry.s)
				{
					transform.gameObject.AddComponent<FallOffPartScript>().fallOffSpeed = this.carData.debrisFallOffSpeed;
					break;
				}
			}
		}
		this.pWeapons = new List<WeaponScript>();
		foreach (KeyValuePair<string, string> keyValuePair in this.carData.weaponSlots)
		{
			Transform transform2 = base.transform.Find(keyValuePair.Key + "_Dummy");
			if (transform2 != null)
			{
				this.pWeapons.Add(WeaponScript.CreateWeapon(transform2, keyValuePair.Value));
			}
			else
			{
				Debug.LogError("Weapon slot dummy not found on car: " + keyValuePair.Key + "_Dummy , it's been skipped");
			}
		}
		this.SetCarShadow();
		this.SetRealTimeCubeMap();
		this.StartSpecific();
	}

	// Token: 0x06000917 RID: 2327 RVA: 0x000455FC File Offset: 0x000437FC
	public void SetVisuals()
	{
		this.SetCarShadow();
	}

	// Token: 0x06000918 RID: 2328 RVA: 0x00045604 File Offset: 0x00043804
	private void SetCarShadow()
	{
		if (this.pRealTimeShadowProjector != null)
		{
			UnityEngine.Object.Destroy(this.pRealTimeShadowProjector);
		}
		this.pRealTimeShadowProjector = base.gameObject.AddComponent<RealTimeShadowProjector>();
		this.pRealTimeShadowProjector.Initialize(null, new Vector3(-2f, 3.13f, 1.81f), GameData.carPartLayer);
		if (!Data.highDetails)
		{
			this.pRealTimeShadowProjector.SetProjectorTexture(Loader.LoadTexture(this.carData.assetBundle, this.carData.modelName + "/" + this.carData.modelName + "Shadow_Texture"));
		}
		if (Data.highDetails)
		{
			base.renderer.sharedMaterial.shader = Shader.Find("CarShaderModel 3.0");
		}
		else
		{
			base.renderer.sharedMaterial.shader = Shader.Find("CarShaderModel 2.0");
		}
	}

	// Token: 0x06000919 RID: 2329 RVA: 0x000456F4 File Offset: 0x000438F4
	public void SetSounds(bool anEnabled)
	{
		this.pEngineStationary.enabled = anEnabled;
		this.pEngineTopSpeed.enabled = anEnabled;
		this.pCarAsphalt.enabled = anEnabled;
		if (anEnabled)
		{
			this.pEngineStationary.Play();
			this.pEngineTopSpeed.Play();
			this.pCarAsphalt.Play();
			this.pEngineStationary.volume = 1f;
		}
		else
		{
			this.pEngineStationary.Stop();
			this.pEngineTopSpeed.Stop();
			this.pCarAsphalt.Stop();
		}
		if (this.pSuperPower != null)
		{
			this.pSuperPower.SetSound(anEnabled);
		}
	}

	// Token: 0x0600091A RID: 2330 RVA: 0x0004579C File Offset: 0x0004399C
	public void SetCarTexture()
	{
		string s = Data.Shared["Car"].d[GameData.playerCar].d["Model"].s;
		this.pDiffuseTexture = Loader.LoadTexture(this.carData.assetBundle, s + "/" + s + "Skin1_Texture");
		if (!this.pDiffuseTexture)
		{
			Debug.LogError("Unable to find texture of pimp: " + s + "Skin1_Texture, check the resources! Is there a material and texture with the same name?");
		}
		else
		{
			base.renderer.sharedMaterial.SetTexture("_Diffuse", this.pDiffuseTexture);
		}
	}

	// Token: 0x0600091B RID: 2331 RVA: 0x00045848 File Offset: 0x00043A48
	public void SetRealTimeCubeMap()
	{
		if (Data.platform == "PC")
		{
			int aCullingMask = ~(1 << GameData.carLayer | 1 << GameData.carPartLayer | 1 << GameData.minimapLayer | 1 << GameData.interfaceLayer | 1 << GameData.invisibleWallLayer);
			base.gameObject.AddComponent<CubemapScript>().Initialize(aCullingMask, !Data.highDetails);
		}
	}

	// Token: 0x0600091C RID: 2332 RVA: 0x000458BC File Offset: 0x00043ABC
	public void LockCarAtStart(bool aLock)
	{
		if (aLock)
		{
			base.rigidbody.constraints |= (RigidbodyConstraints)10;
		}
		else
		{
			base.rigidbody.constraints &= (RigidbodyConstraints)(-11);
		}
	}

	// Token: 0x0600091D RID: 2333 RVA: 0x000458FC File Offset: 0x00043AFC
	public Transform GetTransform()
	{
		return this.pCarBody.transform;
	}

	// Token: 0x0600091E RID: 2334 RVA: 0x0004590C File Offset: 0x00043B0C
	public void ApplyUpgrades()
	{
		this.pNitroDuration = Data.Shared["PickUps"].d["Nitro"].d["Duration"].f;
		if (Shop.GetUpgradeLevel("ArmorStrength") > 0)
		{
			this.carData.armor += Data.Shared["Upgrades"].d["ArmorStrength"].d["Upgrade" + Shop.GetUpgradeLevel("ArmorStrength")].f;
			this.carData.armorInv = 1f / this.carData.armor;
		}
		if (Shop.GetUpgradeLevel("Handling") > 0)
		{
			this.carData.vehicleTurnGain += Data.Shared["Upgrades"].d["Handling"].d["Upgrade" + Shop.GetUpgradeLevel("Handling")].f;
		}
		this.pAirControlStrength = 1f;
		if (Shop.GetUpgradeLevel("AirTimeControl") > 0)
		{
			this.pAirControlStrength += Data.Shared["Upgrades"].d["AirTimeControl"].d["Upgrade" + Shop.GetUpgradeLevel("AirTimeControl")].f;
		}
		if (Shop.GetUpgradeLevel("Speed") > 0)
		{
			float f = Data.Shared["Upgrades"].d["Speed"].d["Upgrade" + Shop.GetUpgradeLevel("Speed")].f;
			this.carData.vehicleMaxSpeed += f;
			this.carData.vehicleMaxSpeedInv = 1f / this.carData.vehicleMaxSpeed;
			this.carData.vehicleMaxNitroSpeed += f;
		}
		if (GameData.upgradedLevels["ArmorStrength"] > 0)
		{
			this.carData.armor += Data.Shared["Upgrades"].d["ArmorStrength"].d["Upgrade" + GameData.upgradedLevels["ArmorStrength"]].f;
			this.carData.armorInv = 1f / this.carData.armor;
		}
		if (GameData.upgradedLevels["Speed"] > 0)
		{
			this.carData.vehicleMaxSpeed += Data.Shared["Upgrades"].d["Speed"].d["Upgrade" + GameData.upgradedLevels["Speed"]].f;
			this.carData.vehicleMaxSpeedInv = 1f / this.carData.vehicleMaxSpeed;
			this.carData.vehicleMaxNitroSpeed += Data.Shared["Upgrades"].d["Speed"].d["Upgrade" + GameData.upgradedLevels["Speed"]].f;
		}
		if (GameData.upgradedLevels["Handling"] > 0)
		{
			this.carData.vehicleTurnGain += Data.Shared["Upgrades"].d["Handling"].d["Upgrade" + GameData.upgradedLevels["Handling"]].f;
		}
	}

	// Token: 0x0600091F RID: 2335 RVA: 0x00045D30 File Offset: 0x00043F30
	public void SetItemEffect(string anItem)
	{
		switch (anItem)
		{
		case "Boost":
			this.pItem = new ItemBoost(this);
			goto IL_128;
		case "CarBomb":
			this.pItem = new ItemCarBomb(this);
			goto IL_128;
		case "Jump":
			this.pItem = new ItemJump(this);
			goto IL_128;
		case "Magnet":
			this.pItem = new ItemMagnet(this);
			goto IL_128;
		case "PulseBurst":
			this.pItem = new ItemPulseBurst(this);
			goto IL_128;
		case "Shield":
			this.pItem = new ItemShield(this);
			goto IL_128;
		}
		Debug.LogError("Unknown gadget/item: " + anItem + ", using standard Boost");
		this.pItem = new ItemBoost(this);
		IL_128:
		this.pItem.StartSpecific();
	}

	// Token: 0x06000920 RID: 2336 RVA: 0x00045E70 File Offset: 0x00044070
	public void SetEquippedSuperPowerEffect(string aSuperPower)
	{
		this.pEquippedSuperPower = this.SetUpSuperPower(aSuperPower);
		this.pSuperPower = this.pEquippedSuperPower;
		if (this.pSuperPower != null)
		{
			this.pSuperPower.Activate();
			this.pSuperPower.SetSound(false);
		}
	}

	// Token: 0x06000921 RID: 2337 RVA: 0x00045EB0 File Offset: 0x000440B0
	public void SetPickUpSuperPowerEffect(string aSuperPower)
	{
		if (this.pPickUpSuperPower != null)
		{
			this.pPickUpSuperPower.DeActivate(true);
		}
		if (this.pEquippedSuperPower != null)
		{
			this.pEquippedSuperPower.DeActivate();
		}
		this.pPickUpSuperPower = this.SetUpSuperPower(aSuperPower);
		this.pSuperPower = this.pPickUpSuperPower;
		if (this.pSuperPower != null)
		{
			this.pSuperPower.Activate(Data.Shared["SuperPowers"].d["PickUpDuration"].f);
		}
	}

	// Token: 0x06000922 RID: 2338 RVA: 0x00045F3C File Offset: 0x0004413C
	private SuperPowerBase SetUpSuperPower(string aSuperPower)
	{
		SuperPowerBase result = null;
		switch (aSuperPower)
		{
		case "None":
			return result;
		case "StuntMan":
			return new SuperPowerStuntMan(this);
		case "Golden":
			return new SuperPowerGolden(this);
		case "QuadDamage":
			return new SuperPowerQuadDamage(this);
		case "Diablo":
			return new SuperPowerDiablo(this);
		case "Toxic":
			return new SuperPowerToxic(this);
		}
		Debug.LogError("Unknown super power: " + aSuperPower + ", not using any super power");
		result = null;
		return result;
	}

	// Token: 0x06000923 RID: 2339 RVA: 0x00046048 File Offset: 0x00044248
	private void CancelPickUpSuperPower()
	{
		this.pPickUpSuperPower.DeActivate(true);
		this.pPickUpSuperPower = null;
		this.pSuperPower = this.pEquippedSuperPower;
		if (this.pSuperPower != null)
		{
			this.pSuperPower.Activate();
		}
	}

	// Token: 0x06000924 RID: 2340 RVA: 0x00046080 File Offset: 0x00044280
	public GameObject GetLeftRearWheel()
	{
		return this.pWheels[2];
	}

	// Token: 0x06000925 RID: 2341 RVA: 0x00046090 File Offset: 0x00044290
	public GameObject GetRightRearWheel()
	{
		return this.pWheels[3];
	}

	// Token: 0x06000926 RID: 2342 RVA: 0x000460A0 File Offset: 0x000442A0
	public List<GameObject> GetAllWheels()
	{
		return this.pWheels;
	}

	// Token: 0x06000927 RID: 2343 RVA: 0x000460A8 File Offset: 0x000442A8
	public void GrantImmunity(float aDuration)
	{
		if (!this.pDestroyed)
		{
			this.pInvincibilityTime = aDuration;
		}
	}

	// Token: 0x06000928 RID: 2344 RVA: 0x000460BC File Offset: 0x000442BC
	public void RepairCar()
	{
		if (!this.pDestroyed)
		{
			this.damage = 0f;
			this.UpdateCarDamageVisuals();
			this.pHoodSmoke.Stop();
		}
	}

	// Token: 0x06000929 RID: 2345 RVA: 0x000460E8 File Offset: 0x000442E8
	public bool IsDestroyed()
	{
		return this.pDestroyed;
	}

	// Token: 0x0600092A RID: 2346 RVA: 0x000460F0 File Offset: 0x000442F0
	public bool HasInvincibility()
	{
		return this.pInvincibilityTime > 0f;
	}

	// Token: 0x0600092B RID: 2347 RVA: 0x00046100 File Offset: 0x00044300
	public bool HasQuadDamage()
	{
		return this.pSuperPower != null && this.pSuperPower.name == "QuadDamage";
	}

	// Token: 0x0600092C RID: 2348 RVA: 0x00046128 File Offset: 0x00044328
	public float GetAttackDamage()
	{
		return this.carData.attack;
	}

	// Token: 0x0600092D RID: 2349 RVA: 0x00046138 File Offset: 0x00044338
	public float GetAirTime()
	{
		return this.pAirTime;
	}

	// Token: 0x0600092E RID: 2350 RVA: 0x00046140 File Offset: 0x00044340
	public void LockCar(bool aLock)
	{
		this.pLock = aLock;
		if (this.pLock)
		{
			base.rigidbody.constraints = RigidbodyConstraints.FreezeAll;
			this.pRestoreLinVel = base.rigidbody.velocity;
			this.pRestoreAngVel = base.rigidbody.angularVelocity;
		}
		else
		{
			base.rigidbody.constraints = RigidbodyConstraints.None;
			base.rigidbody.velocity = this.pRestoreLinVel;
			base.rigidbody.angularVelocity = this.pRestoreAngVel;
		}
	}

	// Token: 0x0600092F RID: 2351 RVA: 0x000461C4 File Offset: 0x000443C4
	public bool GetLock()
	{
		return this.pLock;
	}

	// Token: 0x06000930 RID: 2352 RVA: 0x000461CC File Offset: 0x000443CC
	private void Update()
	{
		if (Data.pause)
		{
			return;
		}
		if (!Data.raceInProgress)
		{
			return;
		}
		this.carData.UpdateKeys();
		this.pItem.UpdateGeneric();
		if (this.pSuperPower != null && this.pSuperPower.Update())
		{
			this.CancelPickUpSuperPower();
		}
		float deltaTime = Time.deltaTime;
		if (!this.pDestroyed)
		{
			foreach (WeaponScript weaponScript in this.pWeapons)
			{
				weaponScript.Update();
			}
			if (!this.carData.prevFireKey)
			{
				if (this.carData.fireKey)
				{
					this.FireWeaponsDown();
				}
			}
			else if (this.carData.fireKey)
			{
				this.FireWeaponsHold();
			}
			else
			{
				this.FireWeaponsUp();
			}
			if (this.carData.useItem && !this.carData.prevUseItem)
			{
				this.pItem.ActionGeneric();
			}
		}
		if (this.pDetachParts)
		{
			this.DetachVehicleParts();
		}
		this.carData.currentSpeed = Vector3.Dot(this.pCarBody.rigidbody.velocity, this.pCarBody.transform.forward);
		this.carData.currentSpeedPerc = this.carData.currentSpeed * this.carData.vehicleMaxSpeedInv;
		float num = this.carData.currentSpeed * this.carData.vehicleMaxSpeedInv;
		float num2 = Mathf.Abs(num);
		if (!this.pDestroyed)
		{
			if (this.carData.leftKey > 0.01f && this.pVehicleWheelTurn >= -0.99f)
			{
				this.pVehicleWheelTurn -= Time.deltaTime * this.carData.vehicleTurnGain * 0.225f * this.carData.leftKey;
			}
			if (this.carData.rightKey > 0.01f && this.pVehicleWheelTurn <= 0.99f)
			{
				this.pVehicleWheelTurn += Time.deltaTime * this.carData.vehicleTurnGain * 0.225f * this.carData.rightKey;
			}
			if (this.carData.currentSpeed > 20f)
			{
				this.pTrackManager.cameraScript.driveCamShakeIntensity = (this.carData.currentSpeed - 20f) * 0.002f;
				if (this.pTrackManager.cameraScript.driveCamShakeIntensity > 0.1f)
				{
					this.pTrackManager.cameraScript.driveCamShakeIntensity = 0.1f;
				}
			}
			else
			{
				this.pTrackManager.cameraScript.driveCamShakeIntensity = -1f;
			}
		}
		if (this.carData.leftKey <= 0.01f && this.carData.rightKey <= 0.01f && (double)Mathf.Abs(this.pVehicleWheelTurn) > 0.01)
		{
			if (this.pVehicleWheelTurn < 0f)
			{
				this.pVehicleWheelTurn += 3f * Time.deltaTime;
			}
			else
			{
				this.pVehicleWheelTurn -= 3f * Time.deltaTime;
			}
		}
		if (this.pLock)
		{
			if (!this.hovering)
			{
				this.pVehicleWheelSpin -= 6f * Vector3.Dot(this.pRestoreLinVel, this.pCarBody.transform.forward) * Time.deltaTime;
			}
		}
		else
		{
			this.pVehicleWheelSpin -= 60f * this.carData.currentSpeed * Time.deltaTime;
		}
		if (this.pVehicleWheelSpin > 360f)
		{
			this.pVehicleWheelSpin -= 360f;
		}
		else if (this.pVehicleWheelSpin < -360f)
		{
			this.pVehicleWheelSpin += 360f;
		}
		if ((double)this.carData.nitroIntensity > 0.01)
		{
			this.carData.nitroIntensity *= 0.95f;
		}
		if (this.pNitroTimeLeft >= 0f)
		{
			this.pNitroTimeLeft -= deltaTime;
			if (this.pNitroTimeLeft >= 0.5f)
			{
				this.carData.nitroIntensity += ((this.carData.forwardKey <= 0.01f) ? 0f : 0.05f);
			}
			this.carData.nitroIntensity = Mathf.Clamp01(this.carData.nitroIntensity);
			if (this.pNitroTimeLeft < 0f)
			{
				if (this.pNitroSound.enabled)
				{
					this.pNitroSound.Stop();
					this.pNitroSound.enabled = false;
				}
				this.doExhaustNitroParticles(false);
			}
			else
			{
				this.updateExhaustNitroParticles();
			}
		}
		if (!this.pDestroyed)
		{
			float num3 = -1f;
			if (0.1f < this.carData.currentSpeed && this.carData.currentSpeed < 20f && this.carData.forwardKey > 0.01f && this.carData.accelerationGain > 1.5f)
			{
				num3 = 1f - Mathf.Clamp01(this.carData.currentSpeed * 0.025f);
			}
			float num4 = -1f;
			if (Mathf.Abs(this.carData.currentSpeed) > 2.5f && this.carData.driftKey)
			{
				num4 = Mathf.Clamp01(this.pCarBody.rigidbody.angularVelocity.magnitude * 0.75f);
			}
			for (int i = 0; i < 4; i++)
			{
				bool flag = false;
				if (i <= 1)
				{
					this.pWheels[i].transform.localRotation = Quaternion.Euler(new Vector3(this.pVehicleWheelSpin, 180f + this.pVehicleWheelTurn * this.carData.vehicleMaxWheelTurn, 0f));
				}
				else
				{
					this.pWheels[i].transform.localRotation = Quaternion.Euler(new Vector3(this.pVehicleWheelSpin, 180f, 0f));
				}
				Vector3 localPosition = this.pWheels[i].transform.localPosition;
				localPosition.y = this.carData.wheelRadius + Mathf.Min(this.pWheelOffset[i] * 0.5f, this.carData.maximumWheelOffset);
				this.pWheels[i].transform.localPosition = localPosition;
				if (this.pWheelOnGround[i])
				{
					if (i <= 1)
					{
						if (num4 > -0.01f)
						{
						}
					}
					else if (!flag && this.pDriveOnGround == 2 && num2 > 0.1f)
					{
						this.pWheelSmoke_PS[i].enableEmission = false;
					}
					else if (num4 < 0.01f && num3 < 0.01f)
					{
						this.pWheelLastSkidMark[i] = -1;
						this.pWheelSmoke_PS[i].enableEmission = false;
					}
					else if (this.pDriveOnGround == 1)
					{
						this.pWheelSmoke_PS[i].enableEmission = true;
						this.pWheelSmoke_PS[i].emissionRate = Mathf.Clamp(this.carData.currentSpeed * 1f, 25f, 100f);
						this.pWheelSmoke_PS[i].startSize = Mathf.Clamp(this.carData.currentSpeed * 0.25f, 7f, 10f);
					}
					else
					{
						this.pWheelSmoke_PS[i].enableEmission = false;
					}
				}
				else
				{
					this.pWheelLastSkidMark[i] = -1;
					if (i >= 2)
					{
						this.pWheelSmoke_PS[i].enableEmission = false;
					}
				}
			}
			if ((this.carData.forwardKey > 0.01f || this.carData.backwardKey > 0.01f) && this.carData.leftKey <= 0.01f && this.carData.rightKey <= 0.01f && 0.01 < (double)num && (double)num < 0.5 && this.carData.accelerationGain > 1.5f)
			{
				this.pSwingModifier += 0.05f * Time.timeScale;
			}
			else
			{
				this.pSwingModifier -= 0.05f * Time.timeScale;
			}
			this.pSwingModifier = Mathf.Clamp01(this.pSwingModifier);
			if (this.carData.driftKey)
			{
				this.pDriftModifier += 0.05f * Time.timeScale;
			}
			this.pDriftModifier = Mathf.Clamp01(this.pDriftModifier);
		}
		if (!this.carData.driftKey)
		{
			this.pDriftModifier -= 0.05f * Time.timeScale;
		}
		if (!this.pDestroyed)
		{
			this.pEngineStationary.volume = Mathf.Clamp01(1f - num2) * 0.75f;
			this.pEngineTopSpeed.volume = Mathf.Clamp01(num2) * 0.75f;
			float num5;
			if (num2 <= 0.8f)
			{
				num5 = Mathf.Max(0.5f, num2);
			}
			else
			{
				num5 = 0.8f + (num2 - 0.8f) * 2f;
				if (this.pTopSpeedFluctuation < this.pTopSpeedFluctuationGoal)
				{
					this.pTopSpeedFluctuation += 0.009f;
				}
				else
				{
					this.pTopSpeedFluctuation -= 0.009f;
				}
				if (Mathf.Abs(this.pTopSpeedFluctuation - this.pTopSpeedFluctuationGoal) < 0.01f)
				{
					this.pTopSpeedFluctuationGoal = UnityEngine.Random.Range(-0.05f, 0.05f);
				}
				num5 += this.pTopSpeedFluctuation;
			}
			this.pEngineTopSpeed.pitch = num5;
		}
		if (this.pEngineAccelerate != null && this.pEngineAccelerate.isPlaying)
		{
			if (this.carData.forwardKey <= 0.01f)
			{
				this.pEngineAccelerate.Stop();
				this.pEngineAccelerate.enabled = false;
			}
		}
		else if (!this.pDestroyed && this.carData.forwardKey > 0.01f && !this.carData.prevForwardKey)
		{
			this.DoExhaustParticles();
			if (this.pEngineAccelerate == null)
			{
				this.pEngineAccelerate = Scripts.audioManager.PlaySFX("Vehicles/" + this.carData.engineAudio + "StartAccelerate", 1f, 0, base.gameObject);
				this.pEngineAccelerate.priority = 3;
				this.pEngineAccelerate.dopplerLevel = 0f;
				this.pEngineAccelerate.minDistance = 20f;
			}
		}
		if ((this.carData.driftKey || (this.carData.backwardKey > 0.01f && (double)num > 0.05) || (this.carData.forwardKey > 0.01f && (double)num < -0.05)) && !this.pDestroyed && this.hovering && num2 > 0.05f)
		{
			if (this.pSkidSound != null && !this.pSkidSound.enabled)
			{
				this.pSkidSound.enabled = true;
				this.pSkidSound.Play();
			}
		}
		else if (this.pSkidSound != null && this.pSkidSound.enabled)
		{
			this.pSkidSound.enabled = false;
			this.pSkidSound.Stop();
		}
		if (this.hovering)
		{
			if (this.pDriveOnGround == 1)
			{
				if (this.pCarAsphalt != null)
				{
					if (!this.pCarAsphalt.isPlaying)
					{
						this.pCarAsphalt.enabled = true;
						this.pCarAsphalt.Play();
					}
					this.pCarAsphalt.volume = Mathf.Clamp01(num2 * 2f);
				}
			}
		}
		else if (this.pCarAsphalt != null && this.pCarAsphalt.isPlaying)
		{
			this.pCarAsphalt.enabled = false;
			this.pCarAsphalt.Stop();
		}
		if (Data.pausingAllowed)
		{
			if (!this.pDestroyed && !this.hovering && !this.pColliding)
			{
				if (this.pAirTime < -0.5f)
				{
					this.pAirTime = 0f;
				}
				else
				{
					this.pAirTime += deltaTime;
					if (this.pAirTime - deltaTime < 1f && this.pAirTime >= 1f)
					{
						this.pTrackManager.NotifyAirTimeStart();
						this.pEngineStationary.Stop();
						this.pEngineStationary.enabled = false;
						this.pEngineTopSpeed.Stop();
						this.pEngineTopSpeed.enabled = false;
						if (this.pCarAsphalt != null)
						{
							this.pCarAsphalt.enabled = false;
							this.pCarAsphalt.Stop();
						}
					}
				}
			}
			else if ((double)this.pAirTime >= 0.0)
			{
				if (this.pAirTime >= 1f)
				{
					this.pEngineStationary.enabled = true;
					this.pEngineStationary.Play();
					this.pEngineTopSpeed.enabled = true;
					this.pEngineTopSpeed.Play();
					if (this.pCarAsphalt != null)
					{
						this.pCarAsphalt.enabled = true;
						this.pCarAsphalt.Play();
					}
					this.pTrackManager.NotifyAirTimeEnd();
				}
				this.pAirTime = -1f;
			}
			if (!this.pDestroyed && !this.hovering && !this.pColliding)
			{
				if (this.pJumpdistanceBonusPos.y < -999f)
				{
					this.pJumpdistanceBonusPos = base.transform.position;
				}
			}
			else if (this.pJumpdistanceBonusPos.y >= -999f)
			{
				this.pTrackManager.NotifyJumpdistanceEnd(Vector3.Distance(this.pJumpdistanceBonusPos, base.transform.position));
				this.pJumpdistanceBonusPos = new Vector3(-1000f, -1000f, -1000f);
			}
			if (!this.pDestroyed && this.carData.currentSpeed > 65f && this.pNitroTimeLeft > 0f)
			{
				if (this.pSpeedBonusTime < -0.5f)
				{
					this.pSpeedBonusTime = 0f;
				}
				this.pSpeedBonusTime += deltaTime;
			}
			else if ((double)this.pSpeedBonusTime >= 0.0)
			{
				this.pTrackManager.NotifySpeedEnd(this.pSpeedBonusTime);
				this.pSpeedBonusTime = -1f;
			}
			if (!this.pDestroyed && !this.hovering)
			{
				this.pJumpheightBonus = Mathf.Max(base.transform.position.y, this.pJumpheightBonus);
			}
			else if (this.pJumpheightBonus >= 0f)
			{
				this.pTrackManager.NotifyJumpheightEnd(this.pJumpheightBonus);
				this.pJumpheightBonus = -1f;
			}
		}
		if (this.pInvincibilityTime > 0f)
		{
			this.pInvincibilityTime -= deltaTime;
		}
		if (this.pImmuneTime > 0f)
		{
			this.pImmuneTime -= deltaTime;
		}
		if (this.pSparkPSTime > 0f)
		{
			this.pSparkPSTime -= deltaTime;
		}
		if (this.pResetTime > 0f)
		{
			this.pResetTime -= deltaTime;
		}
		if (!this.pDestroyed)
		{
			this.pSlidingTime += ((!this.pColliding) ? (-deltaTime) : deltaTime);
			if (this.pSlidingTime < 0f)
			{
				this.pSlidingTime = 0f;
			}
			if (this.pSlidingTime > 0.3f)
			{
				if (this.pCarSlide != null && !this.pCarSlide.enabled)
				{
					this.pCarSlide.enabled = true;
					this.pCarSlide.Play();
				}
				this.pSlidingTime = 0.3f;
			}
			if (this.pSlidingTime < 0.1f && this.pCarSlide != null && this.pCarSlide.enabled)
			{
				this.pCarSlide.enabled = false;
				this.pCarSlide.Stop();
			}
			if (this.pCarSlide != null && this.pCarSlide.enabled)
			{
				this.pCarSlide.volume = Mathf.Clamp01(num2 * 3f);
			}
		}
		this.carData.prevForwardKey = (this.carData.forwardKey > 0.01f);
		this.carData.prevFireKey = this.carData.fireKey;
		this.carData.prevUseItem = this.carData.useItem;
		this.UpdateSpecific();
	}

	// Token: 0x06000931 RID: 2353 RVA: 0x000473CC File Offset: 0x000455CC
	private void FixedUpdate()
	{
		GameData.lastPlayerVelocity = base.rigidbody.velocity;
		GameData.lastPlayerAngularVelocity = base.rigidbody.angularVelocity;
		if (this.pTrackManager.IsInCrashOrAirTime())
		{
			this.FixedUpdateCrashOrAirTime();
		}
		else if (!this.pDestroyed)
		{
			this.FixedUpdateNormal();
		}
		if (this.pSuperPower != null)
		{
			this.pSuperPower.FixedCarUpdate();
		}
		if (this.pCarBody.transform.position.y < -50f)
		{
			this.SetOnStartPos();
		}
	}

	// Token: 0x06000932 RID: 2354 RVA: 0x00047464 File Offset: 0x00045664
	private void FixedUpdateNormal()
	{
		float num = Vector3.Dot(this.pCarBody.rigidbody.velocity, this.pCarBody.transform.forward);
		this.hovering = false;
		this.pBodyUpVector = this.pCarBody.transform.up;
		this.pBodyDownVector = this.pBodyUpVector * -1f;
		Vector3 angularVelocity = this.pCarBody.rigidbody.angularVelocity;
		float num2 = 0f;
		for (int i = 0; i < 4; i++)
		{
			Vector3 vector = this.pCarBody.transform.TransformPoint(this.pWheelRayPositions[i]);
			RaycastHit raycastHit;
			if (Physics.Raycast(vector - this.pBodyDownVector * this.carData.wheelRadius, this.pBodyDownVector, out raycastHit, this.carData.wheelRadius * 2f + this.carData.vehicleHoverOffset))
			{
				this.pWheelOffset[i] = 2f * this.carData.wheelRadius + this.carData.vehicleHoverOffset - raycastHit.distance;
				float num3 = this.carData.vehicleStrength * this.pWheelOffset[i];
				Vector3 pointVelocity = base.rigidbody.GetPointVelocity(vector);
				num3 += this.carData.vehicleDamping * Vector3.Dot(pointVelocity, this.pBodyDownVector);
				Vector3 force = this.pBodyUpVector * (num3 * this.carData.vehicleMass);
				this.pCarBody.rigidbody.AddForceAtPosition(force, vector);
				this.hovering = true;
				num2 += 0.25f;
				this.pWheelNormals[i] = raycastHit.normal;
				if (raycastHit.rigidbody == null && raycastHit.collider.gameObject.layer != 1)
				{
					this.pWheelOnGround[i] = true;
				}
			}
			else
			{
				this.pWheelOffset[i] = 0f;
				this.pWheelOnGround[i] = false;
			}
		}
		bool flag = true;
		if (this.hovering)
		{
			if (num < 0f && this.carData.forwardKey > 0.01f)
			{
				flag = false;
			}
			else if (num > 0f && this.carData.backwardKey > 0.01f)
			{
				flag = false;
			}
			float num4 = (this.pNitroTimeLeft <= 0f) ? this.carData.vehicleMaxSpeed : this.carData.vehicleMaxNitroSpeed;
			Vector3 vector2;
			if (this.carData.forwardKey > 0.01f)
			{
				float num5 = Mathf.Min(num4 - num, 30f);
				vector2 = new Vector3(0f, 0f, 1f) * (this.carData.forwardKey * num2 * this.carData.vehicleMass * num5 * this.carData.accelerationGain);
				this.pCarBody.rigidbody.AddRelativeForce(vector2);
			}
			if (this.carData.backwardKey > 0.01f)
			{
				float num5 = -num4 * 0.5f - num;
				vector2 = new Vector3(0f, 0f, 1f) * (this.carData.backwardKey * num2 * this.carData.vehicleMass * num5 * this.carData.decelerationGain);
				this.pCarBody.rigidbody.AddRelativeForce(vector2);
			}
			float num6 = this.carData.vehicleTurnGain;
			if (Mathf.Abs(num) <= num4 * 0.225f)
			{
				float num7 = Mathf.Abs(num) / (num4 * 0.225f);
				num6 = this.carData.vehicleTurnGain * num7;
				if (num6 < 0.05f)
				{
					num6 = 0f;
				}
			}
			if (this.carData.driftKey)
			{
				float num8 = this.carData.currentSpeedPerc;
				if ((double)num8 > 0.0)
				{
					if ((double)num8 > 0.6)
					{
						num8 = 1f;
					}
					else
					{
						num8 *= 1.6667f;
					}
					num6 *= 5f * num8;
				}
			}
			if (flag)
			{
				if (this.carData.leftKey > 0.01f)
				{
					vector2 = new Vector3(0f, 1f, 0f) * (this.carData.leftKey * -num6 * this.carData.vehicleMass * num2);
					if (num < 0f)
					{
						vector2 = -vector2;
					}
					this.pCarBody.rigidbody.AddRelativeTorque(vector2);
				}
				if (this.carData.rightKey > 0.01f)
				{
					vector2 = new Vector3(0f, 1f, 0f) * (this.carData.rightKey * num6 * this.carData.vehicleMass * num2);
					if (num < 0f)
					{
						vector2 = -vector2;
					}
					this.pCarBody.rigidbody.AddRelativeTorque(vector2);
				}
			}
			if (this.pSwingModifier > 0.01f)
			{
				vector2 = new Vector3(0f, Mathf.Sin(Time.time * 10f) * this.carData.vehicleMass * 4f * this.pSwingModifier, 0f);
				this.pCarBody.rigidbody.AddRelativeTorque(vector2);
			}
			float magnitude = angularVelocity.magnitude;
			if (magnitude > 0f)
			{
				float num9 = 1f;
				float num10 = Mathf.Abs(num);
				if (num10 <= num4 * 7f)
				{
					num9 = Mathf.Abs(num) / (num4 * 0.7f);
				}
				float num11 = 0.25f + num6 * num9;
				if (num11 > num6)
				{
					num11 = num6;
				}
				vector2 = Vector3.up * (Vector3.Dot(angularVelocity, this.pBodyUpVector) * (-num11 * this.carData.vehicleMass * num2));
				this.pCarBody.rigidbody.AddRelativeTorque(vector2);
				if (num10 < 20f)
				{
					this.pCarBody.rigidbody.angularVelocity *= 1f - (0.1f - num10 * 0.005f);
				}
				if (num10 < 5f)
				{
					this.pCarBody.rigidbody.angularVelocity *= 1f - (0.2f - num10 * 0.04f);
				}
			}
			if (this.carData.driftKey)
			{
				this.pVehicleSlip = true;
				this.pVehicleAdjGrip = this.carData.vehicleDriftGrip;
			}
			if (this.pVehicleSlip)
			{
				if (this.pVehicleAdjGrip < this.carData.vehicleGrip)
				{
					this.pVehicleAdjGrip += 0.1f;
				}
				if (this.pVehicleAdjGrip > this.carData.vehicleGrip)
				{
					this.pVehicleAdjGrip = this.carData.vehicleGrip;
					this.pVehicleSlip = false;
				}
			}
			float num12 = Vector3.Dot(this.pCarBody.transform.right, this.pCarBody.rigidbody.velocity);
			vector2 = new Vector3(1f, 0f, 0f) * (-num12 * this.carData.vehicleMass * this.pVehicleAdjGrip * num2);
			this.pCarBody.rigidbody.AddRelativeForce(vector2);
			if (this.carData.forwardKey <= 0.01f)
			{
				vector2 = new Vector3(0f, 0f, -1f) * (num * this.carData.vehicleDrag * this.carData.vehicleMass);
				this.pCarBody.rigidbody.AddRelativeForce(vector2);
			}
			this.pUpsideDownTime = 0f;
		}
		else if (num < 2f)
		{
			if (Vector3.Dot(this.pBodyUpVector, Vector3.up) < 0.3f)
			{
				this.pUpsideDownTime += Time.fixedDeltaTime;
				if (this.pUpsideDownTime > 2f)
				{
					this.ResetCar();
					this.pUpsideDownTime = 0f;
				}
			}
			else
			{
				this.pUpsideDownTime = 0f;
			}
		}
		if (this.carData.resetKey && this.pResetTime <= 0f)
		{
			this.ResetCar();
		}
		if (!Data.raceInProgress)
		{
			this.pCarBody.rigidbody.velocity = new Vector3(0f, this.pCarBody.rigidbody.velocity.y, 0f);
		}
		this.pColliding = false;
		if (this.pItem != null)
		{
			this.pItem.FixedUpdateSpecific();
		}
		this.FixedUpdateSpecific();
	}

	// Token: 0x06000933 RID: 2355 RVA: 0x00047D60 File Offset: 0x00045F60
	private void FixedUpdateCrashOrAirTime()
	{
		if (this.carData.leftKey > 0.01f || this.carData.rightKey > 0.01f || this.carData.forwardKey > 0.01f || this.carData.backwardKey > 0.01f)
		{
			int num = ((this.carData.leftKey <= 0.01f) ? 0 : 1) + ((this.carData.rightKey <= 0.01f) ? 0 : 2) + ((this.carData.forwardKey <= 0.01f) ? 0 : 4) + ((this.carData.backwardKey <= 0.01f) ? 0 : 8);
			if (this.pMapKeysToAngle[num] != 0)
			{
				base.rigidbody.velocity = base.rigidbody.velocity * 0.995f;
				float num2 = this.pTrackManager.cameraScript.GetCurrentYAngle() + (float)this.pMapKeysToAngle[num];
				float f = num2 * 0.017453292f;
				Vector3 to = new Vector3(Mathf.Sin(f) * this.carData.vehicleMaxSpeed, base.rigidbody.velocity.y, Mathf.Cos(f) * this.carData.vehicleMaxSpeed);
				base.rigidbody.velocity = Vector3.Lerp(base.rigidbody.velocity, to, 0.06f * this.pAirControlStrength);
			}
		}
		if (!this.pDestroyed)
		{
			this.FixedUpdateSpecific();
		}
	}

	// Token: 0x06000934 RID: 2356 RVA: 0x00047F00 File Offset: 0x00046100
	private void OnCollisionEnter(Collision aCollision)
	{
		int layer = aCollision.gameObject.layer;
		if (layer == GameData.trafficLayer || layer == GameData.damagedTrafficLayer)
		{
			return;
		}
		if (aCollision.gameObject.name.Contains("Ramp"))
		{
			return;
		}
		float magnitude = aCollision.relativeVelocity.magnitude;
		if (!aCollision.gameObject.name.Contains("Street") && (double)magnitude > 5.0 && this.pInvincibilityTime <= 0f && this.pImmuneTime <= 0f && !this.pLock)
		{
			if (!this.pDestroyed)
			{
				float num = magnitude * 0.02f * this.carData.armorInv;
				if (aCollision.gameObject.GetComponent<DestructibleScript>() == null)
				{
					this.AddDamage(num);
				}
				if (num > 0.25f)
				{
					this.PlayBigCarBump(Mathf.Min(num, 1f));
					Scripts.trackScript.trackManager.cameraScript.AddCameraShake(new CameraShake(0.25f, 3f, 0.25f));
				}
				else
				{
					this.PlaySmallCarBump(Mathf.Min(magnitude * 0.1f, 1f));
					Scripts.trackScript.trackManager.cameraScript.AddCameraShake(new CameraShake(0.25f, 0.75f, 0.25f));
				}
			}
			this.pImmuneTime = 0.5f;
		}
	}

	// Token: 0x06000935 RID: 2357 RVA: 0x00048080 File Offset: 0x00046280
	public void Damage(DamageInfo aDamageInfo)
	{
		this.AddDamage(aDamageInfo.damage * this.carData.armorInv);
		if (this.pInvincibilityTime <= 0f && (this.pSuperPower == null || this.pSuperPower.name != "Golden") && !GameData.godMode)
		{
			base.rigidbody.AddForceAtPosition(aDamageInfo.hitDir * 1800000f * aDamageInfo.damage, aDamageInfo.hitPos);
		}
	}

	// Token: 0x06000936 RID: 2358 RVA: 0x00048110 File Offset: 0x00046310
	private void AddDamage(float aDamageIncrease)
	{
		if (this.pDestroyed)
		{
			return;
		}
		if (Data.cheats && GameData.godMode)
		{
			return;
		}
		if (this.pInvincibilityTime > 0f)
		{
			return;
		}
		if (this.pSuperPower != null && this.pSuperPower.name == "Golden")
		{
			aDamageIncrease *= 0.2f;
		}
		this.damage += aDamageIncrease;
		if (this.damage > 0.75f)
		{
			this.pHoodSmoke.Play();
		}
		if (this.damage >= 1f)
		{
			this.ExplodeCar();
		}
		else
		{
			this.UpdateCarDamageVisuals();
		}
	}

	// Token: 0x06000937 RID: 2359 RVA: 0x000481C8 File Offset: 0x000463C8
	private void OnCollisionStay(Collision aCollision)
	{
		this.pColliding = true;
		if (aCollision.gameObject.name.Contains("Ramp"))
		{
			return;
		}
		if (aCollision.gameObject.name.Contains("Street"))
		{
			if (this.pDestroyed || Vector3.Dot(this.pBodyUpVector, Vector3.up) < 0.3f)
			{
				this.pCarBody.rigidbody.velocity = this.pCarBody.rigidbody.velocity * 0.985f;
			}
		}
		else if (this.pSparkPSTime <= 0f && aCollision.contacts.Length > 0)
		{
			this.pSparkPSTime = 0.15f;
			GameObject gameObject = Loader.LoadGameObject("Shared", "Effects/Particles/Spark_PS");
			ParticleSystem component = gameObject.GetComponent<ParticleSystem>();
			gameObject.transform.position = aCollision.contacts[0].point;
			component.Play();
			UnityEngine.Object.Destroy(component.gameObject, component.duration);
		}
	}

	// Token: 0x06000938 RID: 2360 RVA: 0x000482D8 File Offset: 0x000464D8
	private void ResetCar(Vector3 aPosition)
	{
		this.pCarBody.transform.position = aPosition;
		this.pCarBody.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
		this.pCarBody.rigidbody.velocity = new Vector3(0f, 0f, 0f);
		this.pCarBody.rigidbody.angularVelocity = new Vector3(0f, 0f, 0f);
	}

	// Token: 0x06000939 RID: 2361 RVA: 0x00048364 File Offset: 0x00046564
	private void ResetCar()
	{
		this.pCarBody.transform.position = this.pCarBody.transform.position + new Vector3(0f, 2f, 0f);
		this.pCarBody.transform.rotation = Quaternion.Euler(0f, this.pCarBody.transform.rotation.eulerAngles.y, 0f);
		this.pCarBody.rigidbody.velocity = new Vector3(0f, 0f, 0f);
		this.pCarBody.rigidbody.angularVelocity = new Vector3(0f, 0f, 0f);
		this.pTrackManager.NotifyRespawn(this);
		this.pResetTime = 1f;
	}

	// Token: 0x0600093A RID: 2362 RVA: 0x00048448 File Offset: 0x00046648
	public float GetSpeed()
	{
		return this.carData.currentSpeed;
	}

	// Token: 0x0600093B RID: 2363 RVA: 0x00048458 File Offset: 0x00046658
	public float GetMaxSpeed()
	{
		return this.carData.vehicleMaxSpeed;
	}

	// Token: 0x0600093C RID: 2364 RVA: 0x00048468 File Offset: 0x00046668
	public float GetMaxSpeedInv()
	{
		return this.carData.vehicleMaxSpeedInv;
	}

	// Token: 0x0600093D RID: 2365 RVA: 0x00048478 File Offset: 0x00046678
	public float GetDriftParam()
	{
		return this.pDriftModifier;
	}

	// Token: 0x0600093E RID: 2366 RVA: 0x00048480 File Offset: 0x00046680
	public float GetWheelTurn()
	{
		return this.pVehicleWheelTurn;
	}

	// Token: 0x0600093F RID: 2367 RVA: 0x00048488 File Offset: 0x00046688
	public void AddNitro()
	{
		if (!this.pDestroyed && this.pNitroTimeLeft < this.pNitroDuration)
		{
			this.pNitroTimeLeft = this.pNitroDuration;
			this.doExhaustNitroParticles(true);
			Scripts.trackScript.interfaceScript.interfacePanelScript.Nitro();
			this.pTrackManager.NotifyNitroStart(this);
			this.pNitroSound.enabled = true;
			this.pNitroSound.Play();
		}
	}

	// Token: 0x06000940 RID: 2368 RVA: 0x000484FC File Offset: 0x000466FC
	public bool NitroActive()
	{
		return this.pNitroTimeLeft > 0f;
	}

	// Token: 0x06000941 RID: 2369 RVA: 0x0004850C File Offset: 0x0004670C
	public float GetTurnGain()
	{
		return this.carData.vehicleTurnGain;
	}

	// Token: 0x06000942 RID: 2370 RVA: 0x0004851C File Offset: 0x0004671C
	public void EndBonuses()
	{
		if (this.pJumpdistanceBonusPos.y >= -999f)
		{
			this.pTrackManager.NotifyJumpdistanceEnd(Vector3.Distance(this.pJumpdistanceBonusPos, base.transform.position));
			this.pJumpdistanceBonusPos = new Vector3(-1000f, -1000f, -1000f);
		}
		if ((double)this.pSpeedBonusTime >= 0.0)
		{
			this.pTrackManager.NotifySpeedEnd(this.pSpeedBonusTime);
			this.pSpeedBonusTime = -1f;
		}
		if (this.pJumpheightBonus >= 0f)
		{
			this.pTrackManager.NotifyJumpheightEnd(this.pJumpheightBonus);
			this.pJumpheightBonus = -1f;
		}
	}

	// Token: 0x06000943 RID: 2371 RVA: 0x000485D8 File Offset: 0x000467D8
	public void PlaySound(string aSoundName)
	{
		AudioSource audioSource = Scripts.audioManager.PlaySFX(aSoundName);
		if (audioSource != null)
		{
			audioSource.priority = 5;
			audioSource.minDistance = 20f;
		}
	}

	// Token: 0x06000944 RID: 2372 RVA: 0x00048610 File Offset: 0x00046810
	public void SetRimColor(Color aColor)
	{
		base.renderer.sharedMaterial.SetColor("_RimColor", aColor);
	}

	// Token: 0x06000945 RID: 2373 RVA: 0x00048628 File Offset: 0x00046828
	public void ResetRimColor()
	{
		base.renderer.sharedMaterial.SetColor("_RimColor", new Color(0f, 0f, 0f, 1f));
	}

	// Token: 0x06000946 RID: 2374 RVA: 0x00048664 File Offset: 0x00046864
	public void ExplodeCar()
	{
		this.ExplodeCar(false);
	}

	// Token: 0x06000947 RID: 2375 RVA: 0x00048670 File Offset: 0x00046870
	public void ExplodeCar(bool aRepress)
	{
		this.pDestroyed = true;
		Texture texture = Loader.LoadTexture("Shared", "Effects/CarDamage/CarExploded_Texture");
		base.renderer.sharedMaterial.SetTexture("_Diffuse", texture);
		if (this.pSuperPower != null)
		{
			this.pSuperPower.DeActivate();
		}
		this.pTrackManager.cameraScript.driveCamShakeIntensity = -1f;
		GameObject gameObject = null;
		Vector3 extents = base.gameObject.renderer.bounds.extents;
		if (!aRepress)
		{
			float x = 0f;
			float z = 0f;
			gameObject = Loader.LoadGameObject("Shared", "CarExplosions/CarExplosionDetonator" + this.detonatorLevel);
			gameObject.transform.position = base.transform.position + new Vector3(x, extents.y, z);
			foreach (object obj in gameObject.transform)
			{
				Transform transform = (Transform)obj;
				if (transform.gameObject.name.Contains("Burst"))
				{
					ExplosionRateScript explosionRateScript = transform.gameObject.AddComponent<ExplosionRateScript>();
					explosionRateScript.attachedObject = base.gameObject;
					explosionRateScript.duration = Data.Shared["Misc"].d["CrashTimeDuration"].f;
				}
			}
			gameObject.transform.parent = base.transform;
		}
		base.rigidbody.AddForce(new Vector3(0f, base.rigidbody.mass * 1125f, 0f));
		base.rigidbody.AddTorque(UnityEngine.Random.onUnitSphere * 2000f, ForceMode.Impulse);
		if (!aRepress)
		{
			GameObject gameObject2 = Loader.LoadGameObject("Shared", "CarExplosions/DetonatorShockwave" + this.detonatorLevel + "_PS");
			gameObject2.transform.position = base.transform.position + new Vector3(0f, 0.1f, 0f);
			ParticleSystem component = gameObject2.GetComponent<ParticleSystem>();
			component.Play();
			UnityEngine.Object.Destroy(component.gameObject, component.duration);
		}
		this.pEngineStationary.volume = 0f;
		this.pEngineTopSpeed.volume = 0f;
		this.pSlidingTime = 0f;
		if (this.pCarSlide != null)
		{
			this.pCarSlide.Stop();
			this.pCarSlide.enabled = false;
		}
		if (this.pCarGlass != null)
		{
			this.pCarGlass.renderer.enabled = false;
		}
		this.pHoodSmoke.Stop();
		this.pNitroTimeLeft = -1f;
		this.doExhaustNitroParticles(false);
		if (this.pNitroSound.enabled)
		{
			this.pNitroSound.Stop();
			this.pNitroSound.enabled = false;
		}
		this.pInvincibilityTime = -1f;
		this.EndBonuses();
		this.pDetachParts = true;
		this.pItem.ExplodeCarSpecific();
		this.ExplodeCarSpecific();
		Scripts.trackScript.trackManager.cameraScript.AddCameraShake(new CameraShake(1f, 5f + (float)this.detonatorLevel, 0.5f));
		this.pTrackManager.NotifyCarExploded(this);
	}

	// Token: 0x06000948 RID: 2376 RVA: 0x00048A08 File Offset: 0x00046C08
	private void DetachVehicleParts()
	{
		this.pDetachParts = false;
		foreach (WeaponScript weaponScript in this.pWeapons)
		{
			weaponScript.Detach();
		}
		Physics.IgnoreLayerCollision(GameData.carPartLayer, GameData.trafficLayer);
		if (this.carData.carName != "Tank")
		{
			for (int i = 0; i < 4; i++)
			{
				GameObject gameObject = this.pWheels[i];
				if (!(gameObject.transform.parent == null))
				{
					gameObject.transform.parent = null;
					gameObject.AddComponent<MeshCollider>();
					(gameObject.collider as MeshCollider).convex = true;
					gameObject.collider.material = new PhysicMaterial();
					gameObject.collider.material.staticFriction = 1f;
					gameObject.collider.material.dynamicFriction = 1f;
					gameObject.collider.material.frictionCombine = PhysicMaterialCombine.Maximum;
					gameObject.collider.material.bounciness = 0.8f;
					gameObject.collider.material.bounceCombine = PhysicMaterialCombine.Maximum;
					gameObject.AddComponent<Rigidbody>();
					gameObject.rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
					gameObject.rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
					gameObject.rigidbody.mass = 20f;
					gameObject.rigidbody.velocity = base.rigidbody.velocity * 0.75f + new Vector3(0f, 20f, 0f) + UnityEngine.Random.insideUnitSphere * 15f;
					gameObject.rigidbody.AddTorque(UnityEngine.Random.onUnitSphere * 5000f, ForceMode.Impulse);
					if (i >= 2)
					{
						this.pWheelSmoke_PS[i].enableEmission = false;
					}
				}
			}
		}
		foreach (FallOffPartScript fallOffPartScript in base.GetComponentsInChildren<FallOffPartScript>())
		{
			fallOffPartScript.DisconnectFromVehicle(true);
			fallOffPartScript.gameObject.rigidbody.velocity = base.rigidbody.velocity + UnityEngine.Random.insideUnitSphere * 15f;
			fallOffPartScript.gameObject.rigidbody.angularVelocity = UnityEngine.Random.insideUnitSphere * 5f;
			GameObject gameObject2 = Loader.LoadGameObject("Shared", "CarExplosions/CarDebrisExplosion_PS");
			gameObject2.transform.position = fallOffPartScript.gameObject.renderer.bounds.center;
			gameObject2.transform.parent = fallOffPartScript.transform;
			ExplosionRateScript explosionRateScript = gameObject2.AddComponent<ExplosionRateScript>();
			explosionRateScript.attachedObject = fallOffPartScript.gameObject;
			explosionRateScript.duration = Data.Shared["Misc"].d["CrashTimeDuration"].f;
		}
	}

	// Token: 0x06000949 RID: 2377 RVA: 0x00048D28 File Offset: 0x00046F28
	private void RestoreCar(bool aResetOnTrack)
	{
		this.damage = 0f;
		this.UpdateCarDamageVisuals();
		foreach (GameObject obj in this.pDestrExplosionPSList)
		{
			UnityEngine.Object.Destroy(obj);
		}
		foreach (GameObject obj2 in this.pDestrFirePSList)
		{
			UnityEngine.Object.Destroy(obj2);
		}
		this.pDestrExplosionPSList.Clear();
		this.pDestrExplosionPSPosList.Clear();
		this.pDestrFirePSList.Clear();
		this.pDestrFirePSPosList.Clear();
		base.renderer.sharedMaterial.SetTexture("_Diffuse", this.pDiffuseTexture);
		for (int i = 0; i < 4; i++)
		{
			GameObject gameObject = this.pWheels[i];
			MeshCollider component = gameObject.GetComponent<MeshCollider>();
			if (component != null)
			{
				UnityEngine.Object.Destroy(component);
			}
			UnityEngine.Object.Destroy(gameObject.GetComponent<Rigidbody>());
			gameObject.transform.parent = base.transform;
			gameObject.transform.localPosition = this.pWheelLocalPositions[i];
		}
		if (this.pCarGlass != null)
		{
			this.pCarGlass.renderer.enabled = true;
		}
		this.pDestroyed = false;
		if (aResetOnTrack)
		{
			this.ResetCar();
		}
	}

	// Token: 0x0600094A RID: 2378 RVA: 0x00048EF0 File Offset: 0x000470F0
	private void SetOnStartPos()
	{
		SafeHouseData currentSafeHousePosition = SafeHousePosition.GetCurrentSafeHousePosition();
		this.pCarBody.transform.position = currentSafeHousePosition.position;
		this.pCarBody.transform.rotation = Quaternion.Euler(0f, currentSafeHousePosition.yAngle, 0f);
		this.pCarBody.rigidbody.velocity = new Vector3(0f, 0f, 0f);
		this.pCarBody.rigidbody.angularVelocity = new Vector3(0f, 0f, 0f);
	}

	// Token: 0x0600094B RID: 2379 RVA: 0x00048F88 File Offset: 0x00047188
	private void PlaySmallCarBump(float aVolume)
	{
		AudioSource audioSource = Scripts.audioManager.PlaySFX("Effects/Clunk" + UnityEngine.Random.Range(1, 4), aVolume);
		if (audioSource != null)
		{
			audioSource.minDistance = 20f;
		}
	}

	// Token: 0x0600094C RID: 2380 RVA: 0x00048FD0 File Offset: 0x000471D0
	private void PlayBigCarBump(float aVolume)
	{
		AudioSource audioSource = Scripts.audioManager.PlaySFX("Effects/Crash" + UnityEngine.Random.Range(1, 4), aVolume);
		if (audioSource != null)
		{
			audioSource.minDistance = 20f;
		}
	}

	// Token: 0x0600094D RID: 2381 RVA: 0x00049018 File Offset: 0x00047218
	private void DoExhaustParticles()
	{
		if (this.pExhaustPSList.Count > 0 && !this.pExhaustNitroPSList[0].isPlaying)
		{
			foreach (ParticleSystem particleSystem in this.pExhaustPSList)
			{
				particleSystem.Play();
			}
		}
	}

	// Token: 0x0600094E RID: 2382 RVA: 0x000490A4 File Offset: 0x000472A4
	private void doExhaustNitroParticles(bool aStart)
	{
		if (aStart)
		{
			if (this.pExhaustNitroPSList.Count > 0)
			{
				foreach (ParticleSystem particleSystem in this.pExhaustNitroPSList)
				{
					particleSystem.Play();
				}
			}
		}
		else if (this.pExhaustNitroPSList.Count > 0 && this.pExhaustNitroPSList[0].isPlaying)
		{
			foreach (ParticleSystem particleSystem2 in this.pExhaustNitroPSList)
			{
				particleSystem2.Stop();
			}
		}
	}

	// Token: 0x0600094F RID: 2383 RVA: 0x000491A0 File Offset: 0x000473A0
	private void updateExhaustNitroParticles()
	{
		if (this.pExhaustNitroPSList.Count > 0 && this.pExhaustNitroPSList[0].isPlaying)
		{
			foreach (ParticleSystem particleSystem in this.pExhaustNitroPSList)
			{
				particleSystem.startSpeed = this.pNitroStartSpeed + this.carData.nitroIntensity;
				particleSystem.startSize = this.pNitroStartSize + this.carData.nitroIntensity + UnityEngine.Random.value * 0.6f;
			}
		}
	}

	// Token: 0x06000950 RID: 2384 RVA: 0x00049264 File Offset: 0x00047464
	private void FireWeaponsDown()
	{
		foreach (WeaponScript weaponScript in this.pWeapons)
		{
			weaponScript.FireDown(this.carData.currentSpeed);
		}
	}

	// Token: 0x06000951 RID: 2385 RVA: 0x000492D4 File Offset: 0x000474D4
	private void FireWeaponsHold()
	{
		foreach (WeaponScript weaponScript in this.pWeapons)
		{
			weaponScript.FireHold(this.carData.currentSpeed);
		}
	}

	// Token: 0x06000952 RID: 2386 RVA: 0x00049344 File Offset: 0x00047544
	private void FireWeaponsUp()
	{
		foreach (WeaponScript weaponScript in this.pWeapons)
		{
			weaponScript.FireUp(this.carData.currentSpeed);
		}
	}

	// Token: 0x06000953 RID: 2387 RVA: 0x000493B4 File Offset: 0x000475B4
	private void UpdateCarDamageVisuals()
	{
		base.renderer.sharedMaterial.SetFloat("_Damage", this.damage);
		int num;
		if (this.damage < 0.4f)
		{
			num = 0;
		}
		else if (this.damage < 0.6f)
		{
			num = 1;
		}
		else if (this.damage < 0.8f)
		{
			num = 2;
		}
		else
		{
			num = 3;
		}
		if (num != 0)
		{
			foreach (DicEntry dicEntry in this.carData.damageDebrisDict["DamageDebris" + num].l)
			{
				FallOffPartScript[] componentsInChildren = base.transform.GetComponentsInChildren<FallOffPartScript>();
				foreach (FallOffPartScript fallOffPartScript in componentsInChildren)
				{
					if (dicEntry.s == fallOffPartScript.name)
					{
						if (dicEntry.s.Contains("Glass"))
						{
							UnityEngine.Object.Destroy(fallOffPartScript.gameObject);
						}
						else
						{
							fallOffPartScript.DisconnectFromVehicle(true);
							fallOffPartScript.gameObject.rigidbody.velocity = base.rigidbody.velocity + new Vector3(0f, 15f, 0f) + UnityEngine.Random.insideUnitSphere * 15f;
							fallOffPartScript.gameObject.rigidbody.angularVelocity = UnityEngine.Random.insideUnitSphere * 5f;
						}
					}
				}
			}
		}
	}

	// Token: 0x06000954 RID: 2388 RVA: 0x0004957C File Offset: 0x0004777C
	private void OnDestroy()
	{
		if (this.pRealTimeShadowProjector != null)
		{
			UnityEngine.Object.Destroy(this.pRealTimeShadowProjector);
		}
	}

	// Token: 0x06000955 RID: 2389 RVA: 0x0004959C File Offset: 0x0004779C
	protected void ShareMaterialsToChildrenGeneric()
	{
		foreach (Transform transform in base.GetComponentsInChildren<Transform>())
		{
			if (!transform.name.Contains("Wheel_") && !transform.name.Contains("_PS") && !transform.name.Contains("_Dummy") && !transform.name.Contains("Track") && !transform.name.Contains("MustangDash") && !transform.name.Contains("MustangGold") && transform.renderer != null)
			{
				transform.renderer.sharedMaterial = base.renderer.sharedMaterial;
			}
		}
	}

	// Token: 0x04000984 RID: 2436
	public CarData carData;

	// Token: 0x04000985 RID: 2437
	public float pickUpRangeSqr = 9f;

	// Token: 0x04000986 RID: 2438
	public int detonatorLevel = 1;

	// Token: 0x04000987 RID: 2439
	public bool hovering;

	// Token: 0x04000988 RID: 2440
	public bool cashMagnet;

	// Token: 0x04000989 RID: 2441
	public float damage;

	// Token: 0x0400098A RID: 2442
	private float pImmuneTime;

	// Token: 0x0400098B RID: 2443
	private float pInvincibilityTime;

	// Token: 0x0400098C RID: 2444
	protected TrackManager pTrackManager;

	// Token: 0x0400098D RID: 2445
	protected List<GameObject> pWheels;

	// Token: 0x0400098E RID: 2446
	private List<Vector3> pWheelRayPositions;

	// Token: 0x0400098F RID: 2447
	private List<Vector3> pWheelLocalPositions;

	// Token: 0x04000990 RID: 2448
	private List<float> pWheelOffset;

	// Token: 0x04000991 RID: 2449
	private GameObject pCarBody;

	// Token: 0x04000992 RID: 2450
	private GameObject pCarGlass;

	// Token: 0x04000993 RID: 2451
	private GameObject pSmokeDummy;

	// Token: 0x04000994 RID: 2452
	private Vector3 pBodyDownVector;

	// Token: 0x04000995 RID: 2453
	private Vector3 pBodyUpVector;

	// Token: 0x04000996 RID: 2454
	private Vector3 pCOM;

	// Token: 0x04000997 RID: 2455
	private bool pVehicleSlip;

	// Token: 0x04000998 RID: 2456
	private float pVehicleAdjGrip = 1f;

	// Token: 0x04000999 RID: 2457
	protected float pVehicleWheelTurn;

	// Token: 0x0400099A RID: 2458
	private float pVehicleWheelSpin;

	// Token: 0x0400099B RID: 2459
	private int pDriveOnGround = 1;

	// Token: 0x0400099C RID: 2460
	public float pNitroTimeLeft = -1f;

	// Token: 0x0400099D RID: 2461
	private float pNitroStartSize = -1f;

	// Token: 0x0400099E RID: 2462
	private float pNitroStartSpeed = -1f;

	// Token: 0x0400099F RID: 2463
	private float pNitroDuration;

	// Token: 0x040009A0 RID: 2464
	private float pAirControlStrength;

	// Token: 0x040009A1 RID: 2465
	protected List<ParticleSystem> pExhaustPSList;

	// Token: 0x040009A2 RID: 2466
	private List<ParticleSystem> pExhaustNitroPSList;

	// Token: 0x040009A3 RID: 2467
	private AudioSource pEngineStationary;

	// Token: 0x040009A4 RID: 2468
	private AudioSource pEngineTopSpeed;

	// Token: 0x040009A5 RID: 2469
	private AudioSource pEngineAccelerate;

	// Token: 0x040009A6 RID: 2470
	private float pTopSpeedFluctuation;

	// Token: 0x040009A7 RID: 2471
	private float pTopSpeedFluctuationGoal;

	// Token: 0x040009A8 RID: 2472
	private AudioSource pSkidSound;

	// Token: 0x040009A9 RID: 2473
	private AudioSource pCarAsphalt;

	// Token: 0x040009AA RID: 2474
	private AudioSource pCarSlide;

	// Token: 0x040009AB RID: 2475
	private AudioSource pNitroSound;

	// Token: 0x040009AC RID: 2476
	private float pSlidingTime;

	// Token: 0x040009AD RID: 2477
	private int[] pWheelLastSkidMark;

	// Token: 0x040009AE RID: 2478
	private bool[] pWheelOnGround;

	// Token: 0x040009AF RID: 2479
	private Vector3[] pWheelNormals;

	// Token: 0x040009B0 RID: 2480
	private ParticleSystem[] pWheelSmoke_PS;

	// Token: 0x040009B1 RID: 2481
	private float pSparkPSTime = -0.1f;

	// Token: 0x040009B2 RID: 2482
	private float pResetTime = -0.1f;

	// Token: 0x040009B3 RID: 2483
	private float pUpsideDownTime;

	// Token: 0x040009B4 RID: 2484
	private float pSwingModifier;

	// Token: 0x040009B5 RID: 2485
	private float pDriftModifier;

	// Token: 0x040009B6 RID: 2486
	private RealTimeShadowProjector pRealTimeShadowProjector;

	// Token: 0x040009B7 RID: 2487
	private float pAirTime = -1f;

	// Token: 0x040009B8 RID: 2488
	private Vector3 pJumpdistanceBonusPos = new Vector3(-1000f, -1000f, -1000f);

	// Token: 0x040009B9 RID: 2489
	private float pSpeedBonusTime = -1f;

	// Token: 0x040009BA RID: 2490
	private float pJumpheightBonus = -1f;

	// Token: 0x040009BB RID: 2491
	private bool pColliding;

	// Token: 0x040009BC RID: 2492
	protected bool pDestroyed;

	// Token: 0x040009BD RID: 2493
	private Texture pDiffuseTexture;

	// Token: 0x040009BE RID: 2494
	private List<GameObject> pDestrExplosionPSList;

	// Token: 0x040009BF RID: 2495
	private List<Vector3> pDestrExplosionPSPosList;

	// Token: 0x040009C0 RID: 2496
	private List<GameObject> pDestrFirePSList;

	// Token: 0x040009C1 RID: 2497
	private List<Vector3> pDestrFirePSPosList;

	// Token: 0x040009C2 RID: 2498
	private ParticleSystem pHoodSmoke;

	// Token: 0x040009C3 RID: 2499
	private int[] pMapKeysToAngle = new int[]
	{
		0,
		90,
		-90,
		0,
		180,
		135,
		-135,
		0,
		1,
		45,
		-45,
		0,
		0,
		0,
		0,
		0
	};

	// Token: 0x040009C4 RID: 2500
	private bool pLock;

	// Token: 0x040009C5 RID: 2501
	private bool pDetachParts;

	// Token: 0x040009C6 RID: 2502
	private List<WeaponScript> pWeapons;

	// Token: 0x040009C7 RID: 2503
	private ItemBase pItem;

	// Token: 0x040009C8 RID: 2504
	private SuperPowerBase pSuperPower;

	// Token: 0x040009C9 RID: 2505
	private SuperPowerBase pEquippedSuperPower;

	// Token: 0x040009CA RID: 2506
	private SuperPowerBase pPickUpSuperPower;

	// Token: 0x040009CB RID: 2507
	private Vector3 pRestoreLinVel;

	// Token: 0x040009CC RID: 2508
	private Vector3 pRestoreAngVel;

	// Token: 0x0200013D RID: 317
	private enum pGroundType
	{
		// Token: 0x040009D0 RID: 2512
		ASPHALT = 1,
		// Token: 0x040009D1 RID: 2513
		SAND
	}
}
