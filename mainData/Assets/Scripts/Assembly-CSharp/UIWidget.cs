using System;
using UnityEngine;

// Token: 0x0200005F RID: 95
public abstract class UIWidget : MonoBehaviour
{
	// Token: 0x1700005A RID: 90
	// (get) Token: 0x06000319 RID: 793 RVA: 0x00014C18 File Offset: 0x00012E18
	public bool isVisible
	{
		get
		{
			return this.finalAlpha > 0.001f;
		}
	}

	// Token: 0x1700005B RID: 91
	// (get) Token: 0x0600031A RID: 794 RVA: 0x00014C28 File Offset: 0x00012E28
	// (set) Token: 0x0600031B RID: 795 RVA: 0x00014C30 File Offset: 0x00012E30
	public Color color
	{
		get
		{
			return this.mColor;
		}
		set
		{
			if (!this.mColor.Equals(value))
			{
				this.mColor = value;
				this.mChanged = true;
			}
		}
	}

	// Token: 0x1700005C RID: 92
	// (get) Token: 0x0600031C RID: 796 RVA: 0x00014C64 File Offset: 0x00012E64
	// (set) Token: 0x0600031D RID: 797 RVA: 0x00014C74 File Offset: 0x00012E74
	public float alpha
	{
		get
		{
			return this.mColor.a;
		}
		set
		{
			Color color = this.mColor;
			color.a = value;
			this.color = color;
		}
	}

	// Token: 0x1700005D RID: 93
	// (get) Token: 0x0600031E RID: 798 RVA: 0x00014C98 File Offset: 0x00012E98
	public float finalAlpha
	{
		get
		{
			if (this.mPanel == null)
			{
				this.CreatePanel();
			}
			return (!(this.mPanel != null)) ? this.mColor.a : (this.mColor.a * this.mPanel.alpha);
		}
	}

	// Token: 0x1700005E RID: 94
	// (get) Token: 0x0600031F RID: 799 RVA: 0x00014CF4 File Offset: 0x00012EF4
	// (set) Token: 0x06000320 RID: 800 RVA: 0x00014CFC File Offset: 0x00012EFC
	public UIWidget.Pivot pivot
	{
		get
		{
			return this.mPivot;
		}
		set
		{
			if (this.mPivot != value)
			{
				Vector3 vector = NGUIMath.CalculateWidgetCorners(this)[0];
				this.mPivot = value;
				this.mChanged = true;
				Vector3 vector2 = NGUIMath.CalculateWidgetCorners(this)[0];
				Transform cachedTransform = this.cachedTransform;
				Vector3 vector3 = cachedTransform.position;
				float z = cachedTransform.localPosition.z;
				vector3.x += vector.x - vector2.x;
				vector3.y += vector.y - vector2.y;
				this.cachedTransform.position = vector3;
				vector3 = this.cachedTransform.localPosition;
				vector3.x = Mathf.Round(vector3.x);
				vector3.y = Mathf.Round(vector3.y);
				vector3.z = z;
				this.cachedTransform.localPosition = vector3;
			}
		}
	}

	// Token: 0x1700005F RID: 95
	// (get) Token: 0x06000321 RID: 801 RVA: 0x00014DF4 File Offset: 0x00012FF4
	// (set) Token: 0x06000322 RID: 802 RVA: 0x00014DFC File Offset: 0x00012FFC
	public int depth
	{
		get
		{
			return this.mDepth;
		}
		set
		{
			if (this.mDepth != value)
			{
				this.mDepth = value;
				if (this.mPanel != null)
				{
					this.mPanel.MarkMaterialAsChanged(this.material, true);
				}
			}
		}
	}

