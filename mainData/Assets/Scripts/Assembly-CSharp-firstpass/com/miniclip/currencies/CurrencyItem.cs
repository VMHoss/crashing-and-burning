using System;
using System.Collections.Generic;

namespace com.miniclip.currencies
{
	// Token: 0x02000084 RID: 132
	public class CurrencyItem
	{
		// Token: 0x060003D9 RID: 985 RVA: 0x0000D998 File Offset: 0x0000BB98
		public CurrencyItem(IDictionary<string, object> fromJson)
		{
			if (fromJson.ContainsKey("id"))
			{
				this._id = Convert.ToInt32(fromJson["id"]);
			}
			if (fromJson.ContainsKey("name"))
			{
				this._name = fromJson["name"].ToString();
			}
			if (fromJson.ContainsKey("description"))
			{
				this._description = fromJson["description"].ToString();
			}
			if (fromJson.ContainsKey("item_code"))
			{
				this._item_code = fromJson["item_code"].ToString();
			}
			if (fromJson.ContainsKey("category"))
			{
				this._category = fromJson["category"].ToString();
			}
			if (fromJson.ContainsKey("type"))
			{
				this._type = fromJson["type"].ToString();
			}
			if (fromJson.ContainsKey("display_order"))
			{
				this._display_order = Convert.ToInt32(fromJson["display_order"]);
			}
			if (fromJson.ContainsKey("quantity"))
			{
				this._quantity = Convert.ToUInt32(fromJson["quantity"]);
			}
			if (fromJson.ContainsKey("max_allowed"))
			{
				this._max_allowed = Convert.ToUInt32(fromJson["max_allowed"]);
			}
			if (fromJson.ContainsKey("contains"))
			{
				IDictionary<string, object> dictionary = fromJson["contains"] as IDictionary<string, object>;
				if (dictionary != null && dictionary.Count > 0)
				{
					this._contains = new Dictionary<string, CurrencyItem>();
					foreach (KeyValuePair<string, object> keyValuePair in dictionary)
					{
						IDictionary<string, object> fromJson2 = keyValuePair.Value as IDictionary<string, object>;
						CurrencyItem value = new CurrencyItem(fromJson2);
						this._contains.Add(keyValuePair.Key, value);
					}
				}
			}
			if (fromJson.ContainsKey("prices"))
			{
				IDictionary<string, object> dictionary2 = fromJson["prices"] as IDictionary<string, object>;
				this._prices = new Dictionary<string, CurrencyPrice>();
				foreach (KeyValuePair<string, object> keyValuePair2 in dictionary2)
				{
					IDictionary<string, object> fromJson3 = keyValuePair2.Value as IDictionary<string, object>;
					CurrencyPrice value2 = new CurrencyPrice(fromJson3);
					this._prices.Add(keyValuePair2.Key, value2);
				}
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060003DA RID: 986 RVA: 0x0000DC90 File Offset: 0x0000BE90
		public int id
		{
			get
			{
				return this._id;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060003DB RID: 987 RVA: 0x0000DC98 File Offset: 0x0000BE98
		public string name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060003DC RID: 988 RVA: 0x0000DCA0 File Offset: 0x0000BEA0
		public string description
		{
			get
			{
				return this._description;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060003DD RID: 989 RVA: 0x0000DCA8 File Offset: 0x0000BEA8
		public string itemCode
		{
			get
			{
				return this._item_code;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060003DE RID: 990 RVA: 0x0000DCB0 File Offset: 0x0000BEB0
		public string category
		{
			get
			{
				return this._category;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060003DF RID: 991 RVA: 0x0000DCB8 File Offset: 0x0000BEB8
		public string type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060003E0 RID: 992 RVA: 0x0000DCC0 File Offset: 0x0000BEC0
		public int displayOrder
		{
			get
			{
				return this._display_order;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x0000DCC8 File Offset: 0x0000BEC8
		public uint quantity
		{
			get
			{
				return this._quantity;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060003E2 RID: 994 RVA: 0x0000DCD0 File Offset: 0x0000BED0
		public uint maxAllowed
		{
			get
			{
				return this._max_allowed;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060003E3 RID: 995 RVA: 0x0000DCD8 File Offset: 0x0000BED8
		public Dictionary<string, CurrencyItem> contains
		{
			get
			{
				return this._contains;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060003E4 RID: 996 RVA: 0x0000DCE0 File Offset: 0x0000BEE0
		public Dictionary<string, CurrencyPrice> prices
		{
			get
			{
				return this._prices;
			}
		}

		// Token: 0x040001EB RID: 491
		private int _id;

		// Token: 0x040001EC RID: 492
		private string _name = string.Empty;

		// Token: 0x040001ED RID: 493
		private string _description = string.Empty;

		// Token: 0x040001EE RID: 494
		private string _item_code = string.Empty;

		// Token: 0x040001EF RID: 495
		private string _category = string.Empty;

		// Token: 0x040001F0 RID: 496
		private string _type = string.Empty;

		// Token: 0x040001F1 RID: 497
		private int _display_order;

		// Token: 0x040001F2 RID: 498
		private uint _quantity;

		// Token: 0x040001F3 RID: 499
		private uint _max_allowed;

		// Token: 0x040001F4 RID: 500
		private Dictionary<string, CurrencyItem> _contains;

		// Token: 0x040001F5 RID: 501
		private Dictionary<string, CurrencyPrice> _prices;
	}
}
