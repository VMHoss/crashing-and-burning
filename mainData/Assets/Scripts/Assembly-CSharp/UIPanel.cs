using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

// Token: 0x02000087 RID: 135
[AddComponentMenu("NGUI/UI/Panel")]
[ExecuteInEditMode]
public class UIPanel : MonoBehaviour
{
	// Token: 0x170000B7 RID: 183
	// (get) Token: 0x0600045E RID: 1118 RVA: 0x0001E168 File Offset: 0x0001C368
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

	// Token: 0x170000B8 RID: 184
	// (get) Token: 0x0600045F RID: 1119 RVA: 0x0001E190 File Offset: 0x0001C390
	public bool changedLastFrame
	{
		get
		{
			return this.mChangedLastFrame;
		}
	}

	// Token: 0x170000B9 RID: 185
	// (get) Token: 0x06000460 RID: 1120 RVA: 0x0001E198 File Offset: 0x0001C398
	// (set) Token: 0x06000461 RID: 1121 RVA: 0x0001E1A0 File Offset: 0x0001C3A0
	public float alpha
	{
		get
		{
			return this.mAlpha;
		}
		set
		{
			float num = Mathf.Clamp01(value);
			if (this.mAlpha != num)
			{
				this.mAlpha = num;
				this.mCheckVisibility = true;
				for (int i = 0; i < this.mDrawCalls.size; i++)
				{
					UIDrawCall uidrawCall = this.mDrawCalls[i];
					this.MarkMaterialAsChanged(uidrawCall.material, false);
				}
				for (int j = 0; j < this.mWidgets.size; j++)
				{
					this.mWidgets[j].MarkAsChangedLite();
				}
			}
		}
	}

	// Token: 0x06000462 RID: 1122 RVA: 0x0001E234 File Offset: 0x0001C434
	public void SetAlphaRecursive(float val, bool rebuildList)
	{
		if (rebuildList || this.mChildPanels == null)
		{
			this.mChildPanels = base.GetComponentsInChildren<UIPanel>(true);
		}
		int i = 0;
		int num = this.mChildPanels.Length;
		while (i < num)
		{
			this.mChildPanels[i].alpha = val;
			i++;
		}
	}

	// Token: 0x170000BA RID: 186
	// (get) Token: 0x06000463 RID: 1123 RVA: 0x0001E288 File Offset: 0x0001C488
	// (set) Token: 0x06000464 RID: 1124 RVA: 0x0001E290 File Offset: 0x0001C490
	public UIPanel.DebugInfo debugInfo
	{
		get
		{
			return this.mDebugInfo;
		}
		set
		{
			if (this.mDebugInfo != value)
			{
				this.mDebugInfo = value;
				BetterList<UIDrawCall> drawCalls = this.drawCalls;
				HideFlags hideFlags = (this.mDebugInfo != UIPanel.DebugInfo.Geometry) ? HideFlags.HideAndDontSave : (HideFlags.DontSave | HideFlags.NotEditable);
				int i = 0;
				int size = drawCalls.size;
				while (i < size)
				{
					UIDrawCall uidrawCall = drawCalls[i];
					GameObject gameObject = uidrawCall.gameObject;
					NGUITools.SetActiveSelf(gameObject, false);
					gameObject.hideFlags = hideFlags;
					NGUITools.SetActiveSelf(gameObject, true);
					i++;
				}
			}
		}
	}

	// Token: 0x170000BB RID: 187
	// (get) Token: 0x06000465 RID: 1125 RVA: 0x0001E310 File Offset: 0x0001C510
	// (set) Token: 0x06000466 RID: 1126 RVA: 0x0001E318 File Offset: 0x0001C518
	public UIDrawCall.Clipping clipping
	{
		get
		{
			return this.mClipping;
		}
		set
		{
			if (this.mClipping != value)
			{
				this.mCheckVisibility = true;
				this.mClipping = value;
				this.mMatrixTime = 0f;
				this.UpdateDrawcalls();
			}
		}
	}

