using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200000A RID: 10
public class CJUsersEvents : CJDefinitions
{
	// Token: 0x14000009 RID: 9
	// (add) Token: 0x06000043 RID: 67 RVA: 0x00003224 File Offset: 0x00001424
	// (remove) Token: 0x06000044 RID: 68 RVA: 0x0000323C File Offset: 0x0000143C
	public static event CJUsersEvents.trigger onGetUsername;

	// Token: 0x1400000A RID: 10
	// (add) Token: 0x06000045 RID: 69 RVA: 0x00003254 File Offset: 0x00001454
	// (remove) Token: 0x06000046 RID: 70 RVA: 0x0000326C File Offset: 0x0000146C
	public static event CJUsersEvents.trigger onGetUsernameError;

	// Token: 0x06000047 RID: 71 RVA: 0x00003284 File Offset: 0x00001484
	private void Awake()
	{
		this.comm = (GameObject.Find(this.commObj).GetComponent("CJComm") as CJComm);
		this.developerBuild = this.comm.developerBuild;
	}

	// Token: 0x06000048 RID: 72 RVA: 0x000032B8 File Offset: 0x000014B8
	public void OnGetUsername(string param)
	{
		if (this.developerBuild)
		{
			this.comm.Call("console.log('### Callback: onGetUsername: " + param + "')");
		}
		Hashtable hashtable = new Hashtable();
		hashtable = this.comm.Read(param);
		bool flag = (string)hashtable["success"] == "true";
		this.comm.username = (hashtable["username"] as string);
		if (CJUsersEvents.onGetUsername != null)
		{
			if (flag)
			{
				CJUsersEvents.onGetUsername(true);
				if (this.developerBuild)
				{
					this.comm.Call("console.log('### Event: OnGetUsername triggered')");
				}
			}
			else
			{
				CJUsersEvents.onGetUsername(false);
			}
		}
	}

	// Token: 0x06000049 RID: 73 RVA: 0x00003388 File Offset: 0x00001588
	public void OnGetUsernameError(string param)
	{
		if (this.developerBuild)
		{
			this.comm.Call("console.warn('### Callback: onGetUsernameError: " + param + "')");
		}
		Hashtable hashtable = new Hashtable();
		hashtable = this.comm.Read(param);
		bool flag = (string)hashtable["success"] == "false";
		if (CJUsersEvents.onGetUsernameError != null)
		{
			if (flag)
			{
				CJUsersEvents.onGetUsernameError(true);
				if (this.developerBuild)
				{
					this.comm.Call("console.log('### Event: OnGetUsernameError triggered')");
				}
			}
			else
			{
				CJUsersEvents.onGetUsernameError(false);
			}
		}
	}

	// Token: 0x04000023 RID: 35
	private CJComm comm;

	// Token: 0x02000091 RID: 145
	// (Invoke) Token: 0x06000434 RID: 1076
	public delegate void trigger(bool result);
}
