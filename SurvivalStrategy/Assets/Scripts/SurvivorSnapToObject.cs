using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivorSnapToObject : MonoBehaviour
{
    private GameObject slot_occupied;
    public SurvivorHolder holder;
    private BoxCollider box_collider;
    public TileSpace current_tile;
    public TileSpace old_tile;
    public Squad squad;
    private float t;
    public float snapSpeed;

    // Start is called before the first frame update
    void Start()
    {
        box_collider = gameObject.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (slot_occupied != null)
        {
            Vector3 tile_pos = slot_occupied.transform.position;
            tile_pos.y = 0.4f;
            if (gameObject.transform.position != tile_pos)
            {
                box_collider.enabled = false;
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, tile_pos, Time.deltaTime * snapSpeed);
            }
            else
            {
                box_collider.enabled = true;
            }
        }
        else if (holder.survivor.bunk != null)
        {
            Vector3 bunk_pos = holder.survivor.bunk.transform.position;
            bunk_pos.y = 0.4f;
            if (gameObject.transform.position != bunk_pos)
            {
                box_collider.enabled = false;
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, bunk_pos, Time.deltaTime * snapSpeed);
            }
            else
            {
                box_collider.enabled = true;
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
                current_tile = col.gameObject.GetComponent<TileSpace>();

                //make sure the tile isnt occupied
                if (!current_tile.isOccupied)
                {
                    //unpair from our previous object
                    if (slot_occupied != null)
                    {
                        Unbind();
                    }
                    //pair the object with the tile
                    Debug.Log(gameObject.name + " snapping to " + col.gameObject.name);
                    slot_occupied = col.gameObject;
                    current_tile.Bind(holder.survivor.controlling_player);

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

                slot_occupied = squad.addSurvivor(holder.survivor);
                if (slot_occupied != null)
                {
                    Debug.Log("slot_occupied was not null");
                }
                else
                {
                    //we didnt return a spot for the survivor, squad is full
                    slot_occupied = temp;
                }
                break;

            case "Bunk":
                holder.survivor.bunk = col.gameObject.GetComponent<Bunk>();
                if (holder.survivor.bunk == null)
                {
                  //do nothing
                }
                else
                {
                  holder.survivor.bunk.Bind(holder.survivor);
                  Debug.Log("Bound to Bunk!");
                }
                break;
        }


    }

    private void OnTriggerExit(Collider col)
    {
        Debug.Log("Trigger Exit");
        switch (col.gameObject.tag)
        {
            //IF TILESPACE
            case "TileSpace":
                Unbind();
                break;

            case "Squad":
                Unbind();
                break;

            case "Bunk":
            default:
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
                old_tile.Unbind(holder.survivor.controlling_player);
                slot_occupied = null;
                //set slot_occupied to player bunk
                break;
            case "Squad":
                Debug.Log("Unbinding from squad");
                squad.removeSurvivor(holder.survivor, slot_occupied);
                slot_occupied = null;
                //set slot_occupied back to player's bunk
                break;
            case "Bunk":
                holder.survivor.bunk.Unbind();
              break;
            default:
                Debug.Log(slot_occupied.tag);
                break;
        }
    }
}
     
