using System;
using System.Diagnostics;

namespace Think
{
	public class Utils
	{
		[Conditional("DEBUG")]
		public static void Assert(bool condition)
		{
			if (!condition)
				throw new Exception("Assert failed");
		}

		[Conditional("DEBUG")]
		public static void Assert(bool condition, string msg)
		{
			if (!condition)
				throw new Exception("Assert failed: " + msg);
		}
	}
}

