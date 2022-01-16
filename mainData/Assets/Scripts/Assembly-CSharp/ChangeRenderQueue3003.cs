using System;
using UnityEngine;

// Token: 0x0200014D RID: 333
public class ChangeRenderQueue3003 : MonoBehaviour
{
	// Token: 0x0600099F RID: 2463 RVA: 0x0004B590 File Offset: 0x00049790
	private void Start()
	{
		base.renderer.sharedMaterial.renderQueue = 3003;
	}
}
