using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMountains : MonoBehaviour
{
    //public GameObject spawnManager;
    public GameObject mountainPrefab;
    //public List<Transform> spawnLocations;
    
    // Start is called before the first frame update
    void Start()
    {
        //GetComponentInParent<PointsGenerator>().GeneratePoints(100, 200, 75);
        //SummonMountains();
        //GetComponentInParent<SpawnFirePlace>().SpawnSequence();
    }

    //void PopulateList()
    //{
    //    List<Transform> tempList = new List<Transform>(spawnManager.GetComponentsInChildren<Transform>());
    //    foreach (Transform ChildT in tempList)
    //    {
    //        if (ChildT.gameObject.transform.parent != null)
    //        {
    //            spawnLocations.Add(ChildT);
    //        }
    //    }
    //}

    public void SummonMountains()
    {
        int modifierX, modifierY, modifierZ;
        List<Vector3> summonAreas = GetComponentInParent<PointsGenerator>().outSide;
        foreach (Vector3 MountainPosition in summonAreas)
        {
            modifierX = Random.Range(-12, 12);
            modifierY = Random.Range(12, 24);
            modifierZ = Random.Range(-12, 12);
            Instantiate(mountainPrefab, new Vector3(MountainPosition.x + modifierX, MountainPosition.y + modifierY, MountainPosition.z + modifierX), rotation());
        }
    }

    Quaternion rotation()
    {
        Quaternion rotAm = new Quaternion();
        rotAm.x = 0;
        rotAm.z = 0;
        int tempY = Random.Range(0, 360);
        //Debug.Log(tempY);
        rotAm.y = tempY;
        return rotAm;
    }
    
}
