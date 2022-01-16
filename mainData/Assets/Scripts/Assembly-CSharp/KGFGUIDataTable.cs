using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200016B RID: 363
public class KGFGUIDataTable : KGFIControl
{
	// Token: 0x06000A8E RID: 2702 RVA: 0x000507CC File Offset: 0x0004E9CC
	public KGFGUIDataTable(KGFDataTable theDataTable, params GUILayoutOption[] theLayout)
	{
		this.itsDataTable = theDataTable;
		foreach (KGFDataColumn key in this.itsDataTable.Columns)
		{
			this.itsColumnWidth.Add(key, 0U);
			this.itsColumnVisible.Add(key, true);
		}
	}

	// Token: 0x1400002F RID: 47
	// (add) Token: 0x06000A90 RID: 2704 RVA: 0x00050890 File Offset: 0x0004EA90
	// (remove) Token: 0x06000A91 RID: 2705 RVA: 0x000508AC File Offset: 0x0004EAAC
	public event EventHandler PreRenderRow;

	// Token: 0x14000030 RID: 48
	// (add) Token: 0x06000A92 RID: 2706 RVA: 0x000508C8 File Offset: 0x0004EAC8
	// (remove) Token: 0x06000A93 RID: 2707 RVA: 0x000508E4 File Offset: 0x0004EAE4
	public event EventHandler PostRenderRow;

	// Token: 0x14000031 RID: 49
	// (add) Token: 0x06000A94 RID: 2708 RVA: 0x00050900 File Offset: 0x0004EB00
	// (remove) Token: 0x06000A95 RID: 2709 RVA: 0x0005091C File Offset: 0x0004EB1C
	public event EventHandler PreRenderColumn;

	// Token: 0x14000032 RID: 50
	// (add) Token: 0x06000A96 RID: 2710 RVA: 0x00050938 File Offset: 0x0004EB38
	// (remove) Token: 0x06000A97 RID: 2711 RVA: 0x00050954 File Offset: 0x0004EB54
	public event EventHandler PostRenderColumn;

	// Token: 0x14000033 RID: 51
	// (add) Token: 0x06000A98 RID: 2712 RVA: 0x00050970 File Offset: 0x0004EB70
	// (remove) Token: 0x06000A99 RID: 2713 RVA: 0x0005098C File Offset: 0x0004EB8C
	public event Func<KGFDataRow, KGFDataColumn, uint, bool> PreCellContentHandler;

	// Token: 0x14000034 RID: 52
	// (add) Token: 0x06000A9A RID: 2714 RVA: 0x000509A8 File Offset: 0x0004EBA8
	// (remove) Token: 0x06000A9B RID: 2715 RVA: 0x000509C4 File Offset: 0x0004EBC4
	public event EventHandler OnClickRow;

	// Token: 0x14000035 RID: 53
	// (add) Token: 0x06000A9C RID: 2716 RVA: 0x000509E0 File Offset: 0x0004EBE0
	// (remove) Token: 0x06000A9D RID: 2717 RVA: 0x000509FC File Offset: 0x0004EBFC
	public event EventHandler EventSettingsChanged;

	// Token: 0x06000A9E RID: 2718 RVA: 0x00050A18 File Offset: 0x0004EC18
	private static void LoadTextures()
	{
		string str = "KGFCore/textures/";
		KGFGUIDataTable.itsTextureArrowUp = (Texture2D)Resources.Load(str + "arrow_up", typeof(Texture2D));
		KGFGUIDataTable.itsTextureArrowDown = (Texture2D)Resources.Load(str + "arrow_down", typeof(Texture2D));
	}

	// Token: 0x06000A9F RID: 2719 RVA: 0x00050A74 File Offset: 0x0004EC74
	public uint GetStartRow()
	{
		return this.itsStartRow;
	}

