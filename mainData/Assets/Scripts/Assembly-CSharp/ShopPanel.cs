using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000B7 RID: 183
public class ShopPanel : MonoBehaviour
{
	// Token: 0x0600058F RID: 1423 RVA: 0x00027514 File Offset: 0x00025714
	private void Awake()
	{
		this.unselectedColor = new Color(0.9019608f, 0.9019608f, 0.8627451f, 1f);
		this.selectedColor = new Color(0.98039216f, 0.8627451f, 0.3137255f, 1f);
		this.vehiclesColor = new Color(0.43137255f, 0.627451f, 0.9411765f, 1f);
		this.pimpsColor = new Color(1f, 0.74509805f, 0f, 1f);
		this.gadgetsColor = new Color(0.78431374f, 0.11764706f, 0.19607843f, 1f);
		this.upgradesColor = new Color(0.19607843f, 0.78431374f, 0.3137255f, 1f);
	}

	// Token: 0x06000590 RID: 1424 RVA: 0x000275DC File Offset: 0x000257DC
	private void OnEnable()
	{
		this.OnShopPanel();
	}

	// Token: 0x06000591 RID: 1425 RVA: 0x000275E4 File Offset: 0x000257E4
	private void Start()
	{
	}

	// Token: 0x06000592 RID: 1426 RVA: 0x000275E8 File Offset: 0x000257E8
	private void Update()
	{
		this.totalCoinsValueLabel.text = GameData.cash.ToString();
	}

	// Token: 0x06000593 RID: 1427 RVA: 0x00027600 File Offset: 0x00025800
	public void OnShopPanel()
	{
		Debug.Log("On shop panel activated from: " + Scripts.interfaceScript.shopOriginPanel);
		Debug.Log("OnShopPannel() Called! Clearing leveluprewards!");
		Debug.Log("Shop panel forcing easyjoystick off");
		GameData.player.input.easyJoystick.enable = false;
		GameData.player.input.easyJoystick.enabled = false;
		GameData.player.input.easyJoystick.gameObject.SetActive(false);
		GameData.levelUpRewards.Clear();
		Scripts.audioManager.PlayMusic("SafeHouse", Data.Shared["Misc"].d["MusicVolume"].f, -1);
		this.localization = GameObject.Find("Localization").GetComponent<Localization>();
		UILabel component = this.level.transform.Find("LevelHeader").GetComponent<UILabel>();
		if (GameData.playerLevel < 10)
		{
			component.text = string.Concat(new string[]
			{
				this.localization.Get("LevelText"),
				" ",
				GameData.playerLevel.ToString(),
				": ",
				this.localization.Get("Level" + GameData.playerLevel.ToString() + "Text")
			});
		}
		else
		{
			component.text = string.Concat(new string[]
			{
				this.localization.Get("LevelText"),
				" ",
				GameData.playerLevel.ToString(),
				": ",
				this.localization.Get("Level10Text")
			});
		}
		foreach (object obj in this.shopButtonsLeftAnchor.transform)
		{
			Transform transform = (Transform)obj;
			transform.gameObject.SetActive(false);
		}
		foreach (object obj2 in this.shopButtonsRightAnchor.transform)
		{
			Transform transform2 = (Transform)obj2;
			transform2.gameObject.SetActive(false);
		}
		Debug.Log("Coming from " + Scripts.interfaceScript.shopOriginPanel);
		string shopOriginPanel = Scripts.interfaceScript.shopOriginPanel;
		if (shopOriginPanel != null)
		{
			if (ShopPanel.<>f__switch$map6 == null)
			{
				ShopPanel.<>f__switch$map6 = new Dictionary<string, int>(3)
				{
					{
						"MenuPanel",
						0
					},
					{
						"InterfacePanel",
						1
					},
					{
						"ResultsPanel",
						1
					}
				};
			}
			int num;
			if (ShopPanel.<>f__switch$map6.TryGetValue(shopOriginPanel, out num))
			{
				if (num == 0)
				{
					this.shopButtonsLeftAnchor.transform.Find("ShopBackToMenuButton").gameObject.SetActive(true);
					this.shopButtonsRightAnchor.transform.Find("ShopStartButton").gameObject.SetActive(true);
					goto IL_371;
				}
				if (num == 1)
				{
					this.shopButtonsRightAnchor.transform.Find("ShopDoneButton").gameObject.SetActive(true);
					goto IL_371;
				}
			}
		}
		Debug.LogError("shopOriginPanel contains unexpected (unhandled) value: " + Scripts.interfaceScript.shopOriginPanel);
		IL_371:
		this.SetCategory(this.vehicles);
		Scripts.interfaceScript.UpdateInterfacePlatform();
	}

