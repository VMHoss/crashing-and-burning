using System;
using UnityEngine;

// Token: 0x0200001C RID: 28
public class C_EasyJoystickTemplate : MonoBehaviour
{
	// Token: 0x06000117 RID: 279 RVA: 0x00008730 File Offset: 0x00006930
	private void OnEnable()
	{
		EasyJoystick.On_JoystickTouchStart += this.On_JoystickTouchStart;
		EasyJoystick.On_JoystickMoveStart += this.On_JoystickMoveStart;
		EasyJoystick.On_JoystickMove += this.On_JoystickMove;
		EasyJoystick.On_JoystickMoveEnd += this.On_JoystickMoveEnd;
		EasyJoystick.On_JoystickTouchUp += this.On_JoystickTouchUp;
		EasyJoystick.On_JoystickTap += this.On_JoystickTap;
		EasyJoystick.On_JoystickDoubleTap += this.On_JoystickDoubleTap;
	}

	// Token: 0x06000118 RID: 280 RVA: 0x000087B4 File Offset: 0x000069B4
	private void OnDisable()
	{
		EasyJoystick.On_JoystickTouchStart -= this.On_JoystickTouchStart;
		EasyJoystick.On_JoystickMoveStart -= this.On_JoystickMoveStart;
		EasyJoystick.On_JoystickMove -= this.On_JoystickMove;
		EasyJoystick.On_JoystickMoveEnd -= this.On_JoystickMoveEnd;
		EasyJoystick.On_JoystickTouchUp -= this.On_JoystickTouchUp;
		EasyJoystick.On_JoystickTap -= this.On_JoystickTap;
		EasyJoystick.On_JoystickDoubleTap -= this.On_JoystickDoubleTap;
	}

	// Token: 0x06000119 RID: 281 RVA: 0x00008838 File Offset: 0x00006A38
	private void OnDestroy()
	{
		EasyJoystick.On_JoystickTouchStart -= this.On_JoystickTouchStart;
		EasyJoystick.On_JoystickMoveStart -= this.On_JoystickMoveStart;
		EasyJoystick.On_JoystickMove -= this.On_JoystickMove;
		EasyJoystick.On_JoystickMoveEnd -= this.On_JoystickMoveEnd;
		EasyJoystick.On_JoystickTouchUp -= this.On_JoystickTouchUp;
		EasyJoystick.On_JoystickTap -= this.On_JoystickTap;
		EasyJoystick.On_JoystickDoubleTap -= this.On_JoystickDoubleTap;
	}

	// Token: 0x0600011A RID: 282 RVA: 0x000088BC File Offset: 0x00006ABC
	private void On_JoystickDoubleTap(MovingJoystick move)
	{
	}

	// Token: 0x0600011B RID: 283 RVA: 0x000088C0 File Offset: 0x00006AC0
	private void On_JoystickTap(MovingJoystick move)
	{
	}

	// Token: 0x0600011C RID: 284 RVA: 0x000088C4 File Offset: 0x00006AC4
	private void On_JoystickTouchUp(MovingJoystick move)
	{
	}

	// Token: 0x0600011D RID: 285 RVA: 0x000088C8 File Offset: 0x00006AC8
	private void On_JoystickMoveEnd(MovingJoystick move)
	{
	}

	// Token: 0x0600011E RID: 286 RVA: 0x000088CC File Offset: 0x00006ACC
	private void On_JoystickMove(MovingJoystick move)
	{
	}

	// Token: 0x0600011F RID: 287 RVA: 0x000088D0 File Offset: 0x00006AD0
	private void On_JoystickMoveStart(MovingJoystick move)
	{
	}

	// Token: 0x06000120 RID: 288 RVA: 0x000088D4 File Offset: 0x00006AD4
	private void On_JoystickTouchStart(MovingJoystick move)
	{
	}
}
