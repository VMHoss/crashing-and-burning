using System;
using com.miniclip;
using com.miniclip.currencies;
using UnityEngine;

// Token: 0x02000100 RID: 256
public class MiniclipManager : MonoBehaviour
{
	// Token: 0x0600079F RID: 1951 RVA: 0x0003A0B8 File Offset: 0x000382B8
	private void Awake()
	{
		this._miniclipAPI = base.gameObject.AddComponent<MiniclipAPI>();
		this.AddEventHandlers();
	}

	// Token: 0x060007A0 RID: 1952 RVA: 0x0003A0D4 File Offset: 0x000382D4
	private void AddEventHandlers()
	{
		this._miniclipAPI.HighscoresClosed += this.Handle_miniclipAPIHighscoresClosed;
		this._miniclipAPI.LogInCompleted += this.Handle_miniclipAPILogInCompleted;
		this._miniclipAPI.SignUpCompleted += this.Handle_miniclipAPISignUpCompleted;
		this._miniclipAPI.LogInCancelled += this.Handle_miniclipAPILogInCancelled1;
	}

	// Token: 0x060007A1 RID: 1953 RVA: 0x0003A140 File Offset: 0x00038340
	private void Handle_miniclipAPILogInCancelled1(object sender, EventArgs e)
	{
	}

	// Token: 0x060007A2 RID: 1954 RVA: 0x0003A144 File Offset: 0x00038344
	private void Handle_miniclipAPISignUpCompleted(object sender, UserDetailsEventArgs e)
	{
	}

	// Token: 0x060007A3 RID: 1955 RVA: 0x0003A148 File Offset: 0x00038348
	private void Handle_miniclipAPILogInCompleted(object sender, UserDetailsEventArgs e)
	{
	}

	// Token: 0x060007A4 RID: 1956 RVA: 0x0003A14C File Offset: 0x0003834C
	private void Handle_miniclipAPIHighscoresClosed(object sender, EventArgs e)
	{
	}

	// Token: 0x060007A5 RID: 1957 RVA: 0x0003A150 File Offset: 0x00038350
	public void ToLeaderboards()
	{
		this._miniclipAPI.ShowHighscores();
	}

	// Token: 0x060007A6 RID: 1958 RVA: 0x0003A160 File Offset: 0x00038360
	public void submitScore(int aScore)
	{
		this._miniclipAPI.SaveHighscore(aScore);
	}

	// Token: 0x04000822 RID: 2082
	private MiniclipAPI _miniclipAPI;

	// Token: 0x04000823 RID: 2083
	private MiniclipCurrencies _miniclipCurrencies;

	// Token: 0x04000824 RID: 2084
	private GameObject pObject;
}
