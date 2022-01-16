using System;
using System.Collections.Generic;
using MiniJSON;

namespace com.miniclip.currencies
{
	// Token: 0x0200008D RID: 141
	public class MiniclipCurrencies : AbstractService, ICurrenciesAPI
	{
		// Token: 0x14000034 RID: 52
		// (add) Token: 0x060003FB RID: 1019 RVA: 0x0000DEF4 File Offset: 0x0000C0F4
		// (remove) Token: 0x060003FC RID: 1020 RVA: 0x0000DF10 File Offset: 0x0000C110
		public event EventHandler<CurrenciesReadyEventArgs> Ready;

		// Token: 0x14000035 RID: 53
		// (add) Token: 0x060003FD RID: 1021 RVA: 0x0000DF2C File Offset: 0x0000C12C
		// (remove) Token: 0x060003FE RID: 1022 RVA: 0x0000DF48 File Offset: 0x0000C148
		public event EventHandler<CurrenciesEventArgs> AvailableCurrenciesReceived;

		// Token: 0x14000036 RID: 54
		// (add) Token: 0x060003FF RID: 1023 RVA: 0x0000DF64 File Offset: 0x0000C164
		// (remove) Token: 0x06000400 RID: 1024 RVA: 0x0000DF80 File Offset: 0x0000C180
		public event EventHandler<BundlesEventArgs> BundlesReceived;

		// Token: 0x14000037 RID: 55
		// (add) Token: 0x06000401 RID: 1025 RVA: 0x0000DF9C File Offset: 0x0000C19C
		// (remove) Token: 0x06000402 RID: 1026 RVA: 0x0000DFB8 File Offset: 0x0000C1B8
		public event EventHandler<BalancesEventArgs> BalancesReceived;

		// Token: 0x14000038 RID: 56
		// (add) Token: 0x06000403 RID: 1027 RVA: 0x0000DFD4 File Offset: 0x0000C1D4
		// (remove) Token: 0x06000404 RID: 1028 RVA: 0x0000DFF0 File Offset: 0x0000C1F0
		public event EventHandler<BalancesEventArgs> BundlePurchased;

		// Token: 0x14000039 RID: 57
		// (add) Token: 0x06000405 RID: 1029 RVA: 0x0000E00C File Offset: 0x0000C20C
		// (remove) Token: 0x06000406 RID: 1030 RVA: 0x0000E028 File Offset: 0x0000C228
		public event EventHandler<MessageEventArgs> Error;

		// Token: 0x1400003A RID: 58
		// (add) Token: 0x06000407 RID: 1031 RVA: 0x0000E044 File Offset: 0x0000C244
		// (remove) Token: 0x06000408 RID: 1032 RVA: 0x0000E060 File Offset: 0x0000C260
		public event EventHandler<MessageEventArgs> PurchaseCancelled;

		// Token: 0x1400003B RID: 59
		// (add) Token: 0x06000409 RID: 1033 RVA: 0x0000E07C File Offset: 0x0000C27C
		// (remove) Token: 0x0600040A RID: 1034 RVA: 0x0000E098 File Offset: 0x0000C298
		public event EventHandler<MessageEventArgs> BundlePurchaseFailed;

		// Token: 0x1400003C RID: 60
		// (add) Token: 0x0600040B RID: 1035 RVA: 0x0000E0B4 File Offset: 0x0000C2B4
		// (remove) Token: 0x0600040C RID: 1036 RVA: 0x0000E0D0 File Offset: 0x0000C2D0
		public event EventHandler<UserQuantityEventArgs> UserQuantityReceived;

		// Token: 0x1400003D RID: 61
		// (add) Token: 0x0600040D RID: 1037 RVA: 0x0000E0EC File Offset: 0x0000C2EC
		// (remove) Token: 0x0600040E RID: 1038 RVA: 0x0000E108 File Offset: 0x0000C308
		public event EventHandler<ItemsEventArgs> UserItemsReceived;

		// Token: 0x1400003E RID: 62
		// (add) Token: 0x0600040F RID: 1039 RVA: 0x0000E124 File Offset: 0x0000C324
		// (remove) Token: 0x06000410 RID: 1040 RVA: 0x0000E140 File Offset: 0x0000C340
		public event EventHandler<MessageEventArgs> DebugMessage;

