using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000061 RID: 97
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Internal/Update Manager")]
public class UpdateManager : MonoBehaviour
{
	// Token: 0x06000346 RID: 838 RVA: 0x00015804 File Offset: 0x00013A04
	private static int Compare(UpdateManager.UpdateEntry a, UpdateManager.UpdateEntry b)
	{
		if (a.index < b.index)
		{
			return 1;
		}
		if (a.index > b.index)
		{
			return -1;
		}
		return 0;
	}

	// Token: 0x06000347 RID: 839 RVA: 0x00015830 File Offset: 0x00013A30
	private static void CreateInstance()
	{
		if (UpdateManager.mInst == null)
		{
			UpdateManager.mInst = (UnityEngine.Object.FindObjectOfType(typeof(UpdateManager)) as UpdateManager);
			if (UpdateManager.mInst == null && Application.isPlaying)
			{
				GameObject gameObject = new GameObject("_UpdateManager");
				UnityEngine.Object.DontDestroyOnLoad(gameObject);
				UpdateManager.mInst = gameObject.AddComponent<UpdateManager>();
			}
		}
	}

	// Token: 0x06000348 RID: 840 RVA: 0x0001589C File Offset: 0x00013A9C
	private void UpdateList(List<UpdateManager.UpdateEntry> list, float delta)
	{
		int i = list.Count;
		while (i > 0)
		{
			UpdateManager.UpdateEntry updateEntry = list[--i];
			if (updateEntry.isMonoBehaviour)
			{
				if (updateEntry.mb == null)
				{
					list.RemoveAt(i);
					continue;
				}
				if (!updateEntry.mb.enabled || !NGUITools.GetActive(updateEntry.mb.gameObject))
				{
					continue;
				}
			}
			updateEntry.func(delta);
		}
	}

	// Token: 0x06000349 RID: 841 RVA: 0x00015928 File Offset: 0x00013B28
	private void Start()
	{
		if (Application.isPlaying)
		{
			this.mTime = Time.realtimeSinceStartup;
			base.StartCoroutine(this.CoroutineFunction());
		}
	}

	// Token: 0x0600034A RID: 842 RVA: 0x00015958 File Offset: 0x00013B58
	private void OnApplicationQuit()
	{
		UnityEngine.Object.DestroyImmediate(base.gameObject);
	}

	// Token: 0x0600034B RID: 843 RVA: 0x00015968 File Offset: 0x00013B68
	private void Update()
	{
		if (UpdateManager.mInst != this)
		{
			NGUITools.Destroy(base.gameObject);
		}
		else
		{
			this.UpdateList(this.mOnUpdate, Time.deltaTime);
		}
	}

	// Token: 0x0600034C RID: 844 RVA: 0x000159A8 File Offset: 0x00013BA8
	private void LateUpdate()
	{
		this.UpdateList(this.mOnLate, Time.deltaTime);
		if (!Application.isPlaying)
		{
			this.CoroutineUpdate();
		}
	}

	// Token: 0x0600034D RID: 845 RVA: 0x000159D8 File Offset: 0x00013BD8
	private bool CoroutineUpdate()
	{
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		float num = realtimeSinceStartup - this.mTime;
		if (num < 0.001f)
		{
			return true;
		}
		this.mTime = realtimeSinceStartup;
		this.UpdateList(this.mOnCoro, num);
		bool isPlaying = Application.isPlaying;
		int i = this.mDest.size;
		while (i > 0)
		{
			UpdateManager.DestroyEntry destroyEntry = this.mDest.buffer[--i];
			if (!isPlaying || destroyEntry.time < this.mTime)
			{
				if (destroyEntry.obj != null)
				{
					NGUITools.Destroy(destroyEntry.obj);
					destroyEntry.obj = null;
				}
				this.mDest.RemoveAt(i);
			}
		}
		if (this.mOnUpdate.Count == 0 && this.mOnLate.Count == 0 && this.mOnCoro.Count == 0 && this.mDest.size == 0)
		{
			NGUITools.Destroy(base.gameObject);
			return false;
		}
		return true;
	}

	// Token: 0x0600034E RID: 846 RVA: 0x00015AE0 File Offset: 0x00013CE0
	private IEnumerator CoroutineFunction()
	{
		while (Application.isPlaying)
		{
			if (!this.CoroutineUpdate())
			{
				break;
			}
			yield return null;
		}
		yield break;
	}

