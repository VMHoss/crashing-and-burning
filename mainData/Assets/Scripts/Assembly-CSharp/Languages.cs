using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000FA RID: 250
public class Languages
{
	// Token: 0x0600077F RID: 1919 RVA: 0x00039270 File Offset: 0x00037470
	public static void SetReplacedFont()
	{
		string branding = Data.branding;
		if (branding != null)
		{
			if (Languages.<>f__switch$map18 == null)
			{
				Languages.<>f__switch$map18 = new Dictionary<string, int>(1)
				{
					{
						"KaiserGamesTurkish",
						0
					}
				};
			}
			int num;
			if (Languages.<>f__switch$map18.TryGetValue(branding, out num))
			{
				if (num == 0)
				{
					Languages.ReplaceFont("Arial18Bold");
					Languages.ReplaceFont("NCC64");
				}
			}
		}
		if (Languages.replaceFontMapping != null)
		{
			Debug.LogWarning("Fonts have been replaced");
		}
	}

	// Token: 0x06000780 RID: 1920 RVA: 0x000392F8 File Offset: 0x000374F8
	private static void ReplaceFont(string aFontName)
	{
		if (Languages.replaceFontMapping == null)
		{
			Languages.replaceFontMapping = new Dictionary<string, UIFont>();
		}
		GameObject gameObject = GameObject.Find("FontReplace" + aFontName);
		if (gameObject != null)
		{
			Languages.replaceFontMapping.Add(aFontName, gameObject.GetComponent<UILabel>().font);
		}
		else
		{
			Debug.LogError("Game object 'FontReplace" + aFontName + "' was not found in the StartUp scene!");
		}
	}

	// Token: 0x06000781 RID: 1921 RVA: 0x00039368 File Offset: 0x00037568
	public static void Init()
	{
		Languages.localization = GameObject.Find("Localization").GetComponent<Localization>();
		if (Data.platform == "PC")
		{
			string branding = Data.branding;
			switch (branding)
			{
			case "Miniclip":
				Languages.localization.currentLanguage = "English (US) Miniclip";
				goto IL_178;
			case "A10":
				Languages.localization.currentLanguage = "English (US) A10";
				goto IL_178;
			case "ClickJogos":
				Languages.localization.currentLanguage = "Portuguese (BR) ClickJogos";
				goto IL_178;
			case "KaiserGamesEnglish":
				Languages.localization.currentLanguage = "English (US) KaiserGames";
				goto IL_178;
			case "KaiserGamesGerman":
				Languages.localization.currentLanguage = "German (DE) KaiserGames";
				goto IL_178;
			case "KaiserGamesTurkish":
				Languages.localization.currentLanguage = "Turkish (TR) KaiserGames";
				goto IL_178;
			case "MiniJuegos":
				Languages.localization.currentLanguage = "English (US) MiniJuegos";
				goto IL_178;
			}
			Languages.localization.currentLanguage = "English (US)";
			IL_178:;
		}
		else
		{
			Languages.localization.currentLanguage = "English (US) Mobile";
		}
		Debug.Log("Languages is: " + Languages.localization.currentLanguage);
	}

	// Token: 0x0400080B RID: 2059
	public static Localization localization;

	// Token: 0x0400080C RID: 2060
	public static Dictionary<string, UIFont> replaceFontMapping;
}
