using System;
using Unibill.Demo;
using UnityEngine;

// Token: 0x020001B0 RID: 432
[AddComponentMenu("Unibill/UnibillDemo")]
public class UnibillDemo : MonoBehaviour
{
	// Token: 0x06000CD7 RID: 3287 RVA: 0x0005F2F8 File Offset: 0x0005D4F8
	private void Start()
	{
		if (Resources.Load("unibillInventory") == null)
		{
			Debug.LogError("You must define your purchasable inventory within the inventory editor!");
			base.gameObject.SetActive(false);
			return;
		}
		Unibiller.onBillerReady += this.onBillerReady;
		Unibiller.onTransactionsRestored += this.onTransactionsRestored;
		Unibiller.onPurchaseCancelled += this.onCancelled;
		Unibiller.onPurchaseFailed += this.onFailed;
		Unibiller.onPurchaseComplete += this.onPurchased;
		Unibiller.Initialise();
		this.initCombobox();
	}

	// Token: 0x06000CD8 RID: 3288 RVA: 0x0005F394 File Offset: 0x0005D594
	private void onBillerReady(UnibillState state)
	{
		Debug.Log("onBillerReady:" + state);
	}

	// Token: 0x06000CD9 RID: 3289 RVA: 0x0005F3AC File Offset: 0x0005D5AC
	private void onTransactionsRestored(bool success)
	{
		Debug.Log("Transactions restored.");
	}

	// Token: 0x06000CDA RID: 3290 RVA: 0x0005F3B8 File Offset: 0x0005D5B8
	private void onPurchased(PurchasableItem item)
	{
		Debug.Log("Purchase OK: " + item.Id);
		Debug.Log(string.Format("{0} has now been purchased {1} times.", item.name, Unibiller.GetPurchaseCount(item)));
	}

	// Token: 0x06000CDB RID: 3291 RVA: 0x0005F3FC File Offset: 0x0005D5FC
	private void onCancelled(PurchasableItem item)
	{
		Debug.Log("Purchase cancelled: " + item.Id);
	}

	// Token: 0x06000CDC RID: 3292 RVA: 0x0005F414 File Offset: 0x0005D614
	private void onFailed(PurchasableItem item)
	{
		Debug.Log("Purchase failed: " + item.Id);
	}

	// Token: 0x06000CDD RID: 3293 RVA: 0x0005F42C File Offset: 0x0005D62C
	private void initCombobox()
	{
		this.box = new ComboBox();
		this.items = Unibiller.AllPurchasableItems;
		this.comboBoxList = new GUIContent[this.items.Length];
		for (int i = 0; i < this.items.Length; i++)
		{
			this.comboBoxList[i] = new GUIContent(string.Format("{0} - {1}", this.items[i].localizedTitle, this.items[i].localizedPriceString));
		}
		this.listStyle = new GUIStyle();
		this.listStyle.font = this.font;
		this.listStyle.normal.textColor = Color.white;
		GUIStyleState onHover = this.listStyle.onHover;
		Texture2D background = new Texture2D(2, 2);
		this.listStyle.hover.background = background;
		onHover.background = background;
		RectOffset padding = this.listStyle.padding;
		int num = 4;
		this.listStyle.padding.bottom = num;
		num = num;
		this.listStyle.padding.top = num;
		num = num;
		this.listStyle.padding.right = num;
		padding.left = num;
	}

	// Token: 0x06000CDE RID: 3294 RVA: 0x0005F554 File Offset: 0x0005D754
	public void Update()
	{
		for (int i = 0; i < this.items.Length; i++)
		{
			this.comboBoxList[i] = new GUIContent(string.Format("{0} - {1} - {2}", this.items[i].name, this.items[i].localizedTitle, this.items[i].localizedPriceString));
		}
	}

	// Token: 0x06000CDF RID: 3295 RVA: 0x0005F5B8 File Offset: 0x0005D7B8
	private void OnGUI()
	{
		this.selectedItemIndex = this.box.GetSelectedItemIndex();
		this.selectedItemIndex = this.box.List(new Rect(0f, 0f, (float)Screen.width, (float)Screen.width / 20f), this.comboBoxList[this.selectedItemIndex].text, this.comboBoxList, this.listStyle);
		if (GUI.Button(new Rect(0f, (float)Screen.height - (float)Screen.width / 6f, (float)Screen.width / 2f, (float)Screen.width / 6f), "Buy"))
		{
			Unibiller.initiatePurchase(this.items[this.selectedItemIndex]);
		}
		if (GUI.Button(new Rect((float)Screen.width / 2f, (float)Screen.height - (float)Screen.width / 6f, (float)Screen.width / 2f, (float)Screen.width / 6f), "Restore transactions"))
		{
			Unibiller.restoreTransactions();
		}
		int num = (int)((float)Screen.height - (float)Screen.width / 6f) - 50;
		foreach (PurchasableItem purchasableItem in this.items)
		{
			GUI.Label(new Rect(0f, (float)num, 500f, 50f), purchasableItem.Id, this.listStyle);
			num -= 30;
		}
		GUI.Label(new Rect(0f, (float)(num - 10), 500f, 50f), "Purchasable Item", this.listStyle);
		num = (int)((float)Screen.height - (float)Screen.width / 6f) - 50;
		foreach (PurchasableItem item in this.items)
		{
			GUI.Label(new Rect((float)Screen.width - (float)Screen.width * 0.1f, (float)num, 500f, 50f), Unibiller.GetPurchaseCount(item).ToString(), this.listStyle);
			num -= 30;
		}
		GUI.Label(new Rect((float)Screen.width - (float)Screen.width * 0.2f, (float)(num - 10), 500f, 50f), "Count", this.listStyle);
	}

	// Token: 0x04000C95 RID: 3221
	private ComboBox box;

	// Token: 0x04000C96 RID: 3222
	private GUIContent[] comboBoxList;

	// Token: 0x04000C97 RID: 3223
	private GUIStyle listStyle;

	// Token: 0x04000C98 RID: 3224
	private int selectedItemIndex;

	// Token: 0x04000C99 RID: 3225
	private PurchasableItem[] items;

	// Token: 0x04000C9A RID: 3226
	public Font font;
}
