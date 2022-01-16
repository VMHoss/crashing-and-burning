using System;
using UnityEngine;

namespace com.miniclip
{
	// Token: 0x02000068 RID: 104
	public class CallingRequestedFunction : FlashSwitcherState
	{
		// Token: 0x06000324 RID: 804 RVA: 0x0000B648 File Offset: 0x00009848
		public CallingRequestedFunction(string id, FlashSwitcher flashSwitcher) : base(id, flashSwitcher)
		{
			Debug.Log("-> CallingRequestedFunction - created!");
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0000B65C File Offset: 0x0000985C
		public override void Enter()
		{
			Debug.Log("-> CallingRequestedFunction::Enter()");
			this._flashSwitcher.CallRequestedFunction();
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0000B674 File Offset: 0x00009874
		public override void Update()
		{
			this._stateMachine.ChangeState("ready");
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0000B688 File Offset: 0x00009888
		public override void Exit()
		{
			Debug.Log("-> CallingRequestedFunction::Exit()");
		}
	}
}
