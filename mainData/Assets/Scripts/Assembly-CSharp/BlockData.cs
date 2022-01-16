using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000E2 RID: 226
public class BlockData : GridEntry
{
	// Token: 0x060006CD RID: 1741 RVA: 0x00030E30 File Offset: 0x0002F030
	public BlockData(string aName)
	{
		if (BlockData.ACTIVATE_DISTANCE == -1)
		{
			BlockData.ACTIVATE_DISTANCE = Data.Shared["GridSystem"].d["BlockActivateDist"].i;
		}
		this.pName = aName;
		this.pGameObjects = new List<GameObject>();
		this.pObjectData = new List<ObjectData>();
		this.pActiveObjects = new List<GameObject>();
		this.pWaypointPaths = new List<WaypointPath>();
		this.pTrafficLights = new List<TrafficLight>();
		this.pPickUpsData = new List<PickUpData>();
		this.pLinkGOToPickUpData = new Dictionary<GameObject, PickUpData>();
		this.pSafeHouseData = new List<SafeHouseData>();
		this.pActiveSafeHouses = new Dictionary<GameObject, SafeHouseData>();
	}

	// Token: 0x060006CF RID: 1743 RVA: 0x00030EFC File Offset: 0x0002F0FC
	public void AddBlockGameObject(GameObject aGameObject)
	{
		bool flag = aGameObject.name.EndsWith("Street");
		if (flag)
		{
			aGameObject.AddComponent<DrawBlockPaths>().blockData = this;
		}
		if (!this.pWorldPosSet && (flag || aGameObject.name.EndsWith("Sea")))
		{
			this.pWorldPos = aGameObject.transform.position;
			this.pWorldPosSet = true;
			this.pParentNode = aGameObject.transform;
		}
		this.pGameObjects.Add(aGameObject);
	}

	// Token: 0x060006D0 RID: 1744 RVA: 0x00030F84 File Offset: 0x0002F184
	public void AddTrafficLight(TrafficLight aTrafficLight)
	{
		this.pTrafficLights.Add(aTrafficLight);
	}

	// Token: 0x060006D1 RID: 1745 RVA: 0x00030F94 File Offset: 0x0002F194
	public void AddObject(ObjectData anObjectData)
	{
		this.pObjectData.Add(anObjectData);
	}

	// Token: 0x060006D2 RID: 1746 RVA: 0x00030FA4 File Offset: 0x0002F1A4
	public void AddPickUp(PickUpData aPickUpData)
	{
		this.pPickUpsData.Add(aPickUpData);
	}

	// Token: 0x060006D3 RID: 1747 RVA: 0x00030FB4 File Offset: 0x0002F1B4
	public void AddSafeHousePosition(SafeHouseData aSafeHouseData)
	{
		this.pSafeHouseData.Add(aSafeHouseData);
	}

	// Token: 0x060006D4 RID: 1748 RVA: 0x00030FC4 File Offset: 0x0002F1C4
	public void InitWaypointPaths(List<WaypointPath> aWaypointPaths)
	{
		this.pWaypointPaths = aWaypointPaths;
	}

	// Token: 0x060006D5 RID: 1749 RVA: 0x00030FD0 File Offset: 0x0002F1D0
	public void ConnectWaypointPaths()
	{
		List<GridEntry> list = new List<GridEntry>();
		List<WaypointData> list2 = new List<WaypointData>();
		foreach (WaypointPath waypointPath in this.pWaypointPaths)
		{
			list.Clear();
			WaypointData waypointData = waypointPath.waypoints[waypointPath.waypoints.Count - 1];
			if (waypointData.inOutText == "X")
			{
				Vector3 vector = waypointData.position - this.pWorldPos;
				if (vector.x < -100f)
				{
					list.Add(Scripts.gridManager.GetGridEntry(this.pGridPosX + 1, this.pGridPosY));
				}
				if (vector.x > 100f)
				{
					list.Add(Scripts.gridManager.GetGridEntry(this.pGridPosX - 1, this.pGridPosY));
				}
				if (vector.z < -100f)
				{
					list.Add(Scripts.gridManager.GetGridEntry(this.pGridPosX, this.pGridPosY + 1));
				}
				if (vector.z > 100f)
				{
					list.Add(Scripts.gridManager.GetGridEntry(this.pGridPosX, this.pGridPosY - 1));
				}
				list2.Clear();
				foreach (GridEntry gridEntry in list)
				{
					if (gridEntry != null)
					{
						list2.Add((gridEntry as BlockData).GetClosestConnection(waypointData));
					}
				}
				float num = 999999f;
				WaypointData waypointData2 = null;
				foreach (WaypointData waypointData3 in list2)
				{
					if (waypointData3 != null)
					{
						float num2 = Vector3.SqrMagnitude(waypointData.position - waypointData3.position);
						if (num2 < num)
						{
							num = num2;
							waypointData2 = waypointData3;
						}
					}
				}
				if (waypointData2 != null)
				{
					waypointPath.AddNextPath(waypointData2.waypointPath);
				}
			}
		}
	}

