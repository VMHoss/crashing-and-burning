using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000AD RID: 173
public class MenuPanel : MonoBehaviour
{
	// Token: 0x0600055C RID: 1372 RVA: 0x000262E8 File Offset: 0x000244E8
	private void Start()
	{
		Data.firstRun = false;
	}

	// Token: 0x0600055D RID: 1373 RVA: 0x000262F0 File Offset: 0x000244F0
	private void OnEnable()
	{
		Scripts.interfaceScript.shopOriginPanel = base.gameObject.name;
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
		Scripts.interfaceScript.overlayPanel.transform.Find("BarTop").gameObject.SetActive(true);
		Scripts.interfaceScript.overlayPanel.transform.Find("BarBottom").gameObject.SetActive(true);
	}

	// Token: 0x0600055E RID: 1374 RVA: 0x000263B4 File Offset: 0x000245B4
	private void OnDisable()
	{
		Scripts.interfaceScript.overlayPanel.transform.Find("BarTop").gameObject.SetActive(false);
		Scripts.interfaceScript.overlayPanel.transform.Find("BarBottom").gameObject.SetActive(false);
	}

	// Token: 0x0600055F RID: 1375 RVA: 0x0002640C File Offset: 0x0002460C
	public void OnMenuPanel()
	{
		GameData.showMission = true;
		base.StartCoroutine(this.MenuSequence());
	}

	// Token: 0x06000560 RID: 1376 RVA: 0x00026424 File Offset: 0x00024624
	private IEnumerator MenuSequence()
	{
		Scripts.audioManager.StopAllMusic();
		GenericFunctionsScript.Fade("FromBlackMenu");
		Scripts.audioManager.PlayMusic("Menu", Data.Shared["Misc"].d["MusicVolume"].f, -1);
		yield return new WaitForSeconds(1f);
		GenericFunctionsScript.Fade("FromWhiteToZero");
		Scripts.audioManager.PlaySFX("MenuFlame");
		this.logo.SetActive(true);
		this.logo.GetComponent<TweenScale>().Reset();
		this.logo.GetComponent<TweenScale>().Play(true);
		this.logo.GetComponent<TweenColor>().Reset();
		this.logo.GetComponent<TweenColor>().Play(true);
		this.logoBar.SetActive(true);
		this.logoBar.GetComponent<TweenScale>().Reset();
		this.logoBar.GetComponent<TweenScale>().Play(true);
		if (GameData.numPlays > 1)
		{
			ChartBoostScript.ShowInterstitial("StartUp");
		}
		yield return new WaitForSeconds(1.6f);
		base.StartCoroutine(this.StarSequence());
		this.logoCars.SetActive(true);
		this.logoCars.GetComponent<TweenScale>().Reset();
		this.logoCars.GetComponent<TweenScale>().Play(true);
		this.logoCars.GetComponent<TweenColor>().Reset();
		this.logoCars.GetComponent<TweenColor>().Play(true);
		this.logoCars2.SetActive(true);
		this.logoCars2.GetComponent<TweenScale>().Reset();
		this.logoCars2.GetComponent<TweenScale>().Play(true);
		this.logoCars2.GetComponent<TweenColor>().Reset();
		this.logoCars2.GetComponent<TweenColor>().Play(true);
		Scripts.audioManager.PlaySFX("Interface/KillCam");
		this.menuButtons.SetActive(true);
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
		this.branding.SetActive(true);
		this.awardGames.SetActive(Data.branding == "Miniclip");
		this.smallPrint.SetActive(true);
		this.optionsPanel.SetActive(true);
		Scripts.interfaceScript.UpdateInterfacePlatform();
		yield break;
	}

	// Token: 0x06000561 RID: 1377 RVA: 0x00026440 File Offset: 0x00024640
	private IEnumerator StarSequence()
	{
		for (int loops = 0; loops < 99; loops++)
		{
			for (int i = 0; i < this.positions.Length; i++)
			{
				this._pos = this.positions[i];
				this._scale = this.sizes[i];
				this.StarParticle(this._pos, this._scale);
				yield return new WaitForSeconds(0.5f);
			}
		}
		yield break;
	}

	// Token: 0x06000562 RID: 1378 RVA: 0x0002645C File Offset: 0x0002465C
	public void StarParticle(Vector3 pos, float scale)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(this.starParticlePrefab) as GameObject;
		GameObject gameObject2 = base.gameObject;
		Transform component = gameObject.GetComponent<Transform>();
		component.parent = gameObject2.transform;
		component.localPosition = pos;
		gameObject.GetComponent<UISprite>().depth = 10;
		gameObject.GetComponent<TweenScale>().to = new Vector3(scale * 90f, scale * 90f, 1f);
	}

	// Token: 0x04000582 RID: 1410
	public GameObject awardGames;

	// Token: 0x04000583 RID: 1411
	public GameObject logo;

	// Token: 0x04000584 RID: 1412
	public GameObject logoCars;

	// Token: 0x04000585 RID: 1413
	public GameObject logoCars2;

	// Token: 0x04000586 RID: 1414
	public GameObject logoBar;

	// Token: 0x04000587 RID: 1415
	public GameObject menuButtons;

	// Token: 0x04000588 RID: 1416
	public GameObject iconShopNew;

	// Token: 0x04000589 RID: 1417
	public GameObject languageSelector;

	// Token: 0x0400058A RID: 1418
	public GameObject smallPrint;

	// Token: 0x0400058B RID: 1419
	public GameObject branding;

	// Token: 0x0400058C RID: 1420
	public GameObject optionsPanel;

	// Token: 0x0400058D RID: 1421
	public GameObject menuPanel;

	// Token: 0x0400058E RID: 1422
	public GameObject starParticlePrefab;

	// Token: 0x0400058F RID: 1423
	public Vector3[] positions;

	// Token: 0x04000590 RID: 1424
	public float[] sizes;

	// Token: 0x04000591 RID: 1425
	private Vector3 _pos;

	// Token: 0x04000592 RID: 1426
	private float _scale;
}
