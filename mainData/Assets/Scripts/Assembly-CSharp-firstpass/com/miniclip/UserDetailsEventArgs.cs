using System;

namespace com.miniclip
{
	// Token: 0x0200005F RID: 95
	public class UserDetailsEventArgs : EventArgs
	{
		// Token: 0x06000303 RID: 771 RVA: 0x0000B2F0 File Offset: 0x000094F0
		public UserDetailsEventArgs(UserDetails userDetails)
		{
			this._userDetails = userDetails;
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000304 RID: 772 RVA: 0x0000B300 File Offset: 0x00009500
		public UserDetails UserDetails
		{
			get
			{
				return this._userDetails;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000305 RID: 773 RVA: 0x0000B308 File Offset: 0x00009508
		public bool UserLoggedIn
		{
			get
			{
				return this._userDetails != null;
			}
		}

		// Token: 0x0400014F RID: 335
		private UserDetails _userDetails;
	}
}
