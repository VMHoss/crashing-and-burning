using System;

namespace Mono.Xml
{
	// Token: 0x02000041 RID: 65
	internal class SmallXmlParserException : SystemException
	{
		// Token: 0x06000255 RID: 597 RVA: 0x000097D8 File Offset: 0x000079D8
		public SmallXmlParserException(string msg, int line, int column) : base(string.Format("{0}. At ({1},{2})", msg, line, column))
		{
			this.line = line;
			this.column = column;
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000256 RID: 598 RVA: 0x00009808 File Offset: 0x00007A08
		public int Line
		{
			get
			{
				return this.line;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000257 RID: 599 RVA: 0x00009810 File Offset: 0x00007A10
		public int Column
		{
			get
			{
				return this.column;
			}
		}

		// Token: 0x040000D2 RID: 210
		private int line;

		// Token: 0x040000D3 RID: 211
		private int column;
	}
}
