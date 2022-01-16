using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000135 RID: 309
public class TrafficManager
{
	// Token: 0x060008E8 RID: 2280 RVA: 0x00042448 File Offset: 0x00040648
	public TrafficManager()
	{
		this.pTraffic = new List<TrafficScript>();
		this.pTrafficTypes = new List<string>(Data.Shared["Traffic"].d.Keys);
		Dictionary<string, DicEntry> d = Data.Shared["TrafficSystemSettings"].d["Platform" + Data.platform].d;
		this.pMaxTraffic = d["MaxTraffic"].i;
		this.pSpawnRadius = d["SpawnRadius"].f;
		this.pSpawnRangeLow = d["SpawnRange"].l[0].i;
		this.pSpawnRangeHigh = d["SpawnRange"].l[1].i;
		this.pRearRemoveDist = d["RearRemoveDist"].i;
		this.pRearRemoveMissionObjDist = d["RearRemoveMissionObjDist"].i;
		this.pSpawnRangeLowSqr = this.pSpawnRangeLow * this.pSpawnRangeLow;
		this.pSpawnRangeHighSqr = this.pSpawnRangeHigh * this.pSpawnRangeHigh;
		this.pRearRemoveDistSqr = this.pRearRemoveDist * this.pRearRemoveDist;
		this.pRearRemoveMissionObjDistSqr = this.pRearRemoveMissionObjDist * this.pRearRemoveMissionObjDist;
		this.pBlockDataTrafficAllowed = new List<BlockData>();
		this.pCheckTrafficLayer = (1 << GameData.trafficLayer | 1 << GameData.damagedTrafficLayer);
	}

	// Token: 0x060008E9 RID: 2281 RVA: 0x00042628 File Offset: 0x00040828
	public void RegisterBlock(BlockData aBlockData)
	{
		this.pBlockDataTrafficAllowed.Add(aBlockData);
	}

	// Token: 0x060008EA RID: 2282 RVA: 0x00042638 File Offset: 0x00040838
	public void UnRegisterBlock(BlockData aBlockData)
	{
		this.pBlockDataTrafficAllowed.Remove(aBlockData);
	}

	// Token: 0x060008EB RID: 2283 RVA: 0x00042648 File Offset: 0x00040848
	public void SpawnTrafficAroundPlayer()
	{
		for (int i = 0; i < 10; i++)
		{
			this.AttemptAddTraffic();
		}
	}

	// Token: 0x060008EC RID: 2284 RVA: 0x00042670 File Offset: 0x00040870
	public void Update()
	{
		this.pNextTrafficCheck--;
		if (this.pNextTrafficCheck > 0)
		{
			return;
		}
		this.pNextTrafficCheck = 5;
		if (Data.cheats && Input.GetKeyDown(KeyCode.A))
		{
			Debug.Log("pBlockDataTrafficAllowed: " + this.pBlockDataTrafficAllowed.Count);
		}
		Vector3 position = GameData.playerCarScript.transform.position;
		Vector3 forward = GameData.playerCarScript.transform.forward;
		for (int i = this.pTraffic.Count - 1; i >= 0; i--)
		{
			TrafficScript trafficScript = this.pTraffic[i];
			Vector3 lhs = trafficScript.transform.position - position;
			float sqrMagnitude = lhs.sqrMagnitude;
			if (sqrMagnitude >= (float)this.pSpawnRangeHighSqr)
			{
				trafficScript.Remove();
			}
			else if (Vector3.Dot(lhs, forward) < 0f && sqrMagnitude >= (float)this.pRearRemoveDistSqr)
			{
				if (trafficScript.iconAttached)
				{
					if (sqrMagnitude >= (float)this.pRearRemoveMissionObjDistSqr)
					{
						trafficScript.Remove();
					}
				}
				else
				{
					trafficScript.Remove();
				}
			}
		}
		if (this.pTraffic.Count < this.pMaxTraffic)
		{
			this.AttemptAddTraffic();
		}
	}

