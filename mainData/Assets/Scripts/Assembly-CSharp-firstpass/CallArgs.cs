using System;

// Token: 0x02000058 RID: 88
public class CallArgs
{
	// Token: 0x060002F0 RID: 752 RVA: 0x0000B0F4 File Offset: 0x000092F4
	public CallArgs(string functionName)
	{
		this.functionName = functionName;
		this.data = null;
	}

	// Token: 0x060002F1 RID: 753 RVA: 0x0000B10C File Offset: 0x0000930C
	public CallArgs(string functionName, string data)
	{
		this.functionName = functionName;
		this.data = data;
	}

	// Token: 0x04000142 RID: 322
	public string functionName;

	// Token: 0x04000143 RID: 323
	public string data;
}
