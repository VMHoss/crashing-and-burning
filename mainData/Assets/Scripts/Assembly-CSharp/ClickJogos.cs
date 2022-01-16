using System;
using UnityEngine;

// Token: 0x020000BC RID: 188
public class ClickJogos
{
	// Token: 0x060005C9 RID: 1481 RVA: 0x00029AF8 File Offset: 0x00027CF8
	public static void Initialize()
	{
		GameObject gameObject = new GameObject("ClickJogos");
		ClickJogos.pCJAPI = gameObject.AddComponent<CJAPI>();
		ClickJogos.pCJAPI.initialize("c071beb0cd690b13eb7dd6e4601689a2", "48f36209e1609a5c8e9279510895fff7");
	}

	// Token: 0x060005CA RID: 1482 RVA: 0x00029B30 File Offset: 0x00027D30
	public static void MedalComplete(string aMedalName)
	{
		ClickJogos.pCJAPI.stats.submit(aMedalName, 1);
	}

	// Token: 0x0400063B RID: 1595
	private const string DEVELOPER_KEY = "c071beb0cd690b13eb7dd6e4601689a2";

	// Token: 0x0400063C RID: 1596
	private const string GAME_KEY = "48f36209e1609a5c8e9279510895fff7";

	// Token: 0x0400063D RID: 1597
	private static CJAPI pCJAPI;
}
