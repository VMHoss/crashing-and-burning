using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200015F RID: 351
public class KGFGUIRenderer : KGFObject, KGFIValidator
{
	// Token: 0x06000A27 RID: 2599 RVA: 0x0004D73C File Offset: 0x0004B93C
	protected override void KGFAwake()
	{
		this.itsGUIs = KGFAccessor.GetObjects<KGFIGui2D>();
		KGFAccessor.RegisterAddEvent<KGFIGui2D>(new Action<object, EventArgs>(this.OnRegisterKGFIGui2D));
		KGFAccessor.RegisterRemoveEvent<KGFIGui2D>(new Action<object, EventArgs>(this.OnUnregisterKGFIGui2D));
	}

	// Token: 0x06000A28 RID: 2600 RVA: 0x0004D76C File Offset: 0x0004B96C
	private void OnRegisterKGFIGui2D(object theSender, EventArgs theArgs)
	{
		KGFAccessor.KGFAccessorEventargs kgfaccessorEventargs = theArgs as KGFAccessor.KGFAccessorEventargs;
		if (kgfaccessorEventargs != null)
		{
			KGFIGui2D kgfigui2D = kgfaccessorEventargs.GetObject() as KGFIGui2D;
			if (kgfigui2D != null)
			{
				this.itsGUIs.Add(kgfigui2D);
				this.itsGUIs.Sort(new Comparison<KGFIGui2D>(this.CompareKGFIGui2D));
			}
		}
	}

	// Token: 0x06000A29 RID: 2601 RVA: 0x0004D7BC File Offset: 0x0004B9BC
	private void OnUnregisterKGFIGui2D(object theSender, EventArgs theArgs)
	{
		KGFAccessor.KGFAccessorEventargs kgfaccessorEventargs = theArgs as KGFAccessor.KGFAccessorEventargs;
		if (kgfaccessorEventargs != null)
		{
			KGFIGui2D kgfigui2D = kgfaccessorEventargs.GetObject() as KGFIGui2D;
			if (kgfigui2D != null && this.itsGUIs.Contains(kgfigui2D))
			{
				this.itsGUIs.Remove(kgfigui2D);
			}
		}
	}

	// Token: 0x06000A2A RID: 2602 RVA: 0x0004D808 File Offset: 0x0004BA08
	private int CompareKGFIGui2D(KGFIGui2D theGui1, KGFIGui2D theGui2)
	{
		return theGui1.GetLayer().CompareTo(theGui2.GetLayer());
	}

	// Token: 0x06000A2B RID: 2603 RVA: 0x0004D82C File Offset: 0x0004BA2C
	protected void OnGUI()
	{
		float scaleFactor2D = KGFScreen.GetScaleFactor2D();
		GUIUtility.ScaleAroundPivot(new Vector2(scaleFactor2D, scaleFactor2D), Vector2.zero);
		foreach (KGFIGui2D kgfigui2D in this.itsGUIs)
		{
			kgfigui2D.RenderGUI();
		}
		GUI.matrix = Matrix4x4.identity;
	}

	// Token: 0x06000A2C RID: 2604 RVA: 0x0004D8B4 File Offset: 0x0004BAB4
	public virtual KGFMessageList Validate()
	{
		return new KGFMessageList();
	}

	// Token: 0x04000A59 RID: 2649
	private List<KGFIGui2D> itsGUIs = new List<KGFIGui2D>();
}
