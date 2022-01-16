using System;
using System.Collections.Generic;

namespace Unibill.Impl
{
	// Token: 0x02000032 RID: 50
	public class HelpCentre
	{
		// Token: 0x060001CA RID: 458 RVA: 0x000076D0 File Offset: 0x000058D0
		public HelpCentre(UnibillXmlParser parser)
		{
			foreach (UnibillXmlParser.UnibillXElement unibillXElement in parser.Parse("unibillStrings", "unibillError"))
			{
				UnibillError key = (UnibillError)((int)Enum.Parse(typeof(UnibillError), unibillXElement.attributes["id"]));
				this.helpMap[key] = unibillXElement.kvps["message"];
			}
		}

		// Token: 0x060001CB RID: 459 RVA: 0x0000778C File Offset: 0x0000598C
		public string getMessage(UnibillError error)
		{
			string arg = string.Format("http://www.outlinegames.com/unibillerrors#{0}", error);
			return string.Format("{0}.\nSee {1}", this.helpMap[error], arg);
		}

		// Token: 0x0400009D RID: 157
		private Dictionary<UnibillError, string> helpMap = new Dictionary<UnibillError, string>();
	}
}
