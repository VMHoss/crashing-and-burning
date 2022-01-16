using System;
using Uniject;

// Token: 0x02000042 RID: 66
public class TransactionDatabase
{
	// Token: 0x06000258 RID: 600 RVA: 0x00009818 File Offset: 0x00007A18
	public TransactionDatabase(IStorage storage, ILogger logger)
	{
		this.storage = storage;
		this.logger = logger;
		this.UserId = "default";
	}

	// Token: 0x1700002B RID: 43
	// (get) Token: 0x06000259 RID: 601 RVA: 0x0000983C File Offset: 0x00007A3C
	// (set) Token: 0x0600025A RID: 602 RVA: 0x00009844 File Offset: 0x00007A44
	public string UserId { get; set; }

	// Token: 0x0600025B RID: 603 RVA: 0x00009850 File Offset: 0x00007A50
	public int getPurchaseHistory(PurchasableItem item)
	{
		return this.storage.GetInt(this.getKey(item.Id), 0);
	}

	// Token: 0x0600025C RID: 604 RVA: 0x0000986C File Offset: 0x00007A6C
	public void onPurchase(PurchasableItem item)
	{
		int purchaseHistory = this.getPurchaseHistory(item);
		if (item.PurchaseType != PurchaseType.Consumable && purchaseHistory != 0)
		{
			this.logger.LogWarning("Apparently multi purchased a non consumable:{0}", new object[]
			{
				item.Id
			});
			return;
		}
		this.storage.SetInt(this.getKey(item.Id), purchaseHistory + 1);
	}

	// Token: 0x0600025D RID: 605 RVA: 0x000098CC File Offset: 0x00007ACC
	public void clearPurchases(PurchasableItem item)
	{
		this.storage.SetInt(this.getKey(item.Id), 0);
	}

	// Token: 0x0600025E RID: 606 RVA: 0x000098E8 File Offset: 0x00007AE8
	public void onRefunded(PurchasableItem item)
	{
		int num = this.getPurchaseHistory(item);
		num = Math.Max(0, num - 1);
		this.storage.SetInt(this.getKey(item.Id), num);
	}

	// Token: 0x0600025F RID: 607 RVA: 0x00009920 File Offset: 0x00007B20
	private string getKey(string fragment)
	{
		return string.Format("{0}.{1}", this.UserId, fragment);
	}

	// Token: 0x040000D4 RID: 212
	private IStorage storage;

	// Token: 0x040000D5 RID: 213
	private ILogger logger;
}
