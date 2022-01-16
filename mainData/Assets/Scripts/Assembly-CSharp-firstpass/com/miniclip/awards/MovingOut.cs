using System;
using UnityEngine;

namespace com.miniclip.awards
{
	// Token: 0x02000051 RID: 81
	public class MovingOut : AwardNotificationState
	{
		// Token: 0x060002BC RID: 700 RVA: 0x0000A5A4 File Offset: 0x000087A4
		public MovingOut(string id, AwardNotification award) : base(id, award)
		{
			Debug.Log("-> MovingOut - created!");
			this._speed = 10f;
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000A5C4 File Offset: 0x000087C4
		public override void Enter()
		{
			Debug.Log("-> MovingOut::Enter()");
			this._award.VacantedSlot();
			this._yPos = AwardNotification.offscreen.y;
			this._award.transform.Translate(0f, 0f, 2f);
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000A618 File Offset: 0x00008818
		public override void Update()
		{
			Debug.Log("-> MovingIn::Update()");
			this._award.transform.Translate(0f, -(this._speed * Time.deltaTime), 0f);
			if (this._award.transform.position.y > this._yPos)
			{
				return;
			}
			this._stateMachine.ChangeState("available");
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000A68C File Offset: 0x0000888C
		public override void Exit()
		{
			Debug.Log("-> MovingOut::Exit()");
			this._award.transform.Translate(0f, this._yPos - this._award.transform.position.y, -2f);
		}

		// Token: 0x04000125 RID: 293
		private float _yPos;

		// Token: 0x04000126 RID: 294
		private float _speed;
	}
}
