using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    // Start is called before the first frame update
    public float shootSpeed=900, shootTimer;
    public Transform shootPos;
    public GameObject bullet;
    private bool isShooting;
    public float direction = 1f;
    private float timer = 0;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < -0.01)
        {
            StartCoroutine(Shoot());
            timer = shootTimer;
        }
    
     }
    IEnumerator Shoot()
    {
       yield return new WaitForSeconds(shootTimer);
        GameObject newBullet = Instantiate(bullet, shootPos.position, Quaternion.identity);
        newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(shootSpeed*direction * Time.fixedDeltaTime, 0f);
        newBullet.transform.localScale = new Vector2(newBullet.transform.localScale.x * direction, newBullet.transform.localScale.y);

    }
}
