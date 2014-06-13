using System;
using System.Collections.Generic;

namespace Think {

	public class KB
	{
		private static KB sKB = new KB();
		
		public static KB Instance { get { return sKB; } }
		
		//private Dictionary<string, object> mVars = new Dictionary<string, object>();
		
		private Dictionary<string,List<object>> mUniverse = new Dictionary<string,List<object>>();
			
//		public void Set(string s, object obj)
//		{
//			mVars[s] = obj;
//		}
		
//		public object Get(string s)
//		{
//			object obj = null;
//			mVars.TryGetValue(s, out obj);
//			return obj;
//		}
		
		// KB is filled from the Perception system
		// The classification in Types is just an optimization,
		// to filter out the search a bit better (for instance
		// it might be useful to classify enemy vs ally, although
		// it's not strictly necessary, as the search will filter
		// out each appropriately, in time)
		
		// For bindings, there need to be other mechanisms than
		// visiting the entire universe, for instance to 
		// generate candidates for attack position
		
		public int GetCount(string type)
		{
			return mUniverse[type].Count;
		}
		
		public object Get(string type, int i)
		{
			return mUniverse[type][i];
		}
		
		public void Clear()
		{
			mUniverse.Clear();
		}
		
		public void Add(string type, object obj)
		{
			if (!mUniverse.ContainsKey(type))
				mUniverse[type] = new List<object>();
			mUniverse[type].Add(obj);
		}
		
		public bool HasEventTriggered(string eventName)
		{
			return false;
		}
	}
}

