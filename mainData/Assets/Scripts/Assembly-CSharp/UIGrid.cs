using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200003A RID: 58
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/Grid")]
public class UIGrid : MonoBehaviour
{
	// Token: 0x060001E8 RID: 488 RVA: 0x0000D294 File Offset: 0x0000B494
	private void Start()
	{
		this.mStarted = true;
		this.Reposition();
	}

	// Token: 0x060001E9 RID: 489 RVA: 0x0000D2A4 File Offset: 0x0000B4A4
	private void Update()
	{
		if (this.repositionNow)
		{
			this.repositionNow = false;
			this.Reposition();
		}
	}

	// Token: 0x060001EA RID: 490 RVA: 0x0000D2C0 File Offset: 0x0000B4C0
	public static int SortByName(Transform a, Transform b)
	{
		return string.Compare(a.name, b.name);
	}

	// Token: 0x060001EB RID: 491 RVA: 0x0000D2D4 File Offset: 0x0000B4D4
	public void Reposition()
	{
		if (!this.mStarted)
		{
			this.repositionNow = true;
			return;
		}
		Transform transform = base.transform;
		int num = 0;
		int num2 = 0;
		if (this.sorted)
		{
			List<Transform> list = new List<Transform>();
			for (int i = 0; i < transform.childCount; i++)
			{
				Transform child = transform.GetChild(i);
				if (child && (!this.hideInactive || NGUITools.GetActive(child.gameObject)))
				{
					list.Add(child);
				}
			}
			list.Sort(new Comparison<Transform>(UIGrid.SortByName));
			int j = 0;
			int count = list.Count;
			while (j < count)
			{
				Transform transform2 = list[j];
				if (NGUITools.GetActive(transform2.gameObject) || !this.hideInactive)
				{
					float z = transform2.localPosition.z;
					transform2.localPosition = ((this.arrangement != UIGrid.Arrangement.Horizontal) ? new Vector3(this.cellWidth * (float)num2, -this.cellHeight * (float)num, z) : new Vector3(this.cellWidth * (float)num, -this.cellHeight * (float)num2, z));
					if (++num >= this.maxPerLine && this.maxPerLine > 0)
					{
						num = 0;
						num2++;
					}
				}
				j++;
			}
		}
		else
		{
			for (int k = 0; k < transform.childCount; k++)
			{
				Transform child2 = transform.GetChild(k);
				if (NGUITools.GetActive(child2.gameObject) || !this.hideInactive)
				{
					float z2 = child2.localPosition.z;
					child2.localPosition = ((this.arrangement != UIGrid.Arrangement.Horizontal) ? new Vector3(this.cellWidth * (float)num2, -this.cellHeight * (float)num, z2) : new Vector3(this.cellWidth * (float)num, -this.cellHeight * (float)num2, z2));
					if (++num >= this.maxPerLine && this.maxPerLine > 0)
					{
						num = 0;
						num2++;
					}
				}
			}
		}
		UIDraggablePanel uidraggablePanel = NGUITools.FindInParents<UIDraggablePanel>(base.gameObject);
		if (uidraggablePanel != null)
		{
			uidraggablePanel.UpdateScrollbars(true);
		}
	}

	// Token: 0x04000246 RID: 582
	public UIGrid.Arrangement arrangement;

	// Token: 0x04000247 RID: 583
	public int maxPerLine;

	// Token: 0x04000248 RID: 584
	public float cellWidth = 200f;

	// Token: 0x04000249 RID: 585
	public float cellHeight = 200f;

	// Token: 0x0400024A RID: 586
	public bool repositionNow;

	// Token: 0x0400024B RID: 587
	public bool sorted;

	// Token: 0x0400024C RID: 588
	public bool hideInactive = true;

	// Token: 0x0400024D RID: 589
	private bool mStarted;

	// Token: 0x0200003B RID: 59
	public enum Arrangement
	{
		// Token: 0x0400024F RID: 591
		Horizontal,
		// Token: 0x04000250 RID: 592
		Vertical
	}
}
