using System;
using UnityEngine;

// Token: 0x02000191 RID: 401
public class KGFObject : MonoBehaviour
{
	// Token: 0x06000BC8 RID: 3016 RVA: 0x000567A8 File Offset: 0x000549A8
	protected virtual void Awake()
	{
		KGFAccessor.AddKGFObject(this);
		this.EventOnAwake.Trigger(this);
		this.KGFAwake();
	}

	// Token: 0x06000BC9 RID: 3017 RVA: 0x000567C4 File Offset: 0x000549C4
	private void OnDestroy()
	{
		this.EventOnDestroy.Trigger(this);
		KGFAccessor.RemoveKGFObject(this);
		this.KGFDestroy();
	}

	// Token: 0x06000BCA RID: 3018 RVA: 0x000567E0 File Offset: 0x000549E0
	protected virtual void KGFAwake()
	{
	}

	// Token: 0x06000BCB RID: 3019 RVA: 0x000567E4 File Offset: 0x000549E4
	protected virtual void KGFDestroy()
	{
	}

	// Token: 0x04000BA3 RID: 2979
	public KGFDelegate EventOnAwake = new KGFDelegate();

	// Token: 0x04000BA4 RID: 2980
	public KGFDelegate EventOnDestroy = new KGFDelegate();
}
