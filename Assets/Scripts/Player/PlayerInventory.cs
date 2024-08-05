using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<GameObject> inventory = new List<GameObject>();

    private int usingThingID = 0;

    private KeyCode[] keys = {
        KeyCode.Alpha1,
        KeyCode.Alpha2
    };

    void Start ()
    {
        foreach (Transform thing in transform)
        {
            inventory.Add (thing.gameObject);
        }
    }

    void Update ()
    {
        for (int i = 0; i < keys.Length; ++i)
        {
            if (Input.GetKeyDown (keys[i]))
            {
                ChangeTo (i);
                break;
            }
        }

        if (Input.GetKeyDown (KeyCode.E))
        {
            ChangeTo (usingThingID + 1);
        }
        if (Input.GetKeyDown (KeyCode.Q))
        {
            ChangeTo (usingThingID - 1);
        }
    }

    void ChangeTo (int id)
    {
        if (id < 0) id += inventory.Count;
        else if (id >= inventory.Count) id -= inventory.Count;

        if (id != usingThingID)
        {
            try
            {
                inventory[usingThingID].SetActive (false);
                inventory[id].SetActive (true);
            }
            catch {  }
        }

        usingThingID = id;
    }

    public string UsingThing ()
    {
        if (inventory[usingThingID].name == "grenade") return "Grenade";
        else return "Gun";
    }
}
