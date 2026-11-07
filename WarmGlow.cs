using CharmUraniumMug.Domain;
using UnityEngine;

namespace CharmUraniumMug
{
	/// <summary>
	/// Warm Glow: while the Uranium Mug is in the pocket and the local player crouches, health comes back slowly.
	/// A purely local effect: <c>PlayerHealth.Heal</c> only works on the owner's machine and syncs the new health itself.
	/// Wear is per second, charged only on frames where health is really being restored (never at full health).
	/// </summary>
	internal sealed class WarmGlow : MonoBehaviour
	{
		private readonly HealTimer timer = new HealTimer();

		internal UraniumMugCharm Charm { get; set; } = null!;

		private void Update()
		{
			if (Charm == null || !Charm.IsActive)
			{
				timer.Reset();
				return;
			}

			PlayerController? controller = PlayerController.instance;
			PlayerAvatar? avatar = PlayerAvatar.instance;
			PlayerHealth? health = avatar != null ? avatar.playerHealth : null;
			if (controller == null || avatar == null || health == null)
			{
				timer.Reset();
				return;
			}

			// PlayerController.Crouching is also true while crawling and while tumbling, so tumbling is excluded.
			bool crouching = controller.Crouching && !avatar.isTumbling;
			bool canHeal = !avatar.deadSet && !avatar.isDisabled && health.health > 0 && health.health < health.maxHealth;
			if (!crouching || !canHeal)
			{
				timer.Reset();
				return;
			}

			float dt = Time.deltaTime;
			int heals = timer.Advance(dt, Charm.HealIntervalSeconds.Value);
			Charm.WearForSeconds(dt);
			if (heals <= 0)
			{
				return;
			}

			int amount = Mathf.Max(1, Mathf.RoundToInt(Charm.HealAmount.Value)) * heals;
			int before = health.health;
			health.Heal(amount, false);
			Charm.LogDebug($"Warm Glow: health {before} -> {health.health} of {health.maxHealth}");
		}
	}
}
