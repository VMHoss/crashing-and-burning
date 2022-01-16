using System;
using UnityEngine;

// Token: 0x020000A5 RID: 165
public class HowToPlayPanel : MonoBehaviour
{
	// Token: 0x0600052A RID: 1322 RVA: 0x00024918 File Offset: 0x00022B18
	private void OnEnable()
	{
		base.gameObject.transform.Find("ControlsHeader").gameObject.SetActive(Data.platform == "PC");
	}

	// Token: 0x0600052B RID: 1323 RVA: 0x00024954 File Offset: 0x00022B54
	private void Start()
	{
	}

	// Token: 0x0600052C RID: 1324 RVA: 0x00024958 File Offset: 0x00022B58
	private void Update()
	{
	}
}
