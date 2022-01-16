using System;
using UnityEngine;

// Token: 0x0200014B RID: 331
public class ChangeRenderQueue3001 : MonoBehaviour
{
	// Token: 0x0600099B RID: 2459 RVA: 0x0004B550 File Offset: 0x00049750
	private void Start()
	{
		base.renderer.sharedMaterial.renderQueue = 3001;
	}
}
