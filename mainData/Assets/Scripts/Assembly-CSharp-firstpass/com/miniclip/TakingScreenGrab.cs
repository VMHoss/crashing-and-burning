using System;
using UnityEngine;

namespace com.miniclip
{
	// Token: 0x0200006E RID: 110
	public class TakingScreenGrab : FlashSwitcherState
	{
		// Token: 0x06000335 RID: 821 RVA: 0x0000B784 File Offset: 0x00009984
		public TakingScreenGrab(string id, FlashSwitcher flashSwitcher) : base(id, flashSwitcher)
		{
			Debug.Log("-> TakeScreenGrab - created!");
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0000B798 File Offset: 0x00009998
		public override void Enter()
		{
			Debug.Log("-> TakeScreenGrab::Enter()");
			this._texture = new Texture2D(Screen.width, Screen.height);
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0000B7BC File Offset: 0x000099BC
		public override void Update()
		{
			Debug.Log("-> TakingScreenGrab::Update()");
			this._texture.ReadPixels(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height), 0, 0);
			this._texture.Apply();
			this.QuarterTexture();
			byte[] inArray = this._texture.EncodeToPNG();
			string text = Convert.ToBase64String(inArray);
			Debug.Log("PNG String Len: " + text.Length);
			this._flashSwitcher.bitmapString = text;
			this._stateMachine.ChangeState("clearing_bmp_data");
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0000B858 File Offset: 0x00009A58
		private void QuarterTexture()
		{
			Texture2D texture2D = new Texture2D(this._texture.width / 4, this._texture.height / 4);
			texture2D.SetPixels(this._texture.GetPixels(2));
			texture2D.Apply();
			byte[] data = texture2D.EncodeToPNG();
			UnityEngine.Object.Destroy(texture2D);
			this._texture.LoadImage(data);
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0000B8B8 File Offset: 0x00009AB8
		public override void Exit()
		{
			Debug.Log("-> TakeScreenGrab::Exit()");
			UnityEngine.Object.Destroy(this._texture);
			this._texture = null;
		}

		// Token: 0x04000170 RID: 368
		private Texture2D _texture;
	}
}