	// Token: 0x06000AA0 RID: 2720 RVA: 0x00050A7C File Offset: 0x0004EC7C
	public void SetStartRow(uint theStartRow)
	{
		this.itsStartRow = (uint)Math.Min((long)((ulong)theStartRow), (long)this.itsDataTable.Rows.Count);
	}

	// Token: 0x06000AA1 RID: 2721 RVA: 0x00050AA0 File Offset: 0x0004ECA0
	public uint GetDisplayRowCount()
	{
		return this.itsDisplayRowCount;
	}

	// Token: 0x06000AA2 RID: 2722 RVA: 0x00050AA8 File Offset: 0x0004ECA8
	public void SetDisplayRowCount(uint theDisplayRowCount)
	{
		this.itsDisplayRowCount = (uint)Math.Min((long)((ulong)theDisplayRowCount), (long)this.itsDataTable.Rows.Count - (long)((ulong)this.itsStartRow));
	}

	// Token: 0x06000AA3 RID: 2723 RVA: 0x00050ADC File Offset: 0x0004ECDC
	public void SetColumnVisible(int theColumIndex, bool theValue)
	{
		if (theColumIndex >= 0 && theColumIndex < this.itsDataTable.Columns.Count)
		{
			this.itsColumnVisible[this.itsDataTable.Columns[theColumIndex]] = theValue;
		}
	}

	// Token: 0x06000AA4 RID: 2724 RVA: 0x00050B24 File Offset: 0x0004ED24
	public bool GetColumnVisible(int theColumIndex)
	{
		return theColumIndex >= 0 && theColumIndex < this.itsDataTable.Columns.Count && this.itsColumnVisible[this.itsDataTable.Columns[theColumIndex]];
	}

	// Token: 0x06000AA5 RID: 2725 RVA: 0x00050B6C File Offset: 0x0004ED6C
	public void SetColumnWidth(int theColumIndex, uint theValue)
	{
		if (theColumIndex >= 0 && theColumIndex < this.itsDataTable.Columns.Count)
		{
			this.itsColumnWidth[this.itsDataTable.Columns[theColumIndex]] = theValue;
		}
	}

	// Token: 0x06000AA6 RID: 2726 RVA: 0x00050BB4 File Offset: 0x0004EDB4
	public uint GetColumnWidth(int theColumIndex)
	{
		if (theColumIndex >= 0 && theColumIndex < this.itsDataTable.Columns.Count)
		{
			return this.itsColumnWidth[this.itsDataTable.Columns[theColumIndex]];
		}
		return 0U;
	}

	// Token: 0x06000AA7 RID: 2727 RVA: 0x00050BFC File Offset: 0x0004EDFC
	public KGFDataRow GetCurrentSelected()
	{
		return this.itsCurrentSelected;
	}

	// Token: 0x06000AA8 RID: 2728 RVA: 0x00050C04 File Offset: 0x0004EE04
	public void SetCurrentSelected(KGFDataRow theDataRow)
	{
		if (this.itsDataTable.Rows.Contains(theDataRow))
		{
			this.itsCurrentSelected = theDataRow;
		}
	}

