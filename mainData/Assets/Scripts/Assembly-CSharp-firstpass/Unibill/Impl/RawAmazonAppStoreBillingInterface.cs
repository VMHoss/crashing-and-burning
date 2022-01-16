using System;
using System.IO;
using UnityEngine;

namespace Unibill.Impl
{
	// Token: 0x02000021 RID: 33
	public class RawAmazonAppStoreBillingInterface : IRawAmazonAppStoreBillingInterface
	{
		// Token: 0x06000114 RID: 276 RVA: 0x00005684 File Offset: 0x00003884
		public RawAmazonAppStoreBillingInterface(UnibillConfiguration config)
		{
			if (config.CurrentPlatform == BillingPlatform.AmazonAppstore && config.AmazonSandboxEnabled)
			{
				string text = ((TextAsset)Resources.Load("amazon.sdktester.json")).text;
				File.WriteAllText("/sdcard/amazon.sdktester.json", text);
			}
			using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.outlinegames.unibillAmazon.Unibill"))
			{
				this.amazon = androidJavaClass.CallStatic<AndroidJavaObject>("instance", new object[0]);
			}
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00005720 File Offset: 0x00003920
		public void initialise(AmazonAppStoreBillingService amazon)
		{
			new GameObject().AddComponent<AmazonAppStoreCallbackMonoBehaviour>().initialise(amazon);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00005734 File Offset: 0x00003934
		public void initiateItemDataRequest(string[] productIds)
		{
			IntPtr methodID = AndroidJNI.GetMethodID(this.amazon.GetRawClass(), "initiateItemDataRequest", "([Ljava/lang/String;)V");
			AndroidJNI.CallVoidMethod(this.amazon.GetRawObject(), methodID, AndroidJNIHelper.CreateJNIArgArray(new object[]
			{
				productIds
			}));
		}

		// Token: 0x06000117 RID: 279 RVA: 0x0000577C File Offset: 0x0000397C
		public void initiatePurchaseRequest(string productId)
		{
			this.amazon.Call("initiatePurchaseRequest", new object[]
			{
				productId
			});
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00005798 File Offset: 0x00003998
		public void restoreTransactions()
		{
			this.amazon.Call("restoreTransactions", new object[0]);
		}

		// Token: 0x04000056 RID: 86
		private AndroidJavaObject amazon;
	}
}
