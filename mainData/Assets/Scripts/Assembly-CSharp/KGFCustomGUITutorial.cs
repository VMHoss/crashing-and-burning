using System;
using UnityEngine;

// Token: 0x02000154 RID: 340
public class KGFCustomGUITutorial : KGFObject, KGFICustomGUI
{
	// Token: 0x060009CE RID: 2510 RVA: 0x0004C26C File Offset: 0x0004A46C
	public string GetName()
	{
		return "KGFCustomGUITutorial";
	}

	// Token: 0x060009CF RID: 2511 RVA: 0x0004C274 File Offset: 0x0004A474
	public string GetHeaderName()
	{
		return "Custom GUI Tutorial";
	}

	// Token: 0x060009D0 RID: 2512 RVA: 0x0004C27C File Offset: 0x0004A47C
	public Texture2D GetIcon()
	{
		return null;
	}

	// Token: 0x060009D1 RID: 2513 RVA: 0x0004C280 File Offset: 0x0004A480
	public void Render()
	{
	}
}
