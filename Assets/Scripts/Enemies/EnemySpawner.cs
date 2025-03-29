using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] HenchmanEnemy henchmanEnemyPrefab;
        [SerializeField] BossEnemy bossEnemyPrefab;


        readonly List<BossEnemy> m_boss = new List<BossEnemy>();
        readonly List<HenchmanEnemy> m_henchman = new List<HenchmanEnemy>();


        public void Spawn<T>(Vector3 position, Transform parent = null)
        {
            var type = typeof(T);
            if (type == typeof(BossEnemy))
            {
                var entity = m_henchman.FirstOrDefault(be => !be.gameObject.activeInHierarchy);
                if (entity != null)
                {
                    entity.transform.position = position;
                    entity.gameObject.SetActive(true);
                }
                else
                {
                    entity = Instantiate(henchmanEnemyPrefab, position, Quaternion.identity, parent);
                    m_henchman.Add(entity);
                }
            }

            if (type == typeof(HenchmanEnemy))
            {
                var entity = m_boss.FirstOrDefault(be => !be.gameObject.activeInHierarchy);
                if (entity != null)
                {
                    entity.transform.position = position;
                    entity.gameObject.SetActive(true);
                }
                else
                {
                    entity = Instantiate(bossEnemyPrefab, position, Quaternion.identity, parent);
                    m_boss.Add(entity);
                }
            }
        }
    }
}