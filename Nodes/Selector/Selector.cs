using System;
using System.Collections.Generic;

namespace Think
{
	public abstract class Selector : Node
	{
		protected List<Node> mChildren = new List<Node>();
		
		public abstract Node Select(KB kb);
		
		public override bool IsLeaf() 
		{ 
			return false; 
		}
	}
}

