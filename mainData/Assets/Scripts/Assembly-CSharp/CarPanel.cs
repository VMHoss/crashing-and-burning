using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200009D RID: 157
public class CarPanel : MonoBehaviour
{
	// Token: 0x060004FE RID: 1278 RVA: 0x00023FA8 File Offset: 0x000221A8
	private void Awake()
	{
		this.pColumnList = new string[][]
		{
			this.column1,
			this.column2,
			this.column3,
			this.column4,
			this.column5,
			this.column6
		};
	}

	// Token: 0x060004FF RID: 1279 RVA: 0x00023FF8 File Offset: 0x000221F8
	public void UpdateCarButtons()
	{
		GameObject gameObject = GameObject.Find("CarButtons");
		foreach (object obj in gameObject.transform)
		{
			Transform transform = (Transform)obj;
			if (transform.GetComponent<TweenScale>())
			{
				UnityEngine.Object.Destroy(transform.GetComponent<TweenScale>());
			}
			transform.localScale = new Vector3(0f, 0f, 0f);
		}
		base.StartCoroutine(this.CarButtonsSequence());
	}

	// Token: 0x06000500 RID: 1280 RVA: 0x000240B0 File Offset: 0x000222B0
	private IEnumerator CarButtonsSequence()
	{
		this._cNumber = 0;
		foreach (string[] column in this.pColumnList)
		{
			for (int i = 0; i < 4; i++)
			{
				this.ProcessButton(i, column[i]);
				yield return new WaitForSeconds(this._interval);
			}
			this._cNumber++;
		}
		yield break;
	}

	// Token: 0x06000501 RID: 1281 RVA: 0x000240CC File Offset: 0x000222CC
	private void ProcessButton(int i, string carName)
	{
		this._carButton = GenericFunctionsScript.FindChild(GameObject.Find("CarButtons"), carName);
		this.SetCarButtonSkin(carName);
		this._t = this._carButton.transform;
		this._pos.x = (float)(this._startX + this._columnSpacing * this._cNumber);
		this._pos.y = (float)(this._startY + i * this._rowSpacing);
		this._t.localPosition = this._pos;
		TweenScale tweenScale = this._carButton.AddComponent<TweenScale>();
		tweenScale.duration = 0.08f;
		tweenScale.from = new Vector3(0f, 0f, 0f);
		tweenScale.method = UITweener.Method.EaseOut;
	}

	// Token: 0x06000502 RID: 1282 RVA: 0x0002418C File Offset: 0x0002238C
	public void SetCarButtonSkin(string carName)
	{
		this._skinNumber = 1;
		this._sprite = GenericFunctionsScript.FindChild(GameObject.Find(carName), "Image").GetComponent<UISprite>();
		this._sprite.spriteName = carName + "Skin" + this._skinNumber.ToString() + "_Texture";
		Debug.Log(string.Concat(new string[]
		{
			"Skin set for ",
			carName,
			": ",
			carName,
			"Skin",
			this._skinNumber.ToString(),
			"_Texture"
		}));
	}

	// Token: 0x0400050D RID: 1293
	private Vector3 _pos;

	// Token: 0x0400050E RID: 1294
	private Transform _t;

	// Token: 0x0400050F RID: 1295
	private UISprite _sprite;

	// Token: 0x04000510 RID: 1296
	private int _skinNumber;

	// Token: 0x04000511 RID: 1297
	private int _columnSpacing = 74;

	// Token: 0x04000512 RID: 1298
	private int _rowSpacing = -74;

	// Token: 0x04000513 RID: 1299
	private int _startX = -148;

	// Token: 0x04000514 RID: 1300
	private int _startY = 110;

	// Token: 0x04000515 RID: 1301
	private int _cNumber;

	// Token: 0x04000516 RID: 1302
	private float _interval = 0.03f;

	// Token: 0x04000517 RID: 1303
	private GameObject _carButton;

	// Token: 0x04000518 RID: 1304
	private string[] column1 = new string[]
	{
		"Chisai",
		"ADP",
		"Paiker",
		"R53"
	};

	// Token: 0x04000519 RID: 1305
	private string[] column2 = new string[]
	{
		"Taxi",
		"Police",
		"Hummer",
		"BigTruck"
	};

	// Token: 0x0400051A RID: 1306
	private string[] column3 = new string[]
	{
		"Eldorado",
		"ChevyNova",
		"Rebel",
		"Camaro"
	};

	// Token: 0x0400051B RID: 1307
	private string[] column4 = new string[]
	{
		"Vice",
		"Murcielago",
		"Panini",
		"Sunstorm"
	};

	// Token: 0x0400051C RID: 1308
	private string[] column5 = new string[]
	{
		"BajaBeetle",
		"Baja4A",
		"Baja1000",
		"MonsterTruck"
	};

	// Token: 0x0400051D RID: 1309
	private string[] column6 = new string[]
	{
		"T1",
		"IceCreamTruck",
		"HotRod",
		"Schoolbus"
	};

	// Token: 0x0400051E RID: 1310
	private string[][] pColumnList;
}
