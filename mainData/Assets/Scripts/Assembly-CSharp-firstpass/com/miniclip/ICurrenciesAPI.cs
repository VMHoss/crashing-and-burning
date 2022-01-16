using System;

namespace com.miniclip
{
	// Token: 0x0200007C RID: 124
	public interface ICurrenciesAPI
	{
		// Token: 0x0600038E RID: 910
		void Init();

		// Token: 0x0600038F RID: 911
		void GetBalance(int currencyId);

		// Token: 0x06000390 RID: 912
		void GetBalances();

		// Token: 0x06000391 RID: 913
		void GetUserItemQuantity(int itemId);

		// Token: 0x06000392 RID: 914
		void GetUserItemsByGameId();

		// Token: 0x06000393 RID: 915
		void GetItemById(int itemId);

		// Token: 0x06000394 RID: 916
		void GetItemsByGameId();

		// Token: 0x06000395 RID: 917
		void GetBundles(int currencyId);

		// Token: 0x06000396 RID: 918
		void GetBundles(int currencyId, int[] currencies);

		// Token: 0x06000397 RID: 919
		void GetCurrencyById(int currencyId);

		// Token: 0x06000398 RID: 920
		void GetAvailableCurrencies();

		// Token: 0x06000399 RID: 921
		void PurchaseBundle(int bundleId, int currencyId);

		// Token: 0x0600039A RID: 922
		void PurchaseItem(int itemId, int currencyId, bool skipMax);

		// Token: 0x0600039B RID: 923
		void PurchaseItems(int[] itemIds, int currencyId, bool skipMax);

		// Token: 0x0600039C RID: 924
		void AdjustCurrencyBalance(int currencyId, decimal amount);

		// Token: 0x0600039D RID: 925
		void DecrementItemBalance(int itemId, decimal amount);

		// Token: 0x0600039E RID: 926
		void GiveItem(int itemId, decimal amount);

		// Token: 0x0600039F RID: 927
		void TopUpCurrency();

		// Token: 0x060003A0 RID: 928
		void ConvertCurrency(int sourceId, int destinationId, decimal sourceAmount);

		// Token: 0x060003A1 RID: 929
		void ShowOffer(int currencyId, string offerType);

		// Token: 0x060003A2 RID: 930
		void CurrenciesOfferAvailable(int currencyId, string offerType);
	}
}
