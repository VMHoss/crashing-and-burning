using System;
using UnityEngine;

namespace Uniject.Impl
{
	// Token: 0x0200001A RID: 26
	public class UnityPlayerPrefsStorage : IStorage
	{
		// Token: 0x060000DC RID: 220 RVA: 0x00004C54 File Offset: 0x00002E54
		public int GetInt(string key, int defaultValue)
		{
			return PlayerPrefs.GetInt(key, defaultValue);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00004C60 File Offset: 0x00002E60
		public void SetInt(string key, int value)
		{
			PlayerPrefs.SetInt(key, value);
		}
	}
}
