using System;
using UnityEngine;

// Token: 0x02000020 RID: 32
[AddComponentMenu("NGUI/Interaction/Button Activate")]
public class UIButtonActivate : MonoBehaviour
{
	// Token: 0x06000153 RID: 339 RVA: 0x0000929C File Offset: 0x0000749C
	private void OnClick()
	{
		if (this.target != null)
		{
			NGUITools.SetActive(this.target, this.state);
		}
	}

	// Token: 0x04000186 RID: 390
	public GameObject target;

	// Token: 0x04000187 RID: 391
	public bool state = true;
}