	// Token: 0x17000060 RID: 96
	// (get) Token: 0x06000323 RID: 803 RVA: 0x00014E40 File Offset: 0x00013040
	public Vector2 pivotOffset
	{
		get
		{
			Vector2 zero = Vector2.zero;
			Vector4 relativePadding = this.relativePadding;
			UIWidget.Pivot pivot = this.pivot;
			if (pivot == UIWidget.Pivot.Top || pivot == UIWidget.Pivot.Center || pivot == UIWidget.Pivot.Bottom)
			{
				zero.x = (relativePadding.x - relativePadding.z - 1f) * 0.5f;
			}
			else if (pivot == UIWidget.Pivot.TopRight || pivot == UIWidget.Pivot.Right || pivot == UIWidget.Pivot.BottomRight)
			{
				zero.x = -1f - relativePadding.z;
			}
			else
			{
				zero.x = relativePadding.x;
			}
			if (pivot == UIWidget.Pivot.Left || pivot == UIWidget.Pivot.Center || pivot == UIWidget.Pivot.Right)
			{
				zero.y = (relativePadding.w - relativePadding.y + 1f) * 0.5f;
			}
			else if (pivot == UIWidget.Pivot.BottomLeft || pivot == UIWidget.Pivot.Bottom || pivot == UIWidget.Pivot.BottomRight)
			{
				zero.y = 1f + relativePadding.w;
			}
			else
			{
				zero.y = -relativePadding.y;
			}
			return zero;
		}
	}

	// Token: 0x17000061 RID: 97
	// (get) Token: 0x06000324 RID: 804 RVA: 0x00014F54 File Offset: 0x00013154
	public Transform cachedTransform
	{
		get
		{
			if (this.mTrans == null)
			{
				this.mTrans = base.transform;
			}
			return this.mTrans;
		}
	}

	// Token: 0x17000062 RID: 98
	// (get) Token: 0x06000325 RID: 805 RVA: 0x00014F7C File Offset: 0x0001317C
	// (set) Token: 0x06000326 RID: 806 RVA: 0x00014F84 File Offset: 0x00013184
	public virtual Material material
	{
		get
		{
			return this.mMat;
		}
		set
		{
			if (this.mMat != value)
			{
				if (this.mMat != null && this.mPanel != null)
				{
					this.mPanel.RemoveWidget(this);
				}
				this.mPanel = null;
				this.mMat = value;
				this.mTex = null;
				if (this.mMat != null)
				{
					this.CreatePanel();
				}
			}
		}
	}

	// Token: 0x17000063 RID: 99
	// (get) Token: 0x06000327 RID: 807 RVA: 0x00014FFC File Offset: 0x000131FC
	// (set) Token: 0x06000328 RID: 808 RVA: 0x0001509C File Offset: 0x0001329C
	public virtual Texture mainTexture
	{
		get
		{
			Material material = this.material;
			if (material != null)
			{
				if (material.mainTexture != null)
				{
					this.mTex = material.mainTexture;
				}
				else if (this.mTex != null)
				{
					if (this.mPanel != null)
					{
						this.mPanel.RemoveWidget(this);
					}
					this.mPanel = null;
					this.mMat.mainTexture = this.mTex;
					if (base.enabled)
					{
						this.CreatePanel();
					}
				}
			}
			return this.mTex;
		}
		set
		{
			Material material = this.material;
			if (material == null || material.mainTexture != value)
			{
				if (this.mPanel != null)
				{
					this.mPanel.RemoveWidget(this);
				}
				this.mPanel = null;
				this.mTex = value;
				material = this.material;
				if (material != null)
				{
					material.mainTexture = value;
					if (base.enabled)
					{
						this.CreatePanel();
					}
				}
			}
		}
	}

	// Token: 0x17000064 RID: 100
	// (get) Token: 0x06000329 RID: 809 RVA: 0x00015124 File Offset: 0x00013324
	// (set) Token: 0x0600032A RID: 810 RVA: 0x00015144 File Offset: 0x00013344
	public UIPanel panel
	{
		get
		{
			if (this.mPanel == null)
			{
				this.CreatePanel();
			}
			return this.mPanel;
		}
		set
		{
			this.mPanel = value;
		}
	}

