using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000161 RID: 353
public class KGFGUIUtility
{
	// Token: 0x170000ED RID: 237
	// (get) Token: 0x06000A31 RID: 2609 RVA: 0x0004DCA4 File Offset: 0x0004BEA4
	public static Color itsEditorColorContent
	{
		get
		{
			return new Color(0.1f, 0.1f, 0.1f);
		}
	}

	// Token: 0x170000EE RID: 238
	// (get) Token: 0x06000A32 RID: 2610 RVA: 0x0004DCBC File Offset: 0x0004BEBC
	public static Color itsEditorColorTitle
	{
		get
		{
			return new Color(0.1f, 0.1f, 0.1f);
		}
	}

	// Token: 0x170000EF RID: 239
	// (get) Token: 0x06000A33 RID: 2611 RVA: 0x0004DCD4 File Offset: 0x0004BED4
	public static Color itsEditorDocumentation
	{
		get
		{
			return new Color(0.74f, 0.79f, 0.64f);
		}
	}

	// Token: 0x170000F0 RID: 240
	// (get) Token: 0x06000A34 RID: 2612 RVA: 0x0004DCEC File Offset: 0x0004BEEC
	public static Color itsEditorColorDefault
	{
		get
		{
			return new Color(1f, 1f, 1f);
		}
	}

	// Token: 0x170000F1 RID: 241
	// (get) Token: 0x06000A35 RID: 2613 RVA: 0x0004DD04 File Offset: 0x0004BF04
	public static Color itsEditorColorInfo
	{
		get
		{
			return new Color(1f, 1f, 1f);
		}
	}

	// Token: 0x170000F2 RID: 242
	// (get) Token: 0x06000A36 RID: 2614 RVA: 0x0004DD1C File Offset: 0x0004BF1C
	public static Color itsEditorColorWarning
	{
		get
		{
			return new Color(1f, 1f, 0f);
		}
	}

	// Token: 0x170000F3 RID: 243
	// (get) Token: 0x06000A37 RID: 2615 RVA: 0x0004DD34 File Offset: 0x0004BF34
	public static Color itsEditorColorError
	{
		get
		{
			return new Color(0.9f, 0.5f, 0.5f);
		}
	}

	// Token: 0x06000A38 RID: 2616 RVA: 0x0004DD4C File Offset: 0x0004BF4C
	public static int GetSkinIndex()
	{
		return KGFGUIUtility.itsSkinIndex;
	}

	// Token: 0x06000A39 RID: 2617 RVA: 0x0004DD54 File Offset: 0x0004BF54
	public static float GetSkinHeight()
	{
		if (KGFGUIUtility.itsSkinIndex == -1)
		{
			return 16f;
		}
		if (KGFGUIUtility.itsStyleButton != null && KGFGUIUtility.itsSkinIndex < KGFGUIUtility.itsStyleButton.Length && KGFGUIUtility.itsStyleButton[KGFGUIUtility.itsSkinIndex] != null)
		{
			return KGFGUIUtility.itsStyleButton[KGFGUIUtility.itsSkinIndex].fixedHeight;
		}
		return 16f;
	}

	// Token: 0x06000A3A RID: 2618 RVA: 0x0004DDB4 File Offset: 0x0004BFB4
	public static GUISkin GetSkin()
	{
		if (KGFGUIUtility.itsSkin[KGFGUIUtility.itsSkinIndex] != null)
		{
			return KGFGUIUtility.itsSkin[KGFGUIUtility.itsSkinIndex];
		}
		return null;
	}

	// Token: 0x06000A3B RID: 2619 RVA: 0x0004DDDC File Offset: 0x0004BFDC
	public static Texture2D GetLogo()
	{
		if (KGFGUIUtility.itsIcon == null)
		{
			KGFGUIUtility.itsIcon = (Resources.Load("KGFCore/textures/logo") as Texture2D);
		}
		return KGFGUIUtility.itsIcon;
	}

	// Token: 0x06000A3C RID: 2620 RVA: 0x0004DE08 File Offset: 0x0004C008
	public static Texture2D GetHelpIcon()
	{
		if (KGFGUIUtility.itsIconHelp == null)
		{
			KGFGUIUtility.itsIconHelp = (Resources.Load("KGFCore/textures/help") as Texture2D);
		}
		return KGFGUIUtility.itsIconHelp;
	}

	// Token: 0x06000A3D RID: 2621 RVA: 0x0004DE34 File Offset: 0x0004C034
	public static Texture2D GetKGFCopyright()
	{
		if (KGFGUIUtility.itsKGFCopyright == null)
		{
			KGFGUIUtility.itsKGFCopyright = (Resources.Load("KGFCore/textures/kgf_copyright_512x256") as Texture2D);
		}
		return KGFGUIUtility.itsKGFCopyright;
	}

	// Token: 0x06000A3E RID: 2622 RVA: 0x0004DE60 File Offset: 0x0004C060
	public static GUIStyle GetStyleToggl(KGFGUIUtility.eStyleToggl theTogglStyle)
	{
		if (KGFGUIUtility.itsSkinIndex == -1)
		{
			return GUI.skin.toggle;
		}
		KGFGUIUtility.Init();
		if (theTogglStyle == KGFGUIUtility.eStyleToggl.eTogglStreched && KGFGUIUtility.itsStyleToggleStreched[KGFGUIUtility.itsSkinIndex] != null)
		{
			return KGFGUIUtility.itsStyleToggleStreched[KGFGUIUtility.itsSkinIndex];
		}
		if (theTogglStyle == KGFGUIUtility.eStyleToggl.eTogglCompact && KGFGUIUtility.itsStyleToggleCompact[KGFGUIUtility.itsSkinIndex] != null)
		{
			return KGFGUIUtility.itsStyleToggleCompact[KGFGUIUtility.itsSkinIndex];
		}
		if (theTogglStyle == KGFGUIUtility.eStyleToggl.eTogglSuperCompact && KGFGUIUtility.itsStyleToggleSuperCompact[KGFGUIUtility.itsSkinIndex] != null)
		{
			return KGFGUIUtility.itsStyleToggleSuperCompact[KGFGUIUtility.itsSkinIndex];
		}
		if (theTogglStyle == KGFGUIUtility.eStyleToggl.eTogglRadioStreched && KGFGUIUtility.itsStyleToggleRadioStreched[KGFGUIUtility.itsSkinIndex] != null)
		{
			return KGFGUIUtility.itsStyleToggleRadioStreched[KGFGUIUtility.itsSkinIndex];
		}
		if (theTogglStyle == KGFGUIUtility.eStyleToggl.eTogglRadioCompact && KGFGUIUtility.itsStyleToggleRadioCompact[KGFGUIUtility.itsSkinIndex] != null)
		{
			return KGFGUIUtility.itsStyleToggleRadioCompact[KGFGUIUtility.itsSkinIndex];
		}
		if (theTogglStyle == KGFGUIUtility.eStyleToggl.eTogglRadioSuperCompact && KGFGUIUtility.itsStyleToggleRadioSuperCompact[KGFGUIUtility.itsSkinIndex] != null)
		{
			return KGFGUIUtility.itsStyleToggleRadioSuperCompact[KGFGUIUtility.itsSkinIndex];
		}
		if (theTogglStyle == KGFGUIUtility.eStyleToggl.eTogglSwitch && KGFGUIUtility.itsStyleToggleSwitch[KGFGUIUtility.itsSkinIndex] != null)
		{
			return KGFGUIUtility.itsStyleToggleSwitch[KGFGUIUtility.itsSkinIndex];
		}
		if (theTogglStyle == KGFGUIUtility.eStyleToggl.eTogglBoolean && KGFGUIUtility.itsStyleToggleBoolean[KGFGUIUtility.itsSkinIndex] != null)
		{
			return KGFGUIUtility.itsStyleToggleBoolean[KGFGUIUtility.itsSkinIndex];
		}
		if (theTogglStyle == KGFGUIUtility.eStyleToggl.eTogglArrow && KGFGUIUtility.itsStyleToggleArrow[KGFGUIUtility.itsSkinIndex] != null)
		{
			return KGFGUIUtility.itsStyleToggleArrow[KGFGUIUtility.itsSkinIndex];
		}
		if (theTogglStyle == KGFGUIUtility.eStyleToggl.eTogglButton && KGFGUIUtility.itsStyleToggleButton[KGFGUIUtility.itsSkinIndex] != null)
		{
			return KGFGUIUtility.itsStyleToggleButton[KGFGUIUtility.itsSkinIndex];
		}
		if (KGFGUIUtility.itsStyleToggle[KGFGUIUtility.itsSkinIndex] != null)
		{
			return KGFGUIUtility.itsStyleToggle[KGFGUIUtility.itsSkinIndex];
		}
		return GUI.skin.toggle;
	}

	// Token: 0x06000A3F RID: 2623 RVA: 0x0004E010 File Offset: 0x0004C210
	public static GUIStyle GetStyleTextField(KGFGUIUtility.eStyleTextField theStyleTextField)
	{
		if (KGFGUIUtility.itsSkinIndex == -1)
		{
			return GUI.skin.textField;
		}
		KGFGUIUtility.Init();
		if (theStyleTextField == KGFGUIUtility.eStyleTextField.eTextField && KGFGUIUtility.itsStyleTextField[KGFGUIUtility.itsSkinIndex] != null)
		{
			return KGFGUIUtility.itsStyleTextField[KGFGUIUtility.itsSkinIndex];
		}
		if (theStyleTextField == KGFGUIUtility.eStyleTextField.eTextFieldLeft && KGFGUIUtility.itsStyleTextFieldLeft[KGFGUIUtility.itsSkinIndex] != null)
		{
			return KGFGUIUtility.itsStyleTextFieldLeft[KGFGUIUtility.itsSkinIndex];
		}
		if (theStyleTextField == KGFGUIUtility.eStyleTextField.eTextFieldRight && KGFGUIUtility.itsStyleTextFieldRight[KGFGUIUtility.itsSkinIndex] != null)
		{
			return KGFGUIUtility.itsStyleTextFieldRight[KGFGUIUtility.itsSkinIndex];
		}
		return GUI.skin.textField;
	}

	// Token: 0x06000A40 RID: 2624 RVA: 0x0004E0AC File Offset: 0x0004C2AC
	public static GUIStyle GetStyleTextArea()
	{
		if (KGFGUIUtility.itsSkinIndex == -1)
		{
			return GUI.skin.textArea;
		}
		KGFGUIUtility.Init();
		if (KGFGUIUtility.itsStyleTextArea != null)
		{
			return KGFGUIUtility.itsStyleTextArea[KGFGUIUtility.itsSkinIndex];
		}
		return GUI.skin.textArea;
	}

	// Token: 0x06000A41 RID: 2625 RVA: 0x0004E0F4 File Offset: 0x0004C2F4
	public static GUIStyle GetStyleHorizontalSlider()
	{
		if (KGFGUIUtility.itsSkinIndex == -1)
		{
			return GUI.skin.horizontalSlider;
		}
		KGFGUIUtility.Init();
		if (KGFGUIUtility.itsStyleHorizontalSlider[KGFGUIUtility.itsSkinIndex] != null)
		{
			return KGFGUIUtility.itsStyleHorizontalSlider[KGFGUIUtility.itsSkinIndex];
		}
		return GUI.skin.horizontalSlider;
	}

	// Token: 0x06000A42 RID: 2626 RVA: 0x0004E144 File Offset: 0x0004C344
	public static GUIStyle GetStyleHorizontalSliderThumb()
	{
		if (KGFGUIUtility.itsSkinIndex == -1)
		{
			return GUI.skin.horizontalSliderThumb;
		}
		KGFGUIUtility.Init();
		if (KGFGUIUtility.itsStyleHorizontalSliderThumb[KGFGUIUtility.itsSkinIndex] != null)
		{
			return KGFGUIUtility.itsStyleHorizontalSliderThumb[KGFGUIUtility.itsSkinIndex];
		}
		return GUI.skin.horizontalSliderThumb;
	}

	// Token: 0x06000A43 RID: 2627 RVA: 0x0004E194 File Offset: 0x0004C394
	public static GUIStyle GetStyleHorizontalScrollbar()
	{
		if (KGFGUIUtility.itsSkinIndex == -1)
		{
			return GUI.skin.horizontalScrollbar;
		}
		KGFGUIUtility.Init();
		if (KGFGUIUtility.itsStyleHorizontalScrollbar[KGFGUIUtility.itsSkinIndex] != null)
		{
			return KGFGUIUtility.itsStyleHorizontalScrollbar[KGFGUIUtility.itsSkinIndex];
		}
		return GUI.skin.horizontalScrollbar;
	}

	// Token: 0x06000A44 RID: 2628 RVA: 0x0004E1E4 File Offset: 0x0004C3E4
	public static GUIStyle GetStyleHorizontalScrollbarThumb()
	{
		if (KGFGUIUtility.itsSkinIndex == -1)
		{
			return GUI.skin.horizontalScrollbarThumb;
		}
		KGFGUIUtility.Init();
		if (KGFGUIUtility.itsStyleHorizontalScrollbarThumb[KGFGUIUtility.itsSkinIndex] != null)
		{
			return KGFGUIUtility.itsStyleHorizontalScrollbarThumb[KGFGUIUtility.itsSkinIndex];
		}
		return GUI.skin.horizontalScrollbarThumb;
	}

