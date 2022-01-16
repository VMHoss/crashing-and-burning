using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000010 RID: 16
public class EasyTouch : MonoBehaviour
{
	// Token: 0x0600006B RID: 107 RVA: 0x00004F08 File Offset: 0x00003108
	public EasyTouch()
	{
		this.enable = true;
		this.useBroadcastMessage = false;
		this.enable2FingersGesture = true;
		this.enableTwist = true;
		this.enablePinch = true;
		this.autoSelect = false;
		this.StationnaryTolerance = 25f;
		this.longTapTime = 1f;
		this.swipeTolerance = 0.85f;
		this.minPinchLength = 0f;
		this.minTwistAngle = 1f;
	}

	// Token: 0x1400000B RID: 11
	// (add) Token: 0x0600006C RID: 108 RVA: 0x0000503C File Offset: 0x0000323C
	// (remove) Token: 0x0600006D RID: 109 RVA: 0x00005054 File Offset: 0x00003254
	public static event EasyTouch.TouchCancelHandler On_Cancel;

	// Token: 0x1400000C RID: 12
	// (add) Token: 0x0600006E RID: 110 RVA: 0x0000506C File Offset: 0x0000326C
	// (remove) Token: 0x0600006F RID: 111 RVA: 0x00005084 File Offset: 0x00003284
	public static event EasyTouch.Cancel2FingersHandler On_Cancel2Fingers;

	// Token: 0x1400000D RID: 13
	// (add) Token: 0x06000070 RID: 112 RVA: 0x0000509C File Offset: 0x0000329C
	// (remove) Token: 0x06000071 RID: 113 RVA: 0x000050B4 File Offset: 0x000032B4
	public static event EasyTouch.TouchStartHandler On_TouchStart;

	// Token: 0x1400000E RID: 14
	// (add) Token: 0x06000072 RID: 114 RVA: 0x000050CC File Offset: 0x000032CC
	// (remove) Token: 0x06000073 RID: 115 RVA: 0x000050E4 File Offset: 0x000032E4
	public static event EasyTouch.TouchDownHandler On_TouchDown;

	// Token: 0x1400000F RID: 15
	// (add) Token: 0x06000074 RID: 116 RVA: 0x000050FC File Offset: 0x000032FC
	// (remove) Token: 0x06000075 RID: 117 RVA: 0x00005114 File Offset: 0x00003314
	public static event EasyTouch.TouchUpHandler On_TouchUp;

	// Token: 0x14000010 RID: 16
	// (add) Token: 0x06000076 RID: 118 RVA: 0x0000512C File Offset: 0x0000332C
	// (remove) Token: 0x06000077 RID: 119 RVA: 0x00005144 File Offset: 0x00003344
	public static event EasyTouch.SimpleTapHandler On_SimpleTap;

	// Token: 0x14000011 RID: 17
	// (add) Token: 0x06000078 RID: 120 RVA: 0x0000515C File Offset: 0x0000335C
	// (remove) Token: 0x06000079 RID: 121 RVA: 0x00005174 File Offset: 0x00003374
	public static event EasyTouch.DoubleTapHandler On_DoubleTap;

	// Token: 0x14000012 RID: 18
	// (add) Token: 0x0600007A RID: 122 RVA: 0x0000518C File Offset: 0x0000338C
	// (remove) Token: 0x0600007B RID: 123 RVA: 0x000051A4 File Offset: 0x000033A4
	public static event EasyTouch.LongTapStartHandler On_LongTapStart;

	// Token: 0x14000013 RID: 19
	// (add) Token: 0x0600007C RID: 124 RVA: 0x000051BC File Offset: 0x000033BC
	// (remove) Token: 0x0600007D RID: 125 RVA: 0x000051D4 File Offset: 0x000033D4
	public static event EasyTouch.LongTapHandler On_LongTap;

	// Token: 0x14000014 RID: 20
	// (add) Token: 0x0600007E RID: 126 RVA: 0x000051EC File Offset: 0x000033EC
	// (remove) Token: 0x0600007F RID: 127 RVA: 0x00005204 File Offset: 0x00003404
	public static event EasyTouch.LongTapEndHandler On_LongTapEnd;

	// Token: 0x14000015 RID: 21
	// (add) Token: 0x06000080 RID: 128 RVA: 0x0000521C File Offset: 0x0000341C
	// (remove) Token: 0x06000081 RID: 129 RVA: 0x00005234 File Offset: 0x00003434
	public static event EasyTouch.DragStartHandler On_DragStart;

	// Token: 0x14000016 RID: 22
	// (add) Token: 0x06000082 RID: 130 RVA: 0x0000524C File Offset: 0x0000344C
	// (remove) Token: 0x06000083 RID: 131 RVA: 0x00005264 File Offset: 0x00003464
	public static event EasyTouch.DragHandler On_Drag;

	// Token: 0x14000017 RID: 23
	// (add) Token: 0x06000084 RID: 132 RVA: 0x0000527C File Offset: 0x0000347C
	// (remove) Token: 0x06000085 RID: 133 RVA: 0x00005294 File Offset: 0x00003494
	public static event EasyTouch.DragEndHandler On_DragEnd;

	// Token: 0x14000018 RID: 24
	// (add) Token: 0x06000086 RID: 134 RVA: 0x000052AC File Offset: 0x000034AC
	// (remove) Token: 0x06000087 RID: 135 RVA: 0x000052C4 File Offset: 0x000034C4
	public static event EasyTouch.SwipeStartHandler On_SwipeStart;

	// Token: 0x14000019 RID: 25
	// (add) Token: 0x06000088 RID: 136 RVA: 0x000052DC File Offset: 0x000034DC
	// (remove) Token: 0x06000089 RID: 137 RVA: 0x000052F4 File Offset: 0x000034F4
	public static event EasyTouch.SwipeHandler On_Swipe;

	// Token: 0x1400001A RID: 26
	// (add) Token: 0x0600008A RID: 138 RVA: 0x0000530C File Offset: 0x0000350C
	// (remove) Token: 0x0600008B RID: 139 RVA: 0x00005324 File Offset: 0x00003524
	public static event EasyTouch.SwipeEndHandler On_SwipeEnd;

	// Token: 0x1400001B RID: 27
	// (add) Token: 0x0600008C RID: 140 RVA: 0x0000533C File Offset: 0x0000353C
	// (remove) Token: 0x0600008D RID: 141 RVA: 0x00005354 File Offset: 0x00003554
	public static event EasyTouch.TouchStart2FingersHandler On_TouchStart2Fingers;

	// Token: 0x1400001C RID: 28
	// (add) Token: 0x0600008E RID: 142 RVA: 0x0000536C File Offset: 0x0000356C
	// (remove) Token: 0x0600008F RID: 143 RVA: 0x00005384 File Offset: 0x00003584
	public static event EasyTouch.TouchDown2FingersHandler On_TouchDown2Fingers;

	// Token: 0x1400001D RID: 29
	// (add) Token: 0x06000090 RID: 144 RVA: 0x0000539C File Offset: 0x0000359C
	// (remove) Token: 0x06000091 RID: 145 RVA: 0x000053B4 File Offset: 0x000035B4
	public static event EasyTouch.TouchUp2FingersHandler On_TouchUp2Fingers;

	// Token: 0x1400001E RID: 30
	// (add) Token: 0x06000092 RID: 146 RVA: 0x000053CC File Offset: 0x000035CC
	// (remove) Token: 0x06000093 RID: 147 RVA: 0x000053E4 File Offset: 0x000035E4
	public static event EasyTouch.SimpleTap2FingersHandler On_SimpleTap2Fingers;

	// Token: 0x1400001F RID: 31
	// (add) Token: 0x06000094 RID: 148 RVA: 0x000053FC File Offset: 0x000035FC
	// (remove) Token: 0x06000095 RID: 149 RVA: 0x00005414 File Offset: 0x00003614
	public static event EasyTouch.DoubleTap2FingersHandler On_DoubleTap2Fingers;

	// Token: 0x14000020 RID: 32
	// (add) Token: 0x06000096 RID: 150 RVA: 0x0000542C File Offset: 0x0000362C
	// (remove) Token: 0x06000097 RID: 151 RVA: 0x00005444 File Offset: 0x00003644
	public static event EasyTouch.LongTapStart2FingersHandler On_LongTapStart2Fingers;

	// Token: 0x14000021 RID: 33
	// (add) Token: 0x06000098 RID: 152 RVA: 0x0000545C File Offset: 0x0000365C
	// (remove) Token: 0x06000099 RID: 153 RVA: 0x00005474 File Offset: 0x00003674
	public static event EasyTouch.LongTap2FingersHandler On_LongTap2Fingers;

	// Token: 0x14000022 RID: 34
	// (add) Token: 0x0600009A RID: 154 RVA: 0x0000548C File Offset: 0x0000368C
	// (remove) Token: 0x0600009B RID: 155 RVA: 0x000054A4 File Offset: 0x000036A4
	public static event EasyTouch.LongTapEnd2FingersHandler On_LongTapEnd2Fingers;

	// Token: 0x14000023 RID: 35
	// (add) Token: 0x0600009C RID: 156 RVA: 0x000054BC File Offset: 0x000036BC
	// (remove) Token: 0x0600009D RID: 157 RVA: 0x000054D4 File Offset: 0x000036D4
	public static event EasyTouch.TwistHandler On_Twist;

	// Token: 0x14000024 RID: 36
	// (add) Token: 0x0600009E RID: 158 RVA: 0x000054EC File Offset: 0x000036EC
	// (remove) Token: 0x0600009F RID: 159 RVA: 0x00005504 File Offset: 0x00003704
	public static event EasyTouch.TwistEndHandler On_TwistEnd;

