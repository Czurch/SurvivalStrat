using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class DraftSpawner : MonoBehaviour
{
    public List<GameObject> roster;
    public int free_agents;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnSurvivor()
    {
        //pick a random survivor from the roster
        Debug.Log("Roster Count: " + roster.Count);
        int num = Random.Range(0, roster.Count - 1);
        int x = Random.Range(-3, 3);
        int y = Random.Range(-3, 3);
        Instantiate(roster[num], new Vector3(x, 0, y), Quaternion.identity);
        //when a survivor is spawned, they are removed from the list and never spawned back in during that game session
        roster.Remove(roster[num]);
        free_agents++;
    }

    public void PickCharacter()
    {
        if (free_agents > 0)
        { 
            free_agents--;
        }
        else
        {
            Debug.LogError("WARNING: Draft Spawner free_agents was already zero when a character was picked");
        }
    }
}
