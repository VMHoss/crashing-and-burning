using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

// Token: 0x02000157 RID: 343
[Serializable]
public class KGFEvent : KGFEventBase, KGFIValidator
{
	// Token: 0x060009D8 RID: 2520 RVA: 0x0004C298 File Offset: 0x0004A498
	public void SetDestination(GameObject theGameObject, string theComponentName, string theMethodString)
	{
		this.itsEventData.itsObject = theGameObject;
		this.itsEventData.itsComponentName = theComponentName;
		this.itsEventData.itsMethodName = theMethodString;
	}

	// Token: 0x060009D9 RID: 2521 RVA: 0x0004C2CC File Offset: 0x0004A4CC
	private static bool FindMethod(KGFEvent.KGFEventData theEventData, out MethodInfo theMethod, out MonoBehaviour theComponent)
	{
		theMethod = null;
		theComponent = null;
		if (theEventData.itsRuntimeObjectSearch)
		{
			foreach (MethodInfo methodInfo in KGFEvent.GetMethods(theEventData.GetRuntimeType(), theEventData))
			{
				string methodString = KGFEvent.GetMethodString(methodInfo);
				if (methodString == theEventData.itsMethodName)
				{
					theMethod = methodInfo;
					return true;
				}
			}
		}
		else if (theEventData.itsObject != null)
		{
			MonoBehaviour[] components = theEventData.itsObject.GetComponents<MonoBehaviour>();
			foreach (MonoBehaviour monoBehaviour in components)
			{
				if (monoBehaviour.GetType().Name == theEventData.itsComponentName)
				{
					theComponent = monoBehaviour;
					foreach (MethodInfo methodInfo2 in KGFEvent.GetMethods(monoBehaviour.GetType(), theEventData))
					{
						string methodString2 = KGFEvent.GetMethodString(methodInfo2);
						if (methodString2 == theEventData.itsMethodName)
						{
							theMethod = methodInfo2;
							return true;
						}
					}
				}
			}
		}
		return false;
	}

	// Token: 0x060009DA RID: 2522 RVA: 0x0004C3E4 File Offset: 0x0004A5E4
	public override void Trigger()
	{
		this.itsEventData.Trigger(this, new object[0]);
	}

	// Token: 0x060009DB RID: 2523 RVA: 0x0004C3F8 File Offset: 0x0004A5F8
	private static bool SearchInstanceForVariable(Type theType, object theInstance, string theName, ref object theValue)
	{
		FieldInfo field = theType.GetField(theName);
		if (field != null)
		{
			theValue = field.GetValue(theInstance);
			return true;
		}
		return false;
	}

	// Token: 0x060009DC RID: 2524 RVA: 0x0004C420 File Offset: 0x0004A620
	private static object[] ConvertParameters(ParameterInfo[] theMethodParametersList, KGFEvent.EventParameter[] theParametersList)
	{
		object[] array = new object[theMethodParametersList.Length];
		for (int i = 0; i < theMethodParametersList.Length; i++)
		{
			if (typeof(UnityEngine.Object).IsAssignableFrom(theMethodParametersList[i].ParameterType))
			{
				array[i] = theParametersList[i].itsValueUnityObject;
			}
			else if (!KGFEvent.SearchInstanceForVariable(typeof(KGFEvent.EventParameter), theParametersList[i], "itsValue" + theMethodParametersList[i].ParameterType.Name, ref array[i]))
			{
				Debug.LogError("could not find variable for type:" + theMethodParametersList[i].ParameterType.Name);
			}
		}
		return array;
	}

	// Token: 0x060009DD RID: 2525 RVA: 0x0004C4D0 File Offset: 0x0004A6D0
	public static MethodInfo[] GetMethods(Type theType, KGFEvent.KGFEventData theData)
	{
		List<MethodInfo> list = new List<MethodInfo>();
		for (Type type = theType; type != null; type = type.BaseType)
		{
			MethodInfo[] methods = type.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
			foreach (MethodInfo methodInfo in methods)
			{
				if (methodInfo.GetCustomAttributes(typeof(KGFEventExpose), true).Length > 0 && theData.CheckMethod(methodInfo))
				{
					list.Add(methodInfo);
				}
			}
		}
		return list.ToArray();
	}

