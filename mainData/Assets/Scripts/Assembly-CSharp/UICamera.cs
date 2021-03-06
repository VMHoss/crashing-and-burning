using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000078 RID: 120
[RequireComponent(typeof(Camera))]
[AddComponentMenu("NGUI/UI/Camera")]
[ExecuteInEditMode]
public class UICamera : MonoBehaviour
{
	// Token: 0x17000084 RID: 132
	// (get) Token: 0x060003BE RID: 958 RVA: 0x00018394 File Offset: 0x00016594
	private bool handlesEvents
	{
		get
		{
			return UICamera.eventHandler == this;
		}
	}

	// Token: 0x17000085 RID: 133
	// (get) Token: 0x060003BF RID: 959 RVA: 0x000183A4 File Offset: 0x000165A4
	public Camera cachedCamera
	{
		get
		{
			if (this.mCam == null)
			{
				this.mCam = base.camera;
			}
			return this.mCam;
		}
	}

	// Token: 0x17000086 RID: 134
	// (get) Token: 0x060003C0 RID: 960 RVA: 0x000183CC File Offset: 0x000165CC
	// (set) Token: 0x060003C1 RID: 961 RVA: 0x000183D4 File Offset: 0x000165D4
	public static GameObject selectedObject
	{
		get
		{
			return UICamera.mSel;
		}
		set
		{
			if (UICamera.mSel != value)
			{
				if (UICamera.mSel != null)
				{
					UICamera uicamera = UICamera.FindCameraForLayer(UICamera.mSel.layer);
					if (uicamera != null)
					{
						UICamera.current = uicamera;
						UICamera.currentCamera = uicamera.mCam;
						UICamera.Notify(UICamera.mSel, "OnSelect", false);
						if (uicamera.useController || uicamera.useKeyboard)
						{
							UICamera.Highlight(UICamera.mSel, false);
						}
						UICamera.current = null;
					}
				}
				UICamera.mSel = value;
				if (UICamera.mSel != null)
				{
					UICamera uicamera2 = UICamera.FindCameraForLayer(UICamera.mSel.layer);
					if (uicamera2 != null)
					{
						UICamera.current = uicamera2;
						UICamera.currentCamera = uicamera2.mCam;
						if (uicamera2.useController || uicamera2.useKeyboard)
						{
							UICamera.Highlight(UICamera.mSel, true);
						}
						UICamera.Notify(UICamera.mSel, "OnSelect", true);
						UICamera.current = null;
					}
				}
			}
		}
	}

	// Token: 0x17000087 RID: 135
	// (get) Token: 0x060003C2 RID: 962 RVA: 0x000184EC File Offset: 0x000166EC
	public static int touchCount
	{
		get
		{
			int num = UICamera.mTouches.Count;
			for (int i = 0; i < UICamera.mMouse.Length; i++)
			{
				if (UICamera.mMouse[i].pressed != null)
				{
					num++;
				}
			}
			if (UICamera.mController.pressed != null)
			{
				num++;
			}
			return num;
		}
	}

	// Token: 0x060003C3 RID: 963 RVA: 0x00018554 File Offset: 0x00016754
	private void OnApplicationQuit()
	{
		UICamera.mHighlighted.Clear();
	}

	// Token: 0x17000088 RID: 136
	// (get) Token: 0x060003C4 RID: 964 RVA: 0x00018560 File Offset: 0x00016760
	public static Camera mainCamera
	{
		get
		{
			UICamera eventHandler = UICamera.eventHandler;
			return (!(eventHandler != null)) ? null : eventHandler.cachedCamera;
		}
	}

	// Token: 0x17000089 RID: 137
	// (get) Token: 0x060003C5 RID: 965 RVA: 0x0001858C File Offset: 0x0001678C
	public static UICamera eventHandler
	{
		get
		{
			for (int i = 0; i < UICamera.mList.Count; i++)
			{
				UICamera uicamera = UICamera.mList[i];
				if (!(uicamera == null) && uicamera.enabled && NGUITools.GetActive(uicamera.gameObject))
				{
					return uicamera;
				}
			}
			return null;
		}
	}

	// Token: 0x060003C6 RID: 966 RVA: 0x000185F0 File Offset: 0x000167F0
	private static int CompareFunc(UICamera a, UICamera b)
	{
		if (a.cachedCamera.depth < b.cachedCamera.depth)
		{
			return 1;
		}
		if (a.cachedCamera.depth > b.cachedCamera.depth)
		{
			return -1;
		}
		return 0;
	}

