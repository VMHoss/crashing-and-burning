using System;
using UnityEngine;

// Token: 0x0200001D RID: 29
public class C_EasyTouchTemplate : MonoBehaviour
{
	// Token: 0x06000122 RID: 290 RVA: 0x000088E0 File Offset: 0x00006AE0
	private void OnEnable()
	{
		EasyTouch.On_Cancel += this.On_Cancel;
		EasyTouch.On_TouchStart += this.On_TouchStart;
		EasyTouch.On_TouchDown += this.On_TouchDown;
		EasyTouch.On_TouchUp += this.On_TouchUp;
		EasyTouch.On_SimpleTap += this.On_SimpleTap;
		EasyTouch.On_DoubleTap += this.On_DoubleTap;
		EasyTouch.On_LongTapStart += this.On_LongTapStart;
		EasyTouch.On_LongTap += this.On_LongTap;
		EasyTouch.On_LongTapEnd += this.On_LongTapEnd;
		EasyTouch.On_DragStart += this.On_DragStart;
		EasyTouch.On_Drag += this.On_Drag;
		EasyTouch.On_DragEnd += this.On_DragEnd;
		EasyTouch.On_SwipeStart += this.On_SwipeStart;
		EasyTouch.On_Swipe += this.On_Swipe;
		EasyTouch.On_SwipeEnd += this.On_SwipeEnd;
		EasyTouch.On_TouchStart2Fingers += this.On_TouchStart2Fingers;
		EasyTouch.On_TouchDown2Fingers += this.On_TouchDown2Fingers;
		EasyTouch.On_TouchUp2Fingers += this.On_TouchUp2Fingers;
		EasyTouch.On_SimpleTap2Fingers += this.On_SimpleTap2Fingers;
		EasyTouch.On_DoubleTap2Fingers += this.On_DoubleTap2Fingers;
		EasyTouch.On_LongTapStart2Fingers += this.On_LongTapStart2Fingers;
		EasyTouch.On_LongTap2Fingers += this.On_LongTap2Fingers;
		EasyTouch.On_LongTapEnd2Fingers += this.On_LongTapEnd2Fingers;
		EasyTouch.On_Twist += this.On_Twist;
		EasyTouch.On_TwistEnd += this.On_TwistEnd;
		EasyTouch.On_PinchIn += this.On_PinchIn;
		EasyTouch.On_PinchOut += this.On_PinchOut;
		EasyTouch.On_PinchEnd += this.On_PinchEnd;
		EasyTouch.On_DragStart2Fingers += this.On_DragStart2Fingers;
		EasyTouch.On_Drag2Fingers += this.On_Drag2Fingers;
		EasyTouch.On_DragEnd2Fingers += this.On_DragEnd2Fingers;
		EasyTouch.On_SwipeStart2Fingers += this.On_SwipeStart2Fingers;
		EasyTouch.On_Swipe2Fingers += this.On_Swipe2Fingers;
		EasyTouch.On_SwipeEnd2Fingers += this.On_SwipeEnd2Fingers;
	}

	// Token: 0x06000123 RID: 291 RVA: 0x00008B30 File Offset: 0x00006D30
	private void OnDisable()
	{
		EasyTouch.On_Cancel -= this.On_Cancel;
		EasyTouch.On_TouchStart -= this.On_TouchStart;
		EasyTouch.On_TouchDown -= this.On_TouchDown;
		EasyTouch.On_TouchUp -= this.On_TouchUp;
		EasyTouch.On_SimpleTap -= this.On_SimpleTap;
		EasyTouch.On_DoubleTap -= this.On_DoubleTap;
		EasyTouch.On_LongTapStart -= this.On_LongTapStart;
		EasyTouch.On_LongTap -= this.On_LongTap;
		EasyTouch.On_LongTapEnd -= this.On_LongTapEnd;
		EasyTouch.On_DragStart -= this.On_DragStart;
		EasyTouch.On_Drag -= this.On_Drag;
		EasyTouch.On_DragEnd -= this.On_DragEnd;
		EasyTouch.On_SwipeStart -= this.On_SwipeStart;
		EasyTouch.On_Swipe -= this.On_Swipe;
		EasyTouch.On_SwipeEnd -= this.On_SwipeEnd;
		EasyTouch.On_TouchStart2Fingers -= this.On_TouchStart2Fingers;
		EasyTouch.On_TouchDown2Fingers -= this.On_TouchDown2Fingers;
		EasyTouch.On_TouchUp2Fingers -= this.On_TouchUp2Fingers;
		EasyTouch.On_SimpleTap2Fingers -= this.On_SimpleTap2Fingers;
		EasyTouch.On_DoubleTap2Fingers -= this.On_DoubleTap2Fingers;
		EasyTouch.On_LongTapStart2Fingers -= this.On_LongTapStart2Fingers;
		EasyTouch.On_LongTap2Fingers -= this.On_LongTap2Fingers;
		EasyTouch.On_LongTapEnd2Fingers -= this.On_LongTapEnd2Fingers;
		EasyTouch.On_Twist -= this.On_Twist;
		EasyTouch.On_TwistEnd -= this.On_TwistEnd;
		EasyTouch.On_PinchIn -= this.On_PinchIn;
		EasyTouch.On_PinchOut -= this.On_PinchOut;
		EasyTouch.On_PinchEnd -= this.On_PinchEnd;
		EasyTouch.On_DragStart2Fingers -= this.On_DragStart2Fingers;
		EasyTouch.On_Drag2Fingers -= this.On_Drag2Fingers;
		EasyTouch.On_DragEnd2Fingers -= this.On_DragEnd2Fingers;
		EasyTouch.On_SwipeStart2Fingers -= this.On_SwipeStart2Fingers;
		EasyTouch.On_Swipe2Fingers -= this.On_Swipe2Fingers;
		EasyTouch.On_SwipeEnd2Fingers -= this.On_SwipeEnd2Fingers;
	}

