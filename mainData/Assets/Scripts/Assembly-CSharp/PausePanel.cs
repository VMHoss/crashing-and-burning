using System;
using UnityEngine;

// Token: 0x020000B2 RID: 178
public class PausePanel : MonoBehaviour
{
	// Token: 0x0600057A RID: 1402 RVA: 0x00026FE8 File Offset: 0x000251E8
	private void OnEnable()
	{
		Scripts.interfaceScript.overlayPanel.transform.Find("BarTop").gameObject.SetActive(true);
		Scripts.interfaceScript.overlayPanel.transform.Find("BarMiddle").gameObject.SetActive(true);
		Scripts.interfaceScript.overlayPanel.transform.Find("BarBottom").gameObject.SetActive(true);
		Scripts.interfaceScript.overlayPanel.transform.Find("Background").gameObject.SetActive(true);
	}

	// Token: 0x0600057B RID: 1403 RVA: 0x00027088 File Offset: 0x00025288
	private void OnDisable()
	{
		Scripts.interfaceScript.overlayPanel.transform.Find("BarTop").gameObject.SetActive(false);
		Scripts.interfaceScript.overlayPanel.transform.Find("BarMiddle").gameObject.SetActive(false);
		Scripts.interfaceScript.overlayPanel.transform.Find("BarBottom").gameObject.SetActive(false);
		Scripts.interfaceScript.overlayPanel.transform.Find("Background").gameObject.SetActive(false);
	}

	// Token: 0x0600057C RID: 1404 RVA: 0x00027128 File Offset: 0x00025328
	private void Start()
	{
	}

	// Token: 0x0600057D RID: 1405 RVA: 0x0002712C File Offset: 0x0002532C
	private void Update()
	{
	}
}
