using System;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;

namespace com.miniclip.currencies
{
	// Token: 0x02000080 RID: 128
	public class CurrenciesFactory
	{
		// Token: 0x060003BF RID: 959 RVA: 0x0000D318 File Offset: 0x0000B518
		public static Dictionary<string, CurrencyCurrency> BuildCurrencies(string json)
		{
			Dictionary<string, object> search = Json.Deserialize(json) as Dictionary<string, object>;
			return CurrenciesFactory.BuildCurrencies(search);
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x0000D338 File Offset: 0x0000B538
		public static Dictionary<string, CurrencyCurrency> BuildCurrencies(Dictionary<string, object> search)
		{
			Dictionary<string, CurrencyCurrency> dictionary = new Dictionary<string, CurrencyCurrency>();
			foreach (KeyValuePair<string, object> keyValuePair in search)
			{
				IDictionary<string, object> fromJson = keyValuePair.Value as IDictionary<string, object>;
				CurrencyCurrency value = new CurrencyCurrency(fromJson);
				dictionary.Add(keyValuePair.Key, value);
			}
			return dictionary;
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0000D3C0 File Offset: 0x0000B5C0
		public static Dictionary<string, CurrencyItem> BuildItems(string json)
		{
			Dictionary<string, object> search = Json.Deserialize(json) as Dictionary<string, object>;
			return CurrenciesFactory.BuildItems(search);
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0000D3E0 File Offset: 0x0000B5E0
		public static Dictionary<string, CurrencyItem> BuildItems(Dictionary<string, object> search)
		{
			Dictionary<string, CurrencyItem> dictionary = new Dictionary<string, CurrencyItem>();
			foreach (KeyValuePair<string, object> keyValuePair in search)
			{
				IDictionary<string, object> fromJson = keyValuePair.Value as IDictionary<string, object>;
				CurrencyItem value = new CurrencyItem(fromJson);
				dictionary.Add(keyValuePair.Key, value);
			}
			return dictionary;
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x0000D468 File Offset: 0x0000B668
		public static Dictionary<string, CurrencyBundle> BuildBundles(string json)
		{
			Dictionary<string, CurrencyBundle> dictionary = new Dictionary<string, CurrencyBundle>();
			IDictionary dictionary2 = (IDictionary)Json.Deserialize(json);
			foreach (object obj in dictionary2)
			{
				KeyValuePair<string, object> keyValuePair = (KeyValuePair<string, object>)obj;
				IDictionary<string, object> fromJson = keyValuePair.Value as IDictionary<string, object>;
				CurrencyBundle value = new CurrencyBundle(fromJson);
				dictionary.Add(keyValuePair.Key, value);
			}
			return dictionary;
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0000D50C File Offset: 0x0000B70C
		public static Dictionary<string, CurrencyBalance> BuildBalances(string json)
		{
			Dictionary<string, CurrencyBalance> dictionary = new Dictionary<string, CurrencyBalance>();
			IDictionary dictionary2 = (IDictionary)Json.Deserialize(json);
			foreach (object obj in dictionary2)
			{
				KeyValuePair<string, object> keyValuePair = (KeyValuePair<string, object>)obj;
				IDictionary<string, object> fromJson = keyValuePair.Value as IDictionary<string, object>;
				CurrencyBalance value = new CurrencyBalance(fromJson);
				dictionary.Add(keyValuePair.Key, value);
			}
			return dictionary;
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0000D5B0 File Offset: 0x0000B7B0
		public static CurrencyUserQuantity BuildUserQuantity(string json)
		{
			Dictionary<string, object> fromJson = Json.Deserialize(json) as Dictionary<string, object>;
			return new CurrencyUserQuantity(fromJson);
		}
	}
}
