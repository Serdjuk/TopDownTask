using Enemies;
using Enemies.Minions;
using UnityEngine;
using Zenject;

namespace Installer
{
    public class EnemyInstaller : MonoInstaller
    {

        [SerializeField] Minion minion;
        [SerializeField] BossEnemy bossEnemy;
        [SerializeField] HenchmanEnemy henchmanEnemy;
        public override void InstallBindings()
        {
            Container.Bind<BossEnemy>().FromComponentInNewPrefab(bossEnemy).AsTransient();
            Container.Bind<HenchmanEnemy>().FromComponentInNewPrefab(henchmanEnemy).AsTransient();

            Container.BindMemoryPool<Minion, MinionPool>()
                // .WithInitialSize(5)
                // .ExpandByOneAtATime()
                .FromComponentInNewPrefab(minion);
            // .UnderTransform(null);

        }
    }
}