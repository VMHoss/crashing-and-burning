using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200009B RID: 155
public class AutoHide : MonoBehaviour
{
	// Token: 0x060004F8 RID: 1272 RVA: 0x00023DA8 File Offset: 0x00021FA8
	private void Start()
	{
		if (this.time == 0f)
		{
			this.time = 2f;
		}
		base.StartCoroutine(this.Wait());
	}

	// Token: 0x060004F9 RID: 1273 RVA: 0x00023DE0 File Offset: 0x00021FE0
	private IEnumerator Wait()
	{
		yield return new WaitForSeconds(this.time);
		base.gameObject.SetActive(false);
		yield break;
	}

	// Token: 0x0400050A RID: 1290
	public float time;
}
