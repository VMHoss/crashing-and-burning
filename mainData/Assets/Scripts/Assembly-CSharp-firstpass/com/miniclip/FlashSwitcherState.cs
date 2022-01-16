using System;

namespace com.miniclip
{
	// Token: 0x0200006A RID: 106
	public class FlashSwitcherState : State
	{
		// Token: 0x0600032C RID: 812 RVA: 0x0000B6E0 File Offset: 0x000098E0
		public FlashSwitcherState(string id, FlashSwitcher flashSwitcher) : base(id, flashSwitcher.StateMachine)
		{
			this._flashSwitcher = flashSwitcher;
		}

		// Token: 0x04000169 RID: 361
		protected FlashSwitcher _flashSwitcher;
	}
}