	// Token: 0x060003C7 RID: 967 RVA: 0x00018638 File Offset: 0x00016838
	public static bool Raycast(Vector3 inPos, ref RaycastHit hit)
	{
		for (int i = 0; i < UICamera.mList.Count; i++)
		{
			UICamera uicamera = UICamera.mList[i];
			if (uicamera.enabled && NGUITools.GetActive(uicamera.gameObject))
			{
				UICamera.currentCamera = uicamera.cachedCamera;
				Vector3 vector = UICamera.currentCamera.ScreenToViewportPoint(inPos);
				if (vector.x >= 0f && vector.x <= 1f && vector.y >= 0f && vector.y <= 1f)
				{
					Ray ray = UICamera.currentCamera.ScreenPointToRay(inPos);
					int layerMask = UICamera.currentCamera.cullingMask & uicamera.eventReceiverMask;
					float distance = (uicamera.rangeDistance <= 0f) ? (UICamera.currentCamera.farClipPlane - UICamera.currentCamera.nearClipPlane) : uicamera.rangeDistance;
					if (uicamera.clipRaycasts)
					{
						RaycastHit[] array = Physics.RaycastAll(ray, distance, layerMask);
						if (array.Length > 1)
						{
							Array.Sort<RaycastHit>(array, (RaycastHit r1, RaycastHit r2) => r1.distance.CompareTo(r2.distance));
							int j = 0;
							int num = array.Length;
							while (j < num)
							{
								if (UICamera.IsVisible(ref array[j]))
								{
									hit = array[j];
									return true;
								}
								j++;
							}
						}
						else if (array.Length == 1 && UICamera.IsVisible(ref array[0]))
						{
							hit = array[0];
							return true;
						}
					}
					else if (Physics.Raycast(ray, out hit, distance, layerMask))
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	// Token: 0x060003C8 RID: 968 RVA: 0x0001881C File Offset: 0x00016A1C
	private static bool IsVisible(ref RaycastHit hit)
	{
		UIPanel uipanel = NGUITools.FindInParents<UIPanel>(hit.collider.gameObject);
		return uipanel == null || uipanel.IsVisible(hit.point);
	}

	// Token: 0x060003C9 RID: 969 RVA: 0x0001885C File Offset: 0x00016A5C
	public static UICamera FindCameraForLayer(int layer)
	{
		int num = 1 << layer;
		for (int i = 0; i < UICamera.mList.Count; i++)
		{
			UICamera uicamera = UICamera.mList[i];
			Camera cachedCamera = uicamera.cachedCamera;
			if (cachedCamera != null && (cachedCamera.cullingMask & num) != 0)
			{
				return uicamera;
			}
		}
		return null;
	}

	// Token: 0x060003CA RID: 970 RVA: 0x000188BC File Offset: 0x00016ABC
	private static int GetDirection(KeyCode up, KeyCode down)
	{
		if (Input.GetKeyDown(up))
		{
			return 1;
		}
		if (Input.GetKeyDown(down))
		{
			return -1;
		}
		return 0;
	}

	// Token: 0x060003CB RID: 971 RVA: 0x000188DC File Offset: 0x00016ADC
	private static int GetDirection(KeyCode up0, KeyCode up1, KeyCode down0, KeyCode down1)
	{
		if (Input.GetKeyDown(up0) || Input.GetKeyDown(up1))
		{
			return 1;
		}
		if (Input.GetKeyDown(down0) || Input.GetKeyDown(down1))
		{
			return -1;
		}
		return 0;
	}

	// Token: 0x060003CC RID: 972 RVA: 0x00018910 File Offset: 0x00016B10
	private static int GetDirection(string axis)
	{
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		if (UICamera.mNextEvent < realtimeSinceStartup)
		{
			float axis2 = Input.GetAxis(axis);
			if (axis2 > 0.75f)
			{
				UICamera.mNextEvent = realtimeSinceStartup + 0.25f;
				return 1;
			}
			if (axis2 < -0.75f)
			{
				UICamera.mNextEvent = realtimeSinceStartup + 0.25f;
				return -1;
			}
		}
		return 0;
	}

	// Token: 0x060003CD RID: 973 RVA: 0x00018968 File Offset: 0x00016B68
	public static bool IsHighlighted(GameObject go)
	{
		int i = UICamera.mHighlighted.Count;
		while (i > 0)
		{
			UICamera.Highlighted highlighted = UICamera.mHighlighted[--i];
			if (highlighted.go == go)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060003CE RID: 974 RVA: 0x000189B0 File Offset: 0x00016BB0
	private static void Highlight(GameObject go, bool highlighted)
	{
		if (go != null)
		{
			int i = UICamera.mHighlighted.Count;
			while (i > 0)
			{
				UICamera.Highlighted highlighted2 = UICamera.mHighlighted[--i];
				if (highlighted2 == null || highlighted2.go == null)
				{
					UICamera.mHighlighted.RemoveAt(i);
				}
				else if (highlighted2.go == go)
				{
					if (highlighted)
					{
						highlighted2.counter++;
					}
					else if (--highlighted2.counter < 1)
					{
						UICamera.mHighlighted.Remove(highlighted2);
						UICamera.Notify(go, "OnHover", false);
					}
					return;
				}
			}
			if (highlighted)
			{
				UICamera.Highlighted highlighted3 = new UICamera.Highlighted();
				highlighted3.go = go;
				highlighted3.counter = 1;
				UICamera.mHighlighted.Add(highlighted3);
				UICamera.Notify(go, "OnHover", true);
			}
		}
	}

	// Token: 0x060003CF RID: 975 RVA: 0x00018AAC File Offset: 0x00016CAC
	public static void Notify(GameObject go, string funcName, object obj)
	{
		if (go != null)
		{
			go.SendMessage(funcName, obj, SendMessageOptions.DontRequireReceiver);
			if (UICamera.genericEventHandler != null && UICamera.genericEventHandler != go)
			{
				UICamera.genericEventHandler.SendMessage(funcName, obj, SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	// Token: 0x060003D0 RID: 976 RVA: 0x00018AFC File Offset: 0x00016CFC
	public static UICamera.MouseOrTouch GetTouch(int id)
	{
		UICamera.MouseOrTouch mouseOrTouch = null;
		if (!UICamera.mTouches.TryGetValue(id, out mouseOrTouch))
		{
			mouseOrTouch = new UICamera.MouseOrTouch();
			mouseOrTouch.touchBegan = true;
			UICamera.mTouches.Add(id, mouseOrTouch);
		}
		return mouseOrTouch;
	}

	// Token: 0x060003D1 RID: 977 RVA: 0x00018B38 File Offset: 0x00016D38
	public static void RemoveTouch(int id)
	{
		UICamera.mTouches.Remove(id);
	}

	// Token: 0x060003D2 RID: 978 RVA: 0x00018B48 File Offset: 0x00016D48
	private void Awake()
	{
		this.cachedCamera.eventMask = 0;
		if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
		{
			this.useMouse = false;
			this.useTouch = true;
			if (Application.platform == RuntimePlatform.IPhonePlayer)
			{
				this.useKeyboard = false;
				this.useController = false;
			}
		}
		else if (Application.platform == RuntimePlatform.PS3 || Application.platform == RuntimePlatform.XBOX360)
		{
			this.useMouse = false;
			this.useTouch = false;
			this.useKeyboard = false;
			this.useController = true;
		}
		else if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor)
		{
			this.mIsEditor = true;
		}
		UICamera.mMouse[0].pos.x = Input.mousePosition.x;
		UICamera.mMouse[0].pos.y = Input.mousePosition.y;
		UICamera.lastTouchPosition = UICamera.mMouse[0].pos;
		UICamera.mList.Add(this);
		UICamera.mList.Sort(new Comparison<UICamera>(UICamera.CompareFunc));
		if (this.eventReceiverMask == -1)
		{
			this.eventReceiverMask = this.cachedCamera.cullingMask;
		}
	}

	// Token: 0x060003D3 RID: 979 RVA: 0x00018C90 File Offset: 0x00016E90
	private void OnDestroy()
	{
		UICamera.mList.Remove(this);
	}

	// Token: 0x060003D4 RID: 980 RVA: 0x00018CA0 File Offset: 0x00016EA0
	private void FixedUpdate()
	{
		if (this.useMouse && Application.isPlaying && this.handlesEvents)
		{
			UICamera.hoveredObject = ((!UICamera.Raycast(Input.mousePosition, ref UICamera.lastHit)) ? UICamera.fallThrough : UICamera.lastHit.collider.gameObject);
			if (UICamera.hoveredObject == null)
			{
				UICamera.hoveredObject = UICamera.genericEventHandler;
			}
			for (int i = 0; i < 3; i++)
			{
				UICamera.mMouse[i].current = UICamera.hoveredObject;
			}
		}
	}

	// Token: 0x060003D5 RID: 981 RVA: 0x00018D3C File Offset: 0x00016F3C
	private void Update()
	{
		if (!Application.isPlaying || !this.handlesEvents)
		{
			return;
		}
		UICamera.current = this;
		if (this.useMouse || (this.useTouch && this.mIsEditor))
		{
			this.ProcessMouse();
		}
		if (this.useTouch)
		{
			this.ProcessTouches();
		}
		if (UICamera.onCustomInput != null)
		{
			UICamera.onCustomInput();
		}
		if (this.useMouse && UICamera.mSel != null && ((this.cancelKey0 != KeyCode.None && Input.GetKeyDown(this.cancelKey0)) || (this.cancelKey1 != KeyCode.None && Input.GetKeyDown(this.cancelKey1))))
		{
			UICamera.selectedObject = null;
		}
		if (UICamera.mSel != null)
		{
			string text = Input.inputString;
			if (this.useKeyboard && Input.GetKeyDown(KeyCode.Delete))
			{
				text += "\b";
			}
			if (text.Length > 0)
			{
				if (!this.stickyTooltip && this.mTooltip != null)
				{
					this.ShowTooltip(false);
				}
				UICamera.Notify(UICamera.mSel, "OnInput", text);
			}
		}
		else
		{
			UICamera.inputHasFocus = false;
		}
		if (UICamera.mSel != null)
		{
			this.ProcessOthers();
		}
		if (this.useMouse && UICamera.mHover != null)
		{
			float axis = Input.GetAxis(this.scrollAxisName);
			if (axis != 0f)
			{
				UICamera.Notify(UICamera.mHover, "OnScroll", axis);
			}
			if (UICamera.showTooltips && this.mTooltipTime != 0f && (this.mTooltipTime < Time.realtimeSinceStartup || Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
			{
				this.mTooltip = UICamera.mHover;
				this.ShowTooltip(true);
			}
		}
		UICamera.current = null;
	}

	// Token: 0x060003D6 RID: 982 RVA: 0x00018F48 File Offset: 0x00017148
	public void ProcessMouse()
	{
		bool flag = this.useMouse && Time.timeScale < 0.9f;
		if (!flag)
		{
			for (int i = 0; i < 3; i++)
			{
				if (Input.GetMouseButton(i) || Input.GetMouseButtonUp(i))
				{
					flag = true;
					break;
				}
			}
		}
		UICamera.mMouse[0].pos = Input.mousePosition;
		UICamera.mMouse[0].delta = UICamera.mMouse[0].pos - UICamera.lastTouchPosition;
		bool flag2 = UICamera.mMouse[0].pos != UICamera.lastTouchPosition;
		UICamera.lastTouchPosition = UICamera.mMouse[0].pos;
		if (flag)
		{
			UICamera.hoveredObject = ((!UICamera.Raycast(Input.mousePosition, ref UICamera.lastHit)) ? UICamera.fallThrough : UICamera.lastHit.collider.gameObject);
			if (UICamera.hoveredObject == null)
			{
				UICamera.hoveredObject = UICamera.genericEventHandler;
			}
			UICamera.mMouse[0].current = UICamera.hoveredObject;
		}
		for (int j = 1; j < 3; j++)
		{
			UICamera.mMouse[j].pos = UICamera.mMouse[0].pos;
			UICamera.mMouse[j].delta = UICamera.mMouse[0].delta;
			UICamera.mMouse[j].current = UICamera.mMouse[0].current;
		}
		bool flag3 = false;
		for (int k = 0; k < 3; k++)
		{
			if (Input.GetMouseButton(k))
			{
				flag3 = true;
				break;
			}
		}
		if (flag3)
		{
			this.mTooltipTime = 0f;
		}
		else if (this.useMouse && flag2 && (!this.stickyTooltip || UICamera.mHover != UICamera.mMouse[0].current))
		{
			if (this.mTooltipTime != 0f)
			{
				this.mTooltipTime = Time.realtimeSinceStartup + this.tooltipDelay;
			}
			else if (this.mTooltip != null)
			{
				this.ShowTooltip(false);
			}
		}
		if (this.useMouse && !flag3 && UICamera.mHover != null && UICamera.mHover != UICamera.mMouse[0].current)
		{
			if (this.mTooltip != null)
			{
				this.ShowTooltip(false);
			}
			UICamera.Highlight(UICamera.mHover, false);
			UICamera.mHover = null;
		}
		if (this.useMouse)
		{
			for (int l = 0; l < 3; l++)
			{
				bool mouseButtonDown = Input.GetMouseButtonDown(l);
				bool mouseButtonUp = Input.GetMouseButtonUp(l);
				UICamera.currentTouch = UICamera.mMouse[l];
				UICamera.currentTouchID = -1 - l;
				if (mouseButtonDown)
				{
					UICamera.currentTouch.pressedCam = UICamera.currentCamera;
				}
				else if (UICamera.currentTouch.pressed != null)
				{
					UICamera.currentCamera = UICamera.currentTouch.pressedCam;
				}
				this.ProcessTouch(mouseButtonDown, mouseButtonUp);
			}
			UICamera.currentTouch = null;
		}
		if (this.useMouse && !flag3 && UICamera.mHover != UICamera.mMouse[0].current)
		{
			this.mTooltipTime = Time.realtimeSinceStartup + this.tooltipDelay;
			UICamera.mHover = UICamera.mMouse[0].current;
			UICamera.Highlight(UICamera.mHover, true);
		}
	}

	// Token: 0x060003D7 RID: 983 RVA: 0x000192D8 File Offset: 0x000174D8
	public void ProcessTouches()
	{
		for (int i = 0; i < Input.touchCount; i++)
		{
			Touch touch = Input.GetTouch(i);
			UICamera.currentTouchID = ((!this.allowMultiTouch) ? 1 : touch.fingerId);
			UICamera.currentTouch = UICamera.GetTouch(UICamera.currentTouchID);
			bool flag = touch.phase == TouchPhase.Began || UICamera.currentTouch.touchBegan;
			bool flag2 = touch.phase == TouchPhase.Canceled || touch.phase == TouchPhase.Ended;
			UICamera.currentTouch.touchBegan = false;
			if (flag)
			{
				UICamera.currentTouch.delta = Vector2.zero;
			}
			else
			{
				UICamera.currentTouch.delta = touch.position - UICamera.currentTouch.pos;
			}
			UICamera.currentTouch.pos = touch.position;
			UICamera.hoveredObject = ((!UICamera.Raycast(UICamera.currentTouch.pos, ref UICamera.lastHit)) ? UICamera.fallThrough : UICamera.lastHit.collider.gameObject);
			if (UICamera.hoveredObject == null)
			{
				UICamera.hoveredObject = UICamera.genericEventHandler;
			}
			UICamera.currentTouch.current = UICamera.hoveredObject;
			UICamera.lastTouchPosition = UICamera.currentTouch.pos;
			if (flag)
			{
				UICamera.currentTouch.pressedCam = UICamera.currentCamera;
			}
			else if (UICamera.currentTouch.pressed != null)
			{
				UICamera.currentCamera = UICamera.currentTouch.pressedCam;
			}
			if (touch.tapCount > 1)
			{
				UICamera.currentTouch.clickTime = Time.realtimeSinceStartup;
			}
			this.ProcessTouch(flag, flag2);
			if (flag2)
			{
				UICamera.RemoveTouch(UICamera.currentTouchID);
			}
			UICamera.currentTouch = null;
			if (!this.allowMultiTouch)
			{
				break;
			}
		}
	}

	// Token: 0x060003D8 RID: 984 RVA: 0x000194B8 File Offset: 0x000176B8
	public void ProcessOthers()
	{
		UICamera.currentTouchID = -100;
		UICamera.currentTouch = UICamera.mController;
		UICamera.inputHasFocus = (UICamera.mSel != null && UICamera.mSel.GetComponent<UIInput>() != null);
		bool flag = (this.submitKey0 != KeyCode.None && Input.GetKeyDown(this.submitKey0)) || (this.submitKey1 != KeyCode.None && Input.GetKeyDown(this.submitKey1));
		bool flag2 = (this.submitKey0 != KeyCode.None && Input.GetKeyUp(this.submitKey0)) || (this.submitKey1 != KeyCode.None && Input.GetKeyUp(this.submitKey1));
		if (flag || flag2)
		{
			UICamera.currentTouch.current = UICamera.mSel;
			this.ProcessTouch(flag, flag2);
			UICamera.currentTouch.current = null;
		}
		int num = 0;
		int num2 = 0;
		if (this.useKeyboard)
		{
			if (UICamera.inputHasFocus)
			{
				num += UICamera.GetDirection(KeyCode.UpArrow, KeyCode.DownArrow);
				num2 += UICamera.GetDirection(KeyCode.RightArrow, KeyCode.LeftArrow);
			}
			else
			{
				num += UICamera.GetDirection(KeyCode.W, KeyCode.UpArrow, KeyCode.S, KeyCode.DownArrow);
				num2 += UICamera.GetDirection(KeyCode.D, KeyCode.RightArrow, KeyCode.A, KeyCode.LeftArrow);
			}
		}
		if (this.useController)
		{
			if (!string.IsNullOrEmpty(this.verticalAxisName))
			{
				num += UICamera.GetDirection(this.verticalAxisName);
			}
			if (!string.IsNullOrEmpty(this.horizontalAxisName))
			{
				num2 += UICamera.GetDirection(this.horizontalAxisName);
			}
		}
		if (num != 0)
		{
			UICamera.Notify(UICamera.mSel, "OnKey", (num <= 0) ? KeyCode.DownArrow : KeyCode.UpArrow);
		}
		if (num2 != 0)
		{
			UICamera.Notify(UICamera.mSel, "OnKey", (num2 <= 0) ? KeyCode.LeftArrow : KeyCode.RightArrow);
		}
		if (this.useKeyboard && Input.GetKeyDown(KeyCode.Tab))
		{
			UICamera.Notify(UICamera.mSel, "OnKey", KeyCode.Tab);
		}
		if (this.cancelKey0 != KeyCode.None && Input.GetKeyDown(this.cancelKey0))
		{
			UICamera.Notify(UICamera.mSel, "OnKey", KeyCode.Escape);
		}
		if (this.cancelKey1 != KeyCode.None && Input.GetKeyDown(this.cancelKey1))
		{
			UICamera.Notify(UICamera.mSel, "OnKey", KeyCode.Escape);
		}
		UICamera.currentTouch = null;
	}

	// Token: 0x060003D9 RID: 985 RVA: 0x00019748 File Offset: 0x00017948
	public void ProcessTouch(bool pressed, bool unpressed)
	{
		bool flag = UICamera.currentTouch == UICamera.mMouse[0];
		float num = (!flag) ? this.touchDragThreshold : this.mouseDragThreshold;
		float num2 = (!flag) ? this.touchClickThreshold : this.mouseClickThreshold;
		if (pressed)
		{
			if (this.mTooltip != null)
			{
				this.ShowTooltip(false);
			}
			UICamera.currentTouch.pressStarted = true;
			UICamera.Notify(UICamera.currentTouch.pressed, "OnPress", false);
			UICamera.currentTouch.pressed = UICamera.currentTouch.current;
			UICamera.currentTouch.dragged = UICamera.currentTouch.current;
			UICamera.currentTouch.clickNotification = UICamera.ClickNotification.Always;
			UICamera.currentTouch.totalDelta = Vector2.zero;
			UICamera.currentTouch.dragStarted = false;
			UICamera.Notify(UICamera.currentTouch.pressed, "OnPress", true);
			if (UICamera.currentTouch.pressed != UICamera.mSel)
			{
				if (this.mTooltip != null)
				{
					this.ShowTooltip(false);
				}
				UICamera.selectedObject = null;
			}
		}
		else
		{
			if (!this.stickyPress && !unpressed && UICamera.currentTouch.pressStarted && UICamera.currentTouch.pressed != UICamera.hoveredObject)
			{
				UICamera.isDragging = true;
				UICamera.Notify(UICamera.currentTouch.pressed, "OnPress", false);
				UICamera.currentTouch.pressed = UICamera.hoveredObject;
				UICamera.Notify(UICamera.currentTouch.pressed, "OnPress", true);
				UICamera.isDragging = false;
			}
			if (UICamera.currentTouch.pressed != null)
			{
				float magnitude = UICamera.currentTouch.delta.magnitude;
				if (magnitude != 0f)
				{
					UICamera.currentTouch.totalDelta += UICamera.currentTouch.delta;
					magnitude = UICamera.currentTouch.totalDelta.magnitude;
					if (!UICamera.currentTouch.dragStarted && num < magnitude)
					{
						UICamera.currentTouch.dragStarted = true;
						UICamera.currentTouch.delta = UICamera.currentTouch.totalDelta;
					}
					if (UICamera.currentTouch.dragStarted)
					{
						if (this.mTooltip != null)
						{
							this.ShowTooltip(false);
						}
						UICamera.isDragging = true;
						bool flag2 = UICamera.currentTouch.clickNotification == UICamera.ClickNotification.None;
						UICamera.Notify(UICamera.currentTouch.dragged, "OnDrag", UICamera.currentTouch.delta);
						UICamera.isDragging = false;
						if (flag2)
						{
							UICamera.currentTouch.clickNotification = UICamera.ClickNotification.None;
						}
						else if (UICamera.currentTouch.clickNotification == UICamera.ClickNotification.BasedOnDelta && num2 < magnitude)
						{
							UICamera.currentTouch.clickNotification = UICamera.ClickNotification.None;
						}
					}
				}
			}
		}
		if (unpressed)
		{
			UICamera.currentTouch.pressStarted = false;
			if (this.mTooltip != null)
			{
				this.ShowTooltip(false);
			}
			if (UICamera.currentTouch.pressed != null)
			{
				UICamera.Notify(UICamera.currentTouch.pressed, "OnPress", false);
				if (this.useMouse && UICamera.currentTouch.pressed == UICamera.mHover)
				{
					UICamera.Notify(UICamera.currentTouch.pressed, "OnHover", true);
				}
				if (UICamera.currentTouch.dragged == UICamera.currentTouch.current || (UICamera.currentTouch.clickNotification != UICamera.ClickNotification.None && UICamera.currentTouch.totalDelta.magnitude < num))
				{
					if (UICamera.currentTouch.pressed != UICamera.mSel)
					{
						UICamera.mSel = UICamera.currentTouch.pressed;
						UICamera.Notify(UICamera.currentTouch.pressed, "OnSelect", true);
					}
					else
					{
						UICamera.mSel = UICamera.currentTouch.pressed;
					}
					if (UICamera.currentTouch.clickNotification != UICamera.ClickNotification.None)
					{
						float realtimeSinceStartup = Time.realtimeSinceStartup;
						UICamera.Notify(UICamera.currentTouch.pressed, "OnClick", null);
						if (UICamera.currentTouch.clickTime + 0.35f > realtimeSinceStartup)
						{
							UICamera.Notify(UICamera.currentTouch.pressed, "OnDoubleClick", null);
						}
						UICamera.currentTouch.clickTime = realtimeSinceStartup;
					}
				}
				else
				{
					UICamera.Notify(UICamera.currentTouch.current, "OnDrop", UICamera.currentTouch.dragged);
				}
			}
			UICamera.currentTouch.dragStarted = false;
			UICamera.currentTouch.pressed = null;
			UICamera.currentTouch.dragged = null;
		}
	}

	// Token: 0x060003DA RID: 986 RVA: 0x00019C0C File Offset: 0x00017E0C
	public void ShowTooltip(bool val)
	{
		this.mTooltipTime = 0f;
		UICamera.Notify(this.mTooltip, "OnTooltip", val);
		if (!val)
		{
			this.mTooltip = null;
		}
	}

	// Token: 0x040003D9 RID: 985
	public bool debug;

	// Token: 0x040003DA RID: 986
	public bool useMouse = true;

	// Token: 0x040003DB RID: 987
	public bool useTouch = true;

	// Token: 0x040003DC RID: 988
	public bool allowMultiTouch = true;

	// Token: 0x040003DD RID: 989
	public bool useKeyboard = true;

	// Token: 0x040003DE RID: 990
	public bool useController = true;

	// Token: 0x040003DF RID: 991
	public bool stickyPress = true;

	// Token: 0x040003E0 RID: 992
	public LayerMask eventReceiverMask = -1;

	// Token: 0x040003E1 RID: 993
	public bool clipRaycasts = true;

	// Token: 0x040003E2 RID: 994
	public float tooltipDelay = 1f;

	// Token: 0x040003E3 RID: 995
	public bool stickyTooltip = true;

	// Token: 0x040003E4 RID: 996
	public float mouseDragThreshold = 4f;

	// Token: 0x040003E5 RID: 997
	public float mouseClickThreshold = 10f;

	// Token: 0x040003E6 RID: 998
	public float touchDragThreshold = 40f;

	// Token: 0x040003E7 RID: 999
	public float touchClickThreshold = 40f;

	// Token: 0x040003E8 RID: 1000
	public float rangeDistance = -1f;

	// Token: 0x040003E9 RID: 1001
	public string scrollAxisName = "Mouse ScrollWheel";

	// Token: 0x040003EA RID: 1002
	public string verticalAxisName = "Vertical";

	// Token: 0x040003EB RID: 1003
	public string horizontalAxisName = "Horizontal";

	// Token: 0x040003EC RID: 1004
	public KeyCode submitKey0 = KeyCode.Return;

	// Token: 0x040003ED RID: 1005
	public KeyCode submitKey1 = KeyCode.JoystickButton0;

	// Token: 0x040003EE RID: 1006
	public KeyCode cancelKey0 = KeyCode.Escape;

	// Token: 0x040003EF RID: 1007
	public KeyCode cancelKey1 = KeyCode.JoystickButton1;

	// Token: 0x040003F0 RID: 1008
	public static UICamera.OnCustomInput onCustomInput;

	// Token: 0x040003F1 RID: 1009
	public static bool showTooltips = true;

	// Token: 0x040003F2 RID: 1010
	public static Vector2 lastTouchPosition = Vector2.zero;

	// Token: 0x040003F3 RID: 1011
	public static RaycastHit lastHit;

	// Token: 0x040003F4 RID: 1012
	public static UICamera current = null;

	// Token: 0x040003F5 RID: 1013
	public static Camera currentCamera = null;

	// Token: 0x040003F6 RID: 1014
	public static int currentTouchID = -1;

	// Token: 0x040003F7 RID: 1015
	public static UICamera.MouseOrTouch currentTouch = null;

	// Token: 0x040003F8 RID: 1016
	public static bool inputHasFocus = false;

	// Token: 0x040003F9 RID: 1017
	public static GameObject genericEventHandler;

	// Token: 0x040003FA RID: 1018
	public static GameObject fallThrough;

	// Token: 0x040003FB RID: 1019
	private static List<UICamera> mList = new List<UICamera>();

	// Token: 0x040003FC RID: 1020
	private static List<UICamera.Highlighted> mHighlighted = new List<UICamera.Highlighted>();

	// Token: 0x040003FD RID: 1021
	private static GameObject mSel = null;

	// Token: 0x040003FE RID: 1022
	private static UICamera.MouseOrTouch[] mMouse = new UICamera.MouseOrTouch[]
	{
		new UICamera.MouseOrTouch(),
		new UICamera.MouseOrTouch(),
		new UICamera.MouseOrTouch()
	};

	// Token: 0x040003FF RID: 1023
	private static GameObject mHover;

	// Token: 0x04000400 RID: 1024
	private static UICamera.MouseOrTouch mController = new UICamera.MouseOrTouch();

	// Token: 0x04000401 RID: 1025
	private static float mNextEvent = 0f;

	// Token: 0x04000402 RID: 1026
	private static Dictionary<int, UICamera.MouseOrTouch> mTouches = new Dictionary<int, UICamera.MouseOrTouch>();

	// Token: 0x04000403 RID: 1027
	private GameObject mTooltip;

	// Token: 0x04000404 RID: 1028
	private Camera mCam;

	// Token: 0x04000405 RID: 1029
	private LayerMask mLayerMask;

	// Token: 0x04000406 RID: 1030
	private float mTooltipTime;

	// Token: 0x04000407 RID: 1031
	private bool mIsEditor;

	// Token: 0x04000408 RID: 1032
	public static bool isDragging = false;

	// Token: 0x04000409 RID: 1033
	public static GameObject hoveredObject;

	// Token: 0x02000079 RID: 121
	public enum ClickNotification
	{
		// Token: 0x0400040C RID: 1036
		None,
		// Token: 0x0400040D RID: 1037
		Always,
		// Token: 0x0400040E RID: 1038
		BasedOnDelta
	}

	// Token: 0x0200007A RID: 122
	public class MouseOrTouch
	{
		// Token: 0x0400040F RID: 1039
		public Vector2 pos;

		// Token: 0x04000410 RID: 1040
		public Vector2 delta;

		// Token: 0x04000411 RID: 1041
		public Vector2 totalDelta;

		// Token: 0x04000412 RID: 1042
		public Camera pressedCam;

		// Token: 0x04000413 RID: 1043
		public GameObject current;

		// Token: 0x04000414 RID: 1044
		public GameObject pressed;

		// Token: 0x04000415 RID: 1045
		public GameObject dragged;

		// Token: 0x04000416 RID: 1046
		public float clickTime;

		// Token: 0x04000417 RID: 1047
		public UICamera.ClickNotification clickNotification = UICamera.ClickNotification.Always;

		// Token: 0x04000418 RID: 1048
		public bool touchBegan = true;

		// Token: 0x04000419 RID: 1049
		public bool pressStarted;

		// Token: 0x0400041A RID: 1050
		public bool dragStarted;
	}

	// Token: 0x0200007B RID: 123
	private class Highlighted
	{
		// Token: 0x0400041B RID: 1051
		public GameObject go;

		// Token: 0x0400041C RID: 1052
		public int counter;
	}

	// Token: 0x020001F2 RID: 498
	// (Invoke) Token: 0x06000DE5 RID: 3557
	public delegate void OnCustomInput();
}
