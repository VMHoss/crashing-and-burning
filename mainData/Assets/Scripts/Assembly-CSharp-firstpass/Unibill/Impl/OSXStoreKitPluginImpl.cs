using System;
using System.Runtime.InteropServices;

namespace Unibill.Impl
{
	// Token: 0x02000026 RID: 38
	public class OSXStoreKitPluginImpl : IStoreKitPlugin
	{
		// Token: 0x0600013E RID: 318
		[DllImport("unibillosx")]
		private static extern bool _storeKitPaymentsAvailable();

		// Token: 0x0600013F RID: 319
		[DllImport("unibillosx")]
		private static extern void _storeKitRequestProductData(string productIdentifiers);

		// Token: 0x06000140 RID: 320
		[DllImport("unibillosx")]
		private static extern void _storeKitPurchaseProduct(string productId);

		// Token: 0x06000141 RID: 321
		[DllImport("unibillosx")]
		private static extern void _storeKitRestoreTransactions();

		// Token: 0x06000142 RID: 322 RVA: 0x00005D14 File Offset: 0x00003F14
		public void initialise(AppleAppStoreBillingService callback)
		{
			OSXStoreKitPluginImpl.callback = callback;
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00005D1C File Offset: 0x00003F1C
		public bool storeKitPaymentsAvailable()
		{
			return OSXStoreKitPluginImpl._storeKitPaymentsAvailable();
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00005D24 File Offset: 0x00003F24
		public void storeKitRequestProductData(string productIdentifiers)
		{
			OSXStoreKitPluginImpl._storeKitRequestProductData(productIdentifiers);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00005D2C File Offset: 0x00003F2C
		public void storeKitPurchaseProduct(string productId)
		{
			OSXStoreKitPluginImpl._storeKitPurchaseProduct(productId);
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00005D34 File Offset: 0x00003F34
		public void storeKitRestoreTransactions()
		{
			OSXStoreKitPluginImpl._storeKitRestoreTransactions();
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00005D3C File Offset: 0x00003F3C
		public static void UnibillSendMessage(string method, string argument)
		{
			switch (method)
			{
			case "onProductListReceived":
				OSXStoreKitPluginImpl.onProductListReceived(argument);
				break;
			case "onProductPurchaseSuccess":
				OSXStoreKitPluginImpl.onProductPurchaseSuccess(argument);
				break;
			case "onProductPurchaseCancelled":
				OSXStoreKitPluginImpl.onProductPurchaseCancelled(argument);
				break;
			case "onProductPurchaseFailed":
				OSXStoreKitPluginImpl.onProductPurchaseFailed(argument);
				break;
			case "onTransactionsRestoredSuccess":
				OSXStoreKitPluginImpl.onTransactionsRestoredSuccess(argument);
				break;
			case "onTransactionsRestoredFail":
				OSXStoreKitPluginImpl.onTransactionsRestoredFail(argument);
				break;
			case "onFailedToRetrieveProductList":
				OSXStoreKitPluginImpl.onFailedToRetrieveProductList(argument);
				break;
			}
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00005E44 File Offset: 0x00004044
		public static void onProductListReceived(string productList)
		{
			OSXStoreKitPluginImpl.callback.onProductListReceived(productList);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00005E54 File Offset: 0x00004054
		public static void onProductPurchaseSuccess(string productId)
		{
			OSXStoreKitPluginImpl.callback.onPurchaseSucceeded(productId);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00005E64 File Offset: 0x00004064
		public static void onProductPurchaseCancelled(string productId)
		{
			OSXStoreKitPluginImpl.callback.onPurchaseCancelled(productId);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00005E74 File Offset: 0x00004074
		public static void onProductPurchaseFailed(string productId)
		{
			OSXStoreKitPluginImpl.callback.onPurchaseFailed(productId);
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00005E84 File Offset: 0x00004084
		public static void onTransactionsRestoredSuccess(string empty)
		{
			OSXStoreKitPluginImpl.callback.onTransactionsRestoredSuccess();
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00005E90 File Offset: 0x00004090
		public static void onTransactionsRestoredFail(string error)
		{
			OSXStoreKitPluginImpl.callback.onTransactionsRestoredFail(error);
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00005EA0 File Offset: 0x000040A0
		public static void onFailedToRetrieveProductList(string nop)
		{
			OSXStoreKitPluginImpl.callback.onFailedToRetrieveProductList();
		}

		// Token: 0x04000062 RID: 98
		private static AppleAppStoreBillingService callback;
	}
}
