using System;
using System.Collections.Generic;

namespace com.miniclip.currencies
{
	// Token: 0x0200008A RID: 138
	public class CurrenciesReadyEventArgs : EventArgs
	{
		// Token: 0x060003F3 RID: 1011 RVA: 0x0000DE94 File Offset: 0x0000C094
		public CurrenciesReadyEventArgs(Dictionary<string, CurrencyCurrency> currencies, Dictionary<string, CurrencyItem> items)
		{
			this._currencies = currencies;
			this._items = items;
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x0000DEAC File Offset: 0x0000C0AC
		public Dictionary<string, CurrencyCurrency> Currencies
		{
			get
			{
				return this._currencies;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060003F5 RID: 1013 RVA: 0x0000DEB4 File Offset: 0x0000C0B4
		public Dictionary<string, CurrencyItem> Items
		{
			get
			{
				return this._items;
			}
		}

		// Token: 0x040001FF RID: 511
		private Dictionary<string, CurrencyCurrency> _currencies;

		// Token: 0x04000200 RID: 512
		private Dictionary<string, CurrencyItem> _items;
	}
}