	// Token: 0x06000A45 RID: 2629 RVA: 0x0004E234 File Offset: 0x0004C434
	public static GUIStyle GetStyleHorizontalScrollbarLeftButton()
	{
		if (KGFGUIUtility.itsSkinIndex == -1)
		{
			return GUI.skin.horizontalScrollbarLeftButton;
		}
		KGFGUIUtility.Init();
		if (KGFGUIUtility.itsStyleHorizontalScrollbarLeftButton[KGFGUIUtility.itsSkinIndex] != null)
		{
			return KGFGUIUtility.itsStyleHorizontalScrollbarLeftButton[KGFGUIUtility.itsSkinIndex];
		}
		return GUI.skin.horizontalScrollbarLeftButton;
	}

	// Token: 0x06000A46 RID: 2630 RVA: 0x0004E284 File Offset: 0x0004C484
	public static GUIStyle GetStyleHorizontalScrollbarRightButton()
	{
		if (KGFGUIUtility.itsSkinIndex == -1)
		{
			return GUI.skin.horizontalScrollbarRightButton;
		}
		KGFGUIUtility.Init();
		if (KGFGUIUtility.itsStyleHorizontalScrollbarRightButton[KGFGUIUtility.itsSkinIndex] != null)
		{
			return KGFGUIUtility.itsStyleHorizontalScrollbarRightButton[KGFGUIUtility.itsSkinIndex];
		}
		return GUI.skin.horizontalScrollbarRightButton;
	}

	// Token: 0x06000A47 RID: 2631 RVA: 0x0004E2D4 File Offset: 0x0004C4D4
	public static GUIStyle GetStyleVerticalSlider()
	{
		if (KGFGUIUtility.itsSkinIndex == -1)
		{
			return GUI.skin.verticalSlider;
		}
		KGFGUIUtility.Init();
		if (KGFGUIUtility.itsStyleVerticalSlider[KGFGUIUtility.itsSkinIndex] != null)
		{
			return KGFGUIUtility.itsStyleVerticalSlider[KGFGUIUtility.itsSkinIndex];
		}
		return GUI.skin.verticalSlider;
	}

	// Token: 0x06000A48 RID: 2632 RVA: 0x0004E324 File Offset: 0x0004C524
	public static GUIStyle GetStyleVerticalSliderThumb()
	{
		if (KGFGUIUtility.itsSkinIndex == -1)
		{
			return GUI.skin.verticalSliderThumb;
		}
		KGFGUIUtility.Init();
		if (KGFGUIUtility.itsStyleVerticalSliderThumb[KGFGUIUtility.itsSkinIndex] != null)
		{
			return KGFGUIUtility.itsStyleVerticalSliderThumb[KGFGUIUtility.itsSkinIndex];
		}
		return GUI.skin.verticalSliderThumb;
	}

	// Token: 0x06000A49 RID: 2633 RVA: 0x0004E374 File Offset: 0x0004C574
	public static GUIStyle GetStyleVerticalScrollbar()
	{
		if (KGFGUIUtility.itsSkinIndex == -1)
		{
			return GUI.skin.verticalScrollbar;
		}
		KGFGUIUtility.Init();
		if (KGFGUIUtility.itsStyleVerticalScrollbar[KGFGUIUtility.itsSkinIndex] != null)
		{
			return KGFGUIUtility.itsStyleVerticalScrollbar[KGFGUIUtility.itsSkinIndex];
		}
		return GUI.skin.verticalScrollbar;
	}

	// Token: 0x06000A4A RID: 2634 RVA: 0x0004E3C4 File Offset: 0x0004C5C4
	public static GUIStyle GetStyleVerticalScrollbarThumb()
	{
		if (KGFGUIUtility.itsSkinIndex == -1)
		{
			return GUI.skin.verticalScrollbarThumb;
		}
		KGFGUIUtility.Init();
		if (KGFGUIUtility.itsStyleVerticalScrollbarThumb[KGFGUIUtility.itsSkinIndex] != null)
		{
			return KGFGUIUtility.itsStyleVerticalScrollbarThumb[KGFGUIUtility.itsSkinIndex];
		}
		return GUI.skin.verticalScrollbarThumb;
	}

	// Token: 0x06000A4B RID: 2635 RVA: 0x0004E414 File Offset: 0x0004C614
	public static GUIStyle GetStyleVerticalScrollbarUpButton()
	{
		if (KGFGUIUtility.itsSkinIndex == -1)
		{
			return GUI.skin.verticalScrollbarUpButton;
		}
		KGFGUIUtility.Init();
		if (KGFGUIUtility.itsStyleVerticalScrollbarUpButton[KGFGUIUtility.itsSkinIndex] != null)
		{
			return KGFGUIUtility.itsStyleVerticalScrollbarUpButton[KGFGUIUtility.itsSkinIndex];
		}
		return GUI.skin.verticalScrollbarUpButton;
	}

	// Token: 0x06000A4C RID: 2636 RVA: 0x0004E464 File Offset: 0x0004C664
	public static GUIStyle GetStyleVerticalScrollbarDownButton()
	{
		if (KGFGUIUtility.itsSkinIndex == -1)
		{
			return GUI.skin.verticalScrollbarDownButton;
		}
		KGFGUIUtility.Init();
		if (KGFGUIUtility.itsStyleVerticalScrollbarDownButton[KGFGUIUtility.itsSkinIndex] != null)
		{
			return KGFGUIUtility.itsStyleVerticalScrollbarDownButton[KGFGUIUtility.itsSkinIndex];
		}
		return GUI.skin.verticalScrollbarDownButton;
	}

	// Token: 0x06000A4D RID: 2637 RVA: 0x0004E4B4 File Offset: 0x0004C6B4
	public static GUIStyle GetStyleScrollView()
	{
		if (KGFGUIUtility.itsSkinIndex == -1)
		{
			return GUI.skin.scrollView;
		}
		KGFGUIUtility.Init();
		if (KGFGUIUtility.itsStyleScrollView[KGFGUIUtility.itsSkinIndex] != null)
		{
			return KGFGUIUtility.itsStyleScrollView[KGFGUIUtility.itsSkinIndex];
		}
		return GUI.skin.scrollView;
	}

	// Token: 0x06000A4E RID: 2638 RVA: 0x0004E504 File Offset: 0x0004C704
	public static GUIStyle GetStyleMinimapBorder()
	{
		if (KGFGUIUtility.itsSkinIndex == -1)
		{
			return GUI.skin.box;
		}
		KGFGUIUtility.Init();
		if (KGFGUIUtility.itsStyleMinimap[KGFGUIUtility.itsSkinIndex] != null)
		{
			return KGFGUIUtility.itsStyleMinimap[KGFGUIUtility.itsSkinIndex];
		}
		return GUI.skin.box;
	}

	// Token: 0x06000A4F RID: 2639 RVA: 0x0004E554 File Offset: 0x0004C754
	public static GUIStyle GetStyleMinimapButton()
	{
		if (KGFGUIUtility.itsSkinIndex == -1)
		{
			return GUI.skin.box;
		}
		KGFGUIUtility.Init();
		if (KGFGUIUtility.itsStyleMinimapButton[KGFGUIUtility.itsSkinIndex] != null)
		{
			return KGFGUIUtility.itsStyleMinimapButton[KGFGUIUtility.itsSkinIndex];
		}
		return GUI.skin.button;
	}

	// Token: 0x06000A50 RID: 2640 RVA: 0x0004E5A4 File Offset: 0x0004C7A4
	public static GUIStyle GetStyleButton(KGFGUIUtility.eStyleButton theStyleButton)
	{
		if (KGFGUIUtility.itsSkinIndex == -1)
		{
			return GUI.skin.button;
		}
		KGFGUIUtility.Init();
		if (theStyleButton == KGFGUIUtility.eStyleButton.eButton && KGFGUIUtility.itsStyleButton[KGFGUIUtility.itsSkinIndex] != null)
		{
			return KGFGUIUtility.itsStyleButton[KGFGUIUtility.itsSkinIndex];
		}
		if (theStyleButton == KGFGUIUtility.eStyleButton.eButtonLeft && KGFGUIUtility.itsStyleButtonLeft[KGFGUIUtility.itsSkinIndex] != null)
		{
			return KGFGUIUtility.itsStyleButtonLeft[KGFGUIUtility.itsSkinIndex];
		}
		if (theStyleButton == KGFGUIUtility.eStyleButton.eButtonRight && KGFGUIUtility.itsStyleButtonRight[KGFGUIUtility.itsSkinIndex] != null)
		{
			return KGFGUIUtility.itsStyleButtonRight[KGFGUIUtility.itsSkinIndex];
		}
		if (theStyleButton == KGFGUIUtility.eStyleButton.eButtonTop && KGFGUIUtility.itsStyleButtonTop[KGFGUIUtility.itsSkinIndex] != null)
		{
			return KGFGUIUtility.itsStyleButtonTop[KGFGUIUtility.itsSkinIndex];
		}
		if (theStyleButton == KGFGUIUtility.eStyleButton.eButtonBottom && KGFGUIUtility.itsStyleButtonBottom[KGFGUIUtility.itsSkinIndex] != null)
		{
			return KGFGUIUtility.itsStyleButtonBottom[KGFGUIUtility.itsSkinIndex];
		}
		if (theStyleButton == KGFGUIUtility.eStyleButton.eButtonMiddle && KGFGUIUtility.itsStyleButtonMiddle[KGFGUIUtility.itsSkinIndex] != null)
		{
			return KGFGUIUtility.itsStyleButtonMiddle[KGFGUIUtility.itsSkinIndex];
		}
		return GUI.skin.button;
	}

