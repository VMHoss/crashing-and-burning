using System;
using UnityEngine;

// Token: 0x020000C9 RID: 201
public class TrailScript : MonoBehaviour
{
	// Token: 0x170000E5 RID: 229
	// (get) Token: 0x06000601 RID: 1537 RVA: 0x0002B214 File Offset: 0x00029414
	// (set) Token: 0x06000602 RID: 1538 RVA: 0x0002B21C File Offset: 0x0002941C
	public bool generatingTrail
	{
		get
		{
			return this.pGeneratingTrail;
		}
		set
		{
			if (!this.pGeneratingTrail && value)
			{
				this.pFirstSectionInTrail = true;
			}
			this.pGeneratingTrail = value;
		}
	}

	// Token: 0x06000603 RID: 1539 RVA: 0x0002B240 File Offset: 0x00029440
	public static TrailScript AddTrail(GameObject aGO)
	{
		return TrailScript.AddTrail(aGO, TrailScript.Normal.Y);
	}

	// Token: 0x06000604 RID: 1540 RVA: 0x0002B24C File Offset: 0x0002944C
	public static TrailScript AddTrail(GameObject aGO, TrailScript.Normal aNormal)
	{
		GameObject gameObject = new GameObject(aGO.name + "Trail");
		TrailScript trailScript = gameObject.AddComponent<TrailScript>();
		trailScript.followGO = aGO;
		trailScript.material = (Resources.Load("Misc/TrailMaterial") as Material);
		trailScript.normal = aNormal;
		return trailScript;
	}

	// Token: 0x06000605 RID: 1541 RVA: 0x0002B29C File Offset: 0x0002949C
	private void Start()
	{
		this.trailSections = new TrailSection[this.maxSections];
		for (int i = 0; i < this.maxSections; i++)
		{
			this.trailSections[i] = new TrailSection();
		}
		if (base.gameObject.GetComponent<MeshFilter>() == null)
		{
			base.gameObject.AddComponent<MeshFilter>();
		}
		if (base.GetComponent<MeshFilter>().mesh == null)
		{
			base.GetComponent<MeshFilter>().mesh = new Mesh();
		}
		if (base.gameObject.GetComponent<MeshRenderer>() == null)
		{
			base.gameObject.AddComponent<MeshRenderer>();
		}
		base.gameObject.renderer.sharedMaterial = this.material;
		this.AddSection(this.followGO.transform.position, 1f);
	}

	// Token: 0x06000606 RID: 1542 RVA: 0x0002B37C File Offset: 0x0002957C
	public void Update()
	{
		if (Data.pause)
		{
			return;
		}
		if (this.followGO != null && this.generatingTrail)
		{
			this.AddSection(this.followGO.transform.position, 1f);
		}
		this.updated = true;
	}

	// Token: 0x06000607 RID: 1543 RVA: 0x0002B3D4 File Offset: 0x000295D4
	private void AddSection(Vector3 pos, float intensity)
	{
		Vector3 rhs = Vector3.zero;
		switch (this.normal)
		{
		case TrailScript.Normal.X:
			rhs = this.followGO.transform.right;
			break;
		case TrailScript.Normal.Y:
			rhs = this.followGO.transform.up;
			break;
		case TrailScript.Normal.Z:
			rhs = this.followGO.transform.forward;
			break;
		}
		if (intensity > 1f)
		{
			intensity = 1f;
		}
		if (intensity < 0f)
		{
			return;
		}
		this.curSection = this.trailSections[this.curNextNumSection % this.maxSections];
		this.curSection.pos = pos;
		this.curSection.normal = rhs;
		this.curSection.intensity = intensity;
		this.curSection.colorMod = 0f;
		if (this.curNextNumSection > 0)
		{
			TrailSection trailSection = this.trailSections[(this.curNextNumSection - 1) % this.maxSections];
			Vector3 lhs = this.curSection.pos - trailSection.pos;
			Vector3 normalized = Vector3.Cross(lhs, rhs).normalized;
			Vector3 b = normalized * this.markWidth * 0.5f;
			this.curSection.posl = this.curSection.pos + b;
			this.curSection.posr = this.curSection.pos - b;
			this.curSection.tangent = new Vector4(normalized.x, normalized.y, normalized.z, 1f);
			if (this.pFirstSectionInTrail)
			{
				this.curSection.lastIndex = -1;
				this.pFirstSectionInTrail = false;
			}
			else
			{
				this.curSection.lastIndex = this.curNextNumSection - 1;
			}
			if (this.curNextNumSection == 1)
			{
				trailSection.tangent = this.curSection.tangent;
				trailSection.posl = this.curSection.pos + b;
				trailSection.posr = this.curSection.pos - b;
			}
		}
		this.curNextNumSection++;
		this.updated = true;
	}

