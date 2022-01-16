using System;
using UnityEngine;

// Token: 0x0200016A RID: 362
public class KGFGUIUtilityTutorial : MonoBehaviour
{
	// Token: 0x06000A8D RID: 2701 RVA: 0x00050654 File Offset: 0x0004E854
	private void OnGUI()
	{
		int num = 300;
		int num2 = 250;
		Rect screenRect = new Rect((float)((Screen.width - num) / 2), (float)((Screen.height - num2) / 2), (float)num, (float)num2);
		GUILayout.BeginArea(screenRect);
		KGFGUIUtility.BeginVerticalBox(KGFGUIUtility.eStyleBox.eBoxInvisible, new GUILayoutOption[]
		{
			GUILayout.ExpandHeight(true)
		});
		KGFGUIUtility.BeginHorizontalBox(KGFGUIUtility.eStyleBox.eBoxDarkTop, new GUILayoutOption[0]);
		GUILayout.FlexibleSpace();
		KGFGUIUtility.Label("KGFGUIUtility Tutorial", KGFGUIUtility.eStyleLabel.eLabel, new GUILayoutOption[0]);
		GUILayout.FlexibleSpace();
		KGFGUIUtility.EndHorizontalBox();
		KGFGUIUtility.BeginVerticalBox(KGFGUIUtility.eStyleBox.eBoxMiddleVertical, new GUILayoutOption[]
		{
			GUILayout.ExpandHeight(true)
		});
		GUILayout.FlexibleSpace();
		KGFGUIUtility.BeginHorizontalPadding();
		KGFGUIUtility.Button("Top", KGFGUIUtility.eStyleButton.eButtonTop, new GUILayoutOption[]
		{
			GUILayout.ExpandWidth(true)
		});
		KGFGUIUtility.Button("Middle", KGFGUIUtility.eStyleButton.eButtonMiddle, new GUILayoutOption[]
		{
			GUILayout.ExpandWidth(true)
		});
		KGFGUIUtility.Button("Bottom", KGFGUIUtility.eStyleButton.eButtonBottom, new GUILayoutOption[]
		{
			GUILayout.ExpandWidth(true)
		});
		KGFGUIUtility.EndHorizontalPadding();
		GUILayout.FlexibleSpace();
		KGFGUIUtility.EndVerticalBox();
		KGFGUIUtility.BeginHorizontalBox(KGFGUIUtility.eStyleBox.eBoxDarkBottom, new GUILayoutOption[0]);
		KGFGUIUtility.BeginVerticalPadding();
		KGFGUIUtility.Button("Left", KGFGUIUtility.eStyleButton.eButtonLeft, new GUILayoutOption[]
		{
			GUILayout.ExpandWidth(true)
		});
		KGFGUIUtility.Button("Center", KGFGUIUtility.eStyleButton.eButtonMiddle, new GUILayoutOption[]
		{
			GUILayout.ExpandWidth(true)
		});
		KGFGUIUtility.Button("Right", KGFGUIUtility.eStyleButton.eButtonRight, new GUILayoutOption[]
		{
			GUILayout.ExpandWidth(true)
		});
		KGFGUIUtility.EndVerticalPadding();
		KGFGUIUtility.EndHorizontalBox();
		KGFGUIUtility.EndVerticalBox();
		GUILayout.EndArea();
	}
}
