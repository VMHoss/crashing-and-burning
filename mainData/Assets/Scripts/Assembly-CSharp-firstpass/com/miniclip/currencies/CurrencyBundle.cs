using System;
using System.Collections.Generic;

namespace com.miniclip.currencies
{
	// Token: 0x02000082 RID: 130
	public class CurrencyBundle
	{
		// Token: 0x060003CA RID: 970 RVA: 0x0000D674 File Offset: 0x0000B874
		public CurrencyBundle(IDictionary<string, object> fromJson)
		{
			if (fromJson.ContainsKey("bundleId"))
			{
				this._bundleId = Convert.ToInt32(fromJson["bundleId"]);
			}
			if (fromJson.ContainsKey("name"))
			{
				this._name = fromJson["name"].ToString();
			}
			if (fromJson.ContainsKey("currencyId"))
			{
				this._currencyId = Convert.ToInt32(fromJson["currencyId"]);
			}
			if (fromJson.ContainsKey("amount"))
			{
				this._amount = Convert.ToDecimal(fromJson["amount"]);
			}
			if (fromJson.ContainsKey("prices"))
			{
				IDictionary<string, object> dictionary = fromJson["prices"] as IDictionary<string, object>;
				this._prices = new Dictionary<string, CurrencyPrice>();
				foreach (KeyValuePair<string, object> keyValuePair in dictionary)
				{
					IDictionary<string, object> fromJson2 = keyValuePair.Value as IDictionary<string, object>;
					CurrencyPrice value = new CurrencyPrice(fromJson2);
					this._prices.Add(keyValuePair.Key, value);
				}
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060003CB RID: 971 RVA: 0x0000D7CC File Offset: 0x0000B9CC
		public int bundleId
		{
			get
			{
				return this._bundleId;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060003CC RID: 972 RVA: 0x0000D7D4 File Offset: 0x0000B9D4
		public string name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060003CD RID: 973 RVA: 0x0000D7DC File Offset: 0x0000B9DC
		public int currencyId
		{
			get
			{
				return this._currencyId;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060003CE RID: 974 RVA: 0x0000D7E4 File Offset: 0x0000B9E4
		public decimal amount
		{
			get
			{
				return this._amount;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060003CF RID: 975 RVA: 0x0000D7EC File Offset: 0x0000B9EC
		public Dictionary<string, CurrencyPrice> prices
		{
			get
			{
				return this._prices;
			}
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0000D7F4 File Offset: 0x0000B9F4
		public CurrencyPrice GetPriceById(int id)
		{
			if (this._prices.ContainsKey(id.ToString()))
			{
				return this._prices[id.ToString()];
			}
			return null;
		}

		// Token: 0x040001DF RID: 479
		private int _bundleId;

		// Token: 0x040001E0 RID: 480
		private string _name = string.Empty;

		// Token: 0x040001E1 RID: 481
		private int _currencyId;

		// Token: 0x040001E2 RID: 482
		private decimal _amount;

		// Token: 0x040001E3 RID: 483
		private Dictionary<string, CurrencyPrice> _prices;
	}
}
