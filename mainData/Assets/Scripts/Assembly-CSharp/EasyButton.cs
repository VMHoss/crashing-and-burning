using System;
using UnityEngine;

// Token: 0x02000002 RID: 2
[ExecuteInEditMode]
public class EasyButton : MonoBehaviour
{
	// Token: 0x14000001 RID: 1
	// (add) Token: 0x06000002 RID: 2 RVA: 0x0000216C File Offset: 0x0000036C
	// (remove) Token: 0x06000003 RID: 3 RVA: 0x00002184 File Offset: 0x00000384
	public static event EasyButton.ButtonDownHandler On_ButtonDown;

	// Token: 0x14000002 RID: 2
	// (add) Token: 0x06000004 RID: 4 RVA: 0x0000219C File Offset: 0x0000039C
	// (remove) Token: 0x06000005 RID: 5 RVA: 0x000021B4 File Offset: 0x000003B4
	public static event EasyButton.ButtonPressHandler On_ButtonPress;

	// Token: 0x14000003 RID: 3
	// (add) Token: 0x06000006 RID: 6 RVA: 0x000021CC File Offset: 0x000003CC
	// (remove) Token: 0x06000007 RID: 7 RVA: 0x000021E4 File Offset: 0x000003E4
	public static event EasyButton.ButtonUpHandler On_ButtonUp;

	// Token: 0x17000001 RID: 1
	// (get) Token: 0x06000008 RID: 8 RVA: 0x000021FC File Offset: 0x000003FC
	// (set) Token: 0x06000009 RID: 9 RVA: 0x00002204 File Offset: 0x00000404
	public EasyButton.ButtonAnchor Anchor
	{
		get
		{
			return this.anchor;
		}
		set
		{
			this.anchor = value;
			this.ComputeButtonAnchor(this.anchor);
		}
	}

	// Token: 0x17000002 RID: 2
	// (get) Token: 0x0600000A RID: 10 RVA: 0x0000221C File Offset: 0x0000041C
	// (set) Token: 0x0600000B RID: 11 RVA: 0x00002224 File Offset: 0x00000424
	public Vector2 Offset
	{
		get
		{
			return this.offset;
		}
		set
		{
			this.offset = value;
			this.ComputeButtonAnchor(this.anchor);
		}
	}

	// Token: 0x17000003 RID: 3
	// (get) Token: 0x0600000C RID: 12 RVA: 0x0000223C File Offset: 0x0000043C
	// (set) Token: 0x0600000D RID: 13 RVA: 0x00002244 File Offset: 0x00000444
	public Vector2 Scale
	{
		get
		{
			return this.scale;
		}
		set
		{
			this.scale = value;
			this.ComputeButtonAnchor(this.anchor);
		}
	}

	// Token: 0x17000004 RID: 4
	// (get) Token: 0x0600000E RID: 14 RVA: 0x0000225C File Offset: 0x0000045C
	// (set) Token: 0x0600000F RID: 15 RVA: 0x00002264 File Offset: 0x00000464
	public Texture2D NormalTexture
	{
		get
		{
			return this.normalTexture;
		}
		set
		{
			this.normalTexture = value;
			if (this.normalTexture != null)
			{
				this.ComputeButtonAnchor(this.anchor);
				this.currentTexture = this.normalTexture;
			}
		}
	}

	// Token: 0x17000005 RID: 5
	// (get) Token: 0x06000010 RID: 16 RVA: 0x000022A4 File Offset: 0x000004A4
	// (set) Token: 0x06000011 RID: 17 RVA: 0x000022AC File Offset: 0x000004AC
	public Texture2D ActiveTexture
	{
		get
		{
			return this.activeTexture;
		}
		set
		{
			this.activeTexture = value;
		}
	}

	// Token: 0x06000012 RID: 18 RVA: 0x000022B8 File Offset: 0x000004B8
	private void OnEnable()
	{
		EasyTouch.On_TouchStart += this.On_TouchStart;
		EasyTouch.On_TouchDown += this.On_TouchDown;
		EasyTouch.On_TouchUp += this.On_TouchUp;
	}

