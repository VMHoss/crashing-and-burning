using System;
using UnityEngine;

// Token: 0x02000178 RID: 376
public abstract class KGFModule : KGFObject, KGFIValidator
{
	// Token: 0x06000B2E RID: 2862 RVA: 0x000543BC File Offset: 0x000525BC
	public KGFModule(Version theCurrentVersion, Version theMinimumCoreVersion)
	{
		this.itsCurrentVersion = theCurrentVersion;
		this.itsMinimumCoreVersion = theMinimumCoreVersion;
		if (KGFCoreVersion.GetCurrentVersion() < this.itsMinimumCoreVersion)
		{
			Debug.LogError("the KGFCore verison installed in this scene is older than the required version. please update the KGFCore to the latest version");
		}
	}

	// Token: 0x06000B30 RID: 2864 RVA: 0x000543F8 File Offset: 0x000525F8
	public Version GetCurrentVersion()
	{
		return this.itsCurrentVersion.Clone() as Version;
	}

	// Token: 0x06000B31 RID: 2865 RVA: 0x0005440C File Offset: 0x0005260C
	public Version GetRequiredCoreVersion()
	{
		return this.itsMinimumCoreVersion.Clone() as Version;
	}

	// Token: 0x06000B32 RID: 2866
	public abstract string GetName();

	// Token: 0x06000B33 RID: 2867
	public abstract Texture2D GetIcon();

	// Token: 0x06000B34 RID: 2868
	public abstract string GetDocumentationPath();

	// Token: 0x06000B35 RID: 2869
	public abstract string GetForumPath();

	// Token: 0x06000B36 RID: 2870
	public abstract KGFMessageList Validate();

	// Token: 0x06000B37 RID: 2871 RVA: 0x00054420 File Offset: 0x00052620
	public static void OpenHelpWindow(KGFModule theModule)
	{
		KGFModule.itsOpenModule = theModule;
	}

	// Token: 0x06000B38 RID: 2872 RVA: 0x00054428 File Offset: 0x00052628
	public static void RenderHelpWindow()
	{
		if (KGFModule.itsOpenModule != null)
		{
			int num = 512 + (int)KGFGUIUtility.GetSkinHeight() * 2;
			int num2 = 256 + (int)KGFGUIUtility.GetSkinHeight() * 7;
			Rect theRect = new Rect((float)((Screen.width - num) / 2), (float)((Screen.height - num2) / 2), (float)num, (float)num2);
			KGFGUIUtility.Window(12345689, theRect, new GUI.WindowFunction(KGFModule.RenderHelpWindowMethod), KGFModule.itsOpenModule.GetName() + " (part of KOLMICH Game Framework)", new GUILayoutOption[0]);
			if (theRect.Contains(Event.current.mousePosition) && Event.current.type == EventType.MouseDown && Event.current.button == 0)
			{
				KGFModule.itsOpenModule = null;
			}
		}
		else
		{
			KGFModule.itsOpenModule = null;
		}
	}