	// Token: 0x06000A51 RID: 2641 RVA: 0x0004E6A8 File Offset: 0x0004C8A8
	public static GUIStyle GetStyleBox(KGFGUIUtility.eStyleBox theStyleBox)
	{
		if (KGFGUIUtility.itsSkinIndex == -1)
		{
			return GUI.skin.box;
		}
		KGFGUIUtility.Init();
		if (theStyleBox == KGFGUIUtility.eStyleBox.eBox && KGFGUIUtility.itsStyleBox[KGFGUIUtility.itsSkinIndex] != null)
		{
			return KGFGUIUtility.itsStyleBox[KGFGUIUtility.itsSkinIndex];
		}
		if (theStyleBox == KGFGUIUtility.eStyleBox.eBoxInvisible && KGFGUIUtility.itsStyleBoxInvisible[KGFGUIUtility.itsSkinIndex] != null)
		{
			return KGFGUIUtility.itsStyleBoxInvisible[KGFGUIUtility.itsSkinIndex];
		}
		if (theStyleBox == KGFGUIUtility.eStyleBox.eBoxInteractive && KGFGUIUtility.itsStyleBox[KGFGUIUtility.itsSkinIndex] != null)
		{
			return KGFGUIUtility.itsStyleBoxInteractive[KGFGUIUtility.itsSkinIndex];
		}
		if (theStyleBox == KGFGUIUtility.eStyleBox.eBoxLeft && KGFGUIUtility.itsStyleBoxLeft[KGFGUIUtility.itsSkinIndex] != null)
		{
			return KGFGUIUtility.itsStyleBoxLeft[KGFGUIUtility.itsSkinIndex];
		}
		if (theStyleBox == KGFGUIUtility.eStyleBox.eBoxLeftInteractive && KGFGUIUtility.itsStyleBoxLeft[KGFGUIUtility.itsSkinIndex] != null)
		{
			return KGFGUIUtility.itsStyleBoxLeftInteractive[KGFGUIUtility.itsSkinIndex];
		}
		if (theStyleBox == KGFGUIUtility.eStyleBox.eBoxRight && KGFGUIUtility.itsStyleBoxRight[KGFGUIUtility.itsSkinIndex] != null)
		{
			return KGFGUIUtility.itsStyleBoxRight[KGFGUIUtility.itsSkinIndex];
		}
		if (theStyleBox == KGFGUIUtility.eStyleBox.eBoxRightInteractive && KGFGUIUtility.itsStyleBoxRight[KGFGUIUtility.itsSkinIndex] != null)
		{
			return KGFGUIUtility.itsStyleBoxRightInteractive[KGFGUIUtility.itsSkinIndex];
		}
		if (theStyleBox == KGFGUIUtility.eStyleBox.eBoxMiddleHorizontal && KGFGUIUtility.itsStyleBoxMiddleHorizontal[KGFGUIUtility.itsSkinIndex] != null)
		{
			return KGFGUIUtility.itsStyleBoxMiddleHorizontal[KGFGUIUtility.itsSkinIndex];
		}
		if (theStyleBox == KGFGUIUtility.eStyleBox.eBoxMiddleHorizontalInteractive && KGFGUIUtility.itsStyleBoxMiddleHorizontal[KGFGUIUtility.itsSkinIndex] != null)
		{
			return KGFGUIUtility.itsStyleBoxMiddleHorizontalInteractive[KGFGUIUtility.itsSkinIndex];
		}
		if (theStyleBox == KGFGUIUtility.eStyleBox.eBoxTop && KGFGUIUtility.itsStyleBoxTop[KGFGUIUtility.itsSkinIndex] != null)
		{
			return KGFGUIUtility.itsStyleBoxTop[KGFGUIUtility.itsSkinIndex];
		}
		if (theStyleBox == KGFGUIUtility.eStyleBox.eBoxTopInteractive && KGFGUIUtility.itsStyleBoxTop[KGFGUIUtility.itsSkinIndex] != null)
		{
			return KGFGUIUtility.itsStyleBoxTopInteractive[KGFGUIUtility.itsSkinIndex];
		}
		if (theStyleBox == KGFGUIUtility.eStyleBox.eBoxBottom && KGFGUIUtility.itsStyleBoxBottom[KGFGUIUtility.itsSkinIndex] != null)
		{
			return KGFGUIUtility.itsStyleBoxBottom[KGFGUIUtility.itsSkinIndex];
		}
		if (theStyleBox == KGFGUIUtility.eStyleBox.eBoxBottomInteractive && KGFGUIUtility.itsStyleBoxBottom[KGFGUIUtility.itsSkinIndex] != null)
		{
			return KGFGUIUtility.itsStyleBoxBottomInteractive[KGFGUIUtility.itsSkinIndex];
		}
		if (theStyleBox == KGFGUIUtility.eStyleBox.eBoxMiddleVertical && KGFGUIUtility.itsStyleBoxMiddleVertical[KGFGUIUtility.itsSkinIndex] != null)
		{
			return KGFGUIUtility.itsStyleBoxMiddleVertical[KGFGUIUtility.itsSkinIndex];
		}
		if (theStyleBox == KGFGUIUtility.eStyleBox.eBoxMiddleVerticalInteractive && KGFGUIUtility.itsStyleBoxMiddleVertical[KGFGUIUtility.itsSkinIndex] != null)
		{
			return KGFGUIUtility.itsStyleBoxMiddleVerticalInteractive[KGFGUIUtility.itsSkinIndex];
		}
		if (theStyleBox == KGFGUIUtility.eStyleBox.eBoxDark && KGFGUIUtility.itsStyleBoxDark[KGFGUIUtility.itsSkinIndex] != null)
		{
			return KGFGUIUtility.itsStyleBoxDark[KGFGUIUtility.itsSkinIndex];
		}
		if (theStyleBox == KGFGUIUtility.eStyleBox.eBoxDarkInteractive && KGFGUIUtility.itsStyleBoxDark[KGFGUIUtility.itsSkinIndex] != null)
		{
			return KGFGUIUtility.itsStyleBoxDarkInteractive[KGFGUIUtility.itsSkinIndex];
		}
		if (theStyleBox == KGFGUIUtility.eStyleBox.eBoxDarkLeft && KGFGUIUtility.itsStyleBoxDarkLeft[KGFGUIUtility.itsSkinIndex] != null)
		{
			return KGFGUIUtility.itsStyleBoxDarkLeft[KGFGUIUtility.itsSkinIndex];
		}
		if (theStyleBox == KGFGUIUtility.eStyleBox.eBoxDarkLeftInteractive && KGFGUIUtility.itsStyleBoxDarkLeft[KGFGUIUtility.itsSkinIndex] != null)
		{
			return KGFGUIUtility.itsStyleBoxDarkLeftInteractive[KGFGUIUtility.itsSkinIndex];
		}
		if (theStyleBox == KGFGUIUtility.eStyleBox.eBoxDarkRight && KGFGUIUtility.itsStyleBoxDarkRight[KGFGUIUtility.itsSkinIndex] != null)
		{
			return KGFGUIUtility.itsStyleBoxDarkRight[KGFGUIUtility.itsSkinIndex];
		}
		if (theStyleBox == KGFGUIUtility.eStyleBox.eBoxDarkRightInteractive && KGFGUIUtility.itsStyleBoxDarkRight[KGFGUIUtility.itsSkinIndex] != null)
		{
			return KGFGUIUtility.itsStyleBoxDarkRightInteractive[KGFGUIUtility.itsSkinIndex];
		}
		if (theStyleBox == KGFGUIUtility.eStyleBox.eBoxDarkMiddleHorizontal && KGFGUIUtility.itsStyleBoxDarkMiddleHorizontal[KGFGUIUtility.itsSkinIndex] != null)
		{
			return KGFGUIUtility.itsStyleBoxDarkMiddleHorizontal[KGFGUIUtility.itsSkinIndex];
		}
		if (theStyleBox == KGFGUIUtility.eStyleBox.eBoxDarkMiddleHorizontalInteractive && KGFGUIUtility.itsStyleBoxDarkMiddleHorizontal[KGFGUIUtility.itsSkinIndex] != null)
		{
			return KGFGUIUtility.itsStyleBoxDarkMiddleHorizontalInteractive[KGFGUIUtility.itsSkinIndex];
		}
		if (theStyleBox == KGFGUIUtility.eStyleBox.eBoxDarkTop && KGFGUIUtility.itsStyleBoxDarkTop[KGFGUIUtility.itsSkinIndex] != null)
		{
			return KGFGUIUtility.itsStyleBoxDarkTop[KGFGUIUtility.itsSkinIndex];
		}
		if (theStyleBox == KGFGUIUtility.eStyleBox.eBoxDarkTopInteractive && KGFGUIUtility.itsStyleBoxDarkTop[KGFGUIUtility.itsSkinIndex] != null)
		{
			return KGFGUIUtility.itsStyleBoxDarkTopInteractive[KGFGUIUtility.itsSkinIndex];
		}
		if (theStyleBox == KGFGUIUtility.eStyleBox.eBoxDarkBottom && KGFGUIUtility.itsStyleBoxDarkBottom[KGFGUIUtility.itsSkinIndex] != null)
		{
			return KGFGUIUtility.itsStyleBoxDarkBottom[KGFGUIUtility.itsSkinIndex];
		}
		if (theStyleBox == KGFGUIUtility.eStyleBox.eBoxDarkBottomInteractive && KGFGUIUtility.itsStyleBoxDarkBottom[KGFGUIUtility.itsSkinIndex] != null)
		{
			return KGFGUIUtility.itsStyleBoxDarkBottomInteractive[KGFGUIUtility.itsSkinIndex];
		}
		if (theStyleBox == KGFGUIUtility.eStyleBox.eBoxDarkMiddleVertical && KGFGUIUtility.itsStyleBoxDarkMiddleVertical[KGFGUIUtility.itsSkinIndex] != null)
		{
			return KGFGUIUtility.itsStyleBoxDarkMiddleVertical[KGFGUIUtility.itsSkinIndex];
		}
		if (theStyleBox == KGFGUIUtility.eStyleBox.eBoxDarkMiddleVerticalInteractive && KGFGUIUtility.itsStyleBoxDarkMiddleVertical[KGFGUIUtility.itsSkinIndex] != null)
		{
			return KGFGUIUtility.itsStyleBoxDarkMiddleVerticalInteractive[KGFGUIUtility.itsSkinIndex];
		}
		if (theStyleBox == KGFGUIUtility.eStyleBox.eBoxDecorated && KGFGUIUtility.itsStyleBoxDecorated[KGFGUIUtility.itsSkinIndex] != null)
		{
			return KGFGUIUtility.itsStyleBoxDecorated[KGFGUIUtility.itsSkinIndex];
		}
		return GUI.skin.box;
	}

	// Token: 0x06000A52 RID: 2642 RVA: 0x0004EB08 File Offset: 0x0004CD08
	public static GUIStyle GetStyleSeparator(KGFGUIUtility.eStyleSeparator theStyleSeparator)
	{
		if (KGFGUIUtility.itsSkinIndex == -1)
		{
			return GUI.skin.box;
		}
		KGFGUIUtility.Init();
		if (theStyleSeparator == KGFGUIUtility.eStyleSeparator.eSeparatorHorizontal && KGFGUIUtility.itsStyleSeparatorHorizontal[KGFGUIUtility.itsSkinIndex] != null)
		{
			return KGFGUIUtility.itsStyleSeparatorHorizontal[KGFGUIUtility.itsSkinIndex];
		}
		if (theStyleSeparator == KGFGUIUtility.eStyleSeparator.eSeparatorVertical && KGFGUIUtility.itsStyleSeparatorVertical[KGFGUIUtility.itsSkinIndex] != null)
		{
			return KGFGUIUtility.itsStyleSeparatorVertical[KGFGUIUtility.itsSkinIndex];
		}
		if (theStyleSeparator == KGFGUIUtility.eStyleSeparator.eSeparatorVerticalFitInBox && KGFGUIUtility.itsStyleSeparatorVerticalFitInBox[KGFGUIUtility.itsSkinIndex] != null)
		{
			return KGFGUIUtility.itsStyleSeparatorVerticalFitInBox[KGFGUIUtility.itsSkinIndex];
		}
		return GUI.skin.label;
	}

	// Token: 0x06000A53 RID: 2643 RVA: 0x0004EBA4 File Offset: 0x0004CDA4
	public static GUIStyle GetStyleLabel(KGFGUIUtility.eStyleLabel theStyleLabel)
	{
		if (KGFGUIUtility.itsSkinIndex == -1)
		{
			return GUI.skin.label;
		}
		KGFGUIUtility.Init();
		if (theStyleLabel == KGFGUIUtility.eStyleLabel.eLabel && KGFGUIUtility.itsStyleLabel[KGFGUIUtility.itsSkinIndex] != null)
		{
			return KGFGUIUtility.itsStyleLabel[KGFGUIUtility.itsSkinIndex];
		}
		if (theStyleLabel == KGFGUIUtility.eStyleLabel.eLabelFitIntoBox && KGFGUIUtility.itsStyleLabelFitInToBox[KGFGUIUtility.itsSkinIndex] != null)
		{
			return KGFGUIUtility.itsStyleLabelFitInToBox[KGFGUIUtility.itsSkinIndex];
		}
		if (theStyleLabel == KGFGUIUtility.eStyleLabel.eLabelMultiline && KGFGUIUtility.itsStyleLabelMultiline[KGFGUIUtility.itsSkinIndex] != null)
		{
			return KGFGUIUtility.itsStyleLabelMultiline[KGFGUIUtility.itsSkinIndex];
		}
		if (theStyleLabel == KGFGUIUtility.eStyleLabel.eLabelTitle && KGFGUIUtility.itsStyleLabelTitle[KGFGUIUtility.itsSkinIndex] != null)
		{
			return KGFGUIUtility.itsStyleLabelTitle[KGFGUIUtility.itsSkinIndex];
		}
		return GUI.skin.box;
	}

	// Token: 0x06000A54 RID: 2644 RVA: 0x0004EC64 File Offset: 0x0004CE64
	public static GUIStyle GetStyleWindow()
	{
		if (KGFGUIUtility.itsSkinIndex == -1)
		{
			return GUI.skin.window;
		}
		KGFGUIUtility.Init();
		if (KGFGUIUtility.itsStyleWindow[KGFGUIUtility.itsSkinIndex] != null)
		{
			return KGFGUIUtility.itsStyleWindow[KGFGUIUtility.itsSkinIndex];
		}
		return GUI.skin.window;
	}

	// Token: 0x06000A55 RID: 2645 RVA: 0x0004ECB4 File Offset: 0x0004CEB4
	public static GUIStyle GetStyleCursor()
	{
		if (KGFGUIUtility.itsSkinIndex == -1)
		{
			return GUI.skin.box;
		}
		KGFGUIUtility.Init();
		if (KGFGUIUtility.itsStyleCursor[KGFGUIUtility.itsSkinIndex] != null)
		{
			return KGFGUIUtility.itsStyleCursor[KGFGUIUtility.itsSkinIndex];
		}
		return KGFGUIUtility.itsStyleCursor[KGFGUIUtility.itsSkinIndex];
	}

	// Token: 0x06000A56 RID: 2646 RVA: 0x0004ED04 File Offset: 0x0004CF04
	public static GUIStyle GetTableStyle()
	{
		if (KGFGUIUtility.itsSkinIndex == -1)
		{
			return GUI.skin.box;
		}
		KGFGUIUtility.Init();
		if (KGFGUIUtility.itsStyleTable[KGFGUIUtility.itsSkinIndex] != null)
		{
			return KGFGUIUtility.itsStyleTable[KGFGUIUtility.itsSkinIndex];
		}
		return GUI.skin.box;
	}

	// Token: 0x06000A57 RID: 2647 RVA: 0x0004ED54 File Offset: 0x0004CF54
	public static GUIStyle GetTableHeadingRowStyle()
	{
		if (KGFGUIUtility.itsSkinIndex == -1)
		{
			return GUI.skin.box;
		}
		KGFGUIUtility.Init();
		if (KGFGUIUtility.itsStyleTableHeadingRow[KGFGUIUtility.itsSkinIndex] != null)
		{
			return KGFGUIUtility.itsStyleTableHeadingRow[KGFGUIUtility.itsSkinIndex];
		}
		return GUI.skin.box;
	}

	// Token: 0x06000A58 RID: 2648 RVA: 0x0004EDA4 File Offset: 0x0004CFA4
	public static GUIStyle GetTableHeadingCellStyle()
	{
		if (KGFGUIUtility.itsSkinIndex == -1)
		{
			return GUI.skin.box;
		}
		KGFGUIUtility.Init();
		if (KGFGUIUtility.itsStyleTableHeadingCell[KGFGUIUtility.itsSkinIndex] != null)
		{
			return KGFGUIUtility.itsStyleTableHeadingCell[KGFGUIUtility.itsSkinIndex];
		}
		return GUI.skin.box;
	}

	// Token: 0x06000A59 RID: 2649 RVA: 0x0004EDF4 File Offset: 0x0004CFF4
	public static GUIStyle GetTableRowStyle()
	{
		if (KGFGUIUtility.itsSkinIndex == -1)
		{
			return GUI.skin.box;
		}
		KGFGUIUtility.Init();
		if (KGFGUIUtility.itsStyleTableRow[KGFGUIUtility.itsSkinIndex] != null)
		{
			return KGFGUIUtility.itsStyleTableRow[KGFGUIUtility.itsSkinIndex];
		}
		return GUI.skin.box;
	}

	// Token: 0x06000A5A RID: 2650 RVA: 0x0004EE44 File Offset: 0x0004D044
	public static GUIStyle GetTableCellStyle()
	{
		if (KGFGUIUtility.itsSkinIndex == -1)
		{
			return GUI.skin.box;
		}
		KGFGUIUtility.Init();
		if (KGFGUIUtility.itsStyleTableRowCell[KGFGUIUtility.itsSkinIndex] != null)
		{
			return KGFGUIUtility.itsStyleTableRowCell[KGFGUIUtility.itsSkinIndex];
		}
		return GUI.skin.box;
	}