	// Token: 0x17000065 RID: 101
	// (get) Token: 0x0600032B RID: 811 RVA: 0x00015150 File Offset: 0x00013350
	// (set) Token: 0x0600032C RID: 812 RVA: 0x00015158 File Offset: 0x00013358
	public int visibleFlag
	{
		get
		{
			return this.mVisibleFlag;
		}
		set
		{
			this.mVisibleFlag = value;
		}
	}

	// Token: 0x0600032D RID: 813 RVA: 0x00015164 File Offset: 0x00013364
	public static int CompareFunc(UIWidget left, UIWidget right)
	{
		if (left.mDepth > right.mDepth)
		{
			return 1;
		}
		if (left.mDepth < right.mDepth)
		{
			return -1;
		}
		return 0;
	}

	// Token: 0x0600032E RID: 814 RVA: 0x00015190 File Offset: 0x00013390
	public void MarkAsChangedLite()
	{
		this.mChanged = true;
	}

	// Token: 0x0600032F RID: 815 RVA: 0x0001519C File Offset: 0x0001339C
	public virtual void MarkAsChanged()
	{
		this.mChanged = true;
		if (this.mPanel != null && base.enabled && NGUITools.GetActive(base.gameObject) && !Application.isPlaying && this.material != null)
		{
			this.mPanel.AddWidget(this);
			this.CheckLayer();
		}
	}

	// Token: 0x06000330 RID: 816 RVA: 0x0001520C File Offset: 0x0001340C
	public void CreatePanel()
	{
		if (this.mPanel == null && base.enabled && NGUITools.GetActive(base.gameObject) && this.material != null)
		{
			this.mPanel = UIPanel.Find(this.cachedTransform);
			if (this.mPanel != null)
			{
				this.CheckLayer();
				this.mPanel.AddWidget(this);
				this.mChanged = true;
			}
		}
	}

	// Token: 0x06000331 RID: 817 RVA: 0x00015294 File Offset: 0x00013494
	public void CheckLayer()
	{
		if (this.mPanel != null && this.mPanel.gameObject.layer != base.gameObject.layer)
		{
			Debug.LogWarning("You can't place widgets on a layer different than the UIPanel that manages them.\nIf you want to move widgets to a different layer, parent them to a new panel instead.", this);
			base.gameObject.layer = this.mPanel.gameObject.layer;
		}
	}

	// Token: 0x06000332 RID: 818 RVA: 0x000152F8 File Offset: 0x000134F8
	[Obsolete("Use ParentHasChanged() instead")]
	public void CheckParent()
	{
		this.ParentHasChanged();
	}

	// Token: 0x06000333 RID: 819 RVA: 0x00015300 File Offset: 0x00013500
	public void ParentHasChanged()
	{
		if (this.mPanel != null)
		{
			bool flag = true;
			Transform parent = this.cachedTransform.parent;
			while (parent != null)
			{
				if (parent == this.mPanel.cachedTransform)
				{
					break;
				}
				if (!this.mPanel.WatchesTransform(parent))
				{
					flag = false;
					break;
				}
				parent = parent.parent;
			}
			if (!flag)
			{
				if (!this.keepMaterial || Application.isPlaying)
				{
					this.material = null;
				}
				this.mPanel = null;
				this.CreatePanel();
			}
		}
	}

	// Token: 0x06000334 RID: 820 RVA: 0x000153A8 File Offset: 0x000135A8
	protected virtual void Awake()
	{
		this.mGo = base.gameObject;
		this.mPlayMode = Application.isPlaying;
	}

	// Token: 0x06000335 RID: 821 RVA: 0x000153C4 File Offset: 0x000135C4
	protected virtual void OnEnable()
	{
		this.mChanged = true;
		if (!this.keepMaterial)
		{
			this.mMat = null;
			this.mTex = null;
		}
		this.mPanel = null;
	}

	// Token: 0x06000336 RID: 822 RVA: 0x000153F0 File Offset: 0x000135F0
	private void Start()
	{
		this.OnStart();
		this.CreatePanel();
	}

