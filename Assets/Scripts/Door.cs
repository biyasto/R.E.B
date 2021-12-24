using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isOpen = false;
    public float open_direction=1;
    public Vector2 startPos;
    public Vector2 endPos;
    public Rigidbody2D rb;
    void Start()
    {
        isOpen = false;
        rb = GetComponent<Rigidbody2D>();
        startPos = rb.position;
        endPos = new Vector2(rb.position.x, rb.position.y + 1f);
    }

    // Update is called once per frame
    void Update()
    {
        //if (transform.position.x == transform.position.x && transform.position.y == transform.position.y+0.1f)
          //  haveOpen = true;
        if (isOpen)
        {
            rb.velocity = new Vector2(0, 900f * Time.deltaTime);
        }
        if(rb.position.y >= endPos.y)
        {
            isOpen=false ;
        }
    }
}
