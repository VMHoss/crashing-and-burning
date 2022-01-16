using System;
using UnityEngine;

namespace com.miniclip
{
	// Token: 0x0200007A RID: 122
	public class AwardsAPI
	{
		// Token: 0x06000389 RID: 905 RVA: 0x0000CD70 File Offset: 0x0000AF70
		public void GiveAward(uint awardId)
		{
			if (awardId < 1U)
			{
				Debug.Log("awardId can't be smaller than one!");
				return;
			}
			string data = "{ \"award_id\" : " + awardId + " }";
			JSCaller.Call("awards_GiveAward", data);
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0000CDB0 File Offset: 0x0000AFB0
		public void HasAward(uint awardId)
		{
			if (awardId < 1U)
			{
				Debug.Log("awardId can't be smaller than one!");
				return;
			}
			string data = "{ \"award_id\" : " + awardId + " }";
			JSCaller.Call("awards_HasAward", data);
		}

		// Token: 0x040001C4 RID: 452
		public const string AWARDS_GIVE_AWARD = "awards_GiveAward";

		// Token: 0x040001C5 RID: 453
		public const string AWARDS_HAS_AWARD = "awards_HasAward";
	}
}