	// Token: 0x06000B39 RID: 2873 RVA: 0x000544FC File Offset: 0x000526FC
	private static void RenderHelpWindowMethod(int theWindowID)
	{
		GUILayout.BeginHorizontal(new GUILayoutOption[0]);
		GUILayout.FlexibleSpace();
		KGFGUIUtility.BeginVerticalBox(KGFGUIUtility.eStyleBox.eBoxInvisible, new GUILayoutOption[]
		{
			GUILayout.ExpandHeight(true)
		});
		KGFGUIUtility.BeginHorizontalPadding();
		KGFGUIUtility.BeginHorizontalBox(KGFGUIUtility.eStyleBox.eBoxDarkTop, new GUILayoutOption[]
		{
			GUILayout.ExpandWidth(true)
		});
		GUILayout.FlexibleSpace();
		GUILayout.Label(KGFGUIUtility.GetLogo(), new GUILayoutOption[]
		{
			GUILayout.Height(50f)
		});
		GUILayout.FlexibleSpace();
		KGFGUIUtility.EndHorizontalBox();
		KGFGUIUtility.BeginHorizontalBox(KGFGUIUtility.eStyleBox.eBoxBottom, new GUILayoutOption[]
		{
			GUILayout.ExpandWidth(true),
			GUILayout.ExpandHeight(true)
		});
		GUILayout.Label("KOLMICH Creations e.U. is a small company based out of Vienna, Austria.\nWhile developing cool unity3d projects we put an immense amount of time \nto create professional tools and professional content. \n\n\nIf you have any ideas on improvements or you just want to give us some feedback use one of the links below.", new GUILayoutOption[]
		{
			GUILayout.ExpandWidth(true)
		});
		KGFGUIUtility.EndHorizontalBox();
		GUILayout.Space(KGFGUIUtility.GetSkinHeight());
		KGFGUIUtility.BeginHorizontalBox(KGFGUIUtility.eStyleBox.eBoxDarkTop, new GUILayoutOption[]
		{
			GUILayout.ExpandWidth(true)
		});
		KGFGUIUtility.Label(KGFModule.itsOpenModule.GetName() + " version:", KGFGUIUtility.eStyleLabel.eLabelFitIntoBox, new GUILayoutOption[0]);
		KGFGUIUtility.Label(KGFModule.itsOpenModule.GetCurrentVersion().ToString(), KGFGUIUtility.eStyleLabel.eLabelFitIntoBox, new GUILayoutOption[0]);
		GUILayout.FlexibleSpace();
		KGFGUIUtility.Label("req. KGFCore version:", KGFGUIUtility.eStyleLabel.eLabelFitIntoBox, new GUILayoutOption[0]);
		KGFGUIUtility.Label(KGFModule.itsOpenModule.GetRequiredCoreVersion().ToString(), KGFGUIUtility.eStyleLabel.eLabelFitIntoBox, new GUILayoutOption[0]);
		KGFGUIUtility.EndHorizontalBox();
		KGFGUIUtility.BeginHorizontalBox(KGFGUIUtility.eStyleBox.eBoxDarkBottom, new GUILayoutOption[]
		{
			GUILayout.ExpandWidth(true)
		});
		KGFGUIUtility.BeginVerticalPadding();
		if (KGFGUIUtility.Button(KGFGUIUtility.GetHelpIcon(), "documentation", KGFGUIUtility.eStyleButton.eButtonLeft, new GUILayoutOption[]
		{
			GUILayout.ExpandWidth(true)
		}))
		{
			Application.OpenURL("http://www.kolmich.at/documentation/" + KGFModule.itsOpenModule.GetDocumentationPath());
			KGFModule.itsOpenModule = null;
		}
		if (KGFGUIUtility.Button(KGFGUIUtility.GetHelpIcon(), "forum", KGFGUIUtility.eStyleButton.eButtonMiddle, new GUILayoutOption[]
		{
			GUILayout.ExpandWidth(true)
		}))
		{
			Application.OpenURL("http://www.kolmich.at/forum/" + KGFModule.itsOpenModule.GetForumPath());
			KGFModule.itsOpenModule = null;
		}
		if (KGFGUIUtility.Button(KGFGUIUtility.GetHelpIcon(), "homepage", KGFGUIUtility.eStyleButton.eButtonRight, new GUILayoutOption[]
		{
			GUILayout.ExpandWidth(true)
		}))
		{
			Application.OpenURL("http://www.kolmich.at");
			KGFModule.itsOpenModule = null;
		}
		KGFGUIUtility.EndVerticalPadding();
		KGFGUIUtility.EndHorizontalBox();
		KGFGUIUtility.EndHorizontalPadding();
		KGFGUIUtility.EndVerticalBox();
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();
	}

	// Token: 0x04000B65 RID: 2917
	private const string itsCopyrightText = "KOLMICH Creations e.U. is a small company based out of Vienna, Austria.\nWhile developing cool unity3d projects we put an immense amount of time \nto create professional tools and professional content. \n\n\nIf you have any ideas on improvements or you just want to give us some feedback use one of the links below.";

	// Token: 0x04000B66 RID: 2918
	private Version itsCurrentVersion;

	// Token: 0x04000B67 RID: 2919
	private Version itsMinimumCoreVersion;

	// Token: 0x04000B68 RID: 2920
	private static KGFModule itsOpenModule;
}