	// Token: 0x06000AA9 RID: 2729 RVA: 0x00050C24 File Offset: 0x0004EE24
	private void RenderTableHeadings()
	{
		if (KGFGUIDataTable.itsTextureArrowDown == null)
		{
			KGFGUIDataTable.LoadTextures();
		}
		KGFGUIUtility.BeginHorizontalBox(KGFGUIUtility.eStyleBox.eBoxDarkTop, new GUILayoutOption[]
		{
			GUILayout.ExpandWidth(true),
			GUILayout.ExpandHeight(false)
		});
		foreach (KGFDataColumn kgfdataColumn in this.itsDataTable.Columns)
		{
			if (this.itsColumnVisible[kgfdataColumn])
			{
				GUILayoutOption[] options;
				if (this.itsColumnWidth[kgfdataColumn] != 0U)
				{
					options = new GUILayoutOption[]
					{
						GUILayout.Width(this.itsColumnWidth[kgfdataColumn])
					};
				}
				else
				{
					options = new GUILayoutOption[]
					{
						GUILayout.ExpandWidth(true)
					};
				}
				GUILayout.BeginHorizontal(options);
				KGFGUIUtility.Label(kgfdataColumn.ColumnName, KGFGUIUtility.eStyleLabel.eLabelFitIntoBox, new GUILayoutOption[0]);
				if (kgfdataColumn == this.itsSortColumn)
				{
					if (this.itsSortDirection)
					{
						KGFGUIUtility.Label(string.Empty, KGFGUIDataTable.itsTextureArrowDown, KGFGUIUtility.eStyleLabel.eLabelFitIntoBox, new GUILayoutOption[]
						{
							GUILayout.Width(14f)
						});
					}
					else
					{
						KGFGUIUtility.Label(string.Empty, KGFGUIDataTable.itsTextureArrowUp, KGFGUIUtility.eStyleLabel.eLabelFitIntoBox, new GUILayoutOption[]
						{
							GUILayout.Width(14f)
						});
					}
				}
				GUILayout.EndHorizontal();
				if (Event.current.type == EventType.MouseUp && GUILayoutUtility.GetLastRect().Contains(Event.current.mousePosition))
				{
					this.SortColumn(kgfdataColumn);
				}
				KGFGUIUtility.Separator(KGFGUIUtility.eStyleSeparator.eSeparatorVerticalFitInBox, new GUILayoutOption[0]);
			}
		}
		KGFGUIUtility.EndHorizontalBox();
	}

	// Token: 0x06000AAA RID: 2730 RVA: 0x00050DD4 File Offset: 0x0004EFD4
	private void SortColumn(KGFDataColumn theColumn)
	{
		if (this.itsSortColumn != theColumn)
		{
			this.SetSortingColumn(theColumn);
			this.itsSortDirection = false;
			this.itsDataTable.Rows.Sort(new Comparison<KGFDataRow>(this.RowComparison));
		}
		else
		{
			this.itsSortDirection = !this.itsSortDirection;
			this.itsDataTable.Rows.Reverse();
		}
		this.itsRepaint = true;
	}

	// Token: 0x06000AAB RID: 2731 RVA: 0x00050E44 File Offset: 0x0004F044
	private int RowComparison(KGFDataRow theRow1, KGFDataRow theRow2)
	{
		if (this.itsSortColumn != null)
		{
			return theRow1[this.itsSortColumn].Value.ToString().CompareTo(theRow2[this.itsSortColumn].Value.ToString());
		}
		return 0;
	}

