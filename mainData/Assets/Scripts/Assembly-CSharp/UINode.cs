using System;
using UnityEngine;

// Token: 0x0200005E RID: 94
public class UINode
{
	// Token: 0x06000314 RID: 788 RVA: 0x000149BC File Offset: 0x00012BBC
	public UINode(Transform t)
	{
		this.trans = t;
		this.mLastPos = this.trans.localPosition;
		this.mLastRot = this.trans.localRotation;
		this.mLastScale = this.trans.localScale;
		this.mGo = t.gameObject;
	}

	// Token: 0x17000059 RID: 89
	// (get) Token: 0x06000315 RID: 789 RVA: 0x00014A24 File Offset: 0x00012C24
	// (set) Token: 0x06000316 RID: 790 RVA: 0x00014A50 File Offset: 0x00012C50
	public int visibleFlag
	{
		get
		{
			return (!(this.widget != null)) ? this.mVisibleFlag : this.widget.visibleFlag;
		}
		set
		{
			if (this.widget != null)
			{
				this.widget.visibleFlag = value;
			}
			else
			{
				this.mVisibleFlag = value;
			}
		}
	}

	// Token: 0x06000317 RID: 791 RVA: 0x00014A7C File Offset: 0x00012C7C
	public bool HasChanged()
	{
		bool flag = NGUITools.GetActive(this.mGo) && (this.widget == null || (this.widget.enabled && this.widget.isVisible));
		bool flag2 = this.mLastActive != flag;
		if (this.widget != null)
		{
			float finalAlpha = this.widget.finalAlpha;
			if (finalAlpha != this.mLastAlpha)
			{
				this.mLastAlpha = finalAlpha;
				flag2 = true;
			}
		}
		if (!flag2 && !this.trans.hasChanged)
		{
			return false;
		}
		this.trans.hasChanged = false;
		flag2 = true;
		if (flag2 || (flag && (this.mLastPos != this.trans.localPosition || this.mLastRot != this.trans.localRotation || this.mLastScale != this.trans.localScale)))
		{
			this.mLastActive = flag;
			this.mLastPos = this.trans.localPosition;
			this.mLastRot = this.trans.localRotation;
			this.mLastScale = this.trans.localScale;
			return true;
		}
		return flag2;
	}

	// Token: 0x04000335 RID: 821
	private int mVisibleFlag = -1;

	// Token: 0x04000336 RID: 822
	public Transform trans;

	// Token: 0x04000337 RID: 823
	public UIWidget widget;

	// Token: 0x04000338 RID: 824
	private bool mLastActive;

	// Token: 0x04000339 RID: 825
	private Vector3 mLastPos;

	// Token: 0x0400033A RID: 826
	private Quaternion mLastRot;

	// Token: 0x0400033B RID: 827
	private Vector3 mLastScale;

	// Token: 0x0400033C RID: 828
	private GameObject mGo;

	// Token: 0x0400033D RID: 829
	private float mLastAlpha;

	// Token: 0x0400033E RID: 830
	public int changeFlag = -1;
}
