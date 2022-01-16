using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000A6 RID: 166
public class InterfacePanel : MonoBehaviour
{
	// Token: 0x0600052E RID: 1326 RVA: 0x00024964 File Offset: 0x00022B64
	private void OnEnable()
	{
		Scripts.interfaceScript.shopOriginPanel = base.gameObject.name;
	}

	// Token: 0x0600052F RID: 1327 RVA: 0x0002497C File Offset: 0x00022B7C
	private void Start()
	{
		this.targetIcon.spriteName = GameData.mainMission.GetTarget();
		this.targetProgress.text = GameData.mainMission.GetProgress();
		string currentVehicle = Shop.GetCurrentVehicle();
		switch (currentVehicle)
		{
		case "A7":
			this.weaponName = "Laser";
			break;
		case "Lotus":
			this.weaponName = "RocketLauncher";
			break;
		case "Juggernaut":
			this.weaponName = "FlameThrower";
			break;
		case "Chevrolet":
			this.weaponName = "GrenadeLauncher";
			break;
		case "FireTruck":
			this.weaponName = "StrikerLauncher";
			break;
		case "PanzerTruck":
			this.weaponName = "MachineGun";
			break;
		case "Drill":
			this.weaponName = "StrikerLauncher";
			break;
		case "KillRod":
			this.weaponName = "ShotGun";
			break;
		case "SchoolBus":
			this.weaponName = "FlameThrower";
			break;
		case "Fennek":
			this.weaponName = "HeavyMachineGun";
			break;
		case "Tank":
			this.weaponName = "MainGun";
			break;
		case "Taurus":
			this.weaponName = "MissileLauncher";
			break;
		case "AstonMartin":
			this.weaponName = "FuelAirRPGs";
			break;
		case "FordGT":
			this.weaponName = "RailGun";
			break;
		case "Vice":
			this.weaponName = "HeavyMachineGun";
			break;
		}
		this.weaponIcon.spriteName = this.weaponName + "Icon";
		this.gadgetIcon.spriteName = Shop.GetCurrentGadget() + "Icon";
		this.localization = this.interfaceScript.localization;
		this.OnInterfacePanel();
	}

	// Token: 0x06000530 RID: 1328 RVA: 0x00024C28 File Offset: 0x00022E28
	public void OnInterfacePanel()
	{
		base.StartCoroutine(this.InterfaceSequence());
	}

	// Token: 0x06000531 RID: 1329 RVA: 0x00024C38 File Offset: 0x00022E38
	public IEnumerator InterfaceSequence()
	{
		Scripts.trackScript.StartRun();
		Scripts.audioManager.PlayMusic("InGame", Data.Shared["Misc"].d["MusicVolume"].f, -1);
		yield return new WaitForSeconds(1f);
		this.miniMapObject.SetActive(true);
		this.interfaceScript.brandingPanel.SetActive(true);
		this.InterfaceActive(true);
		this.interfaceScript.UpdateInterfacePlatform();
		yield break;
	}

	// Token: 0x06000532 RID: 1330 RVA: 0x00024C54 File Offset: 0x00022E54
	private void Update()
	{
		float speed = GameData.playerCarScript.GetSpeed();
		int num = (int)(3f * speed);
		if (num < 0)
		{
			num = 0;
		}
		this.spedometerValue.text = num.ToString();
		this.spedometerValueStroke.text = num.ToString();
		this.coinsValue.text = GameData.cash.ToString();
		string progress = GameData.mainMission.GetProgress();
		if (this.targetProgress.text != progress)
		{
			this.targetIcon.gameObject.GetComponent<TweenScale>().enabled = true;
			this.targetIcon.gameObject.GetComponent<TweenScale>().Reset();
			this.targetIcon.gameObject.GetComponent<TweenScale>().Play(true);
		}
		this.targetProgress.text = progress;
		if (GameData.switchControls)
		{
			this.itemsAnchor.GetComponent<UIAnchor>().side = UIAnchor.Side.BottomRight;
			this.items.transform.localPosition = new Vector3(-200f, 100f, 0f);
			GameData.player.input.easyJoystick.JoyAnchor = EasyJoystick.JoystickAnchor.LowerLeft;
		}
		else
		{
			this.itemsAnchor.GetComponent<UIAnchor>().side = UIAnchor.Side.BottomLeft;
			this.items.transform.localPosition = new Vector3(150f, 100f, 0f);
			GameData.player.input.easyJoystick.JoyAnchor = EasyJoystick.JoystickAnchor.LowerRight;
		}
	}

