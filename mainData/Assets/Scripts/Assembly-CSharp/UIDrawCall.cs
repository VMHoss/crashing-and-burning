using System;
using UnityEngine;

// Token: 0x0200005A RID: 90
[AddComponentMenu("NGUI/Internal/Draw Call")]
[ExecuteInEditMode]
public class UIDrawCall : MonoBehaviour
{
	// Token: 0x1700004F RID: 79
	// (get) Token: 0x060002EE RID: 750 RVA: 0x00013BF4 File Offset: 0x00011DF4
	// (set) Token: 0x060002EF RID: 751 RVA: 0x00013BFC File Offset: 0x00011DFC
	public bool depthPass
	{
		get
		{
			return this.mDepthPass;
		}
		set
		{
			if (this.mDepthPass != value)
			{
				this.mDepthPass = value;
				this.mReset = true;
			}
		}
	}

	// Token: 0x17000050 RID: 80
	// (get) Token: 0x060002F0 RID: 752 RVA: 0x00013C18 File Offset: 0x00011E18
	public Transform cachedTransform
	{
		get
		{
			if (this.mTrans == null)
			{
				this.mTrans = base.transform;
			}
			return this.mTrans;
		}
	}

	// Token: 0x17000051 RID: 81
	// (get) Token: 0x060002F1 RID: 753 RVA: 0x00013C40 File Offset: 0x00011E40
	// (set) Token: 0x060002F2 RID: 754 RVA: 0x00013C48 File Offset: 0x00011E48
	public Material material
	{
		get
		{
			return this.mSharedMat;
		}
		set
		{
			this.mSharedMat = value;
		}
	}

	// Token: 0x17000052 RID: 82
	// (get) Token: 0x060002F3 RID: 755 RVA: 0x00013C54 File Offset: 0x00011E54
	public int triangles
	{
		get
		{
			Mesh mesh = (!this.mEven) ? this.mMesh1 : this.mMesh0;
			return (!(mesh != null)) ? 0 : (mesh.vertexCount >> 1);
		}
	}

	// Token: 0x17000053 RID: 83
	// (get) Token: 0x060002F4 RID: 756 RVA: 0x00013C98 File Offset: 0x00011E98
	public bool isClipped
	{
		get
		{
			return this.mClippedMat != null;
		}
	}

	// Token: 0x17000054 RID: 84
	// (get) Token: 0x060002F5 RID: 757 RVA: 0x00013CA8 File Offset: 0x00011EA8
	// (set) Token: 0x060002F6 RID: 758 RVA: 0x00013CB0 File Offset: 0x00011EB0
	public UIDrawCall.Clipping clipping
	{
		get
		{
			return this.mClipping;
		}
		set
		{
			if (this.mClipping != value)
			{
				this.mClipping = value;
				this.mReset = true;
			}
		}
	}

	// Token: 0x17000055 RID: 85
	// (get) Token: 0x060002F7 RID: 759 RVA: 0x00013CCC File Offset: 0x00011ECC
	// (set) Token: 0x060002F8 RID: 760 RVA: 0x00013CD4 File Offset: 0x00011ED4
	public Vector4 clipRange
	{
		get
		{
			return this.mClipRange;
		}
		set
		{
			this.mClipRange = value;
		}
	}

	// Token: 0x17000056 RID: 86
	// (get) Token: 0x060002F9 RID: 761 RVA: 0x00013CE0 File Offset: 0x00011EE0
	// (set) Token: 0x060002FA RID: 762 RVA: 0x00013CE8 File Offset: 0x00011EE8
	public Vector2 clipSoftness
	{
		get
		{
			return this.mClipSoft;
		}
		set
		{
			this.mClipSoft = value;
		}
	}

	// Token: 0x060002FB RID: 763 RVA: 0x00013CF4 File Offset: 0x00011EF4
	private Mesh GetMesh(ref bool rebuildIndices, int vertexCount)
	{
		this.mEven = !this.mEven;
		if (this.mEven)
		{
			if (this.mMesh0 == null)
			{
				this.mMesh0 = new Mesh();
				this.mMesh0.hideFlags = HideFlags.DontSave;
				this.mMesh0.name = "Mesh0 for " + this.mSharedMat.name;
				this.mMesh0.MarkDynamic();
				rebuildIndices = true;
			}
			else if (rebuildIndices || this.mMesh0.vertexCount != vertexCount)
			{
				rebuildIndices = true;
				this.mMesh0.Clear();
			}
			return this.mMesh0;
		}
		if (this.mMesh1 == null)
		{
			this.mMesh1 = new Mesh();
			this.mMesh1.hideFlags = HideFlags.DontSave;
			this.mMesh1.name = "Mesh1 for " + this.mSharedMat.name;
			this.mMesh1.MarkDynamic();
			rebuildIndices = true;
		}
		else if (rebuildIndices || this.mMesh1.vertexCount != vertexCount)
		{
			rebuildIndices = true;
			this.mMesh1.Clear();
		}
		return this.mMesh1;
	}

