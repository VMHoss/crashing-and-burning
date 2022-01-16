using System;
using UnityEngine;

// Token: 0x020000EF RID: 239
public abstract class GridEntry
{
	// Token: 0x06000725 RID: 1829 RVA: 0x00035D68 File Offset: 0x00033F68
	public void SetPos(int aGridPosX, int aGridPosY)
	{
		this.pGridPosX = aGridPosX;
		this.pGridPosY = aGridPosY;
	}

	// Token: 0x06000726 RID: 1830 RVA: 0x00035D78 File Offset: 0x00033F78
	public int GetGridPosX()
	{
		return this.pGridPosX;
	}

	// Token: 0x06000727 RID: 1831 RVA: 0x00035D80 File Offset: 0x00033F80
	public int GetGridPosY()
	{
		return this.pGridPosY;
	}

	// Token: 0x06000728 RID: 1832 RVA: 0x00035D88 File Offset: 0x00033F88
	public void UpdateDistanceFromCenterBlock(int aDistFromCenterBlock)
	{
		if (this.pDistFromCenterBlock != aDistFromCenterBlock)
		{
			this.pDistFromCenterBlock = aDistFromCenterBlock;
			this.DistanceFromCenterBlockUpdated();
		}
	}

	// Token: 0x06000729 RID: 1833
	public abstract Vector3 GetWorldPos();

	// Token: 0x0600072A RID: 1834 RVA: 0x00035DA4 File Offset: 0x00033FA4
	public virtual void Update()
	{
	}

	// Token: 0x0600072B RID: 1835 RVA: 0x00035DA8 File Offset: 0x00033FA8
	protected virtual void DistanceFromCenterBlockUpdated()
	{
	}

	// Token: 0x0600072C RID: 1836 RVA: 0x00035DAC File Offset: 0x00033FAC
	public virtual bool IsActive()
	{
		return false;
	}

	// Token: 0x040007B4 RID: 1972
	public bool Updated;

	// Token: 0x040007B5 RID: 1973
	protected int pGridPosX;

	// Token: 0x040007B6 RID: 1974
	protected int pGridPosY;

	// Token: 0x040007B7 RID: 1975
	protected bool pActive = true;

	// Token: 0x040007B8 RID: 1976
	protected int pDistFromCenterBlock = -2;
}
