using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Marble_Controller : MonoBehaviour
{
    public GameObject field;
    public int owner;
    public string marbleClass;
    public bool abilityShot;
    public int buffedTurn;
    public int debuffedTurn;
    public Rigidbody fireArrow;
    public Rigidbody iceArrow;

    public bool mouseOver;
    public Text a;

    public float forceMultiplier;

    private float defaultDrag;
    private Vector3 delta;
    private Vector3 lastPos;
    private float force;
    private bool doChange;


    private void Start()
    {
        doChange = false;
        abilityShot = false;
        buffedTurn = 0;
        defaultDrag = this.GetComponent<Rigidbody>().drag;
    }

    // Update is called once per frame
    void Update()
    {
        if (field.GetComponent<Field_Controller>().selectedMarble == this.gameObject)
        {
            this.transform.LookAt(Camera.main.transform);
            //Calculate mouse delta
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                if (mouseOver)
                {
                    doChange = true;
                }
                delta = Vector3.zero;
                lastPos = Input.mousePosition;
            }
            else if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
            {
                if (doChange)
                {
                    delta = Input.mousePosition - lastPos;
                    lastPos = Input.mousePosition;

                    if (Input.GetMouseButton(0))
                    {
                        float temp = ((owner == 1 ? field.GetComponent<Field_Controller>().FireRemainingEnergy : field.GetComponent<Field_Controller>().WaterRemainingEnergy) - force) / field.GetComponent<Field_Controller>().totalEnergy * 100;

                        if (temp > (owner == 1 ? field.GetComponent<Field_Controller>().FireRemainingEnergy : field.GetComponent<Field_Controller>().WaterRemainingEnergy) / field.GetComponent<Field_Controller>().totalEnergy * 100)
                        {
                            temp = (owner == 1 ? field.GetComponent<Field_Controller>().FireRemainingEnergy : field.GetComponent<Field_Controller>().WaterRemainingEnergy) / field.GetComponent<Field_Controller>().totalEnergy * 100;
                        }
                        if (temp < 0)
                        {
                            temp = 0;
                        }

                        float displayFactor = force / field.GetComponent<Field_Controller>().totalEnergy + 1;

                        if (displayFactor <= 1)
                        {
                            displayFactor = 1;
                        }

                        if (displayFactor >= 2)
                        {
                            displayFactor = 2;
                        }

                        if (owner == 1)
                        {
                            field.GetComponent<Field_Controller>().FireEnergyDisplay.text = "energy: " + Mathf.RoundToInt(temp) + " %";
                            field.GetComponent<Field_Controller>().FireEnergyBar.GetComponent<Energy_Bar>().barDisplay = temp / 100f;

                            this.transform.Find("VFX").transform.Find("Fire_Light").transform.GetComponent<Light>().intensity = displayFactor;
                            this.transform.Find("VFX").transform.Find("Fire_Dome").transform.Find("Dome_01").transform.localScale = new Vector3(displayFactor, displayFactor, displayFactor);
                        }
                        else
                        {
                            field.GetComponent<Field_Controller>().WaterEnergyDisplay.text = "energy: " + Mathf.RoundToInt(temp) + " %";
                            field.GetComponent<Field_Controller>().WaterEnergyBar.GetComponent<Energy_Bar>().barDisplay = temp / 100f;

                            this.transform.Find("VFX").transform.Find("Ice_Light").transform.GetComponent<Light>().intensity = displayFactor;
                            this.transform.Find("VFX").transform.Find("Ice_Dome").transform.Find("Dome_01").transform.localScale = new Vector3(displayFactor, displayFactor, displayFactor);
                        }
                    }
                    else if (Input.GetMouseButton(1))
                    {
                        float temp = ((owner == 1 ? field.GetComponent<Field_Controller>().FireRemainingEnergy : field.GetComponent<Field_Controller>().WaterRemainingEnergy) - force * 1.5f) / field.GetComponent<Field_Controller>().totalEnergy * 100;

                        if (temp > (owner == 1 ? field.GetComponent<Field_Controller>().FireRemainingEnergy : field.GetComponent<Field_Controller>().WaterRemainingEnergy) / field.GetComponent<Field_Controller>().totalEnergy * 100)
                        {
                            temp = (owner == 1 ? field.GetComponent<Field_Controller>().FireRemainingEnergy : field.GetComponent<Field_Controller>().WaterRemainingEnergy) / field.GetComponent<Field_Controller>().totalEnergy * 100;
                        }
                        if (temp < 0)
                        {
                            temp = 0;
                        }

                        float displayFactor = force * 1.5f / field.GetComponent<Field_Controller>().totalEnergy + 1;

                        if (displayFactor <= 1)
                        {
                            displayFactor = 1;
                        }

                        if (displayFactor >= 2)
                        {
                            displayFactor = 2;
                        }

                        if (owner == 1)
                        {
                            field.GetComponent<Field_Controller>().FireEnergyDisplay.text = "energy: " + Mathf.RoundToInt(temp) + " %";
                            field.GetComponent<Field_Controller>().FireEnergyBar.GetComponent<Energy_Bar>().barDisplay = temp / 100f;

                            this.transform.Find("VFX").transform.Find("Fire_Light").transform.GetComponent<Light>().intensity = displayFactor;
                            this.transform.Find("VFX").transform.Find("Fire_Sphere").gameObject.SetActive(true);
                            this.transform.Find("VFX").transform.Find("Ice_Dome").gameObject.SetActive(true);

                            this.transform.Find("VFX").transform.Find("Fire_Sphere").transform.localScale = new Vector3(displayFactor, displayFactor, displayFactor);
                            this.transform.Find("VFX").transform.Find("Fire_Dome").transform.Find("Dome_01").transform.localScale = new Vector3(displayFactor, displayFactor, displayFactor);
                            this.transform.Find("VFX").transform.Find("Ice_Dome").transform.Find("Dome_01").transform.localScale = new Vector3(displayFactor, displayFactor, displayFactor);
                        }
                        else
                        {
                            field.GetComponent<Field_Controller>().WaterEnergyDisplay.text = "energy: " + Mathf.RoundToInt(temp) + " %";
                            field.GetComponent<Field_Controller>().WaterEnergyBar.GetComponent<Energy_Bar>().barDisplay = temp / 100f;

                            this.transform.Find("VFX").transform.Find("Ice_Light").transform.GetComponent<Light>().intensity = displayFactor;
                            this.transform.Find("VFX").transform.Find("Ice_Sphere").gameObject.SetActive(true);
                            this.transform.Find("VFX").transform.Find("Fire_Dome").gameObject.SetActive(true);

                            this.transform.Find("VFX").transform.Find("Ice_Sphere").transform.localScale = new Vector3(displayFactor, displayFactor, displayFactor);
                            this.transform.Find("VFX").transform.Find("Fire_Dome").transform.Find("Dome_01").transform.localScale = new Vector3(displayFactor, displayFactor, displayFactor);
                            this.transform.Find("VFX").transform.Find("Ice_Dome").transform.Find("Dome_01").transform.localScale = new Vector3(displayFactor, displayFactor, displayFactor);
                        }
                    }
                }
            }
            else if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
            {
                if (doChange)
                {
                    if (Input.GetMouseButtonUp(0))
                    {
                        delta = Vector3.zero;
                        lastPos = Input.mousePosition;
                        doChange = false;

                        if (force > (owner == 1 ? field.GetComponent<Field_Controller>().FireRemainingEnergy : field.GetComponent<Field_Controller>().WaterRemainingEnergy))
                        {
                            force = owner == 1 ? field.GetComponent<Field_Controller>().FireRemainingEnergy : field.GetComponent<Field_Controller>().WaterRemainingEnergy;
                        }
                        else if (force < 0)
                        {
                            force = 0;
                        }

                        force *= forceMultiplier;
                        Shoot();
                        force = 0;
                    }
                    else if (Input.GetMouseButtonUp(1))
                    {
                        delta = Vector3.zero;
                        lastPos = Input.mousePosition;
                        doChange = false;

                        if (force * 2 > (owner == 1 ? field.GetComponent<Field_Controller>().FireRemainingEnergy : field.GetComponent<Field_Controller>().WaterRemainingEnergy))
                        {
                            force = (owner == 1 ? field.GetComponent<Field_Controller>().FireRemainingEnergy : field.GetComponent<Field_Controller>().WaterRemainingEnergy) / 2;
                        }
                        else if (force < 0)
                        {
                            force = 0;
                        }

                        force *= forceMultiplier;
                        AbilityShoot();

                        if (owner == 1)
                        {
                            this.transform.Find("VFX").transform.Find("Ice_Dome").gameObject.SetActive(false);
                        }
                        else
                        {
                            this.transform.Find("VFX").transform.Find("Fire_Dome").gameObject.SetActive(false);
                        }
                        if (marbleClass != "Warrior")
                        {
                            this.transform.Find("VFX").transform.Find("Fire_Sphere").gameObject.SetActive(false);
                            this.transform.Find("VFX").transform.Find("Ice_Sphere").gameObject.SetActive(false);
                        }
                    }
                }
            }

            force -= delta.y;
        }
        
        //Selection VFX
        if (mouseOver && field.GetComponent<Field_Controller>().turn == owner)
        {
            //Select Marble
            if (Input.GetMouseButtonDown(0) && field.GetComponent<Field_Controller>().selectedMarble == null && !field.GetComponent<Field_Controller>().actionTaken)
            {
                field.GetComponent<Field_Controller>().selectedMarble = this.gameObject;
            }

            if (field.GetComponent<Field_Controller>().selectedMarble == null)
            {
                ActiveSelection();
            }
        }
        else if (field.GetComponent<Field_Controller>().selectedMarble == this.gameObject)
        {
            ActiveSelection();
        }
        else
        {
            DeactivateSelection();
        }

        //ability
        if (abilityShot)
        {
            if (this.gameObject.GetComponent<Rigidbody>().velocity == new Vector3(0, 0, 0))
            {
                switch (this.marbleClass)
                {
                    case "Engineer":
                        Explode();
                        break;
                    case "Priest":
                        Buff();
                        break;
                    case "Mage":
                        Meteor();
                        break;
                    case "Warrior":
                        Rage();
                        break;
                    case "Archer":
                        Arrow();
                        break;
                }

                force = 0;
                abilityShot = false;
            }
        }

        //Display Buff
        if (buffedTurn > 0)
        {
            if (owner == 1)
            {
                this.transform.Find("VFX").transform.Find("Fire_Shield").gameObject.SetActive(true);
            }
            else
            {
                print("aha");
                this.transform.Find("VFX").transform.Find("Ice_Shield").gameObject.SetActive(true);
            }
            this.GetComponent<Rigidbody>().angularDrag = defaultDrag * 3.5f;
            this.GetComponent<Rigidbody>().drag = defaultDrag * 3.5f;
        }
        else
        {
            this.transform.Find("VFX").transform.Find("Fire_Shield").gameObject.SetActive(false);
            this.transform.Find("VFX").transform.Find("Ice_Shield").gameObject.SetActive(false);
            this.GetComponent<Rigidbody>().angularDrag = defaultDrag;
            this.GetComponent<Rigidbody>().drag = defaultDrag;
        }

        if (debuffedTurn > 0)
        {
            if (owner == 1)
            {
                this.transform.Find("VFX").transform.Find("Ice_Whirl").gameObject.SetActive(true);
            }
            else
            {
                this.transform.Find("VFX").transform.Find("Fire_Whirl").gameObject.SetActive(true);
            }
            this.GetComponent<Rigidbody>().angularDrag = defaultDrag * 2f;
            this.GetComponent<Rigidbody>().drag = defaultDrag * 2f;
        }
        else {
            this.transform.Find("VFX").transform.Find("Fire_Whirl").gameObject.SetActive(false);
            this.transform.Find("VFX").transform.Find("Ice_Whirl").gameObject.SetActive(false);
            this.GetComponent<Rigidbody>().angularDrag = defaultDrag;
            this.GetComponent<Rigidbody>().drag = defaultDrag;
        }


        this.transform.Find("VFX").eulerAngles = new Vector3(0, 0, 0);
        Vector3 lookAtPosition = new Vector3(Camera.main.transform.position.x, this.transform.position.y, Camera.main.transform.position.z);
        this.transform.Find("VFX").transform.Find("Ice_Shield").transform.LookAt(lookAtPosition);
        this.transform.Find("VFX").transform.Find("Fire_Shield").transform.LookAt(lookAtPosition);
    }

    void OnGUI()
    {
        if (mouseOver && field.GetComponent<Field_Controller>().turn == owner && field.GetComponent<Field_Controller>().selectedMarble == null)
        {
            int tempWidth = 120;
            int tempHeight = 80;

            string player = owner == 1 ? "fire" : "water";

            string description = "\nSkill: ";
            switch (this.marbleClass)
            {
                case "Engineer":
                    description += "Explosion \n Area Effect";
                    break;
                case "Priest":
                    description += "Shield \n Area Friendly\nDefense Buff";
                    break;
                case "Mage":
                    description += "Meteor \n Area\nSlow Debuff";
                    break;
                case "Warrior":
                    description += "Rage \n 37.5%\nMore Strenght";
                    break;
                case "Archer":
                    description += "Snipe \n Attack\nFurtherest Enemy";
                    break;
            }
            GUI.Box(new Rect(Input.mousePosition.x - tempWidth/2, Screen.height - Input.mousePosition.y - tempHeight - 10, tempWidth, tempHeight), marbleClass + "\n owner: " + player + description);
        }
    }

    void Explode()
    {
        Vector3 explosionPosition = this.transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, 5);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null && rb != this.GetComponent<Rigidbody>())
            {
                rb.AddExplosionForce(force / 65, explosionPosition, 5, 0, ForceMode.Impulse);
            }
        }
        if (owner == 1)
        {
            this.transform.Find("VFX").transform.Find("Fire_Explosion").transform.GetComponent<ParticleSystem>().Play();
        }
        else
        {
            this.transform.Find("VFX").transform.Find("Ice_Explosion").transform.GetComponent<ParticleSystem>().Play();
        }
    }

    void Buff()
    {
        Invoke("AddBuff", 0.5f);

        if (owner == 1)
        {
            this.transform.Find("VFX").transform.Find("Fire_Circle").transform.Find("Glow_01").transform.GetComponent<ParticleSystem>().Play();
        }
        else
        {
            this.transform.Find("VFX").transform.Find("Ice_Circle").transform.Find("Glow_01").transform.GetComponent<ParticleSystem>().Play();
        }
    }

    void Meteor()
    {
        Vector3 debuffPosition = this.transform.position;
        Collider[] colliders = Physics.OverlapSphere(debuffPosition, 7);
        foreach (Collider hit in colliders)
        {
            if (hit.CompareTag("Marble"))
            {
                if (hit.GetComponent<Marble_Controller>().owner != owner && hit.gameObject != gameObject)
                {
                    hit.GetComponent<Marble_Controller>().debuffedTurn += 2;
                }
            }
        }

        if (owner == 1)
        {
            foreach (Transform partic in this.transform.Find("VFX").transform.Find("Fire_Meteor_Shower").transform)
            {
                partic.GetComponent<ParticleSystem>().Play();
            }
        }
        else
        {
            foreach (Transform partic in this.transform.Find("VFX").transform.Find("Ice_Meteor_Shower").transform)
            {
                partic.GetComponent<ParticleSystem>().Play();
            }
        }
    }

    void Rage()
    {
        if (owner == 1)
        {
            this.transform.Find("VFX").transform.Find("Fire_Sphere").gameObject.SetActive(true);
        }
        else
        {
            this.transform.Find("VFX").transform.Find("Ice_Sphere").gameObject.SetActive(true);
        }
    }
    void Arrow()
    {
        Vector3 rangePosition = this.transform.position;
        Collider[] colliders = Physics.OverlapSphere(rangePosition, 20);
        GameObject furtherstMarble = null;
        foreach (Collider hit in colliders)
        {
            if (hit.CompareTag("Marble"))
            {
                if (hit.gameObject != this.gameObject && hit.GetComponent<Marble_Controller>().owner != owner)
                {
                    if (furtherstMarble == null)
                    {
                        furtherstMarble = hit.gameObject;
                    }
                    else if (Vector3.Distance(furtherstMarble.transform.position, this.transform.position) < Vector3.Distance(hit.transform.position, this.transform.position))
                    {
                        furtherstMarble = hit.gameObject;
                    }
                }
            }
        }

        this.transform.Find("Pivot").transform.LookAt(furtherstMarble.transform);

        Vector3 temp = this.transform.Find("Pivot").transform.eulerAngles;

        temp.y += 180;

        this.transform.Find("Pivot").transform.eulerAngles = temp;

        Rigidbody arrowInstance;

        if (owner == 1)
        {
            arrowInstance = Instantiate(fireArrow, this.transform.Find("Pivot").transform.position, this.transform.Find("Pivot").transform.rotation);
            arrowInstance.AddForce(arrowInstance.transform.forward * -force / 4);
        }
        else
        {
            arrowInstance = Instantiate(iceArrow, this.transform.Find("Pivot").transform.position, this.transform.Find("Pivot").transform.rotation);
            arrowInstance.AddForce(arrowInstance.transform.forward * -force / 4);
        }

        Physics.IgnoreCollision(arrowInstance.gameObject.GetComponent<BoxCollider>(), this.GetComponent<SphereCollider>(), true);
    }
    private void AddBuff()
    {
        Vector3 buffPosition = this.transform.position;
        Collider[] colliders = Physics.OverlapSphere(buffPosition, 4f);
        foreach (Collider hit in colliders)
        {
            if (hit.CompareTag("Marble"))
            {
                if (hit.GetComponent<Marble_Controller>().owner == owner && hit.gameObject != gameObject)
                {
                    hit.GetComponent<Marble_Controller>().buffedTurn += 2;
                }
            }
        }
    }
    private void ActiveSelection()
    {
        if (owner == 1)
        {
            this.transform.Find("VFX").transform.Find("Fire_Light").gameObject.SetActive(true);
            this.transform.Find("VFX").transform.Find("Fire_Dome").gameObject.SetActive(true);
        }
        else
        {
            this.transform.Find("VFX").transform.Find("Ice_Light").gameObject.SetActive(true);
            this.transform.Find("VFX").transform.Find("Ice_Dome").gameObject.SetActive(true);
        }
    }

    private void DeactivateSelection()
    {
        this.transform.Find("VFX").transform.Find("Fire_Light").gameObject.SetActive(false);
        this.transform.Find("VFX").transform.Find("Fire_Dome").gameObject.SetActive(false);
        this.transform.Find("VFX").transform.Find("Ice_Light").gameObject.SetActive(false);
        this.transform.Find("VFX").transform.Find("Ice_Dome").gameObject.SetActive(false);
        if (marbleClass != "Warrior")
        {
            this.transform.Find("VFX").transform.Find("Fire_Sphere").gameObject.SetActive(false);
            this.transform.Find("VFX").transform.Find("Ice_Sphere").gameObject.SetActive(false);
        }
    }

    private void Shoot()
    {
        if (force > 0)
        {
            print("actionTaken at: " + force);
            this.GetComponent<Rigidbody>().AddForce(-force * 0.9f * Mathf.Sin(transform.localEulerAngles.y / 180 * Mathf.PI), 0f, -force * 0.9f * Mathf.Cos(transform.localEulerAngles.y / 180 * Mathf.PI));
            field.GetComponent<Field_Controller>().selectedMarble = null;
            field.GetComponent<Field_Controller>().actionTaken = true;
            if (owner == 1)
            {
                float forceRemaining = field.GetComponent<Field_Controller>().FireRemainingEnergy;
                float forceUsed = force / forceMultiplier;
                field.GetComponent<Field_Controller>().FireRemainingEnergy -= forceUsed;
                field.GetComponent<Field_Controller>().FireEnergyConsPercent = forceUsed / forceRemaining;
            }
            else
            {
                float forceRemaining = field.GetComponent<Field_Controller>().WaterRemainingEnergy;;
                float forceUsed = force / forceMultiplier;
                field.GetComponent<Field_Controller>().WaterRemainingEnergy -= force / forceMultiplier;
                field.GetComponent<Field_Controller>().WaterEnergyConsPercent = forceUsed / forceRemaining;
            }

            a.text = null;
        }
    }

    private void AbilityShoot()
    {
        if (force > 0)
        {
            print("actionTaken at: " + force);
            if (marbleClass == "Warrior")
            {
                this.GetComponent<Rigidbody>().AddForce(-force * 2.75f * Mathf.Sin(transform.localEulerAngles.y / 180 * Mathf.PI), 0f, -force * 2.75f * Mathf.Cos(transform.localEulerAngles.y / 180 * Mathf.PI));
            }
            else
            {
                this.GetComponent<Rigidbody>().AddForce(-force * Mathf.Sin(transform.localEulerAngles.y / 180 * Mathf.PI), 0f, -force * Mathf.Cos(transform.localEulerAngles.y / 180 * Mathf.PI));
            }

            field.GetComponent<Field_Controller>().selectedMarble = null;
            field.GetComponent<Field_Controller>().actionTaken = true;

            float forceUsed = force / forceMultiplier * 1.5f;
            if (owner == 1)
            {
                float forceRemaining = field.GetComponent<Field_Controller>().FireRemainingEnergy;
                field.GetComponent<Field_Controller>().FireRemainingEnergy -= forceUsed;
                field.GetComponent<Field_Controller>().FireEnergyConsPercent = forceUsed / forceRemaining;
            }
            else
            {
                float forceRemaining = field.GetComponent<Field_Controller>().WaterRemainingEnergy; ;
                field.GetComponent<Field_Controller>().WaterRemainingEnergy -= force / forceMultiplier;
                field.GetComponent<Field_Controller>().WaterEnergyConsPercent = forceUsed / forceRemaining;
            }

            abilityShot = true;
            a.text = null;
        }
    }

    private void OnMouseOver()
    {
        mouseOver = true;
    }

    private void OnMouseExit()
    {
        mouseOver = false;
    }
}
