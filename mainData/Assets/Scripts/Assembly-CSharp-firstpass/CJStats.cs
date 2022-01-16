using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000007 RID: 7
public class CJStats : CJDefinitions
{
	// Token: 0x06000030 RID: 48 RVA: 0x00002CEC File Offset: 0x00000EEC
	private void Awake()
	{
		this.comm = (GameObject.Find(this.commObj).GetComponent("CJComm") as CJComm);
		this.developerBuild = this.comm.developerBuild;
		CJServicesEvents.onIsSignedIn += this.sendRequests;
	}

	// Token: 0x06000031 RID: 49 RVA: 0x00002D3C File Offset: 0x00000F3C
	public void submit(string statName, int valor)
	{
		if (valor < 0)
		{
			if (this.developerBuild)
			{
				this.comm.Call("console.error('### ERROR: Stat value must be greater than or equal to 0')");
			}
		}
		else if (this.comm.isSignedIn)
		{
			this.comm.Call(string.Concat(new object[]
			{
				"CJApi.stats.submit('",
				statName,
				"',",
				valor,
				",'",
				this.comm.salt(),
				"');"
			}));
			if (this.developerBuild)
			{
				this.comm.Call("console.log('### Action: stats.submit')");
			}
		}
		else
		{
			base.gameObject.SendMessage("GuestHasSubmits");
			this.reqStatName.Add(statName);
			this.reqStatValor.Add(valor);
			if (this.developerBuild)
			{
				this.comm.Call("console.log('### Feedback: Stats are being kept since the user is a Guest')");
			}
		}
	}

	// Token: 0x06000032 RID: 50 RVA: 0x00002E34 File Offset: 0x00001034
	public void sendRequests(bool result)
	{
		if (this.reqStatName.Count > 0)
		{
			if (this.developerBuild)
			{
				this.comm.Call("console.log('### Feedback: Stats that were submited while in Guest mode are being resubmited')");
			}
			for (int i = this.reqStatName.Count - 1; i > -1; i--)
			{
				this.comm.Call(string.Concat(new object[]
				{
					"CJApi.stats.submit('",
					this.reqStatName[i],
					"',",
					this.reqStatValor[i],
					",'",
					this.comm.salt(),
					"');"
				}));
				this.reqStatName.RemoveAt(i);
				this.reqStatValor.RemoveAt(i);
			}
		}
	}

	// Token: 0x0400001B RID: 27
	private CJComm comm;

	// Token: 0x0400001C RID: 28
	private List<string> reqStatName = new List<string>();

	// Token: 0x0400001D RID: 29
	private List<int> reqStatValor = new List<int>();
}
