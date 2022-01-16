using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200013B RID: 315
public class CarData
{
	// Token: 0x0600090B RID: 2315 RVA: 0x00043CE8 File Offset: 0x00041EE8
	public CarData(string aCarInstanceName, string aCarName)
	{
		GameData.vehicles[aCarInstanceName] = this;
		this.carName = aCarName;
		Dictionary<string, DicEntry> d = Data.Shared["Car"].d[aCarName].d;
		Dictionary<string, DicEntry> d2 = Data.Shared["CarSetting"].d[d["Setting"].s].d;
		Dictionary<string, DicEntry> d3 = Data.Shared["CarWeapons"].d[d["Setting"].s].d;
		this.assetBundle = d["AssetBundle"].s;
		this.modelName = d["Model"].s;
		this.skin = d["Skin"].i;
		this.engineAudio = d2["EngineAudio"].s;
		this.projectorSize = d2["ProjectorSize"].f;
		this.driftEnabled = d2["DriftEnabled"].b;
		this.vehicleDrag = d2["Drag"].f;
		this.vehicleGrip = d2["Grip"].f;
		this.vehicleDriftGrip = d2["DriftGrip"].f;
		this.accelerationGain = d2["AccelerationGain"].f;
		this.decelerationGain = d2["DecelerationGain"].f;
		this.vehicleMaxSpeed = d2["MaxSpeed"].f;
		this.vehicleMaxSpeedInv = 1f / this.vehicleMaxSpeed;
		this.vehicleMaxNitroSpeed = d2["MaxNitroSpeed"].f;
		this.vehicleStrength = d2["Strength"].f;
		this.vehicleDamping = d2["Damping"].f;
		this.vehicleHoverOffset = d2["HoverOffset"].f;
		this.vehicleMass = d2["Mass"].f;
		this.vehicleAngularDrag = d2["AngularDrag"].f;
		this.vehicleTurnGain = d2["TurnGain"].f;
		this.vehicleMaxWheelTurn = d2["MaxWheelTurn"].f;
		this.wheelWidth = d2["WheelWidth"].f;
		this.maximumWheelOffset = d2["MaximumWheelOffset"].f;
		this.armor = d2["Armor"].f;
		this.armorInv = 1f / this.armor;
		this.attack += d2["Attack"].f;
		this.camPosY = d2["CamPosY"].f;
		this.camPosZ = d2["CamPosZ"].f;
		this.nitroCamPosY = d2["NitroCamPosY"].f;
		this.nitroCamPosZ = d2["NitroCamPosZ"].f;
		this.camAimY = d2["CamAimY"].f;
		this.explosionRange = d2["ExplosionRange"].f;
		this.reqVelocityMod = d2["ReqVelocityMod"].f;
		this.speedReducMod = d2["SpeedReducMod"].f;
		this.debrisFallOffSpeed = Data.Shared["CarDebris"].d[aCarName].d["FallOffSpeed"].f;
		this.debrisList = Data.Shared["CarDebris"].d[aCarName].d["Debris"].l;
		this.damageDebrisDict = Data.Shared["CarDamageDebris"].d[aCarName].d;
		this.weaponSlots = new Dictionary<string, string>();
		foreach (KeyValuePair<string, DicEntry> keyValuePair in d3)
		{
			if (keyValuePair.Value.s != "None")
			{
				this.weaponSlots.Add(keyValuePair.Key, keyValuePair.Value.s);
			}
		}
	}

	// Token: 0x0600090C RID: 2316 RVA: 0x00044270 File Offset: 0x00042470
	public void UpdateKeys()
	{
		this.forwardKey = GameData.player.input["Forward"];
		this.backwardKey = GameData.player.input["Backward"];
		this.rightKey = GameData.player.input["Right"];
		this.leftKey = GameData.player.input["Left"];
		if (Data.platform != "PC")
		{
			this.UpdateJoyStick();
		}
		if (this.driftEnabled)
		{
			this.driftKey = (GameData.player.input["Drift"] > 0.1f);
		}
		this.resetKey = (GameData.player.input["Reset"] > 0.1f);
		this.fireKey = (GameData.player.input["Fire"] > 0.1f || Scripts.interfaceScript.weaponButtonHeldHACK);
		this.useItem = (GameData.player.input["UseItem"] > 0.1f || Scripts.interfaceScript.gadgetButtonHeldHACK);
	}

	// Token: 0x0600090D RID: 2317 RVA: 0x000443B0 File Offset: 0x000425B0
	private void UpdateJoyStick()
	{
		Vector2 joystickAxis = GameData.player.input.easyJoystick.JoystickAxis;
		if (joystickAxis.y < -0.1f || joystickAxis.y > 0.05f)
		{
			if (joystickAxis.y > 0f)
			{
				this.forwardKey += joystickAxis.magnitude;
			}
			else
			{
				this.backwardKey += joystickAxis.magnitude;
			}
		}
		if (joystickAxis.x > 0.01f)
		{
			this.rightKey += joystickAxis.x;
		}
		if (joystickAxis.x < -0.01f)
		{
			this.leftKey -= joystickAxis.x;
		}
	}

