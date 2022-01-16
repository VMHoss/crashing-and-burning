using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000D8 RID: 216
public class MouseInputGetter : InputGetter
{
	// Token: 0x0600069F RID: 1695 RVA: 0x0002F414 File Offset: 0x0002D614
	public MouseInputGetter(Dictionary<string, DicEntry> anInputParameters)
	{
		this.pInputParameters = new Dictionary<string, int>();
		this.pParameterValue = new Dictionary<string, float>();
		foreach (KeyValuePair<string, DicEntry> keyValuePair in anInputParameters)
		{
			if (!(keyValuePair.Key == "Type"))
			{
				this.pParameterValue.Add(keyValuePair.Key, 0f);
				this.pInputParameters.Add(keyValuePair.Key, keyValuePair.Value.i);
			}
		}
	}

	// Token: 0x060006A0 RID: 1696 RVA: 0x0002F4DC File Offset: 0x0002D6DC
	public override void UpdateInput()
	{
		if (UICamera.hoveredObject)
		{
			return;
		}
		foreach (KeyValuePair<string, int> keyValuePair in this.pInputParameters)
		{
			this.pParameterValue[keyValuePair.Key] = (float)((!Input.GetMouseButton(keyValuePair.Value)) ? 0 : 1);
		}
	}

	// Token: 0x060006A1 RID: 1697 RVA: 0x0002F578 File Offset: 0x0002D778
	public override float GetAction(string anAction)
	{
		if (this.pParameterValue.ContainsKey(anAction))
		{
			return this.pParameterValue[anAction];
		}
		return 0f;
	}

	// Token: 0x060006A2 RID: 1698 RVA: 0x0002F5A0 File Offset: 0x0002D7A0
	public override void Reset()
	{
		foreach (KeyValuePair<string, int> keyValuePair in this.pInputParameters)
		{
			this.pParameterValue[keyValuePair.Key] = 0f;
		}
	}

	// Token: 0x040006CF RID: 1743
	private Dictionary<string, int> pInputParameters;

	// Token: 0x040006D0 RID: 1744
	private Dictionary<string, float> pParameterValue;
}