	// Token: 0x06000AAC RID: 2732 RVA: 0x00050E90 File Offset: 0x0004F090
	private void RenderTableRows()
	{
		this.itsDataTableScrollViewPosition = KGFGUIUtility.BeginScrollView(this.itsDataTableScrollViewPosition, false, true, new GUILayoutOption[]
		{
			GUILayout.ExpandHeight(true)
		});
		if (this.itsDataTable.Rows.Count > 0)
		{
			GUILayout.BeginVertical(new GUILayoutOption[0]);
			Color color = GUI.color;
			int num = (int)this.itsStartRow;
			while ((long)num < (long)((ulong)(this.itsStartRow + this.itsDisplayRowCount)) && num < this.itsDataTable.Rows.Count)
			{
				KGFDataRow kgfdataRow = this.itsDataTable.Rows[num];
				if (this.PreRenderRow != null)
				{
					this.PreRenderRow(kgfdataRow, EventArgs.Empty);
				}
				if (kgfdataRow == this.itsCurrentSelected)
				{
					KGFGUIUtility.BeginHorizontalBox(KGFGUIUtility.eStyleBox.eBoxDarkTopInteractive, new GUILayoutOption[]
					{
						GUILayout.ExpandWidth(true)
					});
				}
				else
				{
					KGFGUIUtility.BeginHorizontalBox(KGFGUIUtility.eStyleBox.eBoxMiddleVerticalInteractive, new GUILayoutOption[]
					{
						GUILayout.ExpandWidth(true)
					});
				}
				foreach (KGFDataColumn kgfdataColumn in this.itsDataTable.Columns)
				{
					if (this.itsColumnVisible[kgfdataColumn])
					{
						if (this.PreRenderColumn != null)
						{
							this.PreRenderColumn(kgfdataColumn, EventArgs.Empty);
						}
						bool flag = false;
						if (this.PreCellContentHandler != null)
						{
							flag = this.PreCellContentHandler(kgfdataRow, kgfdataColumn, this.itsColumnWidth[kgfdataColumn]);
						}
						if (!flag)
						{
							int num2 = 85;
							string text = kgfdataRow[kgfdataColumn].ToString().Substring(0, Math.Min(num2, kgfdataRow[kgfdataColumn].ToString().Length));
							if (text.Length == num2)
							{
								text += "...";
							}
							if (this.itsColumnWidth[kgfdataColumn] > 0U)
							{
								KGFGUIUtility.Label(text, KGFGUIUtility.eStyleLabel.eLabelFitIntoBox, new GUILayoutOption[]
								{
									GUILayout.Width(this.itsColumnWidth[kgfdataColumn])
								});
							}
							else
							{
								KGFGUIUtility.Label(text, KGFGUIUtility.eStyleLabel.eLabelFitIntoBox, new GUILayoutOption[]
								{
									GUILayout.ExpandWidth(true)
								});
							}
						}
						KGFGUIUtility.Separator(KGFGUIUtility.eStyleSeparator.eSeparatorVerticalFitInBox, new GUILayoutOption[0]);
						if (this.PostRenderColumn != null)
						{
							this.PostRenderColumn(kgfdataColumn, EventArgs.Empty);
						}
					}
				}
				KGFGUIUtility.EndHorizontalBox();
				if (GUILayoutUtility.GetLastRect().Contains(Event.current.mousePosition) && Event.current.type == EventType.MouseDown && Event.current.button == 0)
				{
					this.itsClickedRow = kgfdataRow;
					this.itsRepaint = true;
				}
				if (this.OnClickRow != null && this.itsClickedRow != null && Event.current.type == EventType.Layout)
				{
					if (this.itsCurrentSelected != this.itsClickedRow)
					{
						this.itsCurrentSelected = this.itsClickedRow;
					}
					else
					{
						this.itsCurrentSelected = null;
					}
					this.OnClickRow(this.itsClickedRow, EventArgs.Empty);
					this.itsClickedRow = null;
				}
				if (this.PostRenderRow != null)
				{
					this.PostRenderRow(kgfdataRow, EventArgs.Empty);
				}
				num++;
			}
			GUI.color = color;
			GUILayout.FlexibleSpace();
			GUILayout.EndVertical();
		}
		else
		{
			GUILayout.Label("no items found", new GUILayoutOption[0]);
			GUILayout.FlexibleSpace();
		}
		GUILayout.EndScrollView();
		this.itsRectScrollView = GUILayoutUtility.GetLastRect();
	}

	// Token: 0x06000AAD RID: 2733 RVA: 0x00051218 File Offset: 0x0004F418
	public Rect GetLastRectScrollview()
	{
		return this.itsRectScrollView;
	}

	// Token: 0x06000AAE RID: 2734 RVA: 0x00051220 File Offset: 0x0004F420
	public bool GetRepaintWish()
	{
		bool result = this.itsRepaint;
		this.itsRepaint = false;
		return result;
	}

	// Token: 0x06000AAF RID: 2735 RVA: 0x0005123C File Offset: 0x0004F43C
	public void SetSortingColumn(string theColumnName)
	{
		foreach (KGFDataColumn kgfdataColumn in this.itsDataTable.Columns)
		{
			if (kgfdataColumn.ColumnName == theColumnName)
			{
				this.itsSortColumn = kgfdataColumn;
				this.itsRepaint = true;
				break;
			}
		}
	}

