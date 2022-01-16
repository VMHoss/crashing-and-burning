using System;
using System.IO;

namespace Uniject
{
	// Token: 0x02000016 RID: 22
	public interface IResourceLoader
	{
		// Token: 0x060000CB RID: 203
		TextReader openTextFile(string path);
	}
}
