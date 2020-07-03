using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnknownFPSGame.GeneralScripts;

public class Gun : MonoBehaviour
{
    [SerializeField] GameObject BulletPrefab;
    [SerializeField] Transform  Burrel;
    [SerializeField] float      FireRate;
    [SerializeField] int        CurrentBullet;
    [SerializeField] int        MaxBullet;

    [SerializeField] float _fireTime;

    //[SerializeField] float      Range;//Niedokładność strzału

    // Start is called before the first frame update
    void Start()
    {
        if (!Burrel) Burrel = this.transform;
        CurrentBullet = MaxBullet;
        _fireTime = 0f;

        //transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
    }

    public void Shoot()
    {
        if(CurrentBullet == 0)
            Debug.Log("Empty magazine.");

        if (Time.time > _fireTime && CurrentBullet > 0)
        {
            _fireTime = Time.time + FireRate;
            GameObject bullet = Instantiate(BulletPrefab, Burrel.transform.position, Burrel.transform.rotation * Quaternion.Euler(90, 90, 0)) as GameObject;
            --CurrentBullet;
            Debug.Log("Shooot!");
        }
    }
}