	// Token: 0x06000124 RID: 292 RVA: 0x00008D80 File Offset: 0x00006F80
	private void OnDestroy()
	{
		EasyTouch.On_Cancel -= this.On_Cancel;
		EasyTouch.On_TouchStart -= this.On_TouchStart;
		EasyTouch.On_TouchDown -= this.On_TouchDown;
		EasyTouch.On_TouchUp -= this.On_TouchUp;
		EasyTouch.On_SimpleTap -= this.On_SimpleTap;
		EasyTouch.On_DoubleTap -= this.On_DoubleTap;
		EasyTouch.On_LongTapStart -= this.On_LongTapStart;
		EasyTouch.On_LongTap -= this.On_LongTap;
		EasyTouch.On_LongTapEnd -= this.On_LongTapEnd;
		EasyTouch.On_DragStart -= this.On_DragStart;
		EasyTouch.On_Drag -= this.On_Drag;
		EasyTouch.On_DragEnd -= this.On_DragEnd;
		EasyTouch.On_SwipeStart -= this.On_SwipeStart;
		EasyTouch.On_Swipe -= this.On_Swipe;
		EasyTouch.On_SwipeEnd -= this.On_SwipeEnd;
		EasyTouch.On_TouchStart2Fingers -= this.On_TouchStart2Fingers;
		EasyTouch.On_TouchDown2Fingers -= this.On_TouchDown2Fingers;
		EasyTouch.On_TouchUp2Fingers -= this.On_TouchUp2Fingers;
		EasyTouch.On_SimpleTap2Fingers -= this.On_SimpleTap2Fingers;
		EasyTouch.On_DoubleTap2Fingers -= this.On_DoubleTap2Fingers;
		EasyTouch.On_LongTapStart2Fingers -= this.On_LongTapStart2Fingers;
		EasyTouch.On_LongTap2Fingers -= this.On_LongTap2Fingers;
		EasyTouch.On_LongTapEnd2Fingers -= this.On_LongTapEnd2Fingers;
		EasyTouch.On_Twist -= this.On_Twist;
		EasyTouch.On_TwistEnd -= this.On_TwistEnd;
		EasyTouch.On_PinchIn -= this.On_PinchIn;
		EasyTouch.On_PinchOut -= this.On_PinchOut;
		EasyTouch.On_PinchEnd -= this.On_PinchEnd;
		EasyTouch.On_DragStart2Fingers -= this.On_DragStart2Fingers;
		EasyTouch.On_Drag2Fingers -= this.On_Drag2Fingers;
		EasyTouch.On_DragEnd2Fingers -= this.On_DragEnd2Fingers;
		EasyTouch.On_SwipeStart2Fingers -= this.On_SwipeStart2Fingers;
		EasyTouch.On_Swipe2Fingers -= this.On_Swipe2Fingers;
		EasyTouch.On_SwipeEnd2Fingers -= this.On_SwipeEnd2Fingers;
	}

	// Token: 0x06000125 RID: 293 RVA: 0x00008FD0 File Offset: 0x000071D0
	private void On_Cancel(Gesture gesture)
	{
	}

	// Token: 0x06000126 RID: 294 RVA: 0x00008FD4 File Offset: 0x000071D4
	private void On_TouchStart(Gesture gesture)
	{
	}

	// Token: 0x06000127 RID: 295 RVA: 0x00008FD8 File Offset: 0x000071D8
	private void On_TouchDown(Gesture gesture)
	{
	}

	// Token: 0x06000128 RID: 296 RVA: 0x00008FDC File Offset: 0x000071DC
	private void On_TouchUp(Gesture gesture)
	{
	}

	// Token: 0x06000129 RID: 297 RVA: 0x00008FE0 File Offset: 0x000071E0
	private void On_SimpleTap(Gesture gesture)
	{
	}

	// Token: 0x0600012A RID: 298 RVA: 0x00008FE4 File Offset: 0x000071E4
	private void On_DoubleTap(Gesture gesture)
	{
	}

