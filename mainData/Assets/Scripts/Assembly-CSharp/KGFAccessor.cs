using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000150 RID: 336
public static class KGFAccessor
{
	// Token: 0x060009A6 RID: 2470 RVA: 0x0004B718 File Offset: 0x00049918
	public static void AddKGFObject(KGFObject theObjectScript)
	{
		Type type = theObjectScript.GetType();
		if (!KGFAccessor.itsListSorted.ContainsKey(type))
		{
			KGFAccessor.itsListSorted[type] = new List<KGFObject>();
		}
		KGFAccessor.itsListSorted[type].Add(theObjectScript);
		foreach (Type type2 in KGFAccessor.itsListEventsAdd.Keys)
		{
			if (type2.IsAssignableFrom(type))
			{
				KGFAccessor.itsListEventsAdd[type2].Trigger(null, new KGFAccessor.KGFAccessorEventargs(theObjectScript));
			}
		}
		if (KGFAccessor.itsListEventsAddOnce.Count > 0)
		{
			List<Type> list = new List<Type>();
			foreach (Type type3 in KGFAccessor.itsListEventsAddOnce.Keys)
			{
				if (type3.IsAssignableFrom(type))
				{
					list.Add(type3);
				}
			}
			foreach (Type key in list)
			{
				KGFAccessor.itsListEventsAddOnce[key].Trigger(null, new KGFAccessor.KGFAccessorEventargs(theObjectScript));
				KGFAccessor.itsListEventsAddOnce.Remove(key);
			}
		}
	}

	// Token: 0x060009A7 RID: 2471 RVA: 0x0004B8C8 File Offset: 0x00049AC8
	public static void RemoveKGFObject(KGFObject theObjectScript)
	{
		Type type = theObjectScript.GetType();
		try
		{
			KGFAccessor.itsListSorted[type].Remove(theObjectScript);
		}
		catch
		{
		}
		foreach (Type type2 in KGFAccessor.itsListEventsRemove.Keys)
		{
			if (type2.IsAssignableFrom(type))
			{
				KGFAccessor.itsListEventsRemove[type2].Trigger(null, new KGFAccessor.KGFAccessorEventargs(theObjectScript));
			}
		}
	}

	// Token: 0x060009A8 RID: 2472 RVA: 0x0004B98C File Offset: 0x00049B8C
	public static void GetExternal<T>(Action<object, EventArgs> theRegisterCallback)
	{
		T @object = KGFAccessor.GetObject<T>();
		if (@object != null)
		{
			theRegisterCallback(null, new KGFAccessor.KGFAccessorEventargs(@object));
		}
		else
		{
			KGFAccessor.RegisterAddOnceEvent<T>(theRegisterCallback);
		}
	}

	// Token: 0x060009A9 RID: 2473 RVA: 0x0004B9C8 File Offset: 0x00049BC8
	public static void RegisterAddEvent<T>(Action<object, EventArgs> theCallback)
	{
		if (theCallback == null)
		{
			return;
		}
		Type typeFromHandle = typeof(T);
		if (!KGFAccessor.itsListEventsAdd.ContainsKey(typeFromHandle))
		{
			KGFAccessor.itsListEventsAdd[typeFromHandle] = new KGFDelegate();
		}
		Dictionary<Type, KGFDelegate> dictionary2;
		Dictionary<Type, KGFDelegate> dictionary = dictionary2 = KGFAccessor.itsListEventsAdd;
		Type key2;
		Type key = key2 = typeFromHandle;
		KGFDelegate theMyDelegate = dictionary2[key2];
		dictionary[key] = theMyDelegate + theCallback;
	}

	// Token: 0x060009AA RID: 2474 RVA: 0x0004BA28 File Offset: 0x00049C28
	public static void RegisterAddOnceEvent<T>(Action<object, EventArgs> theCallback)
	{
		if (theCallback == null)
		{
			return;
		}
		Type typeFromHandle = typeof(T);
		if (!KGFAccessor.itsListEventsAddOnce.ContainsKey(typeFromHandle))
		{
			KGFAccessor.itsListEventsAddOnce[typeFromHandle] = new KGFDelegate();
		}
		Dictionary<Type, KGFDelegate> dictionary2;
		Dictionary<Type, KGFDelegate> dictionary = dictionary2 = KGFAccessor.itsListEventsAddOnce;
		Type key2;
		Type key = key2 = typeFromHandle;
		KGFDelegate theMyDelegate = dictionary2[key2];
		dictionary[key] = theMyDelegate + theCallback;
	}

