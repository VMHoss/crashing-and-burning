using System;
using UnityEngine;

// Token: 0x02000008 RID: 8
[ExecuteInEditMode]
public class EasyJoystick : MonoBehaviour
{
	// Token: 0x14000004 RID: 4
	// (add) Token: 0x0600001F RID: 31 RVA: 0x00002D1C File Offset: 0x00000F1C
	// (remove) Token: 0x06000020 RID: 32 RVA: 0x00002D34 File Offset: 0x00000F34
	public static event EasyJoystick.JoystickMoveStartHandler On_JoystickMoveStart;

	// Token: 0x14000005 RID: 5
	// (add) Token: 0x06000021 RID: 33 RVA: 0x00002D4C File Offset: 0x00000F4C
	// (remove) Token: 0x06000022 RID: 34 RVA: 0x00002D64 File Offset: 0x00000F64
	public static event EasyJoystick.JoystickMoveHandler On_JoystickMove;

	// Token: 0x14000006 RID: 6
	// (add) Token: 0x06000023 RID: 35 RVA: 0x00002D7C File Offset: 0x00000F7C
	// (remove) Token: 0x06000024 RID: 36 RVA: 0x00002D94 File Offset: 0x00000F94
	public static event EasyJoystick.JoystickMoveEndHandler On_JoystickMoveEnd;

	// Token: 0x14000007 RID: 7
	// (add) Token: 0x06000025 RID: 37 RVA: 0x00002DAC File Offset: 0x00000FAC
	// (remove) Token: 0x06000026 RID: 38 RVA: 0x00002DC4 File Offset: 0x00000FC4
	public static event EasyJoystick.JoystickTouchStartHandler On_JoystickTouchStart;

	// Token: 0x14000008 RID: 8
	// (add) Token: 0x06000027 RID: 39 RVA: 0x00002DDC File Offset: 0x00000FDC
	// (remove) Token: 0x06000028 RID: 40 RVA: 0x00002DF4 File Offset: 0x00000FF4
	public static event EasyJoystick.JoystickTapHandler On_JoystickTap;

	// Token: 0x14000009 RID: 9
	// (add) Token: 0x06000029 RID: 41 RVA: 0x00002E0C File Offset: 0x0000100C
	// (remove) Token: 0x0600002A RID: 42 RVA: 0x00002E24 File Offset: 0x00001024
	public static event EasyJoystick.JoystickDoubleTapHandler On_JoystickDoubleTap;

	// Token: 0x1400000A RID: 10
	// (add) Token: 0x0600002B RID: 43 RVA: 0x00002E3C File Offset: 0x0000103C
	// (remove) Token: 0x0600002C RID: 44 RVA: 0x00002E54 File Offset: 0x00001054
	public static event EasyJoystick.JoystickTouchUpHandler On_JoystickTouchUp;

	// Token: 0x17000006 RID: 6
	// (get) Token: 0x0600002D RID: 45 RVA: 0x00002E6C File Offset: 0x0000106C
	public Vector2 JoystickAxis
	{
		get
		{
			return this.joystickAxis;
		}
	}

	// Token: 0x17000007 RID: 7
	// (get) Token: 0x0600002E RID: 46 RVA: 0x00002E74 File Offset: 0x00001074
	// (set) Token: 0x0600002F RID: 47 RVA: 0x00002EA0 File Offset: 0x000010A0
	public Vector2 JoystickTouch
	{
		get
		{
			return new Vector2(this.joystickTouch.x / this.zoneRadius, this.joystickTouch.y / this.zoneRadius);
		}
		set
		{
			float x = Mathf.Clamp(value.x, -1f, 1f) * this.zoneRadius;
			float y = Mathf.Clamp(value.y, -1f, 1f) * this.zoneRadius;
			this.joystickTouch = new Vector2(x, y);
		}
	}

	// Token: 0x17000008 RID: 8
	// (get) Token: 0x06000030 RID: 48 RVA: 0x00002EF8 File Offset: 0x000010F8
	public Vector2 JoystickValue
	{
		get
		{
			return this.joystickValue;
		}
	}

	// Token: 0x17000009 RID: 9
	// (get) Token: 0x06000031 RID: 49 RVA: 0x00002F00 File Offset: 0x00001100
	// (set) Token: 0x06000032 RID: 50 RVA: 0x00002F08 File Offset: 0x00001108
	public bool DynamicJoystick
	{
		get
		{
			return this.dynamicJoystick;
		}
		set
		{
			if (!Application.isPlaying)
			{
				this.joystickIndex = -1;
				this.dynamicJoystick = value;
				if (this.dynamicJoystick)
				{
					this.virtualJoystick = false;
				}
				else
				{
					this.virtualJoystick = true;
					this.joystickCenter = this.joystickPositionOffset;
				}
			}
		}
	}

	// Token: 0x1700000A RID: 10
	// (get) Token: 0x06000033 RID: 51 RVA: 0x00002F58 File Offset: 0x00001158
	// (set) Token: 0x06000034 RID: 52 RVA: 0x00002F60 File Offset: 0x00001160
	public EasyJoystick.JoystickAnchor JoyAnchor
	{
		get
		{
			return this.joyAnchor;
		}
		set
		{
			this.joyAnchor = value;
			this.ComputeJoystickAnchor(this.joyAnchor);
		}
	}

	// Token: 0x1700000B RID: 11
	// (get) Token: 0x06000035 RID: 53 RVA: 0x00002F78 File Offset: 0x00001178
	// (set) Token: 0x06000036 RID: 54 RVA: 0x00002F80 File Offset: 0x00001180
	public Vector2 JoystickPositionOffset
	{
		get
		{
			return this.joystickPositionOffset;
		}
		set
		{
			this.joystickPositionOffset = value;
			this.ComputeJoystickAnchor(this.joyAnchor);
		}
	}

	// Token: 0x1700000C RID: 12
	// (get) Token: 0x06000037 RID: 55 RVA: 0x00002F98 File Offset: 0x00001198
	// (set) Token: 0x06000038 RID: 56 RVA: 0x00002FA0 File Offset: 0x000011A0
	public float ZoneRadius
	{
		get
		{
			return this.zoneRadius;
		}
		set
		{
			this.zoneRadius = value;
			this.ComputeJoystickAnchor(this.joyAnchor);
		}
	}

	// Token: 0x1700000D RID: 13
	// (get) Token: 0x06000039 RID: 57 RVA: 0x00002FB8 File Offset: 0x000011B8
	// (set) Token: 0x0600003A RID: 58 RVA: 0x00002FC0 File Offset: 0x000011C0
	public float TouchSize
	{
		get
		{
			return this.touchSize;
		}
		set
		{
			this.touchSize = value;
			if (this.touchSize > this.zoneRadius / 2f && this.restrictArea)
			{
				this.touchSize = this.zoneRadius / 2f;
			}
			this.ComputeJoystickAnchor(this.joyAnchor);
		}
	}

