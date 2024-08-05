using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeControl : MonoBehaviour
{
    public float throwRate = 1.0f;
    float nextThrow;

    public GameObject grenadePrefab;

    public float throwForce = 40.0f;


    public bool ReadyForThrow ()
    {
        if (Time.time > nextThrow)
        {
            nextThrow = Time.time + throwRate;

            return true;
        }
        else return false;
    }

    public void Throw (Vector3 direction)
    {
        GameObject grenade = Instantiate (grenadePrefab, transform.position, transform.rotation);

        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        rb.AddForce (direction * throwForce, ForceMode.Impulse);
    }
}
