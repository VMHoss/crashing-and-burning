using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200014A RID: 330
public class PoolTest : MonoBehaviour
{
	// Token: 0x06000998 RID: 2456 RVA: 0x0004B3F8 File Offset: 0x000495F8
	private void Awake()
	{
		Data.Shared = new Dictionary<string, DicEntry>();
		TextLoader.LoadText(this.sharedText.text, Data.Shared);
		this.pGOList = new List<GameObject>();
		this.pPoolManager = new PoolManager();
	}

	// Token: 0x06000999 RID: 2457 RVA: 0x0004B430 File Offset: 0x00049630
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.A))
		{
			GameObject @object = this.pPoolManager.GetObject("Shared_Object_StarPiece", false);
			if (@object != null)
			{
				@object.transform.position = UnityEngine.Random.insideUnitSphere * 6f;
				this.pGOList.Add(@object);
			}
		}
		if (Input.GetKeyDown(KeyCode.S))
		{
			GameObject object2 = this.pPoolManager.GetObject("Shared_Object_SpikeA", true);
			object2.transform.position = UnityEngine.Random.insideUnitSphere * 6f;
			this.pGOList.Add(object2);
		}
		if (Input.GetKeyDown(KeyCode.Space))
		{
			foreach (GameObject aGameObject in this.pGOList)
			{
				this.pPoolManager.ReturnToPool(aGameObject);
			}
			this.pGOList.Clear();
		}
	}

	// Token: 0x04000A18 RID: 2584
	private PoolManager pPoolManager;

	// Token: 0x04000A19 RID: 2585
	public TextAsset sharedText;

	// Token: 0x04000A1A RID: 2586
	public List<GameObject> pGOList;
}
