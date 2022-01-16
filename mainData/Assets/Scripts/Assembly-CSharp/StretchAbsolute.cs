using System;
using UnityEngine;

// Token: 0x020000B9 RID: 185
public class StretchAbsolute : MonoBehaviour
{
	// Token: 0x060005A7 RID: 1447 RVA: 0x00029474 File Offset: 0x00027674
	private void Awake()
	{
		this.interfaceScript = GameObject.Find("Interface").GetComponent<InterfaceScript>();
	}

	// Token: 0x060005A8 RID: 1448 RVA: 0x0002948C File Offset: 0x0002768C
	private void Start()
	{
		this.panelSize = this.interfaceScript.panelSize;
		base.transform.localScale = new Vector3((float)Screen.width * 1f / this.panelSize, (float)Screen.height * 1f / this.panelSize, 1f);
	}

	// Token: 0x060005A9 RID: 1449 RVA: 0x000294E8 File Offset: 0x000276E8
	private void Update()
	{
		this.panelSize = this.interfaceScript.panelSize;
		base.transform.localScale = new Vector3((float)Screen.width, (float)Screen.height, 1f) * 1f / this.panelSize * 1.1f;
	}

	// Token: 0x04000624 RID: 1572
	private InterfaceScript interfaceScript;

	// Token: 0x04000625 RID: 1573
	private float panelSize;
}
