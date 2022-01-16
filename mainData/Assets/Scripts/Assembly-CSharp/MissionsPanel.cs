using System;
using UnityEngine;

// Token: 0x020000AF RID: 175
public class MissionsPanel : MonoBehaviour
{
	// Token: 0x0600056D RID: 1389 RVA: 0x00026994 File Offset: 0x00024B94
	private void Start()
	{
		this.MissionAnimation.transform.localPosition = new Vector3(0f, 0f, 0f);
		this.localization = GameObject.Find("Localization").GetComponent<Localization>();
		this.UpdateMissions();
	}

	// Token: 0x0600056E RID: 1390 RVA: 0x000269E0 File Offset: 0x00024BE0
	public void ShowMissions()
	{
		this.UpdateMissions();
		this.boxCollider.enabled = false;
		this.buttonMessage.functionName = "HideMissions";
		this.tweenPosition.Play(true);
		Scripts.audioManager.PlaySFX("Pause", 0.6f);
	}

	// Token: 0x0600056F RID: 1391 RVA: 0x00026A30 File Offset: 0x00024C30
	public void HideMissions()
	{
		this.boxCollider.enabled = false;
		this.buttonMessage.functionName = "ShowMissions";
		this.tweenPosition.Play(false);
		Scripts.audioManager.PlaySFX("UnPause", 0.6f);
	}

	// Token: 0x06000570 RID: 1392 RVA: 0x00026A70 File Offset: 0x00024C70
	public void TweenDone()
	{
		this.boxCollider.enabled = true;
		if (this.buttonMessage.functionName == "ShowMissions")
		{
			this.missionsDirLeft.spriteName = "MissionsDown";
			this.missionsDirRight.spriteName = "MissionsDown";
		}
		else
		{
			this.missionsDirLeft.spriteName = "MissionsUp";
			this.missionsDirRight.spriteName = "MissionsUp";
		}
	}

	// Token: 0x06000571 RID: 1393 RVA: 0x00026AE8 File Offset: 0x00024CE8
	public void UpdateMissions()
	{
		this.localization = GameObject.Find("Localization").GetComponent<Localization>();
		this.mission2Label.text = this.localization.Get(GameData.mainMission.missionName + "Text");
		string str = string.Empty;
		if (!GameData.mainMission.completed)
		{
			str = "Empty";
		}
		this.mission2Stars.spriteName = "Star" + GameData.mainMission.xp.ToString() + str;
	}

	// Token: 0x040005A7 RID: 1447
	public InterfaceScript interfaceScript;

	// Token: 0x040005A8 RID: 1448
	public GameObject MissionAnimation;

	// Token: 0x040005A9 RID: 1449
	public TweenPosition tweenPosition;

	// Token: 0x040005AA RID: 1450
	public UILabel mission1Label;

	// Token: 0x040005AB RID: 1451
	public UISprite mission1Stars;

	// Token: 0x040005AC RID: 1452
	public UILabel mission2Label;

	// Token: 0x040005AD RID: 1453
	public UISprite mission2Stars;

	// Token: 0x040005AE RID: 1454
	public UILabel mission3Label;

	// Token: 0x040005AF RID: 1455
	public UISprite mission3Stars;

	// Token: 0x040005B0 RID: 1456
	public UISprite missionsDirLeft;

	// Token: 0x040005B1 RID: 1457
	public UISprite missionsDirRight;

	// Token: 0x040005B2 RID: 1458
	public BoxCollider boxCollider;

	// Token: 0x040005B3 RID: 1459
	public UIButtonMessage buttonMessage;

	// Token: 0x040005B4 RID: 1460
	public Localization localization;
}
