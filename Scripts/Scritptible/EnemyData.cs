using UnityEngine;

namespace UnknownFPSGame.Scripts.Enemy
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "UnkownFPSGame/EnemyData")]
    public class EnemyData : ScriptableObject
    {
        public float        Health;
        public float        Speed;
        public GameObject   EnemyPrefab;
    }
}