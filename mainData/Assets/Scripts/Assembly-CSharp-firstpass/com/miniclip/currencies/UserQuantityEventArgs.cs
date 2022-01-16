using System;

namespace com.miniclip.currencies
{
	// Token: 0x0200008C RID: 140
	public class UserQuantityEventArgs : EventArgs
	{
		// Token: 0x060003F8 RID: 1016 RVA: 0x0000DED4 File Offset: 0x0000C0D4
		public UserQuantityEventArgs(CurrencyUserQuantity userQuantity)
		{
			this._userQuantity = userQuantity;
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060003F9 RID: 1017 RVA: 0x0000DEE4 File Offset: 0x0000C0E4
		public CurrencyUserQuantity UserQuantity
		{
			get
			{
				return this._userQuantity;
			}
		}

		// Token: 0x04000202 RID: 514
		private CurrencyUserQuantity _userQuantity;
	}
}
