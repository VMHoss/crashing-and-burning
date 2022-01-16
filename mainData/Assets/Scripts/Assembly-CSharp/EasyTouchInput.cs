using System;
using UnityEngine;

// Token: 0x02000014 RID: 20
public class EasyTouchInput
{
	// Token: 0x060000ED RID: 237 RVA: 0x00007A48 File Offset: 0x00005C48
	public int TouchCount()
	{
		return this.getTouchCount(true);
	}

	// Token: 0x060000EE RID: 238 RVA: 0x00007A54 File Offset: 0x00005C54
	private int getTouchCount(bool realTouch)
	{
		int result = 0;
		if (realTouch || EasyTouch.instance.enableRemote)
		{
			result = Input.touchCount;
		}
		else if (Input.GetMouseButton(0) || Input.GetMouseButtonUp(0))
		{
			result = 1;
			if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(EasyTouch.instance.twistKey) || Input.GetKey(KeyCode.LeftControl) || Input.GetKey(EasyTouch.instance.swipeKey))
			{
				result = 2;
			}
			if (Input.GetKeyUp(KeyCode.LeftAlt) || Input.GetKeyUp(EasyTouch.instance.twistKey) || Input.GetKeyUp(KeyCode.LeftControl) || Input.GetKeyUp(EasyTouch.instance.swipeKey))
			{
				result = 2;
			}
		}
		return result;
	}

	// Token: 0x060000EF RID: 239 RVA: 0x00007B2C File Offset: 0x00005D2C
	public Finger GetMouseTouch(int fingerIndex, Finger myFinger)
	{
		Finger finger;
		if (myFinger != null)
		{
			finger = myFinger;
		}
		else
		{
			finger = new Finger();
			finger.gesture = EasyTouch.GestureType.None;
		}
		if (fingerIndex == 1 && (Input.GetKeyUp(KeyCode.LeftAlt) || Input.GetKeyUp(EasyTouch.instance.twistKey) || Input.GetKeyUp(KeyCode.LeftControl) || Input.GetKeyUp(EasyTouch.instance.swipeKey)))
		{
			finger.fingerIndex = fingerIndex;
			finger.position = this.oldFinger2Position;
			finger.deltaPosition = finger.position - this.oldFinger2Position;
			finger.tapCount = this.tapCount[fingerIndex];
			finger.deltaTime = Time.realtimeSinceStartup - this.deltaTime[fingerIndex];
			finger.phase = TouchPhase.Ended;
			return finger;
		}
		if (Input.GetMouseButton(0))
		{
			finger.fingerIndex = fingerIndex;
			finger.position = this.GetPointerPosition(fingerIndex);
			if ((double)(Time.realtimeSinceStartup - this.tapeTime[fingerIndex]) > 0.5)
			{
				this.tapCount[fingerIndex] = 0;
			}
			if (Input.GetMouseButtonDown(0) || (fingerIndex == 1 && (Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetKeyDown(EasyTouch.instance.twistKey) || Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(EasyTouch.instance.swipeKey))))
			{
				finger.position = this.GetPointerPosition(fingerIndex);
				finger.deltaPosition = Vector2.zero;
				this.tapCount[fingerIndex] = this.tapCount[fingerIndex] + 1;
				finger.tapCount = this.tapCount[fingerIndex];
				this.startActionTime[fingerIndex] = Time.realtimeSinceStartup;
				this.deltaTime[fingerIndex] = this.startActionTime[fingerIndex];
				finger.deltaTime = 0f;
				finger.phase = TouchPhase.Began;
				if (fingerIndex == 1)
				{
					this.oldFinger2Position = finger.position;
				}
				else
				{
					this.oldMousePosition[fingerIndex] = finger.position;
				}
				if (this.tapCount[fingerIndex] == 1)
				{
					this.tapeTime[fingerIndex] = Time.realtimeSinceStartup;
				}
				return finger;
			}
			finger.deltaPosition = finger.position - this.oldMousePosition[fingerIndex];
			finger.tapCount = this.tapCount[fingerIndex];
			finger.deltaTime = Time.realtimeSinceStartup - this.deltaTime[fingerIndex];
			if (finger.deltaPosition.sqrMagnitude < 1f)
			{
				finger.phase = TouchPhase.Stationary;
			}
			else
			{
				finger.phase = TouchPhase.Moved;
			}
			this.oldMousePosition[fingerIndex] = finger.position;
			this.deltaTime[fingerIndex] = Time.realtimeSinceStartup;
			return finger;
		}
		else
		{
			if (Input.GetMouseButtonUp(0))
			{
				finger.fingerIndex = fingerIndex;
				finger.position = this.GetPointerPosition(fingerIndex);
				finger.deltaPosition = finger.position - this.oldMousePosition[fingerIndex];
				finger.tapCount = this.tapCount[fingerIndex];
				finger.deltaTime = Time.realtimeSinceStartup - this.deltaTime[fingerIndex];
				finger.phase = TouchPhase.Ended;
				this.oldMousePosition[fingerIndex] = finger.position;
				return finger;
			}
			return null;
		}
	}

	// Token: 0x060000F0 RID: 240 RVA: 0x00007E5C File Offset: 0x0000605C
	public Vector2 GetSecondFingerPosition()
	{
		Vector2 result = new Vector2(-1f, -1f);
		if ((Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(EasyTouch.instance.twistKey)) && (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(EasyTouch.instance.swipeKey)))
		{
			if (!this.bComplex)
			{
				this.bComplex = true;
				this.deltaFingerPosition = Input.mousePosition - this.oldFinger2Position;
			}
			result = this.GetComplex2finger();
			return result;
		}
		if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(EasyTouch.instance.twistKey))
		{
			result = this.GetPinchTwist2Finger();
			this.bComplex = false;
			return result;
		}
		if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(EasyTouch.instance.swipeKey))
		{
			result = this.GetComplex2finger();
			this.bComplex = false;
			return result;
		}
		return result;
	}

	// Token: 0x060000F1 RID: 241 RVA: 0x00007F60 File Offset: 0x00006160
	private Vector2 GetPointerPosition(int index)
	{
		if (index == 0)
		{
			return Input.mousePosition;
		}
		return this.GetSecondFingerPosition();
	}

	// Token: 0x060000F2 RID: 242 RVA: 0x00007F88 File Offset: 0x00006188
	private Vector2 GetPinchTwist2Finger()
	{
		Vector2 result;
		if (this.complexCenter == Vector2.zero)
		{
			result.x = (float)Screen.width / 2f - (Input.mousePosition.x - (float)Screen.width / 2f);
			result.y = (float)Screen.height / 2f - (Input.mousePosition.y - (float)Screen.height / 2f);
		}
		else
		{
			result.x = this.complexCenter.x - (Input.mousePosition.x - this.complexCenter.x);
			result.y = this.complexCenter.y - (Input.mousePosition.y - this.complexCenter.y);
		}
		this.oldFinger2Position = result;
		return result;
	}

	// Token: 0x060000F3 RID: 243 RVA: 0x0000806C File Offset: 0x0000626C
	private Vector2 GetComplex2finger()
	{
		Vector2 result;
		result.x = Input.mousePosition.x - this.deltaFingerPosition.x;
		result.y = Input.mousePosition.y - this.deltaFingerPosition.y;
		this.complexCenter = new Vector2((Input.mousePosition.x + result.x) / 2f, (Input.mousePosition.y + result.y) / 2f);
		this.oldFinger2Position = result;
		return result;
	}

	// Token: 0x04000148 RID: 328
	private Vector2[] oldMousePosition = new Vector2[2];

	// Token: 0x04000149 RID: 329
	private int[] tapCount = new int[2];

	// Token: 0x0400014A RID: 330
	private float[] startActionTime = new float[2];

	// Token: 0x0400014B RID: 331
	private float[] deltaTime = new float[2];

	// Token: 0x0400014C RID: 332
	private float[] tapeTime = new float[2];

	// Token: 0x0400014D RID: 333
	private bool bComplex;

	// Token: 0x0400014E RID: 334
	private Vector2 deltaFingerPosition;

	// Token: 0x0400014F RID: 335
	private Vector2 oldFinger2Position;

	// Token: 0x04000150 RID: 336
	private Vector2 complexCenter;
}
