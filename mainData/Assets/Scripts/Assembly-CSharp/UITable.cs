using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000048 RID: 72
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/Table")]
public class UITable : MonoBehaviour
{
	// Token: 0x06000243 RID: 579 RVA: 0x0000FDA8 File Offset: 0x0000DFA8
	public static int SortByName(Transform a, Transform b)
	{
		return string.Compare(a.name, b.name);
	}

	// Token: 0x17000036 RID: 54
	// (get) Token: 0x06000244 RID: 580 RVA: 0x0000FDBC File Offset: 0x0000DFBC
	public List<Transform> children
	{
		get
		{
			if (this.mChildren.Count == 0)
			{
				Transform transform = base.transform;
				this.mChildren.Clear();
				for (int i = 0; i < transform.childCount; i++)
				{
					Transform child = transform.GetChild(i);
					if (child && child.gameObject && (!this.hideInactive || NGUITools.GetActive(child.gameObject)))
					{
						this.mChildren.Add(child);
					}
				}
				if (this.sorted)
				{
					this.mChildren.Sort(new Comparison<Transform>(UITable.SortByName));
				}
			}
			return this.mChildren;
		}
	}

	// Token: 0x06000245 RID: 581 RVA: 0x0000FE74 File Offset: 0x0000E074
	private void RepositionVariableSize(List<Transform> children)
	{
		float num = 0f;
		float num2 = 0f;
		int num3 = (this.columns <= 0) ? 1 : (children.Count / this.columns + 1);
		int num4 = (this.columns <= 0) ? children.Count : this.columns;
		Bounds[,] array = new Bounds[num3, num4];
		Bounds[] array2 = new Bounds[num4];
		Bounds[] array3 = new Bounds[num3];
		int num5 = 0;
		int num6 = 0;
		int i = 0;
		int count = children.Count;
		while (i < count)
		{
			Transform transform = children[i];
			Bounds bounds = NGUIMath.CalculateRelativeWidgetBounds(transform);
			Vector3 localScale = transform.localScale;
			bounds.min = Vector3.Scale(bounds.min, localScale);
			bounds.max = Vector3.Scale(bounds.max, localScale);
			array[num6, num5] = bounds;
			array2[num5].Encapsulate(bounds);
			array3[num6].Encapsulate(bounds);
			if (++num5 >= this.columns && this.columns > 0)
			{
				num5 = 0;
				num6++;
			}
			i++;
		}
		num5 = 0;
		num6 = 0;
		int j = 0;
		int count2 = children.Count;
		while (j < count2)
		{
			Transform transform2 = children[j];
			Bounds bounds2 = array[num6, num5];
			Bounds bounds3 = array2[num5];
			Bounds bounds4 = array3[num6];
			Vector3 localPosition = transform2.localPosition;
			localPosition.x = num + bounds2.extents.x - bounds2.center.x;
			localPosition.x += bounds2.min.x - bounds3.min.x + this.padding.x;
			if (this.direction == UITable.Direction.Down)
			{
				localPosition.y = -num2 - bounds2.extents.y - bounds2.center.y;
				localPosition.y += (bounds2.max.y - bounds2.min.y - bounds4.max.y + bounds4.min.y) * 0.5f - this.padding.y;
			}
			else
			{
				localPosition.y = num2 + bounds2.extents.y - bounds2.center.y;
				localPosition.y += (bounds2.max.y - bounds2.min.y - bounds4.max.y + bounds4.min.y) * 0.5f - this.padding.y;
			}
			num += bounds3.max.x - bounds3.min.x + this.padding.x * 2f;
			transform2.localPosition = localPosition;
			if (++num5 >= this.columns && this.columns > 0)
			{
				num5 = 0;
				num6++;
				num = 0f;
				num2 += bounds4.size.y + this.padding.y * 2f;
			}
			j++;
		}
	}

	// Token: 0x06000246 RID: 582 RVA: 0x0001022C File Offset: 0x0000E42C
	public void Reposition()
	{
		if (this.mStarted)
		{
			Transform transform = base.transform;
			this.mChildren.Clear();
			List<Transform> children = this.children;
			if (children.Count > 0)
			{
				this.RepositionVariableSize(children);
			}
			if (this.mDrag != null)
			{
				this.mDrag.UpdateScrollbars(true);
				this.mDrag.RestrictWithinBounds(true);
			}
			else if (this.mPanel != null)
			{
				this.mPanel.ConstrainTargetToBounds(transform, true);
			}
			if (this.onReposition != null)
			{
				this.onReposition();
			}
		}
		else
		{
			this.repositionNow = true;
		}
	}

	// Token: 0x06000247 RID: 583 RVA: 0x000102E0 File Offset: 0x0000E4E0
	private void Start()
	{
		this.mStarted = true;
		if (this.keepWithinPanel)
		{
			this.mPanel = NGUITools.FindInParents<UIPanel>(base.gameObject);
			this.mDrag = NGUITools.FindInParents<UIDraggablePanel>(base.gameObject);
		}
		this.Reposition();
	}

	// Token: 0x06000248 RID: 584 RVA: 0x00010328 File Offset: 0x0000E528
	private void LateUpdate()
	{
		if (this.repositionNow)
		{
			this.repositionNow = false;
			this.Reposition();
		}
	}

	// Token: 0x040002A7 RID: 679
	public int columns;

	// Token: 0x040002A8 RID: 680
	public UITable.Direction direction;

	// Token: 0x040002A9 RID: 681
	public Vector2 padding = Vector2.zero;

	// Token: 0x040002AA RID: 682
	public bool sorted;

	// Token: 0x040002AB RID: 683
	public bool hideInactive = true;

	// Token: 0x040002AC RID: 684
	public bool repositionNow;

	// Token: 0x040002AD RID: 685
	public bool keepWithinPanel;

	// Token: 0x040002AE RID: 686
	public UITable.OnReposition onReposition;

	// Token: 0x040002AF RID: 687
	private UIPanel mPanel;

	// Token: 0x040002B0 RID: 688
	private UIDraggablePanel mDrag;

	// Token: 0x040002B1 RID: 689
	private bool mStarted;

	// Token: 0x040002B2 RID: 690
	private List<Transform> mChildren = new List<Transform>();

	// Token: 0x02000049 RID: 73
	public enum Direction
	{
		// Token: 0x040002B4 RID: 692
		Down,
		// Token: 0x040002B5 RID: 693
		Up
	}

	// Token: 0x020001E5 RID: 485
	// (Invoke) Token: 0x06000DB1 RID: 3505
	public delegate void OnReposition();
}
