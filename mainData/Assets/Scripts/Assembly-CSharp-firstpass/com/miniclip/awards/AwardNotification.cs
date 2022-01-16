using System;
using UnityEngine;

namespace com.miniclip.awards
{
	// Token: 0x0200004A RID: 74
	public class AwardNotification : MonoBehaviour, IStateable
	{
		// Token: 0x14000023 RID: 35
		// (add) Token: 0x06000296 RID: 662 RVA: 0x00009F68 File Offset: 0x00008168
		// (remove) Token: 0x06000297 RID: 663 RVA: 0x00009F84 File Offset: 0x00008184
		internal event EventHandler<EventArgs> SlotRequested;

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000298 RID: 664 RVA: 0x00009FA0 File Offset: 0x000081A0
		// (set) Token: 0x06000299 RID: 665 RVA: 0x00009FA8 File Offset: 0x000081A8
		public AwardSlot AllocatedSlot
		{
			get
			{
				return this._allocatedSlot;
			}
			set
			{
				this._allocatedSlot = value;
				if (this._stateMachine.CurrentState.Id == "waiting_for_slot")
				{
					this._stateMachine.ChangeState("moving_in");
				}
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600029A RID: 666 RVA: 0x00009FE4 File Offset: 0x000081E4
		public uint AwardId
		{
			get
			{
				if (this._awardData == null)
				{
					return 0U;
				}
				return this._awardData.Id;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600029B RID: 667 RVA: 0x0000A000 File Offset: 0x00008200
		public string Name
		{
			get
			{
				if (this._awardData == null)
				{
					return string.Empty;
				}
				return this._awardData.Title;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600029C RID: 668 RVA: 0x0000A020 File Offset: 0x00008220
		public string Description
		{
			get
			{
				if (this._awardData == null)
				{
					return string.Empty;
				}
				return this._awardData.Description;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600029D RID: 669 RVA: 0x0000A040 File Offset: 0x00008240
		// (set) Token: 0x0600029E RID: 670 RVA: 0x0000A050 File Offset: 0x00008250
		public Texture IconTexture
		{
			get
			{
				return this._iconMaterial.mainTexture;
			}
			set
			{
				this._iconMaterial.mainTexture = value;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600029F RID: 671 RVA: 0x0000A060 File Offset: 0x00008260
		public StateMachine StateMachine
		{
			get
			{
				return this._stateMachine;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x0000A068 File Offset: 0x00008268
		// (set) Token: 0x060002A1 RID: 673 RVA: 0x0000A070 File Offset: 0x00008270
		public AwardData AwardData
		{
			get
			{
				return this._awardData;
			}
			set
			{
				this._awardData = value;
				this._awardEarned.text = this._awardData.Title;
				this._awardDescription.text = this._awardData.Description;
				this._stateMachine.ChangeState("loading_icon");
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x0000A0C4 File Offset: 0x000082C4
		public bool IsAvailable
		{
			get
			{
				return this._stateMachine.CurrentState.Id == "available";
			}
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000A0E0 File Offset: 0x000082E0
		private void Awake()
		{
			Debug.Log("-> AwardNotification::Awake()");
			this.InitStateMachine();
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000A0F4 File Offset: 0x000082F4
		private void Start()
		{
			Debug.Log("-> AwardNotification::Start()");
			this.ReferenceComponents();
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000A108 File Offset: 0x00008308
		private void Update()
		{
			this._stateMachine.Update();
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000A118 File Offset: 0x00008318
		private void InitStateMachine()
		{
			Debug.Log("-> AwardNotification::Awake()");
			this._stateMachine = new StateMachine();
			this._stateMachine.AddState(new Available("available", this));
			this._stateMachine.AddState(new LoadingIcon("loading_icon", this));
			this._stateMachine.AddState(new WaitingForSlot("waiting_for_slot", this));
			this._stateMachine.AddState(new MovingIn("moving_in", this));
			this._stateMachine.AddState(new MovingOut("moving_out", this));
			this._stateMachine.AddState(new Displaying("showing", this));
			this._stateMachine.SetStartState("available");
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000A1D0 File Offset: 0x000083D0
		private void ReferenceComponents()
		{
			Debug.Log("-> AwardNotification::ReferenceComponents()");
			Transform transform = base.transform.FindChild("AwardIcon");
			if (transform != null)
			{
				Debug.Log("-> Icon Transform found :)");
				this._iconMaterial = transform.renderer.material;
			}
			transform = base.transform.FindChild("txtAwardEarned");
			if (transform != null)
			{
				Debug.Log("-> Award Earned found :)");
				this._awardEarned = transform.GetComponent<TextMesh>();
				if (this._awardEarned != null)
				{
					this._awardEarned.text = "Award! Award! Award!";
				}
			}
			transform = base.transform.FindChild("txtAwardDescription");
			if (transform != null)
			{
				Debug.Log("-> Award Description found :)");
				this._awardDescription = transform.GetComponent<TextMesh>();
				if (this._awardDescription != null)
				{
					this._awardDescription.text = "Blah! Blah! Blah!";
				}
			}
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000A2D0 File Offset: 0x000084D0
		internal void RequestAwardSlot()
		{
			this.SlotRequested(this, null);
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000A2E0 File Offset: 0x000084E0
		internal void VacantedSlot()
		{
			this._allocatedSlot.UsedBy = null;
			this._allocatedSlot = null;
		}

		// Token: 0x0400010F RID: 271
		public const float SPEED = 10f;

		// Token: 0x04000110 RID: 272
		public static Vector3 offscreen;

		// Token: 0x04000111 RID: 273
		private AwardData _awardData;

		// Token: 0x04000112 RID: 274
		private StateMachine _stateMachine;

		// Token: 0x04000113 RID: 275
		private AwardSlot _allocatedSlot;

		// Token: 0x04000114 RID: 276
		private Material _iconMaterial;

		// Token: 0x04000115 RID: 277
		private TextMesh _awardDescription;

		// Token: 0x04000116 RID: 278
		private TextMesh _awardEarned;
	}
}
