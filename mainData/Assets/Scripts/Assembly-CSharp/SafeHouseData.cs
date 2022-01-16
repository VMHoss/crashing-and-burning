using System;
using UnityEngine;

// Token: 0x0200011A RID: 282
public class SafeHouseData
{
	// Token: 0x0600080B RID: 2059 RVA: 0x0003CD10 File Offset: 0x0003AF10
	public SafeHouseData(Vector3 aPosition, float aYAngle, int aNumber)
	{
		this.position = aPosition;
		this.yAngle = aYAngle;
		this.number = aNumber;
	}

	// Token: 0x04000888 RID: 2184
	public readonly Vector3 position;

	// Token: 0x04000889 RID: 2185
	public readonly float yAngle;

	// Token: 0x0400088A RID: 2186
	public readonly int number;
}
