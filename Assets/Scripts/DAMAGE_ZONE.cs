using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DAMAGE_ZONE : MonoBehaviour
{
    public float damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player_Controller pc = other.gameObject.GetComponent<Player_Controller>();
            pc.HitDamage(damage);
        }
    }
}
