using System;
using UnityEngine;

// Token: 0x02000126 RID: 294
public class DiabloCometScript : MonoBehaviour
{
	// Token: 0x0600087B RID: 2171 RVA: 0x0003ECDC File Offset: 0x0003CEDC
	private void Awake()
	{
		this.pMask = (1 << GameData.defaultLayer | 1 << GameData.trafficLayer | 1 << GameData.damagedTrafficLayer);
	}

	// Token: 0x0600087C RID: 2172 RVA: 0x0003ED10 File Offset: 0x0003CF10
	private void Update()
	{
		if (Data.pause)
		{
			return;
		}
		float num = this.pSpeed * Time.deltaTime;
		RaycastHit raycastHit;
		if (Physics.Raycast(base.transform.position, this.direction, out raycastHit, num, this.pMask))
		{
			ExplosionScript.AddExplosion("DiabloExplosion", raycastHit.point + raycastHit.normal);
			UnityEngine.Object.Destroy(base.gameObject);
		}
		else
		{
			base.transform.position += num * this.direction;
		}
	}

	// Token: 0x040008BA RID: 2234
	public Vector3 direction = Vector3.down;

	// Token: 0x040008BB RID: 2235
	private float pSpeed = 1f;

	// Token: 0x040008BC RID: 2236
	private int pMask;
}
