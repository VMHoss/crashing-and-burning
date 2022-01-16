using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000075 RID: 117
[AddComponentMenu("NGUI/UI/Atlas")]
public class UIAtlas : MonoBehaviour
{
	// Token: 0x1700007C RID: 124
	// (get) Token: 0x060003A7 RID: 935 RVA: 0x000178B8 File Offset: 0x00015AB8
	// (set) Token: 0x060003A8 RID: 936 RVA: 0x000178E4 File Offset: 0x00015AE4
	public Material spriteMaterial
	{
		get
		{
			return (!(this.mReplacement != null)) ? this.material : this.mReplacement.spriteMaterial;
		}
		set
		{
			if (this.mReplacement != null)
			{
				this.mReplacement.spriteMaterial = value;
			}
			else if (this.material == null)
			{
				this.mPMA = 0;
				this.material = value;
			}
			else
			{
				this.MarkAsDirty();
				this.mPMA = -1;
				this.material = value;
				this.MarkAsDirty();
			}
		}
	}

	// Token: 0x1700007D RID: 125
	// (get) Token: 0x060003A9 RID: 937 RVA: 0x00017954 File Offset: 0x00015B54
	public bool premultipliedAlpha
	{
		get
		{
			if (this.mReplacement != null)
			{
				return this.mReplacement.premultipliedAlpha;
			}
			if (this.mPMA == -1)
			{
				Material spriteMaterial = this.spriteMaterial;
				this.mPMA = ((!(spriteMaterial != null) || !(spriteMaterial.shader != null) || !spriteMaterial.shader.name.Contains("Premultiplied")) ? 0 : 1);
			}
			return this.mPMA == 1;
		}
	}

	// Token: 0x1700007E RID: 126
	// (get) Token: 0x060003AA RID: 938 RVA: 0x000179E0 File Offset: 0x00015BE0
	// (set) Token: 0x060003AB RID: 939 RVA: 0x00017A0C File Offset: 0x00015C0C
	public List<UIAtlas.Sprite> spriteList
	{
		get
		{
			return (!(this.mReplacement != null)) ? this.sprites : this.mReplacement.spriteList;
		}
		set
		{
			if (this.mReplacement != null)
			{
				this.mReplacement.spriteList = value;
			}
			else
			{
				this.sprites = value;
			}
		}
	}

	// Token: 0x1700007F RID: 127
	// (get) Token: 0x060003AC RID: 940 RVA: 0x00017A38 File Offset: 0x00015C38
	public Texture texture
	{
		get
		{
			return (!(this.mReplacement != null)) ? ((!(this.material != null)) ? null : this.material.mainTexture) : this.mReplacement.texture;
		}
	}

	// Token: 0x17000080 RID: 128
	// (get) Token: 0x060003AD RID: 941 RVA: 0x00017A88 File Offset: 0x00015C88
	// (set) Token: 0x060003AE RID: 942 RVA: 0x00017AB4 File Offset: 0x00015CB4
	public UIAtlas.Coordinates coordinates
	{
		get
		{
			return (!(this.mReplacement != null)) ? this.mCoordinates : this.mReplacement.coordinates;
		}
		set
		{
			if (this.mReplacement != null)
			{
				this.mReplacement.coordinates = value;
			}
			else if (this.mCoordinates != value)
			{
				if (this.material == null || this.material.mainTexture == null)
				{
					Debug.LogError("Can't switch coordinates until the atlas material has a valid texture");
					return;
				}
				this.mCoordinates = value;
				Texture mainTexture = this.material.mainTexture;
				int i = 0;
				int count = this.sprites.Count;
				while (i < count)
				{
					UIAtlas.Sprite sprite = this.sprites[i];
					if (this.mCoordinates == UIAtlas.Coordinates.TexCoords)
					{
						sprite.outer = NGUIMath.ConvertToTexCoords(sprite.outer, mainTexture.width, mainTexture.height);
						sprite.inner = NGUIMath.ConvertToTexCoords(sprite.inner, mainTexture.width, mainTexture.height);
					}
					else
					{
						sprite.outer = NGUIMath.ConvertToPixels(sprite.outer, mainTexture.width, mainTexture.height, true);
						sprite.inner = NGUIMath.ConvertToPixels(sprite.inner, mainTexture.width, mainTexture.height, true);
					}
					i++;
				}
			}
		}
	}