	// Token: 0x06000013 RID: 19 RVA: 0x000022F0 File Offset: 0x000004F0
	private void OnDisable()
	{
		EasyTouch.On_TouchStart -= this.On_TouchStart;
		EasyTouch.On_TouchDown -= this.On_TouchDown;
		EasyTouch.On_TouchUp -= this.On_TouchUp;
		if (Application.isPlaying)
		{
			EasyTouch.RemoveReservedArea(this.buttonRect);
		}
	}

	// Token: 0x06000014 RID: 20 RVA: 0x00002348 File Offset: 0x00000548
	private void OnDestroy()
	{
		EasyTouch.On_TouchStart -= this.On_TouchStart;
		EasyTouch.On_TouchDown -= this.On_TouchDown;
		EasyTouch.On_TouchUp -= this.On_TouchUp;
		if (Application.isPlaying)
		{
			EasyTouch.RemoveReservedArea(this.buttonRect);
		}
	}

	// Token: 0x06000015 RID: 21 RVA: 0x000023A0 File Offset: 0x000005A0
	private void Start()
	{
		this.currentTexture = this.normalTexture;
		this.currentColor = this.buttonNormalColor;
		this.buttonState = EasyButton.ButtonState.None;
		VirtualScreen.ComputeVirtualScreen();
		this.ComputeButtonAnchor(this.anchor);
	}

	// Token: 0x06000016 RID: 22 RVA: 0x000023E0 File Offset: 0x000005E0
	private void OnGUI()
	{
		if (this.enable)
		{
			GUI.depth = this.guiDepth;
			base.useGUILayout = this.isUseGuiLayout;
			VirtualScreen.ComputeVirtualScreen();
			VirtualScreen.SetGuiScaleMatrix();
			if (this.normalTexture != null && this.activeTexture != null)
			{
				this.ComputeButtonAnchor(this.anchor);
				if (this.normalTexture != null)
				{
					if (Application.isEditor && !Application.isPlaying)
					{
						this.currentTexture = this.normalTexture;
					}
					if (this.showDebugArea && Application.isEditor)
					{
						GUI.Box(this.buttonRect, string.Empty);
					}
					if (this.currentTexture != null)
					{
						if (this.isActivated)
						{
							GUI.color = this.currentColor;
							if (Application.isPlaying)
							{
								EasyTouch.RemoveReservedArea(this.buttonRect);
								EasyTouch.AddReservedArea(this.buttonRect);
							}
						}
						else
						{
							GUI.color = new Color(this.currentColor.r, this.currentColor.g, this.currentColor.b, 0.2f);
							if (Application.isPlaying)
							{
								EasyTouch.RemoveReservedArea(this.buttonRect);
							}
						}
						GUI.DrawTexture(this.buttonRect, this.currentTexture);
						GUI.color = Color.white;
					}
				}
			}
		}
		else
		{
			EasyTouch.RemoveReservedArea(this.buttonRect);
		}
	}

	// Token: 0x06000017 RID: 23 RVA: 0x0000255C File Offset: 0x0000075C
	private void Update()
	{
		if (this.buttonState == EasyButton.ButtonState.Up)
		{
			this.buttonState = EasyButton.ButtonState.None;
		}
	}

	// Token: 0x06000018 RID: 24 RVA: 0x00002574 File Offset: 0x00000774
	private void OnDrawGizmos()
	{
	}

