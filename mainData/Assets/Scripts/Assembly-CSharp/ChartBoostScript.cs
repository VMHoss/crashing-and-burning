using System;
using Chartboost;
using UnityEngine;

// Token: 0x020000BB RID: 187
public class ChartBoostScript
{
	// Token: 0x060005B1 RID: 1457 RVA: 0x000297CC File Offset: 0x000279CC
	private static void Log(string aLog)
	{
	}

	// Token: 0x060005B2 RID: 1458 RVA: 0x000297D0 File Offset: 0x000279D0
	private static void ClearLog()
	{
	}

	// Token: 0x060005B3 RID: 1459 RVA: 0x000297D4 File Offset: 0x000279D4
	public static void StartUp()
	{
		ChartBoostScript.ClearLog();
		GameObject gameObject = new GameObject("ChartBoostManager");
		gameObject.AddComponent<CBManager>();
		ChartBoostScript.Initialize();
		CBManager.didFailToLoadInterstitialEvent += ChartBoostScript.DidFailToLoadInterstitialEvent;
		CBManager.didDismissInterstitialEvent += ChartBoostScript.DidDismissInterstitialEvent;
		CBManager.didCloseInterstitialEvent += ChartBoostScript.DidCloseInterstitialEvent;
		CBManager.didClickInterstitialEvent += ChartBoostScript.DidClickInterstitialEvent;
		CBManager.didCacheInterstitialEvent += ChartBoostScript.DidCacheInterstitialEvent;
		CBManager.didShowInterstitialEvent += ChartBoostScript.DidShowInterstitialEvent;
		CBManager.didFailToLoadMoreAppsEvent += ChartBoostScript.DidFailToLoadMoreAppsEvent;
		CBManager.didDismissMoreAppsEvent += ChartBoostScript.DidDismissMoreAppsEvent;
		CBManager.didCloseMoreAppsEvent += ChartBoostScript.DidCloseMoreAppsEvent;
		CBManager.didClickMoreAppsEvent += ChartBoostScript.DidClickMoreAppsEvent;
		CBManager.didCacheMoreAppsEvent += ChartBoostScript.DidCacheMoreAppsEvent;
		CBManager.didShowMoreAppsEvent += ChartBoostScript.DidShowMoreAppsEvent;
	}

	// Token: 0x060005B4 RID: 1460 RVA: 0x000298CC File Offset: 0x00027ACC
	public static void Initialize()
	{
		ChartBoostScript.ClearLog();
		ChartBoostScript.Log("Chartboost initialize: ");
		CBBinding.init();
		ChartBoostScript.CacheInterstitial("StartUp");
		ChartBoostScript.CacheInterstitial("StartFromDock");
		ChartBoostScript.CacheInterstitial("MissionCompleted");
	}

	// Token: 0x060005B5 RID: 1461 RVA: 0x0002990C File Offset: 0x00027B0C
	private static void CacheInterstitial(string aMenuLocation)
	{
		ChartBoostScript.Log("Chartboost CacheInterstitial " + aMenuLocation);
		if (!ChartBoostScript.IsInterstitialCached(aMenuLocation))
		{
			CBBinding.cacheInterstitial(aMenuLocation);
		}
	}

	// Token: 0x060005B6 RID: 1462 RVA: 0x00029930 File Offset: 0x00027B30
	public static bool IsInterstitialCached(string aMenuLocation)
	{
		ChartBoostScript.Log("Chartboost IsInterstitialCached " + aMenuLocation);
		bool flag = CBBinding.hasCachedInterstitial(aMenuLocation);
		ChartBoostScript.Log("Chartboost Interstitial cached: " + flag);
		return flag;
	}

	// Token: 0x060005B7 RID: 1463 RVA: 0x0002996C File Offset: 0x00027B6C
	public static void ShowInterstitial(string aMenuLocation)
	{
		ChartBoostScript.ShowInterstitial(aMenuLocation, false);
	}

	// Token: 0x060005B8 RID: 1464 RVA: 0x00029978 File Offset: 0x00027B78
	public static void ShowInterstitial(string aMenuLocation, bool aForceInterstitial)
	{
		if (!GameData.boughtAnythingWithMoney && (aForceInterstitial || Time.realtimeSinceStartup - ChartBoostScript.pTimeAdShown > 30f))
		{
			ChartBoostScript.pTimeAdShown = Time.realtimeSinceStartup;
			ChartBoostScript.Log("Chartboost ShowInterstitial " + aMenuLocation);
			CBBinding.showInterstitial(aMenuLocation);
		}
		else
		{
			ChartBoostScript.Log("Chartboost ShowInterstitial blocked!");
		}
	}

