using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000136 RID: 310
public class TrafficScript : MonoBehaviour
{
	// Token: 0x060008F3 RID: 2291 RVA: 0x00042B30 File Offset: 0x00040D30
	public void Initialize(string aTrafficType, WaypointPath aWaypointPath, int anIndexPos, Vector3 anActualSpawnPos)
	{
		if (this.pFirstInit)
		{
			if (TrafficScript.pSharedTraffic == null)
			{
				TrafficScript.pSharedTraffic = Data.Shared["Traffic"].d;
				TrafficScript.pTrafficMaterial = Loader.LoadMaterial("Shared", "Traffic/Traffic_Material");
				TrafficScript.pTrafficDamage1Material = Loader.LoadMaterial("Shared", "Traffic/TrafficDamage1_Material");
				TrafficScript.pTrafficDamage2Material = Loader.LoadMaterial("Shared", "Traffic/TrafficDamage2_Material");
				TrafficScript.pTrafficDamage3Material = Loader.LoadMaterial("Shared", "Traffic/TrafficDamage3_Material");
				TrafficScript.pTrafficDestroyedMaterial = Loader.LoadMaterial("Shared", "Traffic/TrafficDestroyed_Material");
			}
			this.pTrafficType = aTrafficType;
			this.pLodModel0 = base.transform.Find("Traffic_" + aTrafficType + "_LOD0").renderer;
			this.pLodModel1 = base.transform.Find("Traffic_" + aTrafficType + "_LOD1").renderer;
			this.pShadowPlane = base.transform.Find("Traffic_" + aTrafficType + "Shadow").renderer;
			base.gameObject.layer = GameData.trafficLayer;
			this.pDefaultMask = 1 << GameData.defaultLayer;
			TrafficScript.pTrafficProps = TrafficScript.pSharedTraffic[aTrafficType].d;
			this.pFloatDist = TrafficScript.pTrafficProps["FloatDist"].f;
			this.pFloatDistTriple = this.pFloatDist * 3f;
			base.gameObject.AddComponent<Rigidbody>();
			base.rigidbody.drag = 0.3f;
			base.rigidbody.mass = (float)TrafficScript.pTrafficProps["Mass"].i;
			base.rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
			this.pGravForce = GameData.gravityMagnitude * base.rigidbody.mass;
			GameObject gameObject = new GameObject("DetectionBox");
			gameObject.transform.parent = base.transform;
			Bounds bounds = this.pLodModel0.GetComponent<MeshFilter>().sharedMesh.bounds;
			gameObject.transform.localPosition = new Vector3(0f, (bounds.center.y + bounds.extents.y) * this.pLodModel0.transform.localScale.y + 1.5f, 1f);
			gameObject.transform.localRotation = Quaternion.identity;
			gameObject.AddComponent<BoxCollider>().size = new Vector3(3f, 3f, 3f);
			this.pTrafficDetectionBoxScript = gameObject.AddComponent<TrafficDetectionBoxScript>();
			this.pTrafficDetectionBoxScript.Initialize(this);
			this.pMaxHealthInv = 1f / ((float)TrafficScript.pTrafficProps["Health"].i * GameData.destrHealthMultiplier);
			this.pFirstInit = false;
		}
		else
		{
			if (this.pState != TrafficScript.State.UNTOUCHED)
			{
				this.pState = TrafficScript.State.UNTOUCHED;
				this.SetSharedMaterial(TrafficScript.pTrafficMaterial);
				this.pTrafficDetectionBoxScript.enabled = true;
			}
			this.pShadowPlane.enabled = true;
		}
		base.rigidbody.angularDrag = 0.5f;
		this.pHealth = (float)TrafficScript.pTrafficProps["Health"].i * GameData.destrHealthMultiplier;
		base.transform.parent = TrafficScript.TrafficGroup;
		this.StartOnPath(aWaypointPath, anIndexPos, anActualSpawnPos);
	}

	// Token: 0x060008F4 RID: 2292 RVA: 0x00042E90 File Offset: 0x00041090
	private void StartOnPath(WaypointPath aWaypointPath, int anIndexPos, Vector3 anActualSpawnPos)
	{
		base.rigidbody.velocity = Vector3.zero;
		base.rigidbody.angularVelocity = Vector3.zero;
		this.pWaypointPath = aWaypointPath;
		RaycastHit raycastHit;
		if (Physics.Raycast(anActualSpawnPos + Vector3.up, Vector3.down, out raycastHit, 30f, this.pDefaultMask))
		{
			base.transform.position = raycastHit.point + new Vector3(0f, 0.1f, 0f);
		}
		else
		{
			base.transform.position = anActualSpawnPos;
		}
		this.pCurWaypointIndex = anIndexPos + 1;
		this.pToNextWaypoint = this.pWaypointPath.waypoints[this.pCurWaypointIndex].position - this.pWaypointPath.waypoints[anIndexPos].position;
		this.pToNextWaypoint.y = 0f;
		base.transform.rotation = Quaternion.LookRotation(-this.pToNextWaypoint) * Quaternion.Euler(-90f, 0f, 0f);
	}