	// Token: 0x06000594 RID: 1428 RVA: 0x000279C8 File Offset: 0x00025BC8
	public void SetCategory(GameObject go)
	{
		Debug.Log("SetCategory function with GameObject: " + go + " received");
		this.shopCategory = go.name;
		FlurryScript.LogEvent("shop category button", new string[]
		{
			this.shopCategory
		});
		Scripts.audioManager.PlaySFX("Select");
		this.ResetButtons();
		go.GetComponent<BoxCollider>().enabled = false;
		go.GetComponent<UIButton>().enabled = false;
		go.transform.Find("Label").GetComponent<UILabel>().color = this.selectedColor;
		this.BuildItemButtons();
		this.inventory.SetActive(false);
		this.upgradeBar.SetActive(false);
		this.costs.SetActive(false);
		this.equipButton.SetActive(false);
		this.buyButton.SetActive(false);
		this.equippedButton.SetActive(false);
		this.unEquipButton.SetActive(false);
		this.maxButton.SetActive(false);
		string text = this.shopCategory;
		switch (text)
		{
		case "Vehicles":
			this.categoryColor = this.vehiclesColor;
			this.PreviewItem(GameObject.Find(Shop.GetCurrentVehicle()));
			break;
		case "Pimps":
			this.categoryColor = this.pimpsColor;
			if (Shop.GetCurrentSuperPower() != "None")
			{
				this.PreviewItem(GameObject.Find(Shop.GetCurrentSuperPower()));
			}
			else
			{
				this.PreviewItem(GameObject.Find("StuntMan"));
			}
			break;
		case "Gadgets":
			this.categoryColor = this.gadgetsColor;
			this.PreviewItem(GameObject.Find(Shop.GetCurrentGadget()));
			break;
		case "Upgrades":
			this.categoryColor = this.upgradesColor;
			this.PreviewItem(GameObject.Find("AfterTouchLength"));
			break;
		}
		this.itemPreviewColor.color = this.categoryColor;
		this.categoryDescription.transform.Find("Text").GetComponent<UILabel>().text = this.localization.Get(this.shopCategory + "Description");
		this.removeAdvertisementsLabel.SetActive(this.shopCategory == "Vehicles");
		this.restorePurchasesButton.SetActive(this.shopCategory == "Vehicles");
		this.buyItem1Button.SetActive(this.shopCategory == "Vehicles" && !Shop.GetUnlockedVehicles().Contains("Drill"));
		this.buyItem2Button.SetActive(this.shopCategory == "Vehicles" && !Shop.GetUnlockedVehicles().Contains("Tank"));
		this.buyItem3Button.SetActive(this.shopCategory == "Vehicles" && !Shop.GetUnlockedVehicles().Contains("Vice"));
	}

	// Token: 0x06000595 RID: 1429 RVA: 0x00027D14 File Offset: 0x00025F14
	public void ResetButtons()
	{
		List<string> list = new List<string>
		{
			"Vehicles",
			"Pimps",
			"Gadgets",
			"Upgrades"
		};
		int num = 5;
		foreach (string name in list)
		{
			GameObject gameObject = GameObject.Find(name);
			gameObject.GetComponent<BoxCollider>().enabled = true;
			gameObject.GetComponent<UIButton>().enabled = true;
			gameObject.transform.Find("Label").GetComponent<UILabel>().color = this.unselectedColor;
			num--;
		}
	}

