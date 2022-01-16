using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000E1 RID: 225
public class UserData
{
	// Token: 0x060006CA RID: 1738 RVA: 0x00030A50 File Offset: 0x0002EC50
	public static void Save()
	{
		Data.CopyFromDataToGlobals();
		string text = TextLoader.SaveText(Data.GetGlobals());
		Debug.Log("Saved string:\n" + text);
		PlayerPrefs.SetString("Save", text);
	}

	// Token: 0x060006CB RID: 1739 RVA: 0x00030A88 File Offset: 0x0002EC88
	public static void Load()
	{
		string @string = PlayerPrefs.GetString("Save");
		if (@string == string.Empty)
		{
			Debug.Log("Loading: failed.");
			UserData.Reset();
			return;
		}
		Debug.Log("Loaded: \n" + @string);
		Dictionary<string, DicEntry> dictionary = new Dictionary<string, DicEntry>();
		TextLoader.LoadText(@string, dictionary);
		bool flag = dictionary["VersionNumber"].s == "v0.9";
		if (Data.versionNumber != dictionary["VersionNumber"].s && !flag)
		{
			Debug.Log("Loading: Version number did not match. Should be " + Data.versionNumber + " but is " + dictionary["VersionNumber"].s);
			UserData.Reset();
			return;
		}
		foreach (string text in Data.saveList)
		{
			if (!dictionary.ContainsKey(text))
			{
				if (!flag)
				{
					Debug.Log("Loading: Property " + text + " not found in loaded text");
					UserData.Reset();
					return;
				}
			}
			else
			{
				Data.GetGlobals()[text] = dictionary[text];
			}
		}
		if (flag)
		{
			Debug.Log("Applying savedata fix coming from v0.9!");
			int num = 1;
			foreach (DicEntry dicEntry in Data.GetGlobals()["UnlockedVehicles"].l)
			{
				if (dicEntry.s == "Chevrolet" && num < 3)
				{
					num = 3;
				}
				if (dicEntry.s == "FireTruck" && num < 5)
				{
					num = 5;
				}
				if (dicEntry.s == "Juggernaut" && num < 7)
				{
					num = 7;
				}
				if (dicEntry.s == "PanzerTruck" && num < 9)
				{
					num = 9;
				}
			}
			foreach (DicEntry dicEntry2 in Data.GetGlobals()["UnlockedSuperPowers"].l)
			{
				if (dicEntry2.s == "StuntMan" && num < 2)
				{
					num = 2;
				}
				if (dicEntry2.s == "Golden" && num < 4)
				{
					num = 4;
				}
				if (dicEntry2.s == "QuadDamage" && num < 6)
				{
					num = 6;
				}
				if (dicEntry2.s == "Toxic" && num < 8)
				{
					num = 8;
				}
				if (dicEntry2.s == "Diablo" && num < 10)
				{
					num = 10;
				}
			}
			Data.GetGlobals()["PlayerLevel"].i = num;
			Data.GetGlobals()["VersionNumber"].s = Data.versionNumber;
		}
		Data.CopyFromGlobalsToData();
	}

	// Token: 0x060006CC RID: 1740 RVA: 0x00030E1C File Offset: 0x0002F01C
	public static void Reset()
	{
		PlayerPrefs.DeleteKey("Save");
		UserData.Save();
	}
}
