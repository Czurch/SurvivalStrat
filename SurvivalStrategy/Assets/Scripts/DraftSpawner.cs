using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraftSpawner : MonoBehaviour
{
    public List<Survivor> roster;
    public GameObject survivor_prefab;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnSurvivors()
    {
        //pick a random survivor from the roster
        int num = Random.Range(0, roster.Count - 1);
        SurvivorHolder holder = survivor_prefab.GetComponent<SurvivorHolder>();
        holder.survivor = roster[num];
        int x = Random.Range(-3, 3);
        int y = Random.Range(-3, 3);
        Instantiate(survivor_prefab, new Vector3(x, 0, y), Quaternion.identity);
        //when a survivor is spawned, they are removed from the list and never spawned back in during that game session
        roster.Remove(roster[num]);
    }
}
