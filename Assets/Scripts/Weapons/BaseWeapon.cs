using System;
using System.Collections.Generic;
using Configs;
using UnityEngine;

namespace Weapons
{
    public abstract class BaseWeapon : MonoBehaviour
    {
        [SerializeField] protected WeaponData data;
        CircleCollider2D m_collider2D;
        protected readonly List<BaseEntity> Entities = new();
        protected int m_layerMask;
        Action m_action;
        protected int m_damage;
        protected float m_radius;
        protected float m_shotsTimer;
        protected Vector3 Direction;

        public void Install(Action action, int layerMask)
        {
            m_action = action;
            m_layerMask = layerMask;
            m_damage = data.damage;
            m_shotsTimer = data.shotsTimer;
            m_radius = data.distance;
            m_collider2D = gameObject.AddComponent<CircleCollider2D>();
            m_collider2D.isTrigger = true;
            m_collider2D.radius = m_radius;
        }


        void OnTriggerEnter2D(Collider2D other)
        {
            if ((m_layerMask & (1 << other.gameObject.layer)) != 0)
            {
                if (other.gameObject.TryGetComponent<BaseEntity>(out var entity))
                {
                    if (Entities.Contains(entity)) return;
                    Entities.Add(entity);
                    m_action?.Invoke();
                    Direction = (entity.transform.position - transform.position).normalized;
                    // entity.ApplyDamage(m_damage);
                    // m_shotsTimer = data.shotsTimer;
                    // Debug.Log($"{gameObject.name} at {transform.position} trigger {other.gameObject.name}");
                }
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if ((m_layerMask & (1 << other.gameObject.layer)) != 0)
            {
                if (other.gameObject.TryGetComponent<BaseEntity>(out var entity))
                {
                    if (Entities.Contains(entity))
                    {
                        Entities.Remove(entity);
                    }
                }
            }
        }
    }
}