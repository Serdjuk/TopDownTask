using Configs;
using UI;
using UnityEngine;
using Weapons;
using Zenject;

namespace Player
{
    public class PlayerController : BaseEntity
    {
        [SerializeField] PlayerData playerData;
        [SerializeField] Animation spriteAnimation;
        [SerializeField] HealthBar healthBar;
        [SerializeField] DamagePanel damagePanel;
        Rigidbody2D m_rigidbody;
        Collider2D m_collider;

        NewInputSystem m_input;
        
        [SerializeField] BaseWeapon[] weapons;
        BaseWeapon m_weapon;
        int m_health;
        int m_weaponIndex;

        Vector2 m_direction;

        void Start()
        {
            m_health = playerData.health;
            m_rigidbody = GetComponent<Rigidbody2D>();
            m_collider = GetComponent<Collider2D>();
            ChangeWeapon();
        }

        public void Install(NewInputSystem input)
        {
            m_input = input;
        }

        void Update()
        {
            if (m_input == null) return;
            m_direction = m_input.Player.Move.ReadValue<Vector2>() / 16.0f;
        }

        void PlayAnimation()
        {
            spriteAnimation.Rewind();
            spriteAnimation.Play();
        }

        void FixedUpdate()
        {
            if (m_rigidbody == null) return;
            m_rigidbody.MovePosition(m_direction + m_rigidbody.position);
        }

        public void ChangeWeapon()
        {
            var position = transform.position;
            if (m_weapon == null)
            {
                m_weapon = Instantiate(weapons[0], position, Quaternion.identity, transform);
            }
            else
            {
                Destroy(m_weapon.gameObject);
                if (m_weaponIndex == 0)
                {
                    m_weapon = Instantiate(weapons[1], position, Quaternion.identity, transform);
                }
                else
                {
                    m_weapon = Instantiate(weapons[0], position, Quaternion.identity, transform);
                }
                m_weaponIndex ^= 1;
            }
            
            m_weapon.Install(PlayAnimation, LayerMask.GetMask("Enemy"));
        }

        public override void ApplyDamage(int damage)
        {
            if (m_health <= 0) return;
            m_health -= damage;

            if (m_health <= 0)
            {
                Destroy(gameObject);
            }

            healthBar.SetSize((float)m_health / playerData.health);
            damagePanel.Initialize(damage);
        }

        
    }
}