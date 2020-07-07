using UnityEngine;

namespace UnknownFPSGame.Scripts.Weapon
{
    public abstract class Gun : Weapon
    {
        [SerializeField] protected GameObject   BulletPrefab;
        [SerializeField] protected Transform    Burrel;
        [SerializeField] protected int          CurrentAmmo;
        [SerializeField] protected int          MaxAmmo; // MaxAmmo

        //[SerializeField] float      Range;//Niedokładność strzału

        protected abstract void Shoot();

        public abstract void Reload();
    }
}