using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnknownFPSGame.Scripts.CharacterController.FirstPerson;

namespace UnknownFPSGame.Scripts.Weapon
{
    public class WeaponHolderController : MonoBehaviour
    {
        [SerializeField] FirstCharacterController characterController;
        [SerializeField] List<GameObject> Weapon = new List<GameObject>();
        public GameObject CurrentHoldGun;
        private Weapon _weapon;

        // Start is called before the first frame update
        void Start()
        {
            if (!characterController)
                characterController = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstCharacterController>();

            foreach (var weapon in this.GetComponentsInChildren<Transform>())
            {
                if(weapon.GetComponent<Weapon>()) 
                    Weapon.Add(weapon.gameObject);
            }

            CurrentHoldGun = Weapon[0];
            _weapon = CurrentHoldGun.GetComponent<Weapon>();
        }

        // Update is called once per frame
        void Update()
        {
            if (characterController.advancedSettings.isShoot)
                _weapon.Attack();
        }
    }
}