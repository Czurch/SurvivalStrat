using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class SurvivorSnapToObject : MonoBehaviour
{
    public SurvivorHolder holder;
    private BoxCollider box_collider;
    public Squad squad;
    public float snapSpeed;

    private DragObject drag;
    private IEnumerator moveRoutine;
    // Start is called before the first frame update
    void Start()
    {
        snapSpeed = 20;
        holder = gameObject.GetComponent<SurvivorHolder>();
        box_collider = gameObject.GetComponent<BoxCollider>();
        drag = gameObject.GetComponent<DragObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!drag.isDragging)
        {
            if (holder.slot_occupied != null)
            {
                Vector3 tile_pos = holder.slot_occupied.transform.position;
                tile_pos.y = 0.4f;
                if (moveRoutine == null)
                {   
                    moveRoutine = MovementRoutine(tile_pos);
                    StartCoroutine(moveRoutine);
                }
            }
            else if (holder.bunk_occupied != null)
            {
                Vector3 bunk_pos = holder.bunk_occupied.gameObject.transform.position;
                bunk_pos.y = 0.4f;

                if (moveRoutine == null)
                {
                    Debug.Log("moveRoutine is null");
                    moveRoutine = MovementRoutine(bunk_pos);
                    StartCoroutine(moveRoutine);
                }
            }
        }
        else 
        {
            if (moveRoutine != null)
            {
                StopCoroutine(moveRoutine);
                moveRoutine = null;
            }
        }
    }

    //When we collide with a snappable Object we set our position to them
    void OnTriggerEnter(Collider col)
    {
        //Debug.Log("Trigger Entered");
        switch (col.gameObject.tag)
        {
            //IF TILESPACE
            case "TileSpace":
                holder.current_tile = col.gameObject.GetComponent<TileSpace>();

                //unpair from our previous object
                if (holder.slot_occupied != null)
                {
                  Unbind();
                }
                //pair the object with the tile
                Debug.Log(gameObject.name + " snapping to " + col.gameObject.name);
                //holder.slot_occupied = col.gameObject;
                holder.current_tile.Bind(holder);
                break;

            case "Squad":
                GameObject temp;        //holds our slot_occupied if we cant join the squad
                squad = col.gameObject.GetComponent<Squad>();
                Debug.Log("we are triggering on " + col.gameObject.name);
                temp = holder.slot_occupied;

                if (holder.slot_occupied != null)
                {
                    break;
                }

                holder.slot_occupied = squad.addSurvivor(holder.survivor);
                if (holder.slot_occupied != null)
                {
                    Debug.Log("slot_occupied was not null");
                }
                else
                {
                    //we didnt return a spot for the survivor, squad is full
                    holder.slot_occupied = temp;
                }
                break;

            case "Bunk":
                if (holder.bunk_occupied != null)
                {
                  //do nothing
                }
                else
                {
                    holder.bunk_occupied = col.gameObject.GetComponent<Bunk>();
                    holder.bunk_occupied.Bind(holder.survivor);
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
        switch (holder.slot_occupied.tag)
        {
            case "TileSpace":
                Debug.Log("Unbinding from previous tile");
                holder.old_tile = holder.current_tile.GetComponent<TileSpace>();
                holder.old_tile.Unbind(holder);
                holder.slot_occupied = holder.bunk_occupied.gameObject;
                break;
            case "Squad":
                Debug.Log("Unbinding from squad");
                squad.removeSurvivor(holder.survivor, holder.slot_occupied);
                holder.slot_occupied = holder.bunk_occupied.gameObject;
                break;
            case "Bunk":
                holder.bunk_occupied.Unbind();
              break;
            default:
                Debug.Log(holder.slot_occupied.tag);
                break;
        }
    }

    public IEnumerator MovementRoutine(Vector3 moveTo)
    {
        while (gameObject.transform.position != moveTo)
        {
            //box_collider.enabled = false;
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, moveTo, Time.deltaTime * snapSpeed);
            yield return null;
        }
        //box_collider.enabled = true;
        yield return new WaitForSeconds(0.1f);
    }
}
     
