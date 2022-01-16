using System;

namespace com.miniclip
{
	// Token: 0x0200005C RID: 92
	public class MessageEventArgs : EventArgs
	{
		// Token: 0x060002FC RID: 764 RVA: 0x0000B298 File Offset: 0x00009498
		public MessageEventArgs(string message)
		{
			this._message = message;
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060002FD RID: 765 RVA: 0x0000B2A8 File Offset: 0x000094A8
		public string Message
		{
			get
			{
				return this._message;
			}
		}

		// Token: 0x0400014B RID: 331
		private string _message;
	}
}
