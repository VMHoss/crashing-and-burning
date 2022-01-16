using System;
using UnityEngine;

namespace com.miniclip
{
	// Token: 0x0200006F RID: 111
	public class TransmittingBmpData : FlashSwitcherState
	{
		// Token: 0x0600033A RID: 826 RVA: 0x0000B8D8 File Offset: 0x00009AD8
		public TransmittingBmpData(string id, FlashSwitcher flashSwitcher) : base(id, flashSwitcher)
		{
			Debug.Log("-> TransmittingBmpData - created!");
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0000B8F8 File Offset: 0x00009AF8
		public override void Enter()
		{
			Debug.Log("-> TransmittingBmpData::Enter()");
			this._strPos = 0;
			this._bitmapString = this._flashSwitcher.bitmapString;
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0000B928 File Offset: 0x00009B28
		public override void Update()
		{
			if (this.TransmitScreenGrabChunk())
			{
				this._stateMachine.ChangeState("rendering_screen_grab");
			}
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000B948 File Offset: 0x00009B48
		public override void Exit()
		{
			Debug.Log("-> TransmittingBmpData::Exit()");
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000B954 File Offset: 0x00009B54
		private bool TransmitScreenGrabChunk()
		{
			if (this._strPos < this._bitmapString.Length)
			{
				int num = this._bitmapString.Length - this._strPos;
				if (num > 16000)
				{
					num = 16000;
				}
				string data = this._bitmapString.Substring(this._strPos, num);
				JSCaller.Call("receiveScreenGrabData", data);
				this._strPos += 16000;
				return false;
			}
			return true;
		}

		// Token: 0x04000171 RID: 369
		private const int MAX_CHUNK_SIZE = 16000;

		// Token: 0x04000172 RID: 370
		private int _strPos;

		// Token: 0x04000173 RID: 371
		private string _bitmapString = string.Empty;
	}
}