	// Token: 0x170000BC RID: 188
	// (get) Token: 0x06000467 RID: 1127 RVA: 0x0001E348 File Offset: 0x0001C548
	// (set) Token: 0x06000468 RID: 1128 RVA: 0x0001E350 File Offset: 0x0001C550
	public Vector4 clipRange
	{
		get
		{
			return this.mClipRange;
		}
		set
		{
			if (this.mClipRange != value)
			{
				this.mCullTime = ((this.mCullTime != 0f) ? (Time.realtimeSinceStartup + 0.15f) : 0.001f);
				this.mCheckVisibility = true;
				this.mClipRange = value;
				this.mMatrixTime = 0f;
				this.UpdateDrawcalls();
			}
		}
	}

	// Token: 0x170000BD RID: 189
	// (get) Token: 0x06000469 RID: 1129 RVA: 0x0001E3B8 File Offset: 0x0001C5B8
	// (set) Token: 0x0600046A RID: 1130 RVA: 0x0001E3C0 File Offset: 0x0001C5C0
	public Vector2 clipSoftness
	{
		get
		{
			return this.mClipSoftness;
		}
		set
		{
			if (this.mClipSoftness != value)
			{
				this.mClipSoftness = value;
				this.UpdateDrawcalls();
			}
		}
	}

	// Token: 0x170000BE RID: 190
	// (get) Token: 0x0600046B RID: 1131 RVA: 0x0001E3E0 File Offset: 0x0001C5E0
	public BetterList<UIWidget> widgets
	{
		get
		{
			return this.mWidgets;
		}
	}

	// Token: 0x170000BF RID: 191
	// (get) Token: 0x0600046C RID: 1132 RVA: 0x0001E3E8 File Offset: 0x0001C5E8
	public BetterList<UIDrawCall> drawCalls
	{
		get
		{
			int i = this.mDrawCalls.size;
			while (i > 0)
			{
				UIDrawCall x = this.mDrawCalls[--i];
				if (x == null)
				{
					this.mDrawCalls.RemoveAt(i);
				}
			}
			return this.mDrawCalls;
		}
	}

	// Token: 0x0600046D RID: 1133 RVA: 0x0001E43C File Offset: 0x0001C63C
	private UINode GetNode(Transform t)
	{
		UINode result = null;
		if (t != null && this.mChildren.Contains(t))
		{
			result = (UINode)this.mChildren[t];
		}
		return result;
	}

	// Token: 0x0600046E RID: 1134 RVA: 0x0001E47C File Offset: 0x0001C67C
	private bool IsVisible(Vector3 a, Vector3 b, Vector3 c, Vector3 d)
	{
		this.UpdateTransformMatrix();
		a = this.mWorldToLocal.MultiplyPoint3x4(a);
		b = this.mWorldToLocal.MultiplyPoint3x4(b);
		c = this.mWorldToLocal.MultiplyPoint3x4(c);
		d = this.mWorldToLocal.MultiplyPoint3x4(d);
		UIPanel.mTemp[0] = a.x;
		UIPanel.mTemp[1] = b.x;
		UIPanel.mTemp[2] = c.x;
		UIPanel.mTemp[3] = d.x;
		float num = Mathf.Min(UIPanel.mTemp);
		float num2 = Mathf.Max(UIPanel.mTemp);
		UIPanel.mTemp[0] = a.y;
		UIPanel.mTemp[1] = b.y;
		UIPanel.mTemp[2] = c.y;
		UIPanel.mTemp[3] = d.y;
		float num3 = Mathf.Min(UIPanel.mTemp);
		float num4 = Mathf.Max(UIPanel.mTemp);
		return num2 >= this.mMin.x && num4 >= this.mMin.y && num <= this.mMax.x && num3 <= this.mMax.y;
	}

	// Token: 0x0600046F RID: 1135 RVA: 0x0001E5B4 File Offset: 0x0001C7B4
	public bool IsVisible(Vector3 worldPos)
	{
		if (this.mAlpha < 0.001f)
		{
			return false;
		}
		if (this.mClipping == UIDrawCall.Clipping.None)
		{
			return true;
		}
		this.UpdateTransformMatrix();
		Vector3 vector = this.mWorldToLocal.MultiplyPoint3x4(worldPos);
		return vector.x >= this.mMin.x && vector.y >= this.mMin.y && vector.x <= this.mMax.x && vector.y <= this.mMax.y;
	}

