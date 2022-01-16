using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000130 RID: 304
public class TrackScript : MonoBehaviour
{
	// Token: 0x060008BD RID: 2237 RVA: 0x00040CF0 File Offset: 0x0003EEF0
	private void Start()
	{
		Scripts.trackScript = this;
		if (this.pLevelText == null)
		{
			this.pLevelText = new Dictionary<string, DicEntry>();
			TextAsset textAsset = Loader.LoadTextFile("Level" + Data.level, "Text/Level" + Data.level);
			TextLoader.LoadText(textAsset.text, this.pLevelText);
		}
		DestructibleScript.physicMaterial = Loader.LoadPhysicMaterial("Shared", "Ojbects_PhysicsMaterial");
		DestructibleScript.objectMaterial = Loader.LoadMaterial("Shared", "Objects/Objects_Material");
		DestructibleScript.objectDestroyedMaterial = Loader.LoadMaterial("Shared", "Objects/ObjectsDestroyed_Material");
		DestructibleScript.objectTransparentMaterial = Loader.LoadMaterial("Shared", "Objects/ObjectsTransparent_Material");
		PickUpData.pickUpData = Data.Shared["PickUps"].d;
		TrafficScript.TrafficGroup = new GameObject("Traffic_Group").transform;
		SafeHousePosition.Initialize(this.pLevelText);
		GameObject gameObject = GameObject.Find("Interface");
		this.interfaceScript = gameObject.GetComponent<InterfaceScript>();
		Scripts.interfaceScript = gameObject.GetComponent<InterfaceScript>();
		Scripts.poolManager = new PoolManager();
		GameObject x = GameObject.Find("Level" + Data.level);
		if (x == null)
		{
			this.track = Loader.LoadGameObject("Level" + Data.level, "Level" + Data.level + "_Prefab");
			this.track.name = "Level" + Data.level;
			UnityEngine.Object.DontDestroyOnLoad(this.track);
		}
		else
		{
			this.track = x;
		}
		if (Data.platform == "PC")
		{
			Loader.LoadGameObject("Level" + Data.level, "AmbientEffects_Prefab");
		}
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		this.InitBlocks();
		this.InitGrid();
		this.ConnectWaypointPaths();
		this.SetupSafeHousesOnBlocks();
		Debug.Log("Block initialization took: " + (Time.realtimeSinceStartup - realtimeSinceStartup) * 1000f + "ms");
		Scripts.trafficManager = new TrafficManager();
		GameData.gainedCoins = 0;
		GameData.newRewardFromCrate = null;
		Color ambientLight = default(Color);
		this.pLevelSettings = Data.Shared["LevelSettings"].d["Level" + Data.level].d;
		ambientLight.r = this.pLevelSettings["AL"].l[0].f;
		ambientLight.g = this.pLevelSettings["AL"].l[1].f;
		ambientLight.b = this.pLevelSettings["AL"].l[2].f;
		RenderSettings.ambientLight = ambientLight;
		if (!GameData.disableFog)
		{
			RenderSettings.fog = true;
			RenderSettings.fogMode = FogMode.Linear;
			RenderSettings.fogColor = GenericFunctionsScript.ColorFromList(this.pLevelSettings["FC"].l);
			RenderSettings.fogStartDistance = this.pLevelSettings["FSD"].f;
			RenderSettings.fogEndDistance = this.pLevelSettings["FED"].f;
			RenderSettings.fogDensity = this.pLevelSettings["FD"].f;
		}
		else
		{
			RenderSettings.fog = false;
		}
		Scripts.scoreManager.Reset();
		GameData.levelUpRewards.Clear();
		GameData.missionRewards.Clear();
		GameData.newRewardFromCrate = null;
		GameData.trackPhysicMaterial = this.trackPhysicMaterial;
		Scripts.inputManager = new InputManager();
		this.trackManager = new TrackManager();
		this.trackManager.Initialize();
		Scripts.pickUpManager = new PickUpManager();
		Scripts.pickUpManager.Initialize(this.trackManager.camera.transform);
		GameObject gameObject2 = Loader.LoadGameObject("Shared", "Skybox/Skybox_Prefab");
		gameObject2.layer = GameData.skyboxLayer;
		gameObject2.transform.position = Vector3.zero;
		GameObject gameObject3 = new GameObject("Skybox Camera");
		gameObject3.transform.position = Vector3.zero;
		gameObject3.AddComponent<Camera>();
		gameObject3.AddComponent<SkyBoxScript>().LinkToCamera(this.trackManager.camera.camera);
		if (GameData.gameState == GameData.GameState.GAME)
		{
			Scripts.gridManager.Init(GameData.playerCarScript.gameObject);
		}
		else
		{
			GameObject gameObject4 = new GameObject("HackyDummy");
			gameObject4.transform.position = new Vector3(-1000f, 20f, -800f);
			Scripts.gridManager.Init(gameObject4);
		}
		Scripts.trafficManager.SpawnTrafficAroundPlayer();
		this.pTimeCounter = 1.5f;
		this.pFlow = 1;
		Data.raceInProgress = false;
		Data.pausingAllowed = true;
		Options.SetVisuals();
	}

