using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000BF RID: 191
public class Data
{
	// Token: 0x060005DA RID: 1498 RVA: 0x00029F5C File Offset: 0x0002815C
	public static void Init(string aGlobalsText, string aSharedText)
	{
		Data.Globals = new Dictionary<string, DicEntry>();
		Data.Shared = new Dictionary<string, DicEntry>();
		TextLoader.LoadText(aGlobalsText, Data.Globals);
		TextLoader.LoadText(aSharedText, Data.Shared);
		if (Data.Globals.ContainsKey("BasePath"))
		{
			Data.basePath = Data.Globals["BasePath"].s;
		}
		else
		{
			Data.basePath = Application.absoluteURL;
			int num = Data.basePath.LastIndexOf('/');
			Data.basePath = Data.basePath.Substring(0, num + 1);
		}
		Data.requiredBundles = new List<BundleEntry>();
		Data.stringToBundle = new Dictionary<string, WWW>();
		Data.playerBaseList = new List<PlayerBaseData>();
		GameData.Init();
		Data.playerBase = Data.playerBaseList[0];
		Data.CopyFromGlobalsToData();
		Data.splash = (Data.Shared["SplashScreen"].d.ContainsKey(Data.branding) && Data.Shared["SplashScreen"].d[Data.branding].b);
		Languages.SetReplacedFont();
	}

	// Token: 0x060005DB RID: 1499 RVA: 0x0002A07C File Offset: 0x0002827C
	public static void CopyFromGlobalsToData()
	{
		Data.scene = Data.Globals["Scene"].s;
		Data.level = Data.Globals["Level"].i;
		Data.versionNumber = Data.Globals["VersionNumber"].s;
		Data.loadUserData = Data.Globals["LoadUserData"].b;
		Data.saveList = new List<string>();
		foreach (DicEntry dicEntry in Data.Globals["SaveList"].l)
		{
			Data.saveList.Add(dicEntry.s);
		}
		Data.sfx = Data.Globals["SFX"].b;
		Data.music = Data.Globals["Music"].b;
		Data.highDetails = Data.Globals["HighDetails"].b;
		Data.fullScreenWidth = Data.Globals["FullScreenWidth"].i;
		Data.fullScreenHeight = Data.Globals["FullScreenHeight"].i;
		Data.targetFrameRate = Data.Globals["TargetFrameRate"].i;
		Data.clickLinks = Data.Globals["ClickLinks"].b;
		Data.branding = Data.Globals["Branding"].s;
		Data.platform = Data.Globals["Platform"].s;
		Data.debug = Data.Globals["Debug"].b;
		Data.cheats = Data.Globals["Cheats"].b;
		Data.useAssetBundles = Data.Globals["UseAssetBundles"].b;
		GameData.CopyFromGlobalsToGameData();
	}

	// Token: 0x060005DC RID: 1500 RVA: 0x0002A298 File Offset: 0x00028498
	public static void CopyFromDataToGlobals()
	{
		Data.Globals["Scene"].s = Data.scene;
		Data.Globals["Level"].i = Data.level;
		Data.Globals["VersionNumber"].s = Data.versionNumber;
		Data.Globals["LoadUserData"].b = Data.loadUserData;
		Data.Globals["SaveList"].l.Clear();
		foreach (string aS in Data.saveList)
		{
			Data.Globals["SaveList"].l.Add(new DicEntry(aS));
		}
		Data.Globals["SFX"].b = Data.sfx;
		Data.Globals["Music"].b = Data.music;
		Data.Globals["HighDetails"].b = Data.highDetails;
		Data.Globals["FullScreenWidth"].i = Data.fullScreenWidth;
		Data.Globals["FullScreenHeight"].i = Data.fullScreenHeight;
		Data.Globals["TargetFrameRate"].i = Data.targetFrameRate;
		Data.Globals["ClickLinks"].b = Data.clickLinks;
		Data.Globals["Branding"].s = Data.branding;
		Data.Globals["Platform"].s = Data.platform;
		Data.Globals["Debug"].b = Data.debug;
		Data.Globals["Cheats"].b = Data.cheats;
		Data.Globals["UseAssetBundles"].b = Data.useAssetBundles;
		GameData.CopyFromGameDataToGlobals();
	}

	// Token: 0x060005DD RID: 1501 RVA: 0x0002A4C4 File Offset: 0x000286C4
	public static Dictionary<string, DicEntry> GetGlobals()
	{
		return Data.Globals;
	}

	// Token: 0x0400064C RID: 1612
	private static Dictionary<string, DicEntry> Globals;

	// Token: 0x0400064D RID: 1613
	public static Dictionary<string, DicEntry> Shared;

	// Token: 0x0400064E RID: 1614
	public static string scene = "Menu";

	// Token: 0x0400064F RID: 1615
	public static int level = 1;

	// Token: 0x04000650 RID: 1616
	public static string versionNumber = string.Empty;

	// Token: 0x04000651 RID: 1617
	public static bool loadUserData;

	// Token: 0x04000652 RID: 1618
	public static List<string> saveList;

	// Token: 0x04000653 RID: 1619
	public static bool sfx = true;

	// Token: 0x04000654 RID: 1620
	public static bool music = true;

	// Token: 0x04000655 RID: 1621
	public static bool highDetails;

	// Token: 0x04000656 RID: 1622
	public static int fullScreenWidth = 800;

	// Token: 0x04000657 RID: 1623
	public static int fullScreenHeight = 450;

	// Token: 0x04000658 RID: 1624
	public static int targetFrameRate = -1;

	// Token: 0x04000659 RID: 1625
	public static string basePath = string.Empty;

	// Token: 0x0400065A RID: 1626
	public static bool clickLinks = true;

	// Token: 0x0400065B RID: 1627
	public static string branding = "None";

	// Token: 0x0400065C RID: 1628
	public static string platform = "PC";

	// Token: 0x0400065D RID: 1629
	public static bool debug;

	// Token: 0x0400065E RID: 1630
	public static bool cheats;

	// Token: 0x0400065F RID: 1631
	public static bool useAssetBundles;

	// Token: 0x04000660 RID: 1632
	public static List<BundleEntry> requiredBundles;

	// Token: 0x04000661 RID: 1633
	public static bool firstRun = true;

	// Token: 0x04000662 RID: 1634
	public static bool skipToChallenges;

	// Token: 0x04000663 RID: 1635
	public static Dictionary<string, WWW> stringToBundle;

	// Token: 0x04000664 RID: 1636
	public static bool muteAllSound;

	// Token: 0x04000665 RID: 1637
	public static List<PlayerBaseData> playerBaseList;

	// Token: 0x04000666 RID: 1638
	public static PlayerBaseData playerBase;

	// Token: 0x04000667 RID: 1639
	public static bool pause;

	// Token: 0x04000668 RID: 1640
	public static bool pausingAllowed = true;

	// Token: 0x04000669 RID: 1641
	public static bool retried;

	// Token: 0x0400066A RID: 1642
	public static bool raceInProgress = true;

	// Token: 0x0400066B RID: 1643
	public static int highScore;

	// Token: 0x0400066C RID: 1644
	public static bool splash;
}
