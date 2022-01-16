using System;
using System.Collections.Generic;

namespace com.miniclip.currencies
{
	// Token: 0x02000085 RID: 133
	public class CurrencyPrice
	{
		// Token: 0x060003E5 RID: 997 RVA: 0x0000DCE8 File Offset: 0x0000BEE8
		public CurrencyPrice(IDictionary<string, object> fromJson)
		{
			if (fromJson.ContainsKey("currencyId"))
			{
				this._currencyId = Convert.ToInt32(fromJson["currencyId"]);
			}
			if (fromJson.ContainsKey("display"))
			{
				this._display = fromJson["display"].ToString();
			}
			if (fromJson.ContainsKey("amount"))
			{
				this._amount = Convert.ToDecimal(fromJson["amount"]);
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060003E6 RID: 998 RVA: 0x0000DD78 File Offset: 0x0000BF78
		public int currencyId
		{
			get
			{
				return this._currencyId;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x0000DD80 File Offset: 0x0000BF80
		public decimal amount
		{
			get
			{
				return this._amount;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060003E8 RID: 1000 RVA: 0x0000DD88 File Offset: 0x0000BF88
		public string display
		{
			get
			{
				return this._display;
			}
		}

		// Token: 0x040001F6 RID: 502
		private int _currencyId;

		// Token: 0x040001F7 RID: 503
		private decimal _amount;

		// Token: 0x040001F8 RID: 504
		private string _display = string.Empty;
	}
}
