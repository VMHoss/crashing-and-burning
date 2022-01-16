using System;

namespace Unibill.Impl
{
	// Token: 0x02000034 RID: 52
	public interface IBillingServiceCallback
	{
		// Token: 0x060001CF RID: 463
		void onSetupComplete(bool successful);

		// Token: 0x060001D0 RID: 464
		void onPurchaseSucceeded(string platformSpecificId);

		// Token: 0x060001D1 RID: 465
		void onPurchaseSucceeded(string platformSpecificId, string receipt);

		// Token: 0x060001D2 RID: 466
		void onPurchaseCancelledEvent(string item);

		// Token: 0x060001D3 RID: 467
		void onPurchaseRefundedEvent(string item);

		// Token: 0x060001D4 RID: 468
		void onPurchaseFailedEvent(string item);

		// Token: 0x060001D5 RID: 469
		void onTransactionsRestoredSuccess();

		// Token: 0x060001D6 RID: 470
		void onTransactionsRestoredFail(string error);

		// Token: 0x060001D7 RID: 471
		void logError(UnibillError error, params object[] args);

		// Token: 0x060001D8 RID: 472
		void logError(UnibillError error);
	}
}
