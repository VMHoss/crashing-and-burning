using System;
using UnityEngine;

// Token: 0x0200005D RID: 93
public class UIGeometry
{
	// Token: 0x17000057 RID: 87
	// (get) Token: 0x0600030E RID: 782 RVA: 0x000146EC File Offset: 0x000128EC
	public bool hasVertices
	{
		get
		{
			return this.verts.size > 0;
		}
	}

	// Token: 0x17000058 RID: 88
	// (get) Token: 0x0600030F RID: 783 RVA: 0x000146FC File Offset: 0x000128FC
	public bool hasTransformed
	{
		get
		{
			return this.mRtpVerts != null && this.mRtpVerts.size > 0 && this.mRtpVerts.size == this.verts.size;
		}
	}

	// Token: 0x06000310 RID: 784 RVA: 0x00014738 File Offset: 0x00012938
	public void Clear()
	{
		this.verts.Clear();
		this.uvs.Clear();
		this.cols.Clear();
		this.mRtpVerts.Clear();
	}

	// Token: 0x06000311 RID: 785 RVA: 0x00014774 File Offset: 0x00012974
	public void ApplyOffset(Vector3 pivotOffset)
	{
		for (int i = 0; i < this.verts.size; i++)
		{
			this.verts.buffer[i] += pivotOffset;
		}
	}

	// Token: 0x06000312 RID: 786 RVA: 0x000147C0 File Offset: 0x000129C0
	public void ApplyTransform(Matrix4x4 widgetToPanel, bool normals)
	{
		if (this.verts.size > 0)
		{
			this.mRtpVerts.Clear();
			int i = 0;
			int size = this.verts.size;
			while (i < size)
			{
				this.mRtpVerts.Add(widgetToPanel.MultiplyPoint3x4(this.verts[i]));
				i++;
			}
			this.mRtpNormal = widgetToPanel.MultiplyVector(Vector3.back).normalized;
			Vector3 normalized = widgetToPanel.MultiplyVector(Vector3.right).normalized;
			this.mRtpTan = new Vector4(normalized.x, normalized.y, normalized.z, -1f);
		}
		else
		{
			this.mRtpVerts.Clear();
		}
	}

	// Token: 0x06000313 RID: 787 RVA: 0x0001488C File Offset: 0x00012A8C
	public void WriteToBuffers(BetterList<Vector3> v, BetterList<Vector2> u, BetterList<Color32> c, BetterList<Vector3> n, BetterList<Vector4> t)
	{
		if (this.mRtpVerts != null && this.mRtpVerts.size > 0)
		{
			if (n == null)
			{
				for (int i = 0; i < this.mRtpVerts.size; i++)
				{
					v.Add(this.mRtpVerts.buffer[i]);
					u.Add(this.uvs.buffer[i]);
					c.Add(this.cols.buffer[i]);
				}
			}
			else
			{
				for (int j = 0; j < this.mRtpVerts.size; j++)
				{
					v.Add(this.mRtpVerts.buffer[j]);
					u.Add(this.uvs.buffer[j]);
					c.Add(this.cols.buffer[j]);
					n.Add(this.mRtpNormal);
					t.Add(this.mRtpTan);
				}
			}
		}
	}

	// Token: 0x0400032F RID: 815
	public BetterList<Vector3> verts = new BetterList<Vector3>();

	// Token: 0x04000330 RID: 816
	public BetterList<Vector2> uvs = new BetterList<Vector2>();

	// Token: 0x04000331 RID: 817
	public BetterList<Color32> cols = new BetterList<Color32>();

	// Token: 0x04000332 RID: 818
	private BetterList<Vector3> mRtpVerts = new BetterList<Vector3>();

	// Token: 0x04000333 RID: 819
	private Vector3 mRtpNormal;

	// Token: 0x04000334 RID: 820
	private Vector4 mRtpTan;
}