	// Token: 0x06000A5B RID: 2651 RVA: 0x0004EE94 File Offset: 0x0004D094
	public static void SetVolume(float theVolume)
	{
		KGFGUIUtility.itsVolume = theVolume;
	}

	// Token: 0x06000A5C RID: 2652 RVA: 0x0004EE9C File Offset: 0x0004D09C
	public static void SetSoundForButton(KGFGUIUtility.eStyleButton theButtonStyle, AudioClip theAudioClip)
	{
		KGFGUIUtility.SetSound(theButtonStyle.ToString(), theAudioClip);
	}

	// Token: 0x06000A5D RID: 2653 RVA: 0x0004EEB0 File Offset: 0x0004D0B0
	public static void SetSoundForToggle(KGFGUIUtility.eStyleToggl theTogglStyle, AudioClip theAudioClip)
	{
		KGFGUIUtility.SetSound(theTogglStyle.ToString(), theAudioClip);
	}

	// Token: 0x06000A5E RID: 2654 RVA: 0x0004EEC4 File Offset: 0x0004D0C4
	private static void SetSound(string theStyle, AudioClip theAudioClip)
	{
		if (theAudioClip != null && KGFGUIUtility.itsAudioClips.ContainsKey(theStyle))
		{
			KGFGUIUtility.itsAudioClips.Remove(theStyle);
		}
		else
		{
			KGFGUIUtility.itsAudioClips[theStyle] = theAudioClip;
		}
	}

	// Token: 0x06000A5F RID: 2655 RVA: 0x0004EF0C File Offset: 0x0004D10C
	private static void PlaySound(string theStyle)
	{
		if (Application.isPlaying && KGFGUIUtility.itsAudioClips.ContainsKey(theStyle))
		{
			AudioSource.PlayClipAtPoint(KGFGUIUtility.itsAudioClips[theStyle], Vector3.zero, KGFGUIUtility.itsVolume);
		}
	}

	// Token: 0x06000A60 RID: 2656 RVA: 0x0004EF50 File Offset: 0x0004D150
	public static void SetEnableKGFSkinsInEdior(bool theSetEnableKGFSkins)
	{
		KGFGUIUtility.itsEnableKGFSkins = theSetEnableKGFSkins;
	}

	// Token: 0x06000A61 RID: 2657 RVA: 0x0004EF58 File Offset: 0x0004D158
	public static void SetSkinIndex(int theIndex)
	{
		KGFGUIUtility.itsSkinIndex = theIndex;
		if (KGFGUIUtility.itsSkinIndex == 0 && !KGFGUIUtility.itsEnableKGFSkins)
		{
			KGFGUIUtility.itsSkinIndex = -1;
		}
	}

	// Token: 0x06000A62 RID: 2658 RVA: 0x0004EF88 File Offset: 0x0004D188
	public static void SetSkinPath(string thePath)
	{
		KGFGUIUtility.itsDefaultGuiSkinPath[1] = thePath;
		KGFGUIUtility.itsResetPath[1] = true;
	}

	// Token: 0x06000A63 RID: 2659 RVA: 0x0004EF9C File Offset: 0x0004D19C
	public static void SetSkinPathEditor(string thePath)
	{
		KGFGUIUtility.itsDefaultGuiSkinPath[0] = thePath;
		KGFGUIUtility.itsResetPath[0] = true;
	}

	// Token: 0x06000A64 RID: 2660 RVA: 0x0004EFB0 File Offset: 0x0004D1B0
	public static string GetSkinPath()
	{
		return KGFGUIUtility.itsDefaultGuiSkinPath[KGFGUIUtility.itsSkinIndex];
	}

	// Token: 0x06000A65 RID: 2661 RVA: 0x0004EFC0 File Offset: 0x0004D1C0
	private static void Init()
	{
		KGFGUIUtility.Init(false);
	}

	// Token: 0x06000A66 RID: 2662 RVA: 0x0004EFC8 File Offset: 0x0004D1C8
	private static void Init(bool theForceInit)
	{
		if (KGFGUIUtility.itsSkinIndex == -1)
		{
			return;
		}
		if (KGFGUIUtility.itsSkin[KGFGUIUtility.itsSkinIndex] != null && !theForceInit && !KGFGUIUtility.itsResetPath[KGFGUIUtility.itsSkinIndex])
		{
			return;
		}
		KGFGUIUtility.itsResetPath[KGFGUIUtility.itsSkinIndex] = false;
		Debug.Log("Loading skin: " + KGFGUIUtility.itsDefaultGuiSkinPath[KGFGUIUtility.itsSkinIndex]);
		KGFGUIUtility.itsSkin[KGFGUIUtility.itsSkinIndex] = (Resources.Load(KGFGUIUtility.itsDefaultGuiSkinPath[KGFGUIUtility.itsSkinIndex]) as GUISkin);
		if (KGFGUIUtility.itsSkin[KGFGUIUtility.itsSkinIndex] == null)
		{
			Debug.Log("Kolmich Game Framework default skin wasn`t found");
			KGFGUIUtility.itsSkin[KGFGUIUtility.itsSkinIndex] = GUI.skin;
			return;
		}
		GUI.skin = KGFGUIUtility.itsSkin[KGFGUIUtility.itsSkinIndex];
		KGFGUIUtility.itsStyleToggle[KGFGUIUtility.itsSkinIndex] = KGFGUIUtility.itsSkin[KGFGUIUtility.itsSkinIndex].GetStyle("toggle");
		KGFGUIUtility.itsStyleTextField[KGFGUIUtility.itsSkinIndex] = KGFGUIUtility.itsSkin[KGFGUIUtility.itsSkinIndex].GetStyle("textfield");
		KGFGUIUtility.itsStyleTextFieldLeft[KGFGUIUtility.itsSkinIndex] = KGFGUIUtility.itsSkin[KGFGUIUtility.itsSkinIndex].GetStyle("textfield_left");
		KGFGUIUtility.itsStyleTextFieldRight[KGFGUIUtility.itsSkinIndex] = KGFGUIUtility.itsSkin[KGFGUIUtility.itsSkinIndex].GetStyle("textfield_right");
		KGFGUIUtility.itsStyleTextArea[KGFGUIUtility.itsSkinIndex] = KGFGUIUtility.itsSkin[KGFGUIUtility.itsSkinIndex].GetStyle("textarea");
		KGFGUIUtility.itsStyleWindow[KGFGUIUtility.itsSkinIndex] = KGFGUIUtility.itsSkin[KGFGUIUtility.itsSkinIndex].GetStyle("window");
		KGFGUIUtility.itsStyleHorizontalSlider[KGFGUIUtility.itsSkinIndex] = KGFGUIUtility.itsSkin[KGFGUIUtility.itsSkinIndex].GetStyle("horizontalslider");
		KGFGUIUtility.itsStyleHorizontalSliderThumb[KGFGUIUtility.itsSkinIndex] = KGFGUIUtility.itsSkin[KGFGUIUtility.itsSkinIndex].GetStyle("horizontalsliderthumb");
		KGFGUIUtility.itsStyleVerticalSlider[KGFGUIUtility.itsSkinIndex] = KGFGUIUtility.itsSkin[KGFGUIUtility.itsSkinIndex].GetStyle("verticalslider");
		KGFGUIUtility.itsStyleVerticalSliderThumb[KGFGUIUtility.itsSkinIndex] = KGFGUIUtility.itsSkin[KGFGUIUtility.itsSkinIndex].GetStyle("verticalsliderthumb");
		KGFGUIUtility.itsStyleHorizontalScrollbar[KGFGUIUtility.itsSkinIndex] = KGFGUIUtility.itsSkin[KGFGUIUtility.itsSkinIndex].GetStyle("horizontalscrollbar");
		KGFGUIUtility.itsStyleHorizontalScrollbarThumb[KGFGUIUtility.itsSkinIndex] = KGFGUIUtility.itsSkin[KGFGUIUtility.itsSkinIndex].GetStyle("horizontalscrollbarthumb");
		KGFGUIUtility.itsStyleHorizontalScrollbarLeftButton[KGFGUIUtility.itsSkinIndex] = KGFGUIUtility.itsSkin[KGFGUIUtility.itsSkinIndex].GetStyle("horizontalscrollbarleftbutton");
		KGFGUIUtility.itsStyleHorizontalScrollbarRightButton[KGFGUIUtility.itsSkinIndex] = KGFGUIUtility.itsSkin[KGFGUIUtility.itsSkinIndex].GetStyle("horizontalscrollbarrightbutton");
		KGFGUIUtility.itsStyleVerticalScrollbar[KGFGUIUtility.itsSkinIndex] = KGFGUIUtility.itsSkin[KGFGUIUtility.itsSkinIndex].GetStyle("verticalscrollbar");
		KGFGUIUtility.itsStyleVerticalScrollbarThumb[KGFGUIUtility.itsSkinIndex] = KGFGUIUtility.itsSkin[KGFGUIUtility.itsSkinIndex].GetStyle("verticalscrollbarthumb");
		KGFGUIUtility.itsStyleVerticalScrollbarUpButton[KGFGUIUtility.itsSkinIndex] = KGFGUIUtility.itsSkin[KGFGUIUtility.itsSkinIndex].GetStyle("verticalscrollbarupbutton");
		KGFGUIUtility.itsStyleVerticalScrollbarDownButton[KGFGUIUtility.itsSkinIndex] = KGFGUIUtility.itsSkin[KGFGUIUtility.itsSkinIndex].GetStyle("verticalscrollbardownbutton");
		KGFGUIUtility.itsStyleScrollView[KGFGUIUtility.itsSkinIndex] = KGFGUIUtility.itsSkin[KGFGUIUtility.itsSkinIndex].GetStyle("scrollview");
		KGFGUIUtility.itsStyleMinimap[KGFGUIUtility.itsSkinIndex] = KGFGUIUtility.itsSkin[KGFGUIUtility.itsSkinIndex].GetStyle("minimap");
		KGFGUIUtility.itsStyleMinimapButton[KGFGUIUtility.itsSkinIndex] = KGFGUIUtility.itsSkin[KGFGUIUtility.itsSkinIndex].GetStyle("minimap_button");
		KGFGUIUtility.itsStyleToggleStreched[KGFGUIUtility.itsSkinIndex] = KGFGUIUtility.itsSkin[KGFGUIUtility.itsSkinIndex].GetStyle("toggle_stretched");
		KGFGUIUtility.itsStyleToggleCompact[KGFGUIUtility.itsSkinIndex] = KGFGUIUtility.itsSkin[KGFGUIUtility.itsSkinIndex].GetStyle("toggle_compact");
		KGFGUIUtility.itsStyleToggleSuperCompact[KGFGUIUtility.itsSkinIndex] = KGFGUIUtility.itsSkin[KGFGUIUtility.itsSkinIndex].GetStyle("toggle_supercompact");
		KGFGUIUtility.itsStyleToggleRadioStreched[KGFGUIUtility.itsSkinIndex] = KGFGUIUtility.itsSkin[KGFGUIUtility.itsSkinIndex].GetStyle("toggle_radio_stretched");
		KGFGUIUtility.itsStyleToggleRadioCompact[KGFGUIUtility.itsSkinIndex] = KGFGUIUtility.itsSkin[KGFGUIUtility.itsSkinIndex].GetStyle("toggle_radio_compact");
		KGFGUIUtility.itsStyleToggleRadioSuperCompact[KGFGUIUtility.itsSkinIndex] = KGFGUIUtility.itsSkin[KGFGUIUtility.itsSkinIndex].GetStyle("toggle_radio_supercompact");
		KGFGUIUtility.itsStyleToggleSwitch[KGFGUIUtility.itsSkinIndex] = KGFGUIUtility.itsSkin[KGFGUIUtility.itsSkinIndex].GetStyle("toggle_switch");
		KGFGUIUtility.itsStyleToggleBoolean[KGFGUIUtility.itsSkinIndex] = KGFGUIUtility.itsSkin[KGFGUIUtility.itsSkinIndex].GetStyle("toggle_boolean");
		KGFGUIUtility.itsStyleToggleArrow[KGFGUIUtility.itsSkinIndex] = KGFGUIUtility.itsSkin[KGFGUIUtility.itsSkinIndex].GetStyle("toggle_arrow");
		KGFGUIUtility.itsStyleToggleButton[KGFGUIUtility.itsSkinIndex] = KGFGUIUtility.itsSkin[KGFGUIUtility.itsSkinIndex].GetStyle("toggle_button");
		KGFGUIUtility.itsStyleButton[KGFGUIUtility.itsSkinIndex] = KGFGUIUtility.itsSkin[KGFGUIUtility.itsSkinIndex].GetStyle("Button");
		KGFGUIUtility.itsStyleButtonLeft[KGFGUIUtility.itsSkinIndex] = KGFGUIUtility.itsSkin[KGFGUIUtility.itsSkinIndex].GetStyle("button_left");
		KGFGUIUtility.itsStyleButtonRight[KGFGUIUtility.itsSkinIndex] = KGFGUIUtility.itsSkin[KGFGUIUtility.itsSkinIndex].GetStyle("button_right");
		KGFGUIUtility.itsStyleButtonTop[KGFGUIUtility.itsSkinIndex] = KGFGUIUtility.itsSkin[KGFGUIUtility.itsSkinIndex].GetStyle("button_top");
		KGFGUIUtility.itsStyleButtonBottom[KGFGUIUtility.itsSkinIndex] = KGFGUIUtility.itsSkin[KGFGUIUtility.itsSkinIndex].GetStyle("button_bottom");
		KGFGUIUtility.itsStyleButtonMiddle[KGFGUIUtility.itsSkinIndex] = KGFGUIUtility.itsSkin[KGFGUIUtility.itsSkinIndex].GetStyle("button_middle");
		KGFGUIUtility.itsStyleBox[KGFGUIUtility.itsSkinIndex] = KGFGUIUtility.itsSkin[KGFGUIUtility.itsSkinIndex].GetStyle("Box");
		KGFGUIUtility.itsStyleBoxInvisible[KGFGUIUtility.itsSkinIndex] = KGFGUIUtility.itsSkin[KGFGUIUtility.itsSkinIndex].GetStyle("box_invisible");
		KGFGUIUtility.itsStyleBoxInteractive[KGFGUIUtility.itsSkinIndex] = KGFGUIUtility.itsSkin[KGFGUIUtility.itsSkinIndex].GetStyle("box_interactive");
		KGFGUIUtility.itsStyleBoxLeft[KGFGUIUtility.itsSkinIndex] = KGFGUIUtility.itsSkin[KGFGUIUtility.itsSkinIndex].GetStyle("box_left");
		KGFGUIUtility.itsStyleBoxLeftInteractive[KGFGUIUtility.itsSkinIndex] = KGFGUIUtility.itsSkin[KGFGUIUtility.itsSkinIndex].GetStyle("box_left_interactive");
		KGFGUIUtility.itsStyleBoxRight[KGFGUIUtility.itsSkinIndex] = KGFGUIUtility.itsSkin[KGFGUIUtility.itsSkinIndex].GetStyle("box_right");
		KGFGUIUtility.itsStyleBoxRightInteractive[KGFGUIUtility.itsSkinIndex] = KGFGUIUtility.itsSkin[KGFGUIUtility.itsSkinIndex].GetStyle("box_right_interactive");
		KGFGUIUtility.itsStyleBoxMiddleHorizontal[KGFGUIUtility.itsSkinIndex] = KGFGUIUtility.itsSkin[KGFGUIUtility.itsSkinIndex].GetStyle("box_middle_horizontal");
		KGFGUIUtility.itsStyleBoxMiddleHorizontalInteractive[KGFGUIUtility.itsSkinIndex] = KGFGUIUtility.itsSkin[KGFGUIUtility.itsSkinIndex].GetStyle("box_middle_horizontal_interactive");
		KGFGUIUtility.itsStyleBoxTop[KGFGUIUtility.itsSkinIndex] = KGFGUIUtility.itsSkin[KGFGUIUtility.itsSkinIndex].GetStyle("box_top");
		KGFGUIUtility.itsStyleBoxTopInteractive[KGFGUIUtility.itsSkinIndex] = KGFGUIUtility.itsSkin[KGFGUIUtility.itsSkinIndex].GetStyle("box_top_interactive");
		KGFGUIUtility.itsStyleBoxBottom[KGFGUIUtility.itsSkinIndex] = KGFGUIUtility.itsSkin[KGFGUIUtility.itsSkinIndex].GetStyle("box_bottom");
		KGFGUIUtility.itsStyleBoxBottomInteractive[KGFGUIUtility.itsSkinIndex] = KGFGUIUtility.itsSkin[KGFGUIUtility.itsSkinIndex].GetStyle("box_bottom_interactive");
		KGFGUIUtility.itsStyleBoxMiddleVertical[KGFGUIUtility.itsSkinIndex] = KGFGUIUtility.itsSkin[KGFGUIUtility.itsSkinIndex].GetStyle("box_middle_vertical");
		KGFGUIUtility.itsStyleBoxMiddleVerticalInteractive[KGFGUIUtility.itsSkinIndex] = KGFGUIUtility.itsSkin[KGFGUIUtility.itsSkinIndex].GetStyle("box_middle_vertical_interactive");
		KGFGUIUtility.itsStyleBoxDark[KGFGUIUtility.itsSkinIndex] = KGFGUIUtility.itsSkin[KGFGUIUtility.itsSkinIndex].GetStyle("box_dark");
		KGFGUIUtility.itsStyleBoxDarkInteractive[KGFGUIUtility.itsSkinIndex] = KGFGUIUtility.itsSkin[KGFGUIUtility.itsSkinIndex].GetStyle("box_dark_interactive");
		KGFGUIUtility.itsStyleBoxDarkLeft[KGFGUIUtility.itsSkinIndex] = KGFGUIUtility.itsSkin[KGFGUIUtility.itsSkinIndex].GetStyle("box_dark_left");
		KGFGUIUtility.itsStyleBoxDarkLeftInteractive[KGFGUIUtility.itsSkinIndex] = KGFGUIUtility.itsSkin[KGFGUIUtility.itsSkinIndex].GetStyle("box_dark_left_interactive");
		KGFGUIUtility.itsStyleBoxDarkRight[KGFGUIUtility.itsSkinIndex] = KGFGUIUtility.itsSkin[KGFGUIUtility.itsSkinIndex].GetStyle("box_dark_right");
		KGFGUIUtility.itsStyleBoxDarkRightInteractive[KGFGUIUtility.itsSkinIndex] = KGFGUIUtility.itsSkin[KGFGUIUtility.itsSkinIndex].GetStyle("box_dark_right_interactive");
		KGFGUIUtility.itsStyleBoxDarkMiddleHorizontal[KGFGUIUtility.itsSkinIndex] = KGFGUIUtility.itsSkin[KGFGUIUtility.itsSkinIndex].GetStyle("box_dark_middle_horizontal");
		KGFGUIUtility.itsStyleBoxDarkMiddleHorizontalInteractive[KGFGUIUtility.itsSkinIndex] = KGFGUIUtility.itsSkin[KGFGUIUtility.itsSkinIndex].GetStyle("box_dark_middle_horizontal_interactive");
		KGFGUIUtility.itsStyleBoxDarkTop[KGFGUIUtility.itsSkinIndex] = KGFGUIUtility.itsSkin[KGFGUIUtility.itsSkinIndex].GetStyle("box_dark_top");
		KGFGUIUtility.itsStyleBoxDarkTopInteractive[KGFGUIUtility.itsSkinIndex] = KGFGUIUtility.itsSkin[KGFGUIUtility.itsSkinIndex].GetStyle("box_dark_top_interactive");
		KGFGUIUtility.itsStyleBoxDarkBottom[KGFGUIUtility.itsSkinIndex] = KGFGUIUtility.itsSkin[KGFGUIUtility.itsSkinIndex].GetStyle("box_dark_bottom");
		KGFGUIUtility.itsStyleBoxDarkBottomInteractive[KGFGUIUtility.itsSkinIndex] = KGFGUIUtility.itsSkin[KGFGUIUtility.itsSkinIndex].GetStyle("box_dark_bottom_interactive");
		KGFGUIUtility.itsStyleBoxDarkMiddleVertical[KGFGUIUtility.itsSkinIndex] = KGFGUIUtility.itsSkin[KGFGUIUtility.itsSkinIndex].GetStyle("box_dark_middle_vertical");
		KGFGUIUtility.itsStyleBoxDarkMiddleVerticalInteractive[KGFGUIUtility.itsSkinIndex] = KGFGUIUtility.itsSkin[KGFGUIUtility.itsSkinIndex].GetStyle("box_dark_middle_vertical_interactive");
		KGFGUIUtility.itsStyleBoxDecorated[KGFGUIUtility.itsSkinIndex] = KGFGUIUtility.itsSkin[KGFGUIUtility.itsSkinIndex].GetStyle("box_decorated");
		KGFGUIUtility.itsStyleSeparatorVertical[KGFGUIUtility.itsSkinIndex] = KGFGUIUtility.itsSkin[KGFGUIUtility.itsSkinIndex].GetStyle("separator_vertical");
		KGFGUIUtility.itsStyleSeparatorVerticalFitInBox[KGFGUIUtility.itsSkinIndex] = KGFGUIUtility.itsSkin[KGFGUIUtility.itsSkinIndex].GetStyle("separator_vertical_fitinbox");
		KGFGUIUtility.itsStyleSeparatorHorizontal[KGFGUIUtility.itsSkinIndex] = KGFGUIUtility.itsSkin[KGFGUIUtility.itsSkinIndex].GetStyle("separator_horizontal");
		KGFGUIUtility.itsStyleLabel[KGFGUIUtility.itsSkinIndex] = KGFGUIUtility.itsSkin[KGFGUIUtility.itsSkinIndex].GetStyle("label");
		KGFGUIUtility.itsStyleLabelFitInToBox[KGFGUIUtility.itsSkinIndex] = KGFGUIUtility.itsSkin[KGFGUIUtility.itsSkinIndex].GetStyle("label_fitintobox");
		KGFGUIUtility.itsStyleLabelMultiline[KGFGUIUtility.itsSkinIndex] = KGFGUIUtility.itsSkin[KGFGUIUtility.itsSkinIndex].GetStyle("label_multiline");
		KGFGUIUtility.itsStyleLabelTitle[KGFGUIUtility.itsSkinIndex] = KGFGUIUtility.itsSkin[KGFGUIUtility.itsSkinIndex].GetStyle("label_title");
		KGFGUIUtility.itsStyleCursor[KGFGUIUtility.itsSkinIndex] = KGFGUIUtility.itsSkin[KGFGUIUtility.itsSkinIndex].GetStyle("mouse_cursor");
	}

