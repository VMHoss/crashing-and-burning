using System;
using UnityEngine;

// Token: 0x020000F3 RID: 243
public class ItemBoost : ItemBase
{
	// Token: 0x06000768 RID: 1896 RVA: 0x00038858 File Offset: 0x00036A58
	public ItemBoost(CarScript aCarScript) : base(aCarScript, "Boost")
	{
		this.pPushForce = this.pItemProps["PushTime"].f;
		this.pAdditionalSpeed = this.pItemProps["AdditionalSpeed"].f;
		if (Shop.GetUpgradeLevel("BoostStrength") > 0)
		{
			this.pAdditionalSpeed += Data.Shared["Upgrades"].d["BoostStrength"].d["Upgrade" + Shop.GetUpgradeLevel("BoostStrength")].f;
		}
	}

	// Token: 0x06000769 RID: 1897 RVA: 0x0003892C File Offset: 0x00036B2C
	public override void StartSpecific()
	{
		this.pCameraScript = Scripts.trackScript.trackManager.cameraScript;
	}

	// Token: 0x0600076A RID: 1898 RVA: 0x00038944 File Offset: 0x00036B44
	public override bool ActionSpecific()
	{
		if (this.pCarScript.hovering)
		{
			this.pPushForceTimer = this.pPushForce;
			this.pTargetSpeed = this.pCarScript.GetSpeed() + this.pAdditionalSpeed;
			this.pDash = Loader.LoadGameObject("Shared", "Effects/Dash_PS");
			this.pDash.transform.parent = this.pCarScript.transform;
			this.pDash.transform.localPosition = new Vector3(0f, 0f, 0f);
			this.pDash.transform.localRotation = Quaternion.identity;
			this.pDash1 = this.pDash.transform.FindChild("LotusDash1");
			this.pDash2 = this.pDash.transform.FindChild("LotusDash2");
			return true;
		}
		return false;
	}

	// Token: 0x0600076B RID: 1899 RVA: 0x00038A28 File Offset: 0x00036C28
	public override void UpdateSpecific()
	{
		if (this.pPushForceTimer > 0f)
		{
			this.pPushForceTimer -= Time.deltaTime;
			if ((double)this.pPushForceTimer <= 0.0)
			{
				this.DisableDashEffect();
			}
			else
			{
				float num = 1f - (this.pPushForceTimer * 2f - 0.5f) * (this.pPushForceTimer * 2f - 0.5f) * 4f;
				this.pDash1.localScale = new Vector3(1f, 1f, 1.5f + num);
				this.pDash2.localScale = new Vector3(1f, 1f, 1.5f + num);
				this.pCameraScript.dashEffectTime = num;
			}
		}
	}

	// Token: 0x0600076C RID: 1900 RVA: 0x00038AF8 File Offset: 0x00036CF8
	public override void FixedUpdateSpecific()
	{
		if (this.pPushForceTimer > 0f)
		{
			float num = Mathf.Min(this.pTargetSpeed - this.pCarScript.GetSpeed(), 100f);
			Vector3 force = new Vector3(0f, 0f, this.pRigidBody.mass * num * 7f);
			this.pRigidBody.AddRelativeForce(force);
		}
	}

	// Token: 0x0600076D RID: 1901 RVA: 0x00038B64 File Offset: 0x00036D64
	public override void ExplodeCarSpecific()
	{
		this.DisableDashEffect();
	}

	// Token: 0x0600076E RID: 1902 RVA: 0x00038B6C File Offset: 0x00036D6C
	private void DisableDashEffect()
	{
		if (this.pDash != null)
		{
			UnityEngine.Object.Destroy(this.pDash);
		}
		this.pCameraScript.dashEffectTime = -1f;
		this.pPushForceTimer = -1f;
	}

	// Token: 0x040007F1 RID: 2033
	private CameraScript pCameraScript;

	// Token: 0x040007F2 RID: 2034
	private float pPushForceTimer = -1f;

	// Token: 0x040007F3 RID: 2035
	private float pPushForce = 1f;

	// Token: 0x040007F4 RID: 2036
	private float pAdditionalSpeed = 10f;

	// Token: 0x040007F5 RID: 2037
	private float pTargetSpeed;

	// Token: 0x040007F6 RID: 2038
	private GameObject pDash;

	// Token: 0x040007F7 RID: 2039
	private Transform pDash1;

	// Token: 0x040007F8 RID: 2040
	private Transform pDash2;
}
