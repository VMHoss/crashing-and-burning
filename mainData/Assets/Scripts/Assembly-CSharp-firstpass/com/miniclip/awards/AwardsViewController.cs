using System;
using System.Collections.Generic;
using UnityEngine;

namespace com.miniclip.awards
{
	// Token: 0x02000054 RID: 84
	public class AwardsViewController : AbstractUpdateable
	{
		// Token: 0x060002C9 RID: 713 RVA: 0x0000A754 File Offset: 0x00008954
		public AwardsViewController()
		{
			this.Init();
		}

		// Token: 0x14000024 RID: 36
		// (add) Token: 0x060002CA RID: 714 RVA: 0x0000A764 File Offset: 0x00008964
		// (remove) Token: 0x060002CB RID: 715 RVA: 0x0000A780 File Offset: 0x00008980
		internal override event EventHandler<UpdateEventArgs> updatePoolRequested;

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060002CC RID: 716 RVA: 0x0000A79C File Offset: 0x0000899C
		internal int AwardDataWaitingCount
		{
			get
			{
				return this._awardDataWaiting.Count;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060002CD RID: 717 RVA: 0x0000A7AC File Offset: 0x000089AC
		internal int NotificationsWaitingCount
		{
			get
			{
				return this._notificationsWaiting.Count;
			}
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000A7BC File Offset: 0x000089BC
		private void Init()
		{
			this._notificationsWaiting = new Queue<AwardNotification>();
			this._awardDataWaiting = new Queue<AwardData>();
			this.InstantiateGameObjects();
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000A7DC File Offset: 0x000089DC
		private void InstantiateGameObjects()
		{
			this.CreateGuiCamera();
			this.CreateAwardNotifications();
			this._objectsActivated = false;
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000A7F4 File Offset: 0x000089F4
		private void SetObjectActivation(bool active)
		{
			this._guiCam.SetActive(active);
			this._objectsActivated = active;
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000A80C File Offset: 0x00008A0C
		private void CreateGuiCamera()
		{
			this._guiCam = (UnityEngine.Object.Instantiate(Resources.Load("Prefabs/GuiCam")) as GameObject);
			if (this._guiCam == null)
			{
				return;
			}
			Debug.Log("-> Got the GUI Cam");
			this._guiCam.name = "GuiCam";
			this._guiCam.SetActive(false);
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000A86C File Offset: 0x00008A6C
		private void CreateAwardNotifications()
		{
			this._awardNotifications = new AwardNotification[4];
			this._awardSlots = new AwardSlot[3];
			Vector3 position = this._guiCam.transform.position;
			AwardNotification.offscreen = new Vector3(position.x, position.y - 11f, position.z + 5f);
			for (int i = 0; i < 4; i++)
			{
				this._awardNotifications[i] = this.ConstructAwardNotification(i);
				if (this._awardNotifications[i] == null)
				{
					Debug.LogError("-> AwardsViewController::CreateAwardNotifications() - _awardNotifications[" + i + "] is NULL :(");
				}
			}
			for (int j = 0; j < 3; j++)
			{
				Vector3 position2 = new Vector3(AwardNotification.offscreen.x, position.y - 8f + (float)j * 2.2f, AwardNotification.offscreen.z);
				this._awardSlots[j] = new AwardSlot(position2);
			}
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000A96C File Offset: 0x00008B6C
		private AwardNotification ConstructAwardNotification(int id)
		{
			GameObject gameObject = new GameObject();
			GameObject gameObject2 = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/AwardPanel")) as GameObject;
			GameObject gameObject3 = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/AwardIcon")) as GameObject;
			GameObject gameObject4 = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/txtAwardEarned")) as GameObject;
			GameObject gameObject5 = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/txtAwardDescription")) as GameObject;
			gameObject2.name = "AwardPanel";
			gameObject3.name = "AwardIcon";
			gameObject4.name = "txtAwardEarned";
			gameObject5.name = "txtAwardDescription";
			gameObject.transform.position = new Vector3(0f, 0f, 0f);
			gameObject2.transform.position = new Vector3(0f, 0f, 0f);
			gameObject3.transform.position = new Vector3(-3f, 0f, -0.5f);
			gameObject4.transform.position = new Vector3(-1.7f, 0.7f, -0.5f);
			gameObject5.transform.position = new Vector3(-1.7f, 0f, -0.5f);
			gameObject2.transform.parent = gameObject.transform;
			gameObject3.transform.parent = gameObject.transform;
			gameObject4.transform.parent = gameObject.transform;
			gameObject5.transform.parent = gameObject.transform;
			AwardNotification awardNotification = gameObject.AddComponent<AwardNotification>();
			awardNotification.SlotRequested += this.OnSlotRequested;
			awardNotification.name = "AwardNotification" + id.ToString();
			awardNotification.transform.position = new Vector3(AwardNotification.offscreen.x, AwardNotification.offscreen.y, AwardNotification.offscreen.z);
			awardNotification.gameObject.layer = 8;
			return awardNotification;
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0000AB54 File Offset: 0x00008D54
		public void AddAwardData(AwardData awardData)
		{
			if (!this._objectsActivated)
			{
				this.SetObjectActivation(true);
			}
			this.ChangeUpdatePool(0);
			this._awardDataWaiting.Enqueue(awardData);
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0000AB7C File Offset: 0x00008D7C
		internal override void Update()
		{
			if (this._awardDataWaiting.Count > 0)
			{
				this._notification = this.LookForAvailableNotification();
				if (this._notification != null)
				{
					this._awardData = this._awardDataWaiting.Dequeue();
					this._notification.AwardData = this._awardData;
				}
				this._awardData = null;
				this._notification = null;
			}
			if (this._notificationsWaiting.Count > 0)
			{
				this._slot = this.LookForAvailableSlot();
				if (this._slot != null)
				{
					this._notification = this._notificationsWaiting.Dequeue();
					this._slot.UsedBy = this._notification;
					this._notification.AllocatedSlot = this._slot;
				}
			}
			if (this._awardDataWaiting.Count < 1 && this._notificationsWaiting.Count < 1 && this.HaveAllNotificationsFinished())
			{
				this.SetObjectActivation(false);
				this.ChangeUpdatePool(-1);
			}
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000AC80 File Offset: 0x00008E80
		private bool HaveAllNotificationsFinished()
		{
			for (int i = 0; i < this._awardNotifications.Length; i++)
			{
				if (!this._awardNotifications[i].IsAvailable)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0000ACBC File Offset: 0x00008EBC
		private AwardNotification LookForAvailableNotification()
		{
			for (int i = 0; i < this._awardNotifications.Length; i++)
			{
				if (this._awardNotifications[i].IsAvailable)
				{
					return this._awardNotifications[i];
				}
			}
			return null;
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000AD00 File Offset: 0x00008F00
		private AwardSlot LookForAvailableSlot()
		{
			for (int i = 0; i < 3; i++)
			{
				if (this._awardSlots[i].IsAvailable)
				{
					return this._awardSlots[i];
				}
			}
			return null;
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000AD3C File Offset: 0x00008F3C
		internal void ChangeUpdatePool(int updatePoolId)
		{
			this.updatePoolRequested(this, new UpdateEventArgs(updatePoolId));
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000AD50 File Offset: 0x00008F50
		internal void OnSlotRequested(object sender, EventArgs args)
		{
			Debug.Log("AwardsService::OnSlotRequested(..)");
			AwardNotification item = sender as AwardNotification;
			this._notificationsWaiting.Enqueue(item);
			Debug.Log("AwardsService::OnAwardSlotRequested(..) Notification Count: " + this._notificationsWaiting.Count);
		}

		// Token: 0x04000129 RID: 297
		private const float AWARD_SEPARATION = 2.2f;

		// Token: 0x0400012A RID: 298
		private const int MAX_AWARDS_DISPLAYED = 3;

		// Token: 0x0400012B RID: 299
		private const int NUM_AWARD_NOTIFICATIONS = 4;

		// Token: 0x0400012C RID: 300
		private Queue<AwardNotification> _notificationsWaiting;

		// Token: 0x0400012D RID: 301
		private Queue<AwardData> _awardDataWaiting;

		// Token: 0x0400012E RID: 302
		private AwardNotification[] _awardNotifications;

		// Token: 0x0400012F RID: 303
		private AwardSlot[] _awardSlots;

		// Token: 0x04000130 RID: 304
		private GameObject _guiCam;

		// Token: 0x04000131 RID: 305
		private bool _objectsActivated;

		// Token: 0x04000132 RID: 306
		private AwardData _awardData;

		// Token: 0x04000133 RID: 307
		private AwardSlot _slot;

		// Token: 0x04000134 RID: 308
		private AwardNotification _notification;
	}
}