	// Token: 0x060008BE RID: 2238 RVA: 0x000411E4 File Offset: 0x0003F3E4
	public void ShowCar()
	{
		Scripts.gridManager.Init(GameData.playerCarScript.gameObject);
		this.StopIntroCamera();
		this.trackManager.ShowCar();
		this.pFlow = 4;
	}

	// Token: 0x060008BF RID: 2239 RVA: 0x00041220 File Offset: 0x0003F420
	public void StartRun()
	{
		Data.raceInProgress = true;
		this.pFlow = 5;
		this.HandleGadgetsAtStart();
		this.trackManager.StartRun();
		this.trackManager.UnlockPlayerCar();
		this.HandleGadgetsAtGo();
		GameObject gameObject = new GameObject("SafeHouseActive");
		gameObject.transform.position = SafeHousePosition.GetCurrentSafeHousePosition().position;
		Scripts.interfaceScript.CreateMapIcon(gameObject);
	}

	// Token: 0x060008C0 RID: 2240 RVA: 0x00041288 File Offset: 0x0003F488
	public void GoToShop()
	{
		this.SetTimeScale(0f);
		Data.pause = true;
		Data.pausingAllowed = false;
		GameData.playerCarScript.SetSounds(false);
		this.interfaceScript.ShowShop();
	}

	// Token: 0x060008C1 RID: 2241 RVA: 0x000412B8 File Offset: 0x0003F4B8
	public void ContinueFromMissionCompletion()
	{
		Debug.Log("Continuing from mission");
		this.SetTimeScale(this.pTimeScale);
		Data.pause = false;
		this.pFlow = 5;
		Data.pausingAllowed = false;
		this.trackManager.ContinueFromMissionCompletion();
	}

