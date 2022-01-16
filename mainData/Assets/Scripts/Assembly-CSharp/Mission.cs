using System;
using System.Collections.Generic;

// Token: 0x02000103 RID: 259
public abstract class Mission
{
	// Token: 0x060007AC RID: 1964 RVA: 0x0003A3CC File Offset: 0x000385CC
	protected Mission(Mission.Type aType, string aMissionName)
	{
		this.type = aType;
		this.missionName = aMissionName;
		this.pMissionProps = Data.Shared["MainMissions"].d[aMissionName].d;
		this.xp = this.pMissionProps["XP"].i;
		this.rewards = new List<string>();
		foreach (DicEntry dicEntry in this.pMissionProps["Rewards"].l)
		{
			this.rewards.Add(dicEntry.s);
		}
	}

	// Token: 0x060007AD RID: 1965
	public abstract string GetProgress();

	// Token: 0x060007AE RID: 1966
	public abstract string GetTarget();

	// Token: 0x060007AF RID: 1967
	public abstract void Reset();

	// Token: 0x060007B0 RID: 1968 RVA: 0x0003A4B4 File Offset: 0x000386B4
	public virtual void UpdateScore(int aScore)
	{
	}

	// Token: 0x060007B1 RID: 1969 RVA: 0x0003A4B8 File Offset: 0x000386B8
	public virtual void UpdateCollect(string anObject, MiscMission.CollectType aCollectType)
	{
	}

	// Token: 0x060007B2 RID: 1970 RVA: 0x0003A4BC File Offset: 0x000386BC
	public virtual void UpdateWreck(string anObject, WreckMission.WreckType anObjectType)
	{
	}

	// Token: 0x060007B3 RID: 1971 RVA: 0x0003A4C0 File Offset: 0x000386C0
	public virtual bool IsMissionObject(string anObject, WreckMission.WreckType anObjectType)
	{
		return false;
	}

	// Token: 0x0400082E RID: 2094
	public Mission.Type type;

	// Token: 0x0400082F RID: 2095
	public string missionName;

	// Token: 0x04000830 RID: 2096
	public int xp;

	// Token: 0x04000831 RID: 2097
	public List<string> rewards;

	// Token: 0x04000832 RID: 2098
	public bool completed;

	// Token: 0x04000833 RID: 2099
	public bool active = true;

	// Token: 0x04000834 RID: 2100
	protected Dictionary<string, DicEntry> pMissionProps;

	// Token: 0x02000104 RID: 260
	public enum Type
	{
		// Token: 0x04000836 RID: 2102
		SCORE,
		// Token: 0x04000837 RID: 2103
		WRECK,
		// Token: 0x04000838 RID: 2104
		MISC
	}
}
