using System;
using UnityEngine;

// Token: 0x02000013 RID: 19
public class FlurryAnalytics
{
	// Token: 0x060000B2 RID: 178 RVA: 0x00004A48 File Offset: 0x00002C48
	private FlurryAnalytics()
	{
		FlurryAnalyticsAndroid.Instance().SetNativeHelper(new FlurryAnalytics.FlurryAnalyticsNativeHelper());
	}

	// Token: 0x060000B4 RID: 180 RVA: 0x00004A64 File Offset: 0x00002C64
	public static FlurryAnalytics Instance()
	{
		if (FlurryAnalytics._instance == null)
		{
			FlurryAnalytics._instance = new FlurryAnalytics();
		}
		return FlurryAnalytics._instance;
	}

	// Token: 0x060000B5 RID: 181 RVA: 0x00004A80 File Offset: 0x00002C80
	public void Init(string apiKey, bool logUseHttps, bool enableCrashReporting, bool enableDebugging)
	{
		FlurryAnalyticsAndroid.Instance().Init(apiKey, logUseHttps, enableCrashReporting, enableDebugging);
	}

	// Token: 0x060000B6 RID: 182 RVA: 0x00004A94 File Offset: 0x00002C94
	public void LogEvent(string eventId)
	{
		FlurryAnalyticsAndroid.Instance().LogEvent(eventId, new string[0], false);
	}

	// Token: 0x060000B7 RID: 183 RVA: 0x00004AA8 File Offset: 0x00002CA8
	public void LogTimedEvent(string eventId)
	{
		FlurryAnalyticsAndroid.Instance().LogEvent(eventId, new string[0], true);
	}

	// Token: 0x060000B8 RID: 184 RVA: 0x00004ABC File Offset: 0x00002CBC
	public void LogEvent(string eventId, string[] keyVals, bool timed)
	{
		FlurryAnalyticsAndroid.Instance().LogEvent(eventId, keyVals, timed);
	}

	// Token: 0x060000B9 RID: 185 RVA: 0x00004ACC File Offset: 0x00002CCC
	public void EndTimedEvent(string eventId)
	{
		FlurryAnalyticsAndroid.Instance().EndTimedEvent(eventId);
	}

	// Token: 0x060000BA RID: 186 RVA: 0x00004ADC File Offset: 0x00002CDC
	public void LogError(string errorId, string message, string errorClass)
	{
		FlurryAnalyticsAndroid.Instance().LogError(errorId, message, errorClass);
	}

	// Token: 0x060000BB RID: 187 RVA: 0x00004AEC File Offset: 0x00002CEC
	public void LogPageview()
	{
		FlurryAnalyticsAndroid.Instance().LogPageview();
	}

	// Token: 0x060000BC RID: 188 RVA: 0x00004AF8 File Offset: 0x00002CF8
	public void SetUserId(string userId)
	{
		FlurryAnalyticsAndroid.Instance().SetUserId(userId);
	}

	// Token: 0x060000BD RID: 189 RVA: 0x00004B08 File Offset: 0x00002D08
	public void SetUserAge(int userAge)
	{
		FlurryAnalyticsAndroid.Instance().SetUserAge(userAge);
	}

	// Token: 0x060000BE RID: 190 RVA: 0x00004B18 File Offset: 0x00002D18
	public void SetUserGender(bool isFemale)
	{
		FlurryAnalyticsAndroid.Instance().SetUserGender(isFemale);
	}

	// Token: 0x060000BF RID: 191 RVA: 0x00004B28 File Offset: 0x00002D28
	public void SetUserLocation(double latitude, double longitude)
	{
		FlurryAnalyticsAndroid.Instance().SetUserLocation(latitude, longitude);
	}

	// Token: 0x04000047 RID: 71
	private static FlurryAnalytics _instance;

	// Token: 0x02000014 RID: 20
	private class FlurryAnalyticsNativeHelper : IFlurryAnalyticsNativeHelper
	{
		// Token: 0x060000C1 RID: 193 RVA: 0x00004B40 File Offset: 0x00002D40
		public void CreateInstance(string className, string instanceMethod)
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass(className);
			this._plugin = androidJavaClass.CallStatic<AndroidJavaObject>(instanceMethod, new object[0]);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00004B68 File Offset: 0x00002D68
		public void Call(string methodName, params object[] args)
		{
			this._plugin.Call(methodName, args);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00004B78 File Offset: 0x00002D78
		public void Call(string methodName, string signature, object arg)
		{
			IntPtr methodID = AndroidJNI.GetMethodID(this._plugin.GetRawClass(), methodName, signature);
			AndroidJNI.CallObjectMethod(this._plugin.GetRawObject(), methodID, AndroidJNIHelper.CreateJNIArgArray(new object[]
			{
				arg
			}));
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00004BBC File Offset: 0x00002DBC
		public ReturnType Call<ReturnType>(string methodName, params object[] args)
		{
			return this._plugin.Call<ReturnType>(methodName, args);
		}

		// Token: 0x04000048 RID: 72
		private AndroidJavaObject _plugin;
	}
}
