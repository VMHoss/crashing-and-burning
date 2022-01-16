using System;
using UnityEngine;

// Token: 0x02000096 RID: 150
[ExecuteInEditMode]
public class UITiledSprite : UISlicedSprite
{
	// Token: 0x170000DE RID: 222
	// (get) Token: 0x060004E5 RID: 1253 RVA: 0x00023530 File Offset: 0x00021730
	public override UISprite.Type type
	{
		get
		{
			return UISprite.Type.Tiled;
		}
	}
}
