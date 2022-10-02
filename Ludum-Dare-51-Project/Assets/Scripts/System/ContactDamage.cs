using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactDamage : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float damageInterval;

    private float lastDamageInterval;

    private void Update()
    {
        lastDamageInterval += Time.deltaTime;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            Debug.Log("Collison");
            if (lastDamageInterval >= damageInterval)
            {
                Health playerHealth = collision.collider.GetComponent<Health>();
                playerHealth.DealDamage(damage);
                lastDamageInterval = 0;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Trigger");
            if (lastDamageInterval >= damageInterval)
            {
                Health playerHealth = other.GetComponent<Health>();
                playerHealth.DealDamage(damage);
                lastDamageInterval = 0;
            }
        }
    }
}
