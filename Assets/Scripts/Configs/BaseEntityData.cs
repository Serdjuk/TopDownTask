using UnityEngine;

namespace Configs
{
    public class BaseEntityData : ScriptableObject
    {
        [Header("Скорость передвижения.")]
        public float movementSpeed = 1;
        [Header("Количество ХП")]
        public int health = 200;
    }
}