	// Token: 0x17000081 RID: 129
	// (get) Token: 0x060003AF RID: 943 RVA: 0x00017BE8 File Offset: 0x00015DE8
	// (set) Token: 0x060003B0 RID: 944 RVA: 0x00017C14 File Offset: 0x00015E14
	public float pixelSize
	{
		get
		{
			return (!(this.mReplacement != null)) ? this.mPixelSize : this.mReplacement.pixelSize;
		}
		set
		{
			if (this.mReplacement != null)
			{
				this.mReplacement.pixelSize = value;
			}
			else
			{
				float num = Mathf.Clamp(value, 0.25f, 4f);
				if (this.mPixelSize != num)
				{
					this.mPixelSize = num;
					this.MarkAsDirty();
				}
			}
		}
	}

	// Token: 0x17000082 RID: 130
	// (get) Token: 0x060003B1 RID: 945 RVA: 0x00017C70 File Offset: 0x00015E70
	// (set) Token: 0x060003B2 RID: 946 RVA: 0x00017C78 File Offset: 0x00015E78
	public UIAtlas replacement
	{
		get
		{
			return this.mReplacement;
		}
		set
		{
			UIAtlas uiatlas = value;
			if (uiatlas == this)
			{
				uiatlas = null;
			}
			if (this.mReplacement != uiatlas)
			{
				if (uiatlas != null && uiatlas.replacement == this)
				{
					uiatlas.replacement = null;
				}
				if (this.mReplacement != null)
				{
					this.MarkAsDirty();
				}
				this.mReplacement = uiatlas;
				this.MarkAsDirty();
			}
		}
	}

	// Token: 0x060003B3 RID: 947 RVA: 0x00017CF0 File Offset: 0x00015EF0
	public UIAtlas.Sprite GetSprite(string name)
	{
		if (this.mReplacement != null)
		{
			return this.mReplacement.GetSprite(name);
		}
		if (!string.IsNullOrEmpty(name))
		{
			int i = 0;
			int count = this.sprites.Count;
			while (i < count)
			{
				UIAtlas.Sprite sprite = this.sprites[i];
				if (!string.IsNullOrEmpty(sprite.name) && name == sprite.name)
				{
					return sprite;
				}
				i++;
			}
		}
		return null;
	}

	// Token: 0x060003B4 RID: 948 RVA: 0x00017D78 File Offset: 0x00015F78
	private static int CompareString(string a, string b)
	{
		return a.CompareTo(b);
	}

	// Token: 0x060003B5 RID: 949 RVA: 0x00017D84 File Offset: 0x00015F84
	public BetterList<string> GetListOfSprites()
	{
		if (this.mReplacement != null)
		{
			return this.mReplacement.GetListOfSprites();
		}
		BetterList<string> betterList = new BetterList<string>();
		int i = 0;
		int count = this.sprites.Count;
		while (i < count)
		{
			UIAtlas.Sprite sprite = this.sprites[i];
			if (sprite != null && !string.IsNullOrEmpty(sprite.name))
			{
				betterList.Add(sprite.name);
			}
			i++;
		}
		return betterList;
	}

	// Token: 0x060003B6 RID: 950 RVA: 0x00017E04 File Offset: 0x00016004
	public BetterList<string> GetListOfSprites(string match)
	{
		if (this.mReplacement != null)
		{
			return this.mReplacement.GetListOfSprites(match);
		}
		if (string.IsNullOrEmpty(match))
		{
			return this.GetListOfSprites();
		}
		BetterList<string> betterList = new BetterList<string>();
		int i = 0;
		int count = this.sprites.Count;
		while (i < count)
		{
			UIAtlas.Sprite sprite = this.sprites[i];
			if (sprite != null && !string.IsNullOrEmpty(sprite.name) && string.Equals(match, sprite.name, StringComparison.OrdinalIgnoreCase))
			{
				betterList.Add(sprite.name);
				return betterList;
			}
			i++;
		}
		string[] array = match.Split(new char[]
		{
			' '
		}, StringSplitOptions.RemoveEmptyEntries);
		for (int j = 0; j < array.Length; j++)
		{
			array[j] = array[j].ToLower();
		}
		int k = 0;
		int count2 = this.sprites.Count;
		while (k < count2)
		{
			UIAtlas.Sprite sprite2 = this.sprites[k];
			if (sprite2 != null && !string.IsNullOrEmpty(sprite2.name))
			{
				string text = sprite2.name.ToLower();
				int num = 0;
				for (int l = 0; l < array.Length; l++)
				{
					if (text.Contains(array[l]))
					{
						num++;
					}
				}
				if (num == array.Length)
				{
					betterList.Add(sprite2.name);
				}
			}
			k++;
		}
		return betterList;
	}