	// Token: 0x060008F5 RID: 2293 RVA: 0x00042FB0 File Offset: 0x000411B0
	private void Update()
	{
		if (this.pState != TrafficScript.State.UNTOUCHED)
		{
			return;
		}
		if (base.transform.position.y < -100f)
		{
			this.Remove();
		}
		if (this.pToNextWaypoint.sqrMagnitude < 4f)
		{
			this.pCurWaypointIndex++;
			if (this.pCurWaypointIndex >= this.pWaypointPath.waypoints.Count)
			{
				if (this.pWaypointPath.nextPaths.Count <= 0)
				{
					this.Remove();
					return;
				}
				WaypointPath waypointPath = this.pWaypointPath.nextPaths[UnityEngine.Random.Range(0, this.pWaypointPath.nextPaths.Count)];
				if (!waypointPath.blockData.IsActive())
				{
					this.Remove();
					return;
				}
				this.pWaypointPath = waypointPath;
				this.pCurWaypointIndex = 0;
				this.pToNextWaypoint = this.pWaypointPath.waypoints[this.pCurWaypointIndex].position - base.transform.position;
				this.pToNextWaypoint.y = 0f;
				base.transform.rotation = Quaternion.LookRotation(-this.pToNextWaypoint) * Quaternion.Euler(-90f, 0f, 0f);
			}
			else
			{
				this.pToNextWaypoint = this.pWaypointPath.waypoints[this.pCurWaypointIndex].position - this.pWaypointPath.waypoints[this.pCurWaypointIndex - 1].position;
				this.pToNextWaypoint.y = 0f;
				base.transform.rotation = Quaternion.LookRotation(-this.pToNextWaypoint) * Quaternion.Euler(-90f, 0f, 0f);
			}
		}
	}

	// Token: 0x060008F6 RID: 2294 RVA: 0x000431A0 File Offset: 0x000413A0
	private void FixedUpdate()
	{
		bool flag = false;
		RaycastHit raycastHit;
		if (Physics.Raycast(base.transform.position + new Vector3(0f, this.pFloatDistTriple, 0f), Vector3.down, out raycastHit, this.pFloatDistTriple + this.pFloatDist, this.pDefaultMask))
		{
			float num = this.pFloatDistTriple - raycastHit.distance;
			num = num * 10f + 0.5f * -base.rigidbody.velocity.y;
			Vector3 force = Vector3.up * ((1f + num) * this.pGravForce);
			base.rigidbody.AddForce(force);
			flag = true;
		}
		Vector3 right = base.transform.right;
		if (flag)
		{
			float d = Vector3.Dot(base.rigidbody.velocity, right);
			base.rigidbody.AddForce(d * -right * 20f * base.rigidbody.mass);
		}
		if (this.pState != TrafficScript.State.UNTOUCHED)
		{
			if (flag)
			{
				base.rigidbody.velocity = base.rigidbody.velocity * 0.97f;
				base.rigidbody.angularVelocity = base.rigidbody.angularVelocity * 0.95f;
			}
			return;
		}
		this.pToNextWaypoint = this.pWaypointPath.waypoints[this.pCurWaypointIndex].position - base.transform.position;
		this.pToNextWaypoint.y = 0f;
		this.REDLIGHT = false;
		if (this.pWaypointPath.waypoints[this.pCurWaypointIndex].redLight && this.pToNextWaypoint.sqrMagnitude < 225f)
		{
			this.REDLIGHT = true;
			this.brake = true;
		}
		if (this.brake)
		{
			base.rigidbody.velocity = base.rigidbody.velocity * 0.85f;
		}
		else
		{
			float sqrMagnitude = base.rigidbody.velocity.sqrMagnitude;
			if (sqrMagnitude <= this.pTargetVelSqr)
			{
				base.rigidbody.AddRelativeForce(Vector3.up * 10f * base.rigidbody.mass);
			}
		}
		this.brake = false;
	}

	// Token: 0x060008F7 RID: 2295 RVA: 0x00043410 File Offset: 0x00041610
	public BlockData GetDrivingBlock()
	{
		return this.pWaypointPath.blockData;
	}

	// Token: 0x060008F8 RID: 2296 RVA: 0x00043420 File Offset: 0x00041620
	private void OnCollisionEnter(Collision aCollision)
	{
		if (this.pState == TrafficScript.State.DESTROYED)
		{
			return;
		}
		this.Hit(aCollision, 0f);
	}

