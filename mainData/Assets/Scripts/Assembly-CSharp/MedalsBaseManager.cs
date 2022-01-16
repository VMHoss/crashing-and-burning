using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000D7 RID: 215
public abstract class MedalsBaseManager
{
	// Token: 0x06000691 RID: 1681 RVA: 0x0002F04C File Offset: 0x0002D24C
	public MedalsBaseManager()
	{
	}

	// Token: 0x06000692 RID: 1682 RVA: 0x0002F054 File Offset: 0x0002D254
	public bool IsMedalObtained(int aMedal)
	{
		bool? flag = this.IsMedalObtainedSpecific(aMedal);
		if (flag != null)
		{
			return flag.Value;
		}
		return GameData.medalProgression["Medal" + aMedal].i == Data.Shared["Medals"].d["Medal" + aMedal].d["Data"].i;
	}

	// Token: 0x06000693 RID: 1683 RVA: 0x0002F0DC File Offset: 0x0002D2DC
	public string GetMedalProgression(int aMedal)
	{
		string medalProgressionSpecific = this.GetMedalProgressionSpecific(aMedal);
		if (medalProgressionSpecific != null)
		{
			return medalProgressionSpecific;
		}
		return GameData.medalProgression["Medal" + aMedal].i.ToString() + "/" + Data.Shared["Medals"].d["Medal" + aMedal].d["Data"].i.ToString();
	}

	// Token: 0x06000694 RID: 1684 RVA: 0x0002F170 File Offset: 0x0002D370
	public float GetMedalProgressionFloat(int aMedal)
	{
		float? medalProgressionFloatSpecific = this.GetMedalProgressionFloatSpecific(aMedal);
		if (medalProgressionFloatSpecific != null)
		{
			return medalProgressionFloatSpecific.Value;
		}
		return (float)GameData.medalProgression["Medal" + aMedal].i / (float)Data.Shared["Medals"].d["Medal" + aMedal].d["Data"].i;
	}

	// Token: 0x06000695 RID: 1685 RVA: 0x0002F1F8 File Offset: 0x0002D3F8
	public void CheatNearlyGetAllMedals()
	{
		foreach (KeyValuePair<string, DicEntry> keyValuePair in GameData.medalProgression)
		{
			if (keyValuePair.Value.type == DicEntry.EntryType.INT)
			{
				keyValuePair.Value.i = Data.Shared["Medals"].d[keyValuePair.Key].d["Data"].i - 1;
			}
		}
		this.CheatNearlyGetAllMedalsSpecific();
	}

	// Token: 0x06000696 RID: 1686 RVA: 0x0002F2B0 File Offset: 0x0002D4B0
	public void UpdateMedal(int aMedal, int aValue)
	{
		this.UpdateMedal(aMedal, aValue, null);
	}

	// Token: 0x06000697 RID: 1687 RVA: 0x0002F2BC File Offset: 0x0002D4BC
	public void UpdateMedal(int aMedal, string aString)
	{
		this.UpdateMedal(aMedal, -999, aString);
	}

	// Token: 0x06000698 RID: 1688 RVA: 0x0002F2CC File Offset: 0x0002D4CC
	private void UpdateMedal(int aMedal, int aValue, string aString)
	{
		if (this.IsMedalObtained(aMedal))
		{
			return;
		}
		if (this.UpdateMedalSpecific(aMedal, aValue, aString) == null)
		{
			if (aString != null)
			{
				throw new UnityException("UpdateMedal called with string value must be handled by child!");
			}
			GameData.medalProgression["Medal" + aMedal].i += aValue;
			if (GameData.medalProgression["Medal" + aMedal].i > Data.Shared["Medals"].d["Medal" + aMedal].d["Data"].i)
			{
				GameData.medalProgression["Medal" + aMedal].i = Data.Shared["Medals"].d["Medal" + aMedal].d["Data"].i;
			}
		}
		if (this.IsMedalObtained(aMedal))
		{
			this.MedalObtained(aMedal);
			GenericFunctionsScript.Medal(aMedal);
		}
	}

	// Token: 0x06000699 RID: 1689
	protected abstract bool? IsMedalObtainedSpecific(int aMedal);

	// Token: 0x0600069A RID: 1690
	protected abstract string GetMedalProgressionSpecific(int aMedal);

	// Token: 0x0600069B RID: 1691
	protected abstract float? GetMedalProgressionFloatSpecific(int aMedal);

	// Token: 0x0600069C RID: 1692
	protected abstract void CheatNearlyGetAllMedalsSpecific();

	// Token: 0x0600069D RID: 1693
	protected abstract bool? UpdateMedalSpecific(int aMedal, int aValue, string aString);

	// Token: 0x0600069E RID: 1694 RVA: 0x0002F410 File Offset: 0x0002D610
	protected virtual void MedalObtained(int aMedal)
	{
	}
}
