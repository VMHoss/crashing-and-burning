using System;
using Unibill.Impl;
using UnityEngine;

// Token: 0x0200002A RID: 42
[AddComponentMenu("")]
public class GooglePlayCallbackMonoBehaviour : MonoBehaviour
{
	// Token: 0x0600016A RID: 362 RVA: 0x0000649C File Offset: 0x0000469C
	public void Awake()
	{
		base.gameObject.name = base.GetType().ToString();
		UnityEngine.Object.DontDestroyOnLoad(this);
	}

	// Token: 0x0600016B RID: 363 RVA: 0x000064C8 File Offset: 0x000046C8
	public void Initialise(GooglePlayBillingService callback)
	{
		this.callback = callback;
	}

	// Token: 0x0600016C RID: 364 RVA: 0x000064D4 File Offset: 0x000046D4
	public void onProductListReceived(string json)
	{
		this.callback.onProductListReceived(json);
	}

	// Token: 0x0600016D RID: 365 RVA: 0x000064E4 File Offset: 0x000046E4
	public void onBillingNotSupported()
	{
		this.callback.onBillingNotSupported();
	}

	// Token: 0x0600016E RID: 366 RVA: 0x000064F4 File Offset: 0x000046F4
	public void onPurchaseSucceeded(string productId)
	{
		this.callback.onPurchaseSucceeded(productId);
	}

	// Token: 0x0600016F RID: 367 RVA: 0x00006504 File Offset: 0x00004704
	public void onPurchaseCancelled(string productId)
	{
		this.callback.onPurchaseCancelled(productId);
	}

	// Token: 0x06000170 RID: 368 RVA: 0x00006514 File Offset: 0x00004714
	public void onPurchaseRefunded(string productId)
	{
		this.callback.onPurchaseRefunded(productId);
	}

	// Token: 0x06000171 RID: 369 RVA: 0x00006524 File Offset: 0x00004724
	public void onPurchaseFailed(string productId)
	{
		this.callback.onPurchaseFailed(productId);
	}

	// Token: 0x06000172 RID: 370 RVA: 0x00006534 File Offset: 0x00004734
	public void onTransactionsRestored(string successString)
	{
		this.callback.onTransactionsRestored(successString);
	}

	// Token: 0x06000173 RID: 371 RVA: 0x00006544 File Offset: 0x00004744
	public void onInvalidPublicKey(string publicKey)
	{
		this.callback.onInvalidPublicKey(publicKey);
	}

	// Token: 0x0400006F RID: 111
	private GooglePlayBillingService callback;
}
