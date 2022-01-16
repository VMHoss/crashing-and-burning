using System;
using UnityEngine;

// Token: 0x02000133 RID: 307
public class TrafficFireScript : MonoBehaviour
{
	// Token: 0x060008E0 RID: 2272 RVA: 0x0004209C File Offset: 0x0004029C
	public void Awake()
	{
		this.pPS = Loader.LoadGameObject("Shared", "CarExplosions/CarFire_PS");
		UnityEngine.Object.Destroy(this.pPS, 5f);
		UnityEngine.Object.Destroy(this, 5f);
	}

	// Token: 0x060008E1 RID: 2273 RVA: 0x000420DC File Offset: 0x000402DC
	private void Update()
	{
		this.pPS.transform.position = base.transform.position + new Vector3(0f, 1f, 0f);
	}

	// Token: 0x04000905 RID: 2309
	private GameObject pPS;
}
