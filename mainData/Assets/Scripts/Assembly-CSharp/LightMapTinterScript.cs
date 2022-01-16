using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000C2 RID: 194
public class LightMapTinterScript : MonoBehaviour
{
	// Token: 0x060005E7 RID: 1511 RVA: 0x0002A6E8 File Offset: 0x000288E8
	public void Initialize(int aLayerMask, List<Material> aMaterials)
	{
		this.pLayerMask = aLayerMask;
		this.pMaterials = aMaterials;
		this.pInitColorMaterials = new Dictionary<Material, Color>();
		foreach (Material material in this.pMaterials)
		{
			this.pInitColorMaterials.Add(material, material.GetColor("_MainColor"));
		}
	}

	// Token: 0x060005E8 RID: 1512 RVA: 0x0002A778 File Offset: 0x00028978
	private void Update()
	{
		RaycastHit raycastHit;
		if (Physics.Raycast(base.transform.position, Vector3.down, out raycastHit, 5f, this.pLayerMask) && raycastHit.collider.renderer != null && raycastHit.collider.renderer.material.HasProperty("_LightTex"))
		{
			Texture2D texture2D = raycastHit.collider.renderer.material.GetTexture("_LightTex") as Texture2D;
			if (texture2D != null)
			{
				Color pixelBilinear = texture2D.GetPixelBilinear(raycastHit.textureCoord2.x, raycastHit.textureCoord2.y);
				foreach (Material material in this.pMaterials)
				{
					material.SetColor("_MainColor", pixelBilinear);
				}
			}
		}
	}

	// Token: 0x060005E9 RID: 1513 RVA: 0x0002A898 File Offset: 0x00028A98
	private void OnDestroy()
	{
		foreach (KeyValuePair<Material, Color> keyValuePair in this.pInitColorMaterials)
		{
			keyValuePair.Key.SetColor("_MainColor", keyValuePair.Value);
		}
	}

	// Token: 0x04000675 RID: 1653
	private List<Material> pMaterials;

	// Token: 0x04000676 RID: 1654
	private int pLayerMask;

	// Token: 0x04000677 RID: 1655
	private Dictionary<Material, Color> pInitColorMaterials;
}
