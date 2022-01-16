using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000CB RID: 203
[RequireComponent(typeof(Camera))]
public class FPSScript : MonoBehaviour
{
	// Token: 0x0600060E RID: 1550 RVA: 0x0002BB68 File Offset: 0x00029D68
	private void Awake()
	{
		this.pPastFrameRates = new List<float>();
		for (int i = 0; i < 10; i++)
		{
			this.pPastFrameRates.Add(0f);
		}
	}

	// Token: 0x0600060F RID: 1551 RVA: 0x0002BBA4 File Offset: 0x00029DA4
	private void OnGUI()
	{
		GUI.TextArea(new Rect(0f, 0f, 80f, 30f), this.pLastFramerate.ToString());
		GUI.TextArea(new Rect(0f, 31f, 80f, 30f), this.pAverageFrameRate.ToString());
	}

	// Token: 0x06000610 RID: 1552 RVA: 0x0002BC08 File Offset: 0x00029E08
	private void Update()
	{
		if (this.pTimeCounter < this.refreshTime)
		{
			this.pTimeCounter += Time.deltaTime;
			this.pFrameCounter++;
		}
		else
		{
			this.pLastFramerate = (float)this.pFrameCounter / this.pTimeCounter;
			this.pPastFrameRates[this.pPastFrameRatesIndex] = this.pLastFramerate;
			this.pPastFrameRatesIndex = ++this.pPastFrameRatesIndex % 10;
			this.pAverageFrameRate = 0f;
			foreach (float num in this.pPastFrameRates)
			{
				float num2 = num;
				this.pAverageFrameRate += num2;
			}
			this.pAverageFrameRate /= 10f;
			this.pFrameCounter = 0;
			this.pTimeCounter = 0f;
		}
	}

	// Token: 0x040006A5 RID: 1701
	private const int AVERAGE_PAST_FRAMERATES = 10;

	// Token: 0x040006A6 RID: 1702
	private int pFrameCounter;

	// Token: 0x040006A7 RID: 1703
	private float pTimeCounter;

	// Token: 0x040006A8 RID: 1704
	private float pLastFramerate;

	// Token: 0x040006A9 RID: 1705
	private float refreshTime = 0.5f;

	// Token: 0x040006AA RID: 1706
	private List<float> pPastFrameRates;

	// Token: 0x040006AB RID: 1707
	private int pPastFrameRatesIndex;

	// Token: 0x040006AC RID: 1708
	private float pAverageFrameRate;
}
