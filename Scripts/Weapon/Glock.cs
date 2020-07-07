using System;
using UnityEngine;

namespace UnknownFPSGame.Scripts.Weapon
{
    class Glock : Gun
    {
        private void Start()
        {
            if (!Burrel) Burrel = this.transform;
            CurrentAmmo         = MaxAmmo;
            _fireTime           = 0f;

            //transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
        }

        public override void Attack()
        {
            Shoot();
        }

        protected override void Shoot()
        {
            if (CurrentAmmo == 0)
                Debug.Log("Empty magazine.");

            if (Time.time > _fireTime && CurrentAmmo > 0)
            {
                _fireTime = Time.time + _fireRate;
                GameObject bullet = Instantiate(BulletPrefab, Burrel.transform.position, Burrel.transform.rotation * Quaternion.Euler(90, 90, 0)) as GameObject;
                --CurrentAmmo;
                Debug.Log("Shooot!");
            }
        }

        public override void Reload()
        {
            throw new NotImplementedException();
        }
    }
}
