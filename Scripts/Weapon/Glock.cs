using System;
using UnityEngine;

namespace UnknownFPSGame.Scripts.Weapon
{
    class Glock : Gun
    {
        [SerializeField] Camera FpsCamera;

        private void Start()
        {
            if (!Barrel) Barrel = this.transform;
            CurrentAmmo         = MaxAmmo;
            _fireTime           = 0f;

            if (!FpsCamera)
                FpsCamera = Camera.main;
        }

        private void Update()
        {
            RaycastHit hit;
            if (Physics.Raycast(FpsCamera.transform.position, FpsCamera.transform.forward, out hit, 1000))
            {
                Debug.DrawLine(FpsCamera.transform.position, hit.point, Color.yellow);
            }
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
                Barrel.transform.LookAt(PointToLookAt());
                GameObject bullet = Instantiate(BulletPrefab, Barrel.transform.position, Barrel.transform.rotation) as GameObject;
                --CurrentAmmo;
                Debug.Log("Shooot!");
            }
        }

        public override void Reload()
        {
            throw new NotImplementedException();
        }

        private Vector3 PointToLookAt()
        {
            RaycastHit hit;
            int layerMask = ~LayerMask.GetMask("Player");
            if (Physics.Raycast(FpsCamera.transform.position, FpsCamera.transform.forward, out hit, 1000, layerMask))
            {
                Debug.DrawLine(FpsCamera.transform.position, hit.point, Color.yellow);
                return hit.point;
            }
            return Vector3.zero;
        }
    }
}
