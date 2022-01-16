using System;
using Unibill.Impl;
using UnityEngine;

// Token: 0x0200001E RID: 30
[AddComponentMenu("")]
public class AmazonAppStoreCallbackMonoBehaviour : MonoBehaviour
{
	// Token: 0x060000FF RID: 255 RVA: 0x00005530 File Offset: 0x00003730
	public void Start()
	{
		base.gameObject.name = base.GetType().ToString();
		UnityEngine.Object.DontDestroyOnLoad(this);
	}

	// Token: 0x06000100 RID: 256 RVA: 0x0000555C File Offset: 0x0000375C
	public void initialise(AmazonAppStoreBillingService amazon)
	{
		this.amazon = amazon;
	}

	// Token: 0x06000101 RID: 257 RVA: 0x00005568 File Offset: 0x00003768
	public void onSDKAvailable(string isSandboxEnvironment)
	{
		this.amazon.onSDKAvailable(isSandboxEnvironment);
	}

	// Token: 0x06000102 RID: 258 RVA: 0x00005578 File Offset: 0x00003778
	public void onGetItemDataFailed(string empty)
	{
		this.amazon.onGetItemDataFailed();
	}

	// Token: 0x06000103 RID: 259 RVA: 0x00005588 File Offset: 0x00003788
	public void onProductListReceived(string productCSVString)
	{
		this.amazon.onProductListReceived(productCSVString);
	}

	// Token: 0x06000104 RID: 260 RVA: 0x00005598 File Offset: 0x00003798
	public void onPurchaseFailed(string item)
	{
		this.amazon.onPurchaseFailed(item);
	}

	// Token: 0x06000105 RID: 261 RVA: 0x000055A8 File Offset: 0x000037A8
	public void onPurchaseSucceeded(string item)
	{
		this.amazon.onPurchaseSucceeded(item);
	}

	// Token: 0x06000106 RID: 262 RVA: 0x000055B8 File Offset: 0x000037B8
	public void onTransactionsRestored(string success)
	{
		this.amazon.onTransactionsRestored(success);
	}

	// Token: 0x06000107 RID: 263 RVA: 0x000055C8 File Offset: 0x000037C8
	public void onPurchaseUpdateFailed(string empty)
	{
		this.amazon.onPurchaseUpdateFailed();
	}

	// Token: 0x06000108 RID: 264 RVA: 0x000055D8 File Offset: 0x000037D8
	public void onPurchaseUpdateSuccess(string data)
	{
		this.amazon.onPurchaseUpdateSuccess(data);
	}

	// Token: 0x06000109 RID: 265 RVA: 0x000055E8 File Offset: 0x000037E8
	public void onUserIdRetrieved(string userId)
	{
		this.amazon.onUserIdRetrieved(userId);
	}

	// Token: 0x04000054 RID: 84
	private AmazonAppStoreBillingService amazon;
}
