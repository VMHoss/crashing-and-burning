using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000120 RID: 288
public static class Shaders
{
	// Token: 0x06000824 RID: 2084 RVA: 0x0003D6FC File Offset: 0x0003B8FC
	public static void SetVisuals()
	{
		Dictionary<string, DicEntry> d = Data.Shared["ShaderMaterials"].d;
		foreach (KeyValuePair<string, DicEntry> keyValuePair in d)
		{
			string text = keyValuePair.Value.d["Path"].s;
			int num = text.IndexOf("/");
			string aBundleName = text.Substring(0, num);
			text = text.Substring(num + 1);
			Material material = Loader.LoadObject(aBundleName, text + "/" + keyValuePair.Key) as Material;
			if (Data.highDetails)
			{
				material.shader = Shader.Find(keyValuePair.Value.d["High"].s.Replace("_", " "));
			}
			else
			{
				material.shader = Shader.Find(keyValuePair.Value.d["Low"].s.Replace("_", " "));
			}
		}
	}
}
