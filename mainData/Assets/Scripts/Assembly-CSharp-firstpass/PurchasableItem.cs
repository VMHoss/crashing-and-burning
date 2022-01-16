using System;

// Token: 0x02000036 RID: 54
public class PurchasableItem : IEquatable<PurchasableItem>
{
	// Token: 0x060001D9 RID: 473 RVA: 0x000077C4 File Offset: 0x000059C4
	internal PurchasableItem(string id, PurchaseType purchaseType, string name, string description)
	{
		this.Id = id;
		this.PurchaseType = purchaseType;
		this.name = name;
		this.description = description;
	}

	// Token: 0x17000013 RID: 19
	// (get) Token: 0x060001DA RID: 474 RVA: 0x000077F4 File Offset: 0x000059F4
	// (set) Token: 0x060001DB RID: 475 RVA: 0x000077FC File Offset: 0x000059FC
	public string Id { get; private set; }

	// Token: 0x17000014 RID: 20
	// (get) Token: 0x060001DC RID: 476 RVA: 0x00007808 File Offset: 0x00005A08
	// (set) Token: 0x060001DD RID: 477 RVA: 0x00007810 File Offset: 0x00005A10
	public PurchaseType PurchaseType { get; private set; }

	// Token: 0x17000015 RID: 21
	// (get) Token: 0x060001DE RID: 478 RVA: 0x0000781C File Offset: 0x00005A1C
	// (set) Token: 0x060001DF RID: 479 RVA: 0x00007824 File Offset: 0x00005A24
	public string name { get; private set; }

	// Token: 0x17000016 RID: 22
	// (get) Token: 0x060001E0 RID: 480 RVA: 0x00007830 File Offset: 0x00005A30
	// (set) Token: 0x060001E1 RID: 481 RVA: 0x00007838 File Offset: 0x00005A38
	public string description { get; private set; }

	// Token: 0x17000017 RID: 23
	// (get) Token: 0x060001E2 RID: 482 RVA: 0x00007844 File Offset: 0x00005A44
	// (set) Token: 0x060001E3 RID: 483 RVA: 0x0000784C File Offset: 0x00005A4C
	public decimal localizedPrice { get; private set; }

	// Token: 0x17000018 RID: 24
	// (get) Token: 0x060001E4 RID: 484 RVA: 0x00007858 File Offset: 0x00005A58
	// (set) Token: 0x060001E5 RID: 485 RVA: 0x00007860 File Offset: 0x00005A60
	public string localizedPriceString { get; private set; }

	// Token: 0x17000019 RID: 25
	// (get) Token: 0x060001E6 RID: 486 RVA: 0x0000786C File Offset: 0x00005A6C
	// (set) Token: 0x060001E7 RID: 487 RVA: 0x00007874 File Offset: 0x00005A74
	public string localizedTitle { get; private set; }

	// Token: 0x1700001A RID: 26
	// (get) Token: 0x060001E8 RID: 488 RVA: 0x00007880 File Offset: 0x00005A80
	// (set) Token: 0x060001E9 RID: 489 RVA: 0x00007888 File Offset: 0x00005A88
	public string localizedDescription { get; private set; }

	// Token: 0x060001EA RID: 490 RVA: 0x00007894 File Offset: 0x00005A94
	public bool Equals(PurchasableItem other)
	{
		return other.Id == this.Id;
	}

	// Token: 0x02000037 RID: 55
	internal class Writer
	{
		// Token: 0x060001EC RID: 492 RVA: 0x000078B0 File Offset: 0x00005AB0
		public static void setLocalizedPrice(PurchasableItem item, decimal price)
		{
			item.localizedPrice = price;
			item.localizedPriceString = price.ToString();
		}

		// Token: 0x060001ED RID: 493 RVA: 0x000078C8 File Offset: 0x00005AC8
		public static void setLocalizedPrice(PurchasableItem item, string price)
		{
			item.localizedPriceString = price;
		}

		// Token: 0x060001EE RID: 494 RVA: 0x000078D4 File Offset: 0x00005AD4
		public static void setLocalizedTitle(PurchasableItem item, string title)
		{
			item.localizedTitle = title;
		}

		// Token: 0x060001EF RID: 495 RVA: 0x000078E0 File Offset: 0x00005AE0
		public static void setLocalizedDescription(PurchasableItem item, string description)
		{
			item.localizedDescription = description;
		}
	}
}
