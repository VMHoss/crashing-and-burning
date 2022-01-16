using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Unibill.Impl
{
	// Token: 0x02000022 RID: 34
	public class AppleAppStoreBillingService : IBillingService
	{
		// Token: 0x06000119 RID: 281 RVA: 0x000057B0 File Offset: 0x000039B0
		public AppleAppStoreBillingService(InventoryDatabase db, ProductIdRemapper mapper, IStoreKitPlugin storekit)
		{
			this.storekit = storekit;
			this.remapper = mapper;
			storekit.initialise(this);
			this.products = new HashSet<PurchasableItem>(db.AllPurchasableItems);
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600011A RID: 282 RVA: 0x000057F4 File Offset: 0x000039F4
		// (set) Token: 0x0600011B RID: 283 RVA: 0x000057FC File Offset: 0x000039FC
		public IStoreKitPlugin storekit { get; private set; }

		// Token: 0x0600011C RID: 284 RVA: 0x00005808 File Offset: 0x00003A08
		public void initialise(IBillingServiceCallback biller)
		{
			this.biller = biller;
			bool flag = this.storekit.storeKitPaymentsAvailable();
			if (flag)
			{
				string[] allPlatformSpecificProductIds = this.remapper.getAllPlatformSpecificProductIds();
				this.storekit.storeKitRequestProductData(string.Join(",", allPlatformSpecificProductIds));
			}
			else
			{
				biller.logError(UnibillError.STOREKIT_BILLING_UNAVAILABLE);
				biller.onSetupComplete(false);
			}
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00005864 File Offset: 0x00003A64
		public void purchase(string item)
		{
			if (this.productsNotReturnedByStorekit.Contains(item))
			{
				this.biller.logError(UnibillError.STOREKIT_ATTEMPTING_TO_PURCHASE_PRODUCT_NOT_RETURNED_BY_STOREKIT, new object[]
				{
					item
				});
				this.biller.onPurchaseFailedEvent(item);
				return;
			}
			this.storekit.storeKitPurchaseProduct(item);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x000058B4 File Offset: 0x00003AB4
		public void restoreTransactions()
		{
			this.storekit.storeKitRestoreTransactions();
		}

		// Token: 0x0600011F RID: 287 RVA: 0x000058C4 File Offset: 0x00003AC4
		public void onProductListReceived(string productListString)
		{
			if (productListString.Length == 0)
			{
				this.biller.logError(UnibillError.STOREKIT_RETURNED_NO_PRODUCTS);
				this.biller.onSetupComplete(false);
				return;
			}
			Hashtable hashtable = (Hashtable)MiniJSON.jsonDecode(productListString);
			HashSet<PurchasableItem> hashSet = new HashSet<PurchasableItem>();
			foreach (object obj in hashtable.Keys)
			{
				PurchasableItem purchasableItemFromPlatformSpecificId = this.remapper.getPurchasableItemFromPlatformSpecificId(obj.ToString());
				Hashtable hashtable2 = (Hashtable)hashtable[obj];
				PurchasableItem.Writer.setLocalizedPrice(purchasableItemFromPlatformSpecificId, (string)hashtable2["price"]);
				PurchasableItem.Writer.setLocalizedTitle(purchasableItemFromPlatformSpecificId, (string)hashtable2["localizedTitle"]);
				PurchasableItem.Writer.setLocalizedDescription(purchasableItemFromPlatformSpecificId, (string)hashtable2["localizedDescription"]);
				hashSet.Add(purchasableItemFromPlatformSpecificId);
			}
			HashSet<PurchasableItem> hashSet2 = new HashSet<PurchasableItem>(this.products);
			hashSet2.ExceptWith(hashSet);
			if (hashSet2.Count > 0)
			{
				foreach (PurchasableItem purchasableItem in hashSet2)
				{
					this.biller.logError(UnibillError.STOREKIT_REQUESTPRODUCTS_MISSING_PRODUCT, new object[]
					{
						purchasableItem.Id,
						this.remapper.mapItemIdToPlatformSpecificId(purchasableItem)
					});
				}
			}
			this.productsNotReturnedByStorekit = new HashSet<string>(from x in hashSet2
			select this.remapper.mapItemIdToPlatformSpecificId(x));
			this.biller.onSetupComplete(true);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00005A98 File Offset: 0x00003C98
		public void onPurchaseSucceeded(string data)
		{
			Hashtable hashtable = (Hashtable)MiniJSON.jsonDecode(data);
			this.biller.onPurchaseSucceeded((string)hashtable["productId"], (string)hashtable["receipt"]);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00005ADC File Offset: 0x00003CDC
		public void onPurchaseCancelled(string productId)
		{
			this.biller.onPurchaseCancelledEvent(productId);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00005AEC File Offset: 0x00003CEC
		public void onPurchaseFailed(string productId)
		{
			this.biller.onPurchaseFailedEvent(productId);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00005AFC File Offset: 0x00003CFC
		public void onTransactionsRestoredSuccess()
		{
			this.biller.onTransactionsRestoredSuccess();
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00005B0C File Offset: 0x00003D0C
		public void onTransactionsRestoredFail(string error)
		{
			this.biller.onTransactionsRestoredFail(error);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00005B1C File Offset: 0x00003D1C
		public void onFailedToRetrieveProductList()
		{
			this.biller.logError(UnibillError.STOREKIT_FAILED_TO_RETRIEVE_PRODUCT_DATA);
			this.biller.onSetupComplete(true);
		}

		// Token: 0x04000057 RID: 87
		private IBillingServiceCallback biller;

		// Token: 0x04000058 RID: 88
		private ProductIdRemapper remapper;

		// Token: 0x04000059 RID: 89
		private HashSet<PurchasableItem> products;

		// Token: 0x0400005A RID: 90
		private HashSet<string> productsNotReturnedByStorekit = new HashSet<string>();
	}
}
