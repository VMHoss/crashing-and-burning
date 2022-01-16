using System;
using System.Collections.Generic;

// Token: 0x02000108 RID: 264
public class WreckMission : Mission
{
	// Token: 0x060007C7 RID: 1991 RVA: 0x0003AB70 File Offset: 0x00038D70
	public WreckMission(string aWreckMission) : base(Mission.Type.WRECK, aWreckMission)
	{
		this.wreckObjects = new List<string>();
		foreach (DicEntry dicEntry in this.pMissionProps["WreckObject"].l)
		{
			this.wreckObjects.Add(dicEntry.s);
		}
		string text = this.wreckObjects[0];
		switch (text)
		{
		case "All":
			this.pWreckType = WreckMission.WreckType.ALL;
			goto IL_111;
		case "Vehicles":
			this.pWreckType = WreckMission.WreckType.VEHICLES;
			goto IL_111;
		case "Destructibles":
			this.pWreckType = WreckMission.WreckType.DESTRUCTIBLES;
			goto IL_111;
		}
		this.pWreckType = WreckMission.WreckType.CUSTOM;
		IL_111:
		if (Data.platform == "PC")
		{
			this.goalAmount = this.pMissionProps["Amount"].i;
		}
		else
		{
			this.goalAmount = this.pMissionProps["MobileAmount"].i;
		}
	}

	// Token: 0x060007C8 RID: 1992 RVA: 0x0003ACFC File Offset: 0x00038EFC
	public override string GetProgress()
	{
		return this.currentAmount + "/" + this.goalAmount;
	}

	// Token: 0x060007C9 RID: 1993 RVA: 0x0003AD2C File Offset: 0x00038F2C
	public override string GetTarget()
	{
		return this.wreckObjects[0];
	}

	// Token: 0x060007CA RID: 1994 RVA: 0x0003AD3C File Offset: 0x00038F3C
	public override void Reset()
	{
		this.currentAmount = 0;
	}

	// Token: 0x060007CB RID: 1995 RVA: 0x0003AD48 File Offset: 0x00038F48
	public override void UpdateWreck(string anObject, WreckMission.WreckType anObjectType)
	{
		if (!this.active)
		{
			return;
		}
		if (!this.completed)
		{
			switch (this.pWreckType)
			{
			case WreckMission.WreckType.ALL:
				this.currentAmount++;
				break;
			case WreckMission.WreckType.VEHICLES:
				if (anObjectType == WreckMission.WreckType.VEHICLES)
				{
					this.currentAmount++;
				}
				break;
			case WreckMission.WreckType.DESTRUCTIBLES:
				if (anObjectType == WreckMission.WreckType.DESTRUCTIBLES)
				{
					this.currentAmount++;
				}
				break;
			case WreckMission.WreckType.CUSTOM:
				if (this.wreckObjects.Contains(anObject))
				{
					this.currentAmount++;
				}
				break;
			}
			if (this.currentAmount >= this.goalAmount)
			{
				Scripts.trackScript.interfaceScript.interfacePanelScript.MissionCleared(this);
				this.completed = true;
			}
		}
	}

	// Token: 0x060007CC RID: 1996 RVA: 0x0003AE28 File Offset: 0x00039028
	public override bool IsMissionObject(string anObject, WreckMission.WreckType anObjectType)
	{
		if (!this.active)
		{
			return false;
		}
		switch (this.pWreckType)
		{
		case WreckMission.WreckType.ALL:
			return true;
		case WreckMission.WreckType.VEHICLES:
			return anObjectType == WreckMission.WreckType.VEHICLES;
		case WreckMission.WreckType.DESTRUCTIBLES:
			return anObjectType == WreckMission.WreckType.DESTRUCTIBLES;
		case WreckMission.WreckType.CUSTOM:
			return this.wreckObjects.Contains(anObject);
		default:
			return false;
		}
	}

	// Token: 0x04000842 RID: 2114
	public List<string> wreckObjects;

	// Token: 0x04000843 RID: 2115
	public int goalAmount;

	// Token: 0x04000844 RID: 2116
	public int currentAmount;

	// Token: 0x04000845 RID: 2117
	private WreckMission.WreckType pWreckType;

	// Token: 0x02000109 RID: 265
	public enum WreckType
	{
		// Token: 0x04000848 RID: 2120
		ALL,
		// Token: 0x04000849 RID: 2121
		VEHICLES,
		// Token: 0x0400084A RID: 2122
		DESTRUCTIBLES,
		// Token: 0x0400084B RID: 2123
		CUSTOM
	}
}
