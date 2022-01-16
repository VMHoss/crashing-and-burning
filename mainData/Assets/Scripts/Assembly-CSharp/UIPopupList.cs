﻿using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200003F RID: 63
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/Popup List")]
public class UIPopupList : MonoBehaviour
{
	// Token: 0x17000027 RID: 39
	// (get) Token: 0x060001F8 RID: 504 RVA: 0x0000D988 File Offset: 0x0000BB88
	public bool isOpen
	{
		get
		{
			return this.mChild != null;
		}
	}

	// Token: 0x17000028 RID: 40
	// (get) Token: 0x060001F9 RID: 505 RVA: 0x0000D998 File Offset: 0x0000BB98
	// (set) Token: 0x060001FA RID: 506 RVA: 0x0000D9A0 File Offset: 0x0000BBA0
	public string selection
	{
		get
		{
			return this.mSelectedItem;
		}
		set
		{
			if (this.mSelectedItem != value)
			{
				this.mSelectedItem = value;
				if (this.textLabel != null)
				{
					this.textLabel.text = ((!this.isLocalized) ? value : Localization.Localize(value));
				}
				UIPopupList.current = this;
				if (this.onSelectionChange != null)
				{
					this.onSelectionChange(this.mSelectedItem);
				}
				if (this.eventReceiver != null && !string.IsNullOrEmpty(this.functionName) && Application.isPlaying)
				{
					this.eventReceiver.SendMessage(this.functionName, this.mSelectedItem, SendMessageOptions.DontRequireReceiver);
				}
				UIPopupList.current = null;
				if (this.textLabel == null)
				{
					this.mSelectedItem = null;
				}
			}
		}
	}

	// Token: 0x17000029 RID: 41
	// (get) Token: 0x060001FB RID: 507 RVA: 0x0000DA7C File Offset: 0x0000BC7C
	// (set) Token: 0x060001FC RID: 508 RVA: 0x0000DAA8 File Offset: 0x0000BCA8
	private bool handleEvents
	{
		get
		{
			UIButtonKeys component = base.GetComponent<UIButtonKeys>();
			return component == null || !component.enabled;
		}
		set
		{
			UIButtonKeys component = base.GetComponent<UIButtonKeys>();
			if (component != null)
			{
				component.enabled = !value;
			}
		}
	}

	// Token: 0x060001FD RID: 509 RVA: 0x0000DAD4 File Offset: 0x0000BCD4
	private void Start()
	{
		if (this.textLabel != null)
		{
			if (string.IsNullOrEmpty(this.mSelectedItem))
			{
				if (this.items.Count > 0)
				{
					this.selection = this.items[0];
				}
			}
			else
			{
				string selection = this.mSelectedItem;
				this.mSelectedItem = null;
				this.selection = selection;
			}
		}
	}

	// Token: 0x060001FE RID: 510 RVA: 0x0000DB40 File Offset: 0x0000BD40
	private void OnLocalize(Localization loc)
	{
		if (this.isLocalized && this.textLabel != null)
		{
			this.textLabel.text = loc.Get(this.mSelectedItem);
		}
	}

	// Token: 0x060001FF RID: 511 RVA: 0x0000DB78 File Offset: 0x0000BD78
	private void Highlight(UILabel lbl, bool instant)
	{
		if (this.mHighlight != null)
		{
			TweenPosition component = lbl.GetComponent<TweenPosition>();
			if (component != null && component.enabled)
			{
				return;
			}
			this.mHighlightedLabel = lbl;
			UIAtlas.Sprite atlasSprite = this.mHighlight.GetAtlasSprite();
			if (atlasSprite == null)
			{
				return;
			}
			float num = atlasSprite.inner.xMin - atlasSprite.outer.xMin;
			float y = atlasSprite.inner.yMin - atlasSprite.outer.yMin;
			Vector3 vector = lbl.cachedTransform.localPosition + new Vector3(-num, y, 0f);
			if (instant || !this.isAnimated)
			{
				this.mHighlight.cachedTransform.localPosition = vector;
			}
			else
			{
				TweenPosition.Begin(this.mHighlight.gameObject, 0.1f, vector).method = UITweener.Method.EaseOut;
			}
		}
	}

	// Token: 0x06000200 RID: 512 RVA: 0x0000DC64 File Offset: 0x0000BE64
	private void OnItemHover(GameObject go, bool isOver)
	{
		if (isOver)
		{
			UILabel component = go.GetComponent<UILabel>();
			this.Highlight(component, false);
		}
	}

