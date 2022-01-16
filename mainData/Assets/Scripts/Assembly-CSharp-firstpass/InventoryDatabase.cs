using System;
using System.Collections.Generic;
using Unibill.Impl;
using Uniject;

// Token: 0x02000038 RID: 56
public class InventoryDatabase
{
	// Token: 0x060001F0 RID: 496 RVA: 0x000078EC File Offset: 0x00005AEC
	public InventoryDatabase(UnibillXmlParser parser, ILogger logger)
	{
		this.logger = logger;
		this.items = new List<PurchasableItem>();
		foreach (UnibillXmlParser.UnibillXElement unibillXElement in parser.Parse("unibillInventory", "item"))
		{
			string id;
			unibillXElement.attributes.TryGetValue("id", out id);
			PurchaseType purchaseType = (PurchaseType)((int)Enum.Parse(typeof(PurchaseType), unibillXElement.attributes["purchaseType"]));
			string name;
			unibillXElement.kvps.TryGetValue("name", out name);
			string description;
			unibillXElement.kvps.TryGetValue("description", out description);
			this.items.Add(new PurchasableItem(id, purchaseType, name, description));
		}
	}

	// Token: 0x060001F1 RID: 497 RVA: 0x000079E4 File Offset: 0x00005BE4
	public PurchasableItem getItemById(string id)
	{
		PurchasableItem purchasableItem = this.items.Find((PurchasableItem x) => x.Id == id);
		if (purchasableItem == null)
		{
			this.logger.LogWarning("Unknown purchasable item:{0}. Check your Unibill inventory configuration.", new object[]
			{
				id
			});
		}
		return purchasableItem;
	}

	// Token: 0x1700001B RID: 27
	// (get) Token: 0x060001F2 RID: 498 RVA: 0x00007A3C File Offset: 0x00005C3C
	public List<PurchasableItem> AllPurchasableItems
	{
		get
		{
			return new List<PurchasableItem>(this.items);
		}
	}

	// Token: 0x1700001C RID: 28
	// (get) Token: 0x060001F3 RID: 499 RVA: 0x00007A4C File Offset: 0x00005C4C
	public List<PurchasableItem> AllNonConsumablePurchasableItems
	{
		get
		{
			return this.items.FindAll((PurchasableItem x) => x.PurchaseType == PurchaseType.NonConsumable);
		}
	}

	// Token: 0x1700001D RID: 29
	// (get) Token: 0x060001F4 RID: 500 RVA: 0x00007A84 File Offset: 0x00005C84
	public List<PurchasableItem> AllConsumablePurchasableItems
	{
		get
		{
			return this.items.FindAll((PurchasableItem x) => x.PurchaseType == PurchaseType.Consumable);
		}
	}

	// Token: 0x1700001E RID: 30
	// (get) Token: 0x060001F5 RID: 501 RVA: 0x00007ABC File Offset: 0x00005CBC
	public List<PurchasableItem> AllSubscriptions
	{
		get
		{
			return this.items.FindAll((PurchasableItem x) => x.PurchaseType == PurchaseType.Subscription);
		}
	}

	// Token: 0x1700001F RID: 31
	// (get) Token: 0x060001F6 RID: 502 RVA: 0x00007AF4 File Offset: 0x00005CF4
	public List<PurchasableItem> AllNonSubscriptionPurchasableItems
	{
		get
		{
			return this.items.FindAll((PurchasableItem x) => x.PurchaseType != PurchaseType.Subscription);
		}
	}

	// Token: 0x040000AA RID: 170
	private List<PurchasableItem> items;

	// Token: 0x040000AB RID: 171
	private ILogger logger;
}