	// Token: 0x06000470 RID: 1136 RVA: 0x0001E658 File Offset: 0x0001C858
	public bool IsVisible(UIWidget w)
	{
		if (this.mAlpha < 0.001f)
		{
			return false;
		}
		if (!w.enabled || !NGUITools.GetActive(w.gameObject) || w.alpha < 0.001f)
		{
			return false;
		}
		if (this.mClipping == UIDrawCall.Clipping.None)
		{
			return true;
		}
		Vector2 relativeSize = w.relativeSize;
		Vector2 vector = Vector2.Scale(w.pivotOffset, relativeSize);
		Vector2 v = vector;
		vector.x += relativeSize.x;
		vector.y -= relativeSize.y;
		Transform cachedTransform = w.cachedTransform;
		Vector3 a = cachedTransform.TransformPoint(vector);
		Vector3 b = cachedTransform.TransformPoint(new Vector2(vector.x, v.y));
		Vector3 c = cachedTransform.TransformPoint(new Vector2(v.x, vector.y));
		Vector3 d = cachedTransform.TransformPoint(v);
		return this.IsVisible(a, b, c, d);
	}

	// Token: 0x06000471 RID: 1137 RVA: 0x0001E764 File Offset: 0x0001C964
	public void MarkMaterialAsChanged(Material mat, bool sort)
	{
		if (mat != null)
		{
			if (sort)
			{
				this.mDepthChanged = true;
			}
			if (!this.mChanged.Contains(mat))
			{
				this.mChanged.Add(mat);
				this.mChangedLastFrame = true;
			}
		}
	}

	// Token: 0x06000472 RID: 1138 RVA: 0x0001E7A4 File Offset: 0x0001C9A4
	public bool WatchesTransform(Transform t)
	{
		return t == this.cachedTransform || this.mChildren.Contains(t);
	}

	// Token: 0x06000473 RID: 1139 RVA: 0x0001E7D4 File Offset: 0x0001C9D4
	private UINode AddTransform(Transform t)
	{
		UINode uinode = null;
		while (t != null && t != this.cachedTransform)
		{
			if (this.mChildren.Contains(t))
			{
				if (uinode == null)
				{
					uinode = (UINode)this.mChildren[t];
				}
			}
			else
			{
				UINode uinode2 = new UINode(t);
				if (uinode == null)
				{
					uinode = uinode2;
				}
				this.mChildren.Add(t, uinode2);
			}
			t = t.parent;
		}
		return uinode;
	}

	// Token: 0x06000474 RID: 1140 RVA: 0x0001E85C File Offset: 0x0001CA5C
	private void RemoveTransform(Transform t)
	{
		if (t != null)
		{
			while (this.mChildren.Contains(t))
			{
				this.mChildren.Remove(t);
				t = t.parent;
				if (t == null || t == this.mTrans || t.childCount > 1)
				{
					break;
				}
			}
		}
	}

	// Token: 0x06000475 RID: 1141 RVA: 0x0001E8D0 File Offset: 0x0001CAD0
	public void AddWidget(UIWidget w)
	{
		if (w != null)
		{
			UINode uinode = this.AddTransform(w.cachedTransform);
			if (uinode != null)
			{
				uinode.widget = w;
				w.visibleFlag = 1;
				if (!this.mWidgets.Contains(w))
				{
					this.mWidgets.Add(w);
					if (!this.mChanged.Contains(w.material))
					{
						this.mChanged.Add(w.material);
						this.mChangedLastFrame = true;
					}
					this.mDepthChanged = true;
					this.mWidgetsAdded = true;
				}
			}
			else
			{
				Debug.LogError("Unable to find an appropriate root for " + NGUITools.GetHierarchy(w.gameObject) + "\nPlease make sure that there is at least one game object above this widget!", w.gameObject);
			}
		}
	}

	// Token: 0x06000476 RID: 1142 RVA: 0x0001E990 File Offset: 0x0001CB90
	public void RemoveWidget(UIWidget w)
	{
		if (w != null)
		{
			UINode node = this.GetNode(w.cachedTransform);
			if (node != null)
			{
				if (node.visibleFlag == 1 && !this.mChanged.Contains(w.material))
				{
					this.mChanged.Add(w.material);
					this.mChangedLastFrame = true;
				}
				this.RemoveTransform(w.cachedTransform);
			}
			this.mWidgets.Remove(w);
		}
	}