	// Token: 0x06000AB0 RID: 2736 RVA: 0x000512C4 File Offset: 0x0004F4C4
	public void SetSortingColumn(KGFDataColumn theColumn)
	{
		this.itsSortColumn = theColumn;
		this.itsRepaint = true;
		if (this.EventSettingsChanged != null)
		{
			this.EventSettingsChanged(this, null);
		}
	}

	// Token: 0x06000AB1 RID: 2737 RVA: 0x000512F8 File Offset: 0x0004F4F8
	public KGFDataColumn GetSortingColumn()
	{
		return this.itsSortColumn;
	}

	// Token: 0x06000AB2 RID: 2738 RVA: 0x00051300 File Offset: 0x0004F500
	public string SaveSettings()
	{
		return string.Format("SortBy:" + ((this.itsSortColumn == null) ? string.Empty : this.itsSortColumn.ColumnName), new object[0]);
	}

	// Token: 0x06000AB3 RID: 2739 RVA: 0x00051338 File Offset: 0x0004F538
	public void LoadSettings(string theSettingsString)
	{
		string[] array = theSettingsString.Split(new char[]
		{
			':'
		});
		if (array.Length == 2 && array[0] == "SortBy")
		{
			if (array[1].Trim() == string.Empty)
			{
				this.SetSortingColumn(null);
			}
			else
			{
				this.SetSortingColumn(array[1]);
			}
		}
	}

	// Token: 0x06000AB4 RID: 2740 RVA: 0x000513A0 File Offset: 0x0004F5A0
	public void Render()
	{
		if (this.itsVisible)
		{
			GUILayout.BeginVertical(new GUILayoutOption[0]);
			this.RenderTableHeadings();
			this.RenderTableRows();
			GUILayout.EndVertical();
		}
	}

	// Token: 0x06000AB5 RID: 2741 RVA: 0x000513CC File Offset: 0x0004F5CC
	public string GetName()
	{
		return "KGFGUIDataTable";
	}

	// Token: 0x06000AB6 RID: 2742 RVA: 0x000513D4 File Offset: 0x0004F5D4
	public bool IsVisible()
	{
		return this.itsVisible;
	}

	// Token: 0x04000AFD RID: 2813
	private KGFDataTable itsDataTable;

	// Token: 0x04000AFE RID: 2814
	private Vector2 itsDataTableScrollViewPosition;

	// Token: 0x04000AFF RID: 2815
	private uint itsStartRow;

	// Token: 0x04000B00 RID: 2816
	private uint itsDisplayRowCount = 100U;

	// Token: 0x04000B01 RID: 2817
	private Dictionary<KGFDataColumn, uint> itsColumnWidth = new Dictionary<KGFDataColumn, uint>();

	// Token: 0x04000B02 RID: 2818
	private Dictionary<KGFDataColumn, bool> itsColumnVisible = new Dictionary<KGFDataColumn, bool>();

	// Token: 0x04000B03 RID: 2819
	private KGFDataRow itsClickedRow;

	// Token: 0x04000B04 RID: 2820
	private KGFDataRow itsCurrentSelected;

	// Token: 0x04000B05 RID: 2821
	private bool itsVisible = true;

	// Token: 0x04000B06 RID: 2822
	private static Texture2D itsTextureArrowUp;

	// Token: 0x04000B07 RID: 2823
	private static Texture2D itsTextureArrowDown;

	// Token: 0x04000B08 RID: 2824
	private KGFDataColumn itsSortColumn;

	// Token: 0x04000B09 RID: 2825
	private bool itsSortDirection;

	// Token: 0x04000B0A RID: 2826
	private Rect itsRectScrollView = default(Rect);

	// Token: 0x04000B0B RID: 2827
	private bool itsRepaint;
}
