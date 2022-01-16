using System;
using UnityEngine;

// Token: 0x020000C1 RID: 193
public class FollowPositionScript : MonoBehaviour
{
	// Token: 0x060005E4 RID: 1508 RVA: 0x0002A69C File Offset: 0x0002889C
	public void Initialize(GameObject aFollowObject, Vector3 aRelativePosition)
	{
		this.pFollowObject = aFollowObject;
		this.pRelativePosition = aRelativePosition;
	}

	// Token: 0x060005E5 RID: 1509 RVA: 0x0002A6AC File Offset: 0x000288AC
	private void Update()
	{
		base.transform.position = this.pFollowObject.transform.position + this.pRelativePosition;
	}

	// Token: 0x04000673 RID: 1651
	private GameObject pFollowObject;

	// Token: 0x04000674 RID: 1652
	private Vector3 pRelativePosition;
}
