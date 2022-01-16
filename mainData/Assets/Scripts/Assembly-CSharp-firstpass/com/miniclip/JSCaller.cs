using System;
using UnityEngine;

namespace com.miniclip
{
	// Token: 0x02000070 RID: 112
	public class JSCaller
	{
		// Token: 0x06000340 RID: 832 RVA: 0x0000B9D8 File Offset: 0x00009BD8
		public static void Call(CallArgs callArgs)
		{
			JSCaller.Call(callArgs.functionName, callArgs.data);
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0000B9EC File Offset: 0x00009BEC
		public static void Call(string functionName)
		{
			JSCaller.Call(functionName, null);
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0000B9F8 File Offset: 0x00009BF8
		public static void Call(string functionName, string data)
		{
			object[] args;
			if (data == null)
			{
				args = new object[]
				{
					functionName
				};
			}
			else
			{
				args = new object[]
				{
					functionName,
					data
				};
			}
			Application.ExternalCall("getGameInstance().invokeService", args);
		}
	}
}
