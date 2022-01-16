using System;
using UnityEngine;

// Token: 0x0200014F RID: 335
public class FlurryAnalyticsListener : MonoBehaviour
{
	// Token: 0x060009A4 RID: 2468 RVA: 0x0004B6A8 File Offset: 0x000498A8
	private void Awake()
	{
		if (FlurryAnalyticsListener._instanceFound)
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		FlurryAnalyticsListener._instanceFound = true;
		UnityEngine.Object.DontDestroyOnLoad(this);
		FlurryAnalytics.Instance();
	}

	// Token: 0x04000A1B RID: 2587
	private static bool _instanceFound;
}