	// Token: 0x1700000E RID: 14
	// (get) Token: 0x0600003B RID: 59 RVA: 0x00003014 File Offset: 0x00001214
	// (set) Token: 0x0600003C RID: 60 RVA: 0x0000301C File Offset: 0x0000121C
	public bool RestrictArea
	{
		get
		{
			return this.restrictArea;
		}
		set
		{
			this.restrictArea = value;
			if (this.restrictArea)
			{
				this.touchSizeCoef = this.touchSize;
			}
			else
			{
				this.touchSizeCoef = 0f;
			}
			this.ComputeJoystickAnchor(this.joyAnchor);
		}
	}

	// Token: 0x1700000F RID: 15
	// (get) Token: 0x0600003D RID: 61 RVA: 0x00003064 File Offset: 0x00001264
	// (set) Token: 0x0600003E RID: 62 RVA: 0x0000306C File Offset: 0x0000126C
	public EasyJoystick.InteractionType Interaction
	{
		get
		{
			return this.interaction;
		}
		set
		{
			this.interaction = value;
			if (this.interaction == EasyJoystick.InteractionType.Direct || this.interaction == EasyJoystick.InteractionType.Include)
			{
				this.useBroadcast = false;
			}
		}
	}

	// Token: 0x17000010 RID: 16
	// (get) Token: 0x0600003F RID: 63 RVA: 0x00003094 File Offset: 0x00001294
	// (set) Token: 0x06000040 RID: 64 RVA: 0x0000309C File Offset: 0x0000129C
	public Transform XAxisTransform
	{
		get
		{
			return this.xAxisTransform;
		}
		set
		{
			this.xAxisTransform = value;
			if (this.xAxisTransform != null)
			{
				this.xAxisCharacterController = this.xAxisTransform.GetComponent<CharacterController>();
			}
			else
			{
				this.xAxisCharacterController = null;
				this.xAxisGravity = 0f;
			}
		}
	}

	// Token: 0x17000011 RID: 17
	// (get) Token: 0x06000041 RID: 65 RVA: 0x000030EC File Offset: 0x000012EC
	// (set) Token: 0x06000042 RID: 66 RVA: 0x000030F4 File Offset: 0x000012F4
	public EasyJoystick.PropertiesInfluenced XTI
	{
		get
		{
			return this.xTI;
		}
		set
		{
			this.xTI = value;
			if (this.xTI != EasyJoystick.PropertiesInfluenced.RotateLocal)
			{
				this.enableXAutoStab = false;
				this.enableXClamp = false;
			}
		}
	}

	// Token: 0x17000012 RID: 18
	// (get) Token: 0x06000043 RID: 67 RVA: 0x00003118 File Offset: 0x00001318
	// (set) Token: 0x06000044 RID: 68 RVA: 0x00003120 File Offset: 0x00001320
	public float ThresholdX
	{
		get
		{
			return this.thresholdX;
		}
		set
		{
			if (value <= 0f)
			{
				this.thresholdX = value * -1f;
			}
			else
			{
				this.thresholdX = value;
			}
		}
	}

	// Token: 0x17000013 RID: 19
	// (get) Token: 0x06000045 RID: 69 RVA: 0x00003154 File Offset: 0x00001354
	// (set) Token: 0x06000046 RID: 70 RVA: 0x0000315C File Offset: 0x0000135C
	public float StabSpeedX
	{
		get
		{
			return this.stabSpeedX;
		}
		set
		{
			if (value <= 0f)
			{
				this.stabSpeedX = value * -1f;
			}
			else
			{
				this.stabSpeedX = value;
			}
		}
	}

	// Token: 0x17000014 RID: 20
	// (get) Token: 0x06000047 RID: 71 RVA: 0x00003190 File Offset: 0x00001390
	// (set) Token: 0x06000048 RID: 72 RVA: 0x00003198 File Offset: 0x00001398
	public Transform YAxisTransform
	{
		get
		{
			return this.yAxisTransform;
		}
		set
		{
			this.yAxisTransform = value;
			if (this.yAxisTransform != null)
			{
				this.yAxisCharacterController = this.yAxisTransform.GetComponent<CharacterController>();
			}
			else
			{
				this.yAxisCharacterController = null;
				this.yAxisGravity = 0f;
			}
		}
	}

	// Token: 0x17000015 RID: 21
	// (get) Token: 0x06000049 RID: 73 RVA: 0x000031E8 File Offset: 0x000013E8
	// (set) Token: 0x0600004A RID: 74 RVA: 0x000031F0 File Offset: 0x000013F0
	public EasyJoystick.PropertiesInfluenced YTI
	{
		get
		{
			return this.yTI;
		}
		set
		{
			this.yTI = value;
			if (this.yTI != EasyJoystick.PropertiesInfluenced.RotateLocal)
			{
				this.enableYAutoStab = false;
				this.enableYClamp = false;
			}
		}
	}

	// Token: 0x17000016 RID: 22
	// (get) Token: 0x0600004B RID: 75 RVA: 0x00003214 File Offset: 0x00001414
	// (set) Token: 0x0600004C RID: 76 RVA: 0x0000321C File Offset: 0x0000141C
	public float ThresholdY
	{
		get
		{
			return this.thresholdY;
		}
		set
		{
			if (value <= 0f)
			{
				this.thresholdY = value * -1f;
			}
			else
			{
				this.thresholdY = value;
			}
		}
	}

	// Token: 0x17000017 RID: 23
	// (get) Token: 0x0600004D RID: 77 RVA: 0x00003250 File Offset: 0x00001450
	// (set) Token: 0x0600004E RID: 78 RVA: 0x00003258 File Offset: 0x00001458
	public float StabSpeedY
	{
		get
		{
			return this.stabSpeedY;
		}
		set
		{
			if (value <= 0f)
			{
				this.stabSpeedY = value * -1f;
			}
			else
			{
				this.stabSpeedY = value;
			}
		}
	}

	// Token: 0x17000018 RID: 24
	// (get) Token: 0x0600004F RID: 79 RVA: 0x0000328C File Offset: 0x0000148C
	// (set) Token: 0x06000050 RID: 80 RVA: 0x00003294 File Offset: 0x00001494
	public Vector2 Smoothing
	{
		get
		{
			return this.smoothing;
		}
		set
		{
			this.smoothing = value;
			if (this.smoothing.x < 0f)
			{
				this.smoothing.x = 0f;
			}
			if (this.smoothing.y < 0f)
			{
				this.smoothing.y = 0f;
			}
		}
	}

	// Token: 0x17000019 RID: 25
	// (get) Token: 0x06000051 RID: 81 RVA: 0x000032F4 File Offset: 0x000014F4
	// (set) Token: 0x06000052 RID: 82 RVA: 0x000032FC File Offset: 0x000014FC
	public Vector2 Inertia
	{
		get
		{
			return this.inertia;
		}
		set
		{
			this.inertia = value;
			if (this.inertia.x <= 0f)
			{
				this.inertia.x = 1f;
			}
			if (this.inertia.y <= 0f)
			{
				this.inertia.y = 1f;
			}
		}
	}

	// Token: 0x06000053 RID: 83 RVA: 0x0000335C File Offset: 0x0000155C
	private void OnEnable()
	{
		EasyTouch.On_TouchStart += this.On_TouchStart;
		EasyTouch.On_TouchUp += this.On_TouchUp;
		EasyTouch.On_TouchDown += this.On_TouchDown;
		EasyTouch.On_SimpleTap += this.On_SimpleTap;
		EasyTouch.On_DoubleTap += this.On_DoubleTap;
	}

