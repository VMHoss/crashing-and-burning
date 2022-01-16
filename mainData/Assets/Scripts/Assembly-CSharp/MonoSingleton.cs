using System;
using UnityEngine;

// Token: 0x02000017 RID: 23
public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
	// Token: 0x1700001A RID: 26
	// (get) Token: 0x060000FE RID: 254 RVA: 0x00008294 File Offset: 0x00006494
	public static T instance
	{
		get
		{
			if (MonoSingleton<T>.m_Instance == null)
			{
				MonoSingleton<T>.m_Instance = (UnityEngine.Object.FindObjectOfType(typeof(T)) as T);
				if (MonoSingleton<T>.m_Instance == null)
				{
					MonoSingleton<T>.m_Instance = new GameObject("Singleton of " + typeof(T).ToString(), new Type[]
					{
						typeof(T)
					}).GetComponent<T>();
					MonoSingleton<T>.m_Instance.Init();
				}
			}
			return MonoSingleton<T>.m_Instance;
		}
	}

	// Token: 0x060000FF RID: 255 RVA: 0x0000833C File Offset: 0x0000653C
	private void Awake()
	{
		if (MonoSingleton<T>.m_Instance == null)
		{
			MonoSingleton<T>.m_Instance = (this as T);
		}
	}

	// Token: 0x06000100 RID: 256 RVA: 0x00008364 File Offset: 0x00006564
	public virtual void Init()
	{
	}

	// Token: 0x06000101 RID: 257 RVA: 0x00008368 File Offset: 0x00006568
	private void OnApplicationQuit()
	{
		MonoSingleton<T>.m_Instance = (T)((object)null);
	}

	// Token: 0x0400016D RID: 365
	private static T m_Instance;
}
