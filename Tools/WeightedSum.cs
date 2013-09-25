using System;

namespace Think
{
	public class WeightedSum
	{
		private float mSum = 0.0f;
		
		public float Value { get { return mSum; } }
		
		public void Reset()
		{
			mSum = 0.0f;
		}
		
		public void Add(float weight, float val, float min, float max, int smooth)
		{
			val = Normalize(val, min, max);
			val = Smooth(val, smooth);
			mSum += weight * val;
		}
		
		public void AddNormalized(float weight, float val, int smooth)
		{
			val = Smooth(val, smooth);
		}
		
		public void AddInverse(float weight, float val, float min, float max, int smooth)
		{
			val = Normalize(val, min, max);
			val = Smooth(val, smooth);
			mSum += weight * (1.0f - val);
		}
		

		private float Normalize(float val, float min, float max)
		{
			if (max - min < 1e-5f)
			{
				val = 0;
			}
			else if (val <= min)
			{
				val = 0;
			}
			else if (val >= max)
			{
				val = 1;
			}
			else
			{
				val = (val - min) / (max - min);
			}
			return val;
		}
		
		private float Smooth(float val, int smooth)
		{
			while (smooth > 0)
			{
				val = val*val*(3 - 2*val);
				--smooth;
			}
			return val;
		}
	}
}

