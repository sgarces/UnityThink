using System;
using System.Collections.Generic;

namespace Think
{
	public class RandomSelector : Selector
	{
		protected List<float> mProbabilities = new List<float>();
		protected Random mRandom = new Random();
		
		public override Node Select(KB kb)
		{
			int numElems = mChildren.Count;
			if (numElems > mProbabilities.Count)
				numElems = mProbabilities.Count;
			
			bool[] valid = new bool[numElems];
			
			float total = 0.0f;
			for (int i=0; i<numElems; ++i)
			{
				bool b = mChildren[i].TestCondition(kb);
				if (b)
				{
					valid[i] = b;
					total += mProbabilities[i];
				}
			}
			
			float val  = (float) mRandom.NextDouble() * total;
			for (int i=0; i<numElems; ++i)
			{
				if (valid[i])
				{
					val -= mProbabilities[i];
					if (val <= 0.0f)
						return mChildren[i];
				}
			}
			
			return null;
		}
	}
}

