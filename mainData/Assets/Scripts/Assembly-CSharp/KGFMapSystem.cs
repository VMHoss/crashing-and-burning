using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200019B RID: 411
public class KGFMapSystem : KGFModule, KGFICustomGUI, KGFIValidator
{
	// Token: 0x06000C0D RID: 3085 RVA: 0x000576AC File Offset: 0x000558AC
	public KGFMapSystem() : base(new Version(1, 10, 0, 0), new Version(1, 2, 0, 0))
	{
	}

	// Token: 0x06000C0E RID: 3086 RVA: 0x00057804 File Offset: 0x00055A04
	public static void KGFSetChildrenActiveRecursively(GameObject theGameObject, bool theActive)
	{
		if (theGameObject == null)
		{
			return;
		}
		theGameObject.SetActiveRecursively(theActive);
	}

	// Token: 0x06000C0F RID: 3087 RVA: 0x0005781C File Offset: 0x00055A1C
	public static bool KGFGetActive(GameObject theGameObject)
	{
		return theGameObject.active;
	}

	// Token: 0x06000C10 RID: 3088 RVA: 0x00057824 File Offset: 0x00055A24
	private Vector3 ChangeVectorHeight(Vector3 theVector, float theHeight)
	{
		KGFMapSystem.KGFMapSystemOrientation itsOrientation = this.itsDataModuleMinimap.itsGlobalSettings.itsOrientation;
		if (itsOrientation != KGFMapSystem.KGFMapSystemOrientation.XYSideScroller)
		{
			if (itsOrientation == KGFMapSystem.KGFMapSystemOrientation.XZDefault)
			{
				theVector.y = theHeight;
			}
		}
		else
		{
			theVector.z = theHeight;
		}
		return theVector;
	}

	// Token: 0x06000C11 RID: 3089 RVA: 0x00057870 File Offset: 0x00055A70
	private Vector3 ChangeVectorPlane(Vector3 theVector, float theI, float theJ)
	{
		KGFMapSystem.KGFMapSystemOrientation itsOrientation = this.itsDataModuleMinimap.itsGlobalSettings.itsOrientation;
		if (itsOrientation != KGFMapSystem.KGFMapSystemOrientation.XYSideScroller)
		{
			if (itsOrientation == KGFMapSystem.KGFMapSystemOrientation.XZDefault)
			{
				theVector.x = theI;
				theVector.z = theJ;
			}
		}
		else
		{
			theVector.x = theI;
			theVector.y = theJ;
		}
		return theVector;
	}

	// Token: 0x06000C12 RID: 3090 RVA: 0x000578CC File Offset: 0x00055ACC
	private Vector3 CreateVector(float theI, float theJ, float theHeight)
	{
		return this.ChangeVectorPlane(this.ChangeVectorHeight(Vector3.zero, theHeight), theI, theJ);
	}

	// Token: 0x06000C13 RID: 3091 RVA: 0x000578E4 File Offset: 0x00055AE4
	private float GetVector3Height(Vector3 theVector)
	{
		KGFMapSystem.KGFMapSystemOrientation itsOrientation = this.itsDataModuleMinimap.itsGlobalSettings.itsOrientation;
		if (itsOrientation == KGFMapSystem.KGFMapSystemOrientation.XYSideScroller)
		{
			return theVector.z;
		}
		if (itsOrientation != KGFMapSystem.KGFMapSystemOrientation.XZDefault)
		{
			return 0f;
		}
		return theVector.y;
	}

	// Token: 0x06000C14 RID: 3092 RVA: 0x0005792C File Offset: 0x00055B2C
	private Vector2 GetVector3Plane(Vector3 theVector)
	{
		KGFMapSystem.KGFMapSystemOrientation itsOrientation = this.itsDataModuleMinimap.itsGlobalSettings.itsOrientation;
		if (itsOrientation == KGFMapSystem.KGFMapSystemOrientation.XYSideScroller)
		{
			return new Vector2(theVector.x, theVector.y);
		}
		if (itsOrientation != KGFMapSystem.KGFMapSystemOrientation.XZDefault)
		{
			return Vector2.zero;
		}
		return new Vector2(theVector.x, theVector.z);
	}

	// Token: 0x06000C15 RID: 3093 RVA: 0x0005798C File Offset: 0x00055B8C
	private float GetTerrainHeight(float theAddHeight)
	{
		if (this.itsDataModuleMinimap.itsGlobalSettings.itsOrientation != KGFMapSystem.KGFMapSystemOrientation.XZDefault)
		{
			return this.itsTerrainBoundsPhoto.max.z + 1f - theAddHeight;
		}
		if (this.itsDataModuleMinimap.itsPhoto.itsTakePhoto)
		{
			return this.itsTerrainBoundsPhoto.min.y - 1f + theAddHeight;
		}
		return this.itsTerrainBoundsPhoto.max.y + 1f + theAddHeight;
	}

	// Token: 0x06000C16 RID: 3094 RVA: 0x00057A18 File Offset: 0x00055C18
	private float GetHeightFog()
	{
		if (this.itsDataModuleMinimap.itsFogOfWar.itsHideMapIcons)
		{
			return this.GetTerrainHeight(0.5f);
		}
		return this.GetTerrainHeight(0.2f);
	}

	// Token: 0x06000C17 RID: 3095 RVA: 0x00057A54 File Offset: 0x00055C54
	private float GetHeightIcons()
	{
		return this.GetTerrainHeight(0.3f);
	}

	// Token: 0x06000C18 RID: 3096 RVA: 0x00057A64 File Offset: 0x00055C64
	private float GetHeightArrows()
	{
		return this.GetTerrainHeight(0.9f);
	}

	// Token: 0x06000C19 RID: 3097 RVA: 0x00057A74 File Offset: 0x00055C74
	private float GetHeightViewPort()
	{
		return this.GetTerrainHeight(0.1f);
	}

	// Token: 0x06000C1A RID: 3098 RVA: 0x00057A84 File Offset: 0x00055C84
	private float GetHeightFlags()
	{
		return this.GetTerrainHeight(0.35f);
	}

	// Token: 0x06000C1B RID: 3099 RVA: 0x00057A94 File Offset: 0x00055C94
	private float GetHeightPhoto()
	{
		return this.GetTerrainHeight(0f);
	}

	// Token: 0x06000C1C RID: 3100 RVA: 0x00057AA4 File Offset: 0x00055CA4
	protected override void KGFAwake()
	{
		this.UpdateStyles();
		this.itsLayerMinimap = LayerMask.NameToLayer("mapsystem");
		if (this.Validate().itsHasErrors)
		{
			this.itsErrorMode = true;
			return;
		}
		this.CreateCameras();
		this.CreateRenderTexture();
		this.itsContainerFlags = new GameObject("flags").transform;
		this.itsContainerFlags.parent = base.transform;
		this.itsContainerUser = new GameObject("user").transform;
		this.itsContainerUser.parent = base.transform;
		this.itsContainerIcons = new GameObject("icons").transform;
		this.itsContainerIcons.parent = base.transform;
		this.itsContainerIconArrows = new GameObject("arrows").transform;
		this.itsContainerIconArrows.parent = base.transform;
		foreach (KGFIMapIcon theMapIcon in KGFAccessor.GetObjects<KGFIMapIcon>())
		{
			this.RegisterIcon(theMapIcon);
		}
		this.SetTarget(this.itsDataModuleMinimap.itsGlobalSettings.itsTarget);
		KGFAccessor.RegisterAddEvent<KGFIMapIcon>(new Action<object, EventArgs>(this.OnMapIconAdd));
		KGFAccessor.RegisterRemoveEvent<KGFIMapIcon>(new Action<object, EventArgs>(this.OnMapIconRemove));
	}

	// Token: 0x06000C1D RID: 3101 RVA: 0x00057C14 File Offset: 0x00055E14
	private bool GetHasProVersion()
	{
		return SystemInfo.supportsRenderTextures;
	}

	// Token: 0x06000C1E RID: 3102 RVA: 0x00057C1C File Offset: 0x00055E1C
	public bool GetHover()
	{
		Vector2 point = Input.mousePosition;
		point.y = (float)Screen.height - point.y;
		return this.itsTargetRect.Contains(point) || this.itsRectZoomIn.Contains(point) || this.itsRectZoomOut.Contains(point) || this.itsRectStatic.Contains(point) || this.itsRectFullscreen.Contains(point);
	}

	// Token: 0x06000C1F RID: 3103 RVA: 0x00057CAC File Offset: 0x00055EAC
	public bool GetHoverWithoutButtons()
	{
		Vector2 point = Input.mousePosition;
		point.y = (float)Screen.height - point.y;
		return !this.itsRectZoomIn.Contains(point) && !this.itsRectZoomOut.Contains(point) && !this.itsRectStatic.Contains(point) && !this.itsRectFullscreen.Contains(point) && this.itsTargetRect.Contains(point);
	}

	// Token: 0x06000C20 RID: 3104 RVA: 0x00057D3C File Offset: 0x00055F3C
	private IEnumerator DeferedPhoto()
	{
		yield return new WaitForSeconds(0.1f);
		this.AutoCreatePhoto();
		yield break;
	}

	// Token: 0x06000C21 RID: 3105 RVA: 0x00057D58 File Offset: 0x00055F58
	private void Start()
	{
		if (this.itsErrorMode)
		{
			return;
		}
		this.MeasureScene();
		if (this.itsDataModuleMinimap.itsPhoto.itsTakePhoto)
		{
			base.StartCoroutine(this.DeferedPhoto());
		}
		if (this.itsDataModuleMinimap.itsShaders.itsShaderFogOfWar == null)
		{
			this.LogWarning("itsDataModuleMinimap.itsShaders.itsShaderFogOfWar is not assigned. Please install the standard unity particle package. Assign the Particle Alpha Blend Shader to itsDataModuleMinimap.itsShaders.itsShaderFogOfWar.", base.name, this);
		}
		else if (this.itsDataModuleMinimap.itsFogOfWar.itsActive)
		{
			this.InitFogOfWar();
		}
		if (this.GetHasProVersion())
		{
			this.itsMinimapPlane = this.GenerateMinimapPlane();
			MeshRenderer component = this.itsMinimapPlane.GetComponent<MeshRenderer>();
			if (component == null)
			{
				this.LogError("Cannot find meshrenderer", base.name, this);
			}
			else
			{
				component.material.SetTexture("_MainTex", this.itsRendertexture);
			}
		}
		this.SetViewportEnabled(this.itsDataModuleMinimap.itsViewport.itsActive);
		this.SetMinimapEnabled(this.itsDataModuleMinimap.itsGlobalSettings.itsIsActive);
	}

	// Token: 0x06000C22 RID: 3106 RVA: 0x00057E70 File Offset: 0x00056070
	private float GetWidth()
	{
		if (this.GetFullscreen())
		{
			return (float)Screen.width;
		}
		return this.GetHeight();
	}

	// Token: 0x06000C23 RID: 3107 RVA: 0x00057E8C File Offset: 0x0005608C
	private float GetHeight()
	{
		if (this.GetFullscreen())
		{
			return (float)Screen.height;
		}
		return this.itsDataModuleMinimap.itsAppearanceMiniMap.itsSize * (float)Screen.height;
	}

	// Token: 0x06000C24 RID: 3108 RVA: 0x00057EB8 File Offset: 0x000560B8
	private float GetButtonSize()
	{
		if (this.GetFullscreen())
		{
			return this.itsDataModuleMinimap.itsAppearanceMap.itsButtonSize * this.GetWidth();
		}
		return this.itsDataModuleMinimap.itsAppearanceMiniMap.itsButtonSize * this.GetWidth();
	}

	// Token: 0x06000C25 RID: 3109 RVA: 0x00057F00 File Offset: 0x00056100
	private float GetButtonPadding()
	{
		if (this.GetFullscreen())
		{
			return this.itsDataModuleMinimap.itsAppearanceMap.itsButtonPadding * this.GetWidth();
		}
		return this.itsDataModuleMinimap.itsAppearanceMiniMap.itsButtonPadding * this.GetWidth();
	}

	// Token: 0x06000C26 RID: 3110 RVA: 0x00057F48 File Offset: 0x00056148
	private void LogError(string theError, string theCategory, MonoBehaviour theObject)
	{
		if (!this.itsDataModuleMinimap.itsGlobalSettings.itsEnableLogMessages)
		{
			return;
		}
		Debug.LogError(string.Format("{0} - {1}", theCategory, theError));
	}

	// Token: 0x06000C27 RID: 3111 RVA: 0x00057F74 File Offset: 0x00056174
	private void LogWarning(string theWarning, string theCategory, MonoBehaviour theObject)
	{
		if (!this.itsDataModuleMinimap.itsGlobalSettings.itsEnableLogMessages)
		{
			return;
		}
		Debug.LogWarning(string.Format("{0} - {1}", theCategory, theWarning));
	}

	// Token: 0x06000C28 RID: 3112 RVA: 0x00057FA0 File Offset: 0x000561A0
	private void LogInfo(string theError, string theCategory, MonoBehaviour theObject)
	{
		if (!this.itsDataModuleMinimap.itsGlobalSettings.itsEnableLogMessages)
		{
			return;
		}
		Debug.Log(string.Format("{0} - {1}", theCategory, theError));
	}

	// Token: 0x06000C29 RID: 3113 RVA: 0x00057FCC File Offset: 0x000561CC
	private Mesh GeneratePlaneMeshXZ()
	{
		Mesh mesh = new Mesh();
		Vector3[] vertices = new Vector3[]
		{
			new Vector3(0f, 0f, 0f),
			new Vector3(1f, 0f, 0f),
			new Vector3(1f, 0f, 1f),
			new Vector3(0f, 0f, 1f)
		};
		Vector3[] normals = new Vector3[]
		{
			new Vector3(0f, 1f, 0f),
			new Vector3(0f, 1f, 0f),
			new Vector3(0f, 1f, 0f),
			new Vector3(0f, 1f, 0f)
		};
		Vector2[] uv = new Vector2[]
		{
			new Vector2(0f, 0f),
			new Vector2(1f, 0f),
			new Vector2(1f, 1f),
			new Vector2(0f, 1f)
		};
		int[] triangles = new int[]
		{
			0,
			3,
			2,
			0,
			2,
			1
		};
		mesh.vertices = vertices;
		mesh.normals = normals;
		mesh.uv = uv;
		mesh.triangles = triangles;
		return mesh;
	}

	// Token: 0x06000C2A RID: 3114 RVA: 0x000581A4 File Offset: 0x000563A4
	private static Mesh GeneratePlaneMeshXZCentered()
	{
		Mesh mesh = new Mesh();
		Vector3[] vertices = new Vector3[]
		{
			new Vector3(-0.5f, 0f, -0.5f),
			new Vector3(0.5f, 0f, -0.5f),
			new Vector3(0.5f, 0f, 0.5f),
			new Vector3(-0.5f, 0f, 0.5f)
		};
		Vector3[] normals = new Vector3[]
		{
			new Vector3(0f, 1f, 0f),
			new Vector3(0f, 1f, 0f),
			new Vector3(0f, 1f, 0f),
			new Vector3(0f, 1f, 0f)
		};
		Vector2[] uv = new Vector2[]
		{
			new Vector2(0f, 0f),
			new Vector2(1f, 0f),
			new Vector2(1f, 1f),
			new Vector2(0f, 1f)
		};
		int[] triangles = new int[]
		{
			0,
			3,
			2,
			0,
			2,
			1
		};
		mesh.vertices = vertices;
		mesh.normals = normals;
		mesh.uv = uv;
		mesh.triangles = triangles;
		return mesh;
	}

	// Token: 0x06000C2B RID: 3115 RVA: 0x0005837C File Offset: 0x0005657C
	private Mesh CreatePlaneMesh(int theWidth, int theHeight)
	{
		Mesh mesh = new Mesh();
		Vector3[] array = new Vector3[(theWidth + 1) * (theHeight + 1)];
		for (int i = 0; i <= theHeight; i++)
		{
			for (int j = 0; j <= theWidth; j++)
			{
				array[i * (theWidth + 1) + j] = new Vector3((float)j, 0f, (float)i);
			}
		}
		Vector3[] array2 = new Vector3[array.Length];
		for (int k = 0; k < array2.Length; k++)
		{
			array2[k] = new Vector3(0f, 1f, 0f);
		}
		int[] array3 = new int[array.Length * 2 * 3];
		int num = 0;
		for (int l = 0; l < theHeight; l++)
		{
			for (int m = 0; m < theWidth; m++)
			{
				int num2 = l * (theWidth + 1) + m;
				if (num2 % 2 == 0)
				{
					array3[num++] = num2;
					array3[num++] = num2 + (theWidth + 2);
					array3[num++] = num2 + (theWidth + 1);
					array3[num++] = num2;
					array3[num++] = num2 + 1;
					array3[num++] = num2 + theWidth + 2;
				}
				else
				{
					array3[num++] = num2;
					array3[num++] = num2 + 1;
					array3[num++] = num2 + (theWidth + 1);
					array3[num++] = num2 + 1;
					array3[num++] = num2 + theWidth + 2;
					array3[num++] = num2 + (theWidth + 1);
				}
			}
		}
		Vector2[] array4 = new Vector2[array.Length];
		for (int n = 0; n < array4.Length; n++)
		{
			array4[n] = Vector2.zero;
		}
		mesh.vertices = array;
		mesh.normals = array2;
		mesh.uv = array4;
		mesh.triangles = array3;
		return mesh;
	}

	// Token: 0x06000C2C RID: 3116 RVA: 0x00058580 File Offset: 0x00056780
	private string SerializeFogOfWar()
	{
		if (this.itsMeshFilterFogOfWarPlane != null && this.itsMeshFilterFogOfWarPlane.mesh.colors != null)
		{
			string[] array = new string[this.itsMeshFilterFogOfWarPlane.mesh.vertices.Length];
			for (int i = 0; i < this.itsMeshFilterFogOfWarPlane.mesh.vertices.Length; i++)
			{
				array[i] = string.Empty + this.itsMeshFilterFogOfWarPlane.mesh.colors[i].a;
			}
			return string.Join(";", array);
		}
		return null;
	}

	// Token: 0x06000C2D RID: 3117 RVA: 0x0005862C File Offset: 0x0005682C
	private void Save(string theSaveGameName)
	{
		string text = this.SerializeFogOfWar();
		if (text != null)
		{
			PlayerPrefs.SetString(theSaveGameName + "minimap_FogOfWar_values", text);
		}
	}

	// Token: 0x06000C2E RID: 3118 RVA: 0x00058658 File Offset: 0x00056858
	private void DeserializeFogOfWar(string theSavedString)
	{
		if (theSavedString != null)
		{
			if (this.itsMeshFilterFogOfWarPlane != null && this.itsMeshFilterFogOfWarPlane.mesh.colors != null)
			{
				Color[] colors = this.itsMeshFilterFogOfWarPlane.mesh.colors;
				string[] array = theSavedString.Split(new char[]
				{
					';'
				});
				if (array.Length == colors.Length)
				{
					for (int i = 0; i < array.Length; i++)
					{
						try
						{
							colors[i].a = float.Parse(array[i]);
						}
						catch
						{
							this.LogError("Could not parse saved fog of war", base.name, this);
							return;
						}
					}
					this.itsMeshFilterFogOfWarPlane.mesh.colors = colors;
				}
				else
				{
					this.LogError("Saved fog of war size different from current.", base.name, this);
				}
			}
		}
		else
		{
			this.LogError("No saved fog of war to load.", base.name, this);
		}
	}

	// Token: 0x06000C2F RID: 3119 RVA: 0x00058764 File Offset: 0x00056964
	private void Load(string theSaveGameName)
	{
		string @string = PlayerPrefs.GetString(theSaveGameName + "minimap_FogOfWar_values", null);
		this.DeserializeFogOfWar(@string);
	}

	// Token: 0x06000C30 RID: 3120 RVA: 0x0005878C File Offset: 0x0005698C
	private void RevealFogOfWar()
	{
		if (this.itsMeshFilterFogOfWarPlane == null)
		{
			return;
		}
		if (this.itsMeshFilterFogOfWarPlane.mesh == null)
		{
			return;
		}
		Color[] colors = this.itsMeshFilterFogOfWarPlane.mesh.colors;
		for (int i = 0; i < colors.Length; i++)
		{
			colors[i].a = 0f;
		}
		this.itsMeshFilterFogOfWarPlane.mesh.colors = colors;
	}

	// Token: 0x06000C31 RID: 3121 RVA: 0x0005880C File Offset: 0x00056A0C
	private GameObject CreateFogOfWarPlane()
	{
		GameObject gameObject = new GameObject("fog_of_war");
		gameObject.transform.parent = base.transform;
		gameObject.layer = this.itsLayerMinimap;
		MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
		meshFilter.mesh = this.CreatePlaneMesh(this.itsDataModuleMinimap.itsFogOfWar.itsResolutionX, this.itsDataModuleMinimap.itsFogOfWar.itsResolutionY);
		MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
		meshRenderer.material = new Material(this.itsDataModuleMinimap.itsShaders.itsShaderFogOfWar);
		return gameObject;
	}

