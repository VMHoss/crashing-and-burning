using System;
using UnityEngine;

// Token: 0x02000199 RID: 409
public class KGFMapIcon : KGFObject, KGFIValidator, KGFIMapIcon
{
	// Token: 0x06000BF3 RID: 3059 RVA: 0x000570B4 File Offset: 0x000552B4
	protected override void KGFAwake()
	{
		this.SetVisibility(this.itsDataMapIcon.itsIsVisible);
		this.itsTransformCache = base.transform;
		base.KGFAwake();
	}

	// Token: 0x06000BF4 RID: 3060 RVA: 0x000570DC File Offset: 0x000552DC
	public void SetToolTipText(string theToolTipText)
	{
		this.itsDataMapIcon.itsToolTip = theToolTipText;
	}

	// Token: 0x06000BF5 RID: 3061 RVA: 0x000570EC File Offset: 0x000552EC
	public string GetToolTipText()
	{
		return this.itsDataMapIcon.itsToolTip;
	}

	// Token: 0x06000BF6 RID: 3062 RVA: 0x000570FC File Offset: 0x000552FC
	public float GetIconScale()
	{
		return this.itsDataMapIcon.itsIconScale;
	}

	// Token: 0x06000BF7 RID: 3063 RVA: 0x0005710C File Offset: 0x0005530C
	private GameObject CreateRepresentation()
	{
		if (this.itsShaderMapIcon == null)
		{
			this.itsShaderMapIcon = Shader.Find("ColorTextureAlpha");
			if (this.itsShaderMapIcon == null)
			{
				KGFMapIcon.LogError("Cannot find shader ColorTextureAlpha", typeof(KGFMapSystem).Name, this);
				return null;
			}
		}
		if (this.itsDataMapIcon.itsTextureIcon == null)
		{
			KGFMapIcon.LogError("itsDataMapIcon.itsTextureIcon is null", typeof(KGFMapSystem).Name, this);
			return null;
		}
		if (this.itsShaderMapIcon != null & this.itsDataMapIcon.itsTextureIcon != null)
		{
			GameObject gameObject = KGFMapSystem.GenerateTexturePlane(this.itsDataMapIcon.itsTextureIcon, this.itsShaderMapIcon);
			this.itsMaterial = gameObject.renderer.sharedMaterial;
			return gameObject;
		}
		return null;
	}

	// Token: 0x06000BF8 RID: 3064 RVA: 0x000571E8 File Offset: 0x000553E8
	public bool GetIsBlinking()
	{
		return this.itsDataMapIcon.itsBlinking;
	}

	// Token: 0x06000BF9 RID: 3065 RVA: 0x000571F8 File Offset: 0x000553F8
	public void SetIsBlinking(bool theActivate)
	{
		this.itsDataMapIcon.itsBlinking = theActivate;
		if (!theActivate)
		{
			this.itsMaterial.color = new Color(this.itsDataMapIcon.itsColor.r, this.itsDataMapIcon.itsColor.g, this.itsDataMapIcon.itsColor.b, 1f);
		}
	}

	// Token: 0x06000BFA RID: 3066 RVA: 0x0005725C File Offset: 0x0005545C
	public Transform GetTransform()
	{
		if (this == null)
		{
			return null;
		}
		if (base.gameObject == null)
		{
			return null;
		}
		return base.transform;
	}

	// Token: 0x06000BFB RID: 3067 RVA: 0x00057290 File Offset: 0x00055490
	public string GetGameObjectName()
	{
		return base.gameObject.name;
	}

	// Token: 0x06000BFC RID: 3068 RVA: 0x000572A0 File Offset: 0x000554A0
	public virtual string GetCategory()
	{
		return this.itsDataMapIcon.itsCategory;
	}

	// Token: 0x06000BFD RID: 3069 RVA: 0x000572B0 File Offset: 0x000554B0
	public Color GetColor()
	{
		return this.itsDataMapIcon.itsColor;
	}

