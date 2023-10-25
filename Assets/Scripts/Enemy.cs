using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

            if(transform.localScale == new Vector3(1, 1, 1))
            {
                transform.position = new Vector2(transform.position.x + speed*Time.deltaTime, transform.position.y);
            }
            else 
            {
                transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
            }
            if(transform.position.x < -13)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if(transform.position.x > 9)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
    }
}