	// Token: 0x14000025 RID: 37
	// (add) Token: 0x060000A0 RID: 160 RVA: 0x0000551C File Offset: 0x0000371C
	// (remove) Token: 0x060000A1 RID: 161 RVA: 0x00005534 File Offset: 0x00003734
	public static event EasyTouch.PinchInHandler On_PinchIn;

	// Token: 0x14000026 RID: 38
	// (add) Token: 0x060000A2 RID: 162 RVA: 0x0000554C File Offset: 0x0000374C
	// (remove) Token: 0x060000A3 RID: 163 RVA: 0x00005564 File Offset: 0x00003764
	public static event EasyTouch.PinchOutHandler On_PinchOut;

	// Token: 0x14000027 RID: 39
	// (add) Token: 0x060000A4 RID: 164 RVA: 0x0000557C File Offset: 0x0000377C
	// (remove) Token: 0x060000A5 RID: 165 RVA: 0x00005594 File Offset: 0x00003794
	public static event EasyTouch.PinchEndHandler On_PinchEnd;

	// Token: 0x14000028 RID: 40
	// (add) Token: 0x060000A6 RID: 166 RVA: 0x000055AC File Offset: 0x000037AC
	// (remove) Token: 0x060000A7 RID: 167 RVA: 0x000055C4 File Offset: 0x000037C4
	public static event EasyTouch.DragStart2FingersHandler On_DragStart2Fingers;

	// Token: 0x14000029 RID: 41
	// (add) Token: 0x060000A8 RID: 168 RVA: 0x000055DC File Offset: 0x000037DC
	// (remove) Token: 0x060000A9 RID: 169 RVA: 0x000055F4 File Offset: 0x000037F4
	public static event EasyTouch.Drag2FingersHandler On_Drag2Fingers;

	// Token: 0x1400002A RID: 42
	// (add) Token: 0x060000AA RID: 170 RVA: 0x0000560C File Offset: 0x0000380C
	// (remove) Token: 0x060000AB RID: 171 RVA: 0x00005624 File Offset: 0x00003824
	public static event EasyTouch.DragEnd2FingersHandler On_DragEnd2Fingers;

	// Token: 0x1400002B RID: 43
	// (add) Token: 0x060000AC RID: 172 RVA: 0x0000563C File Offset: 0x0000383C
	// (remove) Token: 0x060000AD RID: 173 RVA: 0x00005654 File Offset: 0x00003854
	public static event EasyTouch.SwipeStart2FingersHandler On_SwipeStart2Fingers;

	// Token: 0x1400002C RID: 44
	// (add) Token: 0x060000AE RID: 174 RVA: 0x0000566C File Offset: 0x0000386C
	// (remove) Token: 0x060000AF RID: 175 RVA: 0x00005684 File Offset: 0x00003884
	public static event EasyTouch.Swipe2FingersHandler On_Swipe2Fingers;

	// Token: 0x1400002D RID: 45
	// (add) Token: 0x060000B0 RID: 176 RVA: 0x0000569C File Offset: 0x0000389C
	// (remove) Token: 0x060000B1 RID: 177 RVA: 0x000056B4 File Offset: 0x000038B4
	public static event EasyTouch.SwipeEnd2FingersHandler On_SwipeEnd2Fingers;

	// Token: 0x060000B2 RID: 178 RVA: 0x000056CC File Offset: 0x000038CC
	private void OnEnable()
	{
		if (Application.isPlaying && Application.isEditor)
		{
			this.InitEasyTouch();
		}
	}

	// Token: 0x060000B3 RID: 179 RVA: 0x000056E8 File Offset: 0x000038E8
	private void Start()
	{
		this.InitEasyTouch();
	}

	// Token: 0x060000B4 RID: 180 RVA: 0x000056F0 File Offset: 0x000038F0
	private void InitEasyTouch()
	{
		this.input = new EasyTouchInput();
		if (EasyTouch.instance == null)
		{
			EasyTouch.instance = this;
		}
		if (this.easyTouchCamera == null)
		{
			this.easyTouchCamera = Camera.mainCamera;
			if (this.easyTouchCamera == null && this.autoSelect)
			{
				Debug.LogWarning("No camera with flag \"MainCam\" was found in the scene, please setup the camera");
			}
		}
	}

	// Token: 0x060000B5 RID: 181 RVA: 0x00005760 File Offset: 0x00003960
	private void OnDrawGizmos()
	{
	}

	// Token: 0x060000B6 RID: 182 RVA: 0x00005764 File Offset: 0x00003964
	private void Update()
	{
		if (this.enable && EasyTouch.instance == this)
		{
			int num = this.input.TouchCount();
			if (this.oldTouchCount == 2 && num != 2 && num > 0)
			{
				this.CreateGesture2Finger(EasyTouch.EventName.On_Cancel2Fingers, Vector2.zero, Vector2.zero, Vector2.zero, 0f, EasyTouch.SwipeType.None, 0f, Vector2.zero, 0f, 0f, 0f);
			}
			this.UpdateTouches(true, num);
			this.oldPickObject2Finger = this.pickObject2Finger;
			if (this.enable2FingersGesture)
			{
				if (num == 2)
				{
					this.TwoFinger();
				}
				else
				{
					this.complexCurrentGesture = EasyTouch.GestureType.None;
					this.pickObject2Finger = null;
					this.twoFingerSwipeStart = false;
					this.twoFingerDragStart = false;
				}
			}
			for (int i = 0; i < 10; i++)
			{
				if (this.fingers[i] != null)
				{
					this.OneFinger(i);
				}
			}
			this.oldTouchCount = num;
		}
	}

	// Token: 0x060000B7 RID: 183 RVA: 0x00005864 File Offset: 0x00003A64
	private void UpdateTouches(bool realTouch, int touchCount)
	{
		Finger[] array = new Finger[10];
		this.fingers.CopyTo(array, 0);
		if (realTouch || this.enableRemote)
		{
			this.ResetTouches();
			for (int i = 0; i < touchCount; i++)
			{
				Touch touch = Input.GetTouch(i);
				int num = 0;
				while (num < 10 && this.fingers[i] == null)
				{
					if (array[num] != null && array[num].fingerIndex == touch.fingerId)
					{
						this.fingers[i] = array[num];
					}
					num++;
				}
				if (this.fingers[i] == null)
				{
					this.fingers[i] = new Finger();
					this.fingers[i].fingerIndex = touch.fingerId;
					this.fingers[i].gesture = EasyTouch.GestureType.None;
					this.fingers[i].phase = TouchPhase.Began;
				}
				else
				{
					this.fingers[i].phase = touch.phase;
				}
				this.fingers[i].position = touch.position;
				this.fingers[i].deltaPosition = touch.deltaPosition;
				this.fingers[i].tapCount = touch.tapCount;
				this.fingers[i].deltaTime = touch.deltaTime;
				this.fingers[i].touchCount = touchCount;
			}
		}
		else
		{
			for (int j = 0; j < touchCount; j++)
			{
				this.fingers[j] = this.input.GetMouseTouch(j, this.fingers[j]);
				this.fingers[j].touchCount = touchCount;
			}
		}
	}

	// Token: 0x060000B8 RID: 184 RVA: 0x00005A08 File Offset: 0x00003C08
	private void ResetTouches()
	{
		for (int i = 0; i < 10; i++)
		{
			this.fingers[i] = null;
		}
	}

