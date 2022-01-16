using System;
using UnityEngine;

namespace Uniject
{
	// Token: 0x02000018 RID: 24
	public interface IUtil
	{
		// Token: 0x060000CE RID: 206
		T[] getAnyComponentsOfType<T>() where T : class;

		// Token: 0x060000CF RID: 207
		string loadedLevelName();

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x060000D0 RID: 208
		RuntimePlatform Platform { get; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x060000D1 RID: 209
		string persistentDataPath { get; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x060000D2 RID: 210
		DateTime currentTime { get; }
	}
}