	// Token: 0x06000019 RID: 25 RVA: 0x00002578 File Offset: 0x00000778
	private void ComputeButtonAnchor(EasyButton.ButtonAnchor anchor)
	{
		if (this.normalTexture != null)
		{
			Vector2 vector = new Vector2((float)this.normalTexture.width * this.scale.x, (float)this.normalTexture.height * this.scale.y);
			Vector2 zero = Vector2.zero;
			switch (anchor)
			{
			case EasyButton.ButtonAnchor.UpperLeft:
				zero = new Vector2(0f, 0f);
				break;
			case EasyButton.ButtonAnchor.UpperCenter:
				zero = new Vector2(VirtualScreen.width / 2f - vector.x / 2f, this.offset.y);
				break;
			case EasyButton.ButtonAnchor.UpperRight:
				zero = new Vector2(VirtualScreen.width - vector.x, 0f);
				break;
			case EasyButton.ButtonAnchor.MiddleLeft:
				zero = new Vector2(0f, VirtualScreen.height / 2f - vector.y / 2f);
				break;
			case EasyButton.ButtonAnchor.MiddleCenter:
				zero = new Vector2(VirtualScreen.width / 2f - vector.x / 2f, VirtualScreen.height / 2f - vector.y / 2f);
				break;
			case EasyButton.ButtonAnchor.MiddleRight:
				zero = new Vector2(VirtualScreen.width - vector.x, VirtualScreen.height / 2f - vector.y / 2f);
				break;
			case EasyButton.ButtonAnchor.LowerLeft:
				zero = new Vector2(0f, VirtualScreen.height - vector.y);
				break;
			case EasyButton.ButtonAnchor.LowerCenter:
				zero = new Vector2(VirtualScreen.width / 2f - vector.x / 2f, VirtualScreen.height - vector.y);
				break;
			case EasyButton.ButtonAnchor.LowerRight:
				zero = new Vector2(VirtualScreen.width - vector.x, VirtualScreen.height - vector.y);
				break;
			}
			this.buttonRect = new Rect(zero.x + this.offset.x, zero.y + this.offset.y, vector.x, vector.y);
		}
	}

	// Token: 0x0600001A RID: 26 RVA: 0x000027B8 File Offset: 0x000009B8
	private void RaiseEvent(EasyButton.MessageName msg)
	{
		if (this.interaction == EasyButton.InteractionType.Event)
		{
			if (!this.useBroadcast)
			{
				switch (msg)
				{
				case EasyButton.MessageName.On_ButtonDown:
					if (EasyButton.On_ButtonDown != null)
					{
						EasyButton.On_ButtonDown(base.gameObject.name);
					}
					break;
				case EasyButton.MessageName.On_ButtonPress:
					if (EasyButton.On_ButtonPress != null)
					{
						EasyButton.On_ButtonPress(base.gameObject.name);
					}
					break;
				case EasyButton.MessageName.On_ButtonUp:
					if (EasyButton.On_ButtonUp != null)
					{
						EasyButton.On_ButtonUp(base.gameObject.name);
					}
					break;
				}
			}
			else
			{
				string methodName = msg.ToString();
				if (msg == EasyButton.MessageName.On_ButtonDown && this.downMethodName != string.Empty && this.useSpecificalMethod)
				{
					methodName = this.downMethodName;
				}
				if (msg == EasyButton.MessageName.On_ButtonPress && this.pressMethodName != string.Empty && this.useSpecificalMethod)
				{
					methodName = this.pressMethodName;
				}
				if (msg == EasyButton.MessageName.On_ButtonUp && this.upMethodName != string.Empty && this.useSpecificalMethod)
				{
					methodName = this.upMethodName;
				}
				if (this.receiverGameObject != null)
				{
					switch (this.messageMode)
					{
					case EasyButton.Broadcast.SendMessage:
						this.receiverGameObject.SendMessage(methodName, base.name, SendMessageOptions.DontRequireReceiver);
						break;
					case EasyButton.Broadcast.SendMessageUpwards:
						this.receiverGameObject.SendMessageUpwards(methodName, base.name, SendMessageOptions.DontRequireReceiver);
						break;
					case EasyButton.Broadcast.BroadcastMessage:
						this.receiverGameObject.BroadcastMessage(methodName, base.name, SendMessageOptions.DontRequireReceiver);
						break;
					}
				}
				else
				{
					Debug.LogError("Button : " + base.gameObject.name + " : you must setup receiver gameobject");
				}
			}
		}
	}

