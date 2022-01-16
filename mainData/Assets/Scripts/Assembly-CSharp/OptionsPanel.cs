using System;
using UnityEngine;

// Token: 0x020000B1 RID: 177
public class OptionsPanel : MonoBehaviour
{
	// Token: 0x06000576 RID: 1398 RVA: 0x00026CF8 File Offset: 0x00024EF8
	private void Start()
	{
		this.pFullScreenSprite = this.fullscreenButton.transform.Find("Icon").GetComponent<UISprite>();
		this.pVisualSprite = this.visualsButton.transform.Find("Icon").GetComponent<UISprite>();
		this.pSoundSprite = this.soundButton.transform.Find("Icon").GetComponent<UISprite>();
	}

	// Token: 0x06000577 RID: 1399 RVA: 0x00026D68 File Offset: 0x00024F68
	private void Update()
	{
		if (Screen.fullScreen)
		{
			this.pFullScreenSprite.spriteName = "OptionsFullscreenOn";
		}
		else
		{
			this.pFullScreenSprite.spriteName = "OptionsFullscreenOff";
		}
		if (Data.highDetails)
		{
			this.pVisualSprite.spriteName = "OptionsVisualsOn";
		}
		else
		{
			this.pVisualSprite.spriteName = "OptionsVisualsOff";
		}
		if (Data.sfx && !Data.muteAllSound)
		{
			this.pSoundSprite.spriteName = "OptionsSoundOn";
		}
		else
		{
			this.pSoundSprite.spriteName = "OptionsSoundOff";
		}
		this.leaderboardsButton.SetActive(Data.branding == "Miniclip");
	}

	// Token: 0x06000578 RID: 1400 RVA: 0x00026E28 File Offset: 0x00025028
	public void OnButton(GameObject go)
	{
		string name = go.name;
		string text = name;
		switch (text)
		{
		case "FullscreenButton":
			Options.ToggleFullScreen();
			break;
		case "VisualsButton":
			Data.highDetails = !Data.highDetails;
			Options.SetVisuals();
			break;
		case "LeaderboardsButton":
			Scripts.interfaceScript.OnButton("LeaderboardsButton");
			break;
		case "HowToPlayButton":
			Scripts.interfaceScript.OnButton("HowToPlayButton");
			break;
		case "SoundButton":
			if (!Data.sfx)
			{
				Data.muteAllSound = false;
			}
			Data.sfx = !Data.sfx;
			Scripts.audioManager.MuteSFX(!Data.sfx);
			if (!Data.music)
			{
				Data.muteAllSound = false;
			}
			Data.music = !Data.music;
			Scripts.audioManager.MuteMusic(!Data.music);
			UserData.Save();
			Scripts.audioManager.PlaySFX("Interface/Select");
			break;
		case "SoundButtonOld":
			if (Data.muteAllSound)
			{
				Data.muteAllSound = false;
				Data.sfx = true;
				Data.music = false;
			}
			else
			{
				Data.sfx = !Data.sfx;
			}
			Scripts.audioManager.MuteSFX(!Data.sfx);
			break;
		}
	}

	// Token: 0x040005BB RID: 1467
	public GameObject options;

	// Token: 0x040005BC RID: 1468
	public GameObject fullscreenButton;

	// Token: 0x040005BD RID: 1469
	public GameObject musicButton;

	// Token: 0x040005BE RID: 1470
	public GameObject pauseButton;

	// Token: 0x040005BF RID: 1471
	public GameObject soundButton;

	// Token: 0x040005C0 RID: 1472
	public GameObject visualsButton;

	// Token: 0x040005C1 RID: 1473
	public GameObject leaderboardsButton;

	// Token: 0x040005C2 RID: 1474
	private UISprite pFullScreenSprite;

	// Token: 0x040005C3 RID: 1475
	private UISprite pVisualSprite;

	// Token: 0x040005C4 RID: 1476
	private UISprite pSoundSprite;

	// Token: 0x040005C5 RID: 1477
	private UISprite pMusicSprite;
}
