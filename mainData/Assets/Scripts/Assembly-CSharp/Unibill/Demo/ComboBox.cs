using System;
using UnityEngine;

namespace Unibill.Demo
{
	// Token: 0x020001AF RID: 431
	public class ComboBox
	{
		// Token: 0x06000CD1 RID: 3281 RVA: 0x0005F144 File Offset: 0x0005D344
		public int List(Rect rect, string buttonText, GUIContent[] listContent, GUIStyle listStyle)
		{
			return this.List(rect, new GUIContent(buttonText), listContent, "button", "box", listStyle);
		}

		// Token: 0x06000CD2 RID: 3282 RVA: 0x0005F178 File Offset: 0x0005D378
		public int List(Rect rect, GUIContent buttonContent, GUIContent[] listContent, GUIStyle listStyle)
		{
			return this.List(rect, buttonContent, listContent, "button", "box", listStyle);
		}

		// Token: 0x06000CD3 RID: 3283 RVA: 0x0005F1A4 File Offset: 0x0005D3A4
		public int List(Rect rect, string buttonText, GUIContent[] listContent, GUIStyle buttonStyle, GUIStyle boxStyle, GUIStyle listStyle)
		{
			return this.List(rect, new GUIContent(buttonText), listContent, buttonStyle, boxStyle, listStyle);
		}

		// Token: 0x06000CD4 RID: 3284 RVA: 0x0005F1BC File Offset: 0x0005D3BC
		public int List(Rect rect, GUIContent buttonContent, GUIContent[] listContent, GUIStyle buttonStyle, GUIStyle boxStyle, GUIStyle listStyle)
		{
			if (ComboBox.forceToUnShow)
			{
				ComboBox.forceToUnShow = false;
				this.isClickedComboButton = false;
			}
			bool flag = false;
			int controlID = GUIUtility.GetControlID(FocusType.Passive);
			EventType typeForControl = Event.current.GetTypeForControl(controlID);
			if (typeForControl == EventType.MouseUp)
			{
				if (this.isClickedComboButton)
				{
					flag = true;
				}
			}
			if (GUI.Button(rect, buttonContent, buttonStyle))
			{
				if (ComboBox.useControlID == -1)
				{
					ComboBox.useControlID = controlID;
					this.isClickedComboButton = false;
				}
				if (ComboBox.useControlID != controlID)
				{
					ComboBox.forceToUnShow = true;
					ComboBox.useControlID = controlID;
				}
				this.isClickedComboButton = true;
			}
			if (this.isClickedComboButton)
			{
				Rect position = new Rect(rect.x, rect.y + listStyle.CalcHeight(listContent[0], 1f), rect.width, listStyle.CalcHeight(listContent[0], 1f) * (float)listContent.Length);
				GUI.Box(position, string.Empty, boxStyle);
				int num = GUI.SelectionGrid(position, this.selectedItemIndex, listContent, 1, listStyle);
				if (num != this.selectedItemIndex)
				{
					this.selectedItemIndex = num;
				}
			}
			if (flag)
			{
				this.isClickedComboButton = false;
			}
			return this.GetSelectedItemIndex();
		}

		// Token: 0x06000CD5 RID: 3285 RVA: 0x0005F2E8 File Offset: 0x0005D4E8
		public int GetSelectedItemIndex()
		{
			return this.selectedItemIndex;
		}

		// Token: 0x04000C91 RID: 3217
		private static bool forceToUnShow;

		// Token: 0x04000C92 RID: 3218
		private static int useControlID = -1;

		// Token: 0x04000C93 RID: 3219
		private bool isClickedComboButton;

		// Token: 0x04000C94 RID: 3220
		private int selectedItemIndex;
	}
}
