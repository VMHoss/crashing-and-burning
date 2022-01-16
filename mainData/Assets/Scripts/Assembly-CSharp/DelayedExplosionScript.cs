using System;
using UnityEngine;

// Token: 0x020000E7 RID: 231
public class DelayedExplosionScript : MonoBehaviour
{
	// Token: 0x06000702 RID: 1794 RVA: 0x0003337C File Offset: 0x0003157C
	public void Initialize(string anExplosion, float aDelayTime)
	{
		this.pExplosion = anExplosion;
		base.Invoke("Explode", aDelayTime);
	}

	// Token: 0x06000703 RID: 1795 RVA: 0x00033394 File Offset: 0x00031594
	private void Explode()
	{
		ExplosionScript.AddExplosion(this.pExplosion, base.transform.position);
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x0400072B RID: 1835
	private string pExplosion;
}
