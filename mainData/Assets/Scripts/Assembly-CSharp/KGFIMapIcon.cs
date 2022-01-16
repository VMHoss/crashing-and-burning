using System;
using UnityEngine;

// Token: 0x02000198 RID: 408
public interface KGFIMapIcon
{
	// Token: 0x06000BE5 RID: 3045
	string GetCategory();

	// Token: 0x06000BE6 RID: 3046
	Color GetColor();

	// Token: 0x06000BE7 RID: 3047
	Texture2D GetTextureArrow();

	// Token: 0x06000BE8 RID: 3048
	bool GetRotate();

	// Token: 0x06000BE9 RID: 3049
	bool GetIsVisible();

	// Token: 0x06000BEA RID: 3050
	bool GetIsArrowVisible();

	// Token: 0x06000BEB RID: 3051
	Transform GetTransform();

	// Token: 0x06000BEC RID: 3052
	string GetGameObjectName();

	// Token: 0x06000BED RID: 3053
	GameObject GetRepresentation();

	// Token: 0x06000BEE RID: 3054
	string GetToolTipText();

	// Token: 0x06000BEF RID: 3055
	float GetIconScale();

	// Token: 0x06000BF0 RID: 3056
	bool GetIsBlinking();

	// Token: 0x06000BF1 RID: 3057
	void SetIsBlinking(bool theActivate);
}
