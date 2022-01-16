using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000AE RID: 174
public class Minimap : MonoBehaviour
{
	// Token: 0x06000564 RID: 1380 RVA: 0x000264D8 File Offset: 0x000246D8
	private void Awake()
	{
	}

	// Token: 0x06000565 RID: 1381 RVA: 0x000264DC File Offset: 0x000246DC
	private void Start()
	{
		if (Data.platform == "PC" || GameData.enableMiniMapOnMobile)
		{
			base.StartCoroutine(this.MinimapSequence());
		}
	}

	// Token: 0x06000566 RID: 1382 RVA: 0x0002650C File Offset: 0x0002470C
	private void Update()
	{
	}

	// Token: 0x06000567 RID: 1383 RVA: 0x00026510 File Offset: 0x00024710
	private IEnumerator MinimapSequence()
	{
		Debug.Log("Starting MinimapSequence");
		this.CreateMinimap();
		this.mapSystemObject.SetActive(true);
		this.mapSystem.enabled = true;
		yield return new WaitForSeconds(0.001f);
		this.mapSystem.SetMinimapEnabled(true);
		yield return new WaitForSeconds(0.001f);
		this.CreateMapIcon(GameData.playerCarScript.gameObject);
		yield return new WaitForSeconds(2f);
		yield break;
	}

	// Token: 0x06000568 RID: 1384 RVA: 0x0002652C File Offset: 0x0002472C
	public void CreateMinimap()
	{
		this.mapSystemObject = (UnityEngine.Object.Instantiate(this.minimapPrefab) as GameObject);
		this.mapSystem = this.mapSystemObject.GetComponent<KGFMapSystem>();
		this.mapSystem.itsDataModuleMinimap.itsGlobalSettings.itsTarget = GameData.playerCarScript.gameObject;
		this.mapSystem.itsDataModuleMinimap.itsViewport.itsCamera = GenericFunctionsScript.Search(GameObject.Find("CameraParent").transform, "Main Camera").camera;
	}

	// Token: 0x06000569 RID: 1385 RVA: 0x000265B4 File Offset: 0x000247B4
	public void CreateMapIcon(GameObject mapIconGameObject)
	{
		string name = mapIconGameObject.name;
		GameObject original = this.mapIconPickUpPrefab;
		if (name.Contains("Player"))
		{
			original = this.mapIconPlayerPrefab;
		}
		else if (name.StartsWith("Traffic_"))
		{
			original = this.mapIconTargetPrefab;
		}
		else if (name.StartsWith("Object_"))
		{
			original = this.mapIconTargetPrefab;
		}
		else if (name.Contains("SafeHouseActive"))
		{
			original = this.mapIconSafeHouseActivePrefab;
		}
		else if (name.Contains("SafeHouseInActive"))
		{
			original = this.mapIconSafeHouseInActivePrefab;
		}
		else if (name.Contains("PickUp"))
		{
			if (name.Contains("Detonator"))
			{
				original = this.mapIconPickUpDetonatorPrefab;
			}
			else if (name.Contains("FlameBurst"))
			{
				original = this.mapIconPickUpFlameBurstPrefab;
			}
			else if (name.Contains("Nitro"))
			{
				original = this.mapIconPickUpNitroPrefab;
			}
			else if (name.Contains("Repair"))
			{
				original = this.mapIconPickUpRepairPrefab;
			}
			else if (name.Contains("StyleDemon"))
			{
				original = this.mapIconPickUpStyleDemonPrefab;
			}
			else if (name.Contains("StyleGold"))
			{
				original = this.mapIconPickUpStyleGoldPrefab;
			}
			else if (name.Contains("StyleQuadDamage"))
			{
				original = this.mapIconPickUpStyleQuadDamagePrefab;
			}
			else if (name.Contains("StyleStuntMan"))
			{
				original = this.mapIconPickUpStyleStuntManPrefab;
			}
		}
		GameObject gameObject = UnityEngine.Object.Instantiate(original, base.transform.position, base.transform.rotation) as GameObject;
		Transform component = gameObject.GetComponent<Transform>();
		component.parent = mapIconGameObject.transform;
		component.localScale = new Vector3(1f, 1f, 1f);
		component.localPosition = new Vector3(0f, 0f, 0f);
		component.localEulerAngles = new Vector3(0f, 0f, 0f);
	}

