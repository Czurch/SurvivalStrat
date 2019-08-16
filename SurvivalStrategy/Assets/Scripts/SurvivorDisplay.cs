using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SurvivorDisplay : MonoBehaviour
{
    public Survivor survivor;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI combatText;

    // Start is called before the first frame update
    void Start()
    {
        nameText.text = survivor.name;
        healthText.text = survivor.health.ToString();
        combatText.text = survivor.combat_score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = survivor.health.ToString();
        combatText.text = survivor.combat_score.ToString();
    }
}
