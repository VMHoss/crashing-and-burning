using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000DB RID: 219
public class PlayerInput
{
	// Token: 0x060006AA RID: 1706 RVA: 0x0002FBB0 File Offset: 0x0002DDB0
	public PlayerInput()
	{
		this.pInputGetters = new List<InputGetter>();
	}

	// Token: 0x060006AB RID: 1707 RVA: 0x0002FBC4 File Offset: 0x0002DDC4
	public void UpdateInput()
	{
		foreach (InputGetter inputGetter in this.pInputGetters)
		{
			inputGetter.UpdateInput();
		}
	}

	// Token: 0x060006AC RID: 1708 RVA: 0x0002FC2C File Offset: 0x0002DE2C
	public void Lock()
	{
		this.easyJoystick.isActivated = false;
		this.pLocked = true;
	}

	// Token: 0x060006AD RID: 1709 RVA: 0x0002FC44 File Offset: 0x0002DE44
	public void Unlock()
	{
		this.easyJoystick.isActivated = true;
		this.pLocked = false;
	}

	// Token: 0x060006AE RID: 1710 RVA: 0x0002FC5C File Offset: 0x0002DE5C
	public bool IsLocked()
	{
		return this.pLocked;
	}

	// Token: 0x170000EC RID: 236
	public float this[string anAction]
	{
		get
		{
			foreach (InputGetter inputGetter in this.pInputGetters)
			{
				float action = inputGetter.GetAction(anAction);
				if (Mathf.Abs(action) > 0.01f)
				{
					return action;
				}
			}
			return 0f;
		}
	}

	// Token: 0x060006B0 RID: 1712 RVA: 0x0002FCF0 File Offset: 0x0002DEF0
	public void Reset()
	{
		foreach (InputGetter inputGetter in this.pInputGetters)
		{
			inputGetter.Reset();
		}
	}

	// Token: 0x060006B1 RID: 1713 RVA: 0x0002FD58 File Offset: 0x0002DF58
	public void AddInputGetter(InputGetter anInputGetter)
	{
		this.pInputGetters.Add(anInputGetter);
	}

	// Token: 0x040006D5 RID: 1749
	private List<InputGetter> pInputGetters;

	// Token: 0x040006D6 RID: 1750
	private bool pLocked;

	// Token: 0x040006D7 RID: 1751
	public EasyJoystick easyJoystick;
}