	// Token: 0x060009AB RID: 2475 RVA: 0x0004BA88 File Offset: 0x00049C88
	public static void UnregisterAddEvent<T>(Action<object, EventArgs> theCallback)
	{
		Type typeFromHandle = typeof(T);
		if (KGFAccessor.itsListEventsAdd.ContainsKey(typeFromHandle))
		{
			Dictionary<Type, KGFDelegate> dictionary2;
			Dictionary<Type, KGFDelegate> dictionary = dictionary2 = KGFAccessor.itsListEventsAdd;
			Type key2;
			Type key = key2 = typeFromHandle;
			KGFDelegate theMyDelegate = dictionary2[key2];
			dictionary[key] = theMyDelegate - theCallback;
		}
	}

	// Token: 0x060009AC RID: 2476 RVA: 0x0004BAD0 File Offset: 0x00049CD0
	public static void RegisterRemoveEvent<T>(Action<object, EventArgs> theCallback)
	{
		if (theCallback == null)
		{
			return;
		}
		Type typeFromHandle = typeof(T);
		if (!KGFAccessor.itsListEventsRemove.ContainsKey(typeFromHandle))
		{
			KGFAccessor.itsListEventsRemove[typeFromHandle] = new KGFDelegate();
		}
		Dictionary<Type, KGFDelegate> dictionary2;
		Dictionary<Type, KGFDelegate> dictionary = dictionary2 = KGFAccessor.itsListEventsRemove;
		Type key2;
		Type key = key2 = typeFromHandle;
		KGFDelegate theMyDelegate = dictionary2[key2];
		dictionary[key] = theMyDelegate + theCallback;
	}

	// Token: 0x060009AD RID: 2477 RVA: 0x0004BB30 File Offset: 0x00049D30
	public static void UnregisterRemoveEvent<T>(Action<object, EventArgs> theCallback)
	{
		Type typeFromHandle = typeof(T);
		if (KGFAccessor.itsListEventsRemove.ContainsKey(typeFromHandle))
		{
			Dictionary<Type, KGFDelegate> dictionary2;
			Dictionary<Type, KGFDelegate> dictionary = dictionary2 = KGFAccessor.itsListEventsRemove;
			Type key2;
			Type key = key2 = typeFromHandle;
			KGFDelegate theMyDelegate = dictionary2[key2];
			dictionary[key] = theMyDelegate - theCallback;
		}
	}

	// Token: 0x060009AE RID: 2478 RVA: 0x0004BB78 File Offset: 0x00049D78
	public static IEnumerable<T> GetObjectsEnumerable<T>()
	{
		foreach (object anObject in KGFAccessor.GetObjectsEnumerable(typeof(T)))
		{
			yield return (T)((object)anObject);
		}
		yield break;
	}

	// Token: 0x060009AF RID: 2479 RVA: 0x0004BB94 File Offset: 0x00049D94
	public static IEnumerable<object> GetObjectsEnumerable(Type theType)
	{
		foreach (Type aType in KGFAccessor.itsListSorted.Keys)
		{
			if (theType.IsAssignableFrom(aType))
			{
				List<KGFObject> aListObjectScripts = KGFAccessor.itsListSorted[aType];
				for (int i = aListObjectScripts.Count - 1; i >= 0; i--)
				{
					object anObject = aListObjectScripts[i];
					MonoBehaviour aMonobehaviour = aListObjectScripts[i];
					if (aMonobehaviour == null)
					{
						aListObjectScripts.RemoveAt(i);
					}
					else if (aMonobehaviour.gameObject == null)
					{
						aListObjectScripts.RemoveAt(i);
					}
					else
					{
						yield return anObject;
					}
				}
			}
		}
		yield break;
	}