	// Token: 0x060006D6 RID: 1750 RVA: 0x00031250 File Offset: 0x0002F450
	private WaypointData GetClosestConnection(WaypointData aWD)
	{
		float num = 100f;
		WaypointData result = null;
		foreach (WaypointPath waypointPath in this.pWaypointPaths)
		{
			WaypointData waypointData = waypointPath.waypoints[0];
			if (waypointData.inOutText == "X")
			{
				float num2 = Vector3.SqrMagnitude(waypointData.position - aWD.position);
				if (num2 < num)
				{
					num = num2;
					result = waypointData;
				}
			}
		}
		return result;
	}

	// Token: 0x060006D7 RID: 1751 RVA: 0x00031300 File Offset: 0x0002F500
	public void InitTrafficLights()
	{
		foreach (TrafficLight aTrafficLight in this.pTrafficLights)
		{
			this.InitTrafficLight(aTrafficLight);
		}
	}

	// Token: 0x060006D8 RID: 1752 RVA: 0x00031368 File Offset: 0x0002F568
	private void InitTrafficLight(TrafficLight aTrafficLight)
	{
		Vector3 position = aTrafficLight.position;
		HashSet<BlockData> hashSet = new HashSet<BlockData>();
		BlockData blockData = Scripts.gridManager.GetGridEntryFromPoint(position + Vector3.left * 25f + Vector3.back * 25f) as BlockData;
		BlockData blockData2 = Scripts.gridManager.GetGridEntryFromPoint(position + Vector3.left * 25f + Vector3.back * 25f) as BlockData;
		BlockData blockData3 = Scripts.gridManager.GetGridEntryFromPoint(position + Vector3.right * 25f + Vector3.forward * 25f) as BlockData;
		BlockData blockData4 = Scripts.gridManager.GetGridEntryFromPoint(position + Vector3.right * 25f + Vector3.forward * 25f) as BlockData;
		if (blockData != null)
		{
			hashSet.Add(blockData);
		}
		if (blockData2 != null)
		{
			hashSet.Add(blockData2);
		}
		if (blockData3 != null)
		{
			hashSet.Add(blockData3);
		}
		if (blockData4 != null)
		{
			hashSet.Add(blockData4);
		}
		Dictionary<string, WaypointData> dictionary = new Dictionary<string, WaypointData>();
		foreach (BlockData blockData5 in hashSet)
		{
			foreach (WaypointPath waypointPath in blockData5.pWaypointPaths)
			{
				WaypointData waypointData = waypointPath.waypoints[0];
				if (waypointData.inOutText != "X")
				{
					Vector3 vector = waypointData.position - position;
					vector.y = 0f;
					float num = Mathf.Max(Mathf.Abs(vector.x), Mathf.Abs(vector.z));
					if (num <= 25f)
					{
						if (!dictionary.ContainsKey(waypointData.inOutText))
						{
							dictionary.Add(waypointData.inOutText, waypointData);
						}
						else
						{
							Debug.LogError(string.Concat(new object[]
							{
								"Can't add WP: ",
								dictionary[waypointData.inOutText].position,
								"  -  ",
								waypointData.position,
								" cause it was already added, you probably have a duplicate"
							}));
						}
					}
				}
				WaypointData waypointData2 = waypointPath.waypoints[waypointPath.waypoints.Count - 1];
				if (waypointData2.inOutText != "X")
				{
					Vector3 vector = waypointData2.position - position;
					vector.y = 0f;
					float num = Mathf.Max(Mathf.Abs(vector.x), Mathf.Abs(vector.z));
					if (num <= 25f)
					{
						aTrafficLight.AddControlPoint(waypointData2);
						if (!dictionary.ContainsKey(waypointData2.inOutText))
						{
							dictionary.Add(waypointData2.inOutText, waypointData2);
						}
					}
				}
			}
		}
		Dictionary<string, DicEntry> d = Data.Shared["TrafficLightConnections"].d;
		foreach (KeyValuePair<string, WaypointData> keyValuePair in dictionary)
		{
			if (keyValuePair.Value.kind == WaypointData.Kind.OUT)
			{
				foreach (DicEntry dicEntry in d["Out" + keyValuePair.Value.inOutText].l)
				{
					if (dictionary.ContainsKey(dicEntry.s))
					{
						keyValuePair.Value.waypointPath.AddNextPath(dictionary[dicEntry.s].waypointPath);
					}
				}
			}
		}
		aTrafficLight.Initialize();
	}

