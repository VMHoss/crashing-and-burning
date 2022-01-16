using System;
using UnityEngine;

// Token: 0x020000B3 RID: 179
public class ResizeText : MonoBehaviour
{
	// Token: 0x0600057F RID: 1407 RVA: 0x00027138 File Offset: 0x00025338
	private void Start()
	{
		this.pLabel = base.gameObject.GetComponent<UILabel>();
		this.pTransform = base.gameObject.transform;
	}

	// Token: 0x06000580 RID: 1408 RVA: 0x00027168 File Offset: 0x00025368
	private void Update()
	{
		this.pText = this.pLabel.text;
		if (this.pText.Length > this.maxCharLength)
		{
			this.pTransform.localScale = new Vector3(this.resizedX, this.pTransform.localScale.y, this.pTransform.localScale.z);
		}
		else
		{
			this.pTransform.localScale = new Vector3(this.pTransform.localScale.y, this.pTransform.localScale.y, this.pTransform.localScale.z);
		}
	}

	// Token: 0x040005C7 RID: 1479
	public int maxCharLength;

	// Token: 0x040005C8 RID: 1480
	public float resizedX;

	// Token: 0x040005C9 RID: 1481
	private UILabel pLabel;

	// Token: 0x040005CA RID: 1482
	private Transform pTransform;

	// Token: 0x040005CB RID: 1483
	private string pText;
}
