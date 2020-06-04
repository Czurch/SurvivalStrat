using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bunk : MonoBehaviour
{
    //The Bunk class holds one survivor, it may not be reused until that survivor dies.
    public GameObject bunk_prefab;
    public bool isOccupied;
    public Survivor occupant;
    public Player controlling_player;
    private BoxCollider col;
    // Start is called before the first frame update
    void Start()
    {
        col = gameObject.transform.GetComponent<BoxCollider>();
        isOccupied = false;
        occupant = null;
        controlling_player = gameObject.transform.parent.GetComponentInParent<Player>();
        setColor(new Color(0.78f, 0.66f, 0.46f, 0.2f));
    }

    public void Bind(Survivor s)
    {
        col.enabled = false;
        isOccupied = true;
        occupant = s;
        occupant.bunk = this;
        s.BindToPlayer(controlling_player);
        controlling_player.AddSurvivor(s);
        setColor(new Color(0.96f, 0.70f, 0.25f, 0.7f));
    }

    public void Unbind()
    {
        occupant = null;
        isOccupied = false;
        col.enabled = true;
        setColor(new Color(0.78f, 0.66f, 0.46f, 0.2f));
    }

    void setColor(Color c)
    {
        Renderer r = gameObject.GetComponentInChildren<Renderer>();
        Material m = r.material;
        m.color = c;
        r.material = m;
    }
}