	// Token: 0x060008C2 RID: 2242 RVA: 0x000412FC File Offset: 0x0003F4FC
	private void Update()
	{
		if (Data.cheats)
		{
			if (Input.GetKeyDown(KeyCode.U))
			{
				this.CheatUnlockEverything();
			}
			if (Input.GetKeyDown(KeyCode.I))
			{
				this.CheatGive10000Cash();
			}
			if (Input.GetKeyDown(KeyCode.O))
			{
				Scripts.medalsManager.CheatNearlyGetAllMedals();
			}
			if (Input.GetKeyDown(KeyCode.Y))
			{
				GameData.mainMission.completed = true;
				this.MainMissionCompleted();
			}
			if (Input.GetKeyDown(KeyCode.T))
			{
				GameData.mainMission.active = !GameData.mainMission.active;
			}
			if (Input.GetKeyDown(KeyCode.G))
			{
				GameData.godMode = !GameData.godMode;
			}
		}
		if (Input.GetKeyDown(KeyCode.F) && this.pCheatText != "xf")
		{
			Options.ToggleFullScreen();
		}
		switch (this.pFlow)
		{
		case 1:
			this.trackManager.UpdatePickUpsExplicit();
			break;
		case 2:
			this.trackManager.UpdatePickUpsExplicit();
			if (this.pTimeCounter < 0f)
			{
				this.pFlow = 3;
				this.pTimeCounter = 1.5f;
			}
			else
			{
				this.pTimeCounter -= Time.deltaTime;
			}
			break;
		case 6:
			if (this.pTimeCounter < 0f)
			{
				this.pFlow = 8;
			}
			else
			{
				this.pTimeCounter -= Time.deltaTime;
			}
			break;
		}
		if (Data.raceInProgress)
		{
			if (this.trackManager != null && !Data.pause)
			{
				this.trackManager.Update();
			}
			if (Scripts.inputManager != null)
			{
				Scripts.inputManager.UpdateInput();
			}
			Scripts.trafficManager.Update();
		}
	}

	// Token: 0x060008C3 RID: 2243 RVA: 0x000414E8 File Offset: 0x0003F6E8
	public void PlayerDestroyed()
	{
		Scripts.audioManager.StopSound("InGame");
		this.interfaceScript.interfacePanelScript.InterfaceActive(false);
		this.interfaceScript.PlayerDestroyed();
		this.pTimeCounter = 1f;
		this.pFlow = 6;
		Data.pausingAllowed = false;
	}

	// Token: 0x060008C4 RID: 2244 RVA: 0x00041538 File Offset: 0x0003F738
	public void MainMissionCompleted()
	{
		FlurryScript.LogEvent("mission complete", new string[]
		{
			GameData.mainMissionNum.ToString()
		});
		Level.AddExperience(GameData.mainMission.xp);
		Missions.HandleCompletedMissions();
		Data.raceInProgress = false;
		base.StartCoroutine(this.interfaceScript.MissionComplete());
		UserData.Save();
	}

	// Token: 0x060008C5 RID: 2245 RVA: 0x00041594 File Offset: 0x0003F794
	public void MainMissionCompletedFreeze()
	{
		this.pTimeScale = Time.timeScale;
		this.SetTimeScale(0.2f);
		Data.pause = true;
		this.pFlow = 7;
		Data.pausingAllowed = false;
		Scripts.audioManager.StopAllSounds();
	}

	// Token: 0x060008C6 RID: 2246 RVA: 0x000415CC File Offset: 0x0003F7CC
	public bool IsRaceFinished()
	{
		return this.pFlow > 5;
	}

	// Token: 0x060008C7 RID: 2247 RVA: 0x000415D8 File Offset: 0x0003F7D8
	public void StopIntroCamera()
	{
		Debug.Log("StopIntroCamera");
		this.pTimeCounter = 1f;
		this.pFlow = 2;
		this.trackManager.cameraScript.DoCarCamera();
		this.trackManager.UpdatePickUpsExplicit();
	}

	// Token: 0x060008C8 RID: 2248 RVA: 0x00041614 File Offset: 0x0003F814
	public void PauseGame()
	{
		this.pTimeScale = Time.timeScale;
		this.SetTimeScale(0f);
		this.trackManager.PauseGame();
		this.interfaceScript.pauseGame();
		AudioSource sound = Scripts.audioManager.GetSound("InGame");
		if (sound)
		{
			sound.volume = Data.Shared["Misc"].d["MusicPauseVolume"].f;
		}
		if (Data.sfx && !Data.muteAllSound)
		{
			Scripts.audioManager.MuteSFX(true);
		}
		Data.pause = true;
	}

