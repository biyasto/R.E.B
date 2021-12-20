using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{

    Vector2 startPos;
    public bool respawns = true;
    public Rigidbody2D rb;
    public bool isTouched;
    public float freezeTime;
    public float respawnsTime;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;
        isTouched = false;
        freezeTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTouched) { freezeTime -= Time.deltaTime; respawnsTime -= Time.deltaTime; }
            
        if (freezeTime<-0.01)
        {
            rb.isKinematic = false;
        }
        Respawn();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isTouched && collision.gameObject.tag == "Player")
        {
        
            isTouched = true;
            freezeTime = 0.75f;
            respawnsTime = 5f;


        }
        var layerMask = collision.gameObject.layer;
        if (layerMask == 6)
        {
            GetComponent<Collider2D>().enabled = false;
        }
    }
    void Respawn()
    {
        if (respawns && respawnsTime < 0.01)
        {
            GetComponent<Collider2D>().enabled = true;
            rb.isKinematic = true;
            rb.velocity = new Vector2(0, 0);
            transform.position = startPos;
        }
    }
 
}
