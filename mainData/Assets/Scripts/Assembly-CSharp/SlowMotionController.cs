using System;
using UnityEngine;

// Token: 0x02000125 RID: 293
public class SlowMotionController
{
	// Token: 0x06000873 RID: 2163 RVA: 0x0003EA5C File Offset: 0x0003CC5C
	public void StartAirSlowMotion()
	{
		this.pAirSlowMotion = true;
		this.SetTimeScale(Data.Shared["Misc"].d["AirTimeSlowMotion"].f);
	}

	// Token: 0x06000874 RID: 2164 RVA: 0x0003EA9C File Offset: 0x0003CC9C
	public void StopAirSlowMotion()
	{
		this.pAirSlowMotion = false;
		if (!this.pActive && !this.pBuildingSlowMotion)
		{
			this.SetTimeScale(1f);
		}
	}

	// Token: 0x06000875 RID: 2165 RVA: 0x0003EAD4 File Offset: 0x0003CCD4
	public void StartBuildingSlowMotion(float aTimeScale, float aDuration)
	{
		this.pBuildingSlowMotion = true;
		this.pBuildingSlowMotionTime = aTimeScale;
		this.SetTimeScale(this.pBuildingSlowMotionTime);
		this.pBuildingDuration = aDuration;
	}

	// Token: 0x06000876 RID: 2166 RVA: 0x0003EAF8 File Offset: 0x0003CCF8
	public void StartSlowMotion(float aSlowMotionTime, float aDuration)
	{
		if (!this.pActive)
		{
			this.pActive = true;
			this.pSlowMotionTime = aSlowMotionTime;
			this.pDuration = aDuration;
			this.SetTimeScale(this.pSlowMotionTime);
		}
		else
		{
			if (this.pDuration < aDuration)
			{
				this.pDuration = aDuration;
			}
			this.pSlowMotionTime = aSlowMotionTime;
			this.SetTimeScale(this.pSlowMotionTime);
		}
	}

	// Token: 0x06000877 RID: 2167 RVA: 0x0003EB5C File Offset: 0x0003CD5C
	public void Update()
	{
		if (this.pBuildingSlowMotion)
		{
			this.pBuildingDuration -= Time.deltaTime / Time.timeScale;
			if (this.pBuildingDuration < 0.5f)
			{
				if (this.pBuildingDuration > 0f)
				{
					if (!this.pActive && !this.pAirSlowMotion)
					{
						this.SetTimeScale(Mathf.Lerp(1f, this.pSlowMotionTime, this.pBuildingDuration * 2f));
					}
				}
				else
				{
					if (!this.pActive && !this.pAirSlowMotion)
					{
						this.SetTimeScale(1f);
					}
					this.pBuildingSlowMotion = false;
				}
			}
			return;
		}
		if (this.pAirSlowMotion)
		{
			return;
		}
		if (!this.pActive)
		{
			return;
		}
		this.pDuration -= Time.deltaTime / Time.timeScale;
		if (this.pDuration < 0.5f)
		{
			if (this.pDuration > 0f)
			{
				this.SetTimeScale(Mathf.Lerp(1f, this.pSlowMotionTime, this.pDuration * 2f));
			}
			else
			{
				this.SetTimeScale(1f);
				this.pActive = false;
			}
		}
	}

	// Token: 0x06000878 RID: 2168 RVA: 0x0003EC9C File Offset: 0x0003CE9C
	private void SetTimeScale(float aTimeScale)
	{
		Time.timeScale = aTimeScale;
		Time.fixedDeltaTime = GameData.fixedTimeStep * Time.timeScale;
	}

	// Token: 0x06000879 RID: 2169 RVA: 0x0003ECB4 File Offset: 0x0003CEB4
	public bool IsActive()
	{
		return this.pActive;
	}

	// Token: 0x040008B3 RID: 2227
	private bool pActive;

	// Token: 0x040008B4 RID: 2228
	private float pSlowMotionTime = 1f;

	// Token: 0x040008B5 RID: 2229
	private float pDuration = -1f;

	// Token: 0x040008B6 RID: 2230
	private bool pAirSlowMotion;

	// Token: 0x040008B7 RID: 2231
	private bool pBuildingSlowMotion;

	// Token: 0x040008B8 RID: 2232
	private float pBuildingSlowMotionTime = 1f;

	// Token: 0x040008B9 RID: 2233
	private float pBuildingDuration = -1f;
}
