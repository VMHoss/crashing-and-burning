using System;
using System.Collections.Generic;
using Mono.Xml;
using Tests;
using Unibill.Impl;
using unibill.WP8;
using Uniject;
using Uniject.Impl;
using UnityEngine;

namespace Unibill
{
	// Token: 0x02000030 RID: 48
	public class Biller : IBillingServiceCallback
	{
		// Token: 0x0600018E RID: 398 RVA: 0x00006B00 File Offset: 0x00004D00
		public Biller(InventoryDatabase db, TransactionDatabase tDb, IBillingService billingSubsystem, ILogger logger, HelpCentre help, ProductIdRemapper remapper)
		{
			this.InventoryDatabase = db;
			this.transactionDatabase = tDb;
			this.billingSubsystem = billingSubsystem;
			this.logger = logger;
			logger.prefix = "UnibillBiller";
			this.help = help;
			this.Errors = new List<UnibillError>();
			this.remapper = remapper;
		}

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x0600018F RID: 399 RVA: 0x00006B64 File Offset: 0x00004D64
		// (remove) Token: 0x06000190 RID: 400 RVA: 0x00006B80 File Offset: 0x00004D80
		public event Action<bool> onBillerReady;

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x06000191 RID: 401 RVA: 0x00006B9C File Offset: 0x00004D9C
		// (remove) Token: 0x06000192 RID: 402 RVA: 0x00006BB8 File Offset: 0x00004DB8
		public event Action<PurchasableItem> onPurchaseComplete;

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x06000193 RID: 403 RVA: 0x00006BD4 File Offset: 0x00004DD4
		// (remove) Token: 0x06000194 RID: 404 RVA: 0x00006BF0 File Offset: 0x00004DF0
		public event Action<bool> onTransactionsRestored;

		// Token: 0x1400001A RID: 26
		// (add) Token: 0x06000195 RID: 405 RVA: 0x00006C0C File Offset: 0x00004E0C
		// (remove) Token: 0x06000196 RID: 406 RVA: 0x00006C28 File Offset: 0x00004E28
		public event Action<PurchasableItem> onPurchaseCancelled;

		// Token: 0x1400001B RID: 27
		// (add) Token: 0x06000197 RID: 407 RVA: 0x00006C44 File Offset: 0x00004E44
		// (remove) Token: 0x06000198 RID: 408 RVA: 0x00006C60 File Offset: 0x00004E60
		public event Action<PurchasableItem> onPurchaseRefunded;

