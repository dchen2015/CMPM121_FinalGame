using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Field_Controller : MonoBehaviour
{
    public GameObject selectedMarble;
    public GameObject marbles;
    public float totalEnergy;
    public GameObject FireEnergyBar;
    public GameObject WaterEnergyBar;
    public Text FireEnergyDisplay;
    public Text WaterEnergyDisplay;
    public Text turnDisplay;

    [Header("Do not assign any value")]

    public float WaterRemainingEnergy;
    public float FireRemainingEnergy;
    public float WaterEnergyConsPercent;
    public float FireEnergyConsPercent;

    public bool mouseOverMarble;
    public bool actionTaken;
    public int turn;


    // Start is called before the first frame update
    void Start()
    {
        FireRemainingEnergy = totalEnergy;
        WaterRemainingEnergy = totalEnergy;
        selectedMarble = null;
        actionTaken = false;
        turn = 0;

        //Display Initial Energy
        WaterEnergyDisplay.text = "Energy: " + Mathf.RoundToInt(WaterRemainingEnergy / totalEnergy * 100) + " %";
        WaterEnergyDisplay.color = Color.blue;
        FireEnergyDisplay.text = "Energy: " + Mathf.RoundToInt(FireRemainingEnergy / totalEnergy * 100) + " %";
        FireEnergyDisplay.color = Color.red;

        FireEnergyBar.GetComponent<Energy_Bar>().barDisplay = WaterRemainingEnergy / totalEnergy;
        WaterEnergyBar.GetComponent<Energy_Bar>().barDisplay = FireRemainingEnergy / totalEnergy;
    }

    // Update is called once per frame
    void Update()
    {
        //Number of Marbles Count
        int numOfPlayer1 = 0;
        int numOfPlayer2 = 0;
        foreach (Transform child in marbles.transform)
        {
            if (child.gameObject.GetComponent<Marble_Controller>().owner == 1 && child.tag == "Marble")
            {
                numOfPlayer1 += 1;
            }
            else
            {
                numOfPlayer2 += 1;
            }
        }


        //Determin Win
        if (numOfPlayer1 == 0)
        {
            turnDisplay.text = "Water Player Won";
            turnDisplay.color = Color.blue;
        }
        else if (numOfPlayer2 == 0)
        {
            turnDisplay.text = "Fire Player Won";
            turnDisplay.color = Color.red;
        }

        //Display Turn
        else
        {
            turnDisplay.text = turn == 1 ? "Fire Player's Turn" : "Water Player's Turn";
            turnDisplay.color = turn == 1 ? Color.red : Color.blue;
        }

        //Deselect Marble
        if (selectedMarble != null)
        {
            if (!selectedMarble.GetComponent<Marble_Controller>().mouseOver && Input.GetMouseButtonDown(1))
            {
                selectedMarble = null;
            }
        }


        if (actionTaken)
        {
            if (IsStationary())
            {
                turn = turn == 1 ? 0 : 1;

                //Recover ( ( 1 + (Cos(lastTurnUsedEnergy/lastTurnRemainingEnergy) * Pi + 1) / 2) * 25%) * Total 
                if (turn == 1)
                {
                    float temp = FireRemainingEnergy + (FireEnergyConsPercent + 1) * 0.2f * totalEnergy;
                    if (temp > totalEnergy)
                    {
                        FireRemainingEnergy = totalEnergy;
                    }
                    else
                    {
                        FireRemainingEnergy = temp;
                    }
                }
                else
                {
                    float temp = WaterRemainingEnergy + (FireEnergyConsPercent + 1) * 0.2f * totalEnergy;
                    if (temp > totalEnergy)
                    {
                        WaterRemainingEnergy = totalEnergy;
                    }
                    else
                    {
                        WaterRemainingEnergy = temp;
                    }
                }

                foreach (Transform child in marbles.transform)
                {
                    if (child.GetComponent<Marble_Controller>().buffedTurn >= 1)
                    {
                        child.GetComponent<Marble_Controller>().buffedTurn -= 1;
                    }
                    if (child.GetComponent<Marble_Controller>().debuffedTurn >= 1)
                    {
                        child.GetComponent<Marble_Controller>().debuffedTurn -= 1;
                    }
                    child.Find("VFX").transform.Find("Fire_Sphere").gameObject.SetActive(false);
                    child.Find("VFX").transform.Find("Ice_Sphere").gameObject.SetActive(false);
                }

                WaterEnergyDisplay.text = "Energy: " + Mathf.RoundToInt(WaterRemainingEnergy / totalEnergy * 100) + " %";
                FireEnergyDisplay.text = "Energy: " + Mathf.RoundToInt(FireRemainingEnergy / totalEnergy * 100) + " %";

                FireEnergyBar.GetComponent<Energy_Bar>().barDisplay = FireRemainingEnergy / totalEnergy;
                WaterEnergyBar.GetComponent<Energy_Bar>().barDisplay = WaterRemainingEnergy / totalEnergy;

                actionTaken = false;
            }

            foreach (Transform child in marbles.transform)
            {

                if (child.transform.position.y < -20f)
                {
                    Destroy(child.gameObject);
                }
            }
        }
    }

    public bool IsStationary()
    {
        foreach (Transform child in marbles.transform)
        {

            if (child.gameObject.GetComponent<Rigidbody>().velocity != new Vector3(0, 0, 0))
            {
                return false;
            }

        }
        return true;
    }
}
