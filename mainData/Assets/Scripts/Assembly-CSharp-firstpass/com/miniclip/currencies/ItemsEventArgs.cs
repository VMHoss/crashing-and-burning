using System;
using System.Collections.Generic;

namespace com.miniclip.currencies
{
	// Token: 0x0200008B RID: 139
	public class ItemsEventArgs : EventArgs
	{
		// Token: 0x060003F6 RID: 1014 RVA: 0x0000DEBC File Offset: 0x0000C0BC
		public ItemsEventArgs(Dictionary<string, CurrencyItem> items)
		{
			this._items = items;
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060003F7 RID: 1015 RVA: 0x0000DECC File Offset: 0x0000C0CC
		public Dictionary<string, CurrencyItem> Items
		{
			get
			{
				return this._items;
			}
		}

		// Token: 0x04000201 RID: 513
		private Dictionary<string, CurrencyItem> _items;
	}
}
