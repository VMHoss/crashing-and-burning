using System;

namespace com.miniclip
{
	// Token: 0x02000056 RID: 86
	public class AwardData
	{
		// Token: 0x060002E9 RID: 745 RVA: 0x0000AFD8 File Offset: 0x000091D8
		public AwardData(uint id, string title, string description)
		{
			this._id = id;
			this._title = title;
			this._description = description;
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060002EA RID: 746 RVA: 0x0000AFF8 File Offset: 0x000091F8
		public uint Id
		{
			get
			{
				return this._id;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060002EB RID: 747 RVA: 0x0000B000 File Offset: 0x00009200
		public string Title
		{
			get
			{
				return this._title;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060002EC RID: 748 RVA: 0x0000B008 File Offset: 0x00009208
		public string Description
		{
			get
			{
				return this._description;
			}
		}

		// Token: 0x0400013F RID: 319
		private uint _id;

		// Token: 0x04000140 RID: 320
		private string _title;

		// Token: 0x04000141 RID: 321
		private string _description;
	}
}
