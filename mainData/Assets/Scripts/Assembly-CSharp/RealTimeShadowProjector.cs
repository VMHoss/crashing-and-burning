using System;
using UnityEngine;

// Token: 0x020000C4 RID: 196
public class RealTimeShadowProjector : MonoBehaviour
{
	// Token: 0x060005EF RID: 1519 RVA: 0x0002AA60 File Offset: 0x00028C60
	public bool Initialize(Projector aProjector, Vector3 anOffSet)
	{
		return this.Initialize(aProjector, anOffSet, 0);
	}

	// Token: 0x060005F0 RID: 1520 RVA: 0x0002AA6C File Offset: 0x00028C6C
	public bool Initialize(Projector aProjector, Vector3 anOffSet, int pRenderLayer)
	{
		this.pProjectorGO = (UnityEngine.Object.Instantiate(Resources.Load("Misc/BlobShadowProjector")) as GameObject);
		this.pProjectorGO.name = "BlobShadowProjector";
		this.pProjectorGO.transform.position = base.transform.position + anOffSet;
		Projector component = this.pProjectorGO.GetComponent<Projector>();
		component.orthoGraphicSize = 3f;
		Material material = UnityEngine.Object.Instantiate(Resources.Load("Misc/BlobShadow_Material")) as Material;
		component.material = material;
		if (!Data.highDetails || !SystemInfo.supportsRenderTextures || SystemInfo.graphicsDeviceVersion.Contains("OpenGL"))
		{
			component.orthographicSize = 5f;
			component.nearClipPlane = 1.5f;
			this.pProjectorGO.transform.parent = base.transform;
			this.pProjectorGO.transform.localPosition = new Vector3(0f, 2f, 0f);
			this.pProjectorGO.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
			base.enabled = false;
			return false;
		}
		this.pProjectorGO.AddComponent<FollowPositionScript>().Initialize(base.gameObject, anOffSet);
		GameObject gameObject = new GameObject("ProjectorRenderCamera", new Type[]
		{
			typeof(Camera)
		});
		this.renderCamera = gameObject.camera;
		this.renderCamera.enabled = false;
		this.renderCamera.transform.position = base.transform.position + anOffSet;
		gameObject.AddComponent<FollowPositionScript>().Initialize(base.gameObject, anOffSet);
		this.renderCamera.transform.LookAt(base.transform.position + Vector3.up * 0.9f);
		this.pProjectorGO.transform.rotation = this.renderCamera.transform.rotation;
		this.renderCamera.backgroundColor = new Color(1f, 1f, 1f, 0f);
		this.renderCamera.cullingMask = 1 << base.gameObject.layer;
		if (pRenderLayer > 0)
		{
			this.renderCamera.cullingMask |= 1 << pRenderLayer;
		}
		this.renderCamera.orthographic = true;
		this.renderCamera.orthographicSize = 3f;
		component.nearClipPlane = 0.5f;
		this.targetTexture = new RenderTexture(512, 512, 24, RenderTextureFormat.ARGB32);
		this.targetTexture.name = "ShadowProjectTexture";
		this.renderCamera.targetTexture = this.targetTexture;
		this.renderCamera.pixelRect = new Rect(2f, 2f, (float)(this.targetTexture.width - 4), (float)(this.targetTexture.height - 4));
		component.material.SetTexture("_ShadowTex", this.targetTexture);
		this.pShader = (Resources.Load("Shaders/BlackWhiteShader2") as Shader);
		return true;
	}

	// Token: 0x060005F1 RID: 1521 RVA: 0x0002AD90 File Offset: 0x00028F90
	private void Start()
	{
		if (this.targetTexture != null)
		{
			this.SetRenderTextureWhite();
		}
	}

	// Token: 0x060005F2 RID: 1522 RVA: 0x0002ADAC File Offset: 0x00028FAC
	private void SetRenderTextureWhite()
	{
		if (this.targetTexture != null)
		{
			GameObject gameObject = new GameObject("WhiteCamera");
			Camera camera = gameObject.AddComponent<Camera>();
			camera.cullingMask = 0;
			camera.targetTexture = this.targetTexture;
			camera.backgroundColor = new Color(1f, 1f, 1f, 1f);
			camera.Render();
			UnityEngine.Object.Destroy(gameObject);
		}
	}

	// Token: 0x060005F3 RID: 1523 RVA: 0x0002AE1C File Offset: 0x0002901C
	private void LateUpdate()
	{
		this.SetRenderTextureWhite();
		this.renderCamera.RenderWithShader(this.pShader, string.Empty);
	}

	// Token: 0x060005F4 RID: 1524 RVA: 0x0002AE3C File Offset: 0x0002903C
	private void OnDestroy()
	{
		if (this.renderCamera != null)
		{
			UnityEngine.Object.Destroy(this.renderCamera.gameObject);
		}
		if (this.targetTexture != null)
		{
			UnityEngine.Object.Destroy(this.targetTexture);
		}
		if (this.pProjectorGO != null)
		{
			UnityEngine.Object.Destroy(this.pProjectorGO);
		}
	}

	// Token: 0x060005F5 RID: 1525 RVA: 0x0002AEA4 File Offset: 0x000290A4
	public void SetProjectorTexture(Texture aTexture)
	{
		Projector component = this.pProjectorGO.GetComponent<Projector>();
		component.material.SetTexture("_ShadowTex", aTexture);
	}

	// Token: 0x0400067F RID: 1663
	public Camera renderCamera;

	// Token: 0x04000680 RID: 1664
	public RenderTexture targetTexture;

	// Token: 0x04000681 RID: 1665
	private GameObject pProjectorGO;

	// Token: 0x04000682 RID: 1666
	private Shader pShader;
}
