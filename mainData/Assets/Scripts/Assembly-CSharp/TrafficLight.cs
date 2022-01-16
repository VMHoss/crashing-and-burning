using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000134 RID: 308
public class TrafficLight
{
	// Token: 0x060008E2 RID: 2274 RVA: 0x00042120 File Offset: 0x00040320
	public TrafficLight(Vector3 aPosition)
	{
		this.position = -aPosition;
		this.position.y = aPosition.y;
		this.pLanes = new Dictionary<int, List<WaypointData>>();
		for (int i = 0; i < 4; i++)
		{
			this.pLanes.Add(i, new List<WaypointData>());
		}
	}

	// Token: 0x060008E3 RID: 2275 RVA: 0x00042198 File Offset: 0x00040398
	public void AddControlPoint(WaypointData anOutWaypointData)
	{
		if (anOutWaypointData.kind != WaypointData.Kind.OUT)
		{
			Debug.LogWarning("TrafficLight::AddControlPoint is not an out point");
		}
		string text = anOutWaypointData.inOutText.Substring(0, 1).ToUpper();
		string text2 = text;
		if (text2 != null)
		{
			if (TrafficLight.<>f__switch$map25 == null)
			{
				TrafficLight.<>f__switch$map25 = new Dictionary<string, int>(4)
				{
					{
						"A",
						0
					},
					{
						"B",
						1
					},
					{
						"C",
						2
					},
					{
						"D",
						3
					}
				};
			}
			int num;
			if (TrafficLight.<>f__switch$map25.TryGetValue(text2, out num))
			{
				int key;
				switch (num)
				{
				case 0:
					key = 0;
					break;
				case 1:
					key = 1;
					break;
				case 2:
					key = 2;
					break;
				case 3:
					key = 3;
					break;
				default:
					goto IL_C4;
				}
				this.pLanes[key].Add(anOutWaypointData);
				return;
			}
		}
		IL_C4:
		Debug.LogWarning(string.Concat(new object[]
		{
			"Unknown lane named '",
			text,
			"' at traffic light pos: ",
			this.position
		}));
	}

	// Token: 0x060008E4 RID: 2276 RVA: 0x000422B0 File Offset: 0x000404B0
	public void Initialize()
	{
		this.SetRedLights(0, false);
		this.pCurLaneGreenLight = 0;
		for (int i = 1; i < 4; i++)
		{
			this.SetRedLights(i, true);
		}
	}

	// Token: 0x060008E5 RID: 2277 RVA: 0x000422E8 File Offset: 0x000404E8
	public void Update()
	{
		this.pNextSwitchTime -= Time.deltaTime;
		if (this.pNextSwitchTime < 0f)
		{
			this.pOverallRedTime -= Time.deltaTime;
			if (this.pOverallRedTime < 0f)
			{
				this.pOverallRedTime = 2f;
				this.SetNextLane();
				this.SetRedLights(this.pCurLaneGreenLight, false);
				this.pNextSwitchTime = 4f;
			}
			else
			{
				this.SetRedLights(this.pCurLaneGreenLight, true);
			}
		}
	}

	// Token: 0x060008E6 RID: 2278 RVA: 0x00042374 File Offset: 0x00040574
	private void SetNextLane()
	{
		int num = 0;
		do
		{
			this.pCurLaneGreenLight = (this.pCurLaneGreenLight + 1) % 4;
			num++;
		}
		while (this.pLanes[this.pCurLaneGreenLight].Count == 0 && num < 10);
		if (num == 10)
		{
			Debug.LogError("Traffic light prevented endless loop (lanes are inproper) at traffic light pos: " + this.position);
		}
	}

	// Token: 0x060008E7 RID: 2279 RVA: 0x000423DC File Offset: 0x000405DC
	private void SetRedLights(int aLaneInt, bool aRedLight)
	{
		foreach (WaypointData waypointData in this.pLanes[aLaneInt])
		{
			waypointData.redLight = aRedLight;
		}
	}

	// Token: 0x04000906 RID: 2310
	private const float NEXT_SWITCH_TIME = 4f;

	// Token: 0x04000907 RID: 2311
	private const float OVERALL_RED_TIME = 2f;

	// Token: 0x04000908 RID: 2312
	public Vector3 position;

	// Token: 0x04000909 RID: 2313
	private Dictionary<int, List<WaypointData>> pLanes;

	// Token: 0x0400090A RID: 2314
	private float pNextSwitchTime = 4f;

	// Token: 0x0400090B RID: 2315
	private float pOverallRedTime = 2f;

	// Token: 0x0400090C RID: 2316
	private int pCurLaneGreenLight;
}
