using System;
using UnityEngine;

// Token: 0x0200009A RID: 154
public class AutoDestruct : MonoBehaviour
{
	// Token: 0x060004F6 RID: 1270 RVA: 0x00023D8C File Offset: 0x00021F8C
	private void Start()
	{
		UnityEngine.Object.Destroy(base.gameObject, 2f);
	}
}