	// Token: 0x060006D9 RID: 1753 RVA: 0x000317F4 File Offset: 0x0002F9F4
	public override Vector3 GetWorldPos()
	{
		return this.pWorldPos;
	}

	// Token: 0x060006DA RID: 1754 RVA: 0x000317FC File Offset: 0x0002F9FC
	public override void Update()
	{
		foreach (TrafficLight trafficLight in this.pTrafficLights)
		{
			trafficLight.Update();
		}
		Scripts.pickUpManager.UpdatePickUpsOnBlock(this);
		Vector3 position = GameData.playerCarScript.transform.position;
		foreach (KeyValuePair<GameObject, SafeHouseData> keyValuePair in this.pActiveSafeHouses)
		{
			if ((position - keyValuePair.Key.transform.position).sqrMagnitude < 25f)
			{
				if (keyValuePair.Key.activeSelf)
				{
					GameData.currentSafeHouse = keyValuePair.Value.number;
					Scripts.trackScript.GoToShop();
				}
			}
			else if (!keyValuePair.Key.activeSelf)
			{
				keyValuePair.Key.SetActive(true);
			}
		}
	}

	// Token: 0x060006DB RID: 1755 RVA: 0x0003194C File Offset: 0x0002FB4C
	public string GetName()
	{
		return this.pName;
	}

	// Token: 0x060006DC RID: 1756 RVA: 0x00031954 File Offset: 0x0002FB54
	public List<WaypointPath> GetPaths()
	{
		return this.pWaypointPaths;
	}

	// Token: 0x060006DD RID: 1757 RVA: 0x0003195C File Offset: 0x0002FB5C
	public Dictionary<GameObject, PickUpData> GetActivePickUps()
	{
		return this.pLinkGOToPickUpData;
	}

	// Token: 0x060006DE RID: 1758 RVA: 0x00031964 File Offset: 0x0002FB64
	public void GetDestructiblesInRange(Vector3 aPosition, float aDistSqr, List<DestructibleScript> anAppendDestructibleList)
	{
		foreach (GameObject gameObject in this.pActiveObjects)
		{
			DestructibleScript component = gameObject.GetComponent<DestructibleScript>();
			if (component != null && this == component.blockData && (aPosition - component.transform.position).sqrMagnitude <= aDistSqr)
			{
				anAppendDestructibleList.Add(component);
			}
		}
	}

	// Token: 0x060006DF RID: 1759 RVA: 0x00031A08 File Offset: 0x0002FC08
	protected override void DistanceFromCenterBlockUpdated()
	{
		if (this.pDistFromCenterBlock == -1)
		{
			this.Deactivate();
			this.Hide();
		}
		else
		{
			this.Show();
			if (this.pDistFromCenterBlock <= BlockData.ACTIVATE_DISTANCE)
			{
				this.Activate();
			}
			else
			{
				this.Deactivate();
			}
		}
	}

	// Token: 0x060006E0 RID: 1760 RVA: 0x00031A5C File Offset: 0x0002FC5C
	private void Activate()
	{
		if (this.pActivated)
		{
			return;
		}
		this.pActivated = true;
		this.ActivateObjects();
		Scripts.trafficManager.RegisterBlock(this);
		this.ActivatePickUps();
		this.ActivateSafeHouses();
	}

