using System;
using UnityEngine;

// Token: 0x02000194 RID: 404
public class KGFMapSystemStyleDemo : KGFModule
{
	// Token: 0x06000BD4 RID: 3028 RVA: 0x000569C8 File Offset: 0x00054BC8
	public KGFMapSystemStyleDemo() : base(new Version(1, 0, 0, 0), new Version(1, 1, 0, 0))
	{
	}

	// Token: 0x06000BD5 RID: 3029 RVA: 0x000569FC File Offset: 0x00054BFC
	protected override void KGFAwake()
	{
		base.KGFAwake();
		this.UpdateStyle(3);
	}

	// Token: 0x06000BD6 RID: 3030 RVA: 0x00056A0C File Offset: 0x00054C0C
	private void OnGUI()
	{
		int num = 50;
		int num2 = 10;
		for (int i = 0; i < this.itsStyles.Length; i++)
		{
			if (GUI.Button(new Rect(10f, (float)(10 + i * (num + num2)), 100f, (float)num), this.itsStyles[i].itsName))
			{
				this.UpdateStyle(i);
			}
		}
		GUI.Label(new Rect((float)Screen.width - 200f, (float)Screen.height - 50f, 200f, 50f), this.itsKOLMICHTexture);
	}

	// Token: 0x06000BD7 RID: 3031 RVA: 0x00056AA4 File Offset: 0x00054CA4
	private void UpdateStyle(int theIndex)
	{
		KGFMapSystemStyleDemo.MapSystemStyle mapSystemStyle = this.itsStyles[theIndex];
		KGFMapSystem @object = KGFAccessor.GetObject<KGFMapSystem>();
		if (@object != null)
		{
			@object.itsDataModuleMinimap.itsAppearanceMap.itsBackground = mapSystemStyle.itsBackgroundMap;
			@object.itsDataModuleMinimap.itsAppearanceMiniMap.itsBackground = mapSystemStyle.itsBackgroundMinimap;
			@object.itsDataModuleMinimap.itsAppearanceMap.itsButton = mapSystemStyle.itsButton;
			@object.itsDataModuleMinimap.itsAppearanceMap.itsButtonHover = mapSystemStyle.itsButtonHover;
			@object.itsDataModuleMinimap.itsAppearanceMap.itsButtonDown = mapSystemStyle.itsButtonDown;
			@object.itsDataModuleMinimap.itsAppearanceMiniMap.itsButton = mapSystemStyle.itsButton;
			@object.itsDataModuleMinimap.itsAppearanceMiniMap.itsButtonHover = mapSystemStyle.itsButtonHover;
			@object.itsDataModuleMinimap.itsAppearanceMiniMap.itsButtonDown = mapSystemStyle.itsButtonDown;
			@object.itsDataModuleMinimap.itsAppearanceMiniMap.itsIconZoomIn = mapSystemStyle.itsButtonZoomIn;
			@object.itsDataModuleMinimap.itsAppearanceMiniMap.itsIconZoomOut = mapSystemStyle.itsButtonZoomOut;
			@object.itsDataModuleMinimap.itsAppearanceMiniMap.itsIconFullscreen = mapSystemStyle.itsButtonMap;
			@object.itsDataModuleMinimap.itsAppearanceMiniMap.itsIconZoomLock = mapSystemStyle.itsButtonLock;
			@object.itsDataModuleMinimap.itsGlobalSettings.itsColorMap = mapSystemStyle.itsColorMap;
			@object.itsDataModuleMinimap.itsAppearanceMap.itsIconZoomIn = mapSystemStyle.itsButtonZoomIn;
			@object.itsDataModuleMinimap.itsAppearanceMap.itsIconZoomOut = mapSystemStyle.itsButtonZoomOut;
			@object.itsDataModuleMinimap.itsAppearanceMap.itsIconFullscreen = mapSystemStyle.itsButtonMap;
			@object.itsDataModuleMinimap.itsAppearanceMap.itsIconZoomLock = mapSystemStyle.itsButtonLock;
			@object.SetMask(mapSystemStyle.itsMinimapMask, mapSystemStyle.itsMapMask);
			@object.itsDataModuleMinimap.itsAppearanceMiniMap.itsButtonPadding = mapSystemStyle.itsPaddingButtons;
			@object.itsDataModuleMinimap.itsGlobalSettings.itsColorAll = mapSystemStyle.itsColorAll;
			@object.itsDataModuleMinimap.itsViewport.itsColor = mapSystemStyle.itsViewportColor;
			@object.itsDataModuleMinimap.itsToolTip.itsTextureBackground = mapSystemStyle.itsBackgroundTooltip;
			@object.UpdateStyles();
		}
	}