	// Token: 0x060002FC RID: 764 RVA: 0x00013E2C File Offset: 0x0001202C
	private void UpdateMaterials()
	{
		bool flag = this.mClipping != UIDrawCall.Clipping.None;
		if (flag)
		{
			Shader shader = null;
			if (this.mClipping != UIDrawCall.Clipping.None)
			{
				string text = this.mSharedMat.shader.name;
				text = text.Replace(" (AlphaClip)", string.Empty);
				text = text.Replace(" (SoftClip)", string.Empty);
				if (this.mClipping == UIDrawCall.Clipping.HardClip || this.mClipping == UIDrawCall.Clipping.AlphaClip)
				{
					shader = Shader.Find(text + " (AlphaClip)");
				}
				else if (this.mClipping == UIDrawCall.Clipping.SoftClip)
				{
					shader = Shader.Find(text + " (SoftClip)");
				}
				if (shader == null)
				{
					this.mClipping = UIDrawCall.Clipping.None;
				}
			}
			if (shader != null)
			{
				if (this.mClippedMat == null)
				{
					this.mClippedMat = new Material(this.mSharedMat);
					this.mClippedMat.hideFlags = HideFlags.DontSave;
				}
				this.mClippedMat.shader = shader;
				this.mClippedMat.CopyPropertiesFromMaterial(this.mSharedMat);
			}
			else if (this.mClippedMat != null)
			{
				NGUITools.Destroy(this.mClippedMat);
				this.mClippedMat = null;
			}
		}
		else if (this.mClippedMat != null)
		{
			NGUITools.Destroy(this.mClippedMat);
			this.mClippedMat = null;
		}
		if (this.mDepthPass)
		{
			if (this.mDepthMat == null)
			{
				Shader shader2 = Shader.Find("Unlit/Depth Cutout");
				this.mDepthMat = new Material(shader2);
				this.mDepthMat.hideFlags = HideFlags.DontSave;
			}
			this.mDepthMat.mainTexture = this.mSharedMat.mainTexture;
		}
		else if (this.mDepthMat != null)
		{
			NGUITools.Destroy(this.mDepthMat);
			this.mDepthMat = null;
		}
		Material material = (!(this.mClippedMat != null)) ? this.mSharedMat : this.mClippedMat;
		if (this.mDepthMat != null)
		{
			if (this.mRen.sharedMaterials != null && this.mRen.sharedMaterials.Length == 2 && this.mRen.sharedMaterials[1] == material)
			{
				return;
			}
			this.mRen.sharedMaterials = new Material[]
			{
				this.mDepthMat,
				material
			};
		}
		else if (this.mRen.sharedMaterial != material)
		{
			this.mRen.sharedMaterials = new Material[]
			{
				material
			};
		}
	}