	// Token: 0x060006E1 RID: 1761 RVA: 0x00031A9C File Offset: 0x0002FC9C
	private void ActivateObjects()
	{
		foreach (ObjectData objectData in this.pObjectData)
		{
			GameObject @object = Scripts.poolManager.GetObject("Object_" + objectData.objectName, true);
			@object.name = "Object_" + objectData.name;
			if (objectData.isDestructible)
			{
				DestructibleScript destructibleScript = @object.GetComponent<DestructibleScript>();
				if (destructibleScript != null)
				{
					if (!destructibleScript.enabled)
					{
						destructibleScript.enabled = true;
					}
				}
				else
				{
					destructibleScript = @object.AddComponent<DestructibleScript>();
				}
				destructibleScript.Initialize(objectData.objectName, this);
				if (GameData.mainMission.IsMissionObject(objectData.objectName, WreckMission.WreckType.DESTRUCTIBLES))
				{
					destructibleScript.iconAttached = true;
					Scripts.interfaceScript.CreateMapIcon(@object);
				}
			}
			@object.transform.position = objectData.position;
			@object.transform.rotation = objectData.rotation;
			@object.transform.parent = this.pParentNode;
			this.pActiveObjects.Add(@object);
		}
	}

	// Token: 0x060006E2 RID: 1762 RVA: 0x00031BE0 File Offset: 0x0002FDE0
	private void ActivatePickUps()
	{
		foreach (PickUpData pickUpData in this.pPickUpsData)
		{
			if (!pickUpData.isPickedUp)
			{
				GameObject @object = Scripts.poolManager.GetObject("PickUp_" + pickUpData.pickUpName, true);
				@object.name = pickUpData.name;
				if (pickUpData.isTransformed)
				{
					@object.transform.position = pickUpData.position + new Vector3(0f, pickUpData.heightOffset, 0f);
					@object.transform.localScale = new Vector3(pickUpData.scale, pickUpData.scale, pickUpData.scale);
				}
				else
				{
					@object.transform.position = pickUpData.position;
				}
				@object.transform.parent = this.pParentNode;
				if (pickUpData.mapIcon)
				{
					Scripts.interfaceScript.CreateMapIcon(@object);
				}
				this.pLinkGOToPickUpData.Add(@object, pickUpData);
			}
		}
	}

	// Token: 0x060006E3 RID: 1763 RVA: 0x00031D1C File Offset: 0x0002FF1C
	private void ActivateSafeHouses()
	{
		foreach (SafeHouseData safeHouseData in this.pSafeHouseData)
		{
			GameObject @object = Scripts.poolManager.GetObject("SafeHouse_PS", true);
			if (safeHouseData.number == GameData.currentSafeHouse)
			{
				@object.name = "SafeHouse" + safeHouseData.number;
			}
			else
			{
				@object.name = "SafeHouseInActive" + safeHouseData.number;
				Scripts.interfaceScript.CreateMapIcon(@object);
			}
			@object.transform.position = safeHouseData.position;
			@object.transform.parent = this.pParentNode;
			@object.SetActive(false);
			this.pActiveSafeHouses.Add(@object, safeHouseData);
		}
	}

	// Token: 0x060006E4 RID: 1764 RVA: 0x00031E18 File Offset: 0x00030018
	private void Show()
	{
		if (this.pShown)
		{
			return;
		}
		this.pShown = true;
		foreach (GameObject gameObject in this.pGameObjects)
		{
			if (gameObject.renderer != null)
			{
				gameObject.renderer.enabled = true;
			}
		}
	}

	// Token: 0x060006E5 RID: 1765 RVA: 0x00031EA8 File Offset: 0x000300A8
	private void Hide()
	{
		if (!this.pShown)
		{
			return;
		}
		this.pShown = false;
		foreach (GameObject gameObject in this.pGameObjects)
		{
			if (gameObject.renderer != null)
			{
				gameObject.renderer.enabled = false;
			}
		}
	}

	// Token: 0x060006E6 RID: 1766 RVA: 0x00031F38 File Offset: 0x00030138
	private void Deactivate()
	{
		if (!this.pActivated)
		{
			return;
		}
		this.pActivated = false;
		this.DeactivateObjects();
		Scripts.trafficManager.UnRegisterBlock(this);
		this.DeactivatePickUps();
		this.DeactivateSafeHouses();
	}