	// Token: 0x06000201 RID: 513 RVA: 0x0000DC88 File Offset: 0x0000BE88
	private void Select(UILabel lbl, bool instant)
	{
		this.Highlight(lbl, instant);
		UIEventListener component = lbl.gameObject.GetComponent<UIEventListener>();
		this.selection = (component.parameter as string);
		UIButtonSound[] components = base.GetComponents<UIButtonSound>();
		int i = 0;
		int num = components.Length;
		while (i < num)
		{
			UIButtonSound uibuttonSound = components[i];
			if (uibuttonSound.trigger == UIButtonSound.Trigger.OnClick)
			{
				NGUITools.PlaySound(uibuttonSound.audioClip, uibuttonSound.volume, 1f);
			}
			i++;
		}
	}

	// Token: 0x06000202 RID: 514 RVA: 0x0000DD04 File Offset: 0x0000BF04
	private void OnItemPress(GameObject go, bool isPressed)
	{
		if (isPressed)
		{
			this.Select(go.GetComponent<UILabel>(), true);
		}
	}

	// Token: 0x06000203 RID: 515 RVA: 0x0000DD1C File Offset: 0x0000BF1C
	private void OnKey(KeyCode key)
	{
		if (base.enabled && NGUITools.GetActive(base.gameObject) && this.handleEvents)
		{
			int num = this.mLabelList.IndexOf(this.mHighlightedLabel);
			if (key == KeyCode.UpArrow)
			{
				if (num > 0)
				{
					this.Select(this.mLabelList[num - 1], false);
				}
			}
			else if (key == KeyCode.DownArrow)
			{
				if (num + 1 < this.mLabelList.Count)
				{
					this.Select(this.mLabelList[num + 1], false);
				}
			}
			else if (key == KeyCode.Escape)
			{
				this.OnSelect(false);
			}
		}
	}

	// Token: 0x06000204 RID: 516 RVA: 0x0000DDD8 File Offset: 0x0000BFD8
	private void OnSelect(bool isSelected)
	{
		if (!isSelected && this.mChild != null)
		{
			this.mLabelList.Clear();
			this.handleEvents = false;
			if (this.isAnimated)
			{
				UIWidget[] componentsInChildren = this.mChild.GetComponentsInChildren<UIWidget>();
				int i = 0;
				int num = componentsInChildren.Length;
				while (i < num)
				{
					UIWidget uiwidget = componentsInChildren[i];
					Color color = uiwidget.color;
					color.a = 0f;
					TweenColor.Begin(uiwidget.gameObject, 0.15f, color).method = UITweener.Method.EaseOut;
					i++;
				}
				Collider[] componentsInChildren2 = this.mChild.GetComponentsInChildren<Collider>();
				int j = 0;
				int num2 = componentsInChildren2.Length;
				while (j < num2)
				{
					componentsInChildren2[j].enabled = false;
					j++;
				}
				UpdateManager.AddDestroy(this.mChild, 0.15f);
			}
			else
			{
				UnityEngine.Object.Destroy(this.mChild);
			}
			this.mBackground = null;
			this.mHighlight = null;
			this.mChild = null;
		}
	}

	// Token: 0x06000205 RID: 517 RVA: 0x0000DED8 File Offset: 0x0000C0D8
	private void AnimateColor(UIWidget widget)
	{
		Color color = widget.color;
		widget.color = new Color(color.r, color.g, color.b, 0f);
		TweenColor.Begin(widget.gameObject, 0.15f, color).method = UITweener.Method.EaseOut;
	}

	// Token: 0x06000206 RID: 518 RVA: 0x0000DF28 File Offset: 0x0000C128
	private void AnimatePosition(UIWidget widget, bool placeAbove, float bottom)
	{
		Vector3 localPosition = widget.cachedTransform.localPosition;
		Vector3 localPosition2 = (!placeAbove) ? new Vector3(localPosition.x, 0f, localPosition.z) : new Vector3(localPosition.x, bottom, localPosition.z);
		widget.cachedTransform.localPosition = localPosition2;
		GameObject gameObject = widget.gameObject;
		TweenPosition.Begin(gameObject, 0.15f, localPosition).method = UITweener.Method.EaseOut;
	}

