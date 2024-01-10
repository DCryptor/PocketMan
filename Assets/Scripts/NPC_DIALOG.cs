using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_DIALOG : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            print("Hello player!");
        }
    }
}
