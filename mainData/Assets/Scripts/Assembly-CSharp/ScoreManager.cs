using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200011E RID: 286
public class ScoreManager
{
	// Token: 0x06000819 RID: 2073 RVA: 0x0003D2C4 File Offset: 0x0003B4C4
	public ScoreManager()
	{
		this.pBonusSet = new HashSet<ScoreManager.Bonus>();
		this.Reset();
	}

	// Token: 0x0600081A RID: 2074 RVA: 0x0003D2E0 File Offset: 0x0003B4E0
	public void Reset()
	{
		this.pBonusSet.Clear();
		GameData.trafficScore = 0;
		GameData.destroyedTrafficList.Clear();
		GameData.destructiblesScore = 0;
		GameData.destroyedDestructiblesList.Clear();
		GameData.buildingsScore = 0;
		GameData.destroyedBuildingsList.Clear();
		GameData.obtainedCashList.Clear();
		GameData.cashScore = 0;
		GameData.extraScore = 0;
		GameData.obtainedExtrasList.Clear();
		GameData.scoreMultiplier = 1;
		GameData.totalScore = 0;
	}

	// Token: 0x0600081B RID: 2075 RVA: 0x0003D354 File Offset: 0x0003B554
	public void DestroyedTraffic(string aTrafficName)
	{
		if (!Data.raceInProgress)
		{
			return;
		}
		Scripts.medalsManager.UpdateMedal(2, 1);
		int i = Data.Shared["Traffic"].d[aTrafficName].d["Cash"].i;
		Scripts.interfaceScript.interfacePanelScript.PickUp(aTrafficName, i);
		this.AddCash(i);
		GameData.currentChainReaction++;
		GameData.chainReactionTimeLeft = Data.Shared["RampageSettings"].d["ComboTimeWindow"].f;
		GameData.mainMission.UpdateWreck(aTrafficName, WreckMission.WreckType.VEHICLES);
	}

	// Token: 0x0600081C RID: 2076 RVA: 0x0003D400 File Offset: 0x0003B600
	public void DestroyedDestructible(string aDestructibleName)
	{
		if (!Data.raceInProgress)
		{
			return;
		}
		int i = Data.Shared["Destructible"].d[aDestructibleName].d["Cash"].i;
		Scripts.interfaceScript.interfacePanelScript.PickUp(aDestructibleName, i);
		this.AddCash(i);
		GameData.mainMission.UpdateWreck(aDestructibleName, WreckMission.WreckType.DESTRUCTIBLES);
	}

	// Token: 0x0600081D RID: 2077 RVA: 0x0003D46C File Offset: 0x0003B66C
	public void ObtainedCash()
	{
		this.AddCash(Data.Shared["PickUps"].d["Cash"].d["Cash"].i);
	}

	// Token: 0x0600081E RID: 2078 RVA: 0x0003D4B4 File Offset: 0x0003B6B4
	public void ObtainedCashBig()
	{
		this.AddCash(Data.Shared["PickUps"].d["CashBig"].d["Cash"].i);
	}

	// Token: 0x0600081F RID: 2079 RVA: 0x0003D4FC File Offset: 0x0003B6FC
	public void ObtainedCashStash()
	{
		this.AddCash(Data.Shared["PickUps"].d["CashStash"].d["Cash"].i);
	}

	// Token: 0x06000820 RID: 2080 RVA: 0x0003D544 File Offset: 0x0003B744
	public void AddBonus(ScoreManager.Bonus aBonus)
	{
		this.pBonusSet.Add(aBonus);
		switch (aBonus)
		{
		case ScoreManager.Bonus.SPEED:
		{
			int i = Data.Shared["Misc"].d["BonusSpeed"].i;
			GameData.obtainedExtrasList.Add(new ScoreEntry("BonusSpeed", i));
			this.AddCash(i);
			break;
		}
		case ScoreManager.Bonus.JUMPHEIGHT:
		{
			int i = Data.Shared["Misc"].d["BonusJumpheight"].i;
			GameData.obtainedExtrasList.Add(new ScoreEntry("BonusJumpHeight", i));
			this.AddCash(i);
			break;
		}
		case ScoreManager.Bonus.JUMPDISTANCE:
		{
			int i = Data.Shared["Misc"].d["BonusJumpdistance"].i;
			GameData.obtainedExtrasList.Add(new ScoreEntry("BonusJumpDistance", i));
			this.AddCash(i);
			break;
		}
		default:
			throw new UnityException("Unknown bonus! in ScoreManager::AddBonus");
		}
	}

	// Token: 0x06000821 RID: 2081 RVA: 0x0003D654 File Offset: 0x0003B854
	public void AddChainComboBonus(string aChainSizeBonus)
	{
		if (Scripts.trackScript.IsRaceFinished())
		{
			return;
		}
		int i = Data.Shared["Misc"].d[aChainSizeBonus].d["Score"].i;
		GameData.obtainedExtrasList.Add(new ScoreEntry(aChainSizeBonus, i));
		GameData.extraScore += i;
		GameData.totalScore += i;
	}

	// Token: 0x06000822 RID: 2082 RVA: 0x0003D6CC File Offset: 0x0003B8CC
	public bool HasBonus(ScoreManager.Bonus aBonus)
	{
		return this.pBonusSet.Contains(aBonus);
	}

	// Token: 0x06000823 RID: 2083 RVA: 0x0003D6DC File Offset: 0x0003B8DC
	public void AddCash(int aCash)
	{
		GameData.cash += aCash;
		Scripts.medalsManager.UpdateMedal(4, GameData.cash);
	}

	// Token: 0x0400089C RID: 2204
	private HashSet<ScoreManager.Bonus> pBonusSet;

	// Token: 0x0200011F RID: 287
	public enum Bonus
	{
		// Token: 0x0400089E RID: 2206
		SPEED = 1,
		// Token: 0x0400089F RID: 2207
		JUMPHEIGHT,
		// Token: 0x040008A0 RID: 2208
		JUMPDISTANCE
	}
}
