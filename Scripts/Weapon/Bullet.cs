using UnityEngine;
using UnknownFPSGame.Scripts.Enemy;

namespace UnknownFPSGame.Scripts.Weapon
{
    public class Bullet : MonoBehaviour
    {
        [Range(0.0f, 100.0f)] public float damage;
        [SerializeField] Rigidbody Rigidbody;
        [SerializeField] float Speed = 0;

        private void Start()
        {
            if(!Rigidbody)
                Rigidbody = GetComponent<Rigidbody>();

            Rigidbody.AddForce(transform.forward * Speed);
        }

        private void OnCollisionEnter(Collision collision)
        {
            EnemyControl enemyControl = collision.gameObject.GetComponent<EnemyControl>();
            if (enemyControl)
                enemyControl.TakeDamage(damage);
            else 
                Debug.Log("I didn't hit the enemy.");

            Destroy(this.gameObject);
        }
    }
}