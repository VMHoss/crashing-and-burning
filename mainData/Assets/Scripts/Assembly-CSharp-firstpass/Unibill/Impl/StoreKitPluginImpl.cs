using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Unibill.Impl
{
	// Token: 0x02000027 RID: 39
	public class StoreKitPluginImpl : IStoreKitPlugin
	{
		// Token: 0x06000150 RID: 336
		[DllImport("__Internal")]
		private static extern bool _storeKitPaymentsAvailable();

		// Token: 0x06000151 RID: 337
		[DllImport("__Internal")]
		private static extern void _storeKitRequestProductData(string productIdentifiers);

		// Token: 0x06000152 RID: 338
		[DllImport("__Internal")]
		private static extern void _storeKitPurchaseProduct(string productId);

		// Token: 0x06000153 RID: 339
		[DllImport("__Internal")]
		private static extern void _storeKitRestoreTransactions();

		// Token: 0x06000154 RID: 340 RVA: 0x00005EB4 File Offset: 0x000040B4
		public void initialise(AppleAppStoreBillingService svc)
		{
			GameObject gameObject = new GameObject();
			gameObject.AddComponent<AppleAppStoreCallbackMonoBehaviour>().initialise(svc);
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00005ED4 File Offset: 0x000040D4
		public bool storeKitPaymentsAvailable()
		{
			return StoreKitPluginImpl._storeKitPaymentsAvailable();
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00005EDC File Offset: 0x000040DC
		public void storeKitRequestProductData(string productIdentifiers)
		{
			StoreKitPluginImpl._storeKitRequestProductData(productIdentifiers);
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00005EE4 File Offset: 0x000040E4
		public void storeKitPurchaseProduct(string productId)
		{
			StoreKitPluginImpl._storeKitPurchaseProduct(productId);
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00005EEC File Offset: 0x000040EC
		public void storeKitRestoreTransactions()
		{
			StoreKitPluginImpl._storeKitRestoreTransactions();
		}
	}
}
