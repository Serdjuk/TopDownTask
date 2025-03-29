using TMPro;
using UnityEngine;

namespace UI
{
    public class DamagePanel : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI text;
        [SerializeField] RectTransform rectTransform;
        [SerializeField] Animation anim;

        public void Initialize(int value)
        {
            text.text = $"-{value}";
            gameObject.SetActive(true);
            anim.Play();
        }
    }
}