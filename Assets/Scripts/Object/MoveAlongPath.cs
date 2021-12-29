using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAlongPath : MonoBehaviour
{
    public Vector2[] setPaths;
    public int currentPathIndex = 0;
    public float  speed = 2f;
    public Vector2 Position;
    // Start is called before the first frame update
    void Start()
    {
        Position = transform.position;
       // transform.position = setPaths[0];
       

    }

    // Update is called once per frame
    void Update()
    {
     
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(setPaths[currentPathIndex].x +Position.x, setPaths[currentPathIndex].y+Position.y), speed * Time.deltaTime);

        if (transform.position.x == setPaths[currentPathIndex].x+Position.x && transform.position.y == setPaths[currentPathIndex].y +Position.y)
        {
            currentPathIndex++;
            if (currentPathIndex >= setPaths.Length)
            {
                currentPathIndex = 0;
            }
        }
    }
}
