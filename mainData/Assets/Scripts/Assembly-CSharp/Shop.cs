using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000122 RID: 290
public class Shop
{
	// Token: 0x06000827 RID: 2087 RVA: 0x0003D86C File Offset: 0x0003BA6C
	public static List<string> GetAllVehicles()
	{
		return Shop.pGetAllVehicles();
	}

	// Token: 0x06000828 RID: 2088 RVA: 0x0003D874 File Offset: 0x0003BA74
	public static List<string> GetUnlockedVehicles()
	{
		return GameData.unlockedVehicles;
	}

	// Token: 0x06000829 RID: 2089 RVA: 0x0003D87C File Offset: 0x0003BA7C
	public static List<string> GetBoughtVehicles()
	{
		return GameData.boughtVehicles;
	}

	// Token: 0x0600082A RID: 2090 RVA: 0x0003D884 File Offset: 0x0003BA84
	public static string GetCurrentVehicle()
	{
		return GameData.playerCar;
	}

	// Token: 0x0600082B RID: 2091 RVA: 0x0003D88C File Offset: 0x0003BA8C
	public static List<string> GetNewVehicles()
	{
		return Shop.pGetNewVehicles();
	}

	// Token: 0x0600082C RID: 2092 RVA: 0x0003D894 File Offset: 0x0003BA94
	public static bool CanBuyVehicle(string aVehicle)
	{
		return Shop.pCanBuyVehicle(aVehicle);
	}

	// Token: 0x0600082D RID: 2093 RVA: 0x0003D89C File Offset: 0x0003BA9C
	public static int GetVehicleCost(string aVehicle)
	{
		return Shop.pGetVehicleCost(aVehicle);
	}

	// Token: 0x0600082E RID: 2094 RVA: 0x0003D8A4 File Offset: 0x0003BAA4
	public static void UnlockVehicle(string aVehicle)
	{
		Shop.pUnlockVehicle(aVehicle);
	}

	// Token: 0x0600082F RID: 2095 RVA: 0x0003D8AC File Offset: 0x0003BAAC
	public static void BuyVehicle(string aVehicle)
	{
		Shop.pBuyVehicle(aVehicle);
	}

	// Token: 0x06000830 RID: 2096 RVA: 0x0003D8B4 File Offset: 0x0003BAB4
	public static void SetCurrentVehicle(string aVehicle)
	{
		Shop.pSetCurrentVehicle(aVehicle);
	}

	// Token: 0x06000831 RID: 2097 RVA: 0x0003D8BC File Offset: 0x0003BABC
	public static List<string> GetAllSuperPowers()
	{
		return Shop.pGetAllSuperPowers();
	}

	// Token: 0x06000832 RID: 2098 RVA: 0x0003D8C4 File Offset: 0x0003BAC4
	public static List<string> GetUnlockedSuperPowers()
	{
		return GameData.unlockedSuperPowers;
	}

	// Token: 0x06000833 RID: 2099 RVA: 0x0003D8CC File Offset: 0x0003BACC
	public static List<string> GetBoughtSuperPowers()
	{
		return GameData.boughtSuperPowers;
	}

	// Token: 0x06000834 RID: 2100 RVA: 0x0003D8D4 File Offset: 0x0003BAD4
	public static string GetCurrentSuperPower()
	{
		return GameData.superPower;
	}

	// Token: 0x06000835 RID: 2101 RVA: 0x0003D8DC File Offset: 0x0003BADC
	public static List<string> GetNewSuperPowers()
	{
		return Shop.pGetNewSuperPowers();
	}

	// Token: 0x06000836 RID: 2102 RVA: 0x0003D8E4 File Offset: 0x0003BAE4
	public static bool CanBuySuperPower(string aSuperPower)
	{
		return Shop.pCanBuySuperPower(aSuperPower);
	}

	// Token: 0x06000837 RID: 2103 RVA: 0x0003D8EC File Offset: 0x0003BAEC
	public static int GetSuperPowerCost(string aSuperPower)
	{
		return Shop.pGetSuperPowerCost(aSuperPower);
	}

	// Token: 0x06000838 RID: 2104 RVA: 0x0003D8F4 File Offset: 0x0003BAF4
	public static void UnlockSuperPower(string aSuperPower)
	{
		Shop.pUnlockSuperPower(aSuperPower);
	}

	// Token: 0x06000839 RID: 2105 RVA: 0x0003D8FC File Offset: 0x0003BAFC
	public static void BuySuperPower(string aSuperPower)
	{
		Shop.pBuySuperPower(aSuperPower);
	}

