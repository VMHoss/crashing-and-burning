using System;
using System.Collections;
using System.Collections.Generic;
using com.miniclip;
using com.miniclip.awards;
using MiniJSON;
using UnityEngine;

// Token: 0x02000075 RID: 117
public class MiniclipAPI : MonoBehaviour
{
	// Token: 0x14000029 RID: 41
	// (add) Token: 0x0600035B RID: 859 RVA: 0x0000C51C File Offset: 0x0000A71C
	// (remove) Token: 0x0600035C RID: 860 RVA: 0x0000C538 File Offset: 0x0000A738
	public event EventHandler<EmbedVariablesEventArgs> EmbedVariablesReceived;

	// Token: 0x1400002A RID: 42
	// (add) Token: 0x0600035D RID: 861 RVA: 0x0000C554 File Offset: 0x0000A754
	// (remove) Token: 0x0600035E RID: 862 RVA: 0x0000C570 File Offset: 0x0000A770
	public event EventHandler<UserDetailsEventArgs> UserDetailsReceived;

	// Token: 0x1400002B RID: 43
	// (add) Token: 0x0600035F RID: 863 RVA: 0x0000C58C File Offset: 0x0000A78C
	// (remove) Token: 0x06000360 RID: 864 RVA: 0x0000C5A8 File Offset: 0x0000A7A8
	public event EventHandler<UserDetailsEventArgs> LogInCompleted;

	// Token: 0x1400002C RID: 44
	// (add) Token: 0x06000361 RID: 865 RVA: 0x0000C5C4 File Offset: 0x0000A7C4
	// (remove) Token: 0x06000362 RID: 866 RVA: 0x0000C5E0 File Offset: 0x0000A7E0
	public event EventHandler<UserDetailsEventArgs> SignUpCompleted;

	// Token: 0x1400002D RID: 45
	// (add) Token: 0x06000363 RID: 867 RVA: 0x0000C5FC File Offset: 0x0000A7FC
	// (remove) Token: 0x06000364 RID: 868 RVA: 0x0000C618 File Offset: 0x0000A818
	public event EventHandler LogInCancelled;

	// Token: 0x1400002E RID: 46
	// (add) Token: 0x06000365 RID: 869 RVA: 0x0000C634 File Offset: 0x0000A834
	// (remove) Token: 0x06000366 RID: 870 RVA: 0x0000C650 File Offset: 0x0000A850
	public event EventHandler HighscoresClosed;

	// Token: 0x1400002F RID: 47
	// (add) Token: 0x06000367 RID: 871 RVA: 0x0000C66C File Offset: 0x0000A86C
	// (remove) Token: 0x06000368 RID: 872 RVA: 0x0000C688 File Offset: 0x0000A888
	public event EventHandler<UndefinedEventArgs> UndefinedEvent;

	// Token: 0x1700005F RID: 95
	// (get) Token: 0x06000369 RID: 873 RVA: 0x0000C6A4 File Offset: 0x0000A8A4
	public IAwardsAPI Awards
	{
		get
		{
			return this._awards;
		}
	}

	// Token: 0x17000060 RID: 96
	// (get) Token: 0x0600036A RID: 874 RVA: 0x0000C6AC File Offset: 0x0000A8AC
	public ICurrenciesAPI Currencies
	{
		get
		{
			return this._currencies;
		}
	}

	// Token: 0x0600036B RID: 875 RVA: 0x0000C6B4 File Offset: 0x0000A8B4
	private void Awake()
	{
		this.Init();
	}

	// Token: 0x0600036C RID: 876 RVA: 0x0000C6BC File Offset: 0x0000A8BC
	private void Start()
	{
		Debug.Log("-> MiniclipAPI::Start()");
	}

	// Token: 0x0600036D RID: 877 RVA: 0x0000C6C8 File Offset: 0x0000A8C8
	private void Update()
	{
		this._poolCount = this._updatePool.Count;
		this._i = 0;
		while (this._i < this._poolCount)
		{
			this._updatePool[this._i].Update();
			this._i++;
		}
	}

	// Token: 0x0600036E RID: 878 RVA: 0x0000C728 File Offset: 0x0000A928
	private void LateUpdate()
	{
		base.StartCoroutine(this.EndOfFrame());
	}

	// Token: 0x0600036F RID: 879 RVA: 0x0000C738 File Offset: 0x0000A938
	private IEnumerator EndOfFrame()
	{
		yield return new WaitForEndOfFrame();
		this._poolCount = this._endOfFramePool.Count;
		this._i = 0;
		while (this._i < this._poolCount)
		{
			this._endOfFramePool[this._i].Update();
			this._i++;
		}
		yield break;
	}

