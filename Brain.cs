using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Think
{
	public class Brain : MonoBehaviour
	{
		public List<string> Tags;
		
		public enum PerceptionType
		{
			Realistic,
			Omniscient
		}
		public PerceptionType Senses;
		
		[System.Serializable]
		public class VisionConfig
		{
			public float distance;
			public float angle;
		}
		public VisionConfig Vision;
		
		[System.Serializable]
		public class HearingConfig
		{
			public float distance;
		}
		public HearingConfig Hearing;
		
		private KB mKnowledge = new KB();
		private Node mTree = null;
		private Node mCurrent = null;
		
		private List<Node> mRunning = new List<Node>();
		
		private Perception mPerception;
		
		// Use this for initialization
		void Start ()
		{
			mPerception = new Perception(gameObject, this, mKnowledge);
			Sequence n = new Sequence();
			//n.FindBindings();
		}
		
		// Update is called once per frame
		void Update ()
		{
			float dt = Time.deltaTime;
			
			// TODO: Update perception (NOTE: perception has its own Behaviour!)
			WalkTree();
			if (mCurrent != null)
				mCurrent.Update(dt);
		}
		
		private void WalkTree()
		{
			// TODO: move all the Walk Tree logic to integrate it
			// into the nodes themselves, so they return true
			// when they're finish traversing
			//
			// mCurrent is not a variable, it's a number of "sequencers"
			// connected to the parallels running
			//
			// Need to figure out a way to make it event-driven instead of polling
			// constantly
			Node node = mTree;
			while (node != null && !node.IsLeaf())
			{
				Selector selector = node as Selector;
				if (selector != null)
				{
					node = selector.Select(mKnowledge);
				}
			}
			
			mCurrent = node;
		}
	}

}