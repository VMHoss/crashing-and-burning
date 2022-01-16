using System;
using UnityEngine;

namespace com.miniclip.awards
{
	// Token: 0x02000053 RID: 83
	public class AwardSlot
	{
		// Token: 0x060002C4 RID: 708 RVA: 0x0000A718 File Offset: 0x00008918
		public AwardSlot(Vector3 position)
		{
			this._position = position;
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x0000A728 File Offset: 0x00008928
		public bool IsAvailable
		{
			get
			{
				return this._usedBy == null;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x0000A738 File Offset: 0x00008938
		public Vector3 Position
		{
			get
			{
				return this._position;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x0000A740 File Offset: 0x00008940
		// (set) Token: 0x060002C8 RID: 712 RVA: 0x0000A748 File Offset: 0x00008948
		public AwardNotification UsedBy
		{
			get
			{
				return this._usedBy;
			}
			set
			{
				this._usedBy = value;
			}
		}

		// Token: 0x04000127 RID: 295
		private Vector3 _position;

		// Token: 0x04000128 RID: 296
		private AwardNotification _usedBy;
	}
}