	// Token: 0x06000207 RID: 519 RVA: 0x0000DFA0 File Offset: 0x0000C1A0
	private void AnimateScale(UIWidget widget, bool placeAbove, float bottom)
	{
		GameObject gameObject = widget.gameObject;
		Transform cachedTransform = widget.cachedTransform;
		float num = (float)this.font.size * this.textScale + this.mBgBorder * 2f;
		Vector3 localScale = cachedTransform.localScale;
		cachedTransform.localScale = new Vector3(localScale.x, num, localScale.z);
		TweenScale.Begin(gameObject, 0.15f, localScale).method = UITweener.Method.EaseOut;
		if (placeAbove)
		{
			Vector3 localPosition = cachedTransform.localPosition;
			cachedTransform.localPosition = new Vector3(localPosition.x, localPosition.y - localScale.y + num, localPosition.z);
			TweenPosition.Begin(gameObject, 0.15f, localPosition).method = UITweener.Method.EaseOut;
		}
	}

	// Token: 0x06000208 RID: 520 RVA: 0x0000E05C File Offset: 0x0000C25C
	private void Animate(UIWidget widget, bool placeAbove, float bottom)
	{
		this.AnimateColor(widget);
		this.AnimatePosition(widget, placeAbove, bottom);
	}

	// Token: 0x06000209 RID: 521 RVA: 0x0000E070 File Offset: 0x0000C270
	private void OnClick()
	{
		if (this.mChild == null && this.atlas != null && this.font != null && this.items.Count > 0)
		{
			this.mLabelList.Clear();
			this.handleEvents = true;
			if (this.mPanel == null)
			{
				this.mPanel = UIPanel.Find(base.transform, true);
			}
			Transform transform = base.transform;
			Bounds bounds = NGUIMath.CalculateRelativeWidgetBounds(transform.parent, transform);
			this.mChild = new GameObject("Drop-down List");
			this.mChild.layer = base.gameObject.layer;
			Transform transform2 = this.mChild.transform;
			transform2.parent = transform.parent;
			transform2.localPosition = bounds.min;
			transform2.localRotation = Quaternion.identity;
			transform2.localScale = Vector3.one;
			this.mBackground = NGUITools.AddSprite(this.mChild, this.atlas, this.backgroundSprite);
			this.mBackground.pivot = UIWidget.Pivot.TopLeft;
			this.mBackground.depth = NGUITools.CalculateNextDepth(this.mPanel.gameObject);
			this.mBackground.color = this.backgroundColor;
			Vector4 border = this.mBackground.border;
			this.mBgBorder = border.y;
			this.mBackground.cachedTransform.localPosition = new Vector3(0f, border.y, 0f);
			this.mHighlight = NGUITools.AddSprite(this.mChild, this.atlas, this.highlightSprite);
			this.mHighlight.pivot = UIWidget.Pivot.TopLeft;
			this.mHighlight.color = this.highlightColor;
			UIAtlas.Sprite atlasSprite = this.mHighlight.GetAtlasSprite();
			if (atlasSprite == null)
			{
				return;
			}
			float num = atlasSprite.inner.yMin - atlasSprite.outer.yMin;
			float num2 = (float)this.font.size * this.font.pixelSize * this.textScale;
			float num3 = 0f;
			float num4 = -this.padding.y;
			List<UILabel> list = new List<UILabel>();
			int i = 0;
			int count = this.items.Count;
			while (i < count)
			{
				string text = this.items[i];
				UILabel uilabel = NGUITools.AddWidget<UILabel>(this.mChild);
				uilabel.pivot = UIWidget.Pivot.TopLeft;
				uilabel.font = this.font;
				uilabel.text = ((!this.isLocalized || !(Localization.instance != null)) ? text : Localization.instance.Get(text));
				uilabel.color = this.textColor;
				uilabel.cachedTransform.localPosition = new Vector3(border.x + this.padding.x, num4, -0.01f);
				uilabel.MakePixelPerfect();
				if (this.textScale != 1f)
				{
					Vector3 localScale = uilabel.cachedTransform.localScale;
					uilabel.cachedTransform.localScale = localScale * this.textScale;
				}
				list.Add(uilabel);
				num4 -= num2;
				num4 -= this.padding.y;
				num3 = Mathf.Max(num3, uilabel.relativeSize.x * num2);
				UIEventListener uieventListener = UIEventListener.Get(uilabel.gameObject);
				uieventListener.onHover = new UIEventListener.BoolDelegate(this.OnItemHover);
				uieventListener.onPress = new UIEventListener.BoolDelegate(this.OnItemPress);
				uieventListener.parameter = text;
				if (this.mSelectedItem == text)
				{
					this.Highlight(uilabel, true);
				}
				this.mLabelList.Add(uilabel);
				i++;
			}
			num3 = Mathf.Max(num3, bounds.size.x - (border.x + this.padding.x) * 2f);
			Vector3 center = new Vector3(num3 * 0.5f / num2, -0.5f, 0f);
			Vector3 size = new Vector3(num3 / num2, (num2 + this.padding.y) / num2, 1f);
			int j = 0;
			int count2 = list.Count;
			while (j < count2)
			{
				UILabel uilabel2 = list[j];
				BoxCollider boxCollider = NGUITools.AddWidgetCollider(uilabel2.gameObject);
				center.z = boxCollider.center.z;
				boxCollider.center = center;
				boxCollider.size = size;
				j++;
			}
			num3 += (border.x + this.padding.x) * 2f;
			num4 -= border.y;
			this.mBackground.cachedTransform.localScale = new Vector3(num3, -num4 + border.y, 1f);
			this.mHighlight.cachedTransform.localScale = new Vector3(num3 - (border.x + this.padding.x) * 2f + (atlasSprite.inner.xMin - atlasSprite.outer.xMin) * 2f, num2 + num * 2f, 1f);
			bool flag = this.position == UIPopupList.Position.Above;
			if (this.position == UIPopupList.Position.Auto)
			{
				UICamera uicamera = UICamera.FindCameraForLayer(base.gameObject.layer);
				if (uicamera != null)
				{
					flag = (uicamera.cachedCamera.WorldToViewportPoint(transform.position).y < 0.5f);
				}
			}
			if (this.isAnimated)
			{
				float bottom = num4 + num2;
				this.Animate(this.mHighlight, flag, bottom);
				int k = 0;
				int count3 = list.Count;
				while (k < count3)
				{
					this.Animate(list[k], flag, bottom);
					k++;
				}
				this.AnimateColor(this.mBackground);
				this.AnimateScale(this.mBackground, flag, bottom);
			}
			if (flag)
			{
				transform2.localPosition = new Vector3(bounds.min.x, bounds.max.y - num4 - border.y, bounds.min.z);
			}
		}
		else
		{
			this.OnSelect(false);
		}
	}

