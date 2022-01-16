using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200012D RID: 301
public class TrackManager
{
	// Token: 0x0600089E RID: 2206 RVA: 0x0003FAB8 File Offset: 0x0003DCB8
	public void Initialize()
	{
		if (!GameData.sharedTrafficMaterial)
		{
			GameData.sharedTrafficMaterial = Loader.LoadMaterial("Shared", "Traffic/Traffic_Material");
		}
		this.pAirTimeSound = Scripts.audioManager.PlaySFX("Effects/AirTime", 1f, -1);
		this.pAirTimeSound.Stop();
		this.pAirTimeSound.enabled = false;
		this.pBuildingsMaterial = Loader.LoadMaterial("Level1", "Level1Buildings_Material");
		this.pStreetMaterial = Loader.LoadMaterial("Level1", "Level1Street_Material");
		this.SetActiveJoepsMaterial(false);
		this.pCrashTime = Data.Shared["Misc"].d["CrashTimeDuration"].f;
		this.pCrashTimeSlowMoTimeScale = Data.Shared["Misc"].d["CrashTimeSlowMotion"].f;
		this.pPlayOutTime = Data.Shared["Misc"].d["PlayOutDuration"].f;
		this.pSlowMoController = new SlowMotionController();
		GameObject[] array = UnityEngine.Object.FindObjectsOfType(typeof(GameObject)) as GameObject[];
		List<GameObject> list = new List<GameObject>();
		List<GameObject> list2 = new List<GameObject>();
		foreach (GameObject gameObject in array)
		{
			if (gameObject.name.Contains("Destructible_"))
			{
				gameObject.name = gameObject.name.Substring(0, gameObject.name.Length - 3);
				list2.Add(gameObject);
			}
			else if (gameObject.name.Contains("Street"))
			{
				list.Add(gameObject);
			}
			else if (gameObject.name.Contains("InvisibleWall"))
			{
			}
		}
		this.CreatePlayerVehicle(GameData.playerCar);
		this.camera = new GameObject("Main Camera");
		this.camera.AddComponent<Camera>();
		Scripts.audioManager.gameObject.transform.parent = this.camera.transform;
		Scripts.audioManager.gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);
		Scripts.audioManager.gameObject.transform.localRotation = Quaternion.identity;
		this.cameraScript = this.camera.AddComponent<CameraScript>();
		this.cameraScript.followedCar = GameData.playerCarScript;
		this.pKillComboSizes = new List<DicEntry>();
		foreach (KeyValuePair<string, DicEntry> keyValuePair in Data.Shared["RampageSettings"].d)
		{
			if (keyValuePair.Key.StartsWith("Rampage"))
			{
				this.pKillComboSizes.Add(keyValuePair.Value);
			}
		}
		TrackManager.SortOnKillSize comparer = new TrackManager.SortOnKillSize();
		this.pKillComboSizes.Sort(comparer);
	}

	// Token: 0x0600089F RID: 2207 RVA: 0x0003FDE4 File Offset: 0x0003DFE4
	public void SwitchPlayerVehicle(string aNewVehicleModel)
	{
		UnityEngine.Object.Destroy(GameData.playerCarScript.gameObject);
		this.CreatePlayerVehicle(aNewVehicleModel);
		this.cameraScript.followedCar = GameData.playerCarScript;
	}

	// Token: 0x060008A0 RID: 2208 RVA: 0x0003FE18 File Offset: 0x0003E018
	public void ShowCar()
	{
		GameData.playerCarScript.SetEquippedSuperPowerEffect(GameData.superPower);
		GameData.playerCarScript.SetItemEffect(GameData.gadget);
		GameData.playerCarScript.ApplyUpgrades();
		GameData.playerCarScript.SetSounds(true);
		if (Shop.GetUpgradeLevel("AfterTouchLength") > 0)
		{
			this.pCrashTime += Data.Shared["Upgrades"].d["AfterTouchLength"].d["Upgrade" + Shop.GetUpgradeLevel("AfterTouchLength")].f;
		}
		FlurryScript.LogEvent("vehicle used", new string[]
		{
			GameData.playerCar
		});
		if (GameData.superPower != "None")
		{
			FlurryScript.LogEvent("superpower used", new string[]
			{
				GameData.superPower
			});
		}
		FlurryScript.LogEvent("gadget used", new string[]
		{
			GameData.gadget
		});
	}

	// Token: 0x060008A1 RID: 2209 RVA: 0x0003FF18 File Offset: 0x0003E118
	public void StartRun()
	{
	}

	// Token: 0x060008A2 RID: 2210 RVA: 0x0003FF1C File Offset: 0x0003E11C
	public void UnlockPlayerCar()
	{
		GameData.playerCarScript.LockCarAtStart(false);
	}

	// Token: 0x060008A3 RID: 2211 RVA: 0x0003FF2C File Offset: 0x0003E12C
	public void UpdatePickUpsExplicit()
	{
	}

	// Token: 0x060008A4 RID: 2212 RVA: 0x0003FF30 File Offset: 0x0003E130
	public void Update()
	{
		Scripts.gridManager.Update();
		this.pSlowMoController.Update();
		if (this.pCallInterfaceActiveFalse)
		{
			this.pCallInterfaceActiveFalse = false;
			Scripts.trackScript.interfaceScript.interfacePanelScript.InterfaceActive(false);
		}
		switch (this.pState)
		{
		case TrackManager.State.DRIVING:
		case TrackManager.State.AIRTIME:
			if (GameData.playerCarScript.IsDestroyed())
			{
				Scripts.trackScript.interfaceScript.interfacePanelScript.CrashTime(true);
				this.pState = TrackManager.State.CRASHTIME;
			}
			else if (GameData.playerCarScript.transform.position.y < -21.5f)
			{
				GameData.playerCarScript.ExplodeCar();
				Debug.Log("Car exploded by position (below y = -21.5)");
			}
			break;
		case TrackManager.State.CRASHTIME:
			if (!this.pSlowMoController.IsActive())
			{
				Scripts.trackScript.interfaceScript.interfacePanelScript.CrashTime(false);
				this.pState = TrackManager.State.PLAYOUTTIME;
			}
			break;
		case TrackManager.State.PLAYOUTTIME:
			if (this.pNukeDelayTime >= 0f)
			{
				this.pNukeDelayTime -= Time.deltaTime;
				if (this.pNukeDelayTime < 0f)
				{
					GameData.nuclearDetonator = false;
					Debug.LogError("Nuke code was removed");
				}
			}
			else if (this.TimeOutCondition())
			{
				if (GameData.afterBlast || GameData.nuclearDetonator)
				{
					if (GameData.afterBlast)
					{
						GameData.afterBlast = false;
						GameData.playerCarScript.ExplodeCar();
						Scripts.trackScript.interfaceScript.interfacePanelScript.CrashTime(true);
						this.pState = TrackManager.State.CRASHTIME;
					}
					else
					{
						this.pNukeDelayTime = 3.5f;
						this.cameraScript.DoNukeCamera();
						Scripts.audioManager.PlaySFX("NukeAlarm", 1f, 1);
					}
				}
				else
				{
					this.PlayerDestroyed();
					this.pState = TrackManager.State.RESULT;
				}
			}
			break;
		}
		if (this.pState != TrackManager.State.RESULT)
		{
			if (GameData.chainReactionTimeLeft >= 0f)
			{
				if (!GameData.showChainReaction)
				{
					if (GameData.currentChainReaction > 1)
					{
						GameData.showChainReaction = true;
						Scripts.trackScript.interfaceScript.interfacePanelScript.StartChainReaction();
						this.pLastChainReactionCount = GameData.currentChainReaction;
					}
				}
				else if (GameData.currentChainReaction != this.pLastChainReactionCount)
				{
					Scripts.trackScript.interfaceScript.interfacePanelScript.UpdateChainReaction();
					this.pLastChainReactionCount = GameData.currentChainReaction;
				}
				GameData.chainReactionTimeLeft -= Time.deltaTime;
				if (GameData.chainReactionTimeLeft < 0f)
				{
					this.EndComboCounter();
				}
			}
			if (GameData.mainMission.completed)
			{
				this.MainMissionCompleted();
			}
		}
		if (this.pJoepsEffectEnabled)
		{
			this.pBuildingsMaterial.SetVector("_Position", GameData.playerCarScript.transform.position);
			this.pStreetMaterial.SetVector("_Position", GameData.playerCarScript.transform.position);
		}
		if (Input.GetKeyDown(KeyCode.Tab))
		{
			this.SetActiveJoepsMaterial(false);
		}
	}

	// Token: 0x060008A5 RID: 2213 RVA: 0x00040250 File Offset: 0x0003E450
	public void UpdateJoepsMaterialColor(Color aColor)
	{
		this.pBuildingsMaterial.SetColor("_FillColor", aColor);
		this.pStreetMaterial.SetColor("_FillColor", aColor);
	}

	// Token: 0x060008A6 RID: 2214 RVA: 0x00040280 File Offset: 0x0003E480
	public void SetActiveJoepsMaterial(bool anEnabled)
	{
		this.pJoepsEffectEnabled = anEnabled;
		if (!this.pJoepsEffectEnabled)
		{
			this.pBuildingsMaterial.SetVector("_Position", new Vector3(0f, -999f, 0f));
			this.pStreetMaterial.SetVector("_Position", new Vector3(0f, -999f, 0f));
		}
		else
		{
			this.pBuildingsMaterial.SetVector("_Position", GameData.playerCarScript.transform.position);
			this.pStreetMaterial.SetVector("_Position", GameData.playerCarScript.transform.position);
		}
	}

	// Token: 0x060008A7 RID: 2215 RVA: 0x00040340 File Offset: 0x0003E540
	public void EndComboCounter()
	{
		if (GameData.showChainReaction)
		{
			Scripts.trackScript.interfaceScript.interfacePanelScript.EndChainReaction();
			GameData.showChainReaction = false;
			int count = this.pKillComboSizes.Count;
			for (int i = 0; i < count; i++)
			{
				if (GameData.currentChainReaction >= this.pKillComboSizes[i].d["MinKills"].i)
				{
					int num = this.pKillComboSizes[i].d["BaseScore"].i + this.pKillComboSizes[i].d["ScorePerKill"].i * GameData.currentChainReaction;
					Scripts.trackScript.interfaceScript.interfacePanelScript.ObtainedExtra(this.pKillComboSizes[i].d["Name"].s, num);
					Scripts.scoreManager.AddCash(num);
					break;
				}
			}
		}
		this.pLastChainReactionCount = 0;
		GameData.currentChainReaction = 0;
		GameData.chainReactionTimeLeft = -1f;
	}

	// Token: 0x060008A8 RID: 2216 RVA: 0x00040460 File Offset: 0x0003E660
	public void PauseGame()
	{
		this.cameraScript.PauseGame();
	}

	// Token: 0x060008A9 RID: 2217 RVA: 0x00040470 File Offset: 0x0003E670
	public void unPauseGame()
	{
	}

	// Token: 0x060008AA RID: 2218 RVA: 0x00040474 File Offset: 0x0003E674
	public void NotifyCarExploded(CarScript aCarScript)
	{
		if (aCarScript == GameData.playerCarScript)
		{
			aCarScript.PlaySound("CarExplosion");
			aCarScript.PlaySound("Fire");
			aCarScript.PlaySound("Effects/CrashGlass" + UnityEngine.Random.Range(1, 3));
			this.pSlowMoController.StartSlowMotion(this.pCrashTimeSlowMoTimeScale, this.pCrashTime);
			this.pCallInterfaceActiveFalse = true;
			this.cameraScript.LockCamera();
		}
	}

	// Token: 0x060008AB RID: 2219 RVA: 0x000404EC File Offset: 0x0003E6EC
	public void NotifyAirTimeStart()
	{
		this.pSlowMoController.StartAirSlowMotion();
		Scripts.trackScript.interfaceScript.interfacePanelScript.AirTime(true);
		this.pAirTimeSound.enabled = true;
		this.pAirTimeSound.Play();
		this.pState = TrackManager.State.AIRTIME;
	}

	// Token: 0x060008AC RID: 2220 RVA: 0x00040538 File Offset: 0x0003E738
	public void NotifyAirTimeEnd()
	{
		this.pSlowMoController.StopAirSlowMotion();
		Scripts.trackScript.interfaceScript.interfacePanelScript.AirTime(false);
		this.pAirTimeSound.Stop();
		this.pAirTimeSound.enabled = false;
		this.pState = TrackManager.State.DRIVING;
	}

	// Token: 0x060008AD RID: 2221 RVA: 0x00040584 File Offset: 0x0003E784
	public void NotifySpeedEnd(float aTime)
	{
		if (aTime > Data.Shared["Misc"].d["ReqBonusSpeedTime"].f && !Scripts.scoreManager.HasBonus(ScoreManager.Bonus.SPEED))
		{
			Scripts.scoreManager.AddBonus(ScoreManager.Bonus.SPEED);
			Scripts.trackScript.interfaceScript.interfacePanelScript.ObtainedExtra("BonusSpeed", Data.Shared["Misc"].d["BonusSpeed"].i);
		}
	}

	// Token: 0x060008AE RID: 2222 RVA: 0x00040614 File Offset: 0x0003E814
	public void NotifyJumpheightEnd(float aHeight)
	{
		if (aHeight > Data.Shared["Misc"].d["ReqBonusJumpheight"].f && !Scripts.scoreManager.HasBonus(ScoreManager.Bonus.JUMPHEIGHT))
		{
			Scripts.scoreManager.AddBonus(ScoreManager.Bonus.JUMPHEIGHT);
			Scripts.trackScript.interfaceScript.interfacePanelScript.ObtainedExtra("BonusJumpHeight", Data.Shared["Misc"].d["BonusJumpheight"].i);
		}
	}

	// Token: 0x060008AF RID: 2223 RVA: 0x000406A4 File Offset: 0x0003E8A4
	public void NotifyJumpdistanceEnd(float aDistance)
	{
		if (aDistance > Data.Shared["Misc"].d["ReqBonusJumpdistance"].f && !Scripts.scoreManager.HasBonus(ScoreManager.Bonus.JUMPDISTANCE))
		{
			Scripts.scoreManager.AddBonus(ScoreManager.Bonus.JUMPDISTANCE);
			Scripts.trackScript.interfaceScript.interfacePanelScript.ObtainedExtra("BonusJumpDistance", Data.Shared["Misc"].d["BonusJumpdistance"].i);
		}
	}

	// Token: 0x060008B0 RID: 2224 RVA: 0x00040734 File Offset: 0x0003E934
	public void NotifyNitroStart(CarScript aCarScript)
	{
	}

	// Token: 0x060008B1 RID: 2225 RVA: 0x00040738 File Offset: 0x0003E938
	public void NotifyDestructibleDestroyed(string aDestructibleType)
	{
		Scripts.scoreManager.DestroyedDestructible(aDestructibleType);
	}

	// Token: 0x060008B2 RID: 2226 RVA: 0x00040748 File Offset: 0x0003E948
	public void NotifyRespawn(CarScript aCarScript)
	{
		if (GameData.playerCarScript == aCarScript)
		{
			GenericFunctionsScript.Fade("FromWhite");
			aCarScript.PlaySound("Respawn");
			if (!Scripts.trackScript.IsRaceFinished())
			{
				Scripts.trackScript.interfaceScript.interfacePanelScript.InterfaceActive(true);
			}
		}
	}

	// Token: 0x060008B3 RID: 2227 RVA: 0x000407A0 File Offset: 0x0003E9A0
	public bool IsInCrashOrAirTime()
	{
		return this.pState == TrackManager.State.CRASHTIME || this.pState == TrackManager.State.AIRTIME;
	}

	// Token: 0x060008B4 RID: 2228 RVA: 0x000407BC File Offset: 0x0003E9BC
	private void CreatePlayerVehicle(string aVehicleModel)
	{
		string s = Data.Shared["Car"].d[GameData.playerCar].d["Model"].s;
		string s2 = Data.Shared["Car"].d[GameData.playerCar].d["AssetBundle"].s;
		GameObject gameObject = Loader.LoadGameObject(s2, s + "/" + s + "_Prefab");
		gameObject.name = GameData.playerCar + "_Player";
		SafeHouseData currentSafeHousePosition = SafeHousePosition.GetCurrentSafeHousePosition();
		gameObject.transform.position = currentSafeHousePosition.position;
		gameObject.transform.rotation = Quaternion.Euler(0f, currentSafeHousePosition.yAngle, 0f);
		new CarData(gameObject.name, GameData.playerCar);
		GameData.playerCarName = gameObject.name;
		switch (aVehicleModel)
		{
		case "A7":
			GameData.playerCarScript = gameObject.AddComponent<LotusScript>();
			goto IL_333;
		case "Chevrolet":
			GameData.playerCarScript = gameObject.AddComponent<LotusScript>();
			goto IL_333;
		case "FireTruck":
			GameData.playerCarScript = gameObject.AddComponent<LotusScript>();
			goto IL_333;
		case "Juggernaut":
			GameData.playerCarScript = gameObject.AddComponent<LotusScript>();
			goto IL_333;
		case "Lotus":
			GameData.playerCarScript = gameObject.AddComponent<LotusScript>();
			goto IL_333;
		case "PanzerTruck":
			GameData.playerCarScript = gameObject.AddComponent<PanzerTruckScript>();
			goto IL_333;
		case "Drill":
			GameData.playerCarScript = gameObject.AddComponent<DrillScript>();
			goto IL_333;
		case "KillRod":
			GameData.playerCarScript = gameObject.AddComponent<LotusScript>();
			goto IL_333;
		case "SchoolBus":
			GameData.playerCarScript = gameObject.AddComponent<LotusScript>();
			goto IL_333;
		case "Fennek":
			GameData.playerCarScript = gameObject.AddComponent<LotusScript>();
			goto IL_333;
		case "Tank":
			GameData.playerCarScript = gameObject.AddComponent<TankScript>();
			goto IL_333;
		case "Taurus":
			GameData.playerCarScript = gameObject.AddComponent<LotusScript>();
			goto IL_333;
		case "AstonMartin":
			GameData.playerCarScript = gameObject.AddComponent<LotusScript>();
			goto IL_333;
		case "FordGT":
			GameData.playerCarScript = gameObject.AddComponent<LotusScript>();
			goto IL_333;
		case "Vice":
			GameData.playerCarScript = gameObject.AddComponent<LotusScript>();
			goto IL_333;
		}
		Debug.LogError("Unknown aVehicleModel: " + aVehicleModel);
		IL_333:
		GameData.playerCarScript.SetCarTexture();
		GameData.playerCarScript.SetSounds(false);
	}

	// Token: 0x060008B5 RID: 2229 RVA: 0x00040B14 File Offset: 0x0003ED14
	private bool TimeOutCondition()
	{
		this.pPlayOutTime -= Time.deltaTime;
		if (GameData.playerCarScript.rigidbody.velocity.sqrMagnitude >= 2f && this.pPlayOutTime < Data.Shared["Misc"].d["PlayOutDuration"].f)
		{
			this.pPlayOutTime = Data.Shared["Misc"].d["PlayOutDuration"].f;
		}
		return this.pPlayOutTime <= 0f;
	}

	// Token: 0x060008B6 RID: 2230 RVA: 0x00040BC0 File Offset: 0x0003EDC0
	private void PlayerDestroyed()
	{
		GameData.playerCarScript.EndBonuses();
		this.EndComboCounter();
		GameData.playerCarScript.carData.ResetKeys();
		Scripts.trackScript.PlayerDestroyed();
	}

	// Token: 0x060008B7 RID: 2231 RVA: 0x00040BEC File Offset: 0x0003EDEC
	private void MainMissionCompleted()
	{
		GameData.playerCarScript.EndBonuses();
		this.EndComboCounter();
		GameData.playerCarScript.carData.ResetKeys();
		Scripts.trackScript.MainMissionCompleted();
		GameData.playerCarScript.SetSounds(false);
	}

	// Token: 0x060008B8 RID: 2232 RVA: 0x00040C30 File Offset: 0x0003EE30
	public void ContinueFromMissionCompletion()
	{
	}

	// Token: 0x060008B9 RID: 2233 RVA: 0x00040C34 File Offset: 0x0003EE34
	public bool UpdateDestructibles()
	{
		return this.pState == TrackManager.State.DRIVING || this.pState == TrackManager.State.AIRTIME;
	}

	// Token: 0x040008D7 RID: 2263
	public GameObject camera;

	// Token: 0x040008D8 RID: 2264
	public CameraScript cameraScript;

	// Token: 0x040008D9 RID: 2265
	private float pCrashTime = 5f;

	// Token: 0x040008DA RID: 2266
	private float pCrashTimeSlowMoTimeScale = 0.5f;

	// Token: 0x040008DB RID: 2267
	private float pPlayOutTime = 5f;

	// Token: 0x040008DC RID: 2268
	private float pNukeDelayTime = -1f;

	// Token: 0x040008DD RID: 2269
	private SlowMotionController pSlowMoController;

	// Token: 0x040008DE RID: 2270
	private int pLastChainReactionCount;

	// Token: 0x040008DF RID: 2271
	private AudioSource pAirTimeSound;

	// Token: 0x040008E0 RID: 2272
	private bool pJoepsEffectEnabled;

	// Token: 0x040008E1 RID: 2273
	private Material pBuildingsMaterial;

	// Token: 0x040008E2 RID: 2274
	private Material pStreetMaterial;

	// Token: 0x040008E3 RID: 2275
	private List<DicEntry> pKillComboSizes;

	// Token: 0x040008E4 RID: 2276
	private bool pCallInterfaceActiveFalse = true;

	// Token: 0x040008E5 RID: 2277
	private TrackManager.State pState = TrackManager.State.DRIVING;

	// Token: 0x0200012E RID: 302
	private enum State
	{
		// Token: 0x040008E8 RID: 2280
		DRIVING = 1,
		// Token: 0x040008E9 RID: 2281
		AIRTIME,
		// Token: 0x040008EA RID: 2282
		CRASHTIME,
		// Token: 0x040008EB RID: 2283
		PLAYOUTTIME,
		// Token: 0x040008EC RID: 2284
		RESULT
	}

	// Token: 0x0200012F RID: 303
	private class SortOnKillSize : IComparer<DicEntry>
	{
		// Token: 0x060008BB RID: 2235 RVA: 0x00040C58 File Offset: 0x0003EE58
		public int Compare(DicEntry a, DicEntry b)
		{
			if (a.d["MinKills"].i < b.d["MinKills"].i)
			{
				return 1;
			}
			if (a.d["MinKills"].i > b.d["MinKills"].i)
			{
				return -1;
			}
			return 0;
		}
	}
}