		// Token: 0x1400001C RID: 28
		// (add) Token: 0x06000199 RID: 409 RVA: 0x00006C7C File Offset: 0x00004E7C
		// (remove) Token: 0x0600019A RID: 410 RVA: 0x00006C98 File Offset: 0x00004E98
		public event Action<PurchasableItem> onPurchaseFailed;

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600019B RID: 411 RVA: 0x00006CB4 File Offset: 0x00004EB4
		// (set) Token: 0x0600019C RID: 412 RVA: 0x00006CBC File Offset: 0x00004EBC
		public InventoryDatabase InventoryDatabase { get; private set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600019D RID: 413 RVA: 0x00006CC8 File Offset: 0x00004EC8
		// (set) Token: 0x0600019E RID: 414 RVA: 0x00006CD0 File Offset: 0x00004ED0
		public IBillingService billingSubsystem { get; private set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600019F RID: 415 RVA: 0x00006CDC File Offset: 0x00004EDC
		// (set) Token: 0x060001A0 RID: 416 RVA: 0x00006CE4 File Offset: 0x00004EE4
		public BillerState State { get; private set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x00006CF0 File Offset: 0x00004EF0
		// (set) Token: 0x060001A2 RID: 418 RVA: 0x00006CF8 File Offset: 0x00004EF8
		public List<UnibillError> Errors { get; private set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x00006D04 File Offset: 0x00004F04
		public bool Ready
		{
			get
			{
				return this.State == BillerState.INITIALISED || this.State == BillerState.INITIALISED_WITH_ERROR;
			}
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00006D20 File Offset: 0x00004F20
		public void Initialise()
		{
			if (this.InventoryDatabase.AllPurchasableItems.Count == 0)
			{
				this.logError(UnibillError.UNIBILL_NO_PRODUCTS_DEFINED);
				this.onSetupComplete(false);
				return;
			}
			this.billingSubsystem.initialise(this);
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00006D60 File Offset: 0x00004F60
		public int getPurchaseHistory(PurchasableItem item)
		{
			return this.transactionDatabase.getPurchaseHistory(item);
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00006D70 File Offset: 0x00004F70
		public int getPurchaseHistory(string purchasableId)
		{
			return this.getPurchaseHistory(this.InventoryDatabase.getItemById(purchasableId));
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00006D84 File Offset: 0x00004F84
		public string[] getReceiptsForPurchasable(PurchasableItem item)
		{
			if (this.receiptMap.ContainsKey(item))
			{
				return this.receiptMap[item].ToArray();
			}
			return new string[0];
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00006DBC File Offset: 0x00004FBC
		public void purchase(PurchasableItem item)
		{
			if (this.State == BillerState.INITIALISING)
			{
				this.logError(UnibillError.BILLER_NOT_READY);
				return;
			}
			if (this.State == BillerState.INITIALISED_WITH_CRITICAL_ERROR)
			{
				this.logError(UnibillError.UNIBILL_INITIALISE_FAILED_WITH_CRITICAL_ERROR);
				return;
			}
			if (item == null)
			{
				this.logger.LogError("Trying to purchase null PurchasableItem", new object[0]);
				return;
			}
			if (item.PurchaseType == PurchaseType.NonConsumable && this.transactionDatabase.getPurchaseHistory(item) > 0)
			{
				this.logError(UnibillError.UNIBILL_ATTEMPTING_TO_PURCHASE_ALREADY_OWNED_NON_CONSUMABLE);
				return;
			}
			this.billingSubsystem.purchase(this.remapper.mapItemIdToPlatformSpecificId(item));
			this.logger.Log("purchase({0})", new object[]
			{
				item.Id
			});
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00006E6C File Offset: 0x0000506C
		public void purchase(string purchasableId)
		{
			PurchasableItem itemById = this.InventoryDatabase.getItemById(purchasableId);
			if (itemById == null)
			{
				this.logger.LogWarning("Unable to purchase unknown item with id: {0}", new object[]
				{
					purchasableId
				});
			}
			this.purchase(itemById);
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00006EB0 File Offset: 0x000050B0
		public void restoreTransactions()
		{
			this.logger.Log("restoreTransactions()");
			if (!this.Ready)
			{
				this.logError(UnibillError.BILLER_NOT_READY);
				return;
			}
			this.billingSubsystem.restoreTransactions();
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00006EEC File Offset: 0x000050EC
		public void onPurchaseSucceeded(string id)
		{
			if (!this.verifyPlatformId(id))
			{
				return;
			}
			PurchasableItem purchasableItemFromPlatformSpecificId = this.remapper.getPurchasableItemFromPlatformSpecificId(id);
			this.logger.Log("onPurchaseSucceeded({0})", new object[]
			{
				purchasableItemFromPlatformSpecificId.Id
			});
			this.transactionDatabase.onPurchase(purchasableItemFromPlatformSpecificId);
			if (this.onPurchaseComplete != null)
			{
				this.onPurchaseComplete(purchasableItemFromPlatformSpecificId);
			}
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00006F58 File Offset: 0x00005158
		public void onPurchaseSucceeded(string platformSpecificId, string receipt)
		{
			if (receipt != null && receipt.Length > 0)
			{
				PurchasableItem purchasableItemFromPlatformSpecificId = this.remapper.getPurchasableItemFromPlatformSpecificId(platformSpecificId);
				if (!this.receiptMap.ContainsKey(purchasableItemFromPlatformSpecificId))
				{
					this.receiptMap.Add(purchasableItemFromPlatformSpecificId, new List<string>());
				}
				this.receiptMap[purchasableItemFromPlatformSpecificId].Add(receipt);
			}
			this.onPurchaseSucceeded(platformSpecificId);
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00006FC0 File Offset: 0x000051C0
		public void onSetupComplete(bool available)
		{
			this.logger.Log("onSetupComplete({0})", new object[]
			{
				available
			});
			this.State = ((!available) ? BillerState.INITIALISED_WITH_CRITICAL_ERROR : ((this.Errors.Count <= 0) ? BillerState.INITIALISED : BillerState.INITIALISED_WITH_ERROR));
			if (this.onBillerReady != null)
			{
				this.onBillerReady(this.Ready);
			}
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00007034 File Offset: 0x00005234
		public void onPurchaseCancelledEvent(string id)
		{
			if (!this.verifyPlatformId(id))
			{
				return;
			}
			PurchasableItem purchasableItemFromPlatformSpecificId = this.remapper.getPurchasableItemFromPlatformSpecificId(id);
			this.logger.Log("onPurchaseCancelledEvent({0})", new object[]
			{
				purchasableItemFromPlatformSpecificId.Id
			});
			if (this.onPurchaseCancelled != null)
			{
				this.onPurchaseCancelled(purchasableItemFromPlatformSpecificId);
			}
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00007094 File Offset: 0x00005294
		public void onPurchaseRefundedEvent(string id)
		{
			if (!this.verifyPlatformId(id))
			{
				return;
			}
			PurchasableItem purchasableItemFromPlatformSpecificId = this.remapper.getPurchasableItemFromPlatformSpecificId(id);
			this.logger.Log("onPurchaseRefundedEvent({0})", new object[]
			{
				purchasableItemFromPlatformSpecificId.Id
			});
			this.transactionDatabase.onRefunded(purchasableItemFromPlatformSpecificId);
			if (this.onPurchaseRefunded != null)
			{
				this.onPurchaseRefunded(purchasableItemFromPlatformSpecificId);
			}
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00007100 File Offset: 0x00005300
		public void onPurchaseFailedEvent(string id)
		{
			if (!this.verifyPlatformId(id))
			{
				return;
			}
			PurchasableItem purchasableItemFromPlatformSpecificId = this.remapper.getPurchasableItemFromPlatformSpecificId(id);
			this.logger.Log("onPurchaseFailedEvent({0})", new object[]
			{
				purchasableItemFromPlatformSpecificId.Id
			});
			if (this.onPurchaseFailed != null)
			{
				this.onPurchaseFailed(purchasableItemFromPlatformSpecificId);
			}
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00007160 File Offset: 0x00005360
		public void onTransactionsRestoredSuccess()
		{
			this.logger.Log("onTransactionsRestoredSuccess()");
			if (this.onTransactionsRestored != null)
			{
				this.onTransactionsRestored(true);
			}
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x0000718C File Offset: 0x0000538C
		public void ClearPurchases()
		{
			foreach (PurchasableItem item in this.InventoryDatabase.AllPurchasableItems)
			{
				this.transactionDatabase.clearPurchases(item);
			}
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x000071FC File Offset: 0x000053FC
		public void onTransactionsRestoredFail(string error)
		{
			this.logger.Log("onTransactionsRestoredFail({0})", new object[]
			{
				error
			});
			this.onTransactionsRestored(false);
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00007230 File Offset: 0x00005430
		public void logError(UnibillError error)
		{
			this.logError(error, new object[0]);
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00007240 File Offset: 0x00005440
		public void logError(UnibillError error, params object[] args)
		{
			this.Errors.Add(error);
			this.logger.LogError(this.help.getMessage(error), args);
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00007274 File Offset: 0x00005474
		public static Biller instantiate()
		{
			IBillingService billingSubsystem = Biller.instantiateBillingSubsystem();
			return new Biller(Biller.getInventory(), Biller.getTransactionDatabase(), billingSubsystem, Biller.getLogger(), Biller.getHelp(), Biller.getMapper());
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x000072A8 File Offset: 0x000054A8
		private static TransactionDatabase getTransactionDatabase()
		{
			if (Biller._tDb == null)
			{
				Biller._tDb = new TransactionDatabase(Biller.getStorage(), Biller.getLogger());
			}
			return Biller._tDb;
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x000072D0 File Offset: 0x000054D0
		private static IStorage getStorage()
		{
			if (Biller._storage == null)
			{
				Biller._storage = new UnityPlayerPrefsStorage();
			}
			return Biller._storage;
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x000072EC File Offset: 0x000054EC
		private bool verifyPlatformId(string platformId)
		{
			if (!this.remapper.canMapProductSpecificId(platformId))
			{
				this.logError(UnibillError.UNIBILL_UNKNOWN_PRODUCTID, new object[]
				{
					platformId
				});
				return false;
			}
			return true;
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00007320 File Offset: 0x00005520
		private static IBillingService instantiateBillingSubsystem()
		{
			if (Application.platform == RuntimePlatform.WindowsPlayer || Application.isEditor)
			{
				return new FakeBillingService(Biller.getMapper());
			}
			switch (Biller.getConfig().CurrentPlatform)
			{
			case BillingPlatform.GooglePlay:
				return new GooglePlayBillingService(Biller.getGooglePlay(), Biller.getConfig(), Biller.getMapper(), Biller.getInventory(), Biller.getLogger());
			case BillingPlatform.AmazonAppstore:
				return new AmazonAppStoreBillingService(Biller.getAmazon(), Biller.getMapper(), Biller.getInventory(), Biller.getTransactionDatabase(), Biller.getLogger());
			case BillingPlatform.AppleAppStore:
				return new AppleAppStoreBillingService(Biller.getInventory(), Biller.getMapper(), Biller.getStorekit());
			case BillingPlatform.MacAppStore:
				return new AppleAppStoreBillingService(Biller.getInventory(), Biller.getMapper(), Biller.getStorekit());
			case BillingPlatform.WindowsPhone8:
			{
				WP8BillingService wp8BillingService = new WP8BillingService(Factory.Create(Biller.getConfig().WP8SandboxEnabled), Biller.getInventory(), Biller.getMapper(), Biller.getTransactionDatabase(), Biller.getLogger());
				new GameObject().AddComponent<WP8Eventhook>().callback = wp8BillingService;
				return wp8BillingService;
			}
			default:
				throw new ArgumentException(Biller.getConfig().CurrentPlatform.ToString());
			}
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00007434 File Offset: 0x00005634
		private static IRawGooglePlayInterface getGooglePlay()
		{
			if (Application.isEditor)
			{
				return new FakeGooglePlayPlugin(Biller.getInventory(), Biller.getMapper());
			}
			return new RawGooglePlayInterface();
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00007458 File Offset: 0x00005658
		private static IRawAmazonAppStoreBillingInterface getAmazon()
		{
			IRawAmazonAppStoreBillingInterface result;
			if (Application.isEditor)
			{
				IRawAmazonAppStoreBillingInterface rawAmazonAppStoreBillingInterface = new FakeRawAmazonAppStoreBillingInterface();
				result = rawAmazonAppStoreBillingInterface;
			}
			else
			{
				result = new RawAmazonAppStoreBillingInterface(Biller.getConfig());
			}
			return result;
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00007488 File Offset: 0x00005688
		private static IStoreKitPlugin getStorekit()
		{
			if (Application.isEditor)
			{
				return new FakeStorekitPlugin(Biller.getInventory(), Biller.getMapper());
			}
			if (Application.platform == RuntimePlatform.IPhonePlayer)
			{
				return new StoreKitPluginImpl();
			}
			return new OSXStoreKitPluginImpl();
		}

		// Token: 0x060001BE RID: 446 RVA: 0x000074C8 File Offset: 0x000056C8
		private static HelpCentre getHelp()
		{
			if (Biller._helpCentre == null)
			{
				Biller._helpCentre = new HelpCentre(Biller.getParser());
			}
			return Biller._helpCentre;
		}

		// Token: 0x060001BF RID: 447 RVA: 0x000074E8 File Offset: 0x000056E8
		private static InventoryDatabase getInventory()
		{
			if (Biller._inventory == null)
			{
				Biller._inventory = new InventoryDatabase(Biller.getParser(), Biller.getLogger());
			}
			return Biller._inventory;
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00007510 File Offset: 0x00005710
		private static ProductIdRemapper getMapper()
		{
			if (Biller._remapper == null)
			{
				Biller._remapper = new ProductIdRemapper(Biller.getInventory(), Biller.getParser(), Biller.getConfig());
			}
			return Biller._remapper;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00007548 File Offset: 0x00005748
		private static ILogger getLogger()
		{
			return new UnityLogger();
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00007550 File Offset: 0x00005750
		private static UnibillXmlParser getParser()
		{
			return new UnibillXmlParser(new SmallXmlParser(), Biller.getResourceLoader());
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00007564 File Offset: 0x00005764
		private static UnibillConfiguration getConfig()
		{
			if (Biller._config == null)
			{
				Biller._config = new UnibillConfiguration(Biller.getResourceLoader(), Biller.getParser(), Biller.getUtil(), Biller.getLogger());
			}
			return Biller._config;
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x000075A0 File Offset: 0x000057A0
		private static IUtil getUtil()
		{
			return new UnityUtil();
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x000075A8 File Offset: 0x000057A8
		private static IResourceLoader getResourceLoader()
		{
			if (Biller._resourceLoader == null)
			{
				Biller._resourceLoader = new UnityResourceLoader();
			}
			return Biller._resourceLoader;
		}

		// Token: 0x04000080 RID: 128
		private TransactionDatabase transactionDatabase;

		// Token: 0x04000081 RID: 129
		private ILogger logger;

		// Token: 0x04000082 RID: 130
		private HelpCentre help;

		// Token: 0x04000083 RID: 131
		private ProductIdRemapper remapper;

		// Token: 0x04000084 RID: 132
		private Dictionary<PurchasableItem, List<string>> receiptMap = new Dictionary<PurchasableItem, List<string>>();

		// Token: 0x04000085 RID: 133
		private static TransactionDatabase _tDb;

		// Token: 0x04000086 RID: 134
		private static IStorage _storage;

		// Token: 0x04000087 RID: 135
		private static HelpCentre _helpCentre;

		// Token: 0x04000088 RID: 136
		private static InventoryDatabase _inventory;

		// Token: 0x04000089 RID: 137
		private static ProductIdRemapper _remapper;

		// Token: 0x0400008A RID: 138
		private static UnibillConfiguration _config;

		// Token: 0x0400008B RID: 139
		private static IResourceLoader _resourceLoader;
	}
}
