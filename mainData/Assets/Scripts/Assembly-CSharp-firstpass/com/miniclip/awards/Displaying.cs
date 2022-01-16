using System;
using UnityEngine;

namespace com.miniclip.awards
{
	// Token: 0x0200004E RID: 78
	public class Displaying : AwardNotificationState
	{
		// Token: 0x060002B0 RID: 688 RVA: 0x0000A348 File Offset: 0x00008548
		public Displaying(string id, AwardNotification award) : base(id, award)
		{
			Debug.Log("-> Displaying - created!");
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0000A35C File Offset: 0x0000855C
		public override void Enter()
		{
			Debug.Log("-> Displaying::Enter()");
			this._elapsedTime = 0f;
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000A374 File Offset: 0x00008574
		public override void Update()
		{
			this._elapsedTime += Time.deltaTime;
			if (this._elapsedTime > 1.5f)
			{
				this._stateMachine.ChangeState("moving_out");
			}
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000A3AC File Offset: 0x000085AC
		public override void Exit()
		{
			Debug.Log("-> Displaying::Exit()");
			this._elapsedTime = 0f;
		}

		// Token: 0x0400011F RID: 287
		private const float WAIT_TIME = 1.5f;

		// Token: 0x04000120 RID: 288
		private float _elapsedTime;
	}
}