	// Token: 0x06000596 RID: 1430 RVA: 0x00027DF0 File Offset: 0x00025FF0
	public void BuildItemButtons()
	{
		Debug.Log("Builditemsbuttons called for; " + this.shopCategory);
		List<string> list = new List<string>
		{
			"Lotus",
			"A7",
			"Chevrolet",
			"FireTruck",
			"Juggernaut",
			"PanzerTruck"
		};
		List<string> list2 = new List<string>();
		List<string> list3 = new List<string>();
		string str = this.localization.Get("UnlockThroughLevelText");
		int num = 100;
		string text = this.shopCategory;
		switch (text)
		{
		case "Vehicles":
			if (Data.platform != "PC")
			{
				list = Shop.GetAllVehicles();
				num = 6;
			}
			list2 = Shop.GetUnlockedVehicles();
			list3 = Shop.GetNewVehicles();
			break;
		case "Pimps":
			list = Shop.GetAllSuperPowers();
			list2 = Shop.GetUnlockedSuperPowers();
			list3 = Shop.GetNewSuperPowers();
			break;
		case "Gadgets":
			list = Shop.GetAllGadgets();
			list2 = Shop.GetUnlockedGadgets();
			list3 = Shop.GetNewGadgets();
			break;
		case "Upgrades":
			list = Shop.GetAllUpgrades();
			list2 = Shop.GetUnlockedUpgrades();
			list3 = Shop.GetNewUpgrades();
			break;
		}
		GameObject[] array = GameObject.FindGameObjectsWithTag("PanelDynamic");
		foreach (GameObject obj in array)
		{
			UnityEngine.Object.Destroy(obj);
		}
		int num3 = 1;
		int num4 = 105;
		float num5 = (float)list.Count * 0.5f * (float)num4 - (float)num4 * 0.5f;
		if (num < 100)
		{
			num5 = (float)num * 0.5f * (float)num4 - (float)num4 * 0.5f;
		}
		if (this.shopCategory == "Pimps")
		{
			num5 -= 56f;
		}
		int num6 = (int)num5 * -1;
		int num7 = -426;
		int num8 = 0;
		int num9 = 32;
		int num10 = -32;
		foreach (string text2 in list)
		{
			if (!text2.Contains("PickUpDuration"))
			{
				this._pos.x = (float)(num6 + (-1 * num4 + num3 * num4));
				this._pos.y = (float)num8;
				if (num < 100)
				{
					this._pos.y = (float)num9;
				}
				if (num3 > num)
				{
					this._pos.x = (float)(num7 + (-1 * num4 + (num3 - num) * num4));
					this._pos.y = (float)num10;
				}
				GameObject gameObject = UnityEngine.Object.Instantiate(this.itemButtonPrefab, base.transform.position, base.transform.rotation) as GameObject;
				Transform component = gameObject.GetComponent<Transform>();
				component.parent = this.itemButtons.transform;
				component.localScale = new Vector3(1f, 1f, 1f);
				component.localPosition = this._pos;
				gameObject.GetComponent<UIButtonMessage>().target = GameObject.Find("ShopPanel");
				gameObject.name = text2;
				if (this.shopCategory == "Vehicles")
				{
					gameObject.transform.Find("Item").GetComponent<UISprite>().spriteName = gameObject.name;
				}
				else
				{
					gameObject.transform.Find("Item").GetComponent<UISprite>().spriteName = gameObject.name + "Icon";
				}
				gameObject.transform.Find("Item").transform.localScale = new Vector3(128f, 128f, 128f);
				text = text2;
				if (text == null)
				{
					goto IL_6EB;
				}
				if (ShopPanel.<>f__switch$map9 == null)
				{
					ShopPanel.<>f__switch$map9 = new Dictionary<string, int>(18)
					{
						{
							"StuntMan",
							0
						},
						{
							"Chevrolet",
							1
						},
						{
							"Golden",
							2
						},
						{
							"FireTruck",
							3
						},
						{
							"QuadDamage",
							4
						},
						{
							"Juggernaut",
							5
						},
						{
							"Toxic",
							6
						},
						{
							"PanzerTruck",
							7
						},
						{
							"Diablo",
							8
						},
						{
							"Drill",
							9
						},
						{
							"KillRod",
							10
						},
						{
							"SchoolBus",
							11
						},
						{
							"Fennek",
							12
						},
						{
							"Tank",
							13
						},
						{
							"Taurus",
							14
						},
						{
							"AstonMartin",
							15
						},
						{
							"FordGT",
							16
						},
						{
							"Vice",
							17
						}
					};
				}
				int num2;
				if (!ShopPanel.<>f__switch$map9.TryGetValue(text, out num2))
				{
					goto IL_6EB;
				}
				string str2;
				switch (num2)
				{
				case 0:
					str2 = "2";
					break;
				case 1:
					str2 = "3";
					break;
				case 2:
					str2 = "4";
					break;
				case 3:
					str2 = "5";
					break;
				case 4:
					str2 = "6";
					break;
				case 5:
					str2 = "7";
					break;
				case 6:
					str2 = "8";
					break;
				case 7:
					str2 = "9";
					break;
				case 8:
					str2 = "10";
					break;
				case 9:
					str = this.localization.Get("BossPackText");
					str2 = string.Empty;
					break;
				case 10:
					str = this.localization.Get("BossPackText");
					str2 = string.Empty;
					break;
				case 11:
					str = this.localization.Get("BossPackText");
					str2 = string.Empty;
					break;
				case 12:
					str = this.localization.Get("MilitaryPackText");
					str2 = string.Empty;
					break;
				case 13:
					str = this.localization.Get("MilitaryPackText");
					str2 = string.Empty;
					break;
				case 14:
					str = this.localization.Get("MilitaryPackText");
					str2 = string.Empty;
					break;
				case 15:
					str = this.localization.Get("SportsPackText");
					str2 = string.Empty;
					break;
				case 16:
					str = this.localization.Get("SportsPackText");
					str2 = string.Empty;
					break;
				case 17:
					str = this.localization.Get("SportsPackText");
					str2 = string.Empty;
					break;
				default:
					goto IL_6EB;
				}
				IL_6F7:
				gameObject.transform.Find("UnlockText").GetComponent<UILabel>().text = str + str2;
				if (list2.Contains(gameObject.name))
				{
					gameObject.transform.Find("Icon").GetComponent<UISprite>().enabled = false;
					gameObject.transform.Find("UnlockText").GetComponent<UILabel>().enabled = false;
					gameObject.transform.Find("Item").GetComponent<UISprite>().color = Color.white;
					gameObject.GetComponent<BoxCollider>().enabled = true;
					if (list3.Contains(gameObject.name))
					{
						gameObject.transform.Find("Icon").GetComponent<UISprite>().spriteName = "IconNew";
						gameObject.transform.Find("Icon").GetComponent<UISprite>().enabled = true;
					}
				}
				if (this.shopCategory == "Vehicles" && Shop.GetCurrentVehicle() == gameObject.name)
				{
					Debug.Log("Building button for equipped vehicle:" + gameObject.name);
					gameObject.transform.Find("Icon").GetComponent<UISprite>().enabled = true;
					gameObject.transform.Find("Icon").GetComponent<UISprite>().spriteName = "IconEquipped";
				}
				if (this.shopCategory == "Pimps" && Shop.GetCurrentSuperPower() == gameObject.name)
				{
					Debug.Log("Building button for equipped superpower: " + gameObject.name);
					gameObject.transform.Find("Icon").GetComponent<UISprite>().enabled = true;
					gameObject.transform.Find("Icon").GetComponent<UISprite>().spriteName = "IconEquipped";
				}
				if (this.shopCategory == "Gadgets" && Shop.GetCurrentGadget() == gameObject.name)
				{
					Debug.Log("Building button for equipped gadget: " + gameObject.name);
					gameObject.transform.Find("Icon").GetComponent<UISprite>().enabled = true;
					gameObject.transform.Find("Icon").GetComponent<UISprite>().spriteName = "IconEquipped";
				}
				if (gameObject.name == this.itemPreviewString)
				{
					gameObject.GetComponent<UIButton>().enabled = false;
					gameObject.GetComponent<BoxCollider>().enabled = false;
					gameObject.transform.Find("Background").GetComponent<UISprite>().color = this.categoryColor;
				}
				num3++;
				continue;
				IL_6EB:
				str2 = string.Empty;
				goto IL_6F7;
			}
		}
		this.CheckItemButtons();
	}

