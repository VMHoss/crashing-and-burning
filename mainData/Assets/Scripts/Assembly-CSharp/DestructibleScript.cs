using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000E8 RID: 232
public class DestructibleScript : MonoBehaviour
{
	// Token: 0x06000706 RID: 1798 RVA: 0x00033400 File Offset: 0x00031600
	public void Initialize(string anObjectType, BlockData aBlockData)
	{
		if (this.pFirstInit)
		{
			this.pObjectType = anObjectType;
			this.pProps = Data.Shared["Destructible"].d[this.pObjectType].d;
			this.pStatic = this.pProps["Static"].b;
			if (this.pProps.ContainsKey("DestroyInstant"))
			{
				this.pDestroyInstant = this.pProps["DestroyInstant"].b;
			}
			if (this.pProps.ContainsKey("NoCollision"))
			{
				this.pNoCollision = this.pProps["NoCollision"].b;
			}
			if (this.pProps.ContainsKey("RemoveAfter"))
			{
				this.pRemoveAfter = this.pProps["RemoveAfter"].f;
			}
			if (this.pProps.ContainsKey("SwitchMaterial"))
			{
				this.pSwitchMaterial = this.pProps["SwitchMaterial"].b;
			}
			if (this.pProps.ContainsKey("Transparent"))
			{
				this.pIsTransparent = this.pProps["Transparent"].b;
			}
			base.gameObject.layer = GameData.destructibleLayer;
			this.pLODObject = (base.gameObject.GetComponent<LODGroup>() != null);
			base.collider.isTrigger = false;
			if (base.collider.material == null)
			{
				base.collider.material = DestructibleScript.physicMaterial;
			}
			if (this.pObjectType == "MountedGun")
			{
				this.pMountedGunScript = base.gameObject.AddComponent<MountedGunScript>();
				this.pMountedGunScript.Initialize(this);
			}
			if (this.pObjectType == "SamSite")
			{
				this.pSamSiteScript = base.gameObject.AddComponent<SamSiteScript>();
				this.pSamSiteScript.Initialize(this);
			}
			this.pRigidbody = base.gameObject.AddComponent<Rigidbody>();
			this.pRigidbody.isKinematic = true;
			this.pChildRenderers = base.gameObject.GetComponentsInChildren<Renderer>();
			this.pFirstInit = false;
		}
		else
		{
			if (!base.collider.enabled)
			{
				base.collider.enabled = true;
			}
			Material sharedMaterial = (!this.pIsTransparent) ? DestructibleScript.objectMaterial : DestructibleScript.objectTransparentMaterial;
			foreach (Renderer renderer in this.pChildRenderers)
			{
				if (renderer.particleSystem == null)
				{
					renderer.sharedMaterial = sharedMaterial;
				}
			}
			this.destroyed = false;
		}
		this.blockData = aBlockData;
		this.pHealth = this.pProps["Health"].f * GameData.destrHealthMultiplier;
		if (this.pMountedGunScript != null)
		{
			this.pMountedGunScript.Activate();
		}
		if (this.pSamSiteScript != null)
		{
			this.pSamSiteScript.Activate();
		}
	}

	// Token: 0x06000707 RID: 1799 RVA: 0x00033720 File Offset: 0x00031920
	private void OnCollisionEnter(Collision aCollision)
	{
		if (this.destroyed)
		{
			return;
		}
		if (!aCollision.rigidbody)
		{
			return;
		}
		if (GameData.playerCarScript.gameObject == aCollision.gameObject)
		{
			float magnitude = aCollision.relativeVelocity.magnitude;
			float num = 5f;
			if (GameData.playerCarScript.HasQuadDamage())
			{
				num *= GameData.playerCarScript.carData.attack * 4f;
			}
			else
			{
				num *= GameData.playerCarScript.carData.attack;
			}
			float num2 = Mathf.Max(magnitude * 0.25f * num, 0.5f);
			this.pHealth -= num2;
			if (this.pHealth <= 0f)
			{
				this.DestroyIt(GameData.lastPlayerVelocity + new Vector3(0f, 9f, 0f));
				float num3 = 1f - (this.pProps["SpeedReduc"].f + GameData.playerCarScript.carData.speedReducMod);
				if (num3 > 1f)
				{
					num3 = 1f;
				}
				aCollision.rigidbody.velocity = GameData.lastPlayerVelocity * num3;
				aCollision.rigidbody.angularVelocity = GameData.lastPlayerAngularVelocity * num3;
			}
		}
	}

	// Token: 0x06000708 RID: 1800 RVA: 0x00033878 File Offset: 0x00031A78
	public void DestroyedByExplosion(float aDamage, Vector3 aDirection)
	{
		if (this.destroyed)
		{
			return;
		}
		this.pHealth -= aDamage;
		if (this.pHealth <= 0f)
		{
			this.DestroyIt(aDirection + new Vector3(0f, 9f, 0f));
		}
	}

	// Token: 0x06000709 RID: 1801 RVA: 0x000338D0 File Offset: 0x00031AD0
	public void Damage(DamageInfo aDamageInfo)
	{
		this.pHealth -= aDamageInfo.damage;
		if (this.pHealth <= 0f)
		{
			this.DestroyedByExplosion(0f, Vector3.up);
		}
	}

