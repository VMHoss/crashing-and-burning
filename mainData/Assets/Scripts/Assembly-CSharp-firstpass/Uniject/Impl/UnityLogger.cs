using System;
using UnityEngine;

namespace Uniject.Impl
{
	// Token: 0x02000019 RID: 25
	public class UnityLogger : ILogger
	{
		// Token: 0x060000D4 RID: 212 RVA: 0x00004BD4 File Offset: 0x00002DD4
		public void LogWarning(string message, params object[] formatArgs)
		{
			Debug.LogWarning(string.Format(message, formatArgs));
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00004BE4 File Offset: 0x00002DE4
		// (set) Token: 0x060000D6 RID: 214 RVA: 0x00004BEC File Offset: 0x00002DEC
		public string prefix { get; set; }

		// Token: 0x060000D7 RID: 215 RVA: 0x00004BF8 File Offset: 0x00002DF8
		public void Log(string message)
		{
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00004BFC File Offset: 0x00002DFC
		public void Log(string message, object[] args)
		{
			this.Log(string.Format(message, args));
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00004C0C File Offset: 0x00002E0C
		public void LogError(string message, params object[] formatArgs)
		{
			Debug.LogError(this.formatMessage(string.Format(message, formatArgs)));
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00004C20 File Offset: 0x00002E20
		private string formatMessage(string message)
		{
			if (this.prefix == null)
			{
				return message;
			}
			return string.Format("{0}: {1}", this.prefix, message);
		}
	}
}