	// Token: 0x060009B0 RID: 2480 RVA: 0x0004BBC0 File Offset: 0x00049DC0
	public static List<T> GetObjects<T>()
	{
		return new List<T>(KGFAccessor.GetObjectsEnumerable<T>());
	}

	// Token: 0x060009B1 RID: 2481 RVA: 0x0004BBCC File Offset: 0x00049DCC
	public static List<object> GetObjects(Type theType)
	{
		return new List<object>(KGFAccessor.GetObjectsEnumerable(theType));
	}

	// Token: 0x060009B2 RID: 2482 RVA: 0x0004BBDC File Offset: 0x00049DDC
	public static IEnumerable<string> GetObjectsNames<T>() where T : KGFObject
	{
		foreach (T t in KGFAccessor.GetObjects<T>())
		{
			KGFObject anObject = t;
			yield return anObject.name;
		}
		yield break;
	}

	// Token: 0x060009B3 RID: 2483 RVA: 0x0004BBF8 File Offset: 0x00049DF8
	public static T GetObject<T>()
	{
		using (IEnumerator<T> enumerator = KGFAccessor.GetObjectsEnumerable<T>().GetEnumerator())
		{
			if (enumerator.MoveNext())
			{
				return enumerator.Current;
			}
		}
		return default(T);
	}

	// Token: 0x060009B4 RID: 2484 RVA: 0x0004BC68 File Offset: 0x00049E68
	public static object GetObject(Type theType)
	{
		using (IEnumerator<object> enumerator = KGFAccessor.GetObjectsEnumerable(theType).GetEnumerator())
		{
			if (enumerator.MoveNext())
			{
				return enumerator.Current;
			}
		}
		return null;
	}

	// Token: 0x060009B5 RID: 2485 RVA: 0x0004BCD0 File Offset: 0x00049ED0
	public static int GetAddHandlerCount()
	{
		return KGFAccessor.itsListEventsAdd.Count;
	}

	// Token: 0x060009B6 RID: 2486 RVA: 0x0004BCDC File Offset: 0x00049EDC
	public static int GetAddOnceHandlerCount()
	{
		return KGFAccessor.itsListEventsAddOnce.Count;
	}

	// Token: 0x060009B7 RID: 2487 RVA: 0x0004BCE8 File Offset: 0x00049EE8
	public static IEnumerable<Type> GetObjectCacheListTypes()
	{
		foreach (Type aType in KGFAccessor.itsListSorted.Keys)
		{
			yield return aType;
		}
		yield break;
	}

	// Token: 0x060009B8 RID: 2488 RVA: 0x0004BD04 File Offset: 0x00049F04
	public static int GetObjectCacheListCountByType(Type theType)
	{
		if (KGFAccessor.itsListSorted.ContainsKey(theType))
		{
			return KGFAccessor.itsListSorted[theType].Count;
		}
		return 0;
	}

	// Token: 0x04000A1C RID: 2588
	private static Dictionary<Type, List<KGFObject>> itsListSorted = new Dictionary<Type, List<KGFObject>>();

	// Token: 0x04000A1D RID: 2589
	private static Dictionary<Type, KGFDelegate> itsListEventsAdd = new Dictionary<Type, KGFDelegate>();

	// Token: 0x04000A1E RID: 2590
	private static Dictionary<Type, KGFDelegate> itsListEventsAddOnce = new Dictionary<Type, KGFDelegate>();

	// Token: 0x04000A1F RID: 2591
	private static Dictionary<Type, KGFDelegate> itsListEventsRemove = new Dictionary<Type, KGFDelegate>();

	// Token: 0x02000151 RID: 337
	public class KGFAccessorEventargs : EventArgs
	{
		// Token: 0x060009B9 RID: 2489 RVA: 0x0004BD34 File Offset: 0x00049F34
		public KGFAccessorEventargs(object theObject)
		{
			this.itsObject = theObject;
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x0004BD44 File Offset: 0x00049F44
		public object GetObject()
		{
			return this.itsObject;
		}

		// Token: 0x04000A20 RID: 2592
		private object itsObject;
	}
}
