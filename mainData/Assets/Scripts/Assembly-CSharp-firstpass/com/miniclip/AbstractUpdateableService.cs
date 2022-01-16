using System;

namespace com.miniclip
{
	// Token: 0x02000079 RID: 121
	public abstract class AbstractUpdateableService : AbstractService
	{
		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000387 RID: 903
		internal abstract AbstractUpdateable Updateable { get; }
	}
}
