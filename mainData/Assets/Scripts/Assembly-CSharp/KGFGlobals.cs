using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000189 RID: 393
public static class KGFGlobals
{
	// Token: 0x06000BB6 RID: 2998 RVA: 0x0005648C File Offset: 0x0005468C
	public static string GetObjectPath(this GameObject theObject)
	{
		List<string> list = new List<string>();
		Transform transform = theObject.transform;
		do
		{
			list.Add(transform.name);
			transform = transform.parent;
		}
		while (transform != null);
		list.Reverse();
		return string.Join("/", list.ToArray());
	}
}