	// Token: 0x06000477 RID: 1143 RVA: 0x0001EA10 File Offset: 0x0001CC10
	private UIDrawCall GetDrawCall(Material mat, bool createIfMissing)
	{
		int i = 0;
		int size = this.drawCalls.size;
		while (i < size)
		{
			UIDrawCall uidrawCall = this.drawCalls.buffer[i];
			if (uidrawCall.material == mat)
			{
				return uidrawCall;
			}
			i++;
		}
		UIDrawCall uidrawCall2 = null;
		if (createIfMissing)
		{
			GameObject gameObject = new GameObject("_UIDrawCall [" + mat.name + "]");
			UnityEngine.Object.DontDestroyOnLoad(gameObject);
			gameObject.layer = base.gameObject.layer;
			uidrawCall2 = gameObject.AddComponent<UIDrawCall>();
			uidrawCall2.material = mat;
			this.mDrawCalls.Add(uidrawCall2);
		}
		return uidrawCall2;
	}

	// Token: 0x06000478 RID: 1144 RVA: 0x0001EAB8 File Offset: 0x0001CCB8
	private void Start()
	{
		this.mLayer = base.gameObject.layer;
		UICamera uicamera = UICamera.FindCameraForLayer(this.mLayer);
		this.mCam = ((!(uicamera != null)) ? NGUITools.FindCameraForLayer(this.mLayer) : uicamera.cachedCamera);
	}

	// Token: 0x06000479 RID: 1145 RVA: 0x0001EB0C File Offset: 0x0001CD0C
	private void OnEnable()
	{
		int i = 0;
		int size = this.mWidgets.size;
		while (i < size)
		{
			this.AddWidget(this.mWidgets.buffer[i]);
			i++;
		}
		this.mRebuildAll = true;
	}

	// Token: 0x0600047A RID: 1146 RVA: 0x0001EB54 File Offset: 0x0001CD54
	private void OnDisable()
	{
		int i = this.mDrawCalls.size;
		while (i > 0)
		{
			UIDrawCall uidrawCall = this.mDrawCalls.buffer[--i];
			if (uidrawCall != null)
			{
				NGUITools.DestroyImmediate(uidrawCall.gameObject);
			}
		}
		this.mDrawCalls.Clear();
		this.mChanged.Clear();
		this.mChildren.Clear();
	}

	// Token: 0x0600047B RID: 1147 RVA: 0x0001EBC4 File Offset: 0x0001CDC4
	private int GetChangeFlag(UINode start)
	{
		int num = start.changeFlag;
		if (num == -1)
		{
			Transform parent = start.trans.parent;
			while (parent != null && this.mChildren.Contains(parent))
			{
				UINode uinode = (UINode)this.mChildren[parent];
				num = uinode.changeFlag;
				parent = parent.parent;
				if (num != -1)
				{
					IL_89:
					int i = 0;
					int size = UIPanel.mHierarchy.size;
					while (i < size)
					{
						UINode uinode2 = UIPanel.mHierarchy.buffer[i];
						uinode2.changeFlag = num;
						i++;
					}
					UIPanel.mHierarchy.Clear();
					return num;
				}
				UIPanel.mHierarchy.Add(uinode);
			}
			num = 0;
			goto IL_89;
		}
		return num;
	}

	// Token: 0x0600047C RID: 1148 RVA: 0x0001EC9C File Offset: 0x0001CE9C
	private void UpdateTransformMatrix()
	{
		if (this.mUpdateTime == 0f || this.mMatrixTime != this.mUpdateTime)
		{
			this.mMatrixTime = this.mUpdateTime;
			this.mWorldToLocal = this.cachedTransform.worldToLocalMatrix;
			if (this.mClipping != UIDrawCall.Clipping.None)
			{
				Vector2 a = new Vector2(this.mClipRange.z, this.mClipRange.w);
				if (a.x == 0f)
				{
					a.x = ((!(this.mCam == null)) ? this.mCam.pixelWidth : ((float)Screen.width));
				}
				if (a.y == 0f)
				{
					a.y = ((!(this.mCam == null)) ? this.mCam.pixelHeight : ((float)Screen.height));
				}
				a *= 0.5f;
				this.mMin.x = this.mClipRange.x - a.x;
				this.mMin.y = this.mClipRange.y - a.y;
				this.mMax.x = this.mClipRange.x + a.x;
				this.mMax.y = this.mClipRange.y + a.y;
			}
		}
	}