	// Token: 0x060008C9 RID: 2249 RVA: 0x000416B8 File Offset: 0x0003F8B8
	public void UnPauseGame()
	{
		this.SetTimeScale(this.pTimeScale);
		this.trackManager.unPauseGame();
		this.interfaceScript.unPauseGame();
		AudioSource sound = Scripts.audioManager.GetSound("InGame");
		if (sound)
		{
			sound.volume = Data.Shared["Misc"].d["MusicVolume"].f;
		}
		if (Data.sfx && !Data.muteAllSound)
		{
			Scripts.audioManager.MuteSFX(false);
		}
		Data.pause = false;
	}

	// Token: 0x060008CA RID: 2250 RVA: 0x00041750 File Offset: 0x0003F950
	public void RetryGame()
	{
		if (Data.pause)
		{
			this.UnPauseGame();
		}
		Debug.Log("Restarting track");
		this.LoadApplicationLevel("Game");
	}

	// Token: 0x060008CB RID: 2251 RVA: 0x00041778 File Offset: 0x0003F978
	public void QuitTrack()
	{
		if (Data.pause)
		{
			this.UnPauseGame();
		}
		Data.stringToBundle["Level" + Data.level].assetBundle.Unload(true);
		this.LoadApplicationLevel("Game");
		Data.retried = false;
	}

	// Token: 0x060008CC RID: 2252 RVA: 0x000417D0 File Offset: 0x0003F9D0
	public void ContinueTrackNoUnlockScreen()
	{
		this.RetryGame();
	}

	// Token: 0x060008CD RID: 2253 RVA: 0x000417D8 File Offset: 0x0003F9D8
	public void ToMenuOrShop()
	{
		this.trackManager.cameraScript.ResetCam();
	}

	// Token: 0x060008CE RID: 2254 RVA: 0x000417EC File Offset: 0x0003F9EC
	public void ContinueTrackWithUnlockScreen()
	{
	}

	// Token: 0x060008CF RID: 2255 RVA: 0x000417F0 File Offset: 0x0003F9F0
	private void HandleGadgetsAtStart()
	{
		GameData.afterBlast = false;
		GameData.nuclearDetonator = false;
		GameData.coinMagnet = false;
		GameData.coinDuplicator = false;
	}

	// Token: 0x060008D0 RID: 2256 RVA: 0x0004180C File Offset: 0x0003FA0C
	private void HandleGadgetsAtGo()
	{
	}

	// Token: 0x060008D1 RID: 2257 RVA: 0x00041810 File Offset: 0x0003FA10
	private void CheatUnlockEverything()
	{
		Debug.Log("Cheat: Unlocked everything");
		foreach (string aVehicle in Shop.GetAllVehicles())
		{
			Shop.UnlockVehicle(aVehicle);
		}
		foreach (string aSuperPower in Shop.GetAllSuperPowers())
		{
			Shop.UnlockSuperPower(aSuperPower);
		}
		foreach (string aGadget in Shop.GetAllGadgets())
		{
			Shop.UnlockGadget(aGadget);
		}
		foreach (string anUpgrade in Shop.GetAllUpgrades())
		{
			Shop.UnlockUpgrade(anUpgrade);
		}
	}

	// Token: 0x060008D2 RID: 2258 RVA: 0x00041980 File Offset: 0x0003FB80
	private void CheatGive10000Cash()
	{
		Debug.Log("Cheat: Obtained 10000 cash");
		Scripts.scoreManager.AddCash(10000);
	}

