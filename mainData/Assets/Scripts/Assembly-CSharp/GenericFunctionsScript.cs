using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000CC RID: 204
public class GenericFunctionsScript
{
	// Token: 0x06000612 RID: 1554 RVA: 0x0002BD28 File Offset: 0x00029F28
	public static GameObject FindChild(GameObject aGameObject, string aName)
	{
		for (int i = aGameObject.transform.childCount - 1; i >= 0; i--)
		{
			Transform child = aGameObject.transform.GetChild(i);
			GameObject gameObject = GenericFunctionsScript.FindChild(child.gameObject, aName);
			if (gameObject != null)
			{
				return gameObject;
			}
			if (aGameObject.transform.GetChild(i).gameObject.name == aName)
			{
				return aGameObject.transform.GetChild(i).gameObject;
			}
		}
		return null;
	}

	// Token: 0x06000613 RID: 1555 RVA: 0x0002BDB4 File Offset: 0x00029FB4
	public static GameObject FindChildAbsolute(GameObject aGameObject, string[] aNameList)
	{
		if (aNameList.Length == 0)
		{
			return aGameObject;
		}
		return GenericFunctionsScript.FindChildAbsolute(aGameObject, aNameList, 0);
	}

	// Token: 0x06000614 RID: 1556 RVA: 0x0002BDC8 File Offset: 0x00029FC8
	public static List<GameObject> FindGameObjectsWithLayer(int aLayer)
	{
		GameObject[] array = UnityEngine.Object.FindObjectsOfType(typeof(GameObject)) as GameObject[];
		List<GameObject> list = new List<GameObject>();
		for (int i = array.Length - 1; i >= 0; i--)
		{
			if (array[i].layer == aLayer)
			{
				list.Add(array[i]);
			}
		}
		if (list.Count == 0)
		{
			return null;
		}
		return list;
	}

	// Token: 0x06000615 RID: 1557 RVA: 0x0002BE2C File Offset: 0x0002A02C
	private static GameObject FindChildAbsolute(GameObject aGameObject, string[] aNameList, int aIndex)
	{
		if (aIndex == aNameList.Length)
		{
			return aGameObject;
		}
		for (int i = aGameObject.transform.childCount - 1; i >= 0; i--)
		{
			Transform child = aGameObject.transform.GetChild(i);
			if (child.name == aNameList[aIndex])
			{
				return GenericFunctionsScript.FindChildAbsolute(child.gameObject, aNameList, aIndex + 1);
			}
		}
		return null;
	}

