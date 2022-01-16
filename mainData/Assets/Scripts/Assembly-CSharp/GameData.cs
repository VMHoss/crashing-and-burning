using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000ED RID: 237
public class GameData
{
	// Token: 0x0600071F RID: 1823 RVA: 0x00034910 File Offset: 0x00032B10
	public static void Init()
	{
		GameData.Globals = Data.GetGlobals();
		GameData.playerList = new List<PlayerData>();
		new PlayerData("Player1");
		GameData.player = GameData.playerList[0];
		GameData.defaultLayer = LayerMask.NameToLayer("Default");
		GameData.interfaceLayer = LayerMask.NameToLayer("Interface");
		GameData.carLayer = LayerMask.NameToLayer("Car");
		GameData.trafficLayer = LayerMask.NameToLayer("Traffic");
		GameData.destructibleLayer = LayerMask.NameToLayer("Destructible");
		GameData.carPartLayer = LayerMask.NameToLayer("CarPart");
		GameData.damagedTrafficLayer = LayerMask.NameToLayer("DamagedTraffic");
		GameData.skyboxLayer = LayerMask.NameToLayer("Skybox");
		GameData.invisibleWallLayer = LayerMask.NameToLayer("InvisibleWall");
		GameData.minimapLayer = LayerMask.NameToLayer("mapsystem");
		GameData.trafficDetectorLayer = LayerMask.NameToLayer("TrafficDetector");
		GameData.gravityMagnitude = Physics.gravity.magnitude;
		GameData.fixedTimeStep = Time.fixedDeltaTime;
	}

	// Token: 0x06000720 RID: 1824 RVA: 0x00034A10 File Offset: 0x00032C10
	public static void Start()
	{
		if (Data.scene == "Level")
		{
			Data.requiredBundles.Add(new BundleEntry("Shared", "Shared"));
			Data.requiredBundles.Add(new BundleEntry("Levels", "Level" + Data.level));
			Data.requiredBundles.Add(new BundleEntry("Vehicles", "Vehicles"));
		}
		if (Data.branding == "Miniclip")
		{
			GameObject gameObject = new GameObject("MiniclipAPI");
			UnityEngine.Object.DontDestroyOnLoad(gameObject);
			GameData.miniclipManager = gameObject.AddComponent<MiniclipManager>();
		}
		if (Data.branding == "Shockwave3D")
		{
			Data.branding = "Xform";
		}
		GameData.medalProgression = GameData.Globals["MedalProgression"].d;
		Scripts.medalsManager = new MedalsManager();
		Options.SetVisuals();
		GameData.vehicles = new Dictionary<string, CarData>();
		GameData.destroyedTrafficList = new List<ScoreEntry>();
		GameData.destroyedDestructiblesList = new List<ScoreEntry>();
		GameData.destroyedBuildingsList = new List<ScoreEntry>();
		GameData.obtainedCashList = new List<ScoreEntry>();
		GameData.obtainedExtrasList = new List<ScoreEntry>();
		GameData.lastPlayerSelectVehicle = GameData.playerCar;
		GameData.completedMissions = new List<Mission>();
		GameData.missionRewards = new List<Reward>();
		GameData.levelUpRewards = new List<Reward>();
		GameData.newRewardFromCrate = null;
		GameData.carFoV = Data.Shared["Misc"].d["CarFoV"].f;
		Missions.InitMissions();
		if (Data.branding == "ClickJogos")
		{
			ClickJogos.Initialize();
		}
		ChartBoostScript.StartUp();
		if (Data.platform != "PC")
		{
			GameData.destrHealthMultiplier = 0.3f;
		}
		GameData.numPlays++;
		UserData.Save();
		UnibillScript.Initialize();
		if (Screen.orientation != ScreenOrientation.LandscapeLeft && Screen.orientation != ScreenOrientation.LandscapeRight)
		{
			Screen.orientation = ScreenOrientation.LandscapeLeft;
		}
		Application.LoadLevel("Loading");
	}

