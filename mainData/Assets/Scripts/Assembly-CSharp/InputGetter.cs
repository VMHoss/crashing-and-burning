using System;

// Token: 0x020000CE RID: 206
public abstract class InputGetter
{
	// Token: 0x06000646 RID: 1606
	public abstract void UpdateInput();

	// Token: 0x06000647 RID: 1607
	public abstract float GetAction(string anAction);

	// Token: 0x06000648 RID: 1608
	public abstract void Reset();
}