		// Token: 0x06000411 RID: 1041 RVA: 0x0000E15C File Offset: 0x0000C35C
		public void Init()
		{
			JSCaller.Call("currencies_Init");
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0000E168 File Offset: 0x0000C368
		public void GetBalance(int currencyId)
		{
			string data = "{ \"currency_id\" : " + currencyId + " }";
			JSCaller.Call("currencies_GetBalance", data);
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0000E198 File Offset: 0x0000C398
		public void GetBalances()
		{
			JSCaller.Call("currencies_GetBalances");
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0000E1A4 File Offset: 0x0000C3A4
		public void GetUserItemQuantity(int itemId)
		{
			string data = "{ \"item_id\" : " + itemId + " }";
			JSCaller.Call("currencies_GetUserItemQuantity", data);
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0000E1D4 File Offset: 0x0000C3D4
		public void GetUserItemsByGameId()
		{
			JSCaller.Call("currencies_GetUserItemsByGameId");
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0000E1E0 File Offset: 0x0000C3E0
		public void GetItemById(int itemId)
		{
			string data = "{ \"item_id\" : " + itemId + " }";
			JSCaller.Call("currencies_GetItemById", data);
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0000E210 File Offset: 0x0000C410
		public void GetItemsByGameId()
		{
			JSCaller.Call("currencies_GetItemsByGameId");
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0000E21C File Offset: 0x0000C41C
		public void GetBundles(int currencyId)
		{
			this.GetBundles(currencyId, null);
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0000E228 File Offset: 0x0000C428
		public void GetBundles(int currencyId, int[] currencies)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary["currency_id"] = currencyId;
			dictionary["currencies"] = currencies;
			string data = Json.Serialize(dictionary);
			JSCaller.Call("currencies_GetBundles", data);
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0000E26C File Offset: 0x0000C46C
		public void GetCurrencyById(int currencyId)
		{
			string data = "{ \"currency_id\" : " + currencyId + " }";
			JSCaller.Call("currencies_GetCurrencyById", data);
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x0000E29C File Offset: 0x0000C49C
		public void GetAvailableCurrencies()
		{
			JSCaller.Call("currencies_GetAvailableCurrencies");
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0000E2A8 File Offset: 0x0000C4A8
		public void PurchaseBundle(int bundleId, int currencyId)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary["bundle_id"] = bundleId;
			dictionary["currency_id"] = currencyId;
			string data = Json.Serialize(dictionary);
			JSCaller.Call("currencies_PurchaseBundle", data);
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x0000E2F0 File Offset: 0x0000C4F0
		public void PurchaseItem(int itemId, int currencyId, bool skipMax)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary["item_id"] = itemId;
			dictionary["currency_id"] = currencyId;
			dictionary["skip_max"] = skipMax;
			string data = Json.Serialize(dictionary);
			JSCaller.Call("currencies_PurchaseItem", data);
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0000E348 File Offset: 0x0000C548
		public void PurchaseItems(int[] itemIds, int currencyId, bool skipMax)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary["item_ids"] = itemIds;
			dictionary["currency_id"] = currencyId;
			dictionary["skip_max"] = skipMax;
			string data = Json.Serialize(dictionary);
			JSCaller.Call("currencies_PurchaseItems", data);
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0000E39C File Offset: 0x0000C59C
		public void AdjustCurrencyBalance(int currencyId, decimal amount)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary["currency_id"] = currencyId;
			dictionary["amount"] = amount;
			string data = Json.Serialize(dictionary);
			JSCaller.Call("currencies_AdjustCurrencyBalance", data);
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0000E3E4 File Offset: 0x0000C5E4
		public void DecrementItemBalance(int itemId, decimal amount)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary["item_id"] = itemId;
			dictionary["amount"] = amount;
			string data = Json.Serialize(dictionary);
			JSCaller.Call("currencies_DecrementItemBalance", data);
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0000E42C File Offset: 0x0000C62C
		public void GiveItem(int itemId, decimal amount)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary["item_id"] = itemId;
			dictionary["amount"] = amount;
			string data = Json.Serialize(dictionary);
			JSCaller.Call("currencies_GiveItem", data);
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x0000E474 File Offset: 0x0000C674
		public void TopUpCurrency()
		{
			JSCaller.Call("currencies_TopUpCurrency");
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x0000E480 File Offset: 0x0000C680
		public void ConvertCurrency(int sourceId, int destinationId, decimal sourceAmount)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary["src_id"] = sourceId;
			dictionary["des_id"] = destinationId;
			dictionary["src_amount"] = sourceAmount;
			string data = Json.Serialize(dictionary);
			JSCaller.Call("currencies_ConvertCurrency", data);
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0000E4D8 File Offset: 0x0000C6D8
		public void ShowOffer(int currencyId, string offerType)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary["currency_id"] = currencyId;
			dictionary["offer_type"] = offerType;
			string data = Json.Serialize(dictionary);
			JSCaller.Call("currencies_ShowOffer", data);
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0000E51C File Offset: 0x0000C71C
		public void CurrenciesOfferAvailable(int currencyId, string offerType)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary["currency_id"] = currencyId;
			dictionary["offer_type"] = offerType;
			string data = Json.Serialize(dictionary);
			JSCaller.Call("currencies_CurrenciesOfferAvailable", data);
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x0000E560 File Offset: 0x0000C760
		internal override void ProcessData(string noticeID, string json)
		{
			switch (noticeID)
			{
			case "currencies_ready":
			{
				Dictionary<string, object> dictionary = Json.Deserialize(json) as Dictionary<string, object>;
				Dictionary<string, CurrencyCurrency> currencies = null;
				Dictionary<string, CurrencyItem> items = null;
				if (dictionary.ContainsKey("currencies"))
				{
					Dictionary<string, object> search = dictionary["currencies"] as Dictionary<string, object>;
					currencies = CurrenciesFactory.BuildCurrencies(search);
				}
				if (dictionary.ContainsKey("items"))
				{
					Dictionary<string, object> search2 = dictionary["items"] as Dictionary<string, object>;
					items = CurrenciesFactory.BuildItems(search2);
				}
				this.Ready(this, new CurrenciesReadyEventArgs(currencies, items));
				break;
			}
			case "currencies_balance":
			case "currencies_balances":
				this.BalancesReceived(this, new BalancesEventArgs(CurrenciesFactory.BuildBalances(json)));
				break;
			case "currencies_user_item_quantity":
				this.UserQuantityReceived(this, new UserQuantityEventArgs(CurrenciesFactory.BuildUserQuantity(json)));
				break;
			case "currencies_user_items_by_gameid":
			{
				string message = "-> MiniclipCurrencies::ProcessData() - NoticeID.CURRENCIES_USER_ITEMS_BY_GAME_ID, json: " + json;
				this.DebugMessage(this, new MessageEventArgs(message));
				this.UserItemsReceived(this, new ItemsEventArgs(CurrenciesFactory.BuildItems(json)));
				break;
			}
			case "currencies_error":
				this.Error(this, new MessageEventArgs(json));
				break;
			case "currencies_available_currencies":
				this.AvailableCurrenciesReceived(this, new CurrenciesEventArgs(CurrenciesFactory.BuildCurrencies(json)));
				break;
			case "currencies_bundles":
				this.BundlesReceived(this, new BundlesEventArgs(CurrenciesFactory.BuildBundles(json)));
				break;
			case "currencies_bundle_purchased":
				this.BundlePurchased(this, new BalancesEventArgs(CurrenciesFactory.BuildBalances(json)));
				break;
			case "currencies_bundle_purchase_failed":
				this.BundlePurchaseFailed(this, new MessageEventArgs("Purchase Failed!"));
				break;
			case "currencies_purchase_cancelled":
				this.PurchaseCancelled(this, new MessageEventArgs("Purchase Cancelled!"));
				break;
			}
		}

		// Token: 0x04000203 RID: 515
		public const string CURRENCIES_INIT = "currencies_Init";

		// Token: 0x04000204 RID: 516
		public const string CURRENCIES_GET_BALANCE = "currencies_GetBalance";

		// Token: 0x04000205 RID: 517
		public const string CURRENCIES_GET_BALANCES = "currencies_GetBalances";

		// Token: 0x04000206 RID: 518
		public const string CURRENCIES_GET_USER_ITEM_QUANTITY = "currencies_GetUserItemQuantity";

		// Token: 0x04000207 RID: 519
		public const string CURRENCIES_GET_USER_ITEMS_BY_GAME_ID = "currencies_GetUserItemsByGameId";

		// Token: 0x04000208 RID: 520
		public const string CURRENCIES_GET_ITEM_BY_ID = "currencies_GetItemById";

		// Token: 0x04000209 RID: 521
		public const string CURRENCIES_GET_ITEMS_BY_GAME_ID = "currencies_GetItemsByGameId";

		// Token: 0x0400020A RID: 522
		public const string CURRENCIES_GET_BUNDLES = "currencies_GetBundles";

		// Token: 0x0400020B RID: 523
		public const string CURRENCIES_GET_CURRENCY_BY_ID = "currencies_GetCurrencyById";

		// Token: 0x0400020C RID: 524
		public const string CURRENCIES_GET_AVAILABLE_CURRENCIES = "currencies_GetAvailableCurrencies";

		// Token: 0x0400020D RID: 525
		public const string CURRENCIES_PURCHASE_BUNDLE = "currencies_PurchaseBundle";

		// Token: 0x0400020E RID: 526
		public const string CURRENCIES_PURCHASE_ITEM = "currencies_PurchaseItem";

		// Token: 0x0400020F RID: 527
		public const string CURRENCIES_PURCHASE_ITEMS = "currencies_PurchaseItems";

		// Token: 0x04000210 RID: 528
		public const string CURRENCIES_ADJUST_CURRENCY_BALANCE = "currencies_AdjustCurrencyBalance";

		// Token: 0x04000211 RID: 529
		public const string CURRENCIES_DECREMENT_ITEM_BALANCE = "currencies_DecrementItemBalance";

		// Token: 0x04000212 RID: 530
		public const string CURRENCIES_GIVE_ITEM = "currencies_GiveItem";

		// Token: 0x04000213 RID: 531
		public const string CURRENCIES_TOP_UP_CURRENCY = "currencies_TopUpCurrency";

		// Token: 0x04000214 RID: 532
		public const string CURRENCIES_CONVERT_CURRENCY = "currencies_ConvertCurrency";

		// Token: 0x04000215 RID: 533
		public const string CURRENCIES_SHOW_OFFER = "currencies_ShowOffer";

		// Token: 0x04000216 RID: 534
		public const string CURRENCIES_OFFER_AVAILABLE = "currencies_CurrenciesOfferAvailable";
	}
}
