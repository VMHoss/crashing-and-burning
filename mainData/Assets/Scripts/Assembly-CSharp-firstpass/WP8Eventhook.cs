using System;
using Unibill.Impl;
using UnityEngine;

// Token: 0x0200002E RID: 46
internal class WP8Eventhook : MonoBehaviour
{
	// Token: 0x0600018C RID: 396 RVA: 0x00006AD0 File Offset: 0x00004CD0
	public void Start()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	// Token: 0x0600018D RID: 397 RVA: 0x00006AE0 File Offset: 0x00004CE0
	public void OnApplicationPause(bool paused)
	{
		if (!paused && this.callback != null)
		{
			this.callback.enumerateLicenses();
		}
	}

	// Token: 0x0400007A RID: 122
	public WP8BillingService callback;
}
