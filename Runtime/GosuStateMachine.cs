using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace Gosu.StateMachine
{

	public abstract class GosuStateMachine<TKEY, TOwner> : MonoBehaviour
														where TKEY : struct, IConvertible
														where TOwner : GosuStateMachine<TKEY, TOwner>
	{

		private GosuState<TKEY, TOwner> currentState;
		private Dictionary<TKEY, GosuState<TKEY, TOwner>> stateDic = new Dictionary<TKEY, GosuState<TKEY, TOwner>>();

		public TKEY CurrentState => currentState.KEY;

		protected virtual void Awake()
		{
			LoadStates();
		}
		protected virtual void Update()
		{
			UpdateState();
		}

		private void LoadStates()
		{
			GosuState<TKEY, TOwner>[] states = GetComponentsInChildren<GosuState<TKEY, TOwner>>(true);
			foreach (var state in states)
			{
				stateDic.Add(state.KEY, state);
				state.SetOwner(this);
			}
		}

		private void UpdateState()
		{
			if (currentState)
			{
				currentState.OnUpdate();
			}
		}

		public void ChangeState(TKEY key)
		{
			if(stateDic.TryGetValue(key, out GosuState<TKEY, TOwner> state))
			{
				ChangeState(state);
			}
			else
			{
				throw new Exception("State not found: " + key);
			}
		}

		private void ChangeState(GosuState<TKEY, TOwner> state)
		{
			if (currentState == state)
				return;
			if (!state.CanEnterState)
				return;

			if (currentState && !currentState.CanExitState)
				return;

			ForceToState(state);
		}

		private void ForceToState(GosuState<TKEY, TOwner> state)
		{
			if (currentState)
			{
				currentState.OnExit();
			}

			Debug.Log(this.gameObject.name + " change state: " + state.KEY);
			state.OnEnter();
			currentState = state;
		}

	}
}