	// Token: 0x060008F9 RID: 2297 RVA: 0x0004343C File Offset: 0x0004163C
	private void OnCollisionStay(Collision aCollision)
	{
		if (this.pState == TrafficScript.State.DESTROYED)
		{
			return;
		}
		this.Hit(aCollision, 0f);
	}

	// Token: 0x060008FA RID: 2298 RVA: 0x00043458 File Offset: 0x00041658
	public void Damage(DamageInfo aDamageInfo)
	{
		this.Hit(null, aDamageInfo.damage);
	}

	// Token: 0x060008FB RID: 2299 RVA: 0x00043468 File Offset: 0x00041668
	private void Hit(Collision aCollision, float aDamage)
	{
		if (this.pState == TrafficScript.State.DESTROYED)
		{
			return;
		}
		this.pTrafficDetectionBoxScript.enabled = false;
		base.gameObject.layer = GameData.damagedTrafficLayer;
		float num = 1f;
		bool flag = false;
		if (aCollision != null)
		{
			float magnitude = aCollision.relativeVelocity.magnitude;
			flag = (aCollision.gameObject == GameData.playerCarScript.gameObject);
			if (flag)
			{
				if (GameData.playerCarScript.HasQuadDamage())
				{
					this.pHealth = -1f;
				}
				else
				{
					num = GameData.playerCarScript.carData.attack;
					if (GameData.playerCarScript.IsDestroyed())
					{
						num *= 4f;
					}
				}
				float num2 = Mathf.Min(magnitude, 30f);
				base.rigidbody.velocity += GameData.playerCarScript.rigidbody.velocity * num2 * 0.02f + new Vector3(0f, num2 * 0.2f, 0f);
				if (num2 > 15f)
				{
					this.pShadowPlane.enabled = false;
				}
			}
			aDamage = Mathf.Max(magnitude * 0.25f * num, 0.5f);
		}
		if (this.pState == TrafficScript.State.UNTOUCHED)
		{
			this.PlaySound("Effects/Crash" + UnityEngine.Random.Range(1, 4), 0);
			this.pState = TrafficScript.State.HIT;
		}
		this.pHealth -= aDamage;
		float num3 = this.pHealth * this.pMaxHealthInv;
		if (0.66f < num3 && this.pLodModel0.sharedMaterial == TrafficScript.pTrafficMaterial)
		{
			this.ToDamageState(1);
		}
		if (0.33f < num3 && num3 <= 0.66f && this.pLodModel0.sharedMaterial == TrafficScript.pTrafficDamage1Material)
		{
			this.ToDamageState(2);
		}
		if (0f < num3 && num3 <= 0.33f && this.pLodModel0.sharedMaterial == TrafficScript.pTrafficDamage2Material)
		{
			this.ToDamageState(3);
		}
		if (num3 <= 0f)
		{
			this.Explode(flag);
			this.pState = TrafficScript.State.DESTROYED;
		}
	}

	// Token: 0x060008FC RID: 2300 RVA: 0x000436AC File Offset: 0x000418AC
	private void ToDamageState(int aStateNum)
	{
		GameObject gameObject = null;
		switch (aStateNum)
		{
		case 1:
			this.SetSharedMaterial(TrafficScript.pTrafficDamage1Material);
			gameObject = Loader.LoadGameObject("Shared", "Effects/TrafficHit1_PS");
			break;
		case 2:
			this.SetSharedMaterial(TrafficScript.pTrafficDamage2Material);
			gameObject = Loader.LoadGameObject("Shared", "Effects/TrafficHit2_PS");
			break;
		case 3:
			this.SetSharedMaterial(TrafficScript.pTrafficDamage3Material);
			gameObject = Loader.LoadGameObject("Shared", "Effects/TrafficHit3_PS");
			break;
		}
		gameObject.transform.position = base.transform.position + new Vector3(0f, 2f, 0f);
		UnityEngine.Object.Destroy(gameObject, gameObject.particleSystem.duration + 1f);
	}

	// Token: 0x060008FD RID: 2301 RVA: 0x0004377C File Offset: 0x0004197C
	public void Explode(bool aContactByPlayer)
	{
		if (this.pState == TrafficScript.State.DESTROYED)
		{
			return;
		}
		this.pState = TrafficScript.State.DESTROYED;
		this.RemoveIcon();
		this.pShadowPlane.enabled = false;
		base.gameObject.layer = GameData.damagedTrafficLayer;
		this.SetSharedMaterial(TrafficScript.pTrafficDestroyedMaterial);
		ExplosionScript.AddExplosion("Traffic", base.transform.position + new Vector3(0f, 1f, 0f));
		this.StopSounds();
		this.PlaySound("Effects/CrashGlass" + UnityEngine.Random.Range(1, 3), 0);
		Scripts.scoreManager.DestroyedTraffic(this.pTrafficType);
		if (aContactByPlayer)
		{
			base.rigidbody.AddForce(new Vector3(UnityEngine.Random.value * 2500f, UnityEngine.Random.value * 2500f + 3000f, UnityEngine.Random.value * 2500f));
			base.rigidbody.angularVelocity = UnityEngine.Random.onUnitSphere * 7f;
		}
		base.rigidbody.angularDrag = 0.02f;
	}