	// Token: 0x060002FD RID: 765 RVA: 0x000140DC File Offset: 0x000122DC
	public void Set(BetterList<Vector3> verts, BetterList<Vector3> norms, BetterList<Vector4> tans, BetterList<Vector2> uvs, BetterList<Color32> cols)
	{
		int size = verts.size;
		if (size > 0 && size == uvs.size && size == cols.size && size % 4 == 0)
		{
			if (this.mFilter == null)
			{
				this.mFilter = base.gameObject.GetComponent<MeshFilter>();
			}
			if (this.mFilter == null)
			{
				this.mFilter = base.gameObject.AddComponent<MeshFilter>();
			}
			if (this.mRen == null)
			{
				this.mRen = base.gameObject.GetComponent<MeshRenderer>();
			}
			if (this.mRen == null)
			{
				this.mRen = base.gameObject.AddComponent<MeshRenderer>();
				this.UpdateMaterials();
			}
			else if (this.mClippedMat != null && this.mClippedMat.mainTexture != this.mSharedMat.mainTexture)
			{
				this.UpdateMaterials();
			}
			if (verts.size < 65000)
			{
				int num = (size >> 1) * 3;
				bool flag = this.mIndices == null || this.mIndices.Length != num;
				if (flag)
				{
					this.mIndices = new int[num];
					int num2 = 0;
					for (int i = 0; i < size; i += 4)
					{
						this.mIndices[num2++] = i;
						this.mIndices[num2++] = i + 1;
						this.mIndices[num2++] = i + 2;
						this.mIndices[num2++] = i + 2;
						this.mIndices[num2++] = i + 3;
						this.mIndices[num2++] = i;
					}
				}
				Mesh mesh = this.GetMesh(ref flag, verts.size);
				mesh.vertices = verts.ToArray();
				if (norms != null)
				{
					mesh.normals = norms.ToArray();
				}
				if (tans != null)
				{
					mesh.tangents = tans.ToArray();
				}
				mesh.uv = uvs.ToArray();
				mesh.colors32 = cols.ToArray();
				if (flag)
				{
					mesh.triangles = this.mIndices;
				}
				mesh.RecalculateBounds();
				this.mFilter.mesh = mesh;
			}
			else
			{
				if (this.mFilter.mesh != null)
				{
					this.mFilter.mesh.Clear();
				}
				Debug.LogError("Too many vertices on one panel: " + verts.size);
			}
		}
		else
		{
			if (this.mFilter.mesh != null)
			{
				this.mFilter.mesh.Clear();
			}
			Debug.LogError("UIWidgets must fill the buffer with 4 vertices per quad. Found " + size);
		}
	}

	// Token: 0x060002FE RID: 766 RVA: 0x000143A8 File Offset: 0x000125A8
	private void OnWillRenderObject()
	{
		if (this.mReset)
		{
			this.mReset = false;
			this.UpdateMaterials();
		}
		if (this.mClippedMat != null)
		{
			this.mClippedMat.mainTextureOffset = new Vector2(-this.mClipRange.x / this.mClipRange.z, -this.mClipRange.y / this.mClipRange.w);
			this.mClippedMat.mainTextureScale = new Vector2(1f / this.mClipRange.z, 1f / this.mClipRange.w);
			Vector2 v = new Vector2(1000f, 1000f);
			if (this.mClipSoft.x > 0f)
			{
				v.x = this.mClipRange.z / this.mClipSoft.x;
			}
			if (this.mClipSoft.y > 0f)
			{
				v.y = this.mClipRange.w / this.mClipSoft.y;
			}
			this.mClippedMat.SetVector("_ClipSharpness", v);
		}
	}

	// Token: 0x060002FF RID: 767 RVA: 0x000144E0 File Offset: 0x000126E0
	private void OnDestroy()
	{
		NGUITools.DestroyImmediate(this.mMesh0);
		NGUITools.DestroyImmediate(this.mMesh1);
		NGUITools.DestroyImmediate(this.mClippedMat);
		NGUITools.DestroyImmediate(this.mDepthMat);
	}

	// Token: 0x0400030F RID: 783
	private Transform mTrans;

	// Token: 0x04000310 RID: 784
	private Material mSharedMat;

	// Token: 0x04000311 RID: 785
	private Mesh mMesh0;

	// Token: 0x04000312 RID: 786
	private Mesh mMesh1;

	// Token: 0x04000313 RID: 787
	private MeshFilter mFilter;

	// Token: 0x04000314 RID: 788
	private MeshRenderer mRen;

	// Token: 0x04000315 RID: 789
	private UIDrawCall.Clipping mClipping;

	// Token: 0x04000316 RID: 790
	private Vector4 mClipRange;

	// Token: 0x04000317 RID: 791
	private Vector2 mClipSoft;

	// Token: 0x04000318 RID: 792
	private Material mClippedMat;

	// Token: 0x04000319 RID: 793
	private Material mDepthMat;

	// Token: 0x0400031A RID: 794
	private int[] mIndices;

	// Token: 0x0400031B RID: 795
	private bool mDepthPass;

	// Token: 0x0400031C RID: 796
	private bool mReset = true;

	// Token: 0x0400031D RID: 797
	private bool mEven = true;

	// Token: 0x0200005B RID: 91
	public enum Clipping
	{
		// Token: 0x0400031F RID: 799
		None,
		// Token: 0x04000320 RID: 800
		HardClip,
		// Token: 0x04000321 RID: 801
		AlphaClip,
		// Token: 0x04000322 RID: 802
		SoftClip
	}
}
