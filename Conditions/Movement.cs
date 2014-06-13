using System;
using UnityEngine;

namespace Think
{
	public class CanGo : Condition
	{
		public AIObject actor;
		public Vector3 position;
		
		public override float TestCondition(KB kb)
		{
			// TODO: check with the movement/pathfinding system
			return 1.0f;
		}
	}
}

