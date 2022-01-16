using System;
using UnityEngine;

// Token: 0x020000D9 RID: 217
public class Options
{
	// Token: 0x060006A4 RID: 1700 RVA: 0x0002F620 File Offset: 0x0002D820
	public static void SetVisuals()
	{
		string value = "Fastest";
		if (Data.highDetails)
		{
			value = "Fantastic";
		}
		string[] names = QualitySettings.names;
		int num = Array.IndexOf<string>(names, value);
		if (num == -1)
		{
			throw new UnityException("Unknown quality setting!");
		}
		QualitySettings.SetQualityLevel(num, true);
		Camera[] array = UnityEngine.Object.FindObjectsOfType(typeof(Camera)) as Camera[];
		foreach (Camera camera in array)
		{
			if (camera.gameObject.layer != LayerMask.NameToLayer("Interface") && camera.gameObject.layer != LayerMask.NameToLayer("Skybox") && camera.gameObject.layer != LayerMask.NameToLayer("Player"))
			{
				if (Data.highDetails)
				{
					if (camera.gameObject.GetComponent<BloomAndLensFlares>() == null)
					{
						camera.gameObject.AddComponent<BloomAndLensFlares>();
					}
					BloomAndLensFlares component = camera.gameObject.GetComponent<BloomAndLensFlares>();
					component.screenBlendMode = BloomScreenBlendMode.Screen;
					component.lensFlareShader = (Resources.Load("Shaders/LensFlareCreate") as Shader);
					component.vignetteShader = (Resources.Load("Shaders/VignetteShader") as Shader);
					component.separableBlurShader = (Resources.Load("Shaders/SeparableBlurPlus") as Shader);
					component.addBrightStuffOneOneShader = (Resources.Load("Shaders/BlendOneOne") as Shader);
					component.screenBlendShader = (Resources.Load("Shaders/Blend") as Shader);
					component.hollywoodFlaresShader = (Resources.Load("Shaders/MultiPassHollywoodFlares") as Shader);
					component.brightPassFilterShader = (Resources.Load("Shaders/BrightPassFilter") as Shader);
					component.bloomIntensity = Data.Shared["BloomSetting"].d["Intensity"].f;
					component.bloomBlurIterations = Data.Shared["BloomSetting"].d["Iterations"].i;
					component.bloomThreshhold = Data.Shared["BloomSetting"].d["Threshhold"].f;
				}
				else
				{
					BloomAndLensFlares component2 = camera.gameObject.GetComponent<BloomAndLensFlares>();
					if (component2 != null)
					{
						UnityEngine.Object.Destroy(component2);
					}
				}
			}
		}
		if (GameData.playerCarScript != null)
		{
			GameData.playerCarScript.SetVisuals();
		}
		Shaders.SetVisuals();
		RenderSettings.fog = Data.highDetails;
	}

	// Token: 0x060006A5 RID: 1701 RVA: 0x0002F8A0 File Offset: 0x0002DAA0
	public static void ToggleFullScreen()
	{
		if (!Screen.fullScreen)
		{
			Screen.SetResolution(Data.fullScreenWidth, Data.fullScreenHeight, true);
			Screen.fullScreen = true;
		}
		else
		{
			Screen.fullScreen = false;
		}
	}
}