	// Token: 0x0400025E RID: 606
	private const float animSpeed = 0.15f;

	// Token: 0x0400025F RID: 607
	public static UIPopupList current;

	// Token: 0x04000260 RID: 608
	public UIAtlas atlas;

	// Token: 0x04000261 RID: 609
	public UIFont font;

	// Token: 0x04000262 RID: 610
	public UILabel textLabel;

	// Token: 0x04000263 RID: 611
	public string backgroundSprite;

	// Token: 0x04000264 RID: 612
	public string highlightSprite;

	// Token: 0x04000265 RID: 613
	public UIPopupList.Position position;

	// Token: 0x04000266 RID: 614
	public List<string> items = new List<string>();

	// Token: 0x04000267 RID: 615
	public Vector2 padding = new Vector3(4f, 4f);

	// Token: 0x04000268 RID: 616
	public float textScale = 1f;

	// Token: 0x04000269 RID: 617
	public Color textColor = Color.white;

	// Token: 0x0400026A RID: 618
	public Color backgroundColor = Color.white;

	// Token: 0x0400026B RID: 619
	public Color highlightColor = new Color(0.59607846f, 1f, 0.2f, 1f);

	// Token: 0x0400026C RID: 620
	public bool isAnimated = true;

	// Token: 0x0400026D RID: 621
	public bool isLocalized;

	// Token: 0x0400026E RID: 622
	public GameObject eventReceiver;

	// Token: 0x0400026F RID: 623
	public string functionName = "OnSelectionChange";

	// Token: 0x04000270 RID: 624
	public UIPopupList.OnSelectionChange onSelectionChange;

	// Token: 0x04000271 RID: 625
	[HideInInspector]
	[SerializeField]
	private string mSelectedItem;

	// Token: 0x04000272 RID: 626
	private UIPanel mPanel;

	// Token: 0x04000273 RID: 627
	private GameObject mChild;

	// Token: 0x04000274 RID: 628
	private UISprite mBackground;

	// Token: 0x04000275 RID: 629
	private UISprite mHighlight;

	// Token: 0x04000276 RID: 630
	private UILabel mHighlightedLabel;

	// Token: 0x04000277 RID: 631
	private List<UILabel> mLabelList = new List<UILabel>();

	// Token: 0x04000278 RID: 632
	private float mBgBorder;

	// Token: 0x02000040 RID: 64
	public enum Position
	{
		// Token: 0x0400027A RID: 634
		Auto,
		// Token: 0x0400027B RID: 635
		Above,
		// Token: 0x0400027C RID: 636
		Below
	}

	// Token: 0x020001E1 RID: 481
	// (Invoke) Token: 0x06000DA1 RID: 3489
	public delegate void OnSelectionChange(string item);
}
