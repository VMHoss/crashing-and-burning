using System;
using UnityEngine;

// Token: 0x02000090 RID: 144
[AddComponentMenu("NGUI/UI/Stretch")]
[ExecuteInEditMode]
public class UIStretch : MonoBehaviour
{
	// Token: 0x060004C9 RID: 1225 RVA: 0x000225F4 File Offset: 0x000207F4
	private void Awake()
	{
		this.mAnim = base.animation;
		this.mRect = default(Rect);
		this.mTrans = base.transform;
	}

	// Token: 0x060004CA RID: 1226 RVA: 0x00022628 File Offset: 0x00020828
	private void Start()
	{
		if (this.uiCamera == null)
		{
			this.uiCamera = NGUITools.FindCameraForLayer(base.gameObject.layer);
		}
		this.mRoot = NGUITools.FindInParents<UIRoot>(base.gameObject);
	}

	// Token: 0x060004CB RID: 1227 RVA: 0x00022670 File Offset: 0x00020870
	private void Update()
	{
		if (this.mAnim != null && this.mAnim.isPlaying)
		{
			return;
		}
		if (this.style != UIStretch.Style.None)
		{
			float num = 1f;
			if (this.panelContainer != null)
			{
				if (this.panelContainer.clipping == UIDrawCall.Clipping.None)
				{
					this.mRect.xMin = (float)(-(float)Screen.width) * 0.5f;
					this.mRect.yMin = (float)(-(float)Screen.height) * 0.5f;
					this.mRect.xMax = -this.mRect.xMin;
					this.mRect.yMax = -this.mRect.yMin;
				}
				else
				{
					Vector4 clipRange = this.panelContainer.clipRange;
					this.mRect.x = clipRange.x - clipRange.z * 0.5f;
					this.mRect.y = clipRange.y - clipRange.w * 0.5f;
					this.mRect.width = clipRange.z;
					this.mRect.height = clipRange.w;
				}
			}
			else if (this.widgetContainer != null)
			{
				Transform cachedTransform = this.widgetContainer.cachedTransform;
				Vector3 localScale = cachedTransform.localScale;
				Vector3 localPosition = cachedTransform.localPosition;
				Vector3 vector = this.widgetContainer.relativeSize;
				Vector3 vector2 = this.widgetContainer.pivotOffset;
				vector2.y -= 1f;
				vector2.x *= this.widgetContainer.relativeSize.x * localScale.x;
				vector2.y *= this.widgetContainer.relativeSize.y * localScale.y;
				this.mRect.x = localPosition.x + vector2.x;
				this.mRect.y = localPosition.y + vector2.y;
				this.mRect.width = vector.x * localScale.x;
				this.mRect.height = vector.y * localScale.y;
			}
			else
			{
				if (!(this.uiCamera != null))
				{
					return;
				}
				this.mRect = this.uiCamera.pixelRect;
				if (this.mRoot != null)
				{
					num = this.mRoot.pixelSizeAdjustment;
				}
			}
			float num2 = this.mRect.width;
			float num3 = this.mRect.height;
			if (num != 1f && num3 > 1f)
			{
				float num4 = (float)this.mRoot.activeHeight / num3;
				num2 *= num4;
				num3 *= num4;
			}
			Vector3 localScale2 = this.mTrans.localScale;
			if (this.style == UIStretch.Style.BasedOnHeight)
			{
				localScale2.x = this.relativeSize.x * num3;
				localScale2.y = this.relativeSize.y * num3;
			}
			else if (this.style == UIStretch.Style.FillKeepingRatio)
			{
				float num5 = num2 / num3;
				float num6 = this.initialSize.x / this.initialSize.y;
				if (num6 < num5)
				{
					float num7 = num2 / this.initialSize.x;
					localScale2.x = num2;
					localScale2.y = this.initialSize.y * num7;
				}
				else
				{
					float num8 = num3 / this.initialSize.y;
					localScale2.x = this.initialSize.x * num8;
					localScale2.y = num3;
				}
			}
			else if (this.style == UIStretch.Style.FitInternalKeepingRatio)
			{
				float num9 = num2 / num3;
				float num10 = this.initialSize.x / this.initialSize.y;
				if (num10 > num9)
				{
					float num11 = num2 / this.initialSize.x;
					localScale2.x = num2;
					localScale2.y = this.initialSize.y * num11;
				}
				else
				{
					float num12 = num3 / this.initialSize.y;
					localScale2.x = this.initialSize.x * num12;
					localScale2.y = num3;
				}
			}
			else
			{
				if (this.style == UIStretch.Style.Both || this.style == UIStretch.Style.Horizontal)
				{
					localScale2.x = this.relativeSize.x * num2;
				}
				if (this.style == UIStretch.Style.Both || this.style == UIStretch.Style.Vertical)
				{
					localScale2.y = this.relativeSize.y * num3;
				}
			}
			if (this.mTrans.localScale != localScale2)
			{
				this.mTrans.localScale = localScale2;
			}
		}
	}

	// Token: 0x040004CE RID: 1230
	public Camera uiCamera;

	// Token: 0x040004CF RID: 1231
	public UIWidget widgetContainer;

	// Token: 0x040004D0 RID: 1232
	public UIPanel panelContainer;

	// Token: 0x040004D1 RID: 1233
	public UIStretch.Style style;

	// Token: 0x040004D2 RID: 1234
	public Vector2 relativeSize = Vector2.one;

	// Token: 0x040004D3 RID: 1235
	public Vector2 initialSize = Vector2.one;

	// Token: 0x040004D4 RID: 1236
	private Transform mTrans;

	// Token: 0x040004D5 RID: 1237
	private UIRoot mRoot;

	// Token: 0x040004D6 RID: 1238
	private Animation mAnim;

	// Token: 0x040004D7 RID: 1239
	private Rect mRect;

	// Token: 0x02000091 RID: 145
	public enum Style
	{
		// Token: 0x040004D9 RID: 1241
		None,
		// Token: 0x040004DA RID: 1242
		Horizontal,
		// Token: 0x040004DB RID: 1243
		Vertical,
		// Token: 0x040004DC RID: 1244
		Both,
		// Token: 0x040004DD RID: 1245
		BasedOnHeight,
		// Token: 0x040004DE RID: 1246
		FillKeepingRatio,
		// Token: 0x040004DF RID: 1247
		FitInternalKeepingRatio
	}
}
