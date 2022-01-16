using System;
using System.Collections.Generic;

namespace com.miniclip.currencies
{
	// Token: 0x02000087 RID: 135
	public class BalancesEventArgs : EventArgs
	{
		// Token: 0x060003ED RID: 1005 RVA: 0x0000DE4C File Offset: 0x0000C04C
		public BalancesEventArgs(Dictionary<string, CurrencyBalance> balances)
		{
			this._balances = balances;
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060003EE RID: 1006 RVA: 0x0000DE5C File Offset: 0x0000C05C
		public Dictionary<string, CurrencyBalance> Balances
		{
			get
			{
				return this._balances;
			}
		}

		// Token: 0x040001FC RID: 508
		private Dictionary<string, CurrencyBalance> _balances;
	}
}
