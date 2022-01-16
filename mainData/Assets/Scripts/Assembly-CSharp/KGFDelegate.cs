using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000183 RID: 387
public class KGFDelegate
{
	// Token: 0x06000BAE RID: 2990 RVA: 0x00056320 File Offset: 0x00054520
	public void Trigger(object theSender)
	{
		this.Trigger(theSender, null);
	}

	// Token: 0x06000BAF RID: 2991 RVA: 0x0005632C File Offset: 0x0005452C
	public void Trigger(object theSender, EventArgs theArgs)
	{
		for (int i = this.itsDelegateList.Count - 1; i >= 0; i--)
		{
			Action<object, EventArgs> action = this.itsDelegateList[i];
			if (action == null)
			{
				this.itsDelegateList.RemoveAt(i);
			}
			else if (action.Target == null)
			{
				this.itsDelegateList.RemoveAt(i);
			}
			else if (action.Target is MonoBehaviour && (MonoBehaviour)action.Target == null)
			{
				this.itsDelegateList.RemoveAt(i);
			}
			else
			{
				action(theSender, theArgs);
			}
		}
	}

	// Token: 0x06000BB0 RID: 2992 RVA: 0x000563D8 File Offset: 0x000545D8
	public void Clear()
	{
		this.itsDelegateList.Clear();
	}

	// Token: 0x06000BB1 RID: 2993 RVA: 0x000563E8 File Offset: 0x000545E8
	public static KGFDelegate operator +(KGFDelegate theMyDelegate, Action<object, EventArgs> theDelegate)
	{
		theMyDelegate.itsDelegateList.Add(theDelegate);
		return theMyDelegate;
	}

	// Token: 0x06000BB2 RID: 2994 RVA: 0x000563F8 File Offset: 0x000545F8
	public static KGFDelegate operator -(KGFDelegate theMyDelegate, Action<object, EventArgs> theDelegate)
	{
		theMyDelegate.itsDelegateList.Remove(theDelegate);
		return theMyDelegate;
	}

	// Token: 0x04000B92 RID: 2962
	private List<Action<object, EventArgs>> itsDelegateList = new List<Action<object, EventArgs>>();
}
