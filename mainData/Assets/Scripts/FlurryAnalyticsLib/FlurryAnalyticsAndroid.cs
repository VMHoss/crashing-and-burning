using System;
using UnityEngine;

// Token: 0x02000004 RID: 4
public class FlurryAnalyticsAndroid
{
	// Token: 0x0600000A RID: 10 RVA: 0x00002218 File Offset: 0x00000418
	private FlurryAnalyticsAndroid()
	{
	}

	// Token: 0x0600000C RID: 12 RVA: 0x0000223C File Offset: 0x0000043C
	private void GrabConfigFromAgent()
	{
		if (FlurryAnalyticsAndroid._instance == null)
		{
			return;
		}
		GameObject gameObject = GameObject.Find("FlurryAnalyticsAgent");
		if (gameObject != null)
		{
			FlurryAnalyticsAgent component = gameObject.GetComponent<FlurryAnalyticsAgent>();
			if (component != null)
			{
				if (component.apiKeyAndroid != string.Empty)
				{
					component.apiKeyAndroid = component.apiKeyAndroid.Trim();
				}
				if (component.apiKeyAndroid != string.Empty)
				{
					this.Init(component.apiKeyAndroid, component.logUseHttps, component.enableCrashReporting, component.enableDebugging);
				}
			}
		}
	}

	// Token: 0x0600000D RID: 13 RVA: 0x000022D8 File Offset: 0x000004D8
	public static FlurryAnalyticsAndroid Instance()
	{
		if (FlurryAnalyticsAndroid._instance == null)
		{
			FlurryAnalyticsAndroid._instance = new FlurryAnalyticsAndroid();
		}
		return FlurryAnalyticsAndroid._instance;
	}

	// Token: 0x0600000E RID: 14 RVA: 0x000022F4 File Offset: 0x000004F4
	public void SetNativeHelper(IFlurryAnalyticsNativeHelper helper)
	{
		this._nativeHelper = helper;
		if (Application.platform == RuntimePlatform.Android && this._nativeHelper != null)
		{
			this._nativeHelper.CreateInstance("com.neatplug.u3d.plugins.flurry.n", "i");
			FlurryAnalyticsAndroid._itemDelimiter = this._nativeHelper.Call<string>("z", new object[0]);
			this.GrabConfigFromAgent();
		}
	}

	// Token: 0x0600000F RID: 15 RVA: 0x00002358 File Offset: 0x00000558
	public void Init(string apiKey, bool logUseHttps, bool enableCrashReporting, bool enableDebug)
	{
		if (Application.platform == RuntimePlatform.Android && this._nativeHelper != null && apiKey != null)
		{
			this._nativeHelper.Call("a", new object[]
			{
				apiKey,
				logUseHttps,
				true,
				true,
				enableCrashReporting,
				enableDebug
			});
		}
	}

	// Token: 0x06000010 RID: 16 RVA: 0x000023CC File Offset: 0x000005CC
	public void onFocusGot()
	{
		if (Application.platform == RuntimePlatform.Android && this._nativeHelper != null)
		{
			this._nativeHelper.Call("c", new object[0]);
		}
	}

	// Token: 0x06000011 RID: 17 RVA: 0x000023FC File Offset: 0x000005FC
	public void onFocusLost()
	{
		if (Application.platform == RuntimePlatform.Android && this._nativeHelper != null)
		{
			this._nativeHelper.Call("d", new object[0]);
		}
	}

	// Token: 0x06000012 RID: 18 RVA: 0x0000242C File Offset: 0x0000062C
	public void onDestroy()
	{
		if (Application.platform == RuntimePlatform.Android && this._nativeHelper != null)
		{
			this._nativeHelper.Call("q", new object[0]);
		}
	}

	// Token: 0x06000013 RID: 19 RVA: 0x0000245C File Offset: 0x0000065C
	public void LogEvent(string eventId, string[] keyVals, bool timed)
	{
		if (Application.platform == RuntimePlatform.Android && this._nativeHelper != null)
		{
			if (eventId == null)
			{
				eventId = "";
			}
			if (keyVals == null)
			{
				keyVals = new string[0];
			}
			else
			{
				for (int i = 0; i < keyVals.Length; i++)
				{
					keyVals[i] = keyVals[i].ToString().Replace(":", FlurryAnalyticsAndroid._itemDelimiter);
				}
			}
			this._nativeHelper.Call("e", new object[]
			{
				eventId,
				keyVals,
				timed
			});
		}
	}

