using System;
using UnityEngine;

// Token: 0x0200009C RID: 156
public class CarContainer_Script : MonoBehaviour
{
	// Token: 0x060004FB RID: 1275 RVA: 0x00023E04 File Offset: 0x00022004
	private void Awake()
	{
		this.target = base.GetComponent<UISprite>();
		if (this.target == null)
		{
			Debug.Log("Component not found!");
			return;
		}
		this.target.spriteName = "CarOne1";
	}

	// Token: 0x060004FC RID: 1276 RVA: 0x00023E4C File Offset: 0x0002204C
	public void UpdateCar(string x)
	{
		this.target.spriteName = x;
	}

	// Token: 0x0400050B RID: 1291
	public UISprite target;

	// Token: 0x0400050C RID: 1292
	private string newCar;
}
