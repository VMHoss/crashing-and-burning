using System;
using UnityEngine;

// Token: 0x02000016 RID: 22
public class Gesture
{
	// Token: 0x060000F6 RID: 246 RVA: 0x00008114 File Offset: 0x00006314
	public Vector3 GetTouchToWordlPoint(float z)
	{
		return this.GetTouchToWordlPoint(z, false);
	}

	// Token: 0x060000F7 RID: 247 RVA: 0x00008120 File Offset: 0x00006320
	public Vector3 GetTouchToWordlPoint(float z, bool worldZ)
	{
		if (!worldZ)
		{
			return EasyTouch.GetCamera().ScreenToWorldPoint(new Vector3(this.position.x, this.position.y, z));
		}
		return EasyTouch.GetCamera().ScreenToWorldPoint(new Vector3(this.position.x, this.position.y, z - EasyTouch.GetCamera().transform.position.z));
	}

	// Token: 0x060000F8 RID: 248 RVA: 0x00008198 File Offset: 0x00006398
	public float GetSwipeOrDragAngle()
	{
		return Mathf.Atan2(this.swipeVector.normalized.y, this.swipeVector.normalized.x) * 57.29578f;
	}

	// Token: 0x060000F9 RID: 249 RVA: 0x000081D8 File Offset: 0x000063D8
	public bool IsInRect(Rect rect)
	{
		return this.IsInRect(rect, false);
	}

	// Token: 0x060000FA RID: 250 RVA: 0x000081E4 File Offset: 0x000063E4
	public bool IsInRect(Rect rect, bool guiRect)
	{
		if (guiRect)
		{
			rect = new Rect(rect.x, (float)Screen.height - rect.y - rect.height, rect.width, rect.height);
		}
		return rect.Contains(this.position);
	}

	// Token: 0x060000FB RID: 251 RVA: 0x00008238 File Offset: 0x00006438
	public Vector2 NormalizedPosition()
	{
		return new Vector2(100f / (float)Screen.width * this.position.x / 100f, 100f / (float)Screen.height * this.position.y / 100f);
	}

	// Token: 0x0400015D RID: 349
	public int fingerIndex;

	// Token: 0x0400015E RID: 350
	public int touchCount;

	// Token: 0x0400015F RID: 351
	public Vector2 startPosition;

	// Token: 0x04000160 RID: 352
	public Vector2 position;

	// Token: 0x04000161 RID: 353
	public Vector2 deltaPosition;

	// Token: 0x04000162 RID: 354
	public float actionTime;

	// Token: 0x04000163 RID: 355
	public float deltaTime;

	// Token: 0x04000164 RID: 356
	public EasyTouch.SwipeType swipe;

	// Token: 0x04000165 RID: 357
	public float swipeLength;

	// Token: 0x04000166 RID: 358
	public Vector2 swipeVector;

	// Token: 0x04000167 RID: 359
	public float deltaPinch;

	// Token: 0x04000168 RID: 360
	public float twistAngle;

	// Token: 0x04000169 RID: 361
	public float twoFingerDistance;

	// Token: 0x0400016A RID: 362
	public GameObject pickObject;

	// Token: 0x0400016B RID: 363
	public GameObject otherReceiver;

	// Token: 0x0400016C RID: 364
	public bool isHoverReservedArea;
}
