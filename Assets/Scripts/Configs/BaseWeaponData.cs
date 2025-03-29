using UnityEngine;
using Weapons;

namespace Configs
{
    public class BaseWeaponData : ScriptableObject
    {
        [Header("Тип оружия. (ближний бой / дальний бой)")]
        public WeaponType type;

        [Header("Дистанция атаки.")] public float distance = 2.0f;
        [Header("Повреждения от оружия.")] public int damage = 50;
        [Header("Время между выстрелами. (сек)")] public float shotsTimer = 1.0f;
    }
}