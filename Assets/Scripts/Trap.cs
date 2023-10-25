using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private float speed;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.localScale == new Vector3(1, 1, 1))
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + speed * Time.deltaTime);
        }
        else
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - speed * Time.deltaTime);
        }
        if (transform.position.y < -3.7)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (transform.position.y > -2.2)
        {
            transform.localScale = new Vector3(1, -1, 1);
        }
    }
}
