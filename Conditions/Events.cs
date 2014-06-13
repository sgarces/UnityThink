using System;

namespace Think
{
	public class Events : Condition
	{
		public string eventName;
		
		public override float TestCondition(KB kb)
		{
			bool triggered = kb.HasEventTriggered(eventName);
			return triggered ? 1.0f : 0.0f;
		}
	}
}

