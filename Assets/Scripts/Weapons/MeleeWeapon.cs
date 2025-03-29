using UnityEngine;

namespace Weapons
{
    public class MeleeWeapon : BaseWeapon
    {
        
        [SerializeField] Transform swordTransform;
        [SerializeField] Animation swordAnimation;
        
        

        void Update()
        {
            var deltaTime = Time.deltaTime;
            SwordRotate(deltaTime);
            m_shotsTimer -= deltaTime;
            if (m_shotsTimer >= 0.0f || Entities.Count == 0) return;
            swordAnimation.Stop();
            swordAnimation.Play();
            Entities[0].ApplyDamage(m_damage);
            m_shotsTimer = data.shotsTimer;
            Entities.RemoveAll(be => be == null);
        }

        void SwordRotate(float deltaTime)
        {
            transform.up = Vector3.Lerp(transform.up, Direction, deltaTime * 12.0f);
        }
    }
}