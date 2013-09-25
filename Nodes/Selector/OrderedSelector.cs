using System;

namespace Think
{
	public class OrderedSelector : Selector
	{
		public override Node Select(KB kb)
		{
			int numElems = mChildren.Count;
			for (int i=0; i<numElems; ++i)
			{
				bool b = mChildren[i].TestCondition(kb);
				if (b)
					return mChildren[i];
			}
			
			return null;
		}
	}
}

