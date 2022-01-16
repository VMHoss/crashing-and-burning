using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000152 RID: 338
public class KGFCustomGUI : KGFModule
{
	// Token: 0x060009BB RID: 2491 RVA: 0x0004BD4C File Offset: 0x00049F4C
	public KGFCustomGUI() : base(new Version(1, 0, 0, 1), new Version(1, 0, 0, 0))
	{
	}

	// Token: 0x060009BD RID: 2493 RVA: 0x0004BDB4 File Offset: 0x00049FB4
	public static Rect GetItsWindowRectangle()
	{
		return KGFCustomGUI.itsWindowRectangle;
	}

	// Token: 0x060009BE RID: 2494 RVA: 0x0004BDBC File Offset: 0x00049FBC
	protected override void KGFAwake()
	{
		base.KGFAwake();
		if (KGFCustomGUI.itsInstance == null)
		{
			KGFCustomGUI.itsInstance = this;
		}
		else
		{
			UnityEngine.Object.Destroy(this);
		}
		KGFAccessor.RegisterAddEvent<KGFICustomGUI>(new Action<object, EventArgs>(this.OnCustomGuiChanged));
		KGFAccessor.RegisterRemoveEvent<KGFICustomGUI>(new Action<object, EventArgs>(this.OnCustomGuiChanged));
		this.UpdateInternalList();
	}

	// Token: 0x060009BF RID: 2495 RVA: 0x0004BE18 File Offset: 0x0004A018
	private void OnCustomGuiChanged(object theSender, EventArgs theArgs)
	{
		this.UpdateInternalList();
	}

	// Token: 0x060009C0 RID: 2496 RVA: 0x0004BE20 File Offset: 0x0004A020
	private void UpdateInternalList()
	{
		KGFCustomGUI.itsCustomGuiList = KGFAccessor.GetObjects<KGFICustomGUI>();
	}

	// Token: 0x060009C1 RID: 2497 RVA: 0x0004BE2C File Offset: 0x0004A02C
	protected void OnGUI()
	{
		KGFCustomGUI.Render();
	}

	// Token: 0x060009C2 RID: 2498 RVA: 0x0004BE34 File Offset: 0x0004A034
	protected void Update()
	{
		if ((Input.GetKey(this.itsDataModuleCustomGUI.itsModifierKey) && Input.GetKeyDown(this.itsDataModuleCustomGUI.itsSchortcutKey)) || (this.itsDataModuleCustomGUI.itsModifierKey == KeyCode.None && Input.GetKeyDown(this.itsDataModuleCustomGUI.itsSchortcutKey)))
		{
			this.itsDataModuleCustomGUI.itsBarVisible = !this.itsDataModuleCustomGUI.itsBarVisible;
		}
	}

	// Token: 0x060009C3 RID: 2499 RVA: 0x0004BEAC File Offset: 0x0004A0AC
	public static void Render()
	{
		KGFGUIUtility.SetSkinIndex(0);
		if (KGFCustomGUI.itsInstance != null && KGFCustomGUI.itsInstance.itsDataModuleCustomGUI.itsBarVisible)
		{
			GUIStyle styleToggl = KGFGUIUtility.GetStyleToggl(KGFGUIUtility.eStyleToggl.eTogglRadioStreched);
			GUIStyle styleBox = KGFGUIUtility.GetStyleBox(KGFGUIUtility.eStyleBox.eBoxDecorated);
			int num = (int)(styleToggl.contentOffset.x + (float)styleToggl.padding.horizontal + (KGFGUIUtility.GetSkinHeight() - (float)styleToggl.padding.vertical));
			int num2 = (int)((float)(styleBox.margin.top + styleBox.margin.bottom + styleBox.padding.top + styleBox.padding.bottom) + (styleToggl.fixedHeight + (float)styleToggl.margin.top) * (float)KGFCustomGUI.itsCustomGuiList.Count);
			GUILayout.BeginArea(new Rect((float)(Screen.width - num), (float)((Screen.height - num2) / 2), (float)num, (float)num2));
			KGFGUIUtility.BeginVerticalBox(KGFGUIUtility.eStyleBox.eBoxDecorated, new GUILayoutOption[]
			{
				GUILayout.ExpandWidth(true),
				GUILayout.ExpandHeight(true)
			});
			GUILayout.FlexibleSpace();
			foreach (KGFICustomGUI kgficustomGUI in KGFCustomGUI.itsCustomGuiList)
			{
				bool flag = KGFCustomGUI.itsCurrentSelectedGUI != null && KGFCustomGUI.itsCurrentSelectedGUI == kgficustomGUI;
				Texture2D texture2D = kgficustomGUI.GetIcon();
				if (texture2D == null)
				{
					texture2D = KGFCustomGUI.itsInstance.itsDataModuleCustomGUI.itsUnknownIcon;
				}
				if (flag != KGFGUIUtility.Toggle(flag, texture2D, KGFGUIUtility.eStyleToggl.eTogglRadioStreched, new GUILayoutOption[0]))
				{
					if (flag)
					{
						KGFCustomGUI.itsCurrentSelectedGUI = null;
					}
					else
					{
						KGFCustomGUI.itsCurrentSelectedGUI = kgficustomGUI;
					}
				}
			}
			GUILayout.FlexibleSpace();
			KGFGUIUtility.EndVerticalBox();
			GUILayout.EndArea();
			KGFCustomGUI.itsInstance.DrawCurrentCustomGUI((float)num);
		}
		KGFGUIUtility.SetSkinIndex(1);
	}