	// Token: 0x0600090E RID: 2318 RVA: 0x0004447C File Offset: 0x0004267C
	public void ResetKeys()
	{
		this.forwardKey = 0f;
		this.backwardKey = 0f;
		this.leftKey = 0f;
		this.rightKey = 0f;
		this.driftKey = false;
		this.resetKey = false;
		this.fireKey = false;
		this.useItem = false;
		this.prevForwardKey = false;
		this.prevFireKey = false;
		this.prevUseItem = false;
	}

	// Token: 0x0400094E RID: 2382
	public readonly string assetBundle = string.Empty;

	// Token: 0x0400094F RID: 2383
	public readonly string carName = string.Empty;

	// Token: 0x04000950 RID: 2384
	public readonly string modelName = string.Empty;

	// Token: 0x04000951 RID: 2385
	public readonly int skin = 1;

	// Token: 0x04000952 RID: 2386
	public readonly string engineAudio = string.Empty;

	// Token: 0x04000953 RID: 2387
	public readonly float projectorSize = 5f;

	// Token: 0x04000954 RID: 2388
	public readonly bool driftEnabled;

	// Token: 0x04000955 RID: 2389
	public readonly float vehicleDrag;

	// Token: 0x04000956 RID: 2390
	public readonly float vehicleGrip;

	// Token: 0x04000957 RID: 2391
	public readonly float vehicleDriftGrip;

	// Token: 0x04000958 RID: 2392
	public readonly float accelerationGain;

	// Token: 0x04000959 RID: 2393
	public readonly float decelerationGain;

	// Token: 0x0400095A RID: 2394
	public readonly float vehicleStrength = 80f;

	// Token: 0x0400095B RID: 2395
	public readonly float vehicleDamping;

	// Token: 0x0400095C RID: 2396
	public readonly float vehicleHoverOffset = 0.08f;

	// Token: 0x0400095D RID: 2397
	public readonly float vehicleMass;

	// Token: 0x0400095E RID: 2398
	public readonly float vehicleAngularDrag;

	// Token: 0x0400095F RID: 2399
	public readonly float vehicleMaxWheelTurn = 35f;

	// Token: 0x04000960 RID: 2400
	public readonly float wheelWidth = 0.2f;

	// Token: 0x04000961 RID: 2401
	public readonly float maximumWheelOffset;

	// Token: 0x04000962 RID: 2402
	public readonly float attack;

	// Token: 0x04000963 RID: 2403
	public readonly float camPosY = 2.3f;

	// Token: 0x04000964 RID: 2404
	public readonly float camPosZ = 4.5f;

	// Token: 0x04000965 RID: 2405
	public readonly float nitroCamPosY = 2.3f;

	// Token: 0x04000966 RID: 2406
	public readonly float nitroCamPosZ = 4.5f;

	// Token: 0x04000967 RID: 2407
	public readonly float camAimY = 0.75f;

	// Token: 0x04000968 RID: 2408
	public readonly float reqVelocityMod;

	// Token: 0x04000969 RID: 2409
	public readonly float speedReducMod;

	// Token: 0x0400096A RID: 2410
	public readonly float debrisFallOffSpeed = 20f;

	// Token: 0x0400096B RID: 2411
	public readonly List<DicEntry> debrisList;

	// Token: 0x0400096C RID: 2412
	public readonly Dictionary<string, DicEntry> damageDebrisDict;

	// Token: 0x0400096D RID: 2413
	public readonly Dictionary<string, string> weaponSlots;

	// Token: 0x0400096E RID: 2414
	public float armor = 1f;

	// Token: 0x0400096F RID: 2415
	public float armorInv = 1f;

	// Token: 0x04000970 RID: 2416
	public float vehicleMaxSpeed;

	// Token: 0x04000971 RID: 2417
	public float vehicleMaxSpeedInv;

	// Token: 0x04000972 RID: 2418
	public float vehicleMaxNitroSpeed;

	// Token: 0x04000973 RID: 2419
	public float vehicleTurnGain;

	// Token: 0x04000974 RID: 2420
	public float wheelRadius = 0.358f;

	// Token: 0x04000975 RID: 2421
	public float currentSpeed;

	// Token: 0x04000976 RID: 2422
	public float currentSpeedPerc;

	// Token: 0x04000977 RID: 2423
	public float nitroIntensity;

	// Token: 0x04000978 RID: 2424
	public float explosionRange = 10f;

	// Token: 0x04000979 RID: 2425
	public float forwardKey;

	// Token: 0x0400097A RID: 2426
	public float backwardKey;

	// Token: 0x0400097B RID: 2427
	public float leftKey;

	// Token: 0x0400097C RID: 2428
	public float rightKey;

	// Token: 0x0400097D RID: 2429
	public bool driftKey;

	// Token: 0x0400097E RID: 2430
	public bool resetKey;

	// Token: 0x0400097F RID: 2431
	public bool fireKey;

	// Token: 0x04000980 RID: 2432
	public bool useItem;

	// Token: 0x04000981 RID: 2433
	public bool prevForwardKey;

	// Token: 0x04000982 RID: 2434
	public bool prevFireKey;

	// Token: 0x04000983 RID: 2435
	public bool prevUseItem;
}
