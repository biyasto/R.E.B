using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Penduling : MonoBehaviour
{
    public float delta = 1.5f;
    public float speed = 2.0f;
    public float direction = 1;
    public float startTime = 0;
    private Quaternion startPos;
    
    void Start()
    {
        startPos = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion a = startPos;
        a.z += direction * (delta + Mathf.Sin((Time.time+startTime)* speed));
        transform.rotation= a;
    }
}
