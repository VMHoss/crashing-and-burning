using System;
using UnityEngine;

// Token: 0x02000099 RID: 153
public class AndroidButton : MonoBehaviour
{
	// Token: 0x060004F2 RID: 1266 RVA: 0x00023C88 File Offset: 0x00021E88
	private void Awake()
	{
		if (Data.platform != "Android")
		{
			UnityEngine.Object.Destroy(this);
		}
	}

	// Token: 0x060004F3 RID: 1267 RVA: 0x00023CA4 File Offset: 0x00021EA4
	private void Update()
	{
		if (this.backKey && (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyUp(KeyCode.Alpha9)))
		{
			this.SendButtonMessage();
		}
		if (this.settingsKey && (Input.GetKeyDown(KeyCode.Menu) || Input.GetKeyUp(KeyCode.Alpha0)))
		{
			this.SendButtonMessage();
		}
	}

	// Token: 0x060004F4 RID: 1268 RVA: 0x00023D08 File Offset: 0x00021F08
	private void SendButtonMessage()
	{
		if (base.gameObject.GetComponent<UIButtonMessage>())
		{
			Debug.Log("Keybutton: " + base.gameObject.name);
		}
		if (Scripts.interfaceScript != null)
		{
			Scripts.interfaceScript.OnButton(base.gameObject);
		}
		else
		{
			Debug.LogWarning("Cannot perform OnButton for button " + base.gameObject.name);
		}
	}

	// Token: 0x04000507 RID: 1287
	public bool backKey;

	// Token: 0x04000508 RID: 1288
	public bool settingsKey;

	// Token: 0x04000509 RID: 1289
	private InterfaceScript interfaceScript;
}