	// Token: 0x060009C4 RID: 2500 RVA: 0x0004C0A8 File Offset: 0x0004A2A8
	private static KGFCustomGUI GetInstance()
	{
		return KGFCustomGUI.itsInstance;
	}

	// Token: 0x060009C5 RID: 2501 RVA: 0x0004C0B0 File Offset: 0x0004A2B0
	private void DrawCurrentCustomGUI(float aCustomGuiWidth)
	{
		if (KGFCustomGUI.itsCurrentSelectedGUI == null)
		{
			return;
		}
		float num = KGFGUIUtility.GetSkinHeight() + (float)KGFGUIUtility.GetStyleButton(KGFGUIUtility.eStyleButton.eButton).margin.vertical + (float)KGFGUIUtility.GetStyleBox(KGFGUIUtility.eStyleBox.eBoxDecorated).padding.vertical;
		GUILayout.BeginArea(new Rect(num, num, (float)Screen.width - aCustomGuiWidth - num, (float)Screen.height - num * 2f));
		KGFGUIUtility.BeginVerticalBox(KGFGUIUtility.eStyleBox.eBox, new GUILayoutOption[0]);
		if (KGFCustomGUI.itsCurrentSelectedGUI.GetIcon() == null)
		{
			KGFGUIUtility.BeginWindowHeader(KGFCustomGUI.itsCurrentSelectedGUI.GetHeaderName(), this.itsDataModuleCustomGUI.itsUnknownIcon);
		}
		else
		{
			KGFGUIUtility.BeginWindowHeader(KGFCustomGUI.itsCurrentSelectedGUI.GetHeaderName(), KGFCustomGUI.itsCurrentSelectedGUI.GetIcon());
		}
		GUILayout.FlexibleSpace();
		if (!KGFGUIUtility.EndWindowHeader(true))
		{
			GUILayout.Space(0f);
			KGFCustomGUI.itsCurrentSelectedGUI.Render();
		}
		else
		{
			KGFCustomGUI.itsCurrentSelectedGUI = null;
		}
		KGFGUIUtility.EndVerticalBox();
		GUILayout.EndArea();
	}

	// Token: 0x060009C6 RID: 2502 RVA: 0x0004C1AC File Offset: 0x0004A3AC
	public static Texture2D GetDefaultIcon()
	{
		if (KGFCustomGUI.itsInstance != null)
		{
			return KGFCustomGUI.itsInstance.itsDataModuleCustomGUI.itsUnknownIcon;
		}
		return null;
	}

	// Token: 0x060009C7 RID: 2503 RVA: 0x0004C1D0 File Offset: 0x0004A3D0
	public override Texture2D GetIcon()
	{
		return null;
	}

	// Token: 0x060009C8 RID: 2504 RVA: 0x0004C1D4 File Offset: 0x0004A3D4
	public override string GetName()
	{
		return "KGFCustomGUI";
	}

	// Token: 0x060009C9 RID: 2505 RVA: 0x0004C1DC File Offset: 0x0004A3DC
	public override string GetDocumentationPath()
	{
		return "KGFCustomGUIManual.html";
	}

	// Token: 0x060009CA RID: 2506 RVA: 0x0004C1E4 File Offset: 0x0004A3E4
	public override string GetForumPath()
	{
		return string.Empty;
	}

	// Token: 0x060009CB RID: 2507 RVA: 0x0004C1EC File Offset: 0x0004A3EC
	public override KGFMessageList Validate()
	{
		KGFMessageList kgfmessageList = new KGFMessageList();
		if (this.itsDataModuleCustomGUI.itsUnknownIcon == null)
		{
			kgfmessageList.AddWarning("the unknown icon is missing");
		}
		if (this.itsDataModuleCustomGUI.itsModifierKey == this.itsDataModuleCustomGUI.itsSchortcutKey)
		{
			kgfmessageList.AddInfo("the modifier key is equal to the shortcut key");
		}
		return kgfmessageList;
	}

	// Token: 0x04000A21 RID: 2593
	private static KGFCustomGUI itsInstance = null;

	// Token: 0x04000A22 RID: 2594
	public KGFCustomGUI.KGFDataCustomGUI itsDataModuleCustomGUI = new KGFCustomGUI.KGFDataCustomGUI();

	// Token: 0x04000A23 RID: 2595
	private static List<KGFICustomGUI> itsCustomGuiList = null;

	// Token: 0x04000A24 RID: 2596
	private static KGFICustomGUI itsCurrentSelectedGUI = null;

	// Token: 0x04000A25 RID: 2597
	private static Rect itsWindowRectangle = new Rect(50f, 50f, 800f, 600f);

	// Token: 0x02000153 RID: 339
	[Serializable]
	public class KGFDataCustomGUI
	{
		// Token: 0x04000A26 RID: 2598
		public Texture2D itsUnknownIcon;

		// Token: 0x04000A27 RID: 2599
		public KeyCode itsModifierKey;

		// Token: 0x04000A28 RID: 2600
		public KeyCode itsSchortcutKey = KeyCode.F3;

		// Token: 0x04000A29 RID: 2601
		public bool itsBarVisible = true;
	}
}
