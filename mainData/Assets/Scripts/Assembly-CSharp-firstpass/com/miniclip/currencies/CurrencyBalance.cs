using System;
using System.Collections.Generic;

namespace com.miniclip.currencies
{
	// Token: 0x02000081 RID: 129
	public class CurrencyBalance
	{
		// Token: 0x060003C6 RID: 966 RVA: 0x0000D5D0 File Offset: 0x0000B7D0
		public CurrencyBalance(IDictionary<string, object> fromJson)
		{
			if (fromJson.ContainsKey("currencyId"))
			{
				this._currencyId = Convert.ToInt32(fromJson["currencyId"]);
			}
			if (fromJson.ContainsKey("balance"))
			{
				this._balance = Convert.ToDecimal(fromJson["balance"]);
			}
			if (fromJson.ContainsKey("currency"))
			{
				IDictionary<string, object> fromJson2 = fromJson["currency"] as IDictionary<string, object>;
				this._currency = new CurrencyCurrency(fromJson2);
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060003C7 RID: 967 RVA: 0x0000D65C File Offset: 0x0000B85C
		public int currencyId
		{
			get
			{
				return this._currencyId;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060003C8 RID: 968 RVA: 0x0000D664 File Offset: 0x0000B864
		public decimal balance
		{
			get
			{
				return this._balance;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060003C9 RID: 969 RVA: 0x0000D66C File Offset: 0x0000B86C
		public CurrencyCurrency currency
		{
			get
			{
				return this._currency;
			}
		}

		// Token: 0x040001DC RID: 476
		private int _currencyId;

		// Token: 0x040001DD RID: 477
		private decimal _balance;

		// Token: 0x040001DE RID: 478
		private CurrencyCurrency _currency;
	}
}
