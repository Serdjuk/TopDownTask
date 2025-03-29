using UnityEngine;

namespace Weapons
{
    public class RangedWeapon : BaseWeapon
    {
        [SerializeField] Bullet bulletPrefab;


        void Update()
        {
            m_shotsTimer -= Time.deltaTime;
            if (m_shotsTimer >= 0.0f || Entities.Count == 0) return;
            var first = Entities[0];
            var direction = (first.transform.position - transform.position).normalized;
            CreateBullet(direction);
            m_shotsTimer = data.shotsTimer;
            Entities.RemoveAll(be => be == null);
        }


        void CreateBullet(Vector3 direction)
        {
            var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.Run(direction,  m_damage, m_layerMask);
        }
    }
}