	// Token: 0x06000BFE RID: 3070 RVA: 0x000572C0 File Offset: 0x000554C0
	public Texture2D GetTextureArrow()
	{
		return this.itsDataMapIcon.itsTextureArrow;
	}

	// Token: 0x06000BFF RID: 3071 RVA: 0x000572D0 File Offset: 0x000554D0
	public bool GetRotate()
	{
		return this.itsDataMapIcon.itsRotate;
	}

	// Token: 0x06000C00 RID: 3072 RVA: 0x000572E0 File Offset: 0x000554E0
	public virtual bool GetIsVisible()
	{
		return this.itsMapIconIsVisible;
	}

	// Token: 0x06000C01 RID: 3073 RVA: 0x000572E8 File Offset: 0x000554E8
	public bool GetIsArrowVisible()
	{
		return this.itsDataMapIcon.itsUseArrow;
	}

	// Token: 0x06000C02 RID: 3074 RVA: 0x000572F8 File Offset: 0x000554F8
	public GameObject GetRepresentation()
	{
		if (this.itsDataMapIcon.itsRepresentation == null)
		{
			this.itsDataMapIcon.itsRepresentation = this.CreateRepresentation();
		}
		return this.itsDataMapIcon.itsRepresentation;
	}

	// Token: 0x06000C03 RID: 3075 RVA: 0x00057338 File Offset: 0x00055538
	public KGFMessageList Validate()
	{
		KGFMessageList kgfmessageList = new KGFMessageList();
		if (this.itsDataMapIcon.itsCategory == string.Empty)
		{
			kgfmessageList.AddError("itsDataMapIcon.itsCategory is empty");
		}
		if (this.itsDataMapIcon.itsTextureIcon == null)
		{
			kgfmessageList.AddError("itsDataMapIcon.itsTextureIcon is null");
		}
		return kgfmessageList;
	}

	// Token: 0x06000C04 RID: 3076 RVA: 0x00057394 File Offset: 0x00055594
	public void SetColor(Color theColor)
	{
		this.itsDataMapIcon.itsColor = theColor;
		if (this.itsMapSystem == null)
		{
			this.itsMapSystem = KGFAccessor.GetObject<KGFMapSystem>();
		}
		if (this.itsMapSystem != null)
		{
			this.itsMapSystem.UpdateIcon(this);
		}
	}

	// Token: 0x06000C05 RID: 3077 RVA: 0x000573E8 File Offset: 0x000555E8
	public void SetCategory(string theCategory)
	{
		this.itsDataMapIcon.itsCategory = theCategory;
	}

	// Token: 0x06000C06 RID: 3078 RVA: 0x000573F8 File Offset: 0x000555F8
	public void SetVisibility(bool theVisibility)
	{
		this.itsMapIconIsVisible = theVisibility;
		if (this.itsMapSystem == null)
		{
			this.itsMapSystem = KGFAccessor.GetObject<KGFMapSystem>();
		}
		if (this.itsMapSystem != null)
		{
			this.itsMapSystem.RefreshIconsVisibility();
		}
	}

	// Token: 0x06000C07 RID: 3079 RVA: 0x00057444 File Offset: 0x00055644
	public void SetTextureIcon(Texture2D theTexture)
	{
		this.itsDataMapIcon.itsTextureIcon = theTexture;
		if (this.itsDataMapIcon.itsRepresentation == null)
		{
			this.itsDataMapIcon.itsRepresentation = this.CreateRepresentation();
		}
		if (this.itsDataMapIcon.itsRepresentation != null)
		{
			MeshRenderer component = this.itsDataMapIcon.itsRepresentation.GetComponent<MeshRenderer>();
			if (component != null)
			{
				component.material.mainTexture = this.itsDataMapIcon.itsTextureIcon;
			}
		}
	}

