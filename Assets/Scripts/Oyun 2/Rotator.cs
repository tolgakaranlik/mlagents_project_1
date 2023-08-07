using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public Vector3 AngularSpeed = Vector3.one;

    void Start()
    {
        
    }

    void Update()
    {
        transform.Rotate(AngularSpeed * Time.deltaTime);
    }
}