	// Token: 0x06000337 RID: 823 RVA: 0x00015400 File Offset: 0x00013600
	public void Update()
	{
		this.CheckLayer();
		if (this.mPanel == null)
		{
			this.CreatePanel();
		}
		Vector3 localScale = this.cachedTransform.localScale;
		if (localScale.z != 1f)
		{
			localScale.z = 1f;
			this.mTrans.localScale = localScale;
		}
	}

	// Token: 0x06000338 RID: 824 RVA: 0x00015460 File Offset: 0x00013660
	private void OnDisable()
	{
		if (!this.keepMaterial)
		{
			this.material = null;
		}
		else if (this.mPanel != null)
		{
			this.mPanel.RemoveWidget(this);
		}
		this.mPanel = null;
	}

	// Token: 0x06000339 RID: 825 RVA: 0x000154A8 File Offset: 0x000136A8
	private void OnDestroy()
	{
		if (this.mPanel != null)
		{
			this.mPanel.RemoveWidget(this);
			this.mPanel = null;
		}
	}

	// Token: 0x0600033A RID: 826 RVA: 0x000154DC File Offset: 0x000136DC
	public bool UpdateGeometry(UIPanel p, ref Matrix4x4 worldToPanel, bool parentMoved, bool generateNormals)
	{
		if (this.material == null)
		{
			return false;
		}
		if (!this.OnUpdate() && !this.mChanged)
		{
			if (this.mGeom.hasVertices && parentMoved)
			{
				this.mGeom.ApplyTransform(worldToPanel * this.cachedTransform.localToWorldMatrix, generateNormals);
			}
			return false;
		}
		this.mChanged = false;
		if (NGUITools.GetActive(this.mGo))
		{
			this.mPanel = p;
			this.mGeom.Clear();
			this.OnFill(this.mGeom.verts, this.mGeom.uvs, this.mGeom.cols);
			if (this.mGeom.hasVertices)
			{
				Vector3 pivotOffset = this.pivotOffset;
				Vector2 relativeSize = this.relativeSize;
				pivotOffset.x *= relativeSize.x;
				pivotOffset.y *= relativeSize.y;
				this.mGeom.ApplyOffset(pivotOffset);
				this.mGeom.ApplyTransform(worldToPanel * this.cachedTransform.localToWorldMatrix, generateNormals);
			}
			return true;
		}
		if (this.mGeom.hasVertices)
		{
			this.mGeom.Clear();
			return true;
		}
		return false;
	}

	// Token: 0x0600033B RID: 827 RVA: 0x0001563C File Offset: 0x0001383C
	public void WriteToBuffers(BetterList<Vector3> v, BetterList<Vector2> u, BetterList<Color32> c, BetterList<Vector3> n, BetterList<Vector4> t)
	{
		this.mGeom.WriteToBuffers(v, u, c, n, t);
	}

	// Token: 0x0600033C RID: 828 RVA: 0x00015650 File Offset: 0x00013850
	public virtual void MakePixelPerfect()
	{
		Vector3 localScale = this.cachedTransform.localScale;
		int num = Mathf.RoundToInt(localScale.x);
		int num2 = Mathf.RoundToInt(localScale.y);
		localScale.x = (float)num;
		localScale.y = (float)num2;
		localScale.z = 1f;
		Vector3 localPosition = this.cachedTransform.localPosition;
		localPosition.z = (float)Mathf.RoundToInt(localPosition.z);
		if (num % 2 == 1 && (this.pivot == UIWidget.Pivot.Top || this.pivot == UIWidget.Pivot.Center || this.pivot == UIWidget.Pivot.Bottom))
		{
			localPosition.x = Mathf.Floor(localPosition.x) + 0.5f;
		}
		else
		{
			localPosition.x = Mathf.Round(localPosition.x);
		}
		if (num2 % 2 == 1 && (this.pivot == UIWidget.Pivot.Left || this.pivot == UIWidget.Pivot.Center || this.pivot == UIWidget.Pivot.Right))
		{
			localPosition.y = Mathf.Ceil(localPosition.y) - 0.5f;
		}
		else
		{
			localPosition.y = Mathf.Round(localPosition.y);
		}
		this.cachedTransform.localPosition = localPosition;
		this.cachedTransform.localScale = localScale;
	}

