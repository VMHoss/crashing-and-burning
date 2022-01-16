using System;
using UnityEngine;

// Token: 0x020000C7 RID: 199
public class TextureScrollScript : MonoBehaviour
{
	// Token: 0x060005FC RID: 1532 RVA: 0x0002B01C File Offset: 0x0002921C
	private void Awake()
	{
		this.pMaterial = base.renderer.sharedMaterial;
	}

	// Token: 0x060005FD RID: 1533 RVA: 0x0002B030 File Offset: 0x00029230
	private void Update()
	{
		this.pAccumulatedScrollVector += this.ScrollVector * Time.deltaTime;
		if (this.pAccumulatedScrollVector.x > 1f)
		{
			this.pAccumulatedScrollVector.x = this.pAccumulatedScrollVector.x - 1f;
		}
		if (this.pAccumulatedScrollVector.x < 0f)
		{
			this.pAccumulatedScrollVector.x = this.pAccumulatedScrollVector.x + 1f;
		}
		if (this.pAccumulatedScrollVector.y > 1f)
		{
			this.pAccumulatedScrollVector.y = this.pAccumulatedScrollVector.y - 1f;
		}
		if (this.pAccumulatedScrollVector.y < 0f)
		{
			this.pAccumulatedScrollVector.y = this.pAccumulatedScrollVector.y + 1f;
		}
		this.pMaterial.SetTextureOffset("_MainTex", this.pAccumulatedScrollVector);
	}

	// Token: 0x060005FE RID: 1534 RVA: 0x0002B124 File Offset: 0x00029324
	private void OnDestroy()
	{
		if (this.pMaterial != null)
		{
			this.pMaterial.SetTextureOffset("_MainTex", Vector2.zero);
		}
	}

	// Token: 0x04000689 RID: 1673
	public Vector2 ScrollVector = new Vector2(0f, -1f);

	// Token: 0x0400068A RID: 1674
	private Vector2 pAccumulatedScrollVector = new Vector2(0f, 0f);

	// Token: 0x0400068B RID: 1675
	private Material pMaterial;
}
