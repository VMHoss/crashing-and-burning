using System;
using System.Collections.Generic;
using Uniject;
using UnityEngine;

// Token: 0x0200001C RID: 28
public class UnityUtil : IUtil
{
	// Token: 0x060000E2 RID: 226 RVA: 0x00004CF8 File Offset: 0x00002EF8
	public T[] getAnyComponentsOfType<T>() where T : class
	{
		GameObject[] array = (GameObject[])UnityEngine.Object.FindObjectsOfType(typeof(GameObject));
		List<T> list = new List<T>();
		foreach (GameObject gameObject in array)
		{
			foreach (MonoBehaviour monoBehaviour in gameObject.GetComponents<MonoBehaviour>())
			{
				if (monoBehaviour is T)
				{
					list.Add(monoBehaviour as T);
				}
			}
		}
		return list.ToArray();
	}

	// Token: 0x1700000A RID: 10
	// (get) Token: 0x060000E3 RID: 227 RVA: 0x00004D90 File Offset: 0x00002F90
	public DateTime currentTime
	{
		get
		{
			return DateTime.Now;
		}
	}

	// Token: 0x1700000B RID: 11
	// (get) Token: 0x060000E4 RID: 228 RVA: 0x00004D98 File Offset: 0x00002F98
	public string persistentDataPath
	{
		get
		{
			return Application.persistentDataPath;
		}
	}

	// Token: 0x060000E5 RID: 229 RVA: 0x00004DA0 File Offset: 0x00002FA0
	public string loadedLevelName()
	{
		return Application.loadedLevelName;
	}

	// Token: 0x1700000C RID: 12
	// (get) Token: 0x060000E6 RID: 230 RVA: 0x00004DA8 File Offset: 0x00002FA8
	public RuntimePlatform Platform
	{
		get
		{
			return Application.platform;
		}
	}

	// Token: 0x060000E7 RID: 231 RVA: 0x00004DB0 File Offset: 0x00002FB0
	public static T findInstanceOfType<T>() where T : MonoBehaviour
	{
		return (T)((object)UnityEngine.Object.FindObjectOfType(typeof(T)));
	}

	// Token: 0x060000E8 RID: 232 RVA: 0x00004DC8 File Offset: 0x00002FC8
	public static T loadResourceInstanceOfType<T>() where T : MonoBehaviour
	{
		return ((GameObject)UnityEngine.Object.Instantiate(Resources.Load(typeof(T).ToString()))).GetComponent<T>();
	}

	// Token: 0x060000E9 RID: 233 RVA: 0x00004DF0 File Offset: 0x00002FF0
	public static bool pcPlatform()
	{
		return UnityUtil.PCControlledPlatforms.Contains(Application.platform);
	}

	// Token: 0x060000EA RID: 234 RVA: 0x00004E04 File Offset: 0x00003004
	public static void DebugLog(string message, params object[] args)
	{
		try
		{
			Debug.Log(string.Format("com.ballatergames.debug - {0}", string.Format(message, args)));
		}
		catch (ArgumentNullException message2)
		{
			Debug.Log(message2);
		}
		catch (FormatException message3)
		{
			Debug.Log(message3);
		}
	}

	// Token: 0x060000EB RID: 235 RVA: 0x00004E78 File Offset: 0x00003078
	public static float[] getFrustumBoundaries(Camera camera)
	{
		Plane[] array = GeometryUtility.CalculateFrustumPlanes(camera);
		return new float[]
		{
			(-array[0].normal * array[0].distance).x,
			(-array[1].normal * array[1].distance).x,
			(-array[5].normal * array[5].distance).y,
			(-array[4].normal * array[4].distance).y,
			(-array[2].normal * array[2].distance).z,
			(-array[3].normal * array[3].distance).z
		};
	}

	// Token: 0x0400004A RID: 74
	private static List<RuntimePlatform> PCControlledPlatforms = new List<RuntimePlatform>
	{
		RuntimePlatform.FlashPlayer,
		RuntimePlatform.LinuxPlayer,
		RuntimePlatform.NaCl,
		RuntimePlatform.OSXDashboardPlayer,
		RuntimePlatform.OSXEditor,
		RuntimePlatform.OSXPlayer,
		RuntimePlatform.OSXWebPlayer,
		RuntimePlatform.WindowsEditor,
		RuntimePlatform.WindowsPlayer,
		RuntimePlatform.WindowsWebPlayer
	};
}
