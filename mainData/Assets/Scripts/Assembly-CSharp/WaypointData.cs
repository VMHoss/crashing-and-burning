using System;
using UnityEngine;

// Token: 0x02000138 RID: 312
public class WaypointData
{
	// Token: 0x06000903 RID: 2307 RVA: 0x00043984 File Offset: 0x00041B84
	public WaypointData(WaypointPath aWaypointPath, Vector3 aPosition, WaypointData.Kind aKind, string anInOutText)
	{
		this.waypointPath = aWaypointPath;
		this.position = -aPosition;
		this.position.y = aPosition.y;
		this.kind = aKind;
		this.inOutText = anInOutText;
	}

	// Token: 0x0400093F RID: 2367
	public readonly WaypointPath waypointPath;

	// Token: 0x04000940 RID: 2368
	public readonly Vector3 position;

	// Token: 0x04000941 RID: 2369
	public bool redLight;

	// Token: 0x04000942 RID: 2370
	public bool lastPointInPath;

	// Token: 0x04000943 RID: 2371
	public WaypointData.Kind kind;

	// Token: 0x04000944 RID: 2372
	public string inOutText = string.Empty;

	// Token: 0x02000139 RID: 313
	public enum Kind
	{
		// Token: 0x04000946 RID: 2374
		IN,
		// Token: 0x04000947 RID: 2375
		NORMAL,
		// Token: 0x04000948 RID: 2376
		OUT
	}
}