	// Token: 0x06000597 RID: 1431 RVA: 0x000287EC File Offset: 0x000269EC
	public void CheckItemButtons()
	{
		if (this.shopCategory == "Vehicles")
		{
			this.buyItem1Button.SetActive(!Shop.GetUnlockedVehicles().Contains("Drill"));
			this.buyItem2Button.SetActive(!Shop.GetUnlockedVehicles().Contains("Tank"));
			this.buyItem3Button.SetActive(!Shop.GetUnlockedVehicles().Contains("Vice"));
		}
	}

	// Token: 0x06000598 RID: 1432 RVA: 0x00028868 File Offset: 0x00026A68
	public void PreviewItemButton(GameObject go)
	{
		Scripts.audioManager.PlaySFX("Interface/Select2");
		this.PreviewItem(go);
	}

	// Token: 0x06000599 RID: 1433 RVA: 0x00028884 File Offset: 0x00026A84
	public void PreviewItem(GameObject go)
	{
		Debug.Log("Preview item: " + go);
		this.itemPreviewString = go.name;
		this.itemPreviewName.text = this.localization.Get(go.name + "Name");
		this.itemPreviewDescription.text = this.localization.Get(go.name + "Description");
		if (this.shopCategory == "Vehicles")
		{
			this.itemPreviewContainer.spriteName = go.name;
		}
		else
		{
			this.itemPreviewContainer.spriteName = go.name + "Icon";
		}
		this.itemPreviewColor.color = this.categoryColor;
		string text = "0";
		string text2 = this.shopCategory;
		switch (text2)
		{
		case "Vehicles":
			if (GameData.playerCar == go.name)
			{
				this.costs.SetActive(false);
				this.buyButton.SetActive(false);
				this.equipButton.SetActive(false);
				this.costs.SetActive(false);
				this.equippedButton.SetActive(true);
				this.maxButton.SetActive(false);
			}
			else if (GameData.boughtVehicles.Contains(go.name))
			{
				this.costs.SetActive(false);
				this.equipButton.SetActive(true);
				this.buyButton.SetActive(false);
				this.equippedButton.SetActive(false);
				this.maxButton.SetActive(false);
			}
			else
			{
				this.costs.SetActive(true);
				text = Shop.GetVehicleCost(go.name).ToString();
				this.equipButton.SetActive(false);
				this.buyButton.SetActive(true);
				this.equippedButton.SetActive(false);
				this.maxButton.SetActive(false);
			}
			break;
		case "Pimps":
			if (Shop.GetCurrentSuperPower() == go.name)
			{
				this.costs.SetActive(false);
				this.buyButton.SetActive(false);
				this.equipButton.SetActive(false);
				this.unEquipButton.SetActive(true);
				this.equippedButton.SetActive(false);
				this.maxButton.SetActive(false);
			}
			else if (Shop.GetBoughtSuperPowers().Contains(go.name))
			{
				this.costs.SetActive(false);
				this.equipButton.SetActive(true);
				this.buyButton.SetActive(false);
				this.equippedButton.SetActive(false);
				this.unEquipButton.SetActive(false);
				this.maxButton.SetActive(false);
			}
			else
			{
				this.costs.SetActive(true);
				text = Shop.GetSuperPowerCost(go.name).ToString();
				this.equipButton.SetActive(false);
				this.buyButton.SetActive(true);
				this.unEquipButton.SetActive(false);
				this.equippedButton.SetActive(false);
				this.maxButton.SetActive(false);
			}
			break;
		case "Gadgets":
			if (Shop.GetCurrentGadget() == go.name)
			{
				this.costs.SetActive(false);
				this.buyButton.SetActive(false);
				this.equipButton.SetActive(false);
				this.equippedButton.SetActive(true);
				this.maxButton.SetActive(false);
			}
			else if (Shop.GetBoughtGadgets().Contains(go.name))
			{
				this.costs.SetActive(false);
				this.equipButton.SetActive(true);
				this.buyButton.SetActive(false);
				this.equippedButton.SetActive(false);
				this.maxButton.SetActive(false);
			}
			else
			{
				this.costs.SetActive(true);
				text = Shop.GetGadgetCost(go.name).ToString();
				this.equipButton.SetActive(false);
				this.buyButton.SetActive(true);
				this.equippedButton.SetActive(false);
				this.maxButton.SetActive(false);
			}
			break;
		case "Upgrades":
		{
			text = Shop.GetUpgradeCost(go.name).ToString();
			int upgradeLevel = Shop.GetUpgradeLevel(go.name);
			this.upgradeBar.transform.Find("Fill").GetComponent<UIFilledSprite>().fillAmount = (float)upgradeLevel * 0.2f;
			if (upgradeLevel == 5)
			{
				this.buyButton.SetActive(false);
				this.equipButton.SetActive(false);
				this.costs.SetActive(false);
				this.equippedButton.SetActive(false);
				this.maxButton.SetActive(true);
				this.upgradeBar.SetActive(true);
			}
			else
			{
				this.buyButton.SetActive(true);
				this.equipButton.SetActive(false);
				this.costs.SetActive(true);
				this.equippedButton.SetActive(false);
				this.maxButton.SetActive(false);
				this.upgradeBar.SetActive(true);
			}
			break;
		}
		}
		this.costs.transform.Find("Value").GetComponent<UILabel>().text = text;
		this.BuildItemButtons();
	}

