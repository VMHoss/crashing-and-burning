using System;
using System.Collections;
using System.Collections.Generic;
using Uniject;

namespace Unibill.Impl
{
	// Token: 0x02000029 RID: 41
	public class GooglePlayBillingService : IBillingService
	{
		// Token: 0x0600015D RID: 349 RVA: 0x00005FC8 File Offset: 0x000041C8
		public GooglePlayBillingService(IRawGooglePlayInterface rawInterface, UnibillConfiguration config, ProductIdRemapper remapper, InventoryDatabase db, ILogger logger)
		{
			this.rawInterface = rawInterface;
			this.publicKey = config.GooglePlayPublicKey;
			this.remapper = remapper;
			this.db = db;
			this.logger = logger;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00006008 File Offset: 0x00004208
		public void initialise(IBillingServiceCallback callback)
		{
			this.callback = callback;
			if (this.publicKey == null || this.publicKey.Equals("[Your key]"))
			{
				callback.logError(UnibillError.GOOGLEPLAY_PUBLICKEY_NOTCONFIGURED, new object[]
				{
					this.publicKey
				});
				callback.onSetupComplete(false);
				return;
			}
			Hashtable hashtable = new Hashtable();
			hashtable.Add("publicKey", this.publicKey);
			ArrayList arrayList = new ArrayList();
			foreach (PurchasableItem purchasableItem in this.db.AllPurchasableItems)
			{
				arrayList.Add(new Hashtable
				{
					{
						"productId",
						this.remapper.mapItemIdToPlatformSpecificId(purchasableItem)
					},
					{
						"consumable",
						purchasableItem.PurchaseType == PurchaseType.Consumable
					}
				});
			}
			hashtable.Add("products", arrayList);
			string text = hashtable.toJson();
			this.rawInterface.initialise(this, text);
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00006134 File Offset: 0x00004334
		public void restoreTransactions()
		{
			this.rawInterface.restoreTransactions();
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00006144 File Offset: 0x00004344
		public void purchase(string item)
		{
			if (this.unknownAmazonProducts.Contains(item))
			{
				this.callback.logError(UnibillError.GOOGLEPLAY_ATTEMPTING_TO_PURCHASE_PRODUCT_NOT_RETURNED_BY_GOOGLEPLAY, new object[]
				{
					item
				});
				this.callback.onPurchaseFailedEvent(item);
				return;
			}
			this.rawInterface.purchase(item);
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00006194 File Offset: 0x00004394
		public void onBillingNotSupported()
		{
			this.callback.logError(UnibillError.GOOGLEPLAY_BILLING_UNAVAILABLE);
			this.callback.onSetupComplete(false);
		}

		// Token: 0x06000162 RID: 354 RVA: 0x000061B0 File Offset: 0x000043B0
		public void onPurchaseSucceeded(string json)
		{
			Hashtable hashtable = (Hashtable)MiniJSON.jsonDecode(json);
			this.callback.onPurchaseSucceeded((string)hashtable["productId"], (string)hashtable["signature"]);
		}

		// Token: 0x06000163 RID: 355 RVA: 0x000061F4 File Offset: 0x000043F4
		public void onPurchaseCancelled(string item)
		{
			this.callback.onPurchaseCancelledEvent(item);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00006204 File Offset: 0x00004404
		public void onPurchaseRefunded(string item)
		{
			this.callback.onPurchaseRefundedEvent(item);
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00006214 File Offset: 0x00004414
		public void onPurchaseFailed(string item)
		{
			this.callback.onPurchaseFailedEvent(item);
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00006224 File Offset: 0x00004424
		public void onTransactionsRestored(string success)
		{
			if (bool.Parse(success))
			{
				this.callback.onTransactionsRestoredSuccess();
			}
			else
			{
				this.callback.onTransactionsRestoredFail(string.Empty);
			}
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00006254 File Offset: 0x00004454
		public void onInvalidPublicKey(string key)
		{
			this.callback.logError(UnibillError.GOOGLEPLAY_PUBLICKEY_INVALID, new object[]
			{
				key
			});
			this.callback.onSetupComplete(false);
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00006284 File Offset: 0x00004484
		public void onProductListReceived(string productListString)
		{
			Hashtable hashtable = (Hashtable)MiniJSON.jsonDecode(productListString);
			if (hashtable.Count == 0)
			{
				this.callback.logError(UnibillError.GOOGLEPLAY_NO_PRODUCTS_RETURNED);
				this.callback.onSetupComplete(false);
				return;
			}
			HashSet<PurchasableItem> hashSet = new HashSet<PurchasableItem>();
			foreach (object obj in hashtable.Keys)
			{
				if (this.remapper.canMapProductSpecificId(obj.ToString()))
				{
					PurchasableItem purchasableItemFromPlatformSpecificId = this.remapper.getPurchasableItemFromPlatformSpecificId(obj.ToString());
					Hashtable hashtable2 = (Hashtable)hashtable[obj];
					PurchasableItem.Writer.setLocalizedPrice(purchasableItemFromPlatformSpecificId, hashtable2["price"].ToString());
					PurchasableItem.Writer.setLocalizedTitle(purchasableItemFromPlatformSpecificId, (string)hashtable2["localizedTitle"]);
					PurchasableItem.Writer.setLocalizedDescription(purchasableItemFromPlatformSpecificId, (string)hashtable2["localizedDescription"]);
					hashSet.Add(purchasableItemFromPlatformSpecificId);
				}
				else
				{
					this.logger.LogError("Warning: Unknown product identifier: {0}", new object[]
					{
						obj.ToString()
					});
				}
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

		// Token: 0x04000068 RID: 104
		private string publicKey;

		// Token: 0x04000069 RID: 105
		private IRawGooglePlayInterface rawInterface;

		// Token: 0x0400006A RID: 106
		private IBillingServiceCallback callback;

		// Token: 0x0400006B RID: 107
		private ProductIdRemapper remapper;

		// Token: 0x0400006C RID: 108
		private InventoryDatabase db;

		// Token: 0x0400006D RID: 109
		private ILogger logger;

		// Token: 0x0400006E RID: 110
		private HashSet<string> unknownAmazonProducts = new HashSet<string>();
	}
}
