using System;
using System.Collections.Generic;

namespace Think
{
	public class UtilitySelector : Selector
	{
		public struct Utility
		{
			public string name;
			public float weight;
			public float min;
			public float max;
			public int smooth;
			public bool inverse;
		}
		
		protected List<Utility> mUtilities = new List<Utility>();
		
		public override Node Select(KB kb)
		{
			int best = -1;
			float highestUtility = 0.0f;
			WeightedSum sum = new WeightedSum();

			int numElems = mChildren.Count;
			int numUtilities = mUtilities.Count;
			for (int i=0; i<numElems; ++i)
			{
				bool b = mChildren[i].TestCondition(kb);
				if (!b)
					continue;
					
				sum.Reset();
				for (int j=0; j<numUtilities; ++j)
				{
					float val = 0.0f; // TODO: get from KB
					if (mUtilities[j].inverse)
						sum.AddInverse(mUtilities[j].weight, val, mUtilities[j].min, mUtilities[j].max, mUtilities[j].smooth);
					else
						sum.Add(mUtilities[j].weight, val, mUtilities[j].min, mUtilities[j].max, mUtilities[j].smooth);
				}
				
				if (sum.Value > highestUtility)
				{
					best = i;
					highestUtility = sum.Value;
				}
			}
			
			if (best >= 0)
				return mChildren[best];
			return null;
		}
	}
}

