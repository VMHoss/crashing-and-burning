using System;
using System.IO;
using UnityEngine;

// Token: 0x02000184 RID: 388
public class KGFDocumentation : MonoBehaviour
{
	// Token: 0x06000BB4 RID: 2996 RVA: 0x00056410 File Offset: 0x00054610
	public void OpenDocumentation()
	{
		string text = Application.dataPath;
		text = Path.Combine(text, "kolmich");
		text = Path.Combine(text, "documentation");
		text = Path.Combine(text, "files");
		text += Path.DirectorySeparatorChar;
		text += "documentation.html";
		text.Replace('/', Path.DirectorySeparatorChar);
		Application.OpenURL("file://" + text);
	}
}
