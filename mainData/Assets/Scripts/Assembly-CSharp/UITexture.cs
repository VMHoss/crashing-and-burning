using System;
using UnityEngine;

// Token: 0x02000095 RID: 149
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/Texture")]
public class UITexture : UIWidget
{
	// Token: 0x170000D7 RID: 215
	// (get) Token: 0x060004D6 RID: 1238 RVA: 0x00023010 File Offset: 0x00021210
	// (set) Token: 0x060004D7 RID: 1239 RVA: 0x00023018 File Offset: 0x00021218
	public Rect uvRect
	{
		get
		{
			return this.mRect;
		}
		set
		{
			if (this.mRect != value)
			{
				this.mRect = value;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x170000D8 RID: 216
	// (get) Token: 0x060004D8 RID: 1240 RVA: 0x00023038 File Offset: 0x00021238
	// (set) Token: 0x060004D9 RID: 1241 RVA: 0x0002309C File Offset: 0x0002129C
	public Shader shader
	{
		get
		{
			if (this.mShader == null)
			{
				Material material = this.material;
				if (material != null)
				{
					this.mShader = material.shader;
				}
				if (this.mShader == null)
				{
					this.mShader = Shader.Find("Unlit/Texture");
				}
			}
			return this.mShader;
		}
		set
		{
			if (this.mShader != value)
			{
				this.mShader = value;
				Material material = this.material;
				if (material != null)
				{
					material.shader = value;
				}
				this.mPMA = -1;
			}
		}
	}

	// Token: 0x170000D9 RID: 217
	// (get) Token: 0x060004DA RID: 1242 RVA: 0x000230E4 File Offset: 0x000212E4
	public bool hasDynamicMaterial
	{
		get
		{
			return this.mDynamicMat != null;
		}
	}

	// Token: 0x170000DA RID: 218
	// (get) Token: 0x060004DB RID: 1243 RVA: 0x000230F4 File Offset: 0x000212F4
	public override bool keepMaterial
	{
		get
		{
			return true;
		}
	}

	// Token: 0x170000DB RID: 219
	// (get) Token: 0x060004DC RID: 1244 RVA: 0x000230F8 File Offset: 0x000212F8
	// (set) Token: 0x060004DD RID: 1245 RVA: 0x000231A8 File Offset: 0x000213A8
	public override Material material
	{
		get
		{
			if (!this.mCreatingMat && this.mMat == null)
			{
				this.mCreatingMat = true;
				if (this.mainTexture != null)
				{
					if (this.mShader == null)
					{
						this.mShader = Shader.Find("Unlit/Texture");
					}
					this.mDynamicMat = new Material(this.mShader);
					this.mDynamicMat.hideFlags = HideFlags.DontSave;
					this.mDynamicMat.mainTexture = this.mainTexture;
					base.material = this.mDynamicMat;
					this.mPMA = 0;
				}
				this.mCreatingMat = false;
			}
			return this.mMat;
		}
		set
		{
			if (this.mDynamicMat != value && this.mDynamicMat != null)
			{
				NGUITools.Destroy(this.mDynamicMat);
				this.mDynamicMat = null;
			}
			base.material = value;
			this.mPMA = -1;
		}
	}

	// Token: 0x170000DC RID: 220
	// (get) Token: 0x060004DE RID: 1246 RVA: 0x000231F8 File Offset: 0x000213F8
	public bool premultipliedAlpha
	{
		get
		{
			if (this.mPMA == -1)
			{
				Material material = this.material;
				this.mPMA = ((!(material != null) || !(material.shader != null) || !material.shader.name.Contains("Premultiplied")) ? 0 : 1);
			}
			return this.mPMA == 1;
		}
	}

	// Token: 0x170000DD RID: 221
	// (get) Token: 0x060004DF RID: 1247 RVA: 0x00023268 File Offset: 0x00021468
	// (set) Token: 0x060004E0 RID: 1248 RVA: 0x00023298 File Offset: 0x00021498
	public override Texture mainTexture
	{
		get
		{
			return (!(this.mTexture != null)) ? base.mainTexture : this.mTexture;
		}
		set
		{
			if (this.mPanel != null && this.mMat != null)
			{
				this.mPanel.RemoveWidget(this);
			}
			if (this.mMat == null)
			{
				this.mDynamicMat = new Material(this.shader);
				this.mDynamicMat.hideFlags = HideFlags.DontSave;
				this.mMat = this.mDynamicMat;
			}
			this.mPanel = null;
			this.mTex = value;
			this.mTexture = value;
			this.mMat.mainTexture = value;
			if (base.enabled)
			{
				base.CreatePanel();
			}
		}
	}

	// Token: 0x060004E1 RID: 1249 RVA: 0x00023340 File Offset: 0x00021540
	private void OnDestroy()
	{
		NGUITools.Destroy(this.mDynamicMat);
	}

	// Token: 0x060004E2 RID: 1250 RVA: 0x00023350 File Offset: 0x00021550
	public override void MakePixelPerfect()
	{
		Texture mainTexture = this.mainTexture;
		if (mainTexture != null)
		{
			Vector3 localScale = base.cachedTransform.localScale;
			localScale.x = (float)mainTexture.width * this.uvRect.width;
			localScale.y = (float)mainTexture.height * this.uvRect.height;
			localScale.z = 1f;
			base.cachedTransform.localScale = localScale;
		}
		base.MakePixelPerfect();
	}

	// Token: 0x060004E3 RID: 1251 RVA: 0x000233D4 File Offset: 0x000215D4
	public override void OnFill(BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols)
	{
		Color color = base.color;
		color.a *= this.mPanel.alpha;
		Color32 item = (!this.premultipliedAlpha) ? color : NGUITools.ApplyPMA(color);
		verts.Add(new Vector3(1f, 0f, 0f));
		verts.Add(new Vector3(1f, -1f, 0f));
		verts.Add(new Vector3(0f, -1f, 0f));
		verts.Add(new Vector3(0f, 0f, 0f));
		uvs.Add(new Vector2(this.mRect.xMax, this.mRect.yMax));
		uvs.Add(new Vector2(this.mRect.xMax, this.mRect.yMin));
		uvs.Add(new Vector2(this.mRect.xMin, this.mRect.yMin));
		uvs.Add(new Vector2(this.mRect.xMin, this.mRect.yMax));
		cols.Add(item);
		cols.Add(item);
		cols.Add(item);
		cols.Add(item);
	}

	// Token: 0x040004F0 RID: 1264
	[SerializeField]
	[HideInInspector]
	private Rect mRect = new Rect(0f, 0f, 1f, 1f);

	// Token: 0x040004F1 RID: 1265
	[SerializeField]
	[HideInInspector]
	private Shader mShader;

	// Token: 0x040004F2 RID: 1266
	[SerializeField]
	[HideInInspector]
	private Texture mTexture;

	// Token: 0x040004F3 RID: 1267
	private Material mDynamicMat;

	// Token: 0x040004F4 RID: 1268
	private bool mCreatingMat;

	// Token: 0x040004F5 RID: 1269
	private int mPMA = -1;
}
