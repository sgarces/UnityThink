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
		
		public float startTime;
		public float endTime;
		public bool running = false;

		// This is the actual gameplay code.
		// Would it make sense to decouple from Simulation logic?
		public virtual bool Start() 
		{ 
			running = true;
			return running; 
		}
		public virtual bool Update(float dt) 
		{ 
			return false; 
		}
		public virtual void End() 
		{
			running = false;
		}
	}
	
	// These in Proto were called Commands
	public abstract class InstantAction
	{
		public virtual bool Start() 
		{
			Run();
			return false;
		}
		protected abstract void Run();
	}
}

