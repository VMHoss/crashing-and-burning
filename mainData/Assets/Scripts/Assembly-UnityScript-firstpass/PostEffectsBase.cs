using System;
using UnityEngine;

// Token: 0x02000007 RID: 7
[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
[Serializable]
public class PostEffectsBase : MonoBehaviour
{
	// Token: 0x06000009 RID: 9 RVA: 0x00002A94 File Offset: 0x00000C94
	public PostEffectsBase()
	{
		this.supportHDRTextures = true;
		this.isSupported = true;
	}

	// Token: 0x0600000A RID: 10 RVA: 0x00002AAC File Offset: 0x00000CAC
	public virtual Material CheckShaderAndCreateMaterial(Shader s, Material m2Create)
	{
		Material result;
		if (!s)
		{
			Debug.Log("Missing shader in " + this.ToString());
			this.enabled = false;
			result = null;
		}
		else if (s.isSupported && m2Create && m2Create.shader == s)
		{
			result = m2Create;
		}
		else if (!s.isSupported)
		{
			this.NotSupported();
			Debug.Log("The shader " + s.ToString() + " on effect " + this.ToString() + " is not supported on this platform!");
			result = null;
		}
		else
		{
			m2Create = new Material(s);
			m2Create.hideFlags = HideFlags.DontSave;
			result = ((!m2Create) ? null : m2Create);
		}
		return result;
	}

	// Token: 0x0600000B RID: 11 RVA: 0x00002B88 File Offset: 0x00000D88
	public virtual Material CreateMaterial(Shader s, Material m2Create)
	{
		Material result;
		if (!s)
		{
			Debug.Log("Missing shader in " + this.ToString());
			result = null;
		}
		else if (m2Create && m2Create.shader == s && s.isSupported)
		{
			result = m2Create;
		}
		else if (!s.isSupported)
		{
			result = null;
		}
		else
		{
			m2Create = new Material(s);
			m2Create.hideFlags = HideFlags.DontSave;
			result = ((!m2Create) ? null : m2Create);
		}
		return result;
	}

	// Token: 0x0600000C RID: 12 RVA: 0x00002C24 File Offset: 0x00000E24
	public virtual void OnEnable()
	{
		this.isSupported = true;
	}

	// Token: 0x0600000D RID: 13 RVA: 0x00002C30 File Offset: 0x00000E30
	public virtual bool CheckSupport()
	{
		return this.CheckSupport(false);
	}

	// Token: 0x0600000E RID: 14 RVA: 0x00002C3C File Offset: 0x00000E3C
	public virtual bool CheckResources()
	{
		Debug.LogWarning("CheckResources () for " + this.ToString() + " should be overwritten.");
		return this.isSupported;
	}

	// Token: 0x0600000F RID: 15 RVA: 0x00002C64 File Offset: 0x00000E64
	public virtual void Start()
	{
		this.CheckResources();
	}

	// Token: 0x06000010 RID: 16 RVA: 0x00002C70 File Offset: 0x00000E70
	public virtual bool CheckSupport(bool needDepth)
	{
		this.isSupported = true;
		this.supportHDRTextures = SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.ARGBHalf);
		bool flag;
		if (flag = (SystemInfo.graphicsShaderLevel >= 50))
		{
			flag = SystemInfo.supportsComputeShaders;
		}
		this.supportDX11 = flag;
		bool result;
		if (!SystemInfo.supportsImageEffects || !SystemInfo.supportsRenderTextures)
		{
			this.NotSupported();
			result = false;
		}
		else if (needDepth && !SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.Depth))
		{
			this.NotSupported();
			result = false;
		}
		else
		{
			if (needDepth)
			{
				this.camera.depthTextureMode = (this.camera.depthTextureMode | DepthTextureMode.Depth);
			}
			result = true;
		}
		return result;
	}

	// Token: 0x06000011 RID: 17 RVA: 0x00002D0C File Offset: 0x00000F0C
	public virtual bool CheckSupport(bool needDepth, bool needHdr)
	{
		bool result;
		if (!this.CheckSupport(needDepth))
		{
			result = false;
		}
		else if (needHdr && !this.supportHDRTextures)
		{
			this.NotSupported();
			result = false;
		}
		else
		{
			result = true;
		}
		return result;
	}

	// Token: 0x06000012 RID: 18 RVA: 0x00002D4C File Offset: 0x00000F4C
	public virtual bool Dx11Support()
	{
		return this.supportDX11;
	}

	// Token: 0x06000013 RID: 19 RVA: 0x00002D54 File Offset: 0x00000F54
	public virtual void ReportAutoDisable()
	{
		Debug.LogWarning("The image effect " + this.ToString() + " has been disabled as it's not supported on the current platform.");
	}

	// Token: 0x06000014 RID: 20 RVA: 0x00002D78 File Offset: 0x00000F78
	public virtual bool CheckShader(Shader s)
	{
		Debug.Log("The shader " + s.ToString() + " on effect " + this.ToString() + " is not part of the Unity 3.2+ effects suite anymore. For best performance and quality, please ensure you are using the latest Standard Assets Image Effects (Pro only) package.");
		bool result;
		if (!s.isSupported)
		{
			this.NotSupported();
			result = false;
		}
		else
		{
			result = false;
		}
		return result;
	}

	// Token: 0x06000015 RID: 21 RVA: 0x00002DD8 File Offset: 0x00000FD8
	public virtual void NotSupported()
	{
		this.enabled = false;
		this.isSupported = false;
	}

	// Token: 0x06000016 RID: 22 RVA: 0x00002DE8 File Offset: 0x00000FE8
	public virtual void DrawBorder(RenderTexture dest, Material material)
	{
		float x = 0f;
		float x2 = 0f;
		float y = 0f;
		float y2 = 0f;
		RenderTexture.active = dest;
		bool flag = true;
		GL.PushMatrix();
		GL.LoadOrtho();
		for (int i = 0; i < material.passCount; i++)
		{
			material.SetPass(i);
			float y3 = 0f;
			float y4 = 0f;
			if (flag)
			{
				y3 = 1f;
				y4 = (float)0;
			}
			else
			{
				y3 = (float)0;
				y4 = 1f;
			}
			x = (float)0;
			x2 = (float)0 + 1f / ((float)dest.width * 1f);
			y = (float)0;
			y2 = 1f;
			GL.Begin(7);
			GL.TexCoord2((float)0, y3);
			GL.Vertex3(x, y, 0.1f);
			GL.TexCoord2(1f, y3);
			GL.Vertex3(x2, y, 0.1f);
			GL.TexCoord2(1f, y4);
			GL.Vertex3(x2, y2, 0.1f);
			GL.TexCoord2((float)0, y4);
			GL.Vertex3(x, y2, 0.1f);
			x = 1f - 1f / ((float)dest.width * 1f);
			x2 = 1f;
			y = (float)0;
			y2 = 1f;
			GL.TexCoord2((float)0, y3);
			GL.Vertex3(x, y, 0.1f);
			GL.TexCoord2(1f, y3);
			GL.Vertex3(x2, y, 0.1f);
			GL.TexCoord2(1f, y4);
			GL.Vertex3(x2, y2, 0.1f);
			GL.TexCoord2((float)0, y4);
			GL.Vertex3(x, y2, 0.1f);
			x = (float)0;
			x2 = 1f;
			y = (float)0;
			y2 = (float)0 + 1f / ((float)dest.height * 1f);
			GL.TexCoord2((float)0, y3);
			GL.Vertex3(x, y, 0.1f);
			GL.TexCoord2(1f, y3);
			GL.Vertex3(x2, y, 0.1f);
			GL.TexCoord2(1f, y4);
			GL.Vertex3(x2, y2, 0.1f);
			GL.TexCoord2((float)0, y4);
			GL.Vertex3(x, y2, 0.1f);
			x = (float)0;
			x2 = 1f;
			y = 1f - 1f / ((float)dest.height * 1f);
			y2 = 1f;
			GL.TexCoord2((float)0, y3);
			GL.Vertex3(x, y, 0.1f);
			GL.TexCoord2(1f, y3);
			GL.Vertex3(x2, y, 0.1f);
			GL.TexCoord2(1f, y4);
			GL.Vertex3(x2, y2, 0.1f);
			GL.TexCoord2((float)0, y4);
			GL.Vertex3(x, y2, 0.1f);
			GL.End();
		}
		GL.PopMatrix();
	}

	// Token: 0x06000017 RID: 23 RVA: 0x00003090 File Offset: 0x00001290
	public virtual void Main()
	{
	}

	// Token: 0x04000032 RID: 50
	protected bool supportHDRTextures;

	// Token: 0x04000033 RID: 51
	protected bool supportDX11;

	// Token: 0x04000034 RID: 52
	protected bool isSupported;
}
