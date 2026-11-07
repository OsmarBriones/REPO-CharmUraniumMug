using System;

namespace CharmUraniumMug.Domain
{
	/// <summary>
	/// Game-free interval accumulator of Warm Glow: time passes, and it says how many heals have become due.
	/// Kept free of Unity and game types so it is unit tested.
	/// </summary>
	internal sealed class HealTimer
	{
		/// <summary>Most heals one call may report, so a long frame (a freeze or a loading hitch) can never turn into a burst.</summary>
		internal const int MaxHealsPerAdvance = 5;

		private float accumulated;

		/// <summary>Seconds collected towards the next heal.</summary>
		internal float Accumulated => accumulated;

		/// <summary>
		/// Adds <paramref name="deltaSeconds"/> and returns how many whole intervals of <paramref name="intervalSeconds"/> have
		/// completed (0 when none). The remainder is kept for the next call. A non-positive or non-finite delta or interval
		/// adds nothing and returns 0.
		/// </summary>
		internal int Advance(float deltaSeconds, float intervalSeconds)
		{
			if (!(deltaSeconds > 0f) || !(intervalSeconds > 0f) || float.IsInfinity(deltaSeconds) || float.IsInfinity(intervalSeconds))
			{
				return 0;
			}

			accumulated += deltaSeconds;
			if (accumulated < intervalSeconds)
			{
				return 0;
			}

			int due = (int)Math.Floor(accumulated / intervalSeconds);
			if (due > MaxHealsPerAdvance)
			{
				due = MaxHealsPerAdvance;
				accumulated = 0f;
			}
			else
			{
				accumulated -= due * intervalSeconds;
				if (accumulated < 0f)
				{
					accumulated = 0f;
				}
			}

			return due;
		}

		/// <summary>Forgets the time collected so far (the power stopped working).</summary>
		internal void Reset()
		{
			accumulated = 0f;
		}
	}
}
