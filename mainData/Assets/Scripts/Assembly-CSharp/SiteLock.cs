using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000DE RID: 222
public class SiteLock : MonoBehaviour
{
	// Token: 0x060006BB RID: 1723 RVA: 0x0003047C File Offset: 0x0002E67C
	public static bool IsSiteLocked()
	{
		if (SiteLock.pForceSiteLock)
		{
			SiteLock.pSiteLockData = Data.Shared["SiteLock"].d;
			return true;
		}
		if (Data.platform == "PC" && Application.isWebPlayer)
		{
			if (!Data.Shared.ContainsKey("SiteLock"))
			{
				return false;
			}
			SiteLock.pSiteLockData = Data.Shared["SiteLock"].d;
			if (SiteLock.pSiteLockData.ContainsKey(Data.branding))
			{
				if (Application.absoluteURL.StartsWith("file://"))
				{
					return false;
				}
				SiteLock.pDomain = Application.absoluteURL;
				int num = SiteLock.pDomain.IndexOf("//", 0, 8);
				if (num >= 0)
				{
					SiteLock.pDomain = SiteLock.pDomain.Substring(num + 2);
				}
				int num2 = SiteLock.pDomain.IndexOf("/");
				if (num2 >= 0)
				{
					SiteLock.pDomain = SiteLock.pDomain.Substring(0, num2);
				}
				SiteLock.pDomain = SiteLock.pDomain.ToLower();
				Debug.Log("Domain: " + SiteLock.pDomain);
				foreach (DicEntry dicEntry in SiteLock.pSiteLockData["AlwaysAllow"].l)
				{
					if (SiteLock.pDomain.EndsWith(dicEntry.s.Replace("\\", "/")))
					{
						return false;
					}
				}
				foreach (DicEntry dicEntry2 in SiteLock.pSiteLockData[Data.branding].l)
				{
					if (SiteLock.pDomain.EndsWith(dicEntry2.s.Replace("\\", "/")))
					{
						return false;
					}
				}
				return true;
			}
		}
		return false;
	}

	// Token: 0x060006BC RID: 1724 RVA: 0x000306C4 File Offset: 0x0002E8C4
	public static bool Redirect()
	{
		return SiteLock.Redirect(true);
	}

	// Token: 0x060006BD RID: 1725 RVA: 0x000306CC File Offset: 0x0002E8CC
	public static bool Redirect(bool aForced)
	{
		if (Data.Shared == null)
		{
			return false;
		}
		if (SiteLock.pSiteLockData.ContainsKey(Data.branding + "Redirect"))
		{
			Dictionary<string, DicEntry> d = SiteLock.pSiteLockData[Data.branding + "Redirect"].d;
			if (aForced || !d["ShowLockScreen"].b)
			{
				string script = "window.open(\"" + d["UrlPage"].s.Replace("\\", "/") + "\", \"_top\")";
				Application.ExternalEval(script);
				return true;
			}
		}
		else
		{
			Debug.LogError("Missing \"Redirect\" property!");
		}
		return false;
	}

	// Token: 0x060006BE RID: 1726 RVA: 0x00030784 File Offset: 0x0002E984
	private void Awake()
	{
		if (SiteLock.pForceSiteLock)
		{
			return;
		}
		if (GameObject.Find("StartUp") == null)
		{
			SiteLock.pForceSiteLock = true;
			Debug.LogWarning("Started in SiteLock scene: you are testing the sitelock");
			Application.LoadLevel("StartUp");
		}
	}

	// Token: 0x060006BF RID: 1727 RVA: 0x000307CC File Offset: 0x0002E9CC
	private void Start()
	{
		base.gameObject.SetActive(SiteLock.Redirect(false));
		Languages.Init();
	}

	// Token: 0x040006E9 RID: 1769
	private static bool pForceSiteLock;

	// Token: 0x040006EA RID: 1770
	private static string pDomain = string.Empty;

	// Token: 0x040006EB RID: 1771
	private static Dictionary<string, DicEntry> pSiteLockData;
}