	// Token: 0x0600059A RID: 1434 RVA: 0x00028E20 File Offset: 0x00027020
	public void Equip()
	{
		Debug.Log("Equip item: " + this.itemPreviewString);
		string text = this.shopCategory;
		switch (text)
		{
		case "Vehicles":
			Shop.SetCurrentVehicle(this.itemPreviewString);
			break;
		case "Pimps":
			Shop.SetCurrentSuperPower(this.itemPreviewString);
			break;
		case "Gadgets":
			Shop.SetCurrentGadget(this.itemPreviewString);
			break;
		}
		Debug.Log("Now update the preview...");
		this.PreviewItem(GameObject.Find(this.itemPreviewString));
		Scripts.audioManager.PlaySFX("Equip");
	}

	// Token: 0x0600059B RID: 1435 RVA: 0x00028F10 File Offset: 0x00027110
	public void UnEquip()
	{
		Debug.Log("UnEquip item: " + this.itemPreviewString);
		string text = this.shopCategory;
		if (text != null)
		{
			if (ShopPanel.<>f__switch$mapC == null)
			{
				ShopPanel.<>f__switch$mapC = new Dictionary<string, int>(1)
				{
					{
						"Pimps",
						0
					}
				};
			}
			int num;
			if (ShopPanel.<>f__switch$mapC.TryGetValue(text, out num))
			{
				if (num == 0)
				{
					Shop.SetCurrentSuperPower("None");
				}
			}
		}
		Debug.Log("Now update the preview...");
		this.PreviewItem(GameObject.Find(this.itemPreviewString));
		Scripts.audioManager.PlaySFX("Equip");
	}

