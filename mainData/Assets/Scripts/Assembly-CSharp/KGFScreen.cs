using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000179 RID: 377
public class KGFScreen : MonoBehaviour
{
	// Token: 0x06000B3C RID: 2876 RVA: 0x00054750 File Offset: 0x00052950
	protected void Awake()
	{
		if (KGFScreen.itsInstance == null)
		{
			KGFScreen.itsInstance = this;
			KGFScreen.itsInstance.Init();
			return;
		}
		if (KGFScreen.itsInstance != this)
		{
			Debug.Log("there is more than one KFGDebug instance in this scene. please ensure there is always exactly one instance in this scene");
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06000B3D RID: 2877 RVA: 0x000547A8 File Offset: 0x000529A8
	public static KGFScreen GetInstance()
	{
		return KGFScreen.itsInstance;
	}

	// Token: 0x06000B3E RID: 2878 RVA: 0x000547B0 File Offset: 0x000529B0
	private static void SetResolution3D(int theWidth, int theHeight)
	{
		KGFScreen.SetResolution3D(theWidth, theHeight, 60);
	}

	// Token: 0x06000B3F RID: 2879 RVA: 0x000547BC File Offset: 0x000529BC
	public static Resolution GetResolution3D()
	{
		KGFScreen.CheckInstance();
		if (KGFScreen.itsInstance == null)
		{
			return default(Resolution);
		}
		return KGFScreen.itsInstance.itsDataModuleScreen.itsResolution3D;
	}

	// Token: 0x06000B40 RID: 2880 RVA: 0x000547F8 File Offset: 0x000529F8
	public static Resolution GetResolution2D()
	{
		KGFScreen.CheckInstance();
		if (KGFScreen.itsInstance == null)
		{
			return default(Resolution);
		}
		return KGFScreen.itsInstance.itsDataModuleScreen.itsResolution2D;
	}

	// Token: 0x06000B41 RID: 2881 RVA: 0x00054834 File Offset: 0x00052A34
	public static Resolution GetResolutionDisplay()
	{
		KGFScreen.CheckInstance();
		if (KGFScreen.itsInstance == null)
		{
			return default(Resolution);
		}
		return KGFScreen.itsInstance.itsDataModuleScreen.itsResolutionDisplay;
	}

	// Token: 0x06000B42 RID: 2882 RVA: 0x00054870 File Offset: 0x00052A70
	public static KGFScreen.eResolutionMode GetResolutionMode3D()
	{
		KGFScreen.CheckInstance();
		if (KGFScreen.itsInstance == null)
		{
			return KGFScreen.eResolutionMode.eNative;
		}
		return KGFScreen.itsInstance.itsDataModuleScreen.itsResolutionMode3D;
	}

	// Token: 0x06000B43 RID: 2883 RVA: 0x000548A4 File Offset: 0x00052AA4
	public static KGFScreen.eResolutionMode GetResolutionMode2D()
	{
		KGFScreen.CheckInstance();
		if (KGFScreen.itsInstance == null)
		{
			return KGFScreen.eResolutionMode.eNative;
		}
		return KGFScreen.itsInstance.itsDataModuleScreen.itsResolutionMode2D;
	}

	// Token: 0x06000B44 RID: 2884 RVA: 0x000548D8 File Offset: 0x00052AD8
	public static float GetAspect3D()
	{
		KGFScreen.CheckInstance();
		if (KGFScreen.itsInstance == null)
		{
			return 1f;
		}
		return KGFScreen.itsInstance.itsDataModuleScreen.itsAspect3D;
	}

	// Token: 0x06000B45 RID: 2885 RVA: 0x00054910 File Offset: 0x00052B10
	public static float GetAspect2D()
	{
		KGFScreen.CheckInstance();
		if (KGFScreen.itsInstance == null)
		{
			return 1f;
		}
		return KGFScreen.itsInstance.itsDataModuleScreen.itsAspect2D;
	}

	// Token: 0x06000B46 RID: 2886 RVA: 0x00054948 File Offset: 0x00052B48
	public static float GetScaleFactor3D()
	{
		KGFScreen.CheckInstance();
		if (KGFScreen.itsInstance == null)
		{
			return 1f;
		}
		return KGFScreen.itsInstance.itsDataModuleScreen.itsScaleFactor3D;
	}

	// Token: 0x06000B47 RID: 2887 RVA: 0x00054980 File Offset: 0x00052B80
	public static float GetScaleFactor2D()
	{
		KGFScreen.CheckInstance();
		if (KGFScreen.itsInstance == null)
		{
			return 1f;
		}
		return KGFScreen.itsInstance.itsDataModuleScreen.itsScaleFactor2D;
	}

	// Token: 0x06000B48 RID: 2888 RVA: 0x000549B8 File Offset: 0x00052BB8
	public static Vector3 GetConvertedEventCurrentMousePosition(Vector2 theEventCurrentMousePosition)
	{
		Vector3 vector = Input.mousePosition * KGFScreen.GetScaleFactor3D();
		Vector3 mousePosition = Input.mousePosition;
		float num = vector.x - mousePosition.x;
		float num2 = vector.y - mousePosition.y;
		num /= KGFScreen.GetScaleFactor3D();
		num2 /= KGFScreen.GetScaleFactor3D();
		Vector2 v = new Vector2(theEventCurrentMousePosition.x + num, theEventCurrentMousePosition.y - num2);
		return v;
	}

	// Token: 0x06000B49 RID: 2889 RVA: 0x00054A2C File Offset: 0x00052C2C
	public static Vector3 GetMousePositionDisplay()
	{
		KGFScreen.CheckInstance();
		if (KGFScreen.itsInstance == null)
		{
			return Vector3.zero;
		}
		float x = Input.mousePosition.x * KGFScreen.GetScaleFactor3D();
		float y = (float)Screen.height - Input.mousePosition.y * KGFScreen.GetScaleFactor3D();
		Vector3 result = new Vector3(x, y, Input.mousePosition.z);
		return result;
	}

	// Token: 0x06000B4A RID: 2890 RVA: 0x00054A9C File Offset: 0x00052C9C
	public static Vector3 GetMousePosition2D()
	{
		KGFScreen.CheckInstance();
		if (KGFScreen.itsInstance == null)
		{
			return Vector3.zero;
		}
		if (KGFScreen.GetResolutionMode3D() == KGFScreen.GetResolutionMode2D())
		{
			return Input.mousePosition;
		}
		if (KGFScreen.GetResolutionMode2D() == KGFScreen.eResolutionMode.eNative && KGFScreen.GetResolutionMode3D() == KGFScreen.eResolutionMode.eAutoAdjust)
		{
			return Input.mousePosition * KGFScreen.GetScaleFactor3D();
		}
		if (KGFScreen.GetResolutionMode2D() == KGFScreen.eResolutionMode.eAutoAdjust && KGFScreen.GetResolutionMode3D() == KGFScreen.eResolutionMode.eNative)
		{
			return Input.mousePosition / KGFScreen.GetScaleFactor2D();
		}
		return Input.mousePosition;
	}

	// Token: 0x06000B4B RID: 2891 RVA: 0x00054B28 File Offset: 0x00052D28
	public static Vector3 GetMousePositio3D()
	{
		KGFScreen.CheckInstance();
		if (KGFScreen.itsInstance == null)
		{
			return Vector3.zero;
		}
		return Input.mousePosition;
	}

	// Token: 0x06000B4C RID: 2892 RVA: 0x00054B58 File Offset: 0x00052D58
	public static Vector2 DisplayToScreen(Vector2 theDisplayPosition)
	{
		return theDisplayPosition / KGFScreen.GetScaleFactor3D();
	}

	// Token: 0x06000B4D RID: 2893 RVA: 0x00054B68 File Offset: 0x00052D68
	public static Vector2 DisplayToScreen2D(Vector2 theDisplayPosition)
	{
		return theDisplayPosition / KGFScreen.GetScaleFactor2D();
	}

	// Token: 0x06000B4E RID: 2894 RVA: 0x00054B78 File Offset: 0x00052D78
	public static Vector2 DisplayToScreenNormalized(Vector2 theDisplayPosition)
	{
		Vector2 result = new Vector2(0f, 0f);
		Vector2 vector = KGFScreen.DisplayToScreen(theDisplayPosition);
		result.x = vector.x / (float)KGFScreen.GetResolution3D().width;
		result.y = vector.y / (float)KGFScreen.GetResolution3D().height;
		return result;
	}

	// Token: 0x06000B4F RID: 2895 RVA: 0x00054BD8 File Offset: 0x00052DD8
	public static Rect DisplayToScreen(Rect theDisplayRect)
	{
		return new Rect(0f, 0f, 1f, 1f)
		{
			x = theDisplayRect.x / KGFScreen.GetScaleFactor3D(),
			y = theDisplayRect.y / KGFScreen.GetScaleFactor3D(),
			width = theDisplayRect.width / KGFScreen.GetScaleFactor3D(),
			height = theDisplayRect.height / KGFScreen.GetScaleFactor3D()
		};
	}

	// Token: 0x06000B50 RID: 2896 RVA: 0x00054C54 File Offset: 0x00052E54
	public static Rect DisplayToScreenNormalized(Rect theDisplayRect)
	{
		Rect result = new Rect(0f, 0f, 1f, 1f);
		Rect rect = KGFScreen.DisplayToScreen(theDisplayRect);
		result.x = rect.x / (float)KGFScreen.GetResolution3D().width;
		result.y = rect.y / (float)KGFScreen.GetResolution3D().height;
		result.width = rect.width / (float)KGFScreen.GetResolution3D().width;
		result.height = rect.height / (float)KGFScreen.GetResolution3D().height;
		return result;
	}

	// Token: 0x06000B51 RID: 2897 RVA: 0x00054CFC File Offset: 0x00052EFC
	public static Rect NormalizedTo2DScreen(Rect theDisplayRect)
	{
		return new Rect(0f, 0f, 1f, 1f)
		{
			x = (float)KGFScreen.GetResolution2D().width * theDisplayRect.x,
			y = (float)KGFScreen.GetResolution2D().height * theDisplayRect.y,
			width = (float)KGFScreen.GetResolution2D().width * theDisplayRect.width,
			height = (float)KGFScreen.GetResolution2D().height * theDisplayRect.height
		};
	}

	// Token: 0x06000B52 RID: 2898 RVA: 0x00054D9C File Offset: 0x00052F9C
	public static RenderTexture GetRenderTexture()
	{
		KGFScreen.CheckInstance();
		if (KGFScreen.itsInstance == null)
		{
			return null;
		}
		KGFScreen.itsInstance.CreateCamera();
		return KGFScreen.itsInstance.itsRenderTexture;
	}

	// Token: 0x06000B53 RID: 2899 RVA: 0x00054DCC File Offset: 0x00052FCC
	public static void BlitToScreen()
	{
		KGFScreen.CheckInstance();
		if (KGFScreen.itsInstance == null)
		{
			return;
		}
		if (KGFScreen.itsInstance.itsRenderTexture != null)
		{
			Graphics.Blit(KGFScreen.itsInstance.itsRenderTexture, null);
		}
	}

	// Token: 0x06000B54 RID: 2900 RVA: 0x00054E0C File Offset: 0x0005300C
	private void Update()
	{
	}

	// Token: 0x06000B55 RID: 2901 RVA: 0x00054E10 File Offset: 0x00053010
	private void CorrectMousePosition()
	{
	}

	// Token: 0x06000B56 RID: 2902 RVA: 0x00054E14 File Offset: 0x00053014
	private void CreateCamera()
	{
		if (this.itsCamera != null)
		{
			return;
		}
		base.gameObject.AddComponent<Camera>();
		this.itsCamera = base.gameObject.GetComponent<Camera>();
		this.itsCamera.clearFlags = CameraClearFlags.Color;
		this.itsCamera.backgroundColor = Color.black;
		this.itsCamera.cullingMask = 0;
		this.itsCamera.orthographic = true;
		this.itsCamera.orthographicSize = 1f;
		this.itsCamera.depth = 100f;
		this.itsCamera.farClipPlane = 1f;
		this.itsCamera.nearClipPlane = 0.5f;
	}

	// Token: 0x06000B57 RID: 2903 RVA: 0x00054EC4 File Offset: 0x000530C4
	private static void SetResolution3D(int theWidth, int theHeight, int theRefreshRate)
	{
		KGFScreen.CheckInstance();
		if (KGFScreen.itsInstance == null)
		{
			return;
		}
		KGFScreen.itsInstance.itsDataModuleScreen.itsResolution3D.width = theWidth;
		KGFScreen.itsInstance.itsDataModuleScreen.itsResolution3D.height = theHeight;
		KGFScreen.itsInstance.itsDataModuleScreen.itsResolution3D.refreshRate = theRefreshRate;
		KGFScreen.itsInstance.itsDataModuleScreen.itsAspect3D = KGFScreen.ReadAspect(theWidth, theHeight);
		KGFScreen.itsInstance.itsDataModuleScreen.itsScaleFactor3D = (float)KGFScreen.GetResolutionDisplay().width / (float)theWidth;
		Debug.Log(string.Concat(new object[]
		{
			"KGFScreen: set resolution 3D to: ",
			theWidth,
			"/",
			theHeight,
			"/",
			theRefreshRate
		}));
		KGFScreen.itsInstance.CreateRenderTexture();
	}

	// Token: 0x06000B58 RID: 2904 RVA: 0x00054FAC File Offset: 0x000531AC
	private static void SetResolution2D(int theWidth, int theHeight)
	{
		KGFScreen.CheckInstance();
		if (KGFScreen.itsInstance == null)
		{
			return;
		}
		KGFScreen.itsInstance.itsDataModuleScreen.itsResolution2D.width = theWidth;
		KGFScreen.itsInstance.itsDataModuleScreen.itsResolution2D.height = theHeight;
		KGFScreen.itsInstance.itsDataModuleScreen.itsResolution2D.refreshRate = 0;
		KGFScreen.itsInstance.itsDataModuleScreen.itsAspect2D = KGFScreen.ReadAspect(theWidth, theHeight);
		KGFScreen.itsInstance.itsDataModuleScreen.itsScaleFactor2D = ((float)KGFScreen.GetResolutionDisplay().width + 1f) / (float)theWidth;
		Debug.Log(string.Concat(new object[]
		{
			"KGFScreen: set resolution 2D to: ",
			theWidth,
			"/",
			theHeight
		}));
	}

	// Token: 0x06000B59 RID: 2905 RVA: 0x00055080 File Offset: 0x00053280
	private static void UpdateMouseRect()
	{
		Rect windowRect = KGFUtility.GetWindowRect();
		KGFUtility.SetMouseRect(new Rect(windowRect.x, windowRect.y + windowRect.height - (float)KGFScreen.GetResolution2D().height, (float)KGFScreen.GetResolution2D().width, (float)KGFScreen.GetResolution2D().height));
		MonoBehaviour.print("new rect:" + windowRect);
	}

	// Token: 0x06000B5A RID: 2906 RVA: 0x000550F4 File Offset: 0x000532F4
	private static void CheckInstance()
	{
		if (KGFScreen.itsInstance == null)
		{
			UnityEngine.Object @object = UnityEngine.Object.FindObjectOfType(typeof(KGFScreen));
			if (@object != null)
			{
				KGFScreen.itsInstance = (@object as KGFScreen);
				KGFScreen.itsInstance.Init();
			}
			else if (!KGFScreen.itsAlreadyChecked)
			{
				Debug.LogError("KGFScreen is not running. Make sure that there is an instance of the KGFScreen prefab in the current scene.");
				KGFScreen.itsAlreadyChecked = true;
			}
		}
	}

	// Token: 0x06000B5B RID: 2907 RVA: 0x00055164 File Offset: 0x00053364
	private void Init()
	{
		Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, false);
		base.StartCoroutine(this.SetResolutionDelayed());
	}

	// Token: 0x06000B5C RID: 2908 RVA: 0x000551A0 File Offset: 0x000533A0
	private IEnumerator SetResolutionDelayed()
	{
		yield return new WaitForSeconds(1f);
		this.ReadResolutionDisplay();
		Debug.Log(string.Concat(new object[]
		{
			"display resolution set to: ",
			KGFScreen.GetResolutionDisplay().width,
			"/",
			KGFScreen.GetResolutionDisplay().height
		}));
		float anAspect = KGFScreen.ReadAspect(KGFScreen.GetResolutionDisplay().width, KGFScreen.GetResolutionDisplay().height);
		int aHeight = this.itsDataModuleScreen.itsMinHeight;
		int aWidth = (int)((float)aHeight * anAspect);
		if (aWidth < this.itsDataModuleScreen.itsMinWidth)
		{
			aWidth = this.itsDataModuleScreen.itsMinWidth;
			aHeight = (int)((float)this.itsDataModuleScreen.itsMinWidth / anAspect);
		}
		KGFScreen.eResolutionMode aResolutionMode3D = KGFScreen.GetResolutionMode3D();
		if (aResolutionMode3D == KGFScreen.eResolutionMode.eNative)
		{
			KGFScreen.SetResolution3D(KGFScreen.GetResolutionDisplay().width, KGFScreen.GetResolutionDisplay().height);
		}
		else if (aResolutionMode3D == KGFScreen.eResolutionMode.eAutoAdjust)
		{
			KGFScreen.SetResolution3D(aWidth, aHeight);
		}
		if (this.itsDataModuleScreen.itsResolutionMode2D == KGFScreen.eResolutionMode.eNative)
		{
			KGFScreen.SetResolution2D(KGFScreen.GetResolutionDisplay().width, KGFScreen.GetResolutionDisplay().height);
		}
		else if (this.itsDataModuleScreen.itsResolutionMode2D == KGFScreen.eResolutionMode.eAutoAdjust)
		{
			KGFScreen.SetResolution2D(aWidth, aHeight);
		}
		yield break;
	}

	// Token: 0x06000B5D RID: 2909 RVA: 0x000551BC File Offset: 0x000533BC
	private void ReadResolutionDisplay()
	{
		KGFScreen.itsInstance.itsDataModuleScreen.itsResolutionDisplay = default(Resolution);
		KGFScreen.itsInstance.itsDataModuleScreen.itsResolutionDisplay.width = Screen.width;
		KGFScreen.itsInstance.itsDataModuleScreen.itsResolutionDisplay.height = Screen.height;
		KGFScreen.itsInstance.itsDataModuleScreen.itsResolutionDisplay.refreshRate = 60;
		KGFScreen.itsInstance.itsDataModuleScreen.itsAspectDisplay = KGFScreen.ReadAspect(Screen.width, Screen.height);
	}

	// Token: 0x06000B5E RID: 2910 RVA: 0x00055248 File Offset: 0x00053448
	private static float ReadAspect(int theWidth, int theHeight)
	{
		return (float)theWidth / (float)theHeight;
	}

	// Token: 0x06000B5F RID: 2911 RVA: 0x00055250 File Offset: 0x00053450
	private void CreateRenderTexture()
	{
		if (this.itsRenderTexture == null)
		{
			this.itsRenderTexture = new RenderTexture(KGFScreen.GetResolution3D().width, KGFScreen.GetResolution3D().height, 16, RenderTextureFormat.ARGB32);
		}
		else if (this.itsRenderTexture.width != KGFScreen.GetResolution3D().width)
		{
			this.itsRenderTexture.Release();
			this.itsRenderTexture = new RenderTexture(KGFScreen.GetResolution3D().width, KGFScreen.GetResolution3D().height, 16, RenderTextureFormat.ARGB32);
		}
		this.itsRenderTexture.isPowerOfTwo = true;
		this.itsRenderTexture.name = "KGFScreenRenderTexture";
		this.itsRenderTexture.Create();
	}

	// Token: 0x06000B60 RID: 2912 RVA: 0x00055314 File Offset: 0x00053514
	private void OnPostRender()
	{
		KGFScreen.BlitToScreen();
	}

	// Token: 0x04000B69 RID: 2921
	private const string itsSettingsSection = "screen";

	// Token: 0x04000B6A RID: 2922
	private const string itsSettingsNameWidth = "resolution.width";

	// Token: 0x04000B6B RID: 2923
	private const string itsSettingsNameHeight = "resolution.height";

	// Token: 0x04000B6C RID: 2924
	private const string itsSettingsNameRefreshRate = "refreshrate";

	// Token: 0x04000B6D RID: 2925
	private const string itsSettingsNameIsFulscreen = "fullscreen";

	// Token: 0x04000B6E RID: 2926
	private static KGFScreen itsInstance;

	// Token: 0x04000B6F RID: 2927
	private static bool itsAlreadyChecked;

	// Token: 0x04000B70 RID: 2928
	private RenderTexture itsRenderTexture;

	// Token: 0x04000B71 RID: 2929
	private Camera itsCamera;

	// Token: 0x04000B72 RID: 2930
	public KGFScreen.KGFDataScreen itsDataModuleScreen = new KGFScreen.KGFDataScreen();

	// Token: 0x0200017A RID: 378
	public enum eResolutionMode
	{
		// Token: 0x04000B74 RID: 2932
		eNative,
		// Token: 0x04000B75 RID: 2933
		eAutoAdjust
	}

	// Token: 0x0200017B RID: 379
	[Serializable]
	public class KGFDataScreen
	{
		// Token: 0x04000B76 RID: 2934
		public KGFScreen.eResolutionMode itsResolutionMode3D = KGFScreen.eResolutionMode.eAutoAdjust;

		// Token: 0x04000B77 RID: 2935
		public KGFScreen.eResolutionMode itsResolutionMode2D = KGFScreen.eResolutionMode.eAutoAdjust;

		// Token: 0x04000B78 RID: 2936
		[HideInInspector]
		public Resolution itsResolution3D;

		// Token: 0x04000B79 RID: 2937
		[HideInInspector]
		public Resolution itsResolution2D;

		// Token: 0x04000B7A RID: 2938
		[HideInInspector]
		public Resolution itsResolutionDisplay;

		// Token: 0x04000B7B RID: 2939
		[HideInInspector]
		public float itsAspect3D = 1f;

		// Token: 0x04000B7C RID: 2940
		[HideInInspector]
		public float itsAspect2D = 1f;

		// Token: 0x04000B7D RID: 2941
		[HideInInspector]
		public float itsAspectDisplay = 1f;

		// Token: 0x04000B7E RID: 2942
		[HideInInspector]
		public float itsScaleFactor3D = 1f;

		// Token: 0x04000B7F RID: 2943
		[HideInInspector]
		public float itsScaleFactor2D = 1f;

		// Token: 0x04000B80 RID: 2944
		public int itsMinWidth = 480;

		// Token: 0x04000B81 RID: 2945
		public int itsMinHeight = 320;
	}
}
