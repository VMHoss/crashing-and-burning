using System;
using UnityEngine;

// Token: 0x02000132 RID: 306
public class TrafficDetectionBoxScript : MonoBehaviour
{
	// Token: 0x060008DD RID: 2269 RVA: 0x00042054 File Offset: 0x00040254
	public void Initialize(TrafficScript aTrafficScript)
	{
		this.pTrafficScript = aTrafficScript;
		base.collider.isTrigger = true;
		base.gameObject.layer = GameData.trafficDetectorLayer;
	}

	// Token: 0x060008DE RID: 2270 RVA: 0x00042084 File Offset: 0x00040284
	private void OnTriggerStay()
	{
		this.pTrafficScript.brake = true;
	}

	// Token: 0x04000904 RID: 2308
	private TrafficScript pTrafficScript;
}
