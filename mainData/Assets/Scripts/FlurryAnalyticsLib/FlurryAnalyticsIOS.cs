using System;
using System.Runtime.InteropServices;
using UnityEngine;

// Token: 0x02000005 RID: 5
public class FlurryAnalyticsIOS
{
	// Token: 0x0600001B RID: 27 RVA: 0x00002704 File Offset: 0x00000904
	private FlurryAnalyticsIOS()
	{
	}

	// Token: 0x0600001D RID: 29
	[DllImport("__Internal")]
	private static extern void _em_FlurryAnalytics_Init(string apiKey, bool logUseHttps, bool shouldReportOnAppClose, bool shouldReportOnAppPause, bool enableCrashReporting, bool enableDebugging);

	// Token: 0x0600001E RID: 30
	[DllImport("__Internal")]
	private static extern void _em_FlurryAnalytics_onFocusGot();

	// Token: 0x0600001F RID: 31
	[DllImport("__Internal")]
	private static extern void _em_FlurryAnalytics_onFocusLost();

	// Token: 0x06000020 RID: 32
	[DllImport("__Internal")]
	private static extern void _em_FlurryAnalytics_onDestroy();

	// Token: 0x06000021 RID: 33
	[DllImport("__Internal")]
	private static extern void _em_FlurryAnalytics_LogEvent(string eventId, string keyVals, bool timed);

	// Token: 0x06000022 RID: 34
	[DllImport("__Internal")]
	private static extern void _em_FlurryAnalytics_EndTimedEvent(string eventId);

	// Token: 0x06000023 RID: 35
	[DllImport("__Internal")]
	private static extern void _em_FlurryAnalytics_LogError(string errorId, string message, string errorClass);

	// Token: 0x06000024 RID: 36
	[DllImport("__Internal")]
	private static extern void _em_FlurryAnalytics_LogPageview();

	// Token: 0x06000025 RID: 37
	[DllImport("__Internal")]
	private static extern void _em_FlurryAnalytics_SetUserId(string userId);

	// Token: 0x06000026 RID: 38
	[DllImport("__Internal")]
	private static extern void _em_FlurryAnalytics_SetUserAge(int userAge);

	// Token: 0x06000027 RID: 39
	[DllImport("__Internal")]
	private static extern void _em_FlurryAnalytics_SetUserGender(bool isFemale);

	// Token: 0x06000028 RID: 40
	[DllImport("__Internal")]
	private static extern void _em_FlurryAnalytics_SetUserLocation(double latitude, double longitude);

	// Token: 0x06000029 RID: 41 RVA: 0x00002714 File Offset: 0x00000914
	private void GrabConfigFromAgent()
	{
		GameObject gameObject = GameObject.Find("FlurryAnalyticsAgent");
		if (gameObject != null)
		{
			FlurryAnalyticsAgent component = gameObject.GetComponent<FlurryAnalyticsAgent>();
			if (component != null)
			{
				if (component.apiKeyIOS != string.Empty)
				{
					component.apiKeyIOS = component.apiKeyIOS.Trim();
				}
				if (component.apiKeyIOS != string.Empty)
				{
					this.Init(component.apiKeyIOS, component.logUseHttps, component.enableCrashReporting, component.enableDebugging);
				}
			}
		}
	}

	// Token: 0x0600002A RID: 42 RVA: 0x000027A4 File Offset: 0x000009A4
	public static FlurryAnalyticsIOS Instance()
	{
		if (FlurryAnalyticsIOS._instance == null)
		{
			FlurryAnalyticsIOS._instance = new FlurryAnalyticsIOS();
			FlurryAnalyticsIOS._instance.GrabConfigFromAgent();
		}
		return FlurryAnalyticsIOS._instance;
	}

	// Token: 0x0600002B RID: 43 RVA: 0x000027CC File Offset: 0x000009CC
	public void Init(string apiKey, bool logUseHttps, bool enableCrashReporting, bool enableDebugging)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer && apiKey != null)
		{
			FlurryAnalyticsIOS._em_FlurryAnalytics_Init(apiKey, logUseHttps, true, true, enableCrashReporting, enableDebugging);
		}
	}

	// Token: 0x0600002C RID: 44 RVA: 0x000027EC File Offset: 0x000009EC
	public void onFocusGot()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			FlurryAnalyticsIOS._em_FlurryAnalytics_onFocusGot();
		}
	}

	// Token: 0x0600002D RID: 45 RVA: 0x00002800 File Offset: 0x00000A00
	public void onFocusLost()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			FlurryAnalyticsIOS._em_FlurryAnalytics_onFocusLost();
		}
	}

	// Token: 0x0600002E RID: 46 RVA: 0x00002814 File Offset: 0x00000A14
	public void onDestroy()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			FlurryAnalyticsIOS._em_FlurryAnalytics_onDestroy();
		}
	}

	// Token: 0x0600002F RID: 47 RVA: 0x00002828 File Offset: 0x00000A28
	public void LogEvent(string eventId, string[] keyVals, bool timed)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			string text = string.Empty;
			if (keyVals != null)
			{
				for (int i = 0; i < keyVals.Length; i++)
				{
					text += keyVals[i];
					if (i != keyVals.Length - 1)
					{
						text += ",";
					}
				}
			}
			FlurryAnalyticsIOS._em_FlurryAnalytics_LogEvent(eventId, text, timed);
		}
	}

	// Token: 0x06000030 RID: 48 RVA: 0x0000288C File Offset: 0x00000A8C
	public void EndTimedEvent(string eventId)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			FlurryAnalyticsIOS._em_FlurryAnalytics_EndTimedEvent(eventId);
		}
	}

	// Token: 0x06000031 RID: 49 RVA: 0x000028A0 File Offset: 0x00000AA0
	public void LogError(string errorId, string message, string errorClass)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			FlurryAnalyticsIOS._em_FlurryAnalytics_LogError(errorId, message, errorClass);
		}
	}

	// Token: 0x06000032 RID: 50 RVA: 0x000028B8 File Offset: 0x00000AB8
	public void LogPageview()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			FlurryAnalyticsIOS._em_FlurryAnalytics_LogPageview();
		}
	}

	// Token: 0x06000033 RID: 51 RVA: 0x000028CC File Offset: 0x00000ACC
	public void SetUserId(string userId)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			FlurryAnalyticsIOS._em_FlurryAnalytics_SetUserId(userId);
		}
	}

	// Token: 0x06000034 RID: 52 RVA: 0x000028E0 File Offset: 0x00000AE0
	public void SetUserAge(int userAge)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			FlurryAnalyticsIOS._em_FlurryAnalytics_SetUserAge(userAge);
		}
	}

	// Token: 0x06000035 RID: 53 RVA: 0x000028F4 File Offset: 0x00000AF4
	public void SetUserGender(bool isFemale)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			FlurryAnalyticsIOS._em_FlurryAnalytics_SetUserGender(isFemale);
		}
	}

	// Token: 0x06000036 RID: 54 RVA: 0x00002908 File Offset: 0x00000B08
	public void SetUserLocation(double latitude, double longitude)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			FlurryAnalyticsIOS._em_FlurryAnalytics_SetUserLocation(latitude, longitude);
		}
	}

	// Token: 0x04000019 RID: 25
	private static FlurryAnalyticsIOS _instance = null;
}
