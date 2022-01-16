using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000A0 RID: 160
public class FlashScript : MonoBehaviour
{
	// Token: 0x0600050B RID: 1291 RVA: 0x000242A0 File Offset: 0x000224A0
	private void Start()
	{
		this.myName = base.gameObject.name;
		string text = this.myName;
		if (text != null)
		{
			if (FlashScript.<>f__switch$map0 == null)
			{
				FlashScript.<>f__switch$map0 = new Dictionary<string, int>(2)
				{
					{
						"White",
						0
					},
					{
						"Black",
						1
					}
				};
			}
			int num;
			if (FlashScript.<>f__switch$map0.TryGetValue(text, out num))
			{
				if (num != 0)
				{
					if (num == 1)
					{
						UnityEngine.Object.Destroy(base.gameObject, 1f);
					}
				}
				else
				{
					UnityEngine.Object.Destroy(base.gameObject, 3f);
				}
			}
		}
	}

	// Token: 0x0600050C RID: 1292 RVA: 0x00024348 File Offset: 0x00022548
	private void OnGUI()
	{
		string text = this.myName;
		if (text != null)
		{
			if (FlashScript.<>f__switch$map1 == null)
			{
				FlashScript.<>f__switch$map1 = new Dictionary<string, int>(2)
				{
					{
						"White",
						0
					},
					{
						"Black",
						1
					}
				};
			}
			int num;
			if (FlashScript.<>f__switch$map1.TryGetValue(text, out num))
			{
				if (num != 0)
				{
					if (num == 1)
					{
						this.blackFadeValue = Mathf.Clamp01(this.blackFadeValue + Time.deltaTime / this.blackTime);
						GUI.color = new Color(0f, 0f, 0f, this.blackFadeValue);
						GUI.DrawTexture(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height), this.filterTexture);
					}
				}
				else
				{
					this.alphaFadeValue = Mathf.Clamp01(this.alphaFadeValue - Time.deltaTime / this.fadeTime);
					GUI.color = new Color(1f, 1f, 1f, this.alphaFadeValue);
					GUI.DrawTexture(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height), this.filterTexture);
				}
			}
		}
	}

	// Token: 0x0400051F RID: 1311
	private float fadeTime = 2f;

	// Token: 0x04000520 RID: 1312
	private float blackTime = 1f;

	// Token: 0x04000521 RID: 1313
	private float alphaFadeValue = 1f;

	// Token: 0x04000522 RID: 1314
	private float blackFadeValue;

	// Token: 0x04000523 RID: 1315
	private string myName;

	// Token: 0x04000524 RID: 1316
	public Texture filterTexture;
}
