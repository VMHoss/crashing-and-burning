using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200010A RID: 266
public class MountedGunScript : MonoBehaviour
{
	// Token: 0x060007CE RID: 1998 RVA: 0x0003AEE0 File Offset: 0x000390E0
	public void Initialize(DestructibleScript aDestructibleScript)
	{
		this.pTurret = base.transform.Find("Object_MountedGunTurret");
		this.pFireDummies = new List<Transform>();
		this.pFireDummies.Add(this.pTurret.Find("FireDummy1"));
		this.pFireDummies.Add(this.pTurret.Find("FireDummy2"));
		this.pRedLight = this.pTurret.Find("Light");
		this.pPlayerTransform = GameData.playerCarScript.transform;
		this.pMuzzlePS = Loader.LoadGameObject("Shared", "Effects/MountedGunFlash_PS").particleSystem;
	}

	// Token: 0x060007CF RID: 1999 RVA: 0x0003AF84 File Offset: 0x00039184
	public void Activate()
	{
		this.pRedLight.gameObject.SetActive(true);
		if (!base.enabled)
		{
			base.enabled = true;
		}
		if (this.pFireSoundLoop == null)
		{
			this.pFireSoundLoop = Scripts.audioManager.PlaySFX("Weapons/MountedGun", 1f, -1, base.gameObject);
		}
		this.StopPlayingSound();
	}

	// Token: 0x060007D0 RID: 2000 RVA: 0x0003AFEC File Offset: 0x000391EC
	public void GotDestroyed()
	{
		this.pRedLight.gameObject.SetActive(false);
		this.StopPlayingSound();
	}

	// Token: 0x060007D1 RID: 2001 RVA: 0x0003B008 File Offset: 0x00039208
	private void Update()
	{
		if (!Data.raceInProgress || !Scripts.trackScript.trackManager.UpdateDestructibles())
		{
			this.StopPlayingSound();
		}
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
					this.StopPlayingSound();
				}
				else
				{
					flag = true;
				}
			}
			else
			{
				this.StopPlayingSound();
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
					this.pFireRateTimer = 0.1f;
				}
			}
		}
	}

	// Token: 0x060007D2 RID: 2002 RVA: 0x0003B1E4 File Offset: 0x000393E4
	private void StopPlayingSound()
	{
		if (this.pFireSoundLoop == null)
		{
			return;
		}
		if (this.pFireSoundLoop.isPlaying)
		{
			this.pFireSoundLoop.Stop();
			this.pFireSoundLoop.enabled = false;
		}
	}

	// Token: 0x060007D3 RID: 2003 RVA: 0x0003B220 File Offset: 0x00039420
	private void Fire()
	{
		Transform transform = this.pFireDummies[this.pFireNextFireDummy];
		this.pMuzzlePS.transform.parent = transform;
		this.pMuzzlePS.transform.localPosition = Vector3.zero;
		this.pMuzzlePS.transform.localRotation = this.pRotZ90;
		this.pMuzzlePS.Emit(1);
		if (!this.pFireSoundLoop.isPlaying)
		{
			this.pFireSoundLoop.enabled = true;
			this.pFireSoundLoop.Play();
		}
		Projectiles.CreateProjectile("MountedGunBullet", transform.position, Quaternion.LookRotation(this.pPlayerTransform.position + new Vector3(0f, 0.5f, 0f) - transform.position) * this.pRotX90, 0f);
		this.pFireNextFireDummy = ++this.pFireNextFireDummy % this.pFireDummies.Count;
	}

	// Token: 0x0400084C RID: 2124
	private const float pConstFireRate = 0.1f;

	// Token: 0x0400084D RID: 2125
	private const float pFireRange = 100f;

	// Token: 0x0400084E RID: 2126
	private const float pFireRangeSQR = 10000f;

	// Token: 0x0400084F RID: 2127
	private Transform pTurret;

	// Token: 0x04000850 RID: 2128
	private List<Transform> pFireDummies;

	// Token: 0x04000851 RID: 2129
	private Transform pRedLight;

	// Token: 0x04000852 RID: 2130
	private float pFireRateTimer = 0.1f;

	// Token: 0x04000853 RID: 2131
	private float pCurYAngle;

	// Token: 0x04000854 RID: 2132
	private int pFireNextFireDummy;

	// Token: 0x04000855 RID: 2133
	private Transform pPlayerTransform;

	// Token: 0x04000856 RID: 2134
	private Vector3 pToPlayerVec = Vector3.zero;

	// Token: 0x04000857 RID: 2135
	private Quaternion pRotX90 = Quaternion.Euler(90f, 0f, 0f);

	// Token: 0x04000858 RID: 2136
	private Quaternion pRotZ90 = Quaternion.Euler(0f, 0f, 90f);

	// Token: 0x04000859 RID: 2137
	private AudioSource pFireSoundLoop;

	// Token: 0x0400085A RID: 2138
	private ParticleSystem pMuzzlePS;
}
