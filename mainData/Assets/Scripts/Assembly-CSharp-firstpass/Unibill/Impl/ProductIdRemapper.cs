using System;
using System.Collections.Generic;

namespace Unibill.Impl
{
	// Token: 0x0200003B RID: 59
	public class ProductIdRemapper
	{
		// Token: 0x06000216 RID: 534 RVA: 0x00008780 File Offset: 0x00006980
		public ProductIdRemapper(InventoryDatabase db, UnibillXmlParser parser, UnibillConfiguration config)
		{
			this.db = db;
			this.parser = parser;
			this.initialiseForPlatform(config.CurrentPlatform);
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000217 RID: 535 RVA: 0x000087B0 File Offset: 0x000069B0
		// (set) Token: 0x06000218 RID: 536 RVA: 0x000087B8 File Offset: 0x000069B8
		public InventoryDatabase db { get; private set; }

		// Token: 0x06000219 RID: 537 RVA: 0x000087C4 File Offset: 0x000069C4
		public void initialiseForPlatform(BillingPlatform platform)
		{
			this.genericToPlatformSpecificIds = new Dictionary<string, string>();
			this.platformSpecificToGenericIds = new Dictionary<string, string>();
			string key = string.Format("{0}.Id", platform);
			foreach (UnibillXmlParser.UnibillXElement unibillXElement in this.parser.Parse("unibillInventory", "item"))
			{
				string text = unibillXElement.attributes["id"];
				string text2 = text;
				if (unibillXElement.kvps.ContainsKey(key))
				{
					text2 = unibillXElement.kvps[key];
				}
				this.genericToPlatformSpecificIds[text] = text2;
				this.platformSpecificToGenericIds[text2] = text;
			}
		}

		// Token: 0x0600021A RID: 538 RVA: 0x000088A8 File Offset: 0x00006AA8
		public string[] getAllPlatformSpecificProductIds()
		{
			List<string> list = new List<string>();
			foreach (PurchasableItem item in this.db.AllPurchasableItems)
			{
				list.Add(this.mapItemIdToPlatformSpecificId(item));
			}
			return list.ToArray();
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00008928 File Offset: 0x00006B28
		public string mapItemIdToPlatformSpecificId(PurchasableItem item)
		{
			return this.genericToPlatformSpecificIds[item.Id];
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000893C File Offset: 0x00006B3C
		public PurchasableItem getPurchasableItemFromPlatformSpecificId(string platformSpecificId)
		{
			string id = this.platformSpecificToGenericIds[platformSpecificId];
			return this.db.getItemById(id);
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00008964 File Offset: 0x00006B64
		public bool canMapProductSpecificId(string id)
		{
			return this.platformSpecificToGenericIds.ContainsKey(id);
		}

		// Token: 0x040000BF RID: 191
		private Dictionary<string, string> genericToPlatformSpecificIds;

		// Token: 0x040000C0 RID: 192
		private Dictionary<string, string> platformSpecificToGenericIds;

		// Token: 0x040000C1 RID: 193
		private UnibillXmlParser parser;
	}
}
