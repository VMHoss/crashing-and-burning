using System;
using System.Collections.Generic;

namespace com.miniclip.currencies
{
	// Token: 0x02000083 RID: 131
	public class CurrencyCurrency
	{
		// Token: 0x060003D1 RID: 977 RVA: 0x0000D82C File Offset: 0x0000BA2C
		public CurrencyCurrency(IDictionary<string, object> fromJson)
		{
			if (fromJson.ContainsKey("id"))
			{
				this._id = Convert.ToInt32(fromJson["id"]);
			}
			if (fromJson.ContainsKey("name"))
			{
				this._name = fromJson["name"].ToString();
			}
			if (fromJson.ContainsKey("code"))
			{
				this._code = fromJson["code"].ToString();
			}
			if (fromJson.ContainsKey("typeId"))
			{
				this._typeId = Convert.ToInt32(fromJson["typeId"]);
			}
			if (fromJson.ContainsKey("js_item"))
			{
				this._js_item = Convert.ToBoolean(fromJson["js_item"]);
			}
			if (fromJson.ContainsKey("js_other"))
			{
				this._js_other = Convert.ToBoolean(fromJson["js_other"]);
			}
			if (fromJson.ContainsKey("js_item"))
			{
				this._js_this = Convert.ToBoolean(fromJson["js_this"]);
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x0000D960 File Offset: 0x0000BB60
		public int id
		{
			get
			{
				return this._id;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060003D3 RID: 979 RVA: 0x0000D968 File Offset: 0x0000BB68
		public string name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060003D4 RID: 980 RVA: 0x0000D970 File Offset: 0x0000BB70
		public string code
		{
			get
			{
				return this._code;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060003D5 RID: 981 RVA: 0x0000D978 File Offset: 0x0000BB78
		public int typeId
		{
			get
			{
				return this._typeId;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060003D6 RID: 982 RVA: 0x0000D980 File Offset: 0x0000BB80
		public bool js_item
		{
			get
			{
				return this._js_item;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060003D7 RID: 983 RVA: 0x0000D988 File Offset: 0x0000BB88
		public bool js_other
		{
			get
			{
				return this._js_other;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060003D8 RID: 984 RVA: 0x0000D990 File Offset: 0x0000BB90
		public bool js_this
		{
			get
			{
				return this._js_this;
			}
		}

		// Token: 0x040001E4 RID: 484
		private int _id;

		// Token: 0x040001E5 RID: 485
		private string _name = string.Empty;

		// Token: 0x040001E6 RID: 486
		private string _code = string.Empty;

		// Token: 0x040001E7 RID: 487
		private int _typeId;

		// Token: 0x040001E8 RID: 488
		private bool _js_item;

		// Token: 0x040001E9 RID: 489
		private bool _js_other;

		// Token: 0x040001EA RID: 490
		private bool _js_this;
	}
}
