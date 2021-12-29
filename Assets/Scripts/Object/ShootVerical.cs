using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootVerical : MonoBehaviour
{
    // Start is called before the first frame update



    // Start is called before the first frame update
    public float shootSpeed = 300, shootTimer=1;
    public Transform shootPos;
    public GameObject bullet;
    public float direction = 1f;
    private float timer = 0;
    public float startTime=0f;
    void Start()
    {
        timer = startTime;
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
        newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, shootSpeed * direction * Time.fixedDeltaTime);
        newBullet.transform.localScale = new Vector2(newBullet.transform.localScale.x, newBullet.transform.localScale.y * direction);
    }

}
