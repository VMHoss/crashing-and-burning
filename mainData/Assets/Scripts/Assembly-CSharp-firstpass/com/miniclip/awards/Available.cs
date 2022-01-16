using System;
using UnityEngine;

namespace com.miniclip.awards
{
	// Token: 0x0200004B RID: 75
	public class Available : AwardNotificationState
	{
		// Token: 0x060002AA RID: 682 RVA: 0x0000A2F8 File Offset: 0x000084F8
		public Available(string id, AwardNotification award) : base(id, award)
		{
			Debug.Log("-> Available - created!");
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0000A30C File Offset: 0x0000850C
		public override void Enter()
		{
			Debug.Log("-> Available::Enter()");
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0000A318 File Offset: 0x00008518
		public override void Update()
		{
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0000A31C File Offset: 0x0000851C
		public override void Exit()
		{
			Debug.Log("-> Available::Exit()");
		}
	}
}