	// Token: 0x06000721 RID: 1825 RVA: 0x00034C10 File Offset: 0x00032E10
	public static void CopyFromGlobalsToGameData()
	{
		string s = GameData.Globals["GameState"].s;
		switch (s)
		{
		case "Menu":
			GameData.gameState = GameData.GameState.MENU;
			goto IL_D6;
		case "Shop":
			GameData.gameState = GameData.GameState.SHOP;
			goto IL_D6;
		case "Game":
			GameData.gameState = GameData.GameState.GAME;
			goto IL_D6;
		}
		Debug.LogError("Unknown game state: " + GameData.Globals["GameState"].s + " , made a typo?");
		IL_D6:
		GameData.playerCar = GameData.Globals["PlayerCar"].s;
		GameData.superPower = GameData.Globals["SuperPower"].s;
		GameData.gadget = GameData.Globals["Gadget"].s;
		GameData.cash = GameData.Globals["Cash"].i;
		GameData.playerLevel = GameData.Globals["PlayerLevel"].i;
		GameData.XPWithinLevel = GameData.Globals["XPWithinLevel"].i;
		GameData.currentSafeHouse = GameData.Globals["CurrentSafeHouse"].i;
		GameData.obtainedNuclearDetonators = new List<int>();
		foreach (DicEntry dicEntry in GameData.Globals["ObtainedNuclearDetonators"].l)
		{
			GameData.obtainedNuclearDetonators.Add(dicEntry.i);
		}
		GameData.obtainedHiddenPackages = new List<int>();
		foreach (DicEntry dicEntry2 in GameData.Globals["ObtainedHiddenPackages"].l)
		{
			GameData.obtainedHiddenPackages.Add(dicEntry2.i);
		}
		GameData.obtainedCashStashes = new List<int>();
		foreach (DicEntry dicEntry3 in GameData.Globals["ObtainedCashStashes"].l)
		{
			GameData.obtainedCashStashes.Add(dicEntry3.i);
		}
		GameData.obtainedSuperSpecialTrophies = new List<int>();
		foreach (DicEntry dicEntry4 in GameData.Globals["ObtainedSuperSpecialTrophies"].l)
		{
			GameData.obtainedSuperSpecialTrophies.Add(dicEntry4.i);
		}
		GameData.unlockedVehicles = new List<string>();
		foreach (DicEntry dicEntry5 in GameData.Globals["UnlockedVehicles"].l)
		{
			GameData.unlockedVehicles.Add(dicEntry5.s);
		}
		GameData.unlockedSuperPowers = new List<string>();
		foreach (DicEntry dicEntry6 in GameData.Globals["UnlockedSuperPowers"].l)
		{
			GameData.unlockedSuperPowers.Add(dicEntry6.s);
		}
		GameData.unlockedGadgets = new List<string>();
		foreach (DicEntry dicEntry7 in GameData.Globals["UnlockedGadgets"].l)
		{
			GameData.unlockedGadgets.Add(dicEntry7.s);
		}
		GameData.unlockedUpgrades = new List<string>();
		foreach (DicEntry dicEntry8 in GameData.Globals["UnlockedUpgrades"].l)
		{
			GameData.unlockedUpgrades.Add(dicEntry8.s);
		}
		GameData.boughtVehicles = new List<string>();
		foreach (DicEntry dicEntry9 in GameData.Globals["BoughtVehicles"].l)
		{
			GameData.boughtVehicles.Add(dicEntry9.s);
		}
		GameData.boughtSuperPowers = new List<string>();
		foreach (DicEntry dicEntry10 in GameData.Globals["BoughtSuperPowers"].l)
		{
			GameData.boughtSuperPowers.Add(dicEntry10.s);
		}
		GameData.boughtGadgets = new List<string>();
		foreach (DicEntry dicEntry11 in GameData.Globals["BoughtGadgets"].l)
		{
			GameData.boughtGadgets.Add(dicEntry11.s);
		}
		GameData.upgradedLevels = new Dictionary<string, int>();
		foreach (KeyValuePair<string, DicEntry> keyValuePair in GameData.Globals["UpgradedLevels"].d)
		{
			GameData.upgradedLevels.Add(keyValuePair.Key, keyValuePair.Value.i);
		}
		GameData.newItemList = new List<string>();
		foreach (DicEntry dicEntry12 in GameData.Globals["NewItemList"].l)
		{
			GameData.newItemList.Add(dicEntry12.s);
		}
		GameData.mainMissionNum = GameData.Globals["MainMissionNum"].i;
		GameData.switchControls = GameData.Globals["SwitchControls"].b;
		GameData.boughtAnythingWithMoney = GameData.Globals["BoughtAnythingWithMoney"].b;
		GameData.numPlays = GameData.Globals["NumPlays"].i;
		GameData.enableMiniMapOnMobile = GameData.Globals["EnableMiniMapOnMobile"].b;
	}