	// Token: 0x0600083A RID: 2106 RVA: 0x0003D904 File Offset: 0x0003BB04
	public static void SetCurrentSuperPower(string aSuperPower)
	{
		Shop.pSetCurrentSuperPower(aSuperPower);
	}

	// Token: 0x0600083B RID: 2107 RVA: 0x0003D90C File Offset: 0x0003BB0C
	public static List<string> GetAllGadgets()
	{
		return Shop.pGetAllGadgets();
	}

	// Token: 0x0600083C RID: 2108 RVA: 0x0003D914 File Offset: 0x0003BB14
	public static List<string> GetUnlockedGadgets()
	{
		return GameData.unlockedGadgets;
	}

	// Token: 0x0600083D RID: 2109 RVA: 0x0003D91C File Offset: 0x0003BB1C
	public static List<string> GetBoughtGadgets()
	{
		return GameData.boughtGadgets;
	}

	// Token: 0x0600083E RID: 2110 RVA: 0x0003D924 File Offset: 0x0003BB24
	public static string GetCurrentGadget()
	{
		return GameData.gadget;
	}

	// Token: 0x0600083F RID: 2111 RVA: 0x0003D92C File Offset: 0x0003BB2C
	public static List<string> GetNewGadgets()
	{
		return Shop.pGetNewGadgets();
	}

	// Token: 0x06000840 RID: 2112 RVA: 0x0003D934 File Offset: 0x0003BB34
	public static bool CanBuyGadget(string aGadget)
	{
		return Shop.pCanBuyGadget(aGadget);
	}

	// Token: 0x06000841 RID: 2113 RVA: 0x0003D93C File Offset: 0x0003BB3C
	public static int GetGadgetCost(string aGadget)
	{
		return Shop.pGetGadgetCost(aGadget);
	}

	// Token: 0x06000842 RID: 2114 RVA: 0x0003D944 File Offset: 0x0003BB44
	public static void UnlockGadget(string aGadget)
	{
		Shop.pUnlockGadget(aGadget);
	}

	// Token: 0x06000843 RID: 2115 RVA: 0x0003D94C File Offset: 0x0003BB4C
	public static void BuyGadget(string aGadget)
	{
		Shop.pBuyGadget(aGadget);
	}

	// Token: 0x06000844 RID: 2116 RVA: 0x0003D954 File Offset: 0x0003BB54
	public static void SetCurrentGadget(string aGadget)
	{
		Shop.pSetCurrentGadget(aGadget);
	}

	// Token: 0x06000845 RID: 2117 RVA: 0x0003D95C File Offset: 0x0003BB5C
	public static List<string> GetAllUpgrades()
	{
		return Shop.pGetAllUpgrades();
	}

	// Token: 0x06000846 RID: 2118 RVA: 0x0003D964 File Offset: 0x0003BB64
	public static List<string> GetUnlockedUpgrades()
	{
		return GameData.unlockedUpgrades;
	}

	// Token: 0x06000847 RID: 2119 RVA: 0x0003D96C File Offset: 0x0003BB6C
	public static List<string> GetNewUpgrades()
	{
		return Shop.pGetNewUpgrades();
	}

	// Token: 0x06000848 RID: 2120 RVA: 0x0003D974 File Offset: 0x0003BB74
	public static int GetUpgradeLevel(string anUpgrade)
	{
		return Shop.pGetUpgradeLevel(anUpgrade);
	}

	// Token: 0x06000849 RID: 2121 RVA: 0x0003D97C File Offset: 0x0003BB7C
	public static bool CanBuyUpgrade(string anUpgrade)
	{
		return Shop.pCanBuyUpgrade(anUpgrade);
	}

	// Token: 0x0600084A RID: 2122 RVA: 0x0003D984 File Offset: 0x0003BB84
	public static int GetUpgradeCost(string anUpgrade)
	{
		return Shop.pGetUpgradeCost(anUpgrade);
	}

	// Token: 0x0600084B RID: 2123 RVA: 0x0003D98C File Offset: 0x0003BB8C
	public static void UnlockUpgrade(string anUpgrade)
	{
		Shop.pUnlockUpgrade(anUpgrade);
	}

	// Token: 0x0600084C RID: 2124 RVA: 0x0003D994 File Offset: 0x0003BB94
	public static void BuyUpgrade(string anUpgrade)
	{
		Shop.pBuyUpgrade(anUpgrade);
	}

