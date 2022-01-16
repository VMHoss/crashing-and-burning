using System;
using System.Collections;
using System.IO;

namespace Unibill.Impl
{
	// Token: 0x02000028 RID: 40
	public class FakeGooglePlayPlugin : IRawGooglePlayInterface
	{
		// Token: 0x06000159 RID: 345 RVA: 0x00005EF4 File Offset: 0x000040F4
		public FakeGooglePlayPlugin(InventoryDatabase db, ProductIdRemapper remapper)
		{
			this.db = db;
			this.remapper = remapper;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00005F14 File Offset: 0x00004114
		public void initialise(GooglePlayBillingService callback, string publicKey)
		{
			this.callback = callback;
			if (this.available)
			{
				callback.onProductListReceived(File.ReadAllText("../../../data/requestProductsResponse.json"));
			}
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00005F44 File Offset: 0x00004144
		public void purchase(string product)
		{
			Hashtable hashtable = new Hashtable();
			hashtable.Add("productId", product);
			hashtable.Add("signature", "signature");
			this.callback.onPurchaseSucceeded(hashtable.toJson());
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00005F84 File Offset: 0x00004184
		public void restoreTransactions()
		{
			this.callback.onPurchaseSucceeded(this.remapper.mapItemIdToPlatformSpecificId(this.db.AllNonConsumablePurchasableItems[0]));
			this.callback.onTransactionsRestored("true");
		}

		// Token: 0x04000064 RID: 100
		private GooglePlayBillingService callback;

		// Token: 0x04000065 RID: 101
		public bool available = true;

		// Token: 0x04000066 RID: 102
		private InventoryDatabase db;

		// Token: 0x04000067 RID: 103
		private ProductIdRemapper remapper;
	}
}