	// Token: 0x06000054 RID: 84 RVA: 0x000033C0 File Offset: 0x000015C0
	private void OnDisable()
	{
		EasyTouch.On_TouchStart -= this.On_TouchStart;
		EasyTouch.On_TouchUp -= this.On_TouchUp;
		EasyTouch.On_TouchDown -= this.On_TouchDown;
		EasyTouch.On_SimpleTap -= this.On_SimpleTap;
		EasyTouch.On_DoubleTap -= this.On_DoubleTap;
		if (Application.isPlaying)
		{
			EasyTouch.RemoveReservedArea(this.areaRect);
		}
	}

	// Token: 0x06000055 RID: 85 RVA: 0x00003438 File Offset: 0x00001638
	private void OnDestroy()
	{
		EasyTouch.On_TouchStart -= this.On_TouchStart;
		EasyTouch.On_TouchUp -= this.On_TouchUp;
		EasyTouch.On_TouchDown -= this.On_TouchDown;
		EasyTouch.On_SimpleTap -= this.On_SimpleTap;
		EasyTouch.On_DoubleTap -= this.On_DoubleTap;
		if (Application.isPlaying)
		{
			EasyTouch.RemoveReservedArea(this.areaRect);
		}
	}

	// Token: 0x06000056 RID: 86 RVA: 0x000034B0 File Offset: 0x000016B0
	private void Start()
	{
		if (!this.dynamicJoystick)
		{
			this.joystickCenter = this.joystickPositionOffset;
			this.ComputeJoystickAnchor(this.joyAnchor);
			this.virtualJoystick = true;
		}
		else
		{
			this.virtualJoystick = false;
		}
		VirtualScreen.ComputeVirtualScreen();
		this.startXLocalAngle = this.GetStartAutoStabAngle(this.xAxisTransform, this.xAI);
		this.startYLocalAngle = this.GetStartAutoStabAngle(this.yAxisTransform, this.yAI);
	}

	// Token: 0x06000057 RID: 87 RVA: 0x00003528 File Offset: 0x00001728
	private void Update()
	{
		if (!this.useFixedUpdate && this.enable)
		{
			this.UpdateJoystick();
		}
	}

	// Token: 0x06000058 RID: 88 RVA: 0x00003548 File Offset: 0x00001748
	private void FixedUpdate()
	{
		if (this.useFixedUpdate && this.enable)
		{
			this.UpdateJoystick();
		}
	}

	// Token: 0x06000059 RID: 89 RVA: 0x00003568 File Offset: 0x00001768
	private void UpdateJoystick()
	{
		if (Application.isPlaying && this.isActivated)
		{
			if (this.joystickIndex == -1 || (this.joystickAxis == Vector2.zero && this.joystickIndex > -1))
			{
				if (this.enableXAutoStab)
				{
					this.DoAutoStabilisation(this.xAxisTransform, this.xAI, this.thresholdX, this.stabSpeedX, this.startXLocalAngle);
				}
				if (this.enableYAutoStab)
				{
					this.DoAutoStabilisation(this.yAxisTransform, this.yAI, this.thresholdY, this.stabSpeedY, this.startYLocalAngle);
				}
			}
			if (!this.dynamicJoystick)
			{
				this.joystickCenter = this.joystickPositionOffset;
			}
			if (this.joystickIndex == -1)
			{
				if (!this.enableSmoothing)
				{
					this.joystickTouch = Vector2.zero;
				}
				else if ((double)this.joystickTouch.sqrMagnitude > 0.0001)
				{
					this.joystickTouch = new Vector2(this.joystickTouch.x - this.joystickTouch.x * this.smoothing.x * Time.deltaTime, this.joystickTouch.y - this.joystickTouch.y * this.smoothing.y * Time.deltaTime);
				}
				else
				{
					this.joystickTouch = Vector2.zero;
				}
			}
			Vector2 lhs = new Vector2(this.joystickAxis.x, this.joystickAxis.y);
			float num = this.ComputeDeadZone();
			this.joystickAxis = new Vector2(this.joystickTouch.x * num, this.joystickTouch.y * num);
			if (this.inverseXAxis)
			{
				this.joystickAxis.x = this.joystickAxis.x * -1f;
			}
			if (this.inverseYAxis)
			{
				this.joystickAxis.y = this.joystickAxis.y * -1f;
			}
			Vector2 a = new Vector2(this.speed.x * this.joystickAxis.x, this.speed.y * this.joystickAxis.y);
			if (this.enableInertia)
			{
				Vector2 b = a - this.joystickValue;
				b.x /= this.inertia.x;
				b.y /= this.inertia.y;
				this.joystickValue += b;
			}
			else
			{
				this.joystickValue = a;
			}
			if (lhs == Vector2.zero && this.joystickAxis != Vector2.zero && this.interaction != EasyJoystick.InteractionType.Direct && this.interaction != EasyJoystick.InteractionType.Include)
			{
				this.CreateEvent(EasyJoystick.MessageName.On_JoystickMoveStart);
			}
			this.UpdateGravity();
			if (this.joystickAxis != Vector2.zero)
			{
				this.sendEnd = false;
				switch (this.interaction)
				{
				case EasyJoystick.InteractionType.Direct:
					this.UpdateDirect();
					break;
				case EasyJoystick.InteractionType.EventNotification:
					this.CreateEvent(EasyJoystick.MessageName.On_JoystickMove);
					break;
				case EasyJoystick.InteractionType.DirectAndEvent:
					this.UpdateDirect();
					this.CreateEvent(EasyJoystick.MessageName.On_JoystickMove);
					break;
				}
			}
			else if (!this.sendEnd)
			{
				this.CreateEvent(EasyJoystick.MessageName.On_JoystickMoveEnd);
				this.sendEnd = true;
			}
		}
	}