	// Token: 0x060006E7 RID: 1767 RVA: 0x00031F78 File Offset: 0x00030178
	private void DeactivateObjects()
	{
		foreach (GameObject gameObject in this.pActiveObjects)
		{
			DestructibleScript component = gameObject.GetComponent<DestructibleScript>();
			if (component != null)
			{
				if (this == component.blockData)
				{
					component.Remove();
				}
			}
			else
			{
				Scripts.poolManager.ReturnToPool(gameObject);
			}
		}
		this.pActiveObjects.Clear();
	}

	// Token: 0x060006E8 RID: 1768 RVA: 0x00032018 File Offset: 0x00030218
	private void DeactivatePickUps()
	{
		foreach (KeyValuePair<GameObject, PickUpData> keyValuePair in this.pLinkGOToPickUpData)
		{
			if (!keyValuePair.Value.isPickedUp)
			{
				if (keyValuePair.Value.mapIcon)
				{
					Scripts.interfaceScript.DestroyMapIcon(keyValuePair.Key);
				}
				Scripts.poolManager.ReturnToPool(keyValuePair.Key);
			}
		}
		this.pLinkGOToPickUpData.Clear();
	}

	// Token: 0x060006E9 RID: 1769 RVA: 0x000320C8 File Offset: 0x000302C8
	private void DeactivateSafeHouses()
	{
		foreach (KeyValuePair<GameObject, SafeHouseData> keyValuePair in this.pActiveSafeHouses)
		{
			if (keyValuePair.Value.number != GameData.currentSafeHouse)
			{
				Scripts.interfaceScript.DestroyMapIcon(keyValuePair.Key);
			}
			Scripts.poolManager.ReturnToPool(keyValuePair.Key);
		}
		this.pActiveSafeHouses.Clear();
	}

	// Token: 0x060006EA RID: 1770 RVA: 0x00032170 File Offset: 0x00030370
	public override bool IsActive()
	{
		return this.pActivated;
	}

	// Token: 0x060006EB RID: 1771 RVA: 0x00032178 File Offset: 0x00030378
	public void DrawGizmos()
	{
		foreach (WaypointPath waypointPath in this.pWaypointPaths)
		{
			waypointPath.DrawGizmos();
		}
	}

	// Token: 0x040006F0 RID: 1776
	private const int MAX_CONNECT_DISTSQR = 100;

	// Token: 0x040006F1 RID: 1777
	private const int TRAFFIC_LIGHT_CONNECT_CHEBYVDIST = 25;

	// Token: 0x040006F2 RID: 1778
	private static int ACTIVATE_DISTANCE = -1;

	// Token: 0x040006F3 RID: 1779
	private bool pShown = true;

	// Token: 0x040006F4 RID: 1780
	private bool pActivated;

	// Token: 0x040006F5 RID: 1781
	private Vector3 pWorldPos = Vector3.zero;

	// Token: 0x040006F6 RID: 1782
	private bool pWorldPosSet;

	// Token: 0x040006F7 RID: 1783
	private string pName;

	// Token: 0x040006F8 RID: 1784
	private Transform pParentNode;

	// Token: 0x040006F9 RID: 1785
	private List<GameObject> pGameObjects;

	// Token: 0x040006FA RID: 1786
	private List<ObjectData> pObjectData;

	// Token: 0x040006FB RID: 1787
	private List<GameObject> pActiveObjects;

	// Token: 0x040006FC RID: 1788
	private List<WaypointPath> pWaypointPaths;

	// Token: 0x040006FD RID: 1789
	private List<TrafficLight> pTrafficLights;

	// Token: 0x040006FE RID: 1790
	private List<PickUpData> pPickUpsData;

	// Token: 0x040006FF RID: 1791
	private Dictionary<GameObject, PickUpData> pLinkGOToPickUpData;

	// Token: 0x04000700 RID: 1792
	private List<SafeHouseData> pSafeHouseData;

	// Token: 0x04000701 RID: 1793
	private Dictionary<GameObject, SafeHouseData> pActiveSafeHouses;
}
