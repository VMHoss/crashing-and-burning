using System;
using UnityEngine;

// Token: 0x0200006F RID: 111
[AddComponentMenu("NGUI/Tween/Volume")]
public class TweenVolume : UITweener
{
	// Token: 0x17000077 RID: 119
	// (get) Token: 0x0600038F RID: 911 RVA: 0x00016990 File Offset: 0x00014B90
	public AudioSource audioSource
	{
		get
		{
			if (this.mSource == null)
			{
				this.mSource = base.audio;
				if (this.mSource == null)
				{
					this.mSource = base.GetComponentInChildren<AudioSource>();
					if (this.mSource == null)
					{
						Debug.LogError("TweenVolume needs an AudioSource to work with", this);
						base.enabled = false;
					}
				}
			}
			return this.mSource;
		}
	}

	// Token: 0x17000078 RID: 120
	// (get) Token: 0x06000390 RID: 912 RVA: 0x00016A00 File Offset: 0x00014C00
	// (set) Token: 0x06000391 RID: 913 RVA: 0x00016A10 File Offset: 0x00014C10
	public float volume
	{
		get
		{
			return this.audioSource.volume;
		}
		set
		{
			this.audioSource.volume = value;
		}
	}

	// Token: 0x06000392 RID: 914 RVA: 0x00016A20 File Offset: 0x00014C20
	protected override void OnUpdate(float factor, bool isFinished)
	{
		this.volume = this.from * (1f - factor) + this.to * factor;
		this.mSource.enabled = (this.mSource.volume > 0.01f);
	}

	// Token: 0x06000393 RID: 915 RVA: 0x00016A68 File Offset: 0x00014C68
	public static TweenVolume Begin(GameObject go, float duration, float targetVolume)
	{
		TweenVolume tweenVolume = UITweener.Begin<TweenVolume>(go, duration);
		tweenVolume.from = tweenVolume.volume;
		tweenVolume.to = targetVolume;
		if (duration <= 0f)
		{
			tweenVolume.Sample(1f, true);
			tweenVolume.enabled = false;
		}
		return tweenVolume;
	}

	// Token: 0x04000395 RID: 917
	public float from;

	// Token: 0x04000396 RID: 918
	public float to = 1f;

	// Token: 0x04000397 RID: 919
	private AudioSource mSource;
}