	// Token: 0x06000370 RID: 880 RVA: 0x0000C754 File Offset: 0x0000A954
	private void Init()
	{
		if (this.dontDestroyOnLoad)
		{
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}
		this.createUpdatePools();
		this._flashSwitcher = new FlashSwitcher();
		this._flashSwitcher.updatePoolRequested += this.OnUpdatePoolRequested;
		this._awards = new SilentAwards();
	}

	// Token: 0x06000371 RID: 881 RVA: 0x0000C7AC File Offset: 0x0000A9AC
	private void createUpdatePools()
	{
		this._updateables = new List<AbstractUpdateable>();
		this._updatePool = new List<AbstractUpdateable>();
		this._lateUpdatePool = new List<AbstractUpdateable>();
		this._endOfFramePool = new List<AbstractUpdateable>();
	}

	// Token: 0x06000372 RID: 882 RVA: 0x0000C7E8 File Offset: 0x0000A9E8
	public void AddService(AbstractService service)
	{
		Debug.Log("MiniclipAPI::AddService(...)");
		if (service is IAwardsAPI)
		{
			Debug.Log("MiniclipAPI::AddService(...) - Adding Awards");
			this._awards = null;
			this._awards = (service as IAwardsAPI);
		}
		else if (service is ICurrenciesAPI)
		{
			this._currencies = (service as ICurrenciesAPI);
		}
		Debug.Log("MiniclipAPI::AddService(...) - _updateables.Count: " + this._updateables.Count);
		if (service is AbstractUpdateableService)
		{
			AbstractUpdateable updateable = (service as AbstractUpdateableService).Updateable;
			this._updateables.Add(updateable);
			updateable.updatePoolRequested += this.OnUpdatePoolRequested;
		}
	}

	// Token: 0x06000373 RID: 883 RVA: 0x0000C898 File Offset: 0x0000AA98
	public void ShowLoginBox()
	{
		this._flashSwitcher.DisplayServicesAndCall("showLoginBox");
	}

	// Token: 0x06000374 RID: 884 RVA: 0x0000C8AC File Offset: 0x0000AAAC
	public void ShowHighscores()
	{
		this.SaveHighscore(0, 0U, string.Empty);
	}

	// Token: 0x06000375 RID: 885 RVA: 0x0000C8BC File Offset: 0x0000AABC
	public void ShowHighscores(uint level)
	{
		this.SaveHighscore(0, level, string.Empty);
	}

	// Token: 0x06000376 RID: 886 RVA: 0x0000C8CC File Offset: 0x0000AACC
	public void ShowHighscores(uint level, string levelName)
	{
		this.SaveHighscore(0, level, levelName);
	}

	// Token: 0x06000377 RID: 887 RVA: 0x0000C8D8 File Offset: 0x0000AAD8
	public void SaveHighscore(int score)
	{
		this.SaveHighscore(score, 0U, string.Empty);
	}

	// Token: 0x06000378 RID: 888 RVA: 0x0000C8E8 File Offset: 0x0000AAE8
	public void SaveHighscore(int score, uint level)
	{
		this.SaveHighscore(score, level, string.Empty);
	}

	// Token: 0x06000379 RID: 889 RVA: 0x0000C8F8 File Offset: 0x0000AAF8
	public void SaveHighscore(int score, uint level, string levelName)
	{
		string data = string.Concat(new string[]
		{
			"{\"score\": ",
			score.ToString(),
			", \"level\": ",
			level.ToString(),
			", \"levelName\": \"",
			levelName,
			"\" }"
		});
		this._flashSwitcher.DisplayServicesAndCall("saveHighscore", data);
	}

	// Token: 0x0600037A RID: 890 RVA: 0x0000C95C File Offset: 0x0000AB5C
	public void GetUserDetails()
	{
		JSCaller.Call("getUserDetails");
	}

	// Token: 0x0600037B RID: 891 RVA: 0x0000C968 File Offset: 0x0000AB68
	public void EmbedVariables()
	{
		JSCaller.Call("getEmbedVariables");
	}

	// Token: 0x0600037C RID: 892 RVA: 0x0000C974 File Offset: 0x0000AB74
	private UserDetails BuildUserDetails(string data)
	{
		UserDetails result = null;
		if (data == null || data.Length < 1)
		{
			Debug.Log("-> MiniclipAPI::BuildUserDetails() - no User Details - NOT logged in!");
			return null;
		}
		Dictionary<string, object> dictionary = Json.Deserialize(data) as Dictionary<string, object>;
		if (dictionary == null || dictionary.Count < 1)
		{
			Debug.Log("-> MiniclipAPI::BuildUserDetails() - No JSON Dictionary! :(");
		}
		else
		{
			result = new UserDetails(dictionary);
		}
		return result;
	}

