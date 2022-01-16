using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000002 RID: 2
public class CJAPI : CJDefinitions
{
	// Token: 0x14000001 RID: 1
	// (add) Token: 0x06000002 RID: 2 RVA: 0x00002100 File Offset: 0x00000300
	// (remove) Token: 0x06000003 RID: 3 RVA: 0x00002118 File Offset: 0x00000318
	public static event CJAPI.trigger checkingConnection;

	// Token: 0x06000004 RID: 4 RVA: 0x00002130 File Offset: 0x00000330
	private void Awake()
	{
		this.commObject = new GameObject(this.commObj);
		this.comm = (this.commObject.AddComponent("CJComm") as CJComm);
		this.stats = (this.commObject.AddComponent("CJStats") as CJStats);
		this.services = (this.commObject.AddComponent("CJServices") as CJServices);
		this.users = (this.commObject.AddComponent("CJUsers") as CJUsers);
		this.statsE = (this.commObject.AddComponent("CJStatsEvents") as CJStatsEvents);
		this.servicesE = (this.commObject.AddComponent("CJServicesEvents") as CJServicesEvents);
		this.usersE = (this.commObject.AddComponent("CJUsersEvents") as CJUsersEvents);
		this.developerBuild = this.comm.developerBuild;
		UnityEngine.Object.DontDestroyOnLoad(this.commObject);
		UnityEngine.Object.DontDestroyOnLoad(this);
		CJUsersEvents.onGetUsername += this.setGUsername;
		CJServicesEvents.onIsSignedIn += this.setGIsConnected;
	}

	// Token: 0x06000005 RID: 5 RVA: 0x00002250 File Offset: 0x00000450
	public void checkConnectionOnLogin()
	{
		if (!this.checkOnLoginIsSet)
		{
			CJServicesEvents.onLogin += this.checkConnect;
			CJServicesEvents.onLogout += this.checkConnect;
			this.checkOnLoginIsSet = true;
			if (this.developerBuild)
			{
				this.comm.Call("console.log('### Action: checkConnectionOnLogin is set.')");
			}
		}
	}

	// Token: 0x06000006 RID: 6 RVA: 0x000022AC File Offset: 0x000004AC
	public void checkConnect(bool result)
	{
		this.users.getUsername();
		this.services.isSignedIn();
		if (this.developerBuild)
		{
			this.comm.Call("console.log('### Action: checkConnect')");
		}
	}

	// Token: 0x06000007 RID: 7 RVA: 0x000022E0 File Offset: 0x000004E0
	private void setGUsername(bool result)
	{
		this.username = this.comm.username;
		if (this.developerBuild)
		{
			this.comm.Call("console.log('### Feedback: username updated')");
		}
	}

	// Token: 0x06000008 RID: 8 RVA: 0x0000231C File Offset: 0x0000051C
	private void setGIsConnected(bool result)
	{
		this.isSignedIn = this.comm.isSignedIn;
		if (this.developerBuild)
		{
			this.comm.Call("console.log('### Feedback: isSignedIn updated')");
		}
	}

	// Token: 0x06000009 RID: 9 RVA: 0x00002358 File Offset: 0x00000558
	public void checkConnectionEachTime()
	{
		this.checkConnectionEachTime(10);
	}

	// Token: 0x0600000A RID: 10 RVA: 0x00002364 File Offset: 0x00000564
	public void checkConnectionEachTime(int segundos)
	{
		if (!this.checkConnectionIsSet)
		{
			base.StartCoroutine(this.checkConnection(segundos));
			this.checkConnectionIsSet = true;
			if (this.developerBuild)
			{
				this.comm.Call("console.log('### Action: checkConnectionEachTime is set. Set to each " + segundos + " seconds')");
			}
		}
	}

	// Token: 0x0600000B RID: 11 RVA: 0x000023BC File Offset: 0x000005BC
	private IEnumerator checkConnection(int segundos)
	{
		yield return new WaitForSeconds((float)segundos);
		if (this.developerBuild)
		{
			this.comm.Call("console.log('### Feedback: Connection being check by time')");
		}
		this.users.getUsername();
		this.services.isSignedIn();
		base.StartCoroutine(this.checkConnection(segundos));
		if (CJAPI.checkingConnection != null)
		{
			CJAPI.checkingConnection(true);
		}
		yield break;
	}

	// Token: 0x0600000C RID: 12 RVA: 0x000023E8 File Offset: 0x000005E8
	private void Update()
	{
	}