	// Token: 0x0600012B RID: 299 RVA: 0x00008FE8 File Offset: 0x000071E8
	private void On_LongTapStart(Gesture gesture)
	{
	}

	// Token: 0x0600012C RID: 300 RVA: 0x00008FEC File Offset: 0x000071EC
	private void On_LongTap(Gesture gesture)
	{
	}

	// Token: 0x0600012D RID: 301 RVA: 0x00008FF0 File Offset: 0x000071F0
	private void On_LongTapEnd(Gesture gesture)
	{
	}

	// Token: 0x0600012E RID: 302 RVA: 0x00008FF4 File Offset: 0x000071F4
	private void On_DragStart(Gesture gesture)
	{
	}

	// Token: 0x0600012F RID: 303 RVA: 0x00008FF8 File Offset: 0x000071F8
	private void On_Drag(Gesture gesture)
	{
	}

	// Token: 0x06000130 RID: 304 RVA: 0x00008FFC File Offset: 0x000071FC
	private void On_DragEnd(Gesture gesture)
	{
	}

	// Token: 0x06000131 RID: 305 RVA: 0x00009000 File Offset: 0x00007200
	private void On_SwipeStart(Gesture gesture)
	{
	}

	// Token: 0x06000132 RID: 306 RVA: 0x00009004 File Offset: 0x00007204
	private void On_Swipe(Gesture gesture)
	{
	}

	// Token: 0x06000133 RID: 307 RVA: 0x00009008 File Offset: 0x00007208
	private void On_SwipeEnd(Gesture gesture)
	{
	}

	// Token: 0x06000134 RID: 308 RVA: 0x0000900C File Offset: 0x0000720C
	private void On_TouchStart2Fingers(Gesture gesture)
	{
	}

	// Token: 0x06000135 RID: 309 RVA: 0x00009010 File Offset: 0x00007210
	private void On_TouchDown2Fingers(Gesture gesture)
	{
	}

	// Token: 0x06000136 RID: 310 RVA: 0x00009014 File Offset: 0x00007214
	private void On_TouchUp2Fingers(Gesture gesture)
	{
	}

	// Token: 0x06000137 RID: 311 RVA: 0x00009018 File Offset: 0x00007218
	private void On_SimpleTap2Fingers(Gesture gesture)
	{
	}

	// Token: 0x06000138 RID: 312 RVA: 0x0000901C File Offset: 0x0000721C
	private void On_DoubleTap2Fingers(Gesture gesture)
	{
	}

	// Token: 0x06000139 RID: 313 RVA: 0x00009020 File Offset: 0x00007220
	private void On_LongTapStart2Fingers(Gesture gesture)
	{
	}

	// Token: 0x0600013A RID: 314 RVA: 0x00009024 File Offset: 0x00007224
	private void On_LongTap2Fingers(Gesture gesture)
	{
	}

	// Token: 0x0600013B RID: 315 RVA: 0x00009028 File Offset: 0x00007228
	private void On_LongTapEnd2Fingers(Gesture gesture)
	{
	}

	// Token: 0x0600013C RID: 316 RVA: 0x0000902C File Offset: 0x0000722C
	private void On_Twist(Gesture gesture)
	{
	}

	// Token: 0x0600013D RID: 317 RVA: 0x00009030 File Offset: 0x00007230
	private void On_TwistEnd(Gesture gesture)
	{
	}

	// Token: 0x0600013E RID: 318 RVA: 0x00009034 File Offset: 0x00007234
	private void On_PinchIn(Gesture gesture)
	{
	}

	// Token: 0x0600013F RID: 319 RVA: 0x00009038 File Offset: 0x00007238
	private void On_PinchOut(Gesture gesture)
	{
	}

	// Token: 0x06000140 RID: 320 RVA: 0x0000903C File Offset: 0x0000723C
	private void On_PinchEnd(Gesture gesture)
	{
	}

	// Token: 0x06000141 RID: 321 RVA: 0x00009040 File Offset: 0x00007240
	private void On_DragStart2Fingers(Gesture gesture)
	{
	}

	// Token: 0x06000142 RID: 322 RVA: 0x00009044 File Offset: 0x00007244
	private void On_Drag2Fingers(Gesture gesture)
	{
	}

	// Token: 0x06000143 RID: 323 RVA: 0x00009048 File Offset: 0x00007248
	private void On_DragEnd2Fingers(Gesture gesture)
	{
	}

	// Token: 0x06000144 RID: 324 RVA: 0x0000904C File Offset: 0x0000724C
	private void On_SwipeStart2Fingers(Gesture gesture)
	{
	}

	// Token: 0x06000145 RID: 325 RVA: 0x00009050 File Offset: 0x00007250
	private void On_Swipe2Fingers(Gesture gesture)
	{
	}

	// Token: 0x06000146 RID: 326 RVA: 0x00009054 File Offset: 0x00007254
	private void On_SwipeEnd2Fingers(Gesture gesture)
	{
	}
}
