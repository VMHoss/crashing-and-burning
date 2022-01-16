using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200013A RID: 314
public class WaypointPath
{
	// Token: 0x06000904 RID: 2308 RVA: 0x000439D8 File Offset: 0x00041BD8
	public WaypointPath(string aName, BlockData aBlockData)
	{
		this.name = aName;
		this.blockData = aBlockData;
		this.waypoints = new List<WaypointData>();
		this.nextPaths = new List<WaypointPath>();
	}

	// Token: 0x06000905 RID: 2309 RVA: 0x00043A10 File Offset: 0x00041C10
	public void AddWaypoint(WaypointData aWaypointData)
	{
		this.waypoints.Add(aWaypointData);
	}

	// Token: 0x06000906 RID: 2310 RVA: 0x00043A20 File Offset: 0x00041C20
	public void SortWaypoints()
	{
		int num = this.waypoints.FindIndex((WaypointData tWD) => tWD.kind == WaypointData.Kind.IN);
		if (num != 0)
		{
			this.SwapWaypointsByIndex(num, 0);
		}
		int count = this.waypoints.Count;
		for (int i = 0; i < count - 1; i++)
		{
			float num2 = 999999f;
			int num3 = i + 1;
			for (int j = i + 1; j < count; j++)
			{
				float num4 = Vector3.SqrMagnitude(this.waypoints[i].position - this.waypoints[j].position);
				if (num4 < num2)
				{
					num2 = num4;
					num3 = j;
				}
			}
			if (num3 != i + 1)
			{
				this.SwapWaypointsByIndex(num3, i + 1);
			}
		}
		if (this.waypoints[count - 1].kind != WaypointData.Kind.OUT)
		{
			Debug.LogError("Last waypoint was not out for waypoint path with name '" + this.name + "'");
		}
	}

	// Token: 0x06000907 RID: 2311 RVA: 0x00043B34 File Offset: 0x00041D34
	public void AddNextPath(WaypointPath aWaypointPath)
	{
		this.nextPaths.Add(aWaypointPath);
	}

	// Token: 0x06000908 RID: 2312 RVA: 0x00043B44 File Offset: 0x00041D44
	public void DrawGizmos()
	{
		Gizmos.color = Color.cyan;
		foreach (WaypointData waypointData in this.waypoints)
		{
			Gizmos.DrawSphere(waypointData.position, 2f);
		}
		Gizmos.color = Color.white;
		for (int i = 0; i < this.waypoints.Count - 1; i++)
		{
			Gizmos.DrawLine(this.waypoints[i].position, this.waypoints[i + 1].position);
		}
		Gizmos.color = Color.blue;
		foreach (WaypointPath waypointPath in this.nextPaths)
		{
			Gizmos.DrawLine(this.waypoints[this.waypoints.Count - 1].position, waypointPath.waypoints[0].position);
		}
	}

	// Token: 0x06000909 RID: 2313 RVA: 0x00043C9C File Offset: 0x00041E9C
	private void SwapWaypointsByIndex(int tA, int tB)
	{
		WaypointData value = this.waypoints[tA];
		this.waypoints[tA] = this.waypoints[tB];
		this.waypoints[tB] = value;
	}

	// Token: 0x04000949 RID: 2377
	public string name;

	// Token: 0x0400094A RID: 2378
	public BlockData blockData;

	// Token: 0x0400094B RID: 2379
	public List<WaypointData> waypoints;

	// Token: 0x0400094C RID: 2380
	public List<WaypointPath> nextPaths;
}
