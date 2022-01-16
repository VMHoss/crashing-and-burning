using System;
using System.Collections.Generic;
using System.Linq;
using unibill.WP8;
using Uniject;

namespace Unibill.Impl
{
	// Token: 0x0200002D RID: 45
	internal class WP8BillingService : IBillingService, IWP8Callback
	{
		// Token: 0x0600017B RID: 379 RVA: 0x00006618 File Offset: 0x00004818
		public WP8BillingService(IWP8 wp8, InventoryDatabase db, ProductIdRemapper remapper, TransactionDatabase tDb, ILogger logger)
		{
			this.wp8 = wp8;
			this.db = db;
			this.tDb = tDb;
			this.remapper = remapper;
			this.logger = logger;
		}

		// Token: 0x0600017C RID: 380 RVA: 0x0000665C File Offset: 0x0000485C
		public void initialise(IBillingServiceCallback biller)
		{
			this.callback = biller;
			this.init(0);
		}

		// Token: 0x0600017D RID: 381 RVA: 0x0000666C File Offset: 0x0000486C
		private void init(int delay)
		{
			IEnumerable<Product> source = from x in this.db.AllPurchasableItems
			where x.PurchaseType != PurchaseType.Subscription
			select new Product
			{
				Consumable = (x.PurchaseType == PurchaseType.Consumable),
				Description = x.description,
				Id = this.remapper.mapItemIdToPlatformSpecificId(x),
				Price = "$123.45",
				Title = x.name
			};
			this.wp8.Initialise(this, source.ToArray<Product>(), delay);
		}

		// Token: 0x0600017E RID: 382 RVA: 0x000066CC File Offset: 0x000048CC
		public void purchase(string item)
		{
			if (this.unknownProducts.Contains(item))
			{
				this.callback.logError(UnibillError.WP8_ATTEMPTING_TO_PURCHASE_PRODUCT_NOT_RETURNED_BY_MICROSOFT, new object[]
				{
					item
				});
				this.callback.onPurchaseFailedEvent(item);
				return;
			}
			this.wp8.Purchase(item);
		}

		// Token: 0x0600017F RID: 383 RVA: 0x0000671C File Offset: 0x0000491C
		public void restoreTransactions()
		{
			this.enumerateLicenses();
			this.callback.onTransactionsRestoredSuccess();
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00006730 File Offset: 0x00004930
		public void enumerateLicenses()
		{
			this.wp8.EnumerateLicenses();
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00006740 File Offset: 0x00004940
		public void logError(string error)
		{
			this.logger.LogError(error, new object[0]);
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00006754 File Offset: 0x00004954
		public void OnProductListReceived(Product[] products)
		{
			if (products.Length == 0)
			{
				this.callback.logError(UnibillError.WP8_NO_PRODUCTS_RETURNED);
				this.callback.onSetupComplete(false);
				return;
			}
			HashSet<string> hashSet = new HashSet<string>();
			foreach (Product product in products)
			{
				if (this.remapper.canMapProductSpecificId(product.Id))
				{
					hashSet.Add(product.Id);
					PurchasableItem purchasableItemFromPlatformSpecificId = this.remapper.getPurchasableItemFromPlatformSpecificId(product.Id);
					PurchasableItem.Writer.setLocalizedPrice(purchasableItemFromPlatformSpecificId, product.Price);
					PurchasableItem.Writer.setLocalizedTitle(purchasableItemFromPlatformSpecificId, product.Title);
					PurchasableItem.Writer.setLocalizedDescription(purchasableItemFromPlatformSpecificId, product.Description);
				}
				else
				{
					this.logger.LogError("Warning: Unknown product identifier: {0}", new object[]
					{
						product.Id
					});
				}
			}
			this.unknownProducts = new HashSet<string>(from x in this.db.AllNonSubscriptionPurchasableItems
			select this.remapper.mapItemIdToPlatformSpecificId(x));
			this.unknownProducts.ExceptWith(hashSet);
			if (this.unknownProducts.Count > 0)
			{
				foreach (string text in this.unknownProducts)
				{
					this.callback.logError(UnibillError.WP8_MISSING_PRODUCT, new object[]
					{
						text,
						this.remapper.getPurchasableItemFromPlatformSpecificId(text).Id
					});
				}
			}
			this.callback.onSetupComplete(true);
		}

		// Token: 0x06000183 RID: 387 RVA: 0x000068F8 File Offset: 0x00004AF8
		public void OnPurchaseFailed(string productId, string error)
		{
			this.logger.LogError("Purchase failed: {0}, {1}", new object[]
			{
				productId,
				error
			});
			this.callback.onPurchaseFailedEvent(productId);
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00006930 File Offset: 0x00004B30
		public void OnPurchaseCancelled(string productId)
		{
			this.callback.onPurchaseCancelledEvent(productId);
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00006940 File Offset: 0x00004B40
		public void OnPurchaseSucceded(string productId, string receipt)
		{
			this.logger.LogError("PURCHASE SUCCEEDED!:{0}", new object[]
			{
				WP8BillingService.count++
			});
			PurchasableItem purchasableItemFromPlatformSpecificId = this.remapper.getPurchasableItemFromPlatformSpecificId(productId);
			switch (purchasableItemFromPlatformSpecificId.PurchaseType)
			{
			case PurchaseType.Consumable:
				this.callback.onPurchaseSucceeded(productId, receipt);
				break;
			case PurchaseType.NonConsumable:
			case PurchaseType.Subscription:
			{
				PurchasableItem purchasableItemFromPlatformSpecificId2 = this.remapper.getPurchasableItemFromPlatformSpecificId(productId);
				if (this.tDb.getPurchaseHistory(purchasableItemFromPlatformSpecificId2) == 0)
				{
					this.callback.onPurchaseSucceeded(productId, receipt);
				}
				break;
			}
			}
		}

		// Token: 0x06000186 RID: 390 RVA: 0x000069E8 File Offset: 0x00004BE8
		public void OnPurchaseSucceeded(string productId)
		{
			this.OnPurchaseSucceded(productId, string.Empty);
		}

		// Token: 0x06000187 RID: 391 RVA: 0x000069F8 File Offset: 0x00004BF8
		public void OnProductListError(string message)
		{
			if (message.Contains("0x805A0194"))
			{
				this.callback.logError(UnibillError.WP8_APP_ID_NOT_KNOWN);
				this.callback.onSetupComplete(false);
			}
			else
			{
				this.logError("Unable to retrieve product listings. Unibill will automatically retry...");
				this.logError(message);
				this.init(3000);
			}
		}

		// Token: 0x04000071 RID: 113
		private IWP8 wp8;

		// Token: 0x04000072 RID: 114
		private IBillingServiceCallback callback;

		// Token: 0x04000073 RID: 115
		private InventoryDatabase db;

		// Token: 0x04000074 RID: 116
		private TransactionDatabase tDb;

		// Token: 0x04000075 RID: 117
		private ProductIdRemapper remapper;

		// Token: 0x04000076 RID: 118
		private ILogger logger;

		// Token: 0x04000077 RID: 119
		private HashSet<string> unknownProducts = new HashSet<string>();

		// Token: 0x04000078 RID: 120
		private static int count;
	}
}
