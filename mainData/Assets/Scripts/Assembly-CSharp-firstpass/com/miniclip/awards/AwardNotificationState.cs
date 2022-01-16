using System;

namespace com.miniclip.awards
{
	// Token: 0x0200004C RID: 76
	public class AwardNotificationState : State
	{
		// Token: 0x060002AE RID: 686 RVA: 0x0000A328 File Offset: 0x00008528
		public AwardNotificationState(string id, AwardNotification award) : base(id, award.StateMachine)
		{
			this._award = award;
		}

		// Token: 0x04000118 RID: 280
		protected AwardNotification _award;
	}
}