	// Token: 0x06000A67 RID: 2663 RVA: 0x0004F9F8 File Offset: 0x0004DBF8
	public static void BeginWindowHeader(string theTitle, Texture2D theIcon)
	{
		KGFGUIUtility.Init();
		KGFGUIUtility.BeginHorizontalBox(KGFGUIUtility.eStyleBox.eBoxDark, new GUILayoutOption[0]);
		KGFGUIUtility.Label(string.Empty, theIcon, KGFGUIUtility.eStyleLabel.eLabel, new GUILayoutOption[]
		{
			GUILayout.Width(KGFGUIUtility.GetSkinHeight())
		});
		KGFGUIUtility.Label(theTitle, KGFGUIUtility.eStyleLabel.eLabel, new GUILayoutOption[0]);
	}

	// Token: 0x06000A68 RID: 2664 RVA: 0x0004FA44 File Offset: 0x0004DC44
	public static bool EndWindowHeader(bool theCloseButton)
	{
		bool result = false;
		if (theCloseButton)
		{
			KGFGUIUtility.Init();
			if (KGFGUIUtility.itsSkinIndex == -1)
			{
				result = GUILayout.Button("x", new GUILayoutOption[]
				{
					GUILayout.Width(KGFGUIUtility.GetSkinHeight())
				});
			}
			else
			{
				result = KGFGUIUtility.Button("x", KGFGUIUtility.eStyleButton.eButton, new GUILayoutOption[]
				{
					GUILayout.Width(KGFGUIUtility.GetSkinHeight())
				});
			}
		}
		KGFGUIUtility.EndHorizontalBox();
		return result;
	}

	// Token: 0x06000A69 RID: 2665 RVA: 0x0004FAB4 File Offset: 0x0004DCB4
	public static void RenderDropDownList()
	{
		if (KGFGUIDropDown.itsOpenInstance != null && KGFGUIDropDown.itsCorrectedOffset)
		{
			GUI.depth = 0;
			Rect screenRect;
			bool flag;
			if (KGFGUIDropDown.itsOpenInstance.itsDirection == KGFGUIDropDown.eDropDirection.eDown || (KGFGUIDropDown.itsOpenInstance.itsDirection == KGFGUIDropDown.eDropDirection.eAuto && KGFGUIDropDown.itsOpenInstance.itsLastRect.y + KGFGUIUtility.GetStyleButton(KGFGUIUtility.eStyleButton.eButton).fixedHeight + KGFGUIDropDown.itsOpenInstance.itsHeight < (float)Screen.height))
			{
				screenRect = new Rect(KGFGUIDropDown.itsOpenInstance.itsLastRect.x, KGFGUIDropDown.itsOpenInstance.itsLastRect.y + KGFGUIUtility.GetStyleButton(KGFGUIUtility.eStyleButton.eButton).fixedHeight, KGFGUIDropDown.itsOpenInstance.itsWidth, KGFGUIDropDown.itsOpenInstance.itsHeight);
				flag = true;
			}
			else
			{
				screenRect = new Rect(KGFGUIDropDown.itsOpenInstance.itsLastRect.x, KGFGUIDropDown.itsOpenInstance.itsLastRect.y - KGFGUIDropDown.itsOpenInstance.itsHeight, KGFGUIDropDown.itsOpenInstance.itsWidth, KGFGUIDropDown.itsOpenInstance.itsHeight);
				flag = false;
			}
			GUILayout.BeginArea(screenRect);
			if (KGFGUIUtility.itsSkinIndex == -1)
			{
				KGFGUIDropDown.itsOpenInstance.itsScrollPosition = KGFGUIUtility.BeginScrollView(KGFGUIDropDown.itsOpenInstance.itsScrollPosition, false, false, new GUILayoutOption[]
				{
					GUILayout.ExpandWidth(true)
				});
			}
			else
			{
				KGFGUIDropDown.itsOpenInstance.itsScrollPosition = GUILayout.BeginScrollView(KGFGUIDropDown.itsOpenInstance.itsScrollPosition, false, false, new GUILayoutOption[]
				{
					GUILayout.ExpandWidth(true)
				});
			}
			foreach (string text in KGFGUIDropDown.itsOpenInstance.GetEntrys())
			{
				if (text != string.Empty && KGFGUIUtility.Button(text, KGFGUIUtility.eStyleButton.eButtonMiddle, new GUILayoutOption[]
				{
					GUILayout.ExpandWidth(true)
				}))
				{
					KGFGUIDropDown.itsOpenInstance.SetSelectedItem(text);
					KGFGUIDropDown.itsOpenInstance = null;
					break;
				}
			}
			GUILayout.EndScrollView();
			GUILayout.EndArea();
			if (flag)
			{
				screenRect.y -= KGFGUIUtility.GetSkinHeight();
				screenRect.height += KGFGUIUtility.GetSkinHeight();
			}
			else
			{
				screenRect.height += KGFGUIUtility.GetSkinHeight();
			}
			Vector3 mousePosition = Input.mousePosition;
			mousePosition.y = (float)Screen.height - mousePosition.y;
			if (!screenRect.Contains(mousePosition) && Event.current.type == EventType.MouseDown && Event.current.button == 0)
			{
				KGFGUIDropDown.itsOpenInstance = null;
			}
			if (KGFGUIDropDown.itsOpenInstance != null)
			{
				if (screenRect.Contains(mousePosition))
				{
					KGFGUIDropDown.itsOpenInstance.itsHover = true;
				}
				else
				{
					KGFGUIDropDown.itsOpenInstance.itsHover = false;
				}
			}
		}
	}

