using System;

namespace Uniject
{
	// Token: 0x02000015 RID: 21
	public interface ILogger
	{
		// Token: 0x060000C5 RID: 197
		void Log(string message);

		// Token: 0x060000C6 RID: 198
		void Log(string message, params object[] formatArgs);

		// Token: 0x060000C7 RID: 199
		void LogWarning(string message, params object[] formatArgs);

		// Token: 0x060000C8 RID: 200
		void LogError(string message, params object[] formatArgs);

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x060000C9 RID: 201
		// (set) Token: 0x060000CA RID: 202
		string prefix { get; set; }
	}
}
