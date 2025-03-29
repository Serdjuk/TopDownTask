using Player;
using UnityEngine;
using Zenject;

namespace Installer
{
    public class PlayerInstaller : MonoInstaller
    {

        // [SerializeField] PlayerController playerController;
        
        public override void InstallBindings()
        {
            // Container.Bind<PlayerController>().FromInstance(playerController).AsSingle();
        }
    }
}