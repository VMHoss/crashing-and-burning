using System;
using UnityEngine;

// Token: 0x02000192 RID: 402
public class KGFString : MonoBehaviour
{
	// Token: 0x06000BCD RID: 3021 RVA: 0x000567F0 File Offset: 0x000549F0
	public string GetString()
	{
		return this.itsString;
	}

	// Token: 0x06000BCE RID: 3022 RVA: 0x000567F8 File Offset: 0x000549F8
	public override string ToString()
	{
		return this.itsString;
	}

	// Token: 0x04000BA5 RID: 2981
	public string itsString;
}