	// Token: 0x0600037D RID: 893 RVA: 0x0000C9D8 File Offset: 0x0000ABD8
	public void OnUpdatePoolRequested(object sender, UpdateEventArgs args)
	{
		AbstractUpdateable abstractUpdateable = sender as AbstractUpdateable;
		if (abstractUpdateable.currentUpdatePool == args.RequestedUpdatePool)
		{
			return;
		}
		bool flag = true;
		switch (abstractUpdateable.currentUpdatePool)
		{
		case 0:
			flag = this._updatePool.Remove(abstractUpdateable);
			break;
		case 1:
			flag = this._lateUpdatePool.Remove(abstractUpdateable);
			break;
		case 2:
			flag = this._endOfFramePool.Remove(abstractUpdateable);
			break;
		}
		if (flag)
		{
			abstractUpdateable.currentUpdatePool = -1;
			switch (args.RequestedUpdatePool)
			{
			case 0:
				this._updatePool.Add(abstractUpdateable);
				break;
			case 1:
				this._lateUpdatePool.Add(abstractUpdateable);
				break;
			case 2:
				this._endOfFramePool.Add(abstractUpdateable);
				break;
			}
			abstractUpdateable.currentUpdatePool = args.RequestedUpdatePool;
			Debug.Log(string.Concat(new object[]
			{
				"-> MiniclipAPI::OnUpdatePoolRequested - [",
				abstractUpdateable.ToString(),
				"].currentUpdatePool = ",
				abstractUpdateable.currentUpdatePool
			}));
			return;
		}
		Debug.LogError("-> MiniclipAPI::OnUpdatePoolRequested - removal from current Pool failed!");
	}

	// Token: 0x0600037E RID: 894 RVA: 0x0000CB08 File Offset: 0x0000AD08
	public void OnJSNotification(string notification)
	{
		string text = string.Empty;
		string text2 = string.Empty;
		int num = notification.IndexOf("\n");
		if (num < 0)
		{
			return;
		}
		text = notification.Substring(0, num);
		text2 = notification.Substring(num + 1);
		if (text.IndexOf("currencies") > -1)
		{
			(this._currencies as AbstractService).ProcessData(text, text2);
			return;
		}
		if (text.IndexOf("award") > -1)
		{
			(this._awards as AbstractService).ProcessData(text, text2);
			return;
		}
		string text3 = text;
		switch (text3)
		{
		case "auth_user_details":
			if (this.UserDetailsReceived != null)
			{
				this.UserDetailsReceived(this, new UserDetailsEventArgs(this.BuildUserDetails(text2)));
			}
			return;
		case "auth_login":
			if (this.LogInCompleted != null)
			{
				this.LogInCompleted(this, new UserDetailsEventArgs(this.BuildUserDetails(text2)));
			}
			return;
		case "auth_cancelled":
			if (this.LogInCancelled != null)
			{
				this.LogInCancelled(this, EventArgs.Empty);
			}
			return;
		case "auth_signup":
			if (this.SignUpCompleted != null)
			{
				this.SignUpCompleted(this, new UserDetailsEventArgs(this.BuildUserDetails(text2)));
			}
			return;
		case "highscores_close":
			if (this.HighscoresClosed != null)
			{
				this.HighscoresClosed(this, EventArgs.Empty);
			}
			return;
		case "parameters":
			if (this.EmbedVariablesReceived != null)
			{
				EmbedVariables embedVariables = new EmbedVariables(text2);
				this.EmbedVariablesReceived(this, new EmbedVariablesEventArgs(embedVariables));
			}
			return;
		}
		if (this.UndefinedEvent != null)
		{
			this.UndefinedEvent(this, new UndefinedEventArgs(text, text2));
		}
	}

	// Token: 0x04000186 RID: 390
	public const string SHOW_LOGIN_BOX = "showLoginBox";

	// Token: 0x04000187 RID: 391
	public const string SHOW_HIGHSCORES = "showHighscores";

	// Token: 0x04000188 RID: 392
	public const string SAVE_HIGHSCORE = "saveHighscore";

	// Token: 0x04000189 RID: 393
	public const string GET_USER_DETAILS = "getUserDetails";

	// Token: 0x0400018A RID: 394
	public const string GET_EMBED_VARIABLES = "getEmbedVariables";

	// Token: 0x0400018B RID: 395
	private ICurrenciesAPI _currencies;

	// Token: 0x0400018C RID: 396
	private IAwardsAPI _awards;

	// Token: 0x0400018D RID: 397
	private List<AbstractUpdateable> _updateables;

	// Token: 0x0400018E RID: 398
	private List<AbstractUpdateable> _updatePool;

	// Token: 0x0400018F RID: 399
	private List<AbstractUpdateable> _lateUpdatePool;

	// Token: 0x04000190 RID: 400
	private List<AbstractUpdateable> _endOfFramePool;

	// Token: 0x04000191 RID: 401
	private FlashSwitcher _flashSwitcher;

	// Token: 0x04000192 RID: 402
	public bool dontDestroyOnLoad = true;

	// Token: 0x04000193 RID: 403
	private int _poolCount;

	// Token: 0x04000194 RID: 404
	private int _i;
}
