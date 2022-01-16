using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000008 RID: 8
public class CJStatsEvents : CJDefinitions
{
	// Token: 0x14000006 RID: 6
	// (add) Token: 0x06000034 RID: 52 RVA: 0x00002F14 File Offset: 0x00001114
	// (remove) Token: 0x06000035 RID: 53 RVA: 0x00002F2C File Offset: 0x0000112C
	public static event CJStatsEvents.trigger onSubmit;

	// Token: 0x14000007 RID: 7
	// (add) Token: 0x06000036 RID: 54 RVA: 0x00002F44 File Offset: 0x00001144
	// (remove) Token: 0x06000037 RID: 55 RVA: 0x00002F5C File Offset: 0x0000115C
	public static event CJStatsEvents.trigger onSubmitError;

	// Token: 0x14000008 RID: 8
	// (add) Token: 0x06000038 RID: 56 RVA: 0x00002F74 File Offset: 0x00001174
	// (remove) Token: 0x06000039 RID: 57 RVA: 0x00002F8C File Offset: 0x0000118C
	public static event CJStatsEvents.trigger guestHasSubmits;

	// Token: 0x0600003A RID: 58 RVA: 0x00002FA4 File Offset: 0x000011A4
	private void Awake()
	{
		this.comm = (GameObject.Find(this.commObj).GetComponent("CJComm") as CJComm);
		this.developerBuild = this.comm.developerBuild;
	}

	// Token: 0x0600003B RID: 59 RVA: 0x00002FD8 File Offset: 0x000011D8
	public void GuestHasSubmits()
	{
		if (CJStatsEvents.guestHasSubmits != null)
		{
			CJStatsEvents.guestHasSubmits(true);
			if (this.developerBuild)
			{
				this.comm.Call("console.log('### Event: guestHasSubmits triggered')");
			}
		}
	}

	// Token: 0x0600003C RID: 60 RVA: 0x00003018 File Offset: 0x00001218
	public void OnSubmit(string param)
	{
		if (this.developerBuild)
		{
			this.comm.Call("console.log('### Callback: onSubmit: " + param + "')");
		}
		Hashtable hashtable = new Hashtable();
		hashtable = this.comm.Read(param);
		bool flag = (string)hashtable["success"] == "true";
		if (CJStatsEvents.onSubmit != null)
		{
			if (flag)
			{
				CJStatsEvents.onSubmit(true);
				if (this.developerBuild)
				{
					this.comm.Call("console.log('### Event: onSubmit triggered')");
				}
			}
			else
			{
				CJStatsEvents.onSubmit(false);
			}
		}
	}

	// Token: 0x0600003D RID: 61 RVA: 0x000030CC File Offset: 0x000012CC
	public void OnSubmitError(string param)
	{
		if (this.developerBuild)
		{
			this.comm.Call("console.log('### Callback: onSubmitError: " + param + "')");
		}
		Hashtable hashtable = new Hashtable();
		hashtable = this.comm.Read(param);
		bool flag = (string)hashtable["success"] == "false";
		if (CJStatsEvents.onSubmitError != null)
		{
			if (flag)
			{
				CJStatsEvents.onSubmitError(true);
				if (this.developerBuild)
				{
					this.comm.Call("console.log('### Event: onSubmitError triggered')");
				}
			}
			else
			{
				CJStatsEvents.onSubmitError(false);
			}
		}
	}

	// Token: 0x0400001E RID: 30
	private CJComm comm;

	// Token: 0x02000090 RID: 144
	// (Invoke) Token: 0x06000430 RID: 1072
	public delegate void trigger(bool result);
}
