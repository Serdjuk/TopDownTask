using System;
using System.Collections;
using System.Collections.Generic;
using Configs;
using Player;
using UI;
using UnityEngine;
using Zenject;

namespace Enemies.Minions
{
    public class Minion : BaseEntity
    {
        [SerializeField] public HenchmanEnemyData data;
        [SerializeField] public HealthBar healthBar;
        [Header("Радиус обнаружения. Миньон будет следовать за целью если она в радиусе.")] [SerializeField]
        float detectRadius = 10.0f;
        [Header("Миньон выполнит подкат с атакой когда цель будет находится в радиусе.")]
        [SerializeField] float attackRadius = 1.0f;
        [Header("Время между атаками миньона.")]
        [SerializeField] float timeBetweenAttacks = 1.0f;
        MinionPool m_pool;
        int m_hp;
        int m_layerMask;

        bool m_timeAttackCome;
        float m_timeBetweenAttacks;
        float m_rollbackTime;

        Vector2 m_direction;
        Vector2 m_target;
        Rigidbody2D m_rigidbody;
        ContactFilter2D m_contactFilter;
        readonly List<Collider2D> m_colliders = new();
        Collider2D m_targetCollider;


        MinionState m_state;

        void Start()
        {
            m_timeBetweenAttacks = timeBetweenAttacks;
            m_rigidbody = GetComponent<Rigidbody2D>();
            m_rigidbody.linearDamping = 4.0f;
            m_state = MinionState.Pursuit;
            m_layerMask = LayerMask.GetMask("Player");
            m_contactFilter = new ContactFilter2D
            {
                useLayerMask = true,    // Идиотизм физики юнити. Если я передаю фильтры в метод контактов, то фильтры не должны игнорироваться и чекать дополнительный флаг.
                layerMask = new LayerMask { value = m_layerMask }
            };
        }

        [Inject]
        public void Construct(MinionPool pool)
        {
            m_pool = pool;
        }

        public void Initialize(int hp)
        {
            m_hp = hp;
            gameObject.SetActive(true);
        }
        
        void Update()
        {
            if (m_hp <= 0) return;
            m_direction = DetectTarget();
            m_timeBetweenAttacks -= Time.deltaTime;
            m_timeAttackCome = m_timeBetweenAttacks <= 0.0f;
        }

        void FixedUpdate()
        {
            if (m_hp <= 0) return;
            switch (m_state)
            {
                case MinionState.Pursuit:
                    m_rigidbody.linearVelocity = m_direction * data.movementSpeed;
                    if (CanAttack()) m_state = MinionState.Attack;
                    break;
                case MinionState.Attack:
                    m_rigidbody.AddForce(m_direction * 5.0f, ForceMode2D.Impulse);
                    m_state = MinionState.Rollback;
                    break;
                case MinionState.Rollback:
                    m_colliders.Clear();
                    m_rigidbody.GetContacts(m_contactFilter, m_colliders);
                    if (m_colliders.Count == 1)
                    {
                        if (m_colliders[0].TryGetComponent<PlayerController>(out var playerController))
                        {
                            playerController.ApplyDamage(data.contactDamage);
                            m_rigidbody.AddForce(m_direction * -5.0f, ForceMode2D.Impulse);
                            m_state = MinionState.WaitForPursuit;
                        }
                    }

                    if (m_rollbackTime > 1.0f)
                    {
                        m_rollbackTime = 0.0f;
                        m_state = MinionState.WaitForPursuit;
                    }
                    m_rollbackTime += Time.fixedDeltaTime;
                    break;
                case MinionState.WaitForPursuit:
                    if (m_rigidbody.linearVelocity.magnitude < 1.0f)
                    {
                        m_state = MinionState.Pursuit;
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override void ApplyDamage(int value)
        {
            if (m_hp == 0) return;
            m_hp -= value;
            if (m_hp <= 0)
            {
                m_hp = 0;
                healthBar.RedrawHealth(value, (float)m_hp / data.health);
                StartCoroutine(Die());
                return;
            }
            healthBar.RedrawHealth(value, (float)m_hp / data.health);
        }

        Vector2 DetectTarget()
        {
            if (m_targetCollider == null)
            {
                m_targetCollider = Physics2D.OverlapCircle(transform.position, detectRadius, m_layerMask);
            }
            else
            {
                var direction = (m_targetCollider.transform.position - transform.position).normalized;
                return direction;
            }

            return Vector2.zero;
        }

        bool CanAttack()
        {
            if (!m_timeAttackCome || m_targetCollider == null) return false;
            m_timeBetweenAttacks = timeBetweenAttacks;
            var distance = Vector2.Distance(transform.position, m_targetCollider.transform.position);
            return distance <= attackRadius;
        }
        
        IEnumerator Die()
        {
            while (healthBar.IsWorked())
            {
                yield return null;
            }
            
            
            m_pool.Despawn(this);
        }

       
    }

    internal enum MinionState
    {
        Pursuit,
        Attack,
        Rollback,
        WaitForPursuit
    }
}