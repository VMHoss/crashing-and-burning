using System;
using UnityEngine;

// Token: 0x02000118 RID: 280
public class ShellScript : ProjectileScript
{
	// Token: 0x06000805 RID: 2053 RVA: 0x0003CAC0 File Offset: 0x0003ACC0
	protected override void StartSpecific()
	{
		this.pExplodeTimer = this.pProps["ExplodeTimer"].f;
		base.gameObject.layer = GameData.carLayer;
		base.rigidbody.velocity = base.transform.up * this.pSpeed + new Vector3(0f, 10f, 0f);
	}

	// Token: 0x06000806 RID: 2054 RVA: 0x0003CB34 File Offset: 0x0003AD34
	protected override void UpdateSpecific()
	{
		if ((double)this.pExplodeTimer > 0.0)
		{
			this.pExplodeTimer -= Time.deltaTime;
			if ((double)this.pExplodeTimer <= 0.0)
			{
				base.Explode();
			}
		}
	}

	// Token: 0x04000885 RID: 2181
	private float pExplodeTimer = 2f;
}
