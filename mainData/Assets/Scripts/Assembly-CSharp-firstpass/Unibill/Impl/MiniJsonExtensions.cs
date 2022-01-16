using System;
using System.Collections;
using System.Collections.Generic;

namespace Unibill.Impl
{
	// Token: 0x0200003A RID: 58
	public static class MiniJsonExtensions
	{
		// Token: 0x06000212 RID: 530 RVA: 0x00008750 File Offset: 0x00006950
		public static string toJson(this Hashtable obj)
		{
			return MiniJSON.jsonEncode(obj);
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00008758 File Offset: 0x00006958
		public static string toJson(this Dictionary<string, string> obj)
		{
			return MiniJSON.jsonEncode(obj);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00008760 File Offset: 0x00006960
		public static ArrayList arrayListFromJson(this string json)
		{
			return MiniJSON.jsonDecode(json) as ArrayList;
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00008770 File Offset: 0x00006970
		public static Hashtable hashtableFromJson(this string json)
		{
			return MiniJSON.jsonDecode(json) as Hashtable;
		}
	}
}