	// Token: 0x0600034F RID: 847 RVA: 0x00015AFC File Offset: 0x00013CFC
	private void Add(MonoBehaviour mb, int updateOrder, UpdateManager.OnUpdate func, List<UpdateManager.UpdateEntry> list)
	{
		int i = 0;
		int count = list.Count;
		while (i < count)
		{
			UpdateManager.UpdateEntry updateEntry = list[i];
			if (updateEntry.func == func)
			{
				return;
			}
			i++;
		}
		list.Add(new UpdateManager.UpdateEntry
		{
			index = updateOrder,
			func = func,
			mb = mb,
			isMonoBehaviour = (mb != null)
		});
		if (updateOrder != 0)
		{
			list.Sort(new Comparison<UpdateManager.UpdateEntry>(UpdateManager.Compare));
		}
	}

	// Token: 0x06000350 RID: 848 RVA: 0x00015B88 File Offset: 0x00013D88
	public static void AddUpdate(MonoBehaviour mb, int updateOrder, UpdateManager.OnUpdate func)
	{
		UpdateManager.CreateInstance();
		UpdateManager.mInst.Add(mb, updateOrder, func, UpdateManager.mInst.mOnUpdate);
	}

	// Token: 0x06000351 RID: 849 RVA: 0x00015BA8 File Offset: 0x00013DA8
	public static void AddLateUpdate(MonoBehaviour mb, int updateOrder, UpdateManager.OnUpdate func)
	{
		UpdateManager.CreateInstance();
		UpdateManager.mInst.Add(mb, updateOrder, func, UpdateManager.mInst.mOnLate);
	}

	// Token: 0x06000352 RID: 850 RVA: 0x00015BC8 File Offset: 0x00013DC8
	public static void AddCoroutine(MonoBehaviour mb, int updateOrder, UpdateManager.OnUpdate func)
	{
		UpdateManager.CreateInstance();
		UpdateManager.mInst.Add(mb, updateOrder, func, UpdateManager.mInst.mOnCoro);
	}

	// Token: 0x06000353 RID: 851 RVA: 0x00015BE8 File Offset: 0x00013DE8
	public static void AddDestroy(UnityEngine.Object obj, float delay)
	{
		if (obj == null)
		{
			return;
		}
		if (Application.isPlaying)
		{
			if (delay > 0f)
			{
				UpdateManager.CreateInstance();
				UpdateManager.DestroyEntry destroyEntry = new UpdateManager.DestroyEntry();
				destroyEntry.obj = obj;
				destroyEntry.time = Time.realtimeSinceStartup + delay;
				UpdateManager.mInst.mDest.Add(destroyEntry);
			}
			else
			{
				UnityEngine.Object.Destroy(obj);
			}
		}
		else
		{
			UnityEngine.Object.DestroyImmediate(obj);
		}
	}

	// Token: 0x04000358 RID: 856
	private static UpdateManager mInst;

	// Token: 0x04000359 RID: 857
	private List<UpdateManager.UpdateEntry> mOnUpdate = new List<UpdateManager.UpdateEntry>();

	// Token: 0x0400035A RID: 858
	private List<UpdateManager.UpdateEntry> mOnLate = new List<UpdateManager.UpdateEntry>();

	// Token: 0x0400035B RID: 859
	private List<UpdateManager.UpdateEntry> mOnCoro = new List<UpdateManager.UpdateEntry>();

	// Token: 0x0400035C RID: 860
	private BetterList<UpdateManager.DestroyEntry> mDest = new BetterList<UpdateManager.DestroyEntry>();

	// Token: 0x0400035D RID: 861
	private float mTime;

	// Token: 0x02000062 RID: 98
	public class UpdateEntry
	{
		// Token: 0x0400035E RID: 862
		public int index;

		// Token: 0x0400035F RID: 863
		public UpdateManager.OnUpdate func;

		// Token: 0x04000360 RID: 864
		public MonoBehaviour mb;

		// Token: 0x04000361 RID: 865
		public bool isMonoBehaviour;
	}

	// Token: 0x02000063 RID: 99
	public class DestroyEntry
	{
		// Token: 0x04000362 RID: 866
		public UnityEngine.Object obj;

		// Token: 0x04000363 RID: 867
		public float time;
	}

	// Token: 0x020001EF RID: 495
	// (Invoke) Token: 0x06000DD9 RID: 3545
	public delegate void OnUpdate(float delta);
}
