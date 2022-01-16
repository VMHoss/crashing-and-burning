using System;
using UnityEngine;

// Token: 0x0200007C RID: 124
[ExecuteInEditMode]
public class UIFilledSprite : UISprite
{
	// Token: 0x1700008A RID: 138
	// (get) Token: 0x060003DF RID: 991 RVA: 0x00019C94 File Offset: 0x00017E94
	public override UISprite.Type type
	{
		get
		{
			return UISprite.Type.Filled;
		}
	}
}
