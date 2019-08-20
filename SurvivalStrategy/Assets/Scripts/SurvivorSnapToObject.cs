using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivorSnapToObject : MonoBehaviour
{
    private GameObject slot_occupied;
    public SurvivorDisplay sd;
    public TileSpace ts;
    public TileSpace old_tile;
    public Squad squad;
    private bool snapped;
    private float t;
    public float snapSpeed;

    // Start is called before the first frame update
    void Start()
    {
        //gameObject.GetComponent<SurvivorDisplay>();
        snapped = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (slot_occupied != null)
        {
            if (snapped = true && Input.GetMouseButton(0) != true)
            {
                Vector3 tile_pos = slot_occupied.transform.position;
                tile_pos.y = 0.4f;
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, tile_pos, Time.deltaTime * snapSpeed);
            }
        }
    }

    //When we collide with a snappable Object we set our position to them
    void OnTriggerEnter(Collider col)
    {
        switch (col.gameObject.tag)
        {
            //IF TILESPACE
            case "TileSpace":
                ts = col.gameObject.GetComponent<TileSpace>();
                //make sure the tile isnt occupied
                if (!ts.isOccupied)
                {
                    //unpair from our previous object
                    if (slot_occupied != null)
                    {
                        Unbind();
                    }
                    //pair the object with the tile
                    Debug.Log(gameObject.name + " snapping to " + col.gameObject.name);
                    slot_occupied = col.gameObject;
                    snapped = true;
                    ts.Bind(sd.controlling_player);

                }
                else { Debug.Log("tile space for " + col.gameObject.name + " is occupied"); }
                break;

            case "Squad":
                GameObject temp;        //holds our slot_occupied if we cant join the squad
                squad = col.gameObject.GetComponent<Squad>();
                Debug.Log("we are triggering on " + col.gameObject.name);
                temp = slot_occupied;

                if (slot_occupied != null)
                {
                    Unbind();
                }

                slot_occupied = squad.addSurvivor(sd.survivor);
                if (slot_occupied != null)
                {
                    Debug.Log("slot_occupied was not null");
                    snapped = true;
                }
                else
                {
                    //we didnt return a spot for the survivor, squad is full
                    slot_occupied = temp;
                    snapped = true;
                }
                break;

            case "Bunk":
                break;
        }


    }

    void Unbind()
    {
        switch (slot_occupied.tag)
        {
            case "TileSpace":
                Debug.Log("Unbinding from previous tile");
                old_tile = slot_occupied.GetComponent<TileSpace>();
                old_tile.Unbind(sd.controlling_player);
                slot_occupied = null;
                snapped = false;
                //set slot_occupied to player bunk
                break;
            case "Squad":
                Debug.Log("Unbinding from squad");
                squad.removeSurvivor(sd.survivor, slot_occupied);
                slot_occupied = null;
                snapped = false;
                //set slot_occupied back to player's bunk
                break;
            default:
                Debug.Log(slot_occupied.tag);
                break;
        }
    }
}
     