	// Token: 0x06000A6A RID: 2666 RVA: 0x0004FD98 File Offset: 0x0004DF98
	public static void Space()
	{
		GUILayout.Space(KGFGUIUtility.GetSkinHeight());
	}

	// Token: 0x06000A6B RID: 2667 RVA: 0x0004FDA4 File Offset: 0x0004DFA4
	public static void SpaceSmall()
	{
		GUILayout.Space(KGFGUIUtility.GetSkinHeight() / 2f);
	}

	// Token: 0x06000A6C RID: 2668 RVA: 0x0004FDB8 File Offset: 0x0004DFB8
	public static void Label(string theText, params GUILayoutOption[] theLayout)
	{
		KGFGUIUtility.Label(theText, KGFGUIUtility.eStyleLabel.eLabel, theLayout);
	}

	// Token: 0x06000A6D RID: 2669 RVA: 0x0004FDC4 File Offset: 0x0004DFC4
	public static void Label(string theText, KGFGUIUtility.eStyleLabel theStyleLabel, params GUILayoutOption[] theLayout)
	{
		KGFGUIUtility.Label(theText, null, theStyleLabel, theLayout);
	}

	// Token: 0x06000A6E RID: 2670 RVA: 0x0004FDD0 File Offset: 0x0004DFD0
	public static void Label(string theText, Texture2D theImage, KGFGUIUtility.eStyleLabel theStyleLabel, params GUILayoutOption[] theLayout)
	{
		KGFGUIUtility.Init();
		GUIContent content;
		if (theImage != null)
		{
			content = new GUIContent(theText, theImage);
		}
		else
		{
			content = new GUIContent(theText);
		}
		if (KGFGUIUtility.itsSkinIndex == -1)
		{
			GUILayout.Label(content, theLayout);
		}
		else
		{
			GUILayout.Label(content, KGFGUIUtility.GetStyleLabel(theStyleLabel), theLayout);
		}
	}

	// Token: 0x06000A6F RID: 2671 RVA: 0x0004FE28 File Offset: 0x0004E028
	public static void Separator(KGFGUIUtility.eStyleSeparator theStyleSeparator, params GUILayoutOption[] theLayout)
	{
		KGFGUIUtility.Init();
		if (KGFGUIUtility.itsSkinIndex == -1)
		{
			GUILayout.Label("|", theLayout);
		}
		else
		{
			GUILayout.Label(string.Empty, KGFGUIUtility.GetStyleSeparator(theStyleSeparator), theLayout);
		}
	}

	// Token: 0x06000A70 RID: 2672 RVA: 0x0004FE5C File Offset: 0x0004E05C
	public static bool Toggle(bool theValue, string theText, KGFGUIUtility.eStyleToggl theToggleStyle, params GUILayoutOption[] theLayout)
	{
		KGFGUIUtility.Init();
		bool flag;
		if (KGFGUIUtility.itsSkinIndex == -1)
		{
			flag = GUILayout.Toggle(theValue, theText, theLayout);
		}
		else
		{
			flag = GUILayout.Toggle(theValue, theText, KGFGUIUtility.GetStyleToggl(theToggleStyle), theLayout);
		}
		if (flag != theValue)
		{
			KGFGUIUtility.PlaySound(theToggleStyle.ToString());
		}
		return flag;
	}

	// Token: 0x06000A71 RID: 2673 RVA: 0x0004FEB0 File Offset: 0x0004E0B0
	public static bool Toggle(bool theValue, Texture2D theImage, KGFGUIUtility.eStyleToggl theToggleStyle, params GUILayoutOption[] theLayout)
	{
		KGFGUIUtility.Init();
		bool flag;
		if (KGFGUIUtility.itsSkinIndex == -1)
		{
			flag = GUILayout.Toggle(theValue, theImage, theLayout);
		}
		else
		{
			flag = GUILayout.Toggle(theValue, theImage, KGFGUIUtility.GetStyleToggl(theToggleStyle), theLayout);
		}
		if (flag != theValue)
		{
			KGFGUIUtility.PlaySound(theToggleStyle.ToString());
		}
		return flag;
	}

	// Token: 0x06000A72 RID: 2674 RVA: 0x0004FF04 File Offset: 0x0004E104
	public static bool Toggle(bool theValue, string theText, Texture2D theImage, KGFGUIUtility.eStyleToggl theToggleStyle, params GUILayoutOption[] theLayout)
	{
		KGFGUIUtility.Init();
		GUIContent content;
		if (theImage != null)
		{
			content = new GUIContent(theText, theImage);
		}
		else
		{
			content = new GUIContent(theText);
		}
		bool flag;
		if (KGFGUIUtility.itsSkinIndex == -1)
		{
			flag = GUILayout.Toggle(theValue, content, theLayout);
		}
		else
		{
			flag = GUILayout.Toggle(theValue, content, KGFGUIUtility.GetStyleToggl(theToggleStyle), theLayout);
		}
		if (flag != theValue)
		{
			KGFGUIUtility.PlaySound(theToggleStyle.ToString());
		}
		return flag;
	}

	// Token: 0x06000A73 RID: 2675 RVA: 0x0004FF7C File Offset: 0x0004E17C
	public static Rect Window(int theId, Rect theRect, GUI.WindowFunction theFunction, string theText, params GUILayoutOption[] theLayout)
	{
		return KGFGUIUtility.Window(theId, theRect, theFunction, null, theText, theLayout);
	}

	// Token: 0x06000A74 RID: 2676 RVA: 0x0004FF8C File Offset: 0x0004E18C
	public static Rect Window(int theId, Rect theRect, GUI.WindowFunction theFunction, Texture theImage, params GUILayoutOption[] theLayout)
	{
		return KGFGUIUtility.Window(theId, theRect, theFunction, theImage, string.Empty, theLayout);
	}

	// Token: 0x06000A75 RID: 2677 RVA: 0x0004FFA0 File Offset: 0x0004E1A0
	public static Rect Window(int theId, Rect theRect, GUI.WindowFunction theFunction, Texture theImage, string theText, params GUILayoutOption[] theLayout)
	{
		KGFGUIUtility.Init();
		GUIContent content;
		if (theImage != null)
		{
			content = new GUIContent(theText, theImage);
		}
		else
		{
			content = new GUIContent(theText);
		}
		if (KGFGUIUtility.itsSkinIndex == -1)
		{
			return GUILayout.Window(theId, theRect, theFunction, content, theLayout);
		}
		if (KGFGUIUtility.itsStyleWindow[KGFGUIUtility.itsSkinIndex] != null)
		{
			return GUILayout.Window(theId, theRect, theFunction, content, KGFGUIUtility.itsStyleWindow[KGFGUIUtility.itsSkinIndex], theLayout);
		}
		return GUILayout.Window(theId, theRect, theFunction, content, theLayout);
	}

	// Token: 0x06000A76 RID: 2678 RVA: 0x00050020 File Offset: 0x0004E220
	public static void Box(string theText, KGFGUIUtility.eStyleBox theStyleBox, params GUILayoutOption[] theLayout)
	{
		KGFGUIUtility.Box(null, theText, theStyleBox, theLayout);
	}

	// Token: 0x06000A77 RID: 2679 RVA: 0x0005002C File Offset: 0x0004E22C
	public static void Box(Texture theImage, KGFGUIUtility.eStyleBox theStyleBox, params GUILayoutOption[] theLayout)
	{
		KGFGUIUtility.Box(theImage, string.Empty, theStyleBox, theLayout);
	}

	// Token: 0x06000A78 RID: 2680 RVA: 0x0005003C File Offset: 0x0004E23C
	public static void Box(Texture theImage, string theText, KGFGUIUtility.eStyleBox theStyleBox, params GUILayoutOption[] theLayout)
	{
		KGFGUIUtility.Init();
		GUIContent content;
		if (theImage != null)
		{
			content = new GUIContent(theText, theImage);
		}
		else
		{
			content = new GUIContent(theText);
		}
		if (KGFGUIUtility.itsSkinIndex == -1)
		{
			GUILayout.Box(content, theLayout);
		}
		else
		{
			GUILayout.Box(content, KGFGUIUtility.GetStyleBox(theStyleBox), theLayout);
		}
	}

	// Token: 0x06000A79 RID: 2681 RVA: 0x00050094 File Offset: 0x0004E294
	public static void BeginVerticalBox(KGFGUIUtility.eStyleBox theStyleBox, params GUILayoutOption[] theLayout)
	{
		KGFGUIUtility.Init();
		if (KGFGUIUtility.itsSkinIndex == -1)
		{
			GUILayout.BeginVertical(GUI.skin.box, theLayout);
		}
		else
		{
			GUILayout.BeginVertical(KGFGUIUtility.GetStyleBox(theStyleBox), theLayout);
		}
	}

	// Token: 0x06000A7A RID: 2682 RVA: 0x000500D4 File Offset: 0x0004E2D4
	public static void EndVerticalBox()
	{
		GUILayout.EndVertical();
	}

	// Token: 0x06000A7B RID: 2683 RVA: 0x000500DC File Offset: 0x0004E2DC
	public static void BeginVerticalPadding()
	{
		GUILayout.BeginVertical(new GUILayoutOption[0]);
		KGFGUIUtility.BeginHorizontalBox(KGFGUIUtility.eStyleBox.eBoxInvisible, new GUILayoutOption[0]);
	}

	// Token: 0x06000A7C RID: 2684 RVA: 0x000500F8 File Offset: 0x0004E2F8
	public static void EndVerticalPadding()
	{
		KGFGUIUtility.EndHorizontalBox();
		GUILayout.EndVertical();
	}

	// Token: 0x06000A7D RID: 2685 RVA: 0x00050104 File Offset: 0x0004E304
	public static void BeginHorizontalPadding()
	{
		GUILayout.BeginHorizontal(new GUILayoutOption[0]);
		KGFGUIUtility.BeginVerticalBox(KGFGUIUtility.eStyleBox.eBoxInvisible, new GUILayoutOption[0]);
	}

	// Token: 0x06000A7E RID: 2686 RVA: 0x00050120 File Offset: 0x0004E320
	public static void EndHorizontalPadding()
	{
		KGFGUIUtility.EndVerticalBox();
		GUILayout.EndHorizontal();
	}

	// Token: 0x06000A7F RID: 2687 RVA: 0x0005012C File Offset: 0x0004E32C
	public static void BeginHorizontalBox(KGFGUIUtility.eStyleBox theStyleBox, params GUILayoutOption[] theLayout)
	{
		KGFGUIUtility.Init();
		if (KGFGUIUtility.itsSkinIndex == -1)
		{
			GUILayout.BeginHorizontal(GUI.skin.box, theLayout);
		}
		else
		{
			GUILayout.BeginHorizontal(KGFGUIUtility.GetStyleBox(theStyleBox), theLayout);
		}
	}

	// Token: 0x06000A80 RID: 2688 RVA: 0x0005016C File Offset: 0x0004E36C
	public static void EndHorizontalBox()
	{
		GUILayout.EndHorizontal();
	}

	// Token: 0x06000A81 RID: 2689 RVA: 0x00050174 File Offset: 0x0004E374
	public static Vector2 BeginScrollView(Vector2 thePosition, bool theHorizontalAlwaysVisible, bool theVerticalAlwaysVisible, params GUILayoutOption[] theLayout)
	{
		KGFGUIUtility.Init();
		if (KGFGUIUtility.itsSkinIndex != -1)
		{
			GUI.skin = KGFGUIUtility.itsSkin[KGFGUIUtility.itsSkinIndex];
		}
		if (KGFGUIUtility.itsStyleHorizontalScrollbar != null && KGFGUIUtility.itsStyleVerticalScrollbar != null && KGFGUIUtility.itsSkinIndex != -1)
		{
			return GUILayout.BeginScrollView(thePosition, theHorizontalAlwaysVisible, theVerticalAlwaysVisible, KGFGUIUtility.itsStyleHorizontalScrollbar[KGFGUIUtility.itsSkinIndex], KGFGUIUtility.itsStyleVerticalScrollbar[KGFGUIUtility.itsSkinIndex], theLayout);
		}
		return GUILayout.BeginScrollView(thePosition, theHorizontalAlwaysVisible, theVerticalAlwaysVisible, theLayout);
	}

	// Token: 0x06000A82 RID: 2690 RVA: 0x000501EC File Offset: 0x0004E3EC
	public static void EndScrollView()
	{
		GUILayout.EndScrollView();
	}

	// Token: 0x06000A83 RID: 2691 RVA: 0x000501F4 File Offset: 0x0004E3F4
	public static string TextField(string theText, KGFGUIUtility.eStyleTextField theStyleTextField, params GUILayoutOption[] theLayout)
	{
		KGFGUIUtility.Init();
		if (KGFGUIUtility.itsSkinIndex == -1)
		{
			return GUILayout.TextField(theText, theLayout);
		}
		return GUILayout.TextField(theText, KGFGUIUtility.GetStyleTextField(theStyleTextField), theLayout);
	}

