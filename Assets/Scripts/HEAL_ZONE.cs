using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HEAL_ZONE : MonoBehaviour
{
    public float heal;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player_Controller pc = other.gameObject.GetComponent<Player_Controller>();
            pc.Healing(heal);
        }
    }
}