	// Token: 0x0600056A RID: 1386 RVA: 0x000267CC File Offset: 0x000249CC
	public void DestroyMapIcon(GameObject mapIconGameObject)
	{
		string name = mapIconGameObject.name;
		string name2 = "MapIconTarget(Clone)";
		if (name.Contains("Player"))
		{
			name2 = "MapIconPlayer(Clone)";
		}
		else if (name.StartsWith("Traffic_"))
		{
			name2 = "MapIconTarget(Clone)";
		}
		else if (name.StartsWith("Object_"))
		{
			name2 = "MapIconTarget(Clone)";
		}
		else if (name.Contains("SafeHouseActive"))
		{
			name2 = "MapIconSafeHouseActive(Clone)";
		}
		else if (name.Contains("SafeHouseInActive"))
		{
			name2 = "MapIconSafeHouseInActive(Clone)";
		}
		else if (name.Contains("PickUp"))
		{
			if (name.Contains("Detonator"))
			{
				name2 = "MapIconPickUpDetonator(Clone)";
			}
			else if (name.Contains("FlameBurst"))
			{
				name2 = "MapIconPickUpFlameBurst(Clone)";
			}
			else if (name.Contains("Nitro"))
			{
				name2 = "MapIconPickUpNitro(Clone)";
			}
			else if (name.Contains("Repair"))
			{
				name2 = "MapIconPickUpRepair(Clone)";
			}
			else if (name.Contains("StyleDemon"))
			{
				name2 = "MapIconPickUpStyleDemon(Clone)";
			}
			else if (name.Contains("StyleGold"))
			{
				name2 = "MapIconPickUpStyleGold(Clone)";
			}
			else if (name.Contains("StyleQuadDamage"))
			{
				name2 = "MapIconPickUpStyleQuadDamage(Clone)";
			}
			else if (name.Contains("StyleStuntMan"))
			{
				name2 = "MapIconPickUpStuntMan(Clone)";
			}
		}
		Transform transform = mapIconGameObject.transform.Find(name2);
		if (transform != null)
		{
			UnityEngine.Object.Destroy(transform.gameObject);
		}
	}

	// Token: 0x0600056B RID: 1387 RVA: 0x00026974 File Offset: 0x00024B74
	public void SetMinimapCamera(Camera minimapCamera)
	{
		this.mapSystem.itsDataModuleMinimap.itsViewport.itsCamera = minimapCamera;
	}

	// Token: 0x04000593 RID: 1427
	public GameObject minimapPrefab;

	// Token: 0x04000594 RID: 1428
	public GameObject interfaceObject;

	// Token: 0x04000595 RID: 1429
	public GameObject clone;

	// Token: 0x04000596 RID: 1430
	public GameObject mapSystemObject;

	// Token: 0x04000597 RID: 1431
	public KGFMapSystem mapSystem;

	// Token: 0x04000598 RID: 1432
	public GameObject mapIconPickUpPrefab;

	// Token: 0x04000599 RID: 1433
	public GameObject mapIconPlayerPrefab;

	// Token: 0x0400059A RID: 1434
	public GameObject mapIconSafeHouseActivePrefab;

	// Token: 0x0400059B RID: 1435
	public GameObject mapIconSafeHouseInActivePrefab;

	// Token: 0x0400059C RID: 1436
	public GameObject mapIconTargetPrefab;

	// Token: 0x0400059D RID: 1437
	public GameObject mapIconPickUpDetonatorPrefab;

	// Token: 0x0400059E RID: 1438
	public GameObject mapIconPickUpFlameBurstPrefab;

	// Token: 0x0400059F RID: 1439
	public GameObject mapIconPickUpNitroPrefab;

	// Token: 0x040005A0 RID: 1440
	public GameObject mapIconPickUpRepairPrefab;

	// Token: 0x040005A1 RID: 1441
	public GameObject mapIconPickUpStyleDemonPrefab;

	// Token: 0x040005A2 RID: 1442
	public GameObject mapIconPickUpStyleGoldPrefab;

	// Token: 0x040005A3 RID: 1443
	public GameObject mapIconPickUpStyleQuadDamagePrefab;

	// Token: 0x040005A4 RID: 1444
	public GameObject mapIconPickUpStyleStuntManPrefab;

	// Token: 0x040005A5 RID: 1445
	public GameObject mapIconPickUpStyleToxicPrefab;

	// Token: 0x040005A6 RID: 1446
	public GameObject testObject;
}
