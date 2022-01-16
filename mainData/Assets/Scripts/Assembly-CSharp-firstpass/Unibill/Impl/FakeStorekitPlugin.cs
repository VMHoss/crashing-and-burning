using System;

namespace Unibill.Impl
{
	// Token: 0x02000024 RID: 36
	public class FakeStorekitPlugin : IStoreKitPlugin
	{
		// Token: 0x06000131 RID: 305 RVA: 0x00005BF8 File Offset: 0x00003DF8
		public FakeStorekitPlugin(InventoryDatabase db, ProductIdRemapper mapper)
		{
			this.db = db;
			this.remapper = mapper;
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00005C28 File Offset: 0x00003E28
		public bool storeKitPaymentsAvailable()
		{
			return this.available;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00005C30 File Offset: 0x00003E30
		public void storeKitRequestProductData(string productIdentifiers)
		{
			if (this.functional)
			{
			}
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00005C40 File Offset: 0x00003E40
		public void storeKitPurchaseProduct(string productId)
		{
			if (this.functional)
			{
				this.callback.onPurchaseSucceeded(FakeStorekitPlugin.formatPurchaseResponse(productId));
			}
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00005C60 File Offset: 0x00003E60
		public void storeKitRestoreTransactions()
		{
			foreach (PurchasableItem item in this.db.AllNonConsumablePurchasableItems)
			{
				this.callback.onPurchaseSucceeded(FakeStorekitPlugin.formatPurchaseResponse(this.remapper.mapItemIdToPlatformSpecificId(item)));
			}
			this.callback.onTransactionsRestoredSuccess();
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00005CEC File Offset: 0x00003EEC
		public void initialise(AppleAppStoreBillingService callback)
		{
			this.callback = callback;
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00005CF8 File Offset: 0x00003EF8
		public static string formatPurchaseResponse(string productId)
		{
			return "{ \"productId\" : \"" + productId + "\", \"receipt\" : \"THIS IS A RECEIPT!\" }";
		}

		// Token: 0x0400005D RID: 93
		private InventoryDatabase db;

		// Token: 0x0400005E RID: 94
		private ProductIdRemapper remapper;

		// Token: 0x0400005F RID: 95
		private AppleAppStoreBillingService callback;

		// Token: 0x04000060 RID: 96
		public bool available = true;

		// Token: 0x04000061 RID: 97
		public bool functional = true;
	}
}
