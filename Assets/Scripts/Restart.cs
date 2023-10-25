using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restart : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private void OnEnable()
    {
        StartCoroutine(wait());
    }
    IEnumerator wait()
    {
        yield return new WaitForSecondsRealtime(2);
        gameObject.SetActive(false);
        player.SetActive(true);
    }
}
