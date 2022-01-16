using System;
using UnityEngine;

// Token: 0x020000EB RID: 235
public class FadeAudioScript : MonoBehaviour
{
	// Token: 0x06000715 RID: 1813 RVA: 0x0003455C File Offset: 0x0003275C
	private void Start()
	{
	}

	// Token: 0x06000716 RID: 1814 RVA: 0x00034560 File Offset: 0x00032760
	public void setFade(bool aFadeIn, float aFadeTime)
	{
		this.pFadeIn = aFadeIn;
		this.pConstFadeTime = aFadeTime;
		this.pFadeTimeCur = aFadeTime;
	}

	// Token: 0x06000717 RID: 1815 RVA: 0x00034578 File Offset: 0x00032778
	private void Update()
	{
		this.pFadeTimeCur -= Time.deltaTime;
		if (this.pFadeTimeCur < 0f)
		{
			UnityEngine.Object.Destroy(this);
		}
		else if (Data.music)
		{
			if (this.pFadeIn)
			{
				AudioListener.volume = 1f - this.pFadeTimeCur / this.pConstFadeTime;
			}
			else
			{
				AudioListener.volume = this.pFadeTimeCur / this.pConstFadeTime;
			}
		}
	}

	// Token: 0x0400074D RID: 1869
	private bool pFadeIn;

	// Token: 0x0400074E RID: 1870
	private float pConstFadeTime = 1f;

	// Token: 0x0400074F RID: 1871
	private float pFadeTimeCur = 1f;
}
