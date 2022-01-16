using System;
using UnityEngine;

// Token: 0x020000C5 RID: 197
public class RotatorScript : MonoBehaviour
{
	// Token: 0x060005F7 RID: 1527 RVA: 0x0002AEF0 File Offset: 0x000290F0
	private void Update()
	{
		base.transform.RotateAround(this.rotateAround, this.anglePerSec * Time.deltaTime * 0.017453292f);
	}

	// Token: 0x04000683 RID: 1667
	public float anglePerSec = 10f;

	// Token: 0x04000684 RID: 1668
	public Vector3 rotateAround = Vector3.up;
}
