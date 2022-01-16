using System;
using UnityEngine;

// Token: 0x020000FF RID: 255
public class MenuScript : MonoBehaviour
{
	// Token: 0x06000799 RID: 1945 RVA: 0x00039EB4 File Offset: 0x000380B4
	private void Awake()
	{
		if (Data.scene != "Menu")
		{
			UnityEngine.Object.Destroy(this);
			return;
		}
		Scripts.menuScript = this;
		Options.SetVisuals();
	}

	// Token: 0x0600079A RID: 1946 RVA: 0x00039EE8 File Offset: 0x000380E8
	private void Start()
	{
		GameObject.Find("GameAudio").transform.position = new Vector3(0f, 0f, 0f);
		GameObject.Find("GameAudio").transform.rotation = Quaternion.Euler(0f, 180f, 0f);
	}

	// Token: 0x0600079B RID: 1947 RVA: 0x00039F48 File Offset: 0x00038148
	private void Update()
	{
		if (Input.anyKey)
		{
			foreach (char c in Input.inputString)
			{
				if (c == '\n' || c == '\r')
				{
					if (this.pCheatText == "xformisthebest")
					{
						Data.cheats = true;
						Data.muteAllSound = false;
						Scripts.audioManager.MuteSFX(false);
						Scripts.audioManager.PlaySFX("NukeAlarm");
						Debug.Log("CHEATS ENABLED!");
					}
					this.pCheatText = string.Empty;
				}
				else
				{
					this.pCheatText += c;
				}
			}
			if (Input.inputString.Length > 14)
			{
				this.pCheatText = string.Empty;
			}
		}
		if (Input.GetKeyDown(KeyCode.F) && this.pCheatText != "xf")
		{
			Options.ToggleFullScreen();
		}
	}

	// Token: 0x0600079C RID: 1948 RVA: 0x0003A044 File Offset: 0x00038244
	public void StartGame()
	{
		GameData.player.NewGame();
		Data.scene = "Level";
		Scripts.audioManager.StopAllSounds();
		Scripts.audioManager.gameObject.transform.parent = null;
		Data.requiredBundles.Add(new BundleEntry("Shared", "Shared"));
		Application.LoadLevel("Loading");
	}

	// Token: 0x0600079D RID: 1949 RVA: 0x0003A0A8 File Offset: 0x000382A8
	private void OnDestroy()
	{
		Scripts.menuScript = null;
	}

	// Token: 0x04000821 RID: 2081
	private string pCheatText = string.Empty;
}
