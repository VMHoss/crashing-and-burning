using System;
using System.Collections;
using UnityEngine;

namespace Chartboost
{
	// Token: 0x0200000B RID: 11
	public class CBBinding
	{
		// Token: 0x0600004B RID: 75 RVA: 0x00003444 File Offset: 0x00001644
		static CBBinding()
		{
			if (Application.platform != RuntimePlatform.Android)
			{
				return;
			}
			using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.chartboost.sdk.unity.CBPlugin"))
			{
				CBBinding._plugin = androidJavaClass.CallStatic<AndroidJavaObject>("instance", new object[0]);
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000034B0 File Offset: 0x000016B0
		private static bool checkInitialized()
		{
			if (Application.platform != RuntimePlatform.Android)
			{
				return false;
			}
			if (CBBinding.initialized)
			{
				return true;
			}
			Debug.Log("Please call CBBinding.init() before using any other features of this library.");
			return false;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000034D8 File Offset: 0x000016D8
		public static void init()
		{
			if (Application.platform == RuntimePlatform.Android)
			{
				CBBinding._plugin.Call("init", new object[0]);
			}
			CBBinding.initialized = true;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003504 File Offset: 0x00001704
		public static bool getImpressionsUseActivities()
		{
			return !CBBinding.checkInitialized() || CBBinding._plugin.Call<bool>("getImpressionsUseActivities", new object[0]);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003528 File Offset: 0x00001728
		public static bool onBackPressed()
		{
			return CBBinding.checkInitialized() && CBBinding._plugin.Call<bool>("onBackPressed", new object[0]);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x0000354C File Offset: 0x0000174C
		public static bool isImpressionVisible()
		{
			return CBBinding.checkInitialized() && CBManager.isImpressionVisible();
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003560 File Offset: 0x00001760
		public static void cacheInterstitial(string location)
		{
			if (!CBBinding.checkInitialized())
			{
				return;
			}
			if (location == null)
			{
				location = string.Empty;
			}
			CBBinding._plugin.Call("cacheInterstitial", new object[]
			{
				location
			});
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003594 File Offset: 0x00001794
		public static bool hasCachedInterstitial(string location)
		{
			if (!CBBinding.checkInitialized())
			{
				return false;
			}
			if (location == null)
			{
				location = string.Empty;
			}
			if (location == null)
			{
				location = string.Empty;
			}
			return CBBinding._plugin.Call<bool>("hasCachedInterstitial", new object[]
			{
				location
			});
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000035E0 File Offset: 0x000017E0
		public static void showInterstitial(string location)
		{
			if (!CBBinding.checkInitialized())
			{
				return;
			}
			if (location == null)
			{
				location = string.Empty;
			}
			CBBinding._plugin.Call("showInterstitial", new object[]
			{
				location
			});
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003614 File Offset: 0x00001814
		public static void cacheMoreApps()
		{
			if (!CBBinding.checkInitialized())
			{
				return;
			}
			CBBinding._plugin.Call("cacheMoreApps", new object[0]);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003644 File Offset: 0x00001844
		public static bool hasCachedMoreApps()
		{
			return CBBinding.checkInitialized() && CBBinding._plugin.Call<bool>("hasCachedMoreApps", new object[0]);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003668 File Offset: 0x00001868
		public static void showMoreApps()
		{
			if (!CBBinding.checkInitialized())
			{
				return;
			}
			CBBinding._plugin.Call("showMoreApps", new object[0]);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003698 File Offset: 0x00001898
		public static void forceOrientation(ScreenOrientation orient)
		{
			if (!CBBinding.checkInitialized())
			{
				return;
			}
			CBBinding._plugin.Call("forceOrientation", new object[]
			{
				orient.ToString()
			});
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000036D4 File Offset: 0x000018D4
		public static void trackEvent(string eventIdentifier)
		{
			if (!CBBinding.checkInitialized())
			{
				return;
			}
			CBBinding._plugin.Call("trackEvent", new object[]
			{
				eventIdentifier
			});
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003708 File Offset: 0x00001908
		public static void trackEventWithMetadata(string eventIdentifier, Hashtable metadata)
		{
			if (!CBBinding.checkInitialized())
			{
				return;
			}
			CBBinding._plugin.Call("trackEventWithMetadata", new object[]
			{
				eventIdentifier,
				CBJSON.Serialize(metadata)
			});
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003738 File Offset: 0x00001938
		public static void trackEventWithValue(string eventIdentifier, float val)
		{
			if (!CBBinding.checkInitialized())
			{
				return;
			}
			CBBinding._plugin.Call("trackEventWithValue", new object[]
			{
				eventIdentifier,
				val
			});
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003768 File Offset: 0x00001968
		public static void trackEventWithValueAndMetadata(string eventIdentifier, float val, Hashtable metadata)
		{
			if (!CBBinding.checkInitialized())
			{
				return;
			}
			CBBinding._plugin.Call("trackEventWithValueAndMetadata", new object[]
			{
				eventIdentifier,
				val,
				CBJSON.Serialize(metadata)
			});
		}

		// Token: 0x04000026 RID: 38
		private static AndroidJavaObject _plugin;

		// Token: 0x04000027 RID: 39
		private static bool initialized;
	}
}
