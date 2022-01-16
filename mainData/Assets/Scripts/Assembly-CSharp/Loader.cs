using System;
using UnityEngine;

// Token: 0x020000D3 RID: 211
public class Loader
{
	// Token: 0x0600065D RID: 1629 RVA: 0x0002DBB4 File Offset: 0x0002BDB4
	public static UnityEngine.Object LoadObject(string aBundleName, string aPathObjectName)
	{
		if (Data.useAssetBundles)
		{
			aPathObjectName = aPathObjectName.Substring(aPathObjectName.LastIndexOf('/') + 1);
			return Data.stringToBundle[aBundleName].assetBundle.Load(aPathObjectName);
		}
		return Resources.Load("BundleAssets/" + aBundleName + "/" + aPathObjectName);
	}

	// Token: 0x0600065E RID: 1630 RVA: 0x0002DC0C File Offset: 0x0002BE0C
	public static GameObject LoadGameObject(string aBundleName, string aPathGameObjectName)
	{
		UnityEngine.Object @object = null;
		if (Data.useAssetBundles)
		{
			aPathGameObjectName = aPathGameObjectName.Substring(aPathGameObjectName.LastIndexOf('/') + 1);
			if (!Data.highDetails)
			{
				@object = Data.stringToBundle[aBundleName].assetBundle.Load(aPathGameObjectName + "LOW");
			}
			if (@object == null)
			{
				@object = Data.stringToBundle[aBundleName].assetBundle.Load(aPathGameObjectName);
			}
		}
		else
		{
			if (!Data.highDetails)
			{
				@object = Resources.Load(string.Concat(new string[]
				{
					"BundleAssets/",
					aBundleName,
					"/Prefabs/",
					aPathGameObjectName,
					"LOW"
				}), typeof(GameObject));
			}
			if (@object == null)
			{
				@object = Resources.Load("BundleAssets/" + aBundleName + "/Prefabs/" + aPathGameObjectName, typeof(GameObject));
			}
		}
		if (@object != null)
		{
			return UnityEngine.Object.Instantiate(@object) as GameObject;
		}
		return null;
	}

	// Token: 0x0600065F RID: 1631 RVA: 0x0002DD18 File Offset: 0x0002BF18
	public static GameObject LoadChildObject(string aBundleName, string aPathGameObjectName, string aChildObjectName)
	{
		UnityEngine.Object @object;
		if (Data.useAssetBundles)
		{
			aPathGameObjectName = aPathGameObjectName.Substring(aPathGameObjectName.LastIndexOf('/') + 1);
			@object = Data.stringToBundle[aBundleName].assetBundle.Load(aPathGameObjectName);
		}
		else
		{
			@object = Resources.Load("BundleAssets/" + aBundleName + "/Prefabs/" + aPathGameObjectName, typeof(GameObject));
		}
		if (!(@object != null))
		{
			return null;
		}
		Transform transform = (@object as GameObject).transform.Find(aChildObjectName);
		if (transform == null)
		{
			return null;
		}
		GameObject gameObject = transform.gameObject;
		if (gameObject != null)
		{
			return UnityEngine.Object.Instantiate(gameObject) as GameObject;
		}
		return null;
	}

	// Token: 0x06000660 RID: 1632 RVA: 0x0002DDCC File Offset: 0x0002BFCC
	public static Material LoadMaterial(string aBundleName, string aPathMaterialName)
	{
		if (Data.useAssetBundles)
		{
			aPathMaterialName = aPathMaterialName.Substring(aPathMaterialName.LastIndexOf('/') + 1);
			return Data.stringToBundle[aBundleName].assetBundle.Load(aPathMaterialName) as Material;
		}
		return Resources.Load("BundleAssets/" + aBundleName + "/Materials/" + aPathMaterialName, typeof(Material)) as Material;
	}

	// Token: 0x06000661 RID: 1633 RVA: 0x0002DE38 File Offset: 0x0002C038
	public static Texture LoadTexture(string aBundleName, string aPathTextureName)
	{
		if (Data.useAssetBundles)
		{
			aPathTextureName = aPathTextureName.Substring(aPathTextureName.LastIndexOf('/') + 1);
			return Data.stringToBundle[aBundleName].assetBundle.Load(aPathTextureName) as Texture;
		}
		return Resources.Load("BundleAssets/" + aBundleName + "/Textures/" + aPathTextureName, typeof(Texture)) as Texture;
	}

	// Token: 0x06000662 RID: 1634 RVA: 0x0002DEA4 File Offset: 0x0002C0A4
	public static TextAsset LoadTextFile(string aBundleName, string aPathTextFileName)
	{
		if (Data.useAssetBundles)
		{
			aPathTextFileName = aPathTextFileName.Substring(aPathTextFileName.LastIndexOf('/') + 1);
			return Data.stringToBundle[aBundleName].assetBundle.Load(aPathTextFileName) as TextAsset;
		}
		return Resources.Load("BundleAssets/" + aBundleName + "/" + aPathTextFileName, typeof(TextAsset)) as TextAsset;
	}

	// Token: 0x06000663 RID: 1635 RVA: 0x0002DF10 File Offset: 0x0002C110
	public static AudioClip LoadAudio(string aBundleName, string anAudioSourceName)
	{
		if (Data.useAssetBundles)
		{
			anAudioSourceName = anAudioSourceName.Substring(anAudioSourceName.LastIndexOf('/') + 1);
			return Data.stringToBundle[aBundleName].assetBundle.Load(anAudioSourceName) as AudioClip;
		}
		return Resources.Load("BundleAssets/" + aBundleName + "/Audio/" + anAudioSourceName, typeof(AudioClip)) as AudioClip;
	}

	// Token: 0x06000664 RID: 1636 RVA: 0x0002DF7C File Offset: 0x0002C17C
	public static PhysicMaterial LoadPhysicMaterial(string aBundleName, string aPathPhysicMaterialName)
	{
		if (Data.useAssetBundles)
		{
			aPathPhysicMaterialName = aPathPhysicMaterialName.Substring(aPathPhysicMaterialName.LastIndexOf('/') + 1);
			return Data.stringToBundle[aBundleName].assetBundle.Load(aPathPhysicMaterialName) as PhysicMaterial;
		}
		return Resources.Load("BundleAssets/" + aBundleName + "/PhysicMaterials/" + aPathPhysicMaterialName, typeof(PhysicMaterial)) as PhysicMaterial;
	}
}