	// Token: 0x06000616 RID: 1558 RVA: 0x0002BE94 File Offset: 0x0002A094
	public static void Fade(string fadePreset)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/Fade")) as GameObject;
		GameObject gameObject2 = GameObject.Find("Anchor");
		if (gameObject2 == null)
		{
			gameObject2 = GameObject.Find("Loader");
		}
		gameObject.transform.parent = gameObject2.transform;
		gameObject.transform.localPosition = new Vector3(0f, 0f, -250f);
		UISprite component = gameObject.GetComponent<UISprite>();
		component.depth = 0;
		gameObject.layer = gameObject2.layer;
		Color color = new Color(0f, 0f, 0f, 1f);
		Color color2 = new Color(0f, 0f, 0f, 0f);
		Color color3 = new Color(1f, 1f, 1f, 1f);
		Color color4 = new Color(1f, 1f, 1f, 0f);
		GameObject gameObject3 = GameObject.Find("Interface");
		GameObject gameObject4 = GameObject.Find("Loader");
		float duration = 0f;
		UITweener.Method method = UITweener.Method.EaseOut;
		UITweener.Style style = UITweener.Style.Once;
		Color from = color;
		Color to = color2;
		GameObject eventReceiver = gameObject;
		string callWhenFinished = "DestroyMe";
		string text = "None";
		float time = 0.5f;
		switch (fadePreset)
		{
		case "ToBlackAndStartGame":
			duration = 0.5f;
			from = color2;
			to = color;
			callWhenFinished = "StartGame";
			eventReceiver = gameObject3;
			text = "Out";
			time = 1.5f;
			break;
		case "ToBlackHold":
			duration = 0.5f;
			method = UITweener.Method.EaseIn;
			to = color;
			from = color2;
			text = "Out";
			time = 0.5f;
			break;
		case "HoldFromBlack":
			duration = 0.5f;
			method = UITweener.Method.EaseIn;
			from = color;
			to = color2;
			text = "In";
			time = 0.5f;
			break;
		case "FromBlack":
			duration = 0.5f;
			from = color;
			to = color2;
			method = UITweener.Method.EaseIn;
			break;
		case "FromBlackMenu":
			duration = 1f;
			from = color;
			to = color2;
			method = UITweener.Method.EaseIn;
			break;
		case "FromBlackToLoader":
			duration = 0.5f;
			method = UITweener.Method.EaseIn;
			from = color;
			to = color2;
			break;
		case "ToBlack":
			duration = 0.5f;
			from = color2;
			to = color;
			break;
		case "FromBlackAndMenu":
			duration = 0.3f;
			from = color;
			to = color2;
			method = UITweener.Method.EaseIn;
			text = "In";
			time = 0.1f;
			break;
		case "ToBlackAndMenu":
			duration = 0.5f;
			from = color2;
			to = color;
			callWhenFinished = "QuitToMenu";
			eventReceiver = gameObject3;
			text = "Out";
			time = 0.5f;
			break;
		case "ToBlackAndShop":
			duration = 0.5f;
			from = color2;
			to = color;
			callWhenFinished = "ToShop";
			eventReceiver = gameObject3;
			text = "Out";
			time = 0.5f;
			break;
		case "ToBlackResultsToShop":
			duration = 0.5f;
			from = color2;
			to = color;
			callWhenFinished = "ResultsToShop";
			eventReceiver = gameObject3;
			text = "Out";
			time = 0.5f;
			break;
		case "ToBlackAndContinue":
			duration = 0.5f;
			from = color2;
			to = color;
			callWhenFinished = "Continue";
			eventReceiver = gameObject3;
			break;
		case "ToBlackAndComplete":
			duration = 0.5f;
			from = color2;
			to = color;
			callWhenFinished = "Complete";
			eventReceiver = gameObject3;
			break;
		case "ToBlackAndRestart":
			duration = 0.5f;
			from = color2;
			to = color;
			callWhenFinished = "Restart";
			eventReceiver = gameObject3;
			break;
		case "ToBlackAndRetry":
			duration = 0.5f;
			from = color2;
			to = color;
			callWhenFinished = "Retry";
			eventReceiver = gameObject3;
			break;
		case "FromBlackToMenu":
			duration = 0.5f;
			from = color;
			to = color2;
			method = UITweener.Method.EaseIn;
			text = "In";
			time = 1f;
			break;
		case "FromWhiteChallengeSelect":
			duration = 0.6f;
			from = color4;
			to = color3;
			method = UITweener.Method.EaseIn;
			text = "Out";
			time = 0.3f;
			callWhenFinished = "ChallengeSelector";
			eventReceiver = gameObject3;
			break;
		case "FromZeroToBlackStartGame":
			duration = 0.5f;
			from = color2;
			to = color;
			method = UITweener.Method.EaseIn;
			callWhenFinished = "StartGame";
			eventReceiver = gameObject3;
			text = "Out";
			time = 1f;
			break;
		case "FromZeroToBlackQuitApplication":
			duration = 0.5f;
			from = color2;
			to = color;
			method = UITweener.Method.EaseIn;
			callWhenFinished = "QuitApplication";
			eventReceiver = gameObject3;
			text = "Out";
			time = 1f;
			break;
		case "FromZeroToWhiteLoader":
			duration = 0.5f;
			from = color4;
			to = color3;
			method = UITweener.Method.EaseIn;
			callWhenFinished = "LoaderToMenu";
			eventReceiver = gameObject4;
			break;
		case "FromZeroToWhite":
			duration = 0.3f;
			from = color4;
			to = color3;
			method = UITweener.Method.EaseIn;
			break;
		case "FromWhiteToZero":
			duration = 0.3f;
			from = color3;
			to = color4;
			method = UITweener.Method.EaseIn;
			break;
		case "FromBlackToZero":
			duration = 0.3f;
			from = color;
			to = color2;
			method = UITweener.Method.EaseIn;
			break;
		case "FromWhiteToZeroUnpause":
			duration = 0.1f;
			from = color3;
			to = color4;
			method = UITweener.Method.EaseIn;
			break;
		case "FromWhiteToZeroInterface":
			duration = 0.7f;
			from = color3;
			to = color4;
			method = UITweener.Method.EaseIn;
			text = "In";
			time = 1f;
			break;
		case "FromZeroToBlackQuit":
			duration = 0.3f;
			from = color2;
			to = color;
			method = UITweener.Method.EaseIn;
			callWhenFinished = "QuitGame";
			eventReceiver = gameObject3;
			break;
		case "FromZeroToBlackNextLevel":
			duration = 0.3f;
			from = color2;
			to = color;
			method = UITweener.Method.EaseIn;
			callWhenFinished = "NextLevel";
			eventReceiver = gameObject3;
			text = "Out";
			time = 1f;
			break;
		case "FromZeroToWhiteRetry":
			duration = 0.3f;
			from = color4;
			to = color3;
			method = UITweener.Method.EaseIn;
			callWhenFinished = "RetryGame";
			eventReceiver = gameObject3;
			break;
		case "FromZeroToWhiteNext":
			duration = 0.6f;
			from = color4;
			to = color3;
			method = UITweener.Method.EaseIn;
			callWhenFinished = "NextChallenge";
			eventReceiver = gameObject3;
			text = "Out";
			time = 0.3f;
			break;
		case "FromZeroToMenu":
			duration = 0.3f;
			from = color4;
			to = color3;
			method = UITweener.Method.EaseIn;
			callWhenFinished = "BackToMenu";
			eventReceiver = gameObject3;
			break;
		case "UnpausingFade":
			duration = 0.1f;
			from = color4;
			to = color3;
			method = UITweener.Method.EaseOut;
			eventReceiver = gameObject3;
			callWhenFinished = "UnpauseFade";
			break;
		}
		TweenColor component2 = gameObject.GetComponent<TweenColor>();
		component2.duration = duration;
		component2.method = method;
		component2.from = from;
		component2.to = to;
		component2.style = style;
		component2.eventReceiver = eventReceiver;
		component2.callWhenFinished = callWhenFinished;
		if (text != "None")
		{
			HoldFade holdFade = gameObject.AddComponent<HoldFade>();
			holdFade.type = text;
			holdFade.time = time;
		}
	}

	// Token: 0x06000617 RID: 1559 RVA: 0x0002C6D0 File Offset: 0x0002A8D0
	public static void Medal(int aMedalNumber)
	{
		Debug.Log("Medal called: " + aMedalNumber);
		Scripts.interfaceScript.Trophy("Trophy" + aMedalNumber.ToString());
	}

	// Token: 0x06000618 RID: 1560 RVA: 0x0002C710 File Offset: 0x0002A910
	public static Vector3 VectorFromList(List<DicEntry> aList)
	{
		return new Vector3(aList[0].f, aList[1].f, aList[2].f);
	}

	// Token: 0x06000619 RID: 1561 RVA: 0x0002C748 File Offset: 0x0002A948
	public static Quaternion QuaternionFromList(List<DicEntry> aList)
	{
		return new Quaternion(aList[0].f, aList[1].f, aList[2].f, aList[3].f);
	}

	// Token: 0x0600061A RID: 1562 RVA: 0x0002C78C File Offset: 0x0002A98C
	public static Color ColorFromList(List<DicEntry> aList)
	{
		if (aList.Count == 3)
		{
			return new Color(aList[0].f, aList[1].f, aList[2].f);
		}
		return new Color(aList[0].f, aList[1].f, aList[2].f, aList[3].f);
	}

	// Token: 0x0600061B RID: 1563 RVA: 0x0002C804 File Offset: 0x0002AA04
	public static float StandardSCurve(float x)
	{
		return (3f - 2f * x) * x * x;
	}

	// Token: 0x0600061C RID: 1564 RVA: 0x0002C818 File Offset: 0x0002AA18
	public static float StandardSCurveInverseY(float x)
	{
		return 1f - (3f - 2f * x) * x * x;
	}

	// Token: 0x0600061D RID: 1565 RVA: 0x0002C834 File Offset: 0x0002AA34
	public static float StandardParabole(float x)
	{
		return x * x;
	}

	// Token: 0x0600061E RID: 1566 RVA: 0x0002C83C File Offset: 0x0002AA3C
	public static float StandardParaboleInverseY(float x)
	{
		float num = x - 1f;
		return 1f - num * num;
	}

	// Token: 0x0600061F RID: 1567 RVA: 0x0002C85C File Offset: 0x0002AA5C
	public static IEnumerator WaitForSecondsSkippable(float aDelaySeconds)
	{
		float tTimer = Time.time + aDelaySeconds;
		while (Time.time < tTimer && !Input.GetKeyUp(KeyCode.Space) && Input.touchCount == 0)
		{
			yield return null;
		}
		yield break;
	}

	// Token: 0x06000620 RID: 1568 RVA: 0x0002C880 File Offset: 0x0002AA80
	public static string ConvertTimeToStringMSS(float aTime)
	{
		int num = Mathf.FloorToInt(aTime);
		int num2 = num / 60;
		int num3 = num % 60;
		string text = string.Empty;
		if (num2 <= 9)
		{
			text += num2;
			text += ":";
			if (num3 > 9)
			{
				text += num3;
			}
			else
			{
				text = text + "0" + num3;
			}
		}
		else
		{
			text = "9:59";
		}
		return text;
	}

	// Token: 0x06000621 RID: 1569 RVA: 0x0002C900 File Offset: 0x0002AB00
	public static string AddSeparatorInInt(int tInt, string aSeperator)
	{
		string text = tInt.ToString();
		int length = text.Length;
		for (int i = length - 3; i > 0; i -= 3)
		{
			text = text.Insert(i, aSeperator);
		}
		return text;
	}

	// Token: 0x06000622 RID: 1570 RVA: 0x0002C93C File Offset: 0x0002AB3C
	public static void StarParticle(Vector3 pos, float scale)
	{
		GameObject original = Resources.Load("Prefabs/Flare") as GameObject;
		GameObject gameObject = UnityEngine.Object.Instantiate(original) as GameObject;
		GameObject gameObject2 = GameObject.Find("LogoPanel");
		Transform component = gameObject.GetComponent<Transform>();
		component.parent = gameObject2.transform;
		component.localPosition = pos;
		gameObject.GetComponent<UISprite>().depth = 10;
		gameObject.GetComponent<TweenScale>().to = new Vector3(scale * 90f, scale * 90f, 1f);
	}

	// Token: 0x06000623 RID: 1571 RVA: 0x0002C9C0 File Offset: 0x0002ABC0
	public static Transform Search(Transform target, string name)
	{
		if (target.name == name)
		{
			return target;
		}
		for (int i = 0; i < target.childCount; i++)
		{
			Transform transform = GenericFunctionsScript.Search(target.GetChild(i), name);
			if (transform != null)
			{
				return transform;
			}
		}
		return null;
	}

	// Token: 0x06000624 RID: 1572 RVA: 0x0002CA14 File Offset: 0x0002AC14
	public static List<DicEntry> CopyList(List<DicEntry> aList)
	{
		List<DicEntry> list = new List<DicEntry>();
		int count = aList.Count;
		for (int i = 0; i < count; i++)
		{
			list.Add(new DicEntry(aList[i]));
		}
		return list;
	}
}
