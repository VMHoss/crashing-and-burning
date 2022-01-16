using System;

namespace Unibill.Impl
{
	// Token: 0x02000025 RID: 37
	public interface IStoreKitPlugin
	{
		// Token: 0x06000138 RID: 312
		void initialise(AppleAppStoreBillingService callback);

		// Token: 0x06000139 RID: 313
		bool storeKitPaymentsAvailable();

		// Token: 0x0600013A RID: 314
		void storeKitRequestProductData(string productIdentifiers);

		// Token: 0x0600013B RID: 315
		void storeKitPurchaseProduct(string productId);

		// Token: 0x0600013C RID: 316
		void storeKitRestoreTransactions();
	}
}
