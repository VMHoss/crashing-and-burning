using System;
using UnityEngine;

// Token: 0x02000119 RID: 281
public class StrikerRocketScript : ProjectileScript
{
	// Token: 0x06000808 RID: 2056 RVA: 0x0003CB8C File Offset: 0x0003AD8C
	protected override void StartSpecific()
	{
		base.gameObject.AddComponent<Rigidbody>();
		base.rigidbody.velocity = base.transform.up * this.pSpeed + new Vector3(0f, 20f, 0f) + UnityEngine.Random.insideUnitSphere * 2f;
		base.gameObject.AddComponent<ConstantForce>().force = new Vector3(0f, -50f, 0f);
		this.pTrailScript = TrailScript.AddTrail(base.gameObject, TrailScript.Normal.Z);
		UnityEngine.Object.Destroy(base.gameObject, 3f);
		UnityEngine.Object.Destroy(this.pTrailScript, 4f);
		if (Data.platform != "PC")
		{
			UnityEngine.Object.Destroy(base.transform.GetChild(0).gameObject);
		}
		this.pFixRot = Quaternion.Euler(90f, 0f, 0f);
	}

	// Token: 0x06000809 RID: 2057 RVA: 0x0003CC90 File Offset: 0x0003AE90
	protected override void UpdateSpecific()
	{
		Vector3 velocity = base.rigidbody.velocity;
		base.transform.rotation = Quaternion.LookRotation(velocity) * this.pFixRot;
		float distance = velocity.magnitude * Time.deltaTime;
		if (Physics.Raycast(base.transform.position, velocity, distance, this.pCastMask))
		{
			base.Explode();
		}
	}

	// Token: 0x0600080A RID: 2058 RVA: 0x0003CCF8 File Offset: 0x0003AEF8
	protected override void DestroySpecific()
	{
		UnityEngine.Object.Destroy(this.pTrailScript.gameObject, 1f);
	}

	// Token: 0x04000886 RID: 2182
	private TrailScript pTrailScript;

	// Token: 0x04000887 RID: 2183
	private Quaternion pFixRot;
}
