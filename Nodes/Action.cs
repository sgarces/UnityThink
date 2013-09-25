using System;

namespace Think
{
	public abstract class Action
	{
		// These functions are used during the Search, to update
		// the state of the World and guarantee that the full
		// plan is feasible
		public abstract void Simulate(KB kb);
		public abstract void UndoSimulate(KB kb);
		
		public float StartTime;
		public float EndTime;

		// This is the actual gameplay code.
		// Would it make sense to decouple from Simulation logic?
		public virtual bool Start() { return true; }
		public virtual bool Update(float dt) { return false; }
		public virtual void End() {}
	}
	
	public abstract class InstantAction
	{
		public virtual bool Start() 
		{
			Do();
			return false;
		}
		protected abstract void Do();
	}
}

