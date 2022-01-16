using System;

namespace com.miniclip
{
	// Token: 0x02000060 RID: 96
	public interface IState
	{
		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000306 RID: 774
		string Id { get; }

		// Token: 0x06000307 RID: 775
		void Enter();

		// Token: 0x06000308 RID: 776
		void Update();

		// Token: 0x06000309 RID: 777
		void Exit();
	}
}
