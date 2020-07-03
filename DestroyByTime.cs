using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTime : MonoBehaviour
{
    [SerializeField] float Second;
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
