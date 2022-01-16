using System;
using UnityEngine;

namespace com.miniclip
{
	// Token: 0x02000064 RID: 100
	public class WaitState : State
	{
		// Token: 0x06000318 RID: 792 RVA: 0x0000B46C File Offset: 0x0000966C
		public WaitState(string id, StateMachine stateMachine, float t, string nextStateId) : base(id, stateMachine)
		{
			this._waitTime = t;
			this._nextStateId = nextStateId;
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000B488 File Offset: 0x00009688
		public override void Enter()
		{
			this._timeRemaining = this._waitTime;
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0000B498 File Offset: 0x00009698
		public override void Update()
		{
			this._timeRemaining -= Time.deltaTime;
			if (this._timeRemaining < 0f)
			{
				this._stateMachine.ChangeState(this._nextStateId);
			}
		}

		// Token: 0x04000155 RID: 341
		protected float _waitTime;

		// Token: 0x04000156 RID: 342
		private float _timeRemaining;

		// Token: 0x04000157 RID: 343
		private string _nextStateId;
	}
}
