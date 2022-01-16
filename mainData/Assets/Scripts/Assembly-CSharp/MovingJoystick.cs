using System;
using UnityEngine;

// Token: 0x02000018 RID: 24
public class MovingJoystick
{
	// Token: 0x06000103 RID: 259 RVA: 0x00008380 File Offset: 0x00006580
	public float Axis2Angle()
	{
		return this.Axis2Angle(true);
	}

	// Token: 0x06000104 RID: 260 RVA: 0x0000838C File Offset: 0x0000658C
	public float Axis2Angle(bool inDegree)
	{
		float num = Mathf.Atan2(this.joystickAxis.x, this.joystickAxis.y);
		if (inDegree)
		{
			return num * 57.29578f;
		}
		return num;
	}

	// Token: 0x0400016E RID: 366
	public string joystickName;

	// Token: 0x0400016F RID: 367
	public Vector2 joystickAxis;

	// Token: 0x04000170 RID: 368
	public Vector2 joystickValue;

	// Token: 0x04000171 RID: 369
	public EasyJoystick joystick;
}