	// Token: 0x06000A84 RID: 2692 RVA: 0x0005021C File Offset: 0x0004E41C
	public static string TextArea(string theText, params GUILayoutOption[] theLayout)
	{
		KGFGUIUtility.Init();
		if (KGFGUIUtility.itsStyleTextArea[KGFGUIUtility.itsSkinIndex] != null && KGFGUIUtility.itsSkinIndex != -1)
		{
			return GUILayout.TextArea(theText, KGFGUIUtility.itsStyleTextArea[KGFGUIUtility.itsSkinIndex], theLayout);
		}
		return GUILayout.TextArea(theText, theLayout);
	}

	// Token: 0x06000A85 RID: 2693 RVA: 0x00050264 File Offset: 0x0004E464
	public static bool Button(string theText, KGFGUIUtility.eStyleButton theButtonStyle, params GUILayoutOption[] theLayout)
	{
		return KGFGUIUtility.Button(null, theText, theButtonStyle, theLayout);
	}

	// Token: 0x06000A86 RID: 2694 RVA: 0x00050270 File Offset: 0x0004E470
	public static bool Button(Texture theImage, KGFGUIUtility.eStyleButton theButtonStyle, params GUILayoutOption[] theLayout)
	{
		return KGFGUIUtility.Button(theImage, string.Empty, theButtonStyle, theLayout);
	}

	// Token: 0x06000A87 RID: 2695 RVA: 0x00050280 File Offset: 0x0004E480
	public static bool Button(Texture theImage, string theText, KGFGUIUtility.eStyleButton theButtonStyle, params GUILayoutOption[] theLayout)
	{
		GUIContent content;
		if (theImage != null)
		{
			content = new GUIContent(theText, theImage);
		}
		else
		{
			content = new GUIContent(theText);
		}
		KGFGUIUtility.Init();
		if (KGFGUIUtility.itsSkinIndex == -1)
		{
			if (GUILayout.Button(content, theLayout))
			{
				KGFGUIUtility.PlaySound(theButtonStyle.ToString());
				return true;
			}
		}
		else if (GUILayout.Button(content, KGFGUIUtility.GetStyleButton(theButtonStyle), theLayout))
		{
			KGFGUIUtility.PlaySound(theButtonStyle.ToString());
			return true;
		}
		return false;
	}

	// Token: 0x06000A88 RID: 2696 RVA: 0x00050308 File Offset: 0x0004E508
	public static KGFGUIUtility.eCursorState Cursor()
	{
		return KGFGUIUtility.Cursor(null, null, null, null, null);
	}

	// Token: 0x06000A89 RID: 2697 RVA: 0x00050314 File Offset: 0x0004E514
	public static KGFGUIUtility.eCursorState Cursor(Texture theUp, Texture theRight, Texture theDown, Texture theLeft, Texture theCenter)
	{
		float skinHeight = KGFGUIUtility.GetSkinHeight();
		float num = skinHeight * 3f;
		KGFGUIUtility.eCursorState result = KGFGUIUtility.eCursorState.eNone;
		GUILayout.BeginVertical(new GUILayoutOption[]
		{
			GUILayout.ExpandWidth(false),
			GUILayout.ExpandHeight(false)
		});
		KGFGUIUtility.BeginHorizontalBox(KGFGUIUtility.eStyleBox.eBoxInvisible, new GUILayoutOption[0]);
		GUILayout.BeginVertical(new GUILayoutOption[]
		{
			GUILayout.Width(num),
			GUILayout.Height(num)
		});
		GUILayout.BeginHorizontal(new GUILayoutOption[]
		{
			GUILayout.ExpandWidth(false),
			GUILayout.ExpandHeight(false)
		});
		GUILayout.Space(skinHeight);
		if (theUp != null)
		{
			if (KGFGUIUtility.Button(theUp, KGFGUIUtility.eStyleButton.eButtonTop, new GUILayoutOption[]
			{
				GUILayout.Width(skinHeight)
			}))
			{
				result = KGFGUIUtility.eCursorState.eUp;
			}
		}
		else if (KGFGUIUtility.Button(string.Empty, KGFGUIUtility.eStyleButton.eButtonTop, new GUILayoutOption[]
		{
			GUILayout.Width(skinHeight)
		}))
		{
			result = KGFGUIUtility.eCursorState.eUp;
		}
		GUILayout.Space(skinHeight);
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal(new GUILayoutOption[]
		{
			GUILayout.ExpandWidth(false),
			GUILayout.ExpandHeight(false)
		});
		if (theLeft != null)
		{
			if (KGFGUIUtility.Button(theLeft, KGFGUIUtility.eStyleButton.eButtonLeft, new GUILayoutOption[]
			{
				GUILayout.Width(skinHeight)
			}))
			{
				result = KGFGUIUtility.eCursorState.eLeft;
			}
		}
		else if (KGFGUIUtility.Button(string.Empty, KGFGUIUtility.eStyleButton.eButtonLeft, new GUILayoutOption[]
		{
			GUILayout.Width(skinHeight)
		}))
		{
			result = KGFGUIUtility.eCursorState.eLeft;
		}
		if (theCenter != null)
		{
			if (KGFGUIUtility.Button(theCenter, KGFGUIUtility.eStyleButton.eButtonLeft, new GUILayoutOption[]
			{
				GUILayout.Width(skinHeight)
			}))
			{
				result = KGFGUIUtility.eCursorState.eCenter;
			}
		}
		else if (KGFGUIUtility.Button(string.Empty, KGFGUIUtility.eStyleButton.eButtonMiddle, new GUILayoutOption[]
		{
			GUILayout.Width(skinHeight)
		}))
		{
			result = KGFGUIUtility.eCursorState.eCenter;
		}
		if (theRight != null)
		{
			if (KGFGUIUtility.Button(theRight, KGFGUIUtility.eStyleButton.eButtonLeft, new GUILayoutOption[]
			{
				GUILayout.Width(skinHeight)
			}))
			{
				result = KGFGUIUtility.eCursorState.eRight;
			}
		}
		else if (KGFGUIUtility.Button(string.Empty, KGFGUIUtility.eStyleButton.eButtonRight, new GUILayoutOption[]
		{
			GUILayout.Width(skinHeight)
		}))
		{
			result = KGFGUIUtility.eCursorState.eRight;
		}
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal(new GUILayoutOption[]
		{
			GUILayout.ExpandWidth(false),
			GUILayout.ExpandHeight(false)
		});
		GUILayout.Space(skinHeight);
		if (theDown != null)
		{
			if (KGFGUIUtility.Button(theDown, KGFGUIUtility.eStyleButton.eButtonLeft, new GUILayoutOption[]
			{
				GUILayout.Width(skinHeight)
			}))
			{
				result = KGFGUIUtility.eCursorState.eDown;
			}
		}
		else if (KGFGUIUtility.Button(string.Empty, KGFGUIUtility.eStyleButton.eButtonBottom, new GUILayoutOption[]
		{
			GUILayout.Width(skinHeight)
		}))
		{
			result = KGFGUIUtility.eCursorState.eDown;
		}
		GUILayout.Space(skinHeight);
		GUILayout.EndHorizontal();
		GUILayout.EndVertical();
		KGFGUIUtility.EndHorizontalBox();
		GUILayout.EndVertical();
		return result;
	}

	// Token: 0x06000A8A RID: 2698 RVA: 0x00050594 File Offset: 0x0004E794
	public static float HorizontalSlider(float theValue, float theLeftValue, float theRightValue, params GUILayoutOption[] theLayout)
	{
		KGFGUIUtility.Init();
		if (KGFGUIUtility.itsStyleHorizontalSlider != null && KGFGUIUtility.itsStyleHorizontalSliderThumb != null && KGFGUIUtility.itsSkinIndex != -1)
		{
			return GUILayout.HorizontalSlider(theValue, theLeftValue, theRightValue, KGFGUIUtility.itsStyleHorizontalSlider[KGFGUIUtility.itsSkinIndex], KGFGUIUtility.itsStyleHorizontalSliderThumb[KGFGUIUtility.itsSkinIndex], theLayout);
		}
		return GUILayout.HorizontalSlider(theValue, theLeftValue, theRightValue, theLayout);
	}

	// Token: 0x06000A8B RID: 2699 RVA: 0x000505F0 File Offset: 0x0004E7F0
	public static float VerticalSlider(float theValue, float theLeftValue, float theRightValue, params GUILayoutOption[] theLayout)
	{
		KGFGUIUtility.Init();
		if (KGFGUIUtility.itsStyleVerticalSlider != null && KGFGUIUtility.itsStyleVerticalSliderThumb != null && KGFGUIUtility.itsSkinIndex != -1)
		{
			return GUILayout.VerticalSlider(theValue, theLeftValue, theRightValue, KGFGUIUtility.itsStyleVerticalSlider[KGFGUIUtility.itsSkinIndex], KGFGUIUtility.itsStyleVerticalSliderThumb[KGFGUIUtility.itsSkinIndex], theLayout);
		}
		return GUILayout.VerticalSlider(theValue, theLeftValue, theRightValue, theLayout);
	}

	// Token: 0x04000A5A RID: 2650
	private static bool itsEnableKGFSkins = true;

	// Token: 0x04000A5B RID: 2651
	private static string[] itsDefaultGuiSkinPath = new string[]
	{
		"KGFSkins/default/skins/skin_default_16",
		"KGFSkins/default/skins/skin_default_16"
	};

	// Token: 0x04000A5C RID: 2652
	private static int itsSkinIndex = 1;

	// Token: 0x04000A5D RID: 2653
	private static bool[] itsResetPath = new bool[2];

	// Token: 0x04000A5E RID: 2654
	protected static GUISkin[] itsSkin = new GUISkin[2];

	// Token: 0x04000A5F RID: 2655
	private static Texture2D itsIcon = null;

	// Token: 0x04000A60 RID: 2656
	private static Texture2D itsKGFCopyright = null;

	// Token: 0x04000A61 RID: 2657
	private static Texture2D itsIconHelp = null;

	// Token: 0x04000A62 RID: 2658
	private static Dictionary<string, AudioClip> itsAudioClips = new Dictionary<string, AudioClip>();

	// Token: 0x04000A63 RID: 2659
	private static float itsVolume = 1f;

	// Token: 0x04000A64 RID: 2660
	private static GUIStyle[] itsStyleToggle = new GUIStyle[2];

	// Token: 0x04000A65 RID: 2661
	private static GUIStyle[] itsStyleTextField = new GUIStyle[2];

	// Token: 0x04000A66 RID: 2662
	private static GUIStyle[] itsStyleTextFieldLeft = new GUIStyle[2];

	// Token: 0x04000A67 RID: 2663
	private static GUIStyle[] itsStyleTextFieldRight = new GUIStyle[2];

	// Token: 0x04000A68 RID: 2664
	private static GUIStyle[] itsStyleTextArea = new GUIStyle[2];

	// Token: 0x04000A69 RID: 2665
	private static GUIStyle[] itsStyleWindow = new GUIStyle[2];

	// Token: 0x04000A6A RID: 2666
	private static GUIStyle[] itsStyleHorizontalSlider = new GUIStyle[2];

	// Token: 0x04000A6B RID: 2667
	private static GUIStyle[] itsStyleHorizontalSliderThumb = new GUIStyle[2];

	// Token: 0x04000A6C RID: 2668
	private static GUIStyle[] itsStyleVerticalSlider = new GUIStyle[2];

	// Token: 0x04000A6D RID: 2669
	private static GUIStyle[] itsStyleVerticalSliderThumb = new GUIStyle[2];

	// Token: 0x04000A6E RID: 2670
	private static GUIStyle[] itsStyleHorizontalScrollbar = new GUIStyle[2];

	// Token: 0x04000A6F RID: 2671
	private static GUIStyle[] itsStyleHorizontalScrollbarThumb = new GUIStyle[2];

	// Token: 0x04000A70 RID: 2672
	private static GUIStyle[] itsStyleHorizontalScrollbarLeftButton = new GUIStyle[2];

	// Token: 0x04000A71 RID: 2673
	private static GUIStyle[] itsStyleHorizontalScrollbarRightButton = new GUIStyle[2];

	// Token: 0x04000A72 RID: 2674
	private static GUIStyle[] itsStyleVerticalScrollbar = new GUIStyle[2];

	// Token: 0x04000A73 RID: 2675
	private static GUIStyle[] itsStyleVerticalScrollbarThumb = new GUIStyle[2];

	// Token: 0x04000A74 RID: 2676
	private static GUIStyle[] itsStyleVerticalScrollbarUpButton = new GUIStyle[2];

	// Token: 0x04000A75 RID: 2677
	private static GUIStyle[] itsStyleVerticalScrollbarDownButton = new GUIStyle[2];

	// Token: 0x04000A76 RID: 2678
	private static GUIStyle[] itsStyleScrollView = new GUIStyle[2];

	// Token: 0x04000A77 RID: 2679
	private static GUIStyle[] itsStyleMinimap = new GUIStyle[2];

	// Token: 0x04000A78 RID: 2680
	private static GUIStyle[] itsStyleMinimapButton = new GUIStyle[2];

	// Token: 0x04000A79 RID: 2681
	private static GUIStyle[] itsStyleToggleStreched = new GUIStyle[2];

	// Token: 0x04000A7A RID: 2682
	private static GUIStyle[] itsStyleToggleCompact = new GUIStyle[2];

	// Token: 0x04000A7B RID: 2683
	private static GUIStyle[] itsStyleToggleSuperCompact = new GUIStyle[2];

