using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SawSpin : MonoBehaviour
{
    public float SpinSpeed = 1f;
  
    void Update()
    {
        transform.Rotate(0, 0, 360 * SpinSpeed * Time.deltaTime);
    }
}