	// Token: 0x060008D3 RID: 2259 RVA: 0x0004199C File Offset: 0x0003FB9C
	private void InitBlocks()
	{
		if (this.track == null)
		{
			Debug.LogError("Track does not exist, cannot initialize block data!");
		}
		if (this.pBlockDataSet == null)
		{
			this.pBlockDataSet = new Dictionary<string, BlockData>();
			int num = 0;
			int num2 = 0;
			foreach (object obj in this.track.transform)
			{
				Transform transform = (Transform)obj;
				if (transform.name.Length > 5)
				{
					string text = transform.name.Substring(0, 5);
					bool flag = int.TryParse(transform.name.Substring(0, 2), out num);
					if (flag)
					{
						flag = int.TryParse(transform.name.Substring(3, 2), out num2);
					}
					if (flag)
					{
						if (!this.pBlockDataSet.ContainsKey(text))
						{
							BlockData blockData = new BlockData(text);
							string key = "Block" + text.Replace("_", "X");
							if (this.pLevelText.ContainsKey(key))
							{
								Dictionary<string, DicEntry> d = this.pLevelText[key].d;
								foreach (KeyValuePair<string, DicEntry> keyValuePair in d)
								{
									if (keyValuePair.Key == "WayPointPaths")
									{
										this.InitWaypointPath(blockData, keyValuePair.Value.l);
									}
									else if (keyValuePair.Key.StartsWith("TrafficLightController"))
									{
										blockData.AddTrafficLight(new TrafficLight(GenericFunctionsScript.VectorFromList(keyValuePair.Value.d["Pos"].l)));
									}
									else if (keyValuePair.Key.StartsWith("PickUp_"))
									{
										blockData.AddPickUp(new PickUpData(keyValuePair.Key, GenericFunctionsScript.VectorFromList(keyValuePair.Value.d["Pos"].l)));
									}
									else
									{
										blockData.AddObject(new ObjectData(keyValuePair.Key, GenericFunctionsScript.VectorFromList(keyValuePair.Value.d["Pos"].l), GenericFunctionsScript.QuaternionFromList(keyValuePair.Value.d["Rot"].l)));
									}
								}
							}
							this.pBlockDataSet.Add(text, blockData);
						}
						this.pBlockDataSet[text].AddBlockGameObject(transform.gameObject);
					}
				}
			}
		}
	}

	// Token: 0x060008D4 RID: 2260 RVA: 0x00041C8C File Offset: 0x0003FE8C
	private void InitWaypointPath(BlockData tBlockData, List<DicEntry> aWaypointPathsList)
	{
		List<WaypointPath> list = new List<WaypointPath>();
		foreach (DicEntry dicEntry in aWaypointPathsList)
		{
			WaypointPath waypointPath = new WaypointPath(dicEntry.s, tBlockData);
			if (this.pLevelText.ContainsKey(dicEntry.s))
			{
				Dictionary<string, DicEntry> d = this.pLevelText[dicEntry.s].d;
				foreach (KeyValuePair<string, DicEntry> keyValuePair in d)
				{
					Vector3 aPosition = GenericFunctionsScript.VectorFromList(keyValuePair.Value.d["Pos"].l);
					WaypointData.Kind aKind = WaypointData.Kind.NORMAL;
					string anInOutText = string.Empty;
					if (keyValuePair.Value.d.ContainsKey("In"))
					{
						aKind = WaypointData.Kind.IN;
						anInOutText = keyValuePair.Value.d["In"].s;
					}
					else if (keyValuePair.Value.d.ContainsKey("Out"))
					{
						aKind = WaypointData.Kind.OUT;
						anInOutText = keyValuePair.Value.d["Out"].s;
					}
					waypointPath.AddWaypoint(new WaypointData(waypointPath, aPosition, aKind, anInOutText));
				}
				waypointPath.SortWaypoints();
				list.Add(waypointPath);
			}
		}
		tBlockData.InitWaypointPaths(list);
	}

	// Token: 0x060008D5 RID: 2261 RVA: 0x00041E40 File Offset: 0x00040040
	private void InitGrid()
	{
		if (this.track == null)
		{
			Debug.LogError("Track does not exist, cannot initialize grid!");
		}
		if (Scripts.gridManager == null)
		{
			List<BlockData> list = new List<BlockData>(this.pBlockDataSet.Values);
			Scripts.gridManager = new GridManager(250, 250, list.ConvertAll<GridEntry>((BlockData x) => x));
		}
	}

	// Token: 0x060008D6 RID: 2262 RVA: 0x00041EBC File Offset: 0x000400BC
	private void ConnectWaypointPaths()
	{
		foreach (BlockData blockData in this.pBlockDataSet.Values)
		{
			blockData.ConnectWaypointPaths();
			blockData.InitTrafficLights();
		}
	}

