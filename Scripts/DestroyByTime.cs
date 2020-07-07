using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnknownFPSGame.Scripts
{
    public class DestroyByTime : MonoBehaviour
    {
        [SerializeField] float Second = 0f;
        private float _currentTime = 0f;

        // Update is called once per frame
        void Update()
        {
            if (_currentTime > Second)
                Destroy(this);
            else
                _currentTime += Time.deltaTime;
        }
    }
}