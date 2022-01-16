using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000B8 RID: 184
public class StartPanel : MonoBehaviour
{
	// Token: 0x060005A0 RID: 1440 RVA: 0x000292E8 File Offset: 0x000274E8
	private void OnEnable()
	{
		this.OnStartPanel();
	}

	// Token: 0x060005A1 RID: 1441 RVA: 0x000292F0 File Offset: 0x000274F0
	private void OnDisable()
	{
		Scripts.interfaceScript.overlayPanel.transform.Find("BarMiddle").gameObject.SetActive(false);
	}

	// Token: 0x060005A2 RID: 1442 RVA: 0x00029324 File Offset: 0x00027524
	private void Start()
	{
	}

	// Token: 0x060005A3 RID: 1443 RVA: 0x00029328 File Offset: 0x00027528
	public void OnStartPanel()
	{
		Debug.Log("On Start panel called! Probably by on enable. Timescale: " + Time.timeScale.ToString());
		this.startMissionLabel.text = this.interfaceScript.localization.Get(GameData.mainMission.missionName + "Text");
		this.pauseMissionLabel.text = this.interfaceScript.localization.Get(GameData.mainMission.missionName + "Text");
		this.star1.SetActive(GameData.mainMission.xp == 1);
		this.star2.SetActive(GameData.mainMission.xp == 2);
		this.star3.SetActive(GameData.mainMission.xp == 3);
		this.pauseStar1.SetActive(GameData.mainMission.xp == 1);
		this.pauseStar2.SetActive(GameData.mainMission.xp == 2);
		this.pauseStar3.SetActive(GameData.mainMission.xp == 3);
		base.StartCoroutine(this.StartSequence());
	}

	// Token: 0x060005A4 RID: 1444 RVA: 0x0002944C File Offset: 0x0002764C
	private void Update()
	{
	}

	// Token: 0x060005A5 RID: 1445 RVA: 0x00029450 File Offset: 0x00027650
	private IEnumerator StartSequence()
	{
		Debug.Log("Startsequence started!");
		GenericFunctionsScript.Fade("FromBlack");
		Scripts.interfaceScript.overlayPanel.transform.Find("BarMiddle").gameObject.SetActive(true);
		yield return new WaitForSeconds(2f);
		if (GameData.showMission)
		{
			Scripts.audioManager.PlaySFX("Interface/GetReadyJingle");
			this.mission.SetActive(true);
			yield return new WaitForSeconds(1f);
			this.stars.SetActive(true);
			yield return new WaitForSeconds(1f);
			this.mission.SetActive(false);
			Scripts.interfaceScript.overlayPanel.transform.Find("BarMiddle").gameObject.SetActive(false);
			this.status.SetActive(true);
			Scripts.audioManager.PlaySFX("Go");
			yield return new WaitForSeconds(1f);
			this.status.SetActive(false);
		}
		base.gameObject.SetActive(false);
		this.interfaceScript.interfacePanel.SetActive(true);
		Debug.Log("Startsequence ended!");
		yield break;
	}

	// Token: 0x04000617 RID: 1559
	public InterfaceScript interfaceScript;

	// Token: 0x04000618 RID: 1560
	public UILabel startMissionLabel;

	// Token: 0x04000619 RID: 1561
	public UILabel pauseMissionLabel;

	// Token: 0x0400061A RID: 1562
	public GameObject mission;

	// Token: 0x0400061B RID: 1563
	public GameObject status;

	// Token: 0x0400061C RID: 1564
	public GameObject stars;

	// Token: 0x0400061D RID: 1565
	public GameObject star1;

	// Token: 0x0400061E RID: 1566
	public GameObject star2;

	// Token: 0x0400061F RID: 1567
	public GameObject star3;

	// Token: 0x04000620 RID: 1568
	public GameObject pauseStars;

	// Token: 0x04000621 RID: 1569
	public GameObject pauseStar1;

	// Token: 0x04000622 RID: 1570
	public GameObject pauseStar2;

	// Token: 0x04000623 RID: 1571
	public GameObject pauseStar3;
}