	// Token: 0x0600005A RID: 90 RVA: 0x000038D4 File Offset: 0x00001AD4
	private void OnGUI()
	{
		if (this.enable)
		{
			GUI.depth = this.guiDepth;
			base.useGUILayout = this.isUseGuiLayout;
			if (this.dynamicJoystick && Application.isEditor && !Application.isPlaying)
			{
				switch (this.area)
				{
				case EasyJoystick.DynamicArea.FullScreen:
					this.ComputeJoystickAnchor(EasyJoystick.JoystickAnchor.MiddleCenter);
					break;
				case EasyJoystick.DynamicArea.Left:
					this.ComputeJoystickAnchor(EasyJoystick.JoystickAnchor.MiddleLeft);
					break;
				case EasyJoystick.DynamicArea.Right:
					this.ComputeJoystickAnchor(EasyJoystick.JoystickAnchor.MiddleRight);
					break;
				case EasyJoystick.DynamicArea.Top:
					this.ComputeJoystickAnchor(EasyJoystick.JoystickAnchor.UpperCenter);
					break;
				case EasyJoystick.DynamicArea.Bottom:
					this.ComputeJoystickAnchor(EasyJoystick.JoystickAnchor.LowerCenter);
					break;
				case EasyJoystick.DynamicArea.TopLeft:
					this.ComputeJoystickAnchor(EasyJoystick.JoystickAnchor.UpperLeft);
					break;
				case EasyJoystick.DynamicArea.TopRight:
					this.ComputeJoystickAnchor(EasyJoystick.JoystickAnchor.UpperRight);
					break;
				case EasyJoystick.DynamicArea.BottomLeft:
					this.ComputeJoystickAnchor(EasyJoystick.JoystickAnchor.LowerLeft);
					break;
				case EasyJoystick.DynamicArea.BottomRight:
					this.ComputeJoystickAnchor(EasyJoystick.JoystickAnchor.LowerRight);
					break;
				}
			}
			if (Application.isEditor && !Application.isPlaying)
			{
				VirtualScreen.ComputeVirtualScreen();
				this.ComputeJoystickAnchor(this.joyAnchor);
			}
			VirtualScreen.SetGuiScaleMatrix();
			if ((this.showZone && this.areaTexture != null && !this.dynamicJoystick) || (this.showZone && this.dynamicJoystick && this.virtualJoystick && this.areaTexture != null) || (this.dynamicJoystick && Application.isEditor && !Application.isPlaying))
			{
				if (this.isActivated)
				{
					GUI.color = this.areaColor;
					if (Application.isPlaying && !this.dynamicJoystick)
					{
						EasyTouch.RemoveReservedArea(this.areaRect);
						EasyTouch.AddReservedArea(this.areaRect);
					}
				}
				else
				{
					GUI.color = new Color(this.areaColor.r, this.areaColor.g, this.areaColor.b, 0.2f);
					if (Application.isPlaying && !this.dynamicJoystick)
					{
						EasyTouch.RemoveReservedArea(this.areaRect);
					}
				}
				if (this.showDebugRadius && Application.isEditor)
				{
					GUI.Box(this.areaRect, string.Empty);
				}
				GUI.DrawTexture(this.areaRect, this.areaTexture, ScaleMode.StretchToFill, true);
			}
			if ((this.showTouch && this.touchTexture != null && !this.dynamicJoystick) || (this.showTouch && this.dynamicJoystick && this.virtualJoystick && this.touchTexture != null) || (this.dynamicJoystick && Application.isEditor && !Application.isPlaying))
			{
				if (this.isActivated)
				{
					GUI.color = this.touchColor;
				}
				else
				{
					GUI.color = new Color(this.touchColor.r, this.touchColor.g, this.touchColor.b, 0.2f);
				}
				GUI.DrawTexture(new Rect(this.anchorPosition.x + this.joystickCenter.x + (this.joystickTouch.x - this.touchSize), this.anchorPosition.y + this.joystickCenter.y - (this.joystickTouch.y + this.touchSize), this.touchSize * 2f, this.touchSize * 2f), this.touchTexture, ScaleMode.ScaleToFit, true);
			}
			if ((this.showDeadZone && this.deadTexture != null && !this.dynamicJoystick) || (this.showDeadZone && this.dynamicJoystick && this.virtualJoystick && this.deadTexture != null) || (this.dynamicJoystick && Application.isEditor && !Application.isPlaying))
			{
				GUI.DrawTexture(this.deadRect, this.deadTexture, ScaleMode.ScaleToFit, true);
			}
			GUI.color = Color.white;
		}
		else
		{
			EasyTouch.RemoveReservedArea(this.areaRect);
		}
	}

	// Token: 0x0600005B RID: 91 RVA: 0x00003D1C File Offset: 0x00001F1C
	private void OnDrawGizmos()
	{
	}

	// Token: 0x0600005C RID: 92 RVA: 0x00003D20 File Offset: 0x00001F20
	private void CreateEvent(EasyJoystick.MessageName message)
	{
		MovingJoystick movingJoystick = new MovingJoystick();
		movingJoystick.joystickName = base.gameObject.name;
		movingJoystick.joystickAxis = this.joystickAxis;
		movingJoystick.joystickValue = this.joystickValue;
		movingJoystick.joystick = this;
		if (!this.useBroadcast)
		{
			switch (message)
			{
			case EasyJoystick.MessageName.On_JoystickMoveStart:
				if (EasyJoystick.On_JoystickMoveStart != null)
				{
					EasyJoystick.On_JoystickMoveStart(movingJoystick);
				}
				break;
			case EasyJoystick.MessageName.On_JoystickTouchStart:
				if (EasyJoystick.On_JoystickTouchStart != null)
				{
					EasyJoystick.On_JoystickTouchStart(movingJoystick);
				}
				break;
			case EasyJoystick.MessageName.On_JoystickTouchUp:
				if (EasyJoystick.On_JoystickTouchUp != null)
				{
					EasyJoystick.On_JoystickTouchUp(movingJoystick);
				}
				break;
			case EasyJoystick.MessageName.On_JoystickMove:
				if (EasyJoystick.On_JoystickMove != null)
				{
					EasyJoystick.On_JoystickMove(movingJoystick);
				}
				break;
			case EasyJoystick.MessageName.On_JoystickMoveEnd:
				if (EasyJoystick.On_JoystickMoveEnd != null)
				{
					EasyJoystick.On_JoystickMoveEnd(movingJoystick);
				}
				break;
			case EasyJoystick.MessageName.On_JoystickTap:
				if (EasyJoystick.On_JoystickTap != null)
				{
					EasyJoystick.On_JoystickTap(movingJoystick);
				}
				break;
			case EasyJoystick.MessageName.On_JoystickDoubleTap:
				if (EasyJoystick.On_JoystickDoubleTap != null)
				{
					EasyJoystick.On_JoystickDoubleTap(movingJoystick);
				}
				break;
			}
		}
		else if (this.useBroadcast)
		{
			if (this.receiverGameObject != null)
			{
				switch (this.messageMode)
				{
				case EasyJoystick.Broadcast.SendMessage:
					this.receiverGameObject.SendMessage(message.ToString(), movingJoystick, SendMessageOptions.DontRequireReceiver);
					break;
				case EasyJoystick.Broadcast.SendMessageUpwards:
					this.receiverGameObject.SendMessageUpwards(message.ToString(), movingJoystick, SendMessageOptions.DontRequireReceiver);
					break;
				case EasyJoystick.Broadcast.BroadcastMessage:
					this.receiverGameObject.BroadcastMessage(message.ToString(), movingJoystick, SendMessageOptions.DontRequireReceiver);
					break;
				}
			}
			else
			{
				Debug.LogError("Joystick : " + base.gameObject.name + " : you must setup receiver gameobject");
			}
		}
	}

