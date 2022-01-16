using System;
using UnityEngine;

// Token: 0x02000046 RID: 70
[RequireComponent(typeof(UISlider))]
[AddComponentMenu("NGUI/Examples/Slider Colors")]
[ExecuteInEditMode]
public class UISliderColors : MonoBehaviour
{
	// Token: 0x0600023D RID: 573 RVA: 0x0000FC0C File Offset: 0x0000DE0C
	private void Start()
	{
		this.mSlider = base.GetComponent<UISlider>();
		this.Update();
	}

	// Token: 0x0600023E RID: 574 RVA: 0x0000FC20 File Offset: 0x0000DE20
	private void Update()
	{
		if (this.sprite == null || this.colors.Length == 0)
		{
			return;
		}
		float num = this.mSlider.sliderValue;
		num *= (float)(this.colors.Length - 1);
		int num2 = Mathf.FloorToInt(num);
		Color color = this.colors[0];
		if (num2 >= 0)
		{
			if (num2 + 1 < this.colors.Length)
			{
				float t = num - (float)num2;
				color = Color.Lerp(this.colors[num2], this.colors[num2 + 1], t);
			}
			else if (num2 < this.colors.Length)
			{
				color = this.colors[num2];
			}
			else
			{
				color = this.colors[this.colors.Length - 1];
			}
		}
		color.a = this.sprite.color.a;
		this.sprite.color = color;
	}

	// Token: 0x040002A3 RID: 675
	public UISprite sprite;

	// Token: 0x040002A4 RID: 676
	public Color[] colors = new Color[]
	{
		Color.red,
		Color.yellow,
		Color.green
	};

	// Token: 0x040002A5 RID: 677
	private UISlider mSlider;
}