	// Token: 0x0600047D RID: 1149 RVA: 0x0001EE14 File Offset: 0x0001D014
	private void UpdateTransforms()
	{
		this.mChangedLastFrame = false;
		bool flag = false;
		bool flag2 = this.clipping != UIDrawCall.Clipping.None && this.mUpdateTime > this.mCullTime;
		if (!this.widgetsAreStatic || this.mWidgetsAdded || flag2 != this.mCulled)
		{
			int i = 0;
			int count = this.mChildren.Count;
			while (i < count)
			{
				UINode uinode = (UINode)this.mChildren[i];
				if (uinode.trans == null)
				{
					this.mRemoved.Add(uinode.trans);
				}
				else if (uinode.HasChanged())
				{
					uinode.changeFlag = 1;
					flag = true;
				}
				else
				{
					uinode.changeFlag = -1;
				}
				i++;
			}
			int j = 0;
			int count2 = this.mRemoved.Count;
			while (j < count2)
			{
				this.mChildren.Remove(this.mRemoved[j]);
				j++;
			}
			this.mRemoved.Clear();
		}
		if (!this.mCulled && flag2)
		{
			this.mCheckVisibility = true;
		}
		if (this.mCheckVisibility || flag || this.mRebuildAll)
		{
			int k = 0;
			int count3 = this.mChildren.Count;
			while (k < count3)
			{
				UINode uinode2 = (UINode)this.mChildren[k];
				if (uinode2.widget != null)
				{
					int num = 1;
					if (flag2 || flag)
					{
						if (uinode2.changeFlag == -1)
						{
							uinode2.changeFlag = this.GetChangeFlag(uinode2);
						}
						if (flag2)
						{
							num = ((!this.mCheckVisibility && uinode2.changeFlag != 1) ? uinode2.visibleFlag : ((!this.IsVisible(uinode2.widget)) ? 0 : 1));
						}
					}
					if (uinode2.visibleFlag != num)
					{
						uinode2.changeFlag = 1;
					}
					if (uinode2.changeFlag == 1 && (num == 1 || uinode2.visibleFlag != 0))
					{
						uinode2.visibleFlag = num;
						Material material = uinode2.widget.material;
						if (!this.mChanged.Contains(material))
						{
							this.mChanged.Add(material);
							this.mChangedLastFrame = true;
						}
					}
				}
				k++;
			}
		}
		this.mCulled = flag2;
		this.mCheckVisibility = false;
		this.mWidgetsAdded = false;
	}

	// Token: 0x0600047E RID: 1150 RVA: 0x0001F0A8 File Offset: 0x0001D2A8
	private void UpdateWidgets()
	{
		int i = 0;
		int count = this.mChildren.Count;
		while (i < count)
		{
			UINode uinode = (UINode)this.mChildren[i];
			UIWidget widget = uinode.widget;
			if (uinode.visibleFlag == 1 && widget != null && widget.UpdateGeometry(this, ref this.mWorldToLocal, uinode.changeFlag == 1, this.generateNormals) && !this.mChanged.Contains(widget.material))
			{
				this.mChanged.Add(widget.material);
				this.mChangedLastFrame = true;
			}
			uinode.changeFlag = 0;
			i++;
		}
	}