	// Token: 0x0600005D RID: 93 RVA: 0x00003F08 File Offset: 0x00002108
	private void UpdateDirect()
	{
		if (this.xAxisTransform != null)
		{
			Vector3 influencedAxis = this.GetInfluencedAxis(this.xAI);
			this.DoActionDirect(this.xAxisTransform, this.xTI, influencedAxis, this.joystickValue.x, this.xAxisCharacterController);
			if (this.enableXClamp && this.xTI == EasyJoystick.PropertiesInfluenced.RotateLocal)
			{
				this.DoAngleLimitation(this.xAxisTransform, this.xAI, this.clampXMin, this.clampXMax, this.startXLocalAngle);
			}
		}
		if (this.YAxisTransform != null)
		{
			Vector3 influencedAxis2 = this.GetInfluencedAxis(this.yAI);
			this.DoActionDirect(this.yAxisTransform, this.yTI, influencedAxis2, this.joystickValue.y, this.yAxisCharacterController);
			if (this.enableYClamp && this.yTI == EasyJoystick.PropertiesInfluenced.RotateLocal)
			{
				this.DoAngleLimitation(this.yAxisTransform, this.yAI, this.clampYMin, this.clampYMax, this.startYLocalAngle);
			}
		}
	}

	// Token: 0x0600005E RID: 94 RVA: 0x00004010 File Offset: 0x00002210
	private void UpdateGravity()
	{
		if (this.xAxisCharacterController != null && this.xAxisGravity > 0f)
		{
			this.xAxisCharacterController.Move(Vector3.down * this.xAxisGravity * Time.deltaTime);
		}
		if (this.yAxisCharacterController != null && this.yAxisGravity > 0f)
		{
			this.yAxisCharacterController.Move(Vector3.down * this.yAxisGravity * Time.deltaTime);
		}
	}

	// Token: 0x0600005F RID: 95 RVA: 0x000040AC File Offset: 0x000022AC
	private Vector3 GetInfluencedAxis(EasyJoystick.AxisInfluenced axisInfluenced)
	{
		Vector3 result = Vector3.zero;
		switch (axisInfluenced)
		{
		case EasyJoystick.AxisInfluenced.X:
			result = Vector3.right;
			break;
		case EasyJoystick.AxisInfluenced.Y:
			result = Vector3.up;
			break;
		case EasyJoystick.AxisInfluenced.Z:
			result = Vector3.forward;
			break;
		case EasyJoystick.AxisInfluenced.XYZ:
			result = Vector3.one;
			break;
		}
		return result;
	}

	// Token: 0x06000060 RID: 96 RVA: 0x0000410C File Offset: 0x0000230C
	private void DoActionDirect(Transform axisTransform, EasyJoystick.PropertiesInfluenced inlfuencedProperty, Vector3 axis, float sensibility, CharacterController charact)
	{
		switch (inlfuencedProperty)
		{
		case EasyJoystick.PropertiesInfluenced.Rotate:
			axisTransform.Rotate(axis * sensibility * Time.deltaTime, Space.World);
			break;
		case EasyJoystick.PropertiesInfluenced.RotateLocal:
			axisTransform.Rotate(axis * sensibility * Time.deltaTime, Space.Self);
			break;
		case EasyJoystick.PropertiesInfluenced.Translate:
			if (charact == null)
			{
				axisTransform.Translate(axis * sensibility * Time.deltaTime, Space.World);
			}
			else
			{
				charact.Move(axis * sensibility * Time.deltaTime);
			}
			break;
		case EasyJoystick.PropertiesInfluenced.TranslateLocal:
			if (charact == null)
			{
				axisTransform.Translate(axis * sensibility * Time.deltaTime, Space.Self);
			}
			else
			{
				charact.Move(charact.transform.TransformDirection(axis) * sensibility * Time.deltaTime);
			}
			break;
		case EasyJoystick.PropertiesInfluenced.Scale:
			axisTransform.localScale += axis * sensibility * Time.deltaTime;
			break;
		}
	}

	// Token: 0x06000061 RID: 97 RVA: 0x00004240 File Offset: 0x00002440
	private void DoAngleLimitation(Transform axisTransform, EasyJoystick.AxisInfluenced axisInfluenced, float clampMin, float clampMax, float startAngle)
	{
		float num = 0f;
		switch (axisInfluenced)
		{
		case EasyJoystick.AxisInfluenced.X:
			num = axisTransform.localRotation.eulerAngles.x;
			break;
		case EasyJoystick.AxisInfluenced.Y:
			num = axisTransform.localRotation.eulerAngles.y;
			break;
		case EasyJoystick.AxisInfluenced.Z:
			num = axisTransform.localRotation.eulerAngles.z;
			break;
		}
		if (num <= 360f && num >= 180f)
		{
			num -= 360f;
		}
		num = Mathf.Clamp(num, -clampMax, clampMin);
		switch (axisInfluenced)
		{
		case EasyJoystick.AxisInfluenced.X:
			axisTransform.localEulerAngles = new Vector3(num, axisTransform.localEulerAngles.y, axisTransform.localEulerAngles.z);
			break;
		case EasyJoystick.AxisInfluenced.Y:
			axisTransform.localEulerAngles = new Vector3(axisTransform.localEulerAngles.x, num, axisTransform.localEulerAngles.z);
			break;
		case EasyJoystick.AxisInfluenced.Z:
			axisTransform.localEulerAngles = new Vector3(axisTransform.localEulerAngles.x, axisTransform.localEulerAngles.y, num);
			break;
		}
	}

	// Token: 0x06000062 RID: 98 RVA: 0x00004394 File Offset: 0x00002594
	private void DoAutoStabilisation(Transform axisTransform, EasyJoystick.AxisInfluenced axisInfluenced, float threshold, float speed, float startAngle)
	{
		float num = 0f;
		switch (axisInfluenced)
		{
		case EasyJoystick.AxisInfluenced.X:
			num = axisTransform.localRotation.eulerAngles.x;
			break;
		case EasyJoystick.AxisInfluenced.Y:
			num = axisTransform.localRotation.eulerAngles.y;
			break;
		case EasyJoystick.AxisInfluenced.Z:
			num = axisTransform.localRotation.eulerAngles.z;
			break;
		}
		if (num <= 360f && num >= 180f)
		{
			num -= 360f;
		}
		if (num > startAngle - threshold || num < startAngle + threshold)
		{
			float num2 = 0f;
			Vector3 zero = Vector3.zero;
			if (num > startAngle - threshold)
			{
				num2 = num + speed / 100f * Mathf.Abs(num - startAngle) * Time.deltaTime * -1f;
			}
			if (num < startAngle + threshold)
			{
				num2 = num + speed / 100f * Mathf.Abs(num - startAngle) * Time.deltaTime;
			}
			switch (axisInfluenced)
			{
			case EasyJoystick.AxisInfluenced.X:
				zero = new Vector3(num2, axisTransform.localRotation.eulerAngles.y, axisTransform.localRotation.eulerAngles.z);
				break;
			case EasyJoystick.AxisInfluenced.Y:
				zero = new Vector3(axisTransform.localRotation.eulerAngles.x, num2, axisTransform.localRotation.eulerAngles.z);
				break;
			case EasyJoystick.AxisInfluenced.Z:
				zero = new Vector3(axisTransform.localRotation.eulerAngles.x, axisTransform.localRotation.eulerAngles.y, num2);
				break;
			}
			axisTransform.localRotation = Quaternion.Euler(zero);
		}
	}