	// Token: 0x060005B9 RID: 1465 RVA: 0x000299DC File Offset: 0x00027BDC
	public static void CacheMoreApps()
	{
		ChartBoostScript.Log("Chartboost CacheMoreApps");
		CBBinding.cacheMoreApps();
	}

	// Token: 0x060005BA RID: 1466 RVA: 0x000299F0 File Offset: 0x00027BF0
	public static void ShowMoreApps()
	{
		ChartBoostScript.Log("Chartboost ShowMoreApps");
		CBBinding.showMoreApps();
	}

	// Token: 0x060005BB RID: 1467 RVA: 0x00029A04 File Offset: 0x00027C04
	private static void DidFailToLoadInterstitialEvent(string aLocation)
	{
		ChartBoostScript.Log("Chartboost DidFailToLoadInterstitialEvent: " + aLocation);
	}

	// Token: 0x060005BC RID: 1468 RVA: 0x00029A18 File Offset: 0x00027C18
	private static void DidDismissInterstitialEvent(string aLocation)
	{
		ChartBoostScript.Log("Chartboost DidDismissInterstitialEvent: " + aLocation);
		if (aLocation != "None")
		{
			ChartBoostScript.CacheInterstitial(aLocation);
		}
	}

	// Token: 0x060005BD RID: 1469 RVA: 0x00029A4C File Offset: 0x00027C4C
	private static void DidCloseInterstitialEvent(string aLocation)
	{
		ChartBoostScript.Log("Chartboost DidCloseInterstitialEvent: " + aLocation);
	}

	// Token: 0x060005BE RID: 1470 RVA: 0x00029A60 File Offset: 0x00027C60
	private static void DidClickInterstitialEvent(string aLocation)
	{
		ChartBoostScript.Log("Chartboost DidClickInterstitialEvent: " + aLocation);
	}

	// Token: 0x060005BF RID: 1471 RVA: 0x00029A74 File Offset: 0x00027C74
	private static void DidCacheInterstitialEvent(string aLocation)
	{
		ChartBoostScript.Log("Chartboost DidCacheInterstitialEvent: " + aLocation);
	}

	// Token: 0x060005C0 RID: 1472 RVA: 0x00029A88 File Offset: 0x00027C88
	private static void DidShowInterstitialEvent(string aLocation)
	{
		ChartBoostScript.Log("Chartboost DidShowInterstitialEvent: " + aLocation);
	}

	// Token: 0x060005C1 RID: 1473 RVA: 0x00029A9C File Offset: 0x00027C9C
	private static void DidFailToLoadMoreAppsEvent()
	{
		ChartBoostScript.Log("Chartboost DidFailToLoadMoreAppsEvent");
	}

	// Token: 0x060005C2 RID: 1474 RVA: 0x00029AA8 File Offset: 0x00027CA8
	private static void DidDismissMoreAppsEvent()
	{
		ChartBoostScript.Log("Chartboost DidDismissMoreAppsEvent");
		ChartBoostScript.CacheMoreApps();
	}

	// Token: 0x060005C3 RID: 1475 RVA: 0x00029ABC File Offset: 0x00027CBC
	private static void DidCloseMoreAppsEvent()
	{
		ChartBoostScript.Log("Chartboost DidCloseMoreAppsEvent");
	}

	// Token: 0x060005C4 RID: 1476 RVA: 0x00029AC8 File Offset: 0x00027CC8
	private static void DidClickMoreAppsEvent()
	{
		ChartBoostScript.Log("Chartboost DidClickMoreAppsEvent");
	}

	// Token: 0x060005C5 RID: 1477 RVA: 0x00029AD4 File Offset: 0x00027CD4
	private static void DidCacheMoreAppsEvent()
	{
		ChartBoostScript.Log("Chartboost DidCacheMoreAppsEvent");
	}

	// Token: 0x060005C6 RID: 1478 RVA: 0x00029AE0 File Offset: 0x00027CE0
	private static void DidShowMoreAppsEvent()
	{
		ChartBoostScript.Log("Chartboost DidShowMoreAppsEvent");
	}

	// Token: 0x04000636 RID: 1590
	public const string LOCATION_STARTUP = "StartUp";

	// Token: 0x04000637 RID: 1591
	public const string LOCATION_STARTFROMDOCK = "StartFromDock";

	// Token: 0x04000638 RID: 1592
	public const string LOCATION_MISSIONCOMPLETED = "MissionCompleted";

	// Token: 0x04000639 RID: 1593
	public const float TIME_TILL_NEXT_AD = 30f;

	// Token: 0x0400063A RID: 1594
	public static float pTimeAdShown = -30f;
}
