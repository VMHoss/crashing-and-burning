using System;
using System.Collections.Generic;
using UnityEngine;

namespace com.miniclip
{
	// Token: 0x0200007F RID: 127
	public class UserDetails
	{
		// Token: 0x060003AF RID: 943 RVA: 0x0000CFD0 File Offset: 0x0000B1D0
		public UserDetails(Dictionary<string, object> dict)
		{
			Debug.Log("-> id - Type: " + dict["id"].GetType());
			this._id = Convert.ToUInt32(dict["id"]);
			if (dict.ContainsKey("sessionid"))
			{
				this._sessionid = (string)dict["sessionid"];
			}
			else
			{
				this._sessionid = (string)dict["sid"];
			}
			this._email = (string)dict["email"];
			this._nickname = (string)dict["nickname"];
			this._location = (string)dict["location"];
			this._avatar = false;
			this._worldRank = Convert.ToSingle(dict["worldRank"]);
			this._starRank = Convert.ToSingle(dict["starRank"]);
			this._challenges = Convert.ToUInt32(dict["challenges"]);
			this._friends = Convert.ToUInt32(dict["friends"]);
			this._playerPageURL = (string)dict["playerPageURL"];
			this._playerAvatarURL = (string)dict["playerAvatarURL"];
			this._userLevel = Convert.ToInt32(dict["userLevel"]);
			if (dict.ContainsKey("avatar"))
			{
				this._avatar = Convert.ToBoolean(dict["avatar"]);
			}
			if (dict.ContainsKey("avatarCode"))
			{
				uint num = Convert.ToUInt32(dict["avatarCode"]);
				this._avatar = (num > 0U);
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x0000D190 File Offset: 0x0000B390
		public uint Id
		{
			get
			{
				return this._id;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060003B1 RID: 945 RVA: 0x0000D198 File Offset: 0x0000B398
		public string Email
		{
			get
			{
				return this._email;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060003B2 RID: 946 RVA: 0x0000D1A0 File Offset: 0x0000B3A0
		public string Nickname
		{
			get
			{
				return this._nickname;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x0000D1A8 File Offset: 0x0000B3A8
		public string Location
		{
			get
			{
				return this._location;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060003B4 RID: 948 RVA: 0x0000D1B0 File Offset: 0x0000B3B0
		public bool Avatar
		{
			get
			{
				return this._avatar;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x0000D1B8 File Offset: 0x0000B3B8
		public float WorldRank
		{
			get
			{
				return this._worldRank;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060003B6 RID: 950 RVA: 0x0000D1C0 File Offset: 0x0000B3C0
		public float StarRank
		{
			get
			{
				return this._starRank;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060003B7 RID: 951 RVA: 0x0000D1C8 File Offset: 0x0000B3C8
		public uint Challenges
		{
			get
			{
				return this._challenges;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060003B8 RID: 952 RVA: 0x0000D1D0 File Offset: 0x0000B3D0
		public uint Friends
		{
			get
			{
				return this._friends;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060003B9 RID: 953 RVA: 0x0000D1D8 File Offset: 0x0000B3D8
		public string PlayerPageURL
		{
			get
			{
				return this._playerPageURL;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060003BA RID: 954 RVA: 0x0000D1E0 File Offset: 0x0000B3E0
		public string PlayerAvatarURL
		{
			get
			{
				return this._playerAvatarURL;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060003BB RID: 955 RVA: 0x0000D1E8 File Offset: 0x0000B3E8
		public string SessionId
		{
			get
			{
				return this._sessionid;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060003BC RID: 956 RVA: 0x0000D1F0 File Offset: 0x0000B3F0
		public int UserLevel
		{
			get
			{
				return this._userLevel;
			}
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0000D1F8 File Offset: 0x0000B3F8
		public Dictionary<string, object> ToDictionary()
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>(13);
			dictionary["id"] = this._id;
			dictionary["sessionid"] = this._sessionid;
			dictionary["email"] = this._email;
			dictionary["nickname"] = this._nickname;
			dictionary["location"] = this._location;
			dictionary["avatar"] = this._avatar;
			dictionary["worldRank"] = this._worldRank;
			dictionary["starRank"] = this._starRank;
			dictionary["challenges"] = this._challenges;
			dictionary["friends"] = this._friends;
			dictionary["playerPageURL"] = this._playerPageURL;
			dictionary["playerAvatarURL"] = this._playerAvatarURL;
			dictionary["userLevel"] = this._userLevel;
			return dictionary;
		}

		// Token: 0x040001CF RID: 463
		private uint _id;

		// Token: 0x040001D0 RID: 464
		private string _email;

		// Token: 0x040001D1 RID: 465
		private string _nickname;

		// Token: 0x040001D2 RID: 466
		private string _location;

		// Token: 0x040001D3 RID: 467
		private bool _avatar;

		// Token: 0x040001D4 RID: 468
		private float _worldRank;

		// Token: 0x040001D5 RID: 469
		private float _starRank;

		// Token: 0x040001D6 RID: 470
		private uint _challenges;

		// Token: 0x040001D7 RID: 471
		private uint _friends;

		// Token: 0x040001D8 RID: 472
		private string _playerPageURL;

		// Token: 0x040001D9 RID: 473
		private string _playerAvatarURL;

		// Token: 0x040001DA RID: 474
		private string _sessionid;

		// Token: 0x040001DB RID: 475
		private int _userLevel;
	}
}
