using System;

namespace com.miniclip
{
	// Token: 0x02000062 RID: 98
	public class State : IState
	{
		// Token: 0x0600030B RID: 779 RVA: 0x0000B318 File Offset: 0x00009518
		public State(string id, StateMachine stateMachine)
		{
			this._id = id;
			this._stateMachine = stateMachine;
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600030C RID: 780 RVA: 0x0000B33C File Offset: 0x0000953C
		public string Id
		{
			get
			{
				return this._id;
			}
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000B344 File Offset: 0x00009544
		public virtual void Enter()
		{
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000B348 File Offset: 0x00009548
		public virtual void Update()
		{
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000B34C File Offset: 0x0000954C
		public virtual void Exit()
		{
		}

		// Token: 0x04000150 RID: 336
		protected string _id = string.Empty;

		// Token: 0x04000151 RID: 337
		protected StateMachine _stateMachine;
	}
}
