using System;
using System.Collections.Generic;

namespace com.miniclip.currencies
{
	// Token: 0x02000088 RID: 136
	public class BundlesEventArgs : EventArgs
	{
		// Token: 0x060003EF RID: 1007 RVA: 0x0000DE64 File Offset: 0x0000C064
		public BundlesEventArgs(Dictionary<string, CurrencyBundle> bundles)
		{
			this._bundles = bundles;
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x0000DE74 File Offset: 0x0000C074
		public Dictionary<string, CurrencyBundle> Bundles
		{
			get
			{
				return this._bundles;
			}
		}

		// Token: 0x040001FD RID: 509
		private Dictionary<string, CurrencyBundle> _bundles;
	}
}
