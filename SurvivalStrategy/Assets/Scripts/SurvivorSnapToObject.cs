using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivorSnapToObject : MonoBehaviour
{
    private GameObject snappedPiece;
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
        if (snappedPiece != null)
        {
            if (snapped = true && Input.GetMouseButton(0) != true)
            {
                Vector3 tile_pos = snappedPiece.transform.position;
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
                    if (snappedPiece != null)
                    {
                        Unbind();
                    }
                    //pair the object with the tile
                    Debug.Log(gameObject.name + " snapping to " + col.gameObject.name);
                    snappedPiece = col.gameObject;
                    snapped = true;
                    ts.Bind(sd.controlling_player);

                }
                else { Debug.Log("tile space for " + col.gameObject.name + " is occupied"); }
                break;

            case "Squad":
                GameObject temp;        //holds our snappedPiece if we cant join the squad
                squad = col.gameObject.GetComponent<Squad>();
                Debug.Log("we are triggering on " + col.gameObject.name);
                temp = snappedPiece;

                if (snappedPiece != null)
                {
                    Unbind();
                }

                snappedPiece = squad.addSurvivor(sd.survivor);
                if (snappedPiece != null)
                {
                    Debug.Log("snappedPiece was not null");
                    snapped = true;
                }
                else
                {
                    //we didnt return a spot for the survivor, squad is full
                    snappedPiece = temp;
                    snapped = true;
                }
                break;

            case "Bunk":
                break;
        }


    }

    void Unbind()
    {
        switch (snappedPiece.tag)
        {
            case "TileSpace":
                Debug.Log("Unbinding from previous tile");
                old_tile = snappedPiece.GetComponent<TileSpace>();
                old_tile.Unbind(sd.controlling_player);
                snappedPiece = null;
                snapped = false;
                //set snappedPiece to player bunk
                break;
            case "Squad":
                Debug.Log("Unbinding from squad");
                squad.removeSurvivor(sd.survivor);
                snappedPiece = null;
                snapped = false;
                //set snappedPiece back to player's bunk
                break;
            default:
                Debug.Log(snappedPiece.tag);
                break;
        }
    }
}
     
