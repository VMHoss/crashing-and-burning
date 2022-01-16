using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000F0 RID: 240
public class GridManager
{
	// Token: 0x0600072D RID: 1837 RVA: 0x00035DB0 File Offset: 0x00033FB0
	public GridManager(int aGridEntryWidth, int aGridEntryheight, List<GridEntry> aGridEntryList)
	{
		this.pGridEntryWidth = aGridEntryWidth;
		this.pGridEntryHeight = aGridEntryheight;
		this.pGridEntryWidthInv = 1f / (float)this.pGridEntryWidth;
		this.pGridEntryHeightInv = 1f / (float)this.pGridEntryHeight;
		this.UPDATE_DIST = ((!Data.highDetails) ? 1 : Data.Shared["GridSystem"].d["BlockShowDist"].i);
		Vector3 zero = Vector3.zero;
		foreach (GridEntry gridEntry in aGridEntryList)
		{
			Vector3 worldPos = gridEntry.GetWorldPos();
			if (Mathf.Abs(worldPos.x) > zero.x)
			{
				zero.x = Mathf.Abs(worldPos.x);
			}
			if (Mathf.Abs(worldPos.z) > zero.z)
			{
				zero.z = Mathf.Abs(worldPos.z);
			}
		}
		this.pGridWidthSize = Mathf.CeilToInt(zero.x * this.pGridEntryWidthInv) + 1;
		this.pGridHeightSize = Mathf.CeilToInt(zero.z * this.pGridEntryHeightInv) + 1;
		this.pGrid = new GridEntry[this.pGridWidthSize, this.pGridHeightSize];
		foreach (GridEntry gridEntry2 in aGridEntryList)
		{
			Vector3 worldPos = gridEntry2.GetWorldPos();
			int num = Mathf.CeilToInt(Mathf.Abs(worldPos.x) * this.pGridEntryWidthInv);
			int num2 = Mathf.CeilToInt(Mathf.Abs(worldPos.z) * this.pGridEntryHeightInv);
			if (this.pGrid[num, num2] == null)
			{
				this.pGrid[num, num2] = gridEntry2;
				gridEntry2.SetPos(num, num2);
			}
			else
			{
				Debug.LogError(string.Concat(new object[]
				{
					"Grid at coordinate (",
					num,
					", ",
					num2,
					") is already filled."
				}));
			}
		}
		this.pUpdateGridEntries = new List<GridEntry>();
	}

	// Token: 0x0600072E RID: 1838 RVA: 0x00036038 File Offset: 0x00034238
	public void Reset()
	{
		for (int i = 0; i < this.pGridWidthSize; i++)
		{
			for (int j = 0; j < this.pGridHeightSize; j++)
			{
				if (this.pGrid[i, j] != null)
				{
					this.pGrid[i, j].UpdateDistanceFromCenterBlock(-1);
				}
			}
		}
		foreach (GridEntry gridEntry in this.pUpdateGridEntries)
		{
			gridEntry.Updated = false;
		}
		this.pUpdateGridEntries.Clear();
	}

	// Token: 0x0600072F RID: 1839 RVA: 0x000360FC File Offset: 0x000342FC
	public void Init(GameObject aFollowGO)
	{
		this.Reset();
		this.pFollowGO = aFollowGO;
		this.ComputeFollowGridCoord(out this.pFollowGridX, out this.pFollowGridY);
		for (int i = -this.UPDATE_DIST; i <= this.UPDATE_DIST; i++)
		{
			for (int j = -this.UPDATE_DIST; j <= this.UPDATE_DIST; j++)
			{
				this.SetGridEntry(this.pFollowGridX + i, this.pFollowGridY + j, Mathf.Max(Mathf.Abs(i), Mathf.Abs(j)));
			}
		}
	}

