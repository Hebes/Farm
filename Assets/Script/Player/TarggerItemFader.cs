using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TarggerItemFader : MonoBehaviour
{
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ItemFader[] faders = collision.GetComponentsInChildren<ItemFader>();
        for (int i = 0; i < faders?.Length; i++)
            faders[i].fadeOut();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ItemFader[] faders = collision.GetComponentsInChildren<ItemFader>();
        for (int i = 0; i < faders?.Length; i++)
            faders[i].fadeIn();
    }
}