	// Token: 0x04000A7C RID: 2684
	private static GUIStyle[] itsStyleToggleRadioStreched = new GUIStyle[2];

	// Token: 0x04000A7D RID: 2685
	private static GUIStyle[] itsStyleToggleRadioCompact = new GUIStyle[2];

	// Token: 0x04000A7E RID: 2686
	private static GUIStyle[] itsStyleToggleRadioSuperCompact = new GUIStyle[2];

	// Token: 0x04000A7F RID: 2687
	private static GUIStyle[] itsStyleToggleSwitch = new GUIStyle[2];

	// Token: 0x04000A80 RID: 2688
	private static GUIStyle[] itsStyleToggleBoolean = new GUIStyle[2];

	// Token: 0x04000A81 RID: 2689
	private static GUIStyle[] itsStyleToggleArrow = new GUIStyle[2];

	// Token: 0x04000A82 RID: 2690
	private static GUIStyle[] itsStyleToggleButton = new GUIStyle[2];

	// Token: 0x04000A83 RID: 2691
	private static GUIStyle[] itsStyleButton = new GUIStyle[2];

	// Token: 0x04000A84 RID: 2692
	private static GUIStyle[] itsStyleButtonLeft = new GUIStyle[2];

	// Token: 0x04000A85 RID: 2693
	private static GUIStyle[] itsStyleButtonRight = new GUIStyle[2];

	// Token: 0x04000A86 RID: 2694
	private static GUIStyle[] itsStyleButtonTop = new GUIStyle[2];

	// Token: 0x04000A87 RID: 2695
	private static GUIStyle[] itsStyleButtonBottom = new GUIStyle[2];

	// Token: 0x04000A88 RID: 2696
	private static GUIStyle[] itsStyleButtonMiddle = new GUIStyle[2];

	// Token: 0x04000A89 RID: 2697
	private static GUIStyle[] itsStyleBox = new GUIStyle[2];

	// Token: 0x04000A8A RID: 2698
	private static GUIStyle[] itsStyleBoxInvisible = new GUIStyle[2];

	// Token: 0x04000A8B RID: 2699
	private static GUIStyle[] itsStyleBoxInteractive = new GUIStyle[2];

	// Token: 0x04000A8C RID: 2700
	private static GUIStyle[] itsStyleBoxLeft = new GUIStyle[2];

	// Token: 0x04000A8D RID: 2701
	private static GUIStyle[] itsStyleBoxLeftInteractive = new GUIStyle[2];

	// Token: 0x04000A8E RID: 2702
	private static GUIStyle[] itsStyleBoxRight = new GUIStyle[2];

	// Token: 0x04000A8F RID: 2703
	private static GUIStyle[] itsStyleBoxRightInteractive = new GUIStyle[2];

	// Token: 0x04000A90 RID: 2704
	private static GUIStyle[] itsStyleBoxMiddleHorizontal = new GUIStyle[2];

	// Token: 0x04000A91 RID: 2705
	private static GUIStyle[] itsStyleBoxMiddleHorizontalInteractive = new GUIStyle[2];

	// Token: 0x04000A92 RID: 2706
	private static GUIStyle[] itsStyleBoxTop = new GUIStyle[2];

	// Token: 0x04000A93 RID: 2707
	private static GUIStyle[] itsStyleBoxTopInteractive = new GUIStyle[2];

	// Token: 0x04000A94 RID: 2708
	private static GUIStyle[] itsStyleBoxBottom = new GUIStyle[2];

	// Token: 0x04000A95 RID: 2709
	private static GUIStyle[] itsStyleBoxBottomInteractive = new GUIStyle[2];

	// Token: 0x04000A96 RID: 2710
	private static GUIStyle[] itsStyleBoxMiddleVertical = new GUIStyle[2];

	// Token: 0x04000A97 RID: 2711
	private static GUIStyle[] itsStyleBoxMiddleVerticalInteractive = new GUIStyle[2];

	// Token: 0x04000A98 RID: 2712
	private static GUIStyle[] itsStyleBoxDark = new GUIStyle[2];

	// Token: 0x04000A99 RID: 2713
	private static GUIStyle[] itsStyleBoxDarkInteractive = new GUIStyle[2];

	// Token: 0x04000A9A RID: 2714
	private static GUIStyle[] itsStyleBoxDarkLeft = new GUIStyle[2];

	// Token: 0x04000A9B RID: 2715
	private static GUIStyle[] itsStyleBoxDarkLeftInteractive = new GUIStyle[2];

	// Token: 0x04000A9C RID: 2716
	private static GUIStyle[] itsStyleBoxDarkRight = new GUIStyle[2];

	// Token: 0x04000A9D RID: 2717
	private static GUIStyle[] itsStyleBoxDarkRightInteractive = new GUIStyle[2];

	// Token: 0x04000A9E RID: 2718
	private static GUIStyle[] itsStyleBoxDarkMiddleHorizontal = new GUIStyle[2];

	// Token: 0x04000A9F RID: 2719
	private static GUIStyle[] itsStyleBoxDarkMiddleHorizontalInteractive = new GUIStyle[2];

	// Token: 0x04000AA0 RID: 2720
	private static GUIStyle[] itsStyleBoxDarkTop = new GUIStyle[2];

	// Token: 0x04000AA1 RID: 2721
	private static GUIStyle[] itsStyleBoxDarkTopInteractive = new GUIStyle[2];

	// Token: 0x04000AA2 RID: 2722
	private static GUIStyle[] itsStyleBoxDarkBottom = new GUIStyle[2];

	// Token: 0x04000AA3 RID: 2723
	private static GUIStyle[] itsStyleBoxDarkBottomInteractive = new GUIStyle[2];

	// Token: 0x04000AA4 RID: 2724
	private static GUIStyle[] itsStyleBoxDarkMiddleVertical = new GUIStyle[2];

	// Token: 0x04000AA5 RID: 2725
	private static GUIStyle[] itsStyleBoxDarkMiddleVerticalInteractive = new GUIStyle[2];

	// Token: 0x04000AA6 RID: 2726
	private static GUIStyle[] itsStyleBoxDecorated = new GUIStyle[2];

	// Token: 0x04000AA7 RID: 2727
	private static GUIStyle[] itsStyleSeparatorVertical = new GUIStyle[2];

	// Token: 0x04000AA8 RID: 2728
	private static GUIStyle[] itsStyleSeparatorVerticalFitInBox = new GUIStyle[2];

	// Token: 0x04000AA9 RID: 2729
	private static GUIStyle[] itsStyleSeparatorHorizontal = new GUIStyle[2];

	// Token: 0x04000AAA RID: 2730
	private static GUIStyle[] itsStyleLabel = new GUIStyle[2];

	// Token: 0x04000AAB RID: 2731
	private static GUIStyle[] itsStyleLabelMultiline = new GUIStyle[2];

	// Token: 0x04000AAC RID: 2732
	private static GUIStyle[] itsStyleLabelTitle = new GUIStyle[2];

	// Token: 0x04000AAD RID: 2733
	private static GUIStyle[] itsStyleLabelFitInToBox = new GUIStyle[2];

	// Token: 0x04000AAE RID: 2734
	private static GUIStyle[] itsStyleTable = new GUIStyle[2];

	// Token: 0x04000AAF RID: 2735
	private static GUIStyle[] itsStyleTableHeadingRow = new GUIStyle[2];

	// Token: 0x04000AB0 RID: 2736
	private static GUIStyle[] itsStyleTableHeadingCell = new GUIStyle[2];

	// Token: 0x04000AB1 RID: 2737
	private static GUIStyle[] itsStyleTableRow = new GUIStyle[2];

	// Token: 0x04000AB2 RID: 2738
	private static GUIStyle[] itsStyleTableRowCell = new GUIStyle[2];

	// Token: 0x04000AB3 RID: 2739
	private static GUIStyle[] itsStyleCursor = new GUIStyle[2];

	// Token: 0x02000162 RID: 354
	public enum eStyleButton
	{
		// Token: 0x04000AB5 RID: 2741
		eButton,
		// Token: 0x04000AB6 RID: 2742
		eButtonLeft,
		// Token: 0x04000AB7 RID: 2743
		eButtonRight,
		// Token: 0x04000AB8 RID: 2744
		eButtonTop,
		// Token: 0x04000AB9 RID: 2745
		eButtonBottom,
		// Token: 0x04000ABA RID: 2746
		eButtonMiddle
	}

	// Token: 0x02000163 RID: 355
	public enum eStyleToggl
	{
		// Token: 0x04000ABC RID: 2748
		eToggl,
		// Token: 0x04000ABD RID: 2749
		eTogglStreched,
		// Token: 0x04000ABE RID: 2750
		eTogglCompact,
		// Token: 0x04000ABF RID: 2751
		eTogglSuperCompact,
		// Token: 0x04000AC0 RID: 2752
		eTogglRadioStreched,
		// Token: 0x04000AC1 RID: 2753
		eTogglRadioCompact,
		// Token: 0x04000AC2 RID: 2754
		eTogglRadioSuperCompact,
		// Token: 0x04000AC3 RID: 2755
		eTogglSwitch,
		// Token: 0x04000AC4 RID: 2756
		eTogglBoolean,
		// Token: 0x04000AC5 RID: 2757
		eTogglArrow,
		// Token: 0x04000AC6 RID: 2758
		eTogglButton
	}

	// Token: 0x02000164 RID: 356
	public enum eStyleTextField
	{
		// Token: 0x04000AC8 RID: 2760
		eTextField,
		// Token: 0x04000AC9 RID: 2761
		eTextFieldLeft,
		// Token: 0x04000ACA RID: 2762
		eTextFieldRight
	}

	// Token: 0x02000165 RID: 357
	public enum eStyleBox
	{
		// Token: 0x04000ACC RID: 2764
		eBox,
		// Token: 0x04000ACD RID: 2765
		eBoxInvisible,
		// Token: 0x04000ACE RID: 2766
		eBoxInteractive,
		// Token: 0x04000ACF RID: 2767
		eBoxLeft,
		// Token: 0x04000AD0 RID: 2768
		eBoxLeftInteractive,
		// Token: 0x04000AD1 RID: 2769
		eBoxRight,
		// Token: 0x04000AD2 RID: 2770
		eBoxRightInteractive,
		// Token: 0x04000AD3 RID: 2771
		eBoxMiddleHorizontal,
		// Token: 0x04000AD4 RID: 2772
		eBoxMiddleHorizontalInteractive,
		// Token: 0x04000AD5 RID: 2773
		eBoxTop,
		// Token: 0x04000AD6 RID: 2774
		eBoxTopInteractive,
		// Token: 0x04000AD7 RID: 2775
		eBoxMiddleVertical,
		// Token: 0x04000AD8 RID: 2776
		eBoxMiddleVerticalInteractive,
		// Token: 0x04000AD9 RID: 2777
		eBoxBottom,
		// Token: 0x04000ADA RID: 2778
		eBoxBottomInteractive,
		// Token: 0x04000ADB RID: 2779
		eBoxDark,
		// Token: 0x04000ADC RID: 2780
		eBoxDarkInteractive,
		// Token: 0x04000ADD RID: 2781
		eBoxDarkLeft,
		// Token: 0x04000ADE RID: 2782
		eBoxDarkLeftInteractive,
		// Token: 0x04000ADF RID: 2783
		eBoxDarkRight,
		// Token: 0x04000AE0 RID: 2784
		eBoxDarkRightInteractive,
		// Token: 0x04000AE1 RID: 2785
		eBoxDarkMiddleHorizontal,
		// Token: 0x04000AE2 RID: 2786
		eBoxDarkMiddleHorizontalInteractive,
		// Token: 0x04000AE3 RID: 2787
		eBoxDarkTop,
		// Token: 0x04000AE4 RID: 2788
		eBoxDarkTopInteractive,
		// Token: 0x04000AE5 RID: 2789
		eBoxDarkBottom,
		// Token: 0x04000AE6 RID: 2790
		eBoxDarkBottomInteractive,
		// Token: 0x04000AE7 RID: 2791
		eBoxDarkMiddleVertical,
		// Token: 0x04000AE8 RID: 2792
		eBoxDarkMiddleVerticalInteractive,
		// Token: 0x04000AE9 RID: 2793
		eBoxDecorated
	}

	// Token: 0x02000166 RID: 358
	public enum eStyleSeparator
	{
		// Token: 0x04000AEB RID: 2795
		eSeparatorHorizontal,
		// Token: 0x04000AEC RID: 2796
		eSeparatorVertical,
		// Token: 0x04000AED RID: 2797
		eSeparatorVerticalFitInBox
	}

	// Token: 0x02000167 RID: 359
	public enum eStyleLabel
	{
		// Token: 0x04000AEF RID: 2799
		eLabel,
		// Token: 0x04000AF0 RID: 2800
		eLabelMultiline,
		// Token: 0x04000AF1 RID: 2801
		eLabelTitle,
		// Token: 0x04000AF2 RID: 2802
		eLabelFitIntoBox
	}

	// Token: 0x02000168 RID: 360
	public enum eStyleImage
	{
		// Token: 0x04000AF4 RID: 2804
		eImage,
		// Token: 0x04000AF5 RID: 2805
		eImageFitIntoBox
	}

	// Token: 0x02000169 RID: 361
	public enum eCursorState
	{
		// Token: 0x04000AF7 RID: 2807
		eUp,
		// Token: 0x04000AF8 RID: 2808
		eRight,
		// Token: 0x04000AF9 RID: 2809
		eDown,
		// Token: 0x04000AFA RID: 2810
		eLeft,
		// Token: 0x04000AFB RID: 2811
		eCenter,
		// Token: 0x04000AFC RID: 2812
		eNone
	}
}
