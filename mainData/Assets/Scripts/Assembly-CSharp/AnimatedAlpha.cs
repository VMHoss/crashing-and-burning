using System;
using UnityEngine;

// Token: 0x02000064 RID: 100
public class AnimatedAlpha : MonoBehaviour
{
	// Token: 0x06000357 RID: 855 RVA: 0x00015C80 File Offset: 0x00013E80
	private void Awake()
	{
		this.mWidget = base.GetComponent<UIWidget>();
		this.mPanel = base.GetComponent<UIPanel>();
		this.Update();
	}

	// Token: 0x06000358 RID: 856 RVA: 0x00015CA0 File Offset: 0x00013EA0
	private void Update()
	{
		if (this.mWidget != null)
		{
			this.mWidget.alpha = this.alpha;
		}
		if (this.mPanel != null)
		{
			this.mPanel.alpha = this.alpha;
		}
	}

	// Token: 0x04000364 RID: 868
	public float alpha = 1f;

	// Token: 0x04000365 RID: 869
	private UIWidget mWidget;

	// Token: 0x04000366 RID: 870
	private UIPanel mPanel;
}
