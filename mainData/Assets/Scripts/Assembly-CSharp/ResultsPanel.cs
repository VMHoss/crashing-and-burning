using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000B5 RID: 181
public class ResultsPanel : MonoBehaviour
{
	// Token: 0x06000584 RID: 1412 RVA: 0x0002724C File Offset: 0x0002544C
	private void OnEnable()
	{
		GameData.showMission = true;
		Scripts.interfaceScript.shopOriginPanel = base.gameObject.name;
		Scripts.interfaceScript.overlayPanel.transform.Find("BarMiddle").gameObject.SetActive(true);
		Scripts.interfaceScript.overlayPanel.transform.Find("BarTop").gameObject.SetActive(true);
		Scripts.interfaceScript.overlayPanel.transform.Find("BarBottom").gameObject.SetActive(true);
	}

	// Token: 0x06000585 RID: 1413 RVA: 0x000272E0 File Offset: 0x000254E0
	private void OnDisable()
	{
		Scripts.interfaceScript.overlayPanel.transform.Find("BarTop").gameObject.SetActive(false);
		Scripts.interfaceScript.overlayPanel.transform.Find("BarMiddle").gameObject.SetActive(false);
		Scripts.interfaceScript.overlayPanel.transform.Find("BarBottom").gameObject.SetActive(false);
	}

	// Token: 0x06000586 RID: 1414 RVA: 0x0002735C File Offset: 0x0002555C
	private void Awake()
	{
		this.localization = GameObject.Find("Localization").GetComponent<Localization>();
	}

	// Token: 0x06000587 RID: 1415 RVA: 0x00027374 File Offset: 0x00025574
	private void Start()
	{
		this.OnResultsPanel();
	}

	// Token: 0x06000588 RID: 1416 RVA: 0x0002737C File Offset: 0x0002557C
	public void OnResultsPanel()
	{
		Debug.Log("Results current level: " + GameData.playerLevel.ToString());
		this.resultsMissionLabel.text = this.interfaceScript.localization.Get(GameData.mainMission.missionName + "Text");
		if (GameData.mainMission.xp == 1)
		{
			this.star1.SetActive(true);
		}
		if (GameData.mainMission.xp == 2)
		{
			this.star2.SetActive(true);
		}
		if (GameData.mainMission.xp == 3)
		{
			this.star3.SetActive(true);
		}
		this.resultsCoins.transform.Find("CoinsValue").GetComponent<UILabel>().text = GameData.cash.ToString();
		Missions.UpdateMissions();
		base.StartCoroutine(this.ResultsSequence());
	}

	// Token: 0x06000589 RID: 1417 RVA: 0x00027460 File Offset: 0x00025660
	private IEnumerator ResultsSequence()
	{
		Debug.Log("Resultssequence!started! Xp: " + GameData.mainMission.xp.ToString());
		Scripts.audioManager.StopAllMusic();
		this.mission.SetActive(true);
		yield return new WaitForSeconds(1f);
		Scripts.audioManager.PlayMusic("Results", Data.Shared["Misc"].d["MusicVolume"].f, -1);
		this.stars.SetActive(true);
		yield return new WaitForSeconds(0.4f * (float)GameData.mainMission.xp);
		Scripts.audioManager.PlaySFX("Interface/XPMeter");
		this.stars.SetActive(false);
		this.levels.SetActive(true);
		yield return new WaitForSeconds(0.25f);
		ChartBoostScript.ShowInterstitial("MissionCompleted");
		Scripts.audioManager.PlaySFX("PickUpSounds/PickUpCashBig");
		this.resultsCoins.SetActive(true);
		yield return new WaitForSeconds(0.25f);
		this.resultsButtons.SetActive(true);
		if (GameData.levelUpRewards.Count > 0)
		{
			Debug.Log("There are still new items in the shop!");
			this.iconShopNew.SetActive(true);
			this.iconShopNew.GetComponent<TweenColor>().Reset();
			this.iconShopNew.GetComponent<TweenColor>().Play(true);
		}
		else
		{
			this.iconShopNew.SetActive(false);
		}
		Scripts.audioManager.PlaySFX("Interface/StartGame");
		this.interfaceScript.optionsPanel.SetActive(true);
		this.branding.SetActive(true);
		if (GameData.miniclipManager != null)
		{
			GameData.miniclipManager.submitScore(GameData.cash);
		}
		yield break;
	}

	// Token: 0x040005CD RID: 1485
	public InterfaceScript interfaceScript;

	// Token: 0x040005CE RID: 1486
	public UILabel resultsMissionLabel;

	// Token: 0x040005CF RID: 1487
	public GameObject resultsButtons;

	// Token: 0x040005D0 RID: 1488
	public GameObject levels;

	// Token: 0x040005D1 RID: 1489
	public GameObject itemUnlocked;

	// Token: 0x040005D2 RID: 1490
	public GameObject stars;

	// Token: 0x040005D3 RID: 1491
	public GameObject star1;

	// Token: 0x040005D4 RID: 1492
	public GameObject star2;

	// Token: 0x040005D5 RID: 1493
	public GameObject star3;

	// Token: 0x040005D6 RID: 1494
	public GameObject mission;

	// Token: 0x040005D7 RID: 1495
	public GameObject branding;

	// Token: 0x040005D8 RID: 1496
	public GameObject iconShopNew;

	// Token: 0x040005D9 RID: 1497
	public GameObject resultsCoins;

	// Token: 0x040005DA RID: 1498
	public Localization localization;
}
