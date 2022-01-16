using System;
using System.Collections.Generic;

namespace com.miniclip.currencies
{
	// Token: 0x02000089 RID: 137
	public class CurrenciesEventArgs : EventArgs
	{
		// Token: 0x060003F1 RID: 1009 RVA: 0x0000DE7C File Offset: 0x0000C07C
		public CurrenciesEventArgs(Dictionary<string, CurrencyCurrency> currencies)
		{
			this._currencies = currencies;
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x0000DE8C File Offset: 0x0000C08C
		public Dictionary<string, CurrencyCurrency> Currencies
		{
			get
			{
				return this._currencies;
			}
		}

		// Token: 0x040001FE RID: 510
		private Dictionary<string, CurrencyCurrency> _currencies;
	}
}