	// Token: 0x06000722 RID: 1826 RVA: 0x0003545C File Offset: 0x0003365C
	public static void CopyFromGameDataToGlobals()
	{
		switch (GameData.gameState)
		{
		case GameData.GameState.MENU:
			GameData.Globals["GameState"].s = "Menu";
			break;
		case GameData.GameState.SHOP:
			GameData.Globals["GameState"].s = "Shop";
			break;
		case GameData.GameState.GAME:
			GameData.Globals["GameState"].s = "Game";
			break;
		}
		GameData.Globals["PlayerCar"].s = GameData.playerCar;
		GameData.Globals["SuperPower"].s = GameData.superPower;
		GameData.Globals["Gadget"].s = GameData.gadget;
		GameData.Globals["Cash"].i = GameData.cash;
		GameData.Globals["PlayerLevel"].i = GameData.playerLevel;
		GameData.Globals["XPWithinLevel"].i = GameData.XPWithinLevel;
		GameData.Globals["CurrentSafeHouse"].i = GameData.currentSafeHouse;
		GameData.Globals["ObtainedNuclearDetonators"].l.Clear();
		foreach (int aI in GameData.obtainedNuclearDetonators)
		{
			GameData.Globals["ObtainedNuclearDetonators"].l.Add(new DicEntry(aI));
		}
		GameData.Globals["ObtainedHiddenPackages"].l.Clear();
		foreach (int aI2 in GameData.obtainedHiddenPackages)
		{
			GameData.Globals["ObtainedHiddenPackages"].l.Add(new DicEntry(aI2));
		}
		GameData.Globals["ObtainedCashStashes"].l.Clear();
		foreach (int aI3 in GameData.obtainedCashStashes)
		{
			GameData.Globals["ObtainedCashStashes"].l.Add(new DicEntry(aI3));
		}
		GameData.Globals["ObtainedSuperSpecialTrophies"].l.Clear();
		foreach (int aI4 in GameData.obtainedSuperSpecialTrophies)
		{
			GameData.Globals["ObtainedSuperSpecialTrophies"].l.Add(new DicEntry(aI4));
		}
		GameData.Globals["UnlockedVehicles"].l.Clear();
		foreach (string aS in GameData.unlockedVehicles)
		{
			GameData.Globals["UnlockedVehicles"].l.Add(new DicEntry(aS));
		}
		GameData.Globals["UnlockedSuperPowers"].l.Clear();
		foreach (string aS2 in GameData.unlockedSuperPowers)
		{
			GameData.Globals["UnlockedSuperPowers"].l.Add(new DicEntry(aS2));
		}
		GameData.Globals["UnlockedGadgets"].l.Clear();
		foreach (string aS3 in GameData.unlockedGadgets)
		{
			GameData.Globals["UnlockedGadgets"].l.Add(new DicEntry(aS3));
		}
		GameData.Globals["UnlockedUpgrades"].l.Clear();
		foreach (string aS4 in GameData.unlockedUpgrades)
		{
			GameData.Globals["UnlockedUpgrades"].l.Add(new DicEntry(aS4));
		}
		GameData.Globals["BoughtVehicles"].l.Clear();
		foreach (string aS5 in GameData.boughtVehicles)
		{
			GameData.Globals["BoughtVehicles"].l.Add(new DicEntry(aS5));
		}
		GameData.Globals["BoughtSuperPowers"].l.Clear();
		foreach (string aS6 in GameData.boughtSuperPowers)
		{
			GameData.Globals["BoughtSuperPowers"].l.Add(new DicEntry(aS6));
		}
		GameData.Globals["BoughtGadgets"].l.Clear();
		foreach (string aS7 in GameData.boughtGadgets)
		{
			GameData.Globals["BoughtGadgets"].l.Add(new DicEntry(aS7));
		}
		GameData.Globals["UpgradedLevels"].d.Clear();
		foreach (KeyValuePair<string, int> keyValuePair in GameData.upgradedLevels)
		{
			GameData.Globals["UpgradedLevels"].d.Add(keyValuePair.Key, new DicEntry(keyValuePair.Value));
		}
		GameData.Globals["NewItemList"].l.Clear();
		foreach (string aS8 in GameData.newItemList)
		{
			GameData.Globals["NewItemList"].l.Add(new DicEntry(aS8));
		}
		GameData.Globals["MainMissionNum"].i = GameData.mainMissionNum;
		GameData.Globals["SwitchControls"].b = GameData.switchControls;
		GameData.Globals["BoughtAnythingWithMoney"].b = GameData.boughtAnythingWithMoney;
		GameData.Globals["NumPlays"].i = GameData.numPlays;
		GameData.Globals["EnableMiniMapOnMobile"].b = GameData.enableMiniMapOnMobile;
	}

