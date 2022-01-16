using System;
using System.Collections.Generic;
using UnityEngine;

namespace com.miniclip
{
	// Token: 0x02000063 RID: 99
	public class StateMachine
	{
		// Token: 0x06000310 RID: 784 RVA: 0x0000B350 File Offset: 0x00009550
		public StateMachine()
		{
			this._currentState = null;
			this._states = new Dictionary<string, IState>();
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000311 RID: 785 RVA: 0x0000B36C File Offset: 0x0000956C
		public IState CurrentState
		{
			get
			{
				return this._currentState;
			}
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000B374 File Offset: 0x00009574
		public virtual void Update()
		{
			if (this._currentState != null)
			{
				this._currentState.Update();
			}
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000B38C File Offset: 0x0000958C
		public void AddState(IState state)
		{
			Debug.Log("-> StateMachine::AddState() - state ID: " + state.Id);
			this._states.Add(state.Id, state);
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000B3C0 File Offset: 0x000095C0
		public void SetStartState(string stateId)
		{
			if (this._states.ContainsKey(stateId))
			{
				this._currentState = this._states[stateId];
			}
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0000B3E8 File Offset: 0x000095E8
		public bool ChangeState(string stateId)
		{
			return stateId == this._currentState.Id || this.ChangeState(this._states[stateId]);
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0000B420 File Offset: 0x00009620
		public bool ChangeState(IState state)
		{
			if (state != null)
			{
				this._currentState.Exit();
				this._prevStateId = this._currentState.Id;
				this._currentState = state;
				this._currentState.Enter();
				return true;
			}
			return false;
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000B45C File Offset: 0x0000965C
		public void RevertToPreviousState()
		{
			this.ChangeState(this._prevStateId);
		}

		// Token: 0x04000152 RID: 338
		protected IState _currentState;

		// Token: 0x04000153 RID: 339
		protected string _prevStateId;

		// Token: 0x04000154 RID: 340
		protected Dictionary<string, IState> _states;
	}
}
