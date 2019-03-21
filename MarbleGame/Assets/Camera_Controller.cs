using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    public GameObject field;

    public float fieldDistance;
    public float marbleDistance;
    public Text a;

    private float scrollDistanceFactor = 0;
    private Vector3 delta;
    private Vector3 lastPos;
    private float rotateX;
    private float rotateY;
    private bool doChangeAngle;

    

    // Start is called before the first frame update
    void Start()
    {
        rotateY = 230f;
    }

    // Update is called once per frame
    void Update()
    {
        //Get scroll distance factor
        scrollDistanceFactor += Input.GetAxis("Mouse ScrollWheel");
        scrollDistanceFactor = Mathf.Clamp(scrollDistanceFactor, -5f, 5f);

        print(scrollDistanceFactor);
        //Calculate mouse delta
        if (Input.GetMouseButtonDown(0))
        {
            if (field.GetComponent<Field_Controller>().selectedMarble != null)
            {
                if (field.GetComponent<Field_Controller>().selectedMarble.GetComponent<Marble_Controller>().mouseOver)
                {
                    doChangeAngle = false;
                }
            }
            delta = Vector3.zero;
            lastPos = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            if (doChangeAngle)
            {
                delta = Input.mousePosition - lastPos;
                lastPos = Input.mousePosition;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            delta = Vector3.zero;
            lastPos = Input.mousePosition;
            doChangeAngle = true;
        }

        rotateX += delta.x;
        if (rotateY + delta.y >= 10 && rotateY + delta.y <= 250)
        {
            rotateY += delta.y;
        }

        //Calculate Camera Angle

        Vector3 offset;

        if (field.GetComponent<Field_Controller>().selectedMarble == null)
        {
            float temp = fieldDistance - scrollDistanceFactor;
            offset = new Vector3(temp * Mathf.Sin(rotateX / 180) * Mathf.Sin(rotateY / 180), temp * Mathf.Cos(rotateY / 180), temp * Mathf.Cos(rotateX / 180) * Mathf.Sin(rotateY / 180));
            this.transform.position = field.transform.position + offset;
            this.transform.LookAt(field.transform);
        }
        else
        {
            float temp = marbleDistance - scrollDistanceFactor;
            offset = new Vector3(temp * Mathf.Sin(rotateX / 180) * Mathf.Sin(rotateY / 180), temp * Mathf.Cos(rotateY / 180), temp * Mathf.Cos(rotateX / 180) * Mathf.Sin(rotateY / 180));
            this.transform.position = field.GetComponent<Field_Controller>().selectedMarble.transform.position + offset;
            this.transform.LookAt(field.GetComponent<Field_Controller>().selectedMarble.transform);
        }
    }
}
