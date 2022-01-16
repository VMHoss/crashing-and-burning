using System;

namespace com.miniclip
{
	// Token: 0x0200005A RID: 90
	public class AwardDataEventArgs : EventArgs
	{
		// Token: 0x060002F8 RID: 760 RVA: 0x0000B268 File Offset: 0x00009468
		public AwardDataEventArgs(AwardData awardData)
		{
			this._awardData = awardData;
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x0000B278 File Offset: 0x00009478
		public AwardData AwardData
		{
			get
			{
				return this._awardData;
			}
		}

		// Token: 0x04000149 RID: 329
		private AwardData _awardData;
	}
}
