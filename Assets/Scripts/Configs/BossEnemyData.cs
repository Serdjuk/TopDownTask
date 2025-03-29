using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "BossEnemyData", menuName = "Game Configuration/Enemies/Boss Enemy Data")]
    public class BossEnemyData : BaseEntityData
    {
        [Header("Время возрождения. (секунд)")] public float respawnTime = 10f;

        [Header("Время между активацией способности. (секунд)")]
        public float abilityActivationTime = 5f;

        [Header("Расстояние, на котором старается находится от игрока.")]
        public float keepDistance = 10f;

        [Header("Расстояние, на котором появляются второстепенные враги.")]
        public float henchmanSpawnDistance = 1f;

        [Header("Количество второстепенных врагов, которые появляются вследствие способности.")]
        public int henchmanCount = 1;
    }
}