	// Token: 0x06000730 RID: 1840 RVA: 0x0003618C File Offset: 0x0003438C
	public void Update()
	{
		if (this.pFollowGO == null)
		{
			return;
		}
		foreach (GridEntry gridEntry in this.pUpdateGridEntries)
		{
			gridEntry.Update();
		}
		int num;
		int num2;
		this.ComputeFollowGridCoord(out num, out num2);
		if (num != this.pFollowGridX)
		{
			if (num > this.pFollowGridX)
			{
				for (int i = -this.UPDATE_DIST; i <= this.UPDATE_DIST; i++)
				{
					this.SetGridEntry(this.pFollowGridX - this.UPDATE_DIST, this.pFollowGridY + i, -1);
				}
				this.pFollowGridX++;
				for (int j = -this.UPDATE_DIST; j <= this.UPDATE_DIST; j++)
				{
					this.SetGridEntry(this.pFollowGridX + this.UPDATE_DIST, this.pFollowGridY + j, this.UPDATE_DIST);
				}
				for (int k = -this.UPDATE_DIST; k <= this.UPDATE_DIST - 1; k++)
				{
					for (int l = -this.UPDATE_DIST; l <= this.UPDATE_DIST; l++)
					{
						this.SetGridEntry(this.pFollowGridX + k, this.pFollowGridY + l, Mathf.Max(Mathf.Abs(k), Mathf.Abs(l)));
					}
				}
			}
			else
			{
				for (int m = -this.UPDATE_DIST; m <= this.UPDATE_DIST; m++)
				{
					this.SetGridEntry(this.pFollowGridX + this.UPDATE_DIST, this.pFollowGridY + m, -1);
				}
				this.pFollowGridX--;
				for (int n = -this.UPDATE_DIST; n <= this.UPDATE_DIST; n++)
				{
					this.SetGridEntry(this.pFollowGridX - this.UPDATE_DIST, this.pFollowGridY + n, this.UPDATE_DIST);
				}
				for (int num3 = -this.UPDATE_DIST + 1; num3 <= this.UPDATE_DIST; num3++)
				{
					for (int num4 = -this.UPDATE_DIST; num4 <= this.UPDATE_DIST; num4++)
					{
						this.SetGridEntry(this.pFollowGridX + num3, this.pFollowGridY + num4, Mathf.Max(Mathf.Abs(num3), Mathf.Abs(num4)));
					}
				}
			}
		}
		else if (num2 != this.pFollowGridY)
		{
			if (num2 > this.pFollowGridY)
			{
				for (int num5 = -this.UPDATE_DIST; num5 <= this.UPDATE_DIST; num5++)
				{
					this.SetGridEntry(this.pFollowGridX + num5, this.pFollowGridY - this.UPDATE_DIST, -1);
				}
				this.pFollowGridY++;
				for (int num6 = -this.UPDATE_DIST; num6 <= this.UPDATE_DIST; num6++)
				{
					this.SetGridEntry(this.pFollowGridX + num6, this.pFollowGridY + this.UPDATE_DIST, this.UPDATE_DIST);
				}
				for (int num7 = -this.UPDATE_DIST; num7 <= this.UPDATE_DIST; num7++)
				{
					for (int num8 = -this.UPDATE_DIST; num8 <= this.UPDATE_DIST - 1; num8++)
					{
						this.SetGridEntry(this.pFollowGridX + num7, this.pFollowGridY + num8, Mathf.Max(Mathf.Abs(num7), Mathf.Abs(num8)));
					}
				}
			}
			else
			{
				for (int num9 = -this.UPDATE_DIST; num9 <= this.UPDATE_DIST; num9++)
				{
					this.SetGridEntry(this.pFollowGridX + num9, this.pFollowGridY + this.UPDATE_DIST, -1);
				}
				this.pFollowGridY--;
				for (int num10 = -this.UPDATE_DIST; num10 <= this.UPDATE_DIST; num10++)
				{
					this.SetGridEntry(this.pFollowGridX + num10, this.pFollowGridY - this.UPDATE_DIST, this.UPDATE_DIST);
				}
				for (int num11 = -this.UPDATE_DIST; num11 <= this.UPDATE_DIST; num11++)
				{
					for (int num12 = -this.UPDATE_DIST + 1; num12 <= this.UPDATE_DIST; num12++)
					{
						this.SetGridEntry(this.pFollowGridX + num11, this.pFollowGridY + num12, Mathf.Max(Mathf.Abs(num11), Mathf.Abs(num12)));
					}
				}
			}
		}
	}

	// Token: 0x06000731 RID: 1841 RVA: 0x00036644 File Offset: 0x00034844
	public GridEntry GetGridEntry(int aGridPosX, int aGridPosY)
	{
		if (aGridPosX <= 0 || aGridPosX >= this.pGridWidthSize)
		{
			return null;
		}
		if (aGridPosY <= 0 || aGridPosY >= this.pGridHeightSize)
		{
			return null;
		}
		return this.pGrid[aGridPosX, aGridPosY];
	}

	// Token: 0x06000732 RID: 1842 RVA: 0x00036680 File Offset: 0x00034880
	public GridEntry GetGridEntryFromPoint(Vector3 aPosition)
	{
		int aGridPosX = Mathf.CeilToInt(Mathf.Abs(aPosition.x) * this.pGridEntryWidthInv);
		int aGridPosY = Mathf.CeilToInt(Mathf.Abs(aPosition.z) * this.pGridEntryHeightInv);
		return this.GetGridEntry(aGridPosX, aGridPosY);
	}

