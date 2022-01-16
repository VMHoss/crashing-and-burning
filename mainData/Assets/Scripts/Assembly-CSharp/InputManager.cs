using System;
using UnityEngine;

// Token: 0x020000CF RID: 207
public class InputManager
{
	// Token: 0x0600064A RID: 1610 RVA: 0x0002D47C File Offset: 0x0002B67C
	public void UpdateInput()
	{
		foreach (PlayerBaseData playerBaseData in Data.playerBaseList)
		{
			if (!playerBaseData.input.IsLocked())
			{
				playerBaseData.input.UpdateInput();
			}
		}
		if (Data.pausingAllowed)
		{
			bool flag = Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape);
			if (flag)
			{
				if (Data.pause)
				{
					Scripts.trackScript.UnPauseGame();
				}
				else
				{
					Scripts.trackScript.PauseGame();
				}
			}
		}
	}

	// Token: 0x0600064B RID: 1611 RVA: 0x0002D540 File Offset: 0x0002B740
	public void On_DoubleTap2Fingers(Gesture gesture)
	{
		this.AttemptPause();
	}

	// Token: 0x0600064C RID: 1612 RVA: 0x0002D548 File Offset: 0x0002B748
	public void LockAllInput()
	{
		foreach (PlayerBaseData playerBaseData in Data.playerBaseList)
		{
			playerBaseData.input.Lock();
		}
	}

	// Token: 0x0600064D RID: 1613 RVA: 0x0002D5B4 File Offset: 0x0002B7B4
	public void UnlockAllInput()
	{
		foreach (PlayerBaseData playerBaseData in Data.playerBaseList)
		{
			playerBaseData.input.Unlock();
		}
	}

	// Token: 0x0600064E RID: 1614 RVA: 0x0002D620 File Offset: 0x0002B820
	public void ResetInput()
	{
		foreach (PlayerBaseData playerBaseData in Data.playerBaseList)
		{
			playerBaseData.input.Reset();
		}
	}

	// Token: 0x0600064F RID: 1615 RVA: 0x0002D68C File Offset: 0x0002B88C
	private void AttemptPause()
	{
		if (Data.pausingAllowed)
		{
			if (Data.pause)
			{
				Scripts.trackScript.UnPauseGame();
			}
			else
			{
				Scripts.trackScript.PauseGame();
			}
		}
	}
}