	// Token: 0x06000723 RID: 1827 RVA: 0x00035D0C File Offset: 0x00033F0C
	public static void OnApplicationPause(bool aPaused)
	{
		if (GameData.pAvoidStartUpPauseAndroid)
		{
			GameData.pAvoidStartUpPauseAndroid = false;
			return;
		}
		if (!aPaused)
		{
			ChartBoostScript.Initialize();
			if (!GameData.skipAdAfterShopPurchase)
			{
				ChartBoostScript.ShowInterstitial("StartFromDock");
			}
			GameData.skipAdAfterShopPurchase = false;
		}
	}

	// Token: 0x04000752 RID: 1874
	private static Dictionary<string, DicEntry> Globals;

	// Token: 0x04000753 RID: 1875
	public static Dictionary<string, DicEntry> medalProgression;

	// Token: 0x04000754 RID: 1876
	public static string playerCar = "Lotus";

	// Token: 0x04000755 RID: 1877
	public static string superPower = "StuntMan";

	// Token: 0x04000756 RID: 1878
	public static string gadget = "Boost";

	// Token: 0x04000757 RID: 1879
	public static int cash;

	// Token: 0x04000758 RID: 1880
	public static int playerLevel = 1;

	// Token: 0x04000759 RID: 1881
	public static int XPWithinLevel;

	// Token: 0x0400075A RID: 1882
	public static int currentSafeHouse;

	// Token: 0x0400075B RID: 1883
	public static List<int> obtainedNuclearDetonators;

	// Token: 0x0400075C RID: 1884
	public static List<int> obtainedHiddenPackages;

	// Token: 0x0400075D RID: 1885
	public static List<int> obtainedCashStashes;

	// Token: 0x0400075E RID: 1886
	public static List<int> obtainedSuperSpecialTrophies;

	// Token: 0x0400075F RID: 1887
	public static List<string> unlockedVehicles;

	// Token: 0x04000760 RID: 1888
	public static List<string> unlockedSuperPowers;

	// Token: 0x04000761 RID: 1889
	public static List<string> unlockedGadgets;

	// Token: 0x04000762 RID: 1890
	public static List<string> unlockedUpgrades;

	// Token: 0x04000763 RID: 1891
	public static List<string> boughtVehicles;

	// Token: 0x04000764 RID: 1892
	public static List<string> boughtSuperPowers;

	// Token: 0x04000765 RID: 1893
	public static List<string> boughtGadgets;

	// Token: 0x04000766 RID: 1894
	public static Dictionary<string, int> upgradedLevels;

	// Token: 0x04000767 RID: 1895
	public static List<string> newItemList;

	// Token: 0x04000768 RID: 1896
	public static int mainMissionNum = 1;

	// Token: 0x04000769 RID: 1897
	public static bool switchControls;

	// Token: 0x0400076A RID: 1898
	public static bool boughtAnythingWithMoney;

	// Token: 0x0400076B RID: 1899
	public static int numPlays;

	// Token: 0x0400076C RID: 1900
	public static bool enableMiniMapOnMobile;

	// Token: 0x0400076D RID: 1901
	public static List<PlayerData> playerList;

	// Token: 0x0400076E RID: 1902
	public static PlayerData player;

	// Token: 0x0400076F RID: 1903
	public static GameData.GameState gameState;

	// Token: 0x04000770 RID: 1904
	public static string lastPlayerSelectVehicle = "Lotus";

	// Token: 0x04000771 RID: 1905
	public static bool showMission = true;

	// Token: 0x04000772 RID: 1906
	public static bool afterBlast;

	// Token: 0x04000773 RID: 1907
	public static bool nuclearDetonator;

	// Token: 0x04000774 RID: 1908
	public static bool coinMagnet;

	// Token: 0x04000775 RID: 1909
	public static bool coinDuplicator;

	// Token: 0x04000776 RID: 1910
	public static bool retried;

	// Token: 0x04000777 RID: 1911
	public static bool raceInProgress;

	// Token: 0x04000778 RID: 1912
	public static string playerCarName = "Default";

	// Token: 0x04000779 RID: 1913
	public static PhysicMaterial trackPhysicMaterial;

	// Token: 0x0400077A RID: 1914
	public static float carFoV = 47f;

	// Token: 0x0400077B RID: 1915
	public static CarScript playerCarScript;

	// Token: 0x0400077C RID: 1916
	public static Vector3 lastPlayerVelocity;

	// Token: 0x0400077D RID: 1917
	public static Vector3 lastPlayerAngularVelocity;

