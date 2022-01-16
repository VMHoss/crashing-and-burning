using System;

namespace com.miniclip
{
	// Token: 0x0200005D RID: 93
	public class UndefinedEventArgs : EventArgs
	{
		// Token: 0x060002FE RID: 766 RVA: 0x0000B2B0 File Offset: 0x000094B0
		public UndefinedEventArgs(string noticeID, string json)
		{
			this._noticeID = noticeID;
			this._json = json;
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060002FF RID: 767 RVA: 0x0000B2C8 File Offset: 0x000094C8
		public string NoticeID
		{
			get
			{
				return this._noticeID;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000300 RID: 768 RVA: 0x0000B2D0 File Offset: 0x000094D0
		public string Json
		{
			get
			{
				return this._json;
			}
		}

		// Token: 0x0400014C RID: 332
		private string _noticeID;

		// Token: 0x0400014D RID: 333
		private string _json;
	}
}
