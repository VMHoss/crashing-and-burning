using System;
using System.Collections;
using System.IO;

namespace Unibill.Impl
{
	// Token: 0x0200001F RID: 31
	public class FakeRawAmazonAppStoreBillingInterface : IRawAmazonAppStoreBillingInterface
	{
		// Token: 0x0600010B RID: 267 RVA: 0x00005600 File Offset: 0x00003800
		public void initialise(AmazonAppStoreBillingService amazon)
		{
			this.amazon = amazon;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x0000560C File Offset: 0x0000380C
		public void initiateItemDataRequest(string[] productIds)
		{
			this.amazon.onProductListReceived(File.ReadAllText("../../../data/requestProductsResponseAmazon.json"));
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00005624 File Offset: 0x00003824
		public void initiatePurchaseRequest(string productId)
		{
			this.amazon.onPurchaseSucceeded(FakeRawAmazonAppStoreBillingInterface.getPurchaseResponse(productId));
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00005638 File Offset: 0x00003838
		public void restoreTransactions()
		{
			this.amazon.onTransactionsRestored("true");
		}

		// Token: 0x0600010F RID: 271 RVA: 0x0000564C File Offset: 0x0000384C
		public static string getPurchaseResponse(string productId)
		{
			return new Hashtable
			{
				{
					"productId",
					productId
				},
				{
					"purchaseToken",
					"TOKEN"
				}
			}.toJson();
		}

		// Token: 0x04000055 RID: 85
		private AmazonAppStoreBillingService amazon;
	}
}
