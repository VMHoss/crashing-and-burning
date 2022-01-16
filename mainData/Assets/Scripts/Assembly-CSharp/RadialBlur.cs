using System;
using UnityEngine;

// Token: 0x020000C3 RID: 195
[ExecuteInEditMode]
public class RadialBlur : MonoBehaviour
{
	// Token: 0x060005EB RID: 1515 RVA: 0x0002A930 File Offset: 0x00028B30
	private Material GetMaterial()
	{
		if (this.rbMaterial == null)
		{
			this.rbMaterial = new Material(this.rbShader);
			this.rbMaterial.hideFlags = HideFlags.HideAndDontSave;
		}
		return this.rbMaterial;
	}

	// Token: 0x060005EC RID: 1516 RVA: 0x0002A968 File Offset: 0x00028B68
	private void Start()
	{
		if (this.rbShader == null)
		{
			Debug.LogError("shader missing!", this);
		}
		this.isOpenGL = SystemInfo.graphicsDeviceVersion.StartsWith("OpenGL");
	}

	// Token: 0x060005ED RID: 1517 RVA: 0x0002A99C File Offset: 0x00028B9C
	private void OnRenderImage(RenderTexture source, RenderTexture dest)
	{
		float value = 1f;
		float value2 = 1f;
		if (this.isOpenGL)
		{
			value = (float)source.width;
			value2 = (float)source.height;
		}
		this.GetMaterial().SetFloat("_BlurStrength", this.blurStrength);
		this.GetMaterial().SetFloat("_BlurWidth", this.blurWidth);
		this.GetMaterial().SetFloat("_iHeight", value);
		this.GetMaterial().SetFloat("_iWidth", value2);
		this.GetMaterial().SetFloat("_iDeltaCenterX", this.deltaCenterX);
		this.GetMaterial().SetFloat("_iDeltaCenterY", this.deltaCenterY);
		Graphics.Blit(source, dest, this.GetMaterial());
	}

	// Token: 0x04000678 RID: 1656
	public Shader rbShader;

	// Token: 0x04000679 RID: 1657
	public float blurStrength = 2.2f;

	// Token: 0x0400067A RID: 1658
	public float blurWidth = 1f;

	// Token: 0x0400067B RID: 1659
	public float deltaCenterX;

	// Token: 0x0400067C RID: 1660
	public float deltaCenterY;

	// Token: 0x0400067D RID: 1661
	private Material rbMaterial;

	// Token: 0x0400067E RID: 1662
	private bool isOpenGL;
}
