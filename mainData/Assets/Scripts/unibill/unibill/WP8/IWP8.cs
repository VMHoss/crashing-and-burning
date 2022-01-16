using System;

namespace unibill.WP8
{
	// Token: 0x02000004 RID: 4
	public interface IWP8
	{
		// Token: 0x06000009 RID: 9
		void Initialise(IWP8Callback callback, Product[] products, int delayInMilliseconds);

		// Token: 0x0600000A RID: 10
		void Purchase(string productId);

		// Token: 0x0600000B RID: 11
		void EnumerateLicenses();
	}
}
