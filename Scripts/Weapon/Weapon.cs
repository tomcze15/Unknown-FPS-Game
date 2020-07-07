using UnityEngine;

namespace UnknownFPSGame.Scripts.Weapon
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] protected float _fireRate;
        [SerializeField] protected float _damage;
        protected float _fireTime;

        public float FireRate 
        {
            set { _fireRate = value; }
            get => _fireRate;
        }

        public float Damage
        {
            set { _damage = value; }
            get => _damage;
        }

        public abstract void Attack();
    }
}
