using Enemies;
using UnityEngine;
using Zenject;

namespace Installer
{
    public class EnemyInstaller : MonoInstaller
    {

        [SerializeField] BossEnemy bossEnemy;
        [SerializeField] HenchmanEnemy henchmanEnemy;
        public override void InstallBindings()
        {
            Container.Bind<BossEnemy>().FromComponentInNewPrefab(bossEnemy).AsTransient();
            Container.Bind<HenchmanEnemy>().FromNewComponentOnNewPrefab(henchmanEnemy).AsTransient();

        }
    }
}