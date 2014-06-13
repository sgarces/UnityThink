using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Think {
	
	// IDEA: Run the perception all at once from the Manager
	// Unity doesn't let anything run outside of GameObjects, but
	// the Manager can update the Perception in one LateUpdate, and have it 
	// ready for the next frame 
	public class Perception 
	{
		Brain mBrain;
		GameObject mObj;
		KB mKB;
		
		public Perception(GameObject obj, Brain brain, KB kb)
		{
			mObj = obj;
			mBrain = brain;
			mKB = kb;
		}

		void Update(float dt, List<string> tags) 
		{
			for (int i=0; i<tags.Count; ++i)
			{
				GameObject[] objs = GameObject.FindGameObjectsWithTag(tags[i]);
				for (int o=0; o<objs.Length; ++o)
				{
					if (CanSee(objs[o]))
					{
						// TODO: create a proxy AI Object, and add that instead
						mKB.Add(tags[i], objs[o]);
					}
				}
			}
		}
		
		private bool CanSee(GameObject other)
		{
			Brain.VisionConfig vision = mBrain.Vision;
			Brain.HearingConfig hearing = mBrain.Hearing;
			if (hearing.distance > 0.0f)
			{
				float sqDist = (mObj.transform.position - other.transform.position).sqrMagnitude;
				if (sqDist > hearing.distance*hearing.distance)
					return true;
			}
			if (vision.distance > 0.0f)
			{
				float sqDist = (mObj.transform.position - other.transform.position).sqrMagnitude;
				if (sqDist > vision.distance*vision.distance)
					return false;
			}
			if (vision.angle > 0.0f)
			{
				Vector3 toOther = other.transform.position - mObj.transform.position;
				float angle = Vector3.Angle(mObj.transform.forward, toOther);
				if (angle > vision.angle)
					return false;
			}
			return true;
		}
		
	}

}