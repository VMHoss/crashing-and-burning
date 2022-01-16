using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000DC RID: 220
public class PoolManager
{
	// Token: 0x060006B2 RID: 1714 RVA: 0x0002FD68 File Offset: 0x0002DF68
	public PoolManager()
	{
		if (!Data.Shared.ContainsKey("Pooling"))
		{
			Debug.LogError("There's no pooling information in SharedData.txt!");
		}
		this.pDefaultPoolSize = Data.Shared["Pooling"].d["PoolDefaultSize"].i;
		this.pObjectNameToResource = new Dictionary<string, GameObject>();
		this.pFreePoolObjects = new Dictionary<string, List<GameObject>>();
		this.pUsedPoolObjects = new Dictionary<string, List<GameObject>>();
		this.pObjectToPoolName = new Dictionary<GameObject, string>();
		this.pContainer = new GameObject("PoolObjects").transform;
		this.pContainer.gameObject.SetActive(false);
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		foreach (KeyValuePair<string, DicEntry> keyValuePair in Data.Shared["Pooling"].d)
		{
			if (!(keyValuePair.Key == "PoolDefaultSize"))
			{
				string text = keyValuePair.Value.d["Path"].s;
				int num = text.IndexOf("/");
				string text2 = text.Substring(0, num);
				text = text.Substring(num + 1);
				this.pObjectNameToResource.Add(keyValuePair.Key, Loader.LoadObject(text2, text + "/" + keyValuePair.Key) as GameObject);
				text = text.Replace("Prefabs/", string.Empty);
				GameObject gameObject;
				if (keyValuePair.Value.d.ContainsKey("Prefab"))
				{
					gameObject = Loader.LoadChildObject(text2, text + "/" + keyValuePair.Value.d["Prefab"].s, keyValuePair.Key);
				}
				else
				{
					gameObject = Loader.LoadGameObject(text2, text + "/" + keyValuePair.Key);
				}
				if (gameObject == null)
				{
					Debug.LogError(string.Concat(new string[]
					{
						"Pool error, could not find object: ",
						text2,
						"/",
						text,
						"/",
						keyValuePair.Key
					}));
				}
				gameObject.name = keyValuePair.Key;
				gameObject.transform.parent = this.pContainer;
				int i;
				if (keyValuePair.Value.d.ContainsKey("PoolSize"))
				{
					i = keyValuePair.Value.d["PoolSize"].i;
				}
				else
				{
					i = this.pDefaultPoolSize;
				}
				this.pFreePoolObjects.Add(keyValuePair.Key, new List<GameObject>());
				this.pUsedPoolObjects.Add(keyValuePair.Key, new List<GameObject>());
				this.pFreePoolObjects[keyValuePair.Key].Add(gameObject);
				this.pObjectToPoolName.Add(gameObject, keyValuePair.Key);
				for (int j = 1; j < i; j++)
				{
					gameObject = (UnityEngine.Object.Instantiate(gameObject) as GameObject);
					gameObject.name = keyValuePair.Key;
					gameObject.transform.parent = this.pContainer;
					this.pFreePoolObjects[keyValuePair.Key].Add(gameObject);
					this.pObjectToPoolName.Add(gameObject, keyValuePair.Key);
				}
			}
		}
		Debug.Log("Pool manager initialization took: " + (Time.realtimeSinceStartup - realtimeSinceStartup) * 1000f + "ms");
	}

	// Token: 0x060006B3 RID: 1715 RVA: 0x00030128 File Offset: 0x0002E328
	public GameObject GetObject(string anObjectName, bool anAllowCreation)
	{
		List<GameObject> list = this.pFreePoolObjects[anObjectName];
		List<GameObject> list2 = this.pUsedPoolObjects[anObjectName];
		GameObject gameObject;
		if (list.Count == 0)
		{
			if (!anAllowCreation)
			{
				return null;
			}
			gameObject = (UnityEngine.Object.Instantiate(this.pObjectNameToResource[anObjectName]) as GameObject);
			gameObject.name = anObjectName;
			this.pObjectToPoolName.Add(gameObject, anObjectName);
		}
		else
		{
			gameObject = list[list.Count - 1];
			list.RemoveAt(list.Count - 1);
		}
		list2.Add(gameObject);
		gameObject.transform.parent = null;
		return gameObject;
	}

	// Token: 0x060006B4 RID: 1716 RVA: 0x000301C8 File Offset: 0x0002E3C8
	public void ReturnToPool(GameObject aGameObject)
	{
		string key = this.pObjectToPoolName[aGameObject];
		List<GameObject> list = this.pFreePoolObjects[key];
		List<GameObject> list2 = this.pUsedPoolObjects[key];
		list2.Remove(aGameObject);
		list.Add(aGameObject);
		aGameObject.transform.parent = this.pContainer;
	}

	// Token: 0x060006B5 RID: 1717 RVA: 0x0003021C File Offset: 0x0002E41C
	public void TestIntegrity()
	{
		Debug.Log("Testing pool integrity");
		foreach (KeyValuePair<string, List<GameObject>> keyValuePair in this.pFreePoolObjects)
		{
			foreach (GameObject gameObject in keyValuePair.Value)
			{
				if (gameObject == null)
				{
					Debug.LogWarning("We got a tGO == null in the free pool!");
				}
				else
				{
					if (gameObject.transform.parent != this.pContainer)
					{
						Debug.LogWarning("Free object " + gameObject.name + " should be parenter to pool container!");
					}
					if (this.pUsedPoolObjects[keyValuePair.Key].IndexOf(gameObject) >= 0)
					{
						Debug.LogWarning("We got a duplicate!");
					}
				}
			}
		}
		foreach (KeyValuePair<string, List<GameObject>> keyValuePair2 in this.pUsedPoolObjects)
		{
			foreach (GameObject gameObject2 in keyValuePair2.Value)
			{
				if (gameObject2 == null)
				{
					Debug.LogWarning("We got a tGO == null in the used pool!");
				}
				else if (gameObject2.transform.parent == this.pContainer)
				{
					Debug.LogWarning("used object " + gameObject2.name + " should not be in pool container!");
				}
			}
		}
	}

	// Token: 0x040006D8 RID: 1752
	private Dictionary<string, GameObject> pObjectNameToResource;

	// Token: 0x040006D9 RID: 1753
	private Dictionary<string, List<GameObject>> pFreePoolObjects;

	// Token: 0x040006DA RID: 1754
	private Dictionary<string, List<GameObject>> pUsedPoolObjects;

	// Token: 0x040006DB RID: 1755
	private Dictionary<GameObject, string> pObjectToPoolName;

	// Token: 0x040006DC RID: 1756
	private int pDefaultPoolSize = 1;

	// Token: 0x040006DD RID: 1757
	private Transform pContainer;
}
