using System;
using System.Collections.Generic;
using MiniJSON;

namespace com.miniclip
{
	// Token: 0x02000057 RID: 87
	public class AwardsFactory
	{
		// Token: 0x060002EE RID: 750 RVA: 0x0000B018 File Offset: 0x00009218
		public static AwardData BuildAwardData(string json)
		{
			uint id = 0U;
			string title = string.Empty;
			string description = string.Empty;
			Dictionary<string, object> dictionary = Json.Deserialize(json) as Dictionary<string, object>;
			if (dictionary.ContainsKey("id"))
			{
				id = Convert.ToUInt32(dictionary["id"]);
			}
			if (dictionary.ContainsKey("title"))
			{
				title = dictionary["title"].ToString();
			}
			if (dictionary.ContainsKey("description"))
			{
				description = dictionary["description"].ToString();
			}
			return new AwardData(id, title, description);
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0000B0AC File Offset: 0x000092AC
		public static string BuildErrorMessage(string json)
		{
			string text = string.Empty;
			Dictionary<string, object> dictionary = Json.Deserialize(json) as Dictionary<string, object>;
			if (dictionary.ContainsKey("description"))
			{
				text += dictionary["description"].ToString();
			}
			return text;
		}
	}
}
