using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public float speed = 5.0f;
    public float lookRadius = 10.0f;
    public float runAfterPlayerSeconds = 3.0f;
    public Transform eye;

    private bool playerFound;

    Transform target;
    EnemyControl enemy;

    void Start ()
    {
        target = GameObject.FindGameObjectWithTag ("Player").transform;

        enemy = GetComponent<EnemyControl>();

        playerFound = false;
    }


    void Update ()
    {
        if (enemy.Alive ())
        {
            transform.Translate (0, 0, speed * Time.deltaTime);

            Vector3 directionToPlayer  = target.position - eye.position;
            RaycastHit hit;

            if (Physics.Raycast (eye.position, directionToPlayer, out hit))
            {
                if (hit.transform == target)
                {
                    if (!playerFound) StartCoroutine (runAfterPlayer ());
                    else
                    {
                        FaceTarget ();

                        Gun gun = GetComponentInChildren<Gun>();

                        if(gun != null)
                        {
                            if (gun.ReadyForShoot ())
                            {
                                gun.ShotEffects ();
                                hit.transform.gameObject.GetComponent<PlayerControl>().Damage (gun.damage);
                            }
                        }
                    }
                }
            }
        }
    }

    IEnumerator runAfterPlayer ()
    {
        playerFound = true;
        yield return new WaitForSeconds (runAfterPlayerSeconds);
        playerFound = false;
    }

    void FaceTarget ()
    {
        Vector3 targetDirection = (target.position - eye.position).normalized;
        float step = speed * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards (eye.forward, targetDirection, step, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }
}
