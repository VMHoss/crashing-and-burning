using System;
using UnityEngine;

// Token: 0x02000065 RID: 101
[ExecuteInEditMode]
[RequireComponent(typeof(UIWidget))]
public class AnimatedColor : MonoBehaviour
{
	// Token: 0x0600035A RID: 858 RVA: 0x00015D08 File Offset: 0x00013F08
	private void Awake()
	{
		this.mLabel = base.GetComponent<UILabel>();
	}

	// Token: 0x0600035B RID: 859 RVA: 0x00015D18 File Offset: 0x00013F18
	private void Update()
	{
		this.mLabel.color = this.color;
	}

	// Token: 0x04000367 RID: 871
	public Color color = Color.white;

	// Token: 0x04000368 RID: 872
	private UILabel mLabel;
}
