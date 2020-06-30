using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SurvivorHolder : MonoBehaviour
{
    
    public Survivor survivor;

    public TextMeshProUGUI name_text;
    public TextMeshProUGUI description_text;
    public TextMeshProUGUI health_text;
    public TextMeshProUGUI combat_text;
    public Image hunger_indicator;

    public Bunk bunk_occupied;
    public TileSpace current_tile;
    public TileSpace old_tile;

    // Start is called before the first frame update
    void Start()
    {
        survivor = gameObject.GetComponent<Survivor>();
        bunk_occupied = null;
        name_text.text = survivor.name;
        health_text.text = survivor.health.ToString();
        combat_text.text = survivor.combat_score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        //this is inefficient dont call this every frame
        health_text.text = survivor.health.ToString();
        combat_text.text = survivor.combat_score.ToString();
    }

    public void UpdateUI()
    {
    }
}
