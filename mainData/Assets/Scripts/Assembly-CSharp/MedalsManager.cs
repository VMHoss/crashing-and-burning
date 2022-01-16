using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000FD RID: 253
public class MedalsManager : MedalsBaseManager
{
	// Token: 0x06000791 RID: 1937 RVA: 0x00039B54 File Offset: 0x00037D54
	protected override bool? IsMedalObtainedSpecific(int aMedal)
	{
		if (aMedal != 6)
		{
			return null;
		}
		return new bool?(GameData.medalProgression["Medal" + aMedal].i >= Data.Shared["Medals"].d["Medal" + aMedal].d["Data"].i);
	}

	// Token: 0x06000792 RID: 1938 RVA: 0x00039BE0 File Offset: 0x00037DE0
	protected override string GetMedalProgressionSpecific(int aMedal)
	{
		return null;
	}

	// Token: 0x06000793 RID: 1939 RVA: 0x00039BE4 File Offset: 0x00037DE4
	protected override float? GetMedalProgressionFloatSpecific(int aMedal)
	{
		return null;
	}

	// Token: 0x06000794 RID: 1940 RVA: 0x00039BFC File Offset: 0x00037DFC
	protected override void CheatNearlyGetAllMedalsSpecific()
	{
	}

	// Token: 0x06000795 RID: 1941 RVA: 0x00039C00 File Offset: 0x00037E00
	protected override bool? UpdateMedalSpecific(int aMedal, int aValue, string aString)
	{
		switch (aMedal)
		{
		case 3:
			GameData.medalProgression["Medal" + aMedal].i = this.GetAllBoughtItemsCount();
			goto IL_13E;
		case 4:
		{
			DicEntry dicEntry = GameData.medalProgression["Medal" + aMedal];
			dicEntry.i = Mathf.Max(dicEntry.i, aValue);
			if (dicEntry.i > Data.Shared["Medals"].d["Medal" + aMedal].d["Data"].i)
			{
				dicEntry.i = Data.Shared["Medals"].d["Medal" + aMedal].d["Data"].i;
			}
			goto IL_13E;
		}
		case 6:
			GameData.medalProgression["Medal" + aMedal].i = GameData.boughtVehicles.Count;
			goto IL_13E;
		}
		return null;
		IL_13E:
		return new bool?(true);
	}

	// Token: 0x06000796 RID: 1942 RVA: 0x00039D54 File Offset: 0x00037F54
	private int GetAllBoughtItemsCount()
	{
		int num = 0;
		num += GameData.boughtVehicles.Count;
		num += GameData.boughtSuperPowers.Count;
		num += GameData.boughtGadgets.Count;
		foreach (KeyValuePair<string, int> keyValuePair in GameData.upgradedLevels)
		{
			num += keyValuePair.Value;
		}
		return num;
	}

	// Token: 0x06000797 RID: 1943 RVA: 0x00039DE8 File Offset: 0x00037FE8
	protected override void MedalObtained(int aMedal)
	{
		if (Data.branding == "ClickJogos" && Data.Shared["Medals"].d["Medal" + aMedal].d.ContainsKey("ClickJogos") && Data.Shared["Medals"].d["Medal" + aMedal].d["ClickJogos"].b)
		{
			ClickJogos.MedalComplete("Medal" + aMedal);
		}
	}

	// Token: 0x020000FE RID: 254
	public enum Medal
	{
		// Token: 0x04000817 RID: 2071
		MISSIONMASTER = 1,
		// Token: 0x04000818 RID: 2072
		THEDESTROYER,
		// Token: 0x04000819 RID: 2073
		SHOPAHOLIC,
		// Token: 0x0400081A RID: 2074
		MONEYMAN,
		// Token: 0x0400081B RID: 2075
		GRINDER,
		// Token: 0x0400081C RID: 2076
		CARFANATIC,
		// Token: 0x0400081D RID: 2077
		ATOMICKID,
		// Token: 0x0400081E RID: 2078
		DELIVERYGUY,
		// Token: 0x0400081F RID: 2079
		QUICKBUCK,
		// Token: 0x04000820 RID: 2080
		TROPHYHUNTER
	}
}
