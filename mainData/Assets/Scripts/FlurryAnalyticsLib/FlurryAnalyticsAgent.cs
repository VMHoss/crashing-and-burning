using System;
using UnityEngine;

// Token: 0x02000002 RID: 2
public class FlurryAnalyticsAgent : MonoBehaviour
{
	// Token: 0x06000003 RID: 3 RVA: 0x00002128 File Offset: 0x00000328
	private void OnApplicationQuit()
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			FlurryAnalyticsAndroid.Instance().onDestroy();
		}
		else if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			FlurryAnalyticsIOS.Instance().onDestroy();
		}
	}

	// Token: 0x06000004 RID: 4 RVA: 0x00002168 File Offset: 0x00000368
	private void OnApplicationPause(bool isPaused)
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			if (!isPaused)
			{
				FlurryAnalyticsAndroid.Instance().onFocusGot();
			}
			else
			{
				FlurryAnalyticsAndroid.Instance().onFocusLost();
			}
		}
		else if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			if (!isPaused)
			{
				FlurryAnalyticsIOS.Instance().onFocusGot();
			}
			else
			{
				FlurryAnalyticsIOS.Instance().onFocusLost();
			}
		}
	}

	// Token: 0x06000005 RID: 5 RVA: 0x000021D0 File Offset: 0x000003D0
	private void Awake()
	{
		if (FlurryAnalyticsAgent._instanceFound)
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		FlurryAnalyticsAgent._instanceFound = true;
		base.gameObject.name = base.GetType().ToString();
		UnityEngine.Object.DontDestroyOnLoad(this);
	}

	// Token: 0x04000001 RID: 1
	public string apiKeyAndroid = string.Empty;

	// Token: 0x04000002 RID: 2
	public string apiKeyIOS = string.Empty;

	// Token: 0x04000003 RID: 3
	public bool logUseHttps = false;

	// Token: 0x04000004 RID: 4
	public bool enableCrashReporting = false;

	// Token: 0x04000005 RID: 5
	public bool enableDebugging = false;

	// Token: 0x04000006 RID: 6
	private static bool _instanceFound = false;
}