	// Token: 0x0600084D RID: 2125 RVA: 0x0003D99C File Offset: 0x0003BB9C
	public static void UnlockObject(string aType, string anItem)
	{
		Shop.pUnlockObject(aType, anItem);
	}

	// Token: 0x0600084E RID: 2126 RVA: 0x0003D9A8 File Offset: 0x0003BBA8
	public static UnlockItemInfo ObjectUnlockedAt(string anObject)
	{
		return Shop.pObjectUnlockedAt(anObject);
	}

	// Token: 0x0600084F RID: 2127 RVA: 0x0003D9B0 File Offset: 0x0003BBB0
	private static List<string> pGetAllVehicles()
	{
		List<string> list = new List<string>();
		foreach (KeyValuePair<string, DicEntry> keyValuePair in Data.Shared["Car"].d)
		{
			list.Add(keyValuePair.Key);
		}
		return list;
	}

	// Token: 0x06000850 RID: 2128 RVA: 0x0003DA34 File Offset: 0x0003BC34
	private static List<string> pGetNewVehicles()
	{
		List<string> list = new List<string>();
		List<string> list2 = Shop.pGetAllVehicles();
		foreach (string item in GameData.newItemList)
		{
			if (list2.Contains(item))
			{
				list.Add(item);
			}
		}
		return list;
	}

	// Token: 0x06000851 RID: 2129 RVA: 0x0003DAB4 File Offset: 0x0003BCB4
	private static bool pCanBuyVehicle(string aVehicle)
	{
		return GameData.unlockedVehicles.Contains(aVehicle) && !GameData.boughtVehicles.Contains(aVehicle) && GameData.cash >= Shop.pGetVehicleCost(aVehicle);
	}

	// Token: 0x06000852 RID: 2130 RVA: 0x0003DAF8 File Offset: 0x0003BCF8
	private static int pGetVehicleCost(string aVehicle)
	{
		return Data.Shared["CarSetting"].d[aVehicle].d["Cost"].i;
	}

	// Token: 0x06000853 RID: 2131 RVA: 0x0003DB34 File Offset: 0x0003BD34
	private static void pUnlockVehicle(string aVehicle)
	{
		if (GameData.unlockedVehicles.Contains(aVehicle))
		{
			return;
		}
		GameData.unlockedVehicles.Add(aVehicle);
		GameData.newItemList.Add(aVehicle);
	}

	// Token: 0x06000854 RID: 2132 RVA: 0x0003DB60 File Offset: 0x0003BD60
	private static void pBuyVehicle(string aVehicle)
	{
		if (!Shop.pCanBuyVehicle(aVehicle))
		{
			return;
		}
		FlurryScript.LogEvent("bought vehicle", new string[]
		{
			aVehicle
		});
		GameData.cash -= Shop.pGetVehicleCost(aVehicle);
		GameData.boughtVehicles.Add(aVehicle);
		Scripts.medalsManager.UpdateMedal(6, 1);
		Scripts.medalsManager.UpdateMedal(3, 0);
		if (GameData.newItemList.Contains(aVehicle))
		{
			GameData.newItemList.Remove(aVehicle);
		}
	}

	// Token: 0x06000855 RID: 2133 RVA: 0x0003DBE0 File Offset: 0x0003BDE0
	private static void pSetCurrentVehicle(string aVehicle)
	{
		if (!GameData.boughtVehicles.Contains(aVehicle))
		{
			return;
		}
		GameData.lastPlayerSelectVehicle = aVehicle;
		GameData.playerCar = aVehicle;
		Scripts.trackScript.trackManager.SwitchPlayerVehicle(Data.Shared["Car"].d[aVehicle].d["Model"].s);
	}

	// Token: 0x06000856 RID: 2134 RVA: 0x0003DC48 File Offset: 0x0003BE48
	private static List<string> pGetAllSuperPowers()
	{
		List<string> list = new List<string>();
		foreach (KeyValuePair<string, DicEntry> keyValuePair in Data.Shared["SuperPowers"].d)
		{
			list.Add(keyValuePair.Key);
		}
		return list;
	}

	// Token: 0x06000857 RID: 2135 RVA: 0x0003DCCC File Offset: 0x0003BECC
	private static List<string> pGetNewSuperPowers()
	{
		List<string> list = new List<string>();
		List<string> list2 = Shop.pGetAllSuperPowers();
		foreach (string item in GameData.newItemList)
		{
			if (list2.Contains(item))
			{
				list.Add(item);
			}
		}
		return list;
	}

