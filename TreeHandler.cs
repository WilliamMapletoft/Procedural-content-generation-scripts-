using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TreeHandler : MonoBehaviour
{

    public List<GameObject> treeTiles = new List<GameObject>();
    public int counter = 1;
    // Update is called once per frame

    private void Start()
    {
        Application.targetFrameRate = 45;
    }

    void FixedUpdate()
    {
        if (treeTiles.Count > 0)
        {
                //Debug.Log("I swear its running");
                GameObject current = treeTiles[counter - 1];
                int occurance =  Random.Range(0, 4);

                switch (occurance)
                {
                    case 1: // Grow within tile
                        if (current.GetComponent<TreePopulatorNew>().population < 5)
                        {
                            current.GetComponent<TreePopulatorNew>().treeSpawner();
                        }
                        else if (current.GetComponent<TreePopulatorNew>().population == 5)
                        {
                            current.GetComponent<TreePopulatorNew>().spread();
                        }

                        break;
                    case 2: // Die within tile
                        if (current.GetComponent<TreePopulatorNew>().population == 1)
                        {
                            current.GetComponent<TreePopulatorNew>().removeLastTree();
                            treeTiles.Remove(current);
                        }
                        else if(current.GetComponent<TreePopulatorNew>().population == 0)
                        {
                            //Debug.Log("hopefully we never get this.");
                        }
                        else
                        {
                            current.GetComponent<TreePopulatorNew>().removeTree();
                        }
                        break;
                    case 3:
                        current.GetComponent<TreePopulatorNew>().spread();
                        break;
                }

                if (counter < treeTiles.Count) { counter++; }
                else { counter = 1; }
            }
        
    }

    public void newTree(GameObject newObj)
    {
        //Debug.Log("Tree added to list");
        treeTiles.Add(newObj);
    }

    public void removeTreeTile(GameObject newObj)
    {
        //Debug.Log("Tree removed from list");
        treeTiles.Remove(newObj);
    }
}
