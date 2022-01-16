using System;

namespace com.miniclip
{
	// Token: 0x0200005B RID: 91
	public class EmbedVariablesEventArgs : EventArgs
	{
		// Token: 0x060002FA RID: 762 RVA: 0x0000B280 File Offset: 0x00009480
		public EmbedVariablesEventArgs(EmbedVariables embedVariables)
		{
			this._embedVariables = embedVariables;
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060002FB RID: 763 RVA: 0x0000B290 File Offset: 0x00009490
		public EmbedVariables EmbedVariables
		{
			get
			{
				return this._embedVariables;
			}
		}

		// Token: 0x0400014A RID: 330
		private EmbedVariables _embedVariables;
	}
}
