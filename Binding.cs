using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Think
{
	public class Binding
	{
		struct Var
		{
			public string str;
			public string type;
			public int bound;
		}
		Var[] mVars;
		//sbyte[] mVars;
		
		public Binding(Type type)
		{
			Debug.Log("TYPE: "+type.Name);
			//mVars = new sbyte[numVars];
			FieldInfo[] fields = type.GetFields();
			mVars = new Var[fields.Length];
			for (int i=0; i<mVars.Length; ++i)
			{
				mVars[i].str = fields[i].Name;
				mVars[i].type = fields[i].FieldType.Name;
				mVars[i].bound = -1;
			}
		}
		
		public void Begin()
		{
			// Go through all the combinations, starting with everything to 0
			for (int i=0; i<mVars.Length; ++i)
				mVars[i].bound = 0;
			
//			for (int i=0; i<mVars.Count; ++i)
//				mVars[i] = 0;
		}
		
		public bool Next()
		{
			for (int i=0; i<mVars.Length; ++i)
			{
				int newVal = mVars[i].bound + 1;
				if (newVal >= KB.Instance.GetCount(mVars[i].type))
				{
					// Try next variable, reset count here
					mVars[i].bound = 0;
				}
				else
				{
					mVars[i].bound = newVal;
					return true;
				}
			}
			// We exhausted all options
			return false;
		}
		
		public void Apply(Binding b)
		{
			Utils.Assert(mVars.Length >= b.mVars.Length);
			for (int i=0; i<mVars.Length; ++i)
			{
				if (!IsBound(i))
					mVars[i] = b.mVars[i];
			}			
		}
		
		public object Get(int i)
		{
			return KB.Instance.Get(mVars[i].type, mVars[i].bound);
		}
		
		public object Get(string s)
		{
			for (int i=0; i<mVars.Length; ++i)
			{
				if (mVars[i].str == s)
					return Get(i);
			}
			return null;
		}
		
		public bool IsBound(int i)
		{
			return mVars[i].bound >= 0;
		}

		public object IsBound(string s)
		{
			for (int i=0; i<mVars.Length; ++i)
			{
				if (mVars[i].str == s)
					return IsBound(i);
			}
			return false;
		}
		
		
		#region private
		
		void Reset()
		{
			for (int i=0; i<mVars.Length; ++i)
				mVars[i].bound = -1;
		}
		
		void Set(int i, int val)
		{
			mVars[i].bound = val;
		}
		
		#endregion
		
	}
}

