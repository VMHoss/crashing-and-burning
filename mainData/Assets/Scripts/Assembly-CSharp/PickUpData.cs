using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200010E RID: 270
public class PickUpData
{
	// Token: 0x060007DA RID: 2010 RVA: 0x0003B558 File Offset: 0x00039758
	public PickUpData(string aName, Vector3 aPosition)
	{
		this.name = aName;
		this.pickUpName = this.name.Substring(7);
		this.pickUpName = this.pickUpName.Substring(0, this.pickUpName.IndexOf("_"));
		Dictionary<string, DicEntry> d = PickUpData.pickUpData[this.pickUpName].d;
		this.mapIcon = d["MapIcon"].b;
		if (d.ContainsKey("HeightOffset"))
		{
			this.heightOffset = d["HeightOffset"].f;
			this.isTransformed = true;
		}
		if (d.ContainsKey("Scale"))
		{
			this.scale = d["Scale"].f;
			this.isTransformed = true;
		}
		this.position = -aPosition;
		this.position.y = aPosition.y;
		this.isPickedUp = false;
		this.inMagnetField = false;
		this.magnetProgression = 0f;
	}

	// Token: 0x060007DC RID: 2012 RVA: 0x0003B688 File Offset: 0x00039888
	public void Reset()
	{
		this.isPickedUp = false;
		this.inMagnetField = false;
		this.magnetProgression = 0f;
	}

	// Token: 0x04000862 RID: 2146
	public static Dictionary<string, DicEntry> pickUpData;

	// Token: 0x04000863 RID: 2147
	public string name = string.Empty;

	// Token: 0x04000864 RID: 2148
	public string pickUpName = string.Empty;

	// Token: 0x04000865 RID: 2149
	public Vector3 position;

	// Token: 0x04000866 RID: 2150
	public bool isPickedUp;

	// Token: 0x04000867 RID: 2151
	public bool inMagnetField;

	// Token: 0x04000868 RID: 2152
	public float magnetProgression;

	// Token: 0x04000869 RID: 2153
	public bool mapIcon;

	// Token: 0x0400086A RID: 2154
	public bool isTransformed;

	// Token: 0x0400086B RID: 2155
	public float heightOffset;

	// Token: 0x0400086C RID: 2156
	public float scale = 1f;
}
