using System;
using UnityEngine;

namespace com.miniclip.awards
{
	// Token: 0x0200004F RID: 79
	public class LoadingIcon : AwardNotificationState
	{
		// Token: 0x060002B4 RID: 692 RVA: 0x0000A3C4 File Offset: 0x000085C4
		public LoadingIcon(string id, AwardNotification award) : base(id, award)
		{
			Debug.Log("-> LoadingIcon - created!");
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000A3D8 File Offset: 0x000085D8
		public override void Enter()
		{
			Debug.Log("-> LoadingIcon::Enter()");
			this._www = new WWW(("http://dev.miniclip.com//images/awards/full/" + this._award.AwardId + ".png").ToString());
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000A420 File Offset: 0x00008620
		public override void Update()
		{
			if (this._www.isDone)
			{
				Debug.Log("-> LoadingIcon::Update() - texture Loaded! :)");
				this._award.IconTexture = this._www.texture;
				this._stateMachine.ChangeState("waiting_for_slot");
			}
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000A470 File Offset: 0x00008670
		public override void Exit()
		{
		}

		// Token: 0x04000121 RID: 289
		private const string AWARD_ICONS_DIR = "http://dev.miniclip.com//images/awards/full/";

		// Token: 0x04000122 RID: 290
		private WWW _www;
	}
}