	// Token: 0x06000C32 RID: 3122 RVA: 0x00058898 File Offset: 0x00056A98
	public void InitFogOfWar()
	{
		this.itsScalingFogOfWar = new Vector2(this.itsSizeTerrain.x / (float)this.itsDataModuleMinimap.itsFogOfWar.itsResolutionX, this.itsSizeTerrain.y / (float)this.itsDataModuleMinimap.itsFogOfWar.itsResolutionY);
		if (this.itsMeshFilterFogOfWarPlane != null)
		{
			UnityEngine.Object.Destroy(this.itsMeshFilterFogOfWarPlane.gameObject);
		}
		this.itsMeshFilterFogOfWarPlane = this.CreateFogOfWarPlane().GetComponent<MeshFilter>();
		Vector3 vector = this.itsTerrainBoundsPhoto.center - this.itsTerrainBoundsPhoto.extents;
		vector = this.ChangeVectorHeight(vector, this.GetHeightFog());
		if (this.itsDataModuleMinimap.itsGlobalSettings.itsOrientation == KGFMapSystem.KGFMapSystemOrientation.XYSideScroller)
		{
			this.itsMeshFilterFogOfWarPlane.transform.eulerAngles = new Vector3(270f, 0f, 0f);
		}
		this.itsMeshFilterFogOfWarPlane.transform.position = vector;
		this.itsMeshFilterFogOfWarPlane.transform.localScale = new Vector3(this.itsScalingFogOfWar.x, 1f, this.itsScalingFogOfWar.y);
		Color[] array = this.itsMeshFilterFogOfWarPlane.mesh.colors;
		array = new Color[this.itsMeshFilterFogOfWarPlane.mesh.vertexCount];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = this.itsDataModuleMinimap.itsGlobalSettings.itsColorBackground;
		}
		this.itsMeshFilterFogOfWarPlane.mesh.colors = array;
		this.itsFOWColors = array;
		this.itsFOWVertices = this.itsMeshFilterFogOfWarPlane.mesh.vertices;
	}

	// Token: 0x06000C33 RID: 3123 RVA: 0x00058A48 File Offset: 0x00056C48
	public void RevealFogOfWarAtPoint(Vector3 thePosition)
	{
		if (this.itsMeshFilterFogOfWarPlane == null)
		{
			return;
		}
		Vector2 vector3Plane = this.GetVector3Plane(thePosition - this.itsMeshFilterFogOfWarPlane.transform.position);
		Vector2 vector = new Vector2((float)Mathf.RoundToInt(vector3Plane.x / this.itsSizeTerrain.x * (float)this.itsDataModuleMinimap.itsFogOfWar.itsResolutionX), (float)Mathf.RoundToInt(vector3Plane.y / this.itsSizeTerrain.y * (float)this.itsDataModuleMinimap.itsFogOfWar.itsResolutionY));
		Vector2 vector2 = new Vector2((float)Mathf.RoundToInt(this.itsDataModuleMinimap.itsFogOfWar.itsRevealDistance / this.itsSizeTerrain.x * (float)this.itsDataModuleMinimap.itsFogOfWar.itsResolutionX), (float)Mathf.RoundToInt(this.itsDataModuleMinimap.itsFogOfWar.itsRevealDistance / this.itsSizeTerrain.y * (float)this.itsDataModuleMinimap.itsFogOfWar.itsResolutionY)) * 2f;
		Vector3 vector3 = Vector3.zero;
		int num = Math.Min(this.itsDataModuleMinimap.itsFogOfWar.itsResolutionY + 1, (int)(vector.y + vector2.y));
		for (int i = Math.Max(0, (int)(vector.y - vector2.y)); i < num; i++)
		{
			int num2 = Math.Min(this.itsDataModuleMinimap.itsFogOfWar.itsResolutionX + 1, (int)(vector.x + vector2.x));
			for (int j = Math.Max(0, (int)(vector.x - vector2.x)); j < num2; j++)
			{
				int num3 = i * (this.itsDataModuleMinimap.itsFogOfWar.itsResolutionX + 1) + j;
				vector3 = this.itsMeshFilterFogOfWarPlane.transform.position + new Vector3(this.itsFOWVertices[num3].x * this.itsScalingFogOfWar.x, this.itsFOWVertices[num3].z * this.itsScalingFogOfWar.y, this.itsFOWVertices[num3].z * this.itsScalingFogOfWar.y);
				vector3 = this.ChangeVectorHeight(vector3, this.GetVector3Height(thePosition));
				float num4 = Vector3.Distance(vector3, thePosition);
				if (num4 < this.itsDataModuleMinimap.itsFogOfWar.itsRevealedFullDistance)
				{
					this.itsFOWColors[num3] = this.itsColorFogOfWarRevealed;
				}
				else if (num4 < this.itsDataModuleMinimap.itsFogOfWar.itsRevealDistance)
				{
					float a = Mathf.Min(this.itsFOWColors[num3].a, Mathf.Clamp((num4 - this.itsDataModuleMinimap.itsFogOfWar.itsRevealedFullDistance) / (this.itsDataModuleMinimap.itsFogOfWar.itsRevealDistance - this.itsDataModuleMinimap.itsFogOfWar.itsRevealedFullDistance), 0f, 1f));
					this.itsFOWColors[num3].a = a;
				}
			}
		}
		int num5 = (int)(vector.y * (float)(this.itsDataModuleMinimap.itsFogOfWar.itsResolutionX + 1) + vector.x);
		if (num5 >= 0 && num5 < this.itsFOWColors.Length)
		{
			this.itsFOWColors[num5] = this.itsColorFogOfWarRevealed;
		}
		this.itsMeshFilterFogOfWarPlane.mesh.colors = this.itsFOWColors;
	}

	// Token: 0x06000C34 RID: 3124 RVA: 0x00058DD8 File Offset: 0x00056FD8
	private void UpdateViewPortCube()
	{
		if (this.itsDataModuleMinimap.itsGlobalSettings.itsOrientation == KGFMapSystem.KGFMapSystemOrientation.XYSideScroller)
		{
			return;
		}
		if (this.itsDataModuleMinimap.itsGlobalSettings.itsTarget == null)
		{
			return;
		}
		if (this.itsDataModuleMinimap.itsViewport.itsCamera == null)
		{
			return;
		}
		if (this.itsViewPortCubeMesh == null)
		{
			this.itsGameObjectViewPort = new GameObject();
			this.itsGameObjectViewPort.AddComponent<MeshFilter>().mesh = this.GeneratePlaneMeshXZ();
			this.itsMaterialViewport = new Material(this.itsDataModuleMinimap.itsShaders.itsShaderMapIcon);
			this.itsGameObjectViewPort.AddComponent<MeshRenderer>().material = this.itsMaterialViewport;
			this.itsGameObjectViewPort.name = "minimap viewport";
			this.itsGameObjectViewPort.transform.parent = base.transform;
			this.SetLayerRecursively(this.itsGameObjectViewPort, this.itsLayerMinimap);
			this.itsViewPortCubeMesh = this.itsGameObjectViewPort.GetComponent<MeshFilter>().mesh;
		}
		if (KGFMapSystem.KGFGetActive(this.itsGameObjectViewPort) != this.itsDataModuleMinimap.itsViewport.itsActive)
		{
			KGFMapSystem.KGFSetChildrenActiveRecursively(this.itsGameObjectViewPort, this.itsDataModuleMinimap.itsViewport.itsActive);
		}
		if (!this.itsDataModuleMinimap.itsViewport.itsActive)
		{
			return;
		}
		Vector3[] vertices = this.itsViewPortCubeMesh.vertices;
		vertices[1] = this.itsDataModuleMinimap.itsViewport.itsCamera.ScreenToWorldPoint(new Vector3((float)Screen.width, (float)(Screen.height / 2), this.itsDataModuleMinimap.itsViewport.itsCamera.farClipPlane));
		vertices[2] = this.itsDataModuleMinimap.itsViewport.itsCamera.ScreenToWorldPoint(new Vector3((float)Screen.width, (float)(Screen.height / 2), this.itsDataModuleMinimap.itsViewport.itsCamera.nearClipPlane));
		vertices[3] = this.itsDataModuleMinimap.itsViewport.itsCamera.ScreenToWorldPoint(new Vector3(0f, (float)(Screen.height / 2), this.itsDataModuleMinimap.itsViewport.itsCamera.nearClipPlane));
		vertices[0] = this.itsDataModuleMinimap.itsViewport.itsCamera.ScreenToWorldPoint(new Vector3(0f, (float)(Screen.height / 2), this.itsDataModuleMinimap.itsViewport.itsCamera.farClipPlane));
		for (int i = 0; i < 4; i++)
		{
			vertices[i].y = this.GetHeightViewPort();
		}
		this.itsViewPortCubeMesh.vertices = vertices;
		this.itsViewPortCubeMesh.RecalculateBounds();
		this.itsMaterialViewport.SetColor("_Color", this.itsDataModuleMinimap.itsViewport.itsColor);
	}

	// Token: 0x06000C35 RID: 3125 RVA: 0x000590C0 File Offset: 0x000572C0
	private Bounds? GetBoundsOfTerrain(GameObject theTerrain)
	{
		MeshRenderer component = theTerrain.GetComponent<MeshRenderer>();
		if (component != null)
		{
			return new Bounds?(component.bounds);
		}
		TerrainCollider component2 = theTerrain.GetComponent<TerrainCollider>();
		if (component2 != null)
		{
			return new Bounds?(component2.bounds);
		}
		this.LogError("Could not get measure bounds of terrain.", base.name, this);
		return null;
	}

	// Token: 0x06000C36 RID: 3126 RVA: 0x00059128 File Offset: 0x00057328
	private void InitLayer()
	{
		if (this.itsLayerMinimap < 0)
		{
			this.itsLayerMinimap = LayerMask.NameToLayer("mapsystem");
		}
	}

	// Token: 0x06000C37 RID: 3127 RVA: 0x00059148 File Offset: 0x00057348
	public static bool IsInLayerMask(GameObject obj, LayerMask mask)
	{
		return (mask.value & 1 << obj.layer) > 0;
	}

	// Token: 0x06000C38 RID: 3128 RVA: 0x0005916C File Offset: 0x0005736C
	private bool GetMeasuredBounds(LayerMask theLayers, out Bounds theBounds)
	{
		Bounds? bounds = null;
		this.InitLayer();
		if (Terrain.activeTerrain != null)
		{
			bounds = this.GetBoundsOfTerrain(Terrain.activeTerrain.gameObject);
		}
		Renderer[] array = UnityEngine.Object.FindObjectsOfType(typeof(Renderer)) as Renderer[];
		if (array != null)
		{
			foreach (Renderer renderer in array)
			{
				if (renderer.gameObject.layer != this.itsLayerMinimap)
				{
					if (KGFMapSystem.IsInLayerMask(renderer.gameObject, theLayers))
					{
						if (bounds == null)
						{
							bounds = new Bounds?(renderer.bounds);
						}
						else
						{
							Bounds value = bounds.Value;
							value.Encapsulate(renderer.bounds);
							bounds = new Bounds?(value);
						}
					}
				}
			}
		}
		if (bounds == null)
		{
			this.LogError("Could not find terrain nor any other bounds in scene", base.name, this);
			theBounds = default(Bounds);
			return false;
		}
		theBounds = bounds.Value;
		return true;
	}

	// Token: 0x06000C39 RID: 3129 RVA: 0x0005928C File Offset: 0x0005748C
	private float GetHighestNPOTSizeSmallerThanScreen()
	{
		float num = (float)Screen.width;
		if ((float)Screen.height < num)
		{
			num = (float)Screen.height;
		}
		float num2;
		for (num2 = 1f; num2 <= num; num2 *= 2f)
		{
		}
		return num2 / 2f;
	}

	// Token: 0x06000C3A RID: 3130 RVA: 0x000592D4 File Offset: 0x000574D4
	public void TakePhotoOfScene(bool theStartedFromEditor)
	{
		this.MeasureScene();
		this.AutoCreatePhoto();
	}

	// Token: 0x06000C3B RID: 3131 RVA: 0x000592E4 File Offset: 0x000574E4
	private void ClearPhotoData()
	{
		if (Application.isPlaying)
		{
			foreach (KGFMapSystem.KGFPhotoData kgfphotoData in this.itsListOfPhotoData)
			{
				UnityEngine.Object.Destroy(kgfphotoData.itsPhotoPlane);
				UnityEngine.Object.Destroy(kgfphotoData.itsTexture);
			}
		}
		this.itsListOfPhotoData.Clear();
	}

	// Token: 0x06000C3C RID: 3132 RVA: 0x00059370 File Offset: 0x00057570
	private KGFPhotoCapture CreatePhotoCamera(float anOrtographicSize, float aTextureSize)
	{
		this.itsTempCameraGameObject = new GameObject("TempCamera");
		KGFMapSystem.KGFMapSystemOrientation itsOrientation = this.itsDataModuleMinimap.itsGlobalSettings.itsOrientation;
		if (itsOrientation != KGFMapSystem.KGFMapSystemOrientation.XYSideScroller)
		{
			if (itsOrientation == KGFMapSystem.KGFMapSystemOrientation.XZDefault)
			{
				this.itsTempCameraGameObject.transform.eulerAngles = new Vector3(90f, 0f, 0f);
			}
		}
		else
		{
			this.itsTempCameraGameObject.transform.eulerAngles = new Vector3(0f, 0f, 0f);
		}
		Camera camera = this.itsTempCameraGameObject.AddComponent<Camera>();
		camera.depth = -100f;
		camera.clearFlags = CameraClearFlags.Color;
		camera.backgroundColor = Color.red;
		camera.isOrthoGraphic = true;
		if (this.GetOrientation() == KGFMapSystem.KGFMapSystemOrientation.XZDefault)
		{
			camera.farClipPlane = 2f + this.itsTerrainBoundsPhoto.size.y * 2f;
		}
		else
		{
			camera.farClipPlane = 2f + this.itsTerrainBoundsPhoto.size.z * 2f;
		}
		camera.cullingMask = this.itsDataModuleMinimap.itsPhoto.itsPhotoLayers;
		camera.backgroundColor = this.itsDataModuleMinimap.itsGlobalSettings.itsColorBackground;
		camera.clearFlags = CameraClearFlags.Color;
		camera.aspect = 1f;
		camera.orthographicSize = anOrtographicSize / 2f;
		camera.pixelRect = new Rect(0f, 0f, aTextureSize, aTextureSize);
		KGFMapSystem.KGFSetChildrenActiveRecursively(camera.gameObject, true);
		camera.enabled = true;
		KGFPhotoCapture kgfphotoCapture = this.itsTempCameraGameObject.AddComponent<KGFPhotoCapture>();
		kgfphotoCapture.itsMapSystem = this;
		return kgfphotoCapture;
	}

	// Token: 0x06000C3D RID: 3133 RVA: 0x0005951C File Offset: 0x0005771C
	private void AutoCreatePhoto()
	{
		if (this.itsGameObjectPhotoParent == null)
		{
			Transform transform = base.transform.Find("photo");
			if (transform != null)
			{
				this.itsGameObjectPhotoParent = transform.gameObject;
			}
		}
		if (this.itsTempCameraGameObject != null)
		{
			if (Application.isPlaying)
			{
				UnityEngine.Object.Destroy(this.itsTempCameraGameObject);
			}
			else
			{
				UnityEngine.Object.DestroyImmediate(this.itsTempCameraGameObject);
			}
		}
		if (this.itsGameObjectPhotoParent != null)
		{
			if (Application.isPlaying)
			{
				UnityEngine.Object.Destroy(this.itsGameObjectPhotoParent);
			}
			else
			{
				UnityEngine.Object.DestroyImmediate(this.itsGameObjectPhotoParent);
			}
		}
		this.itsGameObjectPhotoParent = new GameObject("photo");
		this.itsGameObjectPhotoParent.transform.parent = base.transform;
		Vector2 vector3Plane = this.GetVector3Plane(this.itsTerrainBoundsPhoto.min);
		Vector2 vector3Plane2 = this.GetVector3Plane(this.itsTerrainBoundsPhoto.max);
		float highestNPOTSizeSmallerThanScreen = this.GetHighestNPOTSizeSmallerThanScreen();
		float num = highestNPOTSizeSmallerThanScreen / this.itsDataModuleMinimap.itsPhoto.itsPixelPerMeter;
		float anOrtographicSize = highestNPOTSizeSmallerThanScreen / this.itsDataModuleMinimap.itsPhoto.itsPixelPerMeter;
		int num2 = 0;
		int num3 = 0;
		this.ClearPhotoData();
		do
		{
			KGFMapSystem.KGFPhotoData kgfphotoData = new KGFMapSystem.KGFPhotoData();
			kgfphotoData.itsMeters = num;
			Vector3 theVector = this.itsTerrainBoundsPhoto.max - this.itsTerrainBoundsPhoto.min;
			theVector = this.ChangeVectorHeight(theVector, 0f);
			KGFMapSystem.KGFMapSystemOrientation itsOrientation = this.itsDataModuleMinimap.itsGlobalSettings.itsOrientation;
			if (itsOrientation != KGFMapSystem.KGFMapSystemOrientation.XYSideScroller)
			{
				if (itsOrientation == KGFMapSystem.KGFMapSystemOrientation.XZDefault)
				{
					kgfphotoData.itsPosition = this.CreateVector(vector3Plane.x + num * (float)num2, vector3Plane.y + num * (float)num3, this.itsTerrainBoundsPhoto.min.y - 1f);
				}
			}
			else
			{
				kgfphotoData.itsPosition = this.CreateVector(vector3Plane.x + num * (float)num2, vector3Plane.y + num * (float)num3, this.itsTerrainBoundsPhoto.max.z + 1f);
			}
			kgfphotoData.itsTexture = new Texture2D((int)highestNPOTSizeSmallerThanScreen, (int)highestNPOTSizeSmallerThanScreen, TextureFormat.ARGB32, false);
			kgfphotoData.itsTextureSize = highestNPOTSizeSmallerThanScreen;
			GameObject gameObject = this.GeneratePhotoPlane(kgfphotoData);
			KGFMapSystem.KGFSetChildrenActiveRecursively(gameObject, false);
			gameObject.transform.parent = this.itsGameObjectPhotoParent.transform;
			gameObject.name = string.Concat(new object[]
			{
				gameObject.name,
				"_",
				num2,
				"_",
				num3
			});
			this.SetLayerRecursively(gameObject.gameObject, this.itsLayerMinimap);
			Vector3 itsPosition = kgfphotoData.itsPosition;
			gameObject.transform.position = itsPosition;
			gameObject.transform.localScale = new Vector3(num, 1f, num);
			itsOrientation = this.itsDataModuleMinimap.itsGlobalSettings.itsOrientation;
			if (itsOrientation != KGFMapSystem.KGFMapSystemOrientation.XYSideScroller)
			{
				if (itsOrientation == KGFMapSystem.KGFMapSystemOrientation.XZDefault)
				{
					gameObject.transform.localEulerAngles = Vector3.zero;
				}
			}
			else
			{
				gameObject.transform.localEulerAngles = new Vector3(270f, 0f, 0f);
			}
			kgfphotoData.itsPhotoPlane = gameObject;
			this.itsListOfPhotoData.Add(kgfphotoData);
			num2++;
			if (vector3Plane.x + (float)num2 * num > vector3Plane2.x)
			{
				num2 = 0;
				num3++;
			}
		}
		while (vector3Plane.y + (float)num3 * num <= vector3Plane2.y);
		this.itsArrayOfPhotoData = this.itsListOfPhotoData.ToArray();
		this.SetColors();
		this.CreatePhotoCamera(anOrtographicSize, highestNPOTSizeSmallerThanScreen);
	}

	// Token: 0x06000C3E RID: 3134 RVA: 0x000598F4 File Offset: 0x00057AF4
	public KGFMapSystem.KGFPhotoData[] GetPhotoData()
	{
		if (this.itsListOfPhotoData != null)
		{
			return this.itsListOfPhotoData.ToArray();
		}
		return null;
	}

	// Token: 0x06000C3F RID: 3135 RVA: 0x00059910 File Offset: 0x00057B10
	public GameObject GetPhotoParent()
	{
		return this.itsGameObjectPhotoParent;
	}

	// Token: 0x06000C40 RID: 3136 RVA: 0x00059918 File Offset: 0x00057B18
	public KGFMapSystem.KGFPhotoData GetNextPhotoData()
	{
		if (this.itsArrayOfPhotoDataIndex < this.itsArrayOfPhotoData.Length)
		{
			this.itsArrayOfPhotoDataIndex++;
			return this.itsArrayOfPhotoData[this.itsArrayOfPhotoDataIndex - 1];
		}
		return null;
	}

	// Token: 0x06000C41 RID: 3137 RVA: 0x00059958 File Offset: 0x00057B58
	private void SetLayerRecursively(GameObject theGameObject, int theLayer)
	{
		theGameObject.layer = theLayer;
		foreach (object obj in theGameObject.transform)
		{
			Transform transform = (Transform)obj;
			GameObject gameObject = transform.gameObject;
			this.SetLayerRecursively(gameObject, theLayer);
		}
	}

	// Token: 0x06000C42 RID: 3138 RVA: 0x000599D8 File Offset: 0x00057BD8
	private void CreateCameras()
	{
		this.itsCamera = new GameObject("minimapcamera")
		{
			transform = 
			{
				parent = base.transform
			}
		}.AddComponent<Camera>();
		this.itsCamera.aspect = 1f;
		this.itsCamera.orthographic = true;
		this.itsCamera.clearFlags = CameraClearFlags.Color;
		this.itsCamera.backgroundColor = this.itsDataModuleMinimap.itsGlobalSettings.itsColorBackground;
		this.itsCameraTransform = this.itsCamera.transform;
		GameObject gameObject = new GameObject("outputcamera");
		this.itsCameraOutput = gameObject.AddComponent<Camera>();
		this.itsCameraOutput.transform.parent = this.itsCamera.transform;
		this.itsCameraOutput.transform.localPosition = Vector3.zero;
		this.itsCameraOutput.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
		this.itsCameraOutput.transform.localScale = Vector3.one;
		this.itsCameraOutput.orthographic = true;
		this.itsCameraOutput.clearFlags = CameraClearFlags.Depth;
		this.itsCameraOutput.depth = 50f;
		this.itsCameraOutput.cullingMask = 1 << this.itsLayerMinimap;
		this.itsCurrentZoom = this.itsDataModuleMinimap.itsZoom.itsZoomStartValue;
		this.itsCurrentZoomDest = this.itsCurrentZoom;
		this.UpdateOrthographicSize();
	}

	// Token: 0x06000C43 RID: 3139 RVA: 0x00059B48 File Offset: 0x00057D48
	private void CreateRenderTexture()
	{
		this.itsRendertexture = new RenderTexture(2048, 2048, 16, RenderTextureFormat.ARGB32);
		if (this.itsRendertexture != null)
		{
			this.itsRendertexture.isPowerOfTwo = true;
			this.itsRendertexture.name = "minimap_rendertexture";
			this.itsRendertexture.Create();
			this.itsCamera.targetTexture = this.itsRendertexture;
		}
		else
		{
			this.LogError("cannot create rendertexture for minimap", base.name, this);
		}
	}

	// Token: 0x06000C44 RID: 3140 RVA: 0x00059BD0 File Offset: 0x00057DD0
	private void OnGUI()
	{
		this.RenderGUI();
	}

	// Token: 0x06000C45 RID: 3141 RVA: 0x00059BD8 File Offset: 0x00057DD8
	private void OnMapIconAdd(object theSender, EventArgs theArgs)
	{
		KGFAccessor.KGFAccessorEventargs kgfaccessorEventargs = theArgs as KGFAccessor.KGFAccessorEventargs;
		if (kgfaccessorEventargs != null)
		{
			KGFIMapIcon kgfimapIcon = kgfaccessorEventargs.GetObject() as KGFIMapIcon;
			if (kgfimapIcon != null)
			{
				this.RegisterIcon(kgfimapIcon);
			}
		}
	}

	// Token: 0x06000C46 RID: 3142 RVA: 0x00059C0C File Offset: 0x00057E0C
	private void OnMapIconRemove(object theSender, EventArgs theArgs)
	{
		KGFAccessor.KGFAccessorEventargs kgfaccessorEventargs = theArgs as KGFAccessor.KGFAccessorEventargs;
		if (kgfaccessorEventargs != null)
		{
			KGFIMapIcon kgfimapIcon = kgfaccessorEventargs.GetObject() as KGFIMapIcon;
			if (kgfimapIcon != null)
			{
				this.UnregisterMapIcon(kgfimapIcon);
			}
		}
	}

	// Token: 0x06000C47 RID: 3143 RVA: 0x00059C40 File Offset: 0x00057E40
	private KGFMapIcon CreateIconInternal(Vector3 theWorldPoint, KGFMapIcon theIcon, Transform theParent)
	{
		GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(theIcon.gameObject);
		gameObject.name = "Flag";
		gameObject.transform.parent = this.itsContainerFlags;
		gameObject.transform.position = theWorldPoint;
		return gameObject.GetComponent<KGFMapIcon>();
	}

	// Token: 0x06000C48 RID: 3144 RVA: 0x00059C8C File Offset: 0x00057E8C
	private GameObject GenerateMinimapPlane()
	{
		GameObject gameObject = new GameObject("output_plane");
		gameObject.layer = this.itsLayerMinimap;
		gameObject.transform.parent = base.transform;
		this.itsMinimapMeshFilter = gameObject.AddComponent<MeshFilter>();
		this.itsMinimapMeshFilter.mesh = this.GeneratePlaneMeshXZ();
		this.itsMeshRendererMinimapPlane = gameObject.gameObject.AddComponent<MeshRenderer>();
		this.itsMeshRendererMinimapPlane.material = new Material(this.itsDataModuleMinimap.itsShaders.itsShaderMapMask);
		this.itsMeshRendererMinimapPlane.material.SetTexture("_Mask", this.itsDataModuleMinimap.itsAppearanceMiniMap.itsMask);
		this.itsMaterialMaskedMinimap = this.itsMeshRendererMinimapPlane.material;
		return gameObject;
	}

	// Token: 0x06000C49 RID: 3145 RVA: 0x00059D48 File Offset: 0x00057F48
	public void SetMask(Texture2D theMinimapMask, Texture2D theMapMask)
	{
		if (this.itsMeshRendererMinimapPlane.material == null)
		{
			return;
		}
		this.itsDataModuleMinimap.itsAppearanceMiniMap.itsMask = theMinimapMask;
		this.itsDataModuleMinimap.itsAppearanceMap.itsMask = theMapMask;
		this.UpdateMaskTexture();
	}

	// Token: 0x06000C4A RID: 3146 RVA: 0x00059D94 File Offset: 0x00057F94
	public static GameObject GenerateTexturePlane(Texture2D theTexture, Shader theShader)
	{
		GameObject gameObject = new GameObject("MapIconPlane");
		MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
		meshFilter.transform.eulerAngles = new Vector3(0f, 0f, 0f);
		meshFilter.mesh = KGFMapSystem.GeneratePlaneMeshXZCentered();
		MeshRenderer meshRenderer = meshFilter.gameObject.AddComponent<MeshRenderer>();
		meshRenderer.material = new Material(theShader);
		meshRenderer.material.mainTexture = theTexture;
		meshRenderer.castShadows = false;
		meshRenderer.receiveShadows = false;
		return gameObject;
	}

	// Token: 0x06000C4B RID: 3147 RVA: 0x00059E10 File Offset: 0x00058010
	private GameObject GeneratePhotoPlane(KGFMapSystem.KGFPhotoData thePhotoData)
	{
		GameObject gameObject = new GameObject("photo_plane");
		gameObject.layer = this.itsLayerMinimap;
		gameObject.transform.parent = base.transform;
		MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
		meshFilter.transform.eulerAngles = Vector3.zero;
		meshFilter.transform.position = Vector3.zero;
		meshFilter.mesh = this.GeneratePlaneMeshXZ();
		MeshRenderer meshRenderer = meshFilter.gameObject.AddComponent<MeshRenderer>();
		meshRenderer.castShadows = false;
		meshRenderer.receiveShadows = false;
		Material material = new Material(this.itsDataModuleMinimap.itsShaders.itsShaderPhotoPlane);
		material.mainTexture = thePhotoData.itsTexture;
		meshRenderer.material = material;
		thePhotoData.itsPhotoPlaneMaterial = material;
		return gameObject;
	}

	// Token: 0x06000C4C RID: 3148 RVA: 0x00059EC4 File Offset: 0x000580C4
	private void UpdateMinimapOutputPlane()
	{
		Camera camera = this.itsCameraOutput;
		if (camera != null && this.itsMinimapPlane != null)
		{
			if (this.itsMinimapMeshFilter == null)
			{
				return;
			}
			Mesh mesh = this.itsMinimapMeshFilter.mesh;
			if (mesh == null)
			{
				return;
			}
			Rect rect = this.itsTargetRect;
			rect.y = (float)Screen.height - rect.y;
			Vector3[] vertices = mesh.vertices;
			vertices[0] = camera.ScreenToWorldPoint(new Vector3(rect.x, rect.y - rect.height, camera.nearClipPlane + 0.01f));
			vertices[1] = camera.ScreenToWorldPoint(new Vector3(rect.x + rect.width, rect.y - rect.height, camera.nearClipPlane + 0.01f));
			vertices[2] = camera.ScreenToWorldPoint(new Vector3(rect.x + rect.width, rect.y, camera.nearClipPlane + 0.01f));
			vertices[3] = camera.ScreenToWorldPoint(new Vector3(rect.x, rect.y, camera.nearClipPlane + 0.01f));
			mesh.vertices = vertices;
			mesh.RecalculateBounds();
			Vector3 forward = camera.transform.forward;
			Vector3[] normals = mesh.normals;
			normals[0] = forward;
			normals[1] = forward;
			normals[2] = forward;
			normals[3] = forward;
			mesh.normals = normals;
		}
	}

	// Token: 0x06000C4D RID: 3149 RVA: 0x0005A090 File Offset: 0x00058290
	private void CleanClickIconsList()
	{
		for (int i = this.itsListClickIcons.Count - 1; i >= 0; i--)
		{
			if (this.itsListClickIcons[i] == null)
			{
				this.itsListClickIcons.RemoveAt(i);
			}
			else if (this.itsListClickIcons[i] is MonoBehaviour && (MonoBehaviour)this.itsListClickIcons[i] == null)
			{
				this.itsListClickIcons.RemoveAt(i);
			}
		}
	}

	// Token: 0x06000C4E RID: 3150 RVA: 0x0005A120 File Offset: 0x00058320
	private void UpdateIconLayer(KGFIMapIcon theMapIcon)
	{
		GameObject representation = theMapIcon.GetRepresentation();
		MeshRenderer[] componentsInChildren = representation.GetComponentsInChildren<MeshRenderer>();
		this.CleanClickIconsList();
		foreach (MeshRenderer meshRenderer in componentsInChildren)
		{
			if (this.itsDataModuleMinimap.itsFogOfWar.itsHideMapIcons && !this.itsListClickIcons.Contains(theMapIcon))
			{
				meshRenderer.sharedMaterial.renderQueue = 3000;
			}
			else
			{
				meshRenderer.sharedMaterial.renderQueue = 3200;
			}
		}
	}

	// Token: 0x06000C4F RID: 3151 RVA: 0x0005A1AC File Offset: 0x000583AC
	private Vector3 GetGameObjectSize(GameObject theGO)
	{
		MeshRenderer[] componentsInChildren = theGO.GetComponentsInChildren<MeshRenderer>(true);
		if (componentsInChildren.Length == 0)
		{
			Debug.LogError("found not meshrenderers on mapicon:" + theGO.name);
			return Vector3.zero;
		}
		Bounds bounds = componentsInChildren[0].bounds;
		for (int i = 1; i < componentsInChildren.Length; i++)
		{
			bounds.Encapsulate(componentsInChildren[i].bounds);
		}
		return bounds.size;
	}

	// Token: 0x06000C50 RID: 3152 RVA: 0x0005A218 File Offset: 0x00058418
	private void RegisterIcon(KGFIMapIcon theMapIcon)
	{
		GameObject gameObject = null;
		GameObject representation = theMapIcon.GetRepresentation();
		if (representation == null)
		{
			this.LogError("missing icon representation for: " + theMapIcon.GetGameObjectName(), base.name, this);
			return;
		}
		this.UpdateIconLayer(theMapIcon);
		if (theMapIcon.GetTextureArrow() != null)
		{
			gameObject = KGFMapSystem.GenerateTexturePlane(theMapIcon.GetTextureArrow(), this.itsDataModuleMinimap.itsShaders.itsShaderMapIcon);
			gameObject.transform.parent = this.itsContainerIconArrows;
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localScale = Vector3.one;
			gameObject.GetComponent<MeshRenderer>().material.renderQueue = 3200;
			this.SetLayerRecursively(gameObject.gameObject, this.itsLayerMinimap);
		}
		representation.transform.parent = this.itsContainerIcons;
		representation.transform.position = Vector3.zero;
		this.SetLayerRecursively(representation.gameObject, this.itsLayerMinimap);
		KGFMapSystem.mapicon_listitem_script mapicon_listitem_script = new KGFMapSystem.mapicon_listitem_script();
		mapicon_listitem_script.itsModule = this;
		mapicon_listitem_script.itsMapIcon = theMapIcon;
		mapicon_listitem_script.itsRepresentationInstance = representation;
		mapicon_listitem_script.itsRepresentationInstanceTransform = representation.transform;
		mapicon_listitem_script.itsRotate = theMapIcon.GetRotate();
		mapicon_listitem_script.itsRepresentationArrowInstance = gameObject;
		mapicon_listitem_script.itsMapIconTransform = theMapIcon.GetTransform();
		mapicon_listitem_script.SetVisibility(true);
		if (gameObject != null)
		{
			mapicon_listitem_script.itsRepresentationArrowInstanceTransform = gameObject.transform;
		}
		mapicon_listitem_script.itsCachedRepresentationSize = this.GetGameObjectSize(representation);
		this.itsListMapIcons.Add(mapicon_listitem_script);
		mapicon_listitem_script.UpdateIcon();
		this.UpdateIconScale();
		this.LogInfo(string.Format("Added icon of category '{0}' for '{1}'", theMapIcon.GetCategory(), theMapIcon.GetTransform().name), base.name, this);
	}

	// Token: 0x06000C51 RID: 3153 RVA: 0x0005A3CC File Offset: 0x000585CC
	private void UnregisterMapIcon(KGFIMapIcon theMapIcon)
	{
		for (int i = 0; i < this.itsListMapIcons.Count; i++)
		{
			KGFMapSystem.mapicon_listitem_script mapicon_listitem_script = this.itsListMapIcons[i];
			if (mapicon_listitem_script.itsMapIcon == theMapIcon)
			{
				this.LogInfo("Removed map icon of " + mapicon_listitem_script.itsMapIconTransform.gameObject.GetObjectPath(), base.name, this);
				mapicon_listitem_script.Destroy();
				this.itsListMapIcons.RemoveAt(i);
				break;
			}
		}
	}

	// Token: 0x06000C52 RID: 3154 RVA: 0x0005A44C File Offset: 0x0005864C
	private void UpdateIconScale()
	{
		float scaleIcons = this.GetScaleIcons();
		float scaleArrows = this.GetScaleArrows();
		foreach (KGFMapSystem.mapicon_listitem_script mapicon_listitem_script in this.itsListMapIcons)
		{
			if (mapicon_listitem_script.itsRepresentationInstanceTransform != null)
			{
				if (mapicon_listitem_script.itsMapIcon != null)
				{
					mapicon_listitem_script.itsRepresentationInstanceTransform.localScale = Vector3.one * scaleIcons * mapicon_listitem_script.itsMapIcon.GetIconScale();
				}
				else
				{
					mapicon_listitem_script.itsRepresentationInstanceTransform.localScale = Vector3.one * scaleIcons;
				}
			}
			if (mapicon_listitem_script.itsRepresentationArrowInstanceTransform != null)
			{
				mapicon_listitem_script.itsRepresentationArrowInstanceTransform.localScale = Vector3.one * scaleArrows;
			}
		}
	}

	// Token: 0x06000C53 RID: 3155 RVA: 0x0005A540 File Offset: 0x00058740
	private float GetScaleArrows()
	{
		if (this.GetFullscreen() || this.GetPanningActive())
		{
			return 0f;
		}
		return this.GetCurrentRange() * this.itsDataModuleMinimap.itsAppearanceMiniMap.itsScaleArrows * 2f;
	}

	// Token: 0x06000C54 RID: 3156 RVA: 0x0005A588 File Offset: 0x00058788
	private float GetScaleIcons()
	{
		if (this.GetFullscreen())
		{
			return this.GetCurrentRange() * this.itsDataModuleMinimap.itsAppearanceMap.itsScaleIcons * 2f * (this.itsSavedResolution.Value.y / this.GetHeight());
		}
		return this.GetCurrentRange() * this.itsDataModuleMinimap.itsAppearanceMiniMap.itsScaleIcons * 2f;
	}

	// Token: 0x06000C55 RID: 3157 RVA: 0x0005A5F8 File Offset: 0x000587F8
	private void UpdateMaskTexture()
	{
		if (this.GetFullscreen())
		{
			this.itsTextureRenderMaskCurrent = this.itsDataModuleMinimap.itsAppearanceMap.itsMask;
		}
		else
		{
			this.itsTextureRenderMaskCurrent = this.itsDataModuleMinimap.itsAppearanceMiniMap.itsMask;
		}
		if (this.itsTextureRenderMaskCurrent != null && this.itsMeshRendererMinimapPlane != null)
		{
			this.itsMeshRendererMinimapPlane.material.SetTexture("_Mask", this.itsTextureRenderMaskCurrent);
		}
		if (this.GetOutputPlaneActive())
		{
			if (this.itsMeshRendererMinimapPlane != null)
			{
				this.itsMeshRendererMinimapPlane.enabled = true;
			}
			this.itsCameraOutput.enabled = true;
			this.itsCamera.targetTexture = this.itsRendertexture;
			this.itsCamera.rect = new Rect(0f, 0f, 1f, 1f);
		}
		else
		{
			if (this.itsMeshRendererMinimapPlane != null)
			{
				this.itsMeshRendererMinimapPlane.enabled = false;
			}
			this.itsCameraOutput.enabled = false;
			this.itsCamera.targetTexture = null;
			this.itsCamera.pixelRect = new Rect(this.itsTargetRect.x, (float)Screen.height - this.itsTargetRect.y - this.itsTargetRect.height, this.itsTargetRect.width, this.itsTargetRect.height);
		}
	}

	// Token: 0x06000C56 RID: 3158 RVA: 0x0005A770 File Offset: 0x00058970
	private void UpdateCameraLayer()
	{
		this.itsCamera.cullingMask = (this.itsDataModuleMinimap.itsGlobalSettings.itsRenderLayers | 1 << this.itsLayerMinimap);
	}

	// Token: 0x06000C57 RID: 3159 RVA: 0x0005A7AC File Offset: 0x000589AC
	private void SetColors()
	{
		if (this.itsMaterialMaskedMinimap != null)
		{
			this.itsMaterialMaskedMinimap.SetColor("_Color", this.itsDataModuleMinimap.itsGlobalSettings.itsColorAll);
		}
		if (this.itsArrayOfPhotoData != null)
		{
			foreach (KGFMapSystem.KGFPhotoData kgfphotoData in this.itsArrayOfPhotoData)
			{
				if (kgfphotoData != null)
				{
					kgfphotoData.itsPhotoPlaneMaterial.SetColor("_Color", this.itsDataModuleMinimap.itsGlobalSettings.itsColorMap);
				}
			}
		}
	}

	// Token: 0x06000C58 RID: 3160 RVA: 0x0005A83C File Offset: 0x00058A3C
	private void Update()
	{
		if (this.itsErrorMode)
		{
			return;
		}
		this.UpdateCameraLayer();
		this.UpdateZoom();
		this.UpdateIconScale();
		this.UpdateOrthographicSize();
		this.SetColors();
		if (this.itsTargetTransform == null)
		{
			base.enabled = false;
			return;
		}
		this.ScrollWheelZooming();
		this.UpdatePanning();
		this.UpdateMaskTexture();
		bool flag;
		this.UpdateMapIconHover(out flag);
		if (!flag)
		{
			this.CheckForClicksOnMinimap();
		}
		this.UpdateMapIconRotation();
		this.UpdateViewPortCube();
	}

	// Token: 0x06000C59 RID: 3161 RVA: 0x0005A8C0 File Offset: 0x00058AC0
	private void ScrollWheelZooming()
	{
		if (this.GetHoverWithoutButtons())
		{
			this.SetZoom(this.GetZoom() - Input.GetAxis("Mouse ScrollWheel") * 50f);
		}
	}

	// Token: 0x06000C5A RID: 3162 RVA: 0x0005A8F8 File Offset: 0x00058AF8
	private bool GetPanningActive()
	{
		return this.itsDataModuleMinimap.itsPanning.itsActive && this.itsMapPanning != Vector2.zero;
	}

	// Token: 0x06000C5B RID: 3163 RVA: 0x0005A924 File Offset: 0x00058B24
	private bool GetPanningMoveActive()
	{
		return this.itsDataModuleMinimap.itsPanning.itsActive && Input.GetMouseButton(this.itsPanningButton);
	}

	// Token: 0x06000C5C RID: 3164 RVA: 0x0005A954 File Offset: 0x00058B54
	private void ForcePanningStart()
	{
		this.itsMapPanning = new Vector2(0.01f, 0.01f);
		this.UpdateIconScale();
	}

	// Token: 0x06000C5D RID: 3165 RVA: 0x0005A974 File Offset: 0x00058B74
	private void StopMapPanning()
	{
		this.itsMapPanningDest = Vector2.zero;
	}

	// Token: 0x06000C5E RID: 3166 RVA: 0x0005A984 File Offset: 0x00058B84
	private void UpdatePanning()
	{
		if (!this.itsDataModuleMinimap.itsPanning.itsActive)
		{
			this.itsMapPanning = Vector2.zero;
			this.itsMapPanningDest = Vector2.zero;
			return;
		}
		if (this.GetHover())
		{
			if (Input.GetMouseButtonDown(this.itsPanningButton))
			{
				this.itsMapPanningMousePosLast = Input.mousePosition;
			}
			if (!this.GetPanningActive() && Input.GetMouseButton(this.itsPanningButton) && Vector2.Distance(Input.mousePosition, this.itsMapPanningMousePosLast) > this.itsPanningMinMouseDistanceStart)
			{
				this.ForcePanningStart();
			}
		}
		if (this.GetPanningActive())
		{
			if (Input.GetMouseButton(this.itsPanningButton))
			{
				float num = this.itsCamera.orthographicSize * 1f * this.itsCamera.aspect / this.itsTargetRect.width;
				float num2 = this.itsCamera.orthographicSize * 1f / this.itsTargetRect.height;
				Vector2 vector = Input.mousePosition - this.itsMapPanningMousePosLast;
				this.itsMapPanning -= new Vector2(vector.x * num, vector.y * num2) * 2f;
				this.itsMapPanningDest = this.itsMapPanning;
				this.itsMapPanningMousePosLast = Input.mousePosition;
			}
			if (Input.GetMouseButtonUp(this.itsPanningButton) && !this.GetFullscreen())
			{
				this.StopMapPanning();
			}
			if (this.itsMapPanning != this.itsMapPanningDest)
			{
				float x = Mathf.SmoothDamp(this.itsMapPanning.x, this.itsMapPanningDest.x, ref this.itsVelX, 0.1f);
				float y = Mathf.SmoothDamp(this.itsMapPanning.y, this.itsMapPanningDest.y, ref this.itsVelY, 0.1f);
				this.itsMapPanning = new Vector2(x, y);
				if (this.itsMapPanning.magnitude < 0.1f)
				{
					this.itsMapPanning = Vector2.zero;
					this.itsMapPanningDest = this.itsMapPanning;
					this.UpdateIconScale();
				}
			}
		}
	}

	// Token: 0x06000C5F RID: 3167 RVA: 0x0005ABB8 File Offset: 0x00058DB8
	private bool GetOutputPlaneActive()
	{
		return this.GetHasProVersion() && this.itsTextureRenderMaskCurrent != null;
	}

	// Token: 0x06000C60 RID: 3168 RVA: 0x0005ABD4 File Offset: 0x00058DD4
	private void LateUpdate()
	{
		if (this.GetOutputPlaneActive())
		{
			this.UpdateMinimapOutputPlane();
		}
	}

	// Token: 0x06000C61 RID: 3169 RVA: 0x0005ABE8 File Offset: 0x00058DE8
	private Vector3? GetMouseToWorldPointOnMap()
	{
		Vector2 point = Input.mousePosition;
		point.y = (float)Screen.height - point.y;
		if (!this.itsTargetRect.Contains(point))
		{
			return null;
		}
		if (this.itsRectFullscreen.Contains(point) || this.itsRectStatic.Contains(point) || this.itsRectZoomIn.Contains(point) || this.itsRectZoomOut.Contains(point))
		{
			return null;
		}
		Vector2 a = new Vector2((point.x - this.itsTargetRect.x) / this.itsTargetRect.width, (point.y - this.itsTargetRect.y) / this.itsTargetRect.height);
		Vector2 vector = a - new Vector2(0.5f, 0.5f);
		Vector3 value;
		if (this.itsDataModuleMinimap.itsGlobalSettings.itsOrientation == KGFMapSystem.KGFMapSystemOrientation.XZDefault)
		{
			value = this.itsCameraTransform.position + this.itsCameraTransform.up * vector.y * this.itsCamera.orthographicSize * -2f + this.itsCameraTransform.right * vector.x * this.itsCamera.orthographicSize * 2f * this.itsCamera.aspect;
		}
		else
		{
			value = this.itsCameraTransform.position + this.itsCameraTransform.up * vector.y * this.itsCamera.orthographicSize * -2f + this.itsCameraTransform.right * vector.x * this.itsCamera.orthographicSize * 2f * this.itsCamera.aspect;
		}
		return new Vector3?(value);
	}

	// Token: 0x06000C62 RID: 3170 RVA: 0x0005AE0C File Offset: 0x0005900C
	private void CheckForClicksOnMinimap()
	{
		this.CheckDeferedClickList();
		if (Input.GetMouseButtonDown(0))
		{
			this.itsSavedMouseDownPoint = Input.mousePosition;
		}
		if (Input.GetMouseButtonUp(0) && Vector3.Distance(Input.mousePosition, this.itsSavedMouseDownPoint) < 2f)
		{
			Vector3? mouseToWorldPointOnMap = this.GetMouseToWorldPointOnMap();
			if (mouseToWorldPointOnMap != null)
			{
				Vector3 vector = mouseToWorldPointOnMap.Value;
				vector = this.ChangeVectorHeight(vector, this.GetHeightFlags());
				if (this.itsDataModuleMinimap.itsGlobalSettings.itsOrientation == KGFMapSystem.KGFMapSystemOrientation.XZDefault)
				{
					this.EventClickedOnMinimap.Trigger(this, new KGFMapSystem.KGFClickEventArgs(new Vector3(vector.x, 0f, vector.z)));
				}
				else
				{
					this.EventClickedOnMinimap.Trigger(this, new KGFMapSystem.KGFClickEventArgs(new Vector3(vector.x, vector.y, 0f)));
				}
				if (this.itsDataModuleMinimap.itsUserFlags.itsActive)
				{
					this.itsDeferedClickList.Add(vector);
				}
			}
		}
	}

	// Token: 0x06000C63 RID: 3171 RVA: 0x0005AF14 File Offset: 0x00059114
	public void SetClickUsed()
	{
		this.itsClickUsedInFrame = Time.frameCount;
	}

	// Token: 0x06000C64 RID: 3172 RVA: 0x0005AF24 File Offset: 0x00059124
	private bool GetClickUsed()
	{
		return Math.Abs(this.itsClickUsedInFrame - Time.frameCount) <= 1;
	}

	// Token: 0x06000C65 RID: 3173 RVA: 0x0005AF40 File Offset: 0x00059140
	private void CheckDeferedClickList()
	{
		if (!this.GetClickUsed())
		{
			for (int i = 0; i < this.itsDeferedClickList.Count; i++)
			{
				Vector3 vector = this.itsDeferedClickList[i];
				if (this.itsDataModuleMinimap.itsUserFlags.itsMapIcon != null)
				{
					this.EventUserFlagCreated.Trigger(this, new KGFMapSystem.KGFFlagEventArgs(vector));
					this.LogInfo(string.Format("Added user flag at {0}", vector), base.name, this);
					KGFMapIcon kgfmapIcon = this.CreateIconInternal(vector, this.itsDataModuleMinimap.itsUserFlags.itsMapIcon, this.itsContainerFlags);
					this.itsListClickIcons.Add(kgfmapIcon);
					this.UpdateIconLayer(kgfmapIcon);
				}
			}
		}
		this.itsDeferedClickList.Clear();
	}

	// Token: 0x06000C66 RID: 3174 RVA: 0x0005B008 File Offset: 0x00059208
	private void UpdateMapIconHover(out bool theClickOnIcon)
	{
		theClickOnIcon = false;
		Vector2 point = Input.mousePosition;
		point.y = (float)Screen.height - point.y;
		if (this.itsTargetRect.Contains(point) && !this.itsMouseEnteredMap)
		{
			this.itsMouseEnteredMap = true;
			this.EventMouseMapEntered.Trigger(this);
			this.LogInfo("Mouse entered map", base.name, this);
		}
		else if (!this.itsTargetRect.Contains(point) && this.itsMouseEnteredMap)
		{
			this.itsMouseEnteredMap = false;
			this.EventMouseMapLeft.Trigger(this);
			this.LogInfo("Mouse left map", base.name, this);
		}
		if (this.itsMapIconHoveredCurrent != null && (MonoBehaviour)this.itsMapIconHoveredCurrent.itsMapIcon == null)
		{
			this.itsMapIconHoveredCurrent = null;
		}
		Vector3? mouseToWorldPointOnMap = this.GetMouseToWorldPointOnMap();
		if (mouseToWorldPointOnMap != null)
		{
			Vector3 vector = mouseToWorldPointOnMap.Value;
			vector = this.ChangeVectorHeight(vector, this.GetHeightIcons());
			bool flag = false;
			for (int i = 0; i < this.itsListMapIcons.Count; i++)
			{
				KGFMapSystem.mapicon_listitem_script mapicon_listitem_script = this.itsListMapIcons[i];
				float num = mapicon_listitem_script.GetRepresentationSize().magnitude / 2f;
				if (Vector3.Distance(mapicon_listitem_script.itsRepresentationInstanceTransform.position, vector) < num)
				{
					this.MapIconEnter(mapicon_listitem_script);
					flag = true;
					break;
				}
			}
			if (this.itsMapIconHoveredCurrent != null && Input.GetMouseButtonUp(0))
			{
				theClickOnIcon = true;
				this.MapIconClick(this.itsMapIconHoveredCurrent);
			}
			if (!flag && this.itsMapIconHoveredCurrent != null)
			{
				this.MapIconLeave(this.itsMapIconHoveredCurrent);
			}
		}
	}

	// Token: 0x06000C67 RID: 3175 RVA: 0x0005B1C8 File Offset: 0x000593C8
	private void MapIconEnter(KGFMapSystem.mapicon_listitem_script theMapIcon)
	{
		if (this.itsMapIconHoveredCurrent != theMapIcon)
		{
			this.LogInfo("Mouse entered map icon:" + theMapIcon.itsMapIcon.GetGameObjectName(), base.name, this);
			this.itsMapIconHoveredCurrent = theMapIcon;
			this.EventMouseMapIconEntered.Trigger(this, new KGFMapSystem.KGFMarkerEventArgs(theMapIcon.itsMapIcon));
		}
	}

	// Token: 0x06000C68 RID: 3176 RVA: 0x0005B224 File Offset: 0x00059424
	private void MapIconLeave(KGFMapSystem.mapicon_listitem_script theMapIcon)
	{
		if (theMapIcon != null)
		{
			this.LogInfo("Mouse left map icon:" + theMapIcon.itsMapIcon.GetGameObjectName(), base.name, this);
			this.itsMapIconHoveredCurrent = null;
			this.EventMouseMapIconLeft.Trigger(this, new KGFMapSystem.KGFMarkerEventArgs(theMapIcon.itsMapIcon));
		}
	}

	// Token: 0x06000C69 RID: 3177 RVA: 0x0005B278 File Offset: 0x00059478
	private void MapIconClick(KGFMapSystem.mapicon_listitem_script theMapIcon)
	{
		if (this.itsListClickIcons.Contains(theMapIcon.itsMapIcon))
		{
			this.RemoveClickMarker(theMapIcon);
		}
		else
		{
			this.LogInfo("Click on map icon:" + theMapIcon.itsMapIcon.GetGameObjectName(), base.name, this);
			this.EventMouseMapIconClicked.Trigger(this, new KGFMapSystem.KGFMarkerEventArgs(theMapIcon.itsMapIcon));
		}
	}

	// Token: 0x06000C6A RID: 3178 RVA: 0x0005B2E0 File Offset: 0x000594E0
	private void RemoveClickMarker(KGFMapSystem.mapicon_listitem_script theMapIcon)
	{
		UnityEngine.Object.Destroy(((MonoBehaviour)theMapIcon.itsMapIcon).gameObject);
	}

	// Token: 0x06000C6B RID: 3179 RVA: 0x0005B2F8 File Offset: 0x000594F8
	private Rect GetBounds2DPanning()
	{
		if (this.itsDataModuleMinimap.itsGlobalSettings.itsOrientation == KGFMapSystem.KGFMapSystemOrientation.XZDefault)
		{
			return new Rect(this.itsTerrainBoundsPanning.min.x, this.itsTerrainBoundsPanning.min.z, this.itsTerrainBoundsPanning.size.x, this.itsTerrainBoundsPanning.size.z);
		}
		return new Rect(this.itsTerrainBoundsPanning.min.x, this.itsTerrainBoundsPanning.min.y, this.itsTerrainBoundsPanning.size.x, this.itsTerrainBoundsPanning.size.y);
	}

	// Token: 0x06000C6C RID: 3180 RVA: 0x0005B3C4 File Offset: 0x000595C4
	private Vector2 ClampPoint(Vector2 thePoint, Rect theArea, float theBorderX, float theBorderY)
	{
		if (thePoint.x < theArea.x + theBorderX)
		{
			thePoint.x = theArea.x + theBorderX;
		}
		if (thePoint.y < theArea.y + theBorderY)
		{
			thePoint.y = theArea.y + theBorderY;
		}
		if (thePoint.x > theArea.xMax - theBorderX)
		{
			thePoint.x = theArea.xMax - theBorderX;
		}
		if (thePoint.y > theArea.yMax - theBorderY)
		{
			thePoint.y = theArea.yMax - theBorderY;
		}
		return thePoint;
	}

	// Token: 0x06000C6D RID: 3181 RVA: 0x0005B46C File Offset: 0x0005966C
	private Vector2 GetCurrentCameraPoint2D()
	{
		Vector2 vector;
		if (this.itsDataModuleMinimap.itsGlobalSettings.itsOrientation == KGFMapSystem.KGFMapSystemOrientation.XZDefault)
		{
			vector = new Vector2(this.itsTargetTransform.position.x, this.itsTargetTransform.position.z);
		}
		else
		{
			vector = new Vector2(this.itsTargetTransform.position.x, this.itsTargetTransform.position.y);
		}
		if (!this.itsDataModuleMinimap.itsPanning.itsActive)
		{
			return vector;
		}
		float num = 0f;
		if (this.itsDataModuleMinimap.itsGlobalSettings.itsIsStatic)
		{
			num = this.itsCurrentZoom;
		}
		if (this.itsDataModuleMinimap.itsPanning.itsUseBounds)
		{
			Rect bounds2DPanning = this.GetBounds2DPanning();
			vector = this.ClampPoint(vector, bounds2DPanning, num * this.itsCamera.aspect, num);
			Vector2 b = this.RotateVector2(this.itsMapPanning, this.itsRotation);
			Vector2 thePoint = vector + b;
			Vector2 vector2 = this.ClampPoint(thePoint, bounds2DPanning, num * this.itsCamera.aspect, num);
			Vector2 theVector = vector2 - vector;
			this.itsMapPanning = this.RotateVector2(theVector, -this.itsRotation);
			return vector2;
		}
		Vector2 b2 = this.RotateVector2(this.itsMapPanning, this.itsRotation);
		return vector + b2;
	}

	// Token: 0x06000C6E RID: 3182 RVA: 0x0005B5D8 File Offset: 0x000597D8
	private Vector2 RotateVector2(Vector2 theVector, float theRotation)
	{
		Vector2 zero = Vector2.zero;
		zero.x = theVector.x * Mathf.Cos(theRotation) - theVector.y * Mathf.Sin(theRotation);
		zero.y = theVector.x * Mathf.Sin(theRotation) + theVector.y * Mathf.Cos(theRotation);
		return zero;
	}

	// Token: 0x06000C6F RID: 3183 RVA: 0x0005B634 File Offset: 0x00059834
	private Vector3 GetCurrentCameraPoint3D()
	{
		Vector2 currentCameraPoint2D = this.GetCurrentCameraPoint2D();
		return this.CreateVector(currentCameraPoint2D.x, currentCameraPoint2D.y, this.GetTerrainHeight(5f));
	}

	// Token: 0x06000C70 RID: 3184 RVA: 0x0005B668 File Offset: 0x00059868
	private void UpdateMapIconRotation()
	{
		if (this.itsDataModuleMinimap.itsGlobalSettings.itsOrientation == KGFMapSystem.KGFMapSystemOrientation.XZDefault)
		{
			if (this.itsDataModuleMinimap.itsGlobalSettings.itsIsStatic)
			{
				this.itsCameraTransform.eulerAngles = this.CreateVector(0f, 0f, this.itsDataModuleMinimap.itsGlobalSettings.itsStaticNorth);
				this.itsCameraTransform.Rotate(90f, 0f, 0f);
			}
			else
			{
				Vector3 forward = this.itsTargetTransform.forward;
				forward.y = 0f;
				forward.Normalize();
				this.itsCameraTransform.rotation = Quaternion.LookRotation(forward, Vector3.up);
				this.itsCameraTransform.Rotate(90f, 0f, 0f);
			}
		}
		else
		{
			this.itsCameraTransform.eulerAngles = new Vector3(0f, 0f, 0f);
		}
		this.itsRotation = -1f * this.itsCameraTransform.eulerAngles.y * 0.017453292f;
		this.itsCameraTransform.position = this.GetCurrentCameraPoint3D();
		for (int i = this.itsListMapIcons.Count - 1; i >= 0; i--)
		{
			KGFMapSystem.mapicon_listitem_script mapicon_listitem_script = this.itsListMapIcons[i];
			if (mapicon_listitem_script.itsMapIconTransform == null)
			{
				this.itsListMapIcons.RemoveAt(i);
			}
			else if (mapicon_listitem_script.GetMapIconVisibilityEffective())
			{
				if (this.itsDataModuleMinimap.itsGlobalSettings.itsOrientation == KGFMapSystem.KGFMapSystemOrientation.XZDefault)
				{
					if (mapicon_listitem_script.itsRotate)
					{
						mapicon_listitem_script.itsRepresentationInstanceTransform.eulerAngles = new Vector3(0f, mapicon_listitem_script.itsMapIconTransform.eulerAngles.y, 0f);
					}
					else
					{
						mapicon_listitem_script.itsRepresentationInstanceTransform.eulerAngles = new Vector3(0f, this.itsCameraTransform.eulerAngles.y, 0f);
					}
				}
				else if (mapicon_listitem_script.itsRotate)
				{
					mapicon_listitem_script.itsRepresentationInstanceTransform.eulerAngles = new Vector3(mapicon_listitem_script.itsMapIconTransform.eulerAngles.z - 90f, 270f, 270f);
				}
				else
				{
					mapicon_listitem_script.itsRepresentationInstanceTransform.eulerAngles = new Vector3(270f, 0f, 0f);
				}
				mapicon_listitem_script.itsRepresentationInstanceTransform.position = this.ChangeVectorHeight(mapicon_listitem_script.itsMapIconTransform.position, this.GetHeightIcons());
				if (mapicon_listitem_script.itsRepresentationArrowInstance != null)
				{
					Vector3 vector = this.itsTargetTransform.position - mapicon_listitem_script.itsRepresentationInstanceTransform.position;
					vector = this.ChangeVectorHeight(vector, 0f);
					bool flag = vector.magnitude > this.GetCurrentRange();
					if (flag != mapicon_listitem_script.GetIsArrowVisible())
					{
						mapicon_listitem_script.ShowArrow(flag);
						if (flag)
						{
							this.LogInfo(string.Format("Icon '{0}' got invisible", mapicon_listitem_script.itsMapIconTransform.name), base.name, this);
						}
						else
						{
							this.LogInfo(string.Format("Icon '{0}' got visible", mapicon_listitem_script.itsMapIconTransform.name), base.name, this);
						}
						this.EventVisibilityOnMinimapChanged.Trigger(this, new KGFMapSystem.KGFMarkerEventArgs(mapicon_listitem_script.itsMapIcon));
					}
					if (mapicon_listitem_script.GetIsArrowVisible())
					{
						float num;
						if (this.itsDataModuleMinimap.itsGlobalSettings.itsOrientation == KGFMapSystem.KGFMapSystemOrientation.XZDefault)
						{
							num = Vector3.Angle(Vector3.forward, vector);
						}
						else
						{
							num = Vector3.Angle(Vector3.up, vector);
						}
						if (Vector3.Dot(Vector3.right, vector) < 0f)
						{
							num = 360f - num;
						}
						num += 180f;
						float d = this.GetCurrentRange() * this.itsDataModuleMinimap.itsAppearanceMiniMap.itsRadiusArrows;
						float num2 = num - this.itsCameraTransform.localEulerAngles.y;
						Vector3 vector2 = this.itsCameraTransform.position + this.itsCameraTransform.right * d * this.itsCamera.aspect * Mathf.Sin(num2 * 0.017453292f) + this.itsCameraTransform.up * d * Mathf.Cos(num2 * 0.017453292f);
						vector2 = this.ChangeVectorHeight(vector2, this.GetHeightArrows());
						mapicon_listitem_script.itsRepresentationArrowInstanceTransform.position = vector2;
						if (this.itsDataModuleMinimap.itsGlobalSettings.itsOrientation == KGFMapSystem.KGFMapSystemOrientation.XZDefault)
						{
							mapicon_listitem_script.itsRepresentationArrowInstanceTransform.eulerAngles = new Vector3(0f, num, 0f);
						}
						else
						{
							mapicon_listitem_script.itsRepresentationArrowInstanceTransform.eulerAngles = new Vector3(num - 90f, 90f, 90f);
						}
					}
				}
			}
		}
	}

	// Token: 0x06000C71 RID: 3185 RVA: 0x0005BB48 File Offset: 0x00059D48
	public void MeasureScene()
	{
		this.GetMeasuredBounds(this.itsDataModuleMinimap.itsPanning.itsBoundsLayers, out this.itsTerrainBoundsPanning);
		this.GetMeasuredBounds(this.itsDataModuleMinimap.itsPhoto.itsPhotoLayers, out this.itsTerrainBoundsPhoto);
		this.itsSizeTerrain = this.GetVector3Plane(this.itsTerrainBoundsPhoto.size);
	}

	// Token: 0x06000C72 RID: 3186 RVA: 0x0005BBA8 File Offset: 0x00059DA8
	public bool GetIsVisibleOnMap(KGFIMapIcon theMapIcon)
	{
		for (int i = this.itsListMapIcons.Count - 1; i >= 0; i--)
		{
			KGFMapSystem.mapicon_listitem_script mapicon_listitem_script = this.itsListMapIcons[i];
			if (mapicon_listitem_script.itsMapIcon == theMapIcon)
			{
				return !mapicon_listitem_script.GetIsArrowVisible() && mapicon_listitem_script.GetMapIconVisibilityEffective();
			}
		}
		return false;
	}

	// Token: 0x06000C73 RID: 3187 RVA: 0x0005BC04 File Offset: 0x00059E04
	public KGFMapSystem.KGFMapSystemOrientation GetOrientation()
	{
		return this.itsDataModuleMinimap.itsGlobalSettings.itsOrientation;
	}

	// Token: 0x06000C74 RID: 3188 RVA: 0x0005BC18 File Offset: 0x00059E18
	public void UpdateStyles()
	{
		this.itsGuiStyleBack = new GUIStyle();
		this.itsGuiStyleBack.normal.background = this.itsDataModuleMinimap.itsAppearanceMiniMap.itsBackground;
		this.itsGuiStyleButton = new GUIStyle();
		this.itsGuiStyleButton.normal.background = this.itsDataModuleMinimap.itsAppearanceMiniMap.itsButton;
		this.itsGuiStyleButton.hover.background = this.itsDataModuleMinimap.itsAppearanceMiniMap.itsButtonHover;
		this.itsGuiStyleButton.active.background = this.itsDataModuleMinimap.itsAppearanceMiniMap.itsButtonDown;
		this.itsGuiStyleButtonFullscreen = new GUIStyle();
		this.itsGuiStyleButtonFullscreen.normal.background = this.itsDataModuleMinimap.itsAppearanceMap.itsButton;
		this.itsGuiStyleButtonFullscreen.hover.background = this.itsDataModuleMinimap.itsAppearanceMap.itsButtonHover;
		this.itsGuiStyleButtonFullscreen.active.background = this.itsDataModuleMinimap.itsAppearanceMap.itsButtonDown;
	}

	// Token: 0x06000C75 RID: 3189 RVA: 0x0005BD28 File Offset: 0x00059F28
	public void UpdateIcon(KGFIMapIcon theIcon)
	{
		foreach (KGFMapSystem.mapicon_listitem_script mapicon_listitem_script in this.itsListMapIcons)
		{
			if (mapicon_listitem_script.itsMapIcon == theIcon)
			{
				mapicon_listitem_script.UpdateIcon();
			}
		}
	}

	// Token: 0x06000C76 RID: 3190 RVA: 0x0005BD9C File Offset: 0x00059F9C
	public string GetSaveString()
	{
		return this.SerializeFogOfWar();
	}

	// Token: 0x06000C77 RID: 3191 RVA: 0x0005BDA4 File Offset: 0x00059FA4
	public void LoadFromString(string theSavedString)
	{
		this.DeserializeFogOfWar(theSavedString);
	}

	// Token: 0x06000C78 RID: 3192 RVA: 0x0005BDB0 File Offset: 0x00059FB0
	public KGFMapIcon CreateIcon(Vector3 theWorldPoint, KGFMapIcon theMapIcon)
	{
		KGFMapIcon kgfmapIcon = this.CreateIconInternal(theWorldPoint, theMapIcon, this.itsContainerUser);
		this.itsListUserIcons.Add(kgfmapIcon);
		return kgfmapIcon;
	}

	// Token: 0x06000C79 RID: 3193 RVA: 0x0005BDDC File Offset: 0x00059FDC
	public void RemoveIcon(KGFMapIcon theIcon)
	{
		if (this.itsListUserIcons.Contains(theIcon))
		{
			this.UnregisterMapIcon(theIcon);
			this.itsListUserIcons.Remove(theIcon);
		}
		else
		{
			this.LogError("Not a user created icon", base.name, this);
		}
	}

	// Token: 0x06000C7A RID: 3194 RVA: 0x0005BE28 File Offset: 0x0005A028
	public KGFMapIcon[] GetUserIcons()
	{
		return this.itsListUserIcons.ToArray();
	}

	// Token: 0x06000C7B RID: 3195 RVA: 0x0005BE38 File Offset: 0x0005A038
	public KGFMapIcon[] GetUserFlags()
	{
		List<KGFMapIcon> list = new List<KGFMapIcon>();
		foreach (object obj in this.itsContainerFlags.transform)
		{
			Transform transform = (Transform)obj;
			list.Add(transform.GetComponent<KGFMapIcon>());
		}
		return list.ToArray();
	}

	// Token: 0x06000C7C RID: 3196 RVA: 0x0005BEC0 File Offset: 0x0005A0C0
	public void SetMinimapEnabled(bool theEnable)
	{
		if (this.itsMinimapActive != theEnable)
		{
			this.itsMinimapActive = theEnable;
			KGFMapSystem.KGFSetChildrenActiveRecursively(base.gameObject, theEnable);
			KGFMapSystem.KGFSetChildrenActiveRecursively(this.itsMinimapPlane, theEnable);
			this.LogInfo("New map system state:" + theEnable, base.name, this);
		}
	}

	// Token: 0x06000C7D RID: 3197 RVA: 0x0005BF18 File Offset: 0x0005A118
	public void RefreshIconsVisibility()
	{
		foreach (KGFMapSystem.mapicon_listitem_script mapicon_listitem_script in this.itsListMapIcons)
		{
			mapicon_listitem_script.UpdateVisibility();
		}
	}

	// Token: 0x06000C7E RID: 3198 RVA: 0x0005BF80 File Offset: 0x0005A180
	public void SetIconsVisibleByCategory(string theCategory, bool theVisible)
	{
		this.LogInfo(string.Format("Icon category '{0}' changed visibility to: {1}", theCategory, theVisible), base.name, this);
		foreach (KGFMapSystem.mapicon_listitem_script mapicon_listitem_script in this.itsListMapIcons)
		{
			if (mapicon_listitem_script.itsMapIcon.GetCategory() == theCategory)
			{
				mapicon_listitem_script.SetVisibility(theVisible);
			}
		}
	}

	// Token: 0x06000C7F RID: 3199 RVA: 0x0005C01C File Offset: 0x0005A21C
	public void SetTarget(GameObject theTarget)
	{
		if (theTarget == null)
		{
			this.LogError("Assign your character to KGFMapsystem.itsTarget. KGFMapSystem will not work without a target.", base.name, this);
			return;
		}
		this.itsDataModuleMinimap.itsGlobalSettings.itsTarget = theTarget;
		this.itsTargetTransform = theTarget.transform;
	}

	// Token: 0x06000C80 RID: 3200 RVA: 0x0005C068 File Offset: 0x0005A268
	public float GetCurrentRange()
	{
		return this.itsCamera.orthographicSize;
	}

	// Token: 0x06000C81 RID: 3201 RVA: 0x0005C078 File Offset: 0x0005A278
	public void SetModeStatic(bool theModeStatic)
	{
		this.itsDataModuleMinimap.itsGlobalSettings.itsIsStatic = theModeStatic;
	}

	// Token: 0x06000C82 RID: 3202 RVA: 0x0005C08C File Offset: 0x0005A28C
	public bool GetModeStatic()
	{
		return this.itsDataModuleMinimap.itsGlobalSettings.itsIsStatic;
	}

	// Token: 0x06000C83 RID: 3203 RVA: 0x0005C0A0 File Offset: 0x0005A2A0
	public void SetMinimapSize(float theSize)
	{
		this.itsDataModuleMinimap.itsAppearanceMiniMap.itsSize = theSize;
		this.UpdateOrthographicSize();
	}

	// Token: 0x06000C84 RID: 3204 RVA: 0x0005C0BC File Offset: 0x0005A2BC
	public bool GetFullscreen()
	{
		return this.itsModeFullscreen;
	}

	// Token: 0x06000C85 RID: 3205 RVA: 0x0005C0C4 File Offset: 0x0005A2C4
	public void SetFullscreen(bool theFullscreenMode)
	{
		if (theFullscreenMode)
		{
			Vector2? vector = this.itsSavedResolution;
			if (vector == null)
			{
				this.itsSavedResolution = new Vector2?(new Vector2(this.GetWidth(), this.GetHeight()));
				goto IL_6B;
			}
		}
		if (!theFullscreenMode)
		{
			Vector2? vector2 = this.itsSavedResolution;
			if (vector2 != null)
			{
				this.itsSavedResolution = null;
				goto IL_6B;
			}
		}
		return;
		IL_6B:
		this.itsModeFullscreen = theFullscreenMode;
		this.UpdateTargetRect();
		this.UpdateMaskTexture();
		this.UpdateOrthographicSize();
		this.UpdateIconScale();
		this.EventFullscreenModeChanged.Trigger(this, EventArgs.Empty);
	}

	// Token: 0x06000C86 RID: 3206 RVA: 0x0005C16C File Offset: 0x0005A36C
	private GUIStyle GetButtonStyle()
	{
		if (this.GetFullscreen())
		{
			return this.itsGuiStyleButtonFullscreen;
		}
		return this.itsGuiStyleButton;
	}

	// Token: 0x06000C87 RID: 3207 RVA: 0x0005C188 File Offset: 0x0005A388
	private bool DrawButton(Rect theRect, Texture2D theTexture)
	{
		return !(theTexture == null) && GUI.Button(theRect, theTexture, this.GetButtonStyle());
	}

	// Token: 0x06000C88 RID: 3208 RVA: 0x0005C1A8 File Offset: 0x0005A3A8
	private void UpdateTargetRect()
	{
		switch (this.itsDataModuleMinimap.itsAppearanceMiniMap.itsAlignmentHorizontal)
		{
		case KGFAlignmentHorizontal.Left:
			this.itsTargetRect.x = 0f;
			this.itsTargetRect.x = this.itsTargetRect.x + ((float)Screen.width - this.GetWidth()) * this.itsDataModuleMinimap.itsAppearanceMiniMap.itsMarginHorizontal;
			break;
		case KGFAlignmentHorizontal.Middle:
			this.itsTargetRect.x = ((float)Screen.width - this.GetWidth()) / 2f;
			break;
		case KGFAlignmentHorizontal.Right:
			this.itsTargetRect.x = (float)Screen.width - this.GetWidth();
			this.itsTargetRect.x = this.itsTargetRect.x - ((float)Screen.width - this.GetWidth()) * this.itsDataModuleMinimap.itsAppearanceMiniMap.itsMarginHorizontal;
			break;
		}
		switch (this.itsDataModuleMinimap.itsAppearanceMiniMap.itsAlignmentVertical)
		{
		case KGFAlignmentVertical.Top:
			this.itsTargetRect.y = 0f;
			this.itsTargetRect.y = this.itsTargetRect.y + ((float)Screen.height - this.GetHeight()) * this.itsDataModuleMinimap.itsAppearanceMiniMap.itsMarginVertical;
			break;
		case KGFAlignmentVertical.Middle:
			this.itsTargetRect.y = ((float)Screen.height - this.GetHeight()) / 2f;
			break;
		case KGFAlignmentVertical.Bottom:
			this.itsTargetRect.y = (float)Screen.height - this.GetHeight();
			this.itsTargetRect.y = this.itsTargetRect.y - ((float)Screen.height - this.GetHeight()) * this.itsDataModuleMinimap.itsAppearanceMiniMap.itsMarginVertical;
			break;
		}
		this.itsTargetRect.width = this.GetWidth();
		this.itsTargetRect.height = this.GetHeight();
	}

	// Token: 0x06000C89 RID: 3209 RVA: 0x0005C394 File Offset: 0x0005A594
	public void RenderGUI()
	{
		if (this.itsErrorMode)
		{
			GUIStyle guistyle = new GUIStyle();
			guistyle.alignment = TextAnchor.MiddleCenter;
			guistyle.wordWrap = true;
			guistyle.normal.textColor = Color.red;
			GUI.Label(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height), "Please click on the KGFMapSystem gameobject and fix all the errors displayed in the inspector.", guistyle);
			return;
		}
		if (!this.itsMinimapActive)
		{
			return;
		}
		this.UpdateTargetRect();
		if (!this.itsDataModuleMinimap.itsGlobalSettings.itsHideGUI)
		{
			this.RenderMainGUI();
		}
		this.RenderToolTip();
	}

	// Token: 0x06000C8A RID: 3210 RVA: 0x0005C42C File Offset: 0x0005A62C
	private void RenderMainGUI()
	{
		float buttonSize = this.GetButtonSize();
		float buttonPadding = this.GetButtonPadding();
		if (this.GetFullscreen())
		{
			this.itsGuiStyleBack.normal.background = this.itsDataModuleMinimap.itsAppearanceMap.itsBackground;
			this.itsGuiStyleBack.border = new RectOffset(this.itsDataModuleMinimap.itsAppearanceMap.itsBackgroundBorder, this.itsDataModuleMinimap.itsAppearanceMap.itsBackgroundBorder, this.itsDataModuleMinimap.itsAppearanceMap.itsBackgroundBorder, this.itsDataModuleMinimap.itsAppearanceMap.itsBackgroundBorder);
		}
		else
		{
			this.itsGuiStyleBack.normal.background = this.itsDataModuleMinimap.itsAppearanceMiniMap.itsBackground;
			this.itsGuiStyleBack.border = new RectOffset(this.itsDataModuleMinimap.itsAppearanceMiniMap.itsBackgroundBorder, this.itsDataModuleMinimap.itsAppearanceMiniMap.itsBackgroundBorder, this.itsDataModuleMinimap.itsAppearanceMiniMap.itsBackgroundBorder, this.itsDataModuleMinimap.itsAppearanceMiniMap.itsBackgroundBorder);
		}
		GUI.Box(this.itsTargetRect, string.Empty, this.itsGuiStyleBack);
		if (this.GetFullscreen())
		{
			int num = (int)(this.itsDataModuleMinimap.itsAppearanceMap.itsButtonSpace * this.GetWidth());
			int num2 = 4;
			int num3 = (int)((float)((num2 - 1) * num) + this.GetButtonSize() * (float)num2);
			int num4 = (int)this.GetButtonSize();
			Rect rect = default(Rect);
			switch (this.itsDataModuleMinimap.itsAppearanceMap.itsAlignmentHorizontal)
			{
			case KGFAlignmentHorizontal.Left:
				rect.x = this.itsTargetRect.x + buttonPadding;
				break;
			case KGFAlignmentHorizontal.Middle:
				if (this.itsDataModuleMinimap.itsAppearanceMap.itsOrientation == KGFOrientation.Horizontal)
				{
					rect.x = (this.itsTargetRect.xMax - this.itsTargetRect.xMin) / 2f - (float)(num3 / 2);
				}
				else
				{
					rect.x = (this.itsTargetRect.xMax - this.itsTargetRect.xMin) / 2f - (float)(num4 / 2);
				}
				break;
			case KGFAlignmentHorizontal.Right:
				if (this.itsDataModuleMinimap.itsAppearanceMap.itsOrientation == KGFOrientation.Horizontal)
				{
					rect.x = this.itsTargetRect.xMax - (float)num3 - buttonPadding;
				}
				else
				{
					rect.x = this.itsTargetRect.xMax - (float)num4 - buttonPadding;
				}
				break;
			}
			switch (this.itsDataModuleMinimap.itsAppearanceMap.itsAlignmentVertical)
			{
			case KGFAlignmentVertical.Top:
				rect.y = this.itsTargetRect.y + buttonPadding;
				break;
			case KGFAlignmentVertical.Middle:
				if (this.itsDataModuleMinimap.itsAppearanceMap.itsOrientation == KGFOrientation.Horizontal)
				{
					rect.y = (this.itsTargetRect.yMax - this.itsTargetRect.yMin) / 2f - (float)(num4 / 2);
				}
				else
				{
					rect.y = (this.itsTargetRect.yMax - this.itsTargetRect.yMin) / 2f - (float)(num3 / 2);
				}
				break;
			case KGFAlignmentVertical.Bottom:
				if (this.itsDataModuleMinimap.itsAppearanceMap.itsOrientation == KGFOrientation.Horizontal)
				{
					rect.y = this.itsTargetRect.yMax - (float)num4 - buttonPadding;
				}
				else
				{
					rect.y = this.itsTargetRect.yMax - (float)num3 - buttonPadding;
				}
				break;
			}
			rect.width = this.GetButtonSize();
			rect.height = this.GetButtonSize();
			this.itsRectZoomIn = (this.itsRectZoomOut = (this.itsRectStatic = (this.itsRectFullscreen = rect)));
			if (this.itsDataModuleMinimap.itsAppearanceMap.itsOrientation == KGFOrientation.Horizontal)
			{
				this.itsRectZoomOut.x = this.itsRectZoomIn.x + (float)num + this.GetButtonSize();
				this.itsRectStatic.x = this.itsRectZoomOut.x + (float)num + this.GetButtonSize();
				this.itsRectFullscreen.x = this.itsRectStatic.x + (float)num + this.GetButtonSize();
			}
			else
			{
				this.itsRectZoomOut.y = this.itsRectZoomIn.y + (float)num + this.GetButtonSize();
				this.itsRectStatic.y = this.itsRectZoomOut.y + (float)num + this.GetButtonSize();
				this.itsRectFullscreen.y = this.itsRectStatic.y + (float)num + this.GetButtonSize();
			}
		}
		else
		{
			this.itsRectZoomIn = new Rect(this.itsTargetRect.x + buttonPadding, this.itsTargetRect.y + buttonPadding, buttonSize, buttonSize);
			this.itsRectZoomOut = new Rect(this.itsTargetRect.x + buttonPadding, this.itsTargetRect.y + this.itsTargetRect.height - buttonSize - buttonPadding, buttonSize, buttonSize);
			this.itsRectStatic = new Rect(this.itsTargetRect.x + this.itsTargetRect.width - buttonSize - buttonPadding, this.itsTargetRect.y + buttonPadding, buttonSize, buttonSize);
			this.itsRectFullscreen = new Rect(this.itsTargetRect.x + this.itsTargetRect.width - buttonSize - buttonPadding, this.itsTargetRect.y + this.itsTargetRect.height - buttonSize - buttonPadding, buttonSize, buttonSize);
		}
		if (this.DrawButton(this.itsRectZoomIn, (!this.GetFullscreen()) ? this.itsDataModuleMinimap.itsAppearanceMiniMap.itsIconZoomIn : this.itsDataModuleMinimap.itsAppearanceMap.itsIconZoomIn))
		{
			this.ZoomIn();
		}
		if (this.DrawButton(this.itsRectZoomOut, (!this.GetFullscreen()) ? this.itsDataModuleMinimap.itsAppearanceMiniMap.itsIconZoomOut : this.itsDataModuleMinimap.itsAppearanceMap.itsIconZoomOut))
		{
			this.ZoomOut();
		}
		if (this.DrawButton(this.itsRectStatic, (!this.GetFullscreen()) ? this.itsDataModuleMinimap.itsAppearanceMiniMap.itsIconZoomLock : this.itsDataModuleMinimap.itsAppearanceMap.itsIconZoomLock))
		{
			this.SetModeStatic(!this.GetModeStatic());
		}
		if (this.DrawButton(this.itsRectFullscreen, (!this.GetFullscreen()) ? this.itsDataModuleMinimap.itsAppearanceMiniMap.itsIconFullscreen : this.itsDataModuleMinimap.itsAppearanceMap.itsIconFullscreen))
		{
			this.SetFullscreen(!this.GetFullscreen());
		}
	}

	// Token: 0x06000C8B RID: 3211 RVA: 0x0005CAC8 File Offset: 0x0005ACC8
	public void SetRenderToolTipMethod(KGFMapSystem.RenderToolTipMethodType theMethod)
	{
		this.itsRenderToolTipMethod = theMethod;
	}

	// Token: 0x06000C8C RID: 3212 RVA: 0x0005CAD4 File Offset: 0x0005ACD4
	public void ResetRenderToolTipMethod()
	{
		this.itsRenderToolTipMethod = null;
	}

	// Token: 0x06000C8D RID: 3213 RVA: 0x0005CAE0 File Offset: 0x0005ACE0
	private void RenderToolTip()
	{
		if (!this.GetHoverWithoutButtons())
		{
			return;
		}
		if (this.GetPanningMoveActive())
		{
			return;
		}
		if (this.itsMapIconHoveredCurrent != null && this.itsDataModuleMinimap.itsToolTip.itsActive && this.itsMapIconHoveredCurrent.itsMapIcon != null)
		{
			if (this.itsRenderToolTipMethod != null)
			{
				this.itsRenderToolTipMethod(this.itsMapIconHoveredCurrent.itsMapIcon.GetToolTipText());
			}
			else
			{
				this.RenderToolTipMethodDefault(this.itsMapIconHoveredCurrent.itsMapIcon.GetToolTipText());
			}
		}
	}

	// Token: 0x06000C8E RID: 3214 RVA: 0x0005CB78 File Offset: 0x0005AD78
	private void RenderToolTipMethodDefault(string theText)
	{
		if (string.IsNullOrEmpty(theText))
		{
			return;
		}
		GUIStyle guistyle = new GUIStyle();
		guistyle.normal.background = this.itsDataModuleMinimap.itsToolTip.itsTextureBackground;
		guistyle.normal.textColor = this.itsDataModuleMinimap.itsToolTip.itsColorText;
		guistyle.font = this.itsDataModuleMinimap.itsToolTip.itsFontText;
		guistyle.border = this.itsDataModuleMinimap.itsToolTip.itsBackgroundBorder;
		guistyle.padding = this.itsDataModuleMinimap.itsToolTip.itsBackgroundPadding;
		Vector2 vector = Input.mousePosition;
		Vector2 vector2 = guistyle.CalcSize(new GUIContent(theText)) + new Vector2((float)(this.itsDataModuleMinimap.itsToolTip.itsBackgroundPadding.left + this.itsDataModuleMinimap.itsToolTip.itsBackgroundPadding.right), (float)(this.itsDataModuleMinimap.itsToolTip.itsBackgroundPadding.top + this.itsDataModuleMinimap.itsToolTip.itsBackgroundPadding.bottom));
		GUI.Label(new Rect(vector.x - vector2.x / 2f, (float)Screen.height - vector.y + 15f, vector2.x, vector2.y), theText, guistyle);
	}

	// Token: 0x06000C8F RID: 3215 RVA: 0x0005CCCC File Offset: 0x0005AECC
	private void UpdateOrthographicSize()
	{
		this.itsCamera.orthographicSize = this.itsCurrentZoom;
		this.itsCamera.aspect = this.GetWidth() / this.GetHeight();
	}

	// Token: 0x06000C90 RID: 3216 RVA: 0x0005CD04 File Offset: 0x0005AF04
	private void CorrectCurrentZoom()
	{
		this.itsCurrentZoom = Mathf.Min(this.itsCurrentZoom, this.itsDataModuleMinimap.itsZoom.itsZoomMax);
		this.itsCurrentZoom = Mathf.Max(this.itsCurrentZoom, this.itsDataModuleMinimap.itsZoom.itsZoomMin);
		this.itsCurrentZoomDest = Mathf.Min(this.itsCurrentZoomDest, this.itsDataModuleMinimap.itsZoom.itsZoomMax);
		this.itsCurrentZoomDest = Mathf.Max(this.itsCurrentZoomDest, this.itsDataModuleMinimap.itsZoom.itsZoomMin);
	}

	// Token: 0x06000C91 RID: 3217 RVA: 0x0005CD98 File Offset: 0x0005AF98
	public float GetZoom()
	{
		return this.itsCurrentZoomDest;
	}

	// Token: 0x06000C92 RID: 3218 RVA: 0x0005CDA0 File Offset: 0x0005AFA0
	public void SetZoom(float theZoom)
	{
		this.SetZoom(theZoom, true);
	}

	// Token: 0x06000C93 RID: 3219 RVA: 0x0005CDAC File Offset: 0x0005AFAC
	public void SetZoom(float theZoom, bool theAnimate)
	{
		if (!theAnimate)
		{
			this.itsCurrentZoom = theZoom;
		}
		this.itsCurrentZoomDest = theZoom;
		this.CorrectCurrentZoom();
		this.UpdateOrthographicSize();
		this.UpdateIconScale();
	}

	// Token: 0x06000C94 RID: 3220 RVA: 0x0005CDE0 File Offset: 0x0005AFE0
	private void UpdateZoom()
	{
		if (this.itsCurrentZoomDest != this.itsCurrentZoom)
		{
			this.itsCurrentZoom = Mathf.SmoothDamp(this.itsCurrentZoom, this.itsCurrentZoomDest, ref this.itsZoomChangeVelocity, 0.3f);
			if (Mathf.Abs(this.itsCurrentZoomDest - this.itsCurrentZoom) < 0.05f)
			{
				this.itsCurrentZoom = this.itsCurrentZoomDest;
			}
			this.CorrectCurrentZoom();
			this.UpdateOrthographicSize();
			this.UpdateIconScale();
		}
	}

	// Token: 0x06000C95 RID: 3221 RVA: 0x0005CE5C File Offset: 0x0005B05C
	public void ZoomOut()
	{
		this.SetZoom(this.GetZoom() + this.itsDataModuleMinimap.itsZoom.itsZoomChangeValue);
	}

	// Token: 0x06000C96 RID: 3222 RVA: 0x0005CE7C File Offset: 0x0005B07C
	public void ZoomIn()
	{
		this.SetZoom(this.GetZoom() - this.itsDataModuleMinimap.itsZoom.itsZoomChangeValue);
	}

	// Token: 0x06000C97 RID: 3223 RVA: 0x0005CE9C File Offset: 0x0005B09C
	public void ZoomMin()
	{
		this.SetZoom(this.itsDataModuleMinimap.itsZoom.itsZoomMin);
	}

	// Token: 0x06000C98 RID: 3224 RVA: 0x0005CEB4 File Offset: 0x0005B0B4
	public void ZoomMax()
	{
		this.SetZoom(this.itsDataModuleMinimap.itsZoom.itsZoomMax);
	}

	// Token: 0x06000C99 RID: 3225 RVA: 0x0005CECC File Offset: 0x0005B0CC
	public void SetViewportEnabled(bool theEnable)
	{
		this.itsDataModuleMinimap.itsViewport.itsActive = theEnable;
	}

	// Token: 0x06000C9A RID: 3226 RVA: 0x0005CEE0 File Offset: 0x0005B0E0
	public bool GetViewportEnabled()
	{
		return this.itsDataModuleMinimap.itsViewport.itsActive;
	}

	// Token: 0x06000C9B RID: 3227 RVA: 0x0005CEF4 File Offset: 0x0005B0F4
	public float GetRevealedPercent()
	{
		float num = 0f;
		if (this.itsMeshFilterFogOfWarPlane != null)
		{
			foreach (Color color in this.itsMeshFilterFogOfWarPlane.mesh.colors)
			{
				num += color.a;
			}
			return 1f - num / (float)this.itsMeshFilterFogOfWarPlane.mesh.colors.Length;
		}
		return 0f;
	}

	// Token: 0x06000C9C RID: 3228 RVA: 0x0005CF78 File Offset: 0x0005B178
	public override string GetName()
	{
		return base.name;
	}

	// Token: 0x06000C9D RID: 3229 RVA: 0x0005CF80 File Offset: 0x0005B180
	public string GetHeaderName()
	{
		return base.name;
	}

	// Token: 0x06000C9E RID: 3230 RVA: 0x0005CF88 File Offset: 0x0005B188
	public override Texture2D GetIcon()
	{
		return (Texture2D)Resources.Load("KGFMapSystem/textures/mapsystem_small", typeof(Texture2D));
	}

	// Token: 0x06000C9F RID: 3231 RVA: 0x0005CFA4 File Offset: 0x0005B1A4
	private string[] GetNamesFromLayerMask(LayerMask theLayers)
	{
		List<string> list = new List<string>();
		for (int i = 0; i < 32; i++)
		{
			if ((theLayers & 1 << i) != 0)
			{
				string text = LayerMask.LayerToName(i);
				if (text.Trim() != string.Empty)
				{
					list.Add(text);
				}
			}
		}
		return list.ToArray();
	}

	// Token: 0x06000CA0 RID: 3232 RVA: 0x0005D008 File Offset: 0x0005B208
	private void DrawCustomGuiMain()
	{
		KGFGUIUtility.BeginHorizontalBox(KGFGUIUtility.eStyleBox.eBoxMiddleVertical, new GUILayoutOption[0]);
		KGFGUIUtility.Label("itsTarget", new GUILayoutOption[0]);
		GUILayout.FlexibleSpace();
		KGFGUIUtility.Label(string.Empty + this.itsDataModuleMinimap.itsGlobalSettings.itsTarget.gameObject.GetObjectPath(), new GUILayoutOption[0]);
		KGFGUIUtility.EndHorizontalBox();
		KGFGUIUtility.BeginHorizontalBox(KGFGUIUtility.eStyleBox.eBoxMiddleVertical, new GUILayoutOption[0]);
		KGFGUIUtility.Label("itsStaticNorth", new GUILayoutOption[0]);
		GUILayout.FlexibleSpace();
		KGFGUIUtility.Label(string.Empty + this.itsDataModuleMinimap.itsGlobalSettings.itsStaticNorth, new GUILayoutOption[0]);
		KGFGUIUtility.EndHorizontalBox();
		KGFGUIUtility.BeginHorizontalBox(KGFGUIUtility.eStyleBox.eBoxBottom, new GUILayoutOption[0]);
		KGFGUIUtility.Label("Enabled", new GUILayoutOption[0]);
		GUILayout.FlexibleSpace();
		KGFGUIUtility.Label(string.Empty + this.itsDataModuleMinimap.itsGlobalSettings.itsIsActive, new GUILayoutOption[0]);
		KGFGUIUtility.EndHorizontalBox();
	}

	// Token: 0x06000CA1 RID: 3233 RVA: 0x0005D110 File Offset: 0x0005B310
	private void DrawCustomGuiAppearanceMinimap()
	{
		KGFGUIUtility.BeginHorizontalBox(KGFGUIUtility.eStyleBox.eBoxMiddleVertical, new GUILayoutOption[0]);
		KGFGUIUtility.Label("itsSize", new GUILayoutOption[0]);
		GUILayout.FlexibleSpace();
		KGFGUIUtility.Label(string.Empty + this.itsDataModuleMinimap.itsAppearanceMiniMap.itsSize, new GUILayoutOption[0]);
		KGFGUIUtility.EndHorizontalBox();
		KGFGUIUtility.BeginHorizontalBox(KGFGUIUtility.eStyleBox.eBoxMiddleVertical, new GUILayoutOption[0]);
		KGFGUIUtility.Label("itsButtonSize", new GUILayoutOption[0]);
		GUILayout.FlexibleSpace();
		KGFGUIUtility.Label(string.Empty + this.itsDataModuleMinimap.itsAppearanceMiniMap.itsButtonSize, new GUILayoutOption[0]);
		KGFGUIUtility.EndHorizontalBox();
		KGFGUIUtility.BeginHorizontalBox(KGFGUIUtility.eStyleBox.eBoxMiddleVertical, new GUILayoutOption[0]);
		KGFGUIUtility.Label("itsButtonPadding", new GUILayoutOption[0]);
		GUILayout.FlexibleSpace();
		KGFGUIUtility.Label(string.Empty + this.itsDataModuleMinimap.itsAppearanceMiniMap.itsButtonPadding, new GUILayoutOption[0]);
		KGFGUIUtility.EndHorizontalBox();
		KGFGUIUtility.BeginHorizontalBox(KGFGUIUtility.eStyleBox.eBoxMiddleVertical, new GUILayoutOption[0]);
		KGFGUIUtility.Label("itsScaleIcons", new GUILayoutOption[0]);
		GUILayout.FlexibleSpace();
		KGFGUIUtility.Label(string.Empty + this.itsDataModuleMinimap.itsAppearanceMiniMap.itsScaleIcons, new GUILayoutOption[0]);
		KGFGUIUtility.EndHorizontalBox();
		KGFGUIUtility.BeginHorizontalBox(KGFGUIUtility.eStyleBox.eBoxMiddleVertical, new GUILayoutOption[0]);
		KGFGUIUtility.Label("itsScaleArrows", new GUILayoutOption[0]);
		GUILayout.FlexibleSpace();
		KGFGUIUtility.Label(string.Empty + this.itsDataModuleMinimap.itsAppearanceMiniMap.itsScaleArrows, new GUILayoutOption[0]);
		KGFGUIUtility.EndHorizontalBox();
		KGFGUIUtility.BeginHorizontalBox(KGFGUIUtility.eStyleBox.eBoxBottom, new GUILayoutOption[0]);
		KGFGUIUtility.Label("itsRadiusArrows", new GUILayoutOption[0]);
		GUILayout.FlexibleSpace();
		KGFGUIUtility.Label(string.Empty + this.itsDataModuleMinimap.itsAppearanceMiniMap.itsRadiusArrows, new GUILayoutOption[0]);
		KGFGUIUtility.EndHorizontalBox();
	}

	// Token: 0x06000CA2 RID: 3234 RVA: 0x0005D304 File Offset: 0x0005B504
	private void DrawCustomGuiAppearanceMap()
	{
		KGFGUIUtility.BeginHorizontalBox(KGFGUIUtility.eStyleBox.eBoxMiddleVertical, new GUILayoutOption[0]);
		KGFGUIUtility.Label("itsButtonSize", new GUILayoutOption[0]);
		GUILayout.FlexibleSpace();
		KGFGUIUtility.Label(string.Empty + this.itsDataModuleMinimap.itsAppearanceMap.itsButtonSize, new GUILayoutOption[0]);
		KGFGUIUtility.EndHorizontalBox();
		KGFGUIUtility.BeginHorizontalBox(KGFGUIUtility.eStyleBox.eBoxMiddleVertical, new GUILayoutOption[0]);
		KGFGUIUtility.Label("itsButtonPadding", new GUILayoutOption[0]);
		GUILayout.FlexibleSpace();
		KGFGUIUtility.Label(string.Empty + this.itsDataModuleMinimap.itsAppearanceMap.itsButtonPadding, new GUILayoutOption[0]);
		KGFGUIUtility.EndHorizontalBox();
		KGFGUIUtility.BeginHorizontalBox(KGFGUIUtility.eStyleBox.eBoxMiddleVertical, new GUILayoutOption[0]);
		KGFGUIUtility.Label("itsButtonSpace", new GUILayoutOption[0]);
		GUILayout.FlexibleSpace();
		KGFGUIUtility.Label(string.Empty + this.itsDataModuleMinimap.itsAppearanceMap.itsButtonSpace, new GUILayoutOption[0]);
		KGFGUIUtility.EndHorizontalBox();
		KGFGUIUtility.BeginHorizontalBox(KGFGUIUtility.eStyleBox.eBoxBottom, new GUILayoutOption[0]);
		KGFGUIUtility.Label("itsScaleIcons", new GUILayoutOption[0]);
		GUILayout.FlexibleSpace();
		KGFGUIUtility.Label(string.Empty + this.itsDataModuleMinimap.itsAppearanceMap.itsScaleIcons, new GUILayoutOption[0]);
		KGFGUIUtility.EndHorizontalBox();
	}

	// Token: 0x06000CA3 RID: 3235 RVA: 0x0005D458 File Offset: 0x0005B658
	private void DrawCustomGuiFogOfWar()
	{
		KGFGUIUtility.BeginHorizontalBox(KGFGUIUtility.eStyleBox.eBoxMiddleVertical, new GUILayoutOption[0]);
		KGFGUIUtility.Label("Enabled", new GUILayoutOption[0]);
		GUILayout.FlexibleSpace();
		KGFGUIUtility.Label(string.Empty + this.itsDataModuleMinimap.itsFogOfWar.itsActive, new GUILayoutOption[0]);
		KGFGUIUtility.EndHorizontalBox();
		KGFGUIUtility.BeginHorizontalBox(KGFGUIUtility.eStyleBox.eBoxMiddleVertical, new GUILayoutOption[0]);
		KGFGUIUtility.Label("itsResolutionX", new GUILayoutOption[0]);
		GUILayout.FlexibleSpace();
		KGFGUIUtility.Label(string.Empty + this.itsDataModuleMinimap.itsFogOfWar.itsResolutionX, new GUILayoutOption[0]);
		KGFGUIUtility.EndHorizontalBox();
		KGFGUIUtility.BeginHorizontalBox(KGFGUIUtility.eStyleBox.eBoxMiddleVertical, new GUILayoutOption[0]);
		KGFGUIUtility.Label("itsResolutionY", new GUILayoutOption[0]);
		GUILayout.FlexibleSpace();
		KGFGUIUtility.Label(string.Empty + this.itsDataModuleMinimap.itsFogOfWar.itsResolutionY, new GUILayoutOption[0]);
		KGFGUIUtility.EndHorizontalBox();
		KGFGUIUtility.BeginHorizontalBox(KGFGUIUtility.eStyleBox.eBoxMiddleVertical, new GUILayoutOption[0]);
		KGFGUIUtility.Label("itsRevealDistance", new GUILayoutOption[0]);
		GUILayout.FlexibleSpace();
		KGFGUIUtility.Label(string.Empty + this.itsDataModuleMinimap.itsFogOfWar.itsRevealDistance, new GUILayoutOption[0]);
		KGFGUIUtility.EndHorizontalBox();
		KGFGUIUtility.BeginHorizontalBox(KGFGUIUtility.eStyleBox.eBoxMiddleVertical, new GUILayoutOption[0]);
		KGFGUIUtility.Label("itsRevealedFullDistance", new GUILayoutOption[0]);
		GUILayout.FlexibleSpace();
		KGFGUIUtility.Label(string.Empty + this.itsDataModuleMinimap.itsFogOfWar.itsRevealedFullDistance, new GUILayoutOption[0]);
		KGFGUIUtility.EndHorizontalBox();
		KGFGUIUtility.BeginHorizontalBox(KGFGUIUtility.eStyleBox.eBoxBottom, new GUILayoutOption[0]);
		KGFGUIUtility.Label("Revealed", new GUILayoutOption[0]);
		GUILayout.FlexibleSpace();
		KGFGUIUtility.Label(string.Format("{0:0.00}%", this.GetRevealedPercent() * 100f), new GUILayoutOption[0]);
		KGFGUIUtility.EndHorizontalBox();
	}

	// Token: 0x06000CA4 RID: 3236 RVA: 0x0005D648 File Offset: 0x0005B848
	private void DrawCustomGuiZoom()
	{
		KGFGUIUtility.BeginHorizontalBox(KGFGUIUtility.eStyleBox.eBoxMiddleVertical, new GUILayoutOption[0]);
		KGFGUIUtility.Label("Current zoom", new GUILayoutOption[0]);
		GUILayout.FlexibleSpace();
		KGFGUIUtility.Label(string.Empty + this.itsCurrentZoom, new GUILayoutOption[0]);
		KGFGUIUtility.EndHorizontalBox();
		KGFGUIUtility.BeginHorizontalBox(KGFGUIUtility.eStyleBox.eBoxMiddleVertical, new GUILayoutOption[0]);
		KGFGUIUtility.Label("itsZoomStartValue", new GUILayoutOption[0]);
		GUILayout.FlexibleSpace();
		KGFGUIUtility.Label(string.Empty + this.itsDataModuleMinimap.itsZoom.itsZoomStartValue, new GUILayoutOption[0]);
		KGFGUIUtility.EndHorizontalBox();
		KGFGUIUtility.BeginHorizontalBox(KGFGUIUtility.eStyleBox.eBoxMiddleVertical, new GUILayoutOption[0]);
		KGFGUIUtility.Label("itsZoomMin", new GUILayoutOption[0]);
		GUILayout.FlexibleSpace();
		KGFGUIUtility.Label(string.Empty + this.itsDataModuleMinimap.itsZoom.itsZoomMin, new GUILayoutOption[0]);
		KGFGUIUtility.EndHorizontalBox();
		KGFGUIUtility.BeginHorizontalBox(KGFGUIUtility.eStyleBox.eBoxMiddleVertical, new GUILayoutOption[0]);
		KGFGUIUtility.Label("itsZoomMax", new GUILayoutOption[0]);
		GUILayout.FlexibleSpace();
		KGFGUIUtility.Label(string.Empty + this.itsDataModuleMinimap.itsZoom.itsZoomMax, new GUILayoutOption[0]);
		KGFGUIUtility.EndHorizontalBox();
		KGFGUIUtility.BeginHorizontalBox(KGFGUIUtility.eStyleBox.eBoxBottom, new GUILayoutOption[0]);
		KGFGUIUtility.Label("itsZoomChangeValue", new GUILayoutOption[0]);
		GUILayout.FlexibleSpace();
		KGFGUIUtility.Label(string.Empty + this.itsDataModuleMinimap.itsZoom.itsZoomChangeValue, new GUILayoutOption[0]);
		KGFGUIUtility.EndHorizontalBox();
	}

	// Token: 0x06000CA5 RID: 3237 RVA: 0x0005D7E0 File Offset: 0x0005B9E0
	private void DrawCustomGuiViewport()
	{
		KGFGUIUtility.BeginHorizontalBox(KGFGUIUtility.eStyleBox.eBoxMiddleVertical, new GUILayoutOption[0]);
		KGFGUIUtility.Label("Enabled", new GUILayoutOption[0]);
		GUILayout.FlexibleSpace();
		KGFGUIUtility.Label(string.Empty + this.itsDataModuleMinimap.itsViewport.itsActive, new GUILayoutOption[0]);
		KGFGUIUtility.EndHorizontalBox();
		KGFGUIUtility.BeginHorizontalBox(KGFGUIUtility.eStyleBox.eBoxBottom, new GUILayoutOption[0]);
		KGFGUIUtility.Label("itsCamera", new GUILayoutOption[0]);
		GUILayout.FlexibleSpace();
		if (this.itsDataModuleMinimap.itsViewport.itsCamera != null)
		{
			KGFGUIUtility.Label(string.Empty + this.itsDataModuleMinimap.itsViewport.itsCamera.gameObject.GetObjectPath(), new GUILayoutOption[0]);
		}
		else
		{
			KGFGUIUtility.Label("NONE", new GUILayoutOption[0]);
		}
		KGFGUIUtility.EndHorizontalBox();
	}

	// Token: 0x06000CA6 RID: 3238 RVA: 0x0005D8C4 File Offset: 0x0005BAC4
	private void DrawCustomGuiPhoto()
	{
		KGFGUIUtility.BeginHorizontalBox(KGFGUIUtility.eStyleBox.eBoxMiddleVertical, new GUILayoutOption[0]);
		KGFGUIUtility.Label("Enabled", new GUILayoutOption[0]);
		GUILayout.FlexibleSpace();
		KGFGUIUtility.Label(string.Empty + this.itsDataModuleMinimap.itsPhoto.itsTakePhoto, new GUILayoutOption[0]);
		KGFGUIUtility.EndHorizontalBox();
		KGFGUIUtility.BeginHorizontalBox(KGFGUIUtility.eStyleBox.eBoxBottom, new GUILayoutOption[0]);
		KGFGUIUtility.Label("itsPhotoLayers", new GUILayoutOption[0]);
		GUILayout.FlexibleSpace();
		KGFGUIUtility.BeginVerticalBox(KGFGUIUtility.eStyleBox.eBoxInvisible, new GUILayoutOption[0]);
		foreach (string text in this.GetNamesFromLayerMask(this.itsDataModuleMinimap.itsPhoto.itsPhotoLayers))
		{
			GUILayout.Label(text, new GUILayoutOption[0]);
		}
		KGFGUIUtility.EndVerticalBox();
		KGFGUIUtility.EndHorizontalBox();
	}

	// Token: 0x06000CA7 RID: 3239 RVA: 0x0005D998 File Offset: 0x0005BB98
	private void DrawCustomGuiMapIcons()
	{
		int num = 0;
		Dictionary<string, int> dictionary = new Dictionary<string, int>();
		foreach (KGFMapSystem.mapicon_listitem_script mapicon_listitem_script in this.itsListMapIcons)
		{
			if (mapicon_listitem_script.GetMapIconVisibilityEffective())
			{
				num++;
			}
			if (!dictionary.ContainsKey(mapicon_listitem_script.itsMapIcon.GetCategory()))
			{
				dictionary[mapicon_listitem_script.itsMapIcon.GetCategory()] = 1;
			}
			else
			{
				Dictionary<string, int> dictionary3;
				Dictionary<string, int> dictionary2 = dictionary3 = dictionary;
				string category;
				string key = category = mapicon_listitem_script.itsMapIcon.GetCategory();
				int num2 = dictionary3[category];
				dictionary2[key] = num2 + 1;
			}
		}
		KGFGUIUtility.BeginVerticalBox(KGFGUIUtility.eStyleBox.eBox, new GUILayoutOption[0]);
		KGFGUIUtility.BeginHorizontalBox(KGFGUIUtility.eStyleBox.eBoxMiddleVertical, new GUILayoutOption[0]);
		KGFGUIUtility.Label("Icons", new GUILayoutOption[0]);
		GUILayout.FlexibleSpace();
		KGFGUIUtility.Label(string.Empty + this.itsListMapIcons.Count, new GUILayoutOption[0]);
		KGFGUIUtility.EndHorizontalBox();
		KGFGUIUtility.BeginHorizontalBox(KGFGUIUtility.eStyleBox.eBoxMiddleVertical, new GUILayoutOption[0]);
		KGFGUIUtility.Label("Icons visible", new GUILayoutOption[0]);
		GUILayout.FlexibleSpace();
		KGFGUIUtility.Label(string.Empty + num, new GUILayoutOption[0]);
		KGFGUIUtility.EndHorizontalBox();
		KGFGUIUtility.BeginHorizontalBox(KGFGUIUtility.eStyleBox.eBoxMiddleVertical, new GUILayoutOption[0]);
		KGFGUIUtility.Label("Icons by category", new GUILayoutOption[0]);
		GUILayout.FlexibleSpace();
		KGFGUIUtility.EndHorizontalBox();
		KGFGUIUtility.BeginHorizontalBox(KGFGUIUtility.eStyleBox.eBoxBottom, new GUILayoutOption[0]);
		KGFGUIUtility.Space();
		KGFGUIUtility.BeginVerticalBox(KGFGUIUtility.eStyleBox.eBox, new GUILayoutOption[0]);
		int num3 = 0;
		foreach (KeyValuePair<string, int> keyValuePair in dictionary)
		{
			if (num3 == 0)
			{
				KGFGUIUtility.BeginHorizontalBox(KGFGUIUtility.eStyleBox.eBoxTop, new GUILayoutOption[0]);
			}
			else if (num3 == dictionary.Count - 1)
			{
				KGFGUIUtility.BeginHorizontalBox(KGFGUIUtility.eStyleBox.eBoxBottom, new GUILayoutOption[0]);
			}
			else
			{
				KGFGUIUtility.BeginHorizontalBox(KGFGUIUtility.eStyleBox.eBoxMiddleVertical, new GUILayoutOption[0]);
			}
			KGFGUIUtility.Label(string.Format("'{0}'", keyValuePair.Key), new GUILayoutOption[0]);
			GUILayout.FlexibleSpace();
			KGFGUIUtility.Label(string.Empty + keyValuePair.Value, new GUILayoutOption[0]);
			KGFGUIUtility.EndHorizontalBox();
			num3++;
		}
		KGFGUIUtility.EndVerticalBox();
		KGFGUIUtility.EndHorizontalBox();
		KGFGUIUtility.EndVerticalBox();
	}

	// Token: 0x06000CA8 RID: 3240 RVA: 0x0005DC34 File Offset: 0x0005BE34
	private void DrawCustomGuiUserFlags()
	{
		KGFGUIUtility.BeginHorizontalBox(KGFGUIUtility.eStyleBox.eBoxMiddleVertical, new GUILayoutOption[0]);
		KGFGUIUtility.Label("Enabled", new GUILayoutOption[0]);
		GUILayout.FlexibleSpace();
		KGFGUIUtility.Label(string.Empty + this.itsDataModuleMinimap.itsUserFlags.itsActive, new GUILayoutOption[0]);
		KGFGUIUtility.EndHorizontalBox();
		KGFGUIUtility.BeginHorizontalBox(KGFGUIUtility.eStyleBox.eBoxMiddleVertical, new GUILayoutOption[0]);
		KGFGUIUtility.Label("itsMapIcon", new GUILayoutOption[0]);
		GUILayout.FlexibleSpace();
		if (this.itsDataModuleMinimap.itsUserFlags.itsMapIcon != null)
		{
			KGFGUIUtility.Label(string.Empty + this.itsDataModuleMinimap.itsUserFlags.itsMapIcon.gameObject.GetObjectPath(), new GUILayoutOption[0]);
		}
		else
		{
			KGFGUIUtility.Label("NONE", new GUILayoutOption[0]);
		}
		KGFGUIUtility.EndHorizontalBox();
		KGFGUIUtility.BeginHorizontalBox(KGFGUIUtility.eStyleBox.eBoxBottom, new GUILayoutOption[0]);
		KGFGUIUtility.Label("Flag count", new GUILayoutOption[0]);
		GUILayout.FlexibleSpace();
		KGFGUIUtility.Label(string.Empty + this.GetUserFlags().Length, new GUILayoutOption[0]);
		KGFGUIUtility.EndHorizontalBox();
	}

	// Token: 0x06000CA9 RID: 3241 RVA: 0x0005DD64 File Offset: 0x0005BF64
	public void Render()
	{
		this.itsCustomGuiPosition = KGFGUIUtility.BeginScrollView(this.itsCustomGuiPosition, false, false, new GUILayoutOption[0]);
		KGFGUIUtility.BeginHorizontalBox(KGFGUIUtility.eStyleBox.eBoxInvisible, new GUILayoutOption[0]);
		KGFGUIUtility.BeginVerticalBox(KGFGUIUtility.eStyleBox.eBoxInvisible, new GUILayoutOption[0]);
		KGFGUIUtility.BeginVerticalBox(KGFGUIUtility.eStyleBox.eBoxDecorated, new GUILayoutOption[0]);
		KGFGUIUtility.BeginHorizontalBox(KGFGUIUtility.eStyleBox.eBoxDarkTop, new GUILayoutOption[0]);
		KGFGUIUtility.Label("Main", this.GetIcon(), KGFGUIUtility.eStyleLabel.eLabelFitIntoBox, new GUILayoutOption[0]);
		GUILayout.FlexibleSpace();
		KGFGUIUtility.EndHorizontalBox();
		this.DrawCustomGuiMain();
		KGFGUIUtility.EndVerticalBox();
		KGFGUIUtility.Space();
		KGFGUIUtility.BeginVerticalBox(KGFGUIUtility.eStyleBox.eBoxDecorated, new GUILayoutOption[0]);
		KGFGUIUtility.BeginHorizontalBox(KGFGUIUtility.eStyleBox.eBoxDarkTop, new GUILayoutOption[0]);
		KGFGUIUtility.Label("Appearance Minimap", this.GetIcon(), KGFGUIUtility.eStyleLabel.eLabelFitIntoBox, new GUILayoutOption[0]);
		GUILayout.FlexibleSpace();
		KGFGUIUtility.EndHorizontalBox();
		this.DrawCustomGuiAppearanceMinimap();
		KGFGUIUtility.EndVerticalBox();
		KGFGUIUtility.Space();
		KGFGUIUtility.BeginVerticalBox(KGFGUIUtility.eStyleBox.eBoxDecorated, new GUILayoutOption[0]);
		KGFGUIUtility.BeginHorizontalBox(KGFGUIUtility.eStyleBox.eBoxDarkTop, new GUILayoutOption[0]);
		KGFGUIUtility.Label("Appearance Map", this.GetIcon(), KGFGUIUtility.eStyleLabel.eLabelFitIntoBox, new GUILayoutOption[0]);
		GUILayout.FlexibleSpace();
		KGFGUIUtility.EndHorizontalBox();
		this.DrawCustomGuiAppearanceMap();
		KGFGUIUtility.EndVerticalBox();
		KGFGUIUtility.Space();
		KGFGUIUtility.BeginVerticalBox(KGFGUIUtility.eStyleBox.eBoxDecorated, new GUILayoutOption[0]);
		KGFGUIUtility.BeginHorizontalBox(KGFGUIUtility.eStyleBox.eBoxDarkTop, new GUILayoutOption[0]);
		KGFGUIUtility.Label("Fog of war", this.GetIcon(), KGFGUIUtility.eStyleLabel.eLabelFitIntoBox, new GUILayoutOption[0]);
		GUILayout.FlexibleSpace();
		KGFGUIUtility.EndHorizontalBox();
		this.DrawCustomGuiFogOfWar();
		KGFGUIUtility.EndVerticalBox();
		KGFGUIUtility.Space();
		KGFGUIUtility.BeginVerticalBox(KGFGUIUtility.eStyleBox.eBoxDecorated, new GUILayoutOption[0]);
		KGFGUIUtility.BeginHorizontalBox(KGFGUIUtility.eStyleBox.eBoxDarkTop, new GUILayoutOption[0]);
		KGFGUIUtility.Label("Zoom", this.GetIcon(), KGFGUIUtility.eStyleLabel.eLabelFitIntoBox, new GUILayoutOption[0]);
		GUILayout.FlexibleSpace();
		KGFGUIUtility.EndHorizontalBox();
		this.DrawCustomGuiZoom();
		KGFGUIUtility.EndVerticalBox();
		KGFGUIUtility.EndVerticalBox();
		KGFGUIUtility.Space();
		KGFGUIUtility.BeginVerticalBox(KGFGUIUtility.eStyleBox.eBoxInvisible, new GUILayoutOption[0]);
		KGFGUIUtility.BeginVerticalBox(KGFGUIUtility.eStyleBox.eBoxDecorated, new GUILayoutOption[0]);
		KGFGUIUtility.BeginHorizontalBox(KGFGUIUtility.eStyleBox.eBoxDarkTop, new GUILayoutOption[0]);
		KGFGUIUtility.Label("Viewport", this.GetIcon(), KGFGUIUtility.eStyleLabel.eLabelFitIntoBox, new GUILayoutOption[0]);
		GUILayout.FlexibleSpace();
		KGFGUIUtility.EndHorizontalBox();
		this.DrawCustomGuiViewport();
		KGFGUIUtility.EndVerticalBox();
		KGFGUIUtility.Space();
		KGFGUIUtility.BeginVerticalBox(KGFGUIUtility.eStyleBox.eBoxDecorated, new GUILayoutOption[0]);
		KGFGUIUtility.BeginHorizontalBox(KGFGUIUtility.eStyleBox.eBoxDarkTop, new GUILayoutOption[0]);
		KGFGUIUtility.Label("Photo", this.GetIcon(), KGFGUIUtility.eStyleLabel.eLabelFitIntoBox, new GUILayoutOption[0]);
		GUILayout.FlexibleSpace();
		KGFGUIUtility.EndHorizontalBox();
		this.DrawCustomGuiPhoto();
		KGFGUIUtility.EndVerticalBox();
		KGFGUIUtility.Space();
		KGFGUIUtility.BeginVerticalBox(KGFGUIUtility.eStyleBox.eBoxDecorated, new GUILayoutOption[0]);
		KGFGUIUtility.BeginHorizontalBox(KGFGUIUtility.eStyleBox.eBoxDarkTop, new GUILayoutOption[0]);
		KGFGUIUtility.Label("Map Icons", this.GetIcon(), KGFGUIUtility.eStyleLabel.eLabelFitIntoBox, new GUILayoutOption[0]);
		GUILayout.FlexibleSpace();
		KGFGUIUtility.EndHorizontalBox();
		this.DrawCustomGuiMapIcons();
		KGFGUIUtility.EndVerticalBox();
		KGFGUIUtility.Space();
		KGFGUIUtility.BeginVerticalBox(KGFGUIUtility.eStyleBox.eBoxDecorated, new GUILayoutOption[0]);
		KGFGUIUtility.BeginHorizontalBox(KGFGUIUtility.eStyleBox.eBoxDarkTop, new GUILayoutOption[0]);
		KGFGUIUtility.Label("User flags", this.GetIcon(), KGFGUIUtility.eStyleLabel.eLabelFitIntoBox, new GUILayoutOption[0]);
		GUILayout.FlexibleSpace();
		KGFGUIUtility.EndHorizontalBox();
		this.DrawCustomGuiUserFlags();
		KGFGUIUtility.EndVerticalBox();
		KGFGUIUtility.EndVerticalBox();
		KGFGUIUtility.EndHorizontalBox();
		KGFGUIUtility.EndScrollView();
	}

	// Token: 0x06000CAA RID: 3242 RVA: 0x0005E060 File Offset: 0x0005C260
	private void NullError(ref KGFMessageList theMessageList, string theName, object theValue)
	{
		if (theValue == null)
		{
			theMessageList.AddError(string.Format("value of '{0}' must not be null", theName));
		}
	}

	// Token: 0x06000CAB RID: 3243 RVA: 0x0005E07C File Offset: 0x0005C27C
	private void RegionError(ref KGFMessageList theMessageList, string theName, float theValue, float theMin, float theMax)
	{
		if (theValue < theMin || theValue > theMax)
		{
			theMessageList.AddError(string.Format("Value has to be between {0} and {1} ({2})", theMin, theMax, theName));
		}
	}

	// Token: 0x06000CAC RID: 3244 RVA: 0x0005E0BC File Offset: 0x0005C2BC
	private void PositiveError(ref KGFMessageList theMessageList, string theName, float theValue)
	{
		if (theValue < 0f)
		{
			theMessageList.AddError(string.Format("{0} must be positive", theName));
		}
	}

	// Token: 0x06000CAD RID: 3245 RVA: 0x0005E0DC File Offset: 0x0005C2DC
	public override KGFMessageList Validate()
	{
		KGFMessageList kgfmessageList = new KGFMessageList();
		this.RegionError(ref kgfmessageList, "itsDataModuleMinimap.itsStaticNorth", this.itsDataModuleMinimap.itsGlobalSettings.itsStaticNorth, 0f, 360f);
		if (this.itsDataModuleMinimap.itsGlobalSettings.itsTarget == null)
		{
			kgfmessageList.AddError("itsTarget must not be null. Please add a target that is always centered on the minimap (e.g.: the character).");
		}
		if (this.itsDataModuleMinimap.itsAppearanceMiniMap.itsMask != null && !this.GetHasProVersion())
		{
			kgfmessageList.AddWarning("Masking texture does only work in Unity Pro version. (itsAppearanceMiniMap.itsMask)");
		}
		this.RegionError(ref kgfmessageList, "itsAppearanceMiniMap.itsSize", this.itsDataModuleMinimap.itsAppearanceMiniMap.itsSize, 0f, 1f);
		this.RegionError(ref kgfmessageList, "itsAppearanceMiniMap.itsButtonSize", this.itsDataModuleMinimap.itsAppearanceMiniMap.itsButtonSize, 0f, 1f);
		this.RegionError(ref kgfmessageList, "itsAppearanceMiniMap.itsButtonPadding", this.itsDataModuleMinimap.itsAppearanceMiniMap.itsButtonPadding, 0f, 1f);
		this.RegionError(ref kgfmessageList, "itsAppearanceMiniMap.itsScaleArrows", this.itsDataModuleMinimap.itsAppearanceMiniMap.itsScaleArrows, 0f, 1f);
		this.RegionError(ref kgfmessageList, "itsAppearanceMiniMap.itsScaleIcons", this.itsDataModuleMinimap.itsAppearanceMiniMap.itsScaleIcons, 0f, 1f);
		this.RegionError(ref kgfmessageList, "itsAppearanceMiniMap.itsRadiusArrows", this.itsDataModuleMinimap.itsAppearanceMiniMap.itsRadiusArrows, 0f, 1f);
		this.PositiveError(ref kgfmessageList, "itsAppearanceMiniMap.itsBackgroundBorder", (float)this.itsDataModuleMinimap.itsAppearanceMiniMap.itsBackgroundBorder);
		if (this.itsDataModuleMinimap.itsAppearanceMiniMap.itsBackground == null)
		{
			kgfmessageList.AddWarning("Appearance texture should be set (itsAppearance.itsBackground)");
		}
		if (this.itsDataModuleMinimap.itsAppearanceMiniMap.itsButton == null)
		{
			kgfmessageList.AddWarning("Appearance texture should be set (itsAppearance.itsButton)");
		}
		if (this.itsDataModuleMinimap.itsAppearanceMiniMap.itsButtonDown == null)
		{
			kgfmessageList.AddWarning("Appearance texture should be set (itsAppearance.itsButtonDown)");
		}
		if (this.itsDataModuleMinimap.itsAppearanceMiniMap.itsButtonHover == null)
		{
			kgfmessageList.AddWarning("Appearance texture should be set (itsAppearance.itsButtonHover)");
		}
		if (this.itsDataModuleMinimap.itsAppearanceMiniMap.itsIconZoomIn == null)
		{
			kgfmessageList.AddWarning("Appearance texture should be set (itsAppearance.itsIconZoomIn)");
		}
		if (this.itsDataModuleMinimap.itsAppearanceMiniMap.itsIconZoomOut == null)
		{
			kgfmessageList.AddWarning("Appearance texture should be set (itsAppearance.itsIconZoomOut)");
		}
		if (this.itsDataModuleMinimap.itsAppearanceMiniMap.itsIconZoomLock == null)
		{
			kgfmessageList.AddWarning("Appearance texture should be set (itsAppearance.itsIconZoomLock)");
		}
		if (this.itsDataModuleMinimap.itsAppearanceMiniMap.itsIconFullscreen == null)
		{
			kgfmessageList.AddWarning("Appearance texture should be set (itsAppearance.itsIconFullscreen)");
		}
		this.RegionError(ref kgfmessageList, "itsAppearanceMap.itsButtonSize", this.itsDataModuleMinimap.itsAppearanceMap.itsButtonSize, 0f, 1f);
		this.RegionError(ref kgfmessageList, "itsAppearanceMap.itsButtonPadding", this.itsDataModuleMinimap.itsAppearanceMap.itsButtonPadding, 0f, 1f);
		this.RegionError(ref kgfmessageList, "itsAppearanceMap.itsButtonSpace", this.itsDataModuleMinimap.itsAppearanceMap.itsButtonSpace, 0f, 1f);
		this.RegionError(ref kgfmessageList, "itsAppearanceMap.itsScaleIcons", this.itsDataModuleMinimap.itsAppearanceMap.itsScaleIcons, 0f, 1f);
		this.PositiveError(ref kgfmessageList, "itsAppearanceMap.itsBackgroundBorder", (float)this.itsDataModuleMinimap.itsAppearanceMap.itsBackgroundBorder);
		if (this.itsDataModuleMinimap.itsGlobalSettings.itsColorAll != Color.white && !this.GetHasProVersion())
		{
			kgfmessageList.AddError("itsColorAll does only work in Unity Pro version. (itsColorAll)");
		}
		if (this.itsDataModuleMinimap.itsAppearanceMap.itsMask != null && !this.GetHasProVersion())
		{
			kgfmessageList.AddWarning("Masking texture does only work in Unity Pro version. (itsAppearanceMap.itsMask)");
		}
		if (this.itsDataModuleMinimap.itsAppearanceMap.itsButton == null)
		{
			kgfmessageList.AddWarning("Appearance texture should be set (itsAppearanceMap.itsButton)");
		}
		if (this.itsDataModuleMinimap.itsAppearanceMap.itsButtonDown == null)
		{
			kgfmessageList.AddWarning("Appearance texture should be set (itsAppearanceMap.itsButtonDown)");
		}
		if (this.itsDataModuleMinimap.itsAppearanceMap.itsButtonHover == null)
		{
			kgfmessageList.AddWarning("Appearance texture should be set (itsAppearanceMap.itsButtonHover)");
		}
		if (this.itsDataModuleMinimap.itsAppearanceMap.itsIconZoomIn == null)
		{
			kgfmessageList.AddWarning("Appearance texture should be set (itsAppearanceMap.itsIconZoomIn)");
		}
		if (this.itsDataModuleMinimap.itsAppearanceMap.itsIconZoomOut == null)
		{
			kgfmessageList.AddWarning("Appearance texture should be set (itsAppearanceMap.itsIconZoomOut)");
		}
		if (this.itsDataModuleMinimap.itsAppearanceMap.itsIconZoomLock == null)
		{
			kgfmessageList.AddWarning("Appearance texture should be set (itsAppearanceMap.itsIconZoomLock)");
		}
		if (this.itsDataModuleMinimap.itsAppearanceMap.itsIconFullscreen == null)
		{
			kgfmessageList.AddWarning("Appearance texture should be set (itsAppearanceMap.itsIconFullscreen)");
		}
		this.PositiveError(ref kgfmessageList, "itsFogOfWar.itsResolutionX", (float)this.itsDataModuleMinimap.itsFogOfWar.itsResolutionX);
		this.PositiveError(ref kgfmessageList, "itsFogOfWar.itsResolutionY", (float)this.itsDataModuleMinimap.itsFogOfWar.itsResolutionY);
		this.PositiveError(ref kgfmessageList, "itsFogOfWar.itsRevealDistance", this.itsDataModuleMinimap.itsFogOfWar.itsRevealDistance);
		this.PositiveError(ref kgfmessageList, "itsFogOfWar.itsRevealedFullDistance", this.itsDataModuleMinimap.itsFogOfWar.itsRevealedFullDistance);
		if (this.itsDataModuleMinimap.itsFogOfWar.itsRevealedFullDistance > this.itsDataModuleMinimap.itsFogOfWar.itsRevealDistance)
		{
			kgfmessageList.AddError("itsFogOfWar.itsRevealDistance must be bigger than itsFogOfWar.itsRevealedFullDistance");
		}
		this.PositiveError(ref kgfmessageList, "itsZoom.itsZoomChangeValue", this.itsDataModuleMinimap.itsZoom.itsZoomChangeValue);
		this.PositiveError(ref kgfmessageList, "itsZoom.itsZoomMax", this.itsDataModuleMinimap.itsZoom.itsZoomMax);
		this.PositiveError(ref kgfmessageList, "itsZoom.itsZoomMin", this.itsDataModuleMinimap.itsZoom.itsZoomMin);
		this.PositiveError(ref kgfmessageList, "itsZoom.itsZoomStartValue", this.itsDataModuleMinimap.itsZoom.itsZoomStartValue);
		if (this.itsDataModuleMinimap.itsZoom.itsZoomMin > this.itsDataModuleMinimap.itsZoom.itsZoomMax)
		{
			kgfmessageList.AddError("itsZoom.itsZoomMax must be bigger than itsZoom.itsZoomMin");
		}
		if (this.itsDataModuleMinimap.itsZoom.itsZoomStartValue < this.itsDataModuleMinimap.itsZoom.itsZoomMin || this.itsDataModuleMinimap.itsZoom.itsZoomStartValue > this.itsDataModuleMinimap.itsZoom.itsZoomMax)
		{
			kgfmessageList.AddError("itsZoom.itsZoomStartValue has to be between itsZoom.itsZoomMin and itsZoom.itsZoomMin");
		}
		if (this.itsDataModuleMinimap.itsViewport.itsActive && this.itsDataModuleMinimap.itsViewport.itsCamera == null)
		{
			kgfmessageList.AddError("Active viewport needs a camera (itsViewport.itsCamera)");
		}
		if (this.itsDataModuleMinimap.itsViewport.itsActive && this.itsDataModuleMinimap.itsGlobalSettings.itsOrientation == KGFMapSystem.KGFMapSystemOrientation.XYSideScroller)
		{
			kgfmessageList.AddError("Viewport display is not supported in SideScroller mode (itsViewport.itsCamera)");
		}
		if (this.itsDataModuleMinimap.itsViewport.itsColor.a == 0f)
		{
			kgfmessageList.AddError("Viewport will be invisible if itsColor.a == 0");
		}
		if (this.itsDataModuleMinimap.itsPhoto.itsTakePhoto && this.itsDataModuleMinimap.itsPhoto.itsPhotoLayers == 0)
		{
			kgfmessageList.AddError("itsPhoto.itsPhotoLayers has to contain some layers for the photo not to be empty");
		}
		if (this.itsDataModuleMinimap.itsUserFlags.itsActive)
		{
		}
		if (LayerMask.NameToLayer("mapsystem") < 0)
		{
			kgfmessageList.AddError(string.Format("The map system needs a layer with the name '{0}'", "mapsystem"));
		}
		if (this.itsDataModuleMinimap.itsPanning.itsActive && this.itsDataModuleMinimap.itsPanning.itsUseBounds && this.itsDataModuleMinimap.itsPanning.itsBoundsLayers == 0)
		{
			kgfmessageList.AddError("itsPanning.itsBoundsLayers has to contain some layers for the panning bounds to work");
		}
		if (this.itsDataModuleMinimap.itsShaders.itsShaderMapIcon == null)
		{
			kgfmessageList.AddError(string.Format("itsDataModuleMinimap.itsShaders.itsShaderMapIcon is null", new object[0]));
		}
		if (this.itsDataModuleMinimap.itsShaders.itsShaderPhotoPlane == null)
		{
			kgfmessageList.AddError(string.Format("itsDataModuleMinimap.itsShaders.itsShaderPhotoPlane is null", new object[0]));
		}
		if (this.itsDataModuleMinimap.itsShaders.itsShaderMapMask == null)
		{
			kgfmessageList.AddError(string.Format("itsDataModuleMinimap.itsShaders.itsShaderMapMask is null", new object[0]));
		}
		if (this.itsDataModuleMinimap.itsFogOfWar.itsActive && this.itsDataModuleMinimap.itsShaders.itsShaderFogOfWar == null)
		{
			kgfmessageList.AddWarning(string.Format("itsDataModuleMinimap.itsShaders.itsShaderFogOfWar is null, fog of war will not work", new object[0]));
		}
		return kgfmessageList;
	}

	// Token: 0x06000CAE RID: 3246 RVA: 0x0005E9A8 File Offset: 0x0005CBA8
	public override string GetForumPath()
	{
		return string.Empty;
	}

	// Token: 0x06000CAF RID: 3247 RVA: 0x0005E9B0 File Offset: 0x0005CBB0
	public override string GetDocumentationPath()
	{
		return string.Empty;
	}

	// Token: 0x04000BD0 RID: 3024
	private const string itsLayerName = "mapsystem";

	// Token: 0x04000BD1 RID: 3025
	private const string itsSaveIDFogOfWarValues = "minimap_FogOfWar_values";

	// Token: 0x04000BD2 RID: 3026
	public KGFMapSystem.KGFDataMinimap itsDataModuleMinimap = new KGFMapSystem.KGFDataMinimap();

	// Token: 0x04000BD3 RID: 3027
	private bool itsMinimapActive = true;

	// Token: 0x04000BD4 RID: 3028
	private List<KGFMapSystem.mapicon_listitem_script> itsListMapIcons = new List<KGFMapSystem.mapicon_listitem_script>();

	// Token: 0x04000BD5 RID: 3029
	private int itsLayerMinimap = -1;

	// Token: 0x04000BD6 RID: 3030
	private Transform itsTargetTransform;

	// Token: 0x04000BD7 RID: 3031
	private Transform itsContainerFlags;

	// Token: 0x04000BD8 RID: 3032
	private Transform itsContainerUser;

	// Token: 0x04000BD9 RID: 3033
	private Transform itsContainerIcons;

	// Token: 0x04000BDA RID: 3034
	private Transform itsContainerIconArrows;

	// Token: 0x04000BDB RID: 3035
	private Material itsMaterialMaskedMinimap;

	// Token: 0x04000BDC RID: 3036
	private Material itsMaterialViewport;

	// Token: 0x04000BDD RID: 3037
	private Camera itsCamera;

	// Token: 0x04000BDE RID: 3038
	private Transform itsCameraTransform;

	// Token: 0x04000BDF RID: 3039
	private Camera itsCameraOutput;

	// Token: 0x04000BE0 RID: 3040
	private GameObject itsMinimapPlane;

	// Token: 0x04000BE1 RID: 3041
	private MeshFilter itsMinimapMeshFilter;

	// Token: 0x04000BE2 RID: 3042
	private RenderTexture itsRendertexture;

	// Token: 0x04000BE3 RID: 3043
	private Rect itsTargetRect;

	// Token: 0x04000BE4 RID: 3044
	private Rect itsRectZoomIn;

	// Token: 0x04000BE5 RID: 3045
	private Rect itsRectZoomOut;

	// Token: 0x04000BE6 RID: 3046
	private Rect itsRectStatic;

	// Token: 0x04000BE7 RID: 3047
	private Rect itsRectFullscreen;

	// Token: 0x04000BE8 RID: 3048
	private GUIStyle itsGuiStyleButton;

	// Token: 0x04000BE9 RID: 3049
	private GUIStyle itsGuiStyleButtonFullscreen;

	// Token: 0x04000BEA RID: 3050
	private GUIStyle itsGuiStyleBack;

	// Token: 0x04000BEB RID: 3051
	private MeshFilter itsMeshFilterFogOfWarPlane;

	// Token: 0x04000BEC RID: 3052
	private Color itsColorFogOfWarRevealed = new Color(0f, 0f, 0f, 0f);

	// Token: 0x04000BED RID: 3053
	private Vector2 itsSizeTerrain = Vector2.zero;

	// Token: 0x04000BEE RID: 3054
	private Vector2 itsScalingFogOfWar = Vector2.zero;

	// Token: 0x04000BEF RID: 3055
	private MeshRenderer itsMeshRendererMinimapPlane;

	// Token: 0x04000BF0 RID: 3056
	private Bounds itsTerrainBoundsPanning;

	// Token: 0x04000BF1 RID: 3057
	private Bounds itsTerrainBoundsPhoto;

	// Token: 0x04000BF2 RID: 3058
	private Vector2? itsSavedResolution;

	// Token: 0x04000BF3 RID: 3059
	private bool itsModeFullscreen;

	// Token: 0x04000BF4 RID: 3060
	private float itsCurrentZoom;

	// Token: 0x04000BF5 RID: 3061
	private float itsCurrentZoomDest;

	// Token: 0x04000BF6 RID: 3062
	private List<KGFMapIcon> itsListUserIcons = new List<KGFMapIcon>();

	// Token: 0x04000BF7 RID: 3063
	private List<KGFIMapIcon> itsListClickIcons = new List<KGFIMapIcon>();

	// Token: 0x04000BF8 RID: 3064
	private List<KGFMapSystem.KGFPhotoData> itsListOfPhotoData = new List<KGFMapSystem.KGFPhotoData>();

	// Token: 0x04000BF9 RID: 3065
	private KGFMapSystem.KGFPhotoData[] itsArrayOfPhotoData;

	// Token: 0x04000BFA RID: 3066
	private int itsArrayOfPhotoDataIndex;

	// Token: 0x04000BFB RID: 3067
	private GameObject itsTempCameraGameObject;

	// Token: 0x04000BFC RID: 3068
	public KGFDelegate EventVisibilityOnMinimapChanged = new KGFDelegate();

	// Token: 0x04000BFD RID: 3069
	public KGFDelegate EventUserFlagCreated = new KGFDelegate();

	// Token: 0x04000BFE RID: 3070
	public KGFDelegate EventClickedOnMinimap = new KGFDelegate();

	// Token: 0x04000BFF RID: 3071
	public KGFDelegate EventMouseMapEntered = new KGFDelegate();

	// Token: 0x04000C00 RID: 3072
	public KGFDelegate EventMouseMapLeft = new KGFDelegate();

	// Token: 0x04000C01 RID: 3073
	public KGFDelegate EventMouseMapIconEntered = new KGFDelegate();

	// Token: 0x04000C02 RID: 3074
	public KGFDelegate EventMouseMapIconLeft = new KGFDelegate();

	// Token: 0x04000C03 RID: 3075
	public KGFDelegate EventMouseMapIconClicked = new KGFDelegate();

	// Token: 0x04000C04 RID: 3076
	public KGFDelegate EventFullscreenModeChanged = new KGFDelegate();

	// Token: 0x04000C05 RID: 3077
	private bool itsErrorMode;

	// Token: 0x04000C06 RID: 3078
	private Color[] itsFOWColors;

	// Token: 0x04000C07 RID: 3079
	private Vector3[] itsFOWVertices;

	// Token: 0x04000C08 RID: 3080
	private GameObject itsGameObjectViewPort;

	// Token: 0x04000C09 RID: 3081
	private Mesh itsViewPortCubeMesh;

	// Token: 0x04000C0A RID: 3082
	private GameObject itsGameObjectPhotoParent;

	// Token: 0x04000C0B RID: 3083
	private Texture2D itsTextureRenderMaskCurrent;

	// Token: 0x04000C0C RID: 3084
	private Vector2 itsMapPanning = Vector2.zero;

	// Token: 0x04000C0D RID: 3085
	private Vector2 itsMapPanningDest = Vector2.zero;

	// Token: 0x04000C0E RID: 3086
	private Vector2 itsMapPanningMousePosLast = Vector2.zero;

	// Token: 0x04000C0F RID: 3087
	private float itsPanningMinMouseDistanceStart = 2f;

	// Token: 0x04000C10 RID: 3088
	private int itsPanningButton;

	// Token: 0x04000C11 RID: 3089
	private float itsVelX;

	// Token: 0x04000C12 RID: 3090
	private float itsVelY;

	// Token: 0x04000C13 RID: 3091
	private List<Vector3> itsDeferedClickList = new List<Vector3>();

	// Token: 0x04000C14 RID: 3092
	private Vector3 itsSavedMouseDownPoint = Vector3.zero;

	// Token: 0x04000C15 RID: 3093
	private int itsClickUsedInFrame = -1;

	// Token: 0x04000C16 RID: 3094
	private KGFMapSystem.mapicon_listitem_script itsMapIconHoveredCurrent;

	// Token: 0x04000C17 RID: 3095
	private bool itsMouseEnteredMap;

	// Token: 0x04000C18 RID: 3096
	private float itsRotation;

	// Token: 0x04000C19 RID: 3097
	private KGFMapSystem.RenderToolTipMethodType itsRenderToolTipMethod;

	// Token: 0x04000C1A RID: 3098
	private float itsZoomChangeVelocity;

	// Token: 0x04000C1B RID: 3099
	private Vector2 itsCustomGuiPosition = Vector2.zero;

	// Token: 0x0200019C RID: 412
	public class KGFPhotoData
	{
		// Token: 0x04000C1C RID: 3100
		public Vector3 itsPosition = Vector3.zero;

		// Token: 0x04000C1D RID: 3101
		public Texture2D itsTexture;

		// Token: 0x04000C1E RID: 3102
		public float itsTextureSize;

		// Token: 0x04000C1F RID: 3103
		public float itsMeters;

		// Token: 0x04000C20 RID: 3104
		public GameObject itsPhotoPlane;

		// Token: 0x04000C21 RID: 3105
		public Material itsPhotoPlaneMaterial;
	}

	// Token: 0x0200019D RID: 413
	public class KGFClickEventArgs : EventArgs
	{
		// Token: 0x06000CB1 RID: 3249 RVA: 0x0005E9CC File Offset: 0x0005CBCC
		public KGFClickEventArgs(Vector3 thePosition)
		{
			this.itsPosition = thePosition;
		}

		// Token: 0x04000C22 RID: 3106
		public Vector3 itsPosition = Vector3.zero;
	}

	// Token: 0x0200019E RID: 414
	public class KGFMarkerEventArgs : EventArgs
	{
		// Token: 0x06000CB2 RID: 3250 RVA: 0x0005E9E8 File Offset: 0x0005CBE8
		public KGFMarkerEventArgs(KGFIMapIcon theMarker)
		{
			this.itsMarker = theMarker;
		}

		// Token: 0x04000C23 RID: 3107
		public KGFIMapIcon itsMarker;
	}

	// Token: 0x0200019F RID: 415
	public enum KGFMapSystemOrientation
	{
		// Token: 0x04000C25 RID: 3109
		XYSideScroller,
		// Token: 0x04000C26 RID: 3110
		XZDefault
	}

	// Token: 0x020001A0 RID: 416
	public class KGFFlagEventArgs : EventArgs
	{
		// Token: 0x06000CB3 RID: 3251 RVA: 0x0005E9F8 File Offset: 0x0005CBF8
		public KGFFlagEventArgs(Vector3 thePosition)
		{
			this.itsPosition = thePosition;
		}

		// Token: 0x04000C27 RID: 3111
		public Vector3 itsPosition = Vector3.zero;
	}

	// Token: 0x020001A1 RID: 417
	[Serializable]
	public class KGFDataMinimap
	{
		// Token: 0x04000C28 RID: 3112
		public KGFMapSystem.minimap_global_settings itsGlobalSettings = new KGFMapSystem.minimap_global_settings();

		// Token: 0x04000C29 RID: 3113
		public KGFMapSystem.minimap_gui_settings itsAppearanceMiniMap = new KGFMapSystem.minimap_gui_settings();

		// Token: 0x04000C2A RID: 3114
		public KGFMapSystem.minimap_gui_fullscreen_settings itsAppearanceMap = new KGFMapSystem.minimap_gui_fullscreen_settings();

		// Token: 0x04000C2B RID: 3115
		public KGFMapSystem.minimap_panning_settings itsPanning = new KGFMapSystem.minimap_panning_settings();

		// Token: 0x04000C2C RID: 3116
		public KGFMapSystem.minimap_fogofwar_settings itsFogOfWar = new KGFMapSystem.minimap_fogofwar_settings();

		// Token: 0x04000C2D RID: 3117
		public KGFMapSystem.minimap_zoom_settings itsZoom = new KGFMapSystem.minimap_zoom_settings();

		// Token: 0x04000C2E RID: 3118
		public KGFMapSystem.minimap_viewport_settings itsViewport = new KGFMapSystem.minimap_viewport_settings();

		// Token: 0x04000C2F RID: 3119
		public KGFMapSystem.minimap_photo_settings itsPhoto = new KGFMapSystem.minimap_photo_settings();

		// Token: 0x04000C30 RID: 3120
		public KGFMapSystem.minimap_userflags_settings itsUserFlags = new KGFMapSystem.minimap_userflags_settings();

		// Token: 0x04000C31 RID: 3121
		public KGFMapSystem.minimap_shader_settings itsShaders = new KGFMapSystem.minimap_shader_settings();

		// Token: 0x04000C32 RID: 3122
		public KGFMapSystem.minimap_tooltip_settings itsToolTip = new KGFMapSystem.minimap_tooltip_settings();
	}

	// Token: 0x020001A2 RID: 418
	[Serializable]
	public class minimap_panning_settings
	{
		// Token: 0x04000C33 RID: 3123
		public bool itsActive;

		// Token: 0x04000C34 RID: 3124
		public bool itsUseBounds;

		// Token: 0x04000C35 RID: 3125
		public LayerMask itsBoundsLayers = -1;
	}

	// Token: 0x020001A3 RID: 419
	[Serializable]
	public class minimap_tooltip_settings
	{
		// Token: 0x04000C36 RID: 3126
		public bool itsActive;

		// Token: 0x04000C37 RID: 3127
		public Texture2D itsTextureBackground;

		// Token: 0x04000C38 RID: 3128
		public RectOffset itsBackgroundBorder;

		// Token: 0x04000C39 RID: 3129
		public RectOffset itsBackgroundPadding;

		// Token: 0x04000C3A RID: 3130
		public Color itsColorText = Color.white;

		// Token: 0x04000C3B RID: 3131
		public Font itsFontText;
	}

	// Token: 0x020001A4 RID: 420
	[Serializable]
	public class minimap_global_settings
	{
		// Token: 0x04000C3C RID: 3132
		public bool itsHideGUI;

		// Token: 0x04000C3D RID: 3133
		public LayerMask itsRenderLayers = 0;

		// Token: 0x04000C3E RID: 3134
		public GameObject itsTarget;

		// Token: 0x04000C3F RID: 3135
		public bool itsIsStatic = true;

		// Token: 0x04000C40 RID: 3136
		public float itsStaticNorth;

		// Token: 0x04000C41 RID: 3137
		public bool itsIsActive = true;

		// Token: 0x04000C42 RID: 3138
		public Color itsColorMap = Color.white;

		// Token: 0x04000C43 RID: 3139
		public Color itsColorBackground = Color.black;

		// Token: 0x04000C44 RID: 3140
		public Color itsColorAll = Color.white;

		// Token: 0x04000C45 RID: 3141
		public bool itsEnableLogMessages = true;

		// Token: 0x04000C46 RID: 3142
		public KGFMapSystem.KGFMapSystemOrientation itsOrientation = KGFMapSystem.KGFMapSystemOrientation.XZDefault;
	}

	// Token: 0x020001A5 RID: 421
	[Serializable]
	public class minimap_photo_settings
	{
		// Token: 0x04000C47 RID: 3143
		public bool itsTakePhoto = true;

		// Token: 0x04000C48 RID: 3144
		public LayerMask itsPhotoLayers = -1;

		// Token: 0x04000C49 RID: 3145
		public float itsPixelPerMeter = 5f;
	}

	// Token: 0x020001A6 RID: 422
	[Serializable]
	public class minimap_shader_settings
	{
		// Token: 0x04000C4A RID: 3146
		public Shader itsShaderMapIcon;

		// Token: 0x04000C4B RID: 3147
		public Shader itsShaderPhotoPlane;

		// Token: 0x04000C4C RID: 3148
		public Shader itsShaderFogOfWar;

		// Token: 0x04000C4D RID: 3149
		public Shader itsShaderMapMask;
	}

	// Token: 0x020001A7 RID: 423
	[Serializable]
	public class minimap_userflags_settings
	{
		// Token: 0x04000C4E RID: 3150
		public bool itsActive = true;

		// Token: 0x04000C4F RID: 3151
		public KGFMapIcon itsMapIcon;
	}

	// Token: 0x020001A8 RID: 424
	[Serializable]
	public class minimap_viewport_settings
	{
		// Token: 0x04000C50 RID: 3152
		public bool itsActive;

		// Token: 0x04000C51 RID: 3153
		public Color itsColor = Color.grey;

		// Token: 0x04000C52 RID: 3154
		public Camera itsCamera;
	}

	// Token: 0x020001A9 RID: 425
	[Serializable]
	public class minimap_gui_settings
	{
		// Token: 0x04000C53 RID: 3155
		public float itsSize = 0.2f;

		// Token: 0x04000C54 RID: 3156
		public float itsButtonSize = 0.1f;

		// Token: 0x04000C55 RID: 3157
		public float itsButtonPadding;

		// Token: 0x04000C56 RID: 3158
		public Texture2D itsButton;

		// Token: 0x04000C57 RID: 3159
		public Texture2D itsButtonHover;

		// Token: 0x04000C58 RID: 3160
		public Texture2D itsButtonDown;

		// Token: 0x04000C59 RID: 3161
		public Texture2D itsIconZoomIn;

		// Token: 0x04000C5A RID: 3162
		public Texture2D itsIconZoomOut;

		// Token: 0x04000C5B RID: 3163
		public Texture2D itsIconZoomLock;

		// Token: 0x04000C5C RID: 3164
		public Texture2D itsIconFullscreen;

		// Token: 0x04000C5D RID: 3165
		public Texture2D itsBackground;

		// Token: 0x04000C5E RID: 3166
		public int itsBackgroundBorder;

		// Token: 0x04000C5F RID: 3167
		public Texture2D itsMask;

		// Token: 0x04000C60 RID: 3168
		public float itsScaleIcons = 1f;

		// Token: 0x04000C61 RID: 3169
		public float itsScaleArrows = 0.2f;

		// Token: 0x04000C62 RID: 3170
		public float itsRadiusArrows = 1f;

		// Token: 0x04000C63 RID: 3171
		public KGFAlignmentVertical itsAlignmentVertical;

		// Token: 0x04000C64 RID: 3172
		public KGFAlignmentHorizontal itsAlignmentHorizontal = KGFAlignmentHorizontal.Right;

		// Token: 0x04000C65 RID: 3173
		public float itsMarginHorizontal;

		// Token: 0x04000C66 RID: 3174
		public float itsMarginVertical;
	}

	// Token: 0x020001AA RID: 426
	[Serializable]
	public class minimap_gui_fullscreen_settings
	{
		// Token: 0x04000C67 RID: 3175
		public float itsButtonSize = 0.1f;

		// Token: 0x04000C68 RID: 3176
		public float itsButtonPadding;

		// Token: 0x04000C69 RID: 3177
		public float itsButtonSpace = 0.01f;

		// Token: 0x04000C6A RID: 3178
		public Texture2D itsButton;

		// Token: 0x04000C6B RID: 3179
		public Texture2D itsButtonHover;

		// Token: 0x04000C6C RID: 3180
		public Texture2D itsButtonDown;

		// Token: 0x04000C6D RID: 3181
		public Texture2D itsIconZoomIn;

		// Token: 0x04000C6E RID: 3182
		public Texture2D itsIconZoomOut;

		// Token: 0x04000C6F RID: 3183
		public Texture2D itsIconZoomLock;

		// Token: 0x04000C70 RID: 3184
		public Texture2D itsIconFullscreen;

		// Token: 0x04000C71 RID: 3185
		public Texture2D itsBackground;

		// Token: 0x04000C72 RID: 3186
		public int itsBackgroundBorder;

		// Token: 0x04000C73 RID: 3187
		public Texture2D itsMask;

		// Token: 0x04000C74 RID: 3188
		public float itsScaleIcons = 1f;

		// Token: 0x04000C75 RID: 3189
		public KGFAlignmentVertical itsAlignmentVertical;

		// Token: 0x04000C76 RID: 3190
		public KGFAlignmentHorizontal itsAlignmentHorizontal = KGFAlignmentHorizontal.Right;

		// Token: 0x04000C77 RID: 3191
		public KGFOrientation itsOrientation;
	}

	// Token: 0x020001AB RID: 427
	[Serializable]
	public class minimap_fogofwar_settings
	{
		// Token: 0x04000C78 RID: 3192
		public bool itsActive = true;

		// Token: 0x04000C79 RID: 3193
		public int itsResolutionX = 10;

		// Token: 0x04000C7A RID: 3194
		public int itsResolutionY = 10;

		// Token: 0x04000C7B RID: 3195
		public float itsRevealDistance = 10f;

		// Token: 0x04000C7C RID: 3196
		public float itsRevealedFullDistance = 5f;

		// Token: 0x04000C7D RID: 3197
		public bool itsHideMapIcons;
	}

	// Token: 0x020001AC RID: 428
	[Serializable]
	public class minimap_zoom_settings
	{
		// Token: 0x04000C7E RID: 3198
		public float itsZoomStartValue = 20f;

		// Token: 0x04000C7F RID: 3199
		public float itsZoomMin = 10f;

		// Token: 0x04000C80 RID: 3200
		public float itsZoomMax = 30f;

		// Token: 0x04000C81 RID: 3201
		public float itsZoomChangeValue = 10f;
	}

	// Token: 0x020001AD RID: 429
	public class mapicon_listitem_script
	{
		// Token: 0x06000CC1 RID: 3265 RVA: 0x0005ECA0 File Offset: 0x0005CEA0
		public Vector3 GetRepresentationSize()
		{
			if (this.itsRepresentationInstanceTransform != null)
			{
				return this.itsCachedRepresentationSize * this.itsRepresentationInstanceTransform.localScale.x;
			}
			return this.itsCachedRepresentationSize;
		}

		// Token: 0x06000CC2 RID: 3266 RVA: 0x0005ECE4 File Offset: 0x0005CEE4
		public void UpdateVisibility()
		{
			if (this.itsMapIcon != null)
			{
				if (this.itsRepresentationInstance != null)
				{
					bool flag = this.itsMapIcon.GetIsVisible() && this.itsVisibility;
					if (flag != KGFMapSystem.KGFGetActive(this.itsRepresentationInstance))
					{
						foreach (object obj in this.itsRepresentationInstance.transform)
						{
							Transform transform = (Transform)obj;
							GameObject gameObject = transform.gameObject;
							KGFMapSystem.KGFSetChildrenActiveRecursively(gameObject, flag);
						}
						KGFMapSystem.KGFSetChildrenActiveRecursively(this.itsRepresentationInstance, flag);
						if (this.itsModule != null)
						{
							this.itsModule.LogInfo(string.Format("Icon of '{0}' (category='{1}') changed visibility to: {2}", this.itsMapIcon.GetTransform().name, this.itsMapIcon.GetCategory(), flag), this.itsModule.name, this.itsModule);
						}
					}
				}
				if (this.itsRepresentationArrowInstance != null)
				{
					KGFMapSystem.KGFSetChildrenActiveRecursively(this.itsRepresentationArrowInstance, this.itsMapIcon.GetIsVisible() && this.itsVisibility && this.itsVisibilityArrow && this.itsMapIcon.GetIsArrowVisible());
				}
			}
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x0005EE60 File Offset: 0x0005D060
		public void UpdateIcon()
		{
			if (this.itsMapIcon != null)
			{
				this.SetColorsInChildren(this.itsRepresentationArrowInstance, this.itsMapIcon.GetColor());
				this.SetColorsInChildren(this.itsRepresentationInstance, this.itsMapIcon.GetColor());
				if (this.itsRepresentationArrowInstance != null)
				{
					MeshRenderer component = this.itsRepresentationArrowInstance.GetComponent<MeshRenderer>();
					if (component != null)
					{
						component.material.mainTexture = this.itsMapIcon.GetTextureArrow();
					}
				}
			}
		}

		// Token: 0x06000CC4 RID: 3268 RVA: 0x0005EEE8 File Offset: 0x0005D0E8
		private void SetColorsInChildren(GameObject theGameObject, Color theColor)
		{
			if (theGameObject != null)
			{
				MeshRenderer[] componentsInChildren = theGameObject.GetComponentsInChildren<MeshRenderer>(true);
				if (componentsInChildren != null)
				{
					foreach (MeshRenderer meshRenderer in componentsInChildren)
					{
						Material sharedMaterial = meshRenderer.sharedMaterial;
						if (sharedMaterial != null)
						{
							sharedMaterial.color = theColor;
						}
					}
				}
			}
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x0005EF48 File Offset: 0x0005D148
		public void SetVisibility(bool theVisible)
		{
			if (theVisible != this.itsVisibility)
			{
				this.itsVisibility = theVisible;
				this.UpdateVisibility();
			}
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x0005EF64 File Offset: 0x0005D164
		public bool GetMapIconVisibilityEffective()
		{
			return this.itsMapIcon != null && this.itsVisibility && this.itsMapIcon.GetIsVisible();
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x0005EF98 File Offset: 0x0005D198
		public void ShowArrow(bool theShow)
		{
			if (theShow != this.itsVisibilityArrow && this.itsRepresentationArrowInstance != null)
			{
				this.itsVisibilityArrow = theShow;
				this.UpdateVisibility();
			}
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x0005EFD0 File Offset: 0x0005D1D0
		public bool GetIsArrowVisible()
		{
			return this.itsVisibilityArrow;
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x0005EFD8 File Offset: 0x0005D1D8
		public void Destroy()
		{
			if (this.itsRepresentationArrowInstance != null)
			{
				UnityEngine.Object.Destroy(this.itsRepresentationArrowInstance);
			}
			if (this.itsRepresentationInstance != null)
			{
				UnityEngine.Object.Destroy(this.itsRepresentationInstance);
			}
		}

		// Token: 0x04000C82 RID: 3202
		public KGFMapSystem itsModule;

		// Token: 0x04000C83 RID: 3203
		public KGFIMapIcon itsMapIcon;

		// Token: 0x04000C84 RID: 3204
		public GameObject itsRepresentationInstance;

		// Token: 0x04000C85 RID: 3205
		public Transform itsRepresentationInstanceTransform;

		// Token: 0x04000C86 RID: 3206
		public bool itsRotate;

		// Token: 0x04000C87 RID: 3207
		public GameObject itsRepresentationArrowInstance;

		// Token: 0x04000C88 RID: 3208
		public Transform itsRepresentationArrowInstanceTransform;

		// Token: 0x04000C89 RID: 3209
		public Transform itsMapIconTransform;

		// Token: 0x04000C8A RID: 3210
		private bool itsVisibility;

		// Token: 0x04000C8B RID: 3211
		private bool itsVisibilityArrow;

		// Token: 0x04000C8C RID: 3212
		public Vector3 itsCachedRepresentationSize = Vector3.zero;
	}

	// Token: 0x020001F6 RID: 502
	// (Invoke) Token: 0x06000DF5 RID: 3573
	public delegate void RenderToolTipMethodType(string theToolTipText);
}
