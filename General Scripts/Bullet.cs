using UnityEngine;

namespace UnknownFPSGame.GeneralScripts
{
    public class Bullet : MonoBehaviour
    {
        [Range(0.0f, 100.0f)] public float damage;
        [SerializeField] Rigidbody Rigidbody;
        [SerializeField] float Speed;

        private void Start()
        {
            if(!Rigidbody)
                Rigidbody = GetComponent<Rigidbody>();

            Rigidbody.AddForce(transform.forward * Speed);
        }

        private void OnCollisionEnter(Collision collision)
        {
            collision.gameObject.SetActive(false);
            Destroy(this.gameObject);
        }
    }
}