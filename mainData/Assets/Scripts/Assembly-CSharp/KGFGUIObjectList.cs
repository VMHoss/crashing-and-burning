using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

// Token: 0x0200016F RID: 367
public class KGFGUIObjectList
{
	// Token: 0x06000AC9 RID: 2761 RVA: 0x00051868 File Offset: 0x0004FA68
	public KGFGUIObjectList(Type theType)
	{
		this.itsListData = new List<KGFITaggable>();
		this.itsItemType = theType;
		this.itsData = new KGFDataTable();
		this.itsListFieldCache = new List<KGFGUIObjectList.KGFObjectListColumnItem>();
		this.CacheTypeMembers();
		this.itsGuiData = new KGFGUIDataTable(this.itsData, new GUILayoutOption[0]);
		this.itsGuiData.OnClickRow += this.OnClickRow;
		this.itsGuiData.EventSettingsChanged += this.OnGuiDataSettingsChanged;
		this.itsGuiData.SetColumnVisible(0, false);
		for (int i = 0; i < this.itsListFieldCache.Count; i++)
		{
			this.itsGuiData.SetColumnVisible(i + 1, this.itsListFieldCache[i].itsDisplay);
		}
		this.itsListViewCategories = new KGFGUISelectionList();
		this.itsListViewCategories.EventItemChanged += this.OnCategoriesChanged;
	}

	// Token: 0x14000037 RID: 55
	// (add) Token: 0x06000ACA RID: 2762 RVA: 0x00051998 File Offset: 0x0004FB98
	// (remove) Token: 0x06000ACB RID: 2763 RVA: 0x000519B4 File Offset: 0x0004FBB4
	public event EventHandler EventSelect;

	// Token: 0x14000038 RID: 56
	// (add) Token: 0x06000ACC RID: 2764 RVA: 0x000519D0 File Offset: 0x0004FBD0
	// (remove) Token: 0x06000ACD RID: 2765 RVA: 0x000519EC File Offset: 0x0004FBEC
	public event EventHandler EventSettingsChanged;

	// Token: 0x14000039 RID: 57
	// (add) Token: 0x06000ACE RID: 2766 RVA: 0x00051A08 File Offset: 0x0004FC08
	// (remove) Token: 0x06000ACF RID: 2767 RVA: 0x00051A24 File Offset: 0x0004FC24
	public event EventHandler EventNew;

	// Token: 0x1400003A RID: 58
	// (add) Token: 0x06000AD0 RID: 2768 RVA: 0x00051A40 File Offset: 0x0004FC40
	// (remove) Token: 0x06000AD1 RID: 2769 RVA: 0x00051A5C File Offset: 0x0004FC5C
	public event EventHandler EventDelete;

	// Token: 0x1400003B RID: 59
	// (add) Token: 0x06000AD2 RID: 2770 RVA: 0x00051A78 File Offset: 0x0004FC78
	// (remove) Token: 0x06000AD3 RID: 2771 RVA: 0x00051A88 File Offset: 0x0004FC88
	public event EventHandler PreRenderRow
	{
		add
		{
			this.itsGuiData.PreRenderRow += value;
		}
		remove
		{
			this.itsGuiData.PreRenderRow -= value;
		}
	}

	// Token: 0x1400003C RID: 60
	// (add) Token: 0x06000AD4 RID: 2772 RVA: 0x00051A98 File Offset: 0x0004FC98
	// (remove) Token: 0x06000AD5 RID: 2773 RVA: 0x00051AA8 File Offset: 0x0004FCA8
	public event Func<KGFDataRow, KGFDataColumn, uint, bool> PreCellHandler
	{
		add
		{
			this.itsGuiData.PreCellContentHandler += value;
		}
		remove
		{
			this.itsGuiData.PreCellContentHandler -= value;
		}
	}

	// Token: 0x1400003D RID: 61
	// (add) Token: 0x06000AD6 RID: 2774 RVA: 0x00051AB8 File Offset: 0x0004FCB8
	// (remove) Token: 0x06000AD7 RID: 2775 RVA: 0x00051AC8 File Offset: 0x0004FCC8
	public event EventHandler PostRenderRow
	{
		add
		{
			this.itsGuiData.PostRenderRow += value;
		}
		remove
		{
			this.itsGuiData.PostRenderRow -= value;
		}
	}

	// Token: 0x06000AD8 RID: 2776 RVA: 0x00051AD8 File Offset: 0x0004FCD8
	private void OnGuiDataSettingsChanged(object theSender, EventArgs theArgs)
	{
		this.OnSettingsChanged();
	}

	// Token: 0x06000AD9 RID: 2777 RVA: 0x00051AE0 File Offset: 0x0004FCE0
	public void SetFulltextFilter(string theFulltextSearch)
	{
		this.itsFulltextSearch = theFulltextSearch;
		this.UpdateList();
	}

	// Token: 0x06000ADA RID: 2778 RVA: 0x00051AF0 File Offset: 0x0004FCF0
	public void SetColumnWidthAll(uint theWidth)
	{
		for (int i = 1; i < this.itsListFieldCache.Count + 1; i++)
		{
			this.itsGuiData.SetColumnWidth(i, theWidth);
		}
	}

