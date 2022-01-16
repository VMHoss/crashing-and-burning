using System;
using UnityEngine;

// Token: 0x020000C6 RID: 198
public class ScalerScript : MonoBehaviour
{
	// Token: 0x060005F9 RID: 1529 RVA: 0x0002AF44 File Offset: 0x00029144
	public void Initialize(float anEndScale, float aDuration)
	{
		this.pStartScale = base.transform.localScale;
		this.pEndScale = new Vector3(anEndScale, anEndScale, anEndScale);
		this.pInvDuration = 1f / aDuration;
	}

	// Token: 0x060005FA RID: 1530 RVA: 0x0002AF80 File Offset: 0x00029180
	private void Update()
	{
		this.pTime += Time.deltaTime;
		float num = this.pTime * this.pInvDuration;
		if (num > 1f)
		{
			UnityEngine.Object.Destroy(this);
		}
		base.transform.localScale = Vector3.Lerp(this.pStartScale, this.pEndScale, num);
	}

	// Token: 0x04000685 RID: 1669
	private Vector3 pStartScale = Vector3.one;

	// Token: 0x04000686 RID: 1670
	private Vector3 pEndScale = Vector3.zero;

	// Token: 0x04000687 RID: 1671
	private float pInvDuration = 1f;

	// Token: 0x04000688 RID: 1672
	private float pTime;
}
