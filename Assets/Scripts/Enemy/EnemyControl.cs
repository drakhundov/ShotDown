using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public float health = 100.0f;

    private bool alive;
    public bool Alive () { return alive; }


    void Start ()
    {
        alive = true;
        SetColor ("blue");
    }


    public void Damage (float damage)
    {
        if (alive)
        {
            StartCoroutine (changeColor ()); // show player, that he damaged enemy

            health -= damage;
            if (health <= 0) ReactToHit ();
        }
    }

    private void ReactToHit()
    {
        if (alive)
        {
            alive = false;
            StartCoroutine (Die ());
        }
    }

    private void SetColor (string color)
    {
        if (color == "red") GetComponentInChildren<Renderer>().material.color = new Color (255, 0, 0);
        else if (color == "blue") GetComponentInChildren<Renderer>().material.color = new Color (0, 0, 255);
    }

    private IEnumerator changeColor ()
    {
        SetColor ("red");

        yield return new WaitForSeconds (0.2f);

        SetColor ("blue");
    }

    private IEnumerator Die ()
    {
        yield return new WaitForSeconds (0.05f);

        // fall
        this.transform.Rotate(-90, 0, 0);
        this.transform.Translate(0, 0, -0.5f);

        yield return new WaitForSeconds(1.5f);

        Destroy (this.gameObject);
    }
}
