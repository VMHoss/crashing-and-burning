using System;
using UnityEngine;

// Token: 0x020000A9 RID: 169
public class Logo : MonoBehaviour
{
	// Token: 0x0600054B RID: 1355 RVA: 0x00025DDC File Offset: 0x00023FDC
	private void Start()
	{
		this.logoHighlightPanel.clipRange = this.range;
	}

	// Token: 0x0600054C RID: 1356 RVA: 0x00025DF0 File Offset: 0x00023FF0
	private void Update()
	{
	}

	// Token: 0x04000570 RID: 1392
	public UIPanel logoHighlightPanel;

	// Token: 0x04000571 RID: 1393
	public Vector4 range;
}