	// Token: 0x0600059C RID: 1436 RVA: 0x00028FBC File Offset: 0x000271BC
	public void Buy()
	{
		Debug.Log("Trying to buy item: " + this.itemPreviewString);
		bool flag = false;
		bool flag2 = false;
		string text = this.shopCategory;
		switch (text)
		{
		case "Vehicles":
			if (Shop.CanBuyVehicle(this.itemPreviewString))
			{
				flag = true;
			}
			flag2 = true;
			break;
		case "Pimps":
			if (Shop.CanBuySuperPower(this.itemPreviewString))
			{
				flag = true;
			}
			flag2 = true;
			break;
		case "Gadgets":
			if (Shop.CanBuyGadget(this.itemPreviewString))
			{
				flag = true;
			}
			flag2 = true;
			break;
		case "Upgrades":
			if (Shop.CanBuyUpgrade(this.itemPreviewString))
			{
				flag = true;
			}
			break;
		}
		if (flag)
		{
			text = this.shopCategory;
			switch (text)
			{
			case "Vehicles":
				Shop.BuyVehicle(this.itemPreviewString);
				break;
			case "Pimps":
				Shop.BuySuperPower(this.itemPreviewString);
				break;
			case "Gadgets":
				Shop.BuyGadget(this.itemPreviewString);
				break;
			case "Upgrades":
				Shop.BuyUpgrade(this.itemPreviewString);
				break;
			}
			this.CoinDown();
			Scripts.audioManager.PlaySFX("Buy");
			this.PreviewItem(GameObject.Find(this.itemPreviewString));
			this.vehicles.transform.Find("Icon").GetComponent<UISprite>().enabled = (Shop.GetNewVehicles().Count > 0);
			this.gadgets.transform.Find("Icon").GetComponent<UISprite>().enabled = (Shop.GetNewGadgets().Count > 0);
			this.upgrades.transform.Find("Icon").GetComponent<UISprite>().enabled = (Shop.GetNewUpgrades().Count > 0);
			if (flag2)
			{
				this.Equip();
			}
		}
		else
		{
			Scripts.audioManager.PlaySFX("CannotBuy");
		}
	}

	// Token: 0x0600059D RID: 1437 RVA: 0x0002925C File Offset: 0x0002745C
	public void CoinDown()
	{
		this.totalCoins.transform.Find("TotalCoinsIcon").GetComponent<TweenScale>().Reset();
		this.totalCoins.transform.Find("TotalCoinsIcon").GetComponent<TweenScale>().Play(true);
	}