	// Token: 0x060009DE RID: 2526 RVA: 0x0004C554 File Offset: 0x0004A754
	public static string GetMethodString(MethodInfo theMethod)
	{
		return theMethod.ToString();
	}

	// Token: 0x060009DF RID: 2527 RVA: 0x0004C55C File Offset: 0x0004A75C
	public static void LogError(string theMessage, string theCategory, MonoBehaviour theCaller)
	{
		Debug.LogError(theMessage);
	}

	// Token: 0x060009E0 RID: 2528 RVA: 0x0004C564 File Offset: 0x0004A764
	public static void LogDebug(string theMessage, string theCategory, MonoBehaviour theCaller)
	{
		Debug.Log(theMessage);
	}

	// Token: 0x060009E1 RID: 2529 RVA: 0x0004C56C File Offset: 0x0004A76C
	public static void LogWarning(string theMessage, string theCategory, MonoBehaviour theCaller)
	{
		Debug.LogWarning(theMessage);
	}

	// Token: 0x060009E2 RID: 2530 RVA: 0x0004C574 File Offset: 0x0004A774
	public override KGFMessageList Validate()
	{
		KGFMessageList kgfmessageList = new KGFMessageList();
		if ((string.Empty + this.itsEventData.itsMethodName).Trim() == string.Empty)
		{
			kgfmessageList.AddError("itsMethod is empty");
		}
		if (!this.itsEventData.itsRuntimeObjectSearch)
		{
			if (this.itsEventData.itsObject == null)
			{
				kgfmessageList.AddError("itsObject == null");
			}
			if ((string.Empty + this.itsEventData.itsComponentName).Trim() == string.Empty)
			{
				kgfmessageList.AddError("itsScript is empty");
			}
			MethodInfo methodInfo;
			MonoBehaviour monoBehaviour;
			if (this.itsEventData.itsObject != null && !KGFEvent.FindMethod(this.itsEventData, out methodInfo, out monoBehaviour))
			{
				kgfmessageList.AddError("method could not be found");
			}
		}
		if (this.itsEventData.itsRuntimeObjectSearch)
		{
			Type runtimeType = this.itsEventData.GetRuntimeType();
			if (runtimeType == null)
			{
				kgfmessageList.AddError("could not find type");
			}
			else if (runtimeType.IsInterface)
			{
				kgfmessageList.AddWarning("you used an interface, please ensure that the objects you want to call the method on are derrived from KGFObject");
			}
			else
			{
				if (!typeof(MonoBehaviour).IsAssignableFrom(runtimeType))
				{
					kgfmessageList.AddError("type must be derrived from Monobehaviour");
				}
				if (!typeof(KGFObject).IsAssignableFrom(runtimeType))
				{
					kgfmessageList.AddWarning("please derrive from KGFObject because it will be faster to search");
				}
			}
		}
		return kgfmessageList;
	}

	// Token: 0x04000A2A RID: 2602
	private const string itsEventCategory = "KGFEventSystem";

	// Token: 0x04000A2B RID: 2603
	public KGFEvent.KGFEventData itsEventData = new KGFEvent.KGFEventData();

