using System;

namespace com.miniclip
{
	// Token: 0x02000078 RID: 120
	public abstract class AbstractUpdateable
	{
		// Token: 0x14000030 RID: 48
		// (add) Token: 0x06000383 RID: 899
		// (remove) Token: 0x06000384 RID: 900
		internal abstract event EventHandler<UpdateEventArgs> updatePoolRequested;

		// Token: 0x06000385 RID: 901
		internal abstract void Update();

		// Token: 0x040001C3 RID: 451
		internal int currentUpdatePool = -1;
	}
}
