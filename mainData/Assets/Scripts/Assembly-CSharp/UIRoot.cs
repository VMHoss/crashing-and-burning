using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000089 RID: 137
[AddComponentMenu("NGUI/UI/Root")]
[ExecuteInEditMode]
public class UIRoot : MonoBehaviour
{
	// Token: 0x170000C0 RID: 192
	// (get) Token: 0x0600048B RID: 1163 RVA: 0x0001F9DC File Offset: 0x0001DBDC
	public static List<UIRoot> list
	{
		get
		{
			return UIRoot.mRoots;
		}
	}

	// Token: 0x170000C1 RID: 193
	// (get) Token: 0x0600048C RID: 1164 RVA: 0x0001F9E4 File Offset: 0x0001DBE4
	public int activeHeight
	{
		get
		{
			int num = Mathf.Max(2, Screen.height);
			if (this.scalingStyle == UIRoot.Scaling.FixedSize)
			{
				return this.manualHeight;
			}
			if (this.scalingStyle == UIRoot.Scaling.FixedSizeOnMobiles)
			{
				return this.manualHeight;
			}
			if (num < this.minimumHeight)
			{
				return this.minimumHeight;
			}
			if (num > this.maximumHeight)
			{
				return this.maximumHeight;
			}
			return num;
		}
	}

	// Token: 0x170000C2 RID: 194
	// (get) Token: 0x0600048D RID: 1165 RVA: 0x0001FA4C File Offset: 0x0001DC4C
	public float pixelSizeAdjustment
	{
		get
		{
			return this.GetPixelSizeAdjustment(Screen.height);
		}
	}

	// Token: 0x0600048E RID: 1166 RVA: 0x0001FA5C File Offset: 0x0001DC5C
	public static float GetPixelSizeAdjustment(GameObject go)
	{
		UIRoot uiroot = NGUITools.FindInParents<UIRoot>(go);
		return (!(uiroot != null)) ? 1f : uiroot.pixelSizeAdjustment;
	}

	// Token: 0x0600048F RID: 1167 RVA: 0x0001FA8C File Offset: 0x0001DC8C
	public float GetPixelSizeAdjustment(int height)
	{
		height = Mathf.Max(2, height);
		if (this.scalingStyle == UIRoot.Scaling.FixedSize)
		{
			return (float)this.manualHeight / (float)height;
		}
		if (this.scalingStyle == UIRoot.Scaling.FixedSizeOnMobiles)
		{
			return (float)this.manualHeight / (float)height;
		}
		if (height < this.minimumHeight)
		{
			return (float)this.minimumHeight / (float)height;
		}
		if (height > this.maximumHeight)
		{
			return (float)this.maximumHeight / (float)height;
		}
		return 1f;
	}

	// Token: 0x06000490 RID: 1168 RVA: 0x0001FB04 File Offset: 0x0001DD04
	private void Awake()
	{
		this.mTrans = base.transform;
		UIRoot.mRoots.Add(this);
		if (this.automatic)
		{
			this.scalingStyle = UIRoot.Scaling.PixelPerfect;
			this.automatic = false;
		}
	}

	// Token: 0x06000491 RID: 1169 RVA: 0x0001FB44 File Offset: 0x0001DD44
	private void OnDestroy()
	{
		UIRoot.mRoots.Remove(this);
	}

	// Token: 0x06000492 RID: 1170 RVA: 0x0001FB54 File Offset: 0x0001DD54
	private void Start()
	{
		UIOrthoCamera componentInChildren = base.GetComponentInChildren<UIOrthoCamera>();
		if (componentInChildren != null)
		{
			Debug.LogWarning("UIRoot should not be active at the same time as UIOrthoCamera. Disabling UIOrthoCamera.", componentInChildren);
			Camera component = componentInChildren.gameObject.GetComponent<Camera>();
			componentInChildren.enabled = false;
			if (component != null)
			{
				component.orthographicSize = 1f;
			}
		}
	}

	// Token: 0x06000493 RID: 1171 RVA: 0x0001FBAC File Offset: 0x0001DDAC
	private void Update()
	{
		if (this.mTrans != null)
		{
			float num = (float)this.activeHeight;
			if (num > 0f)
			{
				float num2 = 2f / num;
				Vector3 localScale = this.mTrans.localScale;
				if (Mathf.Abs(localScale.x - num2) > 1E-45f || Mathf.Abs(localScale.y - num2) > 1E-45f || Mathf.Abs(localScale.z - num2) > 1E-45f)
				{
					this.mTrans.localScale = new Vector3(num2, num2, num2);
				}
			}
		}
	}

	// Token: 0x06000494 RID: 1172 RVA: 0x0001FC4C File Offset: 0x0001DE4C
	public static void Broadcast(string funcName)
	{
		int i = 0;
		int count = UIRoot.mRoots.Count;
		while (i < count)
		{
			UIRoot uiroot = UIRoot.mRoots[i];
			if (uiroot != null)
			{
				uiroot.BroadcastMessage(funcName, SendMessageOptions.DontRequireReceiver);
			}
			i++;
		}
	}

	// Token: 0x06000495 RID: 1173 RVA: 0x0001FC98 File Offset: 0x0001DE98
	public static void Broadcast(string funcName, object param)
	{
		if (param == null)
		{
			Debug.LogError("SendMessage is bugged when you try to pass 'null' in the parameter field. It behaves as if no parameter was specified.");
		}
		else
		{
			int i = 0;
			int count = UIRoot.mRoots.Count;
			while (i < count)
			{
				UIRoot uiroot = UIRoot.mRoots[i];
				if (uiroot != null)
				{
					uiroot.BroadcastMessage(funcName, param, SendMessageOptions.DontRequireReceiver);
				}
				i++;
			}
		}
	}

	// Token: 0x040004A2 RID: 1186
	private static List<UIRoot> mRoots = new List<UIRoot>();

	// Token: 0x040004A3 RID: 1187
	public UIRoot.Scaling scalingStyle = UIRoot.Scaling.FixedSize;

	// Token: 0x040004A4 RID: 1188
	[HideInInspector]
	public bool automatic;

	// Token: 0x040004A5 RID: 1189
	public int manualHeight = 720;

	// Token: 0x040004A6 RID: 1190
	public int minimumHeight = 320;

	// Token: 0x040004A7 RID: 1191
	public int maximumHeight = 1536;

	// Token: 0x040004A8 RID: 1192
	private Transform mTrans;

	// Token: 0x0200008A RID: 138
	public enum Scaling
	{
		// Token: 0x040004AA RID: 1194
		PixelPerfect,
		// Token: 0x040004AB RID: 1195
		FixedSize,
		// Token: 0x040004AC RID: 1196
		FixedSizeOnMobiles
	}
}