	// Token: 0x06000063 RID: 99 RVA: 0x00004584 File Offset: 0x00002784
	private float GetStartAutoStabAngle(Transform axisTransform, EasyJoystick.AxisInfluenced axisInfluenced)
	{
		float num = 0f;
		if (axisTransform != null)
		{
			switch (axisInfluenced)
			{
			case EasyJoystick.AxisInfluenced.X:
				num = axisTransform.localRotation.eulerAngles.x;
				break;
			case EasyJoystick.AxisInfluenced.Y:
				num = axisTransform.localRotation.eulerAngles.y;
				break;
			case EasyJoystick.AxisInfluenced.Z:
				num = axisTransform.localRotation.eulerAngles.z;
				break;
			}
			if (num <= 360f && num >= 180f)
			{
				num -= 360f;
			}
		}
		return num;
	}

	// Token: 0x06000064 RID: 100 RVA: 0x00004634 File Offset: 0x00002834
	private float ComputeDeadZone()
	{
		float num = Mathf.Max(this.joystickTouch.magnitude, 0.1f);
		float result;
		if (this.restrictArea)
		{
			result = Mathf.Max(num - this.deadZone, 0f) / (this.zoneRadius - this.touchSize - this.deadZone) / num;
		}
		else
		{
			result = Mathf.Max(num - this.deadZone, 0f) / (this.zoneRadius - this.deadZone) / num;
		}
		return result;
	}

	// Token: 0x06000065 RID: 101 RVA: 0x000046BC File Offset: 0x000028BC
	private void ComputeJoystickAnchor(EasyJoystick.JoystickAnchor anchor)
	{
		float num = 0f;
		if (!this.restrictArea)
		{
			num = this.touchSize;
		}
		switch (anchor)
		{
		case EasyJoystick.JoystickAnchor.None:
			this.anchorPosition = Vector2.zero;
			break;
		case EasyJoystick.JoystickAnchor.UpperLeft:
			this.anchorPosition = new Vector2(this.zoneRadius + num, this.zoneRadius + num);
			break;
		case EasyJoystick.JoystickAnchor.UpperCenter:
			this.anchorPosition = new Vector2(VirtualScreen.width / 2f, this.zoneRadius + num);
			break;
		case EasyJoystick.JoystickAnchor.UpperRight:
			this.anchorPosition = new Vector2(VirtualScreen.width - this.zoneRadius - num, this.zoneRadius + num);
			break;
		case EasyJoystick.JoystickAnchor.MiddleLeft:
			this.anchorPosition = new Vector2(this.zoneRadius + num, VirtualScreen.height / 2f);
			break;
		case EasyJoystick.JoystickAnchor.MiddleCenter:
			this.anchorPosition = new Vector2(VirtualScreen.width / 2f, VirtualScreen.height / 2f);
			break;
		case EasyJoystick.JoystickAnchor.MiddleRight:
			this.anchorPosition = new Vector2(VirtualScreen.width - this.zoneRadius - num, VirtualScreen.height / 2f);
			break;
		case EasyJoystick.JoystickAnchor.LowerLeft:
			this.anchorPosition = new Vector2(this.zoneRadius + num, VirtualScreen.height - this.zoneRadius - num);
			break;
		case EasyJoystick.JoystickAnchor.LowerCenter:
			this.anchorPosition = new Vector2(VirtualScreen.width / 2f, VirtualScreen.height - this.zoneRadius - num);
			break;
		case EasyJoystick.JoystickAnchor.LowerRight:
			this.anchorPosition = new Vector2(VirtualScreen.width - this.zoneRadius - num, VirtualScreen.height - this.zoneRadius - num);
			break;
		}
		this.areaRect = new Rect(this.anchorPosition.x + this.joystickCenter.x - this.zoneRadius, this.anchorPosition.y + this.joystickCenter.y - this.zoneRadius, this.zoneRadius * 2f, this.zoneRadius * 2f);
		this.deadRect = new Rect(this.anchorPosition.x + this.joystickCenter.x - this.deadZone, this.anchorPosition.y + this.joystickCenter.y - this.deadZone, this.deadZone * 2f, this.deadZone * 2f);
	}

	// Token: 0x06000066 RID: 102 RVA: 0x0000493C File Offset: 0x00002B3C
	private void On_TouchStart(Gesture gesture)
	{
		if (((!gesture.isHoverReservedArea && this.dynamicJoystick) || !this.dynamicJoystick) && this.isActivated)
		{
			if (!this.dynamicJoystick)
			{
				Vector2 b = new Vector2((this.anchorPosition.x + this.joystickCenter.x) * VirtualScreen.xRatio, (VirtualScreen.height - this.anchorPosition.y - this.joystickCenter.y) * VirtualScreen.yRatio);
				if ((gesture.position - b).sqrMagnitude < this.zoneRadius * VirtualScreen.xRatio * (this.zoneRadius * VirtualScreen.xRatio))
				{
					this.joystickIndex = gesture.fingerIndex;
					this.CreateEvent(EasyJoystick.MessageName.On_JoystickTouchStart);
				}
			}
			else if (!this.virtualJoystick)
			{
				switch (this.area)
				{
				case EasyJoystick.DynamicArea.FullScreen:
					this.virtualJoystick = true;
					break;
				case EasyJoystick.DynamicArea.Left:
					if (gesture.position.x < (float)(Screen.width / 2))
					{
						this.virtualJoystick = true;
					}
					break;
				case EasyJoystick.DynamicArea.Right:
					if (gesture.position.x > (float)(Screen.width / 2))
					{
						this.virtualJoystick = true;
					}
					break;
				case EasyJoystick.DynamicArea.Top:
					if (gesture.position.y > (float)(Screen.height / 2))
					{
						this.virtualJoystick = true;
					}
					break;
				case EasyJoystick.DynamicArea.Bottom:
					if (gesture.position.y < (float)(Screen.height / 2))
					{
						this.virtualJoystick = true;
					}
					break;
				case EasyJoystick.DynamicArea.TopLeft:
					if (gesture.position.y > (float)(Screen.height / 2) && gesture.position.x < (float)(Screen.width / 2))
					{
						this.virtualJoystick = true;
					}
					break;
				case EasyJoystick.DynamicArea.TopRight:
					if (gesture.position.y > (float)(Screen.height / 2) && gesture.position.x > (float)(Screen.width / 2))
					{
						this.virtualJoystick = true;
					}
					break;
				case EasyJoystick.DynamicArea.BottomLeft:
					if (gesture.position.y < (float)(Screen.height / 2) && gesture.position.x < (float)(Screen.width / 2))
					{
						this.virtualJoystick = true;
					}
					break;
				case EasyJoystick.DynamicArea.BottomRight:
					if (gesture.position.y < (float)(Screen.height / 2) && gesture.position.x > (float)(Screen.width / 2))
					{
						this.virtualJoystick = true;
					}
					break;
				}
				if (this.virtualJoystick)
				{
					this.joystickCenter = new Vector2(gesture.position.x / VirtualScreen.xRatio, VirtualScreen.height - gesture.position.y / VirtualScreen.yRatio);
					this.JoyAnchor = EasyJoystick.JoystickAnchor.None;
					this.joystickIndex = gesture.fingerIndex;
				}
			}
		}
	}

