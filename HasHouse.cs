using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasHouse : MonoBehaviour
{
    public bool hasHouse, townCentre;
    public GameObject centerPrefab;
    public GameObject[] housePrefabs = new GameObject[4];
    private GameObject assignedHouse;
    public int coOrdinatesX, coOrdinatesY;
    public GameObject manager;
    public int type;
    public bool watersTaken;

    private void Awake()
    {
        manager = GameObject.Find("Observer");
        watersTaken = false;
    }
    public void buildHouse()
    {
        townCentre = true;
        hasHouse = true;
        GameObject centerObject = Instantiate(centerPrefab, this.transform.position, Quaternion.identity);
        centerObject.transform.parent = this.transform;
        centerObject.transform.Rotate(new Vector3(0, rotation(), 0));
    }

    public void spawnSimilarHouses(GameObject[,] houses, GameObject origin)
    {
        //Checking all the tiles within a 3x3 area from the origin tile.
        for (int i = -3; i < 3; i++)
        {

            for (int j = -3; j < 3; j++)
            {

                int spawnHouse = Random.Range(0, 5);
                if (spawnHouse > 1)
                {
                    //Debug.Log("Attempting to spawn house");
                    if (coOrdinatesX + i > 0 &&
                       coOrdinatesX + i < manager.GetComponent<HexSpawner>().mapSize)
                    {
                        if (coOrdinatesY + j > 0 &&
                            coOrdinatesY + j < manager.GetComponent<HexSpawner>().mapSize)
                        {
                            //Debug.Log("Reached tile check");
                            if (houses[coOrdinatesX + i, coOrdinatesY + j].GetComponent<HasHouse>().type == 3 &&
                                houses[coOrdinatesX + i, coOrdinatesY + j].GetComponent<HasHouse>().hasHouse == false &&
                                houses[coOrdinatesX + i, coOrdinatesY + j].GetComponent<TreePopulatorNew>().population == 0)
                            {
                                //Debug.Log("Spawned house");
                                houses[coOrdinatesX + i, coOrdinatesY + j].GetComponent<HasHouse>().hasHouse = true;
                                assignedHouse = Instantiate(housePrefabs[Random.Range(1,4) - 1], houses[coOrdinatesX + i, coOrdinatesY + j].GetComponent<HasHouse>().transform.position, Quaternion.identity);
                                assignedHouse.transform.Translate(new Vector3(0, -0.5f, 0));
                                assignedHouse.transform.parent = houses[coOrdinatesX + i, coOrdinatesY + j].transform;
                                assignedHouse.transform.LookAt(origin.transform);
                                Vector3 EulerLocation = assignedHouse.transform.rotation.eulerAngles;
                                assignedHouse.transform.rotation = Quaternion.Euler(0, EulerLocation.y, 0);
                            }
                            else if (houses[coOrdinatesX + i, coOrdinatesY + j].GetComponent<HasHouse>().type == 2 &&
                               houses[coOrdinatesX + i, coOrdinatesY + j].GetComponent<HasHouse>().hasHouse == false)
                            {
                                int housePrefab = 3;
                                int rotMod = 0;
                                GameObject tmp = onCoast(houses, houses[coOrdinatesX + i, coOrdinatesY + j]);
                                if (tmp != null)
                                {
                                    housePrefab = 4;
                                    origin = tmp;
                                    rotMod = 90;
                                }
                                //Debug.Log("Spawned house");
                                houses[coOrdinatesX + i, coOrdinatesY + j].GetComponent<HasHouse>().hasHouse = true;
                                assignedHouse = Instantiate(housePrefabs[housePrefab], houses[coOrdinatesX + i, coOrdinatesY + j].GetComponent<HasHouse>().transform.position, Quaternion.identity);
                                assignedHouse.transform.Translate(new Vector3(0, -0.5f, 0));
                                assignedHouse.transform.LookAt(origin.transform);
                                Vector3 EulerLocation = assignedHouse.transform.rotation.eulerAngles;

                                if (EulerLocation.y < 0)
                                {
                                    rotMod = -90;
                                }

                                Debug.Log(Mathf.Abs((EulerLocation.y - rotMod) % 60));

                                if (Mathf.Abs((EulerLocation.y - rotMod)) % 60 < 30.1f && Mathf.Abs((EulerLocation.y - rotMod)) % 60 > 29.9f)
                                {
                                    Debug.Log("Goodbye to " + assignedHouse.GetInstanceID());
                                    GameObject.Destroy(assignedHouse);
                                    return;
                                }

                                assignedHouse.transform.rotation = Quaternion.Euler(0, EulerLocation.y - rotMod, 0);
                                assignedHouse.transform.parent = houses[coOrdinatesX + i, coOrdinatesY + j].transform;
                            }

                        }
                    }
                }
                
            }
        }
    }

    public void setCoords(int x, int y)
    {
        coOrdinatesX = x;
        coOrdinatesY = y;
    }

    float rotation()
    {
        return Random.Range(0, 360);
    }

    GameObject onCoast(GameObject[,] houses, GameObject origin)
    {
        int posX = origin.GetComponent<HasHouse>().coOrdinatesX;
        int posY = origin.GetComponent<HasHouse>().coOrdinatesY;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                int tmpJ = j - 1;
                int tmp = i - 1;

                    if (posX + tmp > 0 &&
                      posX + tmp < manager.GetComponent<HexSpawner>().mapSize)
                    {
                        if (posY + tmpJ > 0 &&
                            posY + tmpJ < manager.GetComponent<HexSpawner>().mapSize)
                        {

                            if (houses[posX + tmp, posY + tmpJ].GetComponent<HasHouse>().type == 0 && houses[posX + tmp, posY + tmpJ].GetComponent<HasHouse>().watersTaken == false) {
                                houses[posX + tmp, posY + tmpJ].GetComponent<HasHouse>().watersTaken = true;
                                return houses[posX + tmp, posY + tmpJ];
                            }

                        }
                    }

                
            }

        }

        return null;
    }
}