	// Token: 0x0400077E RID: 1918
	public static int gainedCoins;

	// Token: 0x0400077F RID: 1919
	public static bool godMode;

	// Token: 0x04000780 RID: 1920
	public static Mission mainMission;

	// Token: 0x04000781 RID: 1921
	public static List<Mission> completedMissions;

	// Token: 0x04000782 RID: 1922
	public static List<Reward> missionRewards;

	// Token: 0x04000783 RID: 1923
	public static List<Reward> levelUpRewards;

	// Token: 0x04000784 RID: 1924
	public static Reward newRewardFromCrate;

	// Token: 0x04000785 RID: 1925
	public static Dictionary<string, CarData> vehicles;

	// Token: 0x04000786 RID: 1926
	public static Material sharedTrafficMaterial;

	// Token: 0x04000787 RID: 1927
	public static bool showChainReaction;

	// Token: 0x04000788 RID: 1928
	public static int currentChainReaction;

	// Token: 0x04000789 RID: 1929
	public static float chainReactionTimeLeft = -1f;

	// Token: 0x0400078A RID: 1930
	public static int trafficScore;

	// Token: 0x0400078B RID: 1931
	public static List<ScoreEntry> destroyedTrafficList;

	// Token: 0x0400078C RID: 1932
	public static int destructiblesScore;

	// Token: 0x0400078D RID: 1933
	public static List<ScoreEntry> destroyedDestructiblesList;

	// Token: 0x0400078E RID: 1934
	public static int buildingsScore;

	// Token: 0x0400078F RID: 1935
	public static List<ScoreEntry> destroyedBuildingsList;

	// Token: 0x04000790 RID: 1936
	public static List<ScoreEntry> obtainedCashList;

	// Token: 0x04000791 RID: 1937
	public static int cashScore;

	// Token: 0x04000792 RID: 1938
	public static List<ScoreEntry> obtainedExtrasList;

	// Token: 0x04000793 RID: 1939
	public static int extraScore;

	// Token: 0x04000794 RID: 1940
	public static int scoreMultiplier = 1;

	// Token: 0x04000795 RID: 1941
	public static int totalScore;

	// Token: 0x04000796 RID: 1942
	public static bool spawnPickUps = true;

	// Token: 0x04000797 RID: 1943
	public static bool spawnBuildings = true;

	// Token: 0x04000798 RID: 1944
	public static bool spawnTraffic = true;

	// Token: 0x04000799 RID: 1945
	public static bool spawnDestructibles = true;

	// Token: 0x0400079A RID: 1946
	public static bool spawnObjects = true;

	// Token: 0x0400079B RID: 1947
	public static bool disableAudio;

	// Token: 0x0400079C RID: 1948
	public static bool disableFog;

	// Token: 0x0400079D RID: 1949
	public static bool disablePhysics;

	// Token: 0x0400079E RID: 1950
	public static int defaultLayer;

	// Token: 0x0400079F RID: 1951
	public static int interfaceLayer;

	// Token: 0x040007A0 RID: 1952
	public static int carLayer;

	// Token: 0x040007A1 RID: 1953
	public static int trafficLayer;

	// Token: 0x040007A2 RID: 1954
	public static int destructibleLayer;

	// Token: 0x040007A3 RID: 1955
	public static int carPartLayer;

	// Token: 0x040007A4 RID: 1956
	public static int damagedTrafficLayer;

	// Token: 0x040007A5 RID: 1957
	public static int skyboxLayer;

	// Token: 0x040007A6 RID: 1958
	public static int invisibleWallLayer;

	// Token: 0x040007A7 RID: 1959
	public static int minimapLayer;

	// Token: 0x040007A8 RID: 1960
	public static int trafficDetectorLayer;

	// Token: 0x040007A9 RID: 1961
	public static float gravityMagnitude;

	// Token: 0x040007AA RID: 1962
	public static MiniclipManager miniclipManager;

	// Token: 0x040007AB RID: 1963
	public static float fixedTimeStep = 0.02f;

	// Token: 0x040007AC RID: 1964
	public static bool skipAdAfterShopPurchase;

	// Token: 0x040007AD RID: 1965
	private static bool pAvoidStartUpPauseAndroid = true;

	// Token: 0x040007AE RID: 1966
	public static float destrHealthMultiplier = 1f;

	// Token: 0x020000EE RID: 238
	public enum GameState
	{
		// Token: 0x040007B1 RID: 1969
		MENU,
		// Token: 0x040007B2 RID: 1970
		SHOP,
		// Token: 0x040007B3 RID: 1971
		GAME
	}
}
