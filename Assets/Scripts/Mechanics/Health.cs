using System;
using Platformer.Gameplay;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    /// <summary>
    /// Represebts the current vital statistics of some game entity.
    /// </summary>
    public class Health : MonoBehaviour
    {
        /// <summary>
        /// The maximum hit points for the entity.
        /// </summary>
        public int maxHP = 1;

        [Tooltip("Seconds this entity ignores repeated damage after a successful hit. Set to 0 for no invulnerability.")]
        public float damageCooldown = 0f;

        /// <summary>
        /// Indicates if the entity should be considered 'alive'.
        /// </summary>
        public bool IsAlive => currentHP > 0;

        /// <summary>
        /// The entity's current hit points.
        /// </summary>
        public int CurrentHP => currentHP;

        /// <summary>
        /// Current hit points expressed as a value between 0 and 1.
        /// </summary>
        public float NormalizedHP => maxHP <= 0 ? 0f : (float)currentHP / maxHP;

        int currentHP;
        float nextDamageTime;

        /// <summary>
        /// Increment the HP of the entity.
        /// </summary>
        public void Increment()
        {
            currentHP = Mathf.Clamp(currentHP + 1, 0, maxHP);
        }

        /// <summary>
        /// Decrement the HP of the entity. Will trigger a HealthIsZero event when
        /// current HP reaches 0.
        /// </summary>
        public void Decrement()
        {
            currentHP = Mathf.Clamp(currentHP - 1, 0, maxHP);
            if (currentHP == 0)
            {
                var ev = Schedule<HealthIsZero>();
                ev.health = this;
            }
        }

        /// <summary>
        /// Decrements HP when this entity is not currently invulnerable.
        /// Returns true when damage was applied.
        /// </summary>
        public bool TryDecrement()
        {
            if (!IsAlive || Time.time < nextDamageTime)
                return false;

            Decrement();
            nextDamageTime = Time.time + damageCooldown;
            return true;
        }

        /// <summary>
        /// Decrement the HP of the entitiy until HP reaches 0.
        /// </summary>
        public void Die()
        {
            while (currentHP > 0) Decrement();
        }

        /// <summary>
        /// Restores this entity to its configured maximum HP.
        /// </summary>
        public void ResetHealth()
        {
            currentHP = maxHP;
            nextDamageTime = 0f;
        }

        void Awake()
        {
            currentHP = maxHP;
        }
    }
}