	// Token: 0x06000ADB RID: 2779 RVA: 0x00051B28 File Offset: 0x0004FD28
	public void SetColumnWidth(string theColumnHeader, uint theWidth)
	{
		for (int i = 0; i < this.itsListFieldCache.Count; i++)
		{
			if (this.itsListFieldCache[i].itsDisplay && this.itsListFieldCache[i].itsHeader == theColumnHeader)
			{
				this.itsGuiData.SetColumnWidth(i + 1, theWidth);
				break;
			}
		}
	}

	// Token: 0x06000ADC RID: 2780 RVA: 0x00051B98 File Offset: 0x0004FD98
	public void SetColumnVisible(string theColumnHeader, bool theVisible)
	{
		for (int i = 0; i < this.itsListFieldCache.Count; i++)
		{
			if (this.itsListFieldCache[i].itsDisplay && this.itsListFieldCache[i].itsHeader == theColumnHeader)
			{
				this.itsGuiData.SetColumnVisible(i + 1, theVisible);
				break;
			}
		}
	}

	// Token: 0x06000ADD RID: 2781 RVA: 0x00051C08 File Offset: 0x0004FE08
	public void SetList(IEnumerable theList)
	{
		List<KGFITaggable> list = new List<KGFITaggable>();
		foreach (object obj in theList)
		{
			if (obj is KGFITaggable)
			{
				list.Add((KGFITaggable)obj);
			}
		}
		this.SetList(list);
	}

	// Token: 0x06000ADE RID: 2782 RVA: 0x00051C8C File Offset: 0x0004FE8C
	public void SetList(IEnumerable<KGFITaggable> theList)
	{
		this.itsListData = new List<KGFITaggable>(theList);
		this.itsListViewCategories.SetValues(this.GetAllTags().Distinct<string>());
		this.UpdateList();
	}

	// Token: 0x06000ADF RID: 2783 RVA: 0x00051CC4 File Offset: 0x0004FEC4
	public void AddMember(MemberInfo theMemberInfo, string theHeader)
	{
		this.AddMember(theMemberInfo, theHeader, false);
	}

	// Token: 0x06000AE0 RID: 2784 RVA: 0x00051CD0 File Offset: 0x0004FED0
	public void AddMember(MemberInfo theMemberInfo, string theHeader, bool theSearchable)
	{
		this.AddMember(theMemberInfo, theHeader, theSearchable, true);
	}

	// Token: 0x06000AE1 RID: 2785 RVA: 0x00051CDC File Offset: 0x0004FEDC
	public void AddMember(MemberInfo theMemberInfo, string theHeader, bool theSearchable, bool theDisplay)
	{
		KGFGUIObjectList.KGFObjectListColumnItem kgfobjectListColumnItem = new KGFGUIObjectList.KGFObjectListColumnItem(theMemberInfo);
		kgfobjectListColumnItem.itsHeader = theHeader;
		kgfobjectListColumnItem.itsSearchable = theSearchable;
		kgfobjectListColumnItem.itsDisplay = theDisplay;
		this.itsListFieldCache.Add(kgfobjectListColumnItem);
		this.itsData.Columns.Add(new KGFDataColumn(theHeader, kgfobjectListColumnItem.GetReturnType()));
		if (kgfobjectListColumnItem.itsSearchable)
		{
			this.itsDisplayFullTextSearch = true;
		}
	}

	// Token: 0x06000AE2 RID: 2786 RVA: 0x00051D40 File Offset: 0x0004FF40
	public object GetCurrentSelected()
	{
		return this.itsCurrentSelectedItem;
	}

	// Token: 0x06000AE3 RID: 2787 RVA: 0x00051D48 File Offset: 0x0004FF48
	public void ClearSelected()
	{
		this.itsCurrentSelectedItem = null;
	}

	// Token: 0x06000AE4 RID: 2788 RVA: 0x00051D54 File Offset: 0x0004FF54
	public void SetSelected(KGFITaggable theObject)
	{
		this.itsCurrentSelectedItem = theObject;
		int num = 0;
		foreach (KGFDataRow kgfdataRow in this.itsData.Rows)
		{
			if (kgfdataRow[0].Value == theObject)
			{
				this.itsGuiData.SetCurrentSelected(kgfdataRow);
				this.itsCurrentPage = num / (int)this.itsItemsPerPage;
				break;
			}
			num++;
		}
	}

	// Token: 0x06000AE5 RID: 2789 RVA: 0x00051DF8 File Offset: 0x0004FFF8
	public Rect GetLastRectScrollView()
	{
		return this.itsGuiData.GetLastRectScrollview();
	}

