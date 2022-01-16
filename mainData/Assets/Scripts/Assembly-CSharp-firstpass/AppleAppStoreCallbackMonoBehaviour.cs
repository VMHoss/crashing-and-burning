using System;
using Unibill.Impl;
using UnityEngine;

// Token: 0x02000023 RID: 35
[AddComponentMenu("")]
public class AppleAppStoreCallbackMonoBehaviour : MonoBehaviour
{
	// Token: 0x06000128 RID: 296 RVA: 0x00005B50 File Offset: 0x00003D50
	public void Awake()
	{
		base.gameObject.name = base.GetType().ToString();
		UnityEngine.Object.DontDestroyOnLoad(this);
	}

	// Token: 0x06000129 RID: 297 RVA: 0x00005B7C File Offset: 0x00003D7C
	public void initialise(AppleAppStoreBillingService callback)
	{
		this.callback = callback;
	}

	// Token: 0x0600012A RID: 298 RVA: 0x00005B88 File Offset: 0x00003D88
	public void onProductListReceived(string productList)
	{
		this.callback.onProductListReceived(productList);
	}

	// Token: 0x0600012B RID: 299 RVA: 0x00005B98 File Offset: 0x00003D98
	public void onProductPurchaseSuccess(string productId)
	{
		this.callback.onPurchaseSucceeded(productId);
	}

	// Token: 0x0600012C RID: 300 RVA: 0x00005BA8 File Offset: 0x00003DA8
	public void onProductPurchaseCancelled(string productId)
	{
		this.callback.onPurchaseCancelled(productId);
	}

	// Token: 0x0600012D RID: 301 RVA: 0x00005BB8 File Offset: 0x00003DB8
	public void onProductPurchaseFailed(string productId)
	{
		this.callback.onPurchaseFailed(productId);
	}

	// Token: 0x0600012E RID: 302 RVA: 0x00005BC8 File Offset: 0x00003DC8
	public void onTransactionsRestoredSuccess(string empty)
	{
		this.callback.onTransactionsRestoredSuccess();
	}

	// Token: 0x0600012F RID: 303 RVA: 0x00005BD8 File Offset: 0x00003DD8
	public void onTransactionsRestoredFail(string error)
	{
		this.callback.onTransactionsRestoredFail(error);
	}

	// Token: 0x06000130 RID: 304 RVA: 0x00005BE8 File Offset: 0x00003DE8
	public void onFailedToRetrieveProductList(string nop)
	{
		this.callback.onFailedToRetrieveProductList();
	}

	// Token: 0x0400005C RID: 92
	private AppleAppStoreBillingService callback;
}