	// Token: 0x060000B9 RID: 185 RVA: 0x00005A34 File Offset: 0x00003C34
	private void OneFinger(int fingerIndex)
	{
		if (this.fingers[fingerIndex].gesture == EasyTouch.GestureType.None)
		{
			this.startTimeAction = Time.realtimeSinceStartup;
			this.fingers[fingerIndex].gesture = EasyTouch.GestureType.Acquisition;
			this.fingers[fingerIndex].startPosition = this.fingers[fingerIndex].position;
			if (this.autoSelect)
			{
				this.fingers[fingerIndex].pickedObject = this.GetPickeGameObject(this.fingers[fingerIndex].startPosition);
			}
			this.CreateGesture(fingerIndex, EasyTouch.EventName.On_TouchStart, this.fingers[fingerIndex], 0f, EasyTouch.SwipeType.None, 0f, Vector2.zero);
		}
		float num = Time.realtimeSinceStartup - this.startTimeAction;
		if (this.fingers[fingerIndex].phase == TouchPhase.Canceled)
		{
			this.fingers[fingerIndex].gesture = EasyTouch.GestureType.Cancel;
		}
		if (this.fingers[fingerIndex].phase != TouchPhase.Ended && this.fingers[fingerIndex].phase != TouchPhase.Canceled)
		{
			if (this.fingers[fingerIndex].phase == TouchPhase.Stationary && num >= this.longTapTime && this.fingers[fingerIndex].gesture == EasyTouch.GestureType.Acquisition)
			{
				this.fingers[fingerIndex].gesture = EasyTouch.GestureType.LongTap;
				this.CreateGesture(fingerIndex, EasyTouch.EventName.On_LongTapStart, this.fingers[fingerIndex], num, EasyTouch.SwipeType.None, 0f, Vector2.zero);
			}
			if ((this.fingers[fingerIndex].gesture == EasyTouch.GestureType.Acquisition || this.fingers[fingerIndex].gesture == EasyTouch.GestureType.LongTap) && !this.FingerInTolerance(this.fingers[fingerIndex]))
			{
				if (this.fingers[fingerIndex].gesture == EasyTouch.GestureType.LongTap)
				{
					this.fingers[fingerIndex].gesture = EasyTouch.GestureType.Cancel;
					this.CreateGesture(fingerIndex, EasyTouch.EventName.On_LongTapEnd, this.fingers[fingerIndex], num, EasyTouch.SwipeType.None, 0f, Vector2.zero);
					this.fingers[fingerIndex].gesture = EasyTouch.GestureType.None;
				}
				else if (this.fingers[fingerIndex].pickedObject)
				{
					this.fingers[fingerIndex].gesture = EasyTouch.GestureType.Drag;
					this.CreateGesture(fingerIndex, EasyTouch.EventName.On_DragStart, this.fingers[fingerIndex], num, EasyTouch.SwipeType.None, 0f, Vector2.zero);
				}
				else
				{
					this.fingers[fingerIndex].gesture = EasyTouch.GestureType.Swipe;
					this.CreateGesture(fingerIndex, EasyTouch.EventName.On_SwipeStart, this.fingers[fingerIndex], num, EasyTouch.SwipeType.None, 0f, Vector2.zero);
				}
			}
			EasyTouch.EventName eventName = EasyTouch.EventName.None;
			switch (this.fingers[fingerIndex].gesture)
			{
			case EasyTouch.GestureType.Drag:
				eventName = EasyTouch.EventName.On_Drag;
				break;
			case EasyTouch.GestureType.Swipe:
				eventName = EasyTouch.EventName.On_Swipe;
				break;
			case EasyTouch.GestureType.LongTap:
				eventName = EasyTouch.EventName.On_LongTap;
				break;
			}
			EasyTouch.SwipeType swipe = EasyTouch.SwipeType.None;
			if (eventName != EasyTouch.EventName.None)
			{
				swipe = this.GetSwipe(new Vector2(0f, 0f), this.fingers[fingerIndex].deltaPosition);
				this.CreateGesture(fingerIndex, eventName, this.fingers[fingerIndex], num, swipe, 0f, this.fingers[fingerIndex].deltaPosition);
			}
			this.CreateGesture(fingerIndex, EasyTouch.EventName.On_TouchDown, this.fingers[fingerIndex], num, swipe, 0f, this.fingers[fingerIndex].deltaPosition);
		}
		else
		{
			bool flag = true;
			switch (this.fingers[fingerIndex].gesture)
			{
			case EasyTouch.GestureType.Drag:
				this.CreateGesture(fingerIndex, EasyTouch.EventName.On_DragEnd, this.fingers[fingerIndex], num, this.GetSwipe(this.fingers[fingerIndex].startPosition, this.fingers[fingerIndex].position), (this.fingers[fingerIndex].startPosition - this.fingers[fingerIndex].position).magnitude, this.fingers[fingerIndex].position - this.fingers[fingerIndex].startPosition);
				break;
			case EasyTouch.GestureType.Swipe:
				this.CreateGesture(fingerIndex, EasyTouch.EventName.On_SwipeEnd, this.fingers[fingerIndex], num, this.GetSwipe(this.fingers[fingerIndex].startPosition, this.fingers[fingerIndex].position), (this.fingers[fingerIndex].position - this.fingers[fingerIndex].startPosition).magnitude, this.fingers[fingerIndex].position - this.fingers[fingerIndex].startPosition);
				break;
			case EasyTouch.GestureType.LongTap:
				this.CreateGesture(fingerIndex, EasyTouch.EventName.On_LongTapEnd, this.fingers[fingerIndex], num, EasyTouch.SwipeType.None, 0f, Vector2.zero);
				break;
			case EasyTouch.GestureType.Cancel:
				this.CreateGesture(fingerIndex, EasyTouch.EventName.On_Cancel, this.fingers[fingerIndex], 0f, EasyTouch.SwipeType.None, 0f, Vector2.zero);
				break;
			case EasyTouch.GestureType.Acquisition:
				if (this.FingerInTolerance(this.fingers[fingerIndex]))
				{
					if (this.fingers[fingerIndex].tapCount < 2)
					{
						this.CreateGesture(fingerIndex, EasyTouch.EventName.On_SimpleTap, this.fingers[fingerIndex], num, EasyTouch.SwipeType.None, 0f, Vector2.zero);
					}
					else
					{
						this.CreateGesture(fingerIndex, EasyTouch.EventName.On_DoubleTap, this.fingers[fingerIndex], num, EasyTouch.SwipeType.None, 0f, Vector2.zero);
					}
				}
				else
				{
					EasyTouch.SwipeType swipe2 = this.GetSwipe(new Vector2(0f, 0f), this.fingers[fingerIndex].deltaPosition);
					if (this.fingers[fingerIndex].pickedObject)
					{
						this.CreateGesture(fingerIndex, EasyTouch.EventName.On_DragStart, this.fingers[fingerIndex], num, EasyTouch.SwipeType.None, 0f, Vector2.zero);
						this.CreateGesture(fingerIndex, EasyTouch.EventName.On_Drag, this.fingers[fingerIndex], num, swipe2, 0f, this.fingers[fingerIndex].deltaPosition);
						this.CreateGesture(fingerIndex, EasyTouch.EventName.On_DragEnd, this.fingers[fingerIndex], num, this.GetSwipe(this.fingers[fingerIndex].startPosition, this.fingers[fingerIndex].position), (this.fingers[fingerIndex].startPosition - this.fingers[fingerIndex].position).magnitude, this.fingers[fingerIndex].position - this.fingers[fingerIndex].startPosition);
					}
					else
					{
						this.CreateGesture(fingerIndex, EasyTouch.EventName.On_SwipeStart, this.fingers[fingerIndex], num, EasyTouch.SwipeType.None, 0f, Vector2.zero);
						this.CreateGesture(fingerIndex, EasyTouch.EventName.On_Swipe, this.fingers[fingerIndex], num, swipe2, 0f, this.fingers[fingerIndex].deltaPosition);
						this.CreateGesture(fingerIndex, EasyTouch.EventName.On_SwipeEnd, this.fingers[fingerIndex], num, this.GetSwipe(this.fingers[fingerIndex].startPosition, this.fingers[fingerIndex].position), (this.fingers[fingerIndex].position - this.fingers[fingerIndex].startPosition).magnitude, this.fingers[fingerIndex].position - this.fingers[fingerIndex].startPosition);
					}
				}
				break;
			}
			if (flag)
			{
				this.CreateGesture(fingerIndex, EasyTouch.EventName.On_TouchUp, this.fingers[fingerIndex], num, EasyTouch.SwipeType.None, 0f, Vector2.zero);
				this.fingers[fingerIndex] = null;
			}
		}
	}

	// Token: 0x060000BA RID: 186 RVA: 0x00006118 File Offset: 0x00004318
	private void CreateGesture(int touchIndex, EasyTouch.EventName message, Finger finger, float actionTime, EasyTouch.SwipeType swipe, float swipeLength, Vector2 swipeVector)
	{
		if (message == EasyTouch.EventName.On_TouchStart)
		{
			this.isStartHoverNGUI = this.IsTouchHoverNGui(touchIndex);
		}
		if (message == EasyTouch.EventName.On_Cancel || message == EasyTouch.EventName.On_TouchUp)
		{
			this.isStartHoverNGUI = false;
		}
		if (!this.isStartHoverNGUI)
		{
			Gesture gesture = new Gesture();
			gesture.fingerIndex = finger.fingerIndex;
			gesture.touchCount = finger.touchCount;
			gesture.startPosition = finger.startPosition;
			gesture.position = finger.position;
			gesture.deltaPosition = finger.deltaPosition;
			gesture.actionTime = actionTime;
			gesture.deltaTime = finger.deltaTime;
			gesture.swipe = swipe;
			gesture.swipeLength = swipeLength;
			gesture.swipeVector = swipeVector;
			gesture.deltaPinch = 0f;
			gesture.twistAngle = 0f;
			gesture.pickObject = finger.pickedObject;
			gesture.otherReceiver = this.receiverObject;
			gesture.isHoverReservedArea = this.IsTouchHoverVirtualControll(touchIndex);
			if (this.useBroadcastMessage)
			{
				this.SendGesture(message, gesture);
			}
			if (!this.useBroadcastMessage || this.isExtension)
			{
				this.RaiseEvent(message, gesture);
			}
		}
	}

