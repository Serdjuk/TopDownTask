using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI damageValue;
        [SerializeField] RectTransform damageValueTransform;
        [SerializeField] Animation damageValueAnimation;
        [SerializeField] Slider slider;
        float m_target;


        void Start()
        {
            m_target = slider.value;
        }

        void Update()
        {
            slider.value = Mathf.MoveTowards(slider.value, m_target, Time.deltaTime * 3.0f);
        }

        public void SetSize(float health)
        {
            m_target = health;
        }

        public void RedrawHealth(int damage, float health)
        {
            if (damage > 0)
            {
                damageValue.text = $"-{damage}";
                damageValueAnimation.Rewind();
                damageValueAnimation.Play();
            }
            else
            {
                damageValueAnimation.Stop();
                damageValue.text = "";
            }

            m_target = health;
        }

        public bool IsWorked()
        {
            return damageValueAnimation.isPlaying || slider.value >= 0.001f;
        }

        public bool Alive() => slider.value > 0.0f;
    }
}