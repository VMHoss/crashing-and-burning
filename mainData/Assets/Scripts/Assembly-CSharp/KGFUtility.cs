using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using UnityEngine;

// Token: 0x0200017C RID: 380
public static class KGFUtility
{
	// Token: 0x06000B63 RID: 2915 RVA: 0x000553C4 File Offset: 0x000535C4
	public static T[] GetComponentsInterface<T>(this MonoBehaviour theMonobehaviour) where T : class
	{
		List<T> list = new List<T>();
		foreach (MonoBehaviour monoBehaviour in theMonobehaviour.GetComponents<MonoBehaviour>())
		{
			T t = monoBehaviour as T;
			if (t != null)
			{
				list.Add(t);
			}
		}
		return list.ToArray();
	}

	// Token: 0x06000B64 RID: 2916 RVA: 0x00055424 File Offset: 0x00053624
	public static T GetComponentInterface<T>(this MonoBehaviour theMonobehaviour) where T : class
	{
		T[] componentsInterface = theMonobehaviour.GetComponentsInterface<T>();
		if (componentsInterface.Length > 0)
		{
			return componentsInterface[0];
		}
		return (T)((object)null);
	}

	// Token: 0x06000B65 RID: 2917 RVA: 0x00055450 File Offset: 0x00053650
	public static List<T> Sorted<T>(this List<T> theList)
	{
		List<T> list = new List<T>(theList);
		list.Sort();
		return list;
	}

