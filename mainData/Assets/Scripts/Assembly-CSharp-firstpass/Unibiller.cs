using System;
using Unibill;
using Unibill.Impl;

// Token: 0x02000049 RID: 73
public class Unibiller
{
	// Token: 0x1400001D RID: 29
	// (add) Token: 0x0600027A RID: 634 RVA: 0x00009C28 File Offset: 0x00007E28
	// (remove) Token: 0x0600027B RID: 635 RVA: 0x00009C40 File Offset: 0x00007E40
	public static event Action<UnibillState> onBillerReady;

	// Token: 0x1400001E RID: 30
	// (add) Token: 0x0600027C RID: 636 RVA: 0x00009C58 File Offset: 0x00007E58
	// (remove) Token: 0x0600027D RID: 637 RVA: 0x00009C70 File Offset: 0x00007E70
	public static event Action<PurchasableItem> onPurchaseCancelled;

	// Token: 0x1400001F RID: 31
	// (add) Token: 0x0600027E RID: 638 RVA: 0x00009C88 File Offset: 0x00007E88
	// (remove) Token: 0x0600027F RID: 639 RVA: 0x00009CA0 File Offset: 0x00007EA0
	public static event Action<PurchasableItem> onPurchaseComplete;

	// Token: 0x14000020 RID: 32
	// (add) Token: 0x06000280 RID: 640 RVA: 0x00009CB8 File Offset: 0x00007EB8
	// (remove) Token: 0x06000281 RID: 641 RVA: 0x00009CD0 File Offset: 0x00007ED0
	public static event Action<PurchasableItem> onPurchaseFailed;

	// Token: 0x14000021 RID: 33
	// (add) Token: 0x06000282 RID: 642 RVA: 0x00009CE8 File Offset: 0x00007EE8
	// (remove) Token: 0x06000283 RID: 643 RVA: 0x00009D00 File Offset: 0x00007F00
	public static event Action<PurchasableItem> onPurchaseRefunded;

	// Token: 0x14000022 RID: 34
	// (add) Token: 0x06000284 RID: 644 RVA: 0x00009D18 File Offset: 0x00007F18
	// (remove) Token: 0x06000285 RID: 645 RVA: 0x00009D30 File Offset: 0x00007F30
	public static event Action<bool> onTransactionsRestored;

	// Token: 0x06000286 RID: 646 RVA: 0x00009D48 File Offset: 0x00007F48
	public static void Initialise()
	{
		if (Unibiller.biller == null)
		{
			Unibiller._internal_doInitialise(Biller.instantiate());
		}
	}

	// Token: 0x17000033 RID: 51
	// (get) Token: 0x06000287 RID: 647 RVA: 0x00009D60 File Offset: 0x00007F60
	public static UnibillError[] Errors
	{
		get
		{
			if (Unibiller.biller != null)
			{
				return Unibiller.biller.Errors.ToArray();
			}
			return new UnibillError[0];
		}
	}

	// Token: 0x17000034 RID: 52
	// (get) Token: 0x06000288 RID: 648 RVA: 0x00009D90 File Offset: 0x00007F90
	public static PurchasableItem[] AllPurchasableItems
	{
		get
		{
			return Unibiller.biller.InventoryDatabase.AllPurchasableItems.ToArray();
		}
	}

	// Token: 0x17000035 RID: 53
	// (get) Token: 0x06000289 RID: 649 RVA: 0x00009DA8 File Offset: 0x00007FA8
	public static PurchasableItem[] AllNonConsumablePurchasableItems
	{
		get
		{
			return Unibiller.biller.InventoryDatabase.AllNonConsumablePurchasableItems.ToArray();
		}
	}

	// Token: 0x17000036 RID: 54
	// (get) Token: 0x0600028A RID: 650 RVA: 0x00009DC0 File Offset: 0x00007FC0
	public static PurchasableItem[] AllConsumablePurchasableItems
	{
		get
		{
			return Unibiller.biller.InventoryDatabase.AllConsumablePurchasableItems.ToArray();
		}
	}