	// Token: 0x0600070A RID: 1802 RVA: 0x00033908 File Offset: 0x00031B08
	public void Remove()
	{
		this.pRigidbody.isKinematic = true;
		this.RemoveIcon();
		this.blockData = null;
		base.CancelInvoke();
		base.enabled = false;
		Scripts.poolManager.ReturnToPool(base.gameObject);
	}

	// Token: 0x0600070B RID: 1803 RVA: 0x0003394C File Offset: 0x00031B4C
	private void DestroyIt(Vector3 aVelocity)
	{
		this.destroyed = true;
		this.RemoveIcon();
		if (this.pMountedGunScript != null)
		{
			this.pMountedGunScript.GotDestroyed();
			this.pMountedGunScript.enabled = false;
		}
		if (this.pSamSiteScript != null)
		{
			this.pSamSiteScript.GotDestroyed();
			this.pSamSiteScript.enabled = false;
		}
		if (this.pProps["PartSys"].s != "None")
		{
			GameObject gameObject = Loader.LoadGameObject("Shared", "Effects/Particles/" + this.pProps["PartSys"].s);
			if (gameObject != null)
			{
				ParticleSystem component = gameObject.GetComponent<ParticleSystem>();
				Vector3 b = new Vector3(this.pProps["PartSysOffset"].l[0].f, this.pProps["PartSysOffset"].l[1].f, this.pProps["PartSysOffset"].l[2].f);
				gameObject.transform.position = base.transform.position + b;
				component.Play();
				UnityEngine.Object.Destroy(component.gameObject, component.duration);
			}
		}
		if (this.pProps["Sound"].s != "None")
		{
			Scripts.audioManager.PlaySFX(this.pProps["Sound"].s, 0.6f);
		}
		Scripts.scoreManager.DestroyedDestructible(this.pObjectType);
		if (this.pDestroyInstant)
		{
			this.Remove();
		}
		else
		{
			if (this.pNoCollision)
			{
				base.collider.enabled = false;
			}
			if (!this.pStatic)
			{
				if (!Data.highDetails)
				{
					base.collider.enabled = false;
				}
				this.pRigidbody.isKinematic = false;
				base.rigidbody.velocity = aVelocity;
				base.rigidbody.angularVelocity = UnityEngine.Random.insideUnitSphere * 3f;
			}
			if (this.pSwitchMaterial)
			{
				foreach (Renderer renderer in this.pChildRenderers)
				{
					if (renderer.particleSystem == null)
					{
						renderer.sharedMaterial = DestructibleScript.objectDestroyedMaterial;
					}
				}
			}
			if (this.pRemoveAfter > -0.01f)
			{
				base.Invoke("Remove", this.pRemoveAfter);
			}
			else if (!base.collider.enabled)
			{
				base.Invoke("Remove", 2f);
			}
		}
	}

	// Token: 0x0600070C RID: 1804 RVA: 0x00033C24 File Offset: 0x00031E24
	private void RemoveIcon()
	{
		if (this.iconAttached)
		{
			Scripts.interfaceScript.DestroyMapIcon(base.gameObject);
			this.iconAttached = false;
		}
	}

	// Token: 0x0400072C RID: 1836
	public static PhysicMaterial physicMaterial;

	// Token: 0x0400072D RID: 1837
	public static Material objectMaterial;

	// Token: 0x0400072E RID: 1838
	public static Material objectDestroyedMaterial;

	// Token: 0x0400072F RID: 1839
	public static Material objectTransparentMaterial;

	// Token: 0x04000730 RID: 1840
	public bool iconAttached;

	// Token: 0x04000731 RID: 1841
	public BlockData blockData;

	// Token: 0x04000732 RID: 1842
	private string pObjectType = string.Empty;

	// Token: 0x04000733 RID: 1843
	private Dictionary<string, DicEntry> pProps;

	// Token: 0x04000734 RID: 1844
	private float pHealth = 100f;

	// Token: 0x04000735 RID: 1845
	private bool pStatic = true;

	// Token: 0x04000736 RID: 1846
	private bool pDestroyInstant;

	// Token: 0x04000737 RID: 1847
	private bool pNoCollision;

	// Token: 0x04000738 RID: 1848
	private float pRemoveAfter = -1f;

	// Token: 0x04000739 RID: 1849
	private bool pSwitchMaterial;

	// Token: 0x0400073A RID: 1850
	private bool pIsTransparent;

	// Token: 0x0400073B RID: 1851
	public bool destroyed;

	// Token: 0x0400073C RID: 1852
	private bool pFirstInit = true;

	// Token: 0x0400073D RID: 1853
	private bool pLODObject;

	// Token: 0x0400073E RID: 1854
	private Rigidbody pRigidbody;

	// Token: 0x0400073F RID: 1855
	private Renderer[] pChildRenderers;

	// Token: 0x04000740 RID: 1856
	private MountedGunScript pMountedGunScript;

	// Token: 0x04000741 RID: 1857
	private SamSiteScript pSamSiteScript;
}