	// Token: 0x06000858 RID: 2136 RVA: 0x0003DD4C File Offset: 0x0003BF4C
	private static bool pCanBuySuperPower(string aSuperPower)
	{
		return GameData.unlockedSuperPowers.Contains(aSuperPower) && !GameData.boughtSuperPowers.Contains(aSuperPower) && GameData.cash >= Shop.pGetSuperPowerCost(aSuperPower);
	}

	// Token: 0x06000859 RID: 2137 RVA: 0x0003DD90 File Offset: 0x0003BF90
	private static int pGetSuperPowerCost(string aSuperPower)
	{
		return Data.Shared["SuperPowers"].d[aSuperPower].d["Cost"].i;
	}

	// Token: 0x0600085A RID: 2138 RVA: 0x0003DDCC File Offset: 0x0003BFCC
	private static void pUnlockSuperPower(string aSuperPower)
	{
		if (GameData.unlockedSuperPowers.Contains(aSuperPower))
		{
			return;
		}
		GameData.unlockedSuperPowers.Add(aSuperPower);
		GameData.newItemList.Add(aSuperPower);
	}

	// Token: 0x0600085B RID: 2139 RVA: 0x0003DDF8 File Offset: 0x0003BFF8
	private static void pBuySuperPower(string aSuperPower)
	{
		if (!Shop.pCanBuySuperPower(aSuperPower))
		{
			return;
		}
		FlurryScript.LogEvent("bought superpower", new string[]
		{
			aSuperPower
		});
		GameData.cash -= Shop.pGetSuperPowerCost(aSuperPower);
		GameData.boughtSuperPowers.Add(aSuperPower);
		Scripts.medalsManager.UpdateMedal(3, 0);
		if (GameData.newItemList.Contains(aSuperPower))
		{
			GameData.newItemList.Remove(aSuperPower);
		}
	}

	// Token: 0x0600085C RID: 2140 RVA: 0x0003DE6C File Offset: 0x0003C06C
	private static void pSetCurrentSuperPower(string aSuperPower)
	{
		GameData.superPower = aSuperPower;
	}

	// Token: 0x0600085D RID: 2141 RVA: 0x0003DE74 File Offset: 0x0003C074
	private static List<string> pGetAllGadgets()
	{
		List<string> list = new List<string>();
		foreach (KeyValuePair<string, DicEntry> keyValuePair in Data.Shared["Gadgets"].d)
		{
			list.Add(keyValuePair.Key);
		}
		return list;
	}

	// Token: 0x0600085E RID: 2142 RVA: 0x0003DEF8 File Offset: 0x0003C0F8
	private static List<string> pGetNewGadgets()
	{
		List<string> list = new List<string>();
		List<string> list2 = Shop.pGetAllGadgets();
		foreach (string item in GameData.newItemList)
		{
			if (list2.Contains(item))
			{
				list.Add(item);
			}
		}
		return list;
	}

	// Token: 0x0600085F RID: 2143 RVA: 0x0003DF78 File Offset: 0x0003C178
	private static bool pCanBuyGadget(string aGadget)
	{
		return GameData.unlockedGadgets.Contains(aGadget) && GameData.cash >= Shop.pGetGadgetCost(aGadget);
	}

	// Token: 0x06000860 RID: 2144 RVA: 0x0003DFA8 File Offset: 0x0003C1A8
	private static int pGetGadgetCost(string aGadget)
	{
		return Data.Shared["Gadgets"].d[aGadget].d["Cost"].i;
	}

	// Token: 0x06000861 RID: 2145 RVA: 0x0003DFE4 File Offset: 0x0003C1E4
	private static void pUnlockGadget(string aGadget)
	{
		if (GameData.unlockedGadgets.Contains(aGadget))
		{
			return;
		}
		GameData.unlockedGadgets.Add(aGadget);
		GameData.newItemList.Add(aGadget);
	}

	// Token: 0x06000862 RID: 2146 RVA: 0x0003E010 File Offset: 0x0003C210
	private static void pBuyGadget(string aGadget)
	{
		if (!Shop.pCanBuyGadget(aGadget))
		{
			return;
		}
		FlurryScript.LogEvent("bought gadget", new string[]
		{
			aGadget
		});
		GameData.cash -= Shop.pGetGadgetCost(aGadget);
		GameData.boughtGadgets.Add(aGadget);
		Scripts.medalsManager.UpdateMedal(3, 0);
		if (GameData.newItemList.Contains(aGadget))
		{
			GameData.newItemList.Remove(aGadget);
		}
	}

