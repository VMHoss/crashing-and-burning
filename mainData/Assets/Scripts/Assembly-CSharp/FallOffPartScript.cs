using System;
using UnityEngine;

// Token: 0x020000EC RID: 236
public class FallOffPartScript : MonoBehaviour
{
	// Token: 0x06000719 RID: 1817 RVA: 0x0003460C File Offset: 0x0003280C
	private void Awake()
	{
		base.gameObject.AddComponent<MeshCollider>().convex = true;
		foreach (object obj in base.transform)
		{
			Transform transform = (Transform)obj;
			transform.gameObject.AddComponent<FallOffPartScript>();
		}
		base.gameObject.layer = LayerMask.NameToLayer("CarPart");
	}

	// Token: 0x0600071A RID: 1818 RVA: 0x000346A8 File Offset: 0x000328A8
	public void OnCollisionEnterCustom(ContactPoint aContactPoint)
	{
		if (GameData.lastPlayerVelocity.magnitude > this.fallOffSpeed && (aContactPoint.thisCollider == base.collider || aContactPoint.otherCollider == base.collider))
		{
			this.DisconnectFromVehicle(false);
		}
	}

	// Token: 0x0600071B RID: 1819 RVA: 0x00034700 File Offset: 0x00032900
	public void DisconnectFromVehicle(bool aUseMaterialFromParent)
	{
		if (this.tHit)
		{
			return;
		}
		this.tHit = true;
		if (!aUseMaterialFromParent)
		{
			base.renderer.sharedMaterial = (UnityEngine.Object.Instantiate(base.renderer.sharedMaterial) as Material);
			base.renderer.sharedMaterial.SetColor("_ExtraRimColor", new Color(0f, 0f, 0f, 1f));
		}
		else
		{
			base.renderer.sharedMaterial = base.transform.parent.renderer.sharedMaterial;
		}
		base.gameObject.AddComponent<Rigidbody>();
		foreach (object obj in base.transform)
		{
			Transform transform = (Transform)obj;
			FallOffPartScript component = transform.gameObject.GetComponent<FallOffPartScript>();
			if (component != null)
			{
				component.DisconnectFromVehicle(true);
			}
		}
		UnityEngine.Object.Destroy(base.gameObject, 5f);
		if (base.collider != null)
		{
			UnityEngine.Object.Destroy(base.collider);
		}
		UnityEngine.Object.Destroy(this);
	}

	// Token: 0x0600071C RID: 1820 RVA: 0x00034854 File Offset: 0x00032A54
	public void OnDestroy()
	{
		base.transform.parent = null;
	}

	// Token: 0x04000750 RID: 1872
	public float fallOffSpeed = 20f;

	// Token: 0x04000751 RID: 1873
	private bool tHit;
}