	// Token: 0x060000BB RID: 187 RVA: 0x00006234 File Offset: 0x00004434
	private void SendGesture(EasyTouch.EventName message, Gesture gesture)
	{
		if (this.useBroadcastMessage)
		{
			if (this.receiverObject != null && this.receiverObject != gesture.pickObject)
			{
				this.receiverObject.SendMessage(message.ToString(), gesture, SendMessageOptions.DontRequireReceiver);
			}
			if (gesture.pickObject)
			{
				gesture.pickObject.SendMessage(message.ToString(), gesture, SendMessageOptions.DontRequireReceiver);
			}
			else
			{
				base.SendMessage(message.ToString(), gesture, SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	// Token: 0x060000BC RID: 188 RVA: 0x000062CC File Offset: 0x000044CC
	private void TwoFinger()
	{
		float num = 0f;
		Vector2 zero = Vector2.zero;
		Vector2 vector = Vector2.zero;
		if (this.complexCurrentGesture == EasyTouch.GestureType.None)
		{
			this.twoFinger0 = this.GetTwoFinger(-1);
			this.twoFinger1 = this.GetTwoFinger(this.twoFinger0);
			this.startTimeAction = Time.realtimeSinceStartup;
			this.complexCurrentGesture = EasyTouch.GestureType.Tap;
			this.fingers[this.twoFinger0].complexStartPosition = this.fingers[this.twoFinger0].position;
			this.fingers[this.twoFinger1].complexStartPosition = this.fingers[this.twoFinger1].position;
			this.fingers[this.twoFinger0].oldPosition = this.fingers[this.twoFinger0].position;
			this.fingers[this.twoFinger1].oldPosition = this.fingers[this.twoFinger1].position;
			this.oldFingerDistance = Mathf.Abs(Vector2.Distance(this.fingers[this.twoFinger0].position, this.fingers[this.twoFinger1].position));
			this.startPosition2Finger = new Vector2((this.fingers[this.twoFinger0].position.x + this.fingers[this.twoFinger1].position.x) / 2f, (this.fingers[this.twoFinger0].position.y + this.fingers[this.twoFinger1].position.y) / 2f);
			vector = Vector2.zero;
			if (this.autoSelect)
			{
				this.pickObject2Finger = this.GetPickeGameObject(this.fingers[this.twoFinger0].complexStartPosition);
				if (this.pickObject2Finger != this.GetPickeGameObject(this.fingers[this.twoFinger1].complexStartPosition))
				{
					this.pickObject2Finger = null;
				}
			}
			this.CreateGesture2Finger(EasyTouch.EventName.On_TouchStart2Fingers, this.startPosition2Finger, this.startPosition2Finger, vector, num, EasyTouch.SwipeType.None, 0f, Vector2.zero, 0f, 0f, this.oldFingerDistance);
		}
		num = Time.realtimeSinceStartup - this.startTimeAction;
		zero = new Vector2((this.fingers[this.twoFinger0].position.x + this.fingers[this.twoFinger1].position.x) / 2f, (this.fingers[this.twoFinger0].position.y + this.fingers[this.twoFinger1].position.y) / 2f);
		vector = zero - this.oldStartPosition2Finger;
		float num2 = Mathf.Abs(Vector2.Distance(this.fingers[this.twoFinger0].position, this.fingers[this.twoFinger1].position));
		if (this.fingers[this.twoFinger0].phase == TouchPhase.Canceled || this.fingers[this.twoFinger1].phase == TouchPhase.Canceled)
		{
			this.complexCurrentGesture = EasyTouch.GestureType.Cancel;
		}
		if (this.fingers[this.twoFinger0].phase != TouchPhase.Ended && this.fingers[this.twoFinger1].phase != TouchPhase.Ended && this.complexCurrentGesture != EasyTouch.GestureType.Cancel)
		{
			if (this.complexCurrentGesture == EasyTouch.GestureType.Tap && num >= this.longTapTime && this.FingerInTolerance(this.fingers[this.twoFinger0]) && this.FingerInTolerance(this.fingers[this.twoFinger1]))
			{
				this.complexCurrentGesture = EasyTouch.GestureType.LongTap;
				this.CreateGesture2Finger(EasyTouch.EventName.On_LongTapStart2Fingers, this.startPosition2Finger, zero, vector, num, EasyTouch.SwipeType.None, 0f, Vector2.zero, 0f, 0f, num2);
			}
			bool flag = true;
			if (flag)
			{
				float num3 = Vector2.Dot(this.fingers[this.twoFinger0].deltaPosition.normalized, this.fingers[this.twoFinger1].deltaPosition.normalized);
				if (this.enablePinch && num2 != this.oldFingerDistance)
				{
					if (Mathf.Abs(num2 - this.oldFingerDistance) >= this.minPinchLength)
					{
						this.complexCurrentGesture = EasyTouch.GestureType.Pinch;
					}
					if (this.complexCurrentGesture == EasyTouch.GestureType.Pinch)
					{
						if (num2 < this.oldFingerDistance)
						{
							if (this.oldGesture != EasyTouch.GestureType.Pinch)
							{
								this.CreateStateEnd2Fingers(this.oldGesture, this.startPosition2Finger, zero, vector, num, false, num2);
								this.startTimeAction = Time.realtimeSinceStartup;
							}
							this.CreateGesture2Finger(EasyTouch.EventName.On_PinchIn, this.startPosition2Finger, zero, vector, num, this.GetSwipe(this.fingers[this.twoFinger0].complexStartPosition, this.fingers[this.twoFinger0].position), 0f, Vector2.zero, 0f, Mathf.Abs(num2 - this.oldFingerDistance), num2);
							this.complexCurrentGesture = EasyTouch.GestureType.Pinch;
						}
						else if (num2 > this.oldFingerDistance)
						{
							if (this.oldGesture != EasyTouch.GestureType.Pinch)
							{
								this.CreateStateEnd2Fingers(this.oldGesture, this.startPosition2Finger, zero, vector, num, false, num2);
								this.startTimeAction = Time.realtimeSinceStartup;
							}
							this.CreateGesture2Finger(EasyTouch.EventName.On_PinchOut, this.startPosition2Finger, zero, vector, num, this.GetSwipe(this.fingers[this.twoFinger0].complexStartPosition, this.fingers[this.twoFinger0].position), 0f, Vector2.zero, 0f, Mathf.Abs(num2 - this.oldFingerDistance), num2);
							this.complexCurrentGesture = EasyTouch.GestureType.Pinch;
						}
					}
				}
				if (this.enableTwist)
				{
					if (Mathf.Abs(this.TwistAngle()) > this.minTwistAngle)
					{
						if (this.complexCurrentGesture != EasyTouch.GestureType.Twist)
						{
							this.CreateStateEnd2Fingers(this.complexCurrentGesture, this.startPosition2Finger, zero, vector, num, false, num2);
							this.startTimeAction = Time.realtimeSinceStartup;
						}
						this.complexCurrentGesture = EasyTouch.GestureType.Twist;
					}
					if (this.complexCurrentGesture == EasyTouch.GestureType.Twist)
					{
						this.CreateGesture2Finger(EasyTouch.EventName.On_Twist, this.startPosition2Finger, zero, vector, num, EasyTouch.SwipeType.None, 0f, Vector2.zero, this.TwistAngle(), 0f, num2);
					}
					this.fingers[this.twoFinger0].oldPosition = this.fingers[this.twoFinger0].position;
					this.fingers[this.twoFinger1].oldPosition = this.fingers[this.twoFinger1].position;
				}
				if (num3 > 0f)
				{
					if (this.pickObject2Finger && !this.twoFingerDragStart)
					{
						if (this.complexCurrentGesture != EasyTouch.GestureType.Tap)
						{
							this.CreateStateEnd2Fingers(this.complexCurrentGesture, this.startPosition2Finger, zero, vector, num, false, num2);
							this.startTimeAction = Time.realtimeSinceStartup;
						}
						this.CreateGesture2Finger(EasyTouch.EventName.On_DragStart2Fingers, this.startPosition2Finger, zero, vector, num, EasyTouch.SwipeType.None, 0f, Vector2.zero, 0f, 0f, num2);
						this.twoFingerDragStart = true;
					}
					else if (!this.pickObject2Finger && !this.twoFingerSwipeStart)
					{
						if (this.complexCurrentGesture != EasyTouch.GestureType.Tap)
						{
							this.CreateStateEnd2Fingers(this.complexCurrentGesture, this.startPosition2Finger, zero, vector, num, false, num2);
							this.startTimeAction = Time.realtimeSinceStartup;
						}
						this.CreateGesture2Finger(EasyTouch.EventName.On_SwipeStart2Fingers, this.startPosition2Finger, zero, vector, num, EasyTouch.SwipeType.None, 0f, Vector2.zero, 0f, 0f, num2);
						this.twoFingerSwipeStart = true;
					}
				}
				else if (num3 < 0f)
				{
					this.twoFingerDragStart = false;
					this.twoFingerSwipeStart = false;
				}
				if (this.twoFingerDragStart)
				{
					this.CreateGesture2Finger(EasyTouch.EventName.On_Drag2Fingers, this.startPosition2Finger, zero, vector, num, this.GetSwipe(this.oldStartPosition2Finger, zero), 0f, vector, 0f, 0f, num2);
				}
				if (this.twoFingerSwipeStart)
				{
					this.CreateGesture2Finger(EasyTouch.EventName.On_Swipe2Fingers, this.startPosition2Finger, zero, vector, num, this.GetSwipe(this.oldStartPosition2Finger, zero), 0f, vector, 0f, 0f, num2);
				}
			}
			else if (this.complexCurrentGesture == EasyTouch.GestureType.LongTap)
			{
				this.CreateGesture2Finger(EasyTouch.EventName.On_LongTap2Fingers, this.startPosition2Finger, zero, vector, num, EasyTouch.SwipeType.None, 0f, Vector2.zero, 0f, 0f, num2);
			}
			this.CreateGesture2Finger(EasyTouch.EventName.On_TouchDown2Fingers, this.startPosition2Finger, zero, vector, num, this.GetSwipe(this.oldStartPosition2Finger, zero), 0f, vector, 0f, 0f, num2);
			this.oldFingerDistance = num2;
			this.oldStartPosition2Finger = zero;
			this.oldGesture = this.complexCurrentGesture;
		}
		else
		{
			this.CreateStateEnd2Fingers(this.complexCurrentGesture, this.startPosition2Finger, zero, vector, num, true, num2);
			this.complexCurrentGesture = EasyTouch.GestureType.None;
			this.pickObject2Finger = null;
			this.twoFingerSwipeStart = false;
			this.twoFingerDragStart = false;
		}
	}

	// Token: 0x060000BD RID: 189 RVA: 0x00006B84 File Offset: 0x00004D84
	private int GetTwoFinger(int index)
	{
		int num = index + 1;
		bool flag = false;
		while (num < 10 && !flag)
		{
			if (this.fingers[num] != null && num >= index)
			{
				flag = true;
			}
			num++;
		}
		return num - 1;
	}

	// Token: 0x060000BE RID: 190 RVA: 0x00006BCC File Offset: 0x00004DCC
	private void CreateStateEnd2Fingers(EasyTouch.GestureType gesture, Vector2 startPosition, Vector2 position, Vector2 deltaPosition, float time, bool realEnd, float fingerDistance)
	{
		switch (gesture)
		{
		case EasyTouch.GestureType.Tap:
			if (this.fingers[this.twoFinger0].tapCount < 2 && this.fingers[this.twoFinger1].tapCount < 2)
			{
				this.CreateGesture2Finger(EasyTouch.EventName.On_SimpleTap2Fingers, startPosition, position, deltaPosition, time, EasyTouch.SwipeType.None, 0f, Vector2.zero, 0f, 0f, fingerDistance);
			}
			else
			{
				this.CreateGesture2Finger(EasyTouch.EventName.On_DoubleTap2Fingers, startPosition, position, deltaPosition, time, EasyTouch.SwipeType.None, 0f, Vector2.zero, 0f, 0f, fingerDistance);
			}
			break;
		case EasyTouch.GestureType.LongTap:
			this.CreateGesture2Finger(EasyTouch.EventName.On_LongTapEnd2Fingers, startPosition, position, deltaPosition, time, EasyTouch.SwipeType.None, 0f, Vector2.zero, 0f, 0f, fingerDistance);
			break;
		case EasyTouch.GestureType.Pinch:
			this.CreateGesture2Finger(EasyTouch.EventName.On_PinchEnd, startPosition, position, deltaPosition, time, EasyTouch.SwipeType.None, 0f, Vector2.zero, 0f, 0f, fingerDistance);
			break;
		case EasyTouch.GestureType.Twist:
			this.CreateGesture2Finger(EasyTouch.EventName.On_TwistEnd, startPosition, position, deltaPosition, time, EasyTouch.SwipeType.None, 0f, Vector2.zero, 0f, 0f, fingerDistance);
			break;
		}
		if (realEnd)
		{
			if (this.twoFingerDragStart)
			{
				this.CreateGesture2Finger(EasyTouch.EventName.On_DragEnd2Fingers, startPosition, position, deltaPosition, time, this.GetSwipe(startPosition, position), (position - startPosition).magnitude, position - startPosition, 0f, 0f, fingerDistance);
			}
			if (this.twoFingerSwipeStart)
			{
				this.CreateGesture2Finger(EasyTouch.EventName.On_SwipeEnd2Fingers, startPosition, position, deltaPosition, time, this.GetSwipe(startPosition, position), (position - startPosition).magnitude, position - startPosition, 0f, 0f, fingerDistance);
			}
			this.CreateGesture2Finger(EasyTouch.EventName.On_TouchUp2Fingers, startPosition, position, deltaPosition, time, EasyTouch.SwipeType.None, 0f, Vector2.zero, 0f, 0f, fingerDistance);
		}
	}

	// Token: 0x060000BF RID: 191 RVA: 0x00006DB8 File Offset: 0x00004FB8
	private void CreateGesture2Finger(EasyTouch.EventName message, Vector2 startPosition, Vector2 position, Vector2 deltaPosition, float actionTime, EasyTouch.SwipeType swipe, float swipeLength, Vector2 swipeVector, float twist, float pinch, float twoDistance)
	{
		if (message == EasyTouch.EventName.On_TouchStart2Fingers)
		{
			this.isStartHoverNGUI = (this.IsTouchHoverNGui(this.twoFinger1) & this.IsTouchHoverNGui(this.twoFinger0));
		}
		if (!this.isStartHoverNGUI)
		{
			Gesture gesture = new Gesture();
			gesture.touchCount = 2;
			gesture.fingerIndex = -1;
			gesture.startPosition = startPosition;
			gesture.position = position;
			gesture.deltaPosition = deltaPosition;
			gesture.actionTime = actionTime;
			if (this.fingers[this.twoFinger0] != null)
			{
				gesture.deltaTime = this.fingers[this.twoFinger0].deltaTime;
			}
			else if (this.fingers[this.twoFinger1] != null)
			{
				gesture.deltaTime = this.fingers[this.twoFinger1].deltaTime;
			}
			else
			{
				gesture.deltaTime = 0f;
			}
			gesture.swipe = swipe;
			gesture.swipeLength = swipeLength;
			gesture.swipeVector = swipeVector;
			gesture.deltaPinch = pinch;
			gesture.twistAngle = twist;
			gesture.twoFingerDistance = twoDistance;
			if (message != EasyTouch.EventName.On_Cancel2Fingers)
			{
				gesture.pickObject = this.pickObject2Finger;
			}
			else
			{
				gesture.pickObject = this.oldPickObject2Finger;
			}
			gesture.otherReceiver = this.receiverObject;
			if (this.useBroadcastMessage)
			{
				this.SendGesture2Finger(message, gesture);
			}
			else
			{
				this.RaiseEvent(message, gesture);
			}
		}
	}

	// Token: 0x060000C0 RID: 192 RVA: 0x00006F14 File Offset: 0x00005114
	private void SendGesture2Finger(EasyTouch.EventName message, Gesture gesture)
	{
		if (this.receiverObject != null && this.receiverObject != gesture.pickObject)
		{
			this.receiverObject.SendMessage(message.ToString(), gesture, SendMessageOptions.DontRequireReceiver);
		}
		if (gesture.pickObject != null)
		{
			gesture.pickObject.SendMessage(message.ToString(), gesture, SendMessageOptions.DontRequireReceiver);
		}
		else
		{
			base.SendMessage(message.ToString(), gesture, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x060000C1 RID: 193 RVA: 0x00006FA4 File Offset: 0x000051A4
	private void RaiseEvent(EasyTouch.EventName evnt, Gesture gesture)
	{
		switch (evnt)
		{
		case EasyTouch.EventName.On_Cancel:
			if (EasyTouch.On_Cancel != null)
			{
				EasyTouch.On_Cancel(gesture);
			}
			break;
		case EasyTouch.EventName.On_Cancel2Fingers:
			if (EasyTouch.On_Cancel2Fingers != null)
			{
				EasyTouch.On_Cancel2Fingers(gesture);
			}
			break;
		case EasyTouch.EventName.On_TouchStart:
			if (EasyTouch.On_TouchStart != null)
			{
				EasyTouch.On_TouchStart(gesture);
			}
			break;
		case EasyTouch.EventName.On_TouchDown:
			if (EasyTouch.On_TouchDown != null)
			{
				EasyTouch.On_TouchDown(gesture);
			}
			break;
		case EasyTouch.EventName.On_TouchUp:
			if (EasyTouch.On_TouchUp != null)
			{
				EasyTouch.On_TouchUp(gesture);
			}
			break;
		case EasyTouch.EventName.On_SimpleTap:
			if (EasyTouch.On_SimpleTap != null)
			{
				EasyTouch.On_SimpleTap(gesture);
			}
			break;
		case EasyTouch.EventName.On_DoubleTap:
			if (EasyTouch.On_DoubleTap != null)
			{
				EasyTouch.On_DoubleTap(gesture);
			}
			break;
		case EasyTouch.EventName.On_LongTapStart:
			if (EasyTouch.On_LongTapStart != null)
			{
				EasyTouch.On_LongTapStart(gesture);
			}
			break;
		case EasyTouch.EventName.On_LongTap:
			if (EasyTouch.On_LongTap != null)
			{
				EasyTouch.On_LongTap(gesture);
			}
			break;
		case EasyTouch.EventName.On_LongTapEnd:
			if (EasyTouch.On_LongTapEnd != null)
			{
				EasyTouch.On_LongTapEnd(gesture);
			}
			break;
		case EasyTouch.EventName.On_DragStart:
			if (EasyTouch.On_DragStart != null)
			{
				EasyTouch.On_DragStart(gesture);
			}
			break;
		case EasyTouch.EventName.On_Drag:
			if (EasyTouch.On_Drag != null)
			{
				EasyTouch.On_Drag(gesture);
			}
			break;
		case EasyTouch.EventName.On_DragEnd:
			if (EasyTouch.On_DragEnd != null)
			{
				EasyTouch.On_DragEnd(gesture);
			}
			break;
		case EasyTouch.EventName.On_SwipeStart:
			if (EasyTouch.On_SwipeStart != null)
			{
				EasyTouch.On_SwipeStart(gesture);
			}
			break;
		case EasyTouch.EventName.On_Swipe:
			if (EasyTouch.On_Swipe != null)
			{
				EasyTouch.On_Swipe(gesture);
			}
			break;
		case EasyTouch.EventName.On_SwipeEnd:
			if (EasyTouch.On_SwipeEnd != null)
			{
				EasyTouch.On_SwipeEnd(gesture);
			}
			break;
		case EasyTouch.EventName.On_TouchStart2Fingers:
			if (EasyTouch.On_TouchStart2Fingers != null)
			{
				EasyTouch.On_TouchStart2Fingers(gesture);
			}
			break;
		case EasyTouch.EventName.On_TouchDown2Fingers:
			if (EasyTouch.On_TouchDown2Fingers != null)
			{
				EasyTouch.On_TouchDown2Fingers(gesture);
			}
			break;
		case EasyTouch.EventName.On_TouchUp2Fingers:
			if (EasyTouch.On_TouchUp2Fingers != null)
			{
				EasyTouch.On_TouchUp2Fingers(gesture);
			}
			break;
		case EasyTouch.EventName.On_SimpleTap2Fingers:
			if (EasyTouch.On_SimpleTap2Fingers != null)
			{
				EasyTouch.On_SimpleTap2Fingers(gesture);
			}
			break;
		case EasyTouch.EventName.On_DoubleTap2Fingers:
			if (EasyTouch.On_DoubleTap2Fingers != null)
			{
				EasyTouch.On_DoubleTap2Fingers(gesture);
			}
			break;
		case EasyTouch.EventName.On_LongTapStart2Fingers:
			if (EasyTouch.On_LongTapStart2Fingers != null)
			{
				EasyTouch.On_LongTapStart2Fingers(gesture);
			}
			break;
		case EasyTouch.EventName.On_LongTap2Fingers:
			if (EasyTouch.On_LongTap2Fingers != null)
			{
				EasyTouch.On_LongTap2Fingers(gesture);
			}
			break;
		case EasyTouch.EventName.On_LongTapEnd2Fingers:
			if (EasyTouch.On_LongTapEnd2Fingers != null)
			{
				EasyTouch.On_LongTapEnd2Fingers(gesture);
			}
			break;
		case EasyTouch.EventName.On_Twist:
			if (EasyTouch.On_Twist != null)
			{
				EasyTouch.On_Twist(gesture);
			}
			break;
		case EasyTouch.EventName.On_TwistEnd:
			if (EasyTouch.On_TwistEnd != null)
			{
				EasyTouch.On_TwistEnd(gesture);
			}
			break;
		case EasyTouch.EventName.On_PinchIn:
			if (EasyTouch.On_PinchIn != null)
			{
				EasyTouch.On_PinchIn(gesture);
			}
			break;
		case EasyTouch.EventName.On_PinchOut:
			if (EasyTouch.On_PinchOut != null)
			{
				EasyTouch.On_PinchOut(gesture);
			}
			break;
		case EasyTouch.EventName.On_PinchEnd:
			if (EasyTouch.On_PinchEnd != null)
			{
				EasyTouch.On_PinchEnd(gesture);
			}
			break;
		case EasyTouch.EventName.On_DragStart2Fingers:
			if (EasyTouch.On_DragStart2Fingers != null)
			{
				EasyTouch.On_DragStart2Fingers(gesture);
			}
			break;
		case EasyTouch.EventName.On_Drag2Fingers:
			if (EasyTouch.On_Drag2Fingers != null)
			{
				EasyTouch.On_Drag2Fingers(gesture);
			}
			break;
		case EasyTouch.EventName.On_DragEnd2Fingers:
			if (EasyTouch.On_DragEnd2Fingers != null)
			{
				EasyTouch.On_DragEnd2Fingers(gesture);
			}
			break;
		case EasyTouch.EventName.On_SwipeStart2Fingers:
			if (EasyTouch.On_SwipeStart2Fingers != null)
			{
				EasyTouch.On_SwipeStart2Fingers(gesture);
			}
			break;
		case EasyTouch.EventName.On_Swipe2Fingers:
			if (EasyTouch.On_Swipe2Fingers != null)
			{
				EasyTouch.On_Swipe2Fingers(gesture);
			}
			break;
		case EasyTouch.EventName.On_SwipeEnd2Fingers:
			if (EasyTouch.On_SwipeEnd2Fingers != null)
			{
				EasyTouch.On_SwipeEnd2Fingers(gesture);
			}
			break;
		}
	}

	// Token: 0x060000C2 RID: 194 RVA: 0x000073DC File Offset: 0x000055DC
	private GameObject GetPickeGameObject(Vector2 screenPos)
	{
		if (this.easyTouchCamera != null)
		{
			Ray ray = this.easyTouchCamera.ScreenPointToRay(screenPos);
			LayerMask mask = this.pickableLayers;
			RaycastHit raycastHit;
			if (Physics.Raycast(ray, out raycastHit, 3.4028235E+38f, mask))
			{
				return raycastHit.collider.gameObject;
			}
		}
		else
		{
			Debug.LogWarning("No camera is assigned to EasyTouch");
		}
		return null;
	}

	// Token: 0x060000C3 RID: 195 RVA: 0x00007448 File Offset: 0x00005648
	private EasyTouch.SwipeType GetSwipe(Vector2 start, Vector2 end)
	{
		Vector2 normalized = (end - start).normalized;
		if (Mathf.Abs(normalized.y) > Mathf.Abs(normalized.x))
		{
			if (Vector2.Dot(normalized, Vector2.up) >= this.swipeTolerance)
			{
				return EasyTouch.SwipeType.Up;
			}
			if (Vector2.Dot(normalized, -Vector2.up) >= this.swipeTolerance)
			{
				return EasyTouch.SwipeType.Down;
			}
		}
		else
		{
			if (Vector2.Dot(normalized, Vector2.right) >= this.swipeTolerance)
			{
				return EasyTouch.SwipeType.Right;
			}
			if (Vector2.Dot(normalized, -Vector2.right) >= this.swipeTolerance)
			{
				return EasyTouch.SwipeType.Left;
			}
		}
		return EasyTouch.SwipeType.Other;
	}

	// Token: 0x060000C4 RID: 196 RVA: 0x000074F4 File Offset: 0x000056F4
	private bool FingerInTolerance(Finger finger)
	{
		return (finger.position - finger.startPosition).sqrMagnitude <= this.StationnaryTolerance * this.StationnaryTolerance;
	}

	// Token: 0x060000C5 RID: 197 RVA: 0x00007530 File Offset: 0x00005730
	private float DeltaAngle(Vector2 start, Vector2 end)
	{
		float y = start.x * end.y - start.y * end.x;
		return Mathf.Atan2(y, Vector2.Dot(start, end));
	}

	// Token: 0x060000C6 RID: 198 RVA: 0x0000756C File Offset: 0x0000576C
	private float TwistAngle()
	{
		Vector2 end = this.fingers[this.twoFinger0].position - this.fingers[this.twoFinger1].position;
		Vector2 start = this.fingers[this.twoFinger0].oldPosition - this.fingers[this.twoFinger1].oldPosition;
		return 57.29578f * this.DeltaAngle(start, end);
	}

	// Token: 0x060000C7 RID: 199 RVA: 0x000075E0 File Offset: 0x000057E0
	private bool IsTouchHoverNGui(int touchIndex)
	{
		bool flag = false;
		if (this.enabledNGuiMode)
		{
			LayerMask mask = this.nGUILayers;
			int num = 0;
			while (!flag && num < this.nGUICameras.Count)
			{
				Ray ray = this.nGUICameras[num].ScreenPointToRay(this.fingers[touchIndex].position);
				RaycastHit raycastHit;
				flag = Physics.Raycast(ray, out raycastHit, float.MaxValue, mask);
				num++;
			}
		}
		return flag;
	}

	// Token: 0x060000C8 RID: 200 RVA: 0x00007660 File Offset: 0x00005860
	private bool IsTouchHoverVirtualControll(int touchIndex)
	{
		bool flag = false;
		if (this.enableReservedArea)
		{
			int num = 0;
			while (!flag && num < this.reservedAreas.Count)
			{
				Rect realRect = VirtualScreen.GetRealRect(this.reservedAreas[num]);
				realRect = new Rect(realRect.x, (float)Screen.height - realRect.y - realRect.height, realRect.width, realRect.height);
				flag = realRect.Contains(this.fingers[touchIndex].position);
				num++;
			}
		}
		return flag;
	}

	// Token: 0x060000C9 RID: 201 RVA: 0x000076F8 File Offset: 0x000058F8
	private Finger GetFinger(int finderId)
	{
		int num = 0;
		Finger finger = null;
		while (num < 10 && finger == null)
		{
			if (this.fingers[num] != null && this.fingers[num].fingerIndex == finderId)
			{
				finger = this.fingers[num];
			}
			num++;
		}
		return finger;
	}

	// Token: 0x060000CA RID: 202 RVA: 0x0000774C File Offset: 0x0000594C
	public static void SetEnabled(bool enable)
	{
		EasyTouch.instance.enable = enable;
		if (enable)
		{
			EasyTouch.instance.ResetTouches();
		}
	}

	// Token: 0x060000CB RID: 203 RVA: 0x0000776C File Offset: 0x0000596C
	public static bool GetEnabled()
	{
		return EasyTouch.instance.enable;
	}

	// Token: 0x060000CC RID: 204 RVA: 0x00007778 File Offset: 0x00005978
	public static int GetTouchCount()
	{
		return EasyTouch.instance.input.TouchCount();
	}

	// Token: 0x060000CD RID: 205 RVA: 0x0000778C File Offset: 0x0000598C
	public static void SetCamera(Camera cam)
	{
		EasyTouch.instance.easyTouchCamera = cam;
	}

	// Token: 0x060000CE RID: 206 RVA: 0x0000779C File Offset: 0x0000599C
	public static Camera GetCamera()
	{
		return EasyTouch.instance.easyTouchCamera;
	}

	// Token: 0x060000CF RID: 207 RVA: 0x000077A8 File Offset: 0x000059A8
	public static void SetEnable2FingersGesture(bool enable)
	{
		EasyTouch.instance.enable2FingersGesture = enable;
	}

	// Token: 0x060000D0 RID: 208 RVA: 0x000077B8 File Offset: 0x000059B8
	public static bool GetEnable2FingersGesture()
	{
		return EasyTouch.instance.enable2FingersGesture;
	}

	// Token: 0x060000D1 RID: 209 RVA: 0x000077C4 File Offset: 0x000059C4
	public static void SetEnableTwist(bool enable)
	{
		EasyTouch.instance.enableTwist = enable;
	}

	// Token: 0x060000D2 RID: 210 RVA: 0x000077D4 File Offset: 0x000059D4
	public static bool GetEnableTwist()
	{
		return EasyTouch.instance.enableTwist;
	}

	// Token: 0x060000D3 RID: 211 RVA: 0x000077E0 File Offset: 0x000059E0
	public static void SetEnablePinch(bool enable)
	{
		EasyTouch.instance.enablePinch = enable;
	}

	// Token: 0x060000D4 RID: 212 RVA: 0x000077F0 File Offset: 0x000059F0
	public static bool GetEnablePinch()
	{
		return EasyTouch.instance.enablePinch;
	}

	// Token: 0x060000D5 RID: 213 RVA: 0x000077FC File Offset: 0x000059FC
	public static void SetEnableAutoSelect(bool enable)
	{
		EasyTouch.instance.autoSelect = enable;
	}

	// Token: 0x060000D6 RID: 214 RVA: 0x0000780C File Offset: 0x00005A0C
	public static bool GetEnableAutoSelect()
	{
		return EasyTouch.instance.autoSelect;
	}

	// Token: 0x060000D7 RID: 215 RVA: 0x00007818 File Offset: 0x00005A18
	public static void SetOtherReceiverObject(GameObject receiver)
	{
		EasyTouch.instance.receiverObject = receiver;
	}

	// Token: 0x060000D8 RID: 216 RVA: 0x00007828 File Offset: 0x00005A28
	public static GameObject GetOtherReceiverObject()
	{
		return EasyTouch.instance.receiverObject;
	}

	// Token: 0x060000D9 RID: 217 RVA: 0x00007834 File Offset: 0x00005A34
	public static void SetStationnaryTolerance(float tolerance)
	{
		EasyTouch.instance.StationnaryTolerance = tolerance;
	}

	// Token: 0x060000DA RID: 218 RVA: 0x00007844 File Offset: 0x00005A44
	public static float GetStationnaryTolerance()
	{
		return EasyTouch.instance.StationnaryTolerance;
	}

	// Token: 0x060000DB RID: 219 RVA: 0x00007850 File Offset: 0x00005A50
	public static void SetlongTapTime(float time)
	{
		EasyTouch.instance.longTapTime = time;
	}

	// Token: 0x060000DC RID: 220 RVA: 0x00007860 File Offset: 0x00005A60
	public static float GetlongTapTime()
	{
		return EasyTouch.instance.longTapTime;
	}

	// Token: 0x060000DD RID: 221 RVA: 0x0000786C File Offset: 0x00005A6C
	public static void SetSwipeTolerance(float tolerance)
	{
		EasyTouch.instance.swipeTolerance = tolerance;
	}

	// Token: 0x060000DE RID: 222 RVA: 0x0000787C File Offset: 0x00005A7C
	public static float GetSwipeTolerance()
	{
		return EasyTouch.instance.swipeTolerance;
	}

	// Token: 0x060000DF RID: 223 RVA: 0x00007888 File Offset: 0x00005A88
	public static void SetMinPinchLength(float length)
	{
		EasyTouch.instance.minPinchLength = length;
	}

	// Token: 0x060000E0 RID: 224 RVA: 0x00007898 File Offset: 0x00005A98
	public static float GetMinPinchLength()
	{
		return EasyTouch.instance.minPinchLength;
	}

	// Token: 0x060000E1 RID: 225 RVA: 0x000078A4 File Offset: 0x00005AA4
	public static void SetMinTwistAngle(float angle)
	{
		EasyTouch.instance.minTwistAngle = angle;
	}

	// Token: 0x060000E2 RID: 226 RVA: 0x000078B4 File Offset: 0x00005AB4
	public static float GetMinTwistAngle()
	{
		return EasyTouch.instance.minTwistAngle;
	}

	// Token: 0x060000E3 RID: 227 RVA: 0x000078C0 File Offset: 0x00005AC0
	public static GameObject GetCurrentPickedObject(int fingerIndex)
	{
		return EasyTouch.instance.GetPickeGameObject(EasyTouch.instance.GetFinger(fingerIndex).position);
	}

	// Token: 0x060000E4 RID: 228 RVA: 0x000078DC File Offset: 0x00005ADC
	public static bool IsRectUnderTouch(Rect rect)
	{
		return EasyTouch.IsRectUnderTouch(rect, false);
	}

	// Token: 0x060000E5 RID: 229 RVA: 0x000078E8 File Offset: 0x00005AE8
	public static bool IsRectUnderTouch(Rect rect, bool guiRect)
	{
		bool result = false;
		for (int i = 0; i < 10; i++)
		{
			if (EasyTouch.instance.fingers[i] != null)
			{
				if (guiRect)
				{
					rect = new Rect(rect.x, (float)Screen.height - rect.y - rect.height, rect.width, rect.height);
				}
				result = rect.Contains(EasyTouch.instance.fingers[i].position);
				break;
			}
		}
		return result;
	}

	// Token: 0x060000E6 RID: 230 RVA: 0x00007974 File Offset: 0x00005B74
	public static Vector2 GetFingerPosition(int fingerIndex)
	{
		if (EasyTouch.instance.fingers[fingerIndex] != null)
		{
			return EasyTouch.instance.GetFinger(fingerIndex).position;
		}
		return Vector2.zero;
	}

	// Token: 0x060000E7 RID: 231 RVA: 0x000079A0 File Offset: 0x00005BA0
	public static bool GetIsReservedArea()
	{
		return EasyTouch.instance.enableReservedArea;
	}

	// Token: 0x060000E8 RID: 232 RVA: 0x000079AC File Offset: 0x00005BAC
	public static void SetIsReservedArea(bool enable)
	{
		EasyTouch.instance.enableReservedArea = enable;
	}

	// Token: 0x060000E9 RID: 233 RVA: 0x000079BC File Offset: 0x00005BBC
	public static void AddReservedArea(Rect rec)
	{
		EasyTouch.instance.reservedAreas.Add(rec);
	}

	// Token: 0x060000EA RID: 234 RVA: 0x000079D0 File Offset: 0x00005BD0
	public static void RemoveReservedArea(Rect rec)
	{
		EasyTouch.instance.reservedAreas.Remove(rec);
	}

	// Token: 0x060000EB RID: 235 RVA: 0x000079E4 File Offset: 0x00005BE4
	public static void ResetTouch(int fingerIndex)
	{
		EasyTouch.instance.GetFinger(fingerIndex).gesture = EasyTouch.GestureType.None;
	}

	// Token: 0x040000C1 RID: 193
	public bool enable = true;

	// Token: 0x040000C2 RID: 194
	public bool enableRemote;

	// Token: 0x040000C3 RID: 195
	public bool useBroadcastMessage = true;

	// Token: 0x040000C4 RID: 196
	public GameObject receiverObject;

	// Token: 0x040000C5 RID: 197
	public bool isExtension;

	// Token: 0x040000C6 RID: 198
	public bool enable2FingersGesture = true;

	// Token: 0x040000C7 RID: 199
	public bool enableTwist = true;

	// Token: 0x040000C8 RID: 200
	public bool enablePinch = true;

	// Token: 0x040000C9 RID: 201
	public Camera easyTouchCamera;

	// Token: 0x040000CA RID: 202
	public bool autoSelect;

	// Token: 0x040000CB RID: 203
	public LayerMask pickableLayers;

	// Token: 0x040000CC RID: 204
	public float StationnaryTolerance = 25f;

	// Token: 0x040000CD RID: 205
	public float longTapTime = 1f;

	// Token: 0x040000CE RID: 206
	public float swipeTolerance = 0.85f;

	// Token: 0x040000CF RID: 207
	public float minPinchLength;

	// Token: 0x040000D0 RID: 208
	public float minTwistAngle = 1f;

	// Token: 0x040000D1 RID: 209
	public bool enabledNGuiMode;

	// Token: 0x040000D2 RID: 210
	public LayerMask nGUILayers;

	// Token: 0x040000D3 RID: 211
	public List<Camera> nGUICameras = new List<Camera>();

	// Token: 0x040000D4 RID: 212
	private bool isStartHoverNGUI;

	// Token: 0x040000D5 RID: 213
	public List<Rect> reservedAreas = new List<Rect>();

	// Token: 0x040000D6 RID: 214
	public bool enableReservedArea = true;

	// Token: 0x040000D7 RID: 215
	public KeyCode twistKey = KeyCode.LeftAlt;

	// Token: 0x040000D8 RID: 216
	public KeyCode swipeKey = KeyCode.LeftControl;

	// Token: 0x040000D9 RID: 217
	public bool showGeneral = true;

	// Token: 0x040000DA RID: 218
	public bool showSelect = true;

	// Token: 0x040000DB RID: 219
	public bool showGesture = true;

	// Token: 0x040000DC RID: 220
	public bool showTwoFinger = true;

	// Token: 0x040000DD RID: 221
	public bool showSecondFinger = true;

	// Token: 0x040000DE RID: 222
	public static EasyTouch instance;

	// Token: 0x040000DF RID: 223
	private EasyTouchInput input;

	// Token: 0x040000E0 RID: 224
	private EasyTouch.GestureType complexCurrentGesture = EasyTouch.GestureType.None;

	// Token: 0x040000E1 RID: 225
	private EasyTouch.GestureType oldGesture = EasyTouch.GestureType.None;

	// Token: 0x040000E2 RID: 226
	private float startTimeAction;

	// Token: 0x040000E3 RID: 227
	private Finger[] fingers = new Finger[10];

	// Token: 0x040000E4 RID: 228
	private GameObject pickObject2Finger;

	// Token: 0x040000E5 RID: 229
	private GameObject oldPickObject2Finger;

	// Token: 0x040000E6 RID: 230
	public Texture secondFingerTexture;

	// Token: 0x040000E7 RID: 231
	private Vector2 startPosition2Finger;

	// Token: 0x040000E8 RID: 232
	private int twoFinger0;

	// Token: 0x040000E9 RID: 233
	private int twoFinger1;

	// Token: 0x040000EA RID: 234
	private Vector2 oldStartPosition2Finger;

	// Token: 0x040000EB RID: 235
	private float oldFingerDistance;

	// Token: 0x040000EC RID: 236
	private bool twoFingerDragStart;

	// Token: 0x040000ED RID: 237
	private bool twoFingerSwipeStart;

	// Token: 0x040000EE RID: 238
	private int oldTouchCount;

	// Token: 0x02000011 RID: 17
	public enum GestureType
	{
		// Token: 0x04000113 RID: 275
		Tap,
		// Token: 0x04000114 RID: 276
		Drag,
		// Token: 0x04000115 RID: 277
		Swipe,
		// Token: 0x04000116 RID: 278
		None,
		// Token: 0x04000117 RID: 279
		LongTap,
		// Token: 0x04000118 RID: 280
		Pinch,
		// Token: 0x04000119 RID: 281
		Twist,
		// Token: 0x0400011A RID: 282
		Cancel,
		// Token: 0x0400011B RID: 283
		Acquisition
	}

	// Token: 0x02000012 RID: 18
	public enum SwipeType
	{
		// Token: 0x0400011D RID: 285
		None,
		// Token: 0x0400011E RID: 286
		Left,
		// Token: 0x0400011F RID: 287
		Right,
		// Token: 0x04000120 RID: 288
		Up,
		// Token: 0x04000121 RID: 289
		Down,
		// Token: 0x04000122 RID: 290
		Other
	}

	// Token: 0x02000013 RID: 19
	private enum EventName
	{
		// Token: 0x04000124 RID: 292
		None,
		// Token: 0x04000125 RID: 293
		On_Cancel,
		// Token: 0x04000126 RID: 294
		On_Cancel2Fingers,
		// Token: 0x04000127 RID: 295
		On_TouchStart,
		// Token: 0x04000128 RID: 296
		On_TouchDown,
		// Token: 0x04000129 RID: 297
		On_TouchUp,
		// Token: 0x0400012A RID: 298
		On_SimpleTap,
		// Token: 0x0400012B RID: 299
		On_DoubleTap,
		// Token: 0x0400012C RID: 300
		On_LongTapStart,
		// Token: 0x0400012D RID: 301
		On_LongTap,
		// Token: 0x0400012E RID: 302
		On_LongTapEnd,
		// Token: 0x0400012F RID: 303
		On_DragStart,
		// Token: 0x04000130 RID: 304
		On_Drag,
		// Token: 0x04000131 RID: 305
		On_DragEnd,
		// Token: 0x04000132 RID: 306
		On_SwipeStart,
		// Token: 0x04000133 RID: 307
		On_Swipe,
		// Token: 0x04000134 RID: 308
		On_SwipeEnd,
		// Token: 0x04000135 RID: 309
		On_TouchStart2Fingers,
		// Token: 0x04000136 RID: 310
		On_TouchDown2Fingers,
		// Token: 0x04000137 RID: 311
		On_TouchUp2Fingers,
		// Token: 0x04000138 RID: 312
		On_SimpleTap2Fingers,
		// Token: 0x04000139 RID: 313
		On_DoubleTap2Fingers,
		// Token: 0x0400013A RID: 314
		On_LongTapStart2Fingers,
		// Token: 0x0400013B RID: 315
		On_LongTap2Fingers,
		// Token: 0x0400013C RID: 316
		On_LongTapEnd2Fingers,
		// Token: 0x0400013D RID: 317
		On_Twist,
		// Token: 0x0400013E RID: 318
		On_TwistEnd,
		// Token: 0x0400013F RID: 319
		On_PinchIn,
		// Token: 0x04000140 RID: 320
		On_PinchOut,
		// Token: 0x04000141 RID: 321
		On_PinchEnd,
		// Token: 0x04000142 RID: 322
		On_DragStart2Fingers,
		// Token: 0x04000143 RID: 323
		On_Drag2Fingers,
		// Token: 0x04000144 RID: 324
		On_DragEnd2Fingers,
		// Token: 0x04000145 RID: 325
		On_SwipeStart2Fingers,
		// Token: 0x04000146 RID: 326
		On_Swipe2Fingers,
		// Token: 0x04000147 RID: 327
		On_SwipeEnd2Fingers
	}

	// Token: 0x020001BB RID: 443
	// (Invoke) Token: 0x06000D09 RID: 3337
	public delegate void TouchCancelHandler(Gesture gesture);

	// Token: 0x020001BC RID: 444
	// (Invoke) Token: 0x06000D0D RID: 3341
	public delegate void Cancel2FingersHandler(Gesture gesture);

	// Token: 0x020001BD RID: 445
	// (Invoke) Token: 0x06000D11 RID: 3345
	public delegate void TouchStartHandler(Gesture gesture);

	// Token: 0x020001BE RID: 446
	// (Invoke) Token: 0x06000D15 RID: 3349
	public delegate void TouchDownHandler(Gesture gesture);

	// Token: 0x020001BF RID: 447
	// (Invoke) Token: 0x06000D19 RID: 3353
	public delegate void TouchUpHandler(Gesture gesture);

	// Token: 0x020001C0 RID: 448
	// (Invoke) Token: 0x06000D1D RID: 3357
	public delegate void SimpleTapHandler(Gesture gesture);

	// Token: 0x020001C1 RID: 449
	// (Invoke) Token: 0x06000D21 RID: 3361
	public delegate void DoubleTapHandler(Gesture gesture);

	// Token: 0x020001C2 RID: 450
	// (Invoke) Token: 0x06000D25 RID: 3365
	public delegate void LongTapStartHandler(Gesture gesture);

	// Token: 0x020001C3 RID: 451
	// (Invoke) Token: 0x06000D29 RID: 3369
	public delegate void LongTapHandler(Gesture gesture);

	// Token: 0x020001C4 RID: 452
	// (Invoke) Token: 0x06000D2D RID: 3373
	public delegate void LongTapEndHandler(Gesture gesture);

	// Token: 0x020001C5 RID: 453
	// (Invoke) Token: 0x06000D31 RID: 3377
	public delegate void DragStartHandler(Gesture gesture);

	// Token: 0x020001C6 RID: 454
	// (Invoke) Token: 0x06000D35 RID: 3381
	public delegate void DragHandler(Gesture gesture);

	// Token: 0x020001C7 RID: 455
	// (Invoke) Token: 0x06000D39 RID: 3385
	public delegate void DragEndHandler(Gesture gesture);

	// Token: 0x020001C8 RID: 456
	// (Invoke) Token: 0x06000D3D RID: 3389
	public delegate void SwipeStartHandler(Gesture gesture);

	// Token: 0x020001C9 RID: 457
	// (Invoke) Token: 0x06000D41 RID: 3393
	public delegate void SwipeHandler(Gesture gesture);

	// Token: 0x020001CA RID: 458
	// (Invoke) Token: 0x06000D45 RID: 3397
	public delegate void SwipeEndHandler(Gesture gesture);

	// Token: 0x020001CB RID: 459
	// (Invoke) Token: 0x06000D49 RID: 3401
	public delegate void TouchStart2FingersHandler(Gesture gesture);

	// Token: 0x020001CC RID: 460
	// (Invoke) Token: 0x06000D4D RID: 3405
	public delegate void TouchDown2FingersHandler(Gesture gesture);

	// Token: 0x020001CD RID: 461
	// (Invoke) Token: 0x06000D51 RID: 3409
	public delegate void TouchUp2FingersHandler(Gesture gesture);

	// Token: 0x020001CE RID: 462
	// (Invoke) Token: 0x06000D55 RID: 3413
	public delegate void SimpleTap2FingersHandler(Gesture gesture);

	// Token: 0x020001CF RID: 463
	// (Invoke) Token: 0x06000D59 RID: 3417
	public delegate void DoubleTap2FingersHandler(Gesture gesture);

	// Token: 0x020001D0 RID: 464
	// (Invoke) Token: 0x06000D5D RID: 3421
	public delegate void LongTapStart2FingersHandler(Gesture gesture);

	// Token: 0x020001D1 RID: 465
	// (Invoke) Token: 0x06000D61 RID: 3425
	public delegate void LongTap2FingersHandler(Gesture gesture);

	// Token: 0x020001D2 RID: 466
	// (Invoke) Token: 0x06000D65 RID: 3429
	public delegate void LongTapEnd2FingersHandler(Gesture gesture);

	// Token: 0x020001D3 RID: 467
	// (Invoke) Token: 0x06000D69 RID: 3433
	public delegate void TwistHandler(Gesture gesture);

	// Token: 0x020001D4 RID: 468
	// (Invoke) Token: 0x06000D6D RID: 3437
	public delegate void TwistEndHandler(Gesture gesture);

	// Token: 0x020001D5 RID: 469
	// (Invoke) Token: 0x06000D71 RID: 3441
	public delegate void PinchInHandler(Gesture gesture);

	// Token: 0x020001D6 RID: 470
	// (Invoke) Token: 0x06000D75 RID: 3445
	public delegate void PinchOutHandler(Gesture gesture);

	// Token: 0x020001D7 RID: 471
	// (Invoke) Token: 0x06000D79 RID: 3449
	public delegate void PinchEndHandler(Gesture gesture);

	// Token: 0x020001D8 RID: 472
	// (Invoke) Token: 0x06000D7D RID: 3453
	public delegate void DragStart2FingersHandler(Gesture gesture);

	// Token: 0x020001D9 RID: 473
	// (Invoke) Token: 0x06000D81 RID: 3457
	public delegate void Drag2FingersHandler(Gesture gesture);

	// Token: 0x020001DA RID: 474
	// (Invoke) Token: 0x06000D85 RID: 3461
	public delegate void DragEnd2FingersHandler(Gesture gesture);

	// Token: 0x020001DB RID: 475
	// (Invoke) Token: 0x06000D89 RID: 3465
	public delegate void SwipeStart2FingersHandler(Gesture gesture);

	// Token: 0x020001DC RID: 476
	// (Invoke) Token: 0x06000D8D RID: 3469
	public delegate void Swipe2FingersHandler(Gesture gesture);

	// Token: 0x020001DD RID: 477
	// (Invoke) Token: 0x06000D91 RID: 3473
	public delegate void SwipeEnd2FingersHandler(Gesture gesture);
}
