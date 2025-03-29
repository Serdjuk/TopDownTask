using System;
using UnityEngine;

namespace Weapons
{
    public class Bullet : MonoBehaviour
    {
        Vector3 m_direction;
        Vector3 m_targetPosition;
        int m_layerMask;
        int m_damage;

        public void Run(Vector3 direction,  int damage, int layerMask)
        {
            m_layerMask = layerMask;
            m_direction = direction;
            m_targetPosition = transform.position + m_direction * 4.0f;
            m_damage = damage;
            Move();
        }

        void Update()
        {
            if (m_targetPosition == transform.position)
            {
                Destroy(gameObject);
                return;
            }

            Move();
        }

        void Move()
        {
            transform.position = Vector3.MoveTowards(transform.position, m_targetPosition, Time.deltaTime * 4.0f);
        }


        void OnTriggerEnter2D(Collider2D other)
        {
            if ((m_layerMask & (1 << other.gameObject.layer)) != 0)
            {
                if (other.gameObject.TryGetComponent<BaseEntity>(out var entity))
                {
                    entity.ApplyDamage(m_damage);
                    Destroy(gameObject);
                }
            }
        }
    }
}