	// Token: 0x06000BD8 RID: 3032 RVA: 0x00056CB4 File Offset: 0x00054EB4
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			this.UpdateStyle(0);
		}
		else if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			this.UpdateStyle(1);
		}
		else if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			this.UpdateStyle(2);
		}
		else if (Input.GetKeyDown(KeyCode.Alpha4))
		{
			this.UpdateStyle(3);
		}
	}

	// Token: 0x06000BD9 RID: 3033 RVA: 0x00056D1C File Offset: 0x00054F1C
	public override KGFMessageList Validate()
	{
		return new KGFMessageList();
	}

	// Token: 0x06000BDA RID: 3034 RVA: 0x00056D24 File Offset: 0x00054F24
	public override string GetName()
	{
		return base.name;
	}

	// Token: 0x06000BDB RID: 3035 RVA: 0x00056D2C File Offset: 0x00054F2C
	public override Texture2D GetIcon()
	{
		return null;
	}

	// Token: 0x06000BDC RID: 3036 RVA: 0x00056D30 File Offset: 0x00054F30
	public override string GetForumPath()
	{
		return string.Empty;
	}

	// Token: 0x06000BDD RID: 3037 RVA: 0x00056D38 File Offset: 0x00054F38
	public override string GetDocumentationPath()
	{
		return string.Empty;
	}

	// Token: 0x04000BA7 RID: 2983
	public KGFMapSystemStyleDemo.MapSystemStyle[] itsStyles = new KGFMapSystemStyleDemo.MapSystemStyle[0];

	// Token: 0x04000BA8 RID: 2984
	public Texture2D itsKOLMICHTexture;

	// Token: 0x02000195 RID: 405
	[Serializable]
	public class MapSystemStyle
	{
		// Token: 0x04000BA9 RID: 2985
		public string itsName;

		// Token: 0x04000BAA RID: 2986
		public Texture2D itsBackgroundMinimap;

		// Token: 0x04000BAB RID: 2987
		public Texture2D itsBackgroundMap;

		// Token: 0x04000BAC RID: 2988
		public Texture2D itsBackgroundTooltip;

		// Token: 0x04000BAD RID: 2989
		public Texture2D itsButton;

		// Token: 0x04000BAE RID: 2990
		public Texture2D itsButtonHover;

		// Token: 0x04000BAF RID: 2991
		public Texture2D itsButtonDown;

		// Token: 0x04000BB0 RID: 2992
		public Texture2D itsButtonZoomIn;

		// Token: 0x04000BB1 RID: 2993
		public Texture2D itsButtonZoomOut;

		// Token: 0x04000BB2 RID: 2994
		public Texture2D itsButtonMap;

		// Token: 0x04000BB3 RID: 2995
		public Texture2D itsButtonLock;

		// Token: 0x04000BB4 RID: 2996
		public Color itsColorMap;

		// Token: 0x04000BB5 RID: 2997
		public Color itsColorAll;

		// Token: 0x04000BB6 RID: 2998
		public Texture2D itsMinimapMask;

		// Token: 0x04000BB7 RID: 2999
		public Texture2D itsMapMask;

		// Token: 0x04000BB8 RID: 3000
		public float itsPaddingButtons;

		// Token: 0x04000BB9 RID: 3001
		public Color itsViewportColor;
	}
}