	// Token: 0x06000067 RID: 103 RVA: 0x00004C30 File Offset: 0x00002E30
	private void On_SimpleTap(Gesture gesture)
	{
		if (((!gesture.isHoverReservedArea && this.dynamicJoystick) || !this.dynamicJoystick) && this.isActivated && gesture.fingerIndex == this.joystickIndex)
		{
			this.CreateEvent(EasyJoystick.MessageName.On_JoystickTap);
		}
	}

	// Token: 0x06000068 RID: 104 RVA: 0x00004C84 File Offset: 0x00002E84
	private void On_DoubleTap(Gesture gesture)
	{
		if (((!gesture.isHoverReservedArea && this.dynamicJoystick) || !this.dynamicJoystick) && this.isActivated && gesture.fingerIndex == this.joystickIndex)
		{
			this.CreateEvent(EasyJoystick.MessageName.On_JoystickDoubleTap);
		}
	}

	// Token: 0x06000069 RID: 105 RVA: 0x00004CD8 File Offset: 0x00002ED8
	private void On_TouchDown(Gesture gesture)
	{
		if (((!gesture.isHoverReservedArea && this.dynamicJoystick) || !this.dynamicJoystick) && this.isActivated)
		{
			Vector2 b = new Vector2((this.anchorPosition.x + this.joystickCenter.x) * VirtualScreen.xRatio, (VirtualScreen.height - (this.anchorPosition.y + this.joystickCenter.y)) * VirtualScreen.yRatio);
			if (gesture.fingerIndex == this.joystickIndex)
			{
				if (((gesture.position - b).sqrMagnitude < this.zoneRadius * VirtualScreen.xRatio * (this.zoneRadius * VirtualScreen.xRatio) && this.resetFingerExit) || !this.resetFingerExit)
				{
					this.joystickTouch = new Vector2(gesture.position.x, gesture.position.y) - b;
					this.joystickTouch = new Vector2(this.joystickTouch.x / VirtualScreen.xRatio, this.joystickTouch.y / VirtualScreen.yRatio);
					if (!this.enableXaxis)
					{
						this.joystickTouch.x = 0f;
					}
					if (!this.enableYaxis)
					{
						this.joystickTouch.y = 0f;
					}
					if ((this.joystickTouch / (this.zoneRadius - this.touchSizeCoef)).sqrMagnitude > 1f)
					{
						this.joystickTouch.Normalize();
						this.joystickTouch *= this.zoneRadius - this.touchSizeCoef;
					}
				}
				else
				{
					this.On_TouchUp(gesture);
				}
			}
		}
	}

	// Token: 0x0600006A RID: 106 RVA: 0x00004E9C File Offset: 0x0000309C
	private void On_TouchUp(Gesture gesture)
	{
		if (((!gesture.isHoverReservedArea && this.dynamicJoystick) || !this.dynamicJoystick) && this.isActivated && gesture.fingerIndex == this.joystickIndex)
		{
			this.joystickIndex = -1;
			if (this.dynamicJoystick)
			{
				this.virtualJoystick = false;
			}
			this.CreateEvent(EasyJoystick.MessageName.On_JoystickTouchUp);
		}
	}

	// Token: 0x0400003E RID: 62
	private Vector2 joystickAxis;

	// Token: 0x0400003F RID: 63
	private Vector2 joystickTouch;

	// Token: 0x04000040 RID: 64
	private Vector2 joystickValue;

	// Token: 0x04000041 RID: 65
	public bool enable = true;

	// Token: 0x04000042 RID: 66
	public bool isActivated = true;

	// Token: 0x04000043 RID: 67
	public bool showDebugRadius;

	// Token: 0x04000044 RID: 68
	public bool useFixedUpdate;

	// Token: 0x04000045 RID: 69
	public bool isUseGuiLayout = true;

	// Token: 0x04000046 RID: 70
	[SerializeField]
	private bool dynamicJoystick;

	// Token: 0x04000047 RID: 71
	public EasyJoystick.DynamicArea area;

	// Token: 0x04000048 RID: 72
	[SerializeField]
	private EasyJoystick.JoystickAnchor joyAnchor = EasyJoystick.JoystickAnchor.LowerLeft;

	// Token: 0x04000049 RID: 73
	[SerializeField]
	private Vector2 joystickPositionOffset = Vector2.zero;

	// Token: 0x0400004A RID: 74
	[SerializeField]
	private float zoneRadius = 100f;

	// Token: 0x0400004B RID: 75
	[SerializeField]
	private float touchSize = 30f;

	// Token: 0x0400004C RID: 76
	public float deadZone = 20f;

	// Token: 0x0400004D RID: 77
	[SerializeField]
	private bool restrictArea;

	// Token: 0x0400004E RID: 78
	public bool resetFingerExit;

	// Token: 0x0400004F RID: 79
	[SerializeField]
	private EasyJoystick.InteractionType interaction;

	// Token: 0x04000050 RID: 80
	public bool useBroadcast;

	// Token: 0x04000051 RID: 81
	public EasyJoystick.Broadcast messageMode;

	// Token: 0x04000052 RID: 82
	public GameObject receiverGameObject;

	// Token: 0x04000053 RID: 83
	public Vector2 speed;

	// Token: 0x04000054 RID: 84
	public bool enableXaxis = true;

	// Token: 0x04000055 RID: 85
	[SerializeField]
	private Transform xAxisTransform;

	// Token: 0x04000056 RID: 86
	public CharacterController xAxisCharacterController;

	// Token: 0x04000057 RID: 87
	public float xAxisGravity;

	// Token: 0x04000058 RID: 88
	[SerializeField]
	private EasyJoystick.PropertiesInfluenced xTI;

	// Token: 0x04000059 RID: 89
	public EasyJoystick.AxisInfluenced xAI;

	// Token: 0x0400005A RID: 90
	public bool inverseXAxis;

	// Token: 0x0400005B RID: 91
	public bool enableXClamp;

	// Token: 0x0400005C RID: 92
	public float clampXMax;

	// Token: 0x0400005D RID: 93
	public float clampXMin;

	// Token: 0x0400005E RID: 94
	public bool enableXAutoStab;

	// Token: 0x0400005F RID: 95
	[SerializeField]
	private float thresholdX = 0.01f;

	// Token: 0x04000060 RID: 96
	[SerializeField]
	private float stabSpeedX = 20f;

	// Token: 0x04000061 RID: 97
	public bool enableYaxis = true;

	// Token: 0x04000062 RID: 98
	[SerializeField]
	private Transform yAxisTransform;

	// Token: 0x04000063 RID: 99
	public CharacterController yAxisCharacterController;

	// Token: 0x04000064 RID: 100
	public float yAxisGravity;

	// Token: 0x04000065 RID: 101
	[SerializeField]
	private EasyJoystick.PropertiesInfluenced yTI;

	// Token: 0x04000066 RID: 102
	public EasyJoystick.AxisInfluenced yAI;

	// Token: 0x04000067 RID: 103
	public bool inverseYAxis;

	// Token: 0x04000068 RID: 104
	public bool enableYClamp;

	// Token: 0x04000069 RID: 105
	public float clampYMax;

	// Token: 0x0400006A RID: 106
	public float clampYMin;

	// Token: 0x0400006B RID: 107
	public bool enableYAutoStab;

