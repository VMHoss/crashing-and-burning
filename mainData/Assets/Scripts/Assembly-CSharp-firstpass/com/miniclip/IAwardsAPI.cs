using System;

namespace com.miniclip
{
	// Token: 0x0200007B RID: 123
	public interface IAwardsAPI
	{
		// Token: 0x0600038B RID: 907
		void ShowAward(uint awardId);

		// Token: 0x0600038C RID: 908
		void GiveAward(uint awardId);

		// Token: 0x0600038D RID: 909
		void HasAward(uint awardId);
	}
}
