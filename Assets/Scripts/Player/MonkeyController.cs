using System.Collections.Generic;
using UnityEngine;
using ServiceLocator.Wave.Bloon;
using ServiceLocator.Player.Projectile;
using ServiceLocator.Main;
using ServiceLocator.Sound;

namespace ServiceLocator.Player
{
    public class MonkeyController
    {
        private MonkeyView monkeyView;
        private MonkeyScriptableObject monkeyScriptableObject;
        private ProjectilePool projectilePool;
        private List<BloonController> bloonControllers;
        private SoundService soundService;

        private float attackTimer;

        public MonkeyController(MonkeyScriptableObject monkeyScriptableObject, ProjectilePool projectilePool, SoundService soundService)
        {
            this.soundService = soundService;
            this.monkeyScriptableObject = monkeyScriptableObject;
            this.projectilePool = projectilePool;
            bloonControllers = new List<BloonController>();
            CreateMonkeyView();
            ResetAttackTimer();
        }

        private void CreateMonkeyView()
        {
            monkeyView = Object.Instantiate(monkeyScriptableObject.Prefab);
            monkeyView.SetController(this);
            monkeyView.SetTriggerRadius(monkeyScriptableObject.Range);
        }

        public void UpdateMonkeyController()
        {
            if (bloonControllers.Count > 0)
            {
                RotateTowardsTarget(bloonControllers[0]);
                ShootAtTarget(bloonControllers[0]);
            }
        }

        public void BloonEnteredRange(BloonController bloon)
        {
            if (CanAttackBloon(bloon.GetBloonType()))
            {
                bloonControllers.Add(bloon);
            }
        }

        public void BloonExitedRange(BloonController bloon)
        {
            if (CanAttackBloon(bloon.GetBloonType()))
            {
                bloonControllers.Remove(bloon);
            }
        }

        private void RotateTowardsTarget(BloonController targetBloon)
        {
            Vector3 direction = targetBloon.Position - monkeyView.transform.position;
            float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) + 180;
            monkeyView.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }

        private void ShootAtTarget(BloonController targetBloon)
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0)
            {
                ProjectileController projectile = projectilePool.GetProjectile(monkeyScriptableObject.projectileType);
                projectile.SetPosition(monkeyView.transform.position);
                projectile.SetTarget(targetBloon);
                soundService.PlaySoundEffects(Sound.SoundType.MonkeyShoot);
                ResetAttackTimer();
            }
        }

        public void SetPosition(Vector3 positionToSet) => monkeyView.transform.position = positionToSet;

        public bool CanAttackBloon(BloonType bloonType) => monkeyScriptableObject.AttackableBloons.Contains(bloonType);

        private void ResetAttackTimer() => attackTimer = monkeyScriptableObject.AttackRate;
    }
}