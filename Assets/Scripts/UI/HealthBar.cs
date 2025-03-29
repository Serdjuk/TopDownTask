using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HealthBar : MonoBehaviour
    {
        Camera m_camera;
        Canvas m_canvas;
        [SerializeField] Slider slider;
        float m_target;


        void Start()
        {
            m_target = slider.value;
        }

        void Update()
        {
            slider.value = Mathf.MoveTowards(slider.value,m_target,Time.deltaTime * 3.0f);
        }

        public void SetSize(float health)
        {
            m_target = health;
        }
        public bool Alive() => slider.value > 0.0f; 
    }
}