	// Token: 0x0600000D RID: 13 RVA: 0x000023EC File Offset: 0x000005EC
	public void initialize(string param1, string param2)
	{
		if (!this.initialized)
		{
			this.setDeveloperKey(param1);
			this.setGameKey(param2);
			this.comm.Call(string.Concat(new string[]
			{
				"$(CJApi.users).bind('onGetUsername', function(event, result){ unityObject.getObjectById('game_src').SendMessage('",
				this.commObj,
				"', 'OnGetUsername', JSON.stringify(result))});$(CJApi.users).bind('onGetUsernameError', function(event, result){ unityObject.getObjectById('game_src').SendMessage('",
				this.commObj,
				"', 'OnGetUsername', JSON.stringify(result))});$(CJApi.services).bind('onIsSignedIn', function(event, result){ unityObject.getObjectById('game_src').SendMessage('",
				this.commObj,
				"', 'OnIsSignedIn', JSON.stringify(result))});$(CJApi.services).bind('onIsSignedInError', function(event, result){ unityObject.getObjectById('game_src').SendMessage('",
				this.commObj,
				"', 'OnIsSignedInError', JSON.stringify(result))});$(CJApi.stats).bind('onSubmit', function(event, result){ unityObject.getObjectById('game_src').SendMessage('",
				this.commObj,
				"', 'OnSubmit', JSON.stringify(result) )});$(CJApi.stats).bind('onSubmitError', function(event, result){ unityObject.getObjectById('game_src').SendMessage('",
				this.commObj,
				"', 'OnSubmitError', JSON.stringify(result) )});$(CJApi.services).bind('logged', function(event, result){ unityObject.getObjectById('game_src').SendMessage('",
				this.commObj,
				"', 'OnLogin', '' )});$(CJApi.services).bind('loggedOut', function(event, result){ unityObject.getObjectById('game_src').SendMessage('",
				this.commObj,
				"', 'OnLogout', '')});CJApi.users.getUsername('",
				this.comm.salt(),
				"');CJApi.services.isSignedIn('",
				this.comm.salt(),
				"');"
			}));
			if (this.developerBuild)
			{
				this.comm.Call(string.Concat(new string[]
				{
					"console.log('### Action: API is Initialized');$(CJApi.users).bind('onGetUsernameError', function(event, result){ unityObject.getObjectById('game_src').SendMessage('",
					this.commObj,
					"', 'showError', JSON.stringify(result.errors))});$(CJApi.services).bind('onIsSignedInError', function(event, result){ unityObject.getObjectById('game_src').SendMessage('",
					this.commObj,
					"', 'showError', JSON.stringify(result.errors))});$(CJApi.stats).bind('onSubmitError', function(event, result){ unityObject.getObjectById('game_src').SendMessage('",
					this.commObj,
					"', 'showError', JSON.stringify(result.errors) )});"
				}));
				Debug.Log("CJAPI Inicializada");
			}
			this.initialized = true;
		}
	}

	// Token: 0x0600000E RID: 14 RVA: 0x00002560 File Offset: 0x00000760
	private void setDeveloperKey(string param)
	{
		this.comm.developerKey = param;
	}

	// Token: 0x0600000F RID: 15 RVA: 0x00002570 File Offset: 0x00000770
	private void setGameKey(string param)
	{
		this.comm.gameKey = param;
	}

	// Token: 0x04000001 RID: 1
	public GameObject commObject;

	// Token: 0x04000002 RID: 2
	public CJComm comm;

	// Token: 0x04000003 RID: 3
	public CJStats stats;

	// Token: 0x04000004 RID: 4
	public CJUsers users;

	// Token: 0x04000005 RID: 5
	public CJServices services;

	// Token: 0x04000006 RID: 6
	public CJStatsEvents statsE;

	// Token: 0x04000007 RID: 7
	public CJUsersEvents usersE;

	// Token: 0x04000008 RID: 8
	public CJServicesEvents servicesE;

	// Token: 0x04000009 RID: 9
	private bool initialized;

	// Token: 0x0400000A RID: 10
	private bool checkConnectionIsSet;

	// Token: 0x0400000B RID: 11
	private bool checkOnLoginIsSet;

	// Token: 0x0400000C RID: 12
	public string username = "Guest";

	// Token: 0x0400000D RID: 13
	public bool isSignedIn;

	// Token: 0x0200008E RID: 142
	// (Invoke) Token: 0x06000428 RID: 1064
	public delegate void trigger(bool param);
}
