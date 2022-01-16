using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000173 RID: 371
public class KGFGUISelectionList
{
	// Token: 0x1400003E RID: 62
	// (add) Token: 0x06000B02 RID: 2818 RVA: 0x00053398 File Offset: 0x00051598
	// (remove) Token: 0x06000B03 RID: 2819 RVA: 0x000533B4 File Offset: 0x000515B4
	public event EventHandler EventItemChanged;

	// Token: 0x06000B04 RID: 2820 RVA: 0x000533D0 File Offset: 0x000515D0
	public void SetValues(IEnumerable theList)
	{
		this.itsListSource = theList;
		this.UpdateList();
		this.UpdateItemFilter();
	}

	// Token: 0x06000B05 RID: 2821 RVA: 0x000533E8 File Offset: 0x000515E8
	public bool GetIsSelected(object theItem)
	{
		foreach (KGFGUISelectionList.ListItem listItem in this.itsData)
		{
			if (theItem == listItem.GetItem())
			{
				return listItem.itsSelected;
			}
		}
		return false;
	}

	// Token: 0x06000B06 RID: 2822 RVA: 0x00053464 File Offset: 0x00051664
	public void SetDisplayMethod(Func<object, string> theDisplayMethod)
	{
		this.itsDisplayMethod = theDisplayMethod;
		this.UpdateItemFilter();
	}

	// Token: 0x06000B07 RID: 2823 RVA: 0x00053474 File Offset: 0x00051674
	public void ClearDisplayMethod()
	{
		this.itsDisplayMethod = null;
		this.UpdateItemFilter();
	}

	// Token: 0x06000B08 RID: 2824 RVA: 0x00053484 File Offset: 0x00051684
	private int ListItemComparer(KGFGUISelectionList.ListItem theListItem1, KGFGUISelectionList.ListItem theListItem2)
	{
		return theListItem1.GetString().CompareTo(theListItem2.GetString());
	}

	// Token: 0x06000B09 RID: 2825 RVA: 0x00053498 File Offset: 0x00051698
	public void Render()
	{
		GUILayout.BeginVertical(new GUILayoutOption[0]);
		KGFGUIUtility.BeginVerticalBox(KGFGUIUtility.eStyleBox.eBoxDarkTop, new GUILayoutOption[0]);
		this.DrawButtons();
		KGFGUIUtility.EndVerticalBox();
		KGFGUIUtility.BeginVerticalBox(KGFGUIUtility.eStyleBox.eBoxMiddleVertical, new GUILayoutOption[0]);
		this.DrawList();
		KGFGUIUtility.EndVerticalBox();
		KGFGUIUtility.BeginHorizontalBox(KGFGUIUtility.eStyleBox.eBoxDarkMiddleVertical, new GUILayoutOption[0]);
		KGFGUIUtility.Label(string.Empty, new GUILayoutOption[]
		{
			GUILayout.ExpandWidth(true)
		});
		KGFGUIUtility.EndHorizontalBox();
		KGFGUIUtility.BeginVerticalBox(KGFGUIUtility.eStyleBox.eBoxDarkBottom, new GUILayoutOption[0]);
		this.DrawSearch();
		KGFGUIUtility.EndVerticalBox();
		GUILayout.EndVertical();
		if (GUI.GetNameOfFocusedControl().Equals("tagSearch") && this.itsSearch.Equals("Search"))
		{
			this.itsSearch = string.Empty;
		}
		if (!GUI.GetNameOfFocusedControl().Equals("tagSearch") && this.itsSearch.Equals(string.Empty))
		{
			this.itsSearch = "Search";
		}
	}

	// Token: 0x06000B0A RID: 2826 RVA: 0x00053590 File Offset: 0x00051790
	public IEnumerable<object> GetSelected()
	{
		foreach (KGFGUISelectionList.ListItem anItem in this.itsData)
		{
			if (anItem.itsSelected)
			{
				yield return anItem.GetItem();
			}
		}
		yield break;
	}

	// Token: 0x06000B0B RID: 2827 RVA: 0x000535B4 File Offset: 0x000517B4
	public void SetSelected(IEnumerable<object> theList)
	{
		this.SetSelectedAll(false);
		foreach (object theItem in theList)
		{
			this.SetSelected(theItem, true);
		}
	}

	// Token: 0x06000B0C RID: 2828 RVA: 0x0005361C File Offset: 0x0005181C
	public void SetSelected(object theItem, bool theSelectionState)
	{
		foreach (KGFGUISelectionList.ListItem listItem in this.itsData)
		{
			if (theItem == listItem.GetItem())
			{
				listItem.itsSelected = theSelectionState;
				break;
			}
		}
	}

	// Token: 0x06000B0D RID: 2829 RVA: 0x00053694 File Offset: 0x00051894
	public void SetSelected(string theItem, bool theSelectionState)
	{
		foreach (KGFGUISelectionList.ListItem listItem in this.itsData)
		{
			if (theItem == listItem.GetItem().ToString())
			{
				listItem.itsSelected = theSelectionState;
				break;
			}
		}
	}

	// Token: 0x06000B0E RID: 2830 RVA: 0x00053718 File Offset: 0x00051918
	private void UpdateList()
	{
		List<object> selected = new List<object>(this.GetSelected());
		this.itsData.Clear();
		foreach (object arg in this.itsListSource)
		{
			this.itsData.Add(new KGFGUISelectionList.ListItem(string.Empty + arg));
		}
		this.itsData.Sort(new Comparison<KGFGUISelectionList.ListItem>(this.ListItemComparer));
		this.SetSelected(selected);
	}

