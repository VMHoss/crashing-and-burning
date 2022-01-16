using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000AB RID: 171
public class MedalsOverviewPanel : MonoBehaviour
{
	// Token: 0x06000553 RID: 1363 RVA: 0x00025E38 File Offset: 0x00024038
	private void OnEnable()
	{
		this.OnMedalsPanel();
		Scripts.interfaceScript.overlayPanel.transform.Find("BarTop").gameObject.SetActive(true);
		Scripts.interfaceScript.overlayPanel.transform.Find("BarBottom").gameObject.SetActive(true);
	}

	// Token: 0x06000554 RID: 1364 RVA: 0x00025E94 File Offset: 0x00024094
	private void OnDisable()
	{
	}

	// Token: 0x06000555 RID: 1365 RVA: 0x00025E98 File Offset: 0x00024098
	public void OnMedalsPanel()
	{
		base.gameObject.SetActive(true);
		GameObject[] array = GameObject.FindGameObjectsWithTag("PanelDynamic");
		foreach (GameObject obj in array)
		{
			UnityEngine.Object.Destroy(obj);
		}
		base.StartCoroutine(this.MedalsOverviewSequence());
	}

	// Token: 0x06000556 RID: 1366 RVA: 0x00025EEC File Offset: 0x000240EC
	private IEnumerator MedalsOverviewSequence()
	{
		Vector3 _pos = default(Vector3);
		int rowSpacing = 74;
		this.localization = GameObject.Find("Localization").GetComponent<Localization>();
		for (int i = 0; i < 10; i++)
		{
			if (i < 5)
			{
				_pos.x = -430f;
				_pos.y = 180f - (float)(i * rowSpacing);
			}
			if (i >= 5)
			{
				_pos.x = 30f;
				_pos.y = 180f - (float)((i - 5) * rowSpacing);
			}
			GameObject _clone = UnityEngine.Object.Instantiate(this.trophyOverviewPrefab, base.transform.position, base.transform.rotation) as GameObject;
			Transform _t = _clone.GetComponent<Transform>();
			_t.parent = this.trophiesOverview.transform;
			_t.localScale = new Vector3(1f, 1f, 1f);
			_t.localPosition = _pos;
			_clone.name = "TrophyOverview" + (i + 1).ToString();
			_clone.transform.Find("TrophyHeader").GetComponent<UILabel>().text = this.localization.Get("Trophy" + (i + 1).ToString() + "HeaderText");
			_clone.transform.Find("TrophyDescription").GetComponent<UILabel>().text = this.localization.Get("Trophy" + (i + 1).ToString() + "DescriptionText");
			_clone.transform.Find("TrophyProgress").GetComponent<UILabel>().text = this.localization.Get("ProgressText") + " " + Scripts.medalsManager.GetMedalProgression(i + 1);
			if (Scripts.medalsManager.IsMedalObtained(i + 1))
			{
				Scripts.audioManager.PlaySFX("Interface/Trophy");
				_clone.transform.Find("TrophyIcon").gameObject.SetActive(true);
			}
			else
			{
				_clone.transform.Find("TrophyIcon").gameObject.SetActive(false);
			}
			yield return new WaitForSeconds(0.1f);
		}
		yield break;
	}

	// Token: 0x04000576 RID: 1398
	public Localization localization;

	// Token: 0x04000577 RID: 1399
	public GameObject trophiesOverview;

	// Token: 0x04000578 RID: 1400
	public GameObject trophyOverviewPrefab;
}
