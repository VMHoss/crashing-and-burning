using System;
using UnityEngine;

// Token: 0x02000005 RID: 5
public class CJServices : CJDefinitions
{
	// Token: 0x0600001D RID: 29 RVA: 0x0000286C File Offset: 0x00000A6C
	private void Awake()
	{
		this.comm = (GameObject.Find(this.commObj).GetComponent("CJComm") as CJComm);
		this.developerBuild = this.comm.developerBuild;
	}

	// Token: 0x0600001E RID: 30 RVA: 0x000028A0 File Offset: 0x00000AA0
	public void isSignedIn()
	{
		this.comm.Call("CJApi.services.isSignedIn('" + this.comm.salt() + "');");
		if (this.developerBuild)
		{
			this.comm.Call("console.log('### Action: services.isSignedIn')");
		}
	}

	// Token: 0x0600001F RID: 31 RVA: 0x000028F0 File Offset: 0x00000AF0
	public void showSignInBox()
	{
		this.comm.Call("CJApi.services.showSignInBox('" + this.comm.salt() + "')");
		if (this.developerBuild)
		{
			this.comm.Call("console.log('### Action: services.showSignInBox')");
		}
	}

	// Token: 0x06000020 RID: 32 RVA: 0x00002940 File Offset: 0x00000B40
	public void showRegistrationBox()
	{
		this.comm.Call("CJApi.services.showRegistrationBox('" + this.comm.salt() + "')");
		if (this.developerBuild)
		{
			this.comm.Call("console.log('### Action: services.showRegistrationBox')");
		}
	}

	// Token: 0x04000015 RID: 21
	private CJComm comm;
}