	// Token: 0x06000B0F RID: 2831 RVA: 0x000537CC File Offset: 0x000519CC
	public void UpdateItemFilter()
	{
		if (this.itsSearch.Trim() == string.Empty || this.itsSearch.Trim() == "Search")
		{
			foreach (KGFGUISelectionList.ListItem listItem in this.itsData)
			{
				listItem.itsFiltered = false;
			}
		}
		else
		{
			foreach (KGFGUISelectionList.ListItem listItem2 in this.itsData)
			{
				listItem2.UpdateCache(this.itsDisplayMethod);
				listItem2.itsFiltered = !listItem2.GetString().Trim().ToLower().Contains(this.itsSearch.Trim().ToLower());
			}
		}
	}

	// Token: 0x06000B10 RID: 2832 RVA: 0x000538F4 File Offset: 0x00051AF4
	public void SetSelectedAll(bool theValue)
	{
		foreach (KGFGUISelectionList.ListItem listItem in this.itsData)
		{
			listItem.itsSelected = theValue;
		}
		if (this.EventItemChanged != null)
		{
			this.EventItemChanged(this, null);
		}
	}

	// Token: 0x06000B11 RID: 2833 RVA: 0x00053974 File Offset: 0x00051B74
	private void DrawButtons()
	{
		GUILayout.BeginHorizontal(new GUILayoutOption[0]);
		if (KGFGUIUtility.Button("All", KGFGUIUtility.eStyleButton.eButton, new GUILayoutOption[0]))
		{
			this.SetSelectedAll(true);
		}
		if (KGFGUIUtility.Button("None", KGFGUIUtility.eStyleButton.eButton, new GUILayoutOption[0]))
		{
			this.SetSelectedAll(false);
		}
		GUILayout.EndHorizontal();
	}

	// Token: 0x06000B12 RID: 2834 RVA: 0x000539CC File Offset: 0x00051BCC
	private void DrawList()
	{
		this.itsScrollPosition = GUILayout.BeginScrollView(this.itsScrollPosition, new GUILayoutOption[0]);
		KGFGUIUtility.BeginVerticalBox(KGFGUIUtility.eStyleBox.eBoxInvisible, new GUILayoutOption[0]);
		foreach (KGFGUISelectionList.ListItem listItem in this.itsData)
		{
			if (!listItem.itsFiltered)
			{
				bool flag = KGFGUIUtility.Toggle(listItem.itsSelected, listItem.GetString(), KGFGUIUtility.eStyleToggl.eTogglSuperCompact, new GUILayoutOption[0]);
				if (flag != listItem.itsSelected)
				{
					listItem.itsSelected = flag;
					if (this.EventItemChanged != null)
					{
						this.EventItemChanged(this, null);
					}
				}
			}
		}
		KGFGUIUtility.EndVerticalBox();
		GUILayout.EndScrollView();
	}

	// Token: 0x06000B13 RID: 2835 RVA: 0x00053AAC File Offset: 0x00051CAC
	private void DrawSearch()
	{
		GUI.SetNextControlName("tagSearch");
		string a = KGFGUIUtility.TextField(this.itsSearch, KGFGUIUtility.eStyleTextField.eTextField, new GUILayoutOption[0]);
		if (a != this.itsSearch)
		{
			this.itsSearch = a;
			this.UpdateItemFilter();
		}
	}

	// Token: 0x04000B52 RID: 2898
	private const string itsControlSearchName = "tagSearch";

	// Token: 0x04000B53 RID: 2899
	private const string itsTextSearch = "Search";

	// Token: 0x04000B54 RID: 2900
	private List<KGFGUISelectionList.ListItem> itsData = new List<KGFGUISelectionList.ListItem>();

	// Token: 0x04000B55 RID: 2901
	private string itsSearch = string.Empty;

	// Token: 0x04000B56 RID: 2902
	private IEnumerable itsListSource;

	// Token: 0x04000B57 RID: 2903
	private Vector2 itsScrollPosition = Vector2.zero;

	// Token: 0x04000B58 RID: 2904
	private Func<object, string> itsDisplayMethod;

	// Token: 0x02000174 RID: 372
	private class ListItem
	{
		// Token: 0x06000B14 RID: 2836 RVA: 0x00053AF4 File Offset: 0x00051CF4
		public ListItem(object theItem)
		{
			this.itsItem = theItem;
			this.itsSelected = false;
			this.itsFiltered = false;
			this.UpdateCache(null);
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x00053B24 File Offset: 0x00051D24
		public void UpdateCache(Func<object, string> theDisplayMethod)
		{
			if (theDisplayMethod != null)
			{
				this.itsCachedString = theDisplayMethod(this.itsItem);
			}
			else
			{
				this.itsCachedString = this.itsItem.ToString();
			}
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x00053B60 File Offset: 0x00051D60
		public string GetString()
		{
			return this.itsCachedString;
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x00053B68 File Offset: 0x00051D68
		public object GetItem()
		{
			return this.itsItem;
		}

		// Token: 0x04000B5A RID: 2906
		private string itsCachedString;

		// Token: 0x04000B5B RID: 2907
		private object itsItem;

		// Token: 0x04000B5C RID: 2908
		public bool itsSelected;

		// Token: 0x04000B5D RID: 2909
		public bool itsFiltered;
	}
}
