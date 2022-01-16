using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200016C RID: 364
public class KGFGUIDropDown : KGFIControl
{
	// Token: 0x06000AB7 RID: 2743 RVA: 0x000513DC File Offset: 0x0004F5DC
	public KGFGUIDropDown(IEnumerable<string> theEntrys, uint theWidth, uint theMaxVisibleItems, KGFGUIDropDown.eDropDirection theDirection, params GUILayoutOption[] theLayout)
	{
		if (theEntrys != null)
		{
			foreach (string item in theEntrys)
			{
				this.itsEntrys.Add(item);
			}
			this.itsWidth = theWidth;
			this.itsMaxVisibleItems = theMaxVisibleItems;
			this.itsDirection = theDirection;
			if (this.itsEntrys.Count > 0)
			{
				this.itsCurrentSelected = this.itsEntrys[0];
			}
		}
		else
		{
			Debug.LogError("the list of entrys was null");
		}
	}

	// Token: 0x14000036 RID: 54
	// (add) Token: 0x06000AB9 RID: 2745 RVA: 0x000514D4 File Offset: 0x0004F6D4
	// (remove) Token: 0x06000ABA RID: 2746 RVA: 0x000514F0 File Offset: 0x0004F6F0
	public event EventHandler SelectedValueChanged;

	// Token: 0x06000ABB RID: 2747 RVA: 0x0005150C File Offset: 0x0004F70C
	public void SetEntrys(IEnumerable<string> theEntrys)
	{
		this.itsEntrys.Clear();
		foreach (string item in theEntrys)
		{
			this.itsEntrys.Add(item);
		}
		if (this.itsEntrys.Count > 0)
		{
			this.itsCurrentSelected = this.itsEntrys[0];
		}
	}

	// Token: 0x06000ABC RID: 2748 RVA: 0x000515A0 File Offset: 0x0004F7A0
	public IEnumerable<string> GetEntrys()
	{
		return this.itsEntrys;
	}

	// Token: 0x06000ABD RID: 2749 RVA: 0x000515A8 File Offset: 0x0004F7A8
	public string SelectedItem()
	{
		return this.itsCurrentSelected;
	}

	// Token: 0x06000ABE RID: 2750 RVA: 0x000515B0 File Offset: 0x0004F7B0
	public void SetSelectedItem(string theValue)
	{
		if (!this.itsEntrys.Contains(theValue))
		{
			return;
		}
		this.itsCurrentSelected = theValue;
		if (this.SelectedValueChanged != null)
		{
			this.SelectedValueChanged(theValue, EventArgs.Empty);
		}
	}

