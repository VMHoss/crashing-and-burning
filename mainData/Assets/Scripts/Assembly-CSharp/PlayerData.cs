using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000110 RID: 272
public class PlayerData : PlayerBaseData
{
	// Token: 0x060007E1 RID: 2017 RVA: 0x0003BDE0 File Offset: 0x00039FE0
	public PlayerData(string aPlayerName) : base(aPlayerName)
	{
		GameData.playerList.Add(this);
		base.SetKeyInput();
	}

	// Token: 0x060007E2 RID: 2018 RVA: 0x0003BE08 File Offset: 0x0003A008
	public void NewGame()
	{
		this.health = 100f;
		this.score = 0;
	}

	// Token: 0x060007E3 RID: 2019 RVA: 0x0003BE1C File Offset: 0x0003A01C
	public void RetryLevel()
	{
		this.health = 100f;
	}

	// Token: 0x060007E4 RID: 2020 RVA: 0x0003BE2C File Offset: 0x0003A02C
	public void NextLevel()
	{
	}

	// Token: 0x060007E5 RID: 2021 RVA: 0x0003BE30 File Offset: 0x0003A030
	public void QuitLevel()
	{
	}

	// Token: 0x04000871 RID: 2161
	public CameraScript cameraScript;

	// Token: 0x04000872 RID: 2162
	public float health = 100f;

	// Token: 0x04000873 RID: 2163
	public int score;

	// Token: 0x04000874 RID: 2164
	public List<GameObject> WeaponParticleSystems;
}
