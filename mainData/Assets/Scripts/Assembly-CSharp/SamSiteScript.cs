using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200011C RID: 284
public class SamSiteScript : MonoBehaviour
{
	// Token: 0x06000812 RID: 2066 RVA: 0x0003CEE4 File Offset: 0x0003B0E4
	public void Initialize(DestructibleScript aDestructibleScript)
	{
		this.pTurret = base.transform.Find("Object_SamSiteTurret");
		this.pFireDummies = new List<Transform>();
		for (int i = 1; i <= 6; i++)
		{
			this.pFireDummies.Add(this.pTurret.Find("FireDummy" + i));
		}
		this.pRedLight = base.transform.Find("Light");
		this.pPlayerTransform = GameData.playerCarScript.transform;
	}

	// Token: 0x06000813 RID: 2067 RVA: 0x0003CF70 File Offset: 0x0003B170
	public void Activate()
	{
		this.pRedLight.gameObject.SetActive(true);
		if (!base.enabled)
		{
			base.enabled = true;
		}
	}

	// Token: 0x06000814 RID: 2068 RVA: 0x0003CFA0 File Offset: 0x0003B1A0
	public void GotDestroyed()
	{
		this.pRedLight.gameObject.SetActive(false);
	}

	// Token: 0x06000815 RID: 2069 RVA: 0x0003CFB4 File Offset: 0x0003B1B4
	private void Update()
	{
		if (Data.pause || !Data.raceInProgress)
		{
			return;
		}
		bool flag = false;
		if (this.pPlayerTransform != null)
		{
			this.pToPlayerVec = this.pPlayerTransform.position - this.pTurret.position;
			Vector3 vector = this.pToPlayerVec;
			vector.y = 0f;
			if (vector.sqrMagnitude < 10000f)
			{
				Vector3 up = this.pTurret.up;
				up.y = 0f;
				float num = Vector3.Angle(up, vector);
				if (num > 2f)
				{
					if (Vector3.Dot(this.pTurret.right, vector) < 0f)
					{
						this.pCurYAngle += Mathf.Min(2f, 60f * Time.deltaTime);
					}
					else
					{
						this.pCurYAngle -= Mathf.Min(2f, 60f * Time.deltaTime);
					}
					this.pTurret.rotation = Quaternion.Euler(-90f, this.pCurYAngle, 0f);
				}
				else
				{
					flag = true;
				}
			}
			if (flag && !GameData.playerCarScript.IsDestroyed())
			{
				this.pFireRateTimer -= Time.deltaTime;
				if (this.pFireRateTimer <= 0f)
				{
					if ((this.pPlayerTransform.position - base.transform.position).sqrMagnitude < 10000f)
					{
						this.Fire();
					}
					this.pFireRateTimer = 2f;
				}
			}
		}
	}

	// Token: 0x06000816 RID: 2070 RVA: 0x0003D15C File Offset: 0x0003B35C
	private void Fire()
	{
		if (Mathf.Abs(this.pPlayerTransform.position.y - base.transform.position.y) > 30f)
		{
			return;
		}
		Transform transform = this.pFireDummies[this.pFireNextFireDummy];
		GameObject gameObject = Loader.LoadGameObject("Shared", "Effects/SamSiteFire_PS");
		gameObject.transform.parent = transform;
		gameObject.transform.localPosition = Vector3.zero;
		gameObject.transform.localRotation = this.pRotY180;
		UnityEngine.Object.Destroy(gameObject, gameObject.particleSystem.duration);
		Scripts.audioManager.PlaySFX("Weapons/RocketLauncher", 1f, 0, base.gameObject);
		Projectiles.CreateProjectile("SamSiteRocket", transform.position, Quaternion.LookRotation(this.pPlayerTransform.position + new Vector3(0f, 0.5f, 0f) - transform.position) * this.pRotX90, 0f);
		this.pFireNextFireDummy = ++this.pFireNextFireDummy % this.pFireDummies.Count;
	}

	// Token: 0x0400088D RID: 2189
	private const float pConstFireRate = 2f;

	// Token: 0x0400088E RID: 2190
	private const float pFireRange = 100f;

	// Token: 0x0400088F RID: 2191
	private const float pFireRangeSQR = 10000f;

	// Token: 0x04000890 RID: 2192
	private Transform pTurret;

	// Token: 0x04000891 RID: 2193
	private List<Transform> pFireDummies;

	// Token: 0x04000892 RID: 2194
	private Transform pRedLight;

	// Token: 0x04000893 RID: 2195
	private float pFireRateTimer = 2f;

	// Token: 0x04000894 RID: 2196
	private float pCurYAngle;

	// Token: 0x04000895 RID: 2197
	private int pFireNextFireDummy;

	// Token: 0x04000896 RID: 2198
	private Transform pPlayerTransform;

	// Token: 0x04000897 RID: 2199
	private Vector3 pToPlayerVec = Vector3.zero;

	// Token: 0x04000898 RID: 2200
	private Quaternion pRotX90 = Quaternion.Euler(90f, 0f, 0f);

	// Token: 0x04000899 RID: 2201
	private Quaternion pRotY180 = Quaternion.Euler(0f, 180f, 0f);
}
