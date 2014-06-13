using System;

namespace Think
{
	public class CanSense : Condition
	{
		public AIObject actor1;
		public AIObject actor2;
		
		public override float TestCondition(KB kb)
		{
			return 1.0f;
		}
	}
}

