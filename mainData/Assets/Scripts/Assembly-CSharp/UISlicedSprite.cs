using System;
using UnityEngine;

// Token: 0x0200008B RID: 139
[ExecuteInEditMode]
public class UISlicedSprite : UISprite
{
	// Token: 0x170000C3 RID: 195
	// (get) Token: 0x06000497 RID: 1175 RVA: 0x0001FD00 File Offset: 0x0001DF00
	public override UISprite.Type type
	{
		get
		{
			return UISprite.Type.Sliced;
		}
	}
}
