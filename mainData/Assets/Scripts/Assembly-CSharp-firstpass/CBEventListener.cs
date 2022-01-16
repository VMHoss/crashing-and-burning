using System;
using Chartboost;
using UnityEngine;

// Token: 0x02000011 RID: 17
public class CBEventListener : MonoBehaviour
{
	// Token: 0x0600009F RID: 159 RVA: 0x000046F0 File Offset: 0x000028F0
	private void OnEnable()
	{
		CBManager.didFailToLoadInterstitialEvent += this.didFailToLoadInterstitialEvent;
		CBManager.didDismissInterstitialEvent += this.didDismissInterstitialEvent;
		CBManager.didCloseInterstitialEvent += this.didCloseInterstitialEvent;
		CBManager.didClickInterstitialEvent += this.didClickInterstitialEvent;
		CBManager.didCacheInterstitialEvent += this.didCacheInterstitialEvent;
		CBManager.didShowInterstitialEvent += this.didShowInterstitialEvent;
		CBManager.didFailToLoadMoreAppsEvent += this.didFailToLoadMoreAppsEvent;
		CBManager.didDismissMoreAppsEvent += this.didDismissMoreAppsEvent;
		CBManager.didCloseMoreAppsEvent += this.didCloseMoreAppsEvent;
		CBManager.didClickMoreAppsEvent += this.didClickMoreAppsEvent;
		CBManager.didCacheMoreAppsEvent += this.didCacheMoreAppsEvent;
		CBManager.didShowMoreAppsEvent += this.didShowMoreAppsEvent;
	}

	// Token: 0x060000A0 RID: 160 RVA: 0x000047CC File Offset: 0x000029CC
	private void OnDisable()
	{
		CBManager.didFailToLoadInterstitialEvent -= this.didFailToLoadInterstitialEvent;
		CBManager.didDismissInterstitialEvent -= this.didDismissInterstitialEvent;
		CBManager.didCloseInterstitialEvent -= this.didCloseInterstitialEvent;
		CBManager.didClickInterstitialEvent -= this.didClickInterstitialEvent;
		CBManager.didCacheInterstitialEvent -= this.didCacheInterstitialEvent;
		CBManager.didShowInterstitialEvent -= this.didShowInterstitialEvent;
		CBManager.didFailToLoadMoreAppsEvent -= this.didFailToLoadMoreAppsEvent;
		CBManager.didDismissMoreAppsEvent -= this.didDismissMoreAppsEvent;
		CBManager.didCloseMoreAppsEvent -= this.didCloseMoreAppsEvent;
		CBManager.didClickMoreAppsEvent -= this.didClickMoreAppsEvent;
		CBManager.didCacheMoreAppsEvent -= this.didCacheMoreAppsEvent;
		CBManager.didShowMoreAppsEvent -= this.didShowMoreAppsEvent;
	}

	// Token: 0x060000A1 RID: 161 RVA: 0x000048A8 File Offset: 0x00002AA8
	private void didFailToLoadInterstitialEvent(string location)
	{
		Debug.Log("didFailToLoadInterstitialEvent: " + location);
	}

	// Token: 0x060000A2 RID: 162 RVA: 0x000048BC File Offset: 0x00002ABC
	private void didDismissInterstitialEvent(string location)
	{
		Debug.Log("didDismissInterstitialEvent: " + location);
	}

	// Token: 0x060000A3 RID: 163 RVA: 0x000048D0 File Offset: 0x00002AD0
	private void didCloseInterstitialEvent(string location)
	{
		Debug.Log("didCloseInterstitialEvent: " + location);
	}

	// Token: 0x060000A4 RID: 164 RVA: 0x000048E4 File Offset: 0x00002AE4
	private void didClickInterstitialEvent(string location)
	{
		Debug.Log("didClickInterstitialEvent: " + location);
	}

	// Token: 0x060000A5 RID: 165 RVA: 0x000048F8 File Offset: 0x00002AF8
	private void didCacheInterstitialEvent(string location)
	{
		Debug.Log("didCacheInterstitialEvent: " + location);
	}

	// Token: 0x060000A6 RID: 166 RVA: 0x0000490C File Offset: 0x00002B0C
	private void didShowInterstitialEvent(string location)
	{
		Debug.Log("didShowInterstitialEvent: " + location);
	}

	// Token: 0x060000A7 RID: 167 RVA: 0x00004920 File Offset: 0x00002B20
	private void didFailToLoadMoreAppsEvent()
	{
		Debug.Log("didFailToLoadMoreAppsEvent");
	}

	// Token: 0x060000A8 RID: 168 RVA: 0x0000492C File Offset: 0x00002B2C
	private void didDismissMoreAppsEvent()
	{
		Debug.Log("didDismissMoreAppsEvent");
	}

	// Token: 0x060000A9 RID: 169 RVA: 0x00004938 File Offset: 0x00002B38
	private void didCloseMoreAppsEvent()
	{
		Debug.Log("didCloseMoreAppsEvent");
	}

	// Token: 0x060000AA RID: 170 RVA: 0x00004944 File Offset: 0x00002B44
	private void didClickMoreAppsEvent()
	{
		Debug.Log("didClickMoreAppsEvent");
	}

	// Token: 0x060000AB RID: 171 RVA: 0x00004950 File Offset: 0x00002B50
	private void didCacheMoreAppsEvent()
	{
		Debug.Log("didCacheMoreAppsEvent");
	}

	// Token: 0x060000AC RID: 172 RVA: 0x0000495C File Offset: 0x00002B5C
	private void didShowMoreAppsEvent()
	{
		Debug.Log("didShowMoreAppsEvent");
	}
}
