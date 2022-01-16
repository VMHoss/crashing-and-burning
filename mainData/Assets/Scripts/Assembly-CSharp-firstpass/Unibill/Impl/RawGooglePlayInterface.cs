using System;
using UnityEngine;

namespace Unibill.Impl
{
	// Token: 0x0200002C RID: 44
	public class RawGooglePlayInterface : IRawGooglePlayInterface
	{
		// Token: 0x06000178 RID: 376 RVA: 0x0000655C File Offset: 0x0000475C
		public void initialise(GooglePlayBillingService callback, string publicKey)
		{
			new GameObject().AddComponent<GooglePlayCallbackMonoBehaviour>().Initialise(callback);
			using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.outlinegames.unibill.UniBill"))
			{
				this.plugin = androidJavaClass.CallStatic<AndroidJavaObject>("instance", new object[0]);
			}
			this.plugin.Call("initialise", new object[]
			{
				publicKey
			});
		}

		// Token: 0x06000179 RID: 377 RVA: 0x000065E4 File Offset: 0x000047E4
		public void restoreTransactions()
		{
			this.plugin.Call("restoreTransactions", new object[0]);
		}

		// Token: 0x0600017A RID: 378 RVA: 0x000065FC File Offset: 0x000047FC
		public void purchase(string id)
		{
			this.plugin.Call("purchaseProduct", new object[]
			{
				id
			});
		}

		// Token: 0x04000070 RID: 112
		private AndroidJavaObject plugin;
	}
}
