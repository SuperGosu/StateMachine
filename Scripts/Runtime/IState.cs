using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gosu.StateMachine
{
	public interface IState 
	{
		void OnEnter();
		void OnExit();
		void OnUpdate();
	}

}