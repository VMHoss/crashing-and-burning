using System;
using System.Collections.Generic;
using System.IO;
using Mono.Xml;
using Uniject;

namespace Unibill.Impl
{
	// Token: 0x02000046 RID: 70
	public class UnibillXmlParser : SmallXmlParser.IContentHandler
	{
		// Token: 0x0600026B RID: 619 RVA: 0x00009A60 File Offset: 0x00007C60
		public UnibillXmlParser(SmallXmlParser parser, IResourceLoader loader)
		{
			this.loader = loader;
			this.parser = parser;
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00009A78 File Offset: 0x00007C78
		public List<UnibillXmlParser.UnibillXElement> Parse(string resourceFile, string forElements)
		{
			this.result = new List<UnibillXmlParser.UnibillXElement>();
			this.seeking = forElements;
			using (TextReader textReader = this.loader.openTextFile(resourceFile))
			{
				this.parser.Parse(textReader, this);
			}
			return this.result;
		}

		// Token: 0x0600026D RID: 621 RVA: 0x00009AE8 File Offset: 0x00007CE8
		public void OnStartParsing(SmallXmlParser parser)
		{
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00009AEC File Offset: 0x00007CEC
		public void OnEndParsing(SmallXmlParser parser)
		{
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00009AF0 File Offset: 0x00007CF0
		public void OnStartElement(string name, SmallXmlParser.IAttrList attrs)
		{
			if (this.reading)
			{
				this.currentName = name;
			}
			else if (name == this.seeking)
			{
				this.currentAttributes = new Dictionary<string, string>();
				for (int i = 0; i < attrs.Length; i++)
				{
					this.currentAttributes[attrs.Names[i]] = attrs.Values[i];
				}
				this.currentKvps = new Dictionary<string, string>();
				this.reading = true;
			}
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00009B74 File Offset: 0x00007D74
		public void OnEndElement(string name)
		{
			if (name.Equals(this.seeking))
			{
				this.reading = false;
				this.result.Add(new UnibillXmlParser.UnibillXElement(this.currentAttributes, this.currentKvps));
			}
		}

		// Token: 0x06000271 RID: 625 RVA: 0x00009BB8 File Offset: 0x00007DB8
		public void OnChars(string s)
		{
			if (this.reading)
			{
				this.currentKvps[this.currentName] = s;
			}
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00009BD8 File Offset: 0x00007DD8
		public void OnIgnorableWhitespace(string s)
		{
		}

		// Token: 0x06000273 RID: 627 RVA: 0x00009BDC File Offset: 0x00007DDC
		public void OnProcessingInstruction(string name, string text)
		{
		}

		// Token: 0x040000FA RID: 250
		private SmallXmlParser parser;

		// Token: 0x040000FB RID: 251
		private IResourceLoader loader;

		// Token: 0x040000FC RID: 252
		private List<UnibillXmlParser.UnibillXElement> result;

		// Token: 0x040000FD RID: 253
		private string seeking;

		// Token: 0x040000FE RID: 254
		private bool reading;

		// Token: 0x040000FF RID: 255
		private Dictionary<string, string> currentAttributes;

		// Token: 0x04000100 RID: 256
		private Dictionary<string, string> currentKvps;

		// Token: 0x04000101 RID: 257
		private string currentName;

		// Token: 0x02000047 RID: 71
		public class UnibillXElement
		{
			// Token: 0x06000274 RID: 628 RVA: 0x00009BE0 File Offset: 0x00007DE0
			public UnibillXElement(Dictionary<string, string> attributes, Dictionary<string, string> kvps)
			{
				this.attributes = attributes;
				this.kvps = kvps;
			}

			// Token: 0x17000031 RID: 49
			// (get) Token: 0x06000275 RID: 629 RVA: 0x00009BF8 File Offset: 0x00007DF8
			// (set) Token: 0x06000276 RID: 630 RVA: 0x00009C00 File Offset: 0x00007E00
			public Dictionary<string, string> attributes { get; private set; }

			// Token: 0x17000032 RID: 50
			// (get) Token: 0x06000277 RID: 631 RVA: 0x00009C0C File Offset: 0x00007E0C
			// (set) Token: 0x06000278 RID: 632 RVA: 0x00009C14 File Offset: 0x00007E14
			public Dictionary<string, string> kvps { get; private set; }
		}
	}
}
