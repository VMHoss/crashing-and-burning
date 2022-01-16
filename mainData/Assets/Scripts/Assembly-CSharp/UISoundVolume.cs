using System;
using UnityEngine;

// Token: 0x02000047 RID: 71
[RequireComponent(typeof(UISlider))]
[AddComponentMenu("NGUI/Interaction/Sound Volume")]
public class UISoundVolume : MonoBehaviour
{
	// Token: 0x06000240 RID: 576 RVA: 0x0000FD3C File Offset: 0x0000DF3C
	private void Awake()
	{
		this.mSlider = base.GetComponent<UISlider>();
		this.mSlider.sliderValue = NGUITools.soundVolume;
		this.mSlider.eventReceiver = base.gameObject;
	}

	// Token: 0x06000241 RID: 577 RVA: 0x0000FD78 File Offset: 0x0000DF78
	private void OnSliderChange(float val)
	{
		NGUITools.soundVolume = val;
	}

	// Token: 0x040002A6 RID: 678
	private UISlider mSlider;
}
