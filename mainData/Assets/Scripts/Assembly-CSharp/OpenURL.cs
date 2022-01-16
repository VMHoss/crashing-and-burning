using System;
using UnityEngine;

// Token: 0x020000B0 RID: 176
public class OpenURL : MonoBehaviour
{
	// Token: 0x06000573 RID: 1395 RVA: 0x00026B7C File Offset: 0x00024D7C
	private void Start()
	{
		if (!Data.clickLinks)
		{
			UnityEngine.Object.Destroy(base.gameObject.GetComponent<BoxCollider>());
		}
		if (this.label != null)
		{
			this.myURL = this.label.text;
			this.absoluteURL = Application.absoluteURL;
			this.domainName = this.absoluteURL.Replace("http://", string.Empty);
			if (this.domainName.IndexOf("/") >= 0)
			{
				this.domainName = this.domainName.Substring(0, this.domainName.IndexOf("/"));
			}
		}
		else
		{
			this.localization = GameObject.Find("Localization").GetComponent<Localization>();
			this.myURL = this.localization.Get("BrandingURL");
		}
	}

	// Token: 0x06000574 RID: 1396 RVA: 0x00026C54 File Offset: 0x00024E54
	private void OnClick()
	{
		this.myURL = this.localization.Get("BrandingURL");
		Debug.Log("DomainName: " + this.domainName);
		Debug.Log("The button for the link is pressed, myEval:" + this.myEval);
		if (Application.platform == RuntimePlatform.WindowsWebPlayer || Application.platform == RuntimePlatform.OSXWebPlayer)
		{
			this.myEval = "window.open(\"" + this.myURL + "\", \"_blank\")";
			Application.ExternalEval(this.myEval);
		}
		else
		{
			Application.OpenURL(this.myURL);
		}
	}

	// Token: 0x040005B5 RID: 1461
	private string myURL;

	// Token: 0x040005B6 RID: 1462
	private string absoluteURL;

	// Token: 0x040005B7 RID: 1463
	private string domainName;

	// Token: 0x040005B8 RID: 1464
	private string myEval;

	// Token: 0x040005B9 RID: 1465
	public UILabel label;

	// Token: 0x040005BA RID: 1466
	public Localization localization;
}