	// Token: 0x06000C08 RID: 3080 RVA: 0x000574D0 File Offset: 0x000556D0
	public void SetTextureArrow(Texture2D theTexture)
	{
		this.itsDataMapIcon.itsTextureArrow = theTexture;
		if (this.itsMapSystem == null)
		{
			this.itsMapSystem = KGFAccessor.GetObject<KGFMapSystem>();
		}
		if (this.itsMapSystem != null)
		{
			this.itsMapSystem.UpdateIcon(this);
		}
	}

	// Token: 0x06000C09 RID: 3081 RVA: 0x00057524 File Offset: 0x00055724
	public void SetArrowUsage(bool theIsArrowUsed)
	{
		this.itsDataMapIcon.itsUseArrow = theIsArrowUsed;
		if (this.itsMapSystem == null)
		{
			this.itsMapSystem = KGFAccessor.GetObject<KGFMapSystem>();
		}
		if (this.itsMapSystem != null)
		{
			this.itsMapSystem.RefreshIconsVisibility();
		}
	}

	// Token: 0x06000C0A RID: 3082 RVA: 0x00057578 File Offset: 0x00055778
	private void Update()
	{
		if (this.itsDataMapIcon.itsRevealFogOfWar)
		{
			if (this.itsMapSystem == null)
			{
				this.itsMapSystem = KGFAccessor.GetObject<KGFMapSystem>();
			}
			if (this.itsMapSystem != null)
			{
				this.itsMapSystem.RevealFogOfWarAtPoint(this.itsTransformCache.position);
			}
		}
		if (this.itsDataMapIcon.itsBlinking)
		{
			float a = KGFUtility.PingPong(Time.time, 1f, 0f, 0f, 0.6f);
			this.itsMaterial.color = new Color(this.itsDataMapIcon.itsColor.r, this.itsDataMapIcon.itsColor.g, this.itsDataMapIcon.itsColor.b, a);
		}
	}

	// Token: 0x06000C0B RID: 3083 RVA: 0x00057648 File Offset: 0x00055848
	public static void LogError(string theError, string theCategory, MonoBehaviour theObject)
	{
		Debug.LogError(string.Format("{0} - {1}", theCategory, theError));
	}

	// Token: 0x04000BBE RID: 3006
	public KGFMapIcon.KGFDataMapIcon itsDataMapIcon = new KGFMapIcon.KGFDataMapIcon();

	// Token: 0x04000BBF RID: 3007
	private bool itsMapIconIsVisible;

	// Token: 0x04000BC0 RID: 3008
	private KGFMapSystem itsMapSystem;

	// Token: 0x04000BC1 RID: 3009
	private Shader itsShaderMapIcon;

	// Token: 0x04000BC2 RID: 3010
	private Transform itsTransformCache;

	// Token: 0x04000BC3 RID: 3011
	private Material itsMaterial;

	// Token: 0x0200019A RID: 410
	[Serializable]
	public class KGFDataMapIcon
	{
		// Token: 0x04000BC4 RID: 3012
		public string itsCategory = string.Empty;

		// Token: 0x04000BC5 RID: 3013
		public Texture2D itsTextureIcon;

		// Token: 0x04000BC6 RID: 3014
		public Texture2D itsTextureArrow;

		// Token: 0x04000BC7 RID: 3015
		public bool itsRotate;

		// Token: 0x04000BC8 RID: 3016
		public Color itsColor = Color.white;

		// Token: 0x04000BC9 RID: 3017
		public bool itsIsVisible = true;

		// Token: 0x04000BCA RID: 3018
		public bool itsUseArrow = true;

		// Token: 0x04000BCB RID: 3019
		public bool itsRevealFogOfWar;

		// Token: 0x04000BCC RID: 3020
		public string itsToolTip = string.Empty;

		// Token: 0x04000BCD RID: 3021
		public float itsIconScale = 1f;

		// Token: 0x04000BCE RID: 3022
		public bool itsBlinking;

		// Token: 0x04000BCF RID: 3023
		public GameObject itsRepresentation;
	}
}
