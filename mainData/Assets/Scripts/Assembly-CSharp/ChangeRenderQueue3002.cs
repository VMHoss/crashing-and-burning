using System;
using UnityEngine;

// Token: 0x0200014C RID: 332
public class ChangeRenderQueue3002 : MonoBehaviour
{
	// Token: 0x0600099D RID: 2461 RVA: 0x0004B570 File Offset: 0x00049770
	private void Start()
	{
		base.renderer.sharedMaterial.renderQueue = 3002;
	}
}