	// Token: 0x0600059E RID: 1438 RVA: 0x000292A8 File Offset: 0x000274A8
	public void ClearNew()
	{
		Debug.Log("Clearing new lists!");
		Shop.GetNewVehicles().Clear();
		Shop.GetNewGadgets().Clear();
		Shop.GetNewUpgrades().Clear();
	}

	// Token: 0x040005DE RID: 1502
	public Localization localization;

	// Token: 0x040005DF RID: 1503
	public GameObject barBottom;

	// Token: 0x040005E0 RID: 1504
	public GameObject barTop;

	// Token: 0x040005E1 RID: 1505
	public GameObject shopButtonsLeftAnchor;

	// Token: 0x040005E2 RID: 1506
	public GameObject shopButtonsRightAnchor;

	// Token: 0x040005E3 RID: 1507
	public GameObject categoryButtons;

	// Token: 0x040005E4 RID: 1508
	public GameObject categoryDescription;

	// Token: 0x040005E5 RID: 1509
	public GameObject header;

	// Token: 0x040005E6 RID: 1510
	public GameObject itemButtons;

	// Token: 0x040005E7 RID: 1511
	public GameObject itemPreview;

	// Token: 0x040005E8 RID: 1512
	public GameObject level;

	// Token: 0x040005E9 RID: 1513
	public GameObject shopBackground;

	// Token: 0x040005EA RID: 1514
	public GameObject TabBackground;

	// Token: 0x040005EB RID: 1515
	public GameObject totalCoins;

	// Token: 0x040005EC RID: 1516
	public GameObject removeAdvertisementsLabel;

	// Token: 0x040005ED RID: 1517
	public GameObject restorePurchasesButton;

	// Token: 0x040005EE RID: 1518
	public GameObject buyItem1Button;

	// Token: 0x040005EF RID: 1519
	public GameObject buyItem2Button;

	// Token: 0x040005F0 RID: 1520
	public GameObject buyItem3Button;

	// Token: 0x040005F1 RID: 1521
	public UILabel totalCoinsValueLabel;

	// Token: 0x040005F2 RID: 1522
	public string shopCategory = "None";

	// Token: 0x040005F3 RID: 1523
	public GameObject vehicles;

	// Token: 0x040005F4 RID: 1524
	public GameObject pimps;

	// Token: 0x040005F5 RID: 1525
	public GameObject gadgets;

	// Token: 0x040005F6 RID: 1526
	public GameObject upgrades;

	// Token: 0x040005F7 RID: 1527
	private Color selectedColor;

	// Token: 0x040005F8 RID: 1528
	private Color unselectedColor;

	// Token: 0x040005F9 RID: 1529
	private Color buttonColor;

	// Token: 0x040005FA RID: 1530
	private Color categoryColor;

	// Token: 0x040005FB RID: 1531
	private Color vehiclesColor;

	// Token: 0x040005FC RID: 1532
	private Color pimpsColor;

	// Token: 0x040005FD RID: 1533
	private Color gadgetsColor;

	// Token: 0x040005FE RID: 1534
	private Color upgradesColor;

	// Token: 0x040005FF RID: 1535
	public GameObject itemButtonPrefab;

	// Token: 0x04000600 RID: 1536
	private Vector3 _pos;

	// Token: 0x04000601 RID: 1537
	public string itemPreviewString;

	// Token: 0x04000602 RID: 1538
	public UILabel itemPreviewName;

	// Token: 0x04000603 RID: 1539
	public UILabel itemPreviewDescription;

	// Token: 0x04000604 RID: 1540
	public UISprite itemPreviewColor;

	// Token: 0x04000605 RID: 1541
	public UISprite itemPreviewContainer;

	// Token: 0x04000606 RID: 1542
	public GameObject costs;

	// Token: 0x04000607 RID: 1543
	public GameObject inventory;

	// Token: 0x04000608 RID: 1544
	public GameObject upgradeBar;

	// Token: 0x04000609 RID: 1545
	public GameObject buyButton;

	// Token: 0x0400060A RID: 1546
	public GameObject equipButton;

	// Token: 0x0400060B RID: 1547
	public GameObject maxButton;

	// Token: 0x0400060C RID: 1548
	public GameObject equippedButton;

	// Token: 0x0400060D RID: 1549
	public GameObject unEquipButton;
}
