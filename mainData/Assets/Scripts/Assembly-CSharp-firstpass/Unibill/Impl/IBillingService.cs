using System;

namespace Unibill.Impl
{
	// Token: 0x02000033 RID: 51
	public interface IBillingService
	{
		// Token: 0x060001CC RID: 460
		void initialise(IBillingServiceCallback biller);

		// Token: 0x060001CD RID: 461
		void purchase(string item);

		// Token: 0x060001CE RID: 462
		void restoreTransactions();
	}
}
