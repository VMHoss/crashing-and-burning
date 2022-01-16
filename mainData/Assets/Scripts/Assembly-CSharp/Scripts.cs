using System;

// Token: 0x020000DD RID: 221
public class Scripts
{
	// Token: 0x060006B8 RID: 1720 RVA: 0x00030450 File Offset: 0x0002E650
	public static void Init()
	{
		Scripts.inputManager = new InputManager();
		Scripts.scoreManager = new ScoreManager();
	}

	// Token: 0x040006DE RID: 1758
	public static MenuScript menuScript;

	// Token: 0x040006DF RID: 1759
	public static TrackScript trackScript;

	// Token: 0x040006E0 RID: 1760
	public static InterfaceScript interfaceScript;

	// Token: 0x040006E1 RID: 1761
	public static InputManager inputManager;

	// Token: 0x040006E2 RID: 1762
	public static ScoreManager scoreManager;

	// Token: 0x040006E3 RID: 1763
	public static GlobalAudio audioManager;

	// Token: 0x040006E4 RID: 1764
	public static MedalsBaseManager medalsManager;

	// Token: 0x040006E5 RID: 1765
	public static PoolManager poolManager;

	// Token: 0x040006E6 RID: 1766
	public static GridManager gridManager;

	// Token: 0x040006E7 RID: 1767
	public static TrafficManager trafficManager;

	// Token: 0x040006E8 RID: 1768
	public static PickUpManager pickUpManager;
}
