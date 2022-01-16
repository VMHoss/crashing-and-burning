using System;
using UnityEngine;

namespace com.miniclip.awards
{
	// Token: 0x02000055 RID: 85
	public class MiniclipAwards : AbstractUpdateableService, IAwardsAPI
	{
		// Token: 0x060002DB RID: 731 RVA: 0x0000AD9C File Offset: 0x00008F9C
		public MiniclipAwards()
		{
			this.Init();
		}

		// Token: 0x14000025 RID: 37
		// (add) Token: 0x060002DC RID: 732 RVA: 0x0000ADB8 File Offset: 0x00008FB8
		// (remove) Token: 0x060002DD RID: 733 RVA: 0x0000ADD4 File Offset: 0x00008FD4
		public event EventHandler<AwardDataEventArgs> AwardGiven;

		// Token: 0x14000026 RID: 38
		// (add) Token: 0x060002DE RID: 734 RVA: 0x0000ADF0 File Offset: 0x00008FF0
		// (remove) Token: 0x060002DF RID: 735 RVA: 0x0000AE0C File Offset: 0x0000900C
		public event EventHandler<MessageEventArgs> AwardFailed;

		// Token: 0x14000027 RID: 39
		// (add) Token: 0x060002E0 RID: 736 RVA: 0x0000AE28 File Offset: 0x00009028
		// (remove) Token: 0x060002E1 RID: 737 RVA: 0x0000AE44 File Offset: 0x00009044
		public event EventHandler<MessageEventArgs> AwardError;

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x0000AE60 File Offset: 0x00009060
		internal override AbstractUpdateable Updateable
		{
			get
			{
				return this._awardsViewController;
			}
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0000AE68 File Offset: 0x00009068
		protected void Init()
		{
			this._awardsViewController = new AwardsViewController();
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0000AE78 File Offset: 0x00009078
		public void ShowAward(uint awardId)
		{
			AwardData awardData = new AwardData(awardId, "Miniclip Award " + awardId, "[ Award Description ]");
			this._awardsViewController.AddAwardData(awardData);
			Debug.Log("AwardsService::ShowAward(..) - _awardDataWaiting: " + this._awardsViewController.AwardDataWaitingCount);
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000AECC File Offset: 0x000090CC
		public void GiveAward(uint awardId)
		{
			this._api.GiveAward(awardId);
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000AEDC File Offset: 0x000090DC
		public void HasAward(uint awardId)
		{
			this._api.HasAward(awardId);
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000AEEC File Offset: 0x000090EC
		public void Update()
		{
			this._awardsViewController.Update();
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000AEFC File Offset: 0x000090FC
		internal override void ProcessData(string noticeID, string json)
		{
			switch (noticeID)
			{
			case "award_given":
			{
				AwardData awardData = AwardsFactory.BuildAwardData(json);
				this._awardsViewController.AddAwardData(awardData);
				if (this.AwardGiven != null)
				{
					this.AwardGiven(this, new AwardDataEventArgs(awardData));
				}
				break;
			}
			case "award_failed":
				if (this.AwardFailed != null)
				{
					this.AwardFailed(this, new MessageEventArgs(AwardsFactory.BuildErrorMessage(json)));
				}
				break;
			}
		}

		// Token: 0x04000136 RID: 310
		private AwardsViewController _awardsViewController;

		// Token: 0x04000137 RID: 311
		private AwardsAPI _api = new AwardsAPI();

		// Token: 0x04000138 RID: 312
		private AwardData _awardData;

		// Token: 0x04000139 RID: 313
		private AwardSlot _slot;

		// Token: 0x0400013A RID: 314
		private AwardNotification _notification;
	}
}