	// Token: 0x0400006C RID: 108
	[SerializeField]
	private float thresholdY = 0.01f;

	// Token: 0x0400006D RID: 109
	[SerializeField]
	private float stabSpeedY = 20f;

	// Token: 0x0400006E RID: 110
	public bool enableSmoothing;

	// Token: 0x0400006F RID: 111
	[SerializeField]
	public Vector2 smoothing = new Vector2(2f, 2f);

	// Token: 0x04000070 RID: 112
	public bool enableInertia;

	// Token: 0x04000071 RID: 113
	[SerializeField]
	public Vector2 inertia = new Vector2(100f, 100f);

	// Token: 0x04000072 RID: 114
	public int guiDepth;

	// Token: 0x04000073 RID: 115
	public bool showZone = true;

	// Token: 0x04000074 RID: 116
	public bool showTouch = true;

	// Token: 0x04000075 RID: 117
	public bool showDeadZone = true;

	// Token: 0x04000076 RID: 118
	public Texture areaTexture;

	// Token: 0x04000077 RID: 119
	public Color areaColor = Color.white;

	// Token: 0x04000078 RID: 120
	public Texture touchTexture;

	// Token: 0x04000079 RID: 121
	public Color touchColor = Color.white;

	// Token: 0x0400007A RID: 122
	public Texture deadTexture;

	// Token: 0x0400007B RID: 123
	public bool showProperties = true;

	// Token: 0x0400007C RID: 124
	public bool showInteraction;

	// Token: 0x0400007D RID: 125
	public bool showAppearance;

	// Token: 0x0400007E RID: 126
	public bool showPosition = true;

	// Token: 0x0400007F RID: 127
	private Vector2 joystickCenter;

	// Token: 0x04000080 RID: 128
	private Rect areaRect;

	// Token: 0x04000081 RID: 129
	private Rect deadRect;

	// Token: 0x04000082 RID: 130
	private Vector2 anchorPosition = Vector2.zero;

	// Token: 0x04000083 RID: 131
	private bool virtualJoystick = true;

	// Token: 0x04000084 RID: 132
	private int joystickIndex = -1;

	// Token: 0x04000085 RID: 133
	private float touchSizeCoef;

	// Token: 0x04000086 RID: 134
	private bool sendEnd = true;

	// Token: 0x04000087 RID: 135
	private float startXLocalAngle;

	// Token: 0x04000088 RID: 136
	private float startYLocalAngle;

	// Token: 0x02000009 RID: 9
	public enum JoystickAnchor
	{
		// Token: 0x04000091 RID: 145
		None,
		// Token: 0x04000092 RID: 146
		UpperLeft,
		// Token: 0x04000093 RID: 147
		UpperCenter,
		// Token: 0x04000094 RID: 148
		UpperRight,
		// Token: 0x04000095 RID: 149
		MiddleLeft,
		// Token: 0x04000096 RID: 150
		MiddleCenter,
		// Token: 0x04000097 RID: 151
		MiddleRight,
		// Token: 0x04000098 RID: 152
		LowerLeft,
		// Token: 0x04000099 RID: 153
		LowerCenter,
		// Token: 0x0400009A RID: 154
		LowerRight
	}

	// Token: 0x0200000A RID: 10
	public enum PropertiesInfluenced
	{
		// Token: 0x0400009C RID: 156
		Rotate,
		// Token: 0x0400009D RID: 157
		RotateLocal,
		// Token: 0x0400009E RID: 158
		Translate,
		// Token: 0x0400009F RID: 159
		TranslateLocal,
		// Token: 0x040000A0 RID: 160
		Scale
	}

	// Token: 0x0200000B RID: 11
	public enum AxisInfluenced
	{
		// Token: 0x040000A2 RID: 162
		X,
		// Token: 0x040000A3 RID: 163
		Y,
		// Token: 0x040000A4 RID: 164
		Z,
		// Token: 0x040000A5 RID: 165
		XYZ
	}

	// Token: 0x0200000C RID: 12
	public enum DynamicArea
	{
		// Token: 0x040000A7 RID: 167
		FullScreen,
		// Token: 0x040000A8 RID: 168
		Left,
		// Token: 0x040000A9 RID: 169
		Right,
		// Token: 0x040000AA RID: 170
		Top,
		// Token: 0x040000AB RID: 171
		Bottom,
		// Token: 0x040000AC RID: 172
		TopLeft,
		// Token: 0x040000AD RID: 173
		TopRight,
		// Token: 0x040000AE RID: 174
		BottomLeft,
		// Token: 0x040000AF RID: 175
		BottomRight
	}

	// Token: 0x0200000D RID: 13
	public enum InteractionType
	{
		// Token: 0x040000B1 RID: 177
		Direct,
		// Token: 0x040000B2 RID: 178
		Include,
		// Token: 0x040000B3 RID: 179
		EventNotification,
		// Token: 0x040000B4 RID: 180
		DirectAndEvent
	}

	// Token: 0x0200000E RID: 14
	public enum Broadcast
	{
		// Token: 0x040000B6 RID: 182
		SendMessage,
		// Token: 0x040000B7 RID: 183
		SendMessageUpwards,
		// Token: 0x040000B8 RID: 184
		BroadcastMessage
	}

	// Token: 0x0200000F RID: 15
	private enum MessageName
	{
		// Token: 0x040000BA RID: 186
		On_JoystickMoveStart,
		// Token: 0x040000BB RID: 187
		On_JoystickTouchStart,
		// Token: 0x040000BC RID: 188
		On_JoystickTouchUp,
		// Token: 0x040000BD RID: 189
		On_JoystickMove,
		// Token: 0x040000BE RID: 190
		On_JoystickMoveEnd,
		// Token: 0x040000BF RID: 191
		On_JoystickTap,
		// Token: 0x040000C0 RID: 192
		On_JoystickDoubleTap
	}

	// Token: 0x020001B4 RID: 436
	// (Invoke) Token: 0x06000CED RID: 3309
	public delegate void JoystickMoveStartHandler(MovingJoystick move);

	// Token: 0x020001B5 RID: 437
	// (Invoke) Token: 0x06000CF1 RID: 3313
	public delegate void JoystickMoveHandler(MovingJoystick move);

	// Token: 0x020001B6 RID: 438
	// (Invoke) Token: 0x06000CF5 RID: 3317
	public delegate void JoystickMoveEndHandler(MovingJoystick move);

	// Token: 0x020001B7 RID: 439
	// (Invoke) Token: 0x06000CF9 RID: 3321
	public delegate void JoystickTouchStartHandler(MovingJoystick move);

	// Token: 0x020001B8 RID: 440
	// (Invoke) Token: 0x06000CFD RID: 3325
	public delegate void JoystickTapHandler(MovingJoystick move);

	// Token: 0x020001B9 RID: 441
	// (Invoke) Token: 0x06000D01 RID: 3329
	public delegate void JoystickDoubleTapHandler(MovingJoystick move);

	// Token: 0x020001BA RID: 442
	// (Invoke) Token: 0x06000D05 RID: 3333
	public delegate void JoystickTouchUpHandler(MovingJoystick move);
}
