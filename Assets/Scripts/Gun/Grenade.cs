using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float delay = 3.0f;
    public float damage  = 100.0f;
    public float radius = 5.0f;
    public float force = 700.0f;

    public GameObject explosionEffect;

    bool exploded = false;

    float countdown;

    void Start()
    {
        countdown = delay;
    }

    void Update()
    {
        if (!exploded)
        {
            countdown -= Time.deltaTime;
            if (countdown <= 0) Explode ();
        }
    }

    void Explode ()
    {
        exploded = true;

        Instantiate (explosionEffect, transform.position, transform.rotation);

        RaycastHit[] hits = Physics.SphereCastAll (transform.position, radius, transform.forward);

        foreach (RaycastHit hit in hits)
        {
            GameObject obj = hit.transform.gameObject;

            if (obj.tag == "Enemy") obj.GetComponent<EnemyControl>().Damage (damage);
            else if (obj.tag == "Player") obj.GetComponent<PlayerControl>().Damage (damage);
            else
            {
                Rigidbody rb = obj.GetComponent<Rigidbody>();
                if (rb != null) rb.AddExplosionForce (force, transform.position, radius);
            }
        }

        Destroy (gameObject);
    }
}
