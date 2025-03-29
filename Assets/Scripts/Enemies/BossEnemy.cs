using System.Collections;
using Configs;
using Mangers;
using Player;
using UI;
using UnityEngine;
using Zenject;

namespace Enemies
{
    public class BossEnemy : BaseEntity
    {
        [SerializeField] HenchmanEnemy henchmanEnemy;
        [SerializeField] BossEnemyData data;
        [SerializeField] HealthBar healthBar;
        [SerializeField] DamagePanel damagePanel;
        [SerializeField] GameObject sprite;

        [Inject] GameManager m_gameManager;

        [Inject] Camera m_cam;

        Rigidbody2D m_rigidbody;
        Collider2D m_collider;

        float m_speed;
        float m_health;
        float m_respawnTime;
        float m_abilityActivationTime;
        float m_keepDistance;
        float m_henchmanSpawnDistance;
        int m_henchmanCount;
        bool m_isDead;

        Vector2 m_direction;

        void Start()
        {
            m_respawnTime = data.respawnTime;
            m_abilityActivationTime = data.abilityActivationTime;
            m_keepDistance = data.keepDistance;
            m_henchmanCount = data.henchmanCount;
            m_henchmanSpawnDistance = data.henchmanSpawnDistance;
            m_health = data.health;
            m_speed = data.movementSpeed;
            m_rigidbody = GetComponent<Rigidbody2D>();
            m_collider = GetComponent<Collider2D>();
        }


        void Update()
        {
            if (m_isDead)
            {
                WaitRespawn();
            }
            else
            {
                WaitHenchmanSpawn();
                Move();
            }
        }

        void Move()
        {
            var distance = GetDistance();
            if (distance > m_keepDistance + 1.0f)
            {
                Attack();
            }
            else if (distance < m_keepDistance)
            {
                RunAway();
            }
            else
            {
                CalmDown();
            }
        }

        void FixedUpdate()
        {
            m_rigidbody.linearVelocity = m_direction * m_speed;
        }

        void WaitHenchmanSpawn()
        {
            m_abilityActivationTime -= Time.deltaTime;
            if (m_abilityActivationTime <= 0.0f)
            {
                m_abilityActivationTime = data.abilityActivationTime;
                StartCoroutine(SpawnHenchman());
            }
        }

        IEnumerator SpawnHenchman()
        {
            var player = m_gameManager.GetPlayer();
            if (player != null)
            {
                var direction = (player.transform.position - transform.position).normalized;
                for (var i = 0; i < m_henchmanCount; i++)
                {
                    var henchman = Instantiate(henchmanEnemy, transform.position + direction * m_henchmanSpawnDistance, Quaternion.identity, null);
                    henchman.Initialize(m_gameManager, m_cam);
                    yield return null;
                }
            }
        }

        void Attack()
        {
            var player = m_gameManager.GetPlayer();
            if (player == null) return;
            m_direction = (player.transform.position - transform.position).normalized;
        }

        void RunAway()
        {
            var player = m_gameManager.GetPlayer();
            if (player == null) return;
            m_direction = (transform.position - player.transform.position).normalized;
        }

        void CalmDown()
        {
            m_direction = Vector2.Lerp(m_direction, Vector2.zero, Time.deltaTime * 2.0f);
        }

        float GetDistance()
        {
            var player = m_gameManager.GetPlayer();
            if (player == null) return 0;
            return Vector3.Distance(transform.position, player.transform.position);
        }


        public override void ApplyDamage(int damage)
        {
            if (m_health <= 0) return;
            m_health -= damage;

            if (m_health <= 0)
            {
                m_health = 0;
                m_direction = Vector2.zero;
                m_collider.enabled = false;
                sprite.SetActive(false);
                healthBar.gameObject.SetActive(false);
                m_isDead = true;
            }

            healthBar.SetSize((float)m_health / data.health);
            damagePanel.Initialize(damage);
        }

        void WaitRespawn()
        {
            m_respawnTime -= Time.deltaTime;
            if (m_respawnTime <= 0.0f)
            {
                m_health = data.health;
                m_respawnTime = data.respawnTime;
                m_abilityActivationTime = data.abilityActivationTime;
                healthBar.SetSize(1.0f);
                m_collider.enabled = true;
                sprite.SetActive(true);
                healthBar.gameObject.SetActive(true);
                m_isDead = false;
            }
        }
    }
}