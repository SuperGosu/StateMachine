using System;
using UnityEngine;

namespace Gosu.StateMachine
{
	public abstract class GosuState<TKEY, TOwner> : MonoBehaviour, IState where TKEY : struct, IConvertible where TOwner : GosuStateMachine<TKEY,TOwner>
	{
		public abstract TKEY KEY { get; }
		public TOwner Owner { get; private set; }

		public virtual bool CanEnterState => true;

		public virtual bool CanExitState => true;

		public void SetOwner(MonoBehaviour owner)
		{
			this.Owner = (TOwner)owner;
		}
		public virtual void OnEnter()
		{
		
		}

		public virtual void OnExit()
		{

		}

		public virtual void OnUpdate()
		{
		
		}
	}

}