using System;
using UnityEngine;

// Token: 0x0200015E RID: 350
public class KGFFPSDisplay : MonoBehaviour
{
	// Token: 0x06000A23 RID: 2595 RVA: 0x0004D5F4 File Offset: 0x0004B7F4
	private void Start()
	{
		this.itsStyleText = new GUIStyle();
		this.itsStyleText.fontSize = this.itsFontSize;
		this.itsStyleText.normal.textColor = Color.white;
	}

	// Token: 0x06000A24 RID: 2596 RVA: 0x0004D628 File Offset: 0x0004B828
	private void Update()
	{
		this.itsFrameCounter++;
		if (Time.time - this.itsLastMeasurePoint > this.itsTimeBetweenMeasurePoints)
		{
			this.itsFPS = (float)this.itsFrameCounter / (Time.time - this.itsLastMeasurePoint);
			this.itsFrameCounter = 0;
			this.itsLastMeasurePoint = Time.time;
		}
	}

	// Token: 0x06000A25 RID: 2597 RVA: 0x0004D688 File Offset: 0x0004B888
	private void OnGUI()
	{
		GUI.color = Color.black;
		GUI.Label(new Rect(1f, 1f, 200f, 200f), string.Empty + (int)this.itsFPS + " FPS", this.itsStyleText);
		GUI.color = this.itsFontColor;
		GUI.Label(new Rect(0f, 0f, 200f, 200f), string.Empty + (int)this.itsFPS + " FPS", this.itsStyleText);
	}

	// Token: 0x04000A52 RID: 2642
	private float itsFPS;

	// Token: 0x04000A53 RID: 2643
	private int itsFrameCounter;

	// Token: 0x04000A54 RID: 2644
	private float itsLastMeasurePoint;

	// Token: 0x04000A55 RID: 2645
	public float itsTimeBetweenMeasurePoints = 2f;

	// Token: 0x04000A56 RID: 2646
	public int itsFontSize = 30;

	// Token: 0x04000A57 RID: 2647
	public Color itsFontColor = Color.white;

	// Token: 0x04000A58 RID: 2648
	private GUIStyle itsStyleText;
}
