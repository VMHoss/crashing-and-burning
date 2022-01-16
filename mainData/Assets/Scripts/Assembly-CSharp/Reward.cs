using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000106 RID: 262
public class Reward
{
	// Token: 0x060007BF RID: 1983 RVA: 0x0003A7C4 File Offset: 0x000389C4
	public Reward(string aRewardString)
	{
		int num = aRewardString.IndexOf("_") + 1;
		this.type = aRewardString.Substring(0, num - 1);
		string text = this.type;
		if (text != null)
		{
			if (Reward.<>f__switch$map1C == null)
			{
				Reward.<>f__switch$map1C = new Dictionary<string, int>(2)
				{
					{
						"Unlock",
						0
					},
					{
						"Give",
						0
					}
				};
			}
			int num2;
			if (Reward.<>f__switch$map1C.TryGetValue(text, out num2))
			{
				if (num2 == 0)
				{
					int num3 = aRewardString.LastIndexOf("_");
					this.category = aRewardString.Substring(num, num3 - num);
					text = this.category;
					if (text != null)
					{
						if (Reward.<>f__switch$map1D == null)
						{
							Reward.<>f__switch$map1D = new Dictionary<string, int>(3)
							{
								{
									"Vehicle",
									0
								},
								{
									"SuperPower",
									0
								},
								{
									"Cash",
									0
								}
							};
						}
						if (Reward.<>f__switch$map1D.TryGetValue(text, out num2))
						{
							if (num2 == 0)
							{
								this.item = aRewardString.Substring(num3 + 1);
								return;
							}
						}
					}
					throw new UnityException("Unknown reward category: " + this.category);
				}
			}
		}
		throw new UnityException("Unknown reward type: " + this.type);
	}

	// Token: 0x060007C0 RID: 1984 RVA: 0x0003A918 File Offset: 0x00038B18
	public void Apply()
	{
		Debug.Log(string.Concat(new string[]
		{
			"Applying reward: ",
			this.type,
			" - ",
			this.category,
			" - ",
			this.item
		}));
		string text = this.type;
		if (text != null)
		{
			if (Reward.<>f__switch$map1E == null)
			{
				Reward.<>f__switch$map1E = new Dictionary<string, int>(2)
				{
					{
						"Unlock",
						0
					},
					{
						"Give",
						1
					}
				};
			}
			int num;
			if (Reward.<>f__switch$map1E.TryGetValue(text, out num))
			{
				if (num != 0)
				{
					if (num != 1)
					{
						goto IL_C4;
					}
					this.pGiveItem();
				}
				else
				{
					Shop.UnlockObject(this.category, this.item);
				}
				return;
			}
		}
		IL_C4:
		throw new UnityException("Unknown reward type: " + this.type);
	}

	// Token: 0x060007C1 RID: 1985 RVA: 0x0003AA00 File Offset: 0x00038C00
	private void pGiveItem()
	{
		if (this.category == "Cash")
		{
			if (Data.Shared["Misc"].d.ContainsKey(this.item))
			{
				Scripts.scoreManager.AddCash(Data.Shared["Misc"].d[this.item].i);
			}
			else
			{
				Debug.LogError("Error: item - " + this.item + " was not found in misc");
			}
		}
		else
		{
			Debug.LogError("Error: when giving item it must be cash!");
		}
	}

	// Token: 0x0400083A RID: 2106
	public string type;

	// Token: 0x0400083B RID: 2107
	public string category;

	// Token: 0x0400083C RID: 2108
	public string item;
}
