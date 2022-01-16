using System;

// Token: 0x02000107 RID: 263
public class ScoreMission : Mission
{
	// Token: 0x060007C2 RID: 1986 RVA: 0x0003AAA4 File Offset: 0x00038CA4
	public ScoreMission(string aScoreMission) : base(Mission.Type.SCORE, aScoreMission)
	{
		this.goalScore = this.pMissionProps["Score"].i;
	}

	// Token: 0x060007C3 RID: 1987 RVA: 0x0003AACC File Offset: 0x00038CCC
	public override string GetProgress()
	{
		return this.currentScore + "/" + this.goalScore;
	}

	// Token: 0x060007C4 RID: 1988 RVA: 0x0003AAFC File Offset: 0x00038CFC
	public override string GetTarget()
	{
		return this.goalScore.ToString();
	}

	// Token: 0x060007C5 RID: 1989 RVA: 0x0003AB0C File Offset: 0x00038D0C
	public override void Reset()
	{
		this.currentScore = 0;
	}

	// Token: 0x060007C6 RID: 1990 RVA: 0x0003AB18 File Offset: 0x00038D18
	public override void UpdateScore(int aScore)
	{
		if (!this.active)
		{
			return;
		}
		if (!this.completed)
		{
			this.currentScore = aScore;
			if (this.currentScore >= this.goalScore)
			{
				Scripts.trackScript.interfaceScript.interfacePanelScript.MissionCleared(this);
				this.completed = true;
			}
		}
	}

	// Token: 0x04000840 RID: 2112
	public int goalScore;

	// Token: 0x04000841 RID: 2113
	public int currentScore;
}
