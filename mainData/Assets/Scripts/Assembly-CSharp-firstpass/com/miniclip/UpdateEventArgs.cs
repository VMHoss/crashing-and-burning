using System;

namespace com.miniclip
{
	// Token: 0x0200005E RID: 94
	public class UpdateEventArgs : EventArgs
	{
		// Token: 0x06000301 RID: 769 RVA: 0x0000B2D8 File Offset: 0x000094D8
		public UpdateEventArgs(int requestedUpdatePool)
		{
			this._requestedUpdatePool = requestedUpdatePool;
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000302 RID: 770 RVA: 0x0000B2E8 File Offset: 0x000094E8
		public int RequestedUpdatePool
		{
			get
			{
				return this._requestedUpdatePool;
			}
		}

		// Token: 0x0400014E RID: 334
		private int _requestedUpdatePool;
	}
}