	// Token: 0x06000B66 RID: 2918 RVA: 0x0005546C File Offset: 0x0005366C
	public static bool ContainsItem<T>(this IEnumerable<T> theList, T theNeedle) where T : class
	{
		foreach (T t in theList)
		{
			if (theNeedle.Equals(t))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000B67 RID: 2919 RVA: 0x000554E8 File Offset: 0x000536E8
	public static string JoinToString<T>(this IEnumerable<T> theList, string theSeparator)
	{
		if (theList == null)
		{
			return string.Empty;
		}
		List<string> list = new List<string>();
		foreach (T t in theList)
		{
			list.Add(t.ToString());
		}
		return string.Join(theSeparator, list.ToArray());
	}

	// Token: 0x06000B68 RID: 2920 RVA: 0x00055570 File Offset: 0x00053770
	public static IEnumerable<T> InsertItem<T>(this IEnumerable<T> theList, T theItem, int thePosition)
	{
		int i = 0;
		bool anInserted = false;
		foreach (T anElement in theList)
		{
			if (i == thePosition)
			{
				yield return theItem;
				anInserted = true;
			}
			yield return anElement;
			i++;
		}
		if (!anInserted)
		{
			yield return theItem;
		}
		yield break;
	}

	// Token: 0x06000B69 RID: 2921 RVA: 0x000555B8 File Offset: 0x000537B8
	public static IEnumerable<T> AppendItem<T>(this IEnumerable<T> theList, T theItem)
	{
		foreach (T anElement in theList)
		{
			yield return anElement;
		}
		yield return theItem;
		yield break;
	}

	// Token: 0x06000B6A RID: 2922 RVA: 0x000555F0 File Offset: 0x000537F0
	public static IEnumerable<T> Distinct<T>(this IEnumerable<T> theList)
	{
		List<T> aDistinctList = new List<T>();
		foreach (T anElement in theList)
		{
			if (!aDistinctList.Contains(anElement))
			{
				aDistinctList.Add(anElement);
				yield return anElement;
			}
		}
		yield break;
	}

	// Token: 0x06000B6B RID: 2923 RVA: 0x0005561C File Offset: 0x0005381C
	public static IEnumerable<T> Remove<T>(this IEnumerable<T> theMainList, T[] theListToRemove)
	{
		List<T> aListToRemove = new List<T>(theListToRemove);
		foreach (T anElement in theMainList)
		{
			if (!aListToRemove.Contains(anElement))
			{
				yield return anElement;
			}
		}
		yield break;
	}

	// Token: 0x06000B6C RID: 2924 RVA: 0x00055654 File Offset: 0x00053854
	public static IEnumerable<T> Sorted<T>(this IEnumerable<T> theList)
	{
		List<T> aList = new List<T>(theList);
		aList.Sort();
		foreach (T aT in aList)
		{
			yield return aT;
		}
		yield break;
	}

	// Token: 0x06000B6D RID: 2925 RVA: 0x00055680 File Offset: 0x00053880
	public static IEnumerable<T> Sorted<T>(this IEnumerable<T> theList, Comparison<T> theComparison)
	{
		List<T> aList = new List<T>(theList);
		aList.Sort(theComparison);
		foreach (T aT in aList)
		{
			yield return aT;
		}
		yield break;
	}

	// Token: 0x06000B6E RID: 2926 RVA: 0x000556B8 File Offset: 0x000538B8
	public static List<T> ToDynList<T>(this IEnumerable<T> theList)
	{
		return new List<T>(theList);
	}

	// Token: 0x06000B6F RID: 2927 RVA: 0x000556C0 File Offset: 0x000538C0
	public static void SetScaleRecursively(this Transform theTransform, Vector3 theScale)
	{
		foreach (object obj in theTransform)
		{
			Transform theTransform2 = (Transform)obj;
			theTransform2.SetScaleRecursively(theScale);
		}
		theTransform.localScale = theScale;
	}

	// Token: 0x06000B70 RID: 2928 RVA: 0x00055734 File Offset: 0x00053934
	public static void SetChildrenActiveRecursively(this GameObject theGameObject, bool theActive)
	{
		foreach (object obj in theGameObject.transform)
		{
			Transform transform = (Transform)obj;
			transform.gameObject.SetActiveRecursively(theActive);
		}
	}

	// Token: 0x06000B71 RID: 2929 RVA: 0x000557A8 File Offset: 0x000539A8
	public static void SetLayerRecursively(this GameObject theGameObject, int theLayer)
	{
		theGameObject.layer = theLayer;
		foreach (object obj in theGameObject.transform)
		{
			Transform transform = (Transform)obj;
			GameObject gameObject = transform.gameObject;
			gameObject.SetLayerRecursively(theLayer);
		}
	}

	// Token: 0x06000B72 RID: 2930 RVA: 0x00055828 File Offset: 0x00053A28
	public static long DateToUnix(this DateTime theDate)
	{
		return (long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
	}

	// Token: 0x06000B73 RID: 2931 RVA: 0x00055854 File Offset: 0x00053A54
	public static string Shortened(this string theString, int theMaxLength)
	{
		if (theString.Length > theMaxLength)
		{
			return theString.Substring(0, theMaxLength - 2) + "..";
		}
		return theString;
	}

	// Token: 0x06000B74 RID: 2932 RVA: 0x00055884 File Offset: 0x00053A84
	public static string Join(this string theSeparator, params string[] theItems)
	{
		return string.Join(theSeparator, theItems);
	}

	// Token: 0x06000B75 RID: 2933 RVA: 0x00055890 File Offset: 0x00053A90
	public static string Join(this string theSeparator, IEnumerable<string> theItems)
	{
		return string.Join(theSeparator, new List<string>(theItems).ToArray());
	}

	// Token: 0x06000B76 RID: 2934 RVA: 0x000558A4 File Offset: 0x00053AA4
	public static string RemoveRight(this string theString, char theSeparator)
	{
		string text = string.Empty + theString;
		while (text.Length > 0 && text[text.Length - 1] != theSeparator)
		{
			text = text.Remove(text.Length - 1);
		}
		return text;
	}

	// Token: 0x06000B77 RID: 2935 RVA: 0x000558F4 File Offset: 0x00053AF4
	public static string GetLastPart(this string theString, char theSeparator)
	{
		string[] array = theString.Split(new char[]
		{
			theSeparator
		});
		return array[array.Length - 1];
	}

	// Token: 0x06000B78 RID: 2936 RVA: 0x0005591C File Offset: 0x00053B1C
	public static string ConvertPathToUnity(string thePlatformPath)
	{
		return thePlatformPath.Replace(Path.DirectorySeparatorChar, '/');
	}

	// Token: 0x06000B79 RID: 2937 RVA: 0x0005592C File Offset: 0x00053B2C
	public static string ConvertPathToPlatformSpecific(string theUnityPath)
	{
		return theUnityPath.Replace('/', Path.DirectorySeparatorChar);
	}

	// Token: 0x06000B7A RID: 2938 RVA: 0x0005593C File Offset: 0x00053B3C
	public static void SetMouseRect(Rect theRect)
	{
	}

	// Token: 0x06000B7B RID: 2939 RVA: 0x00055940 File Offset: 0x00053B40
	public static void ClearMouseRect()
	{
	}

	// Token: 0x06000B7C RID: 2940 RVA: 0x00055944 File Offset: 0x00053B44
	public static Rect GetWindowRect()
	{
		return new Rect(0f, 0f, 0f, 0f);
	}

	// Token: 0x06000B7D RID: 2941 RVA: 0x00055960 File Offset: 0x00053B60
	public static float PingPong(float theTime, float theMaxValue, float thePingStayTime, float thePongStayTime, float theTransitionTime)
	{
		float num = thePingStayTime + thePongStayTime + 2f * theTransitionTime;
		float num2 = theTime % num;
		if (num2 < thePingStayTime)
		{
			return 0f;
		}
		if (num2 < thePingStayTime + theTransitionTime)
		{
			return (num2 - thePingStayTime) * theMaxValue / theTransitionTime;
		}
		if (num2 < thePingStayTime + theTransitionTime + thePongStayTime)
		{
			return theMaxValue;
		}
		return theMaxValue - (num2 - (thePingStayTime + theTransitionTime + thePongStayTime)) * theMaxValue / theTransitionTime;
	}

	// Token: 0x06000B7E RID: 2942 RVA: 0x000559BC File Offset: 0x00053BBC
	private static Color32[] BlockBlur1D(Color32[] thePixels, int theWidth, int theHeight, int theBlurRadius)
	{
		Color32[] array = new Color32[thePixels.Length];
		for (int i = 0; i < theHeight; i++)
		{
			for (int j = 0; j < theWidth; j++)
			{
				int num3;
				int num2;
				int num = num2 = (num3 = 0);
				int num4 = 0;
				for (int k = j - theBlurRadius; k <= j + theBlurRadius; k++)
				{
					Color32 color = thePixels[Mathf.Clamp(k, 0, theWidth - 1) + i * theWidth];
					num2 += (int)color.r;
					num += (int)color.g;
					num3 += (int)color.b;
					num4++;
				}
				Color32 color2 = thePixels[j + i * theWidth];
				color2.r = (byte)(num2 / num4);
				color2.g = (byte)(num / num4);
				color2.b = (byte)(num3 / num4);
				array[j + i * theWidth] = color2;
			}
		}
		return array;
	}

	// Token: 0x06000B7F RID: 2943 RVA: 0x00055AAC File Offset: 0x00053CAC
	private static Color32[] BlockBlur2D(Color32[] thePixels, int theWidth, int theHeight, int theBlurRadiusX, int theBlurRadiusY)
	{
		Color32[] array = new Color32[thePixels.Length];
		for (int i = 0; i < theHeight; i++)
		{
			for (int j = 0; j < theWidth; j++)
			{
				int num3;
				int num2;
				int num = num2 = (num3 = 0);
				int num4 = (j - theBlurRadiusX < 0) ? 0 : (j - theBlurRadiusX);
				int num5 = (i - theBlurRadiusY < 0) ? 0 : (i - theBlurRadiusY);
				int num6 = 0;
				int num7 = num5;
				while (num7 < theHeight && num7 <= i + theBlurRadiusY)
				{
					int num8 = num4;
					while (num8 < theWidth && num8 <= j + theBlurRadiusX)
					{
						Color32 color = thePixels[num8 + num7 * theWidth];
						num2 += (int)color.r;
						num += (int)color.g;
						num3 += (int)color.b;
						num6++;
						num8++;
					}
					num7++;
				}
				Color32 color2 = thePixels[j + i * theWidth];
				color2.r = (byte)(num2 / num6);
				color2.g = (byte)(num / num6);
				color2.b = (byte)(num3 / num6);
				array[j + i * theWidth] = color2;
			}
		}
		return array;
	}

	// Token: 0x06000B80 RID: 2944 RVA: 0x00055BE8 File Offset: 0x00053DE8
	public static Rect GetCachedRect(float theX, float theY, float theWidth, float theHeight)
	{
		KGFUtility.itsCachedRect.x = theX;
		KGFUtility.itsCachedRect.y = theY;
		KGFUtility.itsCachedRect.width = theWidth;
		KGFUtility.itsCachedRect.height = theHeight;
		return KGFUtility.itsCachedRect;
	}

	// Token: 0x06000B81 RID: 2945 RVA: 0x00055C28 File Offset: 0x00053E28
	public static Rect GetCachedRect(Rect theRect)
	{
		KGFUtility.itsCachedRect.x = theRect.x;
		KGFUtility.itsCachedRect.y = theRect.y;
		KGFUtility.itsCachedRect.width = theRect.width;
		KGFUtility.itsCachedRect.height = theRect.height;
		return KGFUtility.itsCachedRect;
	}

	// Token: 0x06000B82 RID: 2946 RVA: 0x00055C80 File Offset: 0x00053E80
	public static Vector3 GetCachedVector3(float theX, float theY, float theZ)
	{
		KGFUtility.itsCachedVector3.x = theX;
		KGFUtility.itsCachedVector3.y = theY;
		KGFUtility.itsCachedVector3.z = theZ;
		return KGFUtility.itsCachedVector3;
	}

	// Token: 0x06000B83 RID: 2947 RVA: 0x00055CB4 File Offset: 0x00053EB4
	public static Vector2 GetCachedVector2(float theX, float theY)
	{
		KGFUtility.itsCachedVector2.x = theX;
		KGFUtility.itsCachedVector2.y = theY;
		return KGFUtility.itsCachedVector2;
	}

	// Token: 0x06000B84 RID: 2948 RVA: 0x00055CD4 File Offset: 0x00053ED4
	public static DateTime DateFromUnix(long theSeconds)
	{
		DateTime dateTime = new DateTime(1970, 1, 1);
		return dateTime.AddSeconds((double)theSeconds);
	}

	// Token: 0x06000B85 RID: 2949 RVA: 0x00055CF8 File Offset: 0x00053EF8
	public static string ToHexString(byte[] buffer)
	{
		string text = string.Empty;
		foreach (byte b in buffer)
		{
			text += string.Format("{0:x02}", b);
		}
		return text;
	}

	// Token: 0x06000B86 RID: 2950 RVA: 0x00055D40 File Offset: 0x00053F40
	public static string GetHashMD5OfFile(string theFilePath)
	{
		if (File.Exists(theFilePath))
		{
			MD5CryptoServiceProvider md5CryptoServiceProvider = new MD5CryptoServiceProvider();
			FileStream fileStream = File.Open(theFilePath, FileMode.Open);
			byte[] buffer = md5CryptoServiceProvider.ComputeHash(fileStream);
			fileStream.Close();
			return KGFUtility.ToHexString(buffer);
		}
		return null;
	}

	// Token: 0x06000B87 RID: 2951 RVA: 0x00055D7C File Offset: 0x00053F7C
	public static Texture2D GetBestAspectMatchingTexture(float theAspectRatio, params Texture2D[] theTextures)
	{
		Texture2D texture2D = null;
		if (theTextures.Length > 0)
		{
			texture2D = theTextures[0];
			for (int i = 1; i < theTextures.Length; i++)
			{
				Texture2D texture2D2 = theTextures[i];
				if (!(texture2D2 == null))
				{
					float num = Mathf.Abs(theAspectRatio - (float)texture2D.width / (float)texture2D.height);
					float num2 = Mathf.Abs(theAspectRatio - (float)texture2D2.width / (float)texture2D2.height);
					if (num2 < num)
					{
						texture2D = texture2D2;
					}
				}
			}
		}
		return texture2D;
	}

	// Token: 0x06000B88 RID: 2952 RVA: 0x00055DFC File Offset: 0x00053FFC
	public static Quaternion SetLookRotationSafe(Quaternion theQuaternion, Vector3 theUpVector, Vector3 theLookRotation, Vector3 theAlternativeLookDirection)
	{
		if (theAlternativeLookDirection.magnitude == 0f)
		{
			throw new Exception("Alternative look vector can never be 0!");
		}
		if (theLookRotation.magnitude != 0f)
		{
			theQuaternion.SetLookRotation(theLookRotation, theUpVector);
			return theQuaternion;
		}
		theQuaternion.SetLookRotation(theAlternativeLookDirection, theUpVector);
		return theQuaternion;
	}

	// Token: 0x04000B82 RID: 2946
	private static Rect itsCachedRect = default(Rect);

	// Token: 0x04000B83 RID: 2947
	private static Vector3 itsCachedVector3 = default(Vector3);

	// Token: 0x04000B84 RID: 2948
	private static Vector2 itsCachedVector2 = default(Vector2);
}
