using System;
using System.Collections.Generic;

// Token: 0x02000101 RID: 257
public class MiscMission : Mission
{
	// Token: 0x060007A7 RID: 1959 RVA: 0x0003A170 File Offset: 0x00038370
	public MiscMission(string aMiscMission) : base(Mission.Type.MISC, aMiscMission)
	{
		this.collectObjects = new List<string>();
		foreach (DicEntry dicEntry in this.pMissionProps["CollectObject"].l)
		{
			this.collectObjects.Add(dicEntry.s);
		}
		string text = this.collectObjects[0];
		if (text != null)
		{
			if (MiscMission.<>f__switch$map1A == null)
			{
				MiscMission.<>f__switch$map1A = new Dictionary<string, int>(2)
				{
					{
						"All",
						0
					},
					{
						"Trigger",
						1
					}
				};
			}
			int num;
			if (MiscMission.<>f__switch$map1A.TryGetValue(text, out num))
			{
				if (num == 0)
				{
					this.pCollectType = MiscMission.CollectType.ALL;
					goto IL_F5;
				}
				if (num == 1)
				{
					this.pCollectType = MiscMission.CollectType.TRIGGER;
					goto IL_F5;
				}
			}
		}
		this.pCollectType = MiscMission.CollectType.CUSTOM;
		IL_F5:
		this.goalAmount = this.pMissionProps["Amount"].i;
	}

	// Token: 0x060007A8 RID: 1960 RVA: 0x0003A2AC File Offset: 0x000384AC
	public override string GetProgress()
	{
		return this.currentAmount + "/" + this.goalAmount;
	}

	// Token: 0x060007A9 RID: 1961 RVA: 0x0003A2DC File Offset: 0x000384DC
	public override string GetTarget()
	{
		return this.collectObjects[0];
	}

	// Token: 0x060007AA RID: 1962 RVA: 0x0003A2EC File Offset: 0x000384EC
	public override void Reset()
	{
		this.currentAmount = 0;
	}

	// Token: 0x060007AB RID: 1963 RVA: 0x0003A2F8 File Offset: 0x000384F8
	public void Update(string anObject, MiscMission.CollectType aCollectType)
	{
		if (!this.active)
		{
			return;
		}
		if (!this.completed)
		{
			switch (this.pCollectType)
			{
			case MiscMission.CollectType.ALL:
				this.currentAmount++;
				break;
			case MiscMission.CollectType.TRIGGER:
				if (aCollectType == MiscMission.CollectType.TRIGGER)
				{
					this.currentAmount++;
				}
				break;
			case MiscMission.CollectType.CUSTOM:
				if (aCollectType == MiscMission.CollectType.TRIGGER)
				{
					anObject = anObject.Substring(17);
				}
				if (this.collectObjects.Contains(anObject))
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

	// Token: 0x04000825 RID: 2085
	public List<string> collectObjects;

	// Token: 0x04000826 RID: 2086
	public int goalAmount;

	// Token: 0x04000827 RID: 2087
	public int currentAmount;

	// Token: 0x04000828 RID: 2088
	private MiscMission.CollectType pCollectType;

	// Token: 0x02000102 RID: 258
	public enum CollectType
	{
		// Token: 0x0400082B RID: 2091
		ALL,
		// Token: 0x0400082C RID: 2092
		TRIGGER,
		// Token: 0x0400082D RID: 2093
		CUSTOM
	}
}
