using System;

namespace Unibill.Impl
{
	// Token: 0x02000020 RID: 32
	public interface IRawAmazonAppStoreBillingInterface
	{
		// Token: 0x06000110 RID: 272
		void initialise(AmazonAppStoreBillingService amazon);

		// Token: 0x06000111 RID: 273
		void initiateItemDataRequest(string[] productIds);

		// Token: 0x06000112 RID: 274
		void initiatePurchaseRequest(string productId);

		// Token: 0x06000113 RID: 275
		void restoreTransactions();
	}
}