	// Token: 0x17000037 RID: 55
	// (get) Token: 0x0600028B RID: 651 RVA: 0x00009DD8 File Offset: 0x00007FD8
	public static PurchasableItem[] AllSubscriptions
	{
		get
		{
			return Unibiller.biller.InventoryDatabase.AllSubscriptions.ToArray();
		}
	}

	// Token: 0x0600028C RID: 652 RVA: 0x00009DF0 File Offset: 0x00007FF0
	public static PurchasableItem GetPurchasableItemById(string unibillPurchasableId)
	{
		if (Unibiller.biller != null)
		{
			return Unibiller.biller.InventoryDatabase.getItemById(unibillPurchasableId);
		}
		return null;
	}

	// Token: 0x0600028D RID: 653 RVA: 0x00009E10 File Offset: 0x00008010
	public static string[] GetAllPurchaseReceipts(PurchasableItem forItem)
	{
		if (Unibiller.biller != null)
		{
			return Unibiller.biller.getReceiptsForPurchasable(forItem);
		}
		return new string[0];
	}

	// Token: 0x0600028E RID: 654 RVA: 0x00009E30 File Offset: 0x00008030
	public static void initiatePurchase(PurchasableItem purchasable)
	{
		if (Unibiller.biller != null)
		{
			Unibiller.biller.purchase(purchasable);
		}
	}

	// Token: 0x0600028F RID: 655 RVA: 0x00009E48 File Offset: 0x00008048
	public static void initiatePurchase(string purchasableId)
	{
		if (Unibiller.biller != null)
		{
			Unibiller.biller.purchase(purchasableId);
		}
	}

	// Token: 0x06000290 RID: 656 RVA: 0x00009E60 File Offset: 0x00008060
	public static int GetPurchaseCount(PurchasableItem item)
	{
		if (Unibiller.biller != null)
		{
			return Unibiller.biller.getPurchaseHistory(item);
		}
		return 0;
	}

	// Token: 0x06000291 RID: 657 RVA: 0x00009E7C File Offset: 0x0000807C
	public static int GetPurchaseCount(string purchasableId)
	{
		if (Unibiller.biller != null)
		{
			return Unibiller.biller.getPurchaseHistory(purchasableId);
		}
		return 0;
	}

	// Token: 0x06000292 RID: 658 RVA: 0x00009E98 File Offset: 0x00008098
	public static void restoreTransactions()
	{
		if (Unibiller.biller != null)
		{
			Unibiller.biller.restoreTransactions();
		}
	}

	// Token: 0x06000293 RID: 659 RVA: 0x00009EB0 File Offset: 0x000080B0
	public static void clearTransactions()
	{
		if (Unibiller.biller != null)
		{
			Unibiller.biller.ClearPurchases();
		}
	}

	// Token: 0x06000294 RID: 660 RVA: 0x00009EC8 File Offset: 0x000080C8
	public static void _internal_doInitialise(Biller biller)
	{
		Unibiller.biller = biller;
		biller.onBillerReady += delegate(bool success)
		{
			if (Unibiller.onBillerReady != null)
			{
				if (success)
				{
					Unibiller.onBillerReady((biller.State != BillerState.INITIALISED) ? UnibillState.SUCCESS_WITH_ERRORS : UnibillState.SUCCESS);
				}
				else
				{
					Unibiller.onBillerReady(UnibillState.CRITICAL_ERROR);
				}
			}
		};
		biller.onPurchaseCancelled += Unibiller.onPurchaseCancelled;
		biller.onPurchaseComplete += Unibiller.onPurchaseComplete;
		biller.onPurchaseFailed += Unibiller.onPurchaseFailed;
		biller.onPurchaseRefunded += Unibiller.onPurchaseRefunded;
		biller.onTransactionsRestored += Unibiller.onTransactionsRestored;
		biller.Initialise();
	}

	// Token: 0x04000108 RID: 264
	private static Biller biller;
}
