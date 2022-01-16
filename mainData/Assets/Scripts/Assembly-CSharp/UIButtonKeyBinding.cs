using System;
using UnityEngine;

// Token: 0x02000022 RID: 34
[AddComponentMenu("Game/UI/Button Key Binding")]
public class UIButtonKeyBinding : MonoBehaviour
{
	// Token: 0x0600015E RID: 350 RVA: 0x00009578 File Offset: 0x00007778
	private void Update()
	{
		if (!UICamera.inputHasFocus)
		{
			if (this.keyCode == KeyCode.None)
			{
				return;
			}
			if (Input.GetKeyDown(this.keyCode))
			{
				base.SendMessage("OnPress", true, SendMessageOptions.DontRequireReceiver);
			}
			if (Input.GetKeyUp(this.keyCode))
			{
				base.SendMessage("OnPress", false, SendMessageOptions.DontRequireReceiver);
				base.SendMessage("OnClick", SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	// Token: 0x0400018F RID: 399
	public KeyCode keyCode;
}