	// Token: 0x06000733 RID: 1843 RVA: 0x000366C8 File Offset: 0x000348C8
	public List<GridEntry> GetGridEntriesFromRectangle(Vector3 aStartRectPos, Vector3 anEndRectPos)
	{
		Vector3 zero = Vector3.zero;
		Vector3 zero2 = Vector3.zero;
		if (aStartRectPos.x < 0f)
		{
			aStartRectPos.x = -aStartRectPos.x;
		}
		if (aStartRectPos.z < 0f)
		{
			aStartRectPos.z = -aStartRectPos.z;
		}
		if (anEndRectPos.x < 0f)
		{
			anEndRectPos.x = -anEndRectPos.x;
		}
		if (anEndRectPos.z < 0f)
		{
			anEndRectPos.z = -anEndRectPos.z;
		}
		if (aStartRectPos.x < anEndRectPos.x)
		{
			zero.x = aStartRectPos.x;
			zero2.x = anEndRectPos.x;
		}
		else
		{
			zero.x = anEndRectPos.x;
			zero2.x = aStartRectPos.x;
		}
		if (aStartRectPos.z < anEndRectPos.z)
		{
			zero.z = aStartRectPos.z;
			zero2.z = anEndRectPos.z;
		}
		else
		{
			zero.z = anEndRectPos.z;
			zero2.z = aStartRectPos.z;
		}
		int num;
		int num2;
		this.ComputeGridCoordFromPoint(zero, out num, out num2);
		int num3;
		int num4;
		this.ComputeGridCoordFromPoint(zero2, out num3, out num4);
		List<GridEntry> list = new List<GridEntry>();
		for (int i = num; i <= num3; i++)
		{
			for (int j = num2; j <= num4; j++)
			{
				GridEntry gridEntry = this.GetGridEntry(i, j);
				if (gridEntry != null)
				{
					list.Add(gridEntry);
				}
			}
		}
		return list;
	}

	// Token: 0x06000734 RID: 1844 RVA: 0x0003686C File Offset: 0x00034A6C
	private void ComputeFollowGridCoord(out int aCoordX, out int aCoordY)
	{
		Vector3 position = this.pFollowGO.transform.position;
		aCoordX = Mathf.CeilToInt(Mathf.Abs(position.x) * this.pGridEntryWidthInv);
		aCoordY = Mathf.CeilToInt(Mathf.Abs(position.z) * this.pGridEntryHeightInv);
	}

	// Token: 0x06000735 RID: 1845 RVA: 0x000368C0 File Offset: 0x00034AC0
	private void ComputeGridCoordFromPoint(Vector3 aPoint, out int aCoordX, out int aCoordY)
	{
		aCoordX = Mathf.CeilToInt(Mathf.Abs(aPoint.x) * this.pGridEntryWidthInv);
		aCoordY = Mathf.CeilToInt(Mathf.Abs(aPoint.z) * this.pGridEntryHeightInv);
	}

	// Token: 0x06000736 RID: 1846 RVA: 0x00036904 File Offset: 0x00034B04
	private void SetGridEntry(int aGridPosX, int aGridPosY, int aDist)
	{
		if (aGridPosX <= 0 || aGridPosX >= this.pGridWidthSize)
		{
			return;
		}
		if (aGridPosY <= 0 || aGridPosY >= this.pGridHeightSize)
		{
			return;
		}
		GridEntry gridEntry = this.pGrid[aGridPosX, aGridPosY];
		if (gridEntry == null)
		{
			return;
		}
		gridEntry.UpdateDistanceFromCenterBlock(aDist);
		if (aDist >= 0)
		{
			if (!gridEntry.Updated)
			{
				gridEntry.Updated = true;
				this.pUpdateGridEntries.Add(gridEntry);
			}
		}
		else if (gridEntry.Updated)
		{
			gridEntry.Updated = false;
			this.pUpdateGridEntries.Remove(gridEntry);
		}
	}

	// Token: 0x040007B9 RID: 1977
	private GridEntry[,] pGrid;

	// Token: 0x040007BA RID: 1978
	private int pGridWidthSize;

	// Token: 0x040007BB RID: 1979
	private int pGridHeightSize;

	// Token: 0x040007BC RID: 1980
	private int pGridEntryWidth;

	// Token: 0x040007BD RID: 1981
	private int pGridEntryHeight;

	// Token: 0x040007BE RID: 1982
	private float pGridEntryWidthInv;

	// Token: 0x040007BF RID: 1983
	private float pGridEntryHeightInv;

	// Token: 0x040007C0 RID: 1984
	private GameObject pFollowGO;

	// Token: 0x040007C1 RID: 1985
	private int pFollowGridX;

	// Token: 0x040007C2 RID: 1986
	private int pFollowGridY;

	// Token: 0x040007C3 RID: 1987
	public int UPDATE_DIST = 2;

	// Token: 0x040007C4 RID: 1988
	private List<GridEntry> pUpdateGridEntries;
}
