using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000056 RID: 86
[AddComponentMenu("NGUI/Internal/Debug")]
public class NGUIDebug : MonoBehaviour
{
	// Token: 0x06000296 RID: 662 RVA: 0x00011848 File Offset: 0x0000FA48
	public static void Log(string text)
	{
		if (Application.isPlaying)
		{
			if (NGUIDebug.mLines.Count > 20)
			{
				NGUIDebug.mLines.RemoveAt(0);
			}
			NGUIDebug.mLines.Add(text);
			if (NGUIDebug.mInstance == null)
			{
				GameObject gameObject = new GameObject("_NGUI Debug");
				NGUIDebug.mInstance = gameObject.AddComponent<NGUIDebug>();
				UnityEngine.Object.DontDestroyOnLoad(gameObject);
			}
		}
		else
		{
			Debug.Log(text);
		}
	}

	// Token: 0x06000297 RID: 663 RVA: 0x000118C0 File Offset: 0x0000FAC0
	public static void DrawBounds(Bounds b)
	{
		Vector3 center = b.center;
		Vector3 vector = b.center - b.extents;
		Vector3 vector2 = b.center + b.extents;
		Debug.DrawLine(new Vector3(vector.x, vector.y, center.z), new Vector3(vector2.x, vector.y, center.z), Color.red);
		Debug.DrawLine(new Vector3(vector.x, vector.y, center.z), new Vector3(vector.x, vector2.y, center.z), Color.red);
		Debug.DrawLine(new Vector3(vector2.x, vector.y, center.z), new Vector3(vector2.x, vector2.y, center.z), Color.red);
		Debug.DrawLine(new Vector3(vector.x, vector2.y, center.z), new Vector3(vector2.x, vector2.y, center.z), Color.red);
	}

	// Token: 0x06000298 RID: 664 RVA: 0x000119F8 File Offset: 0x0000FBF8
	private void OnGUI()
	{
		int i = 0;
		int count = NGUIDebug.mLines.Count;
		while (i < count)
		{
			GUILayout.Label(NGUIDebug.mLines[i], new GUILayoutOption[0]);
			i++;
		}
	}

	// Token: 0x04000301 RID: 769
	private static List<string> mLines = new List<string>();

	// Token: 0x04000302 RID: 770
	private static NGUIDebug mInstance = null;
}
