using System;

// Token: 0x020000E3 RID: 227
public class CameraShake
{
	// Token: 0x060006EC RID: 1772 RVA: 0x000321E0 File Offset: 0x000303E0
	public CameraShake(float aDuration, float anIntensity, float aFallOff)
	{
		this.duration = aDuration;
		this.intensity = anIntensity;
		this.fallOff = aFallOff;
		this.fallOffInv = 1f / this.fallOff;
	}

	// Token: 0x04000702 RID: 1794
	public float duration;

	// Token: 0x04000703 RID: 1795
	public float fallOff;

	// Token: 0x04000704 RID: 1796
	public float intensity;

	// Token: 0x04000705 RID: 1797
	public readonly float fallOffInv;
}
