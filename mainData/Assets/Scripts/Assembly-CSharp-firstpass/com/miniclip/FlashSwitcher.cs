using System;

namespace com.miniclip
{
	// Token: 0x02000065 RID: 101
	public class FlashSwitcher : AbstractUpdateable, IStateable
	{
		// Token: 0x0600031B RID: 795 RVA: 0x0000B4DC File Offset: 0x000096DC
		public FlashSwitcher()
		{
			this._stateMachine = new StateMachine();
			this._stateMachine.AddState(new Ready("ready", this));
			this._stateMachine.AddState(new TakingScreenGrab("taking_screen_grab", this));
			this._stateMachine.AddState(new ClearingBmpData("clearing_bmp_data", this));
			this._stateMachine.AddState(new TransmittingBmpData("transmitting_bmp_data", this));
			this._stateMachine.AddState(new RenderingScreenGrab("rendering_screen_grab", this));
			this._stateMachine.AddState(new CallingRequestedFunction("calling_requested_function", this));
			this._stateMachine.SetStartState("ready");
		}

		// Token: 0x14000028 RID: 40
		// (add) Token: 0x0600031C RID: 796 RVA: 0x0000B59C File Offset: 0x0000979C
		// (remove) Token: 0x0600031D RID: 797 RVA: 0x0000B5B8 File Offset: 0x000097B8
		internal override event EventHandler<UpdateEventArgs> updatePoolRequested;

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600031E RID: 798 RVA: 0x0000B5D4 File Offset: 0x000097D4
		public StateMachine StateMachine
		{
			get
			{
				return this._stateMachine;
			}
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000B5DC File Offset: 0x000097DC
		public void DisplayServicesAndCall(string functionName)
		{
			this.DisplayServicesAndCall(functionName, null);
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000B5E8 File Offset: 0x000097E8
		public void DisplayServicesAndCall(string functionName, string data)
		{
			this._callArgs = new CallArgs(functionName, data);
			this.ChangeUpdatePool(2);
			this._stateMachine.ChangeState("taking_screen_grab");
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0000B610 File Offset: 0x00009810
		internal void ChangeUpdatePool(int updatePoolId)
		{
			this.updatePoolRequested(this, new UpdateEventArgs(updatePoolId));
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0000B624 File Offset: 0x00009824
		internal override void Update()
		{
			this._stateMachine.Update();
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0000B634 File Offset: 0x00009834
		internal void CallRequestedFunction()
		{
			JSCaller.Call(this._callArgs);
			this._callArgs = null;
		}

		// Token: 0x04000158 RID: 344
		public const string CLEAR_SCREEN_GRAB_DATA = "clearScreenGrabData";

		// Token: 0x04000159 RID: 345
		public const string RENDER_SCREEN_GRAB = "renderScreenGrab";

		// Token: 0x0400015A RID: 346
		public const string RECEIVE_SCREEN_GRAB_DATA = "receiveScreenGrabData";

		// Token: 0x0400015B RID: 347
		private CallArgs _callArgs;

		// Token: 0x0400015C RID: 348
		public string bitmapString = string.Empty;

		// Token: 0x0400015D RID: 349
		private StateMachine _stateMachine;

		// Token: 0x02000066 RID: 102
		private enum ClientOS
		{
			// Token: 0x04000160 RID: 352
			Windows,
			// Token: 0x04000161 RID: 353
			MacOSX
		}

		// Token: 0x02000067 RID: 103
		private enum State
		{
			// Token: 0x04000163 RID: 355
			Ready,
			// Token: 0x04000164 RID: 356
			TakeScreenGrab,
			// Token: 0x04000165 RID: 357
			ClearBmpData,
			// Token: 0x04000166 RID: 358
			TransmitBmpData,
			// Token: 0x04000167 RID: 359
			RenderScreenGrab,
			// Token: 0x04000168 RID: 360
			CallJSFunction
		}
	}
}
