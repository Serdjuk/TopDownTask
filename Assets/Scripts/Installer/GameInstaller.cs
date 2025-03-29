using Mangers;
using UnityEngine;
using Zenject;

namespace Installer
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] Camera cam;
        [SerializeField] GameManager gameManager;
        public override void InstallBindings()
        {
            
            Container.Bind<NewInputSystem>().AsSingle();
            Container.Bind<Camera>().FromInstance(cam).AsSingle();
            Container.Bind<GameManager>().FromInstance(gameManager).AsSingle();
            // Container.BindFactory<int, DamageNumbers, DamageNumbers.Factory>()
            //     .FromComponentInNewPrefab(damageNumbersPrefab);

            
        }
    }
}