	// Token: 0x060008FE RID: 2302 RVA: 0x00043894 File Offset: 0x00041A94
	private void PlaySound(string aSoundName, int aLoop)
	{
		AudioSource audioSource = Scripts.audioManager.PlaySFX(aSoundName, 1f, aLoop, base.gameObject);
		audioSource.minDistance = 20f;
	}

	// Token: 0x060008FF RID: 2303 RVA: 0x000438C4 File Offset: 0x00041AC4
	private void StopSounds()
	{
		AudioSource[] components = base.gameObject.GetComponents<AudioSource>();
		foreach (AudioSource obj in components)
		{
			UnityEngine.Object.Destroy(obj);
		}
	}

	// Token: 0x06000900 RID: 2304 RVA: 0x00043900 File Offset: 0x00041B00
	private void SetSharedMaterial(Material aMaterial)
	{
		this.pLodModel0.sharedMaterial = aMaterial;
		this.pLodModel1.sharedMaterial = aMaterial;
	}

	// Token: 0x06000901 RID: 2305 RVA: 0x0004391C File Offset: 0x00041B1C
	public void Remove()
	{
		base.enabled = false;
		this.RemoveIcon();
		Scripts.poolManager.ReturnToPool(base.gameObject);
		Scripts.trafficManager.RemoveTraffic(this);
	}

	// Token: 0x06000902 RID: 2306 RVA: 0x00043954 File Offset: 0x00041B54
	private void RemoveIcon()
	{
		if (this.iconAttached)
		{
			Scripts.interfaceScript.DestroyMapIcon(base.gameObject);
			this.iconAttached = false;
		}
	}

	// Token: 0x0400091F RID: 2335
	public bool REDLIGHT;

	// Token: 0x04000920 RID: 2336
	public static Transform TrafficGroup;

	// Token: 0x04000921 RID: 2337
	public bool iconAttached;

	// Token: 0x04000922 RID: 2338
	public bool brake;

	// Token: 0x04000923 RID: 2339
	private bool pFirstInit = true;

	// Token: 0x04000924 RID: 2340
	private static Dictionary<string, DicEntry> pTrafficProps;

	// Token: 0x04000925 RID: 2341
	private string pTrafficType = string.Empty;

	// Token: 0x04000926 RID: 2342
	private int pDefaultMask;

	// Token: 0x04000927 RID: 2343
	private Renderer pLodModel0;

	// Token: 0x04000928 RID: 2344
	private Renderer pLodModel1;

	// Token: 0x04000929 RID: 2345
	private Renderer pShadowPlane;

	// Token: 0x0400092A RID: 2346
	private WaypointPath pWaypointPath;

	// Token: 0x0400092B RID: 2347
	private int pCurWaypointIndex;

	// Token: 0x0400092C RID: 2348
	private float pGravForce;

	// Token: 0x0400092D RID: 2349
	private float pFloatDist = 0.25f;

	// Token: 0x0400092E RID: 2350
	private float pFloatDistTriple = 0.75f;

	// Token: 0x0400092F RID: 2351
	private float pTargetVelSqr = 400f;

	// Token: 0x04000930 RID: 2352
	private Vector3 pToNextWaypoint;

	// Token: 0x04000931 RID: 2353
	private TrafficDetectionBoxScript pTrafficDetectionBoxScript;

	// Token: 0x04000932 RID: 2354
	public TrafficScript.State pState;

	// Token: 0x04000933 RID: 2355
	private float pHealth = 100f;

	// Token: 0x04000934 RID: 2356
	private float pMaxHealthInv = 0.01f;

	// Token: 0x04000935 RID: 2357
	private static Dictionary<string, DicEntry> pSharedTraffic;

	// Token: 0x04000936 RID: 2358
	private static Material pTrafficMaterial;

	// Token: 0x04000937 RID: 2359
	private static Material pTrafficDamage1Material;

	// Token: 0x04000938 RID: 2360
	private static Material pTrafficDamage2Material;

	// Token: 0x04000939 RID: 2361
	private static Material pTrafficDamage3Material;

	// Token: 0x0400093A RID: 2362
	private static Material pTrafficDestroyedMaterial;

	// Token: 0x02000137 RID: 311
	public enum State
	{
		// Token: 0x0400093C RID: 2364
		UNTOUCHED,
		// Token: 0x0400093D RID: 2365
		HIT,
		// Token: 0x0400093E RID: 2366
		DESTROYED
	}
}
