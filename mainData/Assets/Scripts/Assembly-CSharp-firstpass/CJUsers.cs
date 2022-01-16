using System;
using UnityEngine;

// Token: 0x02000009 RID: 9
public class CJUsers : CJDefinitions
{
	// Token: 0x0600003F RID: 63 RVA: 0x00003188 File Offset: 0x00001388
	private void Awake()
	{
		this.comm = (GameObject.Find(this.commObj).GetComponent("CJComm") as CJComm);
		this.developerBuild = this.comm.developerBuild;
	}

	// Token: 0x06000040 RID: 64 RVA: 0x000031BC File Offset: 0x000013BC
	public void getUsername()
	{
		this.comm.Call("CJApi.users.getUsername('" + this.comm.salt() + "')");
		if (this.developerBuild)
		{
			this.comm.Call("console.log('### Action: users.getUsername')");
		}
	}

	// Token: 0x06000041 RID: 65 RVA: 0x0000320C File Offset: 0x0000140C
	public void setUsername(string param)
	{
		this.comm.username = param;
	}

	// Token: 0x04000022 RID: 34
	private CJComm comm;
}