	// Token: 0x06000533 RID: 1331 RVA: 0x00024DCC File Offset: 0x00022FCC
	public void InterfaceActive(bool i)
	{
		this.interfaceActive = i;
		this.branding.SetActive(true);
		this.controls.SetActive(i);
		this.ShowTouchControls(i);
		if (this.miniMap.mapSystemObject != null)
		{
			this.miniMap.mapSystemObject.SetActive(i);
		}
		if (i)
		{
			this.extras.SetActive(i);
		}
		if (!i)
		{
			this.extras.GetComponent<UIAnchor>().relativeOffset = new Vector2(10f, 0f);
		}
		else
		{
			this.extras.GetComponent<UIAnchor>().relativeOffset = new Vector2(0f, 0f);
		}
		this.spedometer.SetActive(i);
		this.pickUps.SetActive(i);
		this.coins.SetActive(i);
		this.target.SetActive(i);
		this.itemsAnchor.SetActive(i);
		this.items.SetActive(i);
		if (Data.platform != "PC")
		{
			this.switchControlsAnchor.SetActive(i);
		}
		Scripts.interfaceScript.UpdateInterfacePlatform();
	}

	// Token: 0x06000534 RID: 1332 RVA: 0x00024EF4 File Offset: 0x000230F4
	private void ShowTouchControls(bool aShow)
	{
		if (Data.platform == "PC")
		{
			aShow = false;
		}
		GameData.player.input.easyJoystick.gameObject.SetActive(aShow);
		GameData.player.input.easyJoystick.enabled = aShow;
		GameData.player.input.easyJoystick.enable = aShow;
	}

	// Token: 0x06000535 RID: 1333 RVA: 0x00024F5C File Offset: 0x0002315C
	public void TriggerTime(bool active)
	{
		if (active)
		{
			this.triggerTimeActive = true;
			this.InterfaceActive(false);
		}
		else
		{
			this.triggerTimeActive = false;
			if (!this.triggerTimeActive && !this.airTimeActive && !this.crashTimeActive)
			{
				this.InterfaceActive(true);
			}
		}
	}

	// Token: 0x06000536 RID: 1334 RVA: 0x00024FB4 File Offset: 0x000231B4
	public void AirTime(bool active)
	{
		if (active)
		{
			if (!this.triggerTimeActive)
			{
				this.airTimeActive = true;
				this.airTime.SetActive(true);
				Scripts.audioManager.PlaySFX("Interface/KillCam");
				this.InterfaceActive(false);
				this.ShowTouchControls(true);
			}
		}
		else
		{
			this.airTime.SetActive(false);
			this.airTimeActive = false;
			if (!this.triggerTimeActive && !this.airTimeActive && !this.crashTimeActive)
			{
				this.InterfaceActive(true);
			}
		}
	}

	// Token: 0x06000537 RID: 1335 RVA: 0x00025044 File Offset: 0x00023244
	public void CrashTime(bool active)
	{
		if (active)
		{
			this.crashTimeActive = true;
			this.AirTime(false);
			this.TriggerTime(false);
			this.crashTime.SetActive(true);
			Scripts.audioManager.PlaySFX("Interface/KillCam");
			this.InterfaceActive(false);
			this.ShowTouchControls(true);
		}
		else
		{
			this.crashTime.SetActive(false);
			this.crashTimeActive = false;
		}
	}