	// Token: 0x06000AE6 RID: 2790 RVA: 0x00051E08 File Offset: 0x00050008
	public string SaveSettings()
	{
		List<string> list = new List<string>();
		foreach (KGFGUIObjectList.KGFObjectListColumnItem kgfobjectListColumnItem in this.itsListFieldCache)
		{
			if (kgfobjectListColumnItem.itsDropDown != null)
			{
				list.Add(kgfobjectListColumnItem.itsHeader + "=" + kgfobjectListColumnItem.itsDropDown.SelectedItem());
			}
			else
			{
				list.Add(kgfobjectListColumnItem.itsHeader + "=" + kgfobjectListColumnItem.itsFilterString);
			}
		}
		string arg = (this.itsGuiData.GetSortingColumn() == null) ? string.Empty : this.itsGuiData.GetSortingColumn().ColumnName;
		List<string> list2 = new List<string>();
		foreach (object arg2 in this.itsListViewCategories.GetSelected())
		{
			list2.Add(string.Empty + arg2);
		}
		string arg3 = list2.JoinToString(",");
		return string.Format("Filter:{0};SortBy:{1};Tags:{2}", list.JoinToString(","), arg, arg3);
	}

	// Token: 0x06000AE7 RID: 2791 RVA: 0x00051F80 File Offset: 0x00050180
	public void LoadSettings(string theSettingsString)
	{
		this.itsLoadingActive = true;
		string[] array = theSettingsString.Split(new char[]
		{
			';'
		});
		foreach (string text in array)
		{
			string[] array3 = text.Split(new char[]
			{
				':'
			});
			if (array3.Length == 2)
			{
				if (array3[0] == "Filter")
				{
					foreach (string text2 in array3[1].Split(new char[]
					{
						','
					}))
					{
						string[] array5 = text2.Split(new char[]
						{
							'='
						});
						if (array5.Length == 2)
						{
							this.SetFilterInternal(array5[0], array5[1]);
						}
					}
				}
				if (array3[0] == "SortBy")
				{
					if (array3[1].Trim() == string.Empty)
					{
						this.itsGuiData.SetSortingColumn(null);
					}
					else
					{
						this.itsGuiData.SetSortingColumn(array3[1]);
					}
				}
				if (array3[0] == "Tags")
				{
					this.itsListViewCategories.SetSelectedAll(false);
					foreach (string theItem in array3[1].Split(new char[]
					{
						','
					}))
					{
						this.itsListViewCategories.SetSelected(theItem, true);
					}
				}
			}
		}
		this.itsRepaintWish = true;
		this.UpdateList();
		this.itsLoadingActive = false;
	}

	// Token: 0x06000AE8 RID: 2792 RVA: 0x00052118 File Offset: 0x00050318
	public void SetFilter(string theColumnName, string theFilter)
	{
		if (this.SetFilterInternal(theColumnName, theFilter))
		{
			this.OnSettingsChanged();
		}
	}

	// Token: 0x06000AE9 RID: 2793 RVA: 0x00052130 File Offset: 0x00050330
	public void ClearFilters()
	{
		foreach (KGFGUIObjectList.KGFObjectListColumnItem kgfobjectListColumnItem in this.itsListFieldCache)
		{
			kgfobjectListColumnItem.itsFilterString = string.Empty;
			if (kgfobjectListColumnItem.itsDropDown != null)
			{
				kgfobjectListColumnItem.itsDropDown.SetSelectedItem("<NONE>");
			}
		}
		this.itsRepaintWish = true;
		this.OnSettingsChanged();
	}

