using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowDie : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject DieEffect;
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
       
       // if(collision.gameObject.tag != "Player")
        {
            Die();
        }
    }
    void Die()
    {
        Instantiate(DieEffect, new Vector2(transform.position.x+1f, transform.position.y), Quaternion.identity);
        Destroy(gameObject);
    }    
}
