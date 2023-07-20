using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selasai : MonoBehaviour
{
    [SerializeField] GameObject canvaWin;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Invoke("win", 2f);
        }
    }

    void win()
    {
        canvaWin.SetActive(true);

    }

}
