using System;
using UnityEngine;

namespace com.miniclip
{
	// Token: 0x0200006B RID: 107
	public class Ready : FlashSwitcherState
	{
		// Token: 0x0600032D RID: 813 RVA: 0x0000B6F8 File Offset: 0x000098F8
		public Ready(string id, FlashSwitcher flashSwitcher) : base(id, flashSwitcher)
		{
			Debug.Log("-> ReadyState - created!");
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0000B70C File Offset: 0x0000990C
		public override void Enter()
		{
			Debug.Log("-> ReadyState::Enter()");
			this._flashSwitcher.ChangeUpdatePool(-1);
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0000B724 File Offset: 0x00009924
		public override void Exit()
		{
			Debug.Log("-> ReadyState::Exit()");
		}
	}
}