	// Token: 0x06000ABF RID: 2751 RVA: 0x000515E8 File Offset: 0x0004F7E8
	public void Render()
	{
		if ((long)this.itsEntrys.Count <= (long)((ulong)this.itsMaxVisibleItems))
		{
			this.itsHeight = (uint)(this.itsEntrys.Count * (int)((uint)KGFGUIUtility.GetSkinHeight()));
		}
		else
		{
			this.itsHeight = this.itsMaxVisibleItems * (uint)KGFGUIUtility.GetSkinHeight();
		}
		if (this.itsVisible)
		{
			GUILayout.BeginHorizontal(new GUILayoutOption[]
			{
				GUILayout.Width(this.itsWidth)
			});
			KGFGUIUtility.BeginHorizontalBox(KGFGUIUtility.eStyleBox.eBoxLeft, new GUILayoutOption[0]);
			if (this.itsTitle != string.Empty)
			{
				KGFGUIUtility.Label(this.itsTitle, KGFGUIUtility.eStyleLabel.eLabelFitIntoBox, new GUILayoutOption[]
				{
					GUILayout.ExpandWidth(true)
				});
			}
			else
			{
				KGFGUIUtility.Label(this.itsCurrentSelected, KGFGUIUtility.eStyleLabel.eLabelFitIntoBox, new GUILayoutOption[]
				{
					GUILayout.ExpandWidth(true)
				});
			}
			KGFGUIUtility.EndHorizontalBox();
			if (this.itsIcon == null)
			{
				if (KGFGUIUtility.Button("v", KGFGUIUtility.eStyleButton.eButtonRight, new GUILayoutOption[]
				{
					GUILayout.ExpandWidth(false)
				}))
				{
					if (KGFGUIDropDown.itsOpenInstance != this)
					{
						KGFGUIDropDown.itsOpenInstance = this;
						KGFGUIDropDown.itsCorrectedOffset = false;
					}
					else
					{
						KGFGUIDropDown.itsOpenInstance = null;
						KGFGUIDropDown.itsCorrectedOffset = false;
					}
				}
			}
			else if (KGFGUIUtility.Button(this.itsIcon, KGFGUIUtility.eStyleButton.eButtonRight, new GUILayoutOption[]
			{
				GUILayout.ExpandWidth(false)
			}))
			{
				if (KGFGUIDropDown.itsOpenInstance != this)
				{
					KGFGUIDropDown.itsOpenInstance = this;
					KGFGUIDropDown.itsCorrectedOffset = false;
				}
				else
				{
					KGFGUIDropDown.itsOpenInstance = null;
					KGFGUIDropDown.itsCorrectedOffset = false;
				}
			}
			GUILayout.EndHorizontal();
			if (Event.current.type == EventType.Repaint)
			{
				this.itsLastRect = GUILayoutUtility.GetLastRect();
			}
			else
			{
				Vector3 mousePosition = Input.mousePosition;
				mousePosition.y = (float)Screen.height - mousePosition.y;
				if (this.itsLastRect.Contains(mousePosition))
				{
					this.itsHover = true;
				}
				else if (KGFGUIDropDown.itsOpenInstance != this)
				{
					this.itsHover = false;
				}
			}
		}
	}

	// Token: 0x06000AC0 RID: 2752 RVA: 0x000517D8 File Offset: 0x0004F9D8
	public string GetName()
	{
		return "KGFGUIDropDown";
	}

	// Token: 0x06000AC1 RID: 2753 RVA: 0x000517E0 File Offset: 0x0004F9E0
	public bool IsVisible()
	{
		return this.itsVisible;
	}

	// Token: 0x06000AC2 RID: 2754 RVA: 0x000517E8 File Offset: 0x0004F9E8
	public bool GetHover()
	{
		return this.itsHover;
	}

	// Token: 0x04000B13 RID: 2835
	private List<string> itsEntrys = new List<string>();

	// Token: 0x04000B14 RID: 2836
	private GUILayoutOption[] itsLayoutOptions;

	// Token: 0x04000B15 RID: 2837
	private string itsCurrentSelected = string.Empty;

	// Token: 0x04000B16 RID: 2838
	private bool itsVisible = true;

	// Token: 0x04000B17 RID: 2839
	public Vector2 itsScrollPosition = Vector2.zero;

	// Token: 0x04000B18 RID: 2840
	public Rect itsLastRect;

	// Token: 0x04000B19 RID: 2841
	public static KGFGUIDropDown itsOpenInstance;

	// Token: 0x04000B1A RID: 2842
	public uint itsWidth;

	// Token: 0x04000B1B RID: 2843
	public uint itsHeight;

	// Token: 0x04000B1C RID: 2844
	private uint itsMaxVisibleItems = 1U;

	// Token: 0x04000B1D RID: 2845
	public KGFGUIDropDown.eDropDirection itsDirection;

	// Token: 0x04000B1E RID: 2846
	public string itsTitle = string.Empty;

	// Token: 0x04000B1F RID: 2847
	public Texture2D itsIcon;

	// Token: 0x04000B20 RID: 2848
	public bool itsHover;

	// Token: 0x04000B21 RID: 2849
	public static bool itsCorrectedOffset;

	// Token: 0x0200016D RID: 365
	public enum eDropDirection
	{
		// Token: 0x04000B24 RID: 2852
		eAuto,
		// Token: 0x04000B25 RID: 2853
		eDown,
		// Token: 0x04000B26 RID: 2854
		eUp
	}
}
