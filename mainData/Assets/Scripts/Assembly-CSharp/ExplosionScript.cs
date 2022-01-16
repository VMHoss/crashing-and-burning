using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000EA RID: 234
public class ExplosionScript : MonoBehaviour
{
	// Token: 0x06000711 RID: 1809 RVA: 0x00033D84 File Offset: 0x00031F84
	public static void AddExplosion(string anExplosionName, Vector3 aPosition)
	{
		Dictionary<string, DicEntry> d = Data.Shared["Explosions"].d[anExplosionName].d;
		GameObject gameObject = Loader.LoadGameObject("Shared", "Effects/" + d["Prefab"].s);
		gameObject.transform.position = aPosition;
		UnityEngine.Object.Destroy(gameObject, gameObject.particleSystem.duration);
		gameObject.AddComponent<ExplosionScript>().Initialize(anExplosionName, d);
	}

	// Token: 0x06000712 RID: 1810 RVA: 0x00033E00 File Offset: 0x00032000
	public void Initialize(string anExplosionName, Dictionary<string, DicEntry> aProps)
	{
		this.pProps = aProps;
		string s = this.pProps["Sound"].s;
		if (s != "None")
		{
			int num = s.IndexOf("-");
			if (num <= 0)
			{
				Scripts.audioManager.PlaySFX(s, base.gameObject);
			}
			else
			{
				string s2 = s.Substring(num - 1, 1);
				string s3 = s.Substring(num + 1, 1);
				int num2 = UnityEngine.Random.Range(int.Parse(s2), int.Parse(s3) + 1);
				string aSFXName = s.Substring(0, num - 1) + num2;
				Scripts.audioManager.PlaySFX(aSFXName, base.gameObject);
			}
		}
		Vector3 position = base.transform.position;
		if (this.pProps["AreaDamage"].s != "None")
		{
			Dictionary<string, DicEntry> d = Data.Shared["AreaDamage"].d[this.pProps["AreaDamage"].s].d;
			this.pDamage = d["Damage"].f;
			this.pMaxDistance = d["MaxDistance"].f;
			RaycastHit raycastHit;
			if (d.ContainsKey("Prefab") && Physics.Raycast(position + Vector3.up, Vector3.down, out raycastHit, 10f, 1 << GameData.defaultLayer))
			{
				this.pAreaDamagePrefab = Loader.LoadGameObject("Shared", "Effects/Particles/" + d["Prefab"].s);
				this.pAreaDamagePrefab.transform.position = base.transform.position + raycastHit.normal * 0.05f;
				this.pAreaDamagePrefab.transform.rotation = Quaternion.LookRotation(raycastHit.normal);
				this.pAreaDamagePrefab.transform.parent = base.transform;
				this.pAreaDamagePrefab.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
			}
			if (d.ContainsKey("Instant"))
			{
				this.pInstantAreaDamage = d["Instant"].b;
			}
			if (this.pMaxDistance > 1f)
			{
				this.pCarsToCheck = Scripts.trafficManager.GetTrafficInRange(position, this.pMaxDistance);
				float aDistSqr = this.pMaxDistance * this.pMaxDistance;
				this.pDestructiblesToCheck = new List<DestructibleScript>();
				Vector3 aStartRectPos = base.transform.position - new Vector3(this.pMaxDistance, this.pMaxDistance);
				Vector3 anEndRectPos = base.transform.position + new Vector3(this.pMaxDistance, this.pMaxDistance);
				List<GridEntry> gridEntriesFromRectangle = Scripts.gridManager.GetGridEntriesFromRectangle(aStartRectPos, anEndRectPos);
				foreach (GridEntry gridEntry in gridEntriesFromRectangle)
				{
					(gridEntry as BlockData).GetDestructiblesInRange(position, aDistSqr, this.pDestructiblesToCheck);
				}
			}
		}
		else
		{
			this.pCarsToCheck = new List<TrafficScript>();
			this.pDestructiblesToCheck = new List<DestructibleScript>();
		}
		if (this.pProps["CameraShake"].s != "None")
		{
			Dictionary<string, DicEntry> d2 = Data.Shared["CameraShake"].d[this.pProps["CameraShake"].s].d;
			CameraScript cameraScript = Scripts.trackScript.trackManager.cameraScript;
			float num3 = d2["Range"].f - (cameraScript.transform.position - position).magnitude;
			if (num3 > 0f)
			{
				float num4 = 1f - num3 / d2["Range"].f;
				cameraScript.AddCameraShake(new CameraShake(d2["Duration"].f, d2["Intensity"].f * num4, 1f));
			}
		}
		this.pProgress = 0f;
	}

