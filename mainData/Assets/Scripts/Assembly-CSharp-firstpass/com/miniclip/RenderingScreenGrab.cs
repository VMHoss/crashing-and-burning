using System;
using UnityEngine;

namespace com.miniclip
{
	// Token: 0x0200006C RID: 108
	public class RenderingScreenGrab : FlashSwitcherState
	{
		// Token: 0x06000330 RID: 816 RVA: 0x0000B730 File Offset: 0x00009930
		public RenderingScreenGrab(string id, FlashSwitcher flashSwitcher) : base(id, flashSwitcher)
		{
			Debug.Log("-> RenderingScreenGrab - created!");
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0000B744 File Offset: 0x00009944
		public override void Enter()
		{
			Debug.Log("-> RenderingScreenGrab::Enter()");
			JSCaller.Call("renderScreenGrab");
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0000B75C File Offset: 0x0000995C
		public override void Update()
		{
			this._stateMachine.ChangeState("calling_requested_function");
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0000B770 File Offset: 0x00009970
		public override void Exit()
		{
			Debug.Log("-> RenderingScreenGrab::Exit()");
		}
	}
}
