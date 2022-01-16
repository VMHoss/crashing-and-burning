using System;
using UnityEngine;

// Token: 0x0200010C RID: 268
public class ObjectData
{
	// Token: 0x060007D7 RID: 2007 RVA: 0x0003B46C File Offset: 0x0003966C
	public ObjectData(string aName, Vector3 aPosition, Quaternion aRotation)
	{
		this.name = aName;
		int length = this.name.IndexOf('_');
		this.objectName = this.name.Substring(0, length);
		this.name = "Object_" + this.name;
		this.position = -aPosition;
		this.position.y = aPosition.y;
		this.rotation = aRotation;
		this.isDestructible = Data.Shared["Destructible"].d.ContainsKey(this.objectName);
	}

	// Token: 0x0400085D RID: 2141
	public string name = string.Empty;

	// Token: 0x0400085E RID: 2142
	public string objectName = string.Empty;

	// Token: 0x0400085F RID: 2143
	public Vector3 position;

	// Token: 0x04000860 RID: 2144
	public Quaternion rotation;

	// Token: 0x04000861 RID: 2145
	public bool isDestructible;
}
