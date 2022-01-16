using System;
using UnityEngine;

// Token: 0x02000059 RID: 89
[AddComponentMenu("NGUI/Internal/Spring Panel")]
[RequireComponent(typeof(UIPanel))]
public class SpringPanel : IgnoreTimeScale
{
	// Token: 0x060002EA RID: 746 RVA: 0x00013A10 File Offset: 0x00011C10
	private void Start()
	{
		this.mPanel = base.GetComponent<UIPanel>();
		this.mDrag = base.GetComponent<UIDraggablePanel>();
		this.mTrans = base.transform;
	}

	// Token: 0x060002EB RID: 747 RVA: 0x00013A44 File Offset: 0x00011C44
	private void Update()
	{
		float deltaTime = base.UpdateRealTimeDelta();
		if (this.mThreshold == 0f)
		{
			this.mThreshold = (this.target - this.mTrans.localPosition).magnitude * 0.005f;
		}
		bool flag = false;
		Vector3 localPosition = this.mTrans.localPosition;
		Vector3 vector = NGUIMath.SpringLerp(this.mTrans.localPosition, this.target, this.strength, deltaTime);
		if (this.mThreshold >= Vector3.Magnitude(vector - this.target))
		{
			vector = this.target;
			base.enabled = false;
			flag = true;
		}
		this.mTrans.localPosition = vector;
		Vector3 vector2 = vector - localPosition;
		Vector4 clipRange = this.mPanel.clipRange;
		clipRange.x -= vector2.x;
		clipRange.y -= vector2.y;
		this.mPanel.clipRange = clipRange;
		if (this.mDrag != null)
		{
			this.mDrag.UpdateScrollbars(false);
		}
		if (flag && this.onFinished != null)
		{
			this.onFinished();
		}
	}

	// Token: 0x060002EC RID: 748 RVA: 0x00013B80 File Offset: 0x00011D80
	public static SpringPanel Begin(GameObject go, Vector3 pos, float strength)
	{
		SpringPanel springPanel = go.GetComponent<SpringPanel>();
		if (springPanel == null)
		{
			springPanel = go.AddComponent<SpringPanel>();
		}
		springPanel.target = pos;
		springPanel.strength = strength;
		springPanel.onFinished = null;
		if (!springPanel.enabled)
		{
			springPanel.mThreshold = 0f;
			springPanel.enabled = true;
		}
		return springPanel;
	}

	// Token: 0x04000308 RID: 776
	public Vector3 target = Vector3.zero;

	// Token: 0x04000309 RID: 777
	public float strength = 10f;

	// Token: 0x0400030A RID: 778
	public SpringPanel.OnFinished onFinished;

	// Token: 0x0400030B RID: 779
	private UIPanel mPanel;

	// Token: 0x0400030C RID: 780
	private Transform mTrans;

	// Token: 0x0400030D RID: 781
	private float mThreshold;

	// Token: 0x0400030E RID: 782
	private UIDraggablePanel mDrag;

	// Token: 0x020001E7 RID: 487
	// (Invoke) Token: 0x06000DB9 RID: 3513
	public delegate void OnFinished();
}
