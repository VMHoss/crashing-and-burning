using System;
using UnityEngine;

// Token: 0x020000A7 RID: 167
public class KeyButton : MonoBehaviour
{
	// Token: 0x06000543 RID: 1347 RVA: 0x00025C90 File Offset: 0x00023E90
	private void Start()
	{
	}

	// Token: 0x06000544 RID: 1348 RVA: 0x00025C94 File Offset: 0x00023E94
	private void Update()
	{
		if (this.escapeKey && Input.GetKeyDown(KeyCode.Escape))
		{
			this.SendButtonMessage();
		}
		if (this.backspaceKey && Input.GetKeyDown(KeyCode.Backspace))
		{
			this.SendButtonMessage();
		}
		if (this.returnKey && Input.GetKeyDown(KeyCode.Return))
		{
			this.SendButtonMessage();
		}
		if (this.stringKey && Input.GetKeyUp(this.key))
		{
			this.SendButtonMessage();
		}
	}

	// Token: 0x06000545 RID: 1349 RVA: 0x00025D18 File Offset: 0x00023F18
	private void SendButtonMessage()
	{
		if (base.gameObject.GetComponent<UIButtonMessage>())
		{
			Debug.Log("Keybutton: " + base.gameObject.name);
		}
		this.interfaceScript = GameObject.Find("Interface").GetComponent<InterfaceScript>();
		this.interfaceScript.OnButton(base.gameObject);
	}

	// Token: 0x04000566 RID: 1382
	public string key = "space";

	// Token: 0x04000567 RID: 1383
	public bool stringKey = true;

	// Token: 0x04000568 RID: 1384
	public bool escapeKey;

	// Token: 0x04000569 RID: 1385
	public bool backspaceKey;

	// Token: 0x0400056A RID: 1386
	public bool returnKey;

	// Token: 0x0400056B RID: 1387
	private InterfaceScript interfaceScript;
}
