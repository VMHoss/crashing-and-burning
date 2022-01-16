using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000BA RID: 186
public class TrackProgress : MonoBehaviour
{
	// Token: 0x060005AB RID: 1451 RVA: 0x00029580 File Offset: 0x00027780
	private void Awake()
	{
		this.menuScript = GameObject.Find("Menu").GetComponent<MenuScript>();
		foreach (object obj in base.gameObject.transform)
		{
			Transform transform = (Transform)obj;
			transform.gameObject.active = false;
		}
	}

	// Token: 0x060005AC RID: 1452 RVA: 0x00029610 File Offset: 0x00027810
	private void Start()
	{
		if (this.track != "None")
		{
			string text = this.track;
			switch (text)
			{
			case "Track1":
				this.trackInternal = "T5";
				break;
			case "Track2":
				this.trackInternal = "T6";
				break;
			case "Track3":
				this.trackInternal = "T7";
				break;
			case "Track4":
				this.trackInternal = "T20";
				break;
			case "Track5":
				this.trackInternal = "T21";
				break;
			case "Track6":
				this.trackInternal = "T22";
				break;
			}
			base.StartCoroutine(this.ProgressSequence());
		}
	}

	// Token: 0x060005AD RID: 1453 RVA: 0x00029740 File Offset: 0x00027940
	private IEnumerator ProgressSequence()
	{
		Debug.Log("Track Progress for :" + this.track + " activated!");
		yield return new WaitForSeconds(0.3f);
		yield break;
	}

	// Token: 0x060005AE RID: 1454 RVA: 0x0002975C File Offset: 0x0002795C
	private void Update()
	{
		if (this.track == "None")
		{
			if (this.challengeNew)
			{
				this.newChallenge.gameObject.active = true;
			}
			if (this.challengeCheck)
			{
				this.checkChallenge.gameObject.active = true;
			}
		}
	}

	// Token: 0x04000626 RID: 1574
	public MenuScript menuScript;

	// Token: 0x04000627 RID: 1575
	public string track = "None";

	// Token: 0x04000628 RID: 1576
	public string trackInternal = "None";

	// Token: 0x04000629 RID: 1577
	public bool trackUnlocked;

	// Token: 0x0400062A RID: 1578
	public GameObject lockIcon;

	// Token: 0x0400062B RID: 1579
	public GameObject newIcon;

	// Token: 0x0400062C RID: 1580
	public GameObject cupBronze;

	// Token: 0x0400062D RID: 1581
	public GameObject cupSilver;

	// Token: 0x0400062E RID: 1582
	public GameObject cupGold;

	// Token: 0x0400062F RID: 1583
	public GameObject hiddenPackage;

	// Token: 0x04000630 RID: 1584
	public GameObject newChallenge;

	// Token: 0x04000631 RID: 1585
	public GameObject checkChallenge;

	// Token: 0x04000632 RID: 1586
	public bool challengeNew = true;

	// Token: 0x04000633 RID: 1587
	public bool challengeCheck;

	// Token: 0x04000634 RID: 1588
	public int packagesRequiredPerTrack = 3;
}
