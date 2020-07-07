using UnityEngine;

namespace UnknownFPSGame.Scripts.Enemy
{
    public abstract class EnemyControl : MonoBehaviour
    {
        [SerializeField] private EnemyData EnemyData = null;
        private GameObject _enemyPrefab;
        private float _health;
        private float _speed;

        public float Health
        {
            set { _health = value; }
            get { return _health; }
        }

        public float Speed
        {
            set { _speed = value; }
            get { return _speed; }
        }

        public void UpdateData()
        {
            if (EnemyData != null)
            {
                _health = EnemyData.Health;
                _speed = EnemyData.Speed;
                _enemyPrefab = EnemyData.EnemyPrefab;
            }
            else 
            {
                Debug.LogError("Your enemy didn't load data.");
            }
        }

        public abstract void TakeDamage(float damage);
    }
}