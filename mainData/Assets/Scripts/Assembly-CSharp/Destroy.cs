using System;
using UnityEngine;

// Token: 0x0200009E RID: 158
public class Destroy : MonoBehaviour
{
	// Token: 0x06000504 RID: 1284 RVA: 0x00024230 File Offset: 0x00022430
	public void DestroyMe()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000505 RID: 1285 RVA: 0x00024240 File Offset: 0x00022440
	public void DestroyMeDelayed()
	{
		UnityEngine.Object.Destroy(base.gameObject, 5f);
	}
}
