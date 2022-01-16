using System;
using UnityEngine;

// Token: 0x020000E9 RID: 233
public class ExplosionRateScript : MonoBehaviour
{
	// Token: 0x0600070E RID: 1806 RVA: 0x00033C68 File Offset: 0x00031E68
	private void Awake()
	{
		this.pParticleSystem = base.gameObject.GetComponent<ParticleSystem>();
		if (this.pParticleSystem == null)
		{
			throw new UnityException("Error, no particle system is attached in game object " + base.gameObject.name);
		}
	}

	// Token: 0x0600070F RID: 1807 RVA: 0x00033CB4 File Offset: 0x00031EB4
	private void Update()
	{
		if (Data.pause)
		{
			return;
		}
		if (this.pParticleSystem.loop)
		{
			this.pParticleSystem.emissionRate = Mathf.Clamp(this.attachedObject.rigidbody.velocity.magnitude, 50f, 100f);
			this.duration -= Time.deltaTime / Time.timeScale;
			if (this.duration < 0f)
			{
				this.pParticleSystem.loop = false;
				UnityEngine.Object.Destroy(base.gameObject, Data.Shared["Misc"].d["ParticleSysDestroyTime"].f);
			}
		}
	}

	// Token: 0x04000742 RID: 1858
	public GameObject attachedObject;

	// Token: 0x04000743 RID: 1859
	public float duration = 1f;

	// Token: 0x04000744 RID: 1860
	private ParticleSystem pParticleSystem;
}
