using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexSpawner : MonoBehaviour
{
    public Material[] hexMaterials = new Material[5];
    public GameObject Hex;
    public GameObject[] Rocks = new GameObject[5];
    public GameObject waterIce;
    public GameObject fallenTree;
    public int mapSize = 80;
    public GameObject[,] hexTiles = new GameObject[80, 80];
    public GameObject[] hexTilesDebg = new GameObject[19600];
    private int newNoise, newNoise2, newNoise3, newNoise4;
    private int textureNo;
    public GameObject hexParent;
    public bool generateOnStart;
    // Start is called before the first frame update

    public void SpawnHexes()
    {
        int counter = 0;
        newNoise = randomSeed();
        newNoise2 = randomSeed();
        newNoise3 = randomSeed();
        newNoise4 = randomSeed();

        for (int i = 0; i < mapSize; i++)
        {
            for (int j = 0; j < mapSize; j++)
            {

                int positionX = Mathf.Abs(j) % 2;

                float modifierY = perlin(i, j) - 8.7f;
                float orig = modifierY;
                //float modifierY = 6 * (Mathf.Sin(Mathf.PerlinNoise((i + newNoise) / 10f, (j + newNoise) / 10f)));

                if (modifierY < 0.6f)
                {
                    modifierY = 0.9f;
                    textureNo = 0;
                }
                else if (modifierY < 1.0f)
                {
                    modifierY = 0.95f;
                    textureNo = 1;
                }
                else if (modifierY < 1.5f)
                {
                    textureNo = 2;
                }
                else if (modifierY < 4.0f)
                {
                    textureNo = 3;
                }
                else
                {
                    textureNo = 4;
                }
                //hexTiles[i, j] = (GameObject)Instantiate(Hex, new Vector3(((i * 2) + positionX) - mapSize, modifierY, 0.0f + (j * 1.73205f) - mapSize), Quaternion.identity); //Horrible maths for where the edges of hexes meet, cause it's never easy.
                hexTilesDebg[counter] = (GameObject)Instantiate(Hex, new Vector3(((i * 2) + positionX) - mapSize, modifierY, 0.0f + (j * 1.73205f) - mapSize), Quaternion.identity);
                hexTilesDebg[counter].GetComponent<TreePopulatorNew>().manager = GameObject.Find("Observer");
                hexTilesDebg[counter].GetComponent<HasHouse>().manager = GameObject.Find("Observer");
                hexTilesDebg[counter].transform.parent = hexParent.transform;
                hexTilesDebg[counter].transform.Find("Cylinder.001").GetComponent<Renderer>().material = hexMaterials[textureNo];
                hexTilesDebg[counter].GetComponent<HasHouse>().setCoords(i, j);
                hexTilesDebg[counter].GetComponent<HasHouse>().type = textureNo;

                

                if (modifierY > 2.4 && modifierY < 2.5)
                {
                    int random = Random.Range(0, 10);
                    if (random >= 1 && random < 3)
                    {
                        hexTilesDebg[counter].GetComponent<HasHouse>().buildHouse();
                    }
                    else if (random > 2 && random < 6)
                    {
                        //Debug.Log("Spawning tree");
                        hexTilesDebg[counter].GetComponent<TreePopulatorNew>().treeType = (Random.Range(1, 4) - 1);
                        hexTilesDebg[counter].GetComponent<TreePopulatorNew>().spawnFirstTree();
                    }
                    else if (random > 5 && random < 7)
                    {
                        GameObject fTree = Instantiate(fallenTree, hexTilesDebg[counter].transform); ;
                        fTree.transform.parent = hexTilesDebg[counter].transform;
                        fTree.transform.Rotate(0, Random.Range(0, 360), 0);
                        hexTilesDebg[counter].GetComponent<TreePopulatorNew>().treeType = 10;
                    }
                }
                else if (modifierY > 5.0f)
                {
                    int rnd = Random.Range(0, 5);
                    int rnd2 = Random.Range(0, 5);
                    if (rnd < 2)
                    {
                        GameObject rock = Instantiate(Rocks[rnd2], hexTilesDebg[counter].transform); ;
                        rock.transform.parent = hexTilesDebg[counter].transform;
                        rock.transform.Rotate(0, 0, Random.Range(0, 360));
                    }
                }
                else if (orig < 0.6f && orig > 0.2f)
                {
                    int rnd = Random.Range(0, 6);
                    if (rnd < 2)
                    {
                        GameObject Ice = Instantiate(waterIce, hexTilesDebg[counter].transform); ;
                        Ice.transform.parent = hexTilesDebg[counter].transform;
                        Ice.transform.Rotate(0, Random.Range(0, 360), 0);
                    }
                }
                counter++;

            }
        }


    }


    public void spawnByButton()
    {
        deleteTiles();
        this.GetComponent<TreeHandler>().treeTiles.Clear();
        this.GetComponent<TreeHandler>().counter = 1;
        SpawnHexes();
        populateArray();

        for (int i = 0; i < (mapSize * mapSize); i++)
        {
            hexTiles[i / mapSize, i % mapSize] = hexTilesDebg[i];
        }
        callAllHouses();
    }

    private void Start()
    {
        if (generateOnStart == true)
        {
            deleteTiles();
            this.GetComponent<TreeHandler>().counter = 1;
            SpawnHexes();
        }
        populateArray();

            for (int i = 0; i < (mapSize * mapSize); i++)
            {
                hexTiles[i / mapSize, i % mapSize] = hexTilesDebg[i];
            }

        callAllHouses();
    }

    //void Start()
    //{
    //    hexParent = GameObject.Find("Hexes");

    //    newNoise = randomSeed();
    //    newNoise2 = randomSeed();
    //    newNoise3 = randomSeed();
    //    newNoise4 = randomSeed();

    //    for (int i = 0; i < mapSize; i++)
    //    {
    //        for (int j = 0; j < mapSize; j++)
    //        {

    //            int positionX = Mathf.Abs(j) % 2;

    //            float modifierY = perlin(i, j) - 8.7f ;
    //            //float modifierY = 6 * (Mathf.Sin(Mathf.PerlinNoise((i + newNoise) / 10f, (j + newNoise) / 10f)));

    //            if (modifierY < 0.6f)
    //            {
    //                modifierY = 0.9f;
    //                textureNo = 0;
    //            }
    //            else if (modifierY < 1.0f)
    //            {
    //                modifierY = 0.95f;
    //                textureNo = 1;
    //            }
    //            else if (modifierY < 1.5f)
    //            {
    //                textureNo = 2;
    //            }
    //            else if (modifierY < 4.0f)
    //            {
    //                textureNo = 3;
    //            }
    //            else
    //            {
    //                textureNo = 4;
    //            }
    //            hexTiles[i, j] = (GameObject)Instantiate(Hex, new Vector3(((i*2) + positionX) - mapSize, modifierY, 0.0f + (j * 1.73205f) - mapSize) , Quaternion.identity); //Horrible maths for where the edges of hexes meet, cause it's never easy.
    //            hexTiles[i, j].transform.parent = hexParent.transform;
    //            hexTiles[i, j].transform.Find("Cylinder.001").GetComponent<Renderer>().material = hexMaterials[textureNo];
    //            hexTiles[i, j].GetComponent<HasHouse>().setCoords(i, j);
    //            hexTiles[i, j].GetComponent<HasHouse>().type = textureNo;

    //            if (modifierY > 2.4 && modifierY < 2.5)
    //            {
    //                int random = Random.Range(0, 10);
    //                if (random >= 1 && random < 3)
    //                {
    //                    hexTiles[i, j].GetComponent<HasHouse>().buildHouse();
    //                }
    //                else if (random > 2 && random < 6)
    //                {
    //                    hexTiles[i, j].GetComponent<TreePopulator>().enabled = true;
    //                    hexTiles[i, j].GetComponent<TreePopulator>().desiredPopulation = Random.Range(1,5);
    //                    hexTiles[i, j].GetComponent<TreePopulator>().treeType = Random.Range(0, 3);
    //                }
    //            }


    //        }
    //    }

    //    callAllHouses();
    //    callAllTrees();

    //}

    float perlin(int x, int y)
    {
        float output;
        output = Mathf.PerlinNoise((x + newNoise), (y + newNoise));

        output += Mathf.PerlinNoise((x + newNoise2) / 10f, (y + newNoise2) / 10f);

        output += Mathf.PerlinNoise((x + newNoise3) / 20f, (y + newNoise3) / 20f);

        output += Mathf.PerlinNoise((x + newNoise4) / 30f, (y + newNoise4) / 30f);

        output *= 6;

        return output;
    }

    int randomSeed()
    {
        int output= Random.Range(0, 10000);

        return output;
    }

    void callAllHouses()
    {
        for (int i = 0; i < mapSize; i++)
        {
            for (int j = 0; j < mapSize; j++)
            {
                if (hexTiles[i,j].GetComponent<HasHouse>().townCentre == true)
                {
                    hexTiles[i, j].GetComponent<HasHouse>().spawnSimilarHouses(hexTiles, hexTiles[i, j]);
                }
            }
        }
    }
    
    //void callAllTrees()
    //{
    //    for (int i = 0; i < mapSize; i++)
    //    {
    //        for (int j = 0; j < mapSize; j++)
    //        {
    //            if (hexTiles[i, j].GetComponent<TreePopulator>().enabled == true)
    //            {
    //                hexTiles[i, j].GetComponent<TreePopulator>().spawnInitialForest();
    //            }
    //        }
    //    }
    //}
    
    public void populateArray()
    {
        int counter = 0;
        foreach (Transform child in hexParent.transform)
        {
            hexTilesDebg[counter] = child.gameObject;
            counter++;
        }
    }

    public void deleteTiles()
    {
        populateArray();
        this.GetComponent<TreeHandler>().treeTiles.Clear();
        foreach (GameObject a in hexTilesDebg)
        {
            GameObject.DestroyImmediate(a);
        }
    }
}