	// Token: 0x06000538 RID: 1336 RVA: 0x000250B0 File Offset: 0x000232B0
	public void Nitro()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(this.overlayNitroPrefab, base.transform.position, base.transform.rotation) as GameObject;
		Transform component = gameObject.GetComponent<Transform>();
		component.parent = this.parent;
	}

	// Token: 0x06000539 RID: 1337 RVA: 0x000250F8 File Offset: 0x000232F8
	public void MissionCleared(Mission missionObject)
	{
	}

	// Token: 0x0600053A RID: 1338 RVA: 0x000250FC File Offset: 0x000232FC
	public void CoinUp()
	{
		this.coins.transform.Find("CoinsIcon").GetComponent<TweenScale>().Reset();
		this.coins.transform.Find("CoinsIcon").GetComponent<TweenScale>().Play(true);
	}

	// Token: 0x0600053B RID: 1339 RVA: 0x00025148 File Offset: 0x00023348
	public void PickUp(string pickUp)
	{
		this.PickUp(pickUp, 0);
	}

	// Token: 0x0600053C RID: 1340 RVA: 0x00025154 File Offset: 0x00023354
	public void PickUp(string pickUpName, int score)
	{
		GameObject original = this.pickUpPrefab;
		string tag = "PickUpDynamic";
		bool flag = true;
		string text;
		string text2;
		if (score == 0)
		{
			text = this.localization.Get("PickUp" + pickUpName + "HeaderText");
			text2 = this.localization.Get("PickUp" + pickUpName + "DescriptionText");
		}
		else
		{
			text = "+$" + score.ToString();
			text2 = this.localization.Get(pickUpName + "Name");
		}
		if (text == "PickUp" + pickUpName + "HeaderText")
		{
			text = "EMPTYHEADER";
		}
		if (text2 == "PickUp" + pickUpName + "DescriptionText")
		{
			text2 = "EMPTYDESCRIPTION";
		}
		string text3 = "None";
		string text4 = "None";
		Color color = new Color(1f, 1f, 1f, 1f);
		Color color2 = new Color(1f, 1f, 1f, 0f);
		Color white = new Color(1f, 1f, 1f, 1f);
		Color black = new Color(1f, 1f, 1f, 0f);
		Color color3 = new Color(0.99215686f, 0.32156864f, 0.08235294f, 1f);
		Color color4 = new Color(0f, 0.77254903f, 0.33333334f, 1f);
		Color color5 = new Color(0.078431375f, 0.7058824f, 0.78431374f, 1f);
		Color color6 = new Color(1f, 0.8235294f, 0.32941177f, 1f);
		Color color7 = new Color(0.7372549f, 0.10980392f, 0.10980392f, 1f);
		Color color8 = new Color(1f, 0.33333334f, 0.8352941f, 1f);
		Color color9 = new Color(0.69411767f, 0.13725491f, 0.6745098f, 1f);
		float num = 1f;
		if (score != 0)
		{
			original = this.pickUpScorePrefab;
			tag = "PickUpScoreDynamic";
			this.CoinUp();
			num = 0.75f;
			text3 = "StuntCounter";
		}
		switch (pickUpName)
		{
		case "Cash":
			original = this.pickUpScorePrefab;
			tag = "PickUpScoreDynamic";
			text3 = "PickUpSounds/PickUpCash";
			this.CoinUp();
			num = 0.75f;
			goto IL_784;
		case "CashBig":
			original = this.pickUpScorePrefab;
			tag = "PickUpScoreDynamic";
			text3 = "PickUpSounds/PickUpCashBig";
			this.CoinUp();
			num = 0.75f;
			goto IL_784;
		case "CashStash":
			color = Color.white;
			color2 = Color.black;
			text3 = "PickUpSounds/PickUpCashStash";
			this.CoinUp();
			num = 0.75f;
			goto IL_784;
		case "BonusJumpDistance":
			original = this.pickUpPrefab;
			tag = "PickUpDynamic";
			color = Color.white;
			color2 = Color.grey;
			text3 = "PickUpSounds/PickUpBonusJumpDistance";
			this.CoinUp();
			num = 0.75f;
			goto IL_784;
		case "BonusJumpHeight":
			original = this.pickUpPrefab;
			tag = "PickUpDynamic";
			color = Color.white;
			color2 = Color.grey;
			text3 = "PickUpSounds/PickUpBonusJumpDistance";
			this.CoinUp();
			num = 0.75f;
			goto IL_784;
		case "BonusSpeed":
			original = this.pickUpPrefab;
			tag = "PickUpDynamic";
			color = Color.white;
			color2 = Color.grey;
			text3 = "PickUpSounds/PickUpBonusSpeed";
			this.CoinUp();
			num = 0.75f;
			goto IL_784;
		case "Detonator":
			color = color7;
			color2 = Color.black;
			text3 = "PickUpSounds/PickUpDetonator";
			goto IL_784;
		case "FlameBurst":
			color = color6;
			color2 = color3;
			text3 = "PickUpSounds/PickUpFlameBurst";
			goto IL_784;
		case "Nitro":
			color = color5;
			color2 = Color.blue;
			text3 = "PickUpSounds/PickUpNitro";
			goto IL_784;
		case "Repair":
			color = color4;
			color2 = Color.green;
			text3 = "PickUpSounds/PickUpRepair";
			goto IL_784;
		case "HiddenPackage":
			text2 = string.Concat(new string[]
			{
				this.localization.Get("YouNowHaveText"),
				" ",
				GameData.obtainedHiddenPackages.Count.ToString(),
				" / 25 ",
				this.localization.Get("HiddenPackagesText")
			});
			color = color6;
			color2 = color9;
			white = Color.white;
			black = Color.black;
			text3 = "PickUpSounds/PickUpHiddenPackage";
			goto IL_784;
		case "StyleDemon":
			color = color7;
			color2 = Color.black;
			text3 = "PickUpSounds/PickUpStyleDemon";
			goto IL_784;
		case "StyleGold":
			color = color6;
			color2 = color3;
			text3 = "PickUpSounds/PickUpStyleGold";
			goto IL_784;
		case "StyleQuadDamage":
			color = color9;
			color2 = color8;
			text3 = "PickUpSounds/PickUpStyleQuadDamage";
			goto IL_784;
		case "StyleStuntMan":
			color = color5;
			color2 = Color.white;
			text3 = "PickUpSounds/PickUpStyleStuntMan";
			goto IL_784;
		case "StyleToxic":
			color = color4;
			color2 = color6;
			text3 = "PickUpSounds/PickUpStyleToxic";
			goto IL_784;
		case "DoubleKill":
			text2 = "+" + score.ToString();
			color = color6;
			color2 = color3;
			white = Color.white;
			black = Color.black;
			text3 = "PickUpSounds/PickupDoubleKill";
			text4 = "PickUpSounds/PickUpBonus";
			goto IL_784;
		case "TripleKill":
			text2 = "+" + score.ToString();
			color = color3;
			color2 = color7;
			white = Color.white;
			black = Color.black;
			text3 = "PickUpSounds/PickupTripleKill";
			text4 = "PickUpSounds/PickUpBonus";
			goto IL_784;
		case "MultiKill":
			text2 = "+" + score.ToString();
			color = color7;
			color2 = color8;
			white = Color.white;
			black = Color.black;
			text3 = "PickUpSounds/PickupMultiKill";
			text4 = "PickUpSounds/PickUpBonus";
			goto IL_784;
		case "UltraKill":
			text2 = "+" + score.ToString();
			color = color8;
			color2 = color9;
			white = Color.white;
			black = Color.black;
			text3 = "PickUpSounds/PickupUltraKill";
			text4 = "PickUpSounds/PickUpBonus";
			goto IL_784;
		case "GodLikeKill":
			text2 = "+" + score.ToString();
			color = color9;
			color2 = Color.black;
			white = Color.white;
			black = Color.black;
			text3 = "PickUpSounds/PickupGodLikeKill";
			text4 = "PickUpSounds/PickUpBonus";
			goto IL_784;
		}
		color = Color.white;
		color2 = Color.black;
		white = Color.white;
		black = Color.black;
		IL_784:
		if (pickUpName.Contains("Rampage"))
		{
			original = this.pickUpPrefab;
			tag = "PickUpDynamic";
			color = Color.white;
			color2 = Color.grey;
			text3 = "PickUpSounds/PickUpBonusRampage";
			this.CoinUp();
			num = 0.75f;
		}
		if (text3 != "None")
		{
			Scripts.audioManager.PlaySFX(text3);
		}
		if (text4 != "None")
		{
			Scripts.audioManager.PlaySFX(text4);
		}
		if (flag && this.pickUps.gameObject.activeSelf)
		{
			GameObject[] array = GameObject.FindGameObjectsWithTag(tag);
			foreach (GameObject obj in array)
			{
				UnityEngine.Object.Destroy(obj);
			}
			GameObject gameObject = UnityEngine.Object.Instantiate(original, base.transform.position, base.transform.rotation) as GameObject;
			Transform component = gameObject.GetComponent<Transform>();
			component.parent = this.pickUps.transform;
			gameObject.transform.localScale = new Vector3(num, num, num);
			UILabel component2 = gameObject.transform.Find("PickUpHeader").GetComponent<UILabel>();
			UILabel component3 = gameObject.transform.Find("PickUpHeaderStroke").GetComponent<UILabel>();
			component2.text = text;
			component2.color = color;
			component2.depth = 1;
			component3.text = text;
			component3.color = color2;
			UILabel component4 = gameObject.transform.Find("PickUpDescription").GetComponent<UILabel>();
			component4.text = text2;
			component4.color = white;
		}
	}

	// Token: 0x0600053D RID: 1341 RVA: 0x00025A88 File Offset: 0x00023C88
	public void StartChainReaction()
	{
		this.chainReaction.SetActive(true);
		this.chainReaction.transform.Find("ChainReactionDescription").GetComponent<UILabel>().text = GameData.currentChainReaction.ToString();
	}

	// Token: 0x0600053E RID: 1342 RVA: 0x00025ACC File Offset: 0x00023CCC
	public void UpdateChainReaction()
	{
		this.chainReaction.transform.Find("ChainReactionDescription").GetComponent<TweenScale>().Reset();
		this.chainReaction.transform.Find("ChainReactionDescription").GetComponent<TweenScale>().Play(true);
		Scripts.audioManager.PlaySFX("RampageCounter");
		this.chainReaction.transform.Find("ChainReactionDescription").GetComponent<UILabel>().text = GameData.currentChainReaction.ToString();
	}

	// Token: 0x0600053F RID: 1343 RVA: 0x00025B54 File Offset: 0x00023D54
	public void EndChainReaction()
	{
		this.chainReaction.SetActive(false);
	}

	// Token: 0x06000540 RID: 1344 RVA: 0x00025B64 File Offset: 0x00023D64
	public void ObtainedExtra(string anExtra, int aScore)
	{
		this.PickUp(anExtra, aScore);
	}

	// Token: 0x06000541 RID: 1345 RVA: 0x00025B70 File Offset: 0x00023D70
	public void Multiplier(string type)
	{
		if (this.multiplier.GetComponent<AutoHide>())
		{
			UnityEngine.Object.Destroy(this.multiplier.GetComponent<AutoHide>());
		}
		this.multiplier.AddComponent<AutoHide>();
		Transform transform = this.multiplier.transform.Find("MultiplierIcon");
		transform.GetComponent<UISprite>().spriteName = type;
		transform.GetComponent<TweenScale>().Reset();
		transform.GetComponent<TweenScale>().Play(true);
		this.multiplier.transform.Find("MultiplierHeader").GetComponent<UILabel>().text = this.interfaceScript.localization.Get("PickUp" + type + "HeaderText");
		this.multiplier.transform.Find("MultiplierDescription").GetComponent<UILabel>().text = this.interfaceScript.localization.Get("PickUp" + type + "DescriptionText");
		this.multiplier.SetActive(true);
	}

	// Token: 0x0400053C RID: 1340
	public InterfaceScript interfaceScript;

	// Token: 0x0400053D RID: 1341
	public GameObject miniMapObject;

	// Token: 0x0400053E RID: 1342
	public Minimap miniMap;

	// Token: 0x0400053F RID: 1343
	public GameObject airTime;

	// Token: 0x04000540 RID: 1344
	public GameObject coins;

	// Token: 0x04000541 RID: 1345
	public UILabel coinsValue;

	// Token: 0x04000542 RID: 1346
	public string displayCoins;

	// Token: 0x04000543 RID: 1347
	public GameObject branding;

	// Token: 0x04000544 RID: 1348
	public GameObject controls;

	// Token: 0x04000545 RID: 1349
	public GameObject crashTime;

	// Token: 0x04000546 RID: 1350
	public GameObject extras;

	// Token: 0x04000547 RID: 1351
	public GameObject bonus;

	// Token: 0x04000548 RID: 1352
	public GameObject chainReaction;

	// Token: 0x04000549 RID: 1353
	public GameObject multiplier;

	// Token: 0x0400054A RID: 1354
	public GameObject itemsAnchor;

	// Token: 0x0400054B RID: 1355
	public GameObject items;

	// Token: 0x0400054C RID: 1356
	public UISprite gadgetIcon;

	// Token: 0x0400054D RID: 1357
	public UISprite weaponIcon;

	// Token: 0x0400054E RID: 1358
	public GameObject switchControlsAnchor;

	// Token: 0x0400054F RID: 1359
	public GameObject missionCleared;

	// Token: 0x04000550 RID: 1360
	public GameObject pickUps;

	// Token: 0x04000551 RID: 1361
	public GameObject spedometer;

	// Token: 0x04000552 RID: 1362
	public UILabel spedometerValue;

	// Token: 0x04000553 RID: 1363
	public UILabel spedometerValueStroke;

	// Token: 0x04000554 RID: 1364
	public UIFilledSprite spedometerBar;

	// Token: 0x04000555 RID: 1365
	public GameObject status;

	// Token: 0x04000556 RID: 1366
	public GameObject target;

	// Token: 0x04000557 RID: 1367
	public UISprite targetIcon;

	// Token: 0x04000558 RID: 1368
	public UILabel targetProgress;

	// Token: 0x04000559 RID: 1369
	public bool interfaceActive;

	// Token: 0x0400055A RID: 1370
	public Transform parent;

	// Token: 0x0400055B RID: 1371
	public GameObject overlayNitroPrefab;

	// Token: 0x0400055C RID: 1372
	public GameObject pickUpPrefab;

	// Token: 0x0400055D RID: 1373
	public GameObject pickUpScorePrefab;

	// Token: 0x0400055E RID: 1374
	public GameObject missionPrefab;

	// Token: 0x0400055F RID: 1375
	public bool triggerTimeActive;

	// Token: 0x04000560 RID: 1376
	public bool airTimeActive;

	// Token: 0x04000561 RID: 1377
	public bool crashTimeActive;

	// Token: 0x04000562 RID: 1378
	private string weaponName;

	// Token: 0x04000563 RID: 1379
	public Localization localization;
}
