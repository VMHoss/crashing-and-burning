using System;
using UnityEngine;

// Token: 0x02000113 RID: 275
public class LineProjectileScript : ProjectileScript
{
	// Token: 0x060007EF RID: 2031 RVA: 0x0003C000 File Offset: 0x0003A200
	protected virtual void HitSpecific(RaycastHit aRaycastHit)
	{
	}

	// Token: 0x060007F0 RID: 2032 RVA: 0x0003C004 File Offset: 0x0003A204
	protected override void UpdateSpecific()
	{
		float num = this.pSpeed * Time.deltaTime;
		if (num > 0.001f)
		{
			RaycastHit aRaycastHit;
			if (Physics.Raycast(base.transform.position, base.transform.up, out aRaycastHit, num, this.pCastMask))
			{
				if (this.pImpactEffect != "None")
				{
					GameObject gameObject = Loader.LoadGameObject("Shared", "Effects/" + this.pImpactEffect);
					gameObject.transform.position = aRaycastHit.point;
					gameObject.transform.rotation = Quaternion.LookRotation(aRaycastHit.normal);
					UnityEngine.Object.Destroy(gameObject, gameObject.particleSystem.duration);
				}
				if (this.pImpactSound != "None")
				{
					int num2 = this.pImpactSound.IndexOf("-");
					if (num2 <= 0)
					{
						Scripts.audioManager.PlaySFX(this.pImpactSound);
					}
					else
					{
						string s = this.pImpactSound.Substring(num2 - 1, 1);
						string s2 = this.pImpactSound.Substring(num2 + 1, 1);
						int num3 = UnityEngine.Random.Range(int.Parse(s), int.Parse(s2) + 1);
						string aSFXName = this.pImpactSound.Substring(0, num2 - 1) + num3;
						Scripts.audioManager.PlaySFX(aSFXName);
					}
				}
				this.HitSpecific(aRaycastHit);
				base.Explode();
			}
			else
			{
				base.transform.position = base.transform.position + base.transform.up * num;
				this.pDistTravelled += num;
				if (this.pDistTravelled > this.pRange)
				{
					this.DestroySpecific();
					UnityEngine.Object.Destroy(base.gameObject);
				}
			}
		}
	}
}
