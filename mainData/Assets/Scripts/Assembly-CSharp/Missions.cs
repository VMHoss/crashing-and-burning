using System;

// Token: 0x02000105 RID: 261
public class Missions
{
	// Token: 0x060007B5 RID: 1973 RVA: 0x0003A4CC File Offset: 0x000386CC
	public static void InitMissions()
	{
		Missions.pInitMissions();
	}

	// Token: 0x060007B6 RID: 1974 RVA: 0x0003A4D4 File Offset: 0x000386D4
	public static void UpdateMissions()
	{
		Missions.pUpdateMissions();
	}

	// Token: 0x060007B7 RID: 1975 RVA: 0x0003A4DC File Offset: 0x000386DC
	public static void HandleCompletedMissions()
	{
		Missions.pHandleCompletedMissions();
	}

	// Token: 0x060007B8 RID: 1976 RVA: 0x0003A4E4 File Offset: 0x000386E4
	public static void AddLevelReward(string aRewardItem)
	{
		Missions.pAddLevelReward(aRewardItem);
	}

	// Token: 0x060007B9 RID: 1977 RVA: 0x0003A4EC File Offset: 0x000386EC
	private static void pInitMissions()
	{
		Missions.pGetNewMission();
	}

	// Token: 0x060007BA RID: 1978 RVA: 0x0003A4F4 File Offset: 0x000386F4
	private static void pUpdateMissions()
	{
		GameData.completedMissions.Clear();
		if (GameData.mainMission.completed)
		{
			Missions.pGetNewMission();
		}
		GameData.mainMission.Reset();
	}

	// Token: 0x060007BB RID: 1979 RVA: 0x0003A52C File Offset: 0x0003872C
	private static void pGetNewMission()
	{
		string s = Data.Shared["MainMissions"].d["Mission" + GameData.mainMissionNum].d["Type"].s;
		string text = s;
		switch (text)
		{
		case "Wreck":
			GameData.mainMission = new WreckMission("Mission" + GameData.mainMissionNum);
			break;
		case "Collect":
			GameData.mainMission = new MiscMission("Mission" + GameData.mainMissionNum);
			break;
		case "Score":
			GameData.mainMission = new ScoreMission("Mission" + GameData.mainMissionNum);
			break;
		}
	}

	// Token: 0x060007BC RID: 1980 RVA: 0x0003A650 File Offset: 0x00038850
	private static void pHandleCompletedMissions()
	{
		if (GameData.mainMission.completed)
		{
			GameData.mainMissionNum++;
			Scripts.medalsManager.UpdateMedal(1, 1);
			if (GameData.mainMissionNum > Data.Shared["MainMissions"].d.Count)
			{
				GameData.mainMissionNum = Data.Shared["MainMissions"].d.Count;
			}
			GameData.completedMissions.Add(GameData.mainMission);
		}
		foreach (Mission mission in GameData.completedMissions)
		{
			foreach (string aRewardItem in mission.rewards)
			{
				Missions.pHandleReward(true, aRewardItem);
			}
		}
	}

	// Token: 0x060007BD RID: 1981 RVA: 0x0003A77C File Offset: 0x0003897C
	private static void pHandleReward(bool anIsMissionReward, string aRewardItem)
	{
		Reward reward = new Reward(aRewardItem);
		reward.Apply();
		if (anIsMissionReward)
		{
			GameData.missionRewards.Add(reward);
		}
		else
		{
			GameData.levelUpRewards.Add(reward);
		}
	}

	// Token: 0x060007BE RID: 1982 RVA: 0x0003A7B8 File Offset: 0x000389B8
	private static void pAddLevelReward(string aRewardItem)
	{
		Missions.pHandleReward(false, aRewardItem);
	}
}