	// Token: 0x060008ED RID: 2285 RVA: 0x000427C8 File Offset: 0x000409C8
	public void AttemptAddTraffic()
	{
		Vector3 position = GameData.playerCarScript.transform.position;
		Vector3 forward = GameData.playerCarScript.transform.forward;
		List<WaypointPath> list = new List<WaypointPath>();
		foreach (BlockData blockData in this.pBlockDataTrafficAllowed)
		{
			list.AddRange(blockData.GetPaths());
		}
		int count = list.Count;
		if (count == 0)
		{
			return;
		}
		int num = UnityEngine.Random.Range(0, count);
		int num2 = 30;
		float t = UnityEngine.Random.Range(0f, 0.7f);
		do
		{
			WaypointPath waypointPath = list[num];
			for (int i = waypointPath.waypoints.Count - 2; i >= 0; i--)
			{
				Vector3 vector = Vector3.Lerp(waypointPath.waypoints[i].position, waypointPath.waypoints[i + 1].position, t);
				Vector3 lhs = vector - position;
				float sqrMagnitude = lhs.sqrMagnitude;
				if ((float)this.pSpawnRangeLowSqr < sqrMagnitude && sqrMagnitude < (float)this.pSpawnRangeHighSqr && Vector3.Dot(lhs, forward) > 0f && this.AddTrafficOnPath(waypointPath, i, vector))
				{
					num2 = -1;
					break;
				}
			}
			num++;
			num2--;
			if (num >= count)
			{
				num = 0;
			}
		}
		while (num2 > 0);
	}

	// Token: 0x060008EE RID: 2286 RVA: 0x00042968 File Offset: 0x00040B68
	private bool AddTrafficOnPath(WaypointPath aWaypointPath, int anIndexPos, Vector3 anActualSpawnPos)
	{
		if (Physics.CheckSphere(anActualSpawnPos, this.pSpawnRadius, this.pCheckTrafficLayer))
		{
			return false;
		}
		string text = this.pTrafficTypes[UnityEngine.Random.Range(0, this.pTrafficTypes.Count)];
		GameObject @object = Scripts.poolManager.GetObject(text, true);
		@object.name = "Traffic_" + text;
		TrafficScript trafficScript = @object.GetComponent<TrafficScript>();
		if (trafficScript == null)
		{
			trafficScript = @object.AddComponent<TrafficScript>();
		}
		else
		{
			trafficScript.enabled = true;
		}
		trafficScript.Initialize(text, aWaypointPath, anIndexPos, anActualSpawnPos);
		this.pTraffic.Add(trafficScript);
		if (GameData.mainMission.IsMissionObject(text, WreckMission.WreckType.VEHICLES))
		{
			trafficScript.iconAttached = true;
			Scripts.interfaceScript.CreateMapIcon(@object);
		}
		return true;
	}

	// Token: 0x060008EF RID: 2287 RVA: 0x00042A2C File Offset: 0x00040C2C
	public void RemoveTraffic(TrafficScript aTrafficScript)
	{
		this.pTraffic.Remove(aTrafficScript);
	}

	// Token: 0x060008F0 RID: 2288 RVA: 0x00042A3C File Offset: 0x00040C3C
	public List<TrafficScript> GetTrafficInRange(Vector3 aPosition, float aRange)
	{
		List<TrafficScript> list = new List<TrafficScript>();
		float num = aRange * aRange;
		foreach (TrafficScript trafficScript in this.pTraffic)
		{
			if ((trafficScript.transform.position - aPosition).sqrMagnitude <= num)
			{
				list.Add(trafficScript);
			}
		}
		return list;
	}

	// Token: 0x0400090E RID: 2318
	private const int ADD_TRAFFIC_TRIES = 30;

	// Token: 0x0400090F RID: 2319
	private const int TRAFFIC_CHECK_INTERVAL = 5;

	// Token: 0x04000910 RID: 2320
	private List<TrafficScript> pTraffic;

	// Token: 0x04000911 RID: 2321
	private List<string> pTrafficTypes;

	// Token: 0x04000912 RID: 2322
	private int pMaxTraffic = 20;

	// Token: 0x04000913 RID: 2323
	private float pSpawnRadius = 10f;

	// Token: 0x04000914 RID: 2324
	private int pSpawnRangeLow = 100;

	// Token: 0x04000915 RID: 2325
	private int pSpawnRangeHigh = 200;

	// Token: 0x04000916 RID: 2326
	private int pRearRemoveDist = 50;

	// Token: 0x04000917 RID: 2327
	private int pRearRemoveMissionObjDist = 50;

	// Token: 0x04000918 RID: 2328
	private int pSpawnRangeLowSqr = 10000;

	// Token: 0x04000919 RID: 2329
	private int pSpawnRangeHighSqr = 40000;

	// Token: 0x0400091A RID: 2330
	private int pRearRemoveDistSqr = 2500;

	// Token: 0x0400091B RID: 2331
	private int pRearRemoveMissionObjDistSqr = 2500;

	// Token: 0x0400091C RID: 2332
	private List<BlockData> pBlockDataTrafficAllowed;

	// Token: 0x0400091D RID: 2333
	private int pCheckTrafficLayer;

	// Token: 0x0400091E RID: 2334
	private int pNextTrafficCheck;
}