	// Token: 0x06000863 RID: 2147 RVA: 0x0003E084 File Offset: 0x0003C284
	private static void pSetCurrentGadget(string aGadget)
	{
		GameData.gadget = aGadget;
	}

	// Token: 0x06000864 RID: 2148 RVA: 0x0003E08C File Offset: 0x0003C28C
	private static List<string> pGetAllUpgrades()
	{
		List<string> list = new List<string>();
		foreach (KeyValuePair<string, DicEntry> keyValuePair in Data.Shared["Upgrades"].d)
		{
			list.Add(keyValuePair.Key);
		}
		return list;
	}

	// Token: 0x06000865 RID: 2149 RVA: 0x0003E110 File Offset: 0x0003C310
	private static List<string> pGetNewUpgrades()
	{
		List<string> list = new List<string>();
		List<string> list2 = Shop.pGetAllUpgrades();
		foreach (string item in GameData.newItemList)
		{
			if (list2.Contains(item))
			{
				list.Add(item);
			}
		}
		return list;
	}

	// Token: 0x06000866 RID: 2150 RVA: 0x0003E190 File Offset: 0x0003C390
	private static int pGetUpgradeLevel(string anUpgrade)
	{
		return GameData.upgradedLevels[anUpgrade];
	}

	// Token: 0x06000867 RID: 2151 RVA: 0x0003E1A0 File Offset: 0x0003C3A0
	private static bool pCanBuyUpgrade(string anUpgrade)
	{
		if (!GameData.unlockedUpgrades.Contains(anUpgrade))
		{
			return false;
		}
		int num = GameData.upgradedLevels[anUpgrade];
		return num != 5 && GameData.cash >= Shop.pGetUpgradeCost(anUpgrade);
	}

	// Token: 0x06000868 RID: 2152 RVA: 0x0003E1E4 File Offset: 0x0003C3E4
	private static int pGetUpgradeCost(string anUpgrade)
	{
		int num = GameData.upgradedLevels[anUpgrade];
		if (num == 5)
		{
			return 0;
		}
		return Data.Shared["Upgrades"].d[anUpgrade].d["Cost" + (num + 1)].i;
	}

	// Token: 0x06000869 RID: 2153 RVA: 0x0003E244 File Offset: 0x0003C444
	private static void pUnlockUpgrade(string anUpgrade)
	{
		if (GameData.unlockedUpgrades.Contains(anUpgrade))
		{
			return;
		}
		GameData.unlockedUpgrades.Add(anUpgrade);
		GameData.newItemList.Add(anUpgrade);
	}

	// Token: 0x0600086A RID: 2154 RVA: 0x0003E270 File Offset: 0x0003C470
	private static void pBuyUpgrade(string anUpgrade)
	{
		if (!Shop.pCanBuyUpgrade(anUpgrade))
		{
			return;
		}
		FlurryScript.LogEvent("bought upgrade", new string[]
		{
			anUpgrade
		});
		GameData.cash -= Shop.pGetUpgradeCost(anUpgrade);
		Dictionary<string, int> upgradedLevels;
		Dictionary<string, int> dictionary = upgradedLevels = GameData.upgradedLevels;
		int num = upgradedLevels[anUpgrade];
		dictionary[anUpgrade] = num + 1;
		Scripts.medalsManager.UpdateMedal(3, 0);
		if (GameData.newItemList.Contains(anUpgrade))
		{
			GameData.newItemList.Remove(anUpgrade);
		}
	}

	// Token: 0x0600086B RID: 2155 RVA: 0x0003E2F0 File Offset: 0x0003C4F0
	private static void pUnlockObject(string aType, string anItem)
	{
		if (aType != null)
		{
			if (Shop.<>f__switch$map23 == null)
			{
				Shop.<>f__switch$map23 = new Dictionary<string, int>(2)
				{
					{
						"Vehicle",
						0
					},
					{
						"SuperPower",
						1
					}
				};
			}
			int num;
			if (Shop.<>f__switch$map23.TryGetValue(aType, out num))
			{
				if (num != 0)
				{
					if (num != 1)
					{
						goto IL_71;
					}
					Shop.pUnlockSuperPower(anItem);
				}
				else
				{
					Shop.pUnlockVehicle(anItem);
				}
				return;
			}
		}
		IL_71:
		throw new UnityException("Unknown type in Unlock::pUnlockObject, type: " + aType);
	}

	// Token: 0x0600086C RID: 2156 RVA: 0x0003E380 File Offset: 0x0003C580
	private static UnlockItemInfo pObjectUnlockedAt(string anObject)
	{
		return new UnlockItemInfo();
	}
}
