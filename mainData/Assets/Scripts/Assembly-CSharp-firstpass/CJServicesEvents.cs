using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000006 RID: 6
public class CJServicesEvents : CJDefinitions
{
	// Token: 0x14000002 RID: 2
	// (add) Token: 0x06000022 RID: 34 RVA: 0x00002998 File Offset: 0x00000B98
	// (remove) Token: 0x06000023 RID: 35 RVA: 0x000029B0 File Offset: 0x00000BB0
	public static event CJServicesEvents.trigger onIsSignedIn;

	// Token: 0x14000003 RID: 3
	// (add) Token: 0x06000024 RID: 36 RVA: 0x000029C8 File Offset: 0x00000BC8
	// (remove) Token: 0x06000025 RID: 37 RVA: 0x000029E0 File Offset: 0x00000BE0
	public static event CJServicesEvents.trigger onIsSignedInError;

	// Token: 0x14000004 RID: 4
	// (add) Token: 0x06000026 RID: 38 RVA: 0x000029F8 File Offset: 0x00000BF8
	// (remove) Token: 0x06000027 RID: 39 RVA: 0x00002A10 File Offset: 0x00000C10
	public static event CJServicesEvents.trigger onLogin;

	// Token: 0x14000005 RID: 5
	// (add) Token: 0x06000028 RID: 40 RVA: 0x00002A28 File Offset: 0x00000C28
	// (remove) Token: 0x06000029 RID: 41 RVA: 0x00002A40 File Offset: 0x00000C40
	public static event CJServicesEvents.trigger onLogout;

	// Token: 0x0600002A RID: 42 RVA: 0x00002A58 File Offset: 0x00000C58
	private void Awake()
	{
		this.comm = (GameObject.Find(this.commObj).GetComponent("CJComm") as CJComm);
		this.developerBuild = this.comm.developerBuild;
	}

	// Token: 0x0600002B RID: 43 RVA: 0x00002A8C File Offset: 0x00000C8C
	public void OnIsSignedIn(string param)
	{
		if (this.developerBuild)
		{
			this.comm.Call("console.log('### Callback: onIsSignedIn: " + param + "')");
		}
		Hashtable hashtable = new Hashtable();
		hashtable = this.comm.Read(param);
		bool flag = (string)hashtable["success"] == "true";
		if ((string)hashtable["isSignedIn"] == "true")
		{
			this.comm.isSignedIn = true;
		}
		if ((string)hashtable["isSignedIn"] == "false")
		{
			this.comm.isSignedIn = false;
		}
		if (CJServicesEvents.onIsSignedIn != null)
		{
			if (flag)
			{
				CJServicesEvents.onIsSignedIn(true);
				if (this.developerBuild)
				{
					this.comm.Call("console.log('### Event: onIsSignedIn triggered')");
				}
			}
			else
			{
				CJServicesEvents.onIsSignedIn(false);
			}
		}
	}

	// Token: 0x0600002C RID: 44 RVA: 0x00002B98 File Offset: 0x00000D98
	public void OnIsSignedInError(string param)
	{
		if (this.developerBuild)
		{
			this.comm.Call("console.log('### Callback: onIsSignedInError: " + param + "')");
		}
		Hashtable hashtable = new Hashtable();
		hashtable = this.comm.Read(param);
		bool flag = (string)hashtable["success"] == "false";
		if (CJServicesEvents.onIsSignedInError != null)
		{
			if (flag)
			{
				CJServicesEvents.onIsSignedInError(true);
				if (this.developerBuild)
				{
					this.comm.Call("console.log('### Event: onIsSignedInError triggered')");
				}
			}
			else
			{
				CJServicesEvents.onIsSignedInError(false);
			}
		}
	}

	// Token: 0x0600002D RID: 45 RVA: 0x00002C4C File Offset: 0x00000E4C
	public void OnLogin()
	{
		if (CJServicesEvents.onLogin != null)
		{
			CJServicesEvents.onLogin(true);
			if (this.developerBuild)
			{
				this.comm.Call("console.log('### Event: onLogin triggered')");
			}
		}
	}

	// Token: 0x0600002E RID: 46 RVA: 0x00002C8C File Offset: 0x00000E8C
	public void OnLogout()
	{
		if (CJServicesEvents.onLogout != null)
		{
			CJServicesEvents.onLogout(true);
			if (this.developerBuild)
			{
				this.comm.Call("console.log('### Event: onLogout triggered')");
			}
		}
	}

	// Token: 0x04000016 RID: 22
	private CJComm comm;

	// Token: 0x0200008F RID: 143
	// (Invoke) Token: 0x0600042C RID: 1068
	public delegate void trigger(bool result);
}
