using System;
using UnityEngine;

// Token: 0x02000155 RID: 341
public interface KGFICustomGUI
{
	// Token: 0x060009D2 RID: 2514
	string GetName();

	// Token: 0x060009D3 RID: 2515
	string GetHeaderName();

	// Token: 0x060009D4 RID: 2516
	Texture2D GetIcon();

	// Token: 0x060009D5 RID: 2517
	void Render();
}
