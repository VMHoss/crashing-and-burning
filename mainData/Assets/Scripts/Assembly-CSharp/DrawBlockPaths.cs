using System;
using UnityEngine;

// Token: 0x02000149 RID: 329
public class DrawBlockPaths : MonoBehaviour
{
	// Token: 0x06000996 RID: 2454 RVA: 0x0004B3E0 File Offset: 0x000495E0
	private void OnDrawGizmosSelected()
	{
		this.blockData.DrawGizmos();
	}

	// Token: 0x04000A17 RID: 2583
	public BlockData blockData;
}
