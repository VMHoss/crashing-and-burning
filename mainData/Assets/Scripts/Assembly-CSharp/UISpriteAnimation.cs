using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200008F RID: 143
[ExecuteInEditMode]
[RequireComponent(typeof(UISprite))]
[AddComponentMenu("NGUI/UI/Sprite Animation")]
public class UISpriteAnimation : MonoBehaviour
{
	// Token: 0x170000D2 RID: 210
	// (get) Token: 0x060004BC RID: 1212 RVA: 0x00022334 File Offset: 0x00020534
	public int frames
	{
		get
		{
			return this.mSpriteNames.Count;
		}
	}

	// Token: 0x170000D3 RID: 211
	// (get) Token: 0x060004BD RID: 1213 RVA: 0x00022344 File Offset: 0x00020544
	// (set) Token: 0x060004BE RID: 1214 RVA: 0x0002234C File Offset: 0x0002054C
	public int framesPerSecond
	{
		get
		{
			return this.mFPS;
		}
		set
		{
			this.mFPS = value;
		}
	}

	// Token: 0x170000D4 RID: 212
	// (get) Token: 0x060004BF RID: 1215 RVA: 0x00022358 File Offset: 0x00020558
	// (set) Token: 0x060004C0 RID: 1216 RVA: 0x00022360 File Offset: 0x00020560
	public string namePrefix
	{
		get
		{
			return this.mPrefix;
		}
		set
		{
			if (this.mPrefix != value)
			{
				this.mPrefix = value;
				this.RebuildSpriteList();
			}
		}
	}

	// Token: 0x170000D5 RID: 213
	// (get) Token: 0x060004C1 RID: 1217 RVA: 0x00022380 File Offset: 0x00020580
	// (set) Token: 0x060004C2 RID: 1218 RVA: 0x00022388 File Offset: 0x00020588
	public bool loop
	{
		get
		{
			return this.mLoop;
		}
		set
		{
			this.mLoop = value;
		}
	}

	// Token: 0x170000D6 RID: 214
	// (get) Token: 0x060004C3 RID: 1219 RVA: 0x00022394 File Offset: 0x00020594
	public bool isPlaying
	{
		get
		{
			return this.mActive;
		}
	}

	// Token: 0x060004C4 RID: 1220 RVA: 0x0002239C File Offset: 0x0002059C
	private void Start()
	{
		this.RebuildSpriteList();
	}

	// Token: 0x060004C5 RID: 1221 RVA: 0x000223A4 File Offset: 0x000205A4
	private void Update()
	{
		if (this.mActive && this.mSpriteNames.Count > 1 && Application.isPlaying && (float)this.mFPS > 0f)
		{
			this.mDelta += Time.deltaTime;
			float num = 1f / (float)this.mFPS;
			if (num < this.mDelta)
			{
				this.mDelta = ((num <= 0f) ? 0f : (this.mDelta - num));
				if (++this.mIndex >= this.mSpriteNames.Count)
				{
					this.mIndex = 0;
					this.mActive = this.loop;
				}
				if (this.mActive)
				{
					this.mSprite.spriteName = this.mSpriteNames[this.mIndex];
					this.mSprite.MakePixelPerfect();
				}
			}
		}
	}

	// Token: 0x060004C6 RID: 1222 RVA: 0x000224A0 File Offset: 0x000206A0
	private void RebuildSpriteList()
	{
		if (this.mSprite == null)
		{
			this.mSprite = base.GetComponent<UISprite>();
		}
		this.mSpriteNames.Clear();
		if (this.mSprite != null && this.mSprite.atlas != null)
		{
			List<UIAtlas.Sprite> spriteList = this.mSprite.atlas.spriteList;
			int i = 0;
			int count = spriteList.Count;
			while (i < count)
			{
				UIAtlas.Sprite sprite = spriteList[i];
				if (string.IsNullOrEmpty(this.mPrefix) || sprite.name.StartsWith(this.mPrefix))
				{
					this.mSpriteNames.Add(sprite.name);
				}
				i++;
			}
			this.mSpriteNames.Sort();
		}
	}

	// Token: 0x060004C7 RID: 1223 RVA: 0x00022570 File Offset: 0x00020770
	public void Reset()
	{
		this.mActive = true;
		this.mIndex = 0;
		if (this.mSprite != null && this.mSpriteNames.Count > 0)
		{
			this.mSprite.spriteName = this.mSpriteNames[this.mIndex];
			this.mSprite.MakePixelPerfect();
		}
	}

	// Token: 0x040004C6 RID: 1222
	[SerializeField]
	[HideInInspector]
	private int mFPS = 30;

	// Token: 0x040004C7 RID: 1223
	[SerializeField]
	[HideInInspector]
	private string mPrefix = string.Empty;

	// Token: 0x040004C8 RID: 1224
	[SerializeField]
	[HideInInspector]
	private bool mLoop = true;

	// Token: 0x040004C9 RID: 1225
	private UISprite mSprite;

	// Token: 0x040004CA RID: 1226
	private float mDelta;

	// Token: 0x040004CB RID: 1227
	private int mIndex;

	// Token: 0x040004CC RID: 1228
	private bool mActive = true;

	// Token: 0x040004CD RID: 1229
	private List<string> mSpriteNames = new List<string>();
}