	// Token: 0x0600001B RID: 27 RVA: 0x00002998 File Offset: 0x00000B98
	private void On_TouchStart(Gesture gesture)
	{
		if (gesture.IsInRect(VirtualScreen.GetRealRect(this.buttonRect), true) && this.enable && this.isActivated)
		{
			this.buttonFingerIndex = gesture.fingerIndex;
			this.currentTexture = this.activeTexture;
			this.currentColor = this.buttonActiveColor;
			this.buttonState = EasyButton.ButtonState.Down;
			this.frame = 0;
			this.RaiseEvent(EasyButton.MessageName.On_ButtonDown);
		}
	}

	// Token: 0x0600001C RID: 28 RVA: 0x00002A0C File Offset: 0x00000C0C
	private void On_TouchDown(Gesture gesture)
	{
		if (gesture.fingerIndex == this.buttonFingerIndex || (this.isSwipeIn && this.buttonState == EasyButton.ButtonState.None))
		{
			if (gesture.IsInRect(VirtualScreen.GetRealRect(this.buttonRect), true) && this.enable && this.isActivated)
			{
				this.currentTexture = this.activeTexture;
				this.currentColor = this.buttonActiveColor;
				this.frame++;
				if ((this.buttonState == EasyButton.ButtonState.Down || this.buttonState == EasyButton.ButtonState.Press) && this.frame >= 2)
				{
					this.RaiseEvent(EasyButton.MessageName.On_ButtonPress);
					this.buttonState = EasyButton.ButtonState.Press;
				}
				if (this.buttonState == EasyButton.ButtonState.None)
				{
					this.buttonFingerIndex = gesture.fingerIndex;
					this.buttonState = EasyButton.ButtonState.Down;
					this.frame = 0;
					this.RaiseEvent(EasyButton.MessageName.On_ButtonDown);
				}
			}
			else if ((this.isSwipeIn || !this.isSwipeIn) && !this.isSwipeOut && this.buttonState == EasyButton.ButtonState.Press)
			{
				this.buttonFingerIndex = -1;
				this.currentTexture = this.normalTexture;
				this.currentColor = this.buttonNormalColor;
				this.buttonState = EasyButton.ButtonState.None;
			}
			else if (this.isSwipeOut && this.buttonState == EasyButton.ButtonState.Press)
			{
				this.RaiseEvent(EasyButton.MessageName.On_ButtonPress);
				this.buttonState = EasyButton.ButtonState.Press;
			}
		}
	}

	// Token: 0x0600001D RID: 29 RVA: 0x00002B74 File Offset: 0x00000D74
	private void On_TouchUp(Gesture gesture)
	{
		if (gesture.fingerIndex == this.buttonFingerIndex)
		{
			if ((EasyTouch.IsRectUnderTouch(VirtualScreen.GetRealRect(this.buttonRect), true) || (this.isSwipeOut && this.buttonState == EasyButton.ButtonState.Press)) && this.enable && this.isActivated)
			{
				this.RaiseEvent(EasyButton.MessageName.On_ButtonUp);
			}
			this.buttonState = EasyButton.ButtonState.Up;
			this.buttonFingerIndex = -1;
			this.currentTexture = this.normalTexture;
			this.currentColor = this.buttonNormalColor;
		}
	}

	// Token: 0x04000001 RID: 1
	public bool enable = true;

	// Token: 0x04000002 RID: 2
	public bool isActivated = true;

	// Token: 0x04000003 RID: 3
	public bool showDebugArea = true;

	// Token: 0x04000004 RID: 4
	public bool isUseGuiLayout = true;

	// Token: 0x04000005 RID: 5
	public EasyButton.ButtonState buttonState = EasyButton.ButtonState.None;

	// Token: 0x04000006 RID: 6
	[SerializeField]
	private EasyButton.ButtonAnchor anchor = EasyButton.ButtonAnchor.LowerRight;

	// Token: 0x04000007 RID: 7
	[SerializeField]
	private Vector2 offset = Vector2.zero;

	// Token: 0x04000008 RID: 8
	[SerializeField]
	private Vector2 scale = Vector2.one;

	// Token: 0x04000009 RID: 9
	public bool isSwipeIn;

	// Token: 0x0400000A RID: 10
	public bool isSwipeOut;

	// Token: 0x0400000B RID: 11
	public EasyButton.InteractionType interaction;

