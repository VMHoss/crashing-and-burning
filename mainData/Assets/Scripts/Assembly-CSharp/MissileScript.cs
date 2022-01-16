using System;
using UnityEngine;

// Token: 0x02000114 RID: 276
public class MissileScript : ProjectileScript
{
	// Token: 0x060007F2 RID: 2034 RVA: 0x0003C1D8 File Offset: 0x0003A3D8
	protected override void StartSpecific()
	{
		this.pTrailScript = TrailScript.AddTrail(base.gameObject, TrailScript.Normal.X);
		if (Data.platform != "PC")
		{
			UnityEngine.Object.Destroy(base.transform.GetChild(0).gameObject);
		}
	}

	// Token: 0x060007F3 RID: 2035 RVA: 0x0003C224 File Offset: 0x0003A424
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
				this.HitSomething(aRaycastHit);
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
				if (Physics.Raycast(base.transform.position, Vector3.down, out aRaycastHit, 2f, this.pCastMask))
				{
					base.transform.position = Vector3.Lerp(base.transform.position, aRaycastHit.point + Vector3.up, 0.5f);
				}
				else
				{
					base.transform.position -= Vector3.up * 0.5f * num;
				}
			}
		}
	}

	// Token: 0x060007F4 RID: 2036 RVA: 0x0003C478 File Offset: 0x0003A678
	private void HitSomething(RaycastHit aRaycastHit)
	{
		aRaycastHit.collider.SendMessage("Damage", new DamageInfo(this.pProps["Damage"].f, aRaycastHit.point, base.transform.up), SendMessageOptions.DontRequireReceiver);
	}

	// Token: 0x060007F5 RID: 2037 RVA: 0x0003C4C4 File Offset: 0x0003A6C4
	protected override void DestroySpecific()
	{
		UnityEngine.Object.Destroy(this.pTrailScript.gameObject, 1f);
	}

	// Token: 0x04000878 RID: 2168
	private TrailScript pTrailScript;
}