	// Token: 0x06000AEA RID: 2794 RVA: 0x000521C4 File Offset: 0x000503C4
	private bool SetFilterInternal(string theColumnName, string theFilter)
	{
		foreach (KGFGUIObjectList.KGFObjectListColumnItem kgfobjectListColumnItem in this.itsListFieldCache)
		{
			if (theColumnName == kgfobjectListColumnItem.itsHeader)
			{
				kgfobjectListColumnItem.itsFilterString = theFilter;
				this.itsRepaintWish = true;
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000AEB RID: 2795 RVA: 0x0005224C File Offset: 0x0005044C
	public void Render()
	{
		if (this.itsUpdateWish)
		{
			this.UpdateList();
		}
		int num = (int)Math.Ceiling((double)((float)this.itsData.Rows.Count / (float)this.itsItemsPerPage));
		if (this.itsCurrentPage >= num)
		{
			this.itsCurrentPage = 0;
		}
		this.itsRepaintWish = false;
		this.itsGuiData.SetDisplayRowCount((uint)this.itsItemsPerPage);
		KGFGUIUtility.BeginHorizontalBox(KGFGUIUtility.eStyleBox.eBoxDecorated, new GUILayoutOption[0]);
		GUILayout.BeginVertical(new GUILayoutOption[]
		{
			GUILayout.Width(180f)
		});
		this.itsListViewCategories.Render();
		GUILayout.EndVertical();
		KGFGUIUtility.SpaceSmall();
		GUILayout.BeginVertical(new GUILayoutOption[0]);
		this.itsGuiData.SetStartRow((uint)((long)this.itsCurrentPage * (long)((ulong)this.itsItemsPerPage)));
		this.itsGuiData.Render();
		KGFGUIUtility.BeginHorizontalBox(KGFGUIUtility.eStyleBox.eBoxMiddleVerticalInteractive, new GUILayoutOption[0]);
		int num2 = 0;
		foreach (KGFGUIObjectList.KGFObjectListColumnItem kgfobjectListColumnItem in this.itsListFieldCache)
		{
			num2++;
			if (kgfobjectListColumnItem.itsDisplay)
			{
				if (this.itsGuiData.GetColumnVisible(num2))
				{
					if (kgfobjectListColumnItem.itsSearchable && (kgfobjectListColumnItem.GetReturnType().IsEnum || kgfobjectListColumnItem.GetReturnType() == typeof(bool) || kgfobjectListColumnItem.GetReturnType() == typeof(string)))
					{
						GUILayout.BeginHorizontal(new GUILayoutOption[]
						{
							GUILayout.Width(this.itsGuiData.GetColumnWidth(num2))
						});
						KGFGUIUtility.BeginVerticalBox(KGFGUIUtility.eStyleBox.eBoxInvisible, new GUILayoutOption[0]);
						this.DrawFilterBox(kgfobjectListColumnItem, this.itsGuiData.GetColumnWidth(num2) - 4U);
						KGFGUIUtility.EndVerticalBox();
						GUILayout.EndHorizontal();
						KGFGUIUtility.Separator(KGFGUIUtility.eStyleSeparator.eSeparatorVerticalFitInBox, new GUILayoutOption[0]);
					}
					else
					{
						GUILayout.BeginHorizontal(new GUILayoutOption[]
						{
							GUILayout.Width(this.itsGuiData.GetColumnWidth(num2))
						});
						GUILayout.Label(" ", new GUILayoutOption[0]);
						GUILayout.EndHorizontal();
						KGFGUIUtility.Separator(KGFGUIUtility.eStyleSeparator.eSeparatorVerticalFitInBox, new GUILayoutOption[0]);
					}
				}
			}
		}
		GUILayout.FlexibleSpace();
		KGFGUIUtility.EndHorizontalBox();
		KGFGUIUtility.BeginHorizontalBox(KGFGUIUtility.eStyleBox.eBoxDarkMiddleVertical, new GUILayoutOption[0]);
		GUILayout.Label(string.Empty, new GUILayoutOption[0]);
		GUILayout.FlexibleSpace();
		KGFGUIUtility.EndHorizontalBox();
		KGFGUIUtility.BeginVerticalBox(KGFGUIUtility.eStyleBox.eBoxDarkBottom, new GUILayoutOption[0]);
		GUILayout.BeginHorizontal(new GUILayoutOption[0]);
		if (!Application.isPlaying)
		{
			if (this.EventNew != null && KGFGUIUtility.Button("New", KGFGUIUtility.eStyleButton.eButton, new GUILayoutOption[]
			{
				GUILayout.Width(75f)
			}))
			{
				this.EventNew(this, null);
			}
			if (this.EventDelete != null && KGFGUIUtility.Button("Delete", KGFGUIUtility.eStyleButton.eButton, new GUILayoutOption[]
			{
				GUILayout.Width(75f)
			}))
			{
				this.EventDelete(this, null);
			}
			GUILayout.FlexibleSpace();
		}
		if (this.itsDisplayFullTextSearch)
		{
			GUI.SetNextControlName("KGFGuiObjectList.FullTextSearch");
			string a = KGFGUIUtility.TextField(this.itsFulltextSearch, KGFGUIUtility.eStyleTextField.eTextField, new GUILayoutOption[]
			{
				GUILayout.Width(200f)
			});
			if (a != this.itsFulltextSearch)
			{
				this.itsFulltextSearch = a;
				this.UpdateList();
			}
		}
		KGFGUIUtility.Space();
		bool flag = KGFGUIUtility.Toggle(this.itsIncludeAll, "all Tags", KGFGUIUtility.eStyleToggl.eTogglSuperCompact, new GUILayoutOption[]
		{
			GUILayout.Width(70f)
		});
		if (flag != this.itsIncludeAll)
		{
			this.itsIncludeAll = flag;
			this.UpdateList();
		}
		if (KGFGUIUtility.Button("clear filters", KGFGUIUtility.eStyleButton.eButton, new GUILayoutOption[]
		{
			GUILayout.Width(100f)
		}))
		{
			this.itsFulltextSearch = string.Empty;
			this.ClearFilters();
			this.UpdateList();
		}
		GUILayout.FlexibleSpace();
		KGFGUIUtility.BeginHorizontalBox(KGFGUIUtility.eStyleBox.eBoxInvisible, new GUILayoutOption[0]);
		if (this.GetDisplayEntriesPerPage())
		{
			if (KGFGUIUtility.Button("<", KGFGUIUtility.eStyleButton.eButtonLeft, new GUILayoutOption[]
			{
				GUILayout.Width(25f)
			}))
			{
				KGFGUIObjectList.KGFeItemsPerPage kgfeItemsPerPage = this.itsItemsPerPage;
				if (kgfeItemsPerPage != KGFGUIObjectList.KGFeItemsPerPage.e25)
				{
					if (kgfeItemsPerPage != KGFGUIObjectList.KGFeItemsPerPage.e50)
					{
						if (kgfeItemsPerPage != KGFGUIObjectList.KGFeItemsPerPage.e100)
						{
							if (kgfeItemsPerPage != KGFGUIObjectList.KGFeItemsPerPage.e250)
							{
								if (kgfeItemsPerPage == KGFGUIObjectList.KGFeItemsPerPage.e500)
								{
									this.itsItemsPerPage = KGFGUIObjectList.KGFeItemsPerPage.e250;
								}
							}
							else
							{
								this.itsItemsPerPage = KGFGUIObjectList.KGFeItemsPerPage.e100;
							}
						}
						else
						{
							this.itsItemsPerPage = KGFGUIObjectList.KGFeItemsPerPage.e50;
						}
					}
					else
					{
						this.itsItemsPerPage = KGFGUIObjectList.KGFeItemsPerPage.e25;
					}
				}
				else
				{
					this.itsItemsPerPage = KGFGUIObjectList.KGFeItemsPerPage.e10;
				}
			}
			KGFGUIUtility.BeginVerticalBox(KGFGUIUtility.eStyleBox.eBoxMiddleHorizontal, new GUILayoutOption[0]);
			string theText = this.itsItemsPerPage.ToString().Substring(1) + " entries per page";
			KGFGUIUtility.Label(theText, KGFGUIUtility.eStyleLabel.eLabelFitIntoBox, new GUILayoutOption[0]);
			KGFGUIUtility.EndVerticalBox();
			if (KGFGUIUtility.Button(">", KGFGUIUtility.eStyleButton.eButtonRight, new GUILayoutOption[]
			{
				GUILayout.Width(25f)
			}))
			{
				KGFGUIObjectList.KGFeItemsPerPage kgfeItemsPerPage = this.itsItemsPerPage;
				if (kgfeItemsPerPage != KGFGUIObjectList.KGFeItemsPerPage.e10)
				{
					if (kgfeItemsPerPage != KGFGUIObjectList.KGFeItemsPerPage.e25)
					{
						if (kgfeItemsPerPage != KGFGUIObjectList.KGFeItemsPerPage.e50)
						{
							if (kgfeItemsPerPage != KGFGUIObjectList.KGFeItemsPerPage.e100)
							{
								if (kgfeItemsPerPage == KGFGUIObjectList.KGFeItemsPerPage.e250)
								{
									this.itsItemsPerPage = KGFGUIObjectList.KGFeItemsPerPage.e500;
								}
							}
							else
							{
								this.itsItemsPerPage = KGFGUIObjectList.KGFeItemsPerPage.e250;
							}
						}
						else
						{
							this.itsItemsPerPage = KGFGUIObjectList.KGFeItemsPerPage.e100;
						}
					}
					else
					{
						this.itsItemsPerPage = KGFGUIObjectList.KGFeItemsPerPage.e50;
					}
				}
				else
				{
					this.itsItemsPerPage = KGFGUIObjectList.KGFeItemsPerPage.e25;
				}
			}
		}
		GUILayout.Space(10f);
		if (KGFGUIUtility.Button("<", KGFGUIUtility.eStyleButton.eButtonLeft, new GUILayoutOption[]
		{
			GUILayout.Width(25f)
		}) && this.itsCurrentPage > 0)
		{
			this.itsCurrentPage--;
		}
		KGFGUIUtility.BeginVerticalBox(KGFGUIUtility.eStyleBox.eBoxMiddleHorizontal, new GUILayoutOption[0]);
		string theText2 = string.Format("page {0}/{1}", this.itsCurrentPage + 1, Math.Max(num, 1));
		KGFGUIUtility.Label(theText2, KGFGUIUtility.eStyleLabel.eLabelFitIntoBox, new GUILayoutOption[0]);
		KGFGUIUtility.EndVerticalBox();
		if (KGFGUIUtility.Button(">", KGFGUIUtility.eStyleButton.eButtonRight, new GUILayoutOption[]
		{
			GUILayout.Width(25f)
		}) && this.itsData.Rows.Count > (int)((this.itsCurrentPage + (KGFGUIObjectList.KGFeItemsPerPage)1) * this.itsItemsPerPage))
		{
			this.itsCurrentPage++;
		}
		KGFGUIUtility.EndHorizontalBox();
		GUILayout.EndHorizontal();
		KGFGUIUtility.EndVerticalBox();
		GUILayout.EndVertical();
		KGFGUIUtility.EndHorizontalBox();
		if (GUI.GetNameOfFocusedControl().Equals("KGFGuiObjectList.FullTextSearch") && this.itsFulltextSearch.Equals("Search"))
		{
			this.itsFulltextSearch = string.Empty;
		}
		if (!GUI.GetNameOfFocusedControl().Equals("KGFGuiObjectList.FullTextSearch") && this.itsFulltextSearch.Equals(string.Empty))
		{
			this.itsFulltextSearch = "Search";
		}
	}

	// Token: 0x06000AEC RID: 2796 RVA: 0x00052950 File Offset: 0x00050B50
	public void SetDisplayEntriesPerPage(bool theDisplay)
	{
		this.itsDisplayEntriesPerPage = theDisplay;
	}

	// Token: 0x06000AED RID: 2797 RVA: 0x0005295C File Offset: 0x00050B5C
	public bool GetDisplayEntriesPerPage()
	{
		return this.itsDisplayEntriesPerPage;
	}

	// Token: 0x06000AEE RID: 2798 RVA: 0x00052964 File Offset: 0x00050B64
	public bool GetRepaint()
	{
		return this.itsGuiData.GetRepaintWish() || this.itsRepaintWish;
	}

	// Token: 0x06000AEF RID: 2799 RVA: 0x00052980 File Offset: 0x00050B80
	private void OnClickRow(object theSender, EventArgs theArgs)
	{
		KGFDataRow kgfdataRow = theSender as KGFDataRow;
		if (kgfdataRow != null)
		{
			this.itsCurrentSelectedItem = (KGFITaggable)kgfdataRow[0].Value;
			if (this.itsCurrentSelectedRow != kgfdataRow)
			{
				this.itsCurrentSelectedRow = kgfdataRow;
			}
			if (this.EventSelect != null)
			{
				this.EventSelect(this, new KGFGUIObjectList.KGFGUIObjectListSelectEventArgs(this.itsCurrentSelectedItem));
			}
		}
	}

	// Token: 0x06000AF0 RID: 2800 RVA: 0x000529E8 File Offset: 0x00050BE8
	private void OnCategoriesChanged(object theSender, EventArgs theArgs)
	{
		this.UpdateList();
		this.OnSettingsChanged();
	}

	// Token: 0x06000AF1 RID: 2801 RVA: 0x000529F8 File Offset: 0x00050BF8
	private void OnSettingsChanged()
	{
		if (!this.itsLoadingActive && this.EventSettingsChanged != null)
		{
			this.EventSettingsChanged(this, null);
		}
	}

	// Token: 0x06000AF2 RID: 2802 RVA: 0x00052A20 File Offset: 0x00050C20
	private IEnumerable<string> GetAllTags()
	{
		foreach (KGFITaggable anItem in this.itsListData)
		{
			if (anItem.GetTags().Length == 0)
			{
				yield return "<untagged>";
			}
			foreach (string aTag in anItem.GetTags())
			{
				yield return aTag;
			}
		}
		yield break;
	}

	// Token: 0x06000AF3 RID: 2803 RVA: 0x00052A44 File Offset: 0x00050C44
	private void CacheTypeMembers()
	{
		this.itsDisplayFullTextSearch = false;
		this.itsData.Rows.Clear();
		this.itsData.Columns.Clear();
		this.itsListFieldCache.Clear();
		this.itsData.Columns.Add(new KGFDataColumn("DATA", this.itsItemType));
		foreach (FieldInfo theMemberInfo in this.itsItemType.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
		{
			this.TryAddMember(theMemberInfo);
		}
		foreach (PropertyInfo theMemberInfo2 in this.itsItemType.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
		{
			this.TryAddMember(theMemberInfo2);
		}
	}

	// Token: 0x06000AF4 RID: 2804 RVA: 0x00052B08 File Offset: 0x00050D08
	private void TryAddMember(MemberInfo theMemberInfo)
	{
		KGFObjectListItemDisplayAttribute[] array = theMemberInfo.GetCustomAttributes(typeof(KGFObjectListItemDisplayAttribute), true) as KGFObjectListItemDisplayAttribute[];
		if (array.Length == 1)
		{
			this.AddMember(theMemberInfo, array[0].Header, array[0].Searchable, array[0].Display);
		}
	}

	// Token: 0x06000AF5 RID: 2805 RVA: 0x00052B54 File Offset: 0x00050D54
	private bool FullTextFilter(KGFITaggable theItem)
	{
		if (this.itsFulltextSearch.Trim() == "Search")
		{
			return false;
		}
		foreach (string text in this.itsFulltextSearch.Trim().ToLower().Split(new char[]
		{
			' '
		}))
		{
			bool flag = false;
			string value = text;
			string text2 = null;
			string[] array2 = text.Split(new char[]
			{
				'='
			});
			if (array2.Length == 2)
			{
				value = array2[1];
				text2 = array2[0];
			}
			foreach (KGFGUIObjectList.KGFObjectListColumnItem kgfobjectListColumnItem in this.itsListFieldCache)
			{
				if (text2 == null || !(kgfobjectListColumnItem.itsHeader.ToLower() != text2.ToLower()))
				{
					object returnValue = kgfobjectListColumnItem.GetReturnValue(theItem);
					if (kgfobjectListColumnItem.itsSearchable)
					{
						if (returnValue is IEnumerable && !(returnValue is string))
						{
							foreach (object obj in ((IEnumerable)returnValue))
							{
								if (obj != null)
								{
									if (obj.ToString().Trim().ToLower().Contains(value))
									{
										flag = true;
									}
								}
							}
						}
						else
						{
							string text3 = returnValue.ToString();
							if (text3.Trim().ToLower().Contains(value))
							{
								flag = true;
							}
						}
					}
				}
			}
			if (!flag)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000AF6 RID: 2806 RVA: 0x00052D4C File Offset: 0x00050F4C
	private bool PerItemFilter(KGFITaggable theItem)
	{
		foreach (KGFGUIObjectList.KGFObjectListColumnItem kgfobjectListColumnItem in this.itsListFieldCache)
		{
			object returnValue = kgfobjectListColumnItem.GetReturnValue(theItem);
			if (kgfobjectListColumnItem.GetIsFiltered(returnValue))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000AF7 RID: 2807 RVA: 0x00052DCC File Offset: 0x00050FCC
	private void UpdateList()
	{
		if (Event.current != null && Event.current.type != EventType.Layout)
		{
			this.itsUpdateWish = true;
			return;
		}
		this.itsUpdateWish = false;
		this.itsData.Rows.Clear();
		foreach (KGFITaggable kgfitaggable in this.itsListData)
		{
			if (this.GetIsTagSelected(kgfitaggable.GetTags()))
			{
				if (string.IsNullOrEmpty(this.itsFulltextSearch) || !this.FullTextFilter(kgfitaggable))
				{
					if (!this.PerItemFilter(kgfitaggable))
					{
						KGFDataRow kgfdataRow = this.itsData.NewRow();
						kgfdataRow[0].Value = kgfitaggable;
						int num = 1;
						foreach (KGFGUIObjectList.KGFObjectListColumnItem kgfobjectListColumnItem in this.itsListFieldCache)
						{
							object returnValue = kgfobjectListColumnItem.GetReturnValue(kgfitaggable);
							kgfdataRow[num].Value = returnValue;
							num++;
						}
						this.itsData.Rows.Add(kgfdataRow);
					}
				}
			}
		}
	}

	// Token: 0x06000AF8 RID: 2808 RVA: 0x00052F48 File Offset: 0x00051148
	private bool GetIsTagSelected(string[] theTags)
	{
		List<object> list = new List<object>(this.itsListViewCategories.GetSelected());
		int count = list.Count;
		int num = 0;
		foreach (object obj in this.itsListViewCategories.GetSelected())
		{
			string text = (string)obj;
			if (theTags.Length == 0 && text == "<untagged>")
			{
				if (!this.itsIncludeAll)
				{
					return true;
				}
				num++;
			}
			foreach (string a in theTags)
			{
				if (a == text)
				{
					if (!this.itsIncludeAll)
					{
						return true;
					}
					num++;
				}
			}
		}
		return num == count && this.itsIncludeAll;
	}

	// Token: 0x06000AF9 RID: 2809 RVA: 0x00053068 File Offset: 0x00051268
	private void OnDropDownValueChanged(object theSender, EventArgs theArgs)
	{
		this.UpdateList();
		this.OnSettingsChanged();
	}

	// Token: 0x06000AFA RID: 2810 RVA: 0x00053078 File Offset: 0x00051278
	private void DrawFilterBox(KGFGUIObjectList.KGFObjectListColumnItem theItem, uint theWidth)
	{
		if (theItem.GetReturnType().IsEnum || theItem.GetReturnType() == typeof(bool))
		{
			if (theItem.itsDropDown == null)
			{
				if (theItem.GetReturnType() == typeof(bool))
				{
					theItem.itsDropDown = new KGFGUIDropDown(new List<string>(this.itsBoolValues).InsertItem("<NONE>", 0), theWidth, 5U, KGFGUIDropDown.eDropDirection.eUp, new GUILayoutOption[0]);
				}
				else if (theItem.GetReturnType().IsEnum)
				{
					theItem.itsDropDown = new KGFGUIDropDown(Enum.GetNames(theItem.GetReturnType()).InsertItem("<NONE>", 0), theWidth, 5U, KGFGUIDropDown.eDropDirection.eUp, new GUILayoutOption[0]);
				}
				theItem.itsDropDown.itsTitle = string.Empty;
				theItem.itsDropDown.SetSelectedItem(theItem.itsFilterString);
				theItem.itsDropDown.SelectedValueChanged += this.OnDropDownValueChanged;
			}
			theItem.itsDropDown.Render();
		}
		else if (theItem.GetReturnType() == typeof(string))
		{
			if (theItem.itsFilterString == null)
			{
				theItem.itsFilterString = string.Empty;
			}
			string text = KGFGUIUtility.TextField(theItem.itsFilterString, KGFGUIUtility.eStyleTextField.eTextField, new GUILayoutOption[]
			{
				GUILayout.Width(theWidth)
			});
			if (text != theItem.itsFilterString)
			{
				theItem.itsFilterString = text;
				this.UpdateList();
				this.OnSettingsChanged();
			}
		}
	}

	// Token: 0x04000B2A RID: 2858
	private const string NONE_STRING = "<NONE>";

	// Token: 0x04000B2B RID: 2859
	private const string itsControlSearchName = "KGFGuiObjectList.FullTextSearch";

	// Token: 0x04000B2C RID: 2860
	private const string itsTextSearch = "Search";

	// Token: 0x04000B2D RID: 2861
	private const string UnTagged = "<untagged>";

	// Token: 0x04000B2E RID: 2862
	private List<KGFITaggable> itsListData;

	// Token: 0x04000B2F RID: 2863
	private Type itsItemType;

	// Token: 0x04000B30 RID: 2864
	private KGFDataTable itsData;

	// Token: 0x04000B31 RID: 2865
	private KGFGUIDataTable itsGuiData;

	// Token: 0x04000B32 RID: 2866
	private List<KGFGUIObjectList.KGFObjectListColumnItem> itsListFieldCache;

	// Token: 0x04000B33 RID: 2867
	private bool itsDisplayFullTextSearch;

	// Token: 0x04000B34 RID: 2868
	private string itsFulltextSearch = string.Empty;

	// Token: 0x04000B35 RID: 2869
	private KGFGUISelectionList itsListViewCategories;

	// Token: 0x04000B36 RID: 2870
	private KGFDataRow itsCurrentSelectedRow;

	// Token: 0x04000B37 RID: 2871
	private KGFITaggable itsCurrentSelectedItem;

	// Token: 0x04000B38 RID: 2872
	private KGFGUIObjectList.KGFeItemsPerPage itsItemsPerPage = KGFGUIObjectList.KGFeItemsPerPage.e50;

	// Token: 0x04000B39 RID: 2873
	private bool itsIncludeAll = true;

	// Token: 0x04000B3A RID: 2874
	public int itsCurrentPage;

	// Token: 0x04000B3B RID: 2875
	private bool itsLoadingActive;

	// Token: 0x04000B3C RID: 2876
	private bool itsDisplayEntriesPerPage = true;

	// Token: 0x04000B3D RID: 2877
	private bool itsRepaintWish;

	// Token: 0x04000B3E RID: 2878
	private bool itsUpdateWish;

	// Token: 0x04000B3F RID: 2879
	private string[] itsBoolValues = new string[]
	{
		"True",
		"False"
	};

	// Token: 0x02000170 RID: 368
	public enum KGFeItemsPerPage
	{
		// Token: 0x04000B45 RID: 2885
		e10 = 10,
		// Token: 0x04000B46 RID: 2886
		e25 = 25,
		// Token: 0x04000B47 RID: 2887
		e50 = 50,
		// Token: 0x04000B48 RID: 2888
		e100 = 100,
		// Token: 0x04000B49 RID: 2889
		e250 = 250,
		// Token: 0x04000B4A RID: 2890
		e500 = 500
	}

	// Token: 0x02000171 RID: 369
	public class KGFGUIObjectListSelectEventArgs : EventArgs
	{
		// Token: 0x06000AFB RID: 2811 RVA: 0x000531E8 File Offset: 0x000513E8
		public KGFGUIObjectListSelectEventArgs(KGFITaggable theItem)
		{
			this.itsItem = theItem;
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x000531F8 File Offset: 0x000513F8
		public KGFITaggable GetItem()
		{
			return this.itsItem;
		}

		// Token: 0x04000B4B RID: 2891
		private KGFITaggable itsItem;
	}

	// Token: 0x02000172 RID: 370
	private class KGFObjectListColumnItem
	{
		// Token: 0x06000AFD RID: 2813 RVA: 0x00053200 File Offset: 0x00051400
		public KGFObjectListColumnItem(MemberInfo theMemberInfo)
		{
			this.itsMemberInfo = theMemberInfo;
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x0005321C File Offset: 0x0005141C
		public Type GetReturnType()
		{
			if (this.itsMemberInfo is FieldInfo)
			{
				return ((FieldInfo)this.itsMemberInfo).FieldType;
			}
			if (this.itsMemberInfo is PropertyInfo)
			{
				return ((PropertyInfo)this.itsMemberInfo).PropertyType;
			}
			return null;
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x0005326C File Offset: 0x0005146C
		public object GetReturnValue(object theInstance)
		{
			if (this.itsMemberInfo is FieldInfo)
			{
				return ((FieldInfo)this.itsMemberInfo).GetValue(theInstance);
			}
			if (this.itsMemberInfo is PropertyInfo)
			{
				return ((PropertyInfo)this.itsMemberInfo).GetValue(theInstance, null);
			}
			return null;
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x000532C0 File Offset: 0x000514C0
		public bool GetIsFiltered(object theInstance)
		{
			if (this.GetReturnType() == typeof(bool) || this.GetReturnType().IsEnum)
			{
				if (this.itsDropDown != null)
				{
					if (this.itsDropDown.SelectedItem() == "<NONE>")
					{
						return false;
					}
					if (this.itsDropDown.SelectedItem() != theInstance.ToString())
					{
						return true;
					}
				}
				return false;
			}
			return !string.IsNullOrEmpty(this.itsFilterString) && !theInstance.ToString().ToLower().Contains(this.itsFilterString.ToLower());
		}

		// Token: 0x04000B4C RID: 2892
		public string itsHeader;

		// Token: 0x04000B4D RID: 2893
		public bool itsSearchable;

		// Token: 0x04000B4E RID: 2894
		public bool itsDisplay;

		// Token: 0x04000B4F RID: 2895
		public KGFGUIDropDown itsDropDown;

		// Token: 0x04000B50 RID: 2896
		public string itsFilterString = string.Empty;

		// Token: 0x04000B51 RID: 2897
		private MemberInfo itsMemberInfo;
	}
}
