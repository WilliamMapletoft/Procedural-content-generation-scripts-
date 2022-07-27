using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    private float xPos, zPos, yPos;
    private Vector3 newPos = new Vector3();
    // Update is called once per frame

    private void Start()
    {
        yPos = 100;
    }
    void FixedUpdate()
    {
        xPos = this.transform.position.x;
        //this.GetComponent<Camera>().transform.forward;
        zPos = this.transform.position.z;

        


        if (Input.GetKey("w"))
        {
            zPos += this.GetComponent<Camera>().transform.forward.z;
            xPos += this.GetComponent<Camera>().transform.forward.x;
            //zPos += 0.5f;
            //newPos += this.GetComponent<Camera>().transform.forward;
        }

        if (Input.GetKey("s"))
        {
            zPos -= this.GetComponent<Camera>().transform.forward.z;
            xPos -= this.GetComponent<Camera>().transform.forward.x;
        }

        if (Input.GetKey("a"))
        {
            Vector3 position;
            RaycastHit hit;
            Ray ray = this.GetComponent<Camera>().ScreenPointToRay(Camera.main.transform.forward);

            if (Physics.Raycast(ray, out hit))
            {
                position = hit.transform.position;
            }
            else
            {
                position = new Vector3(0, 0, 0);
            }

            this.transform.RotateAround(position, new Vector3(0, 1, 0), -1);
        }

        if (Input.GetKey("r"))
        {
            this.transform.Rotate(new Vector3(1, 0, 0), -1);
        }

        if (Input.GetKey("f"))
        {
            this.transform.Rotate(new Vector3(-1, 0, 0), -1);
        }


        if (Input.GetKey("d"))
        {
            Vector3 position;
            RaycastHit hit;
            Ray ray = this.GetComponent<Camera>().ScreenPointToRay(Camera.main.transform.forward);

            if (Physics.Raycast(ray, out hit))
            {
                position = hit.transform.position;
            }
            else
            {
                position = new Vector3(0, 0, 0);
            }

            this.transform.RotateAround(position, new Vector3(0, 1, 0), 1);
        }

        if (Input.GetKey("q"))
        {
            yPos--;
            //zPos += 0.5f;
            //newPos += this.GetComponent<Camera>().transform.forward;
        }

        if (Input.GetKey("e"))
        {
            yPos++;
            //zPos += 0.5f;
            //newPos += this.GetComponent<Camera>().transform.forward;
        }

        if (Input.GetKey("up"))
        {
            if (this.GetComponent<Camera>().fieldOfView > 10)
            {
                this.GetComponent<Camera>().fieldOfView -= 1;
            }
        }

        if (Input.GetKey("down"))
        {
            if (this.GetComponent<Camera>().fieldOfView < 70)
            {
                this.GetComponent<Camera>().fieldOfView += 1;
            }
        }

        newPos = new Vector3(xPos, yPos, zPos);
        this.transform.position = newPos;
    }
}
