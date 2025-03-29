using Configs;
using Mangers;
using UI;
using UnityEngine;
using Weapons;

namespace Enemies
{
    public class HenchmanEnemy : BaseEntity
    {
        [SerializeField] HenchmanEnemyData data;
        [SerializeField] BaseWeapon weapon;
        [SerializeField] HealthBar healthBar;
        [SerializeField] DamagePanel damagePanel;
        [SerializeField] Animation anim;
        [SerializeField] Collider2D objectCollider;
        Rigidbody2D m_rigidbody;
        Transform m_playerTransform;
        GameManager m_gameManager;

        int m_health;
        float m_speed;

        Vector2 m_direction;

        public void Initialize(GameManager gameManager, Camera cam)
        {
            m_gameManager = gameManager;
            m_playerTransform = gameManager.GetPlayer().transform;
            var canvas = healthBar.GetComponent<Canvas>();
            canvas.worldCamera = cam;
        }

        void Start()
        {
            m_health = data.health;
            m_speed = data.movementSpeed;
            m_rigidbody = GetComponent<Rigidbody2D>();
            weapon.Install(PlayAnimation, LayerMask.GetMask("Player"));
        }

        void Update()
        {
            if (m_health == 0)
            {
                if (!healthBar.Alive())
                {
                    Destroy(gameObject);
                }

                return;
            }

            if (m_playerTransform == null)
            {
                var player = m_gameManager.GetPlayer();
                if (player != null)
                {
                    m_playerTransform = player.transform;
                }
                return;
            }
            m_direction = (m_playerTransform.position - transform.position).normalized;
        }

        void PlayAnimation()
        {
            anim.Stop();
            anim.Play();
        }

        void FixedUpdate()
        {
            m_rigidbody.linearVelocity = m_direction * m_speed;
        }

        public override void ApplyDamage(int damage)
        {
            if (m_health <= 0) return;
            m_health -= damage;

            if (m_health <= 0)
            {
                m_health = 0;
                objectCollider.enabled = false;
                m_direction = Vector2.zero;
            }

            healthBar.SetSize((float)m_health / data.health);
            damagePanel.Initialize(damage);
        }
    }
}