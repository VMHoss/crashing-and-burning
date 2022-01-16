using System;

namespace Uniject
{
	// Token: 0x02000017 RID: 23
	public interface IStorage
	{
		// Token: 0x060000CC RID: 204
		int GetInt(string key, int defaultValue);

		// Token: 0x060000CD RID: 205
		void SetInt(string key, int value);
	}
}
