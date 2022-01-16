using System;
using UnityEngine;

namespace Chartboost
{
	// Token: 0x02000010 RID: 16
	public class CBManager : MonoBehaviour
	{
		// Token: 0x1400000B RID: 11
		// (add) Token: 0x06000076 RID: 118 RVA: 0x000042B0 File Offset: 0x000024B0
		// (remove) Token: 0x06000077 RID: 119 RVA: 0x000042C8 File Offset: 0x000024C8
		public static event Action<string> didFailToLoadInterstitialEvent;

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x06000078 RID: 120 RVA: 0x000042E0 File Offset: 0x000024E0
		// (remove) Token: 0x06000079 RID: 121 RVA: 0x000042F8 File Offset: 0x000024F8
		public static event Action<string> didDismissInterstitialEvent;

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x0600007A RID: 122 RVA: 0x00004310 File Offset: 0x00002510
		// (remove) Token: 0x0600007B RID: 123 RVA: 0x00004328 File Offset: 0x00002528
		public static event Action<string> didCloseInterstitialEvent;

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x0600007C RID: 124 RVA: 0x00004340 File Offset: 0x00002540
		// (remove) Token: 0x0600007D RID: 125 RVA: 0x00004358 File Offset: 0x00002558
		public static event Action<string> didClickInterstitialEvent;

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x0600007E RID: 126 RVA: 0x00004370 File Offset: 0x00002570
		// (remove) Token: 0x0600007F RID: 127 RVA: 0x00004388 File Offset: 0x00002588
		public static event Action<string> didCacheInterstitialEvent;

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x06000080 RID: 128 RVA: 0x000043A0 File Offset: 0x000025A0
		// (remove) Token: 0x06000081 RID: 129 RVA: 0x000043B8 File Offset: 0x000025B8
		public static event Action<string> didShowInterstitialEvent;

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x06000082 RID: 130 RVA: 0x000043D0 File Offset: 0x000025D0
		// (remove) Token: 0x06000083 RID: 131 RVA: 0x000043E8 File Offset: 0x000025E8
		public static event Action didFailToLoadMoreAppsEvent;

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x06000084 RID: 132 RVA: 0x00004400 File Offset: 0x00002600
		// (remove) Token: 0x06000085 RID: 133 RVA: 0x00004418 File Offset: 0x00002618
		public static event Action didDismissMoreAppsEvent;

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x06000086 RID: 134 RVA: 0x00004430 File Offset: 0x00002630
		// (remove) Token: 0x06000087 RID: 135 RVA: 0x00004448 File Offset: 0x00002648
		public static event Action didCloseMoreAppsEvent;

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x06000088 RID: 136 RVA: 0x00004460 File Offset: 0x00002660
		// (remove) Token: 0x06000089 RID: 137 RVA: 0x00004478 File Offset: 0x00002678
		public static event Action didClickMoreAppsEvent;

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x0600008A RID: 138 RVA: 0x00004490 File Offset: 0x00002690
		// (remove) Token: 0x0600008B RID: 139 RVA: 0x000044A8 File Offset: 0x000026A8
		public static event Action didCacheMoreAppsEvent;

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x0600008C RID: 140 RVA: 0x000044C0 File Offset: 0x000026C0
		// (remove) Token: 0x0600008D RID: 141 RVA: 0x000044D8 File Offset: 0x000026D8
		public static event Action didShowMoreAppsEvent;

		// Token: 0x0600008E RID: 142 RVA: 0x000044F0 File Offset: 0x000026F0
		private void Awake()
		{
			base.gameObject.name = "ChartBoostManager";
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00004510 File Offset: 0x00002710
		public void didFailToLoadInterstitial(string location)
		{
			if (CBManager.didFailToLoadInterstitialEvent != null)
			{
				CBManager.didFailToLoadInterstitialEvent(location);
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00004528 File Offset: 0x00002728
		public void didDismissInterstitial(string location)
		{
			CBManager.doUnityPause(false);
			if (CBManager.didDismissInterstitialEvent != null)
			{
				CBManager.didDismissInterstitialEvent(location);
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00004548 File Offset: 0x00002748
		public void didClickInterstitial(string location)
		{
			if (CBManager.didClickInterstitialEvent != null)
			{
				CBManager.didClickInterstitialEvent(location);
			}
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00004560 File Offset: 0x00002760
		public void didCloseInterstitial(string location)
		{
			if (CBManager.didCloseInterstitialEvent != null)
			{
				CBManager.didCloseInterstitialEvent(location);
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00004578 File Offset: 0x00002778
		public void didCacheInterstitial(string location)
		{
			if (CBManager.didCacheInterstitialEvent != null)
			{
				CBManager.didCacheInterstitialEvent(location);
			}
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00004590 File Offset: 0x00002790
		public void didShowInterstitial(string location)
		{
			CBManager.doUnityPause(true);
			if (CBManager.didShowInterstitialEvent != null)
			{
				CBManager.didShowInterstitialEvent(location);
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000045B0 File Offset: 0x000027B0
		public void didFailToLoadMoreApps(string empty)
		{
			if (CBManager.didFailToLoadMoreAppsEvent != null)
			{
				CBManager.didFailToLoadMoreAppsEvent();
			}
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000045C8 File Offset: 0x000027C8
		public void didDismissMoreApps(string empty)
		{
			CBManager.doUnityPause(false);
			if (CBManager.didDismissMoreAppsEvent != null)
			{
				CBManager.didDismissMoreAppsEvent();
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x000045E4 File Offset: 0x000027E4
		public void didClickMoreApps(string empty)
		{
			if (CBManager.didClickMoreAppsEvent != null)
			{
				CBManager.didClickMoreAppsEvent();
			}
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000045FC File Offset: 0x000027FC
		public void didCloseMoreApps(string empty)
		{
			if (CBManager.didCloseMoreAppsEvent != null)
			{
				CBManager.didCloseMoreAppsEvent();
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00004614 File Offset: 0x00002814
		public void didCacheMoreApps(string empty)
		{
			if (CBManager.didCacheMoreAppsEvent != null)
			{
				CBManager.didCacheMoreAppsEvent();
			}
		}

		// Token: 0x0600009A RID: 154 RVA: 0x0000462C File Offset: 0x0000282C
		public void didShowMoreApps(string empty)
		{
			CBManager.doUnityPause(true);
			if (CBManager.didShowMoreAppsEvent != null)
			{
				CBManager.didShowMoreAppsEvent();
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00004648 File Offset: 0x00002848
		private static void doUnityPause(bool pause)
		{
			bool flag = true;
			if (pause)
			{
				if (CBManager.isPaused)
				{
					flag = false;
				}
				CBManager.isPaused = true;
				if (flag && !CBBinding.getImpressionsUseActivities())
				{
					CBManager.doCustomPause(pause);
				}
			}
			else
			{
				if (!CBManager.isPaused)
				{
					flag = false;
				}
				CBManager.isPaused = false;
				if (flag && !CBBinding.getImpressionsUseActivities())
				{
					CBManager.doCustomPause(pause);
				}
			}
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000046B4 File Offset: 0x000028B4
		public static bool isImpressionVisible()
		{
			return CBManager.isPaused;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000046BC File Offset: 0x000028BC
		private static void doCustomPause(bool pause)
		{
			if (pause)
			{
				CBManager.lastTimeScale = Time.timeScale;
				Time.timeScale = 0f;
			}
			else
			{
				Time.timeScale = CBManager.lastTimeScale;
			}
		}

		// Token: 0x04000039 RID: 57
		private static bool isPaused;

		// Token: 0x0400003A RID: 58
		private static float lastTimeScale;
	}
}
