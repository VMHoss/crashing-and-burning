using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000F1 RID: 241
public class InterfaceScript : MonoBehaviour
{
	// Token: 0x06000738 RID: 1848 RVA: 0x000369A8 File Offset: 0x00034BA8
	private void Awake()
	{
		this.SetEasyJoystick();
		this.localization = GameObject.Find("Localization").GetComponent<Localization>();
		this.UpdateInterfacePlatform();
		this.UpdateInterfaceSize();
	}

	// Token: 0x06000739 RID: 1849 RVA: 0x000369DC File Offset: 0x00034BDC
	public void UpdateInterfacePlatform()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Web");
		GameObject[] array2 = GameObject.FindGameObjectsWithTag("Touch");
		if (Data.platform == "PC")
		{
			foreach (GameObject gameObject in array)
			{
				gameObject.SetActive(true);
			}
			foreach (GameObject gameObject2 in array2)
			{
				gameObject2.SetActive(false);
			}
		}
		else
		{
			foreach (GameObject gameObject3 in array)
			{
				if (GameData.enableMiniMapOnMobile)
				{
					gameObject3.SetActive(gameObject3.name.StartsWith("MiniMap"));
				}
				else
				{
					gameObject3.SetActive(false);
				}
			}
			foreach (GameObject gameObject4 in array2)
			{
				gameObject4.SetActive(true);
			}
		}
	}

	// Token: 0x0600073A RID: 1850 RVA: 0x00036AE8 File Offset: 0x00034CE8
	private void Start()
	{
		this.localization = GameObject.Find("Localization").GetComponent<Localization>();
		GameData.player.input.easyJoystick.gameObject.SetActive(false);
		Debug.Log("INTERFACESCRIPT START GameState: " + GameData.gameState.ToString());
		switch (GameData.gameState)
		{
		case GameData.GameState.MENU:
			Debug.Log("first run is: " + Data.firstRun.ToString());
			Debug.Log("Splash is: " + Data.splash.ToString());
			if (Data.splash && Data.firstRun)
			{
				this.splashPanel.SetActive(true);
			}
			else
			{
				this.menuPanel.SetActive(true);
				this.menuPanel.GetComponent<MenuPanel>().OnMenuPanel();
			}
			break;
		case GameData.GameState.SHOP:
			this.shopPanel.SetActive(true);
			break;
		case GameData.GameState.GAME:
			this.interfacePanelScript.miniMapObject.SetActive(false);
			this.StartGame();
			break;
		}
	}

	// Token: 0x0600073B RID: 1851 RVA: 0x00036C04 File Offset: 0x00034E04
	private void Update()
	{
		if (this.localization == null)
		{
			this.localization = GameObject.Find("Localization").GetComponent<Localization>();
		}
		if (this.screenWidth != Screen.width || this.screenHeight != Screen.height)
		{
			this.UpdateInterfaceSize();
		}
	}

	// Token: 0x0600073C RID: 1852 RVA: 0x00036C60 File Offset: 0x00034E60
	public void OnRollover()
	{
		Scripts.audioManager.PlaySFX("Interface/Rollover");
	}

	// Token: 0x0600073D RID: 1853 RVA: 0x00036C74 File Offset: 0x00034E74
	public void OnButton(GameObject go)
	{
		this.OnButton(go.name);
	}

	// Token: 0x0600073E RID: 1854 RVA: 0x00036C84 File Offset: 0x00034E84
	public void OnButton(string aButtonName)
	{
		Debug.Log("Interface OnButton received: " + aButtonName);
		switch (aButtonName)
		{
		case "StartButton":
			this.MusicFadeOut();
			this.menuPanel.SetActive(false);
			this.optionsPanel.SetActive(false);
			Scripts.audioManager.PlaySFX("Select");
			Scripts.audioManager.PlaySFX("Interface/StartGame");
			GenericFunctionsScript.Fade("ToBlackAndStartGame");
			FlurryScript.LogEvent("menu button", new string[]
			{
				"start"
			});
			goto IL_B84;
		case "SafeHouseButton":
			this.menuPanel.SetActive(false);
			this.optionsPanel.SetActive(false);
			Scripts.audioManager.PlaySFX("Select");
			FlurryScript.LogEvent("menu button", new string[]
			{
				"shop"
			});
			this.ToShop();
			goto IL_B84;
		case "HowToPlayButton":
			this.menuQuitButton.SetActive(false);
			this.optionsPanel.SetActive(false);
			this.howToPlayPanel.SetActive(true);
			Scripts.audioManager.PlaySFX("Select");
			FlurryScript.LogEvent("menu button", new string[]
			{
				"howtoplay"
			});
			goto IL_B84;
		case "AboutButton":
			this.menuQuitButton.SetActive(false);
			this.optionsPanel.SetActive(false);
			this.aboutPanel.SetActive(true);
			Scripts.audioManager.PlaySFX("Select");
			FlurryScript.LogEvent("menu button", new string[]
			{
				"about"
			});
			goto IL_B84;
		case "HowToPlayBackButton":
			this.howToPlayPanel.SetActive(false);
			this.optionsPanel.SetActive(true);
			if (this.menuPanel.activeSelf)
			{
				this.menuQuitButton.SetActive(true);
			}
			Scripts.audioManager.PlaySFX("Back");
			goto IL_B84;
		case "AboutBackButton":
			this.aboutPanel.SetActive(false);
			this.optionsPanel.SetActive(true);
			if (this.menuPanel.activeSelf)
			{
				this.menuQuitButton.SetActive(true);
			}
			Scripts.audioManager.PlaySFX("Back");
			goto IL_B84;
		case "TrophiesButton":
			this.optionsPanel.SetActive(false);
			this.menuPanel.SetActive(false);
			this.trophiesOverviewPanel.SetActive(true);
			Scripts.audioManager.PlaySFX("Select");
			FlurryScript.LogEvent("menu button", new string[]
			{
				"trophies"
			});
			goto IL_B84;
		case "MenuQuitButton":
			GenericFunctionsScript.Fade("FromZeroToBlackQuitApplication");
			Scripts.audioManager.PlaySFX("Back");
			goto IL_B84;
		case "ShopBackToMenuButton":
			GenericFunctionsScript.Fade("FromBlack");
			this.shopPanel.SetActive(false);
			this.menuPanel.SetActive(true);
			this.optionsPanel.SetActive(true);
			Scripts.audioManager.StopAllMusic();
			Scripts.audioManager.PlayMusic("Menu", Data.Shared["Misc"].d["MusicVolume"].f, -1);
			Scripts.audioManager.PlaySFX("Back");
			goto IL_B84;
		case "ShopStartButton":
			UserData.Save();
			this.MusicFadeOut();
			this.shopPanel.SetActive(false);
			GameData.showMission = true;
			Scripts.audioManager.PlaySFX("Select");
			Scripts.audioManager.PlaySFX("Interface/StartGame");
			GenericFunctionsScript.Fade("ToBlackAndStartGame");
			goto IL_B84;
		case "ShopDoneButton":
			UserData.Save();
			Scripts.audioManager.StopAllMusic();
			GameData.showMission = true;
			Scripts.audioManager.PlaySFX("Select");
			GameData.gameState = GameData.GameState.GAME;
			GenericFunctionsScript.Fade("ToBlackAndRetry");
			GameData.player.input.easyJoystick.areaTexture = null;
			GameData.player.input.easyJoystick.touchTexture = null;
			goto IL_B84;
		case "TrophiesOverviewBackButton":
			GenericFunctionsScript.Fade("FromBlackQuick");
			this.trophiesOverviewPanel.SetActive(false);
			this.howToPlayPanel.SetActive(false);
			this.optionsPanel.SetActive(true);
			this.menuPanel.SetActive(true);
			Scripts.audioManager.PlaySFX("Back");
			goto IL_B84;
		case "MenuButton":
			GenericFunctionsScript.Fade("ToBlackAndMenu");
			Scripts.audioManager.PlaySFX("Back");
			goto IL_B84;
		case "PauseButton":
			this.interfacePanelScript.InterfaceActive(false);
			this.pausePanel.SetActive(true);
			this.optionsPanel.SetActive(true);
			Scripts.trackScript.PauseGame();
			Scripts.audioManager.PlaySFX("Pause", 0.6f);
			goto IL_B84;
		case "QuitButton":
			this.confirmationPanel.SetActive(true);
			this.optionsPanel.SetActive(false);
			Scripts.audioManager.PlaySFX("Back");
			goto IL_B84;
		case "YesButton":
			this.confirmationPanel.SetActive(false);
			this.pausePanel.SetActive(false);
			if (this.minimap.mapSystemObject != null)
			{
				UnityEngine.Object.Destroy(this.minimap.mapSystemObject);
			}
			this.interfacePanel.SetActive(false);
			if (Data.platform != "PC")
			{
				GameData.player.input.easyJoystick.gameObject.SetActive(false);
			}
			Scripts.audioManager.PlaySFX("Select");
			GenericFunctionsScript.Fade("ToBlackAndMenu");
			GameData.showMission = true;
			goto IL_B84;
		case "NoButton":
			this.confirmationPanel.SetActive(false);
			this.optionsPanel.SetActive(true);
			Scripts.audioManager.PlaySFX("Back");
			goto IL_B84;
		case "ResumeButton":
			Scripts.trackScript.UnPauseGame();
			GenericFunctionsScript.Fade("FromWhite");
			Scripts.audioManager.PlaySFX("Select");
			goto IL_B84;
		case "RetryButton":
			this.pausePanel.SetActive(false);
			if (this.minimap.mapSystemObject != null)
			{
				UnityEngine.Object.Destroy(this.minimap.mapSystemObject);
			}
			GameData.gameState = GameData.GameState.GAME;
			Data.retried = true;
			GameData.showMission = true;
			GenericFunctionsScript.Fade("ToBlackAndRetry");
			goto IL_B84;
		case "ResultsContinueButton":
			this.resultsPanel.SetActive(false);
			this.optionsPanel.SetActive(false);
			if (this.minimap.mapSystemObject != null)
			{
				UnityEngine.Object.Destroy(this.minimap.mapSystemObject);
			}
			Scripts.audioManager.PlaySFX("Select");
			this.MusicFadeOut();
			GenericFunctionsScript.Fade("ToBlackAndRetry");
			FlurryScript.LogEvent("result button", new string[]
			{
				"continue"
			});
			goto IL_B84;
		case "ResultsQuitButton":
			if (this.minimap.mapSystemObject != null)
			{
				UnityEngine.Object.Destroy(this.minimap.mapSystemObject);
			}
			this.resultsPanel.SetActive(false);
			Scripts.audioManager.PlaySFX("Select");
			GenericFunctionsScript.Fade("ToBlackAndMenu");
			GameData.showMission = true;
			FlurryScript.LogEvent("result button", new string[]
			{
				"quit"
			});
			goto IL_B84;
		case "ResultsSafeHouseButton":
			if (this.minimap.mapSystemObject != null)
			{
				UnityEngine.Object.Destroy(this.minimap.mapSystemObject);
			}
			this.resultsPanel.SetActive(false);
			Scripts.audioManager.PlaySFX("Select");
			GenericFunctionsScript.Fade("ToBlackResultsToShop");
			GameData.showMission = true;
			FlurryScript.LogEvent("result button", new string[]
			{
				"shop"
			});
			goto IL_B84;
		case "SplashDone":
			this.splashPanel.SetActive(false);
			this.menuPanel.SetActive(true);
			this.menuPanel.GetComponent<MenuPanel>().OnMenuPanel();
			goto IL_B84;
		case "SplashDoneButton":
			this.OnButton("SplashDone");
			goto IL_B84;
		case "SplashButton":
			goto IL_B84;
		case "SwitchControlsButton":
			this.interfacePanelScript.switchControlsAnchor.GetComponent<TweenAlpha>().Reset();
			this.interfacePanelScript.switchControlsAnchor.GetComponent<UIPanel>().alpha = 1f;
			this.interfacePanelScript.switchControlsAnchor.GetComponent<TweenAlpha>().Play(true);
			GameData.switchControls = !GameData.switchControls;
			Scripts.audioManager.PlaySFX("Select");
			UserData.Save();
			goto IL_B84;
		case "LeaderboardsButton":
			if (GameData.miniclipManager != null)
			{
				GameData.miniclipManager.ToLeaderboards();
			}
			else
			{
				Debug.LogError("Clicked leaderboards button, but there's no miniclip manager! Ignored button.");
			}
			goto IL_B84;
		case "BuyItem1Button":
			Shop.UnlockVehicle("Drill");
			Shop.BuyVehicle("Drill");
			Shop.UnlockVehicle("KillRod");
			Shop.BuyVehicle("KillRod");
			Shop.UnlockVehicle("SchoolBus");
			Shop.BuyVehicle("SchoolBus");
			Scripts.interfaceScript.SelectNewBoughtCar("Drill");
			UserData.Save();
			FlurryScript.LogEvent("carpack button", new string[]
			{
				"bossfree"
			});
			Scripts.audioManager.PlaySFX("Buy");
			goto IL_B84;
		case "BuyItem2Button":
			GameData.skipAdAfterShopPurchase = true;
			UnibillScript.TryPurchase("com.xformgames.burninrubbercrashnburn.militarypack");
			Scripts.audioManager.PlaySFX("Buy");
			FlurryScript.LogEvent("carpack button", new string[]
			{
				"military"
			});
			goto IL_B84;
		case "BuyItem3Button":
			GameData.skipAdAfterShopPurchase = true;
			UnibillScript.TryPurchase("com.xformgames.burninrubbercrashnburn.sportspack");
			Scripts.audioManager.PlaySFX("Buy");
			FlurryScript.LogEvent("carpack button", new string[]
			{
				"sports"
			});
			goto IL_B84;
		case "RestorePurchasesButton":
			GameData.skipAdAfterShopPurchase = true;
			Unibiller.restoreTransactions();
			Scripts.audioManager.PlaySFX("Interface/Select", 1f);
			FlurryScript.LogEvent("carpack button", new string[]
			{
				"restore"
			});
			goto IL_B84;
		}
		Debug.LogWarning("Button named " + aButtonName + " is not handled in InterfaceScript");
		IL_B84:
		this.UpdateInterfacePlatform();
	}

	// Token: 0x0600073F RID: 1855 RVA: 0x0003781C File Offset: 0x00035A1C
	public void SelectNewBoughtCar(string aCarName)
	{
		GameObject gameObject = GameObject.Find(aCarName);
		if (gameObject != null && this.shopPanel != null)
		{
			ShopPanel component = this.shopPanel.GetComponent<ShopPanel>();
			if (component != null)
			{
				component.PreviewItem(gameObject);
			}
		}
	}

	// Token: 0x06000740 RID: 1856 RVA: 0x0003786C File Offset: 0x00035A6C
	public void OnButtonPress(GameObject go)
	{
		this.OnButtonPress(go.name);
	}

	// Token: 0x06000741 RID: 1857 RVA: 0x0003787C File Offset: 0x00035A7C
	public void OnButtonPress(string aButtonName)
	{
		if (aButtonName != null)
		{
			if (InterfaceScript.<>f__switch$map15 == null)
			{
				InterfaceScript.<>f__switch$map15 = new Dictionary<string, int>(2)
				{
					{
						"GadgetButton",
						0
					},
					{
						"WeaponButton",
						1
					}
				};
			}
			int num;
			if (InterfaceScript.<>f__switch$map15.TryGetValue(aButtonName, out num))
			{
				if (num != 0)
				{
					if (num == 1)
					{
						this.weaponButtonHeldHACK = true;
					}
				}
				else
				{
					this.gadgetButtonHeldHACK = true;
				}
			}
		}
	}

	// Token: 0x06000742 RID: 1858 RVA: 0x00037904 File Offset: 0x00035B04
	public void OnButtonRelease(GameObject go)
	{
		this.OnButtonRelease(go.name);
	}

	// Token: 0x06000743 RID: 1859 RVA: 0x00037914 File Offset: 0x00035B14
	public void OnButtonRelease(string aButtonName)
	{
		if (aButtonName != null)
		{
			if (InterfaceScript.<>f__switch$map16 == null)
			{
				InterfaceScript.<>f__switch$map16 = new Dictionary<string, int>(2)
				{
					{
						"GadgetButton",
						0
					},
					{
						"WeaponButton",
						1
					}
				};
			}
			int num;
			if (InterfaceScript.<>f__switch$map16.TryGetValue(aButtonName, out num))
			{
				if (num != 0)
				{
					if (num == 1)
					{
						this.weaponButtonHeldHACK = false;
					}
				}
				else
				{
					this.gadgetButtonHeldHACK = false;
				}
			}
		}
	}

	// Token: 0x06000744 RID: 1860 RVA: 0x0003799C File Offset: 0x00035B9C
	public void SplashDone()
	{
		this.OnButton("SplashDone");
	}

	// Token: 0x06000745 RID: 1861 RVA: 0x000379AC File Offset: 0x00035BAC
	private void StartGame()
	{
		Debug.Log("Interfacescript start game called!");
		Scripts.audioManager.StopAllMusic();
		GameData.gameState = GameData.GameState.GAME;
		Scripts.trackScript.ShowCar();
		this.startPanel.SetActive(true);
	}

	// Token: 0x06000746 RID: 1862 RVA: 0x000379EC File Offset: 0x00035BEC
	public void ToShop()
	{
		UnityEngine.Object.Destroy(GameObject.Find("Fade(Clone)"));
		this.interfacePanel.SetActive(false);
		Scripts.audioManager.StopAllMusic();
		this.shopPanel.SetActive(true);
	}

	// Token: 0x06000747 RID: 1863 RVA: 0x00037A2C File Offset: 0x00035C2C
	private void QuitToMenu()
	{
		this.brandingPanel.SetActive(false);
		this.optionsPanel.SetActive(false);
		if (this.minimap.mapSystemObject != null)
		{
			UnityEngine.Object.Destroy(this.minimap.mapSystemObject);
		}
		GameData.gameState = GameData.GameState.MENU;
		Scripts.trackScript.RetryGame();
		GameData.player.input.easyJoystick.areaTexture = null;
		GameData.player.input.easyJoystick.touchTexture = null;
	}

	// Token: 0x06000748 RID: 1864 RVA: 0x00037AB4 File Offset: 0x00035CB4
	private void ResultsToShop()
	{
		UnityEngine.Object.Destroy(GameObject.Find("Fade(Clone)"));
		this.brandingPanel.SetActive(false);
		this.optionsPanel.SetActive(false);
		this.resultsPanel.SetActive(false);
		this.interfacePanel.SetActive(false);
		Scripts.audioManager.StopAllMusic();
		GameData.gameState = GameData.GameState.SHOP;
		this.shopPanel.SetActive(true);
	}

	// Token: 0x06000749 RID: 1865 RVA: 0x00037B1C File Offset: 0x00035D1C
	public void Restart()
	{
		this.Retry();
	}

	// Token: 0x0600074A RID: 1866 RVA: 0x00037B24 File Offset: 0x00035D24
	private void Retry()
	{
		Debug.Log("Retry in interfacescript called");
		this.shopPanel.SetActive(false);
		this.resultsPanel.SetActive(false);
		GameData.player.input.easyJoystick.enable = false;
		this.interfacePanelScript.miniMapObject.SetActive(false);
		Scripts.trackScript.RetryGame();
		GameData.player.input.easyJoystick.areaTexture = null;
		GameData.player.input.easyJoystick.touchTexture = null;
	}

	// Token: 0x0600074B RID: 1867 RVA: 0x00037BB0 File Offset: 0x00035DB0
	public void QuitApplication()
	{
		Debug.Log("Interfacescript: Quit Application called!");
		Application.Quit();
	}

	// Token: 0x0600074C RID: 1868 RVA: 0x00037BC4 File Offset: 0x00035DC4
	private void Continue()
	{
		Scripts.trackScript.ContinueTrackNoUnlockScreen();
	}

	// Token: 0x0600074D RID: 1869 RVA: 0x00037BD0 File Offset: 0x00035DD0
	private void Complete()
	{
		Scripts.trackScript.ContinueTrackWithUnlockScreen();
	}

	// Token: 0x0600074E RID: 1870 RVA: 0x00037BDC File Offset: 0x00035DDC
	public void PlayerDestroyed()
	{
		GenericFunctionsScript.Fade("ToBlackAndRetry");
	}

	// Token: 0x0600074F RID: 1871 RVA: 0x00037BE8 File Offset: 0x00035DE8
	public IEnumerator MissionComplete()
	{
		Scripts.audioManager.PlaySFX("MissionCleared");
		this.interfacePanelScript.InterfaceActive(false);
		this.interfacePanel.SetActive(false);
		this.brandingPanel.SetActive(false);
		this.resultsPanel.SetActive(true);
		yield return new WaitForSeconds(1f);
		Scripts.trackScript.MainMissionCompletedFreeze();
		yield break;
	}

	// Token: 0x06000750 RID: 1872 RVA: 0x00037C04 File Offset: 0x00035E04
	private IEnumerator StijnsWait(float delay)
	{
		float timer = Time.realtimeSinceStartup + delay;
		while (Time.realtimeSinceStartup < timer)
		{
			yield return null;
		}
		yield break;
	}

	// Token: 0x06000751 RID: 1873 RVA: 0x00037C28 File Offset: 0x00035E28
	public void pauseGame()
	{
		this.interfacePanelScript.InterfaceActive(false);
		this.interfacePanelScript.crashTime.SetActive(false);
		this.interfacePanelScript.airTime.SetActive(false);
		this.brandingPanel.SetActive(false);
		this.pausePanel.SetActive(true);
		this.optionsPanel.SetActive(true);
		Scripts.audioManager.SetMusicVolume(Data.Shared["Misc"].d["MusicPauseVolume"].f);
		Scripts.interfaceScript.UpdateInterfacePlatform();
	}

	// Token: 0x06000752 RID: 1874 RVA: 0x00037CC0 File Offset: 0x00035EC0
	public void unPauseGame()
	{
		this.optionsPanel.SetActive(false);
		if (!this.interfacePanelScript.crashTimeActive && !this.interfacePanelScript.airTimeActive)
		{
			this.interfacePanelScript.InterfaceActive(true);
		}
		if (this.interfacePanelScript.crashTimeActive)
		{
			this.interfacePanelScript.crashTime.SetActive(true);
		}
		if (this.interfacePanelScript.airTimeActive)
		{
			this.interfacePanelScript.airTime.SetActive(true);
		}
		this.pausePanel.SetActive(false);
		this.brandingPanel.SetActive(true);
		Scripts.audioManager.PlaySFX("UnPause", 0.6f);
		Scripts.audioManager.SetMusicVolume(Data.Shared["Misc"].d["MusicVolume"].f);
	}

	// Token: 0x06000753 RID: 1875 RVA: 0x00037DA4 File Offset: 0x00035FA4
	public void Trophy(string trophyName)
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("TrophyDynamic");
		foreach (GameObject obj in array)
		{
			UnityEngine.Object.Destroy(obj);
		}
		Debug.Log("Trophy won: " + trophyName);
		this.trophiesPanel.SetActive(true);
		GameObject gameObject = UnityEngine.Object.Instantiate(this.trophyPrefab, base.transform.position, base.transform.rotation) as GameObject;
		Transform component = gameObject.GetComponent<Transform>();
		component.parent = this.trophiesPanel.transform;
		gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);
		gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
		gameObject.transform.Find("TrophyHeader").GetComponent<UILabel>().text = this.localization.Get(trophyName + "HeaderText");
		gameObject.transform.Find("TrophyDescription").GetComponent<UILabel>().text = this.localization.Get(trophyName + "DescriptionText");
		gameObject.transform.Find("TrophyProgress").GetComponent<UILabel>().text = this.localization.Get("TrophyAwardedText");
		gameObject.name = trophyName;
		Scripts.audioManager.PlaySFX("Interface/Trophy");
	}

	// Token: 0x06000754 RID: 1876 RVA: 0x00037F20 File Offset: 0x00036120
	public void CreateMapIcon(GameObject mapIconObject)
	{
		if (Data.platform == "PC" || GameData.enableMiniMapOnMobile)
		{
			this.minimap.CreateMapIcon(mapIconObject);
		}
	}

	// Token: 0x06000755 RID: 1877 RVA: 0x00037F58 File Offset: 0x00036158
	public void DestroyMapIcon(GameObject mapIconObject)
	{
		if (Data.platform == "PC" || GameData.enableMiniMapOnMobile)
		{
			this.minimap.DestroyMapIcon(mapIconObject);
		}
	}

	// Token: 0x06000756 RID: 1878 RVA: 0x00037F90 File Offset: 0x00036190
	public void SetMinimapCamera(Camera minimapCamera)
	{
		this.minimap.SetMinimapCamera(minimapCamera);
	}

	// Token: 0x06000757 RID: 1879 RVA: 0x00037FA0 File Offset: 0x000361A0
	public void ShowShop()
	{
		this.MusicFadeOut();
		this.interfacePanelScript.InterfaceActive(false);
		this.brandingPanel.SetActive(false);
		GenericFunctionsScript.Fade("ToBlackAndShop");
	}

	// Token: 0x06000758 RID: 1880 RVA: 0x00037FD8 File Offset: 0x000361D8
	public void MusicFadeOut()
	{
		base.StartCoroutine(this.MusicFadeOutSequence());
	}

	// Token: 0x06000759 RID: 1881 RVA: 0x00037FE8 File Offset: 0x000361E8
	private IEnumerator MusicFadeOutSequence()
	{
		Debug.Log("Music fade out sequence started");
		float musicVolume = Data.Shared["Misc"].d["MusicVolume"].f;
		for (int i = 99; i > 0; i--)
		{
			musicVolume *= (float)i * 0.01f;
			Scripts.audioManager.SetMusicVolume(musicVolume);
			yield return new WaitForSeconds(0.025f);
		}
		Debug.Log("Music fade out sequence ended");
		yield break;
	}

	// Token: 0x0600075A RID: 1882 RVA: 0x00037FFC File Offset: 0x000361FC
	private void On_DoubleTap2Fingers(Gesture gesture)
	{
		if (Scripts.inputManager != null)
		{
			Scripts.inputManager.On_DoubleTap2Fingers(gesture);
		}
	}

	// Token: 0x0600075B RID: 1883 RVA: 0x00038014 File Offset: 0x00036214
	private void SetEasyJoystick()
	{
		if (GameData.player.input.easyJoystick == null)
		{
			GameData.player.input.easyJoystick = GameObject.Find("Joystick").GetComponent<EasyJoystick>();
		}
		GameData.player.input.easyJoystick.enabled = false;
		GameData.player.input.easyJoystick.enable = false;
		GameData.player.input.easyJoystick.gameObject.SetActive(false);
	}

	// Token: 0x0600075C RID: 1884 RVA: 0x000380A0 File Offset: 0x000362A0
	public void DEBUGUpdateButtons()
	{
		if (Data.debug)
		{
			if (GameData.spawnPickUps)
			{
				this.menuPanel.transform.Find("DebugButtons/PickUpsButton/Label").GetComponent<UILabel>().color = Color.green;
			}
			else
			{
				this.menuPanel.transform.Find("DebugButtons/PickUpsButton/Label").GetComponent<UILabel>().color = Color.red;
			}
			if (GameData.spawnBuildings)
			{
				this.menuPanel.transform.Find("DebugButtons/BuildingsButton/Label").GetComponent<UILabel>().color = Color.green;
			}
			else
			{
				this.menuPanel.transform.Find("DebugButtons/BuildingsButton/Label").GetComponent<UILabel>().color = Color.red;
			}
			if (GameData.spawnTraffic)
			{
				this.menuPanel.transform.Find("DebugButtons/TrafficButton/Label").GetComponent<UILabel>().color = Color.green;
			}
			else
			{
				this.menuPanel.transform.Find("DebugButtons/TrafficButton/Label").GetComponent<UILabel>().color = Color.red;
			}
			if (GameData.spawnDestructibles)
			{
				this.menuPanel.transform.Find("DebugButtons/DestructibleButton/Label").GetComponent<UILabel>().color = Color.green;
			}
			else
			{
				this.menuPanel.transform.Find("DebugButtons/DestructibleButton/Label").GetComponent<UILabel>().color = Color.red;
			}
			if (GameData.spawnObjects)
			{
				this.menuPanel.transform.Find("DebugButtons/ObjectsButton/Label").GetComponent<UILabel>().color = Color.green;
			}
			else
			{
				this.menuPanel.transform.Find("DebugButtons/ObjectsButton/Label").GetComponent<UILabel>().color = Color.red;
			}
			if (!GameData.disableAudio)
			{
				this.menuPanel.transform.Find("DebugButtons/AudioButton/Label").GetComponent<UILabel>().color = Color.green;
			}
			else
			{
				this.menuPanel.transform.Find("DebugButtons/AudioButton/Label").GetComponent<UILabel>().color = Color.red;
			}
			if (!GameData.disableFog)
			{
				this.menuPanel.transform.Find("DebugButtons/FogButton/Label").GetComponent<UILabel>().color = Color.green;
			}
			else
			{
				this.menuPanel.transform.Find("DebugButtons/FogButton/Label").GetComponent<UILabel>().color = Color.red;
			}
			if (!GameData.disablePhysics)
			{
				this.menuPanel.transform.Find("DebugButtons/PhysicsButton/Label").GetComponent<UILabel>().color = Color.green;
			}
			else
			{
				this.menuPanel.transform.Find("DebugButtons/PhysicsButton/Label").GetComponent<UILabel>().color = Color.red;
			}
			this.menuPanel.transform.Find("DebugButtons/LevelButton/Label").GetComponent<UILabel>().text = "Level: " + Data.level;
		}
		else
		{
			this.menuPanel.transform.Find("DebugButtons/PickUpsButton").gameObject.SetActive(false);
			this.menuPanel.transform.Find("DebugButtons/BuildingsButton").gameObject.SetActive(false);
			this.menuPanel.transform.Find("DebugButtons/TrafficButton").gameObject.SetActive(false);
			this.menuPanel.transform.Find("DebugButtons/DestructibleButton").gameObject.SetActive(false);
			this.menuPanel.transform.Find("DebugButtons/ObjectsButton").gameObject.SetActive(false);
			this.menuPanel.transform.Find("DebugButtons/AudioButton").gameObject.SetActive(false);
			this.menuPanel.transform.Find("DebugButtons/FogButton").gameObject.SetActive(false);
			this.menuPanel.transform.Find("DebugButtons/PhysicsButton").gameObject.SetActive(false);
			this.menuPanel.transform.Find("DebugButtons/LevelButton").gameObject.SetActive(false);
		}
	}

	// Token: 0x0600075D RID: 1885 RVA: 0x000384C8 File Offset: 0x000366C8
	public void UpdateInterfaceSize()
	{
		this.screenWidth = Screen.width;
		this.screenHeight = Screen.height;
		this.currentScreenResolution = Screen.currentResolution;
		Debug.Log("Update interface size. Current width is : " + this.screenWidth.ToString());
		if (this.screenWidth < 640)
		{
			this.SetPanelSize(0.5f);
		}
		if (this.screenWidth >= 640 && this.screenWidth < 960)
		{
			this.SetPanelSize(0.75f);
		}
		if (this.screenWidth >= 960 && this.screenWidth <= 1136)
		{
			this.SetPanelSize(1f);
		}
		if (this.screenWidth > 1136 && this.screenWidth <= 1280)
		{
			this.SetPanelSize(1.25f);
		}
		if (this.screenWidth > 1280 && this.screenWidth <= 1600)
		{
			this.SetPanelSize(1.5f);
		}
		if (this.screenWidth > 1600 && this.screenWidth <= 1920)
		{
			this.SetPanelSize(1.75f);
		}
		if (this.screenWidth > 1920)
		{
			this.SetPanelSize(2f);
		}
	}

	// Token: 0x0600075E RID: 1886 RVA: 0x00038620 File Offset: 0x00036820
	public void SetPanelSize(float size)
	{
		Debug.Log("SetPanelSize called with size: " + size.ToString());
		this.panelSize = size;
		int i = 0;
		int num = this.resizePanels.Length;
		while (i < num)
		{
			UIPanel uipanel = this.resizePanels[i];
			string name = uipanel.gameObject.name;
			if (name != null)
			{
				if (InterfaceScript.<>f__switch$map17 == null)
				{
					InterfaceScript.<>f__switch$map17 = new Dictionary<string, int>(0);
				}
				int num2;
				if (InterfaceScript.<>f__switch$map17.TryGetValue(name, out num2))
				{
				}
			}
			float num3 = 1f;
			Vector3 localScale = new Vector3(size * num3, size * num3, size * num3);
			uipanel.gameObject.transform.localScale = localScale;
			i++;
		}
	}

	// Token: 0x0600075F RID: 1887 RVA: 0x000386E0 File Offset: 0x000368E0
	public void PlaySound()
	{
		Debug.Log("Interfacescript play sound received!");
		Scripts.audioManager.PlaySFX("Interface/Star");
	}

	// Token: 0x040007C5 RID: 1989
	public Localization localization;

	// Token: 0x040007C6 RID: 1990
	public GameObject interfaceRoot;

	// Token: 0x040007C7 RID: 1991
	public GameObject interfaceCamera;

	// Token: 0x040007C8 RID: 1992
	public GameObject interfaceAnchor;

	// Token: 0x040007C9 RID: 1993
	public GameObject aboutPanel;

	// Token: 0x040007CA RID: 1994
	public GameObject brandingPanel;

	// Token: 0x040007CB RID: 1995
	public GameObject confirmationPanel;

	// Token: 0x040007CC RID: 1996
	public GameObject interfacePanel;

	// Token: 0x040007CD RID: 1997
	public InterfacePanel interfacePanelScript;

	// Token: 0x040007CE RID: 1998
	public Minimap minimap;

	// Token: 0x040007CF RID: 1999
	public GameObject minimapObject;

	// Token: 0x040007D0 RID: 2000
	public GameObject menuPanel;

	// Token: 0x040007D1 RID: 2001
	public GameObject howToPlayPanel;

	// Token: 0x040007D2 RID: 2002
	public GameObject trophiesOverviewPanel;

	// Token: 0x040007D3 RID: 2003
	public GameObject trophiesPanel;

	// Token: 0x040007D4 RID: 2004
	public GameObject trophyPrefab;

	// Token: 0x040007D5 RID: 2005
	public GameObject overlayPanel;

	// Token: 0x040007D6 RID: 2006
	public GameObject pausePanel;

	// Token: 0x040007D7 RID: 2007
	public GameObject resultsPanel;

	// Token: 0x040007D8 RID: 2008
	public GameObject shopPanel;

	// Token: 0x040007D9 RID: 2009
	public GameObject splashPanel;

	// Token: 0x040007DA RID: 2010
	public GameObject startPanel;

	// Token: 0x040007DB RID: 2011
	public GameObject optionsPanel;

	// Token: 0x040007DC RID: 2012
	public GameObject startGameButton;

	// Token: 0x040007DD RID: 2013
	public GameObject menuQuitButton;

	// Token: 0x040007DE RID: 2014
	public bool gadgetButtonHeldHACK;

	// Token: 0x040007DF RID: 2015
	public bool weaponButtonHeldHACK;

	// Token: 0x040007E0 RID: 2016
	public string shopOriginPanel;

	// Token: 0x040007E1 RID: 2017
	public Resolution currentScreenResolution;

	// Token: 0x040007E2 RID: 2018
	public int screenWidth;

	// Token: 0x040007E3 RID: 2019
	public int screenHeight;

	// Token: 0x040007E4 RID: 2020
	public UIPanel[] resizePanels;

	// Token: 0x040007E5 RID: 2021
	public float panelSize;
}
