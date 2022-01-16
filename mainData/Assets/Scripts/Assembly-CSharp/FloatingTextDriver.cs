using System;
using UnityEngine;

// Token: 0x020000A2 RID: 162
public class FloatingTextDriver : MonoBehaviour
{
	// Token: 0x06000522 RID: 1314 RVA: 0x0002466C File Offset: 0x0002286C
	public void OnClick()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(this.cashPrefab, base.transform.position, base.transform.rotation) as GameObject;
		this._t = gameObject.GetComponent<Transform>();
		this._t.parent = this.parent;
		this._lbl = gameObject.GetComponent<UILabel>();
		this._lbl.transform.localScale = new Vector3(32f, 32f, 1f);
	}

	// Token: 0x0400052F RID: 1327
	public Transform parent;

	// Token: 0x04000530 RID: 1328
	public GameObject cashPrefab;

	// Token: 0x04000531 RID: 1329
	private Transform _t;

	// Token: 0x04000532 RID: 1330
	private UILabel _lbl;
}
