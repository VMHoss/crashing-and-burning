using System;

// Token: 0x0200015B RID: 347
[Serializable]
public abstract class KGFEventBase : KGFObject, KGFIValidator
{
	// Token: 0x06000A04 RID: 2564
	public abstract void Trigger();

	// Token: 0x06000A05 RID: 2565 RVA: 0x0004D130 File Offset: 0x0004B330
	public virtual KGFMessageList Validate()
	{
		return new KGFMessageList();
	}
}
