using System;
using Uniject;

namespace Unibill.Impl
{
	// Token: 0x02000044 RID: 68
	public class UnibillConfiguration
	{
		// Token: 0x06000260 RID: 608 RVA: 0x00009934 File Offset: 0x00007B34
		public UnibillConfiguration(IResourceLoader loader, UnibillXmlParser parser, IUtil util, ILogger logger)
		{
			UnibillXmlParser.UnibillXElement unibillXElement = parser.Parse("unibillInventory", "inventory")[0];
			string text = null;
			unibillXElement.kvps.TryGetValue("iOSSKU", out text);
			this.iOSSKU = text;
			unibillXElement.kvps.TryGetValue("GooglePlayPublicKey", out text);
			this.GooglePlayPublicKey = text;
			this.AmazonSandboxEnabled = bool.Parse(unibillXElement.kvps["useAmazonSandbox"]);
			this.WP8SandboxEnabled = bool.Parse(unibillXElement.kvps["UseWP8MockingFramework"]);
			this.CurrentPlatform = BillingPlatform.MacAppStore;
			this.CurrentPlatform = (BillingPlatform)((int)Enum.Parse(typeof(BillingPlatform), unibillXElement.kvps["androidBillingPlatform"]));
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000261 RID: 609 RVA: 0x000099FC File Offset: 0x00007BFC
		// (set) Token: 0x06000262 RID: 610 RVA: 0x00009A04 File Offset: 0x00007C04
		public string iOSSKU { get; private set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000263 RID: 611 RVA: 0x00009A10 File Offset: 0x00007C10
		// (set) Token: 0x06000264 RID: 612 RVA: 0x00009A18 File Offset: 0x00007C18
		public BillingPlatform CurrentPlatform { get; private set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000265 RID: 613 RVA: 0x00009A24 File Offset: 0x00007C24
		// (set) Token: 0x06000266 RID: 614 RVA: 0x00009A2C File Offset: 0x00007C2C
		public string GooglePlayPublicKey { get; private set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000267 RID: 615 RVA: 0x00009A38 File Offset: 0x00007C38
		// (set) Token: 0x06000268 RID: 616 RVA: 0x00009A40 File Offset: 0x00007C40
		public bool AmazonSandboxEnabled { get; private set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000269 RID: 617 RVA: 0x00009A4C File Offset: 0x00007C4C
		// (set) Token: 0x0600026A RID: 618 RVA: 0x00009A54 File Offset: 0x00007C54
		public bool WP8SandboxEnabled { get; private set; }
	}
}
