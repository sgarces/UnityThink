using System;
using UnityEngine;

namespace Think
{
	public class GoTo : Action
	{
		public AIObject actor;
		public Vector3 position;
		
		private Vector3 prevPos;
		
		public override void Simulate(KB kb)
		{
			prevPos = actor.pos;
			actor.pos = position;
		}
		public override void UndoSimulate(KB kb)
		{
			actor.pos = prevPos;
		}
		
		public override bool Start() 
		{ 
			// TODO: Pathfinding call
			return true; 
		}
		
		public override bool Update(float dt) 
		{ 
			// TODO: If arrived, return false
			return true; 
		}
		
		public override void End() 
		{
			// TODO: do any cleanup here
		}
	}
}

