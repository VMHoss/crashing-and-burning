using System;
using UnityEngine;

// Token: 0x0200001B RID: 27
public class C_EasyButtonTemplate : MonoBehaviour
{
	// Token: 0x06000110 RID: 272 RVA: 0x00008674 File Offset: 0x00006874
	private void OnEnable()
	{
		EasyButton.On_ButtonDown += this.On_ButtonDown;
		EasyButton.On_ButtonPress += this.On_ButtonPress;
		EasyButton.On_ButtonUp += this.On_ButtonUp;
	}

	// Token: 0x06000111 RID: 273 RVA: 0x000086AC File Offset: 0x000068AC
	private void OnDisable()
	{
		EasyButton.On_ButtonDown -= this.On_ButtonDown;
		EasyButton.On_ButtonPress -= this.On_ButtonPress;
		EasyButton.On_ButtonUp -= this.On_ButtonUp;
	}

	// Token: 0x06000112 RID: 274 RVA: 0x000086E4 File Offset: 0x000068E4
	private void OnDestroy()
	{
		EasyButton.On_ButtonDown -= this.On_ButtonDown;
		EasyButton.On_ButtonPress -= this.On_ButtonPress;
		EasyButton.On_ButtonUp -= this.On_ButtonUp;
	}

	// Token: 0x06000113 RID: 275 RVA: 0x0000871C File Offset: 0x0000691C
	private void On_ButtonDown(string buttonName)
	{
	}

	// Token: 0x06000114 RID: 276 RVA: 0x00008720 File Offset: 0x00006920
	private void On_ButtonPress(string buttonName)
	{
	}

	// Token: 0x06000115 RID: 277 RVA: 0x00008724 File Offset: 0x00006924
	private void On_ButtonUp(string buttonName)
	{
	}
}
