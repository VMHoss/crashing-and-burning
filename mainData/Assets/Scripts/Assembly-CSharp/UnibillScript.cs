using System;
using UnityEngine;

// Token: 0x020000BE RID: 190
public class UnibillScript
{
	// Token: 0x060005D0 RID: 1488 RVA: 0x00029B80 File Offset: 0x00027D80
	private static void Log(string aLog)
	{
		UnibillScript.DebugLogString = UnibillScript.DebugLogString + aLog + "\r\n";
		UnibillError[] errors = Unibiller.Errors;
		foreach (UnibillError unibillError in errors)
		{
			string debugLogString = UnibillScript.DebugLogString;
			UnibillScript.DebugLogString = string.Concat(new object[]
			{
				debugLogString,
				"Unibill Error State: ",
				unibillError,
				"\r\n"
			});
		}
		Debug.Log(aLog);
	}

	// Token: 0x060005D1 RID: 1489 RVA: 0x00029C00 File Offset: 0x00027E00
	public static void Initialize()
	{
		if (Resources.Load("unibillInventory") == null)
		{
			UnibillScript.Log("You must define your purchasable inventory within the inventory editor!");
			Debug.LogError("You must define your purchasable inventory within the inventory editor!");
			return;
		}
		Unibiller.onBillerReady += UnibillScript.OnBillerReady;
		Unibiller.onTransactionsRestored += UnibillScript.OnTransactionsRestored;
		Unibiller.onPurchaseCancelled += UnibillScript.OnCancelled;
		Unibiller.onPurchaseFailed += UnibillScript.OnFailed;
		Unibiller.onPurchaseComplete += UnibillScript.OnPurchased;
		Unibiller.Initialise();
	}

	// Token: 0x060005D2 RID: 1490 RVA: 0x00029C94 File Offset: 0x00027E94
	public static void TryPurchase(string anItemId)
	{
		UnibillScript.Log("Trying to purchase: " + anItemId);
		Unibiller.initiatePurchase(Unibiller.GetPurchasableItemById(anItemId));
	}

	// Token: 0x060005D3 RID: 1491 RVA: 0x00029CB4 File Offset: 0x00027EB4
	private static void OnBillerReady(UnibillState state)
	{
		UnibillScript.Log("onBillerReady:" + state);
	}

	// Token: 0x060005D4 RID: 1492 RVA: 0x00029CCC File Offset: 0x00027ECC
	private static void OnTransactionsRestored(bool aSuccess)
	{
		if (aSuccess)
		{
			UnibillScript.Log("Transactions restored succesfully.");
		}
		else
		{
			UnibillScript.Log("Transactions restoring FAILED!");
		}
	}

	// Token: 0x060005D5 RID: 1493 RVA: 0x00029CF0 File Offset: 0x00027EF0
	private static void OnCancelled(PurchasableItem item)
	{
		UnibillScript.Log("Purchase cancelled: " + item.Id);
	}

	// Token: 0x060005D6 RID: 1494 RVA: 0x00029D08 File Offset: 0x00027F08
	private static void OnFailed(PurchasableItem item)
	{
		UnibillScript.Log("Purchase failed: " + item.Id);
	}

	// Token: 0x060005D7 RID: 1495 RVA: 0x00029D20 File Offset: 0x00027F20
	private static void OnPurchased(PurchasableItem item)
	{
		UnibillScript.Log("Purchase OK: " + item.Id);
		UnibillScript.Log(string.Format("{0} has now been purchased {1} times.", item.name, Unibiller.GetPurchaseCount(item)));
		string id = item.Id;
		switch (id)
		{
		case "com.xformgames.burninrubbercrashnburn.bosspack":
			Shop.UnlockVehicle("Drill");
			Shop.BuyVehicle("Drill");
			Shop.UnlockVehicle("KillRod");
			Shop.BuyVehicle("KillRod");
			Shop.UnlockVehicle("SchoolBus");
			Shop.BuyVehicle("SchoolBus");
			Scripts.interfaceScript.SelectNewBoughtCar("Drill");
			break;
		case "com.xformgames.burninrubbercrashnburn.militarypack":
			Shop.UnlockVehicle("Fennek");
			Shop.BuyVehicle("Fennek");
			Shop.UnlockVehicle("Tank");
			Shop.BuyVehicle("Tank");
			Shop.UnlockVehicle("Taurus");
			Shop.BuyVehicle("Taurus");
			Scripts.interfaceScript.SelectNewBoughtCar("Fennek");
			break;
		case "com.xformgames.burninrubbercrashnburn.sportspack":
			Shop.UnlockVehicle("AstonMartin");
			Shop.BuyVehicle("AstonMartin");
			Shop.UnlockVehicle("FordGT");
			Shop.BuyVehicle("FordGT");
			Shop.UnlockVehicle("Vice");
			Shop.BuyVehicle("Vice");
			Scripts.interfaceScript.SelectNewBoughtCar("AstonMartin");
			break;
		}
		GameData.boughtAnythingWithMoney = true;
		UserData.Save();
	}

	// Token: 0x0400064A RID: 1610
	public static string DebugLogString = string.Empty;
}
