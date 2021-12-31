using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    public AudioSource soundDash;
    public AudioSource soundDie;
    public GameOver GameOverScreen;
    private Collision coll;
    [HideInInspector]
    public Rigidbody2D rb;
    private AnimationScript anim;
    [Space]
    [Header("Stats")]
    public float speed = 10;
    public float jumpForce = 50;
    public float slideSpeed = 5;
    public float wallJumpLerp = 10;
    public float dashSpeed = 20;

    [Space]
    [Header("Booleans")]
    public bool canMove;
    public bool wallJumped;
    public bool isDashing;
    public bool isDead;
    [Space]

    private bool groundTouch;
    private bool hasDashed;
    private float jumpTimeCounter;
    public bool invincible=false;
    public bool StopControl = false;
    public int side = 1;

    [Space]
    [Header("Polish")]
    public ParticleSystem dashParticle;
    public ParticleSystem jumpParticle;
    //public ParticleSystem wallJumpParticle;
    //public ParticleSystem slideParticle;

    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collision>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<AnimationScript>();
        GetComponent<BetterJumping>().enabled = true;
    }

    // Update is called once per frame
   
    void Update()
    {
        if (StopControl) { Pause();return; };
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        float xRaw = Input.GetAxisRaw("Horizontal");
        float yRaw = Input.GetAxisRaw("Vertical");
        Vector2 dir = new Vector2(x, y);

        Walk(dir);
        anim.SetHorizontalMovement(x, y, rb.velocity.y);
        rb.gravityScale = 3;

        MovingPlatformFix();


        if ((coll.isDead && !invincible) || Input.GetButton("Cancel"))
        {
            soundDie.Play();
            Reload();
        }
        //cheat
        if (Input.GetButtonDown("Submit"))
        {
            invincible = !invincible;
        }
        if(coll.isDead && !invincible)
        {
            coll.isDead = false;
        }
        //
        if (Input.GetButtonDown("Jump") && !coll.isBounding)
        {
            anim.SetTrigger("jump");

            if (coll.onGround)
                Jump(Vector2.up);
      
        }

        if (Input.GetButtonDown("Fire1") && !hasDashed)
        {
            if(xRaw != 0 || yRaw != 0)
                Dash(xRaw, yRaw);
        }

        if (coll.onGround && !groundTouch)
        {
            GroundTouch();
            groundTouch = true;
        }

        if(!coll.onGround && groundTouch)
        {
            groundTouch = false;
        }
      
        if(x > 0)
        {
            side = 1;
            anim.Flip(side);
        }
        if (x < 0)
        {
            side = -1;
            anim.Flip(side);
        }

        if (coll.resetDash)
        {
            hasDashed = false;
            isDashing = false;
            coll.resetDash = false;
        }
      if (coll.isBounding==true)
        {
            if (jumpTimeCounter > 0)
            {
                Jump(Vector2.up);
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                groundTouch = false;
                coll.isBounding = false;
            }
        }
        else
        {
            jumpTimeCounter = 0.1f;
        }
    }
  
    void Pause()
    {
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0;
    }
    void Reload()
    {
        StopControl = true;
        GameOverScreen.Setup();
    }    
        void GroundTouch()
    {
        hasDashed = false;
        isDashing = false;

        side = anim.sr.flipX ? -1 : 1;

        jumpParticle.Play();
    }

    private void Dash(float x, float y)
    {
        soundDash.Play();
        Camera.main.transform.DOComplete();
        Camera.main.transform.DOShakePosition(.2f, .5f, 14, 90, false, true);
        FindObjectOfType<RippleEffect>().Emit(Camera.main.WorldToViewportPoint(transform.position));

        hasDashed = true;

        anim.SetTrigger("dash");

        rb.velocity = Vector2.zero;
        Vector2 dir = new Vector2(x, y);

        rb.velocity += dir.normalized * dashSpeed;
        StartCoroutine(DashWait());
    }

    IEnumerator DashWait()
    {
        FindObjectOfType<GhostTrail>().ShowGhost();
        StartCoroutine(GroundDash());
        DOVirtual.Float(14, 0, .8f, RigidbodyDrag);

        dashParticle.Play();
        rb.gravityScale = 0;
        wallJumped = true;
        isDashing = true;

        yield return new WaitForSeconds(.3f);

        dashParticle.Stop();
        rb.gravityScale = 3;
       wallJumped = false;
        isDashing = false;
    }

    IEnumerator GroundDash()
    {
        yield return new WaitForSeconds(.15f);
        if (coll.onGround)
            hasDashed = false;
    }

    public void MovingPlatformFix()
    {
        if (coll.inParented)
        {
            rb.interpolation = RigidbodyInterpolation2D.None;
            coll.inParented = false;
        }
       
        if(coll.outParented)
        {
            rb.interpolation = RigidbodyInterpolation2D.Interpolate;
            coll.outParented = false;
        }
    }
    private void Walk(Vector2 dir)
    {
        if (!canMove)
            return;

        if (!wallJumped)
        {
           rb.velocity = new Vector2(dir.x * speed, rb.velocity.y);
        }
      else
        {
            rb.velocity = Vector2.Lerp(rb.velocity, (new Vector2(dir.x * speed, rb.velocity.y)), wallJumpLerp * Time.deltaTime);
        }
    }

    private void Jump(Vector2 dir)
    {
      
        ParticleSystem particle =  jumpParticle;

        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.velocity += dir * jumpForce;

        particle.Play();
    }
   

    void RigidbodyDrag(float x)
    {
        rb.drag = x;
    }

   
}