	// Token: 0x0400000C RID: 12
	public bool useBroadcast;

	// Token: 0x0400000D RID: 13
	public GameObject receiverGameObject;

	// Token: 0x0400000E RID: 14
	public EasyButton.Broadcast messageMode;

	// Token: 0x0400000F RID: 15
	public bool useSpecificalMethod;

	// Token: 0x04000010 RID: 16
	public string downMethodName;

	// Token: 0x04000011 RID: 17
	public string pressMethodName;

	// Token: 0x04000012 RID: 18
	public string upMethodName;

	// Token: 0x04000013 RID: 19
	public int guiDepth;

	// Token: 0x04000014 RID: 20
	[SerializeField]
	private Texture2D normalTexture;

	// Token: 0x04000015 RID: 21
	public Color buttonNormalColor = Color.white;

	// Token: 0x04000016 RID: 22
	[SerializeField]
	private Texture2D activeTexture;

	// Token: 0x04000017 RID: 23
	public Color buttonActiveColor = Color.white;

	// Token: 0x04000018 RID: 24
	public bool showInspectorProperties = true;

	// Token: 0x04000019 RID: 25
	public bool showInspectorPosition = true;

	// Token: 0x0400001A RID: 26
	public bool showInspectorEvent;

	// Token: 0x0400001B RID: 27
	public bool showInspectorTexture;

	// Token: 0x0400001C RID: 28
	private Rect buttonRect;

	// Token: 0x0400001D RID: 29
	private int buttonFingerIndex = -1;

	// Token: 0x0400001E RID: 30
	private Texture2D currentTexture;

	// Token: 0x0400001F RID: 31
	private Color currentColor;

	// Token: 0x04000020 RID: 32
	private int frame;

	// Token: 0x02000003 RID: 3
	public enum ButtonAnchor
	{
		// Token: 0x04000025 RID: 37
		UpperLeft,
		// Token: 0x04000026 RID: 38
		UpperCenter,
		// Token: 0x04000027 RID: 39
		UpperRight,
		// Token: 0x04000028 RID: 40
		MiddleLeft,
		// Token: 0x04000029 RID: 41
		MiddleCenter,
		// Token: 0x0400002A RID: 42
		MiddleRight,
		// Token: 0x0400002B RID: 43
		LowerLeft,
		// Token: 0x0400002C RID: 44
		LowerCenter,
		// Token: 0x0400002D RID: 45
		LowerRight
	}

	// Token: 0x02000004 RID: 4
	public enum Broadcast
	{
		// Token: 0x0400002F RID: 47
		SendMessage,
		// Token: 0x04000030 RID: 48
		SendMessageUpwards,
		// Token: 0x04000031 RID: 49
		BroadcastMessage
	}

	// Token: 0x02000005 RID: 5
	public enum ButtonState
	{
		// Token: 0x04000033 RID: 51
		Down,
		// Token: 0x04000034 RID: 52
		Press,
		// Token: 0x04000035 RID: 53
		Up,
		// Token: 0x04000036 RID: 54
		None
	}

	// Token: 0x02000006 RID: 6
	public enum InteractionType
	{
		// Token: 0x04000038 RID: 56
		Event,
		// Token: 0x04000039 RID: 57
		Include
	}

	// Token: 0x02000007 RID: 7
	private enum MessageName
	{
		// Token: 0x0400003B RID: 59
		On_ButtonDown,
		// Token: 0x0400003C RID: 60
		On_ButtonPress,
		// Token: 0x0400003D RID: 61
		On_ButtonUp
	}

	// Token: 0x020001B1 RID: 433
	// (Invoke) Token: 0x06000CE1 RID: 3297
	public delegate void ButtonUpHandler(string buttonName);

	// Token: 0x020001B2 RID: 434
	// (Invoke) Token: 0x06000CE5 RID: 3301
	public delegate void ButtonPressHandler(string buttonName);

	// Token: 0x020001B3 RID: 435
	// (Invoke) Token: 0x06000CE9 RID: 3305
	public delegate void ButtonDownHandler(string buttonName);
}
