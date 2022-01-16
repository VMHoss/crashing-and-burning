using System;
using System.Collections.Generic;

namespace com.miniclip.currencies
{
	// Token: 0x02000086 RID: 134
	public class CurrencyUserQuantity
	{
		// Token: 0x060003E9 RID: 1001 RVA: 0x0000DD90 File Offset: 0x0000BF90
		public CurrencyUserQuantity(IDictionary<string, object> fromJson)
		{
			if (fromJson.ContainsKey("itemId"))
			{
				this._item_id = Convert.ToInt32(fromJson["itemId"]);
			}
			if (fromJson.ContainsKey("quantity"))
			{
				this._item_balance = Convert.ToUInt32(fromJson["quantity"]);
			}
			if (fromJson.ContainsKey("item") && fromJson["item"] is IDictionary<string, object>)
			{
				IDictionary<string, object> fromJson2 = fromJson["item"] as IDictionary<string, object>;
				this._item = new CurrencyItem(fromJson2);
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060003EA RID: 1002 RVA: 0x0000DE34 File Offset: 0x0000C034
		public int itemId
		{
			get
			{
				return this._item_id;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x0000DE3C File Offset: 0x0000C03C
		public uint quantity
		{
			get
			{
				return this._item_balance;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060003EC RID: 1004 RVA: 0x0000DE44 File Offset: 0x0000C044
		public CurrencyItem item
		{
			get
			{
				return this._item;
			}
		}

		// Token: 0x040001F9 RID: 505
		private int _item_id;

		// Token: 0x040001FA RID: 506
		private uint _item_balance;

		// Token: 0x040001FB RID: 507
		private CurrencyItem _item;
	}
}