	// Token: 0x06000608 RID: 1544 RVA: 0x0002B614 File Offset: 0x00029814
	private void LateUpdate()
	{
		if (!this.updated)
		{
			return;
		}
		this.updated = false;
		Mesh mesh = base.GetComponent<MeshFilter>().mesh;
		mesh.Clear();
		int num = 0;
		int num2 = 0;
		while (num2 < this.curNextNumSection && num2 < this.maxSections)
		{
			if (this.trailSections[num2].lastIndex != -1 && this.trailSections[num2].lastIndex > this.curNextNumSection - this.maxSections)
			{
				num++;
			}
			num2++;
		}
		Vector3[] array = new Vector3[num * 4];
		Vector3[] array2 = new Vector3[num * 4];
		Vector4[] array3 = new Vector4[num * 4];
		Color[] array4 = new Color[num * 4];
		Vector2[] array5 = new Vector2[num * 4];
		int[] array6 = new int[num * 6];
		num = 0;
		int num3 = 0;
		while (num3 < this.curNextNumSection && num3 < this.maxSections)
		{
			TrailSection trailSection = this.trailSections[num3];
			if (trailSection.lastIndex != -1 && trailSection.lastIndex > this.curNextNumSection - this.maxSections)
			{
				trailSection.intensity = Mathf.Clamp01(trailSection.intensity - this.trailDecay * Time.timeScale);
				trailSection.colorMod += 0.025f;
				if (trailSection.colorMod > 1f)
				{
					trailSection.colorMod = 0f;
				}
				TrailSection trailSection2 = this.trailSections[trailSection.lastIndex % this.maxSections];
				array[num * 4] = trailSection2.posl;
				array[num * 4 + 1] = trailSection2.posr;
				array[num * 4 + 2] = trailSection.posl;
				array[num * 4 + 3] = trailSection.posr;
				array2[num * 4] = trailSection2.normal;
				array2[num * 4 + 1] = trailSection2.normal;
				array2[num * 4 + 2] = trailSection.normal;
				array2[num * 4 + 3] = trailSection.normal;
				array3[num * 4] = trailSection2.tangent;
				array3[num * 4 + 1] = trailSection2.tangent;
				array3[num * 4 + 2] = trailSection.tangent;
				array3[num * 4 + 3] = trailSection.tangent;
				Color color = this.Custom(trailSection2.colorMod, trailSection2.intensity);
				Color color2 = this.Custom(trailSection.colorMod, trailSection.intensity);
				array4[num * 4] = color;
				array4[num * 4 + 1] = color;
				array4[num * 4 + 2] = color2;
				array4[num * 4 + 3] = color2;
				array5[num * 4] = new Vector2(0f, 0f);
				array5[num * 4 + 1] = new Vector2(1f, 0f);
				array5[num * 4 + 2] = new Vector2(0f, 1f);
				array5[num * 4 + 3] = new Vector2(1f, 1f);
				array6[num * 6] = num * 4;
				array6[num * 6 + 2] = num * 4 + 1;
				array6[num * 6 + 1] = num * 4 + 2;
				array6[num * 6 + 3] = num * 4 + 2;
				array6[num * 6 + 5] = num * 4 + 1;
				array6[num * 6 + 4] = num * 4 + 3;
				num++;
				if ((double)trailSection.intensity < 0.01)
				{
					trailSection.lastIndex = -1;
				}
			}
			num3++;
		}
		mesh.vertices = array;
		mesh.normals = array2;
		mesh.tangents = array3;
		mesh.triangles = array6;
		mesh.colors = array4;
		mesh.uv = array5;
	}

	// Token: 0x06000609 RID: 1545 RVA: 0x0002BA44 File Offset: 0x00029C44
	private float Bump(float x)
	{
		if (Mathf.Abs(x) > 1f)
		{
			return 0f;
		}
		return 1f - x * x;
	}

	// Token: 0x0600060A RID: 1546 RVA: 0x0002BA68 File Offset: 0x00029C68
	private Color Custom(float t, float a)
	{
		Color result = this.trailColor;
		result.a = 2f * a;
		return result;
	}

	// Token: 0x0600060B RID: 1547 RVA: 0x0002BA8C File Offset: 0x00029C8C
	private Color BlueWhite(float t, float a)
	{
		Color result;
		result.r = 0.5f;
		result.g = 1f;
		result.b = 0.5f;
		result.a = 2f * a;
		return result;
	}

	// Token: 0x0600060C RID: 1548 RVA: 0x0002BACC File Offset: 0x00029CCC
	private Color RainBow(float t, float a)
	{
		Color result;
		result.r = this.Bump(4f * (t - 0.75f)) + this.Bump(4f * t);
		result.g = this.Bump(4f * (t - 0.5f));
		result.b = this.Bump(4f * (t - 0.25f)) + this.Bump(4f * (t - 1f));
		result.a = a;
		return result;
	}

	// Token: 0x04000694 RID: 1684
	public int maxSections = 128;

	// Token: 0x04000695 RID: 1685
	public float markWidth = 0.1f;

	// Token: 0x04000696 RID: 1686
	public Material material;

	// Token: 0x04000697 RID: 1687
	public TrailScript.Normal normal = TrailScript.Normal.Y;

	// Token: 0x04000698 RID: 1688
	public GameObject followGO;

	// Token: 0x04000699 RID: 1689
	public Color trailColor = Color.white;

	// Token: 0x0400069A RID: 1690
	public float trailDecay = 0.1f;

	// Token: 0x0400069B RID: 1691
	private bool pGeneratingTrail = true;

	// Token: 0x0400069C RID: 1692
	private bool pFirstSectionInTrail = true;

	// Token: 0x0400069D RID: 1693
	private int curNextNumSection;

	// Token: 0x0400069E RID: 1694
	private TrailSection curSection;

	// Token: 0x0400069F RID: 1695
	private TrailSection[] trailSections;

	// Token: 0x040006A0 RID: 1696
	private bool updated;

	// Token: 0x020000CA RID: 202
	public enum Normal
	{
		// Token: 0x040006A2 RID: 1698
		X,
		// Token: 0x040006A3 RID: 1699
		Y,
		// Token: 0x040006A4 RID: 1700
		Z
	}
}
