using System;
using UnityEngine;

// Token: 0x020000B4 RID: 180
public class ResizeToSprite : MonoBehaviour
{
	// Token: 0x06000582 RID: 1410 RVA: 0x00027230 File Offset: 0x00025430
	private void Start()
	{
		base.transform.localScale = this.newSize;
	}

	// Token: 0x040005CC RID: 1484
	public Vector3 newSize;
}