	// Token: 0x060008D7 RID: 2263 RVA: 0x00041F2C File Offset: 0x0004012C
	private void SetupSafeHousesOnBlocks()
	{
		for (int i = 0; i < 10; i++)
		{
			SafeHouseData positionAt = SafeHousePosition.GetPositionAt(i);
			if (positionAt != null)
			{
				GridEntry gridEntryFromPoint = Scripts.gridManager.GetGridEntryFromPoint(positionAt.position);
				if (gridEntryFromPoint == null)
				{
					Debug.LogError("Safehouse position at position " + positionAt.position + " is not on a block!");
				}
				else
				{
					(gridEntryFromPoint as BlockData).AddSafeHousePosition(positionAt);
				}
			}
		}
	}

	// Token: 0x060008D8 RID: 2264 RVA: 0x00041FA8 File Offset: 0x000401A8
	private void SetTimeScale(float aTimeScale)
	{
		Time.timeScale = aTimeScale;
		Time.fixedDeltaTime = GameData.fixedTimeStep * aTimeScale;
	}

	// Token: 0x060008D9 RID: 2265 RVA: 0x00041FBC File Offset: 0x000401BC
	private void LoadApplicationLevel(string aScene)
	{
		Scripts.gridManager.Reset();
		Scripts.audioManager.StopAllSounds();
		Scripts.audioManager.gameObject.transform.parent = null;
		Application.LoadLevel(aScene);
	}

	// Token: 0x060008DA RID: 2266 RVA: 0x00041FF8 File Offset: 0x000401F8
	private void OnDestroy()
	{
		this.SetTimeScale(1f);
		Data.pause = false;
		if (Data.music)
		{
			AudioListener.volume = 1f;
		}
		Scripts.trackScript = null;
		Scripts.gridManager = null;
		Scripts.trafficManager = null;
		Scripts.pickUpManager = null;
		Scripts.poolManager = null;
	}

	// Token: 0x040008ED RID: 2285
	public PhysicMaterial trackPhysicMaterial;

	// Token: 0x040008EE RID: 2286
	public TrackManager trackManager;

	// Token: 0x040008EF RID: 2287
	public InterfaceScript interfaceScript;

	// Token: 0x040008F0 RID: 2288
	public Skidmarks skidMarks;

	// Token: 0x040008F1 RID: 2289
	public GameObject track;

	// Token: 0x040008F2 RID: 2290
	public GameObject interfaceRoot;

	// Token: 0x040008F3 RID: 2291
	private float pTimeCounter;

	// Token: 0x040008F4 RID: 2292
	private int pFlow = 1;

	// Token: 0x040008F5 RID: 2293
	private Dictionary<string, DicEntry> pLevelSettings;

	// Token: 0x040008F6 RID: 2294
	private string pCheatText = string.Empty;

	// Token: 0x040008F7 RID: 2295
	private Dictionary<string, DicEntry> pLevelText;

	// Token: 0x040008F8 RID: 2296
	private Dictionary<string, BlockData> pBlockDataSet;

	// Token: 0x040008F9 RID: 2297
	private float pTimeScale = 1f;

	// Token: 0x02000131 RID: 305
	private enum pFlowState
	{
		// Token: 0x040008FC RID: 2300
		INTRO_CAMERA = 1,
		// Token: 0x040008FD RID: 2301
		WAIT_FOR_FADE,
		// Token: 0x040008FE RID: 2302
		SHOW_SHOP,
		// Token: 0x040008FF RID: 2303
		WAIT_FOR_INTERFACE,
		// Token: 0x04000900 RID: 2304
		IN_RACE,
		// Token: 0x04000901 RID: 2305
		PLAYER_DESTROYED,
		// Token: 0x04000902 RID: 2306
		MAIN_MISSION_COMPLETE,
		// Token: 0x04000903 RID: 2307
		FADE_OUT
	}
}
