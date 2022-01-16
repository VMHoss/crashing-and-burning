using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000AA RID: 170
public class LogoPanel : MonoBehaviour
{
	// Token: 0x0600054E RID: 1358 RVA: 0x00025DFC File Offset: 0x00023FFC
	private void Start()
	{
	}

	// Token: 0x0600054F RID: 1359 RVA: 0x00025E00 File Offset: 0x00024000
	private void Update()
	{
	}

	// Token: 0x06000550 RID: 1360 RVA: 0x00025E04 File Offset: 0x00024004
	public void OnLogoPanel()
	{
		base.StartCoroutine(this.StarSequence());
	}

	// Token: 0x06000551 RID: 1361 RVA: 0x00025E14 File Offset: 0x00024014
	private IEnumerator StarSequence()
	{
		base.gameObject.GetComponent<TweenScale>().Reset();
		base.gameObject.GetComponent<TweenScale>().Play(true);
		yield return new WaitForSeconds(0.5f);
		for (int loops = 0; loops < 99; loops++)
		{
			for (int i = 0; i < this.positions.Length; i++)
			{
				this._pos = this.positions[i];
				this._scale = this.sizes[i];
				GenericFunctionsScript.StarParticle(this._pos, this._scale);
				yield return new WaitForSeconds(0.5f);
			}
		}
		yield break;
	}

	// Token: 0x04000572 RID: 1394
	public Vector3[] positions;

	// Token: 0x04000573 RID: 1395
	public float[] sizes;

	// Token: 0x04000574 RID: 1396
	private Vector3 _pos;

	// Token: 0x04000575 RID: 1397
	private float _scale;
}
