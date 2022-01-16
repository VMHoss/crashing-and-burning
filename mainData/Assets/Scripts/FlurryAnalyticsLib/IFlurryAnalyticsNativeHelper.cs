using System;

// Token: 0x02000003 RID: 3
public interface IFlurryAnalyticsNativeHelper
{
	// Token: 0x06000006 RID: 6
	void CreateInstance(string className, string instanceMethod);

	// Token: 0x06000007 RID: 7
	void Call(string methodName, params object[] args);

	// Token: 0x06000008 RID: 8
	void Call(string methodName, string signature, object arg);

	// Token: 0x06000009 RID: 9
	ReturnType Call<ReturnType>(string methodName, params object[] args);
}
