using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "HenchmanEnemyData", menuName = "Game Configuration/Enemies/Henchman Enemy Data")]

    public class HenchmanEnemyData : BaseEntityData
    {
        [Header("Урон при контакте.")]
        public int contactDamage = 50;
    }
}