	// Token: 0x0600047F RID: 1151 RVA: 0x0001F15C File Offset: 0x0001D35C
	public void UpdateDrawcalls()
	{
		Vector4 zero = Vector4.zero;
		if (this.mClipping != UIDrawCall.Clipping.None)
		{
			zero = new Vector4(this.mClipRange.x, this.mClipRange.y, this.mClipRange.z * 0.5f, this.mClipRange.w * 0.5f);
		}
		if (zero.z == 0f)
		{
			zero.z = (float)Screen.width * 0.5f;
		}
		if (zero.w == 0f)
		{
			zero.w = (float)Screen.height * 0.5f;
		}
		RuntimePlatform platform = Application.platform;
		if (platform == RuntimePlatform.WindowsPlayer || platform == RuntimePlatform.WindowsWebPlayer || platform == RuntimePlatform.WindowsEditor)
		{
			zero.x -= 0.5f;
			zero.y += 0.5f;
		}
		Transform cachedTransform = this.cachedTransform;
		int i = 0;
		int size = this.mDrawCalls.size;
		while (i < size)
		{
			UIDrawCall uidrawCall = this.mDrawCalls.buffer[i];
			uidrawCall.clipping = this.mClipping;
			uidrawCall.clipRange = zero;
			uidrawCall.clipSoftness = this.mClipSoftness;
			uidrawCall.depthPass = (this.depthPass && this.mClipping == UIDrawCall.Clipping.None);
			Transform transform = uidrawCall.transform;
			transform.position = cachedTransform.position;
			transform.rotation = cachedTransform.rotation;
			transform.localScale = cachedTransform.lossyScale;
			i++;
		}
	}

	// Token: 0x06000480 RID: 1152 RVA: 0x0001F2E8 File Offset: 0x0001D4E8
	private void Fill(Material mat)
	{
		int i = this.mWidgets.size;
		while (i > 0)
		{
			if (this.mWidgets[--i] == null)
			{
				this.mWidgets.RemoveAt(i);
			}
		}
		int j = 0;
		int size = this.mWidgets.size;
		while (j < size)
		{
			UIWidget uiwidget = this.mWidgets.buffer[j];
			if (uiwidget.visibleFlag == 1 && uiwidget.material == mat)
			{
				UINode node = this.GetNode(uiwidget.cachedTransform);
				if (node != null)
				{
					if (this.generateNormals)
					{
						uiwidget.WriteToBuffers(this.mVerts, this.mUvs, this.mCols, this.mNorms, this.mTans);
					}
					else
					{
						uiwidget.WriteToBuffers(this.mVerts, this.mUvs, this.mCols, null, null);
					}
				}
				else
				{
					Debug.LogError("No transform found for " + NGUITools.GetHierarchy(uiwidget.gameObject), this);
				}
			}
			j++;
		}
		if (this.mVerts.size > 0)
		{
			UIDrawCall drawCall = this.GetDrawCall(mat, true);
			drawCall.depthPass = (this.depthPass && this.mClipping == UIDrawCall.Clipping.None);
			drawCall.Set(this.mVerts, (!this.generateNormals) ? null : this.mNorms, (!this.generateNormals) ? null : this.mTans, this.mUvs, this.mCols);
		}
		else
		{
			UIDrawCall drawCall2 = this.GetDrawCall(mat, false);
			if (drawCall2 != null)
			{
				this.mDrawCalls.Remove(drawCall2);
				NGUITools.DestroyImmediate(drawCall2.gameObject);
			}
		}
		this.mVerts.Clear();
		this.mNorms.Clear();
		this.mTans.Clear();
		this.mUvs.Clear();
		this.mCols.Clear();
	}

	// Token: 0x06000481 RID: 1153 RVA: 0x0001F4F0 File Offset: 0x0001D6F0
	private void LateUpdate()
	{
		this.mUpdateTime = Time.realtimeSinceStartup;
		this.UpdateTransformMatrix();
		this.UpdateTransforms();
		if (this.mLayer != base.gameObject.layer)
		{
			this.mLayer = base.gameObject.layer;
			UICamera uicamera = UICamera.FindCameraForLayer(this.mLayer);
			this.mCam = ((!(uicamera != null)) ? NGUITools.FindCameraForLayer(this.mLayer) : uicamera.cachedCamera);
			UIPanel.SetChildLayer(this.cachedTransform, this.mLayer);
			int i = 0;
			int size = this.drawCalls.size;
			while (i < size)
			{
				this.mDrawCalls.buffer[i].gameObject.layer = this.mLayer;
				i++;
			}
		}
		this.UpdateWidgets();
		if (this.mDepthChanged)
		{
			this.mDepthChanged = false;
			this.mWidgets.Sort(new Comparison<UIWidget>(UIWidget.CompareFunc));
		}
		int j = 0;
		int size2 = this.mChanged.size;
		while (j < size2)
		{
			this.Fill(this.mChanged.buffer[j]);
			j++;
		}
		this.UpdateDrawcalls();
		this.mChanged.Clear();
		this.mRebuildAll = false;
	}

