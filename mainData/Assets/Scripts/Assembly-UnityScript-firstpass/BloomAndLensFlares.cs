using System;
using UnityEngine;

// Token: 0x02000006 RID: 6
[RequireComponent(typeof(Camera))]
[AddComponentMenu("Image Effects/Bloom (HDR, Lens Flares)")]
[ExecuteInEditMode]
[Serializable]
public class BloomAndLensFlares : PostEffectsBase
{
	// Token: 0x06000001 RID: 1 RVA: 0x000020EC File Offset: 0x000002EC
	public BloomAndLensFlares()
	{
		this.screenBlendMode = BloomScreenBlendMode.Add;
		this.hdr = HDRBloomMode.Auto;
		this.sepBlurSpread = 1.5f;
		this.useSrcAlphaAsMask = 0.5f;
		this.bloomIntensity = 1f;
		this.bloomThreshhold = 0.5f;
		this.bloomBlurIterations = 2;
		this.hollywoodFlareBlurIterations = 2;
		this.lensflareMode = LensflareStyle34.Anamorphic;
		this.hollyStretchWidth = 3.5f;
		this.lensflareIntensity = 1f;
		this.lensflareThreshhold = 0.3f;
		this.flareColorA = new Color(0.4f, 0.4f, 0.8f, 0.75f);
		this.flareColorB = new Color(0.4f, 0.8f, 0.8f, 0.75f);
		this.flareColorC = new Color(0.8f, 0.4f, 0.8f, 0.75f);
		this.flareColorD = new Color(0.8f, 0.4f, (float)0, 0.75f);
		this.blurWidth = 1f;
	}

	// Token: 0x06000002 RID: 2 RVA: 0x000021F4 File Offset: 0x000003F4
	public override bool CheckResources()
	{
		this.CheckSupport(false);
		this.screenBlend = this.CheckShaderAndCreateMaterial(this.screenBlendShader, this.screenBlend);
		this.lensFlareMaterial = this.CheckShaderAndCreateMaterial(this.lensFlareShader, this.lensFlareMaterial);
		this.vignetteMaterial = this.CheckShaderAndCreateMaterial(this.vignetteShader, this.vignetteMaterial);
		this.separableBlurMaterial = this.CheckShaderAndCreateMaterial(this.separableBlurShader, this.separableBlurMaterial);
		this.addBrightStuffBlendOneOneMaterial = this.CheckShaderAndCreateMaterial(this.addBrightStuffOneOneShader, this.addBrightStuffBlendOneOneMaterial);
		this.hollywoodFlaresMaterial = this.CheckShaderAndCreateMaterial(this.hollywoodFlaresShader, this.hollywoodFlaresMaterial);
		this.brightPassFilterMaterial = this.CheckShaderAndCreateMaterial(this.brightPassFilterShader, this.brightPassFilterMaterial);
		if (!this.isSupported)
		{
			this.ReportAutoDisable();
		}
		return this.isSupported;
	}

