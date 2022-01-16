using System;
using System.IO;
using UnityEngine;

namespace Uniject.Impl
{
	// Token: 0x0200001B RID: 27
	public class UnityResourceLoader : IResourceLoader
	{
		// Token: 0x060000DF RID: 223 RVA: 0x00004C74 File Offset: 0x00002E74
		public TextReader openTextFile(string path)
		{
			return new StringReader(((TextAsset)Resources.Load(path)).text);
		}
	}
}
