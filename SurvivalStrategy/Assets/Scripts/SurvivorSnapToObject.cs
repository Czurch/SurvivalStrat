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
    private float distance_to_tile;
    public float arcHeight;

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
                Vector3 current_pos = gameObject.transform.position;
                Vector3 tile_pos = slot_occupied.transform.position;
                float current_distance = Vector3.Distance(gameObject.transform.position, tile_pos);
                //Debug.Log(current_distance);
                if (current_distance > 0.1)
                {
                    tile_pos.y = 0.4f;
                    current_pos = Vector3.MoveTowards(gameObject.transform.position, tile_pos, Time.deltaTime * snapSpeed);
                    current_pos.y = MovementFormula.parabolicArc(arcHeight, distance_to_tile, current_distance);
                    Debug.Log(current_pos.y);
                    gameObject.transform.position = current_pos;
                }
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
                    Bind(col.gameObject);
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
                    Bind(temp);
                }
                break;

            case "Bunk":
                break;
        }


    }

    void Bind(GameObject snap)
    {
        slot_occupied = snap;
        distance_to_tile = Vector3.Distance(gameObject.transform.position, slot_occupied.transform.position);
        snapped = true;
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

    public void checkDistance()
    {
        distance_to_tile = Vector3.Distance(gameObject.transform.position, slot_occupied.transform.position);
        Debug.Log("Distance to Tile is " + distance_to_tile);
    }
}
     
