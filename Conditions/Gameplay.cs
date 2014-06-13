using System;

namespace Think
{
	public class HasHealth : Condition
	{
		public AIObject actor;
		public float minPercentHealth;
		
		public override float TestCondition(KB kb)
		{
			return 1.0f;
		}
	}
	
	public class IsDead : Condition
	{
		public AIObject actor;
		
		public override float TestCondition(KB kb)
		{
			return 1.0f;
		}
	}
}

