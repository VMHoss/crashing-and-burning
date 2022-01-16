using System;

namespace Unibill.Impl
{
	// Token: 0x0200002B RID: 43
	public interface IRawGooglePlayInterface
	{
		// Token: 0x06000174 RID: 372
		void initialise(GooglePlayBillingService callback, string publicKey);

		// Token: 0x06000175 RID: 373
		void purchase(string product);

		// Token: 0x06000176 RID: 374
		void restoreTransactions();
	}
}
