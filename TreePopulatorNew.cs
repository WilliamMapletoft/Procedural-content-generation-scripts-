using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreePopulatorNew : MonoBehaviour
{
    public GameObject[] treeObjects = new GameObject[5];
    public int population = 0;

    private int popMax = 5;
    public List<GameObject> currentTrees = new List<GameObject>();
    public float timeToRandom;
    public GameObject manager;
    public int treeType;


    public void treeSpawner()
    {
        spawnTree(returnTreeLoc(currentTrees.Count), randomScale(), randomRotation());
    }
    void spawnTree(Vector3 location, float scale, int rotation)
    {
        population++;
        currentTrees.Add(Instantiate(treeObjects[treeType], this.transform.position + location, Quaternion.identity));
        currentTrees[currentTrees.Count - 1].transform.parent = this.transform;
        currentTrees[currentTrees.Count - 1].transform.Rotate(new Vector3(0, rotation, 0));
        currentTrees[currentTrees.Count - 1].transform.localScale = new Vector3(scale, scale, scale);
    }
    public void spawnFirstTree()
    {
        //Debug.Log("Tree spawned");
        manager.GetComponent<TreeHandler>().newTree(this.gameObject);
        spawnTree(returnTreeLoc(currentTrees.Count), randomScale(), randomRotation());
    }

    public void removeTree()
    {
        //Debug.Log("Tree removed at " + this.GetInstanceID());
        //Debug.Log("Index for currentTrees : " + (currentTrees.Count - 1));
        population--;
        GameObject.Destroy(currentTrees[currentTrees.Count - 1]);
        currentTrees.RemoveAt(currentTrees.Count - 1);
    }

    public void removeLastTree()
    {
        //Debug.Log("Last tree on tile removed");
        manager.GetComponent<TreeHandler>().removeTreeTile(this.gameObject);
        removeTree();
    }

    float randomScale()
    {
        return Random.Range(0.8f, 1.0f);
    }

    int randomRotation()
    {
        return Random.Range(1, 360);
    }
    Vector3 returnTreeLoc(int treeID)
    {
        switch (treeID)
        {
            case 0:
                return new Vector3(0.436f, -0.5f, 0.304f);
            case 1:
                return new Vector3(0.468f, -0.5f, -0.396f);
            case 2:
                return new Vector3(-0.187f, -0.5f, 0.491f);
            case 3:
                return new Vector3(-0.616f, -0.5f, -0.021f);
            case 4:
                return new Vector3(-0.199f, -0.5f, -0.467f);
        }
        return new Vector3(0, 0, 0);
    }

    public void spread()
    {
        int coOrdinatesX, coOrdinatesY;
        coOrdinatesX = this.GetComponent<HasHouse>().coOrdinatesX;
        coOrdinatesY = this.GetComponent<HasHouse>().coOrdinatesY;

        int spreadLocX, spreadLocY;
        spreadLocX = Random.Range(1, 4) - 2;
        spreadLocY = Random.Range(1, 4) - 2;

        if (spreadLocX == 0 && spreadLocY == 0)
        {
            return;
        }

        GameObject spreadTile;

        if ((coOrdinatesX + spreadLocX < manager.GetComponent<HexSpawner>().mapSize && coOrdinatesX + spreadLocX > 0) && (coOrdinatesY + spreadLocY < manager.GetComponent<HexSpawner>().mapSize && coOrdinatesY + spreadLocY > 0))
        {
            spreadTile = manager.GetComponent<HexSpawner>().hexTiles[coOrdinatesX + spreadLocX, coOrdinatesY + spreadLocY];

            if (spreadTile.GetComponent<TreePopulatorNew>().population == 0)
            {
                spreadTile.GetComponent<TreePopulatorNew>().treeType = treeType;
            }
            else if (spreadTile.GetComponent<TreePopulatorNew>().treeType != treeType)
            {
                return;
            }


            if (spreadTile.GetComponent<HasHouse>().type == 3 && spreadTile.GetComponent<HasHouse>().hasHouse == false)
            {
                if (spreadTile.GetComponent<TreePopulatorNew>().population == 0)
                {
                    spreadTile.GetComponent<TreePopulatorNew>().spawnFirstTree();
                    
                }
                else if(spreadTile.GetComponent<TreePopulatorNew>().population < 5)
                {
                    spreadTile.GetComponent<TreePopulatorNew>().treeSpawner();
                }
            }
            else
            {
                return;
            }
        }
        else
        {
            return;
        }

    }
}
