using System;
using System.Collections.Generic;
using MiniJSON;

namespace com.miniclip
{
	// Token: 0x02000059 RID: 89
	public class EmbedVariables
	{
		// Token: 0x060002F2 RID: 754 RVA: 0x0000B124 File Offset: 0x00009324
		public EmbedVariables(string json)
		{
			this._dictionary = (Json.Deserialize(json) as Dictionary<string, object>);
			if (this._dictionary.ContainsKey("mc_sessid"))
			{
				this._sessionId = (this._dictionary["mc_sessid"] as string);
			}
			if (this._dictionary.ContainsKey("mc_geoCode"))
			{
				this._geoCode = (this._dictionary["mc_geoCode"] as string);
			}
			if (this._dictionary.ContainsKey("mc_lang"))
			{
				this._language = (this._dictionary["mc_lang"] as string);
			}
			if (this._dictionary.ContainsKey("mc_webmaster"))
			{
				this._isWebmasterGame = (this._dictionary["mc_webmaster"] as string == "1");
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x0000B240 File Offset: 0x00009440
		public Dictionary<string, object> Dictionary
		{
			get
			{
				return this._dictionary;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x0000B248 File Offset: 0x00009448
		public string SessionId
		{
			get
			{
				return this._sessionId;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x0000B250 File Offset: 0x00009450
		public string GeoCode
		{
			get
			{
				return this._geoCode;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x0000B258 File Offset: 0x00009458
		public string Language
		{
			get
			{
				return this._language;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x0000B260 File Offset: 0x00009460
		public bool IsWebmasterGame
		{
			get
			{
				return this._isWebmasterGame;
			}
		}

		// Token: 0x04000144 RID: 324
		private Dictionary<string, object> _dictionary;

		// Token: 0x04000145 RID: 325
		private string _sessionId = string.Empty;

		// Token: 0x04000146 RID: 326
		private string _geoCode = string.Empty;

		// Token: 0x04000147 RID: 327
		private string _language = string.Empty;

		// Token: 0x04000148 RID: 328
		private bool _isWebmasterGame;
	}
}
