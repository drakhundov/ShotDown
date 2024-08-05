using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 20.0f;

    public float maxDistacnce = 100f;
    public float radius = 0.0f;

    public float bullets = 10.0f;

    public float fireRate = 0.7f;
    private float nextFire;

    private AudioSource audio;
    private Animation anim;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        anim = GetComponent<Animation>();
    }

    public bool ReadyForShoot ()
    {
        if ((Time.time > nextFire) && bullets > 0)
        {
            nextFire = Time.time + fireRate;

            return true;
        } 
        else return false;
    }

    public void ShotEffects ()
    {
        if (audio != null) audio.Play ();
        if (anim != null) anim.Play ("shoot");
    }

    public void Shoot (Vector3 origin, Vector3 direction)
    {
        RaycastHit[] hits = Physics.SphereCastAll (origin, radius, direction, maxDistacnce);

        foreach (RaycastHit hit in hits)
        {
            if (hit.transform.gameObject.tag == "Enemy")
            {
                EnemyControl enemy = hit.transform.gameObject.GetComponent<EnemyControl> ();
                if (enemy != null) enemy.Damage (damage);

                else 
                {
                    PlayerControl player = hit.transform.gameObject.GetComponent<PlayerControl>();
                    if (player != null) 
                    {
                        Debug.Log ("yes");
                        player.Damage (damage);
                    }
                }
            }
        }
    }

    private void Indicate (Vector3 pos)
    {
        GameObject sphere = GameObject.CreatePrimitive (PrimitiveType.Sphere);
        sphere.transform.position = pos;
    }
}
