using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200011B RID: 283
public class SafeHousePosition
{
	// Token: 0x0600080E RID: 2062 RVA: 0x0003CD3C File Offset: 0x0003AF3C
	public static void Initialize(Dictionary<string, DicEntry> aLevelText)
	{
		if (SafeHousePosition.pInitialized)
		{
			return;
		}
		SafeHousePosition.pInitialized = true;
		SafeHousePosition.pSafeHouses = new List<SafeHouseData>(20);
		for (int i = 0; i < 20; i++)
		{
			SafeHousePosition.pSafeHouses.Add(null);
		}
		foreach (KeyValuePair<string, DicEntry> keyValuePair in aLevelText["StartPositions"].d)
		{
			Vector3 aPosition = -GenericFunctionsScript.VectorFromList(keyValuePair.Value.d["Pos"].l);
			aPosition.y = -aPosition.y;
			float f = keyValuePair.Value.d["Angle"].f;
			int num = int.Parse(keyValuePair.Key.Substring(13));
			SafeHousePosition.pSafeHouses[num] = new SafeHouseData(aPosition, f, num);
		}
	}

	// Token: 0x0600080F RID: 2063 RVA: 0x0003CE60 File Offset: 0x0003B060
	public static SafeHouseData GetPositionAt(int anIndex)
	{
		return SafeHousePosition.pSafeHouses[anIndex];
	}

	// Token: 0x06000810 RID: 2064 RVA: 0x0003CE70 File Offset: 0x0003B070
	public static SafeHouseData GetCurrentSafeHousePosition()
	{
		return SafeHousePosition.pSafeHouses[GameData.currentSafeHouse];
	}

	// Token: 0x0400088B RID: 2187
	private static bool pInitialized;

	// Token: 0x0400088C RID: 2188
	private static List<SafeHouseData> pSafeHouses;
}
