using CharmUraniumMug.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CharmUraniumMug.Tests;

[TestClass]
public class HealTimerTests
{
	[TestMethod]
	public void Advance_BelowInterval_ReturnsZeroAndKeepsTime()
	{
		HealTimer timer = new HealTimer();
		Assert.AreEqual(0, timer.Advance(1f, 3f));
		Assert.AreEqual(1f, timer.Accumulated, 0.0001f);
	}

	[TestMethod]
	public void Advance_ReachingInterval_ReturnsOneAndKeepsRemainder()
	{
		HealTimer timer = new HealTimer();
		Assert.AreEqual(0, timer.Advance(2f, 3f));
		Assert.AreEqual(1, timer.Advance(1.5f, 3f));
		Assert.AreEqual(0.5f, timer.Accumulated, 0.0001f);
	}

	[TestMethod]
	public void Advance_ManySmallSteps_HealsOncePerInterval()
	{
		HealTimer timer = new HealTimer();
		int total = 0;
		for (int i = 0; i < 240; i++)
		{
			total += timer.Advance(0.125f, 3f);
		}

		Assert.AreEqual(10, total);
	}

	[TestMethod]
	public void Advance_LongFrame_IsCappedAndDoesNotBurst()
	{
		HealTimer timer = new HealTimer();
		Assert.AreEqual(HealTimer.MaxHealsPerAdvance, timer.Advance(1000f, 3f));
		Assert.AreEqual(0f, timer.Accumulated, 0.0001f);
	}

	[TestMethod]
	public void Advance_SeveralIntervalsInOneStep_ReturnsThatMany()
	{
		HealTimer timer = new HealTimer();
		Assert.AreEqual(3, timer.Advance(9.5f, 3f));
		Assert.AreEqual(0.5f, timer.Accumulated, 0.0001f);
	}

	[DataTestMethod]
	[DataRow(0f, 3f)]
	[DataRow(-1f, 3f)]
	[DataRow(1f, 0f)]
	[DataRow(1f, -2f)]
	[DataRow(float.NaN, 3f)]
	[DataRow(1f, float.NaN)]
	[DataRow(float.PositiveInfinity, 3f)]
	public void Advance_InvalidInput_AddsNothing(float delta, float interval)
	{
		HealTimer timer = new HealTimer();
		Assert.AreEqual(0, timer.Advance(delta, interval));
		Assert.AreEqual(0f, timer.Accumulated, 0.0001f);
	}

	[TestMethod]
	public void Reset_ForgetsCollectedTime()
	{
		HealTimer timer = new HealTimer();
		timer.Advance(2.9f, 3f);
		timer.Reset();
		Assert.AreEqual(0, timer.Advance(0.2f, 3f));
		Assert.AreEqual(0.2f, timer.Accumulated, 0.0001f);
	}
}
