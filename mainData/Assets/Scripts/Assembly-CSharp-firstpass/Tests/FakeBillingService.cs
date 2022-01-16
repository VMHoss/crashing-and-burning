using System;
using System.Collections.Generic;
using Unibill.Impl;

namespace Tests
{
	// Token: 0x02000031 RID: 49
	public class FakeBillingService : IBillingService
	{
		// Token: 0x060001C6 RID: 454 RVA: 0x000075C4 File Offset: 0x000057C4
		public FakeBillingService(ProductIdRemapper remapper)
		{
			this.remapper = remapper;
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x000075E0 File Offset: 0x000057E0
		public void initialise(IBillingServiceCallback biller)
		{
			this.biller = biller;
			if (this.reportError)
			{
				biller.logError(UnibillError.AMAZONAPPSTORE_GETITEMDATAREQUEST_FAILED);
			}
			biller.onSetupComplete(!this.reportCriticalError);
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0000760C File Offset: 0x0000580C
		public void purchase(string item)
		{
			this.purchaseCalled = true;
			if (this.remapper.getPurchasableItemFromPlatformSpecificId(item).PurchaseType == PurchaseType.NonConsumable)
			{
				this.purchasedItems.Add(item);
			}
			this.biller.onPurchaseSucceeded(item);
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00007650 File Offset: 0x00005850
		public void restoreTransactions()
		{
			this.restoreCalled = true;
			foreach (string platformSpecificId in this.purchasedItems)
			{
				this.biller.onPurchaseSucceeded(platformSpecificId);
			}
			this.biller.onTransactionsRestoredSuccess();
		}

		// Token: 0x04000096 RID: 150
		private IBillingServiceCallback biller;

		// Token: 0x04000097 RID: 151
		private List<string> purchasedItems = new List<string>();

		// Token: 0x04000098 RID: 152
		private ProductIdRemapper remapper;

		// Token: 0x04000099 RID: 153
		public bool reportError;

		// Token: 0x0400009A RID: 154
		public bool reportCriticalError;

		// Token: 0x0400009B RID: 155
		public bool purchaseCalled;

		// Token: 0x0400009C RID: 156
		public bool restoreCalled;
	}
}
