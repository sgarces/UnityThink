using System;
using UnityEngine;

namespace Think
{
	public class IsNear : Condition
	{
		public AIObject actor;
		public AIObject target;
		public float distance;
		
		public override float TestCondition(KB kb)
		{
			float sqDist = (target.obj.transform.position - actor.obj.transform.position).sqrMagnitude;
			return (sqDist <= distance*distance) ? 1.0f : 0.0f;
		}
	}
	
	public class Distance : Condition
	{
		public AIObject actor;
		public AIObject target;
		public float minDist;
		public float maxDist;

		public override float TestCondition(KB kb)
		{
			float dist = Vector3.Distance(target.obj.transform.position, actor.obj.transform.position);
			return WeightedSum.Normalize(dist, minDist, maxDist);
		}
	}
	
	public class IsLookingAt : Condition
	{
		public AIObject actor;
		public AIObject target;
		public float angle;
		
		public override float TestCondition(KB kb)
		{
			Vector3 toTarget = target.obj.transform.position - actor.obj.transform.position;
			float angleToTarget = Vector3.Angle(actor.obj.transform.forward, toTarget);
			return (angleToTarget <= angle) ? 1.0f : 0.0f;
		}
	}

	public class Angle : Condition
	{
		public AIObject actor;
		public AIObject target;
		public float minAngle;
		public float maxAngle;
		
		public override float TestCondition(KB kb)
		{
			Vector3 toTarget = target.obj.transform.position - actor.obj.transform.position;
			float angleToTarget = Vector3.Angle(actor.obj.transform.forward, toTarget);
			return WeightedSum.Normalize(angleToTarget, minAngle, maxAngle);
		}
	}
}

