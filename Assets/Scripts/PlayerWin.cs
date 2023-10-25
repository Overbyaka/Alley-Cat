using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerWin : MonoBehaviour
{
    private float spawnDoorCooldown;
    [SerializeField] private GameObject[] doors;
    [SerializeField] private GameObject win;
    [SerializeField] private GameObject lose;
    [SerializeField] private GameObject crate;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask ropeLayer;
    [SerializeField] private LayerMask barrelLayer;
    private float boxColliderCooldown;
    private bool isCrate;

    int currentDoor;
    private void Awake()
    {
        currentDoor = (int)(Random.value * 100) % 5;
        doors[currentDoor].SetActive(true);
        spawnDoorCooldown = 0;
        int i = (int)(Random.value * 100) % 2;
        isCrate = (i == 0);
        print(isCrate);
        if(isCrate)
        {
            crate.transform.position = doors[currentDoor].transform.position;
            crate.SetActive(true);
        }
        boxColliderCooldown = 0;
    }
    private void Update()
    {
        if(spawnDoorCooldown > 3.0f)
        {
            doors[currentDoor].SetActive(false);
            int k = currentDoor;
            while(k == currentDoor)
            {
                k = (int)(Random.value * 100) % 5;
            }
            currentDoor = k;
            doors[currentDoor].SetActive(true);
            
            k = (int)(Random.value * 100) % 2;
            isCrate = (k == 0);
            print(isCrate);
            if (isCrate)
            {
                crate.transform.position = doors[currentDoor].transform.position;
                crate.SetActive(true);
            }
            boxColliderCooldown = 0;
            spawnDoorCooldown = 0;
        }

        else
        {
            spawnDoorCooldown += Time.deltaTime;
        }
        if(crate.activeSelf)
        {
            if(boxColliderCooldown > 0.2f)
            {
                if (!crate.GetComponent<BoxCollider2D>().enabled)
                {
                    crate.GetComponent<BoxCollider2D>().enabled = true;
                }
                if (onRopeDown() || onBarrel())
                {
                    crate.GetComponent<BoxCollider2D>().enabled = false;
                }
                if (isGrounded())
                {
                    crate.SetActive(false);
                }
            }
            else
            {
                boxColliderCooldown += Time.deltaTime;
            }
            
        }
    }
    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(crate.GetComponent<BoxCollider2D>().bounds.center, crate.GetComponent<BoxCollider2D>().bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool onRopeDown()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(crate.GetComponent<BoxCollider2D>().bounds.center, crate.GetComponent<BoxCollider2D>().bounds.size, 0, Vector2.down, 0.1f, ropeLayer);
        return raycastHit.collider != null;
    }
    private bool onBarrel()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(crate.GetComponent<BoxCollider2D>().bounds.center, crate.GetComponent<BoxCollider2D>().bounds.size, 0, Vector2.down, 0.1f, barrelLayer);
        return raycastHit.collider != null;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Win")
        {
            doors[currentDoor].SetActive(false);
            gameObject.SetActive(false);
            crate.SetActive(false);
            win.SetActive(true);
            gameObject.transform.position = new Vector2(-9.43f, -3.52f);
            Awake();

        }
        else if(collision.transform.tag == "Lose")
        {
            doors[currentDoor].SetActive(false);
            gameObject.SetActive(false);
            crate.SetActive(false);
            lose.SetActive(true);
            print("lose");
            gameObject.transform.position = new Vector2(-9.43f, -3.52f);
            Awake();
        }
    }
}
