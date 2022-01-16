using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000DA RID: 218
public class PlayerBaseData
{
	// Token: 0x060006A6 RID: 1702 RVA: 0x0002F8D0 File Offset: 0x0002DAD0
	public PlayerBaseData(string aPlayerName)
	{
		this.input = new PlayerInput();
		this.index = Data.playerBaseList.Count;
		Data.playerBaseList.Add(this);
		this.name = aPlayerName;
	}

	// Token: 0x060006A7 RID: 1703 RVA: 0x0002F91C File Offset: 0x0002DB1C
	public void SetKeyInput()
	{
		string key = "Player" + (this.index + 1) + Data.GetGlobals()["Platform"].s;
		if (Data.Shared["Player"].d.ContainsKey(key))
		{
			Dictionary<string, DicEntry> d = Data.Shared["Player"].d[key].d;
			if (d.ContainsKey("Input"))
			{
				if (d["Input"].type == DicEntry.EntryType.STRING)
				{
					this.AddInputSet(d["Input"].s);
				}
				else
				{
					foreach (DicEntry dicEntry in d["Input"].l)
					{
						this.AddInputSet(dicEntry.s);
					}
				}
			}
			else
			{
				Debug.LogError("Player" + (this.index + 1) + " input property was not found in SharedData.txt -> Player -> Input");
			}
		}
		else
		{
			Debug.LogError("Player" + (this.index + 1) + " was not found in SharedData.txt -> Player");
		}
	}

	// Token: 0x060006A8 RID: 1704 RVA: 0x0002FA8C File Offset: 0x0002DC8C
	private void AddInputSet(string anInputSet)
	{
		Dictionary<string, DicEntry> d = Data.Shared["Input"].d;
		if (d.ContainsKey(anInputSet))
		{
			string s = d[anInputSet].d["Type"].s;
			if (s != null)
			{
				if (PlayerBaseData.<>f__switch$map12 == null)
				{
					PlayerBaseData.<>f__switch$map12 = new Dictionary<string, int>(2)
					{
						{
							"Keyboard",
							0
						},
						{
							"Mouse",
							1
						}
					};
				}
				int num;
				if (PlayerBaseData.<>f__switch$map12.TryGetValue(s, out num))
				{
					if (num == 0)
					{
						this.input.AddInputGetter(new KeyboardInputGetter(d[anInputSet].d));
						goto IL_EF;
					}
					if (num == 1)
					{
						this.input.AddInputGetter(new MouseInputGetter(d[anInputSet].d));
						goto IL_EF;
					}
				}
			}
			this.AddInputSetSpecific(d[anInputSet].d);
			IL_EF:;
		}
		else
		{
			Debug.LogError("Unable to find property " + anInputSet + " in SharedData.txt -> Input");
		}
	}

	// Token: 0x060006A9 RID: 1705 RVA: 0x0002FBA4 File Offset: 0x0002DDA4
	protected virtual void AddInputSetSpecific(Dictionary<string, DicEntry> anInputSet)
	{
		Debug.LogWarning("PlayerBaseData::AddInputSetSpecific is not overridden by child.");
	}

	// Token: 0x040006D1 RID: 1745
	public int index;

	// Token: 0x040006D2 RID: 1746
	public string name = string.Empty;

	// Token: 0x040006D3 RID: 1747
	public PlayerInput input;
}