	// Token: 0x06000014 RID: 20 RVA: 0x000024F8 File Offset: 0x000006F8
	public void EndTimedEvent(string eventId)
	{
		if (Application.platform == RuntimePlatform.Android && this._nativeHelper != null)
		{
			if (eventId == null)
			{
				eventId = "";
			}
			this._nativeHelper.Call("f", new object[]
			{
				eventId
			});
		}
	}

	// Token: 0x06000015 RID: 21 RVA: 0x00002544 File Offset: 0x00000744
	public void LogError(string errorId, string message, string errorClass)
	{
		if (Application.platform == RuntimePlatform.Android && this._nativeHelper != null)
		{
			if (errorId == null)
			{
				errorId = "";
			}
			if (message == null)
			{
				message = "";
			}
			if (errorClass == null)
			{
				errorClass = "";
			}
			this._nativeHelper.Call("g", new object[]
			{
				errorId,
				message,
				errorClass
			});
		}
	}

	// Token: 0x06000016 RID: 22 RVA: 0x000025B4 File Offset: 0x000007B4
	public void LogPageview()
	{
		if (Application.platform == RuntimePlatform.Android && this._nativeHelper != null)
		{
			this._nativeHelper.Call("h", new object[0]);
		}
	}

	// Token: 0x06000017 RID: 23 RVA: 0x000025E4 File Offset: 0x000007E4
	public void SetUserId(string userId)
	{
		if (Application.platform == RuntimePlatform.Android && this._nativeHelper != null)
		{
			if (userId == null)
			{
				userId = "";
			}
			this._nativeHelper.Call("r", new object[]
			{
				userId
			});
		}
	}

	// Token: 0x06000018 RID: 24 RVA: 0x00002630 File Offset: 0x00000830
	public void SetUserAge(int userAge)
	{
		if (Application.platform == RuntimePlatform.Android && this._nativeHelper != null)
		{
			this._nativeHelper.Call("s", new object[]
			{
				userAge
			});
		}
	}

	// Token: 0x06000019 RID: 25 RVA: 0x00002674 File Offset: 0x00000874
	public void SetUserGender(bool isFemale)
	{
		if (Application.platform == RuntimePlatform.Android && this._nativeHelper != null)
		{
			this._nativeHelper.Call("t", new object[]
			{
				isFemale
			});
		}
	}

	// Token: 0x0600001A RID: 26 RVA: 0x000026B8 File Offset: 0x000008B8
	public void SetUserLocation(double latitude, double longitude)
	{
		if (Application.platform == RuntimePlatform.Android && this._nativeHelper != null)
		{
			this._nativeHelper.Call("u", new object[]
			{
				latitude,
				longitude
			});
		}
	}

	// Token: 0x04000007 RID: 7
	private const string RM_CLASS = "n";

	// Token: 0x04000008 RID: 8
	private const string RM_INSTANCE = "i";

	// Token: 0x04000009 RID: 9
	private const string RM_INIT = "a";

	// Token: 0x0400000A RID: 10
	private const string RM_ON_FOCUS_GOT = "c";

	// Token: 0x0400000B RID: 11
	private const string RM_ON_FOCUS_LOST = "d";

	// Token: 0x0400000C RID: 12
	private const string RM_LOG_EVENT = "e";

	// Token: 0x0400000D RID: 13
	private const string RM_END_TIMED_EVENT = "f";

	// Token: 0x0400000E RID: 14
	private const string RM_LOG_ERROR = "g";

	// Token: 0x0400000F RID: 15
	private const string RM_LOG_PAGE_VIEW = "h";

	// Token: 0x04000010 RID: 16
	private const string RM_ON_DESTROY = "q";

	// Token: 0x04000011 RID: 17
	private const string RM_SET_USER_ID = "r";

	// Token: 0x04000012 RID: 18
	private const string RM_SET_USER_AGE = "s";

	// Token: 0x04000013 RID: 19
	private const string RM_SET_USER_GENDER = "t";

	// Token: 0x04000014 RID: 20
	private const string RM_SET_USER_LOCATION = "u";

	// Token: 0x04000015 RID: 21
	private const string RM_GET_ITEM_DELIMITER = "z";

	// Token: 0x04000016 RID: 22
	private static FlurryAnalyticsAndroid _instance = null;

	// Token: 0x04000017 RID: 23
	private static string _itemDelimiter = string.Empty;

	// Token: 0x04000018 RID: 24
	private IFlurryAnalyticsNativeHelper _nativeHelper = null;
}