	// Token: 0x17000066 RID: 102
	// (get) Token: 0x0600033D RID: 829 RVA: 0x00015798 File Offset: 0x00013998
	public virtual Vector2 relativeSize
	{
		get
		{
			return Vector2.one;
		}
	}

	// Token: 0x17000067 RID: 103
	// (get) Token: 0x0600033E RID: 830 RVA: 0x000157A0 File Offset: 0x000139A0
	public virtual Vector4 relativePadding
	{
		get
		{
			return Vector4.zero;
		}
	}

	// Token: 0x17000068 RID: 104
	// (get) Token: 0x0600033F RID: 831 RVA: 0x000157A8 File Offset: 0x000139A8
	public virtual Vector4 border
	{
		get
		{
			return Vector4.zero;
		}
	}

	// Token: 0x17000069 RID: 105
	// (get) Token: 0x06000340 RID: 832 RVA: 0x000157B0 File Offset: 0x000139B0
	public virtual bool keepMaterial
	{
		get
		{
			return false;
		}
	}

	// Token: 0x1700006A RID: 106
	// (get) Token: 0x06000341 RID: 833 RVA: 0x000157B4 File Offset: 0x000139B4
	public virtual bool pixelPerfectAfterResize
	{
		get
		{
			return false;
		}
	}

	// Token: 0x06000342 RID: 834 RVA: 0x000157B8 File Offset: 0x000139B8
	protected virtual void OnStart()
	{
	}

	// Token: 0x06000343 RID: 835 RVA: 0x000157BC File Offset: 0x000139BC
	public virtual bool OnUpdate()
	{
		return false;
	}

	// Token: 0x06000344 RID: 836 RVA: 0x000157C0 File Offset: 0x000139C0
	public virtual void OnFill(BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols)
	{
	}

	// Token: 0x0400033F RID: 831
	[HideInInspector]
	[SerializeField]
	protected Material mMat;

	// Token: 0x04000340 RID: 832
	[HideInInspector]
	[SerializeField]
	protected Texture mTex;

	// Token: 0x04000341 RID: 833
	[SerializeField]
	[HideInInspector]
	private Color mColor = Color.white;

	// Token: 0x04000342 RID: 834
	[HideInInspector]
	[SerializeField]
	private UIWidget.Pivot mPivot = UIWidget.Pivot.Center;

	// Token: 0x04000343 RID: 835
	[SerializeField]
	[HideInInspector]
	private int mDepth;

	// Token: 0x04000344 RID: 836
	protected Transform mTrans;

	// Token: 0x04000345 RID: 837
	protected UIPanel mPanel;

	// Token: 0x04000346 RID: 838
	protected bool mChanged = true;

	// Token: 0x04000347 RID: 839
	protected bool mPlayMode = true;

	// Token: 0x04000348 RID: 840
	private GameObject mGo;

	// Token: 0x04000349 RID: 841
	private Vector3 mDiffPos;

	// Token: 0x0400034A RID: 842
	private Quaternion mDiffRot;

	// Token: 0x0400034B RID: 843
	private Vector3 mDiffScale;

	// Token: 0x0400034C RID: 844
	private int mVisibleFlag = -1;

	// Token: 0x0400034D RID: 845
	private UIGeometry mGeom = new UIGeometry();

	// Token: 0x02000060 RID: 96
	public enum Pivot
	{
		// Token: 0x0400034F RID: 847
		TopLeft,
		// Token: 0x04000350 RID: 848
		Top,
		// Token: 0x04000351 RID: 849
		TopRight,
		// Token: 0x04000352 RID: 850
		Left,
		// Token: 0x04000353 RID: 851
		Center,
		// Token: 0x04000354 RID: 852
		Right,
		// Token: 0x04000355 RID: 853
		BottomLeft,
		// Token: 0x04000356 RID: 854
		Bottom,
		// Token: 0x04000357 RID: 855
		BottomRight
	}
}
