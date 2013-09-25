using System;
using System.Collections.Generic;
using UnityEngine;

namespace Think
{
	// List of nodes to be executed one after another, in order
	// If a node is false, the whole sequence is invalid
	public class Sequence : Node
	{
		protected List<Node> mNodes = new List<Node>();
		protected int mCurrent = -1;
		
		// PUBLIC VARIABLES are automatically exposed to Unification Binding
		public GameObject Mongo;
		
		public override bool TestCondition(KB kb) 
		{
			if (mNodes.Count == 0)
				return false;
			
			for (int i=0; i<mNodes.Count; ++i)
			{
				if (!mNodes[i].TestCondition(kb))
					return false;
			}
			
			return true;
		}
		
		public override void Simulate(KB kb) 
		{
			for (int i=0; i<mNodes.Count; ++i)
			{
				mNodes[i].Simulate(kb);
			}
		}
		public override void UndoSimulate(KB kb) 
		{
			for (int i=mNodes.Count-1; i>=0; --i)
			{
				mNodes[i].UndoSimulate(kb);
			}
		}
		
		public override void Start() 
		{
			mCurrent = 0;
			if (mNodes.Count > 0)
				mNodes[0].Start();
		}
		
		public override bool Update(float dt) 
		{ 
			if (mCurrent >= mNodes.Count)
				return false;
			
			bool cont = mNodes[mCurrent].Update(dt);
			if (!cont)
			{
				mNodes[mCurrent].End();
				mCurrent++;
			}
			
			return mCurrent < mNodes.Count;
		}
		
		public override void End() 
		{
			if (mCurrent < mNodes.Count)
				mNodes[mCurrent].End();
		}

		public override bool IsLeaf() 
		{ 
			return false;
		}
	}
}

