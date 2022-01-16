using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000A8 RID: 168
public class Levels : MonoBehaviour
{
	// Token: 0x06000547 RID: 1351 RVA: 0x00025D84 File Offset: 0x00023F84
	private void Start()
	{
		this.localization = GameObject.Find("Localization").GetComponent<Localization>();
		base.StartCoroutine(this.LevelsSequence());
	}

	// Token: 0x06000548 RID: 1352 RVA: 0x00025DB4 File Offset: 0x00023FB4
	private void Update()
	{
	}

	// Token: 0x06000549 RID: 1353 RVA: 0x00025DB8 File Offset: 0x00023FB8
	private IEnumerator LevelsSequence()
	{
		Debug.Log("LevelsSequence started!");
		if (GameData.playerLevel < 10)
		{
			this.levelsHeaderLabel.text = string.Concat(new string[]
			{
				this.localization.Get("LevelText"),
				" ",
				GameData.playerLevel.ToString(),
				": ",
				this.localization.Get("Level" + GameData.playerLevel.ToString() + "Text")
			});
		}
		else
		{
			this.levelsHeaderLabel.text = string.Concat(new string[]
			{
				this.localization.Get("LevelText"),
				" ",
				GameData.playerLevel.ToString(),
				": ",
				this.localization.Get("Level10Text")
			});
		}
		this.levelsStarsNeededValue.text = (Level.GetReqXPInCurLevel() - GameData.XPWithinLevel).ToString();
		yield return new WaitForSeconds(1f);
		Debug.Log("LevelsSequence ended!");
		yield break;
	}

	// Token: 0x0400056C RID: 1388
	public Localization localization;

	// Token: 0x0400056D RID: 1389
	public GameObject levelsMeter;

	// Token: 0x0400056E RID: 1390
	public UILabel levelsHeaderLabel;

	// Token: 0x0400056F RID: 1391
	public UILabel levelsStarsNeededValue;
}