	// Token: 0x06000482 RID: 1154 RVA: 0x0001F638 File Offset: 0x0001D838
	public void Refresh()
	{
		UIWidget[] componentsInChildren = base.GetComponentsInChildren<UIWidget>();
		int i = 0;
		int num = componentsInChildren.Length;
		while (i < num)
		{
			componentsInChildren[i].Update();
			i++;
		}
		this.LateUpdate();
	}

	// Token: 0x06000483 RID: 1155 RVA: 0x0001F670 File Offset: 0x0001D870
	public Vector3 CalculateConstrainOffset(Vector2 min, Vector2 max)
	{
		float num = this.clipRange.z * 0.5f;
		float num2 = this.clipRange.w * 0.5f;
		Vector2 minRect = new Vector2(min.x, min.y);
		Vector2 maxRect = new Vector2(max.x, max.y);
		Vector2 minArea = new Vector2(this.clipRange.x - num, this.clipRange.y - num2);
		Vector2 maxArea = new Vector2(this.clipRange.x + num, this.clipRange.y + num2);
		if (this.clipping == UIDrawCall.Clipping.SoftClip)
		{
			minArea.x += this.clipSoftness.x;
			minArea.y += this.clipSoftness.y;
			maxArea.x -= this.clipSoftness.x;
			maxArea.y -= this.clipSoftness.y;
		}
		return NGUIMath.ConstrainRect(minRect, maxRect, minArea, maxArea);
	}

	// Token: 0x06000484 RID: 1156 RVA: 0x0001F7B8 File Offset: 0x0001D9B8
	public bool ConstrainTargetToBounds(Transform target, ref Bounds targetBounds, bool immediate)
	{
		Vector3 b = this.CalculateConstrainOffset(targetBounds.min, targetBounds.max);
		if (b.magnitude > 0f)
		{
			if (immediate)
			{
				target.localPosition += b;
				targetBounds.center += b;
				SpringPosition component = target.GetComponent<SpringPosition>();
				if (component != null)
				{
					component.enabled = false;
				}
			}
			else
			{
				SpringPosition springPosition = SpringPosition.Begin(target.gameObject, target.localPosition + b, 13f);
				springPosition.ignoreTimeScale = true;
				springPosition.worldSpace = false;
			}
			return true;
		}
		return false;
	}

	// Token: 0x06000485 RID: 1157 RVA: 0x0001F86C File Offset: 0x0001DA6C
	public bool ConstrainTargetToBounds(Transform target, bool immediate)
	{
		Bounds bounds = NGUIMath.CalculateRelativeWidgetBounds(this.cachedTransform, target);
		return this.ConstrainTargetToBounds(target, ref bounds, immediate);
	}

	// Token: 0x06000486 RID: 1158 RVA: 0x0001F890 File Offset: 0x0001DA90
	private static void SetChildLayer(Transform t, int layer)
	{
		for (int i = 0; i < t.childCount; i++)
		{
			Transform child = t.GetChild(i);
			if (child.GetComponent<UIPanel>() == null)
			{
				child.gameObject.layer = layer;
				UIPanel.SetChildLayer(child, layer);
			}
		}
	}

	// Token: 0x06000487 RID: 1159 RVA: 0x0001F8E0 File Offset: 0x0001DAE0
	public static UIPanel Find(Transform trans, bool createIfMissing)
	{
		Transform y = trans;
		UIPanel uipanel = null;
		while (uipanel == null && trans != null)
		{
			uipanel = trans.GetComponent<UIPanel>();
			if (uipanel != null)
			{
				break;
			}
			if (trans.parent == null)
			{
				break;
			}
			trans = trans.parent;
		}
		if (createIfMissing && uipanel == null && trans != y)
		{
			uipanel = trans.gameObject.AddComponent<UIPanel>();
			UIPanel.SetChildLayer(uipanel.cachedTransform, uipanel.gameObject.layer);
		}
		return uipanel;
	}

	// Token: 0x06000488 RID: 1160 RVA: 0x0001F988 File Offset: 0x0001DB88
	public static UIPanel Find(Transform trans)
	{
		return UIPanel.Find(trans, true);
	}

