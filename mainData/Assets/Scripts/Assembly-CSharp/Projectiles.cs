using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000116 RID: 278
public class Projectiles
{
	// Token: 0x060007FE RID: 2046 RVA: 0x0003C6D4 File Offset: 0x0003A8D4
	public static void CreateProjectile(string aProjectileName, Transform aFireTransform, float anInheritedSpeed)
	{
		Dictionary<string, DicEntry> d = Data.Shared["Projectiles"].d[aProjectileName].d;
		GameObject gameObject = Loader.LoadGameObject("Shared", "Projectiles/" + d["Prefab"].s);
		gameObject.transform.position = aFireTransform.position;
		gameObject.transform.rotation = aFireTransform.rotation;
		switch (aProjectileName)
		{
		case "MachineGunBullet":
			gameObject.AddComponent<BulletScript>().Initialize(aProjectileName, d, anInheritedSpeed);
			return;
		case "Rocket":
			gameObject.AddComponent<RocketScript>().Initialize(aProjectileName, d, anInheritedSpeed);
			return;
		case "Shell":
			gameObject.AddComponent<ShellScript>().Initialize(aProjectileName, d, anInheritedSpeed);
			return;
		case "StrikerRocket":
			gameObject.AddComponent<StrikerRocketScript>().Initialize(aProjectileName, d, anInheritedSpeed);
			return;
		case "Missile":
			gameObject.AddComponent<MissileScript>().Initialize(aProjectileName, d, anInheritedSpeed);
			return;
		case "HeavyMGBullet":
			gameObject.AddComponent<BulletScript>().Initialize(aProjectileName, d, anInheritedSpeed);
			return;
		case "FuelAirRPGRocket":
			gameObject.AddComponent<FuelAirRPGRocketScript>().Initialize(aProjectileName, d, anInheritedSpeed);
			return;
		case "TankShell":
			gameObject.AddComponent<RocketScript>().Initialize(aProjectileName, d, anInheritedSpeed);
			return;
		}
		Debug.LogWarning("Projectile " + aProjectileName + " defaulted to rocket script");
		gameObject.AddComponent<RocketScript>().Initialize(aProjectileName, d, anInheritedSpeed);
	}

	// Token: 0x060007FF RID: 2047 RVA: 0x0003C8C8 File Offset: 0x0003AAC8
	public static void CreateProjectile(string aProjectileName, Vector3 aPosition, Quaternion aRotation, float anInheritedSpeed)
	{
		Dictionary<string, DicEntry> d = Data.Shared["Projectiles"].d[aProjectileName].d;
		GameObject gameObject = Loader.LoadGameObject("Shared", "Projectiles/" + d["Prefab"].s);
		gameObject.transform.position = aPosition;
		gameObject.transform.rotation = aRotation;
		switch (aProjectileName)
		{
		case "ShotGunBullet":
			gameObject.AddComponent<BulletScript>().Initialize(aProjectileName, d, anInheritedSpeed);
			return;
		case "MountedGunBullet":
			gameObject.AddComponent<BulletScript>().Initialize(aProjectileName, d, anInheritedSpeed);
			return;
		case "SamSiteRocket":
			gameObject.AddComponent<RocketScript>().Initialize(aProjectileName, d, anInheritedSpeed);
			return;
		}
		Debug.LogWarning("Projectile " + aProjectileName + " defaulted to rocket script");
	}
}
