using System;
using Chartboost;
using UnityEngine;

// Token: 0x02000012 RID: 18
public class CBUIManager : MonoBehaviour
{
	// Token: 0x060000AE RID: 174 RVA: 0x00004970 File Offset: 0x00002B70
	public void Update()
	{
		if (Input.GetKeyUp(KeyCode.Escape))
		{
			if (CBBinding.onBackPressed())
			{
				return;
			}
			Application.Quit();
		}
	}

	// Token: 0x060000AF RID: 175 RVA: 0x00004990 File Offset: 0x00002B90
	private void OnEnable()
	{
		CBBinding.init();
	}

	// Token: 0x060000B0 RID: 176 RVA: 0x00004998 File Offset: 0x00002B98
	private void OnDisable()
	{
	}

	// Token: 0x060000B1 RID: 177 RVA: 0x0000499C File Offset: 0x00002B9C
	private void OnGUI()
	{
		GUI.enabled = !CBBinding.isImpressionVisible();
		GUI.matrix = Matrix4x4.Scale(new Vector3(2f, 2f, 2f));
		if (GUILayout.Button("Cache Interstitial", new GUILayoutOption[0]))
		{
			CBBinding.cacheInterstitial("default");
		}
		if (GUILayout.Button("Show Interstitial", new GUILayoutOption[0]))
		{
			CBBinding.showInterstitial("default");
		}
		if (GUILayout.Button("Cache More Apps", new GUILayoutOption[0]))
		{
			CBBinding.cacheMoreApps();
		}
		if (GUILayout.Button("Show More Apps", new GUILayoutOption[0]))
		{
			CBBinding.showMoreApps();
		}
	}
}
