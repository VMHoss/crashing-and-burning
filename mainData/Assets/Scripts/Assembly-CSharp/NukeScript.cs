using System;
using UnityEngine;

// Token: 0x0200010B RID: 267
public class NukeScript : MonoBehaviour
{
	// Token: 0x060007D5 RID: 2005 RVA: 0x0003B330 File Offset: 0x00039530
	private void Awake()
	{
		Transform transform = base.transform.FindChild("NukeFire");
		transform.gameObject.AddComponent<TextureScrollScript>().ScrollVector = new Vector2(0.05f, -0.5f);
		Transform transform2 = base.transform.FindChild("NukeSmoke");
		transform2.gameObject.AddComponent<TextureScrollScript>().ScrollVector = new Vector2(0f, -0.5f);
		GameObject gameObject = GameObject.Find("Directional light");
		this.pLightComponent = gameObject.GetComponent<Light>();
		this.pLightComponent.intensity = 4f;
		GenericFunctionsScript.Fade("FromWhiteNuclear");
	}

	// Token: 0x060007D6 RID: 2006 RVA: 0x0003B3D0 File Offset: 0x000395D0
	private void Update()
	{
		if (this.tPassedTime < 3f)
		{
			this.tPassedTime += Time.deltaTime;
			this.pLightComponent.intensity = Mathf.Max(1f, 4f - this.tPassedTime * 1.333f);
			base.transform.localScale = new Vector3(1f + this.tPassedTime * 0.1f, 1f + this.tPassedTime * 0.4f, 1f + this.tPassedTime * 0.1f);
		}
	}

	// Token: 0x0400085B RID: 2139
	private float tPassedTime;

	// Token: 0x0400085C RID: 2140
	private Light pLightComponent;
}