	// Token: 0x06000713 RID: 1811 RVA: 0x00034284 File Offset: 0x00032484
	private void Update()
	{
		if (this.pInstantAreaDamage)
		{
			this.pProgress += Time.deltaTime * 4f;
		}
		else
		{
			this.pProgress += Time.deltaTime * 2f;
		}
		float num;
		if (this.pProgress >= 1f)
		{
			this.pProgress = 1f;
			num = 1f;
			UnityEngine.Object.Destroy(this);
		}
		else
		{
			num = (1f - (this.pProgress - 1f) * (this.pProgress - 1f)) * this.pMaxDistance;
		}
		if (this.pAreaDamagePrefab != null)
		{
			float num2 = num * 0.5f;
			this.pAreaDamagePrefab.transform.localScale = new Vector3(num2, num2, num2);
		}
		float num3 = num * num;
		Vector3 position = base.transform.position;
		int count = this.pCarsToCheck.Count;
		for (int i = count - 1; i >= 0; i--)
		{
			TrafficScript trafficScript = this.pCarsToCheck[i];
			if (!(trafficScript == null))
			{
				Vector3 vector = trafficScript.transform.position - position;
				if (Vector3.SqrMagnitude(vector) < num3)
				{
					float num4 = (this.pMaxDistance - vector.magnitude) * 5f;
					if (num4 > 100f)
					{
						num4 = 100f;
					}
					trafficScript.Damage(new DamageInfo(this.pDamage));
					if (vector.y < 0f)
					{
						vector.y = 0f;
					}
					Vector3 normalized = vector.normalized;
					Rigidbody rigidbody = trafficScript.rigidbody;
					if (rigidbody != null)
					{
						rigidbody.velocity = rigidbody.velocity * 0.5f + normalized * num4 + new Vector3(0f, num4 * 0.5f, 0f);
						Vector3 a = Vector3.Cross(Vector3.up, normalized);
						rigidbody.angularVelocity = a * UnityEngine.Random.Range(2f, 5f);
					}
					this.pCarsToCheck.RemoveAt(i);
				}
			}
		}
		count = this.pDestructiblesToCheck.Count;
		for (int j = count - 1; j >= 0; j--)
		{
			DestructibleScript destructibleScript = this.pDestructiblesToCheck[j];
			Vector3 vector = destructibleScript.transform.position - position;
			if (Vector3.SqrMagnitude(vector) < num3)
			{
				destructibleScript.DestroyedByExplosion(this.pDamage, vector);
				this.pDestructiblesToCheck.RemoveAt(j);
			}
		}
	}

	// Token: 0x04000745 RID: 1861
	private Dictionary<string, DicEntry> pProps;

	// Token: 0x04000746 RID: 1862
	private float pDamage = 1f;

	// Token: 0x04000747 RID: 1863
	private float pMaxDistance;

	// Token: 0x04000748 RID: 1864
	private GameObject pAreaDamagePrefab;

	// Token: 0x04000749 RID: 1865
	private bool pInstantAreaDamage;

	// Token: 0x0400074A RID: 1866
	private float pProgress;

	// Token: 0x0400074B RID: 1867
	private List<TrafficScript> pCarsToCheck;

	// Token: 0x0400074C RID: 1868
	private List<DestructibleScript> pDestructiblesToCheck;
}
