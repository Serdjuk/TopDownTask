using System.Collections;
using System.Collections.Generic;
using Configs;
using UnityEngine;
using Zenject;

namespace Enemies.Minions
{
    public class MinionPool : MonoMemoryPool<Minion>
    {
        private int _maxSize = 20;
        private int _currentCount = 0;

        protected override void OnCreated(Minion item)
        {
            _currentCount++;
            if (_currentCount > _maxSize)
            {
                Debug.LogWarning("Превышен лимит пула!");
            }
        }

        protected override void OnSpawned(Minion item)
        {
            item.Initialize(item.data.health);
            item.healthBar.RedrawHealth(0, 1.0f);
        }

        protected override void OnDespawned(Minion item)
        {
            item.gameObject.SetActive(false);
        }
        
    }
}