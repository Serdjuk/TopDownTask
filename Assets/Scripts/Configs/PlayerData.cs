using UnityEngine;
using Weapons;

namespace Configs
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Game Configuration/PlayerData")]
    public class PlayerData : BaseEntityData
    {
        [Header("Тип оружия. (ближний бой / дальний бой)")]
        public WeaponType weaponType;
    }
}
