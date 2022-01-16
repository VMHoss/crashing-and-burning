using System;
using UnityEngine;

// Token: 0x020000B6 RID: 182
public class Retransform : MonoBehaviour
{
	// Token: 0x0600058B RID: 1419 RVA: 0x00027484 File Offset: 0x00025684
	private void Start()
	{
		this.checkTransform();
	}

	// Token: 0x0600058C RID: 1420 RVA: 0x0002748C File Offset: 0x0002568C
	private void Update()
	{
		this.checkTransform();
	}

	// Token: 0x0600058D RID: 1421 RVA: 0x00027494 File Offset: 0x00025694
	public void checkTransform()
	{
		if (this.branding != string.Empty && this.branding.Contains(Data.branding))
		{
			base.gameObject.transform.localPosition = this.newPosition;
			base.gameObject.transform.localScale = this.newScale;
			base.enabled = false;
		}
	}

	// Token: 0x040005DB RID: 1499
	public string branding;

	// Token: 0x040005DC RID: 1500
	public Vector3 newPosition;

	// Token: 0x040005DD RID: 1501
	public Vector3 newScale;
}
