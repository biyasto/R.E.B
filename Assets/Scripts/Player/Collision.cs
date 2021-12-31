using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collision : MonoBehaviour
{

    [Header("Layers")]
    public LayerMask groundLayer;

    [Space]

    public bool onGround;
    public bool onWall;
    public bool isDead;
    public bool isBounding;
    public bool resetDash;
    public bool inParented;
    public bool outParented;
    [Space]

    [Header("Collision")]

    public float collisionRadius = 0.25f;
    public Vector2 bottomOffset, rightOffset, leftOffset;
    private Color debugCollisionColor = Color.red;

    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        resetDash = false;
        inParented = false;
        outParented = false;
    }

    // Update is called once per frame
    void Update()
    {  
        onGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, groundLayer);
     
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        _ = new Vector2[] { bottomOffset, rightOffset, leftOffset };

        Gizmos.DrawWireSphere((Vector2)transform.position  + bottomOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, collisionRadius);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Danger"))
        {
            isDead = true;

        }
        if (collision.CompareTag("Bound"))
        {
            isBounding = true;
        }
        if (collision.CompareTag("Energy"))
        {
            resetDash = true;
            Destroy(collision.gameObject);
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("platform"))
        {
            transform.parent = collision.gameObject.transform;
            inParented = true;
            outParented = false;
        }
        if (collision.gameObject.CompareTag("Danger"))
        {
            isDead = true;

        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("platform"))
        {
            transform.parent = null;
            inParented = false;
            outParented = true;
        }
    }

}
