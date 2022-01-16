using System;
using UnityEngine;

// Token: 0x0200010D RID: 269
public class ObjectScript : MonoBehaviour
{
	// Token: 0x060007D9 RID: 2009 RVA: 0x0003B528 File Offset: 0x00039728
	private void OnCollisionStay(Collision aCollision)
	{
		if (aCollision.rigidbody != null)
		{
			base.rigidbody.constraints = RigidbodyConstraints.None;
			UnityEngine.Object.Destroy(this);
		}
	}
}
