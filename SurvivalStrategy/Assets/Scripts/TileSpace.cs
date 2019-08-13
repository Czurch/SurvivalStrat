using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpace : MonoBehaviour
{
    public bool isOccupied;
    public GameObject occupant;
    // Start is called before the first frame update
    void Start()
    {
        isOccupied = false;
        occupant = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Bind(GameObject obj)
    {
        isOccupied = true;
        occupant = obj;
    }

    public void Unbind()
    {
        isOccupied = false;
        occupant = null;
    }
}
