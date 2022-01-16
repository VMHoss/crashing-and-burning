using System;

// Token: 0x020000BD RID: 189
public class FlurryScript
{
	// Token: 0x060005CC RID: 1484 RVA: 0x00029B4C File Offset: 0x00027D4C
	public static void LogEvent(string anEvent, string[] aParams)
	{
		FlurryAnalytics.Instance().LogEvent(anEvent, aParams, false);
	}

	// Token: 0x060005CD RID: 1485 RVA: 0x00029B5C File Offset: 0x00027D5C
	public static void LogEvent(string anEvent)
	{
		FlurryAnalytics.Instance().LogEvent(anEvent);
	}

	// Token: 0x0400063E RID: 1598
	public const string EVENT_MENUBUTTON = "menu button";

	// Token: 0x0400063F RID: 1599
	public const string EVENT_RESULT = "result button";

	// Token: 0x04000640 RID: 1600
	public const string EVENT_CARPACK = "carpack button";

	// Token: 0x04000641 RID: 1601
	public const string EVENT_SHOPCATEGORY = "shop category button";

	// Token: 0x04000642 RID: 1602
	public const string EVENT_BOUGHTVEHICLES = "bought vehicle";

	// Token: 0x04000643 RID: 1603
	public const string EVENT_BOUGHTSUPERPOWER = "bought superpower";

	// Token: 0x04000644 RID: 1604
	public const string EVENT_BOUGHTGADGETS = "bought gadget";

	// Token: 0x04000645 RID: 1605
	public const string EVENT_BOUGHTUPGRADES = "bought upgrade";

	// Token: 0x04000646 RID: 1606
	public const string EVENT_VEHICLEUSED = "vehicle used";

	// Token: 0x04000647 RID: 1607
	public const string EVENT_SUPERPOWERUSED = "superpower used";

	// Token: 0x04000648 RID: 1608
	public const string EVENT_GADGETUSED = "gadget used";

	// Token: 0x04000649 RID: 1609
	public const string EVENT_MISSIONCOMPLETED = "mission complete";
}
