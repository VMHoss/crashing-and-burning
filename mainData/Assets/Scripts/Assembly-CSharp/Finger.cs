using System;
using UnityEngine;

// Token: 0x02000015 RID: 21
public class Finger
{
	// Token: 0x04000151 RID: 337
	public int fingerIndex;

	// Token: 0x04000152 RID: 338
	public int touchCount;

	// Token: 0x04000153 RID: 339
	public Vector2 startPosition;

	// Token: 0x04000154 RID: 340
	public Vector2 complexStartPosition;

	// Token: 0x04000155 RID: 341
	public Vector2 position;

	// Token: 0x04000156 RID: 342
	public Vector2 deltaPosition;

	// Token: 0x04000157 RID: 343
	public Vector2 oldPosition;

	// Token: 0x04000158 RID: 344
	public int tapCount;

	// Token: 0x04000159 RID: 345
	public float deltaTime;

	// Token: 0x0400015A RID: 346
	public TouchPhase phase;

	// Token: 0x0400015B RID: 347
	public EasyTouch.GestureType gesture;

	// Token: 0x0400015C RID: 348
	public GameObject pickedObject;
}
