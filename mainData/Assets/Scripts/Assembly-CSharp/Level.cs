using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000FB RID: 251
public class Level
{
	// Token: 0x06000783 RID: 1923 RVA: 0x00039524 File Offset: 0x00037724
	public static bool AddExperience(int aXP)
	{
		return Level.pAddExperience(aXP);
	}

	// Token: 0x06000784 RID: 1924 RVA: 0x0003952C File Offset: 0x0003772C
	public static bool AddExperiencePoint()
	{
		return Level.pAddExperiencePoint();
	}

	// Token: 0x06000785 RID: 1925 RVA: 0x00039534 File Offset: 0x00037734
	public static int GetReqXPInCurLevel()
	{
		return Level.pGetReqXPInCurLevel();
	}

	// Token: 0x06000786 RID: 1926 RVA: 0x0003953C File Offset: 0x0003773C
	public static void ComputePlayerLevelAndXPBasedOnMissions()
	{
		Level.pComputePlayerLevelAndXPBasedOnMissions();
	}

	// Token: 0x06000787 RID: 1927 RVA: 0x00039544 File Offset: 0x00037744
	private static bool pAddExperience(int aXP)
	{
		GameData.XPWithinLevel += aXP;
		Dictionary<string, DicEntry> d;
		if (Data.Shared["Levels"].d.ContainsKey("Level" + (GameData.playerLevel + 1)))
		{
			d = Data.Shared["Levels"].d["Level" + (GameData.playerLevel + 1)].d;
		}
		else
		{
			d = Data.Shared["Levels"].d["Level11"].d;
		}
		Debug.Log(string.Concat(new object[]
		{
			"Added XP: ",
			aXP,
			"- xp progress: ",
			GameData.XPWithinLevel,
			"/",
			d["XP"].i
		}));
		if (GameData.XPWithinLevel >= d["XP"].i)
		{
			GameData.XPWithinLevel -= d["XP"].i;
			GameData.playerLevel++;
			Scripts.medalsManager.UpdateMedal(5, 1);
			foreach (DicEntry dicEntry in d["Rewards"].l)
			{
				Missions.AddLevelReward(dicEntry.s);
			}
			Debug.Log(string.Concat(new object[]
			{
				"Leveled up: Level ",
				GameData.playerLevel,
				" and XP added into new level: ",
				GameData.XPWithinLevel
			}));
			return true;
		}
		return false;
	}

	// Token: 0x06000788 RID: 1928 RVA: 0x00039738 File Offset: 0x00037938
	private static bool pAddExperiencePoint()
	{
		return Level.pAddExperience(1);
	}

	// Token: 0x06000789 RID: 1929 RVA: 0x00039740 File Offset: 0x00037940
	private static int pGetReqXPInCurLevel()
	{
		if (Data.Shared["Levels"].d.ContainsKey("Level" + (GameData.playerLevel + 1)))
		{
			return Data.Shared["Levels"].d["Level" + (GameData.playerLevel + 1)].d["XP"].i;
		}
		return Data.Shared["Levels"].d["Level11"].d["XP"].i;
	}

	// Token: 0x0600078A RID: 1930 RVA: 0x000397F8 File Offset: 0x000379F8
	private static void pComputePlayerLevelAndXPBasedOnMissions()
	{
		int num = 0;
		for (int i = 1; i < GameData.mainMissionNum; i++)
		{
			num += Data.Shared["MainMissions"].d["Mission" + i].d["XP"].i;
		}
		int num2 = 1;
		for (int j = 2; j < Data.Shared["Levels"].d.Count; j++)
		{
			int i2 = Data.Shared["Levels"].d["Level" + j].d["XP"].i;
			if (num >= i2)
			{
				num -= i2;
				num2++;
				foreach (DicEntry dicEntry in Data.Shared["Levels"].d["Level" + j].d["Rewards"].l)
				{
					Reward reward = new Reward(dicEntry.s);
					if (reward.type == "Unlock")
					{
						reward.Apply();
					}
				}
			}
		}
		GameData.playerLevel = num2;
		GameData.XPWithinLevel = num;
	}
}
