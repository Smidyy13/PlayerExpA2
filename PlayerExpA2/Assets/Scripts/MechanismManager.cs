using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class MechanismManager : MonoBehaviour
{
    public float shipPowerTotal;
    public float oxygenQualityTotal;
    public float sheildsTotal;
    public float shipHealthTotal;

    public float totalPowerDraw;

    [HideInInspector] public float shipPower;
    [HideInInspector] public float oxygenQuality;
    [HideInInspector] public float sheilds;
    [HideInInspector] public float shipHealth;

    [SerializeField] TMP_Text shipPowerText; 
    [SerializeField] TMP_Text oxygenQualityText; 
    [SerializeField] TMP_Text sheildsText; 
    [SerializeField] TMP_Text shipHealthText;

    [SerializeField] List<GameObject> terminals = new List<GameObject>();

    float powerDrawTimer;
    float asteroidHitTimer;

    void Start()
    {
        shipPower = shipPowerTotal;
        oxygenQuality = oxygenQualityTotal;
        sheilds = sheildsTotal;
        shipHealth = shipHealthTotal;
    }

    void Update()
    {
        shipPowerText.text = "Ship Power: " + shipPower + "out of " + shipPowerTotal;
        oxygenQualityText.text = "O2 Quality: " + oxygenQuality + "out of " + oxygenQualityTotal;
        sheildsText.text = "Shields: " + sheilds + "out of " + sheildsTotal;
        shipHealthText.text = "Health: " + shipHealth + "out of " + shipHealthTotal;

        PowerDraw();
        AsteroidHit();
    }

    void PowerDraw()
    {
        totalPowerDraw = 0;

        foreach (GameObject terminal in terminals)
        {
            totalPowerDraw += terminal.GetComponent<TerminalInputControl>().totalPowerDraw;
        }

        if (powerDrawTimer >= 1) 
        {
            shipPower -= totalPowerDraw;
            powerDrawTimer = 0;
        }
        else
        {
            powerDrawTimer += Time.deltaTime;
        }
    }

    void AsteroidHit()
    {
        if (asteroidHitTimer >= 2)
        {
            sheilds -= 10;
            asteroidHitTimer = 0;
        }
        else
        {
            asteroidHitTimer += Time.deltaTime;
        }
    }
}
