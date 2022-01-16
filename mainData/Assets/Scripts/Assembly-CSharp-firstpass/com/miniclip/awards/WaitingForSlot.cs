using System;
using UnityEngine;

namespace com.miniclip.awards
{
	// Token: 0x02000052 RID: 82
	public class WaitingForSlot : AwardNotificationState
	{
		// Token: 0x060002C0 RID: 704 RVA: 0x0000A6DC File Offset: 0x000088DC
		public WaitingForSlot(string id, AwardNotification award) : base(id, award)
		{
			Debug.Log("-> WaitingForSlot - created!");
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000A6F0 File Offset: 0x000088F0
		public override void Enter()
		{
			Debug.Log("-> WaitingForSlot::Enter()");
			this._award.RequestAwardSlot();
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000A708 File Offset: 0x00008908
		public override void Update()
		{
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000A70C File Offset: 0x0000890C
		public override void Exit()
		{
			Debug.Log("-> WaitingForSlot::Exit()");
		}
	}
}