	// Token: 0x060003B7 RID: 951 RVA: 0x00017F8C File Offset: 0x0001618C
	private bool References(UIAtlas atlas)
	{
		return !(atlas == null) && (atlas == this || (this.mReplacement != null && this.mReplacement.References(atlas)));
	}

	// Token: 0x060003B8 RID: 952 RVA: 0x00017FD8 File Offset: 0x000161D8
	public static bool CheckIfRelated(UIAtlas a, UIAtlas b)
	{
		return !(a == null) && !(b == null) && (a == b || a.References(b) || b.References(a));
	}

	// Token: 0x060003B9 RID: 953 RVA: 0x00018024 File Offset: 0x00016224
	public void MarkAsDirty()
	{
		if (this.mReplacement != null)
		{
			this.mReplacement.MarkAsDirty();
		}
		UISprite[] array = NGUITools.FindActive<UISprite>();
		int i = 0;
		int num = array.Length;
		while (i < num)
		{
			UISprite uisprite = array[i];
			if (UIAtlas.CheckIfRelated(this, uisprite.atlas))
			{
				UIAtlas atlas = uisprite.atlas;
				uisprite.atlas = null;
				uisprite.atlas = atlas;
			}
			i++;
		}
		UIFont[] array2 = Resources.FindObjectsOfTypeAll(typeof(UIFont)) as UIFont[];
		int j = 0;
		int num2 = array2.Length;
		while (j < num2)
		{
			UIFont uifont = array2[j];
			if (UIAtlas.CheckIfRelated(this, uifont.atlas))
			{
				UIAtlas atlas2 = uifont.atlas;
				uifont.atlas = null;
				uifont.atlas = atlas2;
			}
			j++;
		}
		UILabel[] array3 = NGUITools.FindActive<UILabel>();
		int k = 0;
		int num3 = array3.Length;
		while (k < num3)
		{
			UILabel uilabel = array3[k];
			if (uilabel.font != null && UIAtlas.CheckIfRelated(this, uilabel.font.atlas))
			{
				UIFont font = uilabel.font;
				uilabel.font = null;
				uilabel.font = font;
			}
			k++;
		}
	}

	// Token: 0x040003C8 RID: 968
	[SerializeField]
	[HideInInspector]
	private Material material;

	// Token: 0x040003C9 RID: 969
	[HideInInspector]
	[SerializeField]
	private List<UIAtlas.Sprite> sprites = new List<UIAtlas.Sprite>();

	// Token: 0x040003CA RID: 970
	[HideInInspector]
	[SerializeField]
	private UIAtlas.Coordinates mCoordinates;

	// Token: 0x040003CB RID: 971
	[HideInInspector]
	[SerializeField]
	private float mPixelSize = 1f;

	// Token: 0x040003CC RID: 972
	[HideInInspector]
	[SerializeField]
	private UIAtlas mReplacement;

	// Token: 0x040003CD RID: 973
	private int mPMA = -1;

	// Token: 0x02000076 RID: 118
	[Serializable]
	public class Sprite
	{
		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060003BB RID: 955 RVA: 0x000181C8 File Offset: 0x000163C8
		public bool hasPadding
		{
			get
			{
				return this.paddingLeft != 0f || this.paddingRight != 0f || this.paddingTop != 0f || this.paddingBottom != 0f;
			}
		}

		// Token: 0x040003CE RID: 974
		public string name = "Unity Bug";

		// Token: 0x040003CF RID: 975
		public Rect outer = new Rect(0f, 0f, 1f, 1f);

		// Token: 0x040003D0 RID: 976
		public Rect inner = new Rect(0f, 0f, 1f, 1f);

		// Token: 0x040003D1 RID: 977
		public bool rotated;

		// Token: 0x040003D2 RID: 978
		public float paddingLeft;

		// Token: 0x040003D3 RID: 979
		public float paddingRight;

		// Token: 0x040003D4 RID: 980
		public float paddingTop;

		// Token: 0x040003D5 RID: 981
		public float paddingBottom;
	}

	// Token: 0x02000077 RID: 119
	public enum Coordinates
	{
		// Token: 0x040003D7 RID: 983
		Pixels,
		// Token: 0x040003D8 RID: 984
		TexCoords
	}
}
