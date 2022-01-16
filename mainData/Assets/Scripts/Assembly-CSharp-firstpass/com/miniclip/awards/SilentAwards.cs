using System;
using UnityEngine;

namespace com.miniclip.awards
{
	// Token: 0x0200007D RID: 125
	public class SilentAwards : AbstractService, IAwardsAPI
	{
		// Token: 0x14000031 RID: 49
		// (add) Token: 0x060003A4 RID: 932 RVA: 0x0000CE04 File Offset: 0x0000B004
		// (remove) Token: 0x060003A5 RID: 933 RVA: 0x0000CE20 File Offset: 0x0000B020
		public event EventHandler<AwardDataEventArgs> AwardGiven;

		// Token: 0x14000032 RID: 50
		// (add) Token: 0x060003A6 RID: 934 RVA: 0x0000CE3C File Offset: 0x0000B03C
		// (remove) Token: 0x060003A7 RID: 935 RVA: 0x0000CE58 File Offset: 0x0000B058
		public event EventHandler<MessageEventArgs> AwardFailed;

		// Token: 0x14000033 RID: 51
		// (add) Token: 0x060003A8 RID: 936 RVA: 0x0000CE74 File Offset: 0x0000B074
		// (remove) Token: 0x060003A9 RID: 937 RVA: 0x0000CE90 File Offset: 0x0000B090
		public event EventHandler<MessageEventArgs> AwardError;

		// Token: 0x060003AA RID: 938 RVA: 0x0000CEAC File Offset: 0x0000B0AC
		public void ShowAward(uint awardId)
		{
			Debug.Log("-> AwardsService::ShowAward() - not implemented in this basic service!");
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0000CEB8 File Offset: 0x0000B0B8
		public void GiveAward(uint awardId)
		{
			this._api.GiveAward(awardId);
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0000CEC8 File Offset: 0x0000B0C8
		public void HasAward(uint awardId)
		{
			this._api.HasAward(awardId);
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0000CED8 File Offset: 0x0000B0D8
		internal override void ProcessData(string noticeID, string json)
		{
			switch (noticeID)
			{
			case "award_given":
				if (this.AwardGiven != null)
				{
					this.AwardGiven(this, new AwardDataEventArgs(AwardsFactory.BuildAwardData(json)));
				}
				break;
			case "award_failed":
				if (this.AwardFailed != null)
				{
					this.AwardFailed(this, new MessageEventArgs(AwardsFactory.BuildErrorMessage(json)));
				}
				break;
			case "award_service_error":
				if (this.AwardError != null)
				{
					this.AwardError(this, new MessageEventArgs(AwardsFactory.BuildErrorMessage(json)));
				}
				break;
			}
		}

		// Token: 0x040001C6 RID: 454
		private AwardsAPI _api = new AwardsAPI();
	}
}
