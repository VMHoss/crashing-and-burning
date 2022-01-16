using System;
using UnityEngine;

// Token: 0x020000AC RID: 172
public class MedalsScript : MonoBehaviour
{
	// Token: 0x06000558 RID: 1368 RVA: 0x00025F10 File Offset: 0x00024110
	private void Awake()
	{
		for (int i = 1; i < 6; i++)
		{
			this.parent = GameObject.Find("Medals");
			this.clone = (UnityEngine.Object.Instantiate(this.medalPrefab) as GameObject);
			this._t = this.clone.GetComponent<Transform>();
			this.clone.layer = this.parent.layer;
			this.clone.name = "Medal" + i.ToString();
			this._t.parent = this.parent.transform;
			this._t.localScale = new Vector3(1f, 1f, 1f);
			this._pos.x = -350f;
			this._pos.y = (float)(140 - i * 50);
			this._t.localPosition = this._pos;
			this._headerLocalize = this.clone.transform.Find("MedalHeader").GetComponent<UILocalize>();
			this._descriptionLocalize = this.clone.transform.Find("MedalDescription").GetComponent<UILocalize>();
			this._headerLocalize.key = "Medal" + i.ToString() + "HeaderText";
			this._descriptionLocalize.key = "Medal" + i.ToString() + "DescriptionText";
		}
		for (int j = 6; j < 11; j++)
		{
			this.parent = GameObject.Find("Medals");
			this.clone = (UnityEngine.Object.Instantiate(this.medalPrefab) as GameObject);
			this._t = this.clone.GetComponent<Transform>();
			this.clone.layer = this.parent.layer;
			this.clone.name = "Medal" + j.ToString();
			this._t.parent = this.parent.transform;
			this._t.localScale = new Vector3(1f, 1f, 1f);
			this._pos.x = 50f;
			this._pos.y = (float)(140 - (j - 5) * 50);
			this._t.localPosition = this._pos;
			this._headerLocalize = this.clone.transform.Find("MedalHeader").GetComponent<UILocalize>();
			this._descriptionLocalize = this.clone.transform.Find("MedalDescription").GetComponent<UILocalize>();
			this._headerLocalize.key = "Medal" + j.ToString() + "HeaderText";
			this._descriptionLocalize.key = "Medal" + j.ToString() + "DescriptionText";
		}
	}

	// Token: 0x06000559 RID: 1369 RVA: 0x000261F4 File Offset: 0x000243F4
	public void UpdateMedals()
	{
		for (int i = 1; i < 11; i++)
		{
			GameObject gameObject = GameObject.Find("Medal" + i.ToString());
			if (Scripts.medalsManager.IsMedalObtained(i))
			{
				gameObject.transform.Find("MedalImage").GetComponent<UISprite>().spriteName = "MedalOn";
				gameObject.transform.Find("MedalHeader").GetComponent<UILabel>().color = Color.white;
				gameObject.transform.Find("MedalDescription").GetComponent<UILabel>().color = Color.white;
				gameObject.transform.Find("MedalValue").GetComponent<UILabel>().color = Color.white;
			}
			gameObject.transform.Find("MedalValue").GetComponent<UILabel>().text = Scripts.medalsManager.GetMedalProgression(i);
		}
	}

	// Token: 0x0600055A RID: 1370 RVA: 0x000262DC File Offset: 0x000244DC
	private void Update()
	{
	}

	// Token: 0x04000579 RID: 1401
	public GameObject medalPrefab;

	// Token: 0x0400057A RID: 1402
	private GameObject parent;

	// Token: 0x0400057B RID: 1403
	private GameObject clone;

	// Token: 0x0400057C RID: 1404
	private Vector3 _pos;

	// Token: 0x0400057D RID: 1405
	private Transform _t;

	// Token: 0x0400057E RID: 1406
	private UILocalize _headerLocalize;

	// Token: 0x0400057F RID: 1407
	private UILocalize _descriptionLocalize;

	// Token: 0x04000580 RID: 1408
	private UILabel _valueLabel;

	// Token: 0x04000581 RID: 1409
	private string goal;
}
