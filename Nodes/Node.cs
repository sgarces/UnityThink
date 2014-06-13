using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Think
{
	// Basic building block. Includes a boolean condition, and an action that runs until it's done
	// Ideas:
	// - Have a float condition instead, so it can be used to sort/tournament/utility
	// - Split condition and action, so multiple actions can be grouped without having to check conditions (a la fight tree)
	// - Have the actions start/end at certain time points, like the fight tree
	// - Split the simulation logic from the actual logic. How? 
	// - Undo for simulation is inconvenient, how to automate?
	public abstract class Node
	{
//		struct Var
//		{
//			public Var(string name)
//			{
//				Name = name;
//				Obj = null;
//			}
//			//public GameObject Obj;
//			public object Obj;
//			public string Name;
//		};
		//private List<Var> mVars = new List<Var>();
		private Binding mBinding;
		
		// Variables can be used during the TestCondition phase
		// Unification will assign values to the variables, then test
		// the conditions, in search for a combination of variables that
		// can satisfy it.
		// The better we can narrow down the possible values, the better (i.e. with
		// types, close conditions, etc)
		// Also we can link some sorting logic to the unification, so better values
		// are tested first (this is part of the logic of the system)
		// Storing the variable won't be enough, we need to keep track of the state of
		// the search
		// A possible optimization, and ease of use would be to use reflection
		// to find and evaluate the variables as C# variables, instead of a struct
		// This replaces and enhances the lambda expressions in Proto, and things
		// like the Target Selection (which could benefit from some tree selection
		// logic similar to the one we wrote for SM)
		public Node()
		{
			mBinding = new Binding(this.GetType());
		}
		
		List<Condition> mConditions = new List<Condition>();
		List<Action> mActions = new List<Action>();
		
		// Instead of calling TestCondition every frame using polling,
		// the Node allows listeners to register with AddOnTrue.
		// Thus, when the Node is triggered by an external system that 
		// calls TriggerOnTrue (the Node itself is listening to something
		// or at lest has an external system do the polling when required)
		// the notification is passed along
		// In this way, the "opportunities" don't have to constantly check
		// if there is a change (even with the masks from Proto).
		// Instead, when something external changes, the Node pushes the
		// event and triggers the chain of actions that are relevant
		// NOTE: if there is any binding to be done, we can't do the trigger
		// as we need to search for a good combination of variables,
		// so we must resort to polling (re-planning)
		public virtual bool TestCondition(KB kb) 
		{ 
			for (int i=0; i<mConditions.Count; ++i)
			{
				if (mConditions[i].TestCondition(kb) == 0.0f)
					return false;
			}
			return true;
		}
		
		// NOTE: Should we have a different set of conditions
		// to decide whether we should CONTINUE running the Node
		// So we can implement hysteresis?
		public virtual bool ShouldAbort(KB kb)
		{
			return false;
		}
		
		public delegate void OnTrue(Node node);
		public void AddOnTrue(OnTrue listener) 
		{
			if (mListeners == null)
				mListeners = new List<OnTrue>();
			mListeners.Add(listener);
		}
		
		public void DelOnTrue(OnTrue listener)
		{
			mListeners.Remove(listener);
		}
		
		protected List<OnTrue> mListeners = null;
		protected void TriggerOnTrue()
		{
			for (int i=0; i<mListeners.Count; ++i)
			{
				mListeners[i](this);
			}
		}
		
		//public void AddVar(string name)
		//{
		//	mVars.Add(new Var(name));
		//}
		
//		public void FindBindings()
//		{
//			System.Type myType = this.GetType();
//			FieldInfo[] myFields = myType.GetFields();
//			foreach (FieldInfo fi in myFields)
//			{
//				Debug.Log(fi.Name + " " + fi.FieldType.FullName);
//			}
//		}
		
		// These functions are used during the Search, to update
		// the state of the World and guarantee that the full
		// plan is feasible
		public virtual void Simulate(KB kb) {}
		public virtual void UndoSimulate(KB kb) {}
		
		// This is the actual gameplay code.
		// Would it make sense to decouple from Simulation logic?
		public virtual void Start() {}
		public virtual bool Update(float dt) { return false; }
		public virtual void End() {}
		
		public virtual bool IsLeaf() { return true; }
	}
}