	// Token: 0x06000003 RID: 3 RVA: 0x000022C8 File Offset: 0x000004C8
	public virtual void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (!this.CheckResources())
		{
			Graphics.Blit(source, destination);
		}
		else
		{
			this.doHdr = false;
			if (this.hdr == HDRBloomMode.Auto)
			{
				bool flag;
				if (flag = (source.format == RenderTextureFormat.ARGBHalf))
				{
					flag = this.camera.hdr;
				}
				this.doHdr = flag;
			}
			else
			{
				this.doHdr = (this.hdr == HDRBloomMode.On);
			}
			bool supportHDRTextures;
			if (supportHDRTextures = this.doHdr)
			{
				supportHDRTextures = this.supportHDRTextures;
			}
			this.doHdr = supportHDRTextures;
			BloomScreenBlendMode pass = this.screenBlendMode;
			if (this.doHdr)
			{
				pass = BloomScreenBlendMode.Add;
			}
			RenderTextureFormat format = (!this.doHdr) ? RenderTextureFormat.Default : RenderTextureFormat.ARGBHalf;
			RenderTexture temporary = RenderTexture.GetTemporary(source.width / 2, source.height / 2, 0, format);
			RenderTexture temporary2 = RenderTexture.GetTemporary(source.width / 4, source.height / 4, 0, format);
			RenderTexture temporary3 = RenderTexture.GetTemporary(source.width / 4, source.height / 4, 0, format);
			RenderTexture temporary4 = RenderTexture.GetTemporary(source.width / 4, source.height / 4, 0, format);
			float num = 1f * (float)source.width / (1f * (float)source.height);
			float num2 = 0.001953125f;
			Graphics.Blit(source, temporary, this.screenBlend, 2);
			Graphics.Blit(temporary, temporary2, this.screenBlend, 2);
			RenderTexture.ReleaseTemporary(temporary);
			this.BrightFilter(this.bloomThreshhold, this.useSrcAlphaAsMask, temporary2, temporary3);
			if (this.bloomBlurIterations < 1)
			{
				this.bloomBlurIterations = 1;
			}
			for (int i = 0; i < this.bloomBlurIterations; i++)
			{
				float num3 = (1f + (float)i * 0.5f) * this.sepBlurSpread;
				this.separableBlurMaterial.SetVector("offsets", new Vector4((float)0, num3 * num2, (float)0, (float)0));
				Graphics.Blit((i != 0) ? temporary2 : temporary3, temporary4, this.separableBlurMaterial);
				this.separableBlurMaterial.SetVector("offsets", new Vector4(num3 / num * num2, (float)0, (float)0, (float)0));
				Graphics.Blit(temporary4, temporary2, this.separableBlurMaterial);
			}
			if (this.lensflares)
			{
				if (this.lensflareMode == LensflareStyle34.Ghosting)
				{
					this.BrightFilter(this.lensflareThreshhold, (float)0, temporary2, temporary4);
					this.Vignette(0.975f, temporary4, temporary3);
					this.BlendFlares(temporary3, temporary2);
				}
				else
				{
					this.hollywoodFlaresMaterial.SetVector("_Threshhold", new Vector4(this.lensflareThreshhold, 1f / (1f - this.lensflareThreshhold), (float)0, (float)0));
					this.hollywoodFlaresMaterial.SetVector("tintColor", new Vector4(this.flareColorA.r, this.flareColorA.g, this.flareColorA.b, this.flareColorA.a) * this.flareColorA.a * this.lensflareIntensity);
					Graphics.Blit(temporary4, temporary3, this.hollywoodFlaresMaterial, 2);
					Graphics.Blit(temporary3, temporary4, this.hollywoodFlaresMaterial, 3);
					this.hollywoodFlaresMaterial.SetVector("offsets", new Vector4(this.sepBlurSpread * 1f / num * num2, (float)0, (float)0, (float)0));
					this.hollywoodFlaresMaterial.SetFloat("stretchWidth", this.hollyStretchWidth);
					Graphics.Blit(temporary4, temporary3, this.hollywoodFlaresMaterial, 1);
					this.hollywoodFlaresMaterial.SetFloat("stretchWidth", this.hollyStretchWidth * 2f);
					Graphics.Blit(temporary3, temporary4, this.hollywoodFlaresMaterial, 1);
					this.hollywoodFlaresMaterial.SetFloat("stretchWidth", this.hollyStretchWidth * 4f);
					Graphics.Blit(temporary4, temporary3, this.hollywoodFlaresMaterial, 1);
					if (this.lensflareMode == LensflareStyle34.Anamorphic)
					{
						for (int j = 0; j < this.hollywoodFlareBlurIterations; j++)
						{
							this.separableBlurMaterial.SetVector("offsets", new Vector4(this.hollyStretchWidth * 2f / num * num2, (float)0, (float)0, (float)0));
							Graphics.Blit(temporary3, temporary4, this.separableBlurMaterial);
							this.separableBlurMaterial.SetVector("offsets", new Vector4(this.hollyStretchWidth * 2f / num * num2, (float)0, (float)0, (float)0));
							Graphics.Blit(temporary4, temporary3, this.separableBlurMaterial);
						}
						this.AddTo(1f, temporary3, temporary2);
					}
					else
					{
						for (int k = 0; k < this.hollywoodFlareBlurIterations; k++)
						{
							this.separableBlurMaterial.SetVector("offsets", new Vector4(this.hollyStretchWidth * 2f / num * num2, (float)0, (float)0, (float)0));
							Graphics.Blit(temporary3, temporary4, this.separableBlurMaterial);
							this.separableBlurMaterial.SetVector("offsets", new Vector4(this.hollyStretchWidth * 2f / num * num2, (float)0, (float)0, (float)0));
							Graphics.Blit(temporary4, temporary3, this.separableBlurMaterial);
						}
						this.Vignette(1f, temporary3, temporary4);
						this.BlendFlares(temporary4, temporary3);
						this.AddTo(1f, temporary3, temporary2);
					}
				}
			}
			this.screenBlend.SetFloat("_Intensity", this.bloomIntensity);
			this.screenBlend.SetTexture("_ColorBuffer", source);
			Graphics.Blit(temporary2, destination, this.screenBlend, (int)pass);
			RenderTexture.ReleaseTemporary(temporary2);
			RenderTexture.ReleaseTemporary(temporary3);
			RenderTexture.ReleaseTemporary(temporary4);
		}
	}

	// Token: 0x06000004 RID: 4 RVA: 0x00002838 File Offset: 0x00000A38
	private void AddTo(float intensity_, RenderTexture from, RenderTexture to)
	{
		this.addBrightStuffBlendOneOneMaterial.SetFloat("_Intensity", intensity_);
		Graphics.Blit(from, to, this.addBrightStuffBlendOneOneMaterial);
	}

	// Token: 0x06000005 RID: 5 RVA: 0x00002858 File Offset: 0x00000A58
	private void BlendFlares(RenderTexture from, RenderTexture to)
	{
		this.lensFlareMaterial.SetVector("colorA", new Vector4(this.flareColorA.r, this.flareColorA.g, this.flareColorA.b, this.flareColorA.a) * this.lensflareIntensity);
		this.lensFlareMaterial.SetVector("colorB", new Vector4(this.flareColorB.r, this.flareColorB.g, this.flareColorB.b, this.flareColorB.a) * this.lensflareIntensity);
		this.lensFlareMaterial.SetVector("colorC", new Vector4(this.flareColorC.r, this.flareColorC.g, this.flareColorC.b, this.flareColorC.a) * this.lensflareIntensity);
		this.lensFlareMaterial.SetVector("colorD", new Vector4(this.flareColorD.r, this.flareColorD.g, this.flareColorD.b, this.flareColorD.a) * this.lensflareIntensity);
		Graphics.Blit(from, to, this.lensFlareMaterial);
	}

	// Token: 0x06000006 RID: 6 RVA: 0x000029A4 File Offset: 0x00000BA4
	private void BrightFilter(float thresh, float useAlphaAsMask, RenderTexture from, RenderTexture to)
	{
		if (this.doHdr)
		{
			this.brightPassFilterMaterial.SetVector("threshhold", new Vector4(thresh, 1f, (float)0, (float)0));
		}
		else
		{
			this.brightPassFilterMaterial.SetVector("threshhold", new Vector4(thresh, 1f / (1f - thresh), (float)0, (float)0));
		}
		this.brightPassFilterMaterial.SetFloat("useSrcAlphaAsMask", useAlphaAsMask);
		Graphics.Blit(from, to, this.brightPassFilterMaterial);
	}

	// Token: 0x06000007 RID: 7 RVA: 0x00002A2C File Offset: 0x00000C2C
	private void Vignette(float amount, RenderTexture from, RenderTexture to)
	{
		if (this.lensFlareVignetteMask)
		{
			this.screenBlend.SetTexture("_ColorBuffer", this.lensFlareVignetteMask);
			Graphics.Blit(from, to, this.screenBlend, 3);
		}
		else
		{
			this.vignetteMaterial.SetFloat("vignetteIntensity", amount);
			Graphics.Blit(from, to, this.vignetteMaterial);
		}
	}

	// Token: 0x06000008 RID: 8 RVA: 0x00002A90 File Offset: 0x00000C90
	public override void Main()
	{
	}

	// Token: 0x0400000F RID: 15
	public TweakMode34 tweakMode;

	// Token: 0x04000010 RID: 16
	public BloomScreenBlendMode screenBlendMode;

	// Token: 0x04000011 RID: 17
	public HDRBloomMode hdr;

	// Token: 0x04000012 RID: 18
	private bool doHdr;

	// Token: 0x04000013 RID: 19
	public float sepBlurSpread;

	// Token: 0x04000014 RID: 20
	public float useSrcAlphaAsMask;

	// Token: 0x04000015 RID: 21
	public float bloomIntensity;

	// Token: 0x04000016 RID: 22
	public float bloomThreshhold;

	// Token: 0x04000017 RID: 23
	public int bloomBlurIterations;

	// Token: 0x04000018 RID: 24
	public bool lensflares;

	// Token: 0x04000019 RID: 25
	public int hollywoodFlareBlurIterations;

	// Token: 0x0400001A RID: 26
	public LensflareStyle34 lensflareMode;

	// Token: 0x0400001B RID: 27
	public float hollyStretchWidth;

	// Token: 0x0400001C RID: 28
	public float lensflareIntensity;

	// Token: 0x0400001D RID: 29
	public float lensflareThreshhold;

	// Token: 0x0400001E RID: 30
	public Color flareColorA;

	// Token: 0x0400001F RID: 31
	public Color flareColorB;

	// Token: 0x04000020 RID: 32
	public Color flareColorC;

	// Token: 0x04000021 RID: 33
	public Color flareColorD;

	// Token: 0x04000022 RID: 34
	public float blurWidth;

	// Token: 0x04000023 RID: 35
	public Texture2D lensFlareVignetteMask;

	// Token: 0x04000024 RID: 36
	public Shader lensFlareShader;

	// Token: 0x04000025 RID: 37
	private Material lensFlareMaterial;

	// Token: 0x04000026 RID: 38
	public Shader vignetteShader;

	// Token: 0x04000027 RID: 39
	private Material vignetteMaterial;

	// Token: 0x04000028 RID: 40
	public Shader separableBlurShader;

	// Token: 0x04000029 RID: 41
	private Material separableBlurMaterial;

	// Token: 0x0400002A RID: 42
	public Shader addBrightStuffOneOneShader;

	// Token: 0x0400002B RID: 43
	private Material addBrightStuffBlendOneOneMaterial;

	// Token: 0x0400002C RID: 44
	public Shader screenBlendShader;

	// Token: 0x0400002D RID: 45
	private Material screenBlend;

	// Token: 0x0400002E RID: 46
	public Shader hollywoodFlaresShader;

	// Token: 0x0400002F RID: 47
	private Material hollywoodFlaresMaterial;

	// Token: 0x04000030 RID: 48
	public Shader brightPassFilterShader;

	// Token: 0x04000031 RID: 49
	private Material brightPassFilterMaterial;
}
