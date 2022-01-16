using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000D0 RID: 208
public class KeyboardInputGetter : InputGetter
{
	// Token: 0x06000650 RID: 1616 RVA: 0x0002D6BC File Offset: 0x0002B8BC
	public KeyboardInputGetter(Dictionary<string, DicEntry> anInputParameters)
	{
		this.pInputParameters = new Dictionary<string, string>();
		this.pParameterValue = new Dictionary<string, float>();
		foreach (KeyValuePair<string, DicEntry> keyValuePair in anInputParameters)
		{
			if (!(keyValuePair.Key == "Type"))
			{
				this.pParameterValue.Add(keyValuePair.Key, 0f);
				this.pInputParameters.Add(keyValuePair.Key, keyValuePair.Value.s.ToLower());
			}
		}
	}

	// Token: 0x06000651 RID: 1617 RVA: 0x0002D788 File Offset: 0x0002B988
	public override void UpdateInput()
	{
		foreach (KeyValuePair<string, string> keyValuePair in this.pInputParameters)
		{
			this.pParameterValue[keyValuePair.Key] = (float)((!Input.GetKey(keyValuePair.Value)) ? 0 : 1);
		}
	}

	// Token: 0x06000652 RID: 1618 RVA: 0x0002D814 File Offset: 0x0002BA14
	public override float GetAction(string anAction)
	{
		if (this.pParameterValue.ContainsKey(anAction))
		{
			return this.pParameterValue[anAction];
		}
		return 0f;
	}

	// Token: 0x06000653 RID: 1619 RVA: 0x0002D83C File Offset: 0x0002BA3C
	public override void Reset()
	{
		foreach (KeyValuePair<string, string> keyValuePair in this.pInputParameters)
		{
			this.pParameterValue[keyValuePair.Key] = 0f;
		}
	}

	// Token: 0x040006B2 RID: 1714
	private Dictionary<string, string> pInputParameters;

	// Token: 0x040006B3 RID: 1715
	private Dictionary<string, float> pParameterValue;
}
