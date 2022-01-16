using System;

namespace unibill.WP8
{
	// Token: 0x02000003 RID: 3
	public interface IWP8Callback
	{
		// Token: 0x06000002 RID: 2
		void OnProductListReceived(Product[] products);

		// Token: 0x06000003 RID: 3
		void OnProductListError(string message);

		// Token: 0x06000004 RID: 4
		void OnPurchaseSucceeded(string productId);

		// Token: 0x06000005 RID: 5
		void OnPurchaseSucceded(string productId, string receipt);

		// Token: 0x06000006 RID: 6
		void OnPurchaseCancelled(string productId);

		// Token: 0x06000007 RID: 7
		void OnPurchaseFailed(string productId, string error);

		// Token: 0x06000008 RID: 8
		void logError(string error);
	}
}
