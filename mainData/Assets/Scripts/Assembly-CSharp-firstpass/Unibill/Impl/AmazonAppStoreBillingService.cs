using System;
using System.Collections;
using System.Collections.Generic;
using Uniject;

namespace Unibill.Impl
{
	// Token: 0x0200001D RID: 29
	public class AmazonAppStoreBillingService : IBillingService
	{
		// Token: 0x060000EC RID: 236 RVA: 0x00004FA4 File Offset: 0x000031A4
		public AmazonAppStoreBillingService(IRawAmazonAppStoreBillingInterface amazon, ProductIdRemapper remapper, InventoryDatabase db, TransactionDatabase tDb, ILogger logger)
		{
			this.remapper = remapper;
			this.db = db;
			this.logger = logger;
			logger.prefix = "UnibillAmazonBillingService";
			this.amazon = amazon;
			this.tDb = tDb;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00004FF4 File Offset: 0x000031F4
		public void initialise(IBillingServiceCallback biller)
		{
			this.callback = biller;
			this.amazon.initialise(this);
			this.amazon.initiateItemDataRequest(this.remapper.getAllPlatformSpecificProductIds());
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00005020 File Offset: 0x00003220
		public void purchase(string item)
		{
			if (this.unknownAmazonProducts.Contains(item))
			{
				this.callback.logError(UnibillError.AMAZONAPPSTORE_ATTEMPTING_TO_PURCHASE_PRODUCT_NOT_RETURNED_BY_AMAZON, new object[]
				{
					item
				});
				this.callback.onPurchaseFailedEvent(item);
				return;
			}
			this.amazon.initiatePurchaseRequest(item);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00005070 File Offset: 0x00003270
		public void restoreTransactions()
		{
			this.amazon.restoreTransactions();
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00005080 File Offset: 0x00003280
		public void onSDKAvailable(string isSandbox)
		{
			bool flag = bool.Parse(isSandbox);
			this.logger.Log("Running against {0} Amazon environment", new object[]
			{
				(!flag) ? "PRODUCTION" : "SANDBOX"
			});
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x000050C4 File Offset: 0x000032C4
		public void onGetItemDataFailed()
		{
			this.callback.logError(UnibillError.AMAZONAPPSTORE_GETITEMDATAREQUEST_FAILED);
			this.callback.onSetupComplete(true);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000050E0 File Offset: 0x000032E0
		public void onProductListReceived(string productListString)
		{
			Hashtable hashtable = (Hashtable)MiniJSON.jsonDecode(productListString);
			if (hashtable.Count == 0)
			{
				this.callback.logError(UnibillError.AMAZONAPPSTORE_GETITEMDATAREQUEST_NO_PRODUCTS_RETURNED);
				this.callback.onSetupComplete(false);
				return;
			}
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
			HashSet<PurchasableItem> hashSet2 = new HashSet<PurchasableItem>(this.db.AllPurchasableItems);
			hashSet2.ExceptWith(hashSet);
			if (hashSet2.Count > 0)
			{
				foreach (PurchasableItem purchasableItem in hashSet2)
				{
					this.unknownAmazonProducts.Add(this.remapper.mapItemIdToPlatformSpecificId(purchasableItem));
					this.callback.logError(UnibillError.AMAZONAPPSTORE_GETITEMDATAREQUEST_MISSING_PRODUCT, new object[]
					{
						purchasableItem.Id,
						this.remapper.mapItemIdToPlatformSpecificId(purchasableItem)
					});
				}
			}
			this.callback.onSetupComplete(true);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x000052B8 File Offset: 0x000034B8
		public void onUserIdRetrieved(string userId)
		{
			this.tDb.UserId = userId;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x000052C8 File Offset: 0x000034C8
		public void onTransactionsRestored(string successString)
		{
			bool flag = bool.Parse(successString);
			if (flag)
			{
				this.callback.onTransactionsRestoredSuccess();
			}
			else
			{
				this.callback.onTransactionsRestoredFail(string.Empty);
			}
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00005304 File Offset: 0x00003504
		public void onPurchaseFailed(string item)
		{
			this.callback.onPurchaseFailedEvent(item);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00005314 File Offset: 0x00003514
		public void onPurchaseCancelled(string item)
		{
			this.callback.onPurchaseCancelledEvent(item);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00005324 File Offset: 0x00003524
		public void onPurchaseSucceeded(string json)
		{
			Hashtable hashtable = (Hashtable)MiniJSON.jsonDecode(json);
			string platformSpecificId = (string)hashtable["productId"];
			string receipt = (string)hashtable["purchaseToken"];
			this.callback.onPurchaseSucceeded(platformSpecificId, receipt);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x0000536C File Offset: 0x0000356C
		public void onPurchaseUpdateFailed()
		{
			this.logger.LogWarning("AmazonAppStoreBillingService: onPurchaseUpdate() failed.", new object[0]);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00005384 File Offset: 0x00003584
		public void onPurchaseUpdateSuccess(string data)
		{
			List<string> revoked = new List<string>();
			List<string> purchased = new List<string>();
			AmazonAppStoreBillingService.parsePurchaseUpdates(revoked, purchased, data);
			this.onPurchaseUpdateSucceeded(revoked, purchased);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x000053B0 File Offset: 0x000035B0
		public void onPurchaseUpdateSucceeded(List<string> revoked, List<string> purchased)
		{
			foreach (string item in revoked)
			{
				this.callback.onPurchaseRefundedEvent(item);
			}
			foreach (string platformSpecificId in purchased)
			{
				this.callback.onPurchaseSucceeded(platformSpecificId);
			}
		}

		// Token: 0x060000FB RID: 251 RVA: 0x0000546C File Offset: 0x0000366C
		public static void parsePurchaseUpdates(List<string> revoked, List<string> purchased, string data)
		{
			string[] array = data.Split(new char[]
			{
				'|'
			});
			revoked.AddRange(array[0].Split(new char[]
			{
				','
			}));
			purchased.AddRange(array[1].Split(new char[]
			{
				','
			}));
			revoked.RemoveAll((string x) => x == string.Empty);
			purchased.RemoveAll((string x) => x == string.Empty);
		}

		// Token: 0x0400004B RID: 75
		private IBillingServiceCallback callback;

		// Token: 0x0400004C RID: 76
		private ProductIdRemapper remapper;

		// Token: 0x0400004D RID: 77
		private InventoryDatabase db;

		// Token: 0x0400004E RID: 78
		private ILogger logger;

		// Token: 0x0400004F RID: 79
		private IRawAmazonAppStoreBillingInterface amazon;

		// Token: 0x04000050 RID: 80
		private HashSet<string> unknownAmazonProducts = new HashSet<string>();

		// Token: 0x04000051 RID: 81
		private TransactionDatabase tDb;
	}
}