	// Token: 0x04000479 RID: 1145
	public bool showInPanelTool = true;

	// Token: 0x0400047A RID: 1146
	public bool generateNormals;

	// Token: 0x0400047B RID: 1147
	public bool depthPass;

	// Token: 0x0400047C RID: 1148
	public bool widgetsAreStatic;

	// Token: 0x0400047D RID: 1149
	[HideInInspector]
	[SerializeField]
	private float mAlpha = 1f;

	// Token: 0x0400047E RID: 1150
	[HideInInspector]
	[SerializeField]
	private UIPanel.DebugInfo mDebugInfo = UIPanel.DebugInfo.Gizmos;

	// Token: 0x0400047F RID: 1151
	[HideInInspector]
	[SerializeField]
	private UIDrawCall.Clipping mClipping;

	// Token: 0x04000480 RID: 1152
	[SerializeField]
	[HideInInspector]
	private Vector4 mClipRange = Vector4.zero;

	// Token: 0x04000481 RID: 1153
	[SerializeField]
	[HideInInspector]
	private Vector2 mClipSoftness = new Vector2(40f, 40f);

	// Token: 0x04000482 RID: 1154
	private OrderedDictionary mChildren = new OrderedDictionary();

	// Token: 0x04000483 RID: 1155
	private BetterList<UIWidget> mWidgets = new BetterList<UIWidget>();

	// Token: 0x04000484 RID: 1156
	private BetterList<Material> mChanged = new BetterList<Material>();

	// Token: 0x04000485 RID: 1157
	private BetterList<UIDrawCall> mDrawCalls = new BetterList<UIDrawCall>();

	// Token: 0x04000486 RID: 1158
	private BetterList<Vector3> mVerts = new BetterList<Vector3>();

	// Token: 0x04000487 RID: 1159
	private BetterList<Vector3> mNorms = new BetterList<Vector3>();

	// Token: 0x04000488 RID: 1160
	private BetterList<Vector4> mTans = new BetterList<Vector4>();

	// Token: 0x04000489 RID: 1161
	private BetterList<Vector2> mUvs = new BetterList<Vector2>();

	// Token: 0x0400048A RID: 1162
	private BetterList<Color32> mCols = new BetterList<Color32>();

	// Token: 0x0400048B RID: 1163
	private Transform mTrans;

	// Token: 0x0400048C RID: 1164
	private Camera mCam;

	// Token: 0x0400048D RID: 1165
	private int mLayer = -1;

	// Token: 0x0400048E RID: 1166
	private bool mDepthChanged;

	// Token: 0x0400048F RID: 1167
	private bool mRebuildAll;

	// Token: 0x04000490 RID: 1168
	private bool mChangedLastFrame;

	// Token: 0x04000491 RID: 1169
	private bool mWidgetsAdded;

	// Token: 0x04000492 RID: 1170
	private float mUpdateTime;

	// Token: 0x04000493 RID: 1171
	private float mMatrixTime;

	// Token: 0x04000494 RID: 1172
	private Matrix4x4 mWorldToLocal = Matrix4x4.identity;

	// Token: 0x04000495 RID: 1173
	private static float[] mTemp = new float[4];

	// Token: 0x04000496 RID: 1174
	private Vector2 mMin = Vector2.zero;

	// Token: 0x04000497 RID: 1175
	private Vector2 mMax = Vector2.zero;

	// Token: 0x04000498 RID: 1176
	private List<Transform> mRemoved = new List<Transform>();

	// Token: 0x04000499 RID: 1177
	private UIPanel[] mChildPanels;

	// Token: 0x0400049A RID: 1178
	private bool mCheckVisibility;

	// Token: 0x0400049B RID: 1179
	private float mCullTime;

	// Token: 0x0400049C RID: 1180
	private bool mCulled;

	// Token: 0x0400049D RID: 1181
	private static BetterList<UINode> mHierarchy = new BetterList<UINode>();

	// Token: 0x02000088 RID: 136
	public enum DebugInfo
	{
		// Token: 0x0400049F RID: 1183
		None,
		// Token: 0x040004A0 RID: 1184
		Gizmos,
		// Token: 0x040004A1 RID: 1185
		Geometry
	}
}