	// Token: 0x02000158 RID: 344
	[Serializable]
	public class KGFEventData
	{
		// Token: 0x060009E3 RID: 2531 RVA: 0x0004C6E4 File Offset: 0x0004A8E4
		public KGFEventData()
		{
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x0004C748 File Offset: 0x0004A948
		public KGFEventData(bool thePassThroughMode, params KGFEvent.EventParameterType[] theParameterTypes)
		{
			this.itsParameterTypes = theParameterTypes;
			this.itsPassthroughMode = thePassThroughMode;
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x0004C7B8 File Offset: 0x0004A9B8
		public Type GetRuntimeType()
		{
			return Type.GetType(this.itsRuntimeObjectSearchType);
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x0004C7C8 File Offset: 0x0004A9C8
		public bool GetDirectPassThroughMode()
		{
			return this.itsPassthroughMode;
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x0004C7D0 File Offset: 0x0004A9D0
		public void SetDirectPassThroughMode(bool thePassThroughMode)
		{
			this.itsPassthroughMode = thePassThroughMode;
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x0004C7DC File Offset: 0x0004A9DC
		public void SetRuntimeParameterInfos(params KGFEvent.EventParameterType[] theParameterTypes)
		{
			if (theParameterTypes == null)
			{
				this.itsParameterTypes = new KGFEvent.EventParameterType[0];
			}
			else
			{
				this.itsParameterTypes = theParameterTypes;
			}
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x0004C7FC File Offset: 0x0004A9FC
		public KGFEvent.EventParameterType[] GetParameterLinkTypes()
		{
			return this.itsParameterTypes;
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x0004C804 File Offset: 0x0004AA04
		public bool GetSupportsRuntimeParameterInfos()
		{
			return this.itsParameterTypes.Length > 0;
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x0004C814 File Offset: 0x0004AA14
		public bool GetIsParameterLinked(int theParameterIndex)
		{
			return this.GetSupportsRuntimeParameterInfos() && theParameterIndex < this.itsParameters.Length && this.itsParameters[theParameterIndex].itsLinked;
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x0004C84C File Offset: 0x0004AA4C
		public void SetIsParameterLinked(int theParameterIndex, bool theLinkState)
		{
			if (theParameterIndex >= this.itsParameters.Length)
			{
				return;
			}
			this.itsParameters[theParameterIndex].itsLinked = theLinkState;
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x0004C86C File Offset: 0x0004AA6C
		public int GetParameterLink(int theParameterIndex)
		{
			if (theParameterIndex >= this.itsParameters.Length)
			{
				return 0;
			}
			return this.itsParameters[theParameterIndex].itsLink;
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x0004C88C File Offset: 0x0004AA8C
		public void SetParameterLink(int theParameterIndex, int theLink)
		{
			if (theParameterIndex >= this.itsParameters.Length)
			{
				return;
			}
			this.itsParameters[theParameterIndex].itsLink = theLink;
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x0004C8AC File Offset: 0x0004AAAC
		public KGFEvent.EventParameter[] GetParameters()
		{
			return this.itsParameters;
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x0004C8B4 File Offset: 0x0004AAB4
		public void SetParameters(KGFEvent.EventParameter[] theParameters)
		{
			this.itsParameters = theParameters;
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x0004C8C0 File Offset: 0x0004AAC0
		public GameObject GetGameObject()
		{
			return this.itsObject;
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x0004C8C8 File Offset: 0x0004AAC8
		private object GetFieldValueByReflection(MonoBehaviour theCaller, string theMemberName)
		{
			Type type = theCaller.GetType();
			FieldInfo field = type.GetField(theMemberName);
			if (field != null)
			{
				return field.GetValue(theCaller);
			}
			return null;
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x0004C8F4 File Offset: 0x0004AAF4
		public void Trigger(MonoBehaviour theCaller, params object[] theParameters)
		{
			List<object> list = new List<object>(theParameters);
			foreach (KGFEvent.EventParameterType eventParameterType in this.itsParameterTypes)
			{
				if (eventParameterType.GetCopyFromSourceObject())
				{
					list.Add(this.GetFieldValueByReflection(theCaller, eventParameterType.itsName));
				}
			}
			if (this.itsRuntimeObjectSearch)
			{
				this.TriggerRuntimeSearch(theCaller, list.ToArray());
			}
			else
			{
				this.TriggerDefault(theCaller, list.ToArray());
			}
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x0004C970 File Offset: 0x0004AB70
		private int GetParameterIndexWithType(int theIndex, string theType)
		{
			int num = 0;
			for (int i = 0; i < this.itsParameterTypes.Length; i++)
			{
				KGFEvent.EventParameterType eventParameterType = this.itsParameterTypes[i];
				if (eventParameterType.itsTypeName == theType)
				{
					if (num == theIndex)
					{
						return i;
					}
					num++;
				}
			}
			return 0;
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x0004C9C0 File Offset: 0x0004ABC0
		private bool CheckRuntimeObjectName(MonoBehaviour theMonobehaviour)
		{
			return this.itsRuntimeObjectSearchFilter.Trim() == string.Empty || this.itsRuntimeObjectSearchFilter == theMonobehaviour.name;
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x0004CA04 File Offset: 0x0004AC04
		private void TriggerRuntimeSearch(MonoBehaviour theCaller, object[] theRuntimeParameters)
		{
			Type runtimeType = this.GetRuntimeType();
			if (runtimeType == null)
			{
				KGFEvent.LogError("could not find type", "KGFEventSystem", theCaller);
				return;
			}
			if (this.itsMethodName == null)
			{
				KGFEvent.LogError("event has no selected method", "KGFEventSystem", theCaller);
				return;
			}
			MethodInfo methodInfo;
			MonoBehaviour monoBehaviour;
			if (!KGFEvent.FindMethod(this, out methodInfo, out monoBehaviour))
			{
				KGFEvent.LogError("Could not find method on object.", "KGFEventSystem", theCaller);
				return;
			}
			object[] array = null;
			if (this.GetDirectPassThroughMode())
			{
				array = theRuntimeParameters;
			}
			else
			{
				ParameterInfo[] parameters = methodInfo.GetParameters();
				array = KGFEvent.ConvertParameters(parameters, this.itsParameters);
				for (int i = 0; i < this.itsParameters.Length; i++)
				{
					if (this.GetIsParameterLinked(i))
					{
						int parameterIndexWithType = this.GetParameterIndexWithType(this.GetParameterLink(i), parameters[i].ParameterType.FullName);
						if (parameterIndexWithType < theRuntimeParameters.Length)
						{
							array[i] = theRuntimeParameters[parameterIndexWithType];
						}
						else
						{
							Debug.LogError("you did not give enough parameters");
						}
					}
				}
			}
			List<MonoBehaviour> list = new List<MonoBehaviour>();
			try
			{
				if (runtimeType.IsInterface || typeof(KGFObject).IsAssignableFrom(runtimeType))
				{
					foreach (object obj in KGFAccessor.GetObjects(runtimeType))
					{
						MonoBehaviour monoBehaviour2 = obj as MonoBehaviour;
						if (monoBehaviour2 != null && this.CheckRuntimeObjectName(monoBehaviour2))
						{
							methodInfo.Invoke(obj, array);
							list.Add(monoBehaviour2);
						}
					}
				}
				else if (!runtimeType.IsInterface)
				{
					foreach (UnityEngine.Object obj2 in UnityEngine.Object.FindObjectsOfType(runtimeType))
					{
						MonoBehaviour monoBehaviour3 = obj2 as MonoBehaviour;
						if (monoBehaviour3 != null && this.CheckRuntimeObjectName(monoBehaviour3))
						{
							methodInfo.Invoke(obj2, array);
							list.Add(monoBehaviour3);
						}
					}
				}
			}
			catch (Exception arg)
			{
				KGFEvent.LogError("invoked method caused exception in event_generic:" + arg, "KGFEventSystem", theCaller);
			}
			List<string> list2 = new List<string>();
			if (array != null)
			{
				foreach (object arg2 in array)
				{
					list2.Add(string.Empty + arg2);
				}
			}
			foreach (MonoBehaviour monoBehaviour4 in list)
			{
				string theMessage = string.Format("{0}({1}): {2} ({3})", new object[]
				{
					monoBehaviour4.name,
					this.itsRuntimeObjectSearchType,
					methodInfo.Name,
					string.Join(",", list2.ToArray())
				});
				KGFEvent.LogDebug(theMessage, "KGFEventSystem", theCaller);
			}
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x0004CD3C File Offset: 0x0004AF3C
		private void TriggerDefault(MonoBehaviour theCaller, params object[] theRuntimeParameters)
		{
			if (this.itsObject == null)
			{
				KGFEvent.LogError("event has null object", "KGFEventSystem", theCaller);
				return;
			}
			if (this.itsComponentName == null)
			{
				KGFEvent.LogError("event has no selected component", "KGFEventSystem", theCaller);
				return;
			}
			if (this.itsMethodName == null)
			{
				KGFEvent.LogError("event has no selected method", "KGFEventSystem", theCaller);
				return;
			}
			MethodInfo methodInfo;
			MonoBehaviour monoBehaviour;
			if (!KGFEvent.FindMethod(this, out methodInfo, out monoBehaviour))
			{
				KGFEvent.LogError("Could not find method on object.", "KGFEventSystem", theCaller);
				return;
			}
			object[] array = null;
			if (this.GetDirectPassThroughMode())
			{
				array = theRuntimeParameters;
			}
			else
			{
				ParameterInfo[] parameters = methodInfo.GetParameters();
				array = KGFEvent.ConvertParameters(parameters, this.itsParameters);
				for (int i = 0; i < this.itsParameters.Length; i++)
				{
					if (this.GetIsParameterLinked(i))
					{
						int parameterIndexWithType = this.GetParameterIndexWithType(this.GetParameterLink(i), parameters[i].ParameterType.FullName);
						if (parameterIndexWithType < theRuntimeParameters.Length)
						{
							array[i] = theRuntimeParameters[parameterIndexWithType];
						}
						else
						{
							Debug.LogError("you did not give enough parameters");
						}
					}
				}
			}
			try
			{
				methodInfo.Invoke(monoBehaviour, array);
			}
			catch (Exception arg)
			{
				KGFEvent.LogError("invoked method caused exception in event_generic:" + arg, "KGFEventSystem", theCaller);
			}
			List<string> list = new List<string>();
			if (array != null)
			{
				foreach (object arg2 in array)
				{
					list.Add(string.Empty + arg2);
				}
			}
			string theMessage = string.Format("{0}({1}): {2} ({3})", new object[]
			{
				this.itsObject.name,
				monoBehaviour.GetType().Name,
				methodInfo.Name,
				string.Join(",", list.ToArray())
			});
			KGFEvent.LogDebug(theMessage, "KGFEventSystem", theCaller);
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x0004CF38 File Offset: 0x0004B138
		public void SetMethodFilter(KGFEvent.KGFEventFilterMethod theFilter)
		{
			this.itsFilterMethod = theFilter;
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x0004CF44 File Offset: 0x0004B144
		public void ClearMethodFilter()
		{
			this.itsFilterMethod = null;
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x0004CF50 File Offset: 0x0004B150
		private KGFEvent.KGFEventFilterMethod GetFilterMethod()
		{
			return this.itsFilterMethod;
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x0004CF58 File Offset: 0x0004B158
		public bool CheckMethod(MethodInfo theMethod)
		{
			if (this.itsFilterMethod != null && !this.GetFilterMethod()(theMethod))
			{
				return false;
			}
			if (this.GetSupportsRuntimeParameterInfos() && this.GetDirectPassThroughMode())
			{
				ParameterInfo[] parameters = theMethod.GetParameters();
				if (parameters.Length != this.itsParameterTypes.Length)
				{
					return false;
				}
				for (int i = 0; i < parameters.Length; i++)
				{
					if (!this.itsParameterTypes[i].GetIsMatchingType(parameters[i].ParameterType))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x0004CFE4 File Offset: 0x0004B1E4
		public KGFMessageList GetErrors()
		{
			KGFMessageList kgfmessageList = new KGFMessageList();
			if (string.IsNullOrEmpty(this.itsMethodName))
			{
				kgfmessageList.AddError("Empty method name");
			}
			if (this.itsRuntimeObjectSearch && string.IsNullOrEmpty(this.itsRuntimeObjectSearchType))
			{
				kgfmessageList.AddError("Empty type field");
			}
			MethodInfo methodInfo;
			MonoBehaviour monoBehaviour;
			if (!KGFEvent.FindMethod(this, out methodInfo, out monoBehaviour))
			{
				kgfmessageList.AddError("Could not find method on object.");
			}
			else
			{
				ParameterInfo[] parameters = methodInfo.GetParameters();
				for (int i = 0; i < this.itsParameters.Length; i++)
				{
					if (!this.GetIsParameterLinked(i) && typeof(UnityEngine.Object).IsAssignableFrom(parameters[i].ParameterType) && this.itsParameters[i].itsValueUnityObject == null)
					{
						kgfmessageList.AddError("Empty unity object in parameters");
					}
				}
			}
			return kgfmessageList;
		}

		// Token: 0x04000A2C RID: 2604
		public bool itsRuntimeObjectSearch;

		// Token: 0x04000A2D RID: 2605
		public string itsRuntimeObjectSearchType = string.Empty;

		// Token: 0x04000A2E RID: 2606
		public string itsRuntimeObjectSearchFilter = string.Empty;

		// Token: 0x04000A2F RID: 2607
		public GameObject itsObject;

		// Token: 0x04000A30 RID: 2608
		public string itsComponentName = string.Empty;

		// Token: 0x04000A31 RID: 2609
		public string itsMethodName = string.Empty;

		// Token: 0x04000A32 RID: 2610
		public string itsMethodNameShort = string.Empty;

		// Token: 0x04000A33 RID: 2611
		public KGFEvent.EventParameter[] itsParameters = new KGFEvent.EventParameter[0];

		// Token: 0x04000A34 RID: 2612
		public KGFEvent.EventParameterType[] itsParameterTypes = new KGFEvent.EventParameterType[0];

		// Token: 0x04000A35 RID: 2613
		public bool itsPassthroughMode;

		// Token: 0x04000A36 RID: 2614
		private KGFEvent.KGFEventFilterMethod itsFilterMethod;
	}

	// Token: 0x02000159 RID: 345
	[Serializable]
	public class EventParameterType
	{
		// Token: 0x060009FD RID: 2557 RVA: 0x0004D0CC File Offset: 0x0004B2CC
		public EventParameterType()
		{
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x0004D0D4 File Offset: 0x0004B2D4
		public EventParameterType(string theName, Type theType)
		{
			this.itsName = theName;
			this.itsTypeName = theType.FullName;
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x0004D0F0 File Offset: 0x0004B2F0
		public void SetCopyFromSourceObject(bool theCopy)
		{
			this.itsCopyFromSourceObject = theCopy;
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x0004D0FC File Offset: 0x0004B2FC
		public bool GetCopyFromSourceObject()
		{
			return this.itsCopyFromSourceObject;
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x0004D104 File Offset: 0x0004B304
		public bool GetIsMatchingType(Type theOtherParameterType)
		{
			return this.itsTypeName == theOtherParameterType.FullName;
		}

		// Token: 0x04000A37 RID: 2615
		public string itsName;

		// Token: 0x04000A38 RID: 2616
		public string itsTypeName;

		// Token: 0x04000A39 RID: 2617
		public bool itsCopyFromSourceObject;
	}

	// Token: 0x0200015A RID: 346
	[Serializable]
	public class EventParameter
	{
		// Token: 0x06000A02 RID: 2562 RVA: 0x0004D118 File Offset: 0x0004B318
		public EventParameter()
		{
			this.itsValueUnityObject = null;
		}

		// Token: 0x04000A3A RID: 2618
		public int itsValueInt32;

		// Token: 0x04000A3B RID: 2619
		public string itsValueString;

		// Token: 0x04000A3C RID: 2620
		public float itsValueSingle;

		// Token: 0x04000A3D RID: 2621
		public double itsValueDouble;

		// Token: 0x04000A3E RID: 2622
		public Color itsValueColor;

		// Token: 0x04000A3F RID: 2623
		public Rect itsValueRect;

		// Token: 0x04000A40 RID: 2624
		public Vector2 itsValueVector2;

		// Token: 0x04000A41 RID: 2625
		public Vector3 itsValueVector3;

		// Token: 0x04000A42 RID: 2626
		public Vector4 itsValueVector4;

		// Token: 0x04000A43 RID: 2627
		public bool itsValueBoolean;

		// Token: 0x04000A44 RID: 2628
		public UnityEngine.Object itsValueUnityObject;

		// Token: 0x04000A45 RID: 2629
		public bool itsLinked;

		// Token: 0x04000A46 RID: 2630
		public int itsLink;
	}

	// Token: 0x020001F5 RID: 501
	// (Invoke) Token: 0x06000DF1 RID: 3569
	public delegate bool KGFEventFilterMethod(MethodInfo theMethod);
}
