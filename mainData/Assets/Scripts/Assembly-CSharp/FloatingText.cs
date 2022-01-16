using System;
using UnityEngine;

// Token: 0x020000A1 RID: 161
public class FloatingText : MonoBehaviour
{
	// Token: 0x0600050E RID: 1294 RVA: 0x00024490 File Offset: 0x00022690
	private void Awake()
	{
		this._t = base.transform;
		this._lbl = base.GetComponent<UILabel>();
		Vector3 localScale = this._t.localScale;
		this.Size = localScale;
		if (this._lbl == null)
		{
			Debug.LogError("Could not find the label for the floating text.");
		}
	}

	// Token: 0x0600050F RID: 1295 RVA: 0x000244E4 File Offset: 0x000226E4
	private void Start()
	{
		this.guiCamera = NGUITools.FindCameraForLayer(base.gameObject.layer);
		this.DestroyMe();
	}

	// Token: 0x170000DF RID: 223
	// (get) Token: 0x06000510 RID: 1296 RVA: 0x00024504 File Offset: 0x00022704
	// (set) Token: 0x06000511 RID: 1297 RVA: 0x00024514 File Offset: 0x00022714
	public Color TextColor
	{
		get
		{
			return this._lbl.color;
		}
		set
		{
			this._lbl.color = value;
		}
	}

	// Token: 0x170000E0 RID: 224
	// (get) Token: 0x06000512 RID: 1298 RVA: 0x00024524 File Offset: 0x00022724
	// (set) Token: 0x06000513 RID: 1299 RVA: 0x00024534 File Offset: 0x00022734
	public string Text
	{
		get
		{
			return this._lbl.text;
		}
		set
		{
			this._lbl.text = value;
		}
	}

	// Token: 0x170000E1 RID: 225
	// (get) Token: 0x06000514 RID: 1300 RVA: 0x00024544 File Offset: 0x00022744
	// (set) Token: 0x06000515 RID: 1301 RVA: 0x0002454C File Offset: 0x0002274C
	public bool FollowTarget
	{
		get
		{
			return this._followTarget;
		}
		set
		{
			this._followTarget = value;
		}
	}

	// Token: 0x170000E2 RID: 226
	// (get) Token: 0x06000516 RID: 1302 RVA: 0x00024558 File Offset: 0x00022758
	// (set) Token: 0x06000517 RID: 1303 RVA: 0x00024560 File Offset: 0x00022760
	public GameObject Target
	{
		get
		{
			return this._target;
		}
		set
		{
			this._target = value;
			this.worldCamera = NGUITools.FindCameraForLayer(this._target.layer);
		}
	}

	// Token: 0x170000E3 RID: 227
	// (get) Token: 0x06000518 RID: 1304 RVA: 0x00024580 File Offset: 0x00022780
	// (set) Token: 0x06000519 RID: 1305 RVA: 0x00024590 File Offset: 0x00022790
	public UIFont Font
	{
		get
		{
			return this._lbl.font;
		}
		set
		{
			this._lbl.font = value;
		}
	}

	// Token: 0x170000E4 RID: 228
	// (get) Token: 0x0600051A RID: 1306 RVA: 0x000245A0 File Offset: 0x000227A0
	// (set) Token: 0x0600051B RID: 1307 RVA: 0x000245B4 File Offset: 0x000227B4
	public Vector3 Size
	{
		get
		{
			return this._lbl.transform.localScale;
		}
		set
		{
			this._lbl.transform.localScale = value;
		}
	}

	// Token: 0x0600051C RID: 1308 RVA: 0x000245C8 File Offset: 0x000227C8
	public void Init(string txt, Color clr, GameObject go)
	{
		this.TextColor = clr;
		this.Text = txt;
		this.Target = go;
	}

	// Token: 0x0600051D RID: 1309 RVA: 0x000245E0 File Offset: 0x000227E0
	public void SpawnAt(GameObject target, Vector3 size, Transform parent)
	{
		this.Target = target;
		this._t.parent = parent;
		this.Size = size;
	}

	// Token: 0x0600051E RID: 1310 RVA: 0x000245FC File Offset: 0x000227FC
	private void Following()
	{
		Vector3 position = this.worldCamera.WorldToViewportPoint(this._target.transform.position);
		position = this.guiCamera.ViewportToWorldPoint(position);
		position.z = 0f;
		this._t.position = position;
	}

	// Token: 0x0600051F RID: 1311 RVA: 0x0002464C File Offset: 0x0002284C
	public void LateUpdate()
	{
	}

	// Token: 0x06000520 RID: 1312 RVA: 0x00024650 File Offset: 0x00022850
	public void DestroyMe()
	{
		UnityEngine.Object.Destroy(base.gameObject, 1f);
	}

	// Token: 0x04000527 RID: 1319
	private UILabel _lbl;

	// Token: 0x04000528 RID: 1320
	private bool _followTarget;

	// Token: 0x04000529 RID: 1321
	private Vector3 _pos;

	// Token: 0x0400052A RID: 1322
	public Transform parent;

	// Token: 0x0400052B RID: 1323
	public GameObject _target;

	// Token: 0x0400052C RID: 1324
	public Camera worldCamera;

	// Token: 0x0400052D RID: 1325
	public Camera guiCamera;

	// Token: 0x0400052E RID: 1326
	private Transform _t;
}
