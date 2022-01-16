using System;
using UnityEngine;

namespace com.miniclip.awards
{
	// Token: 0x02000050 RID: 80
	public class MovingIn : AwardNotificationState
	{
		// Token: 0x060002B8 RID: 696 RVA: 0x0000A474 File Offset: 0x00008674
		public MovingIn(string id, AwardNotification award) : base(id, award)
		{
			Debug.Log("-> MovingIn - created!");
			this._speed = 10f;
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000A494 File Offset: 0x00008694
		public override void Enter()
		{
			Debug.Log("-> MovingIn::Enter()");
			this._yPos = this._award.AllocatedSlot.Position.y;
			this._award.transform.Translate(0f, 0f, 1f);
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000A4E8 File Offset: 0x000086E8
		public override void Update()
		{
			this._award.transform.Translate(0f, this._speed * Time.deltaTime, 0f);
			if (this._award.transform.position.y < this._yPos)
			{
				return;
			}
			this._stateMachine.ChangeState("showing");
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000A550 File Offset: 0x00008750
		public override void Exit()
		{
			Debug.Log("-> MovingIn::Exit()");
			this._award.transform.Translate(0f, -(this._award.transform.position.y - this._yPos), -1f);
		}

		// Token: 0x04000123 RID: 291
		private float _yPos;

		// Token: 0x04000124 RID: 292
		private float _speed;
	}
}
