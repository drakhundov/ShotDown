using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float health = 100.0f;
    
    private Camera camera;

    void Start ()
    {
        camera = GetComponentInChildren<Camera>();
    }

    void Update ()
    {
        if (Input.GetButton ("Fire1")) 
        {
            switch (GetComponentInChildren<PlayerInventory>().UsingThing ())
            {
                case "Grenade":
                    ThrowGrenade ();
                    break;

                case "Gun":
                    Shoot ();
                    break;
            }
        }
    }

    void Shoot ()
    {
        Gun gun = GetComponentInChildren<Gun>();

        if (gun != null)
        {
            if (gun.ReadyForShoot ())
            {
                gun.Shoot(
                    camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f)), 
                    camera.transform.forward
                );  

                gun.ShotEffects ();
            }
        }
    }

    void ThrowGrenade ()
    {
        GrenadeControl grenade = GetComponentInChildren<GrenadeControl>();

        if (grenade != null)
        {
            if (grenade.ReadyForThrow ())
            {
                grenade.Throw (camera.transform.forward);
            }
        }
    }
    
    public void Damage (float damage)
    {
        health -= damage;
        GetComponent<PlayerUI>().SetHealthBar (health);

        if (health <= 0) StartCoroutine (Die ());
    }

    private IEnumerator Die ()
    {
        yield return new WaitForSeconds (1.0f);
        Application.Quit ();
    }
}
