using System;
using UnityEngine;

namespace com.miniclip
{
	// Token: 0x02000069 RID: 105
	public class ClearingBmpData : FlashSwitcherState
	{
		// Token: 0x06000328 RID: 808 RVA: 0x0000B694 File Offset: 0x00009894
		public ClearingBmpData(string id, FlashSwitcher flashSwitcher) : base(id, flashSwitcher)
		{
			Debug.Log("-> ClearBmpDataState - created!");
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0000B6A8 File Offset: 0x000098A8
		public override void Enter()
		{
			Debug.Log("-> ClearBmpDataState::Enter()");
			JSCaller.Call("clearScreenGrabData");
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000B6C0 File Offset: 0x000098C0
		public override void Update()
		{
			this._stateMachine.ChangeState("transmitting_bmp_data");
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0000B6D4 File Offset: 0x000098D4
		public override void Exit()
		{
			Debug.Log("-> ClearBmpDataState::Exit()");
		}
	}
}
