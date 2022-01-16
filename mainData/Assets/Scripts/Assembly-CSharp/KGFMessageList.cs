using System;
using System.Collections.Generic;

// Token: 0x0200018C RID: 396
public class KGFMessageList
{
	// Token: 0x06000BBA RID: 3002 RVA: 0x000564F0 File Offset: 0x000546F0
	public void AddError(string theMessage)
	{
		this.itsHasErrors = true;
		this.itsMessageList.Add(new KGFMessageList.Error(theMessage));
	}

	// Token: 0x06000BBB RID: 3003 RVA: 0x0005650C File Offset: 0x0005470C
	public void AddInfo(string theMessage)
	{
		this.itsHasInfos = true;
		this.itsMessageList.Add(new KGFMessageList.Info(theMessage));
	}

	// Token: 0x06000BBC RID: 3004 RVA: 0x00056528 File Offset: 0x00054728
	public void AddWarning(string theMessage)
	{
		this.itsHasWarnings = true;
		this.itsMessageList.Add(new KGFMessageList.Warning(theMessage));
	}

	// Token: 0x06000BBD RID: 3005 RVA: 0x00056544 File Offset: 0x00054744
	public string[] GetErrorArray()
	{
		List<string> list = new List<string>();
		foreach (KGFMessageList.Message message in this.itsMessageList)
		{
			if (message is KGFMessageList.Error)
			{
				list.Add(message.Description);
			}
		}
		return list.ToArray();
	}

	// Token: 0x06000BBE RID: 3006 RVA: 0x000565C8 File Offset: 0x000547C8
	public string[] GetInfoArray()
	{
		List<string> list = new List<string>();
		foreach (KGFMessageList.Message message in this.itsMessageList)
		{
			if (message is KGFMessageList.Info)
			{
				list.Add(message.Description);
			}
		}
		return list.ToArray();
	}

	// Token: 0x06000BBF RID: 3007 RVA: 0x0005664C File Offset: 0x0005484C
	public string[] GetWarningArray()
	{
		List<string> list = new List<string>();
		foreach (KGFMessageList.Message message in this.itsMessageList)
		{
			if (message is KGFMessageList.Warning)
			{
				list.Add(message.Description);
			}
		}
		return list.ToArray();
	}

	// Token: 0x06000BC0 RID: 3008 RVA: 0x000566D0 File Offset: 0x000548D0
	public KGFMessageList.Message[] GetAllMessagesArray()
	{
		return this.itsMessageList.ToArray();
	}

	// Token: 0x06000BC1 RID: 3009 RVA: 0x000566E0 File Offset: 0x000548E0
	public void AddMessages(KGFMessageList.Message[] theMessages)
	{
		foreach (KGFMessageList.Message message in theMessages)
		{
			this.itsMessageList.Add(message);
			if (message is KGFMessageList.Error)
			{
				this.itsHasErrors = true;
			}
			if (message is KGFMessageList.Warning)
			{
				this.itsHasWarnings = true;
			}
			if (message is KGFMessageList.Info)
			{
				this.itsHasInfos = true;
			}
		}
	}

	// Token: 0x04000B9E RID: 2974
	public bool itsHasErrors;

	// Token: 0x04000B9F RID: 2975
	public bool itsHasWarnings;

	// Token: 0x04000BA0 RID: 2976
	public bool itsHasInfos;

	// Token: 0x04000BA1 RID: 2977
	private List<KGFMessageList.Message> itsMessageList = new List<KGFMessageList.Message>();

	// Token: 0x0200018D RID: 397
	public abstract class Message
	{
		// Token: 0x06000BC2 RID: 3010 RVA: 0x0005674C File Offset: 0x0005494C
		public Message(string theMessage)
		{
			this.itsMessage = theMessage;
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000BC3 RID: 3011 RVA: 0x0005675C File Offset: 0x0005495C
		public string Description
		{
			get
			{
				return this.itsMessage;
			}
		}

		// Token: 0x04000BA2 RID: 2978
		private string itsMessage;
	}

	// Token: 0x0200018E RID: 398
	public class Error : KGFMessageList.Message
	{
		// Token: 0x06000BC4 RID: 3012 RVA: 0x00056764 File Offset: 0x00054964
		public Error(string theMessage) : base(theMessage)
		{
		}
	}

	// Token: 0x0200018F RID: 399
	public class Info : KGFMessageList.Message
	{
		// Token: 0x06000BC5 RID: 3013 RVA: 0x00056770 File Offset: 0x00054970
		public Info(string theMessage) : base(theMessage)
		{
		}
	}

	// Token: 0x02000190 RID: 400
	public class Warning : KGFMessageList.Message
	{
		// Token: 0x06000BC6 RID: 3014 RVA: 0x0005677C File Offset: 0x0005497C
		public Warning(string theMessage) : base(theMessage)
		{
		